using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccesoDatos;

namespace LogicaNegocio
{
    public class MantenimientoNegocio
    {
        MantenimientoDatos mantenimientoDatos = new MantenimientoDatos();
        public DataTable ObtenerDatosGeneral()
        {
            return mantenimientoDatos.ObtenerParametrosPlanilla();
        }
        public DataTable ObtenerParametrosPerfilPlanilla(int CodPerfilPlanilla)
        {
            return mantenimientoDatos.ObtenerParametrosPerfilPlanilla(CodPerfilPlanilla);
        }
        public string RegistrarPerfilPlanilla(string NombrePerfilPlanilla, double Jornal, int CodUsuarioRegistro)
        {
            return mantenimientoDatos.RegistrarPerfilPlanilla(NombrePerfilPlanilla, Jornal, CodUsuarioRegistro);
        }
        public string ActualizarPerfilPlanilla(string NombrePerfilPlanilla, double Jornal, int CodUsuarioModifica, int CodPerfilPlanilla)
        {
            return mantenimientoDatos.ActualizarPerfilPlanilla(NombrePerfilPlanilla, Jornal, CodUsuarioModifica, CodPerfilPlanilla);
        }
        public string RegistrarParametroPerfilPlanilla(int CodPerdilPlanilla, int CodParametro, string CampoParametro)
        {
            return mantenimientoDatos.RegistrarParametroPerfilPlanilla(CodPerdilPlanilla, CodParametro, CampoParametro);
        }
        public string ActualizarParametroPerfilPlanilla(int CodPerdilPlanilla, int CodParametro, string CampoParametro)
        {
            return mantenimientoDatos.ActualizarParametroPerfilPlanilla(CodPerdilPlanilla, CodParametro, CampoParametro);
        }
        public DataTable BuscarPerfilPlanilla(string TextoBuscar)
        {
            return mantenimientoDatos.BuscarPerfilPlanilla(TextoBuscar);
        }
        public DataTable BuscarLaborTrabajo(string TextoBuscar)
        {
            return mantenimientoDatos.BuscarLaborTrabajo(TextoBuscar);
        }
        public DataTable ObtenerLaborTrabajo()
        {
            return mantenimientoDatos.ObtenerLaborTrabajo();
        }
        public string RegistrarLaborTrabajador(string NombreLaborTrabajador)
        {
            return mantenimientoDatos.RegistrarLaborTrabajador(NombreLaborTrabajador);
        }
        public string ModificarLaborTrabajador(string NombreLaborTrabajador, int CodLaborTrabajador)
        {
            return mantenimientoDatos.ModificarLaborTrabajador(NombreLaborTrabajador, CodLaborTrabajador);
        }
        public DataTable ObtenerDatosLaborTrabajo(int CodLaborTrabajador)
        {
            return mantenimientoDatos.ObtenerDatosLaborTrabajo(CodLaborTrabajador);
        }
        public DataTable ObtenerServicios()
        {
            return mantenimientoDatos.ObtenerServicios();
        }
        public string RegistrarServicios(string NombreServicio, string UnidadMedida, double CostoUnitario)
        {
            return mantenimientoDatos.RegistrarServicios(NombreServicio, UnidadMedida, CostoUnitario);
        }
        public string ActualizarServicio(string NombreServicio, string UnidadMedida, double CostoUnitario, int CodServicio)
        {
            return mantenimientoDatos.ActualizarServicio(NombreServicio, UnidadMedida, CostoUnitario, CodServicio);
        }
        public DataTable ObtenerDatosServicio(int CodServicio)
        {
            return mantenimientoDatos.ObtenerDatosServicio(CodServicio);
        }
        public string EliminarPerfilPlanilla(int CodPerfilPlanilla, int CodUsuarioModifica)
        {
            return mantenimientoDatos.EliminarPerfilPlanilla(CodPerfilPlanilla, CodUsuarioModifica);
        }
        public DataTable ObtenerTipoAportaciones()
        {
            return mantenimientoDatos.ObtenerTipoAportaciones();
        }
        public DataTable ObtenerDatosAportacion(int CodTipoAportacion)
        {
            return mantenimientoDatos.ObtenerDatosAportacion(CodTipoAportacion);
        }
        public string ModificarAportacion(double AporteObligatorio, double ComisionFlujo, double PrimaSeguro, double AporteComplementario, double ComisionMixta, int CodAportacion, int CodUsuarioModificacion)
        {
            return mantenimientoDatos.ModificarAportacion(AporteObligatorio, ComisionFlujo, PrimaSeguro, AporteComplementario, ComisionMixta, CodAportacion, CodUsuarioModificacion);
        }
    }
}
