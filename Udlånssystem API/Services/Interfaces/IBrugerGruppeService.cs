using Udlånssystem_API.Models;

namespace Udlånssystem_API.Services.Interfaces
{
    public interface IBrugerGruppeService
    {
        Task<BrugerGruppe> GetOrCreateBrugerGruppeAsync(string gruppeNavn);
    }
}
