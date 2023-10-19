// Inside your 'Services/Implementations' folder
using System.Threading.Tasks;
using Udlånssystem_API.Models;
using Udlånssystem_API.Services.Interfaces;
using Udlånssystem_API.Repositories.Interfaces;

namespace Udlånssystem_API.Services.Implementations
{
    public class StamklasseService : IStamklasseService
    {
        private readonly IStamklasseRepository _stamklasseRepository;

        public StamklasseService(IStamklasseRepository stamklasseRepository)
        {
            _stamklasseRepository = stamklasseRepository;
        }

        public async Task<Stamklasse> GetOrCreateStamklasseAsync(string klasseNavn)
        {
            return await _stamklasseRepository.GetOrCreateStamklasseAsync(klasseNavn);
        }
    }
}