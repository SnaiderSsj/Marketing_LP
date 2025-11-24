using Marketing_LP.Core.Entities;
using Marketing_LP.Core.Interfaces;
using Marketing_LP.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Marketing_LP.Infrastructure.Repositories
{
    public class LeadRepository : ILeadRepository
    {
        private readonly ApplicationDbContext _context;

        public LeadRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Lead> GetByIdAsync(Guid id)
        {
            return await _context.Leads.FindAsync(id);
        }

        public async Task<IEnumerable<Lead>> GetAllAsync()
        {
            return await _context.Leads.ToListAsync();
        }

        public async Task<Lead> AddAsync(Lead entity)
        {
            _context.Leads.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(Lead entity)
        {
            entity.FechaActualizacion = DateTime.UtcNow;
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Lead entity)
        {
            _context.Leads.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Lead>> GetLeadsBySucursalAsync(string sucursal)
        {
            return await _context.Leads
                .Where(l => l.Sucursal == sucursal)
                .OrderByDescending(l => l.FechaCreacion)
                .ToListAsync();
        }

        public async Task<IEnumerable<Lead>> GetLeadsByEstadoAsync(string estado)
        {
            return await _context.Leads
                .Where(l => l.Estado == estado)
                .ToListAsync();
        }

        public async Task<LeadsEstadisticas> GetEstadisticasLeadsAsync()
        {
            var estadisticas = new LeadsEstadisticas();

            var leads = await _context.Leads.ToListAsync();

            estadisticas.TotalLeads = leads.Count;
            estadisticas.LeadsNuevos = leads.Count(l => l.Estado == "nuevo");
            estadisticas.LeadsContactados = leads.Count(l => l.Estado == "contactado");

            estadisticas.LeadsPorFuente = leads
                .GroupBy(l => l.Fuente)
                .ToDictionary(g => g.Key, g => g.Count());

            return estadisticas;
        }
    }
}