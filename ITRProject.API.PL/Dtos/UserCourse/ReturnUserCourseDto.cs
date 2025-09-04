using ITR.API.DAL.Models;
using System.ComponentModel.DataAnnotations;

namespace ITRProject.API.PL.Dtos.UserCourse
{
    public class ReturnUserCourseDto
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public string CourseDescription { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public Activation CourseState { get; set; }
        public CourseType CourseType { get; set; }
        public int? CoursePrice { get; set; }
        public string CourseImageUrl { get; set; }


    }
}
