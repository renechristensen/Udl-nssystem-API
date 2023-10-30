// Services/Interfaces/IComputerService.cs
using System.Collections.Generic;
using System.Threading.Tasks;
using Udlånssystem_API.Models;

namespace Udlånssystem_API.Services.Interfaces
{
    public interface IComputerService
    {
        Task<List<Computer>> GetAllComputersWithDetails();
        Task<Computer> GetComputerDetails(int id);
        Task<Computer> GetComputerDetails(string RegistreringsNummer);
        Task<Computer> RegisterComputer(ComputerRegistrationDTO registrationDTO);
        Task<List<Computer>> GetRentedComputersByUserId(int userId);

    }
}

