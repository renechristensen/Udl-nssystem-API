using Udlånssystem_API.Models;

namespace Udlånssystem_API.Repositories.Interfaces
{
    public interface IPostnrRepository
    {
        Task<Postnr> GetOrCreatePostnrAsync(string byNavn);
    }
}
