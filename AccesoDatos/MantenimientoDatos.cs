using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos
{
    public class MantenimientoDatos
    {
        Conexion conexion = new Conexion();
        SqlConnection con;
        SqlCommand cmd = new SqlCommand();
        public MantenimientoDatos()
        {
            con = new SqlConnection(conexion.GetConexion());
        }
        public DataTable ObtenerParametrosPlanilla()
        {
            DataTable dt = new DataTable();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_sel_CargarParametrosMantenimientoPerfilPlanilla";
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
        public DataTable ObtenerParametrosPerfilPlanilla(int CodPerfilPlanilla)
        {
            DataTable dt = new DataTable();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_gen_ObtenerParametrosPerfilPlanilla";
                cmd.Parameters.Add(new SqlParameter("@CodPerfilPlanilla", CodPerfilPlanilla));
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
        public string RegistrarPerfilPlanilla(string NombrePerfilPlanilla, double Jornal, int CodUsuarioRegistro)
        {
            string mensaje;
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_ins_GuardarPerfilPlanilla";

                cmd.Parameters.Add(new SqlParameter("@NombrePerfil", NombrePerfilPlanilla));
                cmd.Parameters.Add(new SqlParameter("@Jornal", Jornal));
                cmd.Parameters.Add(new SqlParameter("@CodUsuarioRegistro", CodUsuarioRegistro));

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
        public string ActualizarPerfilPlanilla(string NombrePerfilPlanilla, double Jornal, int CodUsuarioModifica, int CodPerfilPlanilla)
        {
            string mensaje;
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_upd_ActualizarPerfilPlanilla";

                cmd.Parameters.Add(new SqlParameter("@NombrePerfil", NombrePerfilPlanilla));
                cmd.Parameters.Add(new SqlParameter("@Jornal", Jornal));
                cmd.Parameters.Add(new SqlParameter("@CodUsuarioModifica", CodUsuarioModifica));
                cmd.Parameters.Add(new SqlParameter("@CodPerfilPlanilla", CodPerfilPlanilla));
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
        public string RegistrarParametroPerfilPlanilla(int CodPerdilPlanilla, int CodParametro, string CampoParametro)
        {
            string mensaje;
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_ins_GuardarParametroPerfilPlanilla";

                cmd.Parameters.Add(new SqlParameter("@CodPerfil", CodPerdilPlanilla));
                cmd.Parameters.Add(new SqlParameter("@CodParametro", CodParametro));
                cmd.Parameters.Add(new SqlParameter("@CampoParametro", CampoParametro));

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
        public string ActualizarParametroPerfilPlanilla(int CodPerdilPlanilla, int CodParametro, string CampoParametro)
        {
            string mensaje;
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_upd_ActualizarParametroPerfilPlanilla";

                cmd.Parameters.Add(new SqlParameter("@CodPerfil", CodPerdilPlanilla));
                cmd.Parameters.Add(new SqlParameter("@CodParametro", CodParametro));
                cmd.Parameters.Add(new SqlParameter("@CampoParametro", CampoParametro));

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
        public DataTable BuscarPerfilPlanilla(string TextoBuscar)
        {
            DataTable dt = new DataTable();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_sel_BuscarPerfilPlanilla";
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
        public DataTable ObtenerLaborTrabajo()
        {
            DataTable dt = new DataTable();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_sel_ObtenerLaborTrabajo";
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
        public DataTable BuscarLaborTrabajo(string TextoBuscar)
        {
            DataTable dt = new DataTable();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_sel_BuscarLaborTrabajo";
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
        public string RegistrarLaborTrabajador(string NombreLaborTrabajador)
        {
            string mensaje;
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_ins_RegistrarLaborTrabajador";

                cmd.Parameters.Add(new SqlParameter("@DescripcionLabor", NombreLaborTrabajador));

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
        public string ModificarLaborTrabajador(string NombreLaborTrabajador, int CodLaborTrabajador)
        {
            string mensaje;
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_upd_ActualizarLaborTrabajador";

                cmd.Parameters.Add(new SqlParameter("@CodLaboTrabajador", CodLaborTrabajador));
                cmd.Parameters.Add(new SqlParameter("@DescripcionLabor", NombreLaborTrabajador));
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
        public DataTable ObtenerDatosLaborTrabajo(int CodLaborTrabajador)
        {
            DataTable dt = new DataTable();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_sel_ObtenerDatosLaborTrabajo";
                cmd.Parameters.Add(new SqlParameter("@CodLaborTrabajo", CodLaborTrabajador));
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
        public DataTable ObtenerServicios()
        {
            DataTable dt = new DataTable();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_sel_ObtenerServicios";
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
        public string RegistrarServicios(string NombreServicio, string UnidadMedida, double CostoUnitario)
        {
            string mensaje;
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_ins_RegistrarServicio ";

                cmd.Parameters.Add(new SqlParameter("@DescripcionServicio", NombreServicio));
                cmd.Parameters.Add(new SqlParameter("@UnidadMedida", UnidadMedida));
                cmd.Parameters.Add(new SqlParameter("@PrecioUnitario", CostoUnitario));

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
        public string ActualizarServicio(string NombreServicio, string UnidadMedida, double CostoUnitario, int CodServicio)
        {
            string mensaje;
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_upd_ActualizarServicio";

                cmd.Parameters.Add(new SqlParameter("@CodServicio", CodServicio));
                cmd.Parameters.Add(new SqlParameter("@DescripcionServicio", NombreServicio));
                cmd.Parameters.Add(new SqlParameter("@UnidadMedida", UnidadMedida));
                cmd.Parameters.Add(new SqlParameter("@PrecioUnitario", CostoUnitario));
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
        public DataTable ObtenerDatosServicio(int CodServicio)
        {
            DataTable dt = new DataTable();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_sel_ObtenerDatosServicio";
                cmd.Parameters.Add(new SqlParameter("@CodServicio", CodServicio));
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
        public string EliminarPerfilPlanilla(int CodPerfilPlanilla, int CodUsuarioModifica)
        {
            string mensaje;
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_upd_EliminarPerfilPlanilla";          
                cmd.Parameters.Add(new SqlParameter("@CodPerfilPlanilla", CodPerfilPlanilla));
                cmd.Parameters.Add(new SqlParameter("@CodUsuarioModifica", CodUsuarioModifica));
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
        public DataTable ObtenerTipoAportaciones()
        {
            DataTable dt = new DataTable();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_sel_CargarTipoAportaciones";
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
        public DataTable ObtenerDatosAportacion(int CodTipoAportacion)
        {
            DataTable dt = new DataTable();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_gen_ObtenerDatosAportacion";
                cmd.Parameters.Add(new SqlParameter("@CodAportacion", CodTipoAportacion));
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
        public string ModificarAportacion(double AporteObligatorio, double ComisionFlujo, double PrimaSeguro, double AporteComplementario, double ComisionMixta, int CodAportacion, int CodUsuarioModificacion)
        {
            string mensaje;
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_upd_ActualizarAportacion";

                cmd.Parameters.Add(new SqlParameter("@CodAportacion", CodAportacion));
                cmd.Parameters.Add(new SqlParameter("@ParametroAporteObligatorio", AporteObligatorio));
                cmd.Parameters.Add(new SqlParameter("@ParametroComisionFlujo", ComisionFlujo));
                cmd.Parameters.Add(new SqlParameter("@ParametroPrimaSeguro", PrimaSeguro));
                cmd.Parameters.Add(new SqlParameter("@ParametroAporteComplementario", AporteComplementario));
                cmd.Parameters.Add(new SqlParameter("@ParametroComisionMixta", ComisionMixta));
                cmd.Parameters.Add(new SqlParameter("@CodUsuarioModifica", CodUsuarioModificacion));

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
