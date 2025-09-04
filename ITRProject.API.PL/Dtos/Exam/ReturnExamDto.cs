using ITR.API.DAL.Models;

namespace ITRProject.API.PL.Dtos.Exam
{
    public class ReturnExamDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public int QuestionDegree { get; set; }
        public int Duration { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public Activation State { get; set; }
        public int CourseId { get; set; }
        public string CourseName { get; set; }
    }
}
