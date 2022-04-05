using System.Collections.Generic;
using MyEdu.Domain.Entities.Lessons;

namespace MyEdu.Domain.Entities.Parts
{
    public class Part
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Lesson> Lessons { get; set; }

        public Part()
        {
            Lessons = new List<Lesson>();
        }
    }
}