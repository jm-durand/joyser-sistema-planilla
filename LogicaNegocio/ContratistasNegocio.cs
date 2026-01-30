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
    public class ContratistasNegocio
    {
        ContratistasDatos contratistasDatos = new ContratistasDatos();
        public DataTable BuscarContratistas(string TextoBuscar)
        {
            return contratistasDatos.BuscarContratistas(TextoBuscar);
        }
        public DataTable BuscarContratistasContratos(string TextoBuscar)
        {
            return contratistasDatos.BuscarContratistasContratos(TextoBuscar);
        }
        public String RegistrarContratista(ContratistasEntidad contratistasEntidad)
        {
            return contratistasDatos.RegistrarContratista(contratistasEntidad);
        }
        public String ModificarContratista(ContratistasEntidad contratistasEntidad)
        {
            return contratistasDatos.ModificarContratista(contratistasEntidad);
        }
        public string EliminarContratista(int CodContratista, int CodUsuario)
        {
            return contratistasDatos.EliminarContratista(CodContratista, CodUsuario);
        }
        public DataTable CargarDatosContratista(int CodContratista)
        {
            return contratistasDatos.CargarDatosContratista(CodContratista);
        }
        public DataTable CargarContratistas()
        {
            return contratistasDatos.CargarContratistas();
        }
        public DataTable CargarModosPago()
        {
            return contratistasDatos.CargarModosPago();
        }
        public String RegistrarContratoContratista(ContratistasEntidad contratistasEntidad)
        {
            return contratistasDatos.RegistrarContratoContratista(contratistasEntidad);
        }
        public String ModificarContratoContratista(ContratistasEntidad contratistasEntidad)
        {
            return contratistasDatos.ModificarContratoContratista(contratistasEntidad);
        }
        public DataTable CargarDatosContrato(int CodContrato)
        {
            return contratistasDatos.CargarDatosContrato(CodContrato);
        }
        public string EliminarContrato(int CodContrato, int CodUsuario)
        {
            return contratistasDatos.EliminarContrato(CodContrato, CodUsuario);
        }
        public DataTable ObtenerContratosContratista(int CodContratisa, int codEstadoPago)
        {
            return contratistasDatos.ObtenerContratosContratista(CodContratisa, codEstadoPago);
        }
        public DataTable CargarMediosPagoContratista()
        {
            return contratistasDatos.CargarMediosPagoContratista();
        }
        public DataTable ObtenerDatosContratoPago(int CodContrato)
        {
            return contratistasDatos.ObtenerDatosContratoPago(CodContrato);
        }
        public String RegistrarPagoContratistas(ContratistasEntidad contratistasEntidad)
        {
            return contratistasDatos.RegistrarPagoContratistas(contratistasEntidad);
        }
        public String ModificatPagoContratistas(ContratistasEntidad contratistasEntidad)
        {
            return contratistasDatos.ModificatPagoContratistas(contratistasEntidad);
        }
        public String EliminarPagoContratistas(ContratistasEntidad contratistasEntidad)
        {
            return contratistasDatos.EliminarPagoContratistas(contratistasEntidad);
        }
        public DataTable CargarTipoRecibosPago()
        {
            return contratistasDatos.CargarTipoRecibosPago();
        }
        public DataTable ObtenerPagosRealizados(int CodContrato)
        {
            return contratistasDatos.ObtenerPagosRealizados(CodContrato);
        }
        public DataTable ObtenerDatosPagoContratista(int CodPagoContratista)
        {
            return contratistasDatos.ObtenerDatosPagoContratista(CodPagoContratista);
        }
        public DataTable CargarContratosContratista()
        {
            return contratistasDatos.CargarContratosContratista();
        }
        public DataTable CargarContratosPagarContratista(int CodContratista)
        {
            return contratistasDatos.CargarContratosPagarContratista(CodContratista);
        }
        public String RegistrarPagoContratistasPorCheque(ContratistasEntidad contratistasEntidad)
        {
            return contratistasDatos.RegistrarPagoContratistasPorCheque(contratistasEntidad);
        }
    }
}
