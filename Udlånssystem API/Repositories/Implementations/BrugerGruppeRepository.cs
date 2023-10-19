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
            // Check if the BrugerGruppe already exists
            var existingGruppe = await _context.BrugerGrupper
                                               .FirstOrDefaultAsync(g => g.GruppeNavn == gruppeNavn);

            if (existingGruppe != null)
            {
                // If the BrugerGruppe already exists, return it
                return existingGruppe;
            }

            // If the BrugerGruppe doesn't exist, create a new one
            var newGruppe = new BrugerGruppe
            {
                GruppeNavn = gruppeNavn
            };

            // Add the new BrugerGruppe to the context
            _context.BrugerGrupper.Add(newGruppe);

            // Save the changes in the context
            await _context.SaveChangesAsync();

            return newGruppe;
        }
    }
}
