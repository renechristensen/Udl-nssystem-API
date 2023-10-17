using System.ComponentModel.DataAnnotations.Schema;

namespace Udlånssystem_API.Models
{
    [Table("postnr")]
    public class Postnr
    {
        public int PostnrID { get; set; }
        public string? ByNavn { get; set; }
    }
}
