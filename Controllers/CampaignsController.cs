using Microsoft.AspNetCore.Mvc;
using Marketing_LP.Core.Entities;

namespace Marketing_LP.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CampaignsController : ControllerBase
    {
        // Datos temporales en memoria (eliminar cuando tengamos base de datos)
        private static List<Campaign> _campaigns = new List<Campaign>
        {
            new Campaign
            {
                Id = Guid.NewGuid(),
                Nombre = "Promo Verano 2024",
                Descripcion = "Promoción de productos lácteos para verano",
                Tipo = "email",
                Presupuesto = 5000,
                FechaInicio = DateTime.UtcNow.AddDays(-30),
                FechaFin = DateTime.UtcNow.AddDays(30),
                Estado = "activa",
                Sucursal = "La Paz",
                LeadsGenerados = 45,
                VentasGeneradas = 25000,
                FechaCreacion = DateTime.UtcNow,
                FechaActualizacion = DateTime.UtcNow
            },
            new Campaign
            {
                Id = Guid.NewGuid(),
                Nombre = "Feria Alimentaria LP",
                Descripcion = "Participación en feria internacional de alimentos",
                Tipo = "evento",
                Presupuesto = 15000,
                FechaInicio = DateTime.UtcNow.AddDays(-15),
                FechaFin = DateTime.UtcNow.AddDays(45),
                Estado = "activa",
                Sucursal = "La Paz",
                LeadsGenerados = 120,
                VentasGeneradas = 75000,
                FechaCreacion = DateTime.UtcNow,
                FechaActualizacion = DateTime.UtcNow
            }
        };

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Campaign>>> GetCampaigns()
        {
            try
            {
                return Ok(_campaigns.Where(c => c.Sucursal == "La Paz"));
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
                return Ok(_campaigns.Where(c => c.Sucursal == "La Paz" && c.Estado == "activa"));
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
                var campaign = _campaigns.FirstOrDefault(c => c.Id == id && c.Sucursal == "La Paz");
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

                _campaigns.Add(campaign);

                return CreatedAtAction(nameof(GetCampaigns), new { id = campaign.Id }, campaign);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }
    }
}