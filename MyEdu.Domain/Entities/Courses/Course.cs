using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEdu.Domain.Entities.Courses
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public CourseType CourseType { get; set; }
        public string Author { get; set; }
        public string ImageUrl { get; set; }
        public int ViewCount { get; set; } = 0;
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public string[] LearnAbout { get; set; }

    }
}
