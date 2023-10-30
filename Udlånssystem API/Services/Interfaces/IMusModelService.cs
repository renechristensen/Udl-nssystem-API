using Udlånssystem_API.Models;

namespace Udlånssystem_API.Services.Interfaces
{
    public interface IMusModelService
    {
        Task<MusModel> GetMusModelAsync(int id);
        Task<MusModel> CreateMusModelAsync(MusModel musModel);
    }
}
