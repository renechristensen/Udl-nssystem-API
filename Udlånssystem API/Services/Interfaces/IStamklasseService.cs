using Udlånssystem_API.Models;

namespace Udlånssystem_API.Services.Interfaces
{
    public interface IStamklasseService
    {
        Task<Stamklasse> GetOrCreateStamklasseAsync(string klasseNavn);
    }
}
