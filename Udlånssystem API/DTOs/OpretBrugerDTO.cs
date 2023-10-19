using System.ComponentModel.DataAnnotations;

namespace Udlånssystem_API.DTOs
{
    public class OpretBrugerDTO
    {
        public string? Navn { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        [EmailAddress]
        public string? Email { get; set; }
        
        public string? BrugerGruppeNavn { get; set; }
        public string? PostNrByNavn { get; set; }
        public int PostnrID { get; set; }
        public string? StamklasseNavn { get; set; }
        [System.ComponentModel.DataAnnotations.Required]  // Full namespace for clarity
        [RegularExpression(@"^\d{6}-\d{4}$", ErrorMessage = "CPR number must be in the format of 6 digits, a hyphen, and 4 digits (xxxxxxxx-xxxx).")]
        public string? CprNummer { get; set; }
        public string? Adresse { get; set; }

        [System.ComponentModel.DataAnnotations.Required]  // Full namespace for clarity

        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$", ErrorMessage = "Passwords must be at least 8 characters long and contain at least 1 uppercase letter, 1 lowercase letter, and 1 number.")]
        public string? Adgangskode { get; set; }
        public int Blacklisted { get; set; } = 0;

        public int BrugerGruppeID { get; set; }
        public int StamklasseID { get; set; }
    }
}

