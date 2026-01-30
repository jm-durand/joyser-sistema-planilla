using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos
{
    public class CentroCostosDatos
    {
        Conexion conexion = new Conexion();
        SqlConnection con;
        SqlCommand cmd = new SqlCommand();
        public CentroCostosDatos()
        {
            con = new SqlConnection(conexion.GetConexion());
        }
        public string RegistrarProyectoPlanilla(string NombreProyecto, double Presupuesto, string FechaInicio, string FechaFin, int CodUsuarioRegistro)
        {
            string mensaje;
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_ins_RegistrarProyectoPlanilla";

                cmd.Parameters.Add(new SqlParameter("@NombreProyecto", NombreProyecto));
                cmd.Parameters.Add(new SqlParameter("@Presupuesto", Presupuesto));
                cmd.Parameters.Add(new SqlParameter("@FechaInicio", FechaInicio));
                cmd.Parameters.Add(new SqlParameter("@FechaFin", FechaFin));
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
        public string ActualizarProyectoPlanilla(string NombreProyecto, double Presupuesto, string FechaFin, int CodUsuarioModifica, int CodProyectoPlanilla)
        {
            string mensaje;
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_upd_ActualizarProyectoPlanilla";

                cmd.Parameters.Add(new SqlParameter("@NombreProyecto", NombreProyecto));
                cmd.Parameters.Add(new SqlParameter("@Presupuesto", Presupuesto));             
                cmd.Parameters.Add(new SqlParameter("@FechaFinModificar", FechaFin));
                cmd.Parameters.Add(new SqlParameter("@CodUsuarioModificar", CodUsuarioModifica));
                cmd.Parameters.Add(new SqlParameter("CodProyectoPlanilla", CodProyectoPlanilla));
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
        public DataTable BuscarProyectoPlanilla(string TextoBuscar)
        {
            DataTable dt = new DataTable();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_sel_BuscarProyectoPlanilla";
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
        public DataTable BuscarProyectoPlanillaTrabajadores(string TextoBuscar)
        {
            DataTable dt = new DataTable();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_sel_BuscarProyectoPlanillaTrabajadores";
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
        public DataTable CargarDatosProyectoPlanilla(int CodProyectoPlanilla)
        {
            DataTable dt = new DataTable();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_sel_ObtenerDatosProyectoPlanilla";
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
        public DataTable CargarProyectoPlanilla()
        {
            DataTable dt = new DataTable();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_sel_CargarProyectoPlanilla";
             
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
        public string RegistrarProyectoPlanillaTrabajador(int CodProyectoPlanilla, int CodTrabajador, int CodUsuarioRegistro)
        {
            string mensaje;
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_ins_GuardarTrabajadorProyectoPlanilla";

                cmd.Parameters.Add(new SqlParameter("@CodProyectoPlanilla", CodProyectoPlanilla));
                cmd.Parameters.Add(new SqlParameter("@CodTrabajador", CodTrabajador));
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
        public DataTable CargarTrabajadoresProyectoPlanilla(int CodProyectoPlanilla)
        {
            DataTable dt = new DataTable();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_sel_ObtenerTrabajadoresProyectoPlanilla";
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
        public string DesactivarTrabajadorProyectoPlanilla(int CodProyectoPlanilla, int CodTrabajador, int CodUsuarioModifica)
        {
            string mensaje;
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_upd_RetirarTrabajadorProyectoPlanilla";

                cmd.Parameters.Add(new SqlParameter("@CodTrabajador", CodTrabajador));
                cmd.Parameters.Add(new SqlParameter("@CodProyectoPlanilla", CodProyectoPlanilla));  
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
        public DataTable BuscarProyectoPlanillaProveedores(string TextoBuscar)
        {
            DataTable dt = new DataTable();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_sel_BuscarProyectoPlanillaProveedores";
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
        public string RegistrarProyectoPlanillaProveedor(int CodProyectoPlanilla, int CodProveedor, int CodUsuarioRegistro)
        {
            string mensaje;
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_ins_GuardarProveedorProyectoPlanilla";

                cmd.Parameters.Add(new SqlParameter("@CodProyectoPlanilla", CodProyectoPlanilla));
                cmd.Parameters.Add(new SqlParameter("@CodProveedor", CodProveedor));
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
        public DataTable CargarProveedoresProyectoPlanilla(int CodProyectoPlanilla)
        {
            DataTable dt = new DataTable();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_sel_ObtenerProveedoresProyectoPlanilla";
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
        public string DesactivarProveedorProyectoPlanilla(int CodProyectoPlanilla, int CodProveedor, int CodUsuarioModifica)
        {
            string mensaje;
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_upd_RetirarProveedorProyectoPlanilla";

                cmd.Parameters.Add(new SqlParameter("@CodProveedor", CodProveedor));
                cmd.Parameters.Add(new SqlParameter("@CodProyectoPlanilla", CodProyectoPlanilla));
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
    }
}
