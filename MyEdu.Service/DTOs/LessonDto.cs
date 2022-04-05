using Microsoft.AspNetCore.Http;

namespace MyEdu.Service.DTOs
{
    public class LessonDto
    {
        public string VideoUrl { get; set; }
        public long PartId { get; set; }
    }
}