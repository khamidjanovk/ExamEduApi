using System.Threading.Tasks;

namespace MyEdu.Data.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        ICourseRepository Courses { get; }
        IUserRepository Users { get; }
        Task SaveChangesAsync();
    }
}