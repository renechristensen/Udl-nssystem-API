using System.ComponentModel.DataAnnotations.Schema;

namespace Udlånssystem_API.Models
{
    [Table("stamklasse")]
    public class Stamklasse
    {
        public int StamklasseID {get; set;}
        public string? KlasseNavn {get; set;}
    }
}
