using Microsoft.AspNetCore.Mvc;
using Marketing_LP.Core.Entities;
using Marketing_LP.Core.Interfaces;
using Marketing_LP.Core.DTOs;

namespace Marketing_LP.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LeadsController : ControllerBase
    {
        private readonly ILeadService _leadService;

        public LeadsController(ILeadService leadService)
        {
            _leadService = leadService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LeadDto>>> GetLeads()
        {
            try
            {
                var leads = await _leadService.GetLeadsBySucursalAsync("La Paz");
                var leadDtos = leads.Select(lead => new LeadDto
                {
                    Id = lead.Id,
                    Nombre = lead.Nombre,
                    Email = lead.Email,
                    Telefono = lead.Telefono,
                    ProductoInteres = lead.ProductoInteres,
                    Fuente = lead.Fuente,
                    Estado = lead.Estado,
                    Sucursal = lead.Sucursal,
                    FechaCreacion = lead.FechaCreacion
                });

                return Ok(leadDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }

        [HttpGet("sucursal/la-paz")]
        public async Task<ActionResult<IEnumerable<Lead>>> GetLeadsLaPaz()
        {
            try
            {
                var leads = await _leadService.GetLeadsBySucursalAsync("La Paz");
                return Ok(leads);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }

        [HttpGet("estadisticas")]
        public async Task<ActionResult<LeadsEstadisticasDto>> GetEstadisticas()
        {
            try
            {
                var estadisticas = await _leadService.GetEstadisticasLeadsAsync();
                var estadisticasDto = new LeadsEstadisticasDto
                {
                    TotalLeads = estadisticas.TotalLeads,
                    LeadsNuevos = estadisticas.LeadsNuevos,
                    LeadsContactados = estadisticas.LeadsContactados,
                    LeadsPorFuente = estadisticas.LeadsPorFuente
                };

                return Ok(estadisticasDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<LeadDto>> CreateLead([FromBody] CreateLeadDto createLeadDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var lead = new Lead
                {
                    Id = Guid.NewGuid(),
                    Nombre = createLeadDto.Nombre,
                    Email = createLeadDto.Email,
                    Telefono = createLeadDto.Telefono,
                    ProductoInteres = createLeadDto.ProductoInteres,
                    Fuente = createLeadDto.Fuente,
                    Estado = "nuevo",
                    Sucursal = "La Paz",
                    FechaCreacion = DateTime.UtcNow,
                    FechaActualizacion = DateTime.UtcNow
                };

                var createdLead = await _leadService.CreateLeadAsync(lead);

                var leadDto = new LeadDto
                {
                    Id = createdLead.Id,
                    Nombre = createdLead.Nombre,
                    Email = createdLead.Email,
                    Telefono = createdLead.Telefono,
                    ProductoInteres = createdLead.ProductoInteres,
                    Fuente = createdLead.Fuente,
                    Estado = createdLead.Estado,
                    Sucursal = createdLead.Sucursal,
                    FechaCreacion = createdLead.FechaCreacion
                };

                return CreatedAtAction(nameof(GetLeads), new { id = leadDto.Id }, leadDto);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }
    }
}