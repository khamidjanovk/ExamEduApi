using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using MyEdu.Data.Repositories.Interfaces;
using MyEdu.Domain.Common;
using MyEdu.Domain.Configurations;
using MyEdu.Domain.Entities.Users;
using MyEdu.Service.DTOs;
using MyEdu.Service.Extensions;
using MyEdu.Service.Helpers;
using MyEdu.Service.Interfaces;

namespace MyEdu.Service.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IWebHostEnvironment env;
        private readonly IConfiguration config;
        private readonly HttpContextHelper httpContext;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment env, IConfiguration config)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.env = env;
            this.config = config;
            httpContext = new HttpContextHelper();
        }
        
        public async Task<BaseResponse<User>> CreateAsync(UserDto userDto)
        {
            var response = new BaseResponse<User>();

            // check for user
            var existStudent = await unitOfWork.Users.GetAsync(p => p.PhoneNumber == userDto.PhoneNumber);
            if (existStudent is not null)
            {
                response.Error = new ErrorResponse(400, "User is exist");
                return response;
            }

            // create after checking success
            var mappedUser = mapper.Map<User>(userDto);

            var result = await unitOfWork.Users.CreateAsync(mappedUser);

            await unitOfWork.SaveChangesAsync();

            response.Data = result;

            return response;
        }

        public async Task<BaseResponse<User>> GetAsync(Expression<Func<User, bool>> expression)
        {
            var response = new BaseResponse<User>();

            var student = await unitOfWork.Users.GetAsync(expression);
            if (student is null)
            {
                response.Error = new ErrorResponse(404, "User not found");
                return response;
            }

            response.Data = student;

            return response;
        }

        public async Task<BaseResponse<IEnumerable<User>>> GetAllAsync(PaginationParams @params, Expression<Func<User, bool>> expression = null)
        {
            var response = new BaseResponse<IEnumerable<User>>();

            var students = await unitOfWork.Users.GetAllAsync(expression);

            response.Data = students.ToPagedList(@params);

            return response;
        }

        public async Task<BaseResponse<bool>> DeleteAsync(Expression<Func<User, bool>> expression)
        {
            var response = new BaseResponse<bool>();

            // check for exist student
            var existUser = await unitOfWork.Users.GetAsync(expression);
            if (existUser is null)
            {
                response.Error = new ErrorResponse(404, "User not found");
                return response;
            }

            var result = await unitOfWork.Users.UpdateAsync(existUser);

            await unitOfWork.SaveChangesAsync();

            response.Data = true;

            return response;
        }

        public async Task<BaseResponse<User>> UpdateAsync(long id, UserDto userDto)
        {
            var response = new BaseResponse<User>();

            var result = await unitOfWork.Users.GetAsync(p => p.Id == id);

            if (result is null)
            {
                response.Error = new ErrorResponse(404, "User not found");
                return response;
            }
            
            var user = mapper.Map<User>(userDto);
            user.Id = id;
            user.Update();

            await unitOfWork.Users.UpdateAsync(user);

            await unitOfWork.SaveChangesAsync();

            response.Data = user;

            return response;
        }

    }
}