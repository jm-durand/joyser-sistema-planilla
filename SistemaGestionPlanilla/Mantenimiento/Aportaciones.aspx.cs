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

namespace SistemaGestionPlanilla.Mantenimiento
{
    public partial class Aportaciones : System.Web.UI.Page
    {
        MantenimientoNegocio procesoMantenimiento = new MantenimientoNegocio();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["CodUsuarioInterno"] != null)
                {
                    Session["CodAportacionModificar"] = "";

                    CargarAportaciones();

                    gpParametrosAportacion.Visible = false;
                    gpResultadoBusqueda.Visible = true;
                }
                else
                {
                    Response.Redirect(ConfigurationManager.AppSettings["AssetsUrl"] + "/Seguridad/Logout.aspx");
                }
            }
        }
        protected void dgvAportaciones_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "ModificarParametrosAportacion")
                {
                    LimpiarFormulario();

                    string codAportacion = e.CommandArgument.ToString();

                    DataTable dt = procesoMantenimiento.ObtenerDatosAportacion(Convert.ToInt32(codAportacion));

                    if (dt.Rows.Count > 0)
                    {
                        txtTipoAportacion.Text = dt.Rows[0][1].ToString().Trim();
                        txtAporteObligatorio.Text = dt.Rows[0][2].ToString().Trim();
                        txtComisionPorFlujo.Text = dt.Rows[0][3].ToString().Trim();
                        txtComisionMixta.Text = dt.Rows[0][4].ToString().Trim();
                        txtPrimaSeguro.Text = dt.Rows[0][5].ToString().Trim();
                        txtAporteComplementario.Text = dt.Rows[0][6].ToString().Trim();

                        Session["CodAportacionModificar"] = dt.Rows[0][0].ToString();

                        gpParametrosAportacion.Visible = true;
                        gpResultadoBusqueda.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }
        protected void dgvAportaciones_RowDataBound(object sender, GridViewRowEventArgs e)
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
        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            try
            {
                gpParametrosAportacion.Visible = false;
                gpResultadoBusqueda.Visible = true;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }
        protected void btnModificarAportaciones_Click(object sender, EventArgs e)
        {
            try
            {
                int contadorCamposVacios = 0;
                string aporteObligatorio = txtAporteObligatorio.Text.Trim();
                string comisionFlujo = txtComisionPorFlujo.Text.Trim();
                string primaSeguro = txtPrimaSeguro.Text.Trim();
                string aporteComplementario = txtAporteComplementario.Text.Trim();
                string comisionMixta = txtComisionMixta.Text.Trim();
                string codAportacion = Session["CodAportacionModificar"].ToString();
                string codUsuario = Session["CodUsuarioInterno"].ToString();

                if (aporteObligatorio == string.Empty) { contadorCamposVacios += 1; lblAporteObligatorio.ForeColor = Color.Red; } else { lblAporteObligatorio.ForeColor = ColorTranslator.FromHtml("#3f4047"); }
                if (comisionFlujo == string.Empty) { contadorCamposVacios += 1; lblComisionPorFlujo.ForeColor = Color.Red; } else { lblComisionPorFlujo.ForeColor = ColorTranslator.FromHtml("#3f4047"); }
                if (primaSeguro == string.Empty) { contadorCamposVacios += 1; lblPrimaSeguro.ForeColor = Color.Red; } else { lblPrimaSeguro.ForeColor = ColorTranslator.FromHtml("#3f4047"); }
                if (aporteComplementario == string.Empty) { contadorCamposVacios += 1; lblAporteComplementario.ForeColor = Color.Red; } else { lblAporteComplementario.ForeColor = ColorTranslator.FromHtml("#3f4047"); }
                if (comisionMixta == string.Empty) { contadorCamposVacios += 1; lblComisionMixta.ForeColor = Color.Red; } else { lblComisionMixta.ForeColor = ColorTranslator.FromHtml("#3f4047"); }

                if (contadorCamposVacios == 0)
                {
                    string mensaje;

                    mensaje = ModificarAportacion(double.Parse(aporteObligatorio, CultureInfo.InvariantCulture), double.Parse(comisionFlujo, CultureInfo.InvariantCulture), double.Parse(primaSeguro, CultureInfo.InvariantCulture), double.Parse(aporteComplementario, CultureInfo.InvariantCulture), double.Parse(comisionMixta, CultureInfo.InvariantCulture), Convert.ToInt32(codAportacion), Convert.ToInt32(codUsuario));

                    if (mensaje == "EXITO")
                    {
                        CargarAportaciones();

                        gpParametrosAportacion.Visible = false;
                        gpResultadoBusqueda.Visible = true;

                        ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('Aportación modificada correctamente.','success');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + mensaje + "','error');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('Debe ingresar los campos marcados con rojo.','info');", true);
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
        private string ModificarAportacion(double aporteObligatorio, double comisionFlujo, double primaSeguro, double aporteComplementario, double comisionMixta, int codAportacion, int codUsuario)
        {       
            return procesoMantenimiento.ModificarAportacion(aporteObligatorio, comisionFlujo, primaSeguro, aporteComplementario, comisionMixta, codAportacion, codUsuario);
        }
        private void CargarAportaciones()
        {
            try
            {
                DataTable dt = procesoMantenimiento.ObtenerTipoAportaciones();
                dgvAportaciones.DataSource = dt;
                dgvAportaciones.DataBind();

                lblResultadoBusqueda.InnerHtml = "" + dt.Rows.Count.ToString() + " registros(s) encontrado(s)";
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }
        private void LimpiarFormulario()
        {
            txtTipoAportacion.Text = string.Empty;
            txtAporteObligatorio.Text = string.Empty;
            txtComisionPorFlujo.Text = string.Empty;
            txtComisionMixta.Text = string.Empty;
            txtPrimaSeguro.Text = string.Empty;
            txtAporteComplementario.Text = string.Empty;
        }

       
    }
}