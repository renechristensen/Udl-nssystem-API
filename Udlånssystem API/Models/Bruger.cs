using System.ComponentModel.DataAnnotations.Schema;

namespace Udlånssystem_API.Models
{
    [Table("bruger")]
    public class Bruger
    {

        // Navigation properties
        public virtual BrugerGruppe? BrugerGruppe { get; set; }
        public virtual Postnr? Postnr { get; set; }
        public virtual Stamklasse? Stamklasse { get; set; }

        // class properties
        public int BrugerID { get; set; }
        public string? Navn { get; set; }
        public string? CprNummer { get; set; }
        public string? Adresse { get; set; }
        public string? Email { get; set; }
        public string? Adgangskode { get; set; }
        public bool? Blacklisted { get; set; }

        public int BrugerGruppeID { get; set; }
        public int PostnrID { get; set; }
        public int StamklasseID { get; set; }
        public string? ElevNummer { get; set; }
    }
}
