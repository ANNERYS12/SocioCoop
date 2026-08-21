using Microsoft.AspNetCore.Mvc;
using SocioCoop.Application.DTOs;
using SocioCoop.Application.Interfaces;

namespace SocioCoop.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SociosController : ControllerBase
    {
        private readonly ISocioService _socioService;

        public SociosController(ISocioService socioService)
        {
            _socioService = socioService;
        }

        // GET: api/socios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SocioDto>>> GetSocios()
        {
            var socios = await _socioService.ObtenerTodosAsync();
            return Ok(socios);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<SocioDto>> GetSocio(int id)
        {
            var socio = await _socioService.ObtenerPorIdAsync(id);
            if (socio == null)
            {
                return NotFound(new { mensaje = $"No se encontró el socio con ID {id}" });
            }
            return Ok(socio);
        }

        // POST: api/socios
        [HttpPost]
        public async Task<ActionResult<SocioDto>> CrearSocio([FromBody] CrearSocioDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var nuevoSocio = await _socioService.CrearAsync(dto);
            return CreatedAtAction(nameof(GetSocio), new { id = nuevoSocio.Id }, nuevoSocio);
        }

        // DELETE: api/socios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarSocio(int id)
        {
            var eliminado = await _socioService.EliminarAsync(id);
            if (!eliminado)
            {
                return NotFound(new { mensaje = $"No se encontró el socio con ID {id}" });
            }
            return NoContent();
        }
    }
}