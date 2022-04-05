using MyEdu.Data.Contexts;
using MyEdu.Data.Repositories.Interfaces;
using MyEdu.Domain.Entities.Lessons;

namespace MyEdu.Data.Repositories.Services
{
    public class LessonRepository : GenericRepository<Lesson>, ILessonRepository
    {
        public LessonRepository(EducationCenterDbContext dbContext) : base(dbContext)
        {
        }
    }
}