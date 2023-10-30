using System.ComponentModel.DataAnnotations;
public class LoginDTO
{
    [Required]
    [EmailAddress]
    public string? Email { get; set; }

    [Required]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$", ErrorMessage = "Passwords must be at least 8 characters long and contain at least 1 uppercase letter, 1 lowercase letter, and 1 number.")]
    public string? Adgangskode { get; set; }  // Not nullable anymore
}

