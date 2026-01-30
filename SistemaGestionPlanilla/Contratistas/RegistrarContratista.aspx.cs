using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogicaNegocio;
using Entidad;
using System.Configuration;

namespace SistemaGestionPlanilla.Contratistas
{
    public partial class RegistrarContratista : System.Web.UI.Page
    {
        ContratistasNegocio procesoContratistas = new ContratistasNegocio();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["CodUsuarioInterno"] != null)
                {
                    this.txtTextoBuscar.Attributes.Add("onkeypress", "button_click(this,'" + this.btnBuscar.ClientID + "')");
                    txtTextoBuscar.Focus();

                    Session["CodContratistaModificar"] = "";
                    gpConsultaContratistas.Visible = true;
                    gpFormularioContratista.Visible = false;
                    BuscarContratistas("");
                }
                else
                {
                    Response.Redirect(ConfigurationManager.AppSettings["AssetsUrl"] + "/Seguridad/Logout.aspx");
                }
            }
        }
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                BuscarContratistas(txtTextoBuscar.Text.Trim());
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }
        protected void btnAgregarContratistas_ServerClick(object sender, EventArgs e)
        {
            try
            {
                LimpiarFormulario();

                gpFormularioContratista.Visible = true;
                gpConsultaContratistas.Visible = false;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }
        protected void dgvContratistas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string codContratista = e.CommandArgument.ToString();

                if (e.CommandName == "VerDetalleProveedor")
                {                   
                    CargarDatosContratista(codContratista);
                }
                else if (e.CommandName == "EliminarContratista")
                {
                    string mensaje = EliminarContratista(Convert.ToInt32(codContratista), Convert.ToInt32(Session["CodUsuarioInterno"].ToString()));

                    if (mensaje == "EXITO")
                    {
                        ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('Contratista eliminado correctamente.','success');", true);

                        BuscarContratistas("");
                    }
                    else if (mensaje== "CONTRATOVIGENTE")
                    {
                        ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('El contratista tiene un contrato vigente, no es posible eliminarlo.','info');", true);
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
        protected void dgvContratistas_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    if (e.Row.Cells[3].Text == "1")
                    {
                        e.Row.Cells[3].Text = "<span class='m-badge m-badge--success'></span>";
                    }
                    else if (e.Row.Cells[3].Text == "0")
                    {
                        e.Row.Cells[3].Text = "<span class='m-badge m-badge--danger'></span>";
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }
        protected void dgvContratistas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                dgvContratistas.PageIndex = e.NewPageIndex;

                if (txtTextoBuscar.Text == string.Empty)
                {
                    BuscarContratistas("");
                }
                else
                {
                    BuscarContratistas(txtTextoBuscar.Text.ToString().Trim());
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
                gpConsultaContratistas.Visible = true;
                gpFormularioContratista.Visible = false;

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
                string descripcionContratista = txtDescripcionContratista.Text.Trim();
                string ruc = txtRuc.Text.Trim();
                string fechaInicio = txtFechaInicioVigencia.Text.Trim();
                string fechaFin = txtFechaFinVigencia.Text.Trim();

                if (descripcionContratista == string.Empty) { contadorCamposVacios += 1; lblDescripcionContratista.ForeColor = Color.Red; } else { lblDescripcionContratista.ForeColor = ColorTranslator.FromHtml("#3f4047"); }
                if (ruc == string.Empty) { contadorCamposVacios += 1; lblRuc.ForeColor = Color.Red; } else { lblRuc.ForeColor = ColorTranslator.FromHtml("#3f4047"); }
                if (fechaInicio == string.Empty) { contadorCamposVacios += 1; lblFechaInicio.ForeColor = Color.Red; } else { lblFechaInicio.ForeColor = ColorTranslator.FromHtml("#3f4047"); }
                if (fechaFin == string.Empty) { contadorCamposVacios += 1; lblFechaFin.ForeColor = Color.Red; } else { lblFechaFin.ForeColor = ColorTranslator.FromHtml("#3f4047"); }

                if (contadorCamposVacios == 0)
                {
                    string mensaje;

                    if (Session["CodContratistaModificar"].ToString() == string.Empty)
                    {
                        mensaje = GuardarContratista(descripcionContratista, ruc, fechaInicio, fechaFin, Session["CodUsuarioInterno"].ToString());
                    }
                    else
                    {
                        mensaje = ModificarContratista(descripcionContratista, ruc, fechaInicio, fechaFin, Session["CodUsuarioInterno"].ToString(), Session["CodContratistaModificar"].ToString()); ;
                    }

                    if (mensaje == "EXITO")
                    {

                        ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('Contratista creado correctamente.','success');", true);

                        LimpiarFormulario();

                        gpFormularioContratista.Visible = false;
                        gpConsultaContratistas.Visible = true;

                        BuscarContratistas("");

                    }
                    else if (mensaje == "REPETIDO")
                    {
                        ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('Ya existe otro contratista con el mismo nombre y ruc.','info');", true);
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
        /*---------------------------------------*/
        /*---------------METODOS-----------------*/
        /*---------------------------------------*/
        private void BuscarContratistas(string TextoBuscar)
        {
            DataTable dt = procesoContratistas.BuscarContratistas(TextoBuscar);
            dgvContratistas.DataSource = dt;
            dgvContratistas.DataBind();
            lblResultadoBusqueda.InnerHtml = "" + dt.Rows.Count.ToString() + " registros(s) encontrado(s)";
        }
        private string GuardarContratista(string descripcionContratista, string ruc, string fechaInicio, string fechaFin, string codUsuario)
        {
            ContratistasEntidad contratistasEntidad = new ContratistasEntidad();
            contratistasEntidad.DescripcionContratista = descripcionContratista;
            contratistasEntidad.Ruc = ruc;
            contratistasEntidad.FechaInicioVigencia = fechaInicio;
            contratistasEntidad.FechaFinVigencia = fechaFin;
            contratistasEntidad.CodUsuarioRegistro = Convert.ToInt32(codUsuario);
            return procesoContratistas.RegistrarContratista(contratistasEntidad);
        }
        private string ModificarContratista(string descripcionContratista, string ruc, string fechaInicio, string fechaFin, string codUsuario, string codContratista)
        {
            ContratistasEntidad contratistasEntidad = new ContratistasEntidad();
            contratistasEntidad.DescripcionContratista = descripcionContratista;
            contratistasEntidad.Ruc = ruc;
            contratistasEntidad.FechaInicioVigencia = fechaInicio;
            contratistasEntidad.FechaFinVigencia = fechaFin;
            contratistasEntidad.CodUsuarioModifica = Convert.ToInt32(codUsuario);
            contratistasEntidad.CodContratista = Convert.ToInt32(codContratista);
            return procesoContratistas.ModificarContratista(contratistasEntidad);
        }
        private string EliminarContratista(int codProyectoPlanilla, int codUsuarioModifica)
        {
            return procesoContratistas.EliminarContratista(codProyectoPlanilla, codUsuarioModifica);
        }
        private void CargarDatosContratista(string codContratista)
        {
            DataTable dt = procesoContratistas.CargarDatosContratista(Convert.ToInt32(codContratista));

            if (dt.Rows.Count > 0)
            {
                LimpiarFormulario();

                txtDescripcionContratista.Text = dt.Rows[0][1].ToString();
                txtRuc.Text = dt.Rows[0][2].ToString().Replace(",", ".");
                txtFechaInicioVigencia.Text = dt.Rows[0][3].ToString();
                txtFechaFinVigencia.Text = dt.Rows[0][4].ToString();

                gpFormularioContratista.Visible = true;
                gpConsultaContratistas.Visible = false;

                Session["CodContratistaModificar"] = codContratista;
            }
        }
        private void LimpiarFormulario()
        {
            txtDescripcionContratista.Text = string.Empty;
            txtRuc.Text = string.Empty;
            txtFechaInicioVigencia.Text = string.Empty;
            txtFechaFinVigencia.Text = string.Empty;
        }

       
    }
}