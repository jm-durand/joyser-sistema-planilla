using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class ProveedoresEntidad
    {
        public string DescripcionProveedor { get; set; }
        public string Ruc { get; set; }
        public string Ciudad { get; set; }
        public string Direccion { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public string NumMedioPago { get; set; }
        public string FechaPago { get; set; }
        public string NumReciboPago { get; set; }
        public int CodModoPago { get; set; }
        public int CodMedioPago { get; set; }
        public int CodTipoReciboPago { get; set; }
        public int CodCheque { get; set; }
        public int CodRubroProveedor { get; set; }
        public int CodUsuarioRegistro { get; set; }
        public int CodUsuarioModifica { get; set; }
        public int CodProveedor { get; set; }
        public int CodPagoProveedor { get; set; }
        public int CodProveedorProyectoPlanilla { get; set; }
        public double MontoPagar { get; set; }

    }
}
