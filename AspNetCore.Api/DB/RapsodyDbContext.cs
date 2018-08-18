using Microsoft.EntityFrameworkCore;
using AspNetCore.Api.Models;
using System.Linq;

namespace AspNetCore.Api.DB
{
    public class EFCoreDbContext : DbContext
    {
        public EFCoreDbContext(DbContextOptions<EFCoreDbContext> options) : base(options)
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
