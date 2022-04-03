using Microsoft.Extensions.DependencyInjection;
using MyEdu.Data.Repositories.Interfaces;
using MyEdu.Data.Repositories.Services;
using MyEdu.Service.Interfaces;
using MyEdu.Service.Services;

namespace MyEdu.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddCustomServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICourseService, CourseService>();
            services.AddScoped<ILessonService, LessonService>();
            services.AddScoped<IPartService, PartService>();
        }
    }
}
