using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogicaNegocio;
using Microsoft.Reporting.WebForms;

namespace SistemaGestionPlanilla.Reportes
{
    public partial class ReporteListadoPlanilla : System.Web.UI.Page
    {
        ReporteNegocio procesoReportes = new ReporteNegocio();
        PlanillaNegocio procesoPlanilla = new PlanillaNegocio();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {                
                txtFechaInicial.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtFechaFinal.Text = DateTime.Now.ToString("dd/MM/yyyy");

                gpReporteRV.Style.Add("display", "none");
                CargarTipoPlanilla();
            }
        }   
        protected void btnGenerarReportePeriodoAnoMes_ServerClick(object sender, EventArgs e)
        {
            try
            {
                int contadoVacio = 0;
                string anoPeriodo = txtFechaAnoPeriodoSelect.Text.Trim();
                string mesPeriodo = cboMesPeriodo.SelectedValue.Trim();
                string codTipoPlanilla = cboTipoPlanillaPeriodo.SelectedValue.ToString();
                string tipoBusqueda = "1";

                if (anoPeriodo == string.Empty) { contadoVacio += 1; txtFechaAnoPeriodo.BorderWidth = 1; txtFechaAnoPeriodo.BorderStyle = BorderStyle.Solid; txtFechaAnoPeriodo.BorderColor = Color.Red; } else { txtFechaAnoPeriodo.BorderColor = ColorTranslator.FromHtml("#ebedf2"); }
                if (mesPeriodo == "0") { contadoVacio += 1; cboMesPeriodo.BorderWidth = 1; cboMesPeriodo.BorderStyle = BorderStyle.Solid; cboMesPeriodo.BorderColor = Color.Red; } else { cboMesPeriodo.BorderColor = ColorTranslator.FromHtml("#ebedf2"); }
                if (codTipoPlanilla == "Seleccione") { contadoVacio += 1; lblTipoPlanillaPeriodo.BorderWidth = 1; lblTipoPlanillaPeriodo.BorderStyle = BorderStyle.Solid; lblTipoPlanillaPeriodo.BorderColor = Color.Red; } else { lblTipoPlanillaPeriodo.BorderColor = ColorTranslator.FromHtml("#ebedf2"); }

                if (contadoVacio == 0)
                {
                    
                    if (codTipoPlanilla == "1")
                    {
                        GenerarReportePlanilla(txtFechaInicial.Text, txtFechaFinal.Text, cboMesPeriodo.SelectedValue.ToString(), txtFechaAnoPeriodoSelect.Text, codTipoPlanilla, tipoBusqueda);
                    }
                    else if (codTipoPlanilla == "2")
                    {
                        GenerarReportePlanillaEventuales(txtFechaInicial.Text, txtFechaFinal.Text, cboMesPeriodo.SelectedValue.ToString(), txtFechaAnoPeriodoSelect.Text, tipoBusqueda);
                    }
                    else if (codTipoPlanilla == "3")
                    {
                        GenerarReportePlanillaEmpleados(txtFechaInicial.Text, txtFechaFinal.Text, cboMesPeriodo.SelectedValue.ToString(), txtFechaAnoPeriodoSelect.Text, tipoBusqueda);
                    }
                    else if (codTipoPlanilla == "4")
                    {
                        GenerarReportePlanillaDestajo(txtFechaInicial.Text, txtFechaFinal.Text, cboMesPeriodo.SelectedValue.ToString(), txtFechaAnoPeriodoSelect.Text, tipoBusqueda);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('Debe completar los campos marcados con rojo','info');", true);
                }

                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "PillAnoMesPeriodo", "MantenerPillAnoMes();", true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }
        protected void btnGenerarReporteRangoFechas_ServerClick(object sender, EventArgs e)
        {
            try
            {
                int contadoVacio = 0;
                string fechaInicial = txtFechaInicial.Text.Trim();
                string fechaFinal = txtFechaFinal.Text.Trim();
                string codTipoPlanilla = cboTipoPlanillaRangoFechas.SelectedValue.ToString();
                string tipoBusqueda = "2";

                if (fechaInicial == string.Empty) { contadoVacio += 1; txtFechaInicial.BorderWidth = 1; txtFechaInicial.BorderStyle = BorderStyle.Solid; txtFechaInicial.BorderColor = Color.Red; } else { txtFechaInicial.BorderColor = ColorTranslator.FromHtml("#ebedf2"); }
                if (fechaFinal == string.Empty) { contadoVacio += 1; txtFechaFinal.BorderWidth = 1; txtFechaFinal.BorderStyle = BorderStyle.Solid; txtFechaFinal.BorderColor = Color.Red; } else { txtFechaFinal.BorderColor = ColorTranslator.FromHtml("#ebedf2"); }
                if (codTipoPlanilla == "Seleccione") { contadoVacio += 1; lblTipoPlanillaRangoFechas.ForeColor = Color.Red; } else { lblTipoPlanillaRangoFechas.ForeColor = ColorTranslator.FromHtml("#ebedf2"); }

                if (contadoVacio == 0)
                {
                    if (codTipoPlanilla == "1")
                    {
                        GenerarReportePlanilla(fechaInicial, fechaFinal, cboMesPeriodo.SelectedValue.ToString(), txtFechaAnoPeriodoSelect.Text, codTipoPlanilla, tipoBusqueda);
                    }
                    else if (codTipoPlanilla == "2")
                    {
                        GenerarReportePlanillaEventuales(fechaInicial, fechaFinal, cboMesPeriodo.SelectedValue.ToString(), txtFechaAnoPeriodoSelect.Text, tipoBusqueda);
                    }
                    else if (codTipoPlanilla == "3")
                    {
                        GenerarReportePlanillaEmpleados(fechaInicial, fechaFinal, cboMesPeriodo.SelectedValue.ToString(), txtFechaAnoPeriodoSelect.Text, tipoBusqueda);
                    }
                    else if (codTipoPlanilla == "4")
                    {
                        GenerarReportePlanillaDestajo(fechaInicial, fechaFinal, cboMesPeriodo.SelectedValue.ToString(), txtFechaAnoPeriodoSelect.Text, tipoBusqueda);
                    }
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

        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            gpConsultaReporte.Style.Add("display", "normal");
            gpReporteRV.Style.Add("display", "none");

            cboMesPeriodo.ClearSelection();
            txtFechaAnoPeriodo.Text = string.Empty;
            txtFechaAnoPeriodoSelect.Text = string.Empty;
    
        }

        /*---------------------------------------*/
        /*---------------METODOS-----------------*/
        /*---------------------------------------*/

        private void GenerarReportePlanilla(string fechaInicial, string fechaFinal, string mesPeriodo ,string anoPeriodo, string codTipoPlanilla , string codTipoBusqueda)
        {
            gpConsultaReporte.Style.Add("display", "none");
            gpReporteRV.Style.Add("display", "normal");

            /*CARGAMOS PRIMER DATASET*/

            System.Data.DataSet DataSetPlanilla = new System.Data.DataSet();
   
            DataSetPlanilla.Tables.Add(procesoReportes.GenerarReportePlanilla(fechaInicial, fechaFinal, mesPeriodo, anoPeriodo, Convert.ToInt32(codTipoPlanilla), Convert.ToInt32(codTipoBusqueda)));

            ReportDataSource datosSolicitante = new ReportDataSource("PlanillaReporteDataSet", DataSetPlanilla.Tables[0]);

            ReportViewer1.LocalReport.ReportPath = HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["AssetsUrl"] + "/Reportes/RViewer/PlanillaConstruccion.rdlc");

            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(datosSolicitante);
            ReportViewer1.LocalReport.Refresh();      
        }
        private void GenerarReportePlanillaEventuales(string fechaInicial, string fechaFinal, string mesPeriodo, string anoPeriodo, string codTipoBusqueda)
        {
            gpConsultaReporte.Style.Add("display", "none");
            gpReporteRV.Style.Add("display", "normal");

            /*CARGAMOS PRIMER DATASET*/

            System.Data.DataSet DataSetPlanilla = new System.Data.DataSet();

            DataSetPlanilla.Tables.Add(procesoReportes.GenerarReportePlanillaEventuales(fechaInicial, fechaFinal, mesPeriodo, anoPeriodo, Convert.ToInt32(codTipoBusqueda)));

            ReportDataSource datosSolicitante = new ReportDataSource("PlanillaEventualesReporteDataSet", DataSetPlanilla.Tables[0]);

            ReportViewer1.LocalReport.ReportPath = HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["AssetsUrl"] + "/Reportes/RViewer/PlanillaConstruccionEventuales.rdlc");

            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(datosSolicitante);
            ReportViewer1.LocalReport.Refresh();
        }
        private void GenerarReportePlanillaEmpleados(string fechaInicial, string fechaFinal, string mesPeriodo, string anoPeriodo, string codTipoBusqueda)
        {
            gpConsultaReporte.Style.Add("display", "none");
            gpReporteRV.Style.Add("display", "normal");

            /*CARGAMOS PRIMER DATASET*/

            System.Data.DataSet DataSetPlanilla = new System.Data.DataSet();

            DataSetPlanilla.Tables.Add(procesoReportes.GenerarReportePlanillaEmpleados(fechaInicial, fechaFinal, mesPeriodo, anoPeriodo, Convert.ToInt32(codTipoBusqueda)));

            ReportDataSource datosSolicitante = new ReportDataSource("PlanillaEmpleadosReporteDataSet", DataSetPlanilla.Tables[0]);

            ReportViewer1.LocalReport.ReportPath = HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["AssetsUrl"] + "/Reportes/RViewer/PlanillaConstruccionEmpleados.rdlc");

            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(datosSolicitante);
            ReportViewer1.LocalReport.Refresh();
        }
        private void GenerarReportePlanillaDestajo(string fechaInicial, string fechaFinal, string mesPeriodo, string anoPeriodo, string codTipoBusqueda)
        {
            gpConsultaReporte.Style.Add("display", "none");
            gpReporteRV.Style.Add("display", "normal");

            /*CARGAMOS PRIMER DATASET*/

            System.Data.DataSet DataSetPlanilla = new System.Data.DataSet();

            DataSetPlanilla.Tables.Add(procesoReportes.GenerarReportePlanillaDestajo(fechaInicial, fechaFinal, mesPeriodo, anoPeriodo, Convert.ToInt32(codTipoBusqueda)));

            ReportDataSource datosSolicitante = new ReportDataSource("PlanillaDestajoReporteDataSet", DataSetPlanilla.Tables[0]);

            ReportViewer1.LocalReport.ReportPath = HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["AssetsUrl"] + "/Reportes/RViewer/PlanillaDestajo.rdlc");

            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(datosSolicitante);
            ReportViewer1.LocalReport.Refresh();
        }
        private void CargarTipoPlanilla()
        {
            try
            {
                DataTable dt = procesoPlanilla.CargarTipoPlanilla();
                cboTipoPlanillaPeriodo.DataSource = dt;
                cboTipoPlanillaPeriodo.DataTextField = "DescPlanilla";
                cboTipoPlanillaPeriodo.DataValueField = "CodTipoPlanilla";
                cboTipoPlanillaPeriodo.DataBind();
                cboTipoPlanillaPeriodo.Items.Insert(0, "Seleccione");

                cboTipoPlanillaRangoFechas.DataSource = dt;
                cboTipoPlanillaRangoFechas.DataTextField = "DescPlanilla";
                cboTipoPlanillaRangoFechas.DataValueField = "CodTipoPlanilla";
                cboTipoPlanillaRangoFechas.DataBind();
                cboTipoPlanillaRangoFechas.Items.Insert(0, "Seleccione");
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }

    }
}