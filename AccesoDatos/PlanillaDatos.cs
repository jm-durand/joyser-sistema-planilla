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
    public class PlanillaDatos
    {
        Conexion conexion = new Conexion();
        SqlConnection con;
        SqlCommand cmd = new SqlCommand();
        public PlanillaDatos()
        {
            con = new SqlConnection(conexion.GetConexion());
        }

        public DataTable CargarTipoPlanilla()
        {
            DataTable dt = new DataTable();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_sel_CargarTipoPlanilla";
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
        public DataTable CargarDatosTrabajadorPlanilla(int CodTrabajador, int CodTipoPlanilla)
        {
            DataTable dt = new DataTable();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_sel_CargarDatosPlanillaTrabajador";
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
        public DataTable CargarDatosTrabajadorEventualPlanilla(int CodTrabajador)
        {
            DataTable dt = new DataTable();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_sel_CargarDatosPlanillaTrabajadorEventuales";
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
        public DataTable CargarDatosServicio(int CodServicio)
        {
            DataTable dt = new DataTable();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_sel_CargarDatosServiciosPlanilla";
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
        public DataTable ObtenerParametrosIngresoPlanilla(int CodTrabajador, int cantDiasLaborados, int CodTipoPlanilla)
        {
            DataTable dt = new DataTable();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_gen_ObtenerIngresosPlanillaTrabajador";
                cmd.Parameters.Add(new SqlParameter("@CodTrabajador", CodTrabajador));
                cmd.Parameters.Add(new SqlParameter("@CantidadDias", cantDiasLaborados));
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
        public DataTable ObtenerParametrosBeneficiosPlanilla(int CodTrabajador, int cantDiasLaborados, int Periodo, int CodTipoPlanilla)
        {
            DataTable dt = new DataTable();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_gen_ObtenerBeneficiosPlanillaTrabajador";
                cmd.Parameters.Add(new SqlParameter("@CodTrabajador", CodTrabajador));
                cmd.Parameters.Add(new SqlParameter("@IntMesPeriodo", Periodo));
                cmd.Parameters.Add(new SqlParameter("@CantidadDias", cantDiasLaborados));
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
        public DataTable ObtenerParametrosBeneficiosExtrasPlanilla(int CodTrabajador, int cantDiasLaborados, int CodTipoPlanilla)
        {
            DataTable dt = new DataTable();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_gen_ObtenerBeneficiosBonifExtraPlanillaTrabajador";
                cmd.Parameters.Add(new SqlParameter("@CodTrabajador", CodTrabajador));
                cmd.Parameters.Add(new SqlParameter("@CantidadDias", cantDiasLaborados));
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
        public DataTable ObtenerParametrosBeneficiosEspecialePlanilla(int CodTrabajador, int cantDiasAfectos, int Periodo, int CodTipoPlanilla)
        {
            DataTable dt = new DataTable();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_gen_ObtenerBeneficiosEspecialesPlanillaTrabajador";
                cmd.Parameters.Add(new SqlParameter("@CodTrabajador", CodTrabajador));
                cmd.Parameters.Add(new SqlParameter("@IntMesPeriodo", Periodo));
                cmd.Parameters.Add(new SqlParameter("@CantidadDias", cantDiasAfectos));
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
        public DataTable ObtenerParametrosDescuentosPlanilla(int CodTrabajador, int cantDiasLaborados, int cantDiasDominical, double TotalAfecto, int CodTipoPlanilla)
        {
            DataTable dt = new DataTable();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_gen_ObtenerDescuentosPlanillaTrabajador";
                cmd.Parameters.Add(new SqlParameter("@CodTrabajador", CodTrabajador));
                cmd.Parameters.Add(new SqlParameter("@TotalAfecto", TotalAfecto));
                cmd.Parameters.Add(new SqlParameter("@CantidadDias", cantDiasLaborados));
                cmd.Parameters.Add(new SqlParameter("@CantidadDiasDominical", cantDiasDominical));
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
        public DataTable ObtenerParametrosAportacionesEmpleadorPlanilla(int CodTrabajador, int cantDiasLaborados, double TotalAfecto, int CodTipoPlanilla)
        {
            DataTable dt = new DataTable();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_gen_ObtenerAportacionesEmpleadorPlanillaTrabajador";
                cmd.Parameters.Add(new SqlParameter("@CodTrabajador", CodTrabajador));
                cmd.Parameters.Add(new SqlParameter("@TotalAfecto", TotalAfecto));
                cmd.Parameters.Add(new SqlParameter("@CantidadDias", cantDiasLaborados));
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
        public String RegistrarPlanillaConstruccion(PlanillaEntidad planillaEntidad)
        {
            string mensaje;
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_ins_RegistrarPlanillaConstruccion";

                cmd.Parameters.Add(new SqlParameter("@CodTrabajador", planillaEntidad.CodTrabajador));
                cmd.Parameters.Add(new SqlParameter("@FechaInicio", planillaEntidad.FechaInicial));
                cmd.Parameters.Add(new SqlParameter("@FechaFin", planillaEntidad.FechaFinal));
                cmd.Parameters.Add(new SqlParameter("@PeriodoMes", planillaEntidad.MesPeriodo));
                cmd.Parameters.Add(new SqlParameter("@PeriodoAno", planillaEntidad.AnoPeriodo));
                cmd.Parameters.Add(new SqlParameter("@DiasLaborados", planillaEntidad.DiasLaborados));
                cmd.Parameters.Add(new SqlParameter("@DiasDomingosFeriados", planillaEntidad.DiasDominical));
                cmd.Parameters.Add(new SqlParameter("@AsignacionFamiliar", planillaEntidad.AsignacionFamiliar));
                cmd.Parameters.Add(new SqlParameter("@Reintegro", planillaEntidad.Reintegro));
                cmd.Parameters.Add(new SqlParameter("@Bonificacion", planillaEntidad.Bonificacion));
                cmd.Parameters.Add(new SqlParameter("@HorasExtraSimple", planillaEntidad.HorasExtraSimple));
                cmd.Parameters.Add(new SqlParameter("@HorasExtras60", planillaEntidad.HorasExtra60));
                cmd.Parameters.Add(new SqlParameter("@HorasExtras100", planillaEntidad.HorasExtra100));
                cmd.Parameters.Add(new SqlParameter("@BUC", planillaEntidad.Buc));
                cmd.Parameters.Add(new SqlParameter("@Pasajes", planillaEntidad.Pasajes));
                cmd.Parameters.Add(new SqlParameter("@Vacacional", planillaEntidad.Vacacional));
                cmd.Parameters.Add(new SqlParameter("@Gratificacion", planillaEntidad.Gratificacion));
                cmd.Parameters.Add(new SqlParameter("@Liquidacion", planillaEntidad.Liquidacion));
                cmd.Parameters.Add(new SqlParameter("@BonificacionExtraSalud", planillaEntidad.BonifacionExtraSalud));
                cmd.Parameters.Add(new SqlParameter("@BonificacionExtraPension", planillaEntidad.BonificacionExtraPension));
                cmd.Parameters.Add(new SqlParameter("@SNP", planillaEntidad.Snp));
                cmd.Parameters.Add(new SqlParameter("@AporteObligatorio", planillaEntidad.AporteObligatorio));
                cmd.Parameters.Add(new SqlParameter("@ComisionFlujo", planillaEntidad.ComisionFlujo));
                cmd.Parameters.Add(new SqlParameter("@ComisionMixta", planillaEntidad.ComisionMixta));
                cmd.Parameters.Add(new SqlParameter("@PrimaSeguro", planillaEntidad.PrimaSeguro));
                cmd.Parameters.Add(new SqlParameter("@AporteComplementario", planillaEntidad.AporteComplementario));
                cmd.Parameters.Add(new SqlParameter("@Conafovicer", planillaEntidad.Conafovicer));
                cmd.Parameters.Add(new SqlParameter("@AporteSindical", planillaEntidad.AporteSindical));
                cmd.Parameters.Add(new SqlParameter("@EsSaludVida", planillaEntidad.EsSaludVida));
                cmd.Parameters.Add(new SqlParameter("@Renta5taCategoria", planillaEntidad.Renta5taCategoria));
                cmd.Parameters.Add(new SqlParameter("@EPS", planillaEntidad.Eps));
                cmd.Parameters.Add(new SqlParameter("@OtrosDctos", planillaEntidad.OtrosDescuentos));
                cmd.Parameters.Add(new SqlParameter("@EsSalud", planillaEntidad.EsSalud));
                cmd.Parameters.Add(new SqlParameter("@AportComplementarioAFP", planillaEntidad.AporteComplementarioAFP));
                cmd.Parameters.Add(new SqlParameter("@SCTRSalud", planillaEntidad.SctrSalud));
                cmd.Parameters.Add(new SqlParameter("@SCTRPension", planillaEntidad.SctrPension));
                cmd.Parameters.Add(new SqlParameter("@TotalIngresos", planillaEntidad.TotalIngresos));
                cmd.Parameters.Add(new SqlParameter("@TotalBeneficios", planillaEntidad.TotalBeneficios));
                cmd.Parameters.Add(new SqlParameter("@TotalDescuentos", planillaEntidad.TotalDescuentos));
                cmd.Parameters.Add(new SqlParameter("@TotalAporteEmpresa", planillaEntidad.TotalAporteEmpresa));
                cmd.Parameters.Add(new SqlParameter("@TotalPagarTrabajador", planillaEntidad.TotalPagarTrabajador));
                cmd.Parameters.Add(new SqlParameter("@TotalCostoTrabajador", planillaEntidad.TotalCostoTrabajador));                
                cmd.Parameters.Add(new SqlParameter("@CodUsuarioRegistro", planillaEntidad.CodUsuarioRegistro));
                cmd.Parameters.Add(new SqlParameter("@CodProyectoPlanilla", planillaEntidad.CodProyectoPlanilla));
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
        public String RegistrarPlanillaEmpleados(PlanillaEntidad planillaEntidad)
        {
            string mensaje;
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_ins_RegistrarPlanillaEmpleados";

                cmd.Parameters.Add(new SqlParameter("@CodTrabajador", planillaEntidad.CodTrabajador));
                cmd.Parameters.Add(new SqlParameter("@FechaInicio", planillaEntidad.FechaInicial));
                cmd.Parameters.Add(new SqlParameter("@FechaFin", planillaEntidad.FechaFinal));
                cmd.Parameters.Add(new SqlParameter("@PeriodoMes", planillaEntidad.MesPeriodo));
                cmd.Parameters.Add(new SqlParameter("@PeriodoAno", planillaEntidad.AnoPeriodo));
                cmd.Parameters.Add(new SqlParameter("@DiasLaborados", planillaEntidad.DiasLaborados));
                cmd.Parameters.Add(new SqlParameter("@DiasDomingosFeriados", planillaEntidad.DiasDominical));
                cmd.Parameters.Add(new SqlParameter("@AsignacionFamiliar", planillaEntidad.AsignacionFamiliar));              
                cmd.Parameters.Add(new SqlParameter("@HorasExtraSimple", planillaEntidad.HorasExtraSimple));              
                cmd.Parameters.Add(new SqlParameter("@Pasajes", planillaEntidad.Pasajes));
                cmd.Parameters.Add(new SqlParameter("@OtrosIngresos", planillaEntidad.OtrosIngresos));                
                cmd.Parameters.Add(new SqlParameter("@Vacaciones", planillaEntidad.Vacacional));
                cmd.Parameters.Add(new SqlParameter("@Gratificacion", planillaEntidad.Gratificacion));
                cmd.Parameters.Add(new SqlParameter("@Ley29351", planillaEntidad.Ley29351));
                cmd.Parameters.Add(new SqlParameter("@BonificacionExtraSalud", planillaEntidad.BonifacionExtraSalud));
                cmd.Parameters.Add(new SqlParameter("@OtrosBeneficios", planillaEntidad.OtrosBeneficios));           
                cmd.Parameters.Add(new SqlParameter("@SNP", planillaEntidad.Snp));
                cmd.Parameters.Add(new SqlParameter("@AporteObligatorio", planillaEntidad.AporteObligatorio));
                cmd.Parameters.Add(new SqlParameter("@ComisionFlujo", planillaEntidad.ComisionFlujo));
                cmd.Parameters.Add(new SqlParameter("@ComisionMixta", planillaEntidad.ComisionMixta));
                cmd.Parameters.Add(new SqlParameter("@PrimaSeguro", planillaEntidad.PrimaSeguro));                          
                cmd.Parameters.Add(new SqlParameter("@Renta5taCategoria", planillaEntidad.Renta5taCategoria));
                cmd.Parameters.Add(new SqlParameter("@EPS", planillaEntidad.Eps));
                cmd.Parameters.Add(new SqlParameter("@EsSaludVida", planillaEntidad.EsSaludVida));
                cmd.Parameters.Add(new SqlParameter("@OtrosDctos", planillaEntidad.OtrosDescuentos));
                cmd.Parameters.Add(new SqlParameter("@EsSalud", planillaEntidad.EsSalud));
                cmd.Parameters.Add(new SqlParameter("@SCTRSalud", planillaEntidad.SctrSalud));
                cmd.Parameters.Add(new SqlParameter("@SCTRPension", planillaEntidad.SctrPension));
                cmd.Parameters.Add(new SqlParameter("@TotalIngresos", planillaEntidad.TotalIngresos));
                cmd.Parameters.Add(new SqlParameter("@TotalBeneficios", planillaEntidad.TotalBeneficios));
                cmd.Parameters.Add(new SqlParameter("@TotalDescuentos", planillaEntidad.TotalDescuentos));
                cmd.Parameters.Add(new SqlParameter("@TotalAporteEmpresa", planillaEntidad.TotalAporteEmpresa));
                cmd.Parameters.Add(new SqlParameter("@TotalPagarTrabajador", planillaEntidad.TotalPagarTrabajador));
                cmd.Parameters.Add(new SqlParameter("@TotalCostoTrabajador", planillaEntidad.TotalCostoTrabajador));
                cmd.Parameters.Add(new SqlParameter("@CodUsuarioRegistro", planillaEntidad.CodUsuarioRegistro));
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
        public String ActualizarPlanillaConstruccion(PlanillaEntidad planillaEntidad)
        {
            string mensaje;
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_upd_ActualizarPlanillaConstruccion";

                cmd.Parameters.Add(new SqlParameter("@CodTrabajador", planillaEntidad.CodTrabajador));
                cmd.Parameters.Add(new SqlParameter("@FechaInicio", planillaEntidad.FechaInicial));
                cmd.Parameters.Add(new SqlParameter("@FechaFin", planillaEntidad.FechaFinal));
                cmd.Parameters.Add(new SqlParameter("@PeriodoMes", planillaEntidad.MesPeriodo));
                cmd.Parameters.Add(new SqlParameter("@PeriodoAno", planillaEntidad.AnoPeriodo));
                cmd.Parameters.Add(new SqlParameter("@DiasLaborados", planillaEntidad.DiasLaborados));
                cmd.Parameters.Add(new SqlParameter("@DiasDomingosFeriados", planillaEntidad.DiasDominical));
                cmd.Parameters.Add(new SqlParameter("@AsignacionFamiliar", planillaEntidad.AsignacionFamiliar));
                cmd.Parameters.Add(new SqlParameter("@Reintegro", planillaEntidad.Reintegro));
                cmd.Parameters.Add(new SqlParameter("@Bonificacion", planillaEntidad.Bonificacion));
                cmd.Parameters.Add(new SqlParameter("@HorasExtraSimple", planillaEntidad.HorasExtraSimple));
                cmd.Parameters.Add(new SqlParameter("@HorasExtras60", planillaEntidad.HorasExtra60));
                cmd.Parameters.Add(new SqlParameter("@HorasExtras100", planillaEntidad.HorasExtra100));
                cmd.Parameters.Add(new SqlParameter("@BUC", planillaEntidad.Buc));
                cmd.Parameters.Add(new SqlParameter("@Pasajes", planillaEntidad.Pasajes));
                cmd.Parameters.Add(new SqlParameter("@Vacacional", planillaEntidad.Vacacional));
                cmd.Parameters.Add(new SqlParameter("@Gratificacion", planillaEntidad.Gratificacion));
                cmd.Parameters.Add(new SqlParameter("@Liquidacion", planillaEntidad.Liquidacion));
                cmd.Parameters.Add(new SqlParameter("@BonificacionExtraSalud", planillaEntidad.BonifacionExtraSalud));
                cmd.Parameters.Add(new SqlParameter("@BonificacionExtraPension", planillaEntidad.BonificacionExtraPension));
                cmd.Parameters.Add(new SqlParameter("@SNP", planillaEntidad.Snp));
                cmd.Parameters.Add(new SqlParameter("@AporteObligatorio", planillaEntidad.AporteObligatorio));
                cmd.Parameters.Add(new SqlParameter("@ComisionFlujo", planillaEntidad.ComisionFlujo));
                cmd.Parameters.Add(new SqlParameter("@ComisionMixta", planillaEntidad.ComisionMixta));
                cmd.Parameters.Add(new SqlParameter("@PrimaSeguro", planillaEntidad.PrimaSeguro));
                cmd.Parameters.Add(new SqlParameter("@AporteComplementario", planillaEntidad.AporteComplementario));
                cmd.Parameters.Add(new SqlParameter("@Conafovicer", planillaEntidad.Conafovicer));
                cmd.Parameters.Add(new SqlParameter("@AporteSindical", planillaEntidad.AporteSindical));
                cmd.Parameters.Add(new SqlParameter("@EsSaludVida", planillaEntidad.EsSaludVida));
                cmd.Parameters.Add(new SqlParameter("@Renta5taCategoria", planillaEntidad.Renta5taCategoria));
                cmd.Parameters.Add(new SqlParameter("@EPS", planillaEntidad.Eps));
                cmd.Parameters.Add(new SqlParameter("@OtrosDctos", planillaEntidad.OtrosDescuentos));
                cmd.Parameters.Add(new SqlParameter("@EsSalud", planillaEntidad.EsSalud));
                cmd.Parameters.Add(new SqlParameter("@AportComplementarioAFP", planillaEntidad.AporteComplementarioAFP));
                cmd.Parameters.Add(new SqlParameter("@SCTRSalud", planillaEntidad.SctrSalud));
                cmd.Parameters.Add(new SqlParameter("@SCTRPension", planillaEntidad.SctrPension));
                cmd.Parameters.Add(new SqlParameter("@TotalIngresos", planillaEntidad.TotalIngresos));
                cmd.Parameters.Add(new SqlParameter("@TotalBeneficios", planillaEntidad.TotalBeneficios));
                cmd.Parameters.Add(new SqlParameter("@TotalDescuentos", planillaEntidad.TotalDescuentos));
                cmd.Parameters.Add(new SqlParameter("@TotalAporteEmpresa", planillaEntidad.TotalAporteEmpresa));
                cmd.Parameters.Add(new SqlParameter("@TotalPagarTrabajador", planillaEntidad.TotalPagarTrabajador));
                cmd.Parameters.Add(new SqlParameter("@TotalCostoTrabajador", planillaEntidad.TotalCostoTrabajador));
                cmd.Parameters.Add(new SqlParameter("@CodUsuarioModificacion", planillaEntidad.CodUsuarioModificacion));
                cmd.Parameters.Add(new SqlParameter("@CodPlanillaConstruccion", planillaEntidad.CodPlanillaConstruccion));
                cmd.Parameters.Add(new SqlParameter("@CodProyectoPlanilla", planillaEntidad.CodProyectoPlanilla));
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
        public String ActualizarPlanillaEmpleados(PlanillaEntidad planillaEntidad)
        {
            string mensaje;
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_upd_ActualizarPlanillaEmpleados";

                cmd.Parameters.Add(new SqlParameter("@CodTrabajador", planillaEntidad.CodTrabajador));
                cmd.Parameters.Add(new SqlParameter("@FechaInicio", planillaEntidad.FechaInicial));
                cmd.Parameters.Add(new SqlParameter("@FechaFin", planillaEntidad.FechaFinal));
                cmd.Parameters.Add(new SqlParameter("@PeriodoMes", planillaEntidad.MesPeriodo));
                cmd.Parameters.Add(new SqlParameter("@PeriodoAno", planillaEntidad.AnoPeriodo));
                cmd.Parameters.Add(new SqlParameter("@DiasLaborados", planillaEntidad.DiasLaborados));
                cmd.Parameters.Add(new SqlParameter("@DiasDomingosFeriados", planillaEntidad.DiasDominical));
                cmd.Parameters.Add(new SqlParameter("@AsignacionFamiliar", planillaEntidad.AsignacionFamiliar));
                cmd.Parameters.Add(new SqlParameter("@HorasExtraSimple", planillaEntidad.HorasExtraSimple));
                cmd.Parameters.Add(new SqlParameter("@Pasajes", planillaEntidad.Pasajes));
                cmd.Parameters.Add(new SqlParameter("@OtrosIngresos", planillaEntidad.OtrosIngresos));                
                cmd.Parameters.Add(new SqlParameter("@Vacaciones", planillaEntidad.Vacacional));
                cmd.Parameters.Add(new SqlParameter("@Gratificacion", planillaEntidad.Gratificacion));
                cmd.Parameters.Add(new SqlParameter("@Ley29351", planillaEntidad.Ley29351));
                cmd.Parameters.Add(new SqlParameter("@BonificacionExtraSalud", planillaEntidad.BonifacionExtraSalud));
                cmd.Parameters.Add(new SqlParameter("@OtrosBeneficios", planillaEntidad.OtrosBeneficios));
                cmd.Parameters.Add(new SqlParameter("@SNP", planillaEntidad.Snp));
                cmd.Parameters.Add(new SqlParameter("@AporteObligatorio", planillaEntidad.AporteObligatorio));
                cmd.Parameters.Add(new SqlParameter("@ComisionFlujo", planillaEntidad.ComisionFlujo));
                cmd.Parameters.Add(new SqlParameter("@ComisionMixta", planillaEntidad.ComisionMixta));
                cmd.Parameters.Add(new SqlParameter("@PrimaSeguro", planillaEntidad.PrimaSeguro));                
                cmd.Parameters.Add(new SqlParameter("@Renta5taCategoria", planillaEntidad.Renta5taCategoria));
                cmd.Parameters.Add(new SqlParameter("@EPS", planillaEntidad.Eps));
                cmd.Parameters.Add(new SqlParameter("@EsSaludVida", planillaEntidad.EsSaludVida));
                cmd.Parameters.Add(new SqlParameter("@OtrosDctos", planillaEntidad.OtrosDescuentos));
                cmd.Parameters.Add(new SqlParameter("@EsSalud", planillaEntidad.EsSalud));
                cmd.Parameters.Add(new SqlParameter("@SCTRSalud", planillaEntidad.SctrSalud));
                cmd.Parameters.Add(new SqlParameter("@SCTRPension", planillaEntidad.SctrPension));
                cmd.Parameters.Add(new SqlParameter("@TotalIngresos", planillaEntidad.TotalIngresos));
                cmd.Parameters.Add(new SqlParameter("@TotalBeneficios", planillaEntidad.TotalBeneficios));
                cmd.Parameters.Add(new SqlParameter("@TotalDescuentos", planillaEntidad.TotalDescuentos));
                cmd.Parameters.Add(new SqlParameter("@TotalAporteEmpresa", planillaEntidad.TotalAporteEmpresa));
                cmd.Parameters.Add(new SqlParameter("@TotalPagarTrabajador", planillaEntidad.TotalPagarTrabajador));
                cmd.Parameters.Add(new SqlParameter("@TotalCostoTrabajador", planillaEntidad.TotalCostoTrabajador));
                cmd.Parameters.Add(new SqlParameter("@CodUsuarioModificacion", planillaEntidad.CodUsuarioModificacion));
                cmd.Parameters.Add(new SqlParameter("@CodPlanillaEmpleados", planillaEntidad.CodPlanillaEmpleados));           
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
        public String RegistrarPlanillaEventuales(PlanillaEntidad planillaEntidad)
        {
            string mensaje;
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_ins_RegistrarPlanillaEventuales";

                cmd.Parameters.Add(new SqlParameter("@CodTrabajador", planillaEntidad.CodTrabajador));
                cmd.Parameters.Add(new SqlParameter("@FechaInicio", planillaEntidad.FechaInicial));
                cmd.Parameters.Add(new SqlParameter("@FechaFin", planillaEntidad.FechaFinal));
                cmd.Parameters.Add(new SqlParameter("@CodServicio", planillaEntidad.CodServicio));
                cmd.Parameters.Add(new SqlParameter("@Cantidad", planillaEntidad.Cantidad));
                cmd.Parameters.Add(new SqlParameter("@TotalCostoTrabajador", planillaEntidad.TotalPagarEventual));
                cmd.Parameters.Add(new SqlParameter("@CodUsuarioRegistro", planillaEntidad.CodUsuarioRegistro));
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
        public String ActualizarPlanillaEventuales(PlanillaEntidad planillaEntidad)
        {
            string mensaje;
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_upd_ActualizarPlanillaEventuales";

                cmd.Parameters.Add(new SqlParameter("@CodTrabajador", planillaEntidad.CodTrabajador));
                cmd.Parameters.Add(new SqlParameter("@FechaInicio", planillaEntidad.FechaInicial));
                cmd.Parameters.Add(new SqlParameter("@FechaFin", planillaEntidad.FechaFinal));
                cmd.Parameters.Add(new SqlParameter("@CodServicio", planillaEntidad.CodServicio));
                cmd.Parameters.Add(new SqlParameter("@Cantidad", planillaEntidad.Cantidad));
                cmd.Parameters.Add(new SqlParameter("@TotalCostoTrabajador", planillaEntidad.TotalPagarEventual));
                cmd.Parameters.Add(new SqlParameter("@CodUsuarioModificacion", planillaEntidad.CodUsuarioModificacion));
                cmd.Parameters.Add(new SqlParameter("@CodPlanillaEventual", planillaEntidad.CodPlanillaEventual));
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
        public DataTable ObtenerPlanillasReciente(string FechaInicio, string FechaFin)
        {
            DataTable dt = new DataTable();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_sel_ObtenerPlanillaRecientes";
                cmd.Parameters.Add(new SqlParameter("@FechaInicial", FechaInicio));
                cmd.Parameters.Add(new SqlParameter("@FechaFinal", FechaFin));
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
        public DataTable ObtenerPlanillasEventualesReciente(string FechaInicio, string FechaFin)
        {
            DataTable dt = new DataTable();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_sel_ObtenerPlanillaEventualesRecientes";
                cmd.Parameters.Add(new SqlParameter("@FechaInicial", FechaInicio));
                cmd.Parameters.Add(new SqlParameter("@FechaFinal", FechaFin));
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
        public DataTable ObtenerPlanillasDestajoRecientes(string FechaInicio, string FechaFin)
        {
            DataTable dt = new DataTable();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_sel_ObtenerPlanillaDestajoRecientes";
                cmd.Parameters.Add(new SqlParameter("@FechaInicial", FechaInicio));
                cmd.Parameters.Add(new SqlParameter("@FechaFinal", FechaFin));
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
        public DataTable ObtenerPlanillasEmpleadosReciente(string FechaInicio, string FechaFin)
        {
            DataTable dt = new DataTable();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_sel_ObtenerPlanillaEmpleadosRecientes";
                cmd.Parameters.Add(new SqlParameter("@FechaInicial", FechaInicio));
                cmd.Parameters.Add(new SqlParameter("@FechaFinal", FechaFin));
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
        public DataTable ObtenerDatosPlanilla(int codPlanilla)
        {
            DataTable dt = new DataTable();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_sel_CargarDatosPlanilla";
                cmd.Parameters.Add(new SqlParameter("@CodPlanilla", codPlanilla));
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
        public DataTable ObtenerDatosPlanillaEventuales(int codPlanilla)
        {
            DataTable dt = new DataTable();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_sel_CargarDatosPlanillaEventual";
                cmd.Parameters.Add(new SqlParameter("@CodPlanilla", codPlanilla));
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
        public DataTable ObtenerDatosPlanillaEmpleados(int codPlanilla)
        {
            DataTable dt = new DataTable();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_sel_CargarDatosPlanillaEmpleados";
                cmd.Parameters.Add(new SqlParameter("@CodPlanilla", codPlanilla));
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
        public String RegistrarPlanillaConstruccionEventuales(PlanillaEntidad planillaEntidad)
        {
            string mensaje;
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_ins_RegistrarPlanillaConstruccionEventuales";

                cmd.Parameters.Add(new SqlParameter("@CodTrabajador", planillaEntidad.CodTrabajador));
                cmd.Parameters.Add(new SqlParameter("@FechaInicio", planillaEntidad.FechaInicial));
                cmd.Parameters.Add(new SqlParameter("@FechaFin", planillaEntidad.FechaFinal));
                cmd.Parameters.Add(new SqlParameter("@PeriodoMes", planillaEntidad.MesPeriodo));
                cmd.Parameters.Add(new SqlParameter("@PeriodoAno", planillaEntidad.AnoPeriodo));
                cmd.Parameters.Add(new SqlParameter("@DiasLaborados", planillaEntidad.DiasLaborados)); 
                cmd.Parameters.Add(new SqlParameter("@Bonificacion", planillaEntidad.Bonificacion));
                cmd.Parameters.Add(new SqlParameter("@OtrosIngresos", planillaEntidad.OtrosIngresos));             
                cmd.Parameters.Add(new SqlParameter("@Prestamos", planillaEntidad.Prestamos));
                cmd.Parameters.Add(new SqlParameter("@OtrosDctos", planillaEntidad.OtrosDescuentos));               
                cmd.Parameters.Add(new SqlParameter("@TotalIngresos", planillaEntidad.TotalIngresos));           
                cmd.Parameters.Add(new SqlParameter("@TotalDescuentos", planillaEntidad.TotalDescuentos));   
                cmd.Parameters.Add(new SqlParameter("@TotalPagarTrabajador", planillaEntidad.TotalPagarTrabajador));
                cmd.Parameters.Add(new SqlParameter("@TotalCostoTrabajador", planillaEntidad.TotalCostoTrabajador));
                cmd.Parameters.Add(new SqlParameter("@CodUsuarioRegistro", planillaEntidad.CodUsuarioRegistro));
                cmd.Parameters.Add(new SqlParameter("@CodProyectoPlanilla", planillaEntidad.CodProyectoPlanilla));
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
        public String ActualizarPlanillaConstruccionEventuales(PlanillaEntidad planillaEntidad)
        {
            string mensaje;
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_upd_ActualizarPlanillaConstruccionEventuales";

                cmd.Parameters.Add(new SqlParameter("@CodTrabajador", planillaEntidad.CodTrabajador));
                cmd.Parameters.Add(new SqlParameter("@FechaInicio", planillaEntidad.FechaInicial));
                cmd.Parameters.Add(new SqlParameter("@FechaFin", planillaEntidad.FechaFinal));
                cmd.Parameters.Add(new SqlParameter("@PeriodoMes", planillaEntidad.MesPeriodo));
                cmd.Parameters.Add(new SqlParameter("@PeriodoAno", planillaEntidad.AnoPeriodo));
                cmd.Parameters.Add(new SqlParameter("@DiasLaborados", planillaEntidad.DiasLaborados));               
                cmd.Parameters.Add(new SqlParameter("@Bonificacion", planillaEntidad.Bonificacion));
                cmd.Parameters.Add(new SqlParameter("@OtrosIngresos", planillaEntidad.OtrosIngresos));
                cmd.Parameters.Add(new SqlParameter("@Prestamos", planillaEntidad.Prestamos));
                cmd.Parameters.Add(new SqlParameter("@OtrosDctos", planillaEntidad.OtrosDescuentos));              
                cmd.Parameters.Add(new SqlParameter("@TotalIngresos", planillaEntidad.TotalIngresos));             
                cmd.Parameters.Add(new SqlParameter("@TotalDescuentos", planillaEntidad.TotalDescuentos));             
                cmd.Parameters.Add(new SqlParameter("@TotalPagarTrabajador", planillaEntidad.TotalPagarTrabajador));
                cmd.Parameters.Add(new SqlParameter("@TotalCostoTrabajador", planillaEntidad.TotalCostoTrabajador));
                cmd.Parameters.Add(new SqlParameter("@CodUsuarioModificacion", planillaEntidad.CodUsuarioModificacion));
                cmd.Parameters.Add(new SqlParameter("@CodPlanillaConstruccionEventuales", planillaEntidad.CodPlanillaConstruccion));
                cmd.Parameters.Add(new SqlParameter("@CodProyectoPlanilla", planillaEntidad.CodProyectoPlanilla));
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
        public DataTable ObtenerDatosPlanillaConstruccionEventuales(int codPlanilla)
        {
            DataTable dt = new DataTable();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PA_sel_CargarDatosPlanillaConstruccionEventuales";
                cmd.Parameters.Add(new SqlParameter("@CodPlanilla", codPlanilla));
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
