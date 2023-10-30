// ComputerModelRepository.cs
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Udlånssystem_API.Data;
using Udlånssystem_API.Models;
using Udlånssystem_API.Repositories.Interfaces;

namespace Udlånssystem_API.Repositories.Implementations
{
    public class ComputerModelRepository : IComputerModelRepository
    {
        private readonly UdlånsContext _context;

        public ComputerModelRepository(UdlånsContext context)
        {
            _context = context;
        }

        public async Task<ComputerModel> GetByIdAsync(int id)
        {
            return await _context.ComputerModeller
                                 .Include(c => c.Fabrikat)
                                 .FirstOrDefaultAsync(c => c.ComputerModelID == id);
        }

        public async Task<ComputerModel> CreateAsync(ComputerModel computerModel)
        {
            _context.ComputerModeller.Add(computerModel);
            await _context.SaveChangesAsync();
            return computerModel;
        }
        public async Task<ComputerModel> GetByModelNavnAsync(string modelNavn)
        {
            return await _context.ComputerModeller
                                 .Include(c => c.Fabrikat) // If you need to include related data
                                 .FirstOrDefaultAsync(c => c.ModelNavn == modelNavn);
        }
    }
}
