using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogicaNegocio;
using Entidad;
using System.Globalization;
using System.Configuration;

namespace SistemaGestionPlanilla.Trabajador
{
    public partial class RegistroTrabajador : System.Web.UI.Page
    {
        TrabajadorNegocio procesoTrabajador = new TrabajadorNegocio();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["CodUsuarioInterno"] != null)
                {
                    this.txtTextoBuscar.Attributes.Add("onkeypress", "button_click(this,'" + this.btnBuscar.ClientID + "')");
                    Session["CodTrabajadorModificar"] = "";
                    CargarPerfilPlanilla();
                    CargarLaborTrabajador();
                    BuscarUsuarioInterno("");
                    gpWizardAgregarTrabajador.Visible = false;

                    lblFormularioTrabajador.Text = "Creación de nuevos trabajadores";
                }
                else
                {
                    Response.Redirect(ConfigurationManager.AppSettings["AssetsUrl"] + "/Seguridad/Logout.aspx");
                }                
            }
        }
        protected void dgvTrabajadoresPlanilla_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandArgument.ToString().Split('|').Length > 1)
                {
                    string codTrabajador = e.CommandArgument.ToString().Split('|')[0];
                    string codTipoPlanilla = e.CommandArgument.ToString().Split('|')[1];

                    if (e.CommandName == "VerDetalleTrabajador")
                    {
                        AbrirDetallesTrabajador(codTrabajador, codTipoPlanilla);
                    }
                    else if (e.CommandName == "ModificarTrabajador")
                    {
                        Session["CodTrabajadorModificar"] = codTrabajador;
                        AbrirDetallesTrabajador(codTrabajador, codTipoPlanilla);
                    }
                    else if (e.CommandName == "EliminarTrabajador")
                    {
                        Session["CodTrabajadorEliminar"] = codTrabajador;
                        ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "OpenModal", "abrirModalEliminarTrabajador();", true);
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
                string fechaNacimiento = txtFechaNacimientoForm.Text.Trim();
                
                string codEstadoCivil = lblEstadoCivil.Value.Trim();
                string fechaIngreso = txtFechaIngresoForm.Text.Trim();
                string fechaCese = txtFechaCeseForm.Text.Trim();
                string sexo = txtSexoForm.Text.Trim();
                string haberMensual = txtHaberMensualForm.Text.Trim();
                string codTipoAportacion = lblTipoAportacion.Value.Trim();
                string numeroCuspp = txtNumCusppForm.Text.Trim();
                string codTipoTrabajo = lblTipoTrabajo.SelectedValue.Trim();
                string codCargo = lblCargo.Value.Trim();
                string codBanco = lblBanco.Value.Trim();
                string numeroCuentaBanco = txtNumCuentaBanco.Text.Trim();
                string numeroCuentaCTS = txtNumCuentaCTS.Text.Trim();
                string codTipoPlanilla = lblTipoPlanilla.Value.Trim();
                string codPerfilPlanilla = cboPerfilPlanillaResumen.SelectedValue.ToString().Trim();
                string codUsuarioRegistro = Session["CodUsuarioInterno"].ToString();
                string mensaje;

                if (Session["CodTrabajadorModificar"].ToString() == string.Empty)
                {
                    mensaje = GuardarTrabajador(codTipoDocumentoIdent, documentoIdentidad, nombres, apellidoPaterno, apellidoMaterno, fechaNacimiento, sexo, codEstadoCivil, fechaIngreso, fechaCese,
                   haberMensual, codTipoAportacion, numeroCuspp, codTipoTrabajo, codCargo, codBanco, numeroCuentaBanco, numeroCuentaCTS, codTipoPlanilla, codPerfilPlanilla, codUsuarioRegistro);
                }
                else
                {
                    mensaje = ModificarTrabajador(codTipoDocumentoIdent, documentoIdentidad, nombres, apellidoPaterno, apellidoMaterno, fechaNacimiento, sexo, codEstadoCivil, fechaIngreso, fechaCese,
                   haberMensual, codTipoAportacion, numeroCuspp, codTipoTrabajo, codCargo, codBanco, numeroCuentaBanco, numeroCuentaCTS, codTipoPlanilla, codPerfilPlanilla, codUsuarioRegistro, Session["CodTrabajadorModificar"].ToString());
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
        protected void dgvTrabajadoresPlanilla_RowDataBound(object sender, GridViewRowEventArgs e)
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
        protected void btnRegresarConsultaTrabajador_Click(object sender, EventArgs e)
        {
            try
            {
                gpConsultaTrabajador.Visible = true;
                gpWizardAgregarTrabajador.Visible = false;
                LimpiarFormulario();

                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Limpiar", "NuevoRegistro();", true);

                Session["CodTrabajadorModificar"] = "";

                lblFormularioTrabajador.Text = "Creación de nuevos trabajadores";
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

                if (documentoIdentidad!=string.Empty|| tipoDocumentoIdentidad != string.Empty)
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
                        Session["CodTrabajadorModificar"] = dt.Rows[0][6].ToString().Trim();

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
        private void AbrirDetallesTrabajador(string codTrabajador, string codTipoPlanilla)
        {
            gpConsultaTrabajador.Visible = false;
            gpWizardAgregarTrabajador.Visible = true;

            lblFormularioTrabajador.Text = "Detalle Trabajador";

            DataTable dt = procesoTrabajador.ObtenerDatosTrabajador(Convert.ToInt32(codTrabajador), Convert.ToInt32(codTipoPlanilla));

            if (dt.Rows.Count > 0)
            {
                lblTipoDocumento.Value = dt.Rows[0][0].ToString().Trim();
                txtNumeroDocumentoForm.Text = dt.Rows[0][1].ToString().Trim();
                txtNombresForm.Text = dt.Rows[0][2].ToString().Trim();
                txtApellidosPaternosForm.Text = dt.Rows[0][3].ToString().Trim();
                txtApellidosMaternosForm.Text = dt.Rows[0][4].ToString().Trim();
                txtFechaNacimientoForm.Text = dt.Rows[0][5].ToString().Trim();

                lblEstadoCivil.Value = dt.Rows[0][6].ToString().Trim();
                txtFechaIngresoForm.Text = dt.Rows[0][7].ToString().Trim();
                txtFechaCeseForm.Text = dt.Rows[0][8].ToString().Trim();
                txtSexoForm.Text = dt.Rows[0][9].ToString().Trim();
                txtHaberMensualForm.Text = dt.Rows[0][10].ToString().Trim().Replace(",",".");
                lblTipoAportacion.Value = dt.Rows[0][11].ToString().Trim();
                txtNumCusppForm.Text = dt.Rows[0][12].ToString().Trim();
                lblTipoTrabajo.SelectedValue = dt.Rows[0][13].ToString().Trim();
                lblCargo.Value = dt.Rows[0][14].ToString().Trim();
                lblBanco.Value = dt.Rows[0][15].ToString().Trim();
                txtNumCuentaBanco.Text = dt.Rows[0][16].ToString().Trim();
                txtNumCuentaCTS.Text = dt.Rows[0][17].ToString().Trim();
                lblTipoPlanilla.Value = dt.Rows[0][18].ToString().Trim();
                cboPerfilPlanillaResumen.SelectedValue = dt.Rows[0][19].ToString().Trim();

                if (Session["CodTrabajadorModificar"].ToString() == string.Empty)
                {
                    ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "CargarDatos", "CargarDatosTrabajador();", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "CargarDatos", "CargarDatosTrabajadorModificar();", true);
                }                
            }
        }
        private void BuscarUsuarioInterno(string TextoBuscar)
        {
            DataTable dt = procesoTrabajador.BuscarTrabajador(TextoBuscar);
            dgvTrabajadoresPlanilla.DataSource = dt;
            dgvTrabajadoresPlanilla.DataBind();
            lblResultadoBusqueda.InnerHtml = "" + dt.Rows.Count.ToString() + " registros(s) encontrado(s)";
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

                cboPerfilPlanillaResumen.DataSource = dt;
                cboPerfilPlanillaResumen.DataTextField = "DescPerfilPlanilla";
                cboPerfilPlanillaResumen.DataValueField = "CodPerfilPlanilla";
                cboPerfilPlanillaResumen.DataBind();
                cboPerfilPlanillaResumen.Items.Insert(0, "Seleccione");
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
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
        private string GuardarTrabajador(string codTipoDocumentoIdentidad, string numDocumentoIdentidad, string nombres, string apellidoPaterno, string apellidoMaterno, string fechaNacimiento, string sexo, string codEstadoCivil, string fechaIngreso, string fechaCese, string haberMensual,
            string codTipoAportacion, string numeroCuspp, string codTipoTrabajo, string codCargo, string codBanco, string numeroCuentaBanco, string numeroCuentaCTS, string codTipoPlanilla, string codPerfilPlanilla, string codUsuarioRegistro)
        {
            TrabajadorEntidad trabajadorEntidad = new TrabajadorEntidad();
            trabajadorEntidad.CodTipoDocumentoIdentidad = Convert.ToInt32(codTipoDocumentoIdentidad);
            trabajadorEntidad.NumeroDocumentoIdentidad = numDocumentoIdentidad;
            trabajadorEntidad.Nombres = nombres;
            trabajadorEntidad.ApellidoPaterno = apellidoPaterno;
            trabajadorEntidad.ApellidoMaterno = apellidoMaterno;
            trabajadorEntidad.FechaNacimiento = fechaNacimiento;
            trabajadorEntidad.Sexo = sexo;
            trabajadorEntidad.CodEstadoCivil = Convert.ToInt32(codEstadoCivil);
            trabajadorEntidad.FechaIngreso = fechaIngreso;
            trabajadorEntidad.FechaCese = fechaCese;
            trabajadorEntidad.HaberMensual = double.Parse(haberMensual, CultureInfo.InvariantCulture);
            trabajadorEntidad.CodTipoAportacion = Convert.ToInt32(codTipoAportacion);
            trabajadorEntidad.NumeroCuspp = numeroCuspp;
            trabajadorEntidad.CodTipoTrabajo = Convert.ToInt32(codTipoTrabajo);
            trabajadorEntidad.CodCargo = Convert.ToInt32(codCargo);
            trabajadorEntidad.CodBanco = Convert.ToInt32(codBanco);
            trabajadorEntidad.NumeroCuentaBanco = numeroCuentaBanco;
            trabajadorEntidad.NumeroCuentaCTS = numeroCuentaCTS;
            trabajadorEntidad.CodTipoPlanilla = Convert.ToInt32(codTipoPlanilla);
            trabajadorEntidad.CodPerfilPlanilla = Convert.ToInt32(codPerfilPlanilla);
            trabajadorEntidad.CodUsuarioRegistro = Convert.ToInt32(codUsuarioRegistro);

            return procesoTrabajador.RegistrarTrabajador(trabajadorEntidad);
        }
        private string ModificarTrabajador(string codTipoDocumentoIdentidad, string numDocumentoIdentidad, string nombres, string apellidoPaterno, string apellidoMaterno, string fechaNacimiento, string sexo, string codEstadoCivil, string fechaIngreso, string fechaCese, string haberMensual,
            string codTipoAportacion, string numeroCuspp, string codTipoTrabajo, string codCargo, string codBanco, string numeroCuentaBanco, string numeroCuentaCTS, string codTipoPlanilla, string codPerfilPlanilla, string codUsuarioModifica, string codTrabajador)
        {
            TrabajadorEntidad trabajadorEntidad = new TrabajadorEntidad();
            trabajadorEntidad.CodTipoDocumentoIdentidad = Convert.ToInt32(codTipoDocumentoIdentidad);
            trabajadorEntidad.NumeroDocumentoIdentidad = numDocumentoIdentidad;
            trabajadorEntidad.Nombres = nombres;
            trabajadorEntidad.ApellidoPaterno = apellidoPaterno;
            trabajadorEntidad.ApellidoMaterno = apellidoMaterno;
            trabajadorEntidad.FechaNacimiento = fechaNacimiento;
            trabajadorEntidad.Sexo = sexo;
            trabajadorEntidad.CodEstadoCivil = Convert.ToInt32(codEstadoCivil);
            trabajadorEntidad.FechaIngreso = fechaIngreso;
            trabajadorEntidad.FechaCese = fechaCese;
            trabajadorEntidad.HaberMensual = double.Parse(haberMensual, CultureInfo.InvariantCulture);
            trabajadorEntidad.CodTipoAportacion = Convert.ToInt32(codTipoAportacion);
            trabajadorEntidad.NumeroCuspp = numeroCuspp;
            trabajadorEntidad.CodTipoTrabajo = Convert.ToInt32(codTipoTrabajo);
            trabajadorEntidad.CodCargo = Convert.ToInt32(codCargo);
            trabajadorEntidad.CodBanco = Convert.ToInt32(codBanco);
            trabajadorEntidad.NumeroCuentaBanco = numeroCuentaBanco;
            trabajadorEntidad.NumeroCuentaCTS = numeroCuentaCTS;
            trabajadorEntidad.CodTipoPlanilla = Convert.ToInt32(codTipoPlanilla);
            trabajadorEntidad.CodPerfilPlanilla = Convert.ToInt32(codPerfilPlanilla);
            trabajadorEntidad.CodUsuarioModifica = Convert.ToInt32(codUsuarioModifica);
            trabajadorEntidad.CodTrabajador = Convert.ToInt32(codTrabajador);

            return procesoTrabajador.ActualizarTrabajador(trabajadorEntidad);
        }
        private string EliminarTrabajador(string codTrabajador)
        {
            TrabajadorEntidad trabajadorEntidad = new TrabajadorEntidad();
            trabajadorEntidad.CodTrabajador = Convert.ToInt32(codTrabajador);
            trabajadorEntidad.CodUsuarioModifica = Convert.ToInt32(Session["CodUsuarioInterno"].ToString());
            return procesoTrabajador.EliminarTrabajador(trabajadorEntidad);
        }
        private void LimpiarFormulario()
        {
            lblTipoDocumento.Value = string.Empty;
            txtNumeroDocumentoForm.Text = string.Empty;
            txtNombresForm.Text = string.Empty;
            txtApellidosPaternosForm.Text = string.Empty;
            txtApellidosMaternosForm.Text = string.Empty;
            txtFechaNacimientoForm.Text = string.Empty;

            lblEstadoCivil.Value = string.Empty;
            txtFechaIngresoForm.Text = string.Empty;
            txtFechaCeseForm.Text = string.Empty;
            txtSexoForm.Text = string.Empty;
            txtHaberMensualForm.Text = string.Empty;
            lblTipoAportacion.Value = string.Empty;
            txtNumCusppForm.Text = string.Empty;
         
            lblCargo.Value = string.Empty;
            lblBanco.Value = string.Empty;
            txtNumCuentaBanco.Text = string.Empty;
            txtNumCuentaCTS.Text = string.Empty;
            lblTipoPlanilla.Value = string.Empty;
            cboPerfilPlanillaResumen.ClearSelection();
            cboPerfilPlanilla.ClearSelection();
            lblTipoTrabajo.ClearSelection();
        }

      
    }
}