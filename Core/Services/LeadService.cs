using Marketing_LP.Core.Entities;
using Marketing_LP.Core.Interfaces;

namespace Marketing_LP.Core.Services
{
    public class LeadService : ILeadService
    {
        private readonly ILeadRepository _leadRepository;

        public LeadService(ILeadRepository leadRepository)
        {
            _leadRepository = leadRepository;
        }

        public async Task<Lead> CreateLeadAsync(Lead lead)
        {
            if (string.IsNullOrWhiteSpace(lead.Email))
                throw new ArgumentException("El email es requerido");

            lead.Sucursal = "La Paz";
            lead.FechaCreacion = DateTime.UtcNow;
            lead.FechaActualizacion = DateTime.UtcNow;

            return await _leadRepository.AddAsync(lead);
        }

        public async Task<IEnumerable<Lead>> GetLeadsBySucursalAsync(string sucursal)
        {
            return await _leadRepository.GetLeadsBySucursalAsync(sucursal);
        }

        public async Task<LeadsEstadisticas> GetEstadisticasLeadsAsync()
        {
            return await _leadRepository.GetEstadisticasLeadsAsync();
        }
    }
}