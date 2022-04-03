using MyEdu.Data.Contexts;
using MyEdu.Domain.Entities.Lessons;

namespace MyEdu.Data.Repositories.Services
{
    public class LessonRepository : GenericRepository<Lesson>, Interfaces.ILessonRepository
    {
        public LessonRepository(EducationCenterDbContext dbContext) : base(dbContext)
        {
        }
    }
}