namespace Udlånssystem_API.DTOs
{
    public class ComputerDetailsDTO
    {
        public int ComputerID { get; set; }
        public string? RegistreringsNummer { get; set; }
        public string? ModelNavn { get; set; }  // From ComputerModel
        public string? FabrikatNavn { get; set; }  // From Fabrikat
        public string? MusType { get; set; }  // From MusModel
    }
}
