using LogicaNegocio;
using System;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entidad;
using Microsoft.Reporting.WebForms;
using System.Configuration;

namespace SistemaGestionPlanilla.Proveedores
{
    public partial class ProveedoresRegistroPagos : System.Web.UI.Page
    {
        ProveedoresNegocio procesoProveedores = new ProveedoresNegocio();
        ContratistasNegocio procesoMaestras = new ContratistasNegocio();
        ChequesNegocio procesoCheques = new ChequesNegocio();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["CodUsuarioInterno"] != null)
                {
                    Session["CodPagoModificar"] = "";                    
                    Session["CodProveedorProyectoGenerarPago"] = "";
                    CargarProveedores();
                    CargarMedioPago();
                    CargarTipoReciboPago();
                    gpListadoProyectos.Visible = false;
                    gpListadoProveedorPago.Visible = false;
                    gpReporteRVIndividual.Style.Add("display", "none");
                    gpContratosContratista.Visible = true;
                    gpFormularioPago.Visible = false;
                }
                else
                {
                    Response.Redirect(ConfigurationManager.AppSettings["AssetsUrl"] + "/Seguridad/Logout.aspx");
                }
            }
        }    
        protected void cboProveedores_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cboProveedores.SelectedValue != "Seleccione")
                {
                    string codProveedor = cboProveedores.SelectedValue.ToString();

                    ObtenerProyectosProveedor(codProveedor);

                    gpListadoProveedorPago.Visible = false;
                }
                else
                {
                    gpListadoProyectos.Visible = false;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }
        protected void dgvProyectosProveedores_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string codProyecto = e.CommandArgument.ToString();
                string codProveedor = cboProveedores.SelectedValue.ToString();

                if (e.CommandName == "GenerarPagoContratista")
                {
                    Session["CodPagoModificar"] = "";
                    CargarDatosProveedorProyectoPago(codProveedor, codProyecto);
                }
                else if (e.CommandName == "VerDetallesPago")
                {
                    ObtenerListadoProveedoresPagos(codProyecto, codProveedor);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }
        protected void dgvProyectosProveedores_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                //if (e.Row.RowType == DataControlRowType.DataRow)
                //{
                //    if (e.Row.Cells[6].Text == "1")
                //    {
                //        e.Row.Cells[6].Text = "<span class='m-badge m-badge--warning m-badge--wide'>Pendiente</span>";
                //    }
                //    else if (e.Row.Cells[6].Text == "2")
                //    {
                //        e.Row.Cells[6].Text = "<span class='m-badge m-badge--success m-badge--wide'>Pagado</span>";
                //    }
                //}
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }
        protected void dgvProyectosProveedores_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                dgvProyectosProveedores.PageIndex = e.NewPageIndex;

                if (cboProveedores.SelectedValue != "Seleccione")
                {
                    ObtenerProyectosProveedor(cboProveedores.SelectedValue.ToString());
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }     
        protected void cboModoPago_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cboModoPago.SelectedValue != "Seleccione")
                {
                    txtNumeroCheque.Text = string.Empty;

                    if (cboModoPago.SelectedValue == "1")
                    {
                        txtNumeroCheque.ReadOnly = false;
                        txtNumeroCheque.Focus();
                    }
                    else
                    {
                        txtNumeroCheque.ReadOnly = true;
                    }
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
                gpContratosContratista.Visible = true;
                gpFormularioPago.Visible = false;
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
                string CodProveedorProyectoGenerarPago = Session["CodProveedorProyectoGenerarPago"].ToString();
                string codMedioPago = cboModoPago.SelectedValue.ToString();
                string numMedioPago = txtNumeroCheque.Text.ToString().Trim();
                string fechaPago = txtFechaPago.Text.Trim();
                string codTipoReciboPago = cboTipoReciboPago.SelectedValue.ToString();
                string numReciboPago = txtNumRecigoPago.Text.Trim();
                string montoPagar = txtMontoPagar.Text.Trim();

                if (codMedioPago == "Seleccione") { contadorCamposVacios += 1; lblModoPago.ForeColor = Color.Red; } else { lblModoPago.ForeColor = ColorTranslator.FromHtml("#3f4047"); }
                if (codTipoReciboPago == "Seleccione") { contadorCamposVacios += 1; lblTipoReciboPago.ForeColor = Color.Red; } else { lblTipoReciboPago.ForeColor = ColorTranslator.FromHtml("#3f4047"); }
                if (fechaPago == string.Empty) { contadorCamposVacios += 1; lblFechaPago.ForeColor = Color.Red; } else { lblFechaPago.ForeColor = ColorTranslator.FromHtml("#3f4047"); }
                if (numReciboPago == string.Empty) { contadorCamposVacios += 1; lblNumReciboPago.ForeColor = Color.Red; } else { lblNumReciboPago.ForeColor = ColorTranslator.FromHtml("#3f4047"); }
                if (montoPagar == string.Empty) { contadorCamposVacios += 1; lblMontoPagar.ForeColor = Color.Red; } else { lblMontoPagar.ForeColor = ColorTranslator.FromHtml("#3f4047"); }

                if (contadorCamposVacios == 0)
                {
                    string[] mensaje;

                    if (Session["CodPagoModificar"].ToString() == string.Empty)
                    {
                        mensaje = GuardarPagoProveedorProyectoPlanilla(CodProveedorProyectoGenerarPago, double.Parse(montoPagar, CultureInfo.InvariantCulture), codMedioPago, numMedioPago, fechaPago, codTipoReciboPago, numReciboPago, Session["CodUsuarioInterno"].ToString()).Split('|');
                    }
                    else
                    {
                        mensaje = ModificarPagoProveedorProyectoPlanilla(double.Parse(montoPagar, CultureInfo.InvariantCulture), codMedioPago, numMedioPago, fechaPago, codTipoReciboPago, numReciboPago, Session["CodPagoModificar"].ToString(), Session["CodUsuarioInterno"].ToString()).Split('|');
                    }

                    if (mensaje[0] == "EXITO")
                    {

                        ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('Pago registrado correctamente.','success');", true);

                        LimpiarFormulario();

                        gpContratosContratista.Visible = true;
                        gpFormularioPago.Visible = false;

                        ObtenerProyectosProveedor(cboProveedores.SelectedValue.ToString());

                        ObtenerListadoProveedoresPagos(mensaje[1].ToString(), cboProveedores.SelectedValue.ToString());

                    }
                    else if (mensaje[0] == "REPETIDO")
                    {
                        ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('Existe otro pago, con los mismos datos, por favor intente de nuevo ingresando nueva información.','info');", true);
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
        protected void btnDetallesPagoContrato_Click(object sender, EventArgs e)
        {
            try
            {
                string codPagoProveedor = Request.Cookies["CodPagoProveedorDetalles"].Value.ToString();
                CargarDatosProveedoresPagoRealizado(codPagoProveedor);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }
        protected void btnConfirmarEliminarPagoContratista_Click(object sender, EventArgs e)
        {
            try
            {
                string codPagoProveedorElminar = Request.Cookies["CodPagoProveedorEliminar"].Value.ToString();
                string[] mensaje = EliminarPagoProveedor(codPagoProveedorElminar, Session["CodUsuarioInterno"].ToString()).Split('|');

                if (mensaje[0] == "EXITO")
                {
                    ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('Pago eliminado correctamente.','info');", true);

                    ObtenerProyectosProveedor(cboProveedores.SelectedValue.ToString());
                    ObtenerListadoProveedoresPagos(mensaje[1], cboProveedores.SelectedValue.ToString());

                    ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "backdrop", "DesaparecerBackDrop();", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + mensaje[0] + "','error');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }
        protected void btnRegresarImpresion_Click(object sender, EventArgs e)
        {
            try
            {
                gpContratosContratista.Visible = true;
                gpFormularioPago.Visible = false;
                gpListadoProyectos.Visible = true;
                gpListadoProveedorPago.Visible = true;
                gpReporteRVIndividual.Style.Add("display", "none");                
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }
        protected void btnImprimirCheque_Click(object sender, EventArgs e)
        {
            try
            {
                string codChequeImprimir = Request.Cookies["CodChequeImprimir"].Value.ToString();
                GenerarReporteChequeInvidual(codChequeImprimir);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }
        /*---------------------------------------*/
        /*---------------METODOS-----------------*/
        /*---------------------------------------*/
        private void ObtenerProyectosProveedor(string codProveedor)
        {
            gpListadoProyectos.Visible = true;

            DataTable dt = procesoProveedores.ObtenerProyectosProveedor(Convert.ToInt32(codProveedor));
            dgvProyectosProveedores.DataSource = dt;
            dgvProyectosProveedores.DataBind();
            lblResultado.InnerHtml = "" + dt.Rows.Count.ToString() + " registros(s) encontrado(s)";
        }
        private string GuardarPagoProveedorProyectoPlanilla(string codProveedorProyecto, double montoPagar, string codMedioPago, string numMedioPago, string fechaPago, string codTipoReciboPago, string numReciboPago, string codUsuario)
        {
            ProveedoresEntidad proveedoresEntidad = new ProveedoresEntidad();
            proveedoresEntidad.CodProveedorProyectoPlanilla = Convert.ToInt32(codProveedorProyecto);
            proveedoresEntidad.MontoPagar = montoPagar;
            proveedoresEntidad.CodMedioPago = Convert.ToInt32(codMedioPago);
            proveedoresEntidad.NumMedioPago = numMedioPago;
            proveedoresEntidad.FechaPago = fechaPago;
            proveedoresEntidad.CodTipoReciboPago = Convert.ToInt32(codTipoReciboPago);
            proveedoresEntidad.NumReciboPago = numReciboPago;
            proveedoresEntidad.CodUsuarioRegistro = Convert.ToInt32(codUsuario);
            return procesoProveedores.RegistrarPagoProveedor(proveedoresEntidad);
        }
        private string ModificarPagoProveedorProyectoPlanilla(double montoPagar, string codMedioPago, string numMedioPago, string fechaPago, string codTipoReciboPago, string numReciboPago, string codPagoProveedor, string codUsuario)
        {
            ProveedoresEntidad proveedoresEntidad = new ProveedoresEntidad();
            proveedoresEntidad.MontoPagar = montoPagar;
            proveedoresEntidad.CodMedioPago = Convert.ToInt32(codMedioPago);
            proveedoresEntidad.NumMedioPago = numMedioPago;
            proveedoresEntidad.FechaPago = fechaPago;
            proveedoresEntidad.CodTipoReciboPago = Convert.ToInt32(codTipoReciboPago);
            proveedoresEntidad.NumReciboPago = numReciboPago;
            proveedoresEntidad.CodPagoProveedor = Convert.ToInt32(codPagoProveedor);
            proveedoresEntidad.CodUsuarioModifica = Convert.ToInt32(codUsuario);
            return procesoProveedores.ModificarPagoProveedor(proveedoresEntidad);
        }
        private string EliminarPagoProveedor(string codPagoProveedor, string codUsuario)
        {
            ProveedoresEntidad proveedoresEntidad = new ProveedoresEntidad();
            proveedoresEntidad.CodPagoProveedor = Convert.ToInt32(codPagoProveedor);
            proveedoresEntidad.CodUsuarioModifica = Convert.ToInt32(codUsuario);
            return procesoProveedores.EliminarPagoProveedores(proveedoresEntidad);
        }
        private void CargarDatosProveedorProyectoPago(string codProveedor, string codProyecto)
        {
            LimpiarFormulario();

            DataTable dt = procesoProveedores.ObtenerDatosProveedorProyectoPago(Convert.ToInt32(codProveedor), Convert.ToInt32(codProyecto));

            if (dt.Rows.Count > 0)
            {
                btnGuardar.Text = "<i class='la la-floppy-o'></i> Guardar";

                txtProveedorDetalle.Text = dt.Rows[0][0].ToString();
                txtDescripcionProyecto.Text = dt.Rows[0][1].ToString();                

                Session["CodProveedorProyectoGenerarPago"] = dt.Rows[0][2].ToString();

                gpContratosContratista.Visible = false;
                gpFormularioPago.Visible = true;

                txtMontoPagar.ReadOnly = false;
            }
        }
        private void CargarDatosProveedoresPagoRealizado(string codPagoProveedor)
        {
            LimpiarFormulario();

            DataTable dt = procesoProveedores.ObtenerDatosPagoProveedor(Convert.ToInt32(codPagoProveedor));

            if (dt.Rows.Count > 0)
            {
                btnGuardar.Text = "<i class='la la-floppy-o'></i> Modificar";

                txtProveedorDetalle.Text = dt.Rows[0][0].ToString();
                txtDescripcionProyecto.Text = dt.Rows[0][1].ToString();                
                cboModoPago.SelectedValue = dt.Rows[0][2].ToString();
                txtNumeroCheque.Text = dt.Rows[0][3].ToString();
                txtFechaPago.Text = dt.Rows[0][4].ToString();
                cboTipoReciboPago.SelectedValue = dt.Rows[0][5].ToString();
                txtNumRecigoPago.Text = dt.Rows[0][6].ToString();                
                txtMontoPagar.Text = dt.Rows[0][7].ToString();

                if (dt.Rows[0][2].ToString() == "1")
                {
                    txtMontoPagar.ReadOnly = true;
                }
                else
                {
                    txtMontoPagar.ReadOnly = false;
                }

                Session["CodPagoModificar"] = codPagoProveedor;

                gpContratosContratista.Visible = false;
                gpFormularioPago.Visible = true;
            }
        }
        private void CargarProveedores()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = procesoProveedores.CargarProveedores();
                cboProveedores.DataSource = dt;
                cboProveedores.DataTextField = "DescripcionProveedor";
                cboProveedores.DataValueField = "CodProveedor";
                cboProveedores.DataBind();
                cboProveedores.Items.Insert(0, "Seleccione");
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }
        private void CargarMedioPago()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = procesoMaestras.CargarMediosPagoContratista();
                cboModoPago.DataSource = dt;
                cboModoPago.DataTextField = "DescripcionMedioPago";
                cboModoPago.DataValueField = "CodMedioPago";
                cboModoPago.DataBind();
                cboModoPago.Items.Insert(0, "Seleccione");
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }
        private void CargarTipoReciboPago()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = procesoMaestras.CargarTipoRecibosPago();
                cboTipoReciboPago.DataSource = dt;
                cboTipoReciboPago.DataTextField = "DescripcionReciboPago";
                cboTipoReciboPago.DataValueField = "CodReciboPago";
                cboTipoReciboPago.DataBind();
                cboTipoReciboPago.Items.Insert(0, "Seleccione");
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }
        private void ObtenerListadoProveedoresPagos(string codProyecto, string codProveedor)
        {
            gpListadoProveedorPago.Visible = true;

            DataTable dt = procesoProveedores.ObtenerPagosRealizados(Convert.ToInt32(codProveedor), Convert.ToInt32(codProyecto));
            string html = "<div class='ibox'><div class='ibox-content top-fixed'><div class='table-responsive'><table class='table table-striped- table-bordered table-hover table-checkable' id='m_table_1'><thead><tr><th>#</th><th>Proveedor</th><th>Medio Pago</th><th>Fecha Pago</th><th>Recibo Pago</th><th>Total Pagado</th><th>Opcion</th></tr></thead>";
            try
            {
                html += "<tbody>";
                if (dt.Rows.Count > 0)
                {
                    int contadorfilas = 1;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string codigoPagoProveedor = dt.Rows[i][0].ToString();
                        string proveedor = dt.Rows[i][1].ToString();
                        string ruc = dt.Rows[i][2].ToString();
                        string medioPago = dt.Rows[i][3].ToString();
                        string numMedioPago = dt.Rows[i][4].ToString();
                        string fechaPago = dt.Rows[i][5].ToString();
                        string reciboPago = dt.Rows[i][6].ToString();
                        string numReciboPago = dt.Rows[i][7].ToString();
                        string totalPagado = dt.Rows[i][8].ToString();
                        string proyecto = dt.Rows[i][10].ToString();
                        string codCheque = dt.Rows[i][11].ToString();

                        lblContratoListado.InnerHtml = proyecto;

                        html += "<tr id='" + contadorfilas + "'><td style='height:44px;'><div style='width:100%; height:100%; overflow-y: auto;'>" + contadorfilas + "</div></td>";
                        html += "<td><div class='m-card-user m-card-user--sm'><div class='m-card-user__details'> <span class='m-card-user__name'>" + proveedor + "</span> <a href='#' class='m-card-user__email m-link'>RUC: " + ruc + "</a> </div></div></td>";
                        html += "<td><div class='m-card-user m-card-user--sm'><div class='m-card-user__details'> <span class='m-card-user__name'>" + medioPago + "</span> <a href='#' class='m-card-user__email m-link'>Nro: " + numMedioPago + "</a> </div></div></td>";
                        html += "<td>" + fechaPago + "</td>";
                        html += "<td><div class='m-card-user m-card-user--sm'><div class='m-card-user__details'> <span class='m-card-user__name'>" + reciboPago + "</span> <a href='#' class='m-card-user__email m-link'>Nro: " + numReciboPago + "</a> </div></div></td>";
                        html += "<td>" + totalPagado + "</td>";

                        if (codCheque == string.Empty)
                        {
                            html += "<td style='text-align:center'><a href='#' onclick ='AbriDetalles(event," + codigoPagoProveedor + ");' class='btn btn-sm btn-default'> Detalle</a><a href='#' style='margin-left:5px' onclick ='EliminarPagoProveedor(event," + codigoPagoProveedor + ");' class='btn btn-sm btn-outline-danger'> Eliminar</a></td>";
                        }
                        else
                        {
                            html += "<td style='text-align:center'><a href='#' onclick ='AbriDetalles(event," + codigoPagoProveedor + ");' class='btn btn-sm btn-default'> Detalle</a><a href='#' style='margin-left:5px' onclick ='ImprimirCheque(event," + codCheque + ");' class='btn btn-warning m-btn m-btn--icon m-btn--icon-only'><i class='la la-print'></i></a></td>";
                        }                        

                        contadorfilas += 1;
                    }
                }
                else
                {
                    html = "<p style='font-size: 20px;padding:30px'>Por el momento no hay pagos realizados a este contrato.</p>";
                }

                html += "</tbody></table>";
            }
            catch (Exception)
            {

            }
            finally
            {
                html += "</div></div></div>";
                tbListadoPagosRealizados.InnerHtml = html;
            }
        }
        private void GenerarReporteChequeInvidual(string codCheque)
        {
            /*CARGAMOS PRIMER DATASET*/

            System.Data.DataSet DataSetCheque = new System.Data.DataSet();

            DataSetCheque.Tables.Add(procesoCheques.ObtenerDatosChequeReporteIndividual(Convert.ToInt32(codCheque)));

            ReportDataSource datosSolicitante = new ReportDataSource("ReporteChequeIndividualDataSet", DataSetCheque.Tables[0]);

            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(datosSolicitante);
            ReportViewer1.LocalReport.Refresh();

            gpContratosContratista.Visible = false;
            gpFormularioPago.Visible = false;
            gpListadoProyectos.Visible = false;
            gpListadoProveedorPago.Visible = false;
            gpReporteRVIndividual.Style.Add("display", "normal");
        }
        private void LimpiarFormulario()
        {
            cboModoPago.ClearSelection();            
            txtFechaPago.Text = string.Empty;
            cboTipoReciboPago.ClearSelection();
            txtNumRecigoPago.Text = string.Empty;
            txtNumeroCheque.Text = string.Empty;
            txtMontoPagar.Text = string.Empty;
        }

      
    }
}