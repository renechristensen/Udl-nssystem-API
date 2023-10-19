// PostnrService.cs
using Udlånssystem_API.Models;
using Udlånssystem_API.Services.Interfaces;
using Udlånssystem_API.Repositories.Interfaces;

namespace Udlånssystem_API.Services
{
    public class PostnrService : IPostnrService
    {
        private readonly IPostnrRepository _postnrRepository;

        public PostnrService(IPostnrRepository postnrRepository)
        {
            _postnrRepository = postnrRepository;
        }

        public async Task<Postnr> GetOrCreatePostnrAsync(string byNavn)
        {
            return await _postnrRepository.GetOrCreatePostnrAsync(byNavn);
        }
    }
}

