using System.ComponentModel.DataAnnotations.Schema;

namespace Udlånssystem_API.Models
{
    [Table("fabrikat")]
    public class Fabrikat
    {
        public int FabrikatID { get; set; }
        public string? FabrikatNavn { get; set; }
    }
}
