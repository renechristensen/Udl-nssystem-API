using System.ComponentModel.DataAnnotations.Schema;

namespace Udlånssystem_API.Models
{
    [Table("computer")]
    public class Computer
    {
        public int ComputerID { get; set; }
        public int ComputerModelID { get; set; }
        public int MusModelID { get; set; }
        public string? RegistreringsNummer { get; set; }

        // Navigation properties
        public ComputerModel ComputerModel { get; set; }
        public MusModel MusModel { get; set; }
    }
}
