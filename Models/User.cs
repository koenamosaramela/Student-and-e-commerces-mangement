using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace studentManagement_and__B2S__Consumer.Models
{


    // Base user class that extends IdentityUser
    public class User : IdentityUser
    {
        [Required]
        [Display(Name = "First Name")]
        [StringLength(50, MinimumLength = 2)]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [StringLength(50, MinimumLength = 2)]
        public string LastName { get; set; }

        [Display(Name = "Profile Image")]
        public string ProfileImagePath { get; set; }

        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
    }
}
