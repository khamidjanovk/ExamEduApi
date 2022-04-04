using Microsoft.AspNetCore.Http;

namespace MyEdu.Service.DTOs
{
    public class LessonDto
    {

        public string Name { get; set; }
        public string Length { get; set; }
        public IFormFile VideoUrl { get; set; }
        public long PartId { get; set; }
    }
}