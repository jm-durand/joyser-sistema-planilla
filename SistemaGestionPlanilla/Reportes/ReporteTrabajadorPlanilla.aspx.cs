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
using System.Drawing;
using System.Configuration;

namespace SistemaGestionPlanilla.Reportes
{
    public partial class ReporteTrabajadorPlanilla : System.Web.UI.Page
    {
        PlanillaNegocio procesoPlanilla = new PlanillaNegocio();
        TrabajadorNegocio procesosTrabajador = new TrabajadorNegocio();
        ReporteNegocio procesoReporte = new ReporteNegocio();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {               
                txtFechaInicial.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtFechaFinal.Text = DateTime.Now.ToString("dd/MM/yyyy");

                txtFechaInicialIndividual.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtFechaFinalIndividual.Text = DateTime.Now.ToString("dd/MM/yyyy");

                gpResultadoPlanillaTrabajador.Visible = false;

                gpReporteRVIndividual.Style.Add("display", "none");
                CargarTipoPlanilla();
            }
        }
        protected void cboTrabajadores_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cboTrabajadores.SelectedValue != "Seleccione")
                {
                    string fechaInicial = txtFechaInicialIndividual.Text.Trim();
                    string fechaFinal = txtFechaFinalIndividual.Text.Trim();
                    string codTrabajador = cboTrabajadores.SelectedValue.ToString();
                    string codTipoPlanilla = cboTipoPlanillaReporteIndividual.SelectedValue.ToString();

                    PlanillasTrabajador(fechaInicial, fechaFinal, codTrabajador, codTipoPlanilla);

                    gpResultadoPlanillaTrabajador.Visible = true;
                }

                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "MantenerPillIndividual", "MantenerPillIndividual();", true);
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
                if (e.CommandName == "GenerarBoleta")
                {
                    string[] Parametros = e.CommandArgument.ToString().Split(',');
                    string codPlanilla = Parametros[0].ToString().Trim();
                    string codTipoPlanilla = Parametros[1].ToString().Trim();
               
                    if (codTipoPlanilla == "1")
                    {
                        GenerarReporteBoletaPagoInvidualPlanillaContruccionCivil(codPlanilla);
                    }
                    else if (codTipoPlanilla == "2")
                    {
                        GenerarReporteBoletaPagoInvidualPlanillaEventuales(codPlanilla);
                    }
                    else if (codTipoPlanilla == "3")
                    {
                        GenerarReporteBoletaPagoInvidualPlanillaEmpleados(codPlanilla);
                    }
                    else if (codTipoPlanilla == "4")
                    {
                        GenerarReporteReciboInvidualPlanillaDestajo(codPlanilla);
                    }                        
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }
        protected void btnGenerarBoletaRangoFechas_ServerClick(object sender, EventArgs e)
        {
            try
            {
                int contadoVacio = 0;
                string fechaInicial = txtFechaInicial.Text.Trim();
                string fechaFinal = txtFechaFinal.Text.Trim();
                string codTipoPlanilla = cboTipoPlanillaRangoFechas.SelectedValue.ToString();

                if (fechaInicial == string.Empty) { contadoVacio += 1; txtFechaInicial.BorderWidth = 1; txtFechaInicial.BorderStyle = BorderStyle.Solid; txtFechaInicial.BorderColor = Color.Red; } else { txtFechaInicial.BorderColor = ColorTranslator.FromHtml("#ebedf2"); }
                if (fechaFinal == string.Empty) { contadoVacio += 1; txtFechaFinal.BorderWidth = 1; txtFechaFinal.BorderStyle = BorderStyle.Solid; txtFechaFinal.BorderColor = Color.Red; } else { txtFechaFinal.BorderColor = ColorTranslator.FromHtml("#ebedf2"); }
                if (codTipoPlanilla == "Seleccione") { contadoVacio += 1; lblTipoPlanillaRangoFechas.ForeColor = Color.Red; } else { lblTipoPlanillaRangoFechas.ForeColor = ColorTranslator.FromHtml("#ebedf2"); }

                if (contadoVacio == 0)
                {
                    if (codTipoPlanilla == "1")
                    {
                        GenerarReporteBoletaPagoMasivo(fechaInicial, fechaFinal);
                    }
                    else if (codTipoPlanilla == "2")
                    {
                        GenerarReporteBoletaPagoMasivoEventuales(fechaInicial, fechaFinal);
                    }
                    else if (codTipoPlanilla == "3")
                    {
                        GenerarReporteBoletaPagoMasivoEmpleados(fechaInicial, fechaFinal);
                    }
                    else if (codTipoPlanilla == "4")
                    {
                        GenerarReporteBoletaPagoMasivoDestajo(fechaInicial, fechaFinal);
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
            gpReporteRVIndividual.Style.Add("display", "none");

            cboTrabajadores.ClearSelection();

            gpResultadoPlanillaTrabajador.Visible = false;
        }
        protected void cboTipoPlanillaReporteIndividual_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string codTipoPlanilla = cboTipoPlanillaReporteIndividual.SelectedValue.ToString();

                if (codTipoPlanilla != "Seleccione")
                {
                    CargarListadoTrabajadores(codTipoPlanilla);
                }
                else
                {
                    DataTable dt = new DataTable();
                    cboTrabajadores.DataSource = dt;
                    cboTrabajadores.DataBind();

                    gpResultadoPlanillaTrabajador.Visible = false;
                }           

                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "MantenerPillIndividual", "MantenerPillIndividual();", true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }
        /*---------------------------------------*/
        /*---------------METODOS-----------------*/
        /*---------------------------------------*/

        private void CargarListadoTrabajadores(string codTipoPlanilla)
        {
            try
            {
                DataTable dt = procesosTrabajador.ObtenerListaTrabajadoresReporteIndividual(Convert.ToInt32(codTipoPlanilla));
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
        private void PlanillasTrabajador(string fechaInicial, string fechaFinal, string codTrabajador, string codTipoPlanilla)
        {
            DataTable dt = procesoReporte.ObtenerPlanillasTrabajadorReporteIndividual(DateTime.ParseExact(fechaInicial, "dd/MM/yyyy", null), DateTime.ParseExact(fechaFinal, "dd/MM/yyyy", null), Convert.ToInt32(codTrabajador), Convert.ToInt32(codTipoPlanilla));
            dgvPlanillaRecientes.DataSource = dt;
            dgvPlanillaRecientes.DataBind();

            lblResultadoBusqueda.InnerHtml = "" + dt.Rows.Count.ToString() + " registros(s) encontrado(s)";
        }
        private void GenerarReporteBoletaPagoInvidualPlanillaContruccionCivil(string codPlanilla)
        {
            gpConsultaReporte.Style.Add("display", "none");
            gpReporteRVIndividual.Style.Add("display", "normal");

            /*CARGAMOS PRIMER DATASET*/

            System.Data.DataSet DataSetPlanilla = new System.Data.DataSet();

            DataSetPlanilla.Tables.Add(procesoReporte.ObtenerReporteBoletaPagoIndividual(Convert.ToInt32(codPlanilla)));

            ReportDataSource datosSolicitante = new ReportDataSource("PlanillaBoletaPagoDataSet", DataSetPlanilla.Tables[0]);

            ReportViewer2.LocalReport.ReportPath = HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["AssetsUrl"] + "/Reportes/RViewer/BoletaPagoIndividaul.rdlc");
            
            ReportViewer2.LocalReport.DataSources.Clear();
            ReportViewer2.LocalReport.DataSources.Add(datosSolicitante);
            ReportViewer2.LocalReport.Refresh();
        }
        private void GenerarReporteBoletaPagoInvidualPlanillaEventuales(string codPlanilla)
        {
            gpConsultaReporte.Style.Add("display", "none");
            gpReporteRVIndividual.Style.Add("display", "normal");

            /*CARGAMOS PRIMER DATASET*/

            System.Data.DataSet DataSetPlanilla = new System.Data.DataSet();

            DataSetPlanilla.Tables.Add(procesoReporte.ObtenerReporteBoletaPagoPlanillaEventuales(Convert.ToInt32(codPlanilla)));

            ReportDataSource datosSolicitante = new ReportDataSource("BoletaEventualesDataSet", DataSetPlanilla.Tables[0]);

            ReportViewer2.LocalReport.ReportPath = HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["AssetsUrl"] + "/Reportes/RViewer/BoletaPagoEventuales.rdlc");

            ReportViewer2.LocalReport.DataSources.Clear();
            ReportViewer2.LocalReport.DataSources.Add(datosSolicitante);
            ReportViewer2.LocalReport.Refresh();
        }
        private void GenerarReporteBoletaPagoInvidualPlanillaEmpleados(string codPlanilla)
        {
            gpConsultaReporte.Style.Add("display", "none");
            gpReporteRVIndividual.Style.Add("display", "normal");

            /*CARGAMOS PRIMER DATASET*/

            System.Data.DataSet DataSetPlanilla = new System.Data.DataSet();

            DataSetPlanilla.Tables.Add(procesoReporte.ObtenerReporteBoletaPagoIndividualEmpleados(Convert.ToInt32(codPlanilla)));

            ReportDataSource datosSolicitante = new ReportDataSource("PlanillaBoletaPagoEmpleadosDataSet", DataSetPlanilla.Tables[0]);

            ReportViewer2.LocalReport.ReportPath = HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["AssetsUrl"] + "/Reportes/RViewer/BoletaPagoEmpleados.rdlc");

            ReportViewer2.LocalReport.DataSources.Clear();
            ReportViewer2.LocalReport.DataSources.Add(datosSolicitante);
            ReportViewer2.LocalReport.Refresh();
        }
        private void GenerarReporteReciboInvidualPlanillaDestajo(string codPlanilla)
        {
            gpConsultaReporte.Style.Add("display", "none");
            gpReporteRVIndividual.Style.Add("display", "normal");

            /*CARGAMOS PRIMER DATASET*/

            System.Data.DataSet DataSetPlanilla = new System.Data.DataSet();

            DataSetPlanilla.Tables.Add(procesoReporte.ObtenerReporteReciboEventualIndividual(Convert.ToInt32(codPlanilla)));

            ReportDataSource datosSolicitante = new ReportDataSource("ReciboCajaEventualDataSet", DataSetPlanilla.Tables[0]);

            ReportViewer2.LocalReport.ReportPath = HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["AssetsUrl"] + "/Reportes/RViewer/ReciboCajaEventual.rdlc");

            ReportViewer2.LocalReport.DataSources.Clear();
            ReportViewer2.LocalReport.DataSources.Add(datosSolicitante);
            ReportViewer2.LocalReport.Refresh();
        }
        private void GenerarReporteBoletaPagoMasivo(string fechaInicial, string fechaFinal)
        {
            gpConsultaReporte.Style.Add("display", "none");
            gpReporteRVIndividual.Style.Add("display", "normal");

            /*CARGAMOS PRIMER DATASET*/

            System.Data.DataSet DataSetPlanilla = new System.Data.DataSet();

            DataSetPlanilla.Tables.Add(procesoReporte.ObtenerReporteBoletaPagoMasivo(DateTime.ParseExact(fechaInicial, "dd/MM/yyyy", null), DateTime.ParseExact(fechaFinal, "dd/MM/yyyy", null)));

            ReportDataSource datosSolicitante = new ReportDataSource("PlanillaBoletaPagoDataSet", DataSetPlanilla.Tables[0]);

            ReportViewer2.LocalReport.ReportPath = HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["AssetsUrl"] + "/Reportes/RViewer/BoletaPagoIndividaul.rdlc");

            ReportViewer2.LocalReport.DataSources.Clear();
            ReportViewer2.LocalReport.DataSources.Add(datosSolicitante);
            ReportViewer2.LocalReport.Refresh();
        }
        private void GenerarReporteBoletaPagoMasivoEventuales(string fechaInicial, string fechaFinal)
        {
            gpConsultaReporte.Style.Add("display", "none");
            gpReporteRVIndividual.Style.Add("display", "normal");

            /*CARGAMOS PRIMER DATASET*/

            System.Data.DataSet DataSetPlanilla = new System.Data.DataSet();

            DataSetPlanilla.Tables.Add(procesoReporte.ObtenerReporteBoletaPagoMasivoEventuales(DateTime.ParseExact(fechaInicial, "dd/MM/yyyy", null), DateTime.ParseExact(fechaFinal, "dd/MM/yyyy", null)));

            ReportDataSource datosSolicitante = new ReportDataSource("BoletaEventualesDataSet", DataSetPlanilla.Tables[0]);

            ReportViewer2.LocalReport.ReportPath = HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["AssetsUrl"] + "/Reportes/RViewer/BoletaPagoEventuales.rdlc");

            ReportViewer2.LocalReport.DataSources.Clear();
            ReportViewer2.LocalReport.DataSources.Add(datosSolicitante);
            ReportViewer2.LocalReport.Refresh();
        }
        private void GenerarReporteBoletaPagoMasivoDestajo(string fechaInicial, string fechaFinal)
        {
            gpConsultaReporte.Style.Add("display", "none");
            gpReporteRVIndividual.Style.Add("display", "normal");

            /*CARGAMOS PRIMER DATASET*/

            System.Data.DataSet DataSetPlanilla = new System.Data.DataSet();

            DataSetPlanilla.Tables.Add(procesoReporte.ObtenerReporteBoletaPagoMasivoDestajo(DateTime.ParseExact(fechaInicial, "dd/MM/yyyy", null), DateTime.ParseExact(fechaFinal, "dd/MM/yyyy", null)));

            ReportDataSource datosSolicitante = new ReportDataSource("ReciboCajaEventualDataSet", DataSetPlanilla.Tables[0]);

            ReportViewer2.LocalReport.ReportPath = HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["AssetsUrl"] + "/Reportes/RViewer/ReciboCajaEventual.rdlc");

            ReportViewer2.LocalReport.DataSources.Clear();
            ReportViewer2.LocalReport.DataSources.Add(datosSolicitante);
            ReportViewer2.LocalReport.Refresh();
        }
        private void GenerarReporteBoletaPagoMasivoEmpleados(string fechaInicial, string fechaFinal)
        {
            gpConsultaReporte.Style.Add("display", "none");
            gpReporteRVIndividual.Style.Add("display", "normal");

            /*CARGAMOS PRIMER DATASET*/

            System.Data.DataSet DataSetPlanilla = new System.Data.DataSet();

            DataSetPlanilla.Tables.Add(procesoReporte.ObtenerReporteBoletaPagoMasivoEmpleados(DateTime.ParseExact(fechaInicial, "dd/MM/yyyy", null), DateTime.ParseExact(fechaFinal, "dd/MM/yyyy", null)));

            ReportDataSource datosSolicitante = new ReportDataSource("PlanillaBoletaPagoEmpleadosDataSet", DataSetPlanilla.Tables[0]);

            ReportViewer2.LocalReport.ReportPath = HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["AssetsUrl"] + "/Reportes/RViewer/BoletaPagoEmpleados.rdlc");

            ReportViewer2.LocalReport.DataSources.Clear();
            ReportViewer2.LocalReport.DataSources.Add(datosSolicitante);
            ReportViewer2.LocalReport.Refresh();
        }
        private void CargarTipoPlanilla()
        {
            try
            {
                DataTable dt = procesoPlanilla.CargarTipoPlanilla();
                cboTipoPlanillaRangoFechas.DataSource = dt;
                cboTipoPlanillaRangoFechas.DataTextField = "DescPlanilla";
                cboTipoPlanillaRangoFechas.DataValueField = "CodTipoPlanilla";
                cboTipoPlanillaRangoFechas.DataBind();
                cboTipoPlanillaRangoFechas.Items.Insert(0, "Seleccione");

                cboTipoPlanillaReporteIndividual.DataSource = dt;
                cboTipoPlanillaReporteIndividual.DataTextField = "DescPlanilla";
                cboTipoPlanillaReporteIndividual.DataValueField = "CodTipoPlanilla";
                cboTipoPlanillaReporteIndividual.DataBind();
                cboTipoPlanillaReporteIndividual.Items.Insert(0, "Seleccione");
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }

      
    }
}