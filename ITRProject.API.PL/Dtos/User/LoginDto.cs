using System.ComponentModel.DataAnnotations;

namespace ITRProject.API.PL.Dtos.User
{
    public class LoginDto
    {
        [MaxLength(100)]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }


        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}
