using System.Collections.Generic;
using System.Threading.Tasks;
using Udlånssystem_API.Models;

namespace Udlånssystem_API.Repositories.Interfaces
{
    public interface IBrugerRepository
    {
        Task<bool> UserExists(string? email, string? cprNummer);
        Task<List<Bruger>?> GetAll();
        Task<Bruger?> GetById(int id);
        Task<Bruger?> GetByElevNummer(string elevNummer);
        Task Create(Bruger bruger);
        Task Update(Bruger bruger);
        Task Delete(int id);
        Task<Bruger?> Login(string? email, string? password);
    }
}