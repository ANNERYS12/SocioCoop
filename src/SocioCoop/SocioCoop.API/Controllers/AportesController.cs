using Microsoft.AspNetCore.Mvc;
using SocioCoop.Application.DTOs;
using SocioCoop.Application.Interfaces;

namespace SocioCoop.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AportesController : ControllerBase
    {
        private readonly IAporteService _aporteService;

        public AportesController(IAporteService aporteService)
        {
            _aporteService = aporteService;
        }

        [HttpPost]
        public async Task<ActionResult<AporteDto>> RegistrarAporte([FromBody] RegistrarAporteDto dto)
        {
            try
            {
                var aporte = await _aporteService.RegistrarAsync(dto);
                return Created($"api/aportes?socioId={aporte.SocioId}", aporte);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [HttpGet("socio/{socioId}")]
        public async Task<ActionResult<IEnumerable<AporteDto>>> ObtenerPorSocio(int socioId)
        {
            var aportes = await _aporteService.ObtenerPorSocioAsync(socioId);
            return Ok(aportes);
        }
    }
}
