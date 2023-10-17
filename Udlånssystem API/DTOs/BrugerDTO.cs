namespace Udlånssystem_API.DTOs
{
    public class BrugerDTO
    {
        public int BrugerID { get; set; }

        public int BrugerGruppeID { get; set; }
        public int PostNrID { get; set; }
        public int StamKlasseID { get; set; }

        // for ease of access, making one call to database instead of four
        public string? BrugerGruppeNavn { get; set; }
        public string? PostNrByNavn { get; set; }
        public string? StamKlasseNavn { get; set; }

        // more class properties
        public string? Navn { get; set; }
        public string? CprNummer { get; set; }
        public string? Adresse { get; set; }
        public string? Email { get; set; }
        public string? Adgangskode { get; set; }
        public bool? Blacklisted { get; set; }
    }
}


