using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITR.API.DAL.Models
{
    public class AdminStatistics
    {
        public int NumOfCourses { get; set; }
        public int NumOfUsers { get; set; }
        public int NumOfAdmins { get; set; }
        public int NumOfRejectedOrders { get; set; }
        public int NumOfConfirmedOrders { get; set; }
    }
}
