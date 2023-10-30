using Udlånssystem_API.Models;
using Udlånssystem_API.Services.Interfaces;

namespace Udlånssystem_API.Services.Implementations
{
    public class FabrikatService : IFabrikatService
    {
        private readonly IFabrikatRepository _fabrikatRepository;

        public FabrikatService(IFabrikatRepository fabrikatRepository)
        {
            _fabrikatRepository = fabrikatRepository;
        }

        public async Task<Fabrikat> GetOrCreateFabrikatAsync(string fabrikatNavn)
        {
            var existingFabrikat = await _fabrikatRepository.GetFabrikatByName(fabrikatNavn);
            if (existingFabrikat != null)
            {
                return existingFabrikat; // Return the existing one if found
            }

            // If not found, create a new Fabrikat
            var newFabrikat = new Fabrikat { FabrikatNavn = fabrikatNavn };
            await _fabrikatRepository.Create(newFabrikat);

            return newFabrikat;
        }
    }
}
