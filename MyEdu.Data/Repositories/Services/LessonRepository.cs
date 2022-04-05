using Microsoft.EntityFrameworkCore;
using MyEdu.Data.Contexts;
using MyEdu.Data.Repositories.Interfaces;
using MyEdu.Domain.Entities.Lessons;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyEdu.Data.Repositories.Services
{
    public class LessonRepository : GenericRepository<Lesson>, ILessonRepository
    {
        public LessonRepository(EducationCenterDbContext dbContext) : base(dbContext)
        {
        }
    }
}