using System.Collections.Generic;
using MyEdu.Domain.Entities.Lessons;

namespace MyEdu.Domain.Entities.Parts
{
    public class Part
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public ICollection<Lesson> Lessons { get; set; }
    }
}