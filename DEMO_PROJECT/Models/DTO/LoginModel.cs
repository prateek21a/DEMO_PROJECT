using System.ComponentModel.DataAnnotations;

namespace DEMO_PROJECT.Models.DTO
{
    public class LoginModel
    {
        [Required(ErrorMessage = "First Name is required")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Password Name is required")]
        public string Password { get; set; }
    }
}
