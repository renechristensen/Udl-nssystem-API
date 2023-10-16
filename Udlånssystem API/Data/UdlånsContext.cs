using Microsoft.EntityFrameworkCore;
using Udlånssystem_API.Models;
namespace Udlånssystem_API.Data
{
    public class UdlånsContext : DbContext
    {
        public UdlånsContext(DbContextOptions<UdlånsContext> options) : base(options) 
        {
            
        }

        public DbSet<Bruger> Brugere { get; set; }
        public DbSet<BrugerGruppe> brugerGrupper { get; set; }
        public DbSet<Computer> computere { get; set; }
        public DbSet<ComputerModel> computerModeller { get; set; }
        public DbSet<Fabrikat> fabrikater { get; set; }
        public DbSet<MusModel> musModdeller { get; set; }
        public DbSet<Postnr> porstNumre { get; set; }
        public DbSet<Stamklasse> stamklasser { get; set; }
        public DbSet<Udlån> udlån { get; set; }

    }
}
