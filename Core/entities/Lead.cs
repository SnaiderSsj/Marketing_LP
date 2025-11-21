
namespace Marketing_LP.Core.Entities
{
    public class Lead
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string ProductoInteres { get; set; } = string.Empty;
        public string Fuente { get; set; } = string.Empty;
        public string Estado { get; set; } = "nuevo";
        public string Sucursal { get; set; } = "La Paz";
        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
        public DateTime FechaActualizacion { get; set; } = DateTime.UtcNow;
    }
}