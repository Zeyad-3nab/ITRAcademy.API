using System.ComponentModel.DataAnnotations;

namespace ITRProject.API.PL.Dtos.ExamResult
{
    public class UpdateExamResultDto
    {

        [Required(ErrorMessage = "UserId is required")]
        public string UserId { get; set; }


        [Required(ErrorMessage = "ExamId is required")]
        public int ExamId { get; set; }


        [Required(ErrorMessage = "StartSolution is required")]
        public DateTime StartSolution { get; set; }


        [Required(ErrorMessage = "EndSolution is required")]
        public DateTime EndSolution { get; set; }


        [Required(ErrorMessage = "Result is required")]
        public int Result { get; set; }


        [Required(ErrorMessage = "CorrectAnswer is required")]
        public int CorrectAnswer { get; set; }

        [Required(ErrorMessage = "NumberOfQuestion is required")]
        public int NumberOfQuestion { get; set; }
    }
}
