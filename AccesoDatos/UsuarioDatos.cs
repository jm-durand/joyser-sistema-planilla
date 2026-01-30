using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccesoDatos;
using Entidad;

namespace AccesoDatos
{
    public class UsuarioDatos
    {
        Conexion conexion = new Conexion();
        SqlConnection con;
        SqlCommand cmd = new SqlCommand();
        public UsuarioDatos()
        {
            con = new SqlConnection(conexion.GetConexion());
        }

        public DataTable Login(UsuarioEntidad usuarioEntidad)
        {
            DataTable dt = new DataTable();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_sel_Login";
                cmd.Parameters.Add(new SqlParameter("@Usuario", usuarioEntidad.Usuario));
                cmd.Parameters.Add(new SqlParameter("@Contrasena", usuarioEntidad.Contrasena));
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
        public DataTable BuscarUsuarioInterno(string TextoBuscar)
        {
            DataTable dt = new DataTable();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_sel_BuscarUsuarioInterno";
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
        public DataTable CargarPerfilAcceso()
        {
            DataTable dt = new DataTable();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_sel_CargarPerfilAcceso";
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
        public String RegistrarUsuarioInterno(UsuarioEntidad usuarioEntidad)
        {
            string mensaje;
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_ins_CrearUsuarioInterno";

                cmd.Parameters.Add(new SqlParameter("@Usuario", usuarioEntidad.Usuario));
                cmd.Parameters.Add(new SqlParameter("@Nombres", usuarioEntidad.Nombres));
                cmd.Parameters.Add(new SqlParameter("@ApellidoPaterno", usuarioEntidad.ApellidoPaterno));
                cmd.Parameters.Add(new SqlParameter("@ApellidoMaterno", usuarioEntidad.ApellidoMaterno));
                cmd.Parameters.Add(new SqlParameter("@CodPerfilAcceso", usuarioEntidad.CodPerfilAcceso));
                cmd.Parameters.Add(new SqlParameter("@Estado", usuarioEntidad.Estado));
                cmd.Parameters.Add(new SqlParameter("@Contrasena", usuarioEntidad.Contrasena));
                
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
        public String ActualizarUsuarioInterno(UsuarioEntidad usuarioEntidad, int FlagCambiarContrasena)
        {
            string mensaje;
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_upd_ActualizarUsuarioInterno";
                cmd.Parameters.Add(new SqlParameter("@CodUsuario", usuarioEntidad.CodUsuario));
                cmd.Parameters.Add(new SqlParameter("@Usuario", usuarioEntidad.Usuario));
                cmd.Parameters.Add(new SqlParameter("@Nombres", usuarioEntidad.Nombres));
                cmd.Parameters.Add(new SqlParameter("@ApellidoPaterno", usuarioEntidad.ApellidoPaterno));
                cmd.Parameters.Add(new SqlParameter("@ApellidoMaterno", usuarioEntidad.ApellidoMaterno));
                cmd.Parameters.Add(new SqlParameter("@CodPerfilAcceso", usuarioEntidad.CodPerfilAcceso));
                cmd.Parameters.Add(new SqlParameter("@Estado", usuarioEntidad.Estado));
                cmd.Parameters.Add(new SqlParameter("@Contrasena", usuarioEntidad.Contrasena));
                cmd.Parameters.Add(new SqlParameter("@FlagCambiarContrasena", FlagCambiarContrasena));
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
        public DataTable CargarDatosUsuarioInterno(int codUsuario)
        {
            DataTable dt = new DataTable();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_sel_CargarDatosUsuario";
                cmd.Parameters.Add(new SqlParameter("@CodUsuario", codUsuario));
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
