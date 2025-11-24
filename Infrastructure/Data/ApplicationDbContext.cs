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
                entity.Property(l => l.Telefono).HasMaxLength(20);
                entity.Property(l => l.ProductoInteres).IsRequired().HasMaxLength(100);
                entity.Property(l => l.Fuente).IsRequired().HasMaxLength(50);
                entity.Property(l => l.Estado).HasDefaultValue("nuevo").HasMaxLength(20);
                entity.Property(l => l.Sucursal).HasDefaultValue("La Paz").HasMaxLength(50);
                entity.Property(l => l.FechaCreacion).HasDefaultValueSql("NOW()");
                entity.Property(l => l.FechaActualizacion).HasDefaultValueSql("NOW()");
            });

            modelBuilder.Entity<Campaign>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.Property(c => c.Nombre).IsRequired().HasMaxLength(100);
                entity.Property(c => c.Descripcion);
                entity.Property(c => c.Tipo).IsRequired().HasMaxLength(50);
                entity.Property(c => c.Presupuesto).HasColumnType("decimal(18,2)");
                entity.Property(c => c.VentasGeneradas).HasColumnType("decimal(18,2)");
                entity.Property(c => c.Estado).HasDefaultValue("activa").HasMaxLength(20);
                entity.Property(c => c.Sucursal).HasDefaultValue("La Paz").HasMaxLength(50);
                entity.Property(c => c.LeadsGenerados).HasDefaultValue(0);
                entity.Property(c => c.FechaCreacion).HasDefaultValueSql("NOW()");
                entity.Property(c => c.FechaActualizacion).HasDefaultValueSql("NOW()");
            });
        }
    }
}