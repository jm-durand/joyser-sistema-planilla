using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogicaNegocio;

namespace SistemaGestionPlanilla.CentroCostos
{
    public partial class AsignacionTrabajadoresProyecto : System.Web.UI.Page
    {
        TrabajadorNegocio procesoTrabajadores = new TrabajadorNegocio();
        CentroCostosNegocio procesoCentroCostos = new CentroCostosNegocio();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["CodUsuarioInterno"] != null)
                {
                    Session["CodProyectoPlanillaTrabajadorModificar"] = "";
                    gpConsultaProyectoPlanilla.Visible = true;
                    gpFormularioProyecto.Visible = false;
                    gpListadoTrabajadores.Visible = false;
                    gpListadoTrabajadoresAsignados.Visible = false;

                    BuscarProyectoPlanilla("");

                    CargarProyectoPlanilla();
                }
                else
                {
                    Response.Redirect(ConfigurationManager.AppSettings["AssetsUrl"] + "/Seguridad/Logout.aspx");
                }
            }
        }
        protected void btnAsignarTrabajadores_ServerClick(object sender, EventArgs e)
        {
            try
            {
                LimpiarFormulario();
                gpFormularioProyecto.Visible = true;
                gpConsultaProyectoPlanilla.Visible = false;
                gpListadoTrabajadores.Visible = false;
                gpListadoTrabajadoresAsignados.Visible = false;

                btnGuardar.Visible = true;
                cboProyectoPlanilla.Enabled = true;
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
                    if (e.Row.Cells[6].Text == "1")
                    {
                        e.Row.Cells[6].Text = "<span class='m-badge m-badge--success'></span>";
                    }
                    else if (e.Row.Cells[6].Text == "0")
                    {
                        e.Row.Cells[6].Text = "<span class='m-badge m-badge--danger'></span>";
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

                    AbrirDetallesProyectoPlanilla(codProyectoPlanilla);
                   
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
        protected void cboProyectoPlanilla_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cboProyectoPlanilla.SelectedValue != "Seleccione")
                {
                    DataTable dt = procesoCentroCostos.CargarDatosProyectoPlanilla(Convert.ToInt32(cboProyectoPlanilla.SelectedValue));

                    if (dt.Rows.Count > 0)
                    {
                        txtPresupuesto.Text = dt.Rows[0][2].ToString().Replace(",", ".");
                    }

                    gpListadoTrabajadores.Visible = true;

                    ObtenerListadoTrabajadores(cboProyectoPlanilla.SelectedValue.ToString());
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
                gpConsultaProyectoPlanilla.Visible = true;
                gpFormularioProyecto.Visible = false;
                gpListadoTrabajadores.Visible = false;
                gpListadoTrabajadoresAsignados.Visible = false;

                LimpiarFormulario();

                Session["CodProyectoPlanillaTrabajadorModificar"] = "";
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
                int chckParametrosVacios = 0;
                string codigoProyecto = cboProyectoPlanilla.SelectedValue.ToString();
                string parametros = txtEnlaceExterno.Text.ToString().Trim();

                if (codigoProyecto == "Seleccione") { contadorCamposVacios += 1; lblNombreProyecto.ForeColor = Color.Red; } else { lblNombreProyecto.ForeColor = ColorTranslator.FromHtml("#3f4047"); }
                if (parametros == string.Empty) { chckParametrosVacios += 1; }

                if (contadorCamposVacios == 0)
                {
                    if (chckParametrosVacios == 0)
                    {
                        string mensaje;                       

                        string parametrosValido = parametros.Substring(1, parametros.Length - 1);
                        string[] Lista_Parametros = parametrosValido.Split(',');

                        for (int i = 0; i < Lista_Parametros.Length; i++)
                        {                                
                            string codParametro = Lista_Parametros[i].ToString().Trim();                                     
                            
                            mensaje = GuardarProyectoPlanillaTrabajador(Convert.ToInt32(codigoProyecto), Convert.ToInt32(codParametro), Convert.ToInt32(Session["CodUsuarioInterno"].ToString()));                                                       
                        }

                        ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('Trabajadores asignados al proyecto correctamente.','success');", true);

                        LimpiarFormulario();

                        gpFormularioProyecto.Visible = false;
                        gpConsultaProyectoPlanilla.Visible = true;

                        BuscarProyectoPlanilla("");
                       
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('Debe seleccionar al menos un trabajador.','info');", true);
                    }                    
                }
                else
                {
                    ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('Debe seleccionar un proyecto como centro de costos.','info');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }
        protected void dgvTrabajadoresAsignados_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                dgvTrabajadoresAsignados.PageIndex = e.NewPageIndex;

                CargarTrabajadoresProyectoPlanilla(Session["CodProyectoPlanillaTrabajadorModificar"].ToString());
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }
        protected void dgvTrabajadoresAsignados_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "DesactivarTrabajadorProyecto")
                {
                    string codTrabajador = e.CommandArgument.ToString();

                    string mensaje = DesactivarTrabajadorProyectoPlanilla(Session["CodProyectoPlanillaTrabajadorModificar"].ToString(), codTrabajador, Session["CodUsuarioInterno"].ToString());

                    if (mensaje == "EXITO")
                    {
                        ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('Trabajadores retirados correctamente.','success');", true);
                        CargarTrabajadoresProyectoPlanilla(Session["CodProyectoPlanillaTrabajadorModificar"].ToString());
                        BuscarProyectoPlanilla("");
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + mensaje + "','error');", true);
                    }
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
        private string GuardarProyectoPlanillaTrabajador(int codProyectoPlanilla, int codTrabajador, int codUsuario)
        {
            return procesoCentroCostos.RegistrarProyectoPlanillaTrabajador(codProyectoPlanilla, codTrabajador, codUsuario);
        }
        private string DesactivarTrabajadorProyectoPlanilla(string codProyectoPlanilla, string codTrabajador, string codUsuario)
        {
            return procesoCentroCostos.DesactivarTrabajadorProyectoPlanilla(Convert.ToInt32(codProyectoPlanilla), Convert.ToInt32(codTrabajador), Convert.ToInt32(codUsuario));
        }
        private void BuscarProyectoPlanilla(string TextoBuscar)
        {
            DataTable dt = procesoCentroCostos.BuscarProyectoPlanillaTrabajadores(TextoBuscar);
            dgvProyectos.DataSource = dt;
            dgvProyectos.DataBind();
            lblResultadoBusqueda.InnerHtml = "" + dt.Rows.Count.ToString() + " proyectos(s) encontrado(s)";
        }
        private void CargarTrabajadoresProyectoPlanilla(string CodProyectoPlanilla)
        {
            DataTable dt = procesoCentroCostos.CargarTrabajadoresProyectoPlanilla(Convert.ToInt32(CodProyectoPlanilla));
            dgvTrabajadoresAsignados.DataSource = dt;
            dgvTrabajadoresAsignados.DataBind();
            lblResultadoAsignados.InnerHtml = "" + dt.Rows.Count.ToString() + " trabajador(es) encontrado(s)";
        }
        private void AbrirDetallesProyectoPlanilla(string CodProyectoPlanilla)
        {
            cboProyectoPlanilla.SelectedValue = CodProyectoPlanilla;

            DataTable dt = procesoCentroCostos.CargarDatosProyectoPlanilla(Convert.ToInt32(CodProyectoPlanilla));

            if (dt.Rows.Count > 0)
            {
                txtPresupuesto.Text = dt.Rows[0][2].ToString().Replace(",", ".");
            }

            CargarTrabajadoresProyectoPlanilla(CodProyectoPlanilla);

            Session["CodProyectoPlanillaTrabajadorModificar"] = CodProyectoPlanilla;

            btnGuardar.Visible = false;
            cboProyectoPlanilla.Enabled = false;

            gpFormularioProyecto.Visible = true;
            gpConsultaProyectoPlanilla.Visible = false;
            gpListadoTrabajadoresAsignados.Visible = true;
            gpListadoTrabajadores.Visible = false;
        }
        private void ObtenerListadoTrabajadores(string codProyectoPlanilla)
        {
            DataTable dt = procesoTrabajadores.ObtenerListadoTrabajadores(Convert.ToInt32(codProyectoPlanilla));
            string html = "<div class='ibox'><div class='ibox-content top-fixed'><div class='table-responsive'><table class='table table-striped- table-bordered table-hover table-checkable' id='m_table_1'><thead><tr><th>#</th><th>Trabajador</th><th>Cargo</th><th>Planilla</th><th>Estado</th><th>Selección</th></tr></thead>";
            try
            {
                html += "<tbody>";
                if (dt.Rows.Count > 0)
                {
                    int contadorfilas = 1;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string codigoTrabajador = dt.Rows[i][0].ToString();
                        string trabajador = dt.Rows[i][2].ToString();
                        string cargo = dt.Rows[i][3].ToString();
                        string planilla = dt.Rows[i][4].ToString();
                        string estado = dt.Rows[i][6].ToString();
                       

                        html += "<tr id='" + contadorfilas + "'><td style='height:44px;'><div style='width:100%; height:100%; overflow-y: auto;'>" + contadorfilas + "</div></td>";
                        html += "<td style='text-align:left'>" + trabajador + "</td>";
                        html += "<td>" + cargo + "</td>";
                        html += "<td>" + planilla + "</td>";                        

                        if (estado == "1")
                        {
                            html += "<td><span class='m-badge m-badge--success'></span></td>";
                        }
                        else
                        {
                            html += "<td><span class='m-badge m-badge--danger'></span></td>";
                        }

                        html += "<td style='text-align:center'><label class='m-checkbox m-checkbox--single m-checkbox--solid m-checkbox--brand'><input type='checkbox' value='" + codigoTrabajador + "' class='m-checkable clickme'><span></span></label></td>";

                        contadorfilas += 1;
                    }
                }
                else
                {
                    html = "<p style='font-size: 20px;padding:30px'>Por el momento no hay trabajadores disponibles para asignarlos al centro de costos.</p>";
                }

                html += "</tbody></table>";
            }
            catch (Exception)
            {

            }
            finally
            {
                html += "</div></div></div>";
                tbListadoTrabajadores.InnerHtml = html;
            }
        }
        private void CargarProyectoPlanilla()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = procesoCentroCostos.CargarProyectoPlanilla();
                cboProyectoPlanilla.DataSource = dt;
                cboProyectoPlanilla.DataTextField = "DescripcionProyecto";
                cboProyectoPlanilla.DataValueField = "CodProyecto";
                cboProyectoPlanilla.DataBind();
                cboProyectoPlanilla.Items.Insert(0, "Seleccione");
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }
        private void LimpiarFormulario()
        {
            cboProyectoPlanilla.ClearSelection();
            txtPresupuesto.Text = string.Empty;
            txtEnlaceExterno.Text = string.Empty;
        }

       
    }
}