using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using MyEdu.Data.Repositories.Interfaces;
using MyEdu.Domain.Common;
using MyEdu.Domain.Configurations;
using MyEdu.Domain.Entities.Courses;
using MyEdu.Service.DTOs;
using MyEdu.Service.Extensions;
using MyEdu.Service.Helpers;
using MyEdu.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyEdu.Service.Services
{
    public class CourseTypeService : ICourseTypeService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IWebHostEnvironment env;
        private readonly IConfiguration config;
        private readonly HttpContextHelper httpContext;

        public CourseTypeService(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment env, IConfiguration config)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.env = env;
            this.config = config;
            httpContext = new HttpContextHelper();
        }

        public async Task<BaseResponse<CourseType>> CreateAsync(CourseTypeDto courseTypeDto)
        {
            var response = new BaseResponse<CourseType>();

            // check for course type
            var existCourseType = await unitOfWork.CourseTypes.GetAsync(p => p.Name == courseTypeDto.Name);
            if (existCourseType is not null)
            {
                response.Error = new ErrorResponse(400, "Course type is exist");
                return response;
            }

            // create after checking success
            var mappedCourseType = mapper.Map<CourseType>(courseTypeDto);



            var result = await unitOfWork.CourseTypes.CreateAsync(mappedCourseType);

            await unitOfWork.SaveChangesAsync();

            response.Data = result;

            return response;
        }

        public async Task<BaseResponse<bool>> DeleteAsync(Expression<Func<CourseType, bool>> expression)
        {
            var response = new BaseResponse<bool>();

            // check for exist student
            var existCourseType = await unitOfWork.CourseTypes.GetAsync(expression);
            if (existCourseType is null)
            {
                response.Error = new ErrorResponse(404, "Course not found");
                return response;
            }

            var result = await unitOfWork.CourseTypes.UpdateAsync(existCourseType);

            await unitOfWork.SaveChangesAsync();

            response.Data = true;

            return response;
        }

        public async Task<BaseResponse<IQueryable<CourseType>>> GetAllAsync(PaginationParams @params, Expression<Func<CourseType, bool>> expression = null)
        {
            var response = new BaseResponse<IQueryable<CourseType>>();

            var courseTypes = await unitOfWork.CourseTypes.GetAllAsync(expression);

            response.Data = courseTypes.ToPagedList(@params);

            return response;
        }

        public async Task<BaseResponse<CourseType>> GetAsync(Expression<Func<CourseType, bool>> expression)
        {
            var response = new BaseResponse<CourseType>();

            var courseType = await unitOfWork.CourseTypes.GetAsync(expression);
            if (courseType is null)
            {
                response.Error = new ErrorResponse(404, "Course not found");
                return response;
            }

            response.Data = courseType;

            return response;
        }

        public async Task<BaseResponse<CourseType>> UpdateAsync(long id, CourseTypeDto courseTypeDto)
        {
            var response = new BaseResponse<CourseType>();

            var courseType = await unitOfWork.CourseTypes.GetAsync(p => p.Id == id);

            courseType.Name = courseTypeDto.Name;

            var result = await unitOfWork.CourseTypes.UpdateAsync(courseType);

            await unitOfWork.SaveChangesAsync();
            
            if (result is null)
            {
                response.Error = new ErrorResponse(404, "User not found");
                return response;
            }

            response.Data = result;

            return response;
        }
    }
}
