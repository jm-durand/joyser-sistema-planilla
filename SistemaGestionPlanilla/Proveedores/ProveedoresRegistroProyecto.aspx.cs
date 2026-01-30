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

namespace SistemaGestionPlanilla.Proveedores
{
    public partial class ProveedoresRegistroProyecto : System.Web.UI.Page
    {
        CentroCostosNegocio procesoCentroCostos = new CentroCostosNegocio();
        ProveedoresNegocio procesoProveedores = new ProveedoresNegocio();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["CodUsuarioInterno"] != null)
                {
                    this.txtTextoBuscar.Attributes.Add("onkeypress", "button_click(this,'" + this.btnBuscar.ClientID + "')");

                    gpConsultaProyectoPlanilla.Visible = true;
                    gpFormularioProyecto.Visible = false;
                    gpListadoProveedores.Visible = false;
                    gpListadoProveedoresAsignados.Visible = false;

                    BuscarProyectoPlanilla("");
                    CargarProyectoPlanilla();

                    Session["CodProyectoPlanillaProveedorModificar"] = "";
                }
                else
                {
                    Response.Redirect(ConfigurationManager.AppSettings["AssetsUrl"] + "/Seguridad/Logout.aspx");
                }
            }
        } 
        protected void btnAsignarProveedores_Click(object sender, EventArgs e)
        {
            try
            {
                LimpiarFormulario();
                gpFormularioProyecto.Visible = true;
                gpConsultaProyectoPlanilla.Visible = false;
                gpListadoProveedores.Visible = false;
                gpListadoProveedoresAsignados.Visible = false;

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

                    gpListadoProveedores.Visible = true;

                    ObtenerListadoProveedores(cboProyectoPlanilla.SelectedValue.ToString());
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
                gpListadoProveedores.Visible = false;
                gpListadoProveedoresAsignados.Visible = false;

                LimpiarFormulario();

                Session["CodProyectoPlanillaProveedorModificar"] = "";
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

                            mensaje = GuardarProyectoPlanillaProveedor(Convert.ToInt32(codigoProyecto), Convert.ToInt32(codParametro), Convert.ToInt32(Session["CodUsuarioInterno"].ToString()));
                        }

                        ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('Proveedores asignados al proyecto correctamente.','success');", true);

                        LimpiarFormulario();

                        gpFormularioProyecto.Visible = false;
                        gpConsultaProyectoPlanilla.Visible = true;

                        BuscarProyectoPlanilla("");

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('Debe seleccionar al menos un proveedor.','info');", true);
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
        protected void dgvProveedoresAsignados_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "DesactivarProveedorProyecto")
                {
                    string codProveedor = e.CommandArgument.ToString();
                    string mensaje = DesactivarProveedorProyectoPlanilla(Session["CodProyectoPlanillaProveedorModificar"].ToString(), codProveedor, Session["CodUsuarioInterno"].ToString());

                    if (mensaje == "EXITO")
                    {
                        ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('Proveedor retirado correctamente.','success');", true);
                        CargarProveedoresProyectoPlanilla(Session["CodProyectoPlanillaProveedorModificar"].ToString());
                        BuscarProyectoPlanilla("");
                    }
                    else if (mensaje == "PAGOVIGENTE")
                    {
                        ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('El proveedor no puede ser retirado debido a que contiene un pago vigente en el proyecto.','info');", true);
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
        protected void dgvProveedoresAsignados_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                dgvProveedoresAsignados.PageIndex = e.NewPageIndex;
                CargarProveedoresProyectoPlanilla(Session["CodProyectoPlanillaProveedorModificar"].ToString());
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
                string textoBuscar = txtTextoBuscar.Text.Trim();
                BuscarProyectoPlanilla(textoBuscar);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }
        /*---------------------------------------*/
        /*---------------METODOS-----------------*/
        /*---------------------------------------*/
        private string GuardarProyectoPlanillaProveedor(int codProyectoPlanilla, int codProveedor, int codUsuario)
        {
            return procesoCentroCostos.RegistrarProyectoPlanillaProveedor(codProyectoPlanilla, codProveedor, codUsuario);
        }
        private string DesactivarProveedorProyectoPlanilla(string codProyectoPlanilla, string codProveedor, string codUsuario)
        {
            return procesoCentroCostos.DesactivarProveedorProyectoPlanilla(Convert.ToInt32(codProyectoPlanilla), Convert.ToInt32(codProveedor), Convert.ToInt32(codUsuario));
        }
        private void BuscarProyectoPlanilla(string TextoBuscar)
        {
            DataTable dt = procesoCentroCostos.BuscarProyectoPlanillaProveedores(TextoBuscar);
            dgvProyectos.DataSource = dt;
            dgvProyectos.DataBind();
            lblResultadoBusqueda.InnerHtml = "" + dt.Rows.Count.ToString() + " proyectos(s) encontrado(s)";
        }
        private void CargarProveedoresProyectoPlanilla(string CodProyectoPlanilla)
        {
            DataTable dt = procesoCentroCostos.CargarProveedoresProyectoPlanilla(Convert.ToInt32(CodProyectoPlanilla));
            dgvProveedoresAsignados.DataSource = dt;
            dgvProveedoresAsignados.DataBind();
            lblResultadoAsignados.InnerHtml = "" + dt.Rows.Count.ToString() + " proveedor(es) encontrado(s)";
        }
        private void AbrirDetallesProyectoPlanilla(string CodProyectoPlanilla)
        {
            cboProyectoPlanilla.SelectedValue = CodProyectoPlanilla;

            DataTable dt = procesoCentroCostos.CargarDatosProyectoPlanilla(Convert.ToInt32(CodProyectoPlanilla));

            if (dt.Rows.Count > 0)
            {
                txtPresupuesto.Text = dt.Rows[0][2].ToString().Replace(",", ".");
            }

            CargarProveedoresProyectoPlanilla(CodProyectoPlanilla);

            Session["CodProyectoPlanillaProveedorModificar"] = CodProyectoPlanilla;

            btnGuardar.Visible = false;
            cboProyectoPlanilla.Enabled = false;

            gpFormularioProyecto.Visible = true;
            gpConsultaProyectoPlanilla.Visible = false;
            gpListadoProveedoresAsignados.Visible = true;
            gpListadoProveedores.Visible = false;
        }
        private void ObtenerListadoProveedores(string codProyectoPlanilla)
        {
            DataTable dt = procesoProveedores.ObtenerListadoProveedores(Convert.ToInt32(codProyectoPlanilla));
            string html = "<div class='ibox'><div class='ibox-content top-fixed'><div style='padding-top:20px'><input type='text' maxlength='200' class='form-control input-lg buscarproveedores' placeholder='Ingrese una parte del nombre del proveedor o ruc' onkeyup='BuscarProveedor();' onchange='BuscarProveedor()'></div><div class='table-responsive'><table class='table table-striped- table-bordered table-hover table-checkable' id='m_table_1'><thead><tr><th>#</th><th>RUC</th><th>Razón Social</th><th>Rubro</th><th>Selección</th></tr></thead>";
            try
            {
                html += "<tbody>";
                if (dt.Rows.Count > 0)
                {
                    int contadorfilas = 1;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string codProveedor = dt.Rows[i][0].ToString();
                        string ruc = dt.Rows[i][1].ToString();
                        string razonsocial = dt.Rows[i][2].ToString();
                        string rubro = dt.Rows[i][3].ToString();                        


                        html += "<tr id='" + contadorfilas + "'><td style='height:44px;'><div style='width:100%; height:100%; overflow-y: auto;'>" + contadorfilas + "</div></td>";
                        html += "<td style='text-align:left'>" + ruc + "</td>";
                        html += "<td>" + razonsocial + "</td>";
                        html += "<td>" + rubro + "</td>";

                        html += "<td style='text-align:center'><label class='m-checkbox m-checkbox--single m-checkbox--solid m-checkbox--brand'><input type='checkbox' value='" + codProveedor + "' class='m-checkable clickme'><span></span></label></td>";

                        contadorfilas += 1;
                    }
                }
                else
                {
                    html = "<p style='font-size: 20px;padding:30px'>Por el momento no hay proveedores disponibles para asignarlos al centro de costos.</p>";
                }

                html += "</tbody></table>";
            }
            catch (Exception)
            {

            }
            finally
            {
                html += "</div></div></div>";
                tbListadoProveedores.InnerHtml = html;
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