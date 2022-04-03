using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using MyEdu.Data.Repositories.Interfaces;
using MyEdu.Domain.Common;
using MyEdu.Domain.Configurations;
using MyEdu.Domain.Entities.Courses;
using MyEdu.Domain.Entities.Users;
using MyEdu.Service.DTOs;
using MyEdu.Service.Helpers;
using MyEdu.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
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
        private readonly HttpContextHelper httpContext;

        public CourseService(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment env, IConfiguration config)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.env = env;
            this.config = config;
            httpContext = new HttpContextHelper();
        }
        public async Task<BaseResponse<Course>> CreateAsync(CourseDto courseDto)
        {
            var response = new BaseResponse<Course>();

            // check for course
            var existStudent = await unitOfWork.Courses.GetAsync(p => p.Name == courseDto.Name);
            if (existStudent is not null)
            {
                response.Error = new ErrorResponse(400, "User is exist");
                return response;
            }

            // create after checking success
            var mappedCourse = mapper.Map<Course>(courseDto);

            var result = await unitOfWork.Courses.CreateAsync(mappedCourse);

            await unitOfWork.SaveChangesAsync();

            response.Data = result;

            return response;
        }

        public Task<BaseResponse<bool>> DeleteAsync(Expression<Func<User, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<IEnumerable<Course>>> GetAllAsync(PaginationParams @params, Expression<Func<User, bool>> expression = null)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<Course>> GetAsync(Expression<Func<Course, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public async Task<string> SaveFileAsync(Stream file, string fileName)
        {
            fileName = Guid.NewGuid().ToString("N") + "_" + fileName;
            string storagePath = config.GetSection("Storage:ImageUrl").Value;
            string filePath = Path.Combine(env.WebRootPath, $"{storagePath}/{fileName}");
            FileStream mainFile = File.Create(filePath);
            await file.CopyToAsync(mainFile);
            mainFile.Close();

            return fileName;
        }

        public Task<BaseResponse<User>> UpdateAsync(long id, UserDto studentDto)
        {
            throw new NotImplementedException();
        }
    }
}