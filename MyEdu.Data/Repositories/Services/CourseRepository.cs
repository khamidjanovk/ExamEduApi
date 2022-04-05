using MyEdu.Data.Contexts;
using MyEdu.Data.Repositories.Interfaces;
using MyEdu.Domain.Entities.Courses;

namespace MyEdu.Data.Repositories.Services
{
    public class CourseRepository : GenericRepository<Course>, ICourseRepository
    {
        public CourseRepository(EducationCenterDbContext dbContext) : base(dbContext)
        {
        }
    }
}