using Microsoft.EntityFrameworkCore;
using Udlånssystem_API.Data;
using Udlånssystem_API.Models;
using Udlånssystem_API.Repositories.Interfaces;

namespace Udlånssystem_API.Repositories.Implementations
{
    public class BrugerGruppeRepository : IBrugerGruppeRepository
    {
        private readonly UdlånsContext _context;

        public BrugerGruppeRepository(UdlånsContext context)
        {
            _context = context;
        }

        public async Task<BrugerGruppe> GetOrCreateBrugerGruppeAsync(string gruppeNavn)
        {
            var existingGruppe = await _context.BrugerGrupper
                                               .FirstOrDefaultAsync(g => g.GruppeNavn == gruppeNavn);

            if (existingGruppe != null)
            {
                return existingGruppe;
            }

            var newGruppe = new BrugerGruppe
            {
                GruppeNavn = gruppeNavn
            };

            _context.BrugerGrupper.Add(newGruppe);
            await _context.SaveChangesAsync();

            return newGruppe;
        }
        public async Task<BrugerGruppe> GetBrugerGruppeAsync(int ID)
        {
            var existingGruppe = await _context.BrugerGrupper
                                               .FirstOrDefaultAsync(g => g.BrugerGruppeID == ID);
            return existingGruppe;
        }
    }
}
