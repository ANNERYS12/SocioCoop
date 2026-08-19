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
                    BalanceAportes = s.BalanceAportes
                }).ToListAsync();
        }

        public async Task<SocioDto?> ObtenerPorIdAsync(int id)
        {
            var socio = await _context.Socios.FindAsync(id);
            if (socio == null) return null;

            return new SocioDto
            {
                Id = socio.Id,
                Nombre = socio.Nombre,
                Cedula = socio.Cedula,
                BalanceAportes = socio.BalanceAportes
            };
        }

        public async Task<SocioDto> CrearAsync(CrearSocioDto dto)
        {
            var nuevoSocio = new Socio(dto.Nombre, dto.Cedula);

            _context.Socios.Add(nuevoSocio);
            await _context.SaveChangesAsync();

            return new SocioDto
            {
                Id = nuevoSocio.Id,
                Nombre = nuevoSocio.Nombre,
                Cedula = nuevoSocio.Cedula,
                BalanceAportes = nuevoSocio.BalanceAportes
            };
        }
    }
}