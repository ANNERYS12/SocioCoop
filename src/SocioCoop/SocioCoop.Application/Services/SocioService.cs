using Microsoft.EntityFrameworkCore;
using SocioCoop.Application.DTOs;
using SocioCoop.Application.Interfaces;
using SocioCoop.Domain;
using SocioCoop.Infraestructure.Data;

namespace SocioCoop.Application.Services
{
    public class SocioService : ISocioService
    {
        private readonly AplicationDbContext _context;

        public SocioService(AplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SocioDto>> ObtenerTodosAsync()
        {
            return await _context.Socios
                .Select(s => new SocioDto
                {
                    Id = s.Id,
                    Nombre = s.Nombre,
                    Cedula = s.Cedula,
                    BalanceAportes = _context.Aportes
                        .Where(a => a.SocioId == s.Id && a.Activo)
                        .Sum(a => a.Monto)
                }).ToListAsync();
        }

        public async Task<SocioDto?> ObtenerPorIdAsync(int id)
        {
            var socio = await _context.Socios.FindAsync(id);
            if (socio == null) return null;

            var balance = await _context.Aportes
                .Where(a => a.SocioId == id && a.Activo)
                .SumAsync(a => a.Monto);

            return new SocioDto
            {
                Id = socio.Id,
                Nombre = socio.Nombre,
                Cedula = socio.Cedula,
                BalanceAportes = balance
            };
        }

        public async Task<SocioDto> CrearAsync(CrearSocioDto dto)
        {
            var nuevoSocio = new Socio(dto.Nombre, dto.Cedula);

            _context.Socios.Add(nuevoSocio);
            await _context.SaveChangesAsync();

            if (dto.AporteInicial > 0)
            {
                var aporte = new Aporte
                {
                    SocioId = nuevoSocio.Id,
                    Monto = dto.AporteInicial,
                    Concepto = "Aporte Inicial"
                };
                _context.Aportes.Add(aporte);
                await _context.SaveChangesAsync();
            }

            return new SocioDto
            {
                Id = nuevoSocio.Id,
                Nombre = nuevoSocio.Nombre,
                Cedula = nuevoSocio.Cedula,
                BalanceAportes = dto.AporteInicial
            };
        }

        public async Task<bool> EliminarAsync(int id)
        {
            var socio = await _context.Socios.FindAsync(id);
            if (socio == null) return false;

            _context.Socios.Remove(socio);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}