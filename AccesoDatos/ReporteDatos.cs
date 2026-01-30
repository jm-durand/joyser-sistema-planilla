using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos
{
    public class ReporteDatos
    {
        Conexion conexion = new Conexion();
        SqlConnection con;
        SqlCommand cmd = new SqlCommand();
        public ReporteDatos()
        {
            con = new SqlConnection(conexion.GetConexion());
        }

        public DataTable GenerarReportePlanilla(string FechaInicial, string FechaFinal, string MesPeriodo, string AnoPeriodo, int CodTipoPlanilla, int CodTipoBusqueda)
        {
            DataTable dt = new DataTable();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_gen_ObtenerListadoReportePlanilla";
                cmd.Parameters.Add(new SqlParameter("@FechaInicial", FechaInicial));
                cmd.Parameters.Add(new SqlParameter("@FechaFinal", FechaFinal));
                cmd.Parameters.Add(new SqlParameter("@AnoPeriodo", AnoPeriodo));
                cmd.Parameters.Add(new SqlParameter("@MesPeriodo", MesPeriodo));
                cmd.Parameters.Add(new SqlParameter("@CodTipoPlanilla", CodTipoPlanilla));
                cmd.Parameters.Add(new SqlParameter("@CodTipoBusqueda", CodTipoBusqueda));
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
        public DataTable GenerarReportePlanillaEventuales(string FechaInicial, string FechaFinal, string MesPeriodo, string AnoPeriodo, int CodTipoBusqueda)
        {
            DataTable dt = new DataTable();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_gen_ObtenerListadoReportePlanillaEventuales";
                cmd.Parameters.Add(new SqlParameter("@FechaInicial", FechaInicial));
                cmd.Parameters.Add(new SqlParameter("@FechaFinal", FechaFinal));
                cmd.Parameters.Add(new SqlParameter("@AnoPeriodo", AnoPeriodo));
                cmd.Parameters.Add(new SqlParameter("@MesPeriodo", MesPeriodo));
                cmd.Parameters.Add(new SqlParameter("@CodTipoBusqueda", CodTipoBusqueda));
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
        public DataTable GenerarReportePlanillaEmpleados(string FechaInicial, string FechaFinal, string MesPeriodo, string AnoPeriodo, int CodTipoBusqueda)
        {
            DataTable dt = new DataTable();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_gen_ObtenerListadoReportePlanillaEmpleados";
                cmd.Parameters.Add(new SqlParameter("@FechaInicial", FechaInicial));
                cmd.Parameters.Add(new SqlParameter("@FechaFinal", FechaFinal));
                cmd.Parameters.Add(new SqlParameter("@AnoPeriodo", AnoPeriodo));
                cmd.Parameters.Add(new SqlParameter("@MesPeriodo", MesPeriodo));
                cmd.Parameters.Add(new SqlParameter("@CodTipoBusqueda", CodTipoBusqueda));
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
        public DataTable GenerarReportePlanillaDestajo(string FechaInicial, string FechaFinal, string MesPeriodo, string AnoPeriodo, int CodTipoBusqueda)
        {
            DataTable dt = new DataTable();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_gen_ObtenerListadoReportePlanillaDestajo";
                cmd.Parameters.Add(new SqlParameter("@FechaInicial", FechaInicial));
                cmd.Parameters.Add(new SqlParameter("@FechaFinal", FechaFinal));
                cmd.Parameters.Add(new SqlParameter("@AnoPeriodo", AnoPeriodo));
                cmd.Parameters.Add(new SqlParameter("@MesPeriodo", MesPeriodo));
                cmd.Parameters.Add(new SqlParameter("@CodTipoBusqueda", CodTipoBusqueda));
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
        public DataTable ObtenerPlanillasTrabajadorPorFecha(DateTime FechaInicial, DateTime FechaFinal, int CodTrabajador)
        {
            DataTable dt = new DataTable();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_sel_ObtenerPlanillasTrabajador";
                cmd.Parameters.Add(new SqlParameter("@CodTrabajador", CodTrabajador));
                cmd.Parameters.Add(new SqlParameter("@FechaInicial", FechaInicial));
                cmd.Parameters.Add(new SqlParameter("@FechaFinal", FechaFinal));

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
        public DataTable ObtenerPlanillasTrabajadorReporteIndividual(DateTime FechaInicial, DateTime FechaFinal, int CodTrabajador, int CodTipoPlanilla)
        {
            DataTable dt = new DataTable();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_sel_ObtenerPlanillasTrabajadorReporteIndividual";
                cmd.Parameters.Add(new SqlParameter("@CodTrabajador", CodTrabajador));
                cmd.Parameters.Add(new SqlParameter("@FechaInicial", FechaInicial));
                cmd.Parameters.Add(new SqlParameter("@FechaFinal", FechaFinal));
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
        public DataTable ObtenerReporteBoletaPagoIndividual(int CodPlanilla)
        {
            DataTable dt = new DataTable();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_gen_ObtenerDetallePlanillaTrabajador";
                cmd.Parameters.Add(new SqlParameter("@CodPlanilla", CodPlanilla));         

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
        public DataTable ObtenerReporteBoletaPagoIndividualEmpleados(int CodPlanilla)
        {
            DataTable dt = new DataTable();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_gen_ObtenerDetallePlanillaTrabajadorEmpleados";
                cmd.Parameters.Add(new SqlParameter("@CodPlanilla", CodPlanilla));

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
        public DataTable ObtenerReporteBoletaPagoPlanillaEventuales(int CodPlanilla)
        {
            DataTable dt = new DataTable();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_gen_ObtenerDetallePlanillaEventualesTrabajador";
                cmd.Parameters.Add(new SqlParameter("@CodPlanilla", CodPlanilla));

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
        public DataTable ObtenerReporteReciboEventualIndividual(int CodPlanilla)
        {
            DataTable dt = new DataTable();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_gen_ObtenerDetallePlanillaTrabajadorEventual";
                cmd.Parameters.Add(new SqlParameter("@CodPlanilla", CodPlanilla));

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
        public DataTable ObtenerReporteBoletaPagoMasivo(DateTime FechaInicial, DateTime FechaFinal)
        {
            DataTable dt = new DataTable();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_gen_ObtenerBoletasPagoMasivo";
                cmd.Parameters.Add(new SqlParameter("@FechaInicial", FechaInicial));
                cmd.Parameters.Add(new SqlParameter("@FechaFinal", FechaFinal));

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
        public DataTable ObtenerReporteBoletaPagoMasivoEventuales(DateTime FechaInicial, DateTime FechaFinal)
        {
            DataTable dt = new DataTable();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_gen_ObtenerBoletasPagoMasivoEventuales";
                cmd.Parameters.Add(new SqlParameter("@FechaInicial", FechaInicial));
                cmd.Parameters.Add(new SqlParameter("@FechaFinal", FechaFinal));

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
        public DataTable ObtenerReporteBoletaPagoMasivoDestajo(DateTime FechaInicial, DateTime FechaFinal)
        {
            DataTable dt = new DataTable();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_gen_ObtenerBoletasPagoMasivoDestajo";
                cmd.Parameters.Add(new SqlParameter("@FechaInicial", FechaInicial));
                cmd.Parameters.Add(new SqlParameter("@FechaFinal", FechaFinal));

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
        public DataTable ObtenerReporteBoletaPagoMasivoEmpleados(DateTime FechaInicial, DateTime FechaFinal)
        {
            DataTable dt = new DataTable();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_gen_ObtenerBoletasPagoMasivoEmpleados";
                cmd.Parameters.Add(new SqlParameter("@FechaInicial", FechaInicial));
                cmd.Parameters.Add(new SqlParameter("@FechaFinal", FechaFinal));

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
        public DataTable ObtenerDatosGenerales()
        {
            DataTable dt = new DataTable();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_sel_CargarDatosGenerales";      

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
        public DataTable ObtenerReporteBoletaPagoIndividualEventuales(int CodPlanilla)
        {
            DataTable dt = new DataTable();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_gen_ObtenerDetallePlanillaTrabajadorEventuales";
                cmd.Parameters.Add(new SqlParameter("@CodPlanilla", CodPlanilla));

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
        public DataTable GenerarReportePagoCheques(string CodEstado, string CodTipoPago, string FechaInicio, string FechaFin, string CodProyectoPlanilla)
        {
            DataTable dt = new DataTable();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_rep_ReportePagoCheques";
                cmd.Parameters.Add(new SqlParameter("@CodEstado", CodEstado));
                cmd.Parameters.Add(new SqlParameter("@CodTipoPago", CodTipoPago));
                cmd.Parameters.Add(new SqlParameter("@FechaInicio", FechaInicio));
                cmd.Parameters.Add(new SqlParameter("@FechaFin", FechaFin));
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
    }
}
