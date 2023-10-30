using Udlånssystem_API.Models;

namespace Udlånssystem_API.Repositories.Interfaces
{
    public interface IMusModelRepository
    {
        Task<MusModel> GetByIdAsync(int id);
        Task<MusModel> CreateAsync(MusModel musModel);
        Task<MusModel> GetByTypeAsync(string type);
    }
}
