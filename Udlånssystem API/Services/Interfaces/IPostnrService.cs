using Udlånssystem_API.Models;

namespace Udlånssystem_API.Services.Interfaces
{
    public interface IPostnrService
    {
        Task<Postnr> GetOrCreatePostnrAsync(string byNavn);
    }
}
