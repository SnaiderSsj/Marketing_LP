using Marketing_LP.Core.Entities;
using Marketing_LP.Core.Interfaces;
using Marketing_LP.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Marketing_LP.Infrastructure.Repositories
{
    public class LeadRepository : ILeadRepository
    {
        // Lista en memoria temporal (eliminar después)
        private static List<Lead> _leads = new List<Lead>
        {
            new Lead {
                Id = Guid.NewGuid(),
                Nombre = "Juan Perez",
                Email = "juan@email.com",
                Telefono = "77712345",
                ProductoInteres = "Yogurt Natural",
                Fuente = "web",
                Estado = "nuevo",
                Sucursal = "La Paz",
                FechaCreacion = DateTime.UtcNow,
                FechaActualizacion = DateTime.UtcNow
            },
            new Lead {
                Id = Guid.NewGuid(),
                Nombre = "Maria Lopez",
                Email = "maria@email.com",
                Telefono = "77767890",
                ProductoInteres = "Queso Andino",
                Fuente = "redes_sociales",
                Estado = "contactado",
                Sucursal = "La Paz",
                FechaCreacion = DateTime.UtcNow,
                FechaActualizacion = DateTime.UtcNow
            }
        };

        public LeadRepository() // Constructor temporal sin DbContext
        {
        }

        public async Task<Lead> GetByIdAsync(Guid id)
        {
            return await Task.FromResult(_leads.FirstOrDefault(l => l.Id == id));
        }

        public async Task<IEnumerable<Lead>> GetAllAsync()
        {
            return await Task.FromResult(_leads.AsEnumerable());
        }

        public async Task<Lead> AddAsync(Lead entity)
        {
            _leads.Add(entity);
            return await Task.FromResult(entity);
        }

        public async Task UpdateAsync(Lead entity)
        {
            var existing = _leads.FirstOrDefault(l => l.Id == entity.Id);
            if (existing != null)
            {
                _leads.Remove(existing);
                _leads.Add(entity);
            }
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(Lead entity)
        {
            _leads.Remove(entity);
            await Task.CompletedTask;
        }

        public async Task<IEnumerable<Lead>> GetLeadsBySucursalAsync(string sucursal)
        {
            return await Task.FromResult(_leads.Where(l => l.Sucursal == sucursal));
        }

        public async Task<LeadsEstadisticas> GetEstadisticasLeadsAsync()
        {
            var estadisticas = new LeadsEstadisticas();

            estadisticas.TotalLeads = _leads.Count;
            estadisticas.LeadsNuevos = _leads.Count(l => l.Estado == "nuevo");
            estadisticas.LeadsContactados = _leads.Count(l => l.Estado == "contactado");

            estadisticas.LeadsPorFuente = _leads
                .GroupBy(l => l.Fuente)
                .ToDictionary(g => g.Key, g => g.Count());

            return await Task.FromResult(estadisticas);
        }
    }
}