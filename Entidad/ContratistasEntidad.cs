using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class ContratistasEntidad
    {
        public string FechaInicioVigencia { get; set; }
        public string FechaFinVigencia { get; set; }
        public string FechaInicioContrato { get; set; }
        public string DescripcionContratista { get; set; }
        public string DescripcionLaborContrato { get; set; }
        public string Ruc { get; set; }
        public string NumMedioPago { get; set; }
        public string FechaPago { get; set; }
        public string NumReciboPago { get; set; }
        public int CodContratista { get; set; }
        public int CodUsuarioRegistro { get; set; }
        public int CodUsuarioModifica { get; set; }
        public int CodProyectoPlanilla { get; set; }
        public int CodContrato { get; set; }
        public int CodModoPago { get; set; }
        public int CodMedioPago { get; set; }
        public int CodTipoReciboPago { get; set; }
        public int CodCheque { get; set; }
        public int CodPagoContratista { get; set; }
        public double MontoTotal { get; set; }
        public double MontoPagar { get; set; }

    }
}
