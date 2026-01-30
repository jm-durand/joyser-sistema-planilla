using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogicaNegocio;
using LogicaNegocio.SistemaGestionPlanilla;
using Entidad;
using Entidad.SistemaGestionPlanilla;
using System.Configuration;

namespace SistemaGestionPlanilla.Seguridad
{
    public partial class Login : System.Web.UI.Page
    {
        UsuarioPlanillaNegocio procesoLogin = new UsuarioPlanillaNegocio();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.txtContrasema.Attributes.Add("onkeypress", "button_click(this,'" + this.btnIngresar.ClientID + "')");
                gpMensaje.Visible = false;

                string IdUsuario = Request["IdUsuarioGeneral"];

                if (IdUsuario != null)
                {
                    Ingresar(IdUsuario);
                }
                else
                {
                    Response.Redirect(ConfigurationManager.AppSettings["AssetsUrl"] + "/Seguridad/Logout.aspx");
                }
            }                
        }
        protected void btnIngresar_Click(object sender, EventArgs e)
        {
            try
            {
                string usuario = txtUsuario.Text.Trim();
                string contrasena = txtContrasema.Text.Trim();
                int contadorcamposvacios = 0;

                if (usuario == string.Empty) { contadorcamposvacios = 1; }
                if (contrasena == string.Empty) { contadorcamposvacios = 1; }

                if (contadorcamposvacios == 0)
                {
                    UsuarioPlanillaEntidad usuarioEntidad = new UsuarioPlanillaEntidad();
                    usuarioEntidad.Usuario = usuario;
                    usuarioEntidad.Contrasena = contrasena;

                    DataTable dt = procesoLogin.Login(usuarioEntidad);

                    if (dt.Columns.Count > 1)
                    {
                        if (Convert.ToBoolean(dt.Rows[0][2]) == true)
                        {
                            Session["CodUsuarioInterno"] = dt.Rows[0][0].ToString();
                            Session["UsuarioInterno"] = dt.Rows[0][1].ToString();
                            Session["NombreCompletoUsuarioInterno"] = dt.Rows[0][3].ToString();
                            Session["CodPerfilAccesoUsuarioInterno"] = dt.Rows[0][4].ToString();

                            Response.Redirect("/Inicio.aspx");
                        }
                        else
                        {
                            MostrarMensaje("Este usuario se encuentra inactivo", "info");
                        }
                    }
                    else
                    {
                        MostrarMensaje(dt.Rows[0][0].ToString(), "info");
                    }
                }
                else
                {
                    MostrarMensaje("Debe ingresar su usuario y contraseña para ingresar a la plataforma", "info");
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje(ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", ""), "info");
            }
        }
        private void Ingresar(string IdUsuario)
        {
            try
            {
                DataTable dt = procesoLogin.NuevoAcceso(Convert.ToInt32(IdUsuario));

                if (dt.Rows.Count > 0)
                {
                    Session["CodUsuarioInterno"] = dt.Rows[0][0].ToString();
                    Session["UsuarioInterno"] = dt.Rows[0][1].ToString();
                    Session["NombreCompletoUsuarioInterno"] = dt.Rows[0][3].ToString();
                    Session["CodPerfilAccesoUsuarioInterno"] = dt.Rows[0][4].ToString();

                    Response.Redirect(ConfigurationManager.AppSettings["AssetsUrl"] + "/Inicio.aspx");
                }
                else
                {
                    Response.Redirect(ConfigurationManager.AppSettings["AssetsUrl"] + "/Seguridad/Logout.aspx");
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje(ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", ""), "info");
            }
        }
        private void LimpiarFormulario()
        {
            txtUsuario.Text = string.Empty;
            txtContrasema.Text = string.Empty;        
        }
        private void MostrarMensaje(string mensaje, string tipo)
        {
            if (tipo == "info")
            {
                gpMensaje.Visible = true;
                gpMensaje.InnerHtml = "<div class='alert m-alert--outline alert alert-primary alert-dismissable' style='padding:20px;'><button type='button' class='close' data-dismiss='alert' aria-hidden='true'></button><strong>" + mensaje + "</strong></div>";
            }
            else if (tipo == "success")
            {
                gpMensaje.Visible = true;
                gpMensaje.InnerHtml = "<div class='alert m-alert--outline alert alert-success alert-dismissable' style='padding:20px;'><button type='button' class='close' data-dismiss='alert' aria-hidden='true'></button><strong>¡Listo! " + mensaje + "</strong></div>";
            }
            else if (tipo == "ocultar")
            {
                gpMensaje.Visible = false;
            }
        }

       
    }
}