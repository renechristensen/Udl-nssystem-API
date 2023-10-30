
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Udlånssystem_API.Data;
using Udlånssystem_API.Models;
using Udlånssystem_API.Repositories.Interfaces;

namespace Udlånssystem_API.Repositories.Implementations
{
    public class MusModelRepository : IMusModelRepository
    {
        private readonly UdlånsContext _context;

        public MusModelRepository(UdlånsContext context)
        {
            _context = context;
        }

        public async Task<MusModel> GetByIdAsync(int id)
        {
            return await _context.MusModdeller.FindAsync(id);
        }
        public async Task<MusModel> GetByTypeAsync(string type)
        {
            return await _context.MusModdeller.FirstOrDefaultAsync(m => m.MusType == type);
        }
        public async Task<MusModel> CreateAsync(MusModel musModel)
        {
            _context.MusModdeller.Add(musModel);
            await _context.SaveChangesAsync();
            return musModel;
        }
    }
}
