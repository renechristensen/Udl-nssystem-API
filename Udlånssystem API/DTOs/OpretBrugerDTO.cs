using System.ComponentModel.DataAnnotations;

namespace Udlånssystem_API.DTOs
{
    public class OpretBrugerDTO
    {
        [Required]
        public string? Navn { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        public string? BrugerGruppeNavn { get; set; }

        [Required]
        public string? PostNrByNavn { get; set; }  // Assuming this is the name of the city or area, not the ID

        [Required]
        public string? StamklasseNavn { get; set; }

        [Required]
        [RegularExpression(@"^\d{6}-\d{4}$", ErrorMessage = "CPR number must be in the format of 6 digits, a hyphen, and 4 digits (xxxxxxxx-xxxx).")]
        public string? CprNummer { get; set; }

        [Required]
        public string? Adresse { get; set; }

        [Required]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$", ErrorMessage = "Passwords must be at least 8 characters long and contain at least 1 uppercase letter, 1 lowercase letter, and 1 number.")]
        public string? Adgangskode { get; set; }

        [Required]
        public string? ElevNummer { get; set; }
    }
}
