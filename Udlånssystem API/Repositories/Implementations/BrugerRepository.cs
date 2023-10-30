using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Udlånssystem_API.Data;
using Udlånssystem_API.Models;
using Udlånssystem_API.Repositories.Interfaces;

namespace Udlånssystem_API.Repositories.Implementations
{
    public class BrugerRepository : IBrugerRepository
    {
        private readonly UdlånsContext _context;

        public BrugerRepository(UdlånsContext context)
        {
            _context = context;
        }

        public async Task<bool> UserExists(string? email, string? cprNummer)
        {
            // Avoid throwing exceptions here, as it's a simple check.
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(cprNummer))
            {
                return false; // Interpret lack of credentials as "user does not exist"
            }

            // Check if a user with the provided email or CPR number exists in the database.
            return await _context.Brugere.AnyAsync(u => u.Email == email || u.CprNummer == cprNummer);
        }

        public async Task<List<Bruger>?> GetAll()
        {
            return await _context.Brugere.ToListAsync();
        }

        public async Task<Bruger?> GetById(int id)
        {
            return await _context.Brugere
                .Include(b => b.BrugerGruppe)
                .Include(b => b.Postnr)
                .Include(b => b.Stamklasse)
                .FirstOrDefaultAsync(b => b.BrugerID == id);
        }
        public async Task<Bruger?> GetByElevNummer(string elevNummer)
        {
            return await _context.Brugere
                .Include(b => b.BrugerGruppe)
                .Include(b => b.Postnr)
                .Include(b => b.Stamklasse)
                .FirstOrDefaultAsync(b => b.ElevNummer == elevNummer);
        }

        public async Task<Bruger?> Login(string? email, string? password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                return null; 
            }

            var user = await _context.Brugere
                .Include(b => b.BrugerGruppe)
                .Include(b => b.Postnr)
                .Include(b => b.Stamklasse)
                .FirstOrDefaultAsync(u => u.Email == email);


            if (user == null || user.Adgangskode != password)
            {
                return null;
            }

            return user;
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
