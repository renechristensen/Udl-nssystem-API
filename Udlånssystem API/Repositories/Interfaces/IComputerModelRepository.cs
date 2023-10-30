using System.Threading.Tasks;
using Udlånssystem_API.Models;

namespace Udlånssystem_API.Repositories.Interfaces
{
    public interface IComputerModelRepository
    {
        Task<ComputerModel> GetByIdAsync(int id);
        Task<ComputerModel> CreateAsync(ComputerModel computerModel);
        Task<ComputerModel> GetByModelNavnAsync(string modelNavn);

    }
}

