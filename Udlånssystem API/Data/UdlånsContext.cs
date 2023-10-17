using Microsoft.EntityFrameworkCore;
using Udlånssystem_API.Models;
namespace Udlånssystem_API.Data
{
    public partial class UdlånsContext : DbContext
    {
        public UdlånsContext(DbContextOptions<UdlånsContext> options) : base(options) 
        {
            this.ChangeTracker.LazyLoadingEnabled = false;
        }

        public DbSet<Bruger> Brugere { get; set; }
        public DbSet<BrugerGruppe> BrugerGrupper { get; set; }
        public DbSet<Computer> Computere { get; set; }
        public DbSet<ComputerModel> ComputerModeller { get; set; }
        public DbSet<Fabrikat> Fabrikater { get; set; }
        public DbSet<MusModel> MusModdeller { get; set; }
        public DbSet<Postnr> PostNumre { get; set; }
        public DbSet<Stamklasse> Stamklasser { get; set; }
        public DbSet<Udlån> Udlån { get; set; }

    }
}
