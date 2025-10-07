using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AutoLuxBackend.Models;

namespace AutoLuxBackend.Context
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Brand> Brands { get; set; }
        public DbSet<Cars> Cars { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Cars>()
                .Property(c => c.Price)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Brand>()
                .HasIndex(b => b.Name)
                .IsUnique();
        }
    }
}
