using System.ComponentModel.DataAnnotations;

namespace ITRProject.API.PL.Dtos.User
{
    public class UpdatePasswordDto
    {
        [Required(ErrorMessage = "Email is required")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}
