namespace Udlånssystem_API.Models
{
    public class Bruger
    {
        public int BrugerID { get; set; }
        public int BrugerGruppeID { get; set; }
        public int PostNrID { get; set; }
        public int StamKlasseID { get; set; }
        public string? Navn { get; set; }
        public int CprNummer { get; set; }
        public string? Addresse { get; set; }
        public string? Email { get; set; }
        public string? Adgangskode { get; set; }
        public string? Blacklisted { get; set; }
    }
}
