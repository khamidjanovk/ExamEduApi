using MyEdu.Domain.Entities.Parts;

namespace MyEdu.Domain.Entities.Lessons
{
    public class Lesson
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Length { get; set; }
        public string VideoUrl { get; set; }
        public Part Part { get; set; }
    }
}