using Udlånssystem_API.Models;

namespace Udlånssystem_API.Repositories.Interfaces
{
    public interface IBrugerGruppeRepository
    {
        Task<BrugerGruppe> GetOrCreateBrugerGruppeAsync(string gruppeNavn);
        // Other necessary method definitions for the BrugerGruppe repository
    }
}
