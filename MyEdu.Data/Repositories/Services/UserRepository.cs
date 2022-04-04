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
        private readonly IMapper mapper;
        
        public UserRepository(EducationCenterDbContext dbContext, IMapper mapper) : base(dbContext)
        {
            this.mapper = mapper;
        }
        
        public new async Task<User> UpdateAsync(User user)
        {
            var result = await dbSet.FirstOrDefaultAsync(x => x.Id == user.Id);

            result = mapper.Map(user, result);

            dbContext.Entry(user).State = EntityState.Modified;

            return result;
        }
    }
}