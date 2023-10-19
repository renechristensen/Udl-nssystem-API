// PostnrRepository.cs
using Microsoft.EntityFrameworkCore;
using Udlånssystem_API.Data;
using Udlånssystem_API.Models;
using Udlånssystem_API.Repositories.Interfaces;

namespace Udlånssystem_API.Repositories.Implementations
{
    public class PostnrRepository : IPostnrRepository
    {
        private readonly UdlånsContext _context;

        public PostnrRepository(UdlånsContext context)
        {
            _context = context;
        }

        public async Task<Postnr> GetOrCreatePostnrAsync(string byNavn)
        {
            var existingPostnr = await _context.PostNumre
                                               .FirstOrDefaultAsync(p => p.ByNavn == byNavn);

            if (existingPostnr != null)
            {
                return existingPostnr;
            }

            var newPostnr = new Postnr { ByNavn = byNavn };

            _context.PostNumre.Add(newPostnr);
            await _context.SaveChangesAsync();

            return newPostnr;
        }
    }
}
