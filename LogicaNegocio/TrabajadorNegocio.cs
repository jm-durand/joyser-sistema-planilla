using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccesoDatos;
using Entidad;

namespace LogicaNegocio
{
    public class TrabajadorNegocio
    {
        TrabajadorDatos trabajadorDatos = new TrabajadorDatos();
        public DataTable ObtenerPerfilPlanilla()
        {
            return trabajadorDatos.ObtenerPerfilPlanilla();
        }
        public DataTable ObtenerListaTrabajadores()
        {
            return trabajadorDatos.ObtenerListaTrabajadores();
        }
        public DataTable ObtenerListaTrabajadoresEventuales()
        {
            return trabajadorDatos.ObtenerListaTrabajadoresEventuales();
        }
        public DataTable ObtenerListaTrabajadoresEmpleados()
        {
            return trabajadorDatos.ObtenerListaTrabajadoresEmpleados();
        }
        public DataTable ObtenerListaTrabajadoresReportes()
        {
            return trabajadorDatos.ObtenerListaTrabajadoresReportes();
        }
        public DataTable ObtenerListaTrabajadoresReporteIndividual(int CodTipoPlanilla)
        {
            return trabajadorDatos.ObtenerListaTrabajadoresReporteIndividual(CodTipoPlanilla);
        }
        public String RegistrarTrabajador(TrabajadorEntidad trabajadorEntidad)
        {
            return trabajadorDatos.RegistrarTrabajador(trabajadorEntidad);
        }
        public String ActualizarTrabajador(TrabajadorEntidad trabajadorEntidad)
        {
            return trabajadorDatos.ActualizarTrabajador(trabajadorEntidad);
        }
        public String EliminarTrabajador(TrabajadorEntidad trabajadorEntidad)
        {
            return trabajadorDatos.EliminarTrabajador(trabajadorEntidad);
        }
        public String RegistrarTrabajadorEventuales(TrabajadorEntidad trabajadorEntidad)
        {
            return trabajadorDatos.RegistrarTrabajadorEventuales(trabajadorEntidad);
        }
        public String ActualizarTrabajadorEventuales(TrabajadorEntidad trabajadorEntidad)
        {
            return trabajadorDatos.ActualizarTrabajadorEventuales(trabajadorEntidad);
        }
        public String EliminarTrabajadorEventuales(TrabajadorEntidad trabajadorEntidad)
        {
            return trabajadorDatos.EliminarTrabajadorEventuales(trabajadorEntidad);
        }
        public DataTable ObtenerDatosTrabajadorBoletaPlanillaPrevio(int CodTrabajador)
        {
            return trabajadorDatos.ObtenerDatosTrabajadorBoletaPlanillaPrevio(CodTrabajador);
        }
        public DataTable ObtenerDatosTrabajadorEventualPlanillaPrevio(int CodTrabajador)
        {
            return trabajadorDatos.ObtenerDatosTrabajadorEventualPlanillaPrevio(CodTrabajador);
        }
        public DataTable BuscarTrabajador(string TextoBuscar)
        {
            return trabajadorDatos.BuscarTrabajador(TextoBuscar);
        }
        public DataTable BuscarTrabajadoresEventuales(string TextoBuscar)
        {
            return trabajadorDatos.BuscarTrabajadoresEventuales(TextoBuscar);
        }
        public DataTable ObtenerDatosTrabajador(int CodTrabajador, int CodTipoPlanilla)
        {
            return trabajadorDatos.ObtenerDatosTrabajador(CodTrabajador, CodTipoPlanilla);
        }
        public DataTable ObtenerDatosTrabajadorDocumentoIdentidad(string DocumentoIdentidad, int CodTipoDocumentoIdentidad)
        {
            return trabajadorDatos.ObtenerDatosTrabajadorDocumentoIdentidad(DocumentoIdentidad, CodTipoDocumentoIdentidad);
        }
        public DataTable ObtenerListaLaborTrabajador()
        {
            return trabajadorDatos.ObtenerListaLaborTrabajador();
        }
        public DataTable ObtenerLaborTrabajo()
        {
            return trabajadorDatos.ObtenerLaborTrabajo();
        }
        public DataTable ObtenerServicios()
        {
            return trabajadorDatos.ObtenerServicios();
        }
        public DataTable ObtenerListadoTrabajadores(int CodProyecto)
        {
            return trabajadorDatos.ObtenerListadoTrabajadores(CodProyecto);
        }
        public DataTable ObtenerProyectoPlanillaTrabajadores(int CodProyectoPlanilla)
        {
            return trabajadorDatos.ObtenerProyectoPlanillaTrabajadores(CodProyectoPlanilla);
        }
        public DataTable ObtenerProyectoPlanillaTrabajadoresEventuales(int CodProyectoPlanilla)
        {
            return trabajadorDatos.ObtenerProyectoPlanillaTrabajadoresEventuales(CodProyectoPlanilla);
        }
        public DataTable CargarTipoDocumentoIdentidad()
        {
            return trabajadorDatos.CargarTipoDocumentoIdentidad();
        }
    }
}
