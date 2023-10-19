using Udlånssystem_API.Models;

namespace Udlånssystem_API.Repositories.Interfaces
{
    public interface IStamklasseRepository
    {
        Task<Stamklasse> GetOrCreateStamklasseAsync(string klasseNavn);
    }
}

