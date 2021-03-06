using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using MyEdu.Data.Repositories.Interfaces;
using MyEdu.Domain.Common;
using MyEdu.Domain.Configurations;
using MyEdu.Domain.Entities.Users;
using MyEdu.Service.Configurations;
using MyEdu.Service.DTOs;
using MyEdu.Service.Extensions;
using MyEdu.Service.Helpers;
using MyEdu.Service.Interfaces;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;

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
            var existUser = await unitOfWork.Users.GetAsync(p => p.PhoneNumber == userDto.PhoneNumber);
            if (existUser is not null)
            {
                response.Error = new ErrorResponse(400, "User is exist");
                return response;
            }

            // create after checking success
            var mappedUser = mapper.Map<User>(userDto);
            mappedUser.Password = HashPassword.Create(mappedUser.Password);
            var result = await unitOfWork.Users.CreateAsync(mappedUser);

            await unitOfWork.SaveChangesAsync();

            response.Data = result;

            return response;
        }

        public async Task<BaseResponse<User>> GetAsync(Expression<Func<User, bool>> expression)
        {
            var response = new BaseResponse<User>();

            var user = await unitOfWork.Users.GetAsync(expression);
            if (user is null)
            {
                response.Error = new ErrorResponse(404, "User not found");
                return response;
            }

            response.Data = user;

            Console.WriteLine(CheckInternetSpeed());

            return response;
        }

        public async Task<BaseResponse<IQueryable<User>>> GetAllAsync(PaginationParams @params, Expression<Func<User, bool>> expression = null)
        {
            var response = new BaseResponse<IQueryable<User>>();

            var users = await unitOfWork.Users.GetAllAsync(expression);

            response.Data = users.ToPagedList(@params);

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

            result = mapper.Map(userDto, result);
            result.Update();

            await unitOfWork.Users.UpdateAsync(result);

            await unitOfWork.SaveChangesAsync();

            response.Data = result;

            return response;
        }
        
        public static double CheckInternetSpeed()
        {
            // Create Object Of WebClient
            WebClient wc = new WebClient();

            //DateTime Variable To Store Download Start Time.
            DateTime dt1 = DateTime.Now;

            //Number Of Bytes Downloaded Are Stored In ‘data’
            byte[] data = wc.DownloadData("http://google.com");

            //DateTime Variable To Store Download End Time.
            DateTime dt2 = DateTime.Now;

            //To Calculate Speed in Kb Divide Value Of data by 1024 And Then by End Time Subtract Start Time To Know Download Per Second.
            return Math.Round((data.Length / 1024) / (dt2 - dt1).TotalSeconds, 2) * 0.125;
        }

    }
}