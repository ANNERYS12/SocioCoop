namespace SocioCoop.Application.DTOs
{
    public class AporteDto
    {
        public int Id { get; set; }
        public int SocioId { get; set; }
        public decimal Monto { get; set; }
        public string Concepto { get; set; } = string.Empty;
        public DateTime FechaCreacion { get; set; }
    }

    public class RegistrarAporteDto
    {
        public int SocioId { get; set; }
        public decimal Monto { get; set; }
        public string? Concepto { get; set; }
    }
}