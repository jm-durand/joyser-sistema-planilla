using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogicaNegocio;
using Entidad;
using System.Configuration;

namespace SistemaGestionPlanilla.Trabajador
{
    public partial class RegistroEventuales : System.Web.UI.Page
    {
        TrabajadorNegocio procesoTrabajador = new TrabajadorNegocio();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["CodUsuarioInterno"] != null)
                {
                    this.txtTextoBuscar.Attributes.Add("onkeypress", "button_click(this,'" + this.btnBuscar.ClientID + "')");                 
                    Session["CodEventualModificar"] = "";
                    CargarPerfilPlanilla();
                    CargarLaborTrabajador();
                    BuscarUsuarioInterno("");
                    gpWizardAgregarTrabajador.Visible = false;

                    lblFormularioTrabajador.Text = "Creación de nuevos trabajadores eventuales";
                }
                else
                {
                    Response.Redirect(ConfigurationManager.AppSettings["AssetsUrl"] + "/Seguridad/Logout.aspx");
                }
            }
        }
        protected void btnAgregarTrabajador_ServerClick(object sender, EventArgs e)
        {
            try
            {
                gpConsultaTrabajador.Visible = false;
                gpWizardAgregarTrabajador.Visible = true;
                LimpiarFormulario();

                lblFormularioTrabajador.Text = "Creación de nuevos trabajadores";

                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Limpiar", "NuevoRegistro();", true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }
        protected void btnRegresarConsultaTrabajador_Click(object sender, EventArgs e)
        {
            try
            {
                gpConsultaTrabajador.Visible = true;
                gpWizardAgregarTrabajador.Visible = false;
                LimpiarFormulario();

                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Limpiar", "NuevoRegistro();", true);

                Session["CodEventualModificar"] = "";

                lblFormularioTrabajador.Text = "Creación de nuevos trabajadores eventuales";
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }
        protected void dgvTrabajadoresPlanilla_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "VerDetalleTrabajador")
                {
                    string codTrabajador = e.CommandArgument.ToString();
                    AbrirDetallesTrabajador(codTrabajador, "2");
                }
                else if (e.CommandName == "ModificarTrabajador")
                {
                    Session["CodEventualModificar"] = e.CommandArgument.ToString();
                    AbrirDetallesTrabajador(Session["CodEventualModificar"].ToString(), "2");
                }
                else if (e.CommandName == "EliminarTrabajador")
                {
                    Session["CodTrabajadorEliminar"] = e.CommandArgument.ToString();
                    ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "OpenModal", "abrirModalEliminarTrabajador();", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }
        protected void dgvTrabajadoresPlanilla_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    if (e.Row.Cells[5].Text == "1")
                    {
                        e.Row.Cells[5].Text = "<span class='m-badge m-badge--success'></span>";
                    }
                    else if (e.Row.Cells[5].Text == "0")
                    {
                        e.Row.Cells[5].Text = "<span class='m-badge m-badge--danger'></span>";
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }
        protected void btnGuardarTrabajador_Click(object sender, EventArgs e)
        {
            try
            {
                string codTipoDocumentoIdent = lblTipoDocumento.Value.Trim();
                string documentoIdentidad = txtNumeroDocumentoForm.Text.Trim();
                string nombres = txtNombresForm.Text.Trim();
                string apellidoPaterno = txtApellidosPaternosForm.Text.Trim();
                string apellidoMaterno = txtApellidosMaternosForm.Text.Trim();            
                string sexo = txtSexoForm.Text.Trim();            
                string codTipoTrabajo = lblTipoTrabajo.SelectedValue.Trim();
                string codPerfilPlanilla = lblPerfilPlanilla.SelectedValue.Trim();
                string codUsuarioRegistro = Session["CodUsuarioInterno"].ToString();
                string mensaje;

                if (codPerfilPlanilla == "Seleccione") { codPerfilPlanilla = "0"; }

                if (Session["CodEventualModificar"].ToString() == string.Empty)
                {
                    mensaje = GuardarTrabajador(codTipoDocumentoIdent, documentoIdentidad, nombres, apellidoPaterno, apellidoMaterno, sexo,
                    codTipoTrabajo, codPerfilPlanilla, codUsuarioRegistro, "2");
                }
                else
                {
                    mensaje = ModificarTrabajador(codTipoDocumentoIdent, documentoIdentidad, nombres, apellidoPaterno, apellidoMaterno, sexo,
                    codTipoTrabajo, codPerfilPlanilla, codUsuarioRegistro, Session["CodEventualModificar"].ToString(), "2");
                }                

                if (mensaje == "EXITO")
                {
                    ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('Trabajador agregado correctamente.','success');", true);

                    gpConsultaTrabajador.Visible = true;
                    gpWizardAgregarTrabajador.Visible = false;

                    BuscarUsuarioInterno("");

                    lblFormularioTrabajador.Text = "Creación de nuevos trabajadores";
                }
                else if (mensaje == "REPETIDO")
                {
                    ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('Los datos del trabajador ingresado ya existen en la base de datos.','info');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + mensaje + "','error');", true);
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
                BuscarUsuarioInterno(txtTextoBuscar.Text.Trim());
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }
        protected void dgvTrabajadoresPlanilla_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                dgvTrabajadoresPlanilla.PageIndex = e.NewPageIndex;

                if (txtTextoBuscar.Text == string.Empty)
                {
                    BuscarUsuarioInterno("");
                }
                else
                {
                    BuscarUsuarioInterno(txtTextoBuscar.Text.ToString().Trim());
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }
        protected void btnEliminarTrabajador_Click(object sender, EventArgs e)
        {
            try
            {
                string codTrabajadorEliminar = Session["CodTrabajadorEliminar"].ToString();

                string mensaje = EliminarTrabajador(codTrabajadorEliminar);

                if (mensaje == "EXITO")
                {
                    ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('Trabajador eliminado correctamente.','success');", true);
                    BuscarUsuarioInterno("");
                }
                else if (mensaje == "RETIRARPROYECTO")
                {
                    ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('Estimado usuario, el trabajador no puede ser eliminado mientras este asociado a un proyecto.','info');", true);
                }
                else if (mensaje == "EXISTEPLANILLA")
                {
                    ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('Estimado usuario, el trabajador no puede ser eliminado mientras tenga un registro de planilla.','info');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + mensaje + "','error');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }
        protected void btnCargarDatosTrabajador_Click(object sender, EventArgs e)
        {
            try
            {
                string documentoIdentidad = txtNumeroDocumentoForm.Text.Trim();
                string tipoDocumentoIdentidad = lblTipoDocumento.Value.Trim();

                if (documentoIdentidad != string.Empty || tipoDocumentoIdentidad != string.Empty)
                {
                    DataTable dt = procesoTrabajador.ObtenerDatosTrabajadorDocumentoIdentidad(documentoIdentidad, Convert.ToInt32(tipoDocumentoIdentidad));

                    if (dt.Rows.Count > 0)
                    {
                        lblTipoDocumento.Value = dt.Rows[0][0].ToString().Trim();
                        txtNumeroDocumentoForm.Text = dt.Rows[0][1].ToString().Trim();
                        txtNombresForm.Text = dt.Rows[0][2].ToString().Trim();
                        txtApellidosPaternosForm.Text = dt.Rows[0][3].ToString().Trim();
                        txtApellidosMaternosForm.Text = dt.Rows[0][4].ToString().Trim();
                        txtSexoForm.Text = dt.Rows[0][5].ToString().Trim();
                        Session["CodEventualModificar"] = dt.Rows[0][6].ToString().Trim();

                        ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "CargarDatosTemp", "CargarDatosTrabajadorDNI();", true);
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }
        /*---------------------------------*/
        /*------------METODOS--------------*/
        /*---------------------------------*/
        private void BuscarUsuarioInterno(string TextoBuscar)
        {
            try
            {
                DataTable dt = procesoTrabajador.BuscarTrabajadoresEventuales(TextoBuscar);
                dgvTrabajadoresPlanilla.DataSource = dt;
                dgvTrabajadoresPlanilla.DataBind();
                lblResultadoBusqueda.InnerHtml = "" + dt.Rows.Count.ToString() + " registros(s) encontrado(s)";
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }
        private void AbrirDetallesTrabajador(string codTrabajador, string codTipoPlanilla)
        {
            gpConsultaTrabajador.Visible = false;
            gpWizardAgregarTrabajador.Visible = true;

            lblFormularioTrabajador.Text = "Detalle Trabajador Eventual";

            DataTable dt = procesoTrabajador.ObtenerDatosTrabajador(Convert.ToInt32(codTrabajador), Convert.ToInt32(codTipoPlanilla));

            if (dt.Rows.Count > 0)
            {
                lblTipoDocumento.Value = dt.Rows[0][0].ToString().Trim();
                txtNumeroDocumentoForm.Text = dt.Rows[0][1].ToString().Trim();
                txtNombresForm.Text = dt.Rows[0][2].ToString().Trim();
                txtApellidosPaternosForm.Text = dt.Rows[0][3].ToString().Trim();
                txtApellidosMaternosForm.Text = dt.Rows[0][4].ToString().Trim();                             
                txtSexoForm.Text = dt.Rows[0][9].ToString().Trim();      
                lblTipoTrabajo.SelectedValue = dt.Rows[0][13].ToString().Trim();
                if (dt.Rows[0][19].ToString() != string.Empty)
                {
                    lblPerfilPlanilla.SelectedValue = dt.Rows[0][19].ToString().Trim();
                }

                if (Session["CodEventualModificar"].ToString() == string.Empty)
                {
                    ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "CargarDatos", "CargarDatosTrabajador();", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "CargarDatos", "CargarDatosTrabajadorModificar();", true);
                }
            }
        }
        private string GuardarTrabajador(string codTipoDocumentoIdentidad, string numDocumentoIdentidad, string nombres, string apellidoPaterno, string apellidoMaterno, string sexo,
        string codTipoTrabajo, string codPerfilPlanilla, string codUsuarioRegistro, string codTipoPlanilla)
        {
            TrabajadorEntidad trabajadorEntidad = new TrabajadorEntidad();
            trabajadorEntidad.CodTipoDocumentoIdentidad = Convert.ToInt32(codTipoDocumentoIdentidad);
            trabajadorEntidad.NumeroDocumentoIdentidad = numDocumentoIdentidad;
            trabajadorEntidad.Nombres = nombres;
            trabajadorEntidad.ApellidoPaterno = apellidoPaterno;
            trabajadorEntidad.ApellidoMaterno = apellidoMaterno;
            trabajadorEntidad.Sexo = sexo;       
            trabajadorEntidad.CodTipoTrabajo = Convert.ToInt32(codTipoTrabajo);
            trabajadorEntidad.CodPerfilPlanilla = Convert.ToInt32(codPerfilPlanilla);
            trabajadorEntidad.CodUsuarioRegistro = Convert.ToInt32(codUsuarioRegistro);
            trabajadorEntidad.CodTipoPlanilla = Convert.ToInt32(codTipoPlanilla);

            return procesoTrabajador.RegistrarTrabajadorEventuales(trabajadorEntidad);
        }
        private string ModificarTrabajador(string codTipoDocumentoIdentidad, string numDocumentoIdentidad, string nombres, string apellidoPaterno, string apellidoMaterno, string sexo,
       string codTipoTrabajo, string codPerfilPlanilla, string codUsuarioModifica, string codTrabajadorEventual, string codTipoPlanilla)
        {
            TrabajadorEntidad trabajadorEntidad = new TrabajadorEntidad();
            trabajadorEntidad.CodTipoDocumentoIdentidad = Convert.ToInt32(codTipoDocumentoIdentidad);
            trabajadorEntidad.NumeroDocumentoIdentidad = numDocumentoIdentidad;
            trabajadorEntidad.Nombres = nombres;
            trabajadorEntidad.ApellidoPaterno = apellidoPaterno;
            trabajadorEntidad.ApellidoMaterno = apellidoMaterno;
            trabajadorEntidad.Sexo = sexo;
            trabajadorEntidad.CodTipoTrabajo = Convert.ToInt32(codTipoTrabajo);
            trabajadorEntidad.CodPerfilPlanilla = Convert.ToInt32(codPerfilPlanilla);
            trabajadorEntidad.CodUsuarioModifica = Convert.ToInt32(codUsuarioModifica);
            trabajadorEntidad.CodTrabajador = Convert.ToInt32(codTrabajadorEventual);
            trabajadorEntidad.CodTipoPlanilla = Convert.ToInt32(codTipoPlanilla);

            return procesoTrabajador.ActualizarTrabajadorEventuales(trabajadorEntidad);
        }
        private string EliminarTrabajador(string codTrabajador)
        {
            TrabajadorEntidad trabajadorEntidad = new TrabajadorEntidad();
            trabajadorEntidad.CodTrabajador = Convert.ToInt32(codTrabajador);
            trabajadorEntidad.CodUsuarioModifica = Convert.ToInt32(Session["CodUsuarioInterno"].ToString());
            return procesoTrabajador.EliminarTrabajadorEventuales(trabajadorEntidad);
        }
        private void CargarLaborTrabajador()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = procesoTrabajador.ObtenerListaLaborTrabajador();
                cboTipoTrabajo.DataSource = dt;
                cboTipoTrabajo.DataTextField = "DescLaborTrabajo";
                cboTipoTrabajo.DataValueField = "CodLaborTrabajo";
                cboTipoTrabajo.DataBind();
                cboTipoTrabajo.Items.Insert(0, "Seleccione");

                lblTipoTrabajo.DataSource = dt;
                lblTipoTrabajo.DataTextField = "DescLaborTrabajo";
                lblTipoTrabajo.DataValueField = "CodLaborTrabajo";
                lblTipoTrabajo.DataBind();
                lblTipoTrabajo.Items.Insert(0, "Seleccione");
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }
        private void CargarPerfilPlanilla()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = procesoTrabajador.ObtenerPerfilPlanilla();
                cboPerfilPlanilla.DataSource = dt;
                cboPerfilPlanilla.DataTextField = "DescPerfilPlanilla";
                cboPerfilPlanilla.DataValueField = "CodPerfilPlanilla";
                cboPerfilPlanilla.DataBind();
                cboPerfilPlanilla.Items.Insert(0, "Seleccione");

                lblPerfilPlanilla.DataSource = dt;
                lblPerfilPlanilla.DataTextField = "DescPerfilPlanilla";
                lblPerfilPlanilla.DataValueField = "CodPerfilPlanilla";
                lblPerfilPlanilla.DataBind();
                lblPerfilPlanilla.Items.Insert(0, "Seleccione");
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }
        private void LimpiarFormulario()
        {
            lblTipoDocumento.Value = string.Empty;
            txtNumeroDocumentoForm.Text = string.Empty;
            txtNombresForm.Text = string.Empty;
            txtApellidosPaternosForm.Text = string.Empty;
            txtApellidosMaternosForm.Text = string.Empty;           
            txtSexoForm.Text = string.Empty;
            cboTipoTrabajo.ClearSelection();
            cboPerfilPlanilla.ClearSelection();
            lblTipoTrabajo.ClearSelection();
            lblPerfilPlanilla.ClearSelection();
        }

        
    }
}