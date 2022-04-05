using Microsoft.AspNetCore.Http;
using System;

namespace MyEdu.Service.DTOs
{
    public class CourseDto
    {
        public string Name { get; set; }
        public long CourseTypeId { get; set; }
        public string Author { get; set; }
        public IFormFile Image { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public string[] LearnAbout { get; set; }
    }
}
