using ITR.API.DAL.Models;
using System.ComponentModel.DataAnnotations;

namespace ITRProject.API.PL.Dtos.Exam
{
    public class ExamDto
    {
        public int Id { get; set; }


        [Required(ErrorMessage ="Title is required")]
        public string Title { get; set; } = string.Empty;


        [Required(ErrorMessage = "QuestionDegree is required")]
        public int QuestionDegree { get; set; }


        [Required(ErrorMessage = "Duration is required")]
        public int Duration { get; set; }


        [Required(ErrorMessage = "StartTime is required")]
        public DateTime StartTime { get; set; }


        [Required(ErrorMessage = "EndTime is required")]
        public DateTime EndTime { get; set; }

        [Required(ErrorMessage = "ForAll is required")]
        public bool ForAll { get; set; }

        [Required(ErrorMessage = "State is required")]
        public Activation State { get; set; }

        public int CourseId { get; set; }
    }
}