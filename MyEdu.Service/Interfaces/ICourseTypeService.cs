using MyEdu.Domain.Common;
using MyEdu.Domain.Configurations;
using MyEdu.Domain.Entities.Courses;
using MyEdu.Service.DTOs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyEdu.Service.Interfaces
{
    public interface ICourseTypeService
    {
        Task<BaseResponse<CourseType>> CreateAsync(CourseTypeDto courseTypeDto);
        Task<BaseResponse<CourseType>> GetAsync(Expression<Func<CourseType, bool>> expression);
        Task<BaseResponse<IQueryable<CourseType>>> GetAllAsync(PaginationParams @params, Expression<Func<CourseType, bool>> expression = null);
        Task<BaseResponse<bool>> DeleteAsync(Expression<Func<CourseType, bool>> expression);
        Task<BaseResponse<CourseType>> UpdateAsync(long id, CourseTypeDto courseTypeDto);
    }
}
