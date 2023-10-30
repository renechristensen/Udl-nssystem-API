using System.ComponentModel.DataAnnotations;

namespace Udlånssystem_API.DTOs
{
    public class ReserveComputerRequestDTO
    {
        [Required]
        public string? ElevNummer { get; set; }
        [Required]
        public string? RegistreringsNummer { get; set; }
        [Required]
        public DateTime Udlånsdato { get; set; }
        [Required]
        public DateTime Udløbsdato { get; set; }
    }
}

