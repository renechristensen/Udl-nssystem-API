using Udlånssystem_API.DTOs;
using Udlånssystem_API.Models;

namespace Udlånssystem_API.Services.Interfaces
{
    public interface IBrugerService
    {
        Task<BrugerDTO> GetBruger(int id);
        Task<BrugerDTO> GetBruger(string elevNummer);
        Task<LoginResponseDTO> Login(string email, string password);
        Task<BrugerDTO> CreateUser(OpretBrugerDTO opretBrugerDto);
    }
}

