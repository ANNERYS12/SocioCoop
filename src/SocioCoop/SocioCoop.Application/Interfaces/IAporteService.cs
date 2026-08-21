using SocioCoop.Application.DTOs;

namespace SocioCoop.Application.Interfaces
{
    public interface IAporteService
    {
        Task<AporteDto> RegistrarAsync(RegistrarAporteDto dto);
        Task<IEnumerable<AporteDto>> ObtenerPorSocioAsync(int socioId);
    }
}
