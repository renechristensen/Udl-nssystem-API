namespace Udlånssystem_API.DTOs
{
    public class RentedComputerDetailsDTO
    {
        public int UdlånID { get; set; }
        public int ComputerID { get; set; }
        public string? RegistreringsNummer { get; set; }
        public string? ModelNavn { get; set; }  // From ComputerModel
        public string? FabrikatNavn { get; set; }  // From Fabrikat
        public string? MusType { get; set; }  // From MusModel
        public DateTime Udlånsdato { get; set; }
        public DateTime Udløbsdato { get; set; }
        public string? Status { get; set; }
    }
}