using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITR.API.DAL.Models
{
    public class Question
    {
        public int Id { get; set; }
        public QuestionType Type { get; set; }
        public string Content { get; set; }

        public int ExamId { get; set; }
        public Exam exam { get; set; }

        public ICollection<Choice> Choices { get; set; }
    }
}