namespace Udlånssystem_API.Models
{
    public class Udlån
    {
        public int UdlånID { get; set; }
        public int BrugerID { get; set; }
        public int ComputerID { get; set; }
        public DateTime Udlånsdato { get; set; }
        public DateTime Udløbsdato { get; set; } 
        public string? Status { get; set; }
        public Computer Computer { get; set; }
    }
}