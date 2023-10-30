using Udlånssystem_API.Data;
using Udlånssystem_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Udlånssystem_API.Repositories.Implementations
{
    public class FabrikatRepository : IFabrikatRepository
    {
        private readonly UdlånsContext _context;

        public FabrikatRepository(UdlånsContext context)
        {
            _context = context;
        }

        public async Task<Fabrikat> GetFabrikatByName(string fabrikatNavn)
        {
            return await _context.Fabrikater.FirstOrDefaultAsync(f => f.FabrikatNavn == fabrikatNavn);
        }

        public async Task Create(Fabrikat fabrikat)
        {
            _context.Fabrikater.Add(fabrikat);
            await _context.SaveChangesAsync();
        }
    }

}
