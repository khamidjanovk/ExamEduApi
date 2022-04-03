using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MyEdu.Data.Contexts;
using MyEdu.Data.Repositories.Interfaces;

namespace MyEdu.Data.Repositories.Services
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly EducationCenterDbContext context;
        private readonly IConfiguration config;
        
        public ICourseRepository Courses { get; private set; }
        public IUserRepository Users { get; private set; }

        public UnitOfWork(EducationCenterDbContext context, IConfiguration config)
        {
            this.config = config;
            this.context = context;
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