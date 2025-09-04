using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITR.API.DAL.Models
{
    public class ExamResults
    {
        public string UserId { get; set; }
        public int ExamId { get; set; }
        public DateTime StartSolution { get; set; }
        public DateTime EndSolution { get; set; }
        public int Result { get; set; }
        public int CorrectAnswer { get; set; }
        public int NumberOfQuestion { get; set; }

        public ApplicationUser User { get; set; }
       
        public Exam Exam { get; set; }
    }
}
