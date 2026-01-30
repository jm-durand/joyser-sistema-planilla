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
    public partial class Servicios : System.Web.UI.Page
    {
        MantenimientoNegocio procesoMantenimiento = new MantenimientoNegocio();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["CodUsuarioInterno"] != null)
                {
                    Session["CodServicioModificar"] = "";

                    CargarServicios();
                }
                else
                {
                    Response.Redirect(ConfigurationManager.AppSettings["AssetsUrl"] + "/Seguridad/Logout.aspx");
                }
            }
        }
        protected void dgvServicios_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "ModificarServicio")
                {
                    string codServicio = e.CommandArgument.ToString();

                    DataTable dt = procesoMantenimiento.ObtenerDatosServicio(Convert.ToInt32(codServicio));

                    if (dt.Rows.Count > 0)
                    {
                        txtNombreServicio.Text = dt.Rows[0][1].ToString().Trim();
                        txtUnidadMedida.Text = dt.Rows[0][2].ToString().Trim();
                        txtCosto.Text = dt.Rows[0][3].ToString().Trim().Replace(",",".");

                        btnGuardarServicio.Text = "<span><i class='la la-floppy-o'></i> <span> Modificar</span> </span>";

                        Session["CodServicioModificar"] = dt.Rows[0][0].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }

        protected void dgvServicios_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    if (e.Row.Cells[4].Text == "True")
                    {
                        e.Row.Cells[4].Text = "<span class='m-badge m-badge--success'></span>";
                    }
                    else if (e.Row.Cells[4].Text == "False")
                    {
                        e.Row.Cells[4].Text = "<span class='m-badge m-badge--danger'></span>";
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }
        protected void btnGuardarServicio_Click(object sender, EventArgs e)
        {
            try
            {
                int contadorCamposVacios = 0;
                string nombreServicio = txtNombreServicio.Text.Trim();
                string unidadMedida = txtUnidadMedida.Text.Trim();
                string costo = txtCosto.Text.Trim();

                if (nombreServicio == string.Empty) { contadorCamposVacios += 1; lblNombreServicio.ForeColor = Color.Red; } else { lblNombreServicio.ForeColor = ColorTranslator.FromHtml("#3f4047"); }
                if (unidadMedida == string.Empty) { contadorCamposVacios += 1; lblUnidadMedida.ForeColor = Color.Red; } else { lblUnidadMedida.ForeColor = ColorTranslator.FromHtml("#3f4047"); }
                if (costo == string.Empty) { contadorCamposVacios += 1; lblCosto.ForeColor = Color.Red; } else { lblCosto.ForeColor = ColorTranslator.FromHtml("#3f4047"); }

                if (contadorCamposVacios == 0)
                {
                    string mensaje;

                    if (Session["CodServicioModificar"].ToString() == string.Empty)
                    {
                        mensaje = GuardarServicio(nombreServicio, unidadMedida, costo);
                    }
                    else
                    {
                        mensaje = ModificarServicio(nombreServicio, unidadMedida, costo, Session["CodServicioModificar"].ToString());
                    }

                    if (mensaje == "EXITO")
                    {
                        ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('Labor de trabajador agregado correctamente.','success');", true);

                        LimpiarFormulario();

                        CargarServicios();

                        btnGuardarServicio.Text = "<span><i class='la la-floppy-o'></i> <span> Guardar</span> </span>";
                    }
                    else if (mensaje == "REPETIDO")
                    {
                        ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('Ya existe en otro labor con el mismo nombre en la base de datos.','info');", true);
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
        private void CargarServicios()
        {
            try
            {
                DataTable dt = procesoMantenimiento.ObtenerServicios();
                dgvServicios.DataSource = dt;
                dgvServicios.DataBind();

                lblResultadoBusqueda.InnerHtml = "" + dt.Rows.Count.ToString() + " registros(s) encontrado(s)";
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }
        private string GuardarServicio(string nombreServicio, string unidadMedida, string costoUnitario)
        {
            return procesoMantenimiento.RegistrarServicios(nombreServicio, unidadMedida, double.Parse(costoUnitario, CultureInfo.InvariantCulture));
        }
        private string ModificarServicio(string nombreServicio, string unidadMedida, string costoUnitario, string codServicio)
        {
            return procesoMantenimiento.ActualizarServicio(nombreServicio, unidadMedida, double.Parse(costoUnitario, CultureInfo.InvariantCulture), Convert.ToInt32(codServicio));
        }
        private void LimpiarFormulario()
        {
            txtNombreServicio.Text = string.Empty;
            txtUnidadMedida.Text = string.Empty;
            txtCosto.Text = string.Empty;
        }

      
    }
}