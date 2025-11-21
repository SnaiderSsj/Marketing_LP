using Marketing_LP.Core.Entities;

namespace Marketing_LP.Core.Interfaces
{
    public interface ILeadRepository
    {
        Task<Lead> GetByIdAsync(Guid id);
        Task<IEnumerable<Lead>> GetAllAsync();
        Task<Lead> AddAsync(Lead entity);
        Task UpdateAsync(Lead entity);
        Task DeleteAsync(Lead entity);
        Task<IEnumerable<Lead>> GetLeadsBySucursalAsync(string sucursal);
        Task<LeadsEstadisticas> GetEstadisticasLeadsAsync();
    }

    public class LeadsEstadisticas
    {
        public int TotalLeads { get; set; }
        public int LeadsNuevos { get; set; }
        public int LeadsContactados { get; set; }
        public Dictionary<string, int> LeadsPorFuente { get; set; } = new();
    }
}