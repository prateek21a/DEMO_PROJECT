using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace DEMO_PROJECT.Models.Domain
{

    public class ApplicationUser : IdentityUser
    {
        [Required(ErrorMessage = "First Name is required")] 
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")] 
        public string LastName { get; set; }
            //public string Role { get; set; }

    }

}
