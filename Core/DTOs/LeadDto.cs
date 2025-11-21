namespace Marketing_LP.Core.DTOs
{
    public class LeadDto
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string ProductoInteres { get; set; } = string.Empty;
        public string Fuente { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
        public string Sucursal { get; set; } = string.Empty;
        public DateTime FechaCreacion { get; set; }
    }

    public class CreateLeadDto
    {
        public string Nombre { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string ProductoInteres { get; set; } = string.Empty;
        public string Fuente { get; set; } = string.Empty;
    }

    public class LeadsEstadisticasDto
    {
        public int TotalLeads { get; set; }
        public int LeadsNuevos { get; set; }
        public int LeadsContactados { get; set; }
        public Dictionary<string, int> LeadsPorFuente { get; set; } = new();
    }
}