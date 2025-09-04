using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITR.API.DAL.Models
{
    public class ApplicationUser : IdentityUser
    {
        private static int _Code = 1;
        public int Code { get; set; }
        public DateOnly DateOfCreation { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        public string Role { get; set; }

        public ApplicationUser()
        {
            Code = _Code;
            _Code++;
        }
    }
}