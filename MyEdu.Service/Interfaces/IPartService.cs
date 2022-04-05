using MyEdu.Domain.Common;
using MyEdu.Domain.Configurations;
using MyEdu.Domain.Entities.Parts;
using MyEdu.Service.DTOs;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyEdu.Service.Interfaces
{
    public interface IPartService
    {
        Task<BaseResponse<Part>> CreateAsync(PartDto partDto);
        Task<BaseResponse<Part>> GetAsync(Expression<Func<Part, bool>> expression);
        Task<BaseResponse<IQueryable<Part>>> GetAllAsync(PaginationParams @params, Expression<Func<Part, bool>> expression = null);
        Task<BaseResponse<bool>> DeleteAsync(Expression<Func<Part, bool>> expression);
        Task<BaseResponse<Part>> UpdateAsync(long id, PartDto partDto);
    }
}