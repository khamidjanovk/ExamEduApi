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
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyEdu.Service.Services
{
    public class CourseService : ICourseService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IWebHostEnvironment env;
        private readonly IConfiguration config;
        public CourseService(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment env, IConfiguration config)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.env = env;
            this.config = config;
        }

        public async Task<BaseResponse<Course>> CreateAsync(CourseDto courseDto)
        {
            var response = new BaseResponse<Course>();

            // check for course
            var existStudent =
                await unitOfWork.Courses.GetAsync(p => p.Name == courseDto.Name);

            if (existStudent is not null)
            {
                response.Error = new ErrorResponse(400, "Course is exist");
                return response;
            }

            //check for CourseType
            var existCourseType =
                await unitOfWork.CourseTypes.GetAsync(p => p.Id == courseDto.CourseTypeId);

            var mappedCourse = mapper.Map<Course>(courseDto);

            mappedCourse.CourseType = existCourseType;

            var resultFile =
                await FileExtensions
                .SaveFileAsync
                (courseDto.Image.OpenReadStream(),
                config,
                env,
                courseDto.Image.FileName);

            mappedCourse.ImageUrl = resultFile.FileName;

            var result = await unitOfWork.Courses.CreateAsync(mappedCourse);

            await unitOfWork.SaveChangesAsync();

            result.ImageUrl = resultFile.WebUrl;

            response.Data = result;

            return response;
        }

        public async Task<BaseResponse<bool>> DeleteAsync(Expression<Func<Course, bool>> expression)
        {
            var response = new BaseResponse<bool>();

            // check for exist student
            var existCourse = await unitOfWork.Courses.GetAsync(expression);
            if (existCourse is null)
            {
                response.Error = new ErrorResponse(404, "Course not found");
                return response;
            }

            var result = await unitOfWork.Courses.UpdateAsync(existCourse);

            await unitOfWork.SaveChangesAsync();

            response.Data = true;

            return response;
        }

        public async Task<BaseResponse<IQueryable<Course>>> GetAllAsync(PaginationParams @params, Expression<Func<Course, bool>> expression = null)
        {
            var response = new BaseResponse<IQueryable<Course>>();

            var courses = await unitOfWork.Courses.GetAllAsync(expression);
            
            courses.ToList().ForEach(p => p.ImageUrl = WebUrlExtensions.GetFullWebUrl(p.ImageUrl, config));

            response.Data = courses.ToPagedList(@params);

            return response;
        }

        public async Task<BaseResponse<Course>> GetAsync(Expression<Func<Course, bool>> expression)
        {
            var response = new BaseResponse<Course>();

            var course = await unitOfWork.Courses.GetAsync(expression);
            if (course is null)
            {
                response.Error = new ErrorResponse(404, "Course not found");
                return response;
            }

            course.ImageUrl = WebUrlExtensions.GetFullWebUrl(course.ImageUrl, config);

            response.Data = course;

            return response;
        }

        public async Task<BaseResponse<Course>> UpdateAsync(long id, CourseDto courseDto)
        {
            var response = new BaseResponse<Course>();

            var course = await unitOfWork.Courses.GetAsync(p => p.Id == id);

            course.Author = courseDto.Author;
            course.Description = courseDto.Description;
            course.Name = courseDto.Name;
            course.CreatedDate = courseDto.CreatedDate;
            course.CourseType =
                await unitOfWork.CourseTypes.GetAsync(p => p.Id == courseDto.CourseTypeId);
            
            course.LearnAbout = courseDto.LearnAbout;
            
            course.ImageUrl =
                (await FileExtensions
                .SaveFileAsync(courseDto.Image.OpenReadStream(),
                config,
                env,
                courseDto.Image.FileName)).FileName;

            var result = await unitOfWork.Courses.UpdateAsync(course);

            await unitOfWork.SaveChangesAsync();

            if (result is null)
            {
                response.Error = new ErrorResponse(404, "User not found");
                return response;
            }

            course.ImageUrl = WebUrlExtensions.GetFullWebUrl(course.ImageUrl, config);

            response.Data = course;

            return response;
        }
    }
}