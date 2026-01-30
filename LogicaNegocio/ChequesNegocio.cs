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
    public class ChequesNegocio
    {
        ChequesDatos chequesDatos = new ChequesDatos();
        public DataTable BuscarCheques(int CodTipoPago, string FechaInicio, string FechaFin)
        {
            return chequesDatos.BuscarCheques(CodTipoPago, FechaInicio, FechaFin);
        }
        public DataTable CargarTipoPagoCheque()
        {
            return chequesDatos.CargarTipoPagoCheque();
        }
        public DataTable ObtenerPagosContratistas()
        {
            return chequesDatos.ObtenerPagosContratistas();
        }
        public DataTable ObtenerContratosPagarContratistaCheque(string TextoContratos)
        {
            return chequesDatos.ObtenerContratosPagarContratistaCheque(TextoContratos);
        }
        public DataTable CargarTipoMoneda()
        {
            return chequesDatos.CargarTipoMoneda();
        }
        public DataTable ObtenerTipoCambio(int CodTipoMoneda, double MontoCambio)
        {
            return chequesDatos.ObtenerTipoCambio(CodTipoMoneda, MontoCambio);
        }
        public DataTable ObtenerMontoPagarLetras(double MontoNumerico)
        {
            return chequesDatos.ObtenerMontoPagarLetras(MontoNumerico);
        }
        public String RegistrarCheque(ChequesEntidad chequesEntidad)
        {
            return chequesDatos.RegistrarCheque(chequesEntidad);
        }
        public String ModificarCheque(ChequesEntidad chequesEntidad)
        {
            return chequesDatos.ModificarCheque(chequesEntidad);
        }
        public DataTable ObtenerDatosChequeResumen(int CodCheque)
        {
            return chequesDatos.ObtenerDatosChequeResumen(CodCheque);
        }
        public DataTable ObtenerDatosPersonaPagar(string NumeroDocumentoIdentidad, int CodTipoDocumentoIdentidad)
        {
            return chequesDatos.ObtenerDatosPersonaPagar(NumeroDocumentoIdentidad, CodTipoDocumentoIdentidad);
        }
        public string LimpiarModificarChequePagoContratistas(int CodCheque, int CodUsuario)
        {
            return chequesDatos.LimpiarModificarChequePagoContratistas(CodCheque, CodUsuario);
        }
        public DataTable ObtenerDatosCheque(int CodCheque)
        {
            return chequesDatos.ObtenerDatosCheque(CodCheque);
        }
        public string EmitirCheque(int CodCheque, int CodUsuario)
        {
            return chequesDatos.EmitirCheque(CodCheque, CodUsuario);
        }
        public DataTable ObtenerDatosChequePersonaContratista(int CodCheque)
        {
            return chequesDatos.ObtenerDatosChequePersonaContratista(CodCheque);
        }
        public string AnularCheque(int CodCheque, int CodUsuario)
        {
            return chequesDatos.AnularCheque(CodCheque, CodUsuario);
        }
        public DataTable ObtenerDatosChequeReporteIndividual(int CodCheque)
        {
            return chequesDatos.ObtenerDatosChequeReporteIndividual(CodCheque);
        }
        public DataTable ObtenerProyectosPagarProveedoresCheque(string TextoProyectos)
        {
            return chequesDatos.ObtenerProyectosPagarProveedoresCheque(TextoProyectos);
        }
        public string LimpiarModificarChequePagoProveedores(int CodCheque, int CodUsuario)
        {
            return chequesDatos.LimpiarModificarChequePagoProveedores(CodCheque, CodUsuario);
        }
        public DataTable ObtenerDatosChequePersonaProveedor(int CodCheque)
        {
            return chequesDatos.ObtenerDatosChequePersonaProveedor(CodCheque);
        }
        public DataTable CargarEstadoCheque()
        {
            return chequesDatos.CargarEstadoCheque();
        }
    }
}
