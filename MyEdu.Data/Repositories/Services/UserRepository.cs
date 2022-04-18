using Microsoft.EntityFrameworkCore;
using MyEdu.Data.Contexts;
using MyEdu.Data.Repositories.Interfaces;
using MyEdu.Domain.Entities.Users;
using System.Threading.Tasks;

namespace MyEdu.Data.Repositories.Services
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(EducationCenterDbContext dbContext) : base(dbContext)
        {

        }
    }
}