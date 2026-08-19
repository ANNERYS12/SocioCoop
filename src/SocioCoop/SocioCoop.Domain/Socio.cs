using System;
using System.Collections.Generic;

namespace SocioCoop.Domain
{
    public class Socio : EntidadBase
    {
        public string Nombre { get; set; } = string.Empty;
        public string Cedula { get; set; } = string.Empty;
        public decimal BalanceAportes { get; set; }

        public List<Aporte> Aportes { get; set; } = new List<Aporte>();

        public Socio() { }

        public Socio(string nombre, string cedula)
        {
            Nombre = nombre;
            Cedula = cedula;
            BalanceAportes = 0;
        }

        public override string ObtenerResumen()
        {
            return $"Socio: {Nombre} | Cédula: {Cedula} | Balance Total: RD${BalanceAportes}";
        }
    }
}