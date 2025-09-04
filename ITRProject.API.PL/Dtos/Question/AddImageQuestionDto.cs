using ITR.API.DAL.Models;
using System.ComponentModel.DataAnnotations;

namespace ITRProject.API.PL.Dtos.Question
{
    public class AddImageQuestionDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Type is required")]
        public QuestionType Type { get; set; }

        public string? Content { get; set; }

        [Required(ErrorMessage = "ContentImage is required")]
        public IFormFile ContentImage { get; set; }

        [Required(ErrorMessage = "ExamId is required")]
        public int ExamId { get; set; }

        public ICollection<Choice>? Choices { get; set; }   // Indexed binding
        public string? ChoicesJson { get; set; }            // JSON string binding
    }
}
