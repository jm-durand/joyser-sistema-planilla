using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogicaNegocio;
using Entidad;
using System.Data;
using Microsoft.Reporting.WebForms;
using System.Globalization;
using System.Drawing;
using System.Configuration;

namespace SistemaGestionPlanilla.Planilla
{
    public partial class GenerarPlanillaEventuales : System.Web.UI.Page
    {
        PlanillaNegocio procesoPlanilla = new PlanillaNegocio();
        TrabajadorNegocio procesosTrabajador = new TrabajadorNegocio();
        ReporteNegocio procesoReporte = new ReporteNegocio();
        CentroCostosNegocio procesoCentroCostos = new CentroCostosNegocio();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["CodUsuarioInterno"] != null)
                {                    
                    txtFechaInicial.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    txtFechaFinal.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    txtFechaInicialReporte.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    txtFechaFinalReporte.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    txtFechaAnoPeriodoSelect.Text = DateTime.Now.Year.ToString();

                    gpBoletaPlanilla.Visible = false;
                    gpFormularioTrabajador.Visible = false;
                    gpFormularioParametros.Visible = false;
                    gpBtnGenerarBoleta.Visible = false;
                    gpMensaje.Visible = false;

                    Session["CodPlanillaModificar"] = "";
                    gpReporteRVIndividual.Style.Add("display", "none");
                    PlanillasRecientes(txtFechaInicialReporte.Text.Trim(), txtFechaFinalReporte.Text.Trim());
                    CargarProyectoPlanilla();
                }
                else
                {
                    Response.Redirect(ConfigurationManager.AppSettings["AssetsUrl"] + "/Seguridad/Logout.aspx");
                }
            }
        }
        protected void cboTrabajadores_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cboTrabajadores.SelectedValue.ToString() != "Seleccione")
                {
                    CargarDatosTrabajadorPlanilla(cboTrabajadores.SelectedValue.ToString(), "2");                                     
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }

        protected void txtDiasLaborados_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (cboTrabajadores.SelectedValue != "Seleccione")
                {
                    ObtenerIngresos(cboTrabajadores.SelectedValue.ToString(), txtDiasLaborados.Text.Trim(), "2");                    

                    ObtenerTotalIngresos();         
                    
                    ObtenerTotalDescuentos();

                    ObtenerTotalPagar();

                    ObtenerCostoTotalTrabajador();
                }
                else
                {

                    txtDiasLaborados.Text = "0";
                    ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('Debe seleccionar un trabajador para generar el calculo','info');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }     
        protected void txtBonificacion_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ObtenerTotalIngresos();             
                ObtenerTotalDescuentos();

                ObtenerTotalPagar();
                ObtenerCostoTotalTrabajador();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }
        protected void txtOtrosIngresos_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ObtenerTotalIngresos();
                ObtenerTotalDescuentos();

                ObtenerTotalPagar();
                ObtenerCostoTotalTrabajador();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }
        protected void cboMesPeriodo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (cboTrabajadores.SelectedValue != "Seleccione")
                {
                    ObtenerTotalIngresos();

                    ObtenerTotalDescuentos();

                    ObtenerTotalPagar();

                    ObtenerCostoTotalTrabajador();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }
        protected void txtPrestamosTrabajador_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ObtenerTotalIngresos();
                ObtenerTotalDescuentos();
                ObtenerTotalPagar();
                ObtenerCostoTotalTrabajador();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }
        protected void txtOtrosDescuentosTrabajador_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ObtenerTotalIngresos();
                ObtenerTotalDescuentos();
                ObtenerTotalPagar();
                ObtenerCostoTotalTrabajador();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }
        protected void btnGenerarBoleta_Click(object sender, EventArgs e)
        {
            try
            {
                if (cboTrabajadores.SelectedValue != "Seleccione")
                {
                    gpFormularioCalculoPlanilla.Visible = false;
                    gpBoletaPlanilla.Visible = true;

                    ObtenerTotalIngresos();
                    ObtenerTotalDescuentos();
                    ObtenerTotalPagar();
                    ObtenerCostoTotalTrabajador();

                    GenerarBoletaTrabajadorPrevio(cboTrabajadores.SelectedValue.ToString());

                    ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "GotoTop", "GotoTop();", true);

                    ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('Vista de Boleta generada correctamente','info');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('Debe seleccionar un trabajador para generar el calculo','info');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }
        protected void btnMostrarListaTrabajador_Click(object sender, EventArgs e)
        {
            try
            {
                gpFormularioTrabajador.Visible = true;
                gpPlanillaRecientes.Visible = false;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }           
        }
        protected void btnRegresarCalculoPlanilla_Click(object sender, EventArgs e)
        {
            gpFormularioCalculoPlanilla.Visible = true;
            gpBoletaPlanilla.Visible = false;
        }
        protected void btnGuardarPlanilla_Click(object sender, EventArgs e)
        {
            try
            {
                string codTrabajador = cboTrabajadores.SelectedValue.ToString();                
                string fechaInicio = txtFechaInicial.Text.Trim();
                string fechaFinal = txtFechaFinal.Text.Trim();
                string periodoMes = cboMesPeriodo.SelectedValue.ToString().Trim();
                string periodoAno = txtFechaAnoPeriodoSelect.Text.Trim();
                string diasLaborado = txtDiasLaborados.Text.Trim();
                string otroIngresos = txtOtrosIngresos.Text.Trim();
                string bonificacion = txtBonificacion.Text.Trim();
                string prestamos = txtPrestamosTrabajador.Text.Trim();
                string otrosDescuentos = txtOtrosDescuentosTrabajador.Text.Trim();
                string codProyectoPlanilla = cboProyectoPlanilla.SelectedValue.ToString();
               
                string totalIngresos = Session["TotalIngresos"].ToString();    
                string totalDescuentos = Session["TotalDescuentos"].ToString();
             
                string totalPagarTrabajador = Session["TotalPagar"].ToString();
                string totalCostoTrabajador = Session["CostoTotalTrabajador"].ToString();
                string codUsuarioRegistrador = Session["CodUsuarioInterno"].ToString();

                if (otroIngresos == string.Empty) { otroIngresos = "0"; }             
                if (bonificacion == string.Empty) { bonificacion = "0"; }
                if (prestamos == string.Empty) { prestamos = "0"; }
                if (otrosDescuentos == string.Empty) { otrosDescuentos = "0"; }               

                string mensaje="";
                if (Session["CodPlanillaModificar"].ToString() == string.Empty)
                {
                    mensaje = RegistrarPlanillaConstruccion(codTrabajador, fechaInicio, fechaFinal, periodoMes, periodoAno, diasLaborado, bonificacion, otroIngresos,
                    prestamos, otrosDescuentos, totalIngresos, totalDescuentos, totalPagarTrabajador, totalCostoTrabajador, codUsuarioRegistrador, codProyectoPlanilla);
                }
                else
                {
                    mensaje = ActualizarPlanillaConstruccion(codTrabajador, fechaInicio, fechaFinal, periodoMes, periodoAno, diasLaborado, bonificacion, otroIngresos,
                    prestamos, otrosDescuentos, totalIngresos, totalDescuentos, totalPagarTrabajador, totalCostoTrabajador, codUsuarioRegistrador, Session["CodPlanillaModificar"].ToString(), codProyectoPlanilla);
                }


                if (mensaje == "EXITO")
                {
                    ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('Planilla registrada correctamente.','success');", true);

                    PlanillasRecientes(txtFechaInicialReporte.Text.Trim(), txtFechaFinalReporte.Text.Trim());

                    gpFormularioCalculoPlanilla.Visible = true;
                    gpBoletaPlanilla.Visible = false;

                    gpFormularioTrabajador.Visible = false;
                    gpFormularioParametros.Visible = false;
                    gpBtnGenerarBoleta.Visible = false;

                    gpPlanillaRecientes.Visible = true;

                    LimpiarFomulario();
                }
                else if (mensaje == "REPETIDO")
                {
                    ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('Ya hay una planilla registrada para este trabajador en el rango de fechas especificado.','info');", true);
                }
                else if (mensaje == "CESADO")
                {
                    ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('No puede registrar planilla a un trabajador cesado.','info');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + mensaje + "','error');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }
        protected void dgvPlanillaRecientes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "ModificarPlanilla")
                {
                    string codPlanilla = e.CommandArgument.ToString();
                    CargarDatosPlanilla(codPlanilla);
                }
                else if (e.CommandName == "ImprimirPlanilla")
                {
                    string codPlanilla = e.CommandArgument.ToString();
                    gpFormularioGenerarPlanilla.Style.Add("display", "none");
                    gpReporteRVIndividual.Style.Add("display", "normal");
                    GenerarReporteBoletaPagoInvidual(codPlanilla);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }
        protected void btnCancelarBoleta_Click(object sender, EventArgs e)
        {
            try
            {
                LimpiarFomulario();

                gpFormularioCalculoPlanilla.Visible = true;
                gpBoletaPlanilla.Visible = false;

                gpFormularioTrabajador.Visible = false;
                gpFormularioParametros.Visible = false;
                gpBtnGenerarBoleta.Visible = false;

                gpPlanillaRecientes.Visible = true;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }
        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            try
            {
                LimpiarFomulario();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }
        protected void dgvPlanillaRecientes_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                dgvPlanillaRecientes.PageIndex = e.NewPageIndex;
                PlanillasRecientes(txtFechaInicialReporte.Text.Trim(), txtFechaFinalReporte.Text.Trim());
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }
        protected void btnRegresarCalculos_Click(object sender, EventArgs e)
        {
            gpFormularioGenerarPlanilla.Style.Add("display", "normal");
            gpReporteRVIndividual.Style.Add("display", "none");
        }
        protected void cboProyectoPlanilla_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                CargarProyectoTrabajadores(cboProyectoPlanilla.SelectedValue.ToString());
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }
        protected void btnBuscarReporteReciente_Click(object sender, EventArgs e)
        {
            try
            {
                int contadoVacio = 0;
                string fechaInicial = txtFechaInicialReporte.Text.Trim();
                string fechaFinal = txtFechaFinalReporte.Text.Trim();

                if (fechaInicial == string.Empty) { contadoVacio += 1; txtFechaInicial.BorderWidth = 1; txtFechaInicial.BorderStyle = BorderStyle.Solid; txtFechaInicial.BorderColor = Color.Red; } else { txtFechaInicial.BorderColor = ColorTranslator.FromHtml("#ebedf2"); }
                if (fechaFinal == string.Empty) { contadoVacio += 1; txtFechaFinal.BorderWidth = 1; txtFechaFinal.BorderStyle = BorderStyle.Solid; txtFechaFinal.BorderColor = Color.Red; } else { txtFechaFinal.BorderColor = ColorTranslator.FromHtml("#ebedf2"); }

                if (contadoVacio == 0)
                {
                    PlanillasRecientes(fechaInicial, fechaFinal);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('Debe completar los campos marcados con rojo','info');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }
        /*---------------------------------------*/
        /*---------------METODOS-----------------*/
        /*---------------------------------------*/

        private string RegistrarPlanillaConstruccion(string codTrabajador, string fechaInicio, string fechaFin, string mesPeriodo, string anoPeriodo, string diasLaborados, string bonificacion, string otrosIngresos, string prestamos, string otrosDsctos, string totalIngresos, string totalDescuentos, string totalPagarTrabajador, string totalCostoTrabajador, string codUsuarioRegistro, string codProyectoPlanilla)
        {
            PlanillaEntidad planillaEntidad = new PlanillaEntidad();
            planillaEntidad.CodTrabajador = Convert.ToInt32(codTrabajador);
            planillaEntidad.FechaInicial = Convert.ToDateTime(fechaInicio);
            planillaEntidad.FechaFinal = Convert.ToDateTime(fechaFin);
            planillaEntidad.MesPeriodo = mesPeriodo;
            planillaEntidad.AnoPeriodo = anoPeriodo;
            planillaEntidad.DiasLaborados = double.Parse(diasLaborados, CultureInfo.InvariantCulture);           
            planillaEntidad.Bonificacion = double.Parse(bonificacion, CultureInfo.InvariantCulture);
            planillaEntidad.OtrosIngresos = double.Parse(otrosIngresos, CultureInfo.InvariantCulture);
            planillaEntidad.Prestamos = double.Parse(prestamos, CultureInfo.InvariantCulture);
            planillaEntidad.OtrosDescuentos = double.Parse(otrosDsctos, CultureInfo.InvariantCulture);           
            planillaEntidad.TotalIngresos = double.Parse(totalIngresos.Replace(",", "."), CultureInfo.InvariantCulture);
            planillaEntidad.TotalDescuentos = double.Parse(totalDescuentos.Replace(",", "."), CultureInfo.InvariantCulture);
            planillaEntidad.TotalPagarTrabajador = double.Parse(totalPagarTrabajador.Replace(",", "."), CultureInfo.InvariantCulture);
            planillaEntidad.TotalCostoTrabajador = double.Parse(totalCostoTrabajador.Replace(",", "."), CultureInfo.InvariantCulture);
            planillaEntidad.CodUsuarioRegistro = Convert.ToInt32(codUsuarioRegistro);
            planillaEntidad.CodProyectoPlanilla = Convert.ToInt32(codProyectoPlanilla);

            return procesoPlanilla.RegistrarPlanillaConstruccionEventuales(planillaEntidad);
        }
        private string ActualizarPlanillaConstruccion(string codTrabajador, string fechaInicio, string fechaFin, string mesPeriodo, string anoPeriodo, string diasLaborados, string bonificacion, string otrosIngresos, string prestamos, string otrosDsctos, string totalIngresos, string totalDescuentos, string totalPagarTrabajador, string totalCostoTrabajador, string codUsuarioModificacion, string codPlanillaConstruccion, string codProyectoPlanilla)
        {
            PlanillaEntidad planillaEntidad = new PlanillaEntidad();
            planillaEntidad.CodTrabajador = Convert.ToInt32(codTrabajador);
            planillaEntidad.FechaInicial = Convert.ToDateTime(fechaInicio);
            planillaEntidad.FechaFinal = Convert.ToDateTime(fechaFin);
            planillaEntidad.MesPeriodo = mesPeriodo;
            planillaEntidad.AnoPeriodo = anoPeriodo;
            planillaEntidad.DiasLaborados = double.Parse(diasLaborados, CultureInfo.InvariantCulture);                     
            planillaEntidad.Bonificacion = double.Parse(bonificacion, CultureInfo.InvariantCulture);
            planillaEntidad.OtrosIngresos = double.Parse(otrosIngresos, CultureInfo.InvariantCulture);
            planillaEntidad.Prestamos = double.Parse(prestamos, CultureInfo.InvariantCulture);
            planillaEntidad.OtrosDescuentos = double.Parse(otrosDsctos, CultureInfo.InvariantCulture);            
            planillaEntidad.TotalIngresos = double.Parse(totalIngresos.Replace(",", "."), CultureInfo.InvariantCulture);          
            planillaEntidad.TotalDescuentos = double.Parse(totalDescuentos.Replace(",", "."), CultureInfo.InvariantCulture);        
            planillaEntidad.TotalPagarTrabajador = double.Parse(totalPagarTrabajador.Replace(",", "."), CultureInfo.InvariantCulture);
            planillaEntidad.TotalCostoTrabajador = double.Parse(totalCostoTrabajador.Replace(",", "."), CultureInfo.InvariantCulture);
            planillaEntidad.CodUsuarioModificacion = Convert.ToInt32(codUsuarioModificacion);
            planillaEntidad.CodPlanillaConstruccion = Convert.ToInt32(codPlanillaConstruccion);
            planillaEntidad.CodProyectoPlanilla = Convert.ToInt32(codProyectoPlanilla);

            return procesoPlanilla.ActualizarPlanillaConstruccionEventuales(planillaEntidad);
        }
        private void CargarDatosPlanilla(string codPlanilla)
        {
            DataTable dt = procesoPlanilla.ObtenerDatosPlanillaConstruccionEventuales(Convert.ToInt32(codPlanilla));

            if (dt.Rows.Count > 0)
            {
                LimpiarFomulario();

                gpFormularioCalculoPlanilla.Visible = true;
                gpBoletaPlanilla.Visible = false;

                gpFormularioTrabajador.Visible = true;
                gpFormularioParametros.Visible = true;
                gpBtnGenerarBoleta.Visible = true;

                gpPlanillaRecientes.Visible = false;

                txtFechaAnoPeriodo.Text = dt.Rows[0][0].ToString();
                txtFechaAnoPeriodoSelect.Text = dt.Rows[0][0].ToString();
                cboMesPeriodo.SelectedValue = dt.Rows[0][1].ToString().Trim();
                txtFechaInicial.Text = dt.Rows[0][2].ToString();
                txtFechaFinal.Text = dt.Rows[0][3].ToString();

                cboProyectoPlanilla.SelectedValue = dt.Rows[0][10].ToString();

                cboProyectoPlanilla_SelectedIndexChanged(cboProyectoPlanilla, new EventArgs());

                cboTrabajadores.SelectedValue = dt.Rows[0][4].ToString().Trim();

                cboTrabajadores_SelectedIndexChanged(cboTrabajadores, new EventArgs());

                txtDiasLaborados.Text = Convert.ToInt32(dt.Rows[0][5]).ToString();     
                txtBonificacion.Text = dt.Rows[0][6].ToString().Replace(",", ".");
                txtOtrosIngresos.Text = dt.Rows[0][7].ToString().Replace(",", ".");
                txtPrestamosTrabajador.Text = dt.Rows[0][8].ToString().Replace(",", ".");
                txtOtrosDescuentosTrabajador.Text = dt.Rows[0][9].ToString().Replace(",", ".");

                txtDiasLaborados_TextChanged(txtDiasLaborados.Text, new EventArgs());

                Session["CodPlanillaModificar"] = codPlanilla;
            }
        }
        private void CargarListadoTrabajadores()
        {
            try
            {
                DataTable dt = procesosTrabajador.ObtenerListaTrabajadoresEventuales();
                cboTrabajadores.DataSource = dt;
                cboTrabajadores.DataTextField = "NombreTrabajador";
                cboTrabajadores.DataValueField = "CodTrabajador";
                cboTrabajadores.DataBind();
                cboTrabajadores.Items.Insert(0, "Seleccione");
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }
        private void CargarTipoPlanilla()
        {
            try
            {
                DataTable dt = procesoPlanilla.CargarTipoPlanilla();
                cboTipoPlanilla.DataSource = dt;
                cboTipoPlanilla.DataTextField = "DescPlanilla";
                cboTipoPlanilla.DataValueField = "CodTipoPlanilla";
                cboTipoPlanilla.DataBind();
                cboTipoPlanilla.Items.Insert(0, "Seleccione");
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }
        private void CargarTrabajadores()
        {
            try
            {
                DataTable dt = procesosTrabajador.ObtenerListaTrabajadoresEventuales();
                cboTrabajadores.DataSource = dt;
                cboTrabajadores.DataTextField = "NombreTrabajador";
                cboTrabajadores.DataValueField = "CodTrabajador";
                cboTrabajadores.DataBind();
                cboTrabajadores.Items.Insert(0, "Seleccione");
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }
        private void CargarProyectoTrabajadores(string codProyectoPlanilla)
        {
            try
            {
                DataTable dt = procesosTrabajador.ObtenerProyectoPlanillaTrabajadoresEventuales(Convert.ToInt32(codProyectoPlanilla));
                cboTrabajadores.DataSource = dt;
                cboTrabajadores.DataTextField = "NombreTrabajador";
                cboTrabajadores.DataValueField = "CodTrabajador";
                cboTrabajadores.DataBind();
                cboTrabajadores.Items.Insert(0, "Seleccione");
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }
        private void CargarDatosTrabajadorPlanilla(string codTrabajador, string codTipoPlanilla)
        {
            DataTable dt = procesoPlanilla.CargarDatosTrabajadorPlanilla(Convert.ToInt32(codTrabajador), Convert.ToInt32(codTipoPlanilla));

            txtJornalTrabajador.Text = "S/. " + Convert.ToDouble(dt.Rows[0][2]).ToString("N", CultureInfo.GetCultureInfo("es-PE"));
            txtPerfilPlanilla.Text = dt.Rows[0][6].ToString().Trim();

            if (dt.Rows[0][6].ToString() != string.Empty)
            {
                gpFormularioParametros.Visible = true;
                gpBtnGenerarBoleta.Visible = true;
                gpMensaje.Visible = false;

                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Rangofecha", "CargarDateRange();", true);
            }
            else
            {
                txtPerfilPlanilla.Text = "Sin Perfil de Planilla asignado";
                gpMensaje.Visible = true;
                gpFormularioParametros.Visible = false;
                gpBtnGenerarBoleta.Visible = false;
            }
        }
        private void ObtenerIngresos(string codTrabajador, string cantDiasLaborados, string codTipoPlanilla)
        {
            DataTable dt = procesoPlanilla.ObtenerParametrosIngresoPlanilla(Convert.ToInt32(codTrabajador), Convert.ToInt32(cantDiasLaborados), Convert.ToInt32(codTipoPlanilla));

            txtTotalCalculadoDiasLaborados.Text = Convert.ToDouble(dt.Rows[0][0]).ToString("N", CultureInfo.GetCultureInfo("es-PE"));         
        }             
        private void ObtenerTotalIngresos()
        {      
            string bonificacion = txtBonificacion.Text.Trim();
            string otrosIngresos = txtOtrosIngresos.Text.Trim();
            string totalDiasLaborados = txtTotalCalculadoDiasLaborados.Text.Trim();

            if (bonificacion == string.Empty) { bonificacion = "0"; }
            if (otrosIngresos == string.Empty) { otrosIngresos = "0"; }
            if (totalDiasLaborados == string.Empty) { totalDiasLaborados = "0"; }

            double bonificacionDoub = double.Parse(bonificacion, CultureInfo.InvariantCulture);
            double otrosIngresosDoub = double.Parse(otrosIngresos, CultureInfo.InvariantCulture);
            double totalDiasLaboradosDoub = double.Parse(totalDiasLaborados, CultureInfo.InvariantCulture);

            Session["TotalIngresos"] = (bonificacionDoub + otrosIngresosDoub + totalDiasLaboradosDoub).ToString();
            txtTotalIngresos.InnerText = "S/. " + (bonificacionDoub + otrosIngresosDoub + totalDiasLaboradosDoub).ToString("N", CultureInfo.GetCultureInfo("es-PE"));
        }      
        private void ObtenerTotalDescuentos()
        {
            string prestamos = txtPrestamosTrabajador.Text.Trim();
            string otroDescuentos = txtOtrosDescuentosTrabajador.Text.Trim();

            if (prestamos == string.Empty) { prestamos = "0"; }
            if (otroDescuentos == string.Empty) { otroDescuentos = "0"; }

            double prestamosDoub = double.Parse(prestamos, CultureInfo.InvariantCulture);
            double otroDescuentosDoub = double.Parse(otroDescuentos, CultureInfo.InvariantCulture);

            Session["TotalDescuentos"] = (prestamosDoub + otroDescuentosDoub).ToString();
            txtTotalDescuentos.InnerText = "S/. " + (prestamosDoub + otroDescuentosDoub).ToString("N", CultureInfo.GetCultureInfo("es-PE"));
        }

        private void ObtenerTotalPagar()
        {
            string totalIngresos = Session["TotalIngresos"].ToString().Trim();   
            string totalDescuentos = Session["TotalDescuentos"].ToString().Trim();

            if (totalIngresos == string.Empty) { totalIngresos = "0"; }
            if (totalDescuentos == string.Empty) { totalDescuentos = "0"; }

            double totalIngresosDoub = double.Parse(totalIngresos);
            double totalDescuentosDoub = double.Parse(totalDescuentos);

            Session["TotalPagar"] = (totalIngresosDoub - totalDescuentosDoub);
            txtTotalPagar.InnerText = "S/. " + (totalIngresosDoub - totalDescuentosDoub).ToString("N", CultureInfo.GetCultureInfo("es-PE"));
        }        
        private void ObtenerCostoTotalTrabajador()
        {
            string totalPagarTrabajador = Session["TotalPagar"].ToString().Trim();

            if (totalPagarTrabajador == string.Empty) { totalPagarTrabajador = "0"; }

            double totalPagarTrabajadorDoub = double.Parse(totalPagarTrabajador);

            Session["CostoTotalTrabajador"] = (totalPagarTrabajadorDoub);
            txtCostoTotalTrabajador.InnerText = "S/. " + (totalPagarTrabajadorDoub).ToString("N", CultureInfo.GetCultureInfo("es-PE"));
        }

        private void GenerarBoletaTrabajadorPrevio(string codTrabajador)
        {
            DataTable dt = procesosTrabajador.ObtenerDatosTrabajadorBoletaPlanillaPrevio(Convert.ToInt32(codTrabajador));
            lblNombreTrabajadorBoleta.Text = dt.Rows[0][0].ToString();
            lblDocumentoTrabajadorBoleta.Text = dt.Rows[0][1].ToString();
            lblCargoTrabajadorBoleta.Text = dt.Rows[0][2].ToString();
            lblNombreBancoBoleta.Text = dt.Rows[0][3].ToString();
            lblNroCCIBoleta.Text = dt.Rows[0][4].ToString();
            lblNroCuentaBoleta.Text = dt.Rows[0][4].ToString();

            lblPeriodoBoleta.Text = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(Convert.ToInt32(cboMesPeriodo.SelectedValue)) + ", " + txtFechaAnoPeriodoSelect.Text;
            lblSemanaBoleta.Text = txtFechaInicial.Text + " - " + txtFechaFinal.Text;

            lblDiasLaboradosDescuentos.Text = txtDiasLaborados.Text + " días";
            lblDiasLaboradosIngresos.Text = txtDiasLaborados.Text + " días";

            lblTotalIngreso.Text = Session["TotalIngresos"].ToString();
            lblTotalDescuentos.Text = Session["TotalDescuentos"].ToString();

            lblTotalCostoEmpresa.InnerText = "S/. " + Session["CostoTotalTrabajador"].ToString();

        }
        private void PlanillasRecientes(string fechaInicial, string fechaFinal)
        {
            DataTable dt = procesoPlanilla.ObtenerPlanillasEventualesReciente(fechaInicial, fechaFinal);
            dgvPlanillaRecientes.DataSource = dt;
            dgvPlanillaRecientes.DataBind();

            lblResultadoBusqueda.InnerHtml = "" + dt.Rows.Count.ToString() + " registros(s) reciente(s) encontrado(s)";
        }
        private void LimpiarFomulario()
        {
            cboTrabajadores.ClearSelection();        
            txtFechaInicial.Text = DateTime.Now.ToShortDateString();
            txtFechaFinal.Text = DateTime.Now.ToShortDateString();
            cboMesPeriodo.ClearSelection();
            txtFechaAnoPeriodoSelect.Text = string.Empty;
            txtDiasLaborados.Text = string.Empty;
            txtOtrosIngresos.Text = string.Empty;          
            txtBonificacion.Text = string.Empty;
            txtPrestamosTrabajador.Text = string.Empty;
            txtOtrosDescuentosTrabajador.Text = string.Empty;           
            txtJornalTrabajador.Text = string.Empty;
            txtTotalCalculadoDiasLaborados.Text = string.Empty;          
            txtTotalIngresos.InnerText = "0.00";
            txtTotalDescuentos.InnerText = "0.00";   
            txtTotalPagar.InnerText = "0.00";
            txtCostoTotalTrabajador.InnerText = "0.00";

            Session["TotalIngresos"] = "";
            Session["TotalDescuentos"] = "";     
            Session["TotalPagar"] = "";
            Session["CostoTotalTrabajador"] = "";
            Session["CodPlanillaModificar"] = "";
        }
        private void GenerarReporteBoletaPagoInvidual(string codPlanilla)
        {
            /*CARGAMOS PRIMER DATASET*/

            System.Data.DataSet DataSetPlanilla = new System.Data.DataSet();

            DataSetPlanilla.Tables.Add(procesoReporte.ObtenerReporteBoletaPagoPlanillaEventuales(Convert.ToInt32(codPlanilla)));

            ReportDataSource datosSolicitante = new ReportDataSource("BoletaEventualesDataSet", DataSetPlanilla.Tables[0]);

            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(datosSolicitante);
            ReportViewer1.LocalReport.Refresh();
        }
        private void CargarProyectoPlanilla()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = procesoCentroCostos.CargarProyectoPlanilla();
                cboProyectoPlanilla.DataSource = dt;
                cboProyectoPlanilla.DataTextField = "DescripcionProyecto";
                cboProyectoPlanilla.DataValueField = "CodProyecto";
                cboProyectoPlanilla.DataBind();
                cboProyectoPlanilla.Items.Insert(0, "Seleccione");
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }

       
    }
}