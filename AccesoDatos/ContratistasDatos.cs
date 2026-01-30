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
    public class ContratistasDatos
    {
        Conexion conexion = new Conexion();
        SqlConnection con;
        SqlCommand cmd = new SqlCommand();
        public ContratistasDatos()
        {
            con = new SqlConnection(conexion.GetConexion());
        }
        public DataTable BuscarContratistas(string TextoBuscar)
        {
            DataTable dt = new DataTable();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_sel_BuscarContratistas";
                cmd.Parameters.Add(new SqlParameter("@TextoBuscar", TextoBuscar));
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
        public DataTable BuscarContratistasContratos(string TextoBuscar)
        {
            DataTable dt = new DataTable();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_sel_BuscarContratosContratistas";
                cmd.Parameters.Add(new SqlParameter("@TextoBuscar", TextoBuscar));
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
        public String RegistrarContratista(ContratistasEntidad contratistasEntidad)
        {
            string mensaje;
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_ins_RegistrarContratista";

                cmd.Parameters.Add(new SqlParameter("@DescripcionContratista", contratistasEntidad.DescripcionContratista));
                cmd.Parameters.Add(new SqlParameter("@Ruc", contratistasEntidad.Ruc));
                cmd.Parameters.Add(new SqlParameter("@FechaInicioVigencia", contratistasEntidad.FechaInicioVigencia));
                cmd.Parameters.Add(new SqlParameter("@FechaFinVigencia", contratistasEntidad.FechaFinVigencia));
                cmd.Parameters.Add(new SqlParameter("@CodUsuarioRegistro", contratistasEntidad.CodUsuarioRegistro));
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
        public String ModificarContratista(ContratistasEntidad contratistasEntidad)
        {
            string mensaje;
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_upd_ActualizarContratista";

                cmd.Parameters.Add(new SqlParameter("@DescripcionContratista", contratistasEntidad.DescripcionContratista));
                cmd.Parameters.Add(new SqlParameter("@Ruc", contratistasEntidad.Ruc));
                cmd.Parameters.Add(new SqlParameter("@FechaInicioVigencia", contratistasEntidad.FechaInicioVigencia));
                cmd.Parameters.Add(new SqlParameter("@FechaFinVigencia", contratistasEntidad.FechaFinVigencia));
                cmd.Parameters.Add(new SqlParameter("@CodUsuarioModificar", contratistasEntidad.CodUsuarioModifica));
                cmd.Parameters.Add(new SqlParameter("@CodContratista", contratistasEntidad.CodContratista));
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
        public string EliminarContratista(int CodContratista, int CodUsuario)
        {
            string mensaje;
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_upd_EliminarContratista";
                cmd.Parameters.Add(new SqlParameter("@CodContratista", CodContratista));
                cmd.Parameters.Add(new SqlParameter("@CodUsuarioModifica", CodUsuario));
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
        public DataTable CargarDatosContratista(int CodContratista)
        {
            DataTable dt = new DataTable();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_sel_ObtenerDatosContratista";
                cmd.Parameters.Add(new SqlParameter("@CodContratista", CodContratista));
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
        public DataTable CargarContratistas()
        {
            DataTable dt = new DataTable();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_sel_CargarContratistas";
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
        public DataTable CargarModosPago()
        {
            DataTable dt = new DataTable();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_sel_CargarModoPagoContratistas";
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
        public String RegistrarContratoContratista(ContratistasEntidad contratistasEntidad)
        {
            string mensaje;
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_ins_RegistrarContratistaContrato";

                cmd.Parameters.Add(new SqlParameter("@CodContratista", contratistasEntidad.CodContratista));
                cmd.Parameters.Add(new SqlParameter("@CodProyectoPlanilla", contratistasEntidad.CodProyectoPlanilla));
                cmd.Parameters.Add(new SqlParameter("@DescripcionLaborContrato", contratistasEntidad.DescripcionLaborContrato));
                cmd.Parameters.Add(new SqlParameter("@CodModoPago", contratistasEntidad.CodModoPago));
                cmd.Parameters.Add(new SqlParameter("@MontoTotal", contratistasEntidad.MontoTotal));
                cmd.Parameters.Add(new SqlParameter("@FechaInicioContrato", contratistasEntidad.FechaInicioContrato));
                cmd.Parameters.Add(new SqlParameter("@CodUsuarioRegistro", contratistasEntidad.CodUsuarioRegistro));
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
        public String ModificarContratoContratista(ContratistasEntidad contratistasEntidad)
        {
            string mensaje;
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_upd_ActualizarontratistaContrato";

                cmd.Parameters.Add(new SqlParameter("@CodContratista", contratistasEntidad.CodContratista));
                cmd.Parameters.Add(new SqlParameter("@CodProyectoPlanilla", contratistasEntidad.CodProyectoPlanilla));
                cmd.Parameters.Add(new SqlParameter("@DescripcionLaborContrato", contratistasEntidad.DescripcionLaborContrato));
                cmd.Parameters.Add(new SqlParameter("@CodModoPago", contratistasEntidad.CodModoPago));
                cmd.Parameters.Add(new SqlParameter("@MontoTotal", contratistasEntidad.MontoTotal));
                cmd.Parameters.Add(new SqlParameter("@FechaInicioContrato", contratistasEntidad.FechaInicioContrato));
                cmd.Parameters.Add(new SqlParameter("@CodUsuarioModificar", contratistasEntidad.CodUsuarioModifica));
                cmd.Parameters.Add(new SqlParameter("@CodContrato", contratistasEntidad.CodContrato));
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
        public DataTable CargarDatosContrato(int CodContrato)
        {
            DataTable dt = new DataTable();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_sel_ObtenerDatosContrato";
                cmd.Parameters.Add(new SqlParameter("@CodContrato", CodContrato));
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
        public string EliminarContrato(int CodContrato, int CodUsuario)
        {
            string mensaje;
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_upd_EliminarContrato";
                cmd.Parameters.Add(new SqlParameter("@CodContrato", CodContrato));
                cmd.Parameters.Add(new SqlParameter("@CodUsuarioModifica", CodUsuario));
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
        public DataTable ObtenerContratosContratista(int CodContratisa, int codEstadoPago)
        {
            DataTable dt = new DataTable();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_sel_ObtenerContratosContratista";
                cmd.Parameters.Add(new SqlParameter("@CodContratista", CodContratisa));
                cmd.Parameters.Add(new SqlParameter("@CodEstadoPago", codEstadoPago));
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
        public DataTable CargarMediosPagoContratista()
        {
            DataTable dt = new DataTable();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_sel_CargarMediosPagoContratista";
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
        public DataTable ObtenerDatosContratoPago(int CodContrato)
        {
            DataTable dt = new DataTable();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_sel_CargarDatosContratoPago";
                cmd.Parameters.Add(new SqlParameter("@CodContrato", CodContrato));
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
        public String RegistrarPagoContratistas(ContratistasEntidad contratistasEntidad)
        {
            string mensaje;
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_ins_RegistrarPagoContratoContratista";

                cmd.Parameters.Add(new SqlParameter("@CodContrato", contratistasEntidad.CodContrato));
                cmd.Parameters.Add(new SqlParameter("@MontoPagar", contratistasEntidad.MontoPagar));
                cmd.Parameters.Add(new SqlParameter("@CodMedioPago", contratistasEntidad.CodMedioPago));
                cmd.Parameters.Add(new SqlParameter("@NumMedioPago", contratistasEntidad.NumMedioPago));
                cmd.Parameters.Add(new SqlParameter("@FechaPago", contratistasEntidad.FechaPago));
                cmd.Parameters.Add(new SqlParameter("@CodTipoReciboPago", contratistasEntidad.CodTipoReciboPago));
                cmd.Parameters.Add(new SqlParameter("@NumReciboPago", contratistasEntidad.NumReciboPago));
                cmd.Parameters.Add(new SqlParameter("@CodUsuarioRegistro", contratistasEntidad.CodUsuarioRegistro));
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
        public String ModificatPagoContratistas(ContratistasEntidad contratistasEntidad)
        {
            string mensaje;
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_upd_ActualizarPagoContratoContratista";

                cmd.Parameters.Add(new SqlParameter("@MontoPagar", contratistasEntidad.MontoPagar));
                cmd.Parameters.Add(new SqlParameter("@CodMedioPago", contratistasEntidad.CodMedioPago));
                cmd.Parameters.Add(new SqlParameter("@NumMedioPago", contratistasEntidad.NumMedioPago));
                cmd.Parameters.Add(new SqlParameter("@FechaPago", contratistasEntidad.FechaPago));
                cmd.Parameters.Add(new SqlParameter("@CodTipoReciboPago", contratistasEntidad.CodTipoReciboPago));
                cmd.Parameters.Add(new SqlParameter("@NumReciboPago", contratistasEntidad.NumReciboPago));
                cmd.Parameters.Add(new SqlParameter("@CodPagoContratista", contratistasEntidad.CodPagoContratista));
                cmd.Parameters.Add(new SqlParameter("@CodUsuarioModifica", contratistasEntidad.CodUsuarioModifica));
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
        public String EliminarPagoContratistas(ContratistasEntidad contratistasEntidad)
        {
            string mensaje;
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_upd_EliminarPagoContratista";                
                cmd.Parameters.Add(new SqlParameter("@CodPagoContratista", contratistasEntidad.CodPagoContratista));
                cmd.Parameters.Add(new SqlParameter("@CodUsuarioModifica", contratistasEntidad.CodUsuarioModifica));
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
        public DataTable CargarTipoRecibosPago()
        {
            DataTable dt = new DataTable();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_sel_CargarRecibosPago";
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
        public DataTable ObtenerPagosRealizados(int CodContrato)
        {
            DataTable dt = new DataTable();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_sel_ObtenerPagosRealizadosContratista";
                cmd.Parameters.Add(new SqlParameter("@CodContrato", CodContrato));
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
        public DataTable ObtenerDatosPagoContratista(int CodPagoContratista)
        {
            DataTable dt = new DataTable();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_sel_ObtenerDatosPagoContratista";
                cmd.Parameters.Add(new SqlParameter("@CodPagoContratista", CodPagoContratista));
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
        public DataTable CargarContratosContratista()
        {
            DataTable dt = new DataTable();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_sel_CargarContratosContratista";
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
        public DataTable CargarContratosPagarContratista(int CodContratista)
        {
            DataTable dt = new DataTable();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "ObtenerContratosPagarContratista";
                cmd.Parameters.Add(new SqlParameter("@CodContratista", CodContratista));
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
        public String RegistrarPagoContratistasPorCheque(ContratistasEntidad contratistasEntidad)
        {
            string mensaje;
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_gen_GenerarPagoContratoContratistaPorCheque";
                cmd.Parameters.Add(new SqlParameter("@CodCheque", contratistasEntidad.CodCheque));
                cmd.Parameters.Add(new SqlParameter("@CodContrato", contratistasEntidad.CodContrato));
                cmd.Parameters.Add(new SqlParameter("@MontoPagar", contratistasEntidad.MontoPagar));                
                cmd.Parameters.Add(new SqlParameter("@NumMedioPago", contratistasEntidad.NumMedioPago));
                cmd.Parameters.Add(new SqlParameter("@FechaPago", contratistasEntidad.FechaPago));
                cmd.Parameters.Add(new SqlParameter("@CodTipoReciboPago", contratistasEntidad.CodTipoReciboPago));
                cmd.Parameters.Add(new SqlParameter("@NumReciboPago", contratistasEntidad.NumReciboPago));
                cmd.Parameters.Add(new SqlParameter("@CodUsuarioRegistro", contratistasEntidad.CodUsuarioRegistro));
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
    }
}
