using System.Threading.Tasks;

namespace MyEdu.Data.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        ICourseRepository Courses { get; }
        IUserRepository Users { get; }
        ILessonRepository Lessons { get; }
        IPartRepository Parts { get; }
        ICourseTypeRepository CourseTypes { get; }
        Task SaveChangesAsync();
    }
}