using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;

namespace Udlånssystem_API.DTOs
{
    public class LoginDTO
    {
        [System.ComponentModel.DataAnnotations.Required]
        [EmailAddress]
        public string? Email { get; set; }
        [System.ComponentModel.DataAnnotations.Required]  // Full namespace for clarity
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$", ErrorMessage = "Passwords must be at least 8 characters long and contain at least 1 uppercase letter, 1 lowercase letter, and 1 number.")]
        public string? Adgangskode { get; set; }
    }

}
