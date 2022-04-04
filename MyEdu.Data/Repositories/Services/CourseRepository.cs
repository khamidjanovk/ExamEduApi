using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyEdu.Data.Contexts;
using MyEdu.Data.Repositories.Interfaces;
using MyEdu.Domain.Entities.Courses;
using System.Threading.Tasks;

namespace MyEdu.Data.Repositories.Services
{
    public class CourseRepository : GenericRepository<Course>, ICourseRepository
    {
        private readonly IMapper mapper;
        public CourseRepository(EducationCenterDbContext dbContext, IMapper mapper) : base(dbContext)
        {
            this.mapper = mapper;
        }

        public new async Task<Course> UpdateAsync(Course course)
        {
            var result = await dbSet.FirstOrDefaultAsync(x => x.Id == course.Id);

            result = mapper.Map(course, result);

            dbContext.Entry(course).State = EntityState.Modified;

            return result;
        }
    }
}