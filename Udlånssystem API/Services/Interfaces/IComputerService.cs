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
        // Method signatures for other business operations.
    }
}

