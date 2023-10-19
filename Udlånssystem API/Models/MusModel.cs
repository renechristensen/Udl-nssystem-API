using System.ComponentModel.DataAnnotations.Schema;

namespace Udlånssystem_API.Models
{
    [Table("musmodel")]
    public class MusModel
    {
        public int MusModelID { get; set; }
        public string? MusType { get; set; }
    }
}
