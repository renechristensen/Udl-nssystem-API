using System.Collections.Generic;
using System.Threading.Tasks;
using Udlånssystem_API.Models;

namespace Udlånssystem_API.Repositories.Interfaces
{
    public interface IComputerRepository
    {
        Task<List<Computer>> GetAllComputers();
        Task<Computer> GetComputerById(int id);
        Task<Computer> GetComputerByRegistreringsNummer(string registreringsNummer);
        Task<Computer> AddComputer(Computer computer);


    }
}
