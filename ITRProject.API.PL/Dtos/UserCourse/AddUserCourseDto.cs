using ITR.API.DAL.Models;
using System.ComponentModel.DataAnnotations;

namespace ITRProject.API.PL.Dtos.UserCourse
{
    public class AddUserCourseDto
    {
        [Required(ErrorMessage ="Course Id is Required")]
        public int CourseId { get; set; }
    }
}
