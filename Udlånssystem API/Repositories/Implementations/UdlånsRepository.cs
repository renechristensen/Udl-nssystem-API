using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Udlånssystem_API.Data;
using Udlånssystem_API.Models;
using Udlånssystem_API.Repositories.Interfaces;

namespace Udlånssystem_API.Repositories.Implementations
{
    public class UdlånRepository : IUdlånRepository
    {
        private readonly UdlånsContext _context;

        public UdlånRepository(UdlånsContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<List<Udlån>> GetAll()
        {
            return await _context.Udlån
                .Include(u => u.Computer)
                .ToListAsync();
        }

        public async Task<Udlån> GetById(int id)
        {
            var udlån = await _context.Udlån
                .Include(u => u.Computer)
                .FirstOrDefaultAsync(u => u.UdlånID == id);

            if (udlån == null)
            {
                throw new Exception($"Loan with ID {id} could not be found.");
            }

            return udlån;
        }

        public async Task Create(Udlån udlån)
        {
            if (udlån == null)
            {
                throw new ArgumentNullException(nameof(udlån));
            }

            _context.Udlån.Add(udlån);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Udlån udlån)
        {
            if (udlån == null)
            {
                throw new ArgumentNullException(nameof(udlån));
            }

            _context.Entry(udlån).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var udlån = await _context.Udlån.FindAsync(id);
            if (udlån != null)
            {
                _context.Udlån.Remove(udlån);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Udlån>> GetActiveLoans()
        {

            List<Udlån> loaned = await _context.Udlån
                .Where(u => u.Status != "Afleveret") 
                .Include(u => u.Computer)
                .ToListAsync();
            Console.WriteLine("Halt");
            return loaned;
        }

    }
}
