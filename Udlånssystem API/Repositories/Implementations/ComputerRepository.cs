// Repositories/ComputerRepository.cs
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Udlånssystem_API.Data;
using Udlånssystem_API.Models;
using Udlånssystem_API.Repositories.Interfaces;

namespace Udlånssystem_API.Repositories.Implementations
{
    public class ComputerRepository : IComputerRepository
    {
        private readonly UdlånsContext _context;

        public ComputerRepository(UdlånsContext context)
        {
            _context = context;
        }

        public async Task<List<Computer>> GetAllComputers()
        {
            // Adjusting the reference to your actual DbSet name
            return await _context.Computere
                .Include(c => c.ComputerModel)
                    .ThenInclude(cm => cm.Fabrikat) // Assuming ComputerModel includes Fabrikat navigation property
                .Include(c => c.MusModel)
                .ToListAsync();
        }

        public async Task<Computer> GetComputerById(int id)
        {
            return await _context.Computere
                .Include(c => c.ComputerModel)
                    .ThenInclude(cm => cm.Fabrikat)
                .Include(c => c.MusModel)
                .FirstOrDefaultAsync(c => c.ComputerID == id);
        }

        // ... (other CRUD operations as needed)
    }
}

