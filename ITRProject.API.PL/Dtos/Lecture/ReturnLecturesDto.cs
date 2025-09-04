using ITR.API.DAL.Models;

namespace ITRProject.API.PL.Dtos.Lecture
{
    public class ReturnLecturesDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
        public Activation State { get; set; }
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public string? Image { get; set; }
    }
}
