using MyEdu.Domain.Common;
using MyEdu.Domain.Configurations;
using MyEdu.Domain.Entities.Lessons;
using MyEdu.Service.DTOs;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyEdu.Service.Interfaces
{
    public interface ILessonService
    {
        Task<BaseResponse<Lesson>> CreateAsync(LessonDto lessonDto);
        Task<BaseResponse<Lesson>> GetAsync(Expression<Func<Lesson, bool>> expression);
        Task<BaseResponse<IQueryable<Lesson>>> GetAllAsync(PaginationParams @params, Expression<Func<Lesson, bool>> expression = null);
        Task<BaseResponse<bool>> DeleteAsync(Expression<Func<Lesson, bool>> expression);
        Task<BaseResponse<Lesson>> UpdateAsync(long id, LessonDto lessonDto);
    }
}