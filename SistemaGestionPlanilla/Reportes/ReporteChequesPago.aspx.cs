using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogicaNegocio;
using LogicaNegocio.SistemaGestionPlanilla;

namespace SistemaGestionPlanilla.Reportes
{
    public partial class ReporteChequesPago : System.Web.UI.Page
    {
        ChequesNegocio procesoCheques = new ChequesNegocio();
        CentroCostosNegocio procesoCentroCostos = new CentroCostosNegocio();
        ReporteNegocio procesoReportes = new ReporteNegocio();
        ContratistasNegocio procesoContratistas = new ContratistasNegocio();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["CodUsuarioInterno"] != null)
                {
                    txtFechaInicio.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    txtFechaFin.Text = DateTime.Now.ToString("dd/MM/yyyy");

                    CargarTipoPagoCheque();
                    CargarMedioPago();
                    CargarProyectoPlanilla();
                    CargarEstadoCheque();

                    GenerarReportePagos();

                    gpResultadoReporte.Visible = false;                  
                }
                else
                {
                    Response.Redirect(ConfigurationManager.AppSettings["AssetsUrl"] + "/Seguridad/Logout.aspx");
                }
            }
        }
      
        protected void dgvReporte_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvReporte.PageIndex = e.NewPageIndex;
            GenerarReportePagos();
        }
        protected void btnGenerarReporte_Click(object sender, EventArgs e)
        {
            if (txtFechaInicio.Text != string.Empty || txtFechaFin.Text!=string.Empty)
            {
                GenerarReportePagos();
            }
            else
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('Debe seleccionar el rango de fechas','info');", true);
            }
            
        }

        protected void btnExportarExcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtFechaInicio.Text != string.Empty || txtFechaFin.Text != string.Empty)
                {
                    if (dgvReporte.Rows.Count > 0)
                    {
                        ExportarExcel();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('No hay datos para exportar','info');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('Debe seleccionar el rango de fechas','info');", true);
                }              
            }
            catch (Exception ex)
            {
                string error = ex.Message.ToString().Trim().Replace("\r\n", "").Replace("(", "").Replace(")", "").Replace("'", "");
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + error + "','error');", true);
            }
        }
        //----------------------
        //--METODOS
        //----------------------
        private void GenerarReportePagos()
        {
            try
            {
                DataTable dt = procesoReportes.GenerarReportePagoCheques(cboEstadoCheque.SelectedValue, cboTipoPago.SelectedValue,cboMedioPago.SelectedValue, txtFechaInicio.Text, txtFechaFin.Text, cboProyecto.SelectedValue);

                dgvReporte.DataSource = dt;
                dgvReporte.DataBind();

                gpResultadoReporte.Visible = true;

                lblResultadoBusqueda.InnerHtml = "<h3 style='color: #0047ba;'>" + dt.Rows.Count.ToString() + " resultado(s) encontrado(s)</h3>";
            }
            catch (Exception ex)
            {
                string error = ex.Message.ToString().Trim().Replace("\r\n", "").Replace("(", "").Replace(")", "").Replace("'", "");
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + error + "','error');", true);
            }
        }
        private void ExportarExcel()
        {
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment;filename=ReportePagos.xls");
            Response.ContentType = "application/vnd.xls";

            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                this.GenerarReportePagos();

                dgvReporte.AllowPaging = false;
                dgvReporte.DataBind();

                dgvReporte.HeaderRow.BackColor = Color.White;
                foreach (TableCell cell in dgvReporte.HeaderRow.Cells)
                {
                    cell.BackColor = dgvReporte.HeaderStyle.BackColor;
                }
                foreach (GridViewRow row in dgvReporte.Rows)
                {
                    row.BackColor = Color.White;
                    foreach (TableCell cell in row.Cells)
                    {
                        if (row.RowIndex % 2 == 0)
                        {
                            cell.BackColor = dgvReporte.AlternatingRowStyle.BackColor;
                        }
                        else
                        {
                            cell.BackColor = dgvReporte.RowStyle.BackColor;
                        }
                        cell.CssClass = "textmode";
                    }
                }

                dgvReporte.RenderBeginTag(hw);
                dgvReporte.HeaderRow.RenderControl(hw);
                foreach (GridViewRow row in dgvReporte.Rows)
                {
                    row.RenderControl(hw);
                }
                dgvReporte.FooterRow.RenderControl(hw);
                dgvReporte.RenderEndTag(hw);

                Response.Charset = "UTF-8";
                Response.ContentEncoding = Encoding.Default;
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
        }
        private void CargarTipoPagoCheque()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = procesoCheques.CargarTipoPagoCheque();
                cboTipoPago.DataSource = dt;
                cboTipoPago.DataTextField = "DescripcionTipoPago";
                cboTipoPago.DataValueField = "CodTipoPago";
                cboTipoPago.DataBind();
                cboTipoPago.Items.Insert(0, new ListItem("Todos", "0"));
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }
        private void CargarProyectoPlanilla()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = procesoCentroCostos.CargarProyectoPlanilla();
                cboProyecto.DataSource = dt;
                cboProyecto.DataTextField = "DescripcionProyecto";
                cboProyecto.DataValueField = "CodProyecto";
                cboProyecto.DataBind();
                cboProyecto.Items.Insert(0, new ListItem("Todos", "0"));
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }
        private void CargarEstadoCheque()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = procesoCheques.CargarEstadoCheque();
                cboEstadoCheque.DataSource = dt;
                cboEstadoCheque.DataTextField = "DescEstado";
                cboEstadoCheque.DataValueField = "CodEstado";
                cboEstadoCheque.DataBind();
                cboEstadoCheque.Items.Insert(0, new ListItem("Todos", "0"));
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }
        private void CargarMedioPago()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = procesoContratistas.CargarMediosPagoContratista();
                cboMedioPago.DataSource = dt;
                cboMedioPago.DataTextField = "DescripcionMedioPago";
                cboMedioPago.DataValueField = "CodMedioPago";
                cboMedioPago.DataBind();
                cboMedioPago.Items.Insert(0, new ListItem("Todos", "0"));
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }

    }
}