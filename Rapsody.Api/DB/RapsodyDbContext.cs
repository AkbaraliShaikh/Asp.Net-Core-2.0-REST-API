using Microsoft.EntityFrameworkCore;
using Rapsody.Api.Models;
using System.Linq;

namespace Rapsody.Api.DB
{
    public class RapsodyDbContext : DbContext
    {
        public RapsodyDbContext(DbContextOptions<RapsodyDbContext> options) : base(options)
        {
            Database.EnsureCreated();

            if (Database.GetPendingMigrations().Any())
                Database.Migrate();

        }

        public virtual DbSet<Magnitude> Magnitude { get; set; }
        public virtual DbSet<Campaign> Campaign { get; set; }
        public virtual DbSet<CurrencyCode> CurrencyCode { get; set; }
        public virtual DbSet<BSOBS> BSOBS { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Magnitude>().HasKey(x => x.Id);
            modelBuilder.Entity<Campaign>().HasKey(x => x.Id);
            modelBuilder.Entity<CurrencyCode>().HasKey(x => x.Id);
            modelBuilder.Entity<BSOBS>().HasKey(x => x.Id);

            base.OnModelCreating(modelBuilder);

        }
    }
}
