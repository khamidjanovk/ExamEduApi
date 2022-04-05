using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyEdu.Data.Contexts;
using MyEdu.Data.Repositories.Interfaces;
using MyEdu.Domain.Entities.Users;

namespace MyEdu.Data.Repositories.Services
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(EducationCenterDbContext dbContext) : base(dbContext)
        {
        }
    }
}