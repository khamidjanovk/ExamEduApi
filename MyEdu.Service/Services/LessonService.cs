using MyEdu.Domain.Common;
using MyEdu.Domain.Configurations;
using MyEdu.Domain.Entities.Lessons;
using MyEdu.Service.DTOs;
using MyEdu.Service.Interfaces;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyEdu.Service.Services
{
    public class LessonService : ILessonService
    {
        public Task<BaseResponse<Lesson>> CreateAsync(LessonDto lessonDto)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<bool>> DeleteAsync(Expression<Func<Lesson, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<IQueryable<Lesson>>> GetAllAsync(PaginationParams @params, Expression<Func<Lesson, bool>> expression = null)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<Lesson>> GetAsync(Expression<Func<Lesson, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<Lesson>> UpdateAsync(long id, LessonDto lessonDto)
        {
            throw new NotImplementedException();
        }
    }
}