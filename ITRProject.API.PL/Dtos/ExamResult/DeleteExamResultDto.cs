using System.ComponentModel.DataAnnotations;

namespace ITRProject.API.PL.Dtos.ExamResult
{
    public class DeleteExamResultDto
    {
        [Required(ErrorMessage = "UserId is required")]
        public string UserId { get; set; }


        [Required(ErrorMessage = "ExamId is required")]
        public int ExamId { get; set; }
    }
}
