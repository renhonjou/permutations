using Microsoft.EntityFrameworkCore;
using permutationsWeb.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace permutationsWeb
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        public DbSet<PermutationsResult> PermutationsResults { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PermutationsResult>()
                .HasIndex(b => b.Request)
                .IsUnique();
        }
    }
}
