using Microsoft.EntityFrameworkCore;

namespace MyEdu.Data.Contexts
{
    public class EducationCenterDbContext : DbContext
    {
        public EducationCenterDbContext(DbContextOptions<EducationCenterDbContext> options) : base(options)
        {
            
        }
    }
}
