using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogicaNegocio;
using Entidad;
using System.Drawing;
using System.Globalization;
using System.Configuration;

namespace SistemaGestionPlanilla.Contratistas
{
    public partial class ContratistasRegistroContrato : System.Web.UI.Page
    {
        ContratistasNegocio procesoContratistas = new ContratistasNegocio();
        CentroCostosNegocio procesoCentroCostos = new CentroCostosNegocio();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["CodUsuarioInterno"] != null)
                {
                    this.txtTextoBuscar.Attributes.Add("onkeypress", "button_click(this,'" + this.btnBuscar.ClientID + "')");
                    txtTextoBuscar.Focus();

                    Session["CodContratoModificar"] = "";
                    gpConsultaContratos.Visible = true;
                    gpFormularioContrato.Visible = false;

                    BuscarContratoContratista("");

                    CargarProyectoPlanilla();
                    CargarContratistas();
                    CargarModoPagoContratistas();
                }
                else
                {
                    Response.Redirect(ConfigurationManager.AppSettings["AssetsUrl"] + "/Seguridad/Logout.aspx");
                }
            }
        }
        protected void btnRegistrarContrato_ServerClick(object sender, EventArgs e)
        {
            try
            {
                LimpiarFormulario();

                gpFormularioContrato.Visible = true;
                gpConsultaContratos.Visible = false;

                Session["CodContratoModificar"] = "";
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }
        protected void dgvContratos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    if (e.Row.Cells[7].Text == "1")
                    {
                        e.Row.Cells[7].Text = "<span class='m-badge m-badge--success'></span>";
                    }
                    else if (e.Row.Cells[7].Text == "0")
                    {
                        e.Row.Cells[7].Text = "<span class='m-badge m-badge--danger'></span>";
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }
        protected void dgvContratos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string codContrato = e.CommandArgument.ToString();

                if (e.CommandName == "VerDetalleContrato")
                {
                    CargarDatosContrato(codContrato);
                }
                else if (e.CommandName == "EliminarContrato")
                {
                    string mensaje = EliminarContrato(Convert.ToInt32(codContrato), Convert.ToInt32(Session["CodUsuarioInterno"].ToString()));

                    if (mensaje == "EXITO")
                    {
                        ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('Contrato eliminado correctamente.','success');", true);

                        BuscarContratoContratista("");
                    }
                    else if (mensaje == "PAGOVIGENTE")
                    {
                        ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('El contrato tiene un registro de pago vigente, no es posible eliminarlo.','info');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + mensaje + ".','error');", true);
                    }
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }
        protected void dgvContratos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                dgvContratos.PageIndex = e.NewPageIndex;

                if (txtTextoBuscar.Text == string.Empty)
                {
                    BuscarContratoContratista("");
                }
                else
                {
                    BuscarContratoContratista(txtTextoBuscar.Text.ToString().Trim());
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
                gpConsultaContratos.Visible = true;
                gpFormularioContrato.Visible = false;

                LimpiarFormulario();
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
                string codContratista = cboContratista.SelectedValue.ToString();
                string codProyectoPlanilla = cboProyecto.SelectedValue.ToString();
                string descripcionLaborContrato = txtLaborContratoContratista.Text.Trim();
                string codModoPago = cboTipoPago.SelectedValue.ToString();
                string montoTotal = txtMontoTotal.Text.Trim();
                string fechaInicio = txtFechaInicioContrato.Text.Trim();

                if (codContratista == "Seleccione") { contadorCamposVacios += 1; lblContratista.ForeColor = Color.Red; } else { lblContratista.ForeColor = ColorTranslator.FromHtml("#3f4047"); }
                if (codProyectoPlanilla == "Seleccione") { contadorCamposVacios += 1; lblProyecto.ForeColor = Color.Red; } else { lblProyecto.ForeColor = ColorTranslator.FromHtml("#3f4047"); }
                if (descripcionLaborContrato == string.Empty) { contadorCamposVacios += 1; lblLaborContratista.ForeColor = Color.Red; } else { lblLaborContratista.ForeColor = ColorTranslator.FromHtml("#3f4047"); }
                if (codModoPago == "Seleccione") { contadorCamposVacios += 1; lblTipoPago.ForeColor = Color.Red; } else { lblTipoPago.ForeColor = ColorTranslator.FromHtml("#3f4047"); }
                if (montoTotal == string.Empty) { contadorCamposVacios += 1; lblMontoTotal.ForeColor = Color.Red; } else { lblMontoTotal.ForeColor = ColorTranslator.FromHtml("#3f4047"); }
                if (fechaInicio == string.Empty) { contadorCamposVacios += 1; lblFechaInicioContrato.ForeColor = Color.Red; } else { lblFechaInicioContrato.ForeColor = ColorTranslator.FromHtml("#3f4047"); }

                if (contadorCamposVacios == 0)
                {
                    string mensaje;

                    if (Session["CodContratoModificar"].ToString() == string.Empty)
                    {
                        mensaje = GuardarContrato(codContratista, codProyectoPlanilla, descripcionLaborContrato, codModoPago, double.Parse(montoTotal, CultureInfo.InvariantCulture), fechaInicio, Session["CodUsuarioInterno"].ToString());
                    }
                    else
                    {
                        mensaje = ModificarContrato(codContratista, codProyectoPlanilla, descripcionLaborContrato, codModoPago, double.Parse(montoTotal, CultureInfo.InvariantCulture), fechaInicio, Session["CodUsuarioInterno"].ToString(), Session["CodContratoModificar"].ToString());
                    }

                    if (mensaje == "EXITO")
                    {

                        ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('Contrato creado correctamente.','success');", true);

                        LimpiarFormulario();

                        gpFormularioContrato.Visible = false;
                        gpConsultaContratos.Visible = true;

                        BuscarContratoContratista("");

                    }
                    else if (mensaje == "REPETIDO")
                    {
                        ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('Ya existe otro contrato con los mismos parámetros.','info');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + mensaje + "','error');", true);
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
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                BuscarContratoContratista(txtTextoBuscar.Text.Trim());
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }
       
        /*---------------------------------------*/
        /*---------------METODOS-----------------*/
        /*---------------------------------------*/
        private void BuscarContratoContratista(string TextoBuscar)
        {
            DataTable dt = procesoContratistas.BuscarContratistasContratos(TextoBuscar);
            dgvContratos.DataSource = dt;
            dgvContratos.DataBind();
            lblResultadoBusqueda.InnerHtml = "" + dt.Rows.Count.ToString() + " registros(s) encontrado(s)";
        }
        private string GuardarContrato(string codContratista, string codProyecto, string descripcionLaborContrato, string codModoPago, double montoTotal, string fechaInicio, string codUsuario)
        {
            ContratistasEntidad contratistasEntidad = new ContratistasEntidad();
            contratistasEntidad.CodContratista = Convert.ToInt32(codContratista);
            contratistasEntidad.CodProyectoPlanilla = Convert.ToInt32(codProyecto);
            contratistasEntidad.DescripcionLaborContrato = descripcionLaborContrato;
            contratistasEntidad.CodModoPago = Convert.ToInt32(codModoPago);
            contratistasEntidad.MontoTotal = montoTotal;
            contratistasEntidad.FechaInicioContrato = fechaInicio;
            contratistasEntidad.CodUsuarioRegistro = Convert.ToInt32(codUsuario);
            return procesoContratistas.RegistrarContratoContratista(contratistasEntidad);
        }
        private string ModificarContrato(string codContratista, string codProyecto, string descripcionLaborContrato, string codModoPago, double montoTotal, string fechaInicio, string codUsuario, string codContrato)
        {
            ContratistasEntidad contratistasEntidad = new ContratistasEntidad();
            contratistasEntidad.CodContratista = Convert.ToInt32(codContratista);
            contratistasEntidad.CodProyectoPlanilla = Convert.ToInt32(codProyecto);
            contratistasEntidad.DescripcionLaborContrato = descripcionLaborContrato;
            contratistasEntidad.CodModoPago = Convert.ToInt32(codModoPago);
            contratistasEntidad.MontoTotal = montoTotal;
            contratistasEntidad.FechaInicioContrato = fechaInicio;
            contratistasEntidad.CodUsuarioModifica = Convert.ToInt32(codUsuario);
            contratistasEntidad.CodContrato = Convert.ToInt32(codContrato);
            return procesoContratistas.ModificarContratoContratista(contratistasEntidad);
        }
        private string EliminarContrato(int codContrato, int codUsuarioModifica)
        {
            return procesoContratistas.EliminarContrato(codContrato, codUsuarioModifica);
        }
        private void CargarDatosContrato(string codContrato)
        {
            DataTable dt = procesoContratistas.CargarDatosContrato(Convert.ToInt32(codContrato));

            if (dt.Rows.Count > 0)
            {
                LimpiarFormulario();

                cboContratista.SelectedValue = dt.Rows[0][1].ToString();
                cboProyecto.SelectedValue = dt.Rows[0][2].ToString();
                txtLaborContratoContratista.Text = dt.Rows[0][3].ToString();
                cboTipoPago.SelectedValue = dt.Rows[0][4].ToString();
                txtMontoTotal.Text = dt.Rows[0][5].ToString().Replace(",", ".");
                txtFechaInicioContrato.Text = dt.Rows[0][6].ToString();

                gpFormularioContrato.Visible = true;
                gpConsultaContratos.Visible = false;

                Session["CodContratoModificar"] = codContrato;
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
                cboProyecto.Items.Insert(0, "Seleccione");
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }
        private void CargarContratistas()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = procesoContratistas.CargarContratistas();
                cboContratista.DataSource = dt;
                cboContratista.DataTextField = "DescripcionProveedor";
                cboContratista.DataValueField = "CodProveedor";
                cboContratista.DataBind();
                cboContratista.Items.Insert(0, "Seleccione");
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }
        private void CargarModoPagoContratistas()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = procesoContratistas.CargarModosPago();
                cboTipoPago.DataSource = dt;
                cboTipoPago.DataTextField = "DescripcionTipoPago";
                cboTipoPago.DataValueField = "CodTipoPago";
                cboTipoPago.DataBind();
                cboTipoPago.Items.Insert(0, "Seleccione");
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }
        private void LimpiarFormulario()
        {
            cboContratista.ClearSelection();
            cboProyecto.ClearSelection();
            txtLaborContratoContratista.Text = string.Empty;
            cboTipoPago.ClearSelection();
            txtMontoTotal.Text = string.Empty;
            txtFechaInicioContrato.Text = string.Empty;
        }

        
    }
}