using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Udlånssystem_API.Data;
using Udlånssystem_API.Models;
using Udlånssystem_API.Repositories.Interfaces;

namespace Udlånssystem_API.Services.Interfaces
{
    public class ComputerService : IComputerService
    {
        private readonly IComputerRepository _computerRepository;

        public ComputerService(IComputerRepository computerRepository)
        {
            _computerRepository = computerRepository;
        }

        public async Task<List<Computer>> GetAllComputersWithDetails()
        {
            return await _computerRepository.GetAllComputers();
        }

        public async Task<Computer> GetComputerDetails(int id)
        {
            return await _computerRepository.GetComputerById(id);
        }
    }
}

