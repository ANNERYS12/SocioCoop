namespace SocioCoop.Application.DTOs
{
    public class SocioDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Cedula { get; set; } = string.Empty;
        public decimal BalanceAportes { get; set; }
    }

    public class CrearSocioDto
    {
        public string Nombre { get; set; } = string.Empty;
        public string Cedula { get; set; } = string.Empty;
    }
}