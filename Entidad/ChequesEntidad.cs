using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class ChequesEntidad
    {
        public string FechaPago { get; set; }
        public string NumeroCheque { get; set; }
        public string NumeroReciboPago { get; set; }
        public string NumeroDocumentoIdentidadPersona { get; set; }
        public string NombreCompletoPersona { get; set; }
        public string MontoTotalLetras { get; set; }
        public double MontoTotalNumerico { get; set; }
        public int CodTipoDocumentoIdentidadPersona { get; set; }
        public int CodTipoPersona { get; set; }
        public int CodTipoMoneda{ get; set; }
        public int CodTipoReciboPago { get; set; }
        public int CodCheque { get; set; }
        public int CodUsuarioRegistro { get; set; }
        public int CodUsuarioModifica { get; set; }
    }
}
