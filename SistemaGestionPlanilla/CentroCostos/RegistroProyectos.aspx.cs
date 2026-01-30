using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogicaNegocio;

namespace SistemaGestionPlanilla.CentroCostos
{
    public partial class RegistroProyectos : System.Web.UI.Page
    {
        CentroCostosNegocio procesoCentroCostos = new CentroCostosNegocio();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["CodUsuarioInterno"] != null)
                {
                    Session["CodProyectoPlanillaModificar"] = "";
                    gpConsultaProyectoPlanilla.Visible = true;
                    gpFormularioProyecto.Visible = false;
                    BuscarProyectoPlanilla("");
                }
                else
                {
                    Response.Redirect(ConfigurationManager.AppSettings["AssetsUrl"] + "/Seguridad/Logout.aspx");
                }
            }
        }
        protected void btnAgregarProyecto_ServerClick(object sender, EventArgs e)
        {
            try
            {
                LimpiarFormulario();

                gpFormularioProyecto.Visible = true;
                gpConsultaProyectoPlanilla.Visible = false;
                txtFechaInicioProyecto.Enabled = true;

                Session["CodProyectoPlanillaModificar"] = "";
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
                gpConsultaProyectoPlanilla.Visible = true;
                gpFormularioProyecto.Visible = false;

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
                string nombreProyecto = txtNombreProyecto.Text.Trim();
                string prespuesto = txtPresupuesto.Text.Trim();
                string fechaInicio = txtFechaInicioProyecto.Text.Trim();
                string fechaFin = txtFechaFinProyecto.Text.Trim();             

                if (nombreProyecto == string.Empty) { contadorCamposVacios += 1; lblNombreProyecto.ForeColor = Color.Red; } else { lblNombreProyecto.ForeColor = ColorTranslator.FromHtml("#3f4047"); }
                if (prespuesto == string.Empty) { contadorCamposVacios += 1; lblPresupuesto.ForeColor = Color.Red; } else { lblPresupuesto.ForeColor = ColorTranslator.FromHtml("#3f4047"); }
                if (fechaInicio == string.Empty) { contadorCamposVacios += 1; lblFechaInicio.ForeColor = Color.Red; } else { lblFechaInicio.ForeColor = ColorTranslator.FromHtml("#3f4047"); }
                if (fechaFin == string.Empty) { contadorCamposVacios += 1; lblFechaFin.ForeColor = Color.Red; } else { lblFechaFin.ForeColor = ColorTranslator.FromHtml("#3f4047"); }                
              
                if (contadorCamposVacios == 0)
                {                   
                    string mensaje;

                    if (Session["CodProyectoPlanillaModificar"].ToString() == string.Empty)
                    {
                        mensaje = GuardarProyectoPlanilla(nombreProyecto, double.Parse(prespuesto, CultureInfo.InvariantCulture), fechaInicio, fechaFin, Convert.ToInt32(Session["CodUsuarioInterno"].ToString()));
                    }
                    else
                    {
                        mensaje = ModificarProyectoPlanilla(nombreProyecto, double.Parse(prespuesto, CultureInfo.InvariantCulture), fechaFin, Convert.ToInt32(Session["CodUsuarioInterno"].ToString()), Convert.ToInt32(Session["CodProyectoPlanillaModificar"].ToString())); ;
                    }

                    if (mensaje == "EXITO")
                    {

                        ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('Proyecto creado correctamente.','success');", true);

                        LimpiarFormulario();

                        gpFormularioProyecto.Visible = false;
                        gpConsultaProyectoPlanilla.Visible = true;

                        BuscarProyectoPlanilla("");

                    }
                    else if (mensaje == "REPETIDO")
                    {
                        ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('Ya existe otro proyecto con el mismo nombre y presupuesto.','info');", true);
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
        protected void dgvProyectos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    if (e.Row.Cells[5].Text == "1")
                    {
                        e.Row.Cells[5].Text = "<span class='m-badge m-badge--success'></span>";
                    }
                    else if (e.Row.Cells[5].Text == "0")
                    {
                        e.Row.Cells[5].Text = "<span class='m-badge m-badge--danger'></span>";
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }
        protected void dgvProyectos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "VerDetalleProyectoPlanilla")
                {
                    string codProyectoPlanilla = e.CommandArgument.ToString();
                    CargarDatosProyectoPlanilla(codProyectoPlanilla);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }
        protected void dgvProyectos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                dgvProyectos.PageIndex = e.NewPageIndex;

                if (txtTextoBuscar.Text == string.Empty)
                {
                    BuscarProyectoPlanilla("");
                }
                else
                {
                    BuscarProyectoPlanilla(txtTextoBuscar.Text.ToString().Trim());
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
        private void BuscarProyectoPlanilla(string TextoBuscar)
        {
            DataTable dt = procesoCentroCostos.BuscarProyectoPlanilla(TextoBuscar);
            dgvProyectos.DataSource = dt;
            dgvProyectos.DataBind();
            lblResultadoBusqueda.InnerHtml = "" + dt.Rows.Count.ToString() + " registros(s) encontrado(s)";
        }
        private string GuardarProyectoPlanilla(string nombreProyectoPlanilla, double presupuesto, string fechaInicio, string fechaFin, int codUsuario)
        {
            return procesoCentroCostos.RegistrarProyectoPlanilla(nombreProyectoPlanilla, presupuesto, fechaInicio, fechaFin, codUsuario);
        }
        private string ModificarProyectoPlanilla(string nombreProyectoPlanilla, double presupuesto, string fechaFin, int codUsuario, int codProyectoPlanilla)
        {
            return procesoCentroCostos.ActualizarProyectoPlanilla(nombreProyectoPlanilla, presupuesto, fechaFin, codUsuario, codProyectoPlanilla);
        }
        private void CargarDatosProyectoPlanilla(string codProyectoPlanilla)
        {
            DataTable dt = procesoCentroCostos.CargarDatosProyectoPlanilla(Convert.ToInt32(codProyectoPlanilla));

            if (dt.Rows.Count > 0)
            {
                LimpiarFormulario();

                txtNombreProyecto.Text = dt.Rows[0][1].ToString();
                txtPresupuesto.Text = dt.Rows[0][2].ToString().Replace(",",".");
                txtFechaInicioProyecto.Text = dt.Rows[0][3].ToString();
                txtFechaFinProyecto.Text = dt.Rows[0][4].ToString();

                txtFechaInicioProyecto.Enabled = false;

                gpFormularioProyecto.Visible = true;
                gpConsultaProyectoPlanilla.Visible = false;

                Session["CodProyectoPlanillaModificar"] = codProyectoPlanilla;
            }
        }
        private void LimpiarFormulario()
        {
            txtNombreProyecto.Text = string.Empty;
            txtPresupuesto.Text = string.Empty;
            txtFechaInicioProyecto.Text = string.Empty;
            txtFechaFinProyecto.Text = string.Empty;
        }

       
    }
}