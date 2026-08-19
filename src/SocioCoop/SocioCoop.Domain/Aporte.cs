using System;

namespace SocioCoop.Domain
{
    public class Aporte : EntidadBase
    {
        public int SocioId { get; set; }
        public decimal Monto { get; set; }
        public string Concepto { get; set; } = string.Empty;

        public Socio? Socio { get; set; }

        public Aporte() { }

        public void Registrar(decimal monto)
        {
            Monto = monto;
            Concepto = "Aporte Ordinario";
        }

        public void Registrar(decimal monto, string concepto)
        {
            Monto = monto;
            Concepto = concepto;
        }

        public override string ObtenerResumen()
        {
            return $"Aporte #{Id}: RD${Monto} - Concepto: {Concepto}";
        }
    }
}