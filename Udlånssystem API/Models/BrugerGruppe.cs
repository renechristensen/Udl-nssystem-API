using System.ComponentModel.DataAnnotations.Schema;

namespace Udlånssystem_API.Models
{
    [Table("brugergruppe")]
    public class BrugerGruppe
    {
        public int BrugerGruppeID { get; set; }
        public string? GruppeNavn { get; set; }
    }
}
