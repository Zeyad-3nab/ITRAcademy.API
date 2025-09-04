using ITR.API.DAL.Models;
using System.ComponentModel.DataAnnotations;

namespace ITRProject.API.PL.Dtos.Course
{
    public class UpdateCourseDto
    {
        [Required(ErrorMessage = "Id is required")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        [Required(ErrorMessage = "State is required")]
        public Activation State { get; set; }

        [Required(ErrorMessage = "Type is required")]
        public CourseType Type { get; set; }
        public int? Price { get; set; }
        public IFormFile? Image { get; set; }

        [Required(ErrorMessage = "ImageUrl is required")]
        public string ImageUrl { get; set; }
    }
}
