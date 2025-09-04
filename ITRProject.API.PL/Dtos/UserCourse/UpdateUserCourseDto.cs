using ITR.API.DAL.Models;
using System.ComponentModel.DataAnnotations;

namespace ITRProject.API.PL.Dtos.UserCourse
{
    public class UpdateUserCourseDto
    {
        [Required(ErrorMessage = "UserId is required")]
        public string UserId { get; set; }

        [Required(ErrorMessage = "CourseId is required")]
        public int CourseId { get; set; }

        [Required(ErrorMessage = "StartTime is required")]
        public DateTime StartTime { get; set; }

        [Required(ErrorMessage = "EndTime is required")]
        public DateTime EndTime { get; set; }
    }
}
