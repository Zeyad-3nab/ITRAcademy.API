using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITR.API.DAL.Models
{
    public class UserCourse
    {
        public string UserId { get; set; }
        public int CourseId { get; set; }
        public UserCourseState State { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }

        public ApplicationUser User { get; set; }
        public Course Course { get; set; }

    }
}
