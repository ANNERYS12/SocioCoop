using SocioCoop.Application.DTOs;

namespace SocioCoop.Application.Interfaces
{
    public interface ISocioService
    {
        Task<IEnumerable<SocioDto>> ObtenerTodosAsync();
        Task<SocioDto?> ObtenerPorIdAsync(int id);
        Task<SocioDto> CrearAsync(CrearSocioDto dto);
    }
}