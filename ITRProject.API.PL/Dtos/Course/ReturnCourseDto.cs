using ITR.API.DAL.Models;
using System.ComponentModel.DataAnnotations;

namespace ITRProject.API.PL.Dtos.Course
{
    public class ReturnCourseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Activation State { get; set; }
        public CourseType Type { get; set; }
        public int? Price { get; set; }
        public string? ImageUrl { get; set; }
    }
}
