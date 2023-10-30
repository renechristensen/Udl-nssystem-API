using Udlånssystem_API.Models;
using Udlånssystem_API.Services.Interfaces;
using Udlånssystem_API.Repositories.Interfaces;

namespace Udlånssystem_API.Services.Implementations
{  
    public class BrugerGruppeService : IBrugerGruppeService
    {
        private readonly IBrugerGruppeRepository _brugerGruppeRepository;

        public BrugerGruppeService(IBrugerGruppeRepository brugerGruppeRepository)
        {
            _brugerGruppeRepository = brugerGruppeRepository;
        }

        public async Task<BrugerGruppe> GetOrCreateBrugerGruppeAsync(string gruppeNavn)
        {
            return await _brugerGruppeRepository.GetOrCreateBrugerGruppeAsync(gruppeNavn);
        }

        public async Task<BrugerGruppe> GetBrugerGruppeAsync(int ID)
        {
            return await _brugerGruppeRepository.GetBrugerGruppeAsync(ID);
        }
    }
}
