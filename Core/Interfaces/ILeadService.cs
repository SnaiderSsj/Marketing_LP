using Marketing_LP.Core.Entities;

namespace Marketing_LP.Core.Interfaces
{
    public interface ILeadService
    {
        Task<Lead> CreateLeadAsync(Lead lead);
        Task<IEnumerable<Lead>> GetLeadsBySucursalAsync(string sucursal);
        Task<LeadsEstadisticas> GetEstadisticasLeadsAsync();
    }
}