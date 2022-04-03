using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MyEdu.Domain.Common;
using MyEdu.Domain.Configurations;
using MyEdu.Domain.Entities.Users;
using MyEdu.Service.DTOs;

namespace MyEdu.Service.Interfaces
{
    public interface IUserService
    {
        Task<BaseResponse<User>> CreateAsync(UserDto userDto);
        Task<BaseResponse<User>> GetAsync(Expression<Func<User, bool>> expression);
        Task<BaseResponse<IEnumerable<User>>> GetAllAsync(PaginationParams @params, Expression<Func<User, bool>> expression = null);
        Task<BaseResponse<bool>> DeleteAsync(Expression<Func<User, bool>> expression);
        Task<BaseResponse<User>> UpdateAsync(long id, UserDto studentDto);
    }
}