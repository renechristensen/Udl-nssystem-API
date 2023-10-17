using System.Collections.Generic;
using System.Threading.Tasks;
using Udlånssystem_API.Models;

namespace Udlånssystem_API.Data
{
    public interface IBrugerRepository
    {
        Task<List<Bruger>> GetAll();
        Task<Bruger> GetById(int id);
        Task Create(Bruger bruger);
        Task Update(Bruger bruger);
        Task Delete(int id);
    }
}