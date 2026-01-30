using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogicaNegocio;
using Entidad;
using Microsoft.Reporting.WebForms;
using System.Drawing;
using System.Configuration;

namespace SistemaGestionPlanilla.Planilla
{
    public partial class GenerarServicioEventuales : System.Web.UI.Page
    {
        PlanillaNegocio procesoPlanilla = new PlanillaNegocio();
        TrabajadorNegocio procesosTrabajador = new TrabajadorNegocio();
        ReporteNegocio procesoReporte = new ReporteNegocio();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["CodUsuarioInterno"] != null)
                {
                    CargarListadoTrabajadores();
                    CargarLaborTrabajo();
                    CargarServicios();

                    txtFechaInicial.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    txtFechaFinal.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    txtFechaInicialReporte.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    txtFechaFinalReporte.Text = DateTime.Now.ToString("dd/MM/yyyy");

                    gpBoletaPlanilla.Visible = false;
                    gpFormularioTrabajador.Visible = false;
                    gpFormularioParametros.Visible = false;
                    gpBtnGenerarBoleta.Visible = false;


                    Session["CodPlanillaEventualModificar"] = "";
                    Session["TotalPagar"] = "";

                    PlanillasRecientes(txtFechaInicialReporte.Text.Trim(), txtFechaFinalReporte.Text.Trim());

                    gpReporteRVIndividual.Style.Add("display", "none");
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
                    CargarDatosTrabajadorPlanilla(cboTrabajadores.SelectedValue.ToString());
                    ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Rangofecha", "CargarDateRange();", true);

                    gpFormularioParametros.Visible = true;
                    gpBtnGenerarBoleta.Visible = true;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }
        protected void cboServicios_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cboServicios.SelectedValue.ToString() != "Seleccione")
                {
                    CargarDatosServicio(cboServicios.SelectedValue.ToString());
                    txtCantidad.Text = string.Empty;
                    txtCantidad.Focus();

                    ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Rangofecha", "CargarDateRange();", true);
                }
                else
                {
                    txtUnidadMedida.Text = string.Empty;
                    txtCostoUnitario.Text = string.Empty;
                    txtCantidad.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }
        protected void txtCantidad_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtCantidad.Text != string.Empty)
                {
                    ObtenerTotalPagar();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }
        protected void btnMostrarListaTrabajador_Click(object sender, EventArgs e)
        {
            gpFormularioTrabajador.Visible = true;
            gpPlanillaRecientes.Visible = false;
        }
        protected void btnGenerarBoleta_Click(object sender, EventArgs e)
        {
            try
            {
                int intContadorCamposVacios = 0;
                string codTrabajador = cboTrabajadores.SelectedValue;
                string codServicio = cboServicios.SelectedValue;

                if (codTrabajador == "Seleccione") { intContadorCamposVacios += 1; }
                if (codServicio == "Seleccione") { intContadorCamposVacios += 1; }

                if (intContadorCamposVacios == 0)
                {
                    gpFormularioCalculoPlanilla.Visible = false;
                    gpBoletaPlanilla.Visible = true;

                    ObtenerTotalPagar();

                    GenerarBoletaTrabajadorPrevio(cboTrabajadores.SelectedValue.ToString());

                    ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "GotoTop", "GotoTop();", true);

                    ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('Vista de Recibo generado correctamente','info');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('Debe seleccionar un trabajador o servicio para generar el recibo.','info');", true);
                }
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
        protected void btnGuardarPlanilla_Click(object sender, EventArgs e)
        {
            try
            {
                string codTrabajador = cboTrabajadores.SelectedValue.ToString();
                string fechaInicio = txtFechaInicial.Text.Trim();
                string fechaFinal = txtFechaFinal.Text.Trim();
                string codServicio = cboServicios.SelectedValue;
                string cantidad = txtCantidad.Text.Trim();               

                string totalCostoTrabajador = Session["TotalPagar"].ToString();
                string codUsuarioRegistrador = Session["CodUsuarioInterno"].ToString();

                if (totalCostoTrabajador == string.Empty) { totalCostoTrabajador = "0"; }
                if (cantidad == string.Empty) { cantidad = "0"; }
                

                string mensaje="";
                if (Session["CodPlanillaEventualModificar"].ToString() == string.Empty)
                {
                    mensaje = RegistrarPlanillaEventual(codTrabajador, fechaInicio, fechaFinal, codServicio, cantidad, totalCostoTrabajador, codUsuarioRegistrador);
                }
                else
                {
                    mensaje = ModificarPlanillaEventual(codTrabajador, fechaInicio, fechaFinal, codServicio, cantidad, totalCostoTrabajador, codUsuarioRegistrador, Session["CodPlanillaEventualModificar"].ToString());
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
                    gpFormularioPlanillaEventuales.Style.Add("display", "none");
                    gpReporteRVIndividual.Style.Add("display", "normal");
                    GenerarReporteReciboInvidual(codPlanilla);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }
        protected void btnRegresarCalculos_Click(object sender, EventArgs e)
        {
            gpFormularioPlanillaEventuales.Style.Add("display", "normal");
            gpReporteRVIndividual.Style.Add("display", "none");
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
        private string RegistrarPlanillaEventual(string codTrabajador, string fechaInicio, string fechaFin, string codServicio, string cantidad, string totalPagar, string codUsuarioRegistro)
        {
            PlanillaEntidad planillaEntidad = new PlanillaEntidad();
            planillaEntidad.CodTrabajador = Convert.ToInt32(codTrabajador);
            planillaEntidad.FechaInicial = DateTime.ParseExact(fechaInicio, "dd/MM/yyyy", null);
            planillaEntidad.FechaFinal = DateTime.ParseExact(fechaFin, "dd/MM/yyyy", null);
            planillaEntidad.CodServicio = Convert.ToInt32(codServicio);
            planillaEntidad.Cantidad = cantidad;
            planillaEntidad.TotalPagarEventual = double.Parse(totalPagar, CultureInfo.InvariantCulture);
            planillaEntidad.CodUsuarioRegistro = Convert.ToInt32(codUsuarioRegistro);

            return procesoPlanilla.RegistrarPlanillaEventuales(planillaEntidad);
        }
        private string ModificarPlanillaEventual(string codTrabajador, string fechaInicio, string fechaFin, string codServicio, string cantidad, string totalPagar, string codUsuarioRegistro, string codPlanillaEventual)
        {
            PlanillaEntidad planillaEntidad = new PlanillaEntidad();
            planillaEntidad.CodTrabajador = Convert.ToInt32(codTrabajador);
            planillaEntidad.FechaInicial = DateTime.ParseExact(fechaInicio, "dd/MM/yyyy", null);
            planillaEntidad.FechaFinal = DateTime.ParseExact(fechaFin, "dd/MM/yyyy", null);
            planillaEntidad.CodServicio = Convert.ToInt32(codServicio);
            planillaEntidad.Cantidad = cantidad;
            planillaEntidad.TotalPagarEventual = double.Parse(totalPagar, CultureInfo.InvariantCulture);
            planillaEntidad.CodUsuarioModificacion = Convert.ToInt32(codUsuarioRegistro);
            planillaEntidad.CodPlanillaEventual = Convert.ToInt32(codPlanillaEventual);

            return procesoPlanilla.ActualizarPlanillaEventuales(planillaEntidad);
        }
        private void CargarDatosPlanilla(string codPlanilla)
        {
            DataTable dt = procesoPlanilla.ObtenerDatosPlanillaEventuales(Convert.ToInt32(codPlanilla));

            if (dt.Rows.Count > 0)
            {
                LimpiarFomulario();

                gpFormularioCalculoPlanilla.Visible = true;
                gpBoletaPlanilla.Visible = false;

                gpFormularioTrabajador.Visible = true;
                gpFormularioParametros.Visible = true;
                gpBtnGenerarBoleta.Visible = true;

                gpPlanillaRecientes.Visible = false;

                txtFechaInicial.Text = dt.Rows[0][0].ToString();
                txtFechaFinal.Text = dt.Rows[0][1].ToString();
                cboTrabajadores.SelectedValue = dt.Rows[0][2].ToString().Trim();

                cboTrabajadores_SelectedIndexChanged(cboTrabajadores, new EventArgs());

                cboServicios.SelectedValue = dt.Rows[0][3].ToString();

                cboServicios_SelectedIndexChanged(cboServicios, new EventArgs());

                txtCantidad.Text = dt.Rows[0][4].ToString().Replace(",",".");

                txtCantidad_TextChanged(txtCantidad, new EventArgs());

                Session["CodPlanillaEventualModificar"] = codPlanilla;

            }
        }
        private void PlanillasRecientes(string fechaInicial, string fechaFinal)
        {
            DataTable dt = procesoPlanilla.ObtenerPlanillasDestajoRecientes(fechaInicial, fechaFinal);
            dgvPlanillaRecientes.DataSource = dt;
            dgvPlanillaRecientes.DataBind();

            lblResultadoBusqueda.InnerHtml = "" + dt.Rows.Count.ToString() + " registros(s) reciente(s) encontrado(s)";
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
        private void CargarLaborTrabajo()
        {
            try
            {
                DataTable dt = procesosTrabajador.ObtenerLaborTrabajo();
                cboLaborTrabajo.DataSource = dt;
                cboLaborTrabajo.DataTextField = "DescLaborTrabajo";
                cboLaborTrabajo.DataValueField = "CodLaborTrabajo";
                cboLaborTrabajo.DataBind();
                cboLaborTrabajo.Items.Insert(0, "Seleccione");
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }
        private void CargarServicios()
        {
            try
            {
                DataTable dt = procesosTrabajador.ObtenerServicios();
                cboServicios.DataSource = dt;
                cboServicios.DataTextField = "DescripcionServicio";
                cboServicios.DataValueField = "CodServicio";
                cboServicios.DataBind();
                cboServicios.Items.Insert(0, "Seleccione");
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }
        private void CargarDatosTrabajadorPlanilla(string codTrabajador)
        {
            DataTable dt = procesoPlanilla.CargarDatosTrabajadorEventualPlanilla(Convert.ToInt32(codTrabajador));

            cboLaborTrabajo.SelectedValue = dt.Rows[0][1].ToString();
        }
        private void CargarDatosServicio(string codServicio)
        {
            DataTable dt = procesoPlanilla.CargarDatosServicio(Convert.ToInt32(codServicio));

            txtUnidadMedida.Text = dt.Rows[0][1].ToString();
            txtCostoUnitario.Text = dt.Rows[0][2].ToString().Replace(",",".");
        }
        private void ObtenerTotalPagar()
        {
            string cantidad = txtCantidad.Text.Trim();
            string costoUnit = txtCostoUnitario.Text.Trim();

            double totalCantidad = double.Parse(cantidad, CultureInfo.InvariantCulture);
            double totalCostoUnit = double.Parse(costoUnit, CultureInfo.InvariantCulture);

            Session["TotalPagar"] = totalCantidad * totalCostoUnit;
            txtTotalPagar.InnerText = "S/. " + (totalCantidad * totalCostoUnit).ToString("N", CultureInfo.GetCultureInfo("es-PE"));
        }
        private void GenerarBoletaTrabajadorPrevio(string codTrabajador)
        {
            DataTable dt = procesosTrabajador.ObtenerDatosTrabajadorEventualPlanillaPrevio(Convert.ToInt32(codTrabajador));

            string nroBoleta = dt.Rows[0][0].ToString();
            nroBoleta = nroBoleta.PadLeft(nroBoleta.Length + 7, '0');

            lblNroBoleta.Text = nroBoleta;
            lblNombreTrabajadorBoleta.Text = dt.Rows[0][1].ToString();
            lblDocumentoTrabajadorBoleta.Text = dt.Rows[0][2].ToString();
            lblLaborTrabajadorBoleta.Text = dt.Rows[0][3].ToString();
            lblSemanaBoleta.Text = txtFechaInicial.Text + " - " + txtFechaFinal.Text;

            lblServicioRecibo.Text = cboServicios.SelectedItem.ToString();
            lblUnidadMedidaRecibo.Text = txtUnidadMedida.Text;
            lblPrecioUnitarioRecibo.Text = txtCostoUnitario.Text;
            lblCantidadRecibo.Text = txtCantidad.Text;

            lblTotalPagar.Text = Session["TotalPagar"].ToString();
            lblTotalCostoEmpresa.InnerText = "S/. " + Session["TotalPagar"].ToString();

        }
        private void LimpiarFomulario()
        {
            cboTrabajadores.ClearSelection();
            txtFechaInicial.Text = DateTime.Now.ToShortDateString();
            txtFechaFinal.Text = DateTime.Now.ToShortDateString();
            cboServicios.ClearSelection();
            cboLaborTrabajo.ClearSelection();
            txtUnidadMedida.Text = string.Empty;
            txtCostoUnitario.Text = string.Empty;
            txtCantidad.Text = string.Empty;
           
            txtTotalPagar.InnerText = "0.00";

            Session["TotalPagar"] = "";
            Session["CodPlanillaEventualModificar"] = "";
        }
        private void GenerarReporteReciboInvidual(string codPlanilla)
        {
            /*CARGAMOS PRIMER DATASET*/

            System.Data.DataSet DataSetPlanilla = new System.Data.DataSet();

            DataSetPlanilla.Tables.Add(procesoReporte.ObtenerReporteReciboEventualIndividual(Convert.ToInt32(codPlanilla)));

            ReportDataSource datosSolicitante = new ReportDataSource("ReciboCajaEventualDataSet", DataSetPlanilla.Tables[0]);


            ReportViewer2.LocalReport.DataSources.Clear();
            ReportViewer2.LocalReport.DataSources.Add(datosSolicitante);
            ReportViewer2.LocalReport.Refresh();
        }

       
    }
}