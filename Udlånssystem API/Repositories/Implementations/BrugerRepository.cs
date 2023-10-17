using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Udlånssystem_API.Models;

namespace Udlånssystem_API.Data
{
    public class BrugerRepository : IBrugerRepository
    {
        private readonly UdlånsContext _context;

        public BrugerRepository(UdlånsContext context)
        {
            _context = context;
        }

        public async Task<List<Bruger>> GetAll()
        {
            return await _context.Brugere.ToListAsync();
        }

        public async Task<Bruger> GetById(int id)
        {
            var bruger = await _context.Brugere
                .Include(b => b.BrugerGruppe)
                .Include(b => b.Postnr)
                .Include(b => b.Stamklasse)
                .FirstOrDefaultAsync(b => b.BrugerID == id);

            if (bruger == null)
            {
                // Handle the error appropriately.
                // You could throw an exception, or you could return null and handle it based on the calling method's behavior.
                throw new Exception($"Bruger with ID {id} could not be found.");
            }

            return bruger;
        }


        public async Task Create(Bruger bruger)
        {
            _context.Brugere.Add(bruger);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Bruger bruger)
        {
            _context.Entry(bruger).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var bruger = await _context.Brugere.FindAsync(id);
            if (bruger != null)
            {
                _context.Brugere.Remove(bruger);
                await _context.SaveChangesAsync();
            }
        }
    }
}
