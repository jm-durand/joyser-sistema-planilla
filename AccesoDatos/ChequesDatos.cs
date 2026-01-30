using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidad;

namespace AccesoDatos
{
    public class ChequesDatos
    {
        Conexion conexion = new Conexion();
        SqlConnection con;
        SqlCommand cmd = new SqlCommand();
        public ChequesDatos()
        {
            con = new SqlConnection(conexion.GetConexion());
        }
        public DataTable BuscarCheques(int CodTipoPago, string FechaInicio, string FechaFin)
        {
            DataTable dt = new DataTable();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_sel_BuscarCheque";
                cmd.Parameters.Add(new SqlParameter("@CodTipoPago", CodTipoPago));
                cmd.Parameters.Add(new SqlParameter("@FechaInicio", FechaInicio));
                cmd.Parameters.Add(new SqlParameter("@FechaFin", FechaFin));
                SqlDataAdapter adapter;
                adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                cmd.Parameters.Clear();
            }
            return (dt);
        }
        public DataTable CargarTipoPagoCheque()
        {
            DataTable dt = new DataTable();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_sel_CargarTipoPagoCheque";                
                SqlDataAdapter adapter;
                adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                cmd.Parameters.Clear();
            }
            return (dt);
        }
        public DataTable ObtenerPagosContratistas()
        {
            DataTable dt = new DataTable();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_sel_ObtenerPagoContratistas";
                SqlDataAdapter adapter;
                adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                cmd.Parameters.Clear();
            }
            return (dt);
        }
        public DataTable ObtenerContratosPagarContratistaCheque(string TextoContratos)
        {
            DataTable dt = new DataTable();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_sel_ObtenerListadoPagarContratistasCheque";
                cmd.Parameters.Add(new SqlParameter("@TextoContratosContratitas", TextoContratos));
                SqlDataAdapter adapter;
                adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                cmd.Parameters.Clear();
            }
            return (dt);
        }
        public DataTable CargarTipoMoneda()
        {
            DataTable dt = new DataTable();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_sel_CargarTipoMoneda";
                SqlDataAdapter adapter;
                adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                cmd.Parameters.Clear();
            }
            return (dt);
        }
        public DataTable ObtenerTipoCambio(int CodTipoMoneda, double MontoCambio)
        {
            DataTable dt = new DataTable();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_sel_CargarTipoCambio";
                cmd.Parameters.Add(new SqlParameter("@CodTipoMoneda", CodTipoMoneda));
                cmd.Parameters.Add(new SqlParameter("@MontoCambio", MontoCambio));
                SqlDataAdapter adapter;
                adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                cmd.Parameters.Clear();
            }
            return (dt);
        }
        public DataTable ObtenerMontoPagarLetras(double MontoNumerico)
        {
            DataTable dt = new DataTable();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_sel_ObtenerMontoEnLetras";
                cmd.Parameters.Add(new SqlParameter("@MontoConvertir", MontoNumerico));
                SqlDataAdapter adapter;
                adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                cmd.Parameters.Clear();
            }
            return (dt);
        }
        public String RegistrarCheque(ChequesEntidad chequesEntidad)
        {
            string mensaje;
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_ins_GuardarCheque";
                cmd.Parameters.Add(new SqlParameter("@CodTipoPagoPersona", chequesEntidad.CodTipoPersona));
                cmd.Parameters.Add(new SqlParameter("@NumeroCheque", chequesEntidad.NumeroCheque));
                cmd.Parameters.Add(new SqlParameter("@CodTipoDocumentoIdentidadPersona", chequesEntidad.CodTipoDocumentoIdentidadPersona));
                cmd.Parameters.Add(new SqlParameter("@NumeroDocumentoIdentidad", chequesEntidad.NumeroDocumentoIdentidadPersona));
                cmd.Parameters.Add(new SqlParameter("@NombresCompletosPersona", chequesEntidad.NombreCompletoPersona));
                cmd.Parameters.Add(new SqlParameter("@CodTipoMoneda", chequesEntidad.CodTipoMoneda));
                cmd.Parameters.Add(new SqlParameter("@MontoTotalNumerico", chequesEntidad.MontoTotalNumerico));
                cmd.Parameters.Add(new SqlParameter("@MontoTotalLetras", chequesEntidad.MontoTotalLetras));
                cmd.Parameters.Add(new SqlParameter("@FechaPago", chequesEntidad.FechaPago));
                cmd.Parameters.Add(new SqlParameter("@CodUsuarioRegistro", chequesEntidad.CodUsuarioRegistro));
                SqlParameter output = new SqlParameter("@Mensaje", SqlDbType.VarChar, 500);
                output.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(output);

                con.Open();
                cmd.ExecuteNonQuery();
                mensaje = output.Value.ToString();
            }
            catch (SqlException ex)
            {
                mensaje = ex.Message.ToString();
            }
            finally
            {
                con.Close();
                cmd.Parameters.Clear();
            }
            return mensaje;
        }
        public String ModificarCheque(ChequesEntidad chequesEntidad)
        {
            string mensaje;
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_ins_ModificarCheque";
                cmd.Parameters.Add(new SqlParameter("@CodTipoPagoPersona", chequesEntidad.CodTipoPersona));
                cmd.Parameters.Add(new SqlParameter("@NumeroCheque", chequesEntidad.NumeroCheque));
                cmd.Parameters.Add(new SqlParameter("@CodTipoDocumentoIdentidadPersona", chequesEntidad.CodTipoDocumentoIdentidadPersona));
                cmd.Parameters.Add(new SqlParameter("@NumeroDocumentoIdentidad", chequesEntidad.NumeroDocumentoIdentidadPersona));
                cmd.Parameters.Add(new SqlParameter("@NombresCompletosPersona", chequesEntidad.NombreCompletoPersona));
                cmd.Parameters.Add(new SqlParameter("@CodTipoMoneda", chequesEntidad.CodTipoMoneda));
                cmd.Parameters.Add(new SqlParameter("@MontoTotalNumerico", chequesEntidad.MontoTotalNumerico));
                cmd.Parameters.Add(new SqlParameter("@MontoTotalLetras", chequesEntidad.MontoTotalLetras));
                cmd.Parameters.Add(new SqlParameter("@FechaPago", chequesEntidad.FechaPago));
                cmd.Parameters.Add(new SqlParameter("@CodUsuarioModifica", chequesEntidad.CodUsuarioModifica));
                cmd.Parameters.Add(new SqlParameter("@CodCheque", chequesEntidad.CodCheque));
                SqlParameter output = new SqlParameter("@Mensaje", SqlDbType.VarChar, 500);
                output.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(output);

                con.Open();
                cmd.ExecuteNonQuery();
                mensaje = output.Value.ToString();
            }
            catch (SqlException ex)
            {
                mensaje = ex.Message.ToString();
            }
            finally
            {
                con.Close();
                cmd.Parameters.Clear();
            }
            return mensaje;
        }
        public DataTable ObtenerDatosChequeResumen(int CodCheque)
        {
            DataTable dt = new DataTable();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_sel_ObtenerDatosResumenCheque";
                cmd.Parameters.Add(new SqlParameter("@CodCheque", CodCheque));
                SqlDataAdapter adapter;
                adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                cmd.Parameters.Clear();
            }
            return (dt);
        }
        public DataTable ObtenerDatosPersonaPagar(string NumeroDocumentoIdentidad, int CodTipoDocumentoIdentidad)
        {
            DataTable dt = new DataTable();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_sel_ObtenerDatosPersonaPagarCheque";
                cmd.Parameters.Add(new SqlParameter("@NumeroDocumentoIdentidad", NumeroDocumentoIdentidad));
                cmd.Parameters.Add(new SqlParameter("@CodTipoDocumentoIdentidad", CodTipoDocumentoIdentidad));
                SqlDataAdapter adapter;
                adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                cmd.Parameters.Clear();
            }
            return (dt);
        }
        public string LimpiarModificarChequePagoContratistas(int CodCheque, int CodUsuario)
        {
            string mensaje;
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_upd_LimpiarPagoContratoContratistasPorChequeModificar";
                cmd.Parameters.Add(new SqlParameter("@CodCheque", CodCheque));
                cmd.Parameters.Add(new SqlParameter("@CodUsuarioModificar", CodUsuario));          
                SqlParameter output = new SqlParameter("@Mensaje", SqlDbType.VarChar, 500);
                output.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(output);

                con.Open();
                cmd.ExecuteNonQuery();
                mensaje = output.Value.ToString();
            }
            catch (SqlException ex)
            {
                mensaje = ex.Message.ToString();
            }
            finally
            {
                con.Close();
                cmd.Parameters.Clear();
            }
            return mensaje;
        }
        public DataTable ObtenerDatosCheque(int CodCheque)
        {
            DataTable dt = new DataTable();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_sel_ObtenerDatosCheque";
                cmd.Parameters.Add(new SqlParameter("@CodCheque", CodCheque));                
                SqlDataAdapter adapter;
                adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                cmd.Parameters.Clear();
            }
            return (dt);
        }
        public string EmitirCheque(int CodCheque, int CodUsuario)
        {
            string mensaje;
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_upd_EmitirCheque";
                cmd.Parameters.Add(new SqlParameter("@CodCheque", CodCheque));
                cmd.Parameters.Add(new SqlParameter("@CodUsuarioEmision", CodUsuario));
                SqlParameter output = new SqlParameter("@Mensaje", SqlDbType.VarChar, 500);
                output.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(output);

                con.Open();
                cmd.ExecuteNonQuery();
                mensaje = output.Value.ToString();
            }
            catch (SqlException ex)
            {
                mensaje = ex.Message.ToString();
            }
            finally
            {
                con.Close();
                cmd.Parameters.Clear();
            }
            return mensaje;
        }
        public DataTable ObtenerDatosChequePersonaContratista(int CodCheque)
        {
            DataTable dt = new DataTable();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_sel_ObtenerDatosChequeContratista";
                cmd.Parameters.Add(new SqlParameter("@CodCheque", CodCheque));
                SqlDataAdapter adapter;
                adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                cmd.Parameters.Clear();
            }
            return (dt);
        }
        public string AnularCheque(int CodCheque, int CodUsuario)
        {
            string mensaje;
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_upd_AnularCheque";
                cmd.Parameters.Add(new SqlParameter("@CodCheque", CodCheque));
                cmd.Parameters.Add(new SqlParameter("@CodUsuarioAnular", CodUsuario));
                SqlParameter output = new SqlParameter("@Mensaje", SqlDbType.VarChar, 500);
                output.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(output);

                con.Open();
                cmd.ExecuteNonQuery();
                mensaje = output.Value.ToString();
            }
            catch (SqlException ex)
            {
                mensaje = ex.Message.ToString();
            }
            finally
            {
                con.Close();
                cmd.Parameters.Clear();
            }
            return mensaje;
        }
        public DataTable ObtenerDatosChequeReporteIndividual(int CodCheque)
        {
            DataTable dt = new DataTable();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_sel_ObtenerDatosChequeReporteIndividual";
                cmd.Parameters.Add(new SqlParameter("@CodCheque", CodCheque));
                SqlDataAdapter adapter;
                adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                cmd.Parameters.Clear();
            }
            return (dt);
        }
        public DataTable ObtenerProyectosPagarProveedoresCheque(string TextoProyectos)
        {
            DataTable dt = new DataTable();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_sel_ObtenerListadoPagarProveedoresCheque";
                cmd.Parameters.Add(new SqlParameter("@TextoProyectosProveedores", TextoProyectos));
                SqlDataAdapter adapter;
                adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                cmd.Parameters.Clear();
            }
            return (dt);
        }
        public string LimpiarModificarChequePagoProveedores(int CodCheque, int CodUsuario)
        {
            string mensaje;
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_upd_LimpiarPagoProyectoProveedoresPorCheque";
                cmd.Parameters.Add(new SqlParameter("@CodCheque", CodCheque));
                cmd.Parameters.Add(new SqlParameter("@CodUsuarioModificar", CodUsuario));
                SqlParameter output = new SqlParameter("@Mensaje", SqlDbType.VarChar, 500);
                output.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(output);

                con.Open();
                cmd.ExecuteNonQuery();
                mensaje = output.Value.ToString();
            }
            catch (SqlException ex)
            {
                mensaje = ex.Message.ToString();
            }
            finally
            {
                con.Close();
                cmd.Parameters.Clear();
            }
            return mensaje;
        }
        public DataTable ObtenerDatosChequePersonaProveedor(int CodCheque)
        {
            DataTable dt = new DataTable();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_sel_ObtenerDatosChequeProveedor";
                cmd.Parameters.Add(new SqlParameter("@CodCheque", CodCheque));
                SqlDataAdapter adapter;
                adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                cmd.Parameters.Clear();
            }
            return (dt);
        }
        public DataTable CargarEstadoCheque()
        {
            DataTable dt = new DataTable();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_sel_CargarEstadoCheque";
                SqlDataAdapter adapter;
                adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                cmd.Parameters.Clear();
            }
            return (dt);
        }
    }
}
