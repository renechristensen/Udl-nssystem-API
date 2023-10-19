// Inside your 'Repositories/Implementations' folder
using Microsoft.EntityFrameworkCore;
using Udlånssystem_API.Data;
using Udlånssystem_API.Models;
using Udlånssystem_API.Repositories.Interfaces;
using System.Threading.Tasks;

namespace Udlånssystem_API.Repositories.Implementations
{
    public class StamklasseRepository : IStamklasseRepository
    {
        private readonly UdlånsContext _context;

        public StamklasseRepository(UdlånsContext context)
        {
            _context = context;
        }

        public async Task<Stamklasse> GetOrCreateStamklasseAsync(string klasseNavn)
        {
            // Check if the Stamklasse already exists
            var existingKlasse = await _context.Stamklasser
                                               .FirstOrDefaultAsync(k => k.KlasseNavn == klasseNavn);

            if (existingKlasse != null)
            {
                // If the Stamklasse already exists, return it
                return existingKlasse;
            }

            // If the Stamklasse doesn't exist, create a new one
            var newKlasse = new Stamklasse
            {
                KlasseNavn = klasseNavn
            };

            // Add the new Stamklasse to the context
            _context.Stamklasser.Add(newKlasse);

            // Save the changes in the context
            await _context.SaveChangesAsync();

            return newKlasse;
        }
    }
}
