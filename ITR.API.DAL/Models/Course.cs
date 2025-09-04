using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITR.API.DAL.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Activation State { get; set; }
        public CourseType Type { get; set; }
        public int? Price { get; set; }
        public string ImageUrl { get; set; }

        public ICollection<Exam> Exams { get; set; } = new List<Exam>();
    }
}
