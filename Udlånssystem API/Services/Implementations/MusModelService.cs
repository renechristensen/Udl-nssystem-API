
using System.Threading.Tasks;
using Udlånssystem_API.Models;
using Udlånssystem_API.Services.Interfaces;
using Udlånssystem_API.Repositories.Interfaces;

namespace Udlånssystem_API.Services.Implementations
{
    public class MusModelService : IMusModelService
    {
        private readonly IMusModelRepository _repository;

        public MusModelService(IMusModelRepository repository)
        {
            _repository = repository;
        }

        public async Task<MusModel> GetMusModelAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<MusModel> CreateMusModelAsync(MusModel musModel)
        {
            return await _repository.CreateAsync(musModel);
        }
    }
}
