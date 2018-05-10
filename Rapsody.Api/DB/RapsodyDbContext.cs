using Microsoft.EntityFrameworkCore;
using Rapsody.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rapsody.Api.DB
{
    public class RapsodyDbContext : DbContext
    {
        public RapsodyDbContext(DbContextOptions<RapsodyDbContext> options) : base(options)
        {
                Database.EnsureCreated();
        }

        public virtual DbSet<Magnitude> Magnitude { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Magnitude>().HasKey(x => x.Id);

            base.OnModelCreating(modelBuilder);

        }
    }
}
