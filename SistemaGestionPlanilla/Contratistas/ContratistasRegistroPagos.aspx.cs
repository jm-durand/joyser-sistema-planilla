using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogicaNegocio;
using Entidad;
using Microsoft.Reporting.WebForms;
using System.Configuration;

namespace SistemaGestionPlanilla.Contratistas
{
    public partial class ContratistasRegistroPagos : System.Web.UI.Page
    {
        ContratistasNegocio procesoContratistas = new ContratistasNegocio();
        ChequesNegocio procesoCheques = new ChequesNegocio();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["CodUsuarioInterno"] != null)
                {               
                    Session["CodPagoModificar"] = "";
                    Session["MontoTotalContrato"] = "";
                    Session["CodContratoGenerarPago"] = "";
                    CargarContratistas();
                    CargarMedioPago();
                    CargarTipoReciboPago();
                    gpListadoContratos.Visible = false;
                    gpListadoTrabajadores.Visible = false;
                    gpReporteRVIndividual.Style.Add("display", "none");
                    gpContratosContratista.Visible = true;
                    gpFormularioPago.Visible = false;
                }
                else
                {
                    Response.Redirect(ConfigurationManager.AppSettings["AssetsUrl"] + "/Seguridad/Logout.aspx");
                }
            }
        }
        protected void cboContratistas_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if(cboContratistas.SelectedValue!= "Seleccione")
                {
                    string codContratista = cboContratistas.SelectedValue.ToString();
                    string codFiltroPago = cboFiltroPagos.SelectedValue.ToString();

                    ObtenerContratosContratista(codContratista, codFiltroPago);

                    gpListadoTrabajadores.Visible = false;
                }
                else
                {
                    gpListadoContratos.Visible = false;
                }             
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }
        protected void cboFiltroPagos_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cboContratistas.SelectedValue != "Seleccione")
                {
                    string codContratista = cboContratistas.SelectedValue.ToString();
                    string codFiltroPago = cboFiltroPagos.SelectedValue.ToString();

                    ObtenerContratosContratista(codContratista, codFiltroPago);

                    gpListadoContratos.Visible = true;
                }
                else
                {
                    gpListadoContratos.Visible = false;
                }

                gpListadoTrabajadores.Visible = false;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }
        protected void dgvContratosContratista_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    if (e.Row.Cells[6].Text == "1")
                    {
                        e.Row.Cells[6].Text = "<span class='m-badge m-badge--warning m-badge--wide'>Pendiente</span>";
                    }
                    else if (e.Row.Cells[6].Text == "2")
                    {
                        e.Row.Cells[6].Text = "<span class='m-badge m-badge--success m-badge--wide'>Pagado</span>";
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }
        protected void dgvContratosContratista_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string codContrato = e.CommandArgument.ToString();

                if (e.CommandName == "GenerarPagoContratista")
                {
                    Session["CodPagoModificar"] = "";

                    CargarDatosContratoPago(codContrato);              
                }
                else if (e.CommandName == "VerDetallesPago")
                {
                    ObtenerListadoTrabajadores(codContrato);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }
        protected void dgvContratosContratista_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                dgvContratosContratista.PageIndex = e.NewPageIndex;

                if (cboContratistas.SelectedValue != "Seleccione")
                {
                    ObtenerContratosContratista(cboContratistas.SelectedValue.ToString(), cboFiltroPagos.SelectedValue.ToString());
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }
        protected void cboModoPago_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cboModoPago.SelectedValue != "Seleccione")
                {
                    txtNumeroCheque.Text = string.Empty;

                    if (cboModoPago.SelectedValue == "1")
                    {
                        txtNumeroCheque.ReadOnly = false;
                        txtNumeroCheque.Focus();
                    }
                    else
                    {
                        txtNumeroCheque.ReadOnly = true;
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }
        protected void cboTipoPago_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string montoTotalContrato = Session["MontoTotalContrato"].ToString();
                double montoTotalContratoDoub = double.Parse(montoTotalContrato, CultureInfo.InvariantCulture);
                string montoParcialContrato = (montoTotalContratoDoub / 2).ToString();

                if (cboTipoPago.SelectedValue == "0")
                {
                    txtMontoPagar.ReadOnly = true;
                    txtMontoPagar.Text = montoTotalContrato;
                }
                else if (cboTipoPago.SelectedValue == "1")
                {

                    txtMontoPagar.ReadOnly = true;
                    txtMontoPagar.Text = montoParcialContrato;
                }
                else
                {
                    txtMontoPagar.ReadOnly = false;
                    txtMontoPagar.Text = string.Empty;
                    txtMontoPagar.Focus();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }
        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            try
            {
                gpContratosContratista.Visible = true;
                gpFormularioPago.Visible = false;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                int contadorCamposVacios = 0;
                string codContrato = Session["CodContratoGenerarPago"].ToString();
                string codMedioPago = cboModoPago.SelectedValue.ToString();
                string numMedioPago = txtNumeroCheque.Text.ToString().Trim();
                string fechaPago = txtFechaPago.Text.Trim();
                string codTipoReciboPago = cboTipoReciboPago.SelectedValue.ToString();
                string numReciboPago = txtNumRecigoPago.Text.Trim();
                string montoPagar = txtMontoPagar.Text.Trim();

                if (codMedioPago == "Seleccione") { contadorCamposVacios += 1; lblModoPago.ForeColor = Color.Red; } else { lblModoPago.ForeColor = ColorTranslator.FromHtml("#3f4047"); }
                if (codTipoReciboPago == "Seleccione") { contadorCamposVacios += 1; lblTipoReciboPago.ForeColor = Color.Red; } else { lblTipoReciboPago.ForeColor = ColorTranslator.FromHtml("#3f4047"); }
                if (fechaPago == string.Empty) { contadorCamposVacios += 1; lblFechaPago.ForeColor = Color.Red; } else { lblFechaPago.ForeColor = ColorTranslator.FromHtml("#3f4047"); }
                if (numReciboPago == string.Empty) { contadorCamposVacios += 1; lblNumReciboPago.ForeColor = Color.Red; } else { lblNumReciboPago.ForeColor = ColorTranslator.FromHtml("#3f4047"); }
                if (montoPagar == string.Empty) { contadorCamposVacios += 1; lblMontoPagar.ForeColor = Color.Red; } else { lblMontoPagar.ForeColor = ColorTranslator.FromHtml("#3f4047"); }                

                if (contadorCamposVacios == 0)
                {
                    string[] mensaje;

                    if (Session["CodPagoModificar"].ToString() == string.Empty)
                    {
                        mensaje = GuardarPagoContratista(codContrato, double.Parse(montoPagar, CultureInfo.InvariantCulture), codMedioPago, numMedioPago, fechaPago, codTipoReciboPago, numReciboPago, Session["CodUsuarioInterno"].ToString()).Split('|');
                    }
                    else
                    {
                        mensaje = ModificarPagoContratista(double.Parse(montoPagar, CultureInfo.InvariantCulture), codMedioPago, numMedioPago, fechaPago, codTipoReciboPago, numReciboPago, Session["CodPagoModificar"].ToString(), Session["CodUsuarioInterno"].ToString()).Split('|');
                    }

                    if (mensaje[0] == "EXITO")
                    {

                        ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('Pago registrado correctamente.','success');", true);

                        LimpiarFormulario();

                        gpContratosContratista.Visible = true;
                        gpFormularioPago.Visible = false;

                        ObtenerContratosContratista(cboContratistas.SelectedValue.ToString(),"0");

                        ObtenerListadoTrabajadores(mensaje[1]);

                    }
                    else if (mensaje[0] == "MAYORAMONTO")
                    {
                        ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('El monto ingresado supera al monto total del contrato.','info');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + mensaje[0] + "','error');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('Debe llenar los campos marcados con rojo','info');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }
        protected void btnDetallesPagoContrato_Click(object sender, EventArgs e)
        {
            try
            {
                string codPagoContratista = Request.Cookies["CodPagoContratistaDetalles"].Value.ToString();
                CargarDatosContratistaPagoRealizado(codPagoContratista);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }
        protected void btnConfirmarEliminarPagoContratista_Click(object sender, EventArgs e)
        {
            try
            {
                string codPagoContratistaElminar = Request.Cookies["CodPagoContratistaEliminar"].Value.ToString();
                string[] mensaje = EliminarPagoContratista(codPagoContratistaElminar, Session["CodUsuarioInterno"].ToString()).Split('|');

                if (mensaje[0] == "EXITO")
                {
                    ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('Pago eliminado correctamente.','info');", true);

                    ObtenerContratosContratista(cboContratistas.SelectedValue.ToString(), "0");
                    ObtenerListadoTrabajadores(mensaje[1]);

                    ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "backdrop", "DesaparecerBackDrop();", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + mensaje[0] + "','error');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }
        protected void btnRegresarImpresion_Click(object sender, EventArgs e)
        {
            try
            {
                gpContratosContratista.Visible = true;
                gpFormularioPago.Visible = false;
                gpListadoContratos.Visible = true;
                gpListadoTrabajadores.Visible = true;
                gpReporteRVIndividual.Style.Add("display", "none");
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }
        protected void btnImprimirCheque_Click(object sender, EventArgs e)
        {
            try
            {
                string codChequeImprimir = Request.Cookies["CodChequeImprimir"].Value.ToString();
                GenerarReporteChequeInvidual(codChequeImprimir);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }
        /*---------------------------------------*/
        /*---------------METODOS-----------------*/
        /*---------------------------------------*/
        private void ObtenerContratosContratista(string codContratista, string codEstadoPago)
        {
            gpListadoContratos.Visible = true;

            DataTable dt = procesoContratistas.ObtenerContratosContratista(Convert.ToInt32(codContratista),Convert.ToInt32(codEstadoPago));
            dgvContratosContratista.DataSource = dt;
            dgvContratosContratista.DataBind();
            lblResultado.InnerHtml = "" + dt.Rows.Count.ToString() + " registros(s) encontrado(s)";
        }
        private string GuardarPagoContratista(string codContrato, double montoPagar, string codMedioPago, string numMedioPago, string fechaPago, string codTipoReciboPago, string numReciboPago, string codUsuario)
        {
            ContratistasEntidad contratistasEntidad = new ContratistasEntidad();
            contratistasEntidad.CodContrato = Convert.ToInt32(codContrato);
            contratistasEntidad.MontoPagar = montoPagar;
            contratistasEntidad.CodMedioPago = Convert.ToInt32(codMedioPago);
            contratistasEntidad.NumMedioPago = numMedioPago;
            contratistasEntidad.FechaPago = fechaPago;
            contratistasEntidad.CodTipoReciboPago = Convert.ToInt32(codTipoReciboPago);
            contratistasEntidad.NumReciboPago = numReciboPago;
            contratistasEntidad.CodUsuarioRegistro = Convert.ToInt32(codUsuario);
            return procesoContratistas.RegistrarPagoContratistas(contratistasEntidad);
        }
        private string ModificarPagoContratista(double montoPagar, string codMedioPago, string numMedioPago, string fechaPago, string codTipoReciboPago, string numReciboPago, string codPagoContratista, string codUsuario)
        {
            ContratistasEntidad contratistasEntidad = new ContratistasEntidad();
            contratistasEntidad.MontoPagar = montoPagar;
            contratistasEntidad.CodMedioPago = Convert.ToInt32(codMedioPago);
            contratistasEntidad.NumMedioPago = numMedioPago;
            contratistasEntidad.FechaPago = fechaPago;
            contratistasEntidad.CodTipoReciboPago = Convert.ToInt32(codTipoReciboPago);
            contratistasEntidad.NumReciboPago = numReciboPago;
            contratistasEntidad.CodPagoContratista = Convert.ToInt32(codPagoContratista);
            contratistasEntidad.CodUsuarioModifica = Convert.ToInt32(codUsuario);
            return procesoContratistas.ModificatPagoContratistas(contratistasEntidad);
        }
        private string EliminarPagoContratista(string codPagoContratista, string codUsuario)
        {
            ContratistasEntidad contratistasEntidad = new ContratistasEntidad();         
            contratistasEntidad.CodPagoContratista = Convert.ToInt32(codPagoContratista);
            contratistasEntidad.CodUsuarioModifica = Convert.ToInt32(codUsuario);
            return procesoContratistas.EliminarPagoContratistas(contratistasEntidad);
        }
        private void CargarDatosContratoPago(string codContrato)
        {
            LimpiarFormulario();

            DataTable dt = procesoContratistas.ObtenerDatosContratoPago(Convert.ToInt32(codContrato));

            if (dt.Rows.Count > 0)
            {
                btnGuardar.Text = "<i class='la la-floppy-o'></i> Guardar";

                txtContratistaDetalle.Text = dt.Rows[0][0].ToString();
                txtDescripcionContrato.Text = dt.Rows[0][1].ToString();
                txtMontoPagar.Text = dt.Rows[0][2].ToString().Replace(",", ".");
              
                Session["MontoTotalContrato"] = dt.Rows[0][2].ToString().Replace(",", ".");

                Session["CodContratoGenerarPago"] = codContrato;
               
                gpContratosContratista.Visible = false;
                gpFormularioPago.Visible = true;
            }
        }
        private void CargarDatosContratistaPagoRealizado(string codPagoContratista)
        {
            LimpiarFormulario();

            DataTable dt = procesoContratistas.ObtenerDatosPagoContratista(Convert.ToInt32(codPagoContratista));

            if (dt.Rows.Count > 0)
            {
                btnGuardar.Text = "<i class='la la-floppy-o'></i> Modificar";

                txtContratistaDetalle.Text = dt.Rows[0][0].ToString();
                txtDescripcionContrato.Text = dt.Rows[0][1].ToString();
                //txtMontoPagar.Text = dt.Rows[0][2].ToString().Replace(",", ".");
                cboModoPago.SelectedValue = dt.Rows[0][3].ToString();
                txtNumeroCheque.Text = dt.Rows[0][4].ToString();
                txtFechaPago.Text = dt.Rows[0][5].ToString();
                cboTipoReciboPago.SelectedValue = dt.Rows[0][6].ToString();
                txtNumRecigoPago.Text = dt.Rows[0][7].ToString();
                cboTipoPago.SelectedValue = dt.Rows[0][8].ToString();
                txtMontoPagar.Text = dt.Rows[0][9].ToString().Replace(",", "."); ;

                Session["MontoTotalContrato"] = dt.Rows[0][2].ToString().Replace(",", ".");

                Session["CodPagoModificar"] = codPagoContratista;

                if (dt.Rows[0][8].ToString() == "2")
                {
                    txtMontoPagar.ReadOnly = false;
                }
                else
                {
                    txtMontoPagar.ReadOnly = true;
                }

                if (dt.Rows[0][3].ToString() == "1")
                {
                    txtMontoPagar.ReadOnly = true;                    
                }
                else
                {
                    txtMontoPagar.ReadOnly = false;                    
                }
                
                gpContratosContratista.Visible = false;
                gpFormularioPago.Visible = true;
            }
        }
        private void CargarContratistas()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = procesoContratistas.CargarContratistas();
                cboContratistas.DataSource = dt;
                cboContratistas.DataTextField = "DescripcionProveedor";
                cboContratistas.DataValueField = "CodProveedor";
                cboContratistas.DataBind();
                cboContratistas.Items.Insert(0, "Seleccione");
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
                cboModoPago.DataSource = dt;
                cboModoPago.DataTextField = "DescripcionMedioPago";
                cboModoPago.DataValueField = "CodMedioPago";
                cboModoPago.DataBind();
                cboModoPago.Items.Insert(0, "Seleccione");
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }
        private void CargarTipoReciboPago()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = procesoContratistas.CargarTipoRecibosPago();
                cboTipoReciboPago.DataSource = dt;
                cboTipoReciboPago.DataTextField = "DescripcionReciboPago";
                cboTipoReciboPago.DataValueField = "CodReciboPago";
                cboTipoReciboPago.DataBind();
                cboTipoReciboPago.Items.Insert(0, "Seleccione");
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }
        private void ObtenerListadoTrabajadores(string codContrato)
        {
            gpListadoTrabajadores.Visible = true;

            DataTable datos = procesoContratistas.ObtenerDatosContratoPago(Convert.ToInt32(codContrato));
            lblContratoListado.InnerHtml = datos.Rows[0][1].ToString();

            DataTable dt = procesoContratistas.ObtenerPagosRealizados(Convert.ToInt32(codContrato));
            string html = "<div class='ibox'><div class='ibox-content top-fixed'><div class='table-responsive'><table class='table table-striped- table-bordered table-hover table-checkable' id='m_table_1'><thead><tr><th>#</th><th>Contratista</th><th>Medio Pago</th><th>Fecha Pago</th><th>Recibo Pago</th><th>Total Pagado</th><th>Opcion</th></tr></thead>";
            try
            {
                html += "<tbody>";
                if (dt.Rows.Count > 0)
                {
                    int contadorfilas = 1;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string codigoPagoContratista = dt.Rows[i][0].ToString();
                        string contratista = dt.Rows[i][1].ToString();
                        string ruc = dt.Rows[i][2].ToString();
                        string medioPago = dt.Rows[i][3].ToString();
                        string numMedioPago = dt.Rows[i][4].ToString();
                        string fechaPago = dt.Rows[i][5].ToString();
                        string reciboPago = dt.Rows[i][6].ToString();
                        string numReciboPago = dt.Rows[i][7].ToString();
                        string totalPagado = dt.Rows[i][8].ToString();
                        string codCheque = dt.Rows[i][10].ToString();

                        html += "<tr id='" + contadorfilas + "'><td style='height:44px;'><div style='width:100%; height:100%; overflow-y: auto;'>" + contadorfilas + "</div></td>";
                        html += "<td><div class='m-card-user m-card-user--sm'><div class='m-card-user__details'> <span class='m-card-user__name'>" + contratista + "</span> <a href='#' class='m-card-user__email m-link'>RUC: " + ruc + "</a> </div></div></td>";
                        html += "<td><div class='m-card-user m-card-user--sm'><div class='m-card-user__details'> <span class='m-card-user__name'>" + medioPago + "</span> <a href='#' class='m-card-user__email m-link'>Nro: " + numMedioPago + "</a> </div></div></td>";
                        html += "<td>" + fechaPago + "</td>";
                        html += "<td><div class='m-card-user m-card-user--sm'><div class='m-card-user__details'> <span class='m-card-user__name'>" + reciboPago + "</span> <a href='#' class='m-card-user__email m-link'>Nro: " + numReciboPago + "</a> </div></div></td>";
                        html += "<td>" + totalPagado + "</td>";

                        if (codCheque == string.Empty)
                        {
                            html += "<td style='text-align:center'><a href='#' onclick ='AbriDetalles(event," + codigoPagoContratista + ");' class='btn btn-sm btn-default'> Detalle</a><a href='#' style='margin-left:5px' onclick ='EliminarPagoContratista(event," + codigoPagoContratista + ");' class='btn btn-sm btn-outline-danger'> Eliminar</a></td>";
                        }
                        else
                        {
                            html += "<td style='text-align:center'><a href='#' onclick ='AbriDetalles(event," + codigoPagoContratista + ");' class='btn btn-sm btn-default'> Detalle</a><a href='#' style='margin-left:5px' onclick ='ImprimirCheque(event," + codCheque + ");' class='btn btn-warning m-btn m-btn--icon m-btn--icon-only'><i class='la la-print'></i></a></td>";
                        }
                        
                        contadorfilas += 1;
                    }
                }
                else
                {
                    html = "<p style='font-size: 20px;padding:30px'>Por el momento no hay pagos realizados a este contrato.</p>";
                }

                html += "</tbody></table>";
            }
            catch (Exception)
            {

            }
            finally
            {
                html += "</div></div></div>";
                tbListadoPagosRealizados.InnerHtml = html;
            }
        }
        private void GenerarReporteChequeInvidual(string codCheque)
        {
            /*CARGAMOS PRIMER DATASET*/

            System.Data.DataSet DataSetCheque = new System.Data.DataSet();

            DataSetCheque.Tables.Add(procesoCheques.ObtenerDatosChequeReporteIndividual(Convert.ToInt32(codCheque)));

            ReportDataSource datosSolicitante = new ReportDataSource("ReporteChequeIndividualDataSet", DataSetCheque.Tables[0]);

            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(datosSolicitante);
            ReportViewer1.LocalReport.Refresh();

            gpListadoContratos.Visible = false;
            gpListadoTrabajadores.Visible = false;
            gpReporteRVIndividual.Style.Add("display", "normal");
            gpContratosContratista.Visible = false;
            gpFormularioPago.Visible = false;
        }
        private void LimpiarFormulario()
        {
            cboModoPago.ClearSelection();
            cboTipoPago.ClearSelection();
            txtFechaPago.Text = string.Empty;
            cboTipoReciboPago.ClearSelection();
            txtNumRecigoPago.Text = string.Empty;
            txtNumeroCheque.Text = string.Empty;
            txtMontoPagar.Text = string.Empty;
        }

       
    }
}