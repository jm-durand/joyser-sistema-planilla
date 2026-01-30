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
    public class ProveedoresDatos
    {
        Conexion conexion = new Conexion();
        SqlConnection con;
        SqlCommand cmd = new SqlCommand();
        public ProveedoresDatos()
        {
            con = new SqlConnection(conexion.GetConexion());
        }
        public DataTable BuscarProveedores(string TextoBuscar)
        {
            DataTable dt = new DataTable();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_sel_BuscarProveedores";
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
        public DataTable CargarDatosProveedor(int CodProveedor)
        {
            DataTable dt = new DataTable();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_sel_ObtenerDatosProveedor";
                cmd.Parameters.Add(new SqlParameter("@CodProveedor", CodProveedor));
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
        public DataTable CargarRubro()
        {
            DataTable dt = new DataTable();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_sel_CargarRubro";
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
        public String RegistrarProveedor(ProveedoresEntidad proveedoresEntidad)
        {
            string mensaje;
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_ins_RegistrarProveedor";

                cmd.Parameters.Add(new SqlParameter("@DescripcionProveedor", proveedoresEntidad.DescripcionProveedor));
                cmd.Parameters.Add(new SqlParameter("@Ruc", proveedoresEntidad.Ruc));
                cmd.Parameters.Add(new SqlParameter("@CodRubro", proveedoresEntidad.CodRubroProveedor));
                cmd.Parameters.Add(new SqlParameter("@Ciudad", proveedoresEntidad.Ciudad));
                cmd.Parameters.Add(new SqlParameter("@Direccion", proveedoresEntidad.Direccion));
                cmd.Parameters.Add(new SqlParameter("@Correo", proveedoresEntidad.Correo));
                cmd.Parameters.Add(new SqlParameter("@Telefono", proveedoresEntidad.Telefono));
                cmd.Parameters.Add(new SqlParameter("@CodUsuarioRegistro", proveedoresEntidad.CodUsuarioRegistro));
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
        public String ActualizarProveedor(ProveedoresEntidad proveedoresEntidad)
        {
            string mensaje;
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_upd_ActualizarProveedor";

                cmd.Parameters.Add(new SqlParameter("@DescripcionProveedor", proveedoresEntidad.DescripcionProveedor));
                cmd.Parameters.Add(new SqlParameter("@Ruc", proveedoresEntidad.Ruc));
                cmd.Parameters.Add(new SqlParameter("@CodRubro", proveedoresEntidad.CodRubroProveedor));
                cmd.Parameters.Add(new SqlParameter("@Ciudad", proveedoresEntidad.Ciudad));
                cmd.Parameters.Add(new SqlParameter("@Direccion", proveedoresEntidad.Direccion));
                cmd.Parameters.Add(new SqlParameter("@Correo", proveedoresEntidad.Correo));
                cmd.Parameters.Add(new SqlParameter("@Telefono", proveedoresEntidad.Telefono));
                cmd.Parameters.Add(new SqlParameter("@CodProveedor", proveedoresEntidad.CodProveedor));
                cmd.Parameters.Add(new SqlParameter("@CodUsuarioModificar", proveedoresEntidad.CodUsuarioModifica));
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
        public string EliminarProveedor(int CodProveedor, int CodUsuario)
        {
            string mensaje;
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_upd_EliminarProveedor";
                cmd.Parameters.Add(new SqlParameter("@CodProveedor", CodProveedor));
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
        public DataTable ObtenerListadoProveedores(int CodProyecto)
        {
            DataTable dt = new DataTable();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_sel_ObtenerListadoProveedores";
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
        public DataTable CargarProveedores()
        {
            DataTable dt = new DataTable();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_sel_CargarProveedores";                
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
        public DataTable CargarProyectosPagarProveedores(int CodProveedor)
        {
            DataTable dt = new DataTable();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_sel_ObtenerProyectosPagarProveedor";
                cmd.Parameters.Add(new SqlParameter("@CodProveedor", CodProveedor));
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
        public String RegistrarPagoProveedoresPorCheque(ProveedoresEntidad proveedoresEntidad)
        {
            string mensaje;
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_gen_GenerarPagoProyectoProveedorPorCheque";
                cmd.Parameters.Add(new SqlParameter("@CodCheque", proveedoresEntidad.CodCheque));
                cmd.Parameters.Add(new SqlParameter("@CodProyectoProveedor", proveedoresEntidad.CodProveedorProyectoPlanilla));
                cmd.Parameters.Add(new SqlParameter("@MontoPagar", proveedoresEntidad.MontoPagar));
                cmd.Parameters.Add(new SqlParameter("@NumMedioPago", proveedoresEntidad.NumMedioPago));
                cmd.Parameters.Add(new SqlParameter("@FechaPago", proveedoresEntidad.FechaPago));
                cmd.Parameters.Add(new SqlParameter("@CodTipoReciboPago", proveedoresEntidad.CodTipoReciboPago));
                cmd.Parameters.Add(new SqlParameter("@NumReciboPago", proveedoresEntidad.NumReciboPago));
                cmd.Parameters.Add(new SqlParameter("@CodUsuarioRegistro", proveedoresEntidad.CodUsuarioRegistro));
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
        public String RegistrarPagoProveedoresSinProyectoPorCheque(ProveedoresEntidad proveedoresEntidad)
        {
            string mensaje;
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_gen_GenerarPagoProveedorPorCheque";
                cmd.Parameters.Add(new SqlParameter("@CodCheque", proveedoresEntidad.CodCheque));
                cmd.Parameters.Add(new SqlParameter("@CodProveedor", proveedoresEntidad.CodProveedor));
                cmd.Parameters.Add(new SqlParameter("@MontoPagar", proveedoresEntidad.MontoPagar));
                cmd.Parameters.Add(new SqlParameter("@NumMedioPago", proveedoresEntidad.NumMedioPago));
                cmd.Parameters.Add(new SqlParameter("@FechaPago", proveedoresEntidad.FechaPago));
                cmd.Parameters.Add(new SqlParameter("@CodTipoReciboPago", proveedoresEntidad.CodTipoReciboPago));
                cmd.Parameters.Add(new SqlParameter("@NumReciboPago", proveedoresEntidad.NumReciboPago));
                cmd.Parameters.Add(new SqlParameter("@CodUsuarioRegistro", proveedoresEntidad.CodUsuarioRegistro));
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
        public DataTable ObtenerProyectosProveedor(int CodProveedor)
        {
            DataTable dt = new DataTable();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_sel_ObtenerProyectosProveedores";
                cmd.Parameters.Add(new SqlParameter("@CodProveedor", CodProveedor));
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
        public DataTable ObtenerPagosRealizados(int CodProveedor, int CodProyecto)
        {
            DataTable dt = new DataTable();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_sel_ObtenerPagosRealizadosProveedor";
                cmd.Parameters.Add(new SqlParameter("@CodProveedor", CodProveedor));
                cmd.Parameters.Add(new SqlParameter("@CodProyecto", CodProyecto));
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
        public DataTable ObtenerDatosPagoProveedor(int CodPagoProveedor)
        {
            DataTable dt = new DataTable();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_sel_ObtenerDatosPagoProveedor";
                cmd.Parameters.Add(new SqlParameter("@CodPagoProveedor", CodPagoProveedor));                
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
        public DataTable ObtenerDatosProveedorProyectoPago(int CodProveedor, int CodProyecto)
        {
            DataTable dt = new DataTable();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_sel_CargarDatosProveedorProyectoPago";
                cmd.Parameters.Add(new SqlParameter("@CodProveedor", CodProveedor));
                cmd.Parameters.Add(new SqlParameter("@CodProyecto", CodProyecto));
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
        public String RegistrarPagoProveedor(ProveedoresEntidad proveedoresEntidad)
        {
            string mensaje;
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_ins_RegistrarPagoProveedorProyecto";

                cmd.Parameters.Add(new SqlParameter("@CodProveedorProyecto", proveedoresEntidad.CodProveedorProyectoPlanilla));
                cmd.Parameters.Add(new SqlParameter("@MontoPagar", proveedoresEntidad.MontoPagar));
                cmd.Parameters.Add(new SqlParameter("@CodMedioPago", proveedoresEntidad.CodMedioPago));
                cmd.Parameters.Add(new SqlParameter("@NumMedioPago", proveedoresEntidad.NumMedioPago));
                cmd.Parameters.Add(new SqlParameter("@FechaPago", proveedoresEntidad.FechaPago));
                cmd.Parameters.Add(new SqlParameter("@CodTipoReciboPago", proveedoresEntidad.CodTipoReciboPago));
                cmd.Parameters.Add(new SqlParameter("@NumReciboPago", proveedoresEntidad.NumReciboPago));
                cmd.Parameters.Add(new SqlParameter("@CodUsuarioRegistro", proveedoresEntidad.CodUsuarioRegistro));
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
        public String ModificarPagoProveedor(ProveedoresEntidad proveedoresEntidad)
        {
            string mensaje;
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_upd_ActualizarPagoProveedorProyecto";

                cmd.Parameters.Add(new SqlParameter("@MontoPagar", proveedoresEntidad.MontoPagar));
                cmd.Parameters.Add(new SqlParameter("@CodMedioPago", proveedoresEntidad.CodMedioPago));
                cmd.Parameters.Add(new SqlParameter("@NumMedioPago", proveedoresEntidad.NumMedioPago));
                cmd.Parameters.Add(new SqlParameter("@FechaPago", proveedoresEntidad.FechaPago));
                cmd.Parameters.Add(new SqlParameter("@CodTipoReciboPago", proveedoresEntidad.CodTipoReciboPago));
                cmd.Parameters.Add(new SqlParameter("@NumReciboPago", proveedoresEntidad.NumReciboPago));
                cmd.Parameters.Add(new SqlParameter("@CodPagoProveedor", proveedoresEntidad.CodPagoProveedor));
                cmd.Parameters.Add(new SqlParameter("@CodUsuarioModifica", proveedoresEntidad.CodUsuarioModifica));
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
        public String EliminarPagoProveedores(ProveedoresEntidad proveedoresEntidad)
        {
            string mensaje;
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_upd_EliminarPagoProveedor";
                cmd.Parameters.Add(new SqlParameter("@CodPagoProveedor", proveedoresEntidad.CodPagoProveedor));
                cmd.Parameters.Add(new SqlParameter("@CodUsuarioModifica", proveedoresEntidad.CodUsuarioModifica));
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
