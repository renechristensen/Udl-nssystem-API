namespace Udlånssystem_API.DTOs
{
    public class LoginResponseDTO
    {
        public int? BrugerID { get; set; }
        public string? Navn { get; set; }
        public string? Email { get; set; }

        public string? BrugerGruppeNavn { get; set; }
        public string? PostNrByNavn { get; set; }
        public int PostNrID { get; set; }
        public string? StamKlasseNavn { get; set; }

        // more class properties
        public string? CprNummer { get; set; }
        public string? Adresse { get; set; }
        public string? Adgangskode { get; set; }
        public string? ElevNummer { get; set; }

    }
}
