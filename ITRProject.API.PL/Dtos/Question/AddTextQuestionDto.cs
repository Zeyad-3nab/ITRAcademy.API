using ITR.API.DAL.Models;
using System.ComponentModel.DataAnnotations;

namespace ITRProject.API.PL.Dtos.Question
{
    public class AddTextQuestionDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Type is required")]
        public QuestionType Type { get; set; }

        [Required(ErrorMessage = "Content is required")]
        public string Content { get; set; }

        [Required(ErrorMessage = "ExamId is required")]
        public int ExamId { get; set; }

        public ICollection<Choice> Choices { get; set; }
    }
}
