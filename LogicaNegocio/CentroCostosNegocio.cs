using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccesoDatos;

namespace LogicaNegocio
{
    public class CentroCostosNegocio
    {
        CentroCostosDatos costosDatos = new CentroCostosDatos();
        public string RegistrarProyectoPlanilla(string NombreProyecto, double Presupuesto, string FechaInicio, string FechaFin, int CodUsuarioRegistro)
        {
            return costosDatos.RegistrarProyectoPlanilla(NombreProyecto, Presupuesto, FechaInicio, FechaFin, CodUsuarioRegistro);
        }
        public string ActualizarProyectoPlanilla(string NombreProyecto, double Presupuesto, string FechaFin, int CodUsuarioModifica, int CodProyectoPlanilla)
        {
            return costosDatos.ActualizarProyectoPlanilla(NombreProyecto, Presupuesto, FechaFin, CodUsuarioModifica, CodProyectoPlanilla);
        }
        public DataTable BuscarProyectoPlanilla(string TextoBuscar)
        {
            return costosDatos.BuscarProyectoPlanilla(TextoBuscar);
        }
        public DataTable BuscarProyectoPlanillaTrabajadores(string TextoBuscar)
        {
            return costosDatos.BuscarProyectoPlanillaTrabajadores(TextoBuscar);
        }
        public DataTable CargarDatosProyectoPlanilla(int CodProyectoPlanilla)
        {
            return costosDatos.CargarDatosProyectoPlanilla(CodProyectoPlanilla);
        }
        public DataTable CargarProyectoPlanilla()
        {
            return costosDatos.CargarProyectoPlanilla();
        }
        public string RegistrarProyectoPlanillaTrabajador(int CodProyectoPlanilla, int CodTrabajador, int CodUsuarioRegistro)
        {
            return costosDatos.RegistrarProyectoPlanillaTrabajador(CodProyectoPlanilla, CodTrabajador, CodUsuarioRegistro);
        }
        public DataTable CargarTrabajadoresProyectoPlanilla(int CodProyectoPlanilla)
        {
            return costosDatos.CargarTrabajadoresProyectoPlanilla(CodProyectoPlanilla);
        }
        public string DesactivarTrabajadorProyectoPlanilla(int CodProyectoPlanilla, int CodTrabajador, int CodUsuarioModifica)
        {
            return costosDatos.DesactivarTrabajadorProyectoPlanilla(CodProyectoPlanilla, CodTrabajador, CodUsuarioModifica);
        }
        public DataTable BuscarProyectoPlanillaProveedores(string TextoBuscar)
        {
            return costosDatos.BuscarProyectoPlanillaProveedores(TextoBuscar);
        }
        public string RegistrarProyectoPlanillaProveedor(int CodProyectoPlanilla, int CodProveedor, int CodUsuarioRegistro)
        {
            return costosDatos.RegistrarProyectoPlanillaProveedor(CodProyectoPlanilla, CodProveedor, CodUsuarioRegistro);
        }
        public DataTable CargarProveedoresProyectoPlanilla(int CodProyectoPlanilla)
        {
            return costosDatos.CargarProveedoresProyectoPlanilla(CodProyectoPlanilla);
        }
        public string DesactivarProveedorProyectoPlanilla(int CodProyectoPlanilla, int CodProveedor, int CodUsuarioModifica)
        {
            return costosDatos.DesactivarProveedorProyectoPlanilla(CodProyectoPlanilla, CodProveedor, CodUsuarioModifica);
        }
    }
}
