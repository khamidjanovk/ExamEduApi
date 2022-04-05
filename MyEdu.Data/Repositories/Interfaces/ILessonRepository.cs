using MyEdu.Domain.Entities.Lessons;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyEdu.Data.Repositories.Interfaces
{
    public interface ILessonRepository : IGenericRepository<Lesson>
    {
    }
}