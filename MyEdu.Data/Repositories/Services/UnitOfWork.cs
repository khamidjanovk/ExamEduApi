using Microsoft.Extensions.Configuration;
using MyEdu.Data.Contexts;
using MyEdu.Data.Repositories.Interfaces;
using System;
using System.Threading.Tasks;

namespace MyEdu.Data.Repositories.Services
{
    public class UnitOfWork : IUnitOfWork,  IDisposable
    {
        private readonly EducationCenterDbContext context;
        private readonly IConfiguration config;

        public ICourseRepository Courses { get; private set; }

        public IUserRepository Users { get; private set; }

        public ILessonRepository Lessons { get; private set; }

        public IPartRepository Parts { get; private set; }

        public ICourseTypeRepository CourseTypes { get; private set; }

        public UnitOfWork(EducationCenterDbContext context, IConfiguration config)
        {
            this.config = config;
            this.context = context;

            Courses = new CourseRepository(context);
            Users = new UserRepository(context);
            CourseTypes = new CourseTypeRepository(context);
            Lessons = new LessonRepository(context);
            Parts = new PartRepository(context);
        }

        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}