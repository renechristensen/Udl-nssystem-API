using System.Threading.Tasks;
using Udlånssystem_API.Models;
using Udlånssystem_API.Repositories.Interfaces;
using Udlånssystem_API.Services.Interfaces;

namespace Udlånssystem_API.Services.Implementations
{
    public class ComputerModelService : IComputerModelService
    {
        private readonly IComputerModelRepository _computerModelRepository;

        public ComputerModelService(IComputerModelRepository computerModelRepository)
        {
            _computerModelRepository = computerModelRepository;
        }

        public async Task<ComputerModel> GetComputerModelByIdAsync(int id)
        {
            return await _computerModelRepository.GetByIdAsync(id);
        }

        public async Task<ComputerModel> CreateComputerModelAsync(ComputerModel computerModel)
        {
            return await _computerModelRepository.CreateAsync(computerModel);
        }
    }
}
