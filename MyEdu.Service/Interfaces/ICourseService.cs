using MyEdu.Domain.Common;
using MyEdu.Domain.Configurations;
using MyEdu.Domain.Entities.Courses;
using MyEdu.Domain.Entities.Users;
using MyEdu.Service.DTOs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyEdu.Service.Interfaces
{
    public interface ICourseService
    {
        Task<BaseResponse<Course>> CreateAsync(CourseDto userDto);
        Task<BaseResponse<Course>> GetAsync(Expression<Func<Course, bool>> expression);
        Task<BaseResponse<IQueryable<Course>>> GetAllAsync(PaginationParams @params, Expression<Func<Course, bool>> expression = null);
        Task<BaseResponse<bool>> DeleteAsync(Expression<Func<Course, bool>> expression);
        Task<BaseResponse<Course>> UpdateAsync(long id, CourseDto courseDto);
        Task<string> SaveFileAsync(Stream file, string fileName);
    }
}