using System.ComponentModel.DataAnnotations;

namespace ITRProject.API.PL.Dtos.UserCourse
{
    public class DeleteuserCourseDto
    {

        [Required(ErrorMessage = "UserId is required")]
        public string UserId { get; set; }

        [Required(ErrorMessage = "CourseId is required")]
        public int CourseId { get; set; }
    }
}