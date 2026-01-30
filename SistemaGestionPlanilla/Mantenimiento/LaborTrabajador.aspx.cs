using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogicaNegocio;

namespace SistemaGestionPlanilla.Mantenimiento
{
    public partial class LaborTrabajador : System.Web.UI.Page
    {
        MantenimientoNegocio procesoMantenimiento = new MantenimientoNegocio();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["CodUsuarioInterno"] != null)
                {
                    Session["CodLaborTrabajadorModificar"] = "";

                    BuscarLaborTrabajo("");
                }
                else
                {
                    Response.Redirect(ConfigurationManager.AppSettings["AssetsUrl"] + "/Seguridad/Logout.aspx");
                }           
            }
        }
        protected void dgvLaborTrabajo_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "ModificarLaborTrabajo")
                {
                    string codLaborTrabajador = e.CommandArgument.ToString();                    

                    DataTable dt = procesoMantenimiento.ObtenerDatosLaborTrabajo(Convert.ToInt32(codLaborTrabajador));

                    if (dt.Rows.Count > 0)
                    {
                        txtNombreLaborTrabajo.Text = dt.Rows[0][1].ToString().Trim();
                        btnLaborTrabajo.Text = "<span><i class='la la-floppy-o'></i> <span> Modificar</span> </span>";
                        Session["CodLaborTrabajadorModificar"] = dt.Rows[0][0].ToString();
                    }
                }               
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }
        protected void dgvLaborTrabajo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    if (e.Row.Cells[2].Text == "1")
                    {
                        e.Row.Cells[2].Text = "<span class='m-badge m-badge--success'></span>";
                    }
                    else if (e.Row.Cells[2].Text == "0")
                    {
                        e.Row.Cells[2].Text = "<span class='m-badge m-badge--danger'></span>";
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }
        protected void btnLaborTrabajo_Click(object sender, EventArgs e)
        {
            try
            {
                int contadorCamposVacios = 0;
                string nombreLaborTrabajador = txtNombreLaborTrabajo.Text.Trim();

                if (nombreLaborTrabajador == string.Empty) { contadorCamposVacios += 1; }

                if (contadorCamposVacios == 0)
                {
                    string mensaje;

                    if (Session["CodLaborTrabajadorModificar"].ToString() == string.Empty)
                    {
                        mensaje = GuardarLaborTrabajador(nombreLaborTrabajador);
                    }
                    else
                    {
                        mensaje = ModificarLaborTrabajador(nombreLaborTrabajador, Session["CodLaborTrabajadorModificar"].ToString());
                    }
                    
                    if (mensaje == "EXITO")
                    {
                        ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('Labor de trabajador agregado correctamente.','success');", true);

                        txtNombreLaborTrabajo.Text = string.Empty;

                        CargarLaborTrabajo();

                        btnLaborTrabajo.Text = "<span><i class='la la-floppy-o'></i> <span> Guardar</span> </span>";
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
                    ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('Debe ingresar al un nombre para el nuevo registro.','info');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }
        protected void dgvLaborTrabajo_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                dgvLaborTrabajo.PageIndex = e.NewPageIndex;               
                BuscarLaborTrabajo("");                               
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }
        private void BuscarLaborTrabajo(string TextoBuscar)
        {
            try
            {
                DataTable dt = procesoMantenimiento.BuscarLaborTrabajo(TextoBuscar);
                dgvLaborTrabajo.DataSource = dt;
                dgvLaborTrabajo.DataBind();

                lblResultadoBusqueda.InnerHtml = "" + dt.Rows.Count.ToString() + " registros(s) encontrado(s)";
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
                DataTable dt = procesoMantenimiento.ObtenerLaborTrabajo();
                dgvLaborTrabajo.DataSource = dt;
                dgvLaborTrabajo.DataBind();

                lblResultadoBusqueda.InnerHtml = "" + dt.Rows.Count.ToString() + " registros(s) encontrado(s)";
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }
        private string GuardarLaborTrabajador(string nombreLaborTrabajador)
        {
            return procesoMantenimiento.RegistrarLaborTrabajador(nombreLaborTrabajador);
        }
        private string ModificarLaborTrabajador(string nombreLaborTrabajador, string codLaborTrabajo)
        {
            return procesoMantenimiento.ModificarLaborTrabajador(nombreLaborTrabajador, Convert.ToInt32(codLaborTrabajo));
        }

       
    }
}