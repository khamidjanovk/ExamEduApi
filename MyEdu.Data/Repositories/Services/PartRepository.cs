using MyEdu.Data.Contexts;
using MyEdu.Data.Repositories.Interfaces;
using MyEdu.Domain.Entities.Parts;

namespace MyEdu.Data.Repositories.Services
{
    public class PartRepository : GenericRepository<Part>, IPartRepository
    {
        public PartRepository(EducationCenterDbContext dbContext) : base(dbContext)
        {
        }
    }
}