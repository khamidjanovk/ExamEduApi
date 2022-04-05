using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyEdu.Data.Contexts;
using MyEdu.Data.Repositories.Interfaces;
using MyEdu.Domain.Entities.Courses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEdu.Data.Repositories.Services
{
    public class CourseTypeRepository : GenericRepository<CourseType>, ICourseTypeRepository
    {
        public CourseTypeRepository(EducationCenterDbContext dbContext) : base(dbContext)
        {
        }

    }
}
