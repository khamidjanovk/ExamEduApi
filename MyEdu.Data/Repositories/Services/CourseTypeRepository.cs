using MyEdu.Data.Contexts;
using MyEdu.Data.Repositories.Interfaces;
using MyEdu.Domain.Entities.Courses;

namespace MyEdu.Data.Repositories.Services
{
    public class CourseTypeRepository : GenericRepository<CourseType>, ICourseTypeRepository
    {
        public CourseTypeRepository(EducationCenterDbContext dbContext) : base(dbContext)
        {
        }

    }
}
