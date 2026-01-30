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
    public class TrabajadorDatos
    {
        Conexion conexion = new Conexion();
        SqlConnection con;
        SqlCommand cmd = new SqlCommand();
        public TrabajadorDatos()
        {
            con = new SqlConnection(conexion.GetConexion());
        }
        public DataTable ObtenerPerfilPlanilla()
        {
            DataTable dt = new DataTable();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_sel_CargarPerfilesPlanilla";
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
        public DataTable ObtenerListaTrabajadores()
        {
            DataTable dt = new DataTable();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_gen_ObtenerListaTrabajadores";
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
        public DataTable ObtenerProyectoPlanillaTrabajadores(int CodProyectoPlanilla)
        {
            DataTable dt = new DataTable();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_gen_ObtenerTrabajadoresProyectoPlanilla";
                cmd.Parameters.Add(new SqlParameter("@CodProyectoPlanilla", CodProyectoPlanilla));
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
        public DataTable ObtenerProyectoPlanillaTrabajadoresEventuales(int CodProyectoPlanilla)
        {
            DataTable dt = new DataTable();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_gen_ObtenerTrabajadoresEventualesProyectoPlanilla";
                cmd.Parameters.Add(new SqlParameter("@CodProyectoPlanilla", CodProyectoPlanilla));
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
        public DataTable ObtenerListaTrabajadoresEventuales()
        {
            DataTable dt = new DataTable();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_gen_ObtenerListaTrabajadoresEventuales";
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
        public DataTable ObtenerListaTrabajadoresEmpleados()
        {
            DataTable dt = new DataTable();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_sel_ObtenerListadoEmpleados";
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
        public DataTable ObtenerListaTrabajadoresReportes()
        {
            DataTable dt = new DataTable();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_sel_CargarTrabajadoresReportes";
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
        public DataTable ObtenerListaTrabajadoresReporteIndividual(int CodTipoPlanilla)
        {
            DataTable dt = new DataTable();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_sel_CargarTrabajadoresReporteIndividual";
                cmd.Parameters.Add(new SqlParameter("@CodTipoPlanilla", CodTipoPlanilla));
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
        public String RegistrarTrabajador(TrabajadorEntidad trabajadorEntidad)
        {
            string mensaje;
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_ins_GuardarTrabajador";

                cmd.Parameters.Add(new SqlParameter("@CodTipoDocumentoIdentidad", trabajadorEntidad.CodTipoDocumentoIdentidad));
                cmd.Parameters.Add(new SqlParameter("@DocumentoIdentidad", trabajadorEntidad.NumeroDocumentoIdentidad));
                cmd.Parameters.Add(new SqlParameter("@Nombres", trabajadorEntidad.Nombres));
                cmd.Parameters.Add(new SqlParameter("@ApellidoPaterno", trabajadorEntidad.ApellidoPaterno));
                cmd.Parameters.Add(new SqlParameter("@ApellidoMaterno", trabajadorEntidad.ApellidoMaterno));
                cmd.Parameters.Add(new SqlParameter("@FechaNacimiento", trabajadorEntidad.FechaNacimiento));
                cmd.Parameters.Add(new SqlParameter("@Sexo", trabajadorEntidad.Sexo));
                cmd.Parameters.Add(new SqlParameter("@CodEstadoCivil", trabajadorEntidad.CodEstadoCivil));
                cmd.Parameters.Add(new SqlParameter("@FechaIngreso", trabajadorEntidad.FechaIngreso));
                cmd.Parameters.Add(new SqlParameter("@FechaCese", trabajadorEntidad.FechaCese));
                cmd.Parameters.Add(new SqlParameter("@HaberMensual", trabajadorEntidad.HaberMensual));
                cmd.Parameters.Add(new SqlParameter("@CodTipoAportacion", trabajadorEntidad.CodTipoAportacion));
                cmd.Parameters.Add(new SqlParameter("@NroCuspp", trabajadorEntidad.NumeroCuspp));
                cmd.Parameters.Add(new SqlParameter("@CodTipoTrabajo", trabajadorEntidad.CodTipoTrabajo));
                cmd.Parameters.Add(new SqlParameter("@CodCargo", trabajadorEntidad.CodCargo));
                cmd.Parameters.Add(new SqlParameter("@CodBanco", trabajadorEntidad.CodBanco));
                cmd.Parameters.Add(new SqlParameter("@NumeroCuentaBanco", trabajadorEntidad.NumeroCuentaBanco));
                cmd.Parameters.Add(new SqlParameter("@NumeroCuentaCTS", trabajadorEntidad.NumeroCuentaCTS));
                cmd.Parameters.Add(new SqlParameter("@CodTipoPlanilla", trabajadorEntidad.CodTipoPlanilla));
                cmd.Parameters.Add(new SqlParameter("@CodPerfilPlanilla", trabajadorEntidad.CodPerfilPlanilla));
                cmd.Parameters.Add(new SqlParameter("@CodUsuarioRegistro", trabajadorEntidad.CodUsuarioRegistro));
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
        public String ActualizarTrabajador(TrabajadorEntidad trabajadorEntidad)
        {
            string mensaje;
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_upd_ActualizarTrabajador";

                cmd.Parameters.Add(new SqlParameter("@CodTipoDocumentoIdentidad", trabajadorEntidad.CodTipoDocumentoIdentidad));
                cmd.Parameters.Add(new SqlParameter("@DocumentoIdentidad", trabajadorEntidad.NumeroDocumentoIdentidad));
                cmd.Parameters.Add(new SqlParameter("@Nombres", trabajadorEntidad.Nombres));
                cmd.Parameters.Add(new SqlParameter("@ApellidoPaterno", trabajadorEntidad.ApellidoPaterno));
                cmd.Parameters.Add(new SqlParameter("@ApellidoMaterno", trabajadorEntidad.ApellidoMaterno));
                cmd.Parameters.Add(new SqlParameter("@FechaNacimiento", trabajadorEntidad.FechaNacimiento));
                cmd.Parameters.Add(new SqlParameter("@Sexo", trabajadorEntidad.Sexo));
                cmd.Parameters.Add(new SqlParameter("@CodEstadoCivil", trabajadorEntidad.CodEstadoCivil));
                cmd.Parameters.Add(new SqlParameter("@FechaIngreso", trabajadorEntidad.FechaIngreso));
                cmd.Parameters.Add(new SqlParameter("@FechaCese", trabajadorEntidad.FechaCese));
                cmd.Parameters.Add(new SqlParameter("@HaberMensual", trabajadorEntidad.HaberMensual));
                cmd.Parameters.Add(new SqlParameter("@CodTipoAportacion", trabajadorEntidad.CodTipoAportacion));
                cmd.Parameters.Add(new SqlParameter("@NroCuspp", trabajadorEntidad.NumeroCuspp));
                cmd.Parameters.Add(new SqlParameter("@CodTipoTrabajo", trabajadorEntidad.CodTipoTrabajo));
                cmd.Parameters.Add(new SqlParameter("@CodCargo", trabajadorEntidad.CodCargo));
                cmd.Parameters.Add(new SqlParameter("@CodBanco", trabajadorEntidad.CodBanco));
                cmd.Parameters.Add(new SqlParameter("@NumeroCuentaBanco", trabajadorEntidad.NumeroCuentaBanco));
                cmd.Parameters.Add(new SqlParameter("@NumeroCuentaCTS", trabajadorEntidad.NumeroCuentaCTS));
                cmd.Parameters.Add(new SqlParameter("@CodTipoPlanilla", trabajadorEntidad.CodTipoPlanilla));
                cmd.Parameters.Add(new SqlParameter("@CodPerfilPlanilla", trabajadorEntidad.CodPerfilPlanilla));
                cmd.Parameters.Add(new SqlParameter("@CodUsuarioModifica", trabajadorEntidad.CodUsuarioModifica));
                cmd.Parameters.Add(new SqlParameter("@CodTrabajador", trabajadorEntidad.CodTrabajador));
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
        public String EliminarTrabajador(TrabajadorEntidad trabajadorEntidad)
        {
            string mensaje;
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_upd_EliminarTrabajadorPlanilla";

                cmd.Parameters.Add(new SqlParameter("@CodTrabajador", trabajadorEntidad.CodTrabajador));
                cmd.Parameters.Add(new SqlParameter("@CodUsuarioModifica", trabajadorEntidad.CodUsuarioModifica));
                
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
        public String RegistrarTrabajadorEventuales(TrabajadorEntidad trabajadorEntidad)
        {
            string mensaje;
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_ins_GuardarTrabajadorEventuales";

                cmd.Parameters.Add(new SqlParameter("@CodTipoDocumentoIdentidad", trabajadorEntidad.CodTipoDocumentoIdentidad));
                cmd.Parameters.Add(new SqlParameter("@DocumentoIdentidad", trabajadorEntidad.NumeroDocumentoIdentidad));
                cmd.Parameters.Add(new SqlParameter("@Nombres", trabajadorEntidad.Nombres));
                cmd.Parameters.Add(new SqlParameter("@ApellidoPaterno", trabajadorEntidad.ApellidoPaterno));
                cmd.Parameters.Add(new SqlParameter("@ApellidoMaterno", trabajadorEntidad.ApellidoMaterno));              
                cmd.Parameters.Add(new SqlParameter("@Sexo", trabajadorEntidad.Sexo));              
                cmd.Parameters.Add(new SqlParameter("@CodTipoTrabajo", trabajadorEntidad.CodTipoTrabajo));
                cmd.Parameters.Add(new SqlParameter("@CodPerfilPlanilla", trabajadorEntidad.CodPerfilPlanilla));
                cmd.Parameters.Add(new SqlParameter("@CodUsuarioRegistro", trabajadorEntidad.CodUsuarioRegistro));
                cmd.Parameters.Add(new SqlParameter("@CodTipoPlanilla", trabajadorEntidad.CodTipoPlanilla));
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
        public String ActualizarTrabajadorEventuales(TrabajadorEntidad trabajadorEntidad)
        {
            string mensaje;
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_ins_ActualizarTrabajadorEventuales";

                cmd.Parameters.Add(new SqlParameter("@CodTipoDocumentoIdentidad", trabajadorEntidad.CodTipoDocumentoIdentidad));
                cmd.Parameters.Add(new SqlParameter("@DocumentoIdentidad", trabajadorEntidad.NumeroDocumentoIdentidad));
                cmd.Parameters.Add(new SqlParameter("@Nombres", trabajadorEntidad.Nombres));
                cmd.Parameters.Add(new SqlParameter("@ApellidoPaterno", trabajadorEntidad.ApellidoPaterno));
                cmd.Parameters.Add(new SqlParameter("@ApellidoMaterno", trabajadorEntidad.ApellidoMaterno));
                cmd.Parameters.Add(new SqlParameter("@Sexo", trabajadorEntidad.Sexo));
                cmd.Parameters.Add(new SqlParameter("@CodTipoTrabajo", trabajadorEntidad.CodTipoTrabajo));
                cmd.Parameters.Add(new SqlParameter("@CodPerfilPlanilla", trabajadorEntidad.CodPerfilPlanilla));
                cmd.Parameters.Add(new SqlParameter("@CodUsuarioModifica", trabajadorEntidad.CodUsuarioModifica));
                cmd.Parameters.Add(new SqlParameter("@CodTrabajadorEventual", trabajadorEntidad.CodTrabajador));
                cmd.Parameters.Add(new SqlParameter("@CodTipoPlanilla", trabajadorEntidad.CodTipoPlanilla));
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
        public String EliminarTrabajadorEventuales(TrabajadorEntidad trabajadorEntidad)
        {
            string mensaje;
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_upd_EliminarTrabajadorPlanillaEventuales";

                cmd.Parameters.Add(new SqlParameter("@CodTrabajador", trabajadorEntidad.CodTrabajador));
                cmd.Parameters.Add(new SqlParameter("@CodUsuarioModifica", trabajadorEntidad.CodUsuarioModifica));

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
        public DataTable ObtenerDatosTrabajadorBoletaPlanillaPrevio(int CodTrabajador)
        {
            DataTable dt = new DataTable();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_gen_ObtenerDatosTrabajadorBoletaPlanillaPrevio";
                cmd.Parameters.Add(new SqlParameter("@CodTrabajador", CodTrabajador));
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
        public DataTable ObtenerDatosTrabajadorEventualPlanillaPrevio(int CodTrabajador)
        {
            DataTable dt = new DataTable();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_gen_ObtenerDatosTrabajadorEventualPlanillaPrevio";
                cmd.Parameters.Add(new SqlParameter("@CodTrabajador", CodTrabajador));
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
        public DataTable BuscarTrabajador(string TextoBuscar)
        {
            DataTable dt = new DataTable();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_sel_BuscarTrabajador";
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
        public DataTable BuscarTrabajadoresEventuales(string TextoBuscar)
        {
            DataTable dt = new DataTable();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_sel_BuscarEventuales";
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
        public DataTable ObtenerDatosTrabajador(int CodTrabajador, int CodTipoPlanilla)
        {
            DataTable dt = new DataTable();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_sel_CargarDatosTrabajador";
                cmd.Parameters.Add(new SqlParameter("@CodTrabajador", CodTrabajador));
                cmd.Parameters.Add(new SqlParameter("@CodTipoPlanilla", CodTipoPlanilla));
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
        public DataTable ObtenerDatosTrabajadorDocumentoIdentidad(string DocumentoIdentidad, int CodTipoDocumentoIdentidad)
        {
            DataTable dt = new DataTable();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_sel_CargarDatosTrabajadorPorDocumentoIdentidad";
                cmd.Parameters.Add(new SqlParameter("@DocumentoIdentidad", DocumentoIdentidad));
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
        public DataTable ObtenerListadoTrabajadores(int CodProyecto)
        {
            DataTable dt = new DataTable();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_sel_ObtenerListadoTrabajadores";
                cmd.Parameters.Add(new SqlParameter("@CodProyectoPlanilla", CodProyecto));
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
        public DataTable ObtenerListaLaborTrabajador()
        {
            DataTable dt = new DataTable();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_sel_CargarLaborTrabajador";
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
                cmd.CommandText = "PA_sel_CargarLaborTrabajo";
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
                cmd.CommandText = "PA_sel_CargarServicios";
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
        public DataTable CargarTipoDocumentoIdentidad()
        {
            DataTable dt = new DataTable();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_sel_CargarTipoDocumentoIdentidad";
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
