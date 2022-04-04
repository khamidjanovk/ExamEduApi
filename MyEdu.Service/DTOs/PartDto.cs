using MyEdu.Domain.Entities.Lessons;
using System.Collections.Generic;

namespace MyEdu.Service.DTOs
{
    public class PartDto
    {
        public string Name { get; set; }
        public ICollection<Lesson> Lessons { get; set; }
    }
}