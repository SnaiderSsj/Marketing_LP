using Marketing_LP.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Marketing_LP.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Lead> Leads { get; set; }
        public DbSet<Campaign> Campaigns { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Lead>(entity =>
            {
                entity.HasKey(l => l.Id);
                entity.Property(l => l.Nombre).IsRequired().HasMaxLength(100);
                entity.Property(l => l.Email).IsRequired().HasMaxLength(100);
                entity.Property(l => l.Estado).HasDefaultValue("nuevo");
                entity.Property(l => l.Sucursal).HasDefaultValue("La Paz");
            });

            modelBuilder.Entity<Campaign>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.Property(c => c.Nombre).IsRequired().HasMaxLength(100);
                entity.Property(c => c.Presupuesto).HasColumnType("decimal(18,2)");
                entity.Property(c => c.VentasGeneradas).HasColumnType("decimal(18,2)");
                entity.Property(c => c.Estado).HasDefaultValue("activa");
                entity.Property(c => c.Sucursal).HasDefaultValue("La Paz");
            });
        }
    }
}