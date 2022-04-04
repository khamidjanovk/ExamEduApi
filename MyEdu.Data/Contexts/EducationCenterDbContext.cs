using Microsoft.EntityFrameworkCore;
using MyEdu.Domain.Entities.Courses;
using MyEdu.Domain.Entities.Lessons;
using MyEdu.Domain.Entities.Parts;
using MyEdu.Domain.Entities.Users;

namespace MyEdu.Data.Contexts
{
    public class EducationCenterDbContext : DbContext
    {
        public EducationCenterDbContext(DbContextOptions<EducationCenterDbContext> options) : base(options)
        {
            
        }

        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Part> Parts { get; set; }
        public virtual DbSet<Lesson> Lessons { get; set; }

    }
}
