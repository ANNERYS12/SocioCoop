using Microsoft.EntityFrameworkCore;
using SocioCoop.Application.DTOs;
using SocioCoop.Application.Interfaces;
using SocioCoop.Domain;
using SocioCoop.Infraestructure.Data;

namespace SocioCoop.Application.Services
{
    public class AporteService : IAporteService
    {
        private readonly AplicationDbContext _context;

        public AporteService(AplicationDbContext context)
        {
            _context = context;
        }

        public async Task<AporteDto> RegistrarAsync(RegistrarAporteDto dto)
        {
            var socio = await _context.Socios.FindAsync(dto.SocioId);
            if (socio == null)
                throw new Exception($"No se encontró el socio con ID {dto.SocioId}");

            var aporte = new Aporte
            {
                SocioId = dto.SocioId,
                Monto = dto.Monto,
                Concepto = string.IsNullOrWhiteSpace(dto.Concepto) ? "Aporte Ordinario" : dto.Concepto
            };

            _context.Aportes.Add(aporte);
            await _context.SaveChangesAsync();

            return new AporteDto
            {
                Id = aporte.Id,
                SocioId = aporte.SocioId,
                Monto = aporte.Monto,
                Concepto = aporte.Concepto,
                FechaCreacion = aporte.FechaCreacion
            };
        }

        public async Task<IEnumerable<AporteDto>> ObtenerPorSocioAsync(int socioId)
        {
            return await _context.Aportes
                .Where(a => a.SocioId == socioId && a.Activo)
                .Select(a => new AporteDto
                {
                    Id = a.Id,
                    SocioId = a.SocioId,
                    Monto = a.Monto,
                    Concepto = a.Concepto,
                    FechaCreacion = a.FechaCreacion
                }).ToListAsync();
        }
    }
}
