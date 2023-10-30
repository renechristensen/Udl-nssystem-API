namespace Udlånssystem_API.DTOs
{
    public class UdlånOversigtDetailsDTO
    {
        public int UdlånID { get; set; }
        public string ElevNummer { get; set; }
        public string RegistreringsNummer { get; set; }
        public DateTime Udlånsdato { get; set; }
        public DateTime Udløbsdato { get; set; }
    }
}