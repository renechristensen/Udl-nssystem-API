using System.ComponentModel.DataAnnotations;

namespace Udlånssystem_API.DTOs
{
    public class ReturnLoanRequestDTO
    {
        [Required]
        public string RegistreringsNummer { get; set; }
    }
}
