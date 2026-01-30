using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class TrabajadorEntidad
    {
        public string FechaNacimiento { get; set; }
        public string FechaIngreso { get; set; }
        public string FechaCese { get; set; }
        public string NumeroDocumentoIdentidad { get; set; }
        public string Nombres { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Sexo { get; set; }
        public int CodEstadoCivil { get; set; }
        public int CodTipoAportacion { get; set; }
        public int CodTipoTrabajo { get; set; }
        public int CodTipoDocumentoIdentidad { get; set; }
        public int CodCargo { get; set; }
        public int CodBanco { get; set; }
        public int CodTipoPlanilla { get; set; }
        public int CodPerfilPlanilla { get; set; }
        public int CodUsuarioRegistro { get; set; }
        public int CodUsuarioModifica { get; set; }
        public int CodTrabajador { get; set; }
        public double HaberMensual { get; set; }
        public string NumeroCuspp { get; set; }
        public string NumeroCuentaBanco { get; set; }
        public string NumeroCuentaCTS{ get; set; }
        
    }
}
