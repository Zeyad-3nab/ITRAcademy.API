using System.ComponentModel.DataAnnotations;

namespace ITRProject.API.PL.Dtos.User
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "UserName is required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Phone is required")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }


        public string Role { get; set; }
    }
}
