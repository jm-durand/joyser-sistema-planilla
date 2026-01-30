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
using System.Configuration;

namespace SistemaGestionPlanilla.Proveedores
{
    public partial class ProveedoresRegistrar : System.Web.UI.Page
    {
        ProveedoresNegocio procesoProveedores = new ProveedoresNegocio();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["CodUsuarioInterno"] != null)
                {
                    this.txtTextoBuscar.Attributes.Add("onkeypress", "button_click(this,'" + this.btnBuscar.ClientID + "')");
                    txtTextoBuscar.Focus();
                    CargarRubro();
                    Session["CodProveedorModificar"] = "";
                    gpConsultaProveedores.Visible = true;
                    gpFormularioProveedores.Visible = false;
                    BuscarProveedor("");
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
                BuscarProveedor(txtTextoBuscar.Text.Trim());
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

                gpFormularioProveedores.Visible = true;
                gpConsultaProveedores.Visible = false;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }
        protected void dgvProveedores_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string codProveedor = e.CommandArgument.ToString();

                if (e.CommandName == "VerDetalleProveedor")
                {
                    CargarDatosProveedor(codProveedor);
                }
                else if (e.CommandName == "EliminarContratista")
                {
                    string mensaje = EliminarProveedor(Convert.ToInt32(codProveedor), Convert.ToInt32(Session["CodUsuarioInterno"].ToString()));

                    if (mensaje == "EXITO")
                    {
                        ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('Contratista eliminado correctamente.','success');", true);

                        BuscarProveedor("");
                    }
                    else if (mensaje == "CONTRATOVIGENTE")
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
        protected void dgvProveedores_RowDataBound(object sender, GridViewRowEventArgs e)
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

        protected void dgvProveedores_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                dgvProveedores.PageIndex = e.NewPageIndex;

                if (txtTextoBuscar.Text == string.Empty)
                {
                    BuscarProveedor("");
                }
                else
                {
                    BuscarProveedor(txtTextoBuscar.Text.ToString().Trim());
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
                gpConsultaProveedores.Visible = true;
                gpFormularioProveedores.Visible = false;

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
                string descripcionProveedor = txtDescripcionProveedor.Text.Trim();
                string ruc = txtRucProveedor.Text.Trim();
                string codrubro = cboRubroProveedores.SelectedValue.ToString();
                string ciudad = txtCiudadProveedor.Text.Trim();
                string direccion = txtDireccionProveedor.Text.Trim();
                string correo = txtCorreoProveedor.Text.Trim();
                string telefono = txtTelefonoProveedor.Text.Trim();

                if (descripcionProveedor == string.Empty) { contadorCamposVacios += 1; lblDescripcionProveedor.ForeColor = Color.Red; } else { lblDescripcionProveedor.ForeColor = ColorTranslator.FromHtml("#3f4047"); }
                if (ruc == string.Empty) { contadorCamposVacios += 1; lblRucProveedor.ForeColor = Color.Red; } else { lblRucProveedor.ForeColor = ColorTranslator.FromHtml("#3f4047"); }
                if (codrubro == string.Empty) { contadorCamposVacios += 1; lblRubroProveedores.ForeColor = Color.Red; } else { lblRubroProveedores.ForeColor = ColorTranslator.FromHtml("#3f4047"); }
                if (ciudad == string.Empty) { contadorCamposVacios += 1; lblCiudadProveedor.ForeColor = Color.Red; } else { lblCiudadProveedor.ForeColor = ColorTranslator.FromHtml("#3f4047"); }
                if (direccion == string.Empty) { contadorCamposVacios += 1; lblDireccionProveedor.ForeColor = Color.Red; } else { lblDireccionProveedor.ForeColor = ColorTranslator.FromHtml("#3f4047"); }
                if (correo == string.Empty) { contadorCamposVacios += 1; lblCorreoProveedor.ForeColor = Color.Red; } else { lblCorreoProveedor.ForeColor = ColorTranslator.FromHtml("#3f4047"); }
                if (telefono == string.Empty) { contadorCamposVacios += 1; lblTelefonoProveedor.ForeColor = Color.Red; } else { lblTelefonoProveedor.ForeColor = ColorTranslator.FromHtml("#3f4047"); }

                if (contadorCamposVacios == 0)
                {
                    string mensaje;

                    if (Session["CodProveedorModificar"].ToString() == string.Empty)
                    {
                        mensaje = GuardarProveedor(descripcionProveedor, ruc, codrubro, ciudad, direccion, correo, telefono);
                    }
                    else
                    {
                        mensaje = ModificarProveedor(descripcionProveedor, ruc, codrubro, ciudad, direccion, correo, telefono, Session["CodProveedorModificar"].ToString()); ;
                    }

                    if (mensaje == "EXITO")
                    {

                        ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('Proveedor creado correctamente.','success');", true);

                        LimpiarFormulario();

                        gpFormularioProveedores.Visible = false;
                        gpConsultaProveedores.Visible = true;

                        BuscarProveedor("");

                    }
                    else if (mensaje == "REPETIDO")
                    {
                        ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('Ya existe otro proveedor con el mismo nombre y ruc.','info');", true);
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
        private void BuscarProveedor(string TextoBuscar)
        {
            DataTable dt = procesoProveedores.BuscarProveedores(TextoBuscar);
            dgvProveedores.DataSource = dt;
            dgvProveedores.DataBind();
            lblResultadoBusqueda.InnerHtml = "" + dt.Rows.Count.ToString() + " registros(s) encontrado(s)";
        }
        private string GuardarProveedor(string descripcionProveedor, string ruc, string codrubro, string ciudad, string direccion, string correo, string telefono)
        {
            ProveedoresEntidad proveedoresEntidad = new ProveedoresEntidad();
            proveedoresEntidad.DescripcionProveedor = descripcionProveedor;
            proveedoresEntidad.Ruc = ruc;
            proveedoresEntidad.CodRubroProveedor = Convert.ToInt32(codrubro);
            proveedoresEntidad.Ciudad = ciudad;
            proveedoresEntidad.Direccion = direccion;
            proveedoresEntidad.Correo = correo;
            proveedoresEntidad.Telefono = telefono;
            proveedoresEntidad.CodUsuarioRegistro = Convert.ToInt32(Session["CodUsuarioInterno"].ToString());
            return procesoProveedores.RegistrarProveedor(proveedoresEntidad);
        }
        private string ModificarProveedor(string descripcionProveedor, string ruc, string codrubro, string ciudad, string direccion, string correo, string telefono, string codProveedor)
        {
            ProveedoresEntidad proveedoresEntidad = new ProveedoresEntidad();
            proveedoresEntidad.DescripcionProveedor = descripcionProveedor;
            proveedoresEntidad.Ruc = ruc;
            proveedoresEntidad.CodRubroProveedor = Convert.ToInt32(codrubro);
            proveedoresEntidad.Ciudad = ciudad;
            proveedoresEntidad.Direccion = direccion;
            proveedoresEntidad.Correo = correo;
            proveedoresEntidad.Telefono = telefono;
            proveedoresEntidad.CodUsuarioModifica = Convert.ToInt32(Session["CodUsuarioInterno"].ToString());
            proveedoresEntidad.CodProveedor = Convert.ToInt32(codProveedor);
            return procesoProveedores.ActualizarProveedor(proveedoresEntidad);
        }
        private string EliminarProveedor(int codProveedor, int codUsuarioModifica)
        {
            return procesoProveedores.EliminarProveedor(codProveedor, codUsuarioModifica);
        }
        private void CargarDatosProveedor(string codProveedor)
        {
            DataTable dt = procesoProveedores.CargarDatosProveedor(Convert.ToInt32(codProveedor));

            if (dt.Rows.Count > 0)
            {
                LimpiarFormulario();

                txtDescripcionProveedor.Text = dt.Rows[0][1].ToString();
                txtRucProveedor.Text = dt.Rows[0][2].ToString();             
                txtCiudadProveedor.Text = dt.Rows[0][4].ToString();
                txtDireccionProveedor.Text = dt.Rows[0][5].ToString();
                txtCorreoProveedor.Text = dt.Rows[0][6].ToString();
                txtTelefonoProveedor.Text = dt.Rows[0][7].ToString();

                if (dt.Rows[0][3].ToString() != string.Empty) { cboRubroProveedores.SelectedValue = dt.Rows[0][3].ToString(); }
                
                gpFormularioProveedores.Visible = true;
                gpConsultaProveedores.Visible = false;

                Session["CodProveedorModificar"] = codProveedor;
            }
        }
        private void CargarRubro()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = procesoProveedores.CargarRubro();
                cboRubroProveedores.DataSource = dt;
                cboRubroProveedores.DataTextField = "DescripcionRubro";
                cboRubroProveedores.DataValueField = "CodRubro";
                cboRubroProveedores.DataBind();
                cboRubroProveedores.Items.Insert(0, new ListItem("Seleccione", "0"));
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }
        private void LimpiarFormulario()
        {
            txtDescripcionProveedor.Text = string.Empty;
            txtRucProveedor.Text = string.Empty;
            cboRubroProveedores.ClearSelection();
            txtCiudadProveedor.Text = string.Empty;
            txtDireccionProveedor.Text = string.Empty;
            txtCorreoProveedor.Text = string.Empty;
            txtTelefonoProveedor.Text = string.Empty;
        }     
    }
}