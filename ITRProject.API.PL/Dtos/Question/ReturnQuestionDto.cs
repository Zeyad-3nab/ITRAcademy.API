using ITR.API.DAL.Models;

namespace ITRProject.API.PL.Dtos.Question
{
    public class ReturnQuestionDto
    {
        public int Id { get; set; }
        public QuestionType Type { get; set; }
        public string Content { get; set; }
        public int ExamId { get; set; }
        public ICollection<Choice> Choices { get; set; }
    }
}
