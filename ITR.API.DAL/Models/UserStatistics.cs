using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITR.API.DAL.Models
{
    public class UserStatistics
    {
        public int NumOfCourses { get; set; }
        public int NumOfFreeCourses { get; set; }
        public int NumOfPaidCourses { get; set; }
    }
}
