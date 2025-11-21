using Marketing_LP.Core.Entities;

namespace Marketing_LP.Infrastructure.Data
{
    public static class SeedData
    {
        public static async Task InitializeAsync(ApplicationDbContext context)
        {
            if (!context.Leads.Any())
            {
                context.Leads.AddRange(
                    new Lead
                    {
                        Id = Guid.NewGuid(),
                        Nombre = "Juan Perez",
                        Email = "juan@email.com",
                        Telefono = "77712345",
                        ProductoInteres = "Yogurt Natural",
                        Fuente = "web",
                        Estado = "nuevo",
                        Sucursal = "La Paz"
                    },
                    new Lead
                    {
                        Id = Guid.NewGuid(),
                        Nombre = "Maria Lopez",
                        Email = "maria@email.com",
                        Telefono = "77767890",
                        ProductoInteres = "Queso Andino",
                        Fuente = "redes_sociales",
                        Estado = "contactado",
                        Sucursal = "La Paz"
                    }
                );
            }

            if (!context.Campaigns.Any())
            {
                context.Campaigns.AddRange(
                    new Campaign
                    {
                        Id = Guid.NewGuid(),
                        Nombre = "Promo Verano",
                        Descripcion = "Promoción de productos lácteos para verano",
                        Tipo = "email",
                        Presupuesto = 5000,
                        FechaInicio = DateTime.UtcNow.AddDays(-30),
                        FechaFin = DateTime.UtcNow.AddDays(30),
                        Estado = "activa",
                        Sucursal = "La Paz",
                        LeadsGenerados = 45,
                        VentasGeneradas = 25000
                    }
                );
            }

            await context.SaveChangesAsync();
        }
    }
}