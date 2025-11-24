using Microsoft.AspNetCore.Mvc;
using Marketing_LP.Core.Entities;
using Marketing_LP.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Marketing_LP.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CampaignsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CampaignsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Campaign>>> GetCampaigns()
        {
            try
            {
                var campaigns = await _context.Campaigns
                    .Where(c => c.Sucursal == "La Paz")
                    .OrderByDescending(c => c.FechaCreacion)
                    .ToListAsync();

                return Ok(campaigns);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }

        [HttpGet("activas")]
        public async Task<ActionResult<IEnumerable<Campaign>>> GetCampaignsActivas()
        {
            try
            {
                var campaigns = await _context.Campaigns
                    .Where(c => c.Sucursal == "La Paz" && c.Estado == "activa")
                    .OrderByDescending(c => c.FechaInicio)
                    .ToListAsync();

                return Ok(campaigns);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Campaign>> GetCampaign(Guid id)
        {
            try
            {
                var campaign = await _context.Campaigns
                    .FirstOrDefaultAsync(c => c.Id == id && c.Sucursal == "La Paz");

                if (campaign == null)
                {
                    return NotFound($"Campaña con ID {id} no encontrada");
                }
                return Ok(campaign);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Campaign>> CreateCampaign([FromBody] Campaign campaign)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                campaign.Id = Guid.NewGuid();
                campaign.Sucursal = "La Paz";
                campaign.FechaCreacion = DateTime.UtcNow;
                campaign.FechaActualizacion = DateTime.UtcNow;

                _context.Campaigns.Add(campaign);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetCampaigns), new { id = campaign.Id }, campaign);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }

        [HttpGet("estadisticas/roi")]
        public async Task<ActionResult<object>> GetROIEstadisticas()
        {
            try
            {
                var campaigns = await _context.Campaigns
                    .Where(c => c.Sucursal == "La Paz")
                    .Select(c => new {
                        c.Nombre,
                        c.Presupuesto,
                        c.VentasGeneradas,
                        ROI = c.Presupuesto > 0 ? ((c.VentasGeneradas - c.Presupuesto) / c.Presupuesto) * 100 : 0
                    })
                    .ToListAsync();

                return Ok(campaigns);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }
    }
}