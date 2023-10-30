using Udlånssystem_API.Models;

namespace Udlånssystem_API.Services.Interfaces
{
    public interface IFabrikatService
    {
        Task<Fabrikat> GetOrCreateFabrikatAsync(string fabrikatNavn);
    }
}
