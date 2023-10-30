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
            return await _context.Udlån
                .Include(u => u.Computer)
                .FirstOrDefaultAsync(u => u.UdlånID == id);
        }


        public async Task<int> Create(Udlån udlån)
        {
            if (udlån == null)
            {
                throw new ArgumentNullException(nameof(udlån));
            }

            _context.Udlån.Add(udlån);
            await _context.SaveChangesAsync();

            return udlån.UdlånID;
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
                            .ThenInclude(c => c.ComputerModel)
                                .ThenInclude(cm => cm.Fabrikat)
                        .Include(u => u.Computer)
                            .ThenInclude(c => c.MusModel)
        .               ToListAsync();
            Console.WriteLine("Halt");
            return loaned;
        }
        public async Task<Udlån> FindActiveLoanByComputerID(int computerID)
        {
            // Find the loan that matches the provided student and computer numbers and is currently active.
            return await _context.Udlån
                .Where(u => u.ComputerID == computerID && u.Status != "Afleveret")
                .FirstOrDefaultAsync();
        }
    }
}