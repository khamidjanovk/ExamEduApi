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
        public CourseRepository(EducationCenterDbContext dbContext) : base(dbContext)
        {
        }
    }
}