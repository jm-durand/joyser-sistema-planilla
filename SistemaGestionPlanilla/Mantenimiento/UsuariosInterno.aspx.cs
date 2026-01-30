using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogicaNegocio;
using LogicaNegocio.SistemaGestionPlanilla;
using Entidad;
using Entidad.SistemaGestionPlanilla;
using System.Configuration;

namespace SistemaGestionPlanilla.Mantenimiento
{
    public partial class UsuariosInterno : System.Web.UI.Page
    {
        UsuarioPlanillaNegocio procesosUsuario = new UsuarioPlanillaNegocio();
        UsuarioGeneralNegocio procesoUsuarioGeneral = new UsuarioGeneralNegocio();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["CodUsuarioInterno"] != null)
                {
                    this.txtTextoBuscarUsuario.Attributes.Add("onkeypress", "button_click(this,'" + this.btnBuscar.ClientID + "')");
                    txtTextoBuscarUsuario.Focus();
                    BuscarUsuarioInterno("");

                    CargarPerfilAcceso();

                    gpFormularioUsuarioInterno.Visible = false;
                    gpConsultaUsuario.Visible = true;

                    Session["CodUsuarioModificar"] = "";
                    Session["FlagModificarContrasena"] = "";
                }
                else
                {
                    Response.Redirect(ConfigurationManager.AppSettings["AssetsUrl"] + "/Seguridad/Logout.aspx");
                }              
            }
        }
        protected void btnAgregarUsuario_ServerClick(object sender, EventArgs e)
        {
            try
            {                
                LimpiarFormulario();               

                gpFormularioUsuarioInterno.Visible = true;
                gpConsultaUsuario.Visible = false;

                gpCheckCambiarContrasena.Visible = false;
                txtContrasena.Enabled = true;
                txtRepiteContrasena.Enabled = true;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }
        protected void dgvUsuariosInterno_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "ModificarUsuario")
                {
                    string codUsuarioInterno = e.CommandArgument.ToString();

                    gpFormularioUsuarioInterno.Visible = true;
                    gpConsultaUsuario.Visible = false;

                    CargarDatosUsuario(codUsuarioInterno);
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
                BuscarUsuarioInterno(txtTextoBuscarUsuario.Text.Trim());
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }
        protected void dgvUsuariosInterno_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    if (e.Row.Cells[3].Text == "True")
                    {
                        e.Row.Cells[3].Text = "<span class='m-badge m-badge--success'></span>";
                    }
                    else if (e.Row.Cells[3].Text == "False")
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
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                int contadorCamposVacios = 0;
                string usuario = txtUsuario.Text.Trim();
                string nombres = txtNombres.Text.Trim();
                string apellidoPaterno = txtApellidoPaterno.Text.Trim();
                string apellidoMaterno = txtApellidoMaterno.Text.Trim();
                string perfilAcceso = cboPerfilAcceso.SelectedValue.ToString();
                string estado = cboEstado.SelectedValue.ToString();
                string contrasena = txtContrasena.Text.Trim();
                string repitecontrasena = txtRepiteContrasena.Text.Trim();

                if (usuario == string.Empty) { contadorCamposVacios += 1; lblUsuario.ForeColor = Color.Red; } else { lblUsuario.ForeColor = ColorTranslator.FromHtml("#3f4047"); }
                if (nombres == string.Empty) { contadorCamposVacios += 1; lblNombres.ForeColor = Color.Red; } else { lblNombres.ForeColor = ColorTranslator.FromHtml("#3f4047"); }
                if (apellidoPaterno == string.Empty) { contadorCamposVacios += 1; lblApellidoPaterno.ForeColor = Color.Red; } else { lblApellidoPaterno.ForeColor = ColorTranslator.FromHtml("#3f4047"); }
                if (apellidoMaterno == string.Empty) { contadorCamposVacios += 1; lblApellidoMaterno.ForeColor = Color.Red; } else { lblApellidoMaterno.ForeColor = ColorTranslator.FromHtml("#3f4047"); }
                if (perfilAcceso == "Seleccione") { contadorCamposVacios += 1; lblPerfilAcceso.ForeColor = Color.Red; } else { lblPerfilAcceso.ForeColor = ColorTranslator.FromHtml("#3f4047"); }                

                if (Session["CodUsuarioModificar"].ToString() == string.Empty)
                {
                    if (contrasena == string.Empty) { contadorCamposVacios += 1; lblContrasena.ForeColor = Color.Red; } else { lblContrasena.ForeColor = ColorTranslator.FromHtml("#3f4047"); }
                    if (repitecontrasena == string.Empty) { contadorCamposVacios += 1; lblRepiteContrasena.ForeColor = Color.Red; } else { lblRepiteContrasena.ForeColor = ColorTranslator.FromHtml("#3f4047"); }
                }
                else
                {
                    if (chkCambiarContrasena.Checked)
                    {
                        if (contrasena == string.Empty) { contadorCamposVacios += 1; lblContrasena.ForeColor = Color.Red; } else { lblContrasena.ForeColor = ColorTranslator.FromHtml("#3f4047"); }
                        if (repitecontrasena == string.Empty) { contadorCamposVacios += 1; lblRepiteContrasena.ForeColor = Color.Red; } else { lblRepiteContrasena.ForeColor = ColorTranslator.FromHtml("#3f4047"); }
                    }
                    else
                    {
                        Session["FlagModificarContrasena"] = "0";
                    }
                }

                if (contadorCamposVacios == 0)
                {
                    if (contrasena == repitecontrasena)
                    {
                        string[] mensaje;
                        string finalmente;

                        mensaje = GuardarUsuarioGeneral(usuario, nombres, apellidoPaterno, apellidoMaterno, estado, contrasena, perfilAcceso, "1", Session["FlagModificarContrasena"].ToString()).Split('|');                                                                                             

                        if (mensaje[0] == "EXITO")
                        {
                            if (Session["CodUsuarioModificar"].ToString() == string.Empty)
                            {
                                finalmente = GuardarUsuario(usuario, nombres, apellidoPaterno, apellidoMaterno, perfilAcceso, estado, contrasena, mensaje[1].ToString());
                            }
                            else
                            {
                                finalmente = ActualizarUsuario(usuario, nombres, apellidoPaterno, apellidoMaterno, perfilAcceso, estado, contrasena, Session["CodUsuarioModificar"].ToString(), Session["FlagModificarContrasena"].ToString());
                            }
                                
                            if (finalmente == "EXITO")
                            {
                                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('Usuario correctamente creado.','success');", true);

                                LimpiarFormulario();

                                gpFormularioUsuarioInterno.Visible = false;
                                gpConsultaUsuario.Visible = true;

                                BuscarUsuarioInterno("");

                                Session["FlagModificarContrasena"] = "";
                            }
                            else if (finalmente == "REPETIDO")
                            {
                                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('Ya existe otro usuario con el mismo usuario, por favor intente otro nombre.','info');", true);
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + finalmente + "','error');", true);
                            }
                        }                                            
                        else
                        {
                            ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + mensaje + "','error');", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('Las contraseñas no coinciden, por favor vuelva a intentarlo.','info');", true);
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

        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            try
            {
                LimpiarFormulario();

                gpFormularioUsuarioInterno.Visible = false;
                gpConsultaUsuario.Visible = true;

                gpCheckCambiarContrasena.Visible = false;
                txtContrasena.Enabled = true;
                txtRepiteContrasena.Enabled = true;

                Session["FlagModificarContrasena"] = "";
                Session["CodUsuarioModificar"] = "";
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }
        protected void HabilitarContrasena_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkCambiarContrasena.Checked)
                {
                    txtContrasena.Enabled = true;
                    txtRepiteContrasena.Enabled = true;

                    Session["FlagModificarContrasena"] = "1";
                }
                else
                {
                    txtContrasena.Enabled = false;
                    txtRepiteContrasena.Enabled = false;

                    Session["FlagModificarContrasena"] = "0";
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
        private string GuardarUsuarioGeneral(string usuario, string nombres, string apellidoPaterno, string apellidoMaterno, string flagActivo, string contrasena, string codPerfilAcceso, string codSistema, string flagModificarContrasena)
        {
            UsuarioGeneralEntidad usuarioGeneralEntidad = new UsuarioGeneralEntidad();
            usuarioGeneralEntidad.Usuario = usuario;
            usuarioGeneralEntidad.Nombres = nombres;
            usuarioGeneralEntidad.Apellidos = apellidoPaterno + " " + apellidoMaterno;            
            usuarioGeneralEntidad.Contrasena = contrasena;
            usuarioGeneralEntidad.CodPerfilAcceso = Convert.ToInt32(codPerfilAcceso); 
            usuarioGeneralEntidad.CodSistema = Convert.ToInt32(codSistema); 
            usuarioGeneralEntidad.Estado = Convert.ToInt32(flagActivo);
            usuarioGeneralEntidad.FlagModificarContrasena = Convert.ToInt32(flagModificarContrasena);

            return procesoUsuarioGeneral.RegistrarUsuarioGeneral(usuarioGeneralEntidad);
        }
        private string GuardarUsuario(string usuario, string nombres, string apellidoPaterno, string apellidoMaterno, string codPerfilAcceso, string flagActivo, string contrasena, string codUsuarioGeneral)
        {
            UsuarioPlanillaEntidad usuarioEntidad = new UsuarioPlanillaEntidad();
            usuarioEntidad.Usuario = usuario;
            usuarioEntidad.Nombres = nombres;
            usuarioEntidad.ApellidoPaterno = apellidoPaterno;
            usuarioEntidad.ApellidoMaterno = apellidoMaterno;
            usuarioEntidad.CodPerfilAcceso = Convert.ToInt32(codPerfilAcceso);
            usuarioEntidad.Estado = Convert.ToInt32(flagActivo);
            usuarioEntidad.Contrasena = contrasena;
            usuarioEntidad.CodUsuarioGeneral = Convert.ToInt32(codUsuarioGeneral);

            return procesosUsuario.RegistrarUsuarioInterno(usuarioEntidad);
        }
        private string ActualizarUsuario(string usuario, string nombres, string apellidoPaterno, string apellidoMaterno, string codPerfilAcceso, string flagActivo, string contrasena, string codUsuario, string flagCambiarContrasena)
        {
            UsuarioPlanillaEntidad usuarioEntidad = new UsuarioPlanillaEntidad();
            usuarioEntidad.Usuario = usuario;
            usuarioEntidad.Nombres = nombres;
            usuarioEntidad.ApellidoPaterno = apellidoPaterno;
            usuarioEntidad.ApellidoMaterno = apellidoMaterno;
            usuarioEntidad.CodPerfilAcceso = Convert.ToInt32(codPerfilAcceso);
            usuarioEntidad.Estado = Convert.ToInt32(flagActivo);
            usuarioEntidad.Contrasena = contrasena;
            usuarioEntidad.CodUsuario = Convert.ToInt32(codUsuario);

            return procesosUsuario.ActualizarUsuarioInterno(usuarioEntidad, Convert.ToInt32(flagCambiarContrasena));
        }
        private void CargarDatosUsuario(string codUsuario)
        {
            DataTable dt = procesosUsuario.CargarDatosUsuarioInterno(Convert.ToInt32(codUsuario));

            if (dt.Rows.Count > 0)
            {
                LimpiarFormulario();

                txtUsuario.Text = dt.Rows[0][0].ToString();
                txtNombres.Text = dt.Rows[0][1].ToString();
                txtApellidoPaterno.Text = dt.Rows[0][2].ToString().Split(' ')[0];
                txtApellidoMaterno.Text = dt.Rows[0][2].ToString().Split(' ')[1];
                cboPerfilAcceso.SelectedValue = dt.Rows[0][3].ToString();
                cboEstado.SelectedValue = dt.Rows[0][4].ToString();

                gpCheckCambiarContrasena.Visible = true;
                chkCambiarContrasena.Checked = false;
                txtContrasena.Enabled = false;
                txtRepiteContrasena.Enabled = false;

                Session["CodUsuarioModificar"] = codUsuario;
            }
        }
        private void BuscarUsuarioInterno(string TextoBuscar)
        {
            DataTable dt = procesosUsuario.BuscarUsuarioInterno(TextoBuscar);
            dgvUsuariosInterno.DataSource = dt;
            dgvUsuariosInterno.DataBind();
            lblResultadoBusqueda.InnerHtml = "" + dt.Rows.Count.ToString() + " registros(s) encontrado(s)";
        }
        private void CargarPerfilAcceso()
        {
            try
            {
                DataTable dt = procesosUsuario.CargarPerfilAcceso();
                cboPerfilAcceso.DataSource = dt;
                cboPerfilAcceso.DataTextField = "DescPerfilAcceso";
                cboPerfilAcceso.DataValueField = "CodPerfilAcceso";
                cboPerfilAcceso.DataBind();
                cboPerfilAcceso.Items.Insert(0, "Seleccione");
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }
        private void LimpiarFormulario()
        {
            txtUsuario.Text = string.Empty;
            txtNombres.Text = string.Empty;
            txtApellidoPaterno.Text = string.Empty;
            txtApellidoMaterno.Text = string.Empty;
            cboPerfilAcceso.ClearSelection();
            cboEstado.ClearSelection();
            txtContrasena.Text = string.Empty;
            txtRepiteContrasena.Text = string.Empty;
            chkCambiarContrasena.Checked = false;
        }

       
    }
}