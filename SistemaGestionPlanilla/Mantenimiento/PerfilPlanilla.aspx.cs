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
    public partial class PerfilPlanilla : System.Web.UI.Page
    {
        MantenimientoNegocio procesoMantenimiento = new MantenimientoNegocio();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["CodUsuarioInterno"] != null)
                {
                    Session["CodPerfilPlanillaModificar"] = "";
                    gpFormularioPerfilPlanilla.Visible = false;
                    BuscarPerfilPlanilla("");
                }
                else
                {
                    Response.Redirect(ConfigurationManager.AppSettings["AssetsUrl"] + "/Seguridad/Logout.aspx");
                }               
            }
        }
        protected void btnAgregarPerfilPlanilla_ServerClick(object sender, EventArgs e)
        {
            try
            {
                gpConsultaPerfilPlanilla.Visible = false;
                gpFormularioPerfilPlanilla.Visible = true;
                CargarParametros();
                LimpiarRegistro();

                Session["CodPerfilPlanillaModificar"] = "";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            gpConsultaPerfilPlanilla.Visible = true;
            gpFormularioPerfilPlanilla.Visible = false;
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                int contadorCamposVacios = 0;
                int chckParametrosVacios = 0;

                string nombrePerfil = txtNombrePerfil.Text.ToString().Trim();
                string jornal = txtJornal.Text.ToString().Trim();
                string parametros = txtEnlaceExterno.Text.ToString().Trim();
                string parametrosValores = txtParametrosValores.Text.ToString().Trim();

                if (nombrePerfil == string.Empty) { contadorCamposVacios += 1; lblNombrePerfil.ForeColor = Color.Red; } else { lblNombrePerfil.ForeColor = ColorTranslator.FromHtml("#3f4047"); }
                if (jornal == string.Empty) { contadorCamposVacios += 1; lblJornal.ForeColor = Color.Red; } else { lblJornal.ForeColor = ColorTranslator.FromHtml("#3f4047"); }

                if (contadorCamposVacios == 0)
                {                    
                    string[] mensaje;
                    if (Session["CodPerfilPlanillaModificar"].ToString() == string.Empty)
                    {
                        mensaje = GuardarPerfilPlanilla(nombrePerfil, double.Parse(jornal, CultureInfo.InvariantCulture), Convert.ToInt32(Session["CodUsuarioInterno"].ToString())).Split('|');
                    }
                    else
                    {
                        mensaje = ModificarPerfilPlanilla(nombrePerfil, double.Parse(jornal, CultureInfo.InvariantCulture), Convert.ToInt32(Session["CodUsuarioInterno"].ToString()), Convert.ToInt32(Session["CodPerfilPlanillaModificar"].ToString())).Split('|');
                    }

                    if (mensaje[0] == "EXITO")
                    {
                        if (parametros != string.Empty)
                        {
                            string parametrosValido = parametros.Substring(1, parametros.Length - 1);
                            string[] Lista_Parametros = parametrosValido.Split(',');

                            for (int i = 0; i < Lista_Parametros.Length; i++)
                            {
                                string codPerfilPlanilla = mensaje[1].ToString();
                                string codParametro = Lista_Parametros[i].ToString().Trim();
                                string campoParametros = parametrosValores.Substring(1, parametrosValores.Length - 1);
                                string[] Lista_CampoParametros = campoParametros.Split(',');

                                string finalmente;

                                if (Session["CodPerfilPlanillaModificar"].ToString() == string.Empty)
                                {
                                    finalmente = GuardarParametroPerfilPlanilla(Convert.ToInt32(codPerfilPlanilla), Convert.ToInt32(codParametro), Lista_CampoParametros[i]);
                                }
                                else
                                {
                                    finalmente = ModificarParametroPerfilPlanilla(Convert.ToInt32(codPerfilPlanilla), Convert.ToInt32(codParametro), Lista_CampoParametros[i]);
                                }
                            }
                        }
                                               
                        if (Session["CodPerfilPlanillaModificar"].ToString() == string.Empty)
                        {
                            ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('Perfil Planilla creado correctamente.','success');", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('Perfil Planilla modificado correctamente.','info');", true);
                        }
                            
                        gpConsultaPerfilPlanilla.Visible = true;
                        gpFormularioPerfilPlanilla.Visible = false;
                        LimpiarRegistro();

                        BuscarPerfilPlanilla("");
                    }
                    else if (mensaje[0] == "REPETIDO")
                    {
                        ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('Ya existe en otro perfil con los mismos datos en la base de datos.','info');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + mensaje[0] + "','error');", true);
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
        protected void dgvPerfilPlanilla_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    if (e.Row.Cells[4].Text == "1")
                    {
                        e.Row.Cells[4].Text = "<span class='m-badge m-badge--success'></span>";
                    }
                    else if (e.Row.Cells[4].Text == "0")
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
        protected void dgvPerfilPlanilla_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "VerDetallePerfilPlanilla")
                {
                    string codPerfilPlanilla = e.CommandArgument.ToString();
                    CargarParametrosPerfilPlanilla(codPerfilPlanilla);
                    gpConsultaPerfilPlanilla.Visible = false;
                    gpFormularioPerfilPlanilla.Visible = true;
                }
                else if (e.CommandName == "EliminarPerfilPlanilla")
                {
                    string codPerfilPlanilla = e.CommandArgument.ToString();
                    string mensaje = EliminarPerfilPlanilla(Convert.ToInt32(codPerfilPlanilla), Convert.ToInt32(Session["CodUsuarioInterno"].ToString()));

                    if (mensaje == "EXITO")
                    {
                        ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('Perfil planilla eliminado correctamente.','success');", true);
                        BuscarPerfilPlanilla("");
                    }
                    else if (mensaje == "PERFILENUSO")
                    {
                        ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('El perfil de planilla se encuentra en uso, no puede ser eliminado.','info');", true);
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
        protected void dgvPerfilPlanilla_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                dgvPerfilPlanilla.PageIndex = e.NewPageIndex;

                if (txtTextoBuscarPapeleta.Text == string.Empty)
                {
                    BuscarPerfilPlanilla("");
                }
                else
                {
                    BuscarPerfilPlanilla(txtTextoBuscarPapeleta.Text.ToString().Trim());
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }
        /*------------METODOS--------------*/
        /*---------------------------------*/
        private void BuscarPerfilPlanilla(string TextoBuscar)
        {
            DataTable dt = procesoMantenimiento.BuscarPerfilPlanilla(TextoBuscar);
            dgvPerfilPlanilla.DataSource = dt;
            dgvPerfilPlanilla.DataBind();
            //if (TextoBuscar == string.Empty)
            //{
            //    lblResultadoBusqueda.InnerHtml = "<h2>" + dt.Rows.Count.ToString() + " resultado(s) encontrado(s)</h2>";
            //}
            //else
            //{
            //    lblResultadoBusqueda.InnerHtml = "<h2>" + dt.Rows.Count.ToString() + "</span> resultado(s) encontrado(s) para: <span class='text-primary'>" + TextoBuscar + "</span></h2>";
            //}
        }
        private void CargarParametros()
        {
            DataTable dt = new DataTable();
            dt = procesoMantenimiento.ObtenerDatosGeneral();
            string htmlGrupoParametros = string.Empty;

            if (dt.Rows.Count > 0)
            {
                int contadorfilas = 1;
                
                foreach (DataRow row in dt.Rows)
                {
                  
                    if (contadorfilas % 3 == 0)
                    {
                        htmlGrupoParametros += "<div class='col-lg-4'> <label class='m-option'> <span class='m-option__control'> <label class='m-checkbox m-checkbox--solid m-checkbox--state-success'> <input type='checkbox' value='" + row["CodParametro"].ToString() + "' class='clickme'> <span></span> </label> </span> <span class='m-option__label'> <span class='m-option__head' style='padding-top:5px'> <span class='m-option__title'>" + row["DescParametro"].ToString() + "</span> <span class='m-option__focus' style='position:absolute'><input id=txtParametro" + row["CodParametro"].ToString() + " class='form-control m-input perfil parametro" + row["CodParametro"].ToString()+ "' readonly='readonly'/></span> </span> <span class='m-option__body'>" + row["DescUnidadMedidaParametro"].ToString() + "</span> </span> </label> </div>";
                        htmlGrupoParametros += "</div>";
                        htmlGrupoParametros += "<div class='row'>";
                    }
                    else
                    {
                        if (contadorfilas == 1)
                        {
                            htmlGrupoParametros += "<div class='row'>";
                            htmlGrupoParametros += "<div class='col-lg-4'> <label class='m-option'> <span class='m-option__control'> <label class='m-checkbox m-checkbox--solid m-checkbox--state-success'> <input type='checkbox' value='" + row["CodParametro"].ToString() + "' class='clickme'> <span></span> </label> </span> <span class='m-option__label'> <span class='m-option__head' style='padding-top:5px'> <span class='m-option__title'>" + row["DescParametro"].ToString() + "</span> <span class='m-option__focus' style='position:absolute'><input id=txtParametro" + row["CodParametro"].ToString() + " class='form-control m-input perfil parametro" + row["CodParametro"].ToString() + "' readonly='readonly' /></span> </span> <span class='m-option__body'>" + row["DescUnidadMedidaParametro"].ToString() + "</span> </span> </label> </div>";
                        }
                        else
                        {
                            htmlGrupoParametros += "<div class='col-lg-4'> <label class='m-option'> <span class='m-option__control'> <label class='m-checkbox m-checkbox--solid m-checkbox--state-success'> <input type='checkbox' value='" + row["CodParametro"].ToString() + "' class='clickme'> <span></span> </label> </span> <span class='m-option__label'> <span class='m-option__head' style='padding-top:5px'> <span class='m-option__title'>" + row["DescParametro"].ToString() + "</span> <span class='m-option__focus' style='position:absolute'><input id=txtParametro" + row["CodParametro"].ToString() + " class='form-control m-input perfil parametro" + row["CodParametro"].ToString() + "' readonly='readonly'/></span> </span> <span class='m-option__body'>" + row["DescUnidadMedidaParametro"].ToString() + "</span> </span> </label> </div>";
                        }

                    }
                    
                   contadorfilas += 1;
                }

                gpParametros.InnerHtml = "</div>" + htmlGrupoParametros;
            }
        }
        private void CargarParametrosPerfilPlanilla(string codPerfilPlanilla)
        {
            LimpiarRegistro();
            DataTable dt = new DataTable();
            dt = procesoMantenimiento.ObtenerParametrosPerfilPlanilla(Convert.ToInt32(codPerfilPlanilla));

            txtNombrePerfil.Text = dt.Rows[0][5].ToString();
            txtJornal.Text = dt.Rows[0][6].ToString().Replace(",",".");
            string htmlGrupoParametros = string.Empty;
            string parametros = string.Empty;
            string valores = string.Empty;

            if (dt.Rows.Count > 0)
            {
                int contadorfilas = 1;

                foreach (DataRow row in dt.Rows)
                {
                    string valornumerico = row["CampoParametro"].ToString().Replace(",",".");
                    if (contadorfilas % 3 == 0)
                    {
                        if (row["FlagHabilitado"].ToString() == "1")
                        {
                            htmlGrupoParametros += "<div class='col-lg-4'> <label class='m-option'> <span class='m-option__control'> <label class='m-checkbox m-checkbox--solid m-checkbox--state-success'> <input type='checkbox' checked='checked' value='" + row["CodParametro"].ToString() + "' class='clickme'> <span></span> </label> </span> <span class='m-option__label'> <span class='m-option__head' style='padding-top:5px'> <span class='m-option__title'>" + row["DescParametro"].ToString() + "</span> <span class='m-option__focus' style='position:absolute'><input id=txtParametro" + row["CodParametro"].ToString() + " class='form-control m-input perfil parametro" + row["CodParametro"].ToString() + "' value='" + valornumerico + "'/></span> </span> <span class='m-option__body'>" + row["DescUnidadMedidaParametro"].ToString() + "</span> </span> </label> </div>";
                            parametros += "," + row["CodParametro"].ToString();
                            valores += "," + valornumerico;
                        }
                        else
                        {
                            htmlGrupoParametros += "<div class='col-lg-4'> <label class='m-option'> <span class='m-option__control'> <label class='m-checkbox m-checkbox--solid m-checkbox--state-success'> <input type='checkbox' value='" + row["CodParametro"].ToString() + "' class='clickme'> <span></span> </label> </span> <span class='m-option__label'> <span class='m-option__head' style='padding-top:5px'> <span class='m-option__title'>" + row["DescParametro"].ToString() + "</span> <span class='m-option__focus' style='position:absolute'><input id=txtParametro" + row["CodParametro"].ToString() + " class='form-control m-input perfil parametro" + row["CodParametro"].ToString() + "' readonly='readonly'/></span> </span> <span class='m-option__body'>" + row["DescUnidadMedidaParametro"].ToString() + "</span> </span> </label> </div>";
                        }
                        
                        htmlGrupoParametros += "</div>";
                        htmlGrupoParametros += "<div class='row'>";
                    }
                    else
                    {
                        if (contadorfilas == 1)
                        {
                            htmlGrupoParametros += "<div class='row'>";

                            if (row["FlagHabilitado"].ToString() == "1")
                            {
                                htmlGrupoParametros += "<div class='col-lg-4'> <label class='m-option'> <span class='m-option__control'> <label class='m-checkbox m-checkbox--solid m-checkbox--state-success'> <input type='checkbox' checked='checked' value='" + row["CodParametro"].ToString() + "' class='clickme'> <span></span> </label> </span> <span class='m-option__label'> <span class='m-option__head' style='padding-top:5px'> <span class='m-option__title'>" + row["DescParametro"].ToString() + "</span> <span class='m-option__focus' style='position:absolute'><input id=txtParametro" + row["CodParametro"].ToString() + " class='form-control m-input perfil parametro" + row["CodParametro"].ToString() + "' value='" + valornumerico + "' /></span> </span> <span class='m-option__body'>" + row["DescUnidadMedidaParametro"].ToString() + "</span> </span> </label> </div>";
                                parametros += "," + row["CodParametro"].ToString();
                                valores += "," + valornumerico;
                            }
                            else
                            {
                                htmlGrupoParametros += "<div class='col-lg-4'> <label class='m-option'> <span class='m-option__control'> <label class='m-checkbox m-checkbox--solid m-checkbox--state-success'> <input type='checkbox' value='" + row["CodParametro"].ToString() + "' class='clickme'> <span></span> </label> </span> <span class='m-option__label'> <span class='m-option__head' style='padding-top:5px'> <span class='m-option__title'>" + row["DescParametro"].ToString() + "</span> <span class='m-option__focus' style='position:absolute'><input id=txtParametro" + row["CodParametro"].ToString() + " class='form-control m-input perfil parametro" + row["CodParametro"].ToString() + "' readonly='readonly' /></span> </span> <span class='m-option__body'>" + row["DescUnidadMedidaParametro"].ToString() + "</span> </span> </label> </div>";
                            }                            
                        }
                        else
                        {
                            if (row["FlagHabilitado"].ToString() == "1")
                            {
                                htmlGrupoParametros += "<div class='col-lg-4'> <label class='m-option'> <span class='m-option__control'> <label class='m-checkbox m-checkbox--solid m-checkbox--state-success'> <input type='checkbox' checked='checked' value='" + row["CodParametro"].ToString() + "' class='clickme'> <span></span> </label> </span> <span class='m-option__label'> <span class='m-option__head' style='padding-top:5px'> <span class='m-option__title'>" + row["DescParametro"].ToString() + "</span> <span class='m-option__focus' style='position:absolute'><input id=txtParametro" + row["CodParametro"].ToString() + " class='form-control m-input perfil parametro" + row["CodParametro"].ToString() + "' value='" + valornumerico + "'/></span> </span> <span class='m-option__body'>" + row["DescUnidadMedidaParametro"].ToString() + "</span> </span> </label> </div>";
                                parametros += "," + row["CodParametro"].ToString();
                                valores += "," + valornumerico;
                            }
                            else
                            {
                                htmlGrupoParametros += "<div class='col-lg-4'> <label class='m-option'> <span class='m-option__control'> <label class='m-checkbox m-checkbox--solid m-checkbox--state-success'> <input type='checkbox' value='" + row["CodParametro"].ToString() + "' class='clickme'> <span></span> </label> </span> <span class='m-option__label'> <span class='m-option__head' style='padding-top:5px'> <span class='m-option__title'>" + row["DescParametro"].ToString() + "</span> <span class='m-option__focus' style='position:absolute'><input id=txtParametro" + row["CodParametro"].ToString() + " class='form-control m-input perfil parametro" + row["CodParametro"].ToString() + "' readonly='readonly'/></span> </span> <span class='m-option__body'>" + row["DescUnidadMedidaParametro"].ToString() + "</span> </span> </label> </div>";
                            }                                
                        }
                    }

                    contadorfilas += 1;
                }

                gpParametros.InnerHtml = "</div>" + htmlGrupoParametros;
                txtEnlaceExterno.Text = parametros;
                txtParametrosValores.Text = valores;                
            }
            Session["CodPerfilPlanillaModificar"] = codPerfilPlanilla;
        }
        private string GuardarPerfilPlanilla(string nombrePerfilPlanilla, double jornal, int codUsuario)
        {
            return procesoMantenimiento.RegistrarPerfilPlanilla(nombrePerfilPlanilla, jornal, codUsuario);
        }
        private string ModificarPerfilPlanilla(string nombrePerfilPlanilla, double jornal, int codUsuario, int codPerfilPlanilla)
        {
            return procesoMantenimiento.ActualizarPerfilPlanilla(nombrePerfilPlanilla, jornal, codUsuario, codPerfilPlanilla);
        }
        private string EliminarPerfilPlanilla(int codPerfilPlanilla, int codUsuario)
        {
            return procesoMantenimiento.EliminarPerfilPlanilla(codPerfilPlanilla, codUsuario);
        }
        private string GuardarParametroPerfilPlanilla(int codPerfilPlanilla, int codParametro, string campoParametro)
        {
            return procesoMantenimiento.RegistrarParametroPerfilPlanilla(codPerfilPlanilla, codParametro, campoParametro);
        }
        private string ModificarParametroPerfilPlanilla(int codPerfilPlanilla, int codParametro, string campoParametro)
        {
            return procesoMantenimiento.ActualizarParametroPerfilPlanilla(codPerfilPlanilla, codParametro, campoParametro);
        }
        private void LimpiarRegistro()
        {
            txtNombrePerfil.Text = string.Empty;
            txtJornal.Text = string.Empty;
            txtEnlaceExterno.Text = string.Empty;
            txtParametrosValores.Text = string.Empty;
        }

       
    }
}