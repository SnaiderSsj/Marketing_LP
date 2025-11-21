namespace Marketing_LP.Core.Entities
{
    public class Campaign
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public string Tipo { get; set; } = string.Empty;
        public decimal Presupuesto { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string Estado { get; set; } = "activa";
        public string Sucursal { get; set; } = "La Paz";
        public int LeadsGenerados { get; set; }
        public decimal VentasGeneradas { get; set; }
        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
        public DateTime FechaActualizacion { get; set; } = DateTime.UtcNow;

        public decimal ROI => Presupuesto > 0 ? (VentasGeneradas - Presupuesto) / Presupuesto * 100 : 0;
    }
}