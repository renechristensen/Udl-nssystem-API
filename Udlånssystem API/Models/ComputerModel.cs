using System.ComponentModel.DataAnnotations.Schema;

namespace Udlånssystem_API.Models
{
    [Table("computermodel")]
    public class ComputerModel
    {
        public int ComputerModelID { get; set; }
        public int FabrikatID { get; set; }
        public string? ModelNavn { get; set; }

        // Navigation property
        [ForeignKey("FabrikatID")]
        public Fabrikat Fabrikat { get; set; }
    }
}
