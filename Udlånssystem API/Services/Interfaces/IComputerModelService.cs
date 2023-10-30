using System.Threading.Tasks;
using Udlånssystem_API.Models;

namespace Udlånssystem_API.Services.Interfaces
{
    public interface IComputerModelService
    {
        Task<ComputerModel> GetComputerModelByIdAsync(int id);
        Task<ComputerModel> CreateComputerModelAsync(ComputerModel computerModel);
    }
}

