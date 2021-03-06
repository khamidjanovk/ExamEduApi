using MyEdu.Domain.Common;
using MyEdu.Domain.Configurations;
using MyEdu.Domain.Entities.Users;
using MyEdu.Service.DTOs;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyEdu.Service.Interfaces
{
    public interface IUserService
    {
        Task<BaseResponse<User>> CreateAsync(UserDto userDto);
        Task<BaseResponse<User>> GetAsync(Expression<Func<User, bool>> expression);
        Task<BaseResponse<IQueryable<User>>> GetAllAsync(PaginationParams @params, Expression<Func<User, bool>> expression = null);
        Task<BaseResponse<bool>> DeleteAsync(Expression<Func<User, bool>> expression);
        Task<BaseResponse<User>> UpdateAsync(long id, UserDto userDto);
    }
}