using System;

namespace MyEdu.Domain.Entities.Courses
{
    public class Course
    {
        public long Id { get; set; }
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
