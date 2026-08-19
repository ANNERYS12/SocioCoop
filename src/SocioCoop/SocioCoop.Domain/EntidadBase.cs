using System;

namespace SocioCoop.Domain
{
    public abstract class EntidadBase
    {
        public int Id { get; set; }
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
        public bool Activo { get; set; } = true;

        public abstract string ObtenerResumen();
    }
}