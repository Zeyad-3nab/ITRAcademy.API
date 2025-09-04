using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITR.API.DAL.Models
{
    public class Exam
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public int QuestionDegree { get; set; }
        public int Duration { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public Activation State { get; set; }
        public bool ForAll { get; set; }
        public int? CourseId { get; set; }
        public Course? Course { get; set; }
        public ICollection<Question> Questions { get; set; }

    }
}
