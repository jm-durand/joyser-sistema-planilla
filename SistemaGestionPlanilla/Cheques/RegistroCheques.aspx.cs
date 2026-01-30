using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogicaNegocio;
using Entidad;
using Microsoft.Reporting.WebForms;
using System.Configuration;

namespace SistemaGestionPlanilla.Cheques
{
    public partial class RegistroCheques : System.Web.UI.Page
    {
        ChequesNegocio procesoCheques = new ChequesNegocio();
        ContratistasNegocio procesoContratista = new ContratistasNegocio();
        TrabajadorNegocio procesoTrabajador = new TrabajadorNegocio();
        ProveedoresNegocio procesoProveedor = new ProveedoresNegocio();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["CodUsuarioInterno"] != null)
                {
                    txtFechaInicial.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    txtFechaFinal.Text = DateTime.Now.ToString("dd/MM/yyyy");                 

                    CargarTipoPagoCheque();
                    CargarTipoMoneda();
                    CargarTipoDocumentoIdentidad();
                    CargarTipoReciboPago();
                    CargarBancos();

                    BuscarChequeTipoPago("0", "", "");

                    Session["CodChequeModificar"] = "";
                    Session["CodChequeEmitir"] = "";
                    Session["CodChequeAnular"] = "";
                    Session["MontoTotalCheque"] = "0";
                    Session["EstadoCheque"] = "";

                    gpFormularioBusqueda.Visible = true;
                    gpFormularioCheque.Visible = false;
                    gpResumenChequeEmitir.Visible = false;
                    gpButtonNuevoCheque.Visible = true;
                    gpReciboPagoCheque.Visible = false;
                    gpListadoChequesBusqueda.Visible = false;
                    gpReporteRVIndividual.Style.Add("display", "none");

                    gpSeleccionTipoPersonaPersonaPagar.Visible = false;
                    gpSeleccionListadoTipoPersonaPagar.Visible = false;
                    gpSeleccionFormularioFinalPersonaPagar.Visible = false;
                }
                else
                {
                    Response.Redirect(ConfigurationManager.AppSettings["AssetsUrl"] + "/Seguridad/Logout.aspx");
                }
            }
        }
        protected void btnBuscarChequeTipoPago_Click(object sender, EventArgs e)
        {
            try
            {
                string codTipoPago = cboTipoPagoChequeBusqueda.SelectedValue.ToString();
                string fechaInicial = txtFechaInicial.Text.Trim();
                string fechaFinal = txtFechaFinal.Text.Trim();

                if (codTipoPago != "Seleccione")
                {
                    BuscarChequeTipoPago(codTipoPago, fechaInicial, fechaFinal);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('Debe seleccionar un tipo de pago para realizar la búsqueda.','info');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }
        protected void cboTipoPagoChequeBusqueda_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string codTipoPago = cboTipoPagoChequeBusqueda.SelectedValue.ToString();
                string fechaInicial = txtFechaInicial.Text.Trim();
                string fechaFinal = txtFechaFinal.Text.Trim();

                if (codTipoPago != "Seleccione")
                {                    
                    BuscarChequeTipoPago(codTipoPago, fechaInicial, fechaFinal);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('Debe seleccionar un tipo de pago para realizar la búsqueda.','info');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }
        protected void btnRegistrarCheque_Click(object sender, EventArgs e)
        {
            try
            {
                LimpiarFormulario1();
                LimpiarFormulario2();

                gpFormularioBusqueda.Visible = false;
                gpFormularioCheque.Visible = true;
                gpResumenChequeEmitir.Visible = false;
                gpButtonNuevoCheque.Visible = false;
                gpReciboPagoCheque.Visible = false;
                gpListadoChequesBusqueda.Visible = false;
                gpReporteRVIndividual.Style.Add("display", "none");

                gpSeleccionTipoPersonaPersonaPagar.Visible = true;
                gpSeleccionListadoTipoPersonaPagar.Visible = false;
                gpSeleccionFormularioFinalPersonaPagar.Visible = false;

                Session["CodChequeModificar"] = "";
                Session["CodChequeEmitir"] = "";
                Session["CodChequeAnular"] = "";
                Session["EstadoCheque"] = "";
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }
        protected void cboTipoPagoChequeRegistro_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string codTipoPagoCheque = cboTipoPagoChequeRegistro.SelectedValue.ToString();

                LimpiarSeleccion();

                if (codTipoPagoCheque == "1")
                {                                
                    CargarContratistas();                    
                    lblTipoPersonaPagoChequeRegistro.Text = "Contratistas";
                }
                else if (codTipoPagoCheque == "2")
                {
                    CargarProveedores();
                    lblTipoPersonaPagoChequeRegistro.Text = "Proveedores";
                }
                else
                {
                    LimpiarFormulario2();

                    lblTipoPersonaPagoChequeRegistro.Text = "Otros";   
                                      
                    gpFormularioBusqueda.Visible = false;
                    gpFormularioCheque.Visible = true;
                    gpResumenChequeEmitir.Visible = false;
                    gpButtonNuevoCheque.Visible = false;
                    gpReciboPagoCheque.Visible = false;
                    gpListadoChequesBusqueda.Visible = false;
                    gpReporteRVIndividual.Style.Add("display", "none");

                    gpSeleccionTipoPersonaPersonaPagar.Visible = false;
                    gpSeleccionListadoTipoPersonaPagar.Visible = false;
                    gpSeleccionFormularioFinalPersonaPagar.Visible = true;                    
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }
        protected void cboTipoPersonaPagoChequeRegistro_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {                
                string codTipoPersonaCheque = cboTipoPersonaPagoChequeRegistro.SelectedValue.ToString();
                string codTipoPagoCheque = cboTipoPagoChequeRegistro.SelectedValue.ToString();

                if (codTipoPersonaCheque != "0")
                {
                    if (codTipoPagoCheque == "1")
                    {
                        ObtenerPagosContratistas(codTipoPersonaCheque);

                        gpSeleccionTipoPersonaPersonaPagar.Visible = true;
                        gpSeleccionListadoTipoPersonaPagar.Visible = true;
                        gpSeleccionFormularioFinalPersonaPagar.Visible = false;

                        ReloadiChecks();
                    }
                    else if (codTipoPagoCheque == "2")
                    {
                        ObtenerPagosProveedores(codTipoPersonaCheque);

                        gpSeleccionTipoPersonaPersonaPagar.Visible = true;
                        gpSeleccionListadoTipoPersonaPagar.Visible = true;
                        gpSeleccionFormularioFinalPersonaPagar.Visible = false;

                        ReloadiChecks();
                    }                                  
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }
        protected void btnCancelarRegistroCheque_Click(object sender, EventArgs e)
        {
            try
            {
                gpFormularioBusqueda.Visible = true;
                gpFormularioCheque.Visible = false;
                gpResumenChequeEmitir.Visible = false;
                gpButtonNuevoCheque.Visible = true;
                gpReciboPagoCheque.Visible = false;
                gpListadoChequesBusqueda.Visible = true;
                gpReporteRVIndividual.Style.Add("display", "none");

                gpSeleccionTipoPersonaPersonaPagar.Visible = false;
                gpSeleccionListadoTipoPersonaPagar.Visible = false;
                gpSeleccionFormularioFinalPersonaPagar.Visible = false;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }
        protected void btnContinuarRegistroCheque_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["CodChequeModificar"].ToString() != string.Empty)
                {
                    LimpiarFormulario2();
                }

                string valorescheckbox = txtValoresCheckBox.Text.Trim();
                string codTipoPago = cboTipoPagoChequeRegistro.SelectedValue.ToString();
                string valoresContratosPagos = txtValoresContratos.Text.Trim();
                string codPersona = cboTipoPersonaPagoChequeRegistro.SelectedValue.ToString();

                if (codTipoPago != "5")
                {
                    if (codTipoPago == "1")
                    {
                        if (valorescheckbox != string.Empty)
                        {
                            if (valoresContratosPagos != string.Empty)
                            {
                                MantenerValoresPago(valoresContratosPagos);

                                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Recalculo", "CalcularMontoTotal();", true);
                            }

                            if (valorescheckbox != string.Empty)
                            {
                                string valorescheckboxsValido = valorescheckbox.Substring(1, valorescheckbox.Length - 1);
                                if (codTipoPago == "1")
                                {
                                    GenerarListadoPagarContratista(valorescheckboxsValido);
                                }
                                else if (codTipoPago == "2")
                                {
                                    GenerarListadoPagarProveedores(valorescheckboxsValido);
                                }
                            }

                            gpFormularioBusqueda.Visible = false;
                            gpFormularioCheque.Visible = true;
                            gpResumenChequeEmitir.Visible = false;
                            gpButtonNuevoCheque.Visible = false;
                            gpReciboPagoCheque.Visible = true;
                            gpListadoChequesBusqueda.Visible = false;
                            gpReporteRVIndividual.Style.Add("display", "none");

                            gpSeleccionTipoPersonaPersonaPagar.Visible = false;
                            gpSeleccionListadoTipoPersonaPagar.Visible = false;
                            gpSeleccionFormularioFinalPersonaPagar.Visible = true;

                            AutoCompletarInformacion(codPersona, codTipoPago);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('Debe seleccionar al menos 1 item de la lista para continuar.','info');", true);
                        }
                    }
                    else
                    {
                        if (valoresContratosPagos != string.Empty)
                        {
                            MantenerValoresPago(valoresContratosPagos);

                            ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Recalculo", "CalcularMontoTotal();", true);
                        }

                        if (valorescheckbox != string.Empty)
                        {
                            string valorescheckboxsValido = valorescheckbox.Substring(1, valorescheckbox.Length - 1);
                            if (codTipoPago == "1")
                            {
                                GenerarListadoPagarContratista(valorescheckboxsValido);
                            }
                            else if (codTipoPago == "2")
                            {
                                GenerarListadoPagarProveedores(valorescheckboxsValido);
                            }
                        }

                        gpFormularioBusqueda.Visible = false;
                        gpFormularioCheque.Visible = true;
                        gpResumenChequeEmitir.Visible = false;
                        gpButtonNuevoCheque.Visible = false;
                        gpReciboPagoCheque.Visible = true;
                        gpListadoChequesBusqueda.Visible = false;
                        gpReporteRVIndividual.Style.Add("display", "none");

                        gpSeleccionTipoPersonaPersonaPagar.Visible = false;
                        gpSeleccionListadoTipoPersonaPagar.Visible = false;
                        gpSeleccionFormularioFinalPersonaPagar.Visible = true;

                        AutoCompletarInformacion(codPersona, codTipoPago);
                    }                   
                }
                else
                {
                    gpFormularioBusqueda.Visible = false;
                    gpFormularioCheque.Visible = true;
                    gpResumenChequeEmitir.Visible = false;
                    gpButtonNuevoCheque.Visible = false;
                    gpReciboPagoCheque.Visible = false;
                    gpListadoChequesBusqueda.Visible = false;
                    gpReporteRVIndividual.Style.Add("display", "none");

                    gpSeleccionTipoPersonaPersonaPagar.Visible = false;
                    gpSeleccionListadoTipoPersonaPagar.Visible = false;
                    gpSeleccionFormularioFinalPersonaPagar.Visible = true;
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
                string valorescheckbox = txtValoresCheckBox.Text.Trim();
                string codTipoPago = cboTipoPagoChequeRegistro.SelectedValue.ToString();

                gpFormularioBusqueda.Visible = false;
                gpFormularioCheque.Visible = true;
                gpResumenChequeEmitir.Visible = false;
                gpButtonNuevoCheque.Visible = false;
                gpReciboPagoCheque.Visible = false;
                gpListadoChequesBusqueda.Visible = false;
                gpReporteRVIndividual.Style.Add("display", "none");

                gpSeleccionTipoPersonaPersonaPagar.Visible = true;
                gpSeleccionListadoTipoPersonaPagar.Visible = true;
                gpSeleccionFormularioFinalPersonaPagar.Visible = false;

                if (valorescheckbox != string.Empty)
                {
                    ReloadJqueryCheckBox(valorescheckbox);
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
                string codTipoPersonaCheque = cboTipoPagoChequeRegistro.SelectedValue.ToString();
                string fechaPago = txtFechaPago.Text.Trim();
                string numeroCheque = txtNumeroCheque.Text.Trim();
                string codTipoDocumentoIdentidad = cboTipoDocumentoOrdenPagar.SelectedValue.ToString();
                string numDocumentoIdentidad = txtNumeroDocumentoIdentidadOrdenPagar.Text.Trim();
                string nombresCompletos = txtNombreCompletoOrdenPagar.Text.Trim();
                string valoresContratosPagos = txtValoresContratos.Text.Trim();
                string valoresContratosPagosValido = string.Empty;
                string codTipoMoneda = cboTipoMoneda.SelectedValue.ToString();
                string montoTotal = txtMontoPagar.Text.Trim();
                string montoTotalLetras = txtMontoPagarEnLetras.Text.Trim();
                string codTipoReciboPago = cboTipoReciboPago.SelectedValue.ToString();
                string numReciboPago = txtNumeroReciboPago.Text.Trim();
                string codPersona = cboTipoPersonaPagoChequeRegistro.SelectedValue.ToString();
                string codBanco = cboBancoCheque.SelectedValue.ToString();

                if (fechaPago == string.Empty) { contadorCamposVacios += 1; lblFechaPago.ForeColor = Color.Red; } else { lblFechaPago.ForeColor = ColorTranslator.FromHtml("#3f4047"); }
                if (numeroCheque == string.Empty) { contadorCamposVacios += 1; lblNumeroCheque.ForeColor = Color.Red; } else { lblNumeroCheque.ForeColor = ColorTranslator.FromHtml("#3f4047"); }
                if (codTipoDocumentoIdentidad == "0") { contadorCamposVacios += 1; lblTipoDocumentoOrdenPagar.ForeColor = Color.Red; } else { lblTipoDocumentoOrdenPagar.ForeColor = ColorTranslator.FromHtml("#3f4047"); }
                if (numDocumentoIdentidad == string.Empty) { contadorCamposVacios += 1; lblNumeroDocumentoIdentidadOrdenPagar.ForeColor = Color.Red; } else { lblNumeroDocumentoIdentidadOrdenPagar.ForeColor = ColorTranslator.FromHtml("#3f4047"); }
                if (nombresCompletos == string.Empty) { contadorCamposVacios += 1; lblNombreCompletoOrdenPagar.ForeColor = Color.Red; } else { lblNombreCompletoOrdenPagar.ForeColor = ColorTranslator.FromHtml("#3f4047"); }
                if (codTipoMoneda == "0") { contadorCamposVacios += 1; lblTipoMoneda.ForeColor = Color.Red; } else { lblTipoMoneda.ForeColor = ColorTranslator.FromHtml("#3f4047"); }
                if (codBanco == "0") { contadorCamposVacios += 1; lblBancoCheque.ForeColor = Color.Red; } else { lblBancoCheque.ForeColor = ColorTranslator.FromHtml("#3f4047"); }
                if (montoTotal == string.Empty) { contadorCamposVacios += 1; lblMontoPagar.ForeColor = Color.Red; } else { lblMontoPagar.ForeColor = ColorTranslator.FromHtml("#3f4047"); }
                if (montoTotalLetras == string.Empty) { contadorCamposVacios += 1; lblMontoPagarEnLetras.ForeColor = Color.Red; } else { lblMontoPagarEnLetras.ForeColor = ColorTranslator.FromHtml("#3f4047"); }
                if (codTipoPersonaCheque != "5")
                {
                    if (codTipoReciboPago == "0") { contadorCamposVacios += 1; lblTipoReciboPago.ForeColor = Color.Red; } else { lblTipoReciboPago.ForeColor = ColorTranslator.FromHtml("#3f4047"); }
                    if (numReciboPago == string.Empty) { contadorCamposVacios += 1; lblNumeroReciboPago.ForeColor = Color.Red; } else { lblNumeroReciboPago.ForeColor = ColorTranslator.FromHtml("#3f4047"); }
                }                
                if (valoresContratosPagos == string.Empty) { } else { MantenerValoresPago(valoresContratosPagos); }
               
                if (contadorCamposVacios == 0)
                {                   
                    if (codTipoPersonaCheque != "0")
                    {                            
                        string[] mensaje;
                        string finalmente = string.Empty;

                        if (Session["CodChequeModificar"].ToString() == string.Empty)
                        {
                            mensaje = GuardarCheque(codTipoPersonaCheque, numeroCheque, codTipoDocumentoIdentidad, numDocumentoIdentidad, nombresCompletos, codTipoMoneda, montoTotal, montoTotalLetras, fechaPago, codBanco).Split('|');                              
                        }
                        else
                        {
                            mensaje = ModificarCheque(codTipoPersonaCheque, numeroCheque, codTipoDocumentoIdentidad, numDocumentoIdentidad, nombresCompletos, codTipoMoneda, montoTotal, montoTotalLetras, fechaPago, codBanco, Session["CodChequeModificar"].ToString()).Split('|');
                        }

                        if (mensaje[0] == "EXITO")
                        {
                            if (valoresContratosPagos != string.Empty)
                            {
                                if (codTipoPersonaCheque == "1")
                                {
                                    valoresContratosPagosValido = valoresContratosPagos.Substring(1, valoresContratosPagos.Length - 1);
                                    string limpiamosregistros = procesoCheques.LimpiarModificarChequePagoContratistas(Convert.ToInt32(mensaje[1].ToString()), Convert.ToInt32(Session["CodUsuarioInterno"].ToString()));
                                    string[] Lista_Parametros = valoresContratosPagosValido.Split(',');

                                    for (int i = 0; i < Lista_Parametros.Length; i++)
                                    {
                                        string codContrato = Lista_Parametros[i].Split('_')[0].ToString();
                                        string montoPagarContrato = Lista_Parametros[i].Split('_')[1].ToString();

                                        finalmente = GuardarPagoContratista(codContrato, montoPagarContrato, numeroCheque, fechaPago, codTipoReciboPago, numReciboPago, mensaje[1].ToString());
                                    }

                                    if (finalmente == "EXITO")
                                    {
                                        ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "MensajeRelacion", "Mensaje('Registro de pago a contratistas guardado correctamente','info');", true);
                                    }
                                    else
                                    {
                                        ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + finalmente + "','error');", true);
                                    }
                                }
                                else if (codTipoPersonaCheque == "2")
                                {
                                    valoresContratosPagosValido = valoresContratosPagos.Substring(1, valoresContratosPagos.Length - 1);
                                    string limpiamosregistros = procesoCheques.LimpiarModificarChequePagoProveedores(Convert.ToInt32(mensaje[1].ToString()), Convert.ToInt32(Session["CodUsuarioInterno"].ToString()));
                                    string[] Lista_Parametros = valoresContratosPagosValido.Split(',');

                                    for (int i = 0; i < Lista_Parametros.Length; i++)
                                    {
                                        string codProyectoProveedor = Lista_Parametros[i].Split('_')[0].ToString();
                                        string montoPagar = Lista_Parametros[i].Split('_')[1].ToString();

                                        finalmente = GuardarPagoProveedor(codProyectoProveedor, montoPagar, numeroCheque, fechaPago, codTipoReciboPago, numReciboPago, mensaje[1].ToString());
                                    }

                                    if (finalmente == "EXITO")
                                    {
                                        ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "MensajeRelacion", "Mensaje('Registro de pago a proveedor guardado correctamente','info');", true);
                                    }
                                    else
                                    {
                                        ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + finalmente + "','error');", true);
                                    }
                                }
                            }
                            else
                            {
                                if (codTipoPersonaCheque == "2")
                                {
                                    finalmente = GuardarPagoProveedorSinProyecto(codPersona, montoTotal, numeroCheque, fechaPago, codTipoReciboPago, numReciboPago, mensaje[1].ToString());

                                    if (finalmente == "EXITO")
                                    {
                                        ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "MensajeRelacion", "Mensaje('Registro de pago a proveedor guardado correctamente','info');", true);
                                    }
                                    else
                                    {
                                        ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + finalmente + "','error');", true);
                                    }
                                }
                            }

                            ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "MensajeCheque", "Mensaje('Cheque guardado con exito','success');", true);

                            if (Session["EstadoCheque"].ToString() == string.Empty)
                            {
                                gpFormularioBusqueda.Visible = false;
                                gpFormularioCheque.Visible = false;
                                gpResumenChequeEmitir.Visible = true;
                                gpButtonNuevoCheque.Visible = false;
                                gpReciboPagoCheque.Visible = false;
                                gpListadoChequesBusqueda.Visible = false;

                                gpSeleccionTipoPersonaPersonaPagar.Visible = false;
                                gpSeleccionListadoTipoPersonaPagar.Visible = false;
                                gpSeleccionFormularioFinalPersonaPagar.Visible = false;

                                Session["CodChequeEmitir"] = mensaje[1].ToString();
                                Session["CodChequeModificar"] = mensaje[1].ToString();

                                GenerarResumenCheque(mensaje[1].ToString());
                            }
                            else
                            {
                                gpFormularioBusqueda.Visible = true;
                                gpFormularioCheque.Visible = false;
                                gpResumenChequeEmitir.Visible = false;
                                gpButtonNuevoCheque.Visible = true;
                                gpReciboPagoCheque.Visible = false;
                                gpListadoChequesBusqueda.Visible = true;

                                gpSeleccionTipoPersonaPersonaPagar.Visible = false;
                                gpSeleccionListadoTipoPersonaPagar.Visible = false;
                                gpSeleccionFormularioFinalPersonaPagar.Visible = false;

                                Session["CodChequeModificar"] = "";
                                Session["CodChequeEmitir"] = "";
                                Session["CodChequeAnular"] = "";
                                Session["MontoTotalCheque"] = "0";
                                Session["EstadoCheque"] = "";

                                cboTipoPagoChequeBusqueda_SelectedIndexChanged(cboTipoPagoChequeBusqueda.SelectedValue, new EventArgs());
                            }
                        }
                        else if (mensaje[0] == "REPETIDO")
                        {
                            ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('Ya existe un cheque generado con la misma fecha, tipo de persona, numero de cheque y monto de pago, por favor modique los datos repetidos.','info');", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + mensaje + "','error');", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('Debe seleccionar el tipo de persona a pagar.','info');", true);
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
        protected void btnGenerarMontoLetras_Click(object sender, EventArgs e)
        {
            try
            {
                string valoresContratosPagos = txtValoresContratos.Text.Trim();
                string montoTotal = txtMontoPagar.Text.Trim();

                if (valoresContratosPagos != string.Empty)
                {
                    MantenerValoresPago(valoresContratosPagos);

                    if (montoTotal != string.Empty)
                    {
                        GenerarMontoEnLetras(montoTotal);
                        Session["MontoTotalCheque"] = montoTotal;
                        txtMontoPagar.Enabled = false;
                    }                    
                }             
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }
        protected void cboTipoMoneda_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string codTipoMoneda = cboTipoMoneda.SelectedValue.ToString();
                string montoTotalCheque = Session["MontoTotalCheque"].ToString();
                string valoresContratosPagos = txtValoresContratos.Text.Trim();
                string codTipoPago = cboTipoPagoChequeRegistro.SelectedValue.ToString();

                if (valoresContratosPagos != string.Empty)
                {
                    MantenerValoresPago(valoresContratosPagos);

                    if (codTipoMoneda != "0")
                    {
                        if (codTipoMoneda == "1")
                        {
                            txtMontoPagar.Text = Session["MontoTotalCheque"].ToString();

                            if (montoTotalCheque != string.Empty)
                            {
                                GenerarMontoEnLetras(montoTotalCheque);
                                txtMontoPagar.Enabled = false;
                            }
                        }
                        else
                        {
                            txtMontoPagar.Text = string.Empty;
                            txtMontoPagarEnLetras.Text = string.Empty;
                            txtMontoPagar.Enabled = true;
                        }
                    }
                }
                else
                {
                    //if (codTipoPago == "5")
                    //{
                        if (codTipoMoneda == "1")
                        {                                                       
                            if (montoTotalCheque != "0")
                            {
                                GenerarMontoEnLetras(montoTotalCheque);
                            }                            
                        }
                    //}
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }
        protected void txtMontoPagar_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string codTipoMoneda = cboTipoMoneda.SelectedValue.ToString();
                string montoTotalCheque = txtMontoPagar.Text.ToString();
                string valoresContratosPagos = txtValoresContratos.Text.Trim();
                string codTipoPago = cboTipoPagoChequeRegistro.SelectedValue.ToString();
                            
                if (valoresContratosPagos != string.Empty)
                {
                    MantenerValoresPago(valoresContratosPagos);

                    if (codTipoMoneda != "0")
                    {
                        if (codTipoMoneda != "1")
                        {
                            if (montoTotalCheque != string.Empty)
                            {
                                GenerarMontoEnLetras(montoTotalCheque);                                
                            }
                        }
                    }
                }
                else
                {
                    //if (codTipoPago == "5")
                    //{
                        if (codTipoMoneda == "1")
                        {                            
                            if (montoTotalCheque != string.Empty)
                            {
                                GenerarMontoEnLetras(montoTotalCheque);
                            }                            
                        }
                    ////}
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
                string codTipoPago = cboTipoPagoChequeBusqueda.SelectedValue.ToString();
                string fechaInicial = txtFechaInicial.Text.Trim();
                string fechaFinal = txtFechaFinal.Text.Trim();

                dgvContratistas.PageIndex = e.NewPageIndex;
                BuscarChequeTipoPago(codTipoPago, fechaInicial, fechaFinal);
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
                    if (e.Row.Cells[8].Text == "1")
                    {
                        e.Row.Cells[8].Text = "<span class='m-badge m-badge--warning m-badge--wide'>Pendiente</span>";
                    }
                    else if (e.Row.Cells[8].Text == "2")
                    {
                        e.Row.Cells[8].Text = "<span class='m-badge m-badge--success m-badge--wide'>Emitido</span>";
                    }
                    else if (e.Row.Cells[8].Text == "3")
                    {
                        e.Row.Cells[8].Text = "<span class='m-badge m-badge--danger m-badge--wide'>Anulado</span>";
                    }
                }
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
                if (e.CommandName == "ModificarCheque")
                {
                    string codChequeModificar = e.CommandArgument.ToString();
                    CargarDatosCheque(codChequeModificar);
                }
                else if (e.CommandName == "AnularCheque")
                {
                    string codChequeAnular = e.CommandArgument.ToString();
                    Session["CodChequeAnular"] = codChequeAnular;

                    ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "OpenModal", "abrirModalAnularCheque();", true);
                }
                else if (e.CommandName == "EmitirCheque")
                {
                    string codChequeEmitir = e.CommandArgument.ToString();
                    Session["CodChequeEmitir"] = codChequeEmitir;

                    ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "OpenModal", "abrirModalEmitirCheque();", true);
                }
                else if (e.CommandName == "ImprimirCheque")
                {
                    string codChequeImpresion = e.CommandArgument.ToString();  
                
                    GenerarReporteChequeInvidual(codChequeImpresion);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }
        protected void txtNumeroDocumentoIdentidadOrdenPagar_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string numerodocumentoIdentidad = txtNumeroDocumentoIdentidadOrdenPagar.Text.Trim();
                string codtipodocumentoIdentidad = cboTipoDocumentoOrdenPagar.SelectedValue.ToString();
                string valoresContratosPagos = txtValoresContratos.Text.Trim();

                if (valoresContratosPagos != string.Empty)
                {
                    MantenerValoresPago(valoresContratosPagos);
                }

                if (codtipodocumentoIdentidad != "0" && numerodocumentoIdentidad != string.Empty)
                {
                    DataTable dt = procesoCheques.ObtenerDatosPersonaPagar(numerodocumentoIdentidad, Convert.ToInt32(codtipodocumentoIdentidad));
                    if (dt.Rows.Count > 0)
                    {
                        txtNombreCompletoOrdenPagar.Text = dt.Rows[0][0].ToString().Trim();
                    }
                    else
                    {
                        txtNombreCompletoOrdenPagar.Text = string.Empty;
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }
        protected void cboTipoDocumentoOrdenPagar_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string numerodocumentoIdentidad = txtNumeroDocumentoIdentidadOrdenPagar.Text.Trim();
                string codtipodocumentoIdentidad = cboTipoDocumentoOrdenPagar.SelectedValue.ToString();
                string valoresContratosPagos = txtValoresContratos.Text.Trim();

                if (valoresContratosPagos != string.Empty)
                {
                    MantenerValoresPago(valoresContratosPagos);
                }

                if (codtipodocumentoIdentidad != "0" && numerodocumentoIdentidad != string.Empty)
                {
                    DataTable dt = procesoCheques.ObtenerDatosPersonaPagar(numerodocumentoIdentidad, Convert.ToInt32(codtipodocumentoIdentidad));
                    if (dt.Rows.Count > 0)
                    {
                        txtNombreCompletoOrdenPagar.Text = dt.Rows[0][0].ToString().Trim();
                    }
                    else
                    {
                        txtNombreCompletoOrdenPagar.Text = string.Empty;
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }
        protected void btnEmitirCheque_Click(object sender, EventArgs e)
        {
            try
            {
                string codChequeEmitir = Session["CodChequeEmitir"].ToString();
                string[] mensaje = EmitirCheque(codChequeEmitir).Split('|');

                if (mensaje[0] == "EXITO")
                {
                    gpFormularioBusqueda.Visible = true;
                    gpFormularioCheque.Visible = false;
                    gpResumenChequeEmitir.Visible = false;
                    gpButtonNuevoCheque.Visible = true;
                    gpReciboPagoCheque.Visible = false;
                    gpListadoChequesBusqueda.Visible = true;

                    gpSeleccionTipoPersonaPersonaPagar.Visible = false;
                    gpSeleccionListadoTipoPersonaPagar.Visible = false;
                    gpSeleccionFormularioFinalPersonaPagar.Visible = false;

                    Session["CodChequeModificar"] = "";
                    Session["CodChequeEmitir"] = "";
                    Session["CodChequeAnular"] = "";
                    Session["MontoTotalCheque"] = "0";
                    Session["EstadoCheque"] = "";

                    ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('Cheque emitido con exito','success');", true);

                    cboTipoPagoChequeBusqueda.SelectedValue = mensaje[1].ToString();
                    cboTipoPagoChequeBusqueda_SelectedIndexChanged(mensaje[1].ToString(), new EventArgs());                                    
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
        protected void btnRegresarRegistroCheque_Click(object sender, EventArgs e)
        {
            try
            {
                string valoresContratosPagos = txtValoresContratos.Text.Trim();
                if (valoresContratosPagos != string.Empty)
                {
                    MantenerValoresPago(valoresContratosPagos);
                }

                gpFormularioBusqueda.Visible = false;
                gpFormularioCheque.Visible = true;
                gpResumenChequeEmitir.Visible = false;
                gpButtonNuevoCheque.Visible = false;
                gpReciboPagoCheque.Visible = false;
                gpListadoChequesBusqueda.Visible = false;

                gpSeleccionTipoPersonaPersonaPagar.Visible = false;
                gpSeleccionListadoTipoPersonaPagar.Visible = false;
                gpSeleccionFormularioFinalPersonaPagar.Visible = true;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }
        protected void btnCancelarEmisionCheque_Click(object sender, EventArgs e)
        {
            try
            {
                LimpiarFormulario1();
                LimpiarFormulario2();

                gpFormularioBusqueda.Visible = true;
                gpFormularioCheque.Visible = false;
                gpResumenChequeEmitir.Visible = false;
                gpButtonNuevoCheque.Visible = true;
                gpReciboPagoCheque.Visible = false;
                gpListadoChequesBusqueda.Visible = true;

                gpSeleccionTipoPersonaPersonaPagar.Visible = false;
                gpSeleccionListadoTipoPersonaPagar.Visible = false;
                gpSeleccionFormularioFinalPersonaPagar.Visible = false;

                Session["CodChequeModificar"] = "";
                Session["CodChequeEmitir"] = "";
                Session["CodChequeAnular"] = "";
                Session["MontoTotalCheque"] = "0";
                Session["EstadoCheque"] = "";
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }
        protected void btnAnularCheque_Click(object sender, EventArgs e)
        {
            try
            {
                string codChequeAnular = Session["CodChequeAnular"].ToString();
                string[] mensaje = AnularCheque(codChequeAnular).Split('|');

                if (mensaje[0] == "EXITO")
                {            
                    if (mensaje[1].ToString() == "1")
                    {
                        string finalmente = procesoCheques.LimpiarModificarChequePagoContratistas(Convert.ToInt32(codChequeAnular), Convert.ToInt32(Session["CodUsuarioInterno"].ToString()));
                        if (finalmente == "EXITO")
                        {
                            ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('Pagos anulados correctamente','info');", true);
                        }
                    }
                    else if (mensaje[1].ToString() == "2")
                    {
                        string finalmente = procesoCheques.LimpiarModificarChequePagoProveedores(Convert.ToInt32(codChequeAnular), Convert.ToInt32(Session["CodUsuarioInterno"].ToString()));
                        if (finalmente == "EXITO")
                        {
                            ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('Pagos anulados correctamente','info');", true);
                        }
                    }

                    Session["CodChequeModificar"] = "";
                    Session["CodChequeEmitir"] = "";
                    Session["CodChequeAnular"] = "";
                    Session["MontoTotalCheque"] = "0";
                    Session["EstadoCheque"] = "";

                    ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensajes", "Mensaje('Cheque anulado con exito','success');", true);

                    cboTipoPagoChequeBusqueda.SelectedValue = mensaje[1].ToString();
                    cboTipoPagoChequeBusqueda_SelectedIndexChanged(mensaje[1].ToString(), new EventArgs());
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
                gpFormularioBusqueda.Visible = true;
                gpFormularioCheque.Visible = false;
                gpResumenChequeEmitir.Visible = false;
                gpButtonNuevoCheque.Visible = true;
                gpReciboPagoCheque.Visible = false;
                gpListadoChequesBusqueda.Visible = true;

                gpSeleccionTipoPersonaPersonaPagar.Visible = false;
                gpSeleccionListadoTipoPersonaPagar.Visible = false;
                gpSeleccionFormularioFinalPersonaPagar.Visible = false;

                gpReporteRVIndividual.Style.Add("display", "none");
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }
        /*---------------------------------------*/
        /*---------------METODOS-----------------*/
        /*---------------------------------------*/
        private string GuardarCheque(string codTipoPersona, string numeroCheque, string codTipoDocIdentidadPersona, string numeroDocIdentidad, string nombresCompletosPersona, string codTipoMoneda, string montoTotalNumerico, string montoTotalLetras, string fechaPago, string codBanco)
        {
            ChequesEntidad chequesEntidad = new ChequesEntidad();
            chequesEntidad.CodTipoPersona = Convert.ToInt32(codTipoPersona);
            chequesEntidad.NumeroCheque = numeroCheque;
            chequesEntidad.CodTipoDocumentoIdentidadPersona = Convert.ToInt32(codTipoDocIdentidadPersona);
            chequesEntidad.NumeroDocumentoIdentidadPersona = numeroDocIdentidad;
            chequesEntidad.NombreCompletoPersona = nombresCompletosPersona;
            chequesEntidad.CodTipoMoneda = Convert.ToInt32(codTipoMoneda);
            chequesEntidad.MontoTotalNumerico = double.Parse(montoTotalNumerico, CultureInfo.InvariantCulture);
            chequesEntidad.MontoTotalLetras = montoTotalLetras;
            chequesEntidad.FechaPago = fechaPago;
            chequesEntidad.CodBanco = Convert.ToInt32(codBanco);
            chequesEntidad.CodUsuarioRegistro = Convert.ToInt32(Session["CodUsuarioInterno"].ToString());
            return procesoCheques.RegistrarCheque(chequesEntidad);
        }
        private string ModificarCheque(string codTipoPersona, string numeroCheque, string codTipoDocIdentidadPersona, string numeroDocIdentidad, string nombresCompletosPersona, string codTipoMoneda, string montoTotalNumerico, string montoTotalLetras, string fechaPago, string codBanco, string codCheque)
        {
            ChequesEntidad chequesEntidad = new ChequesEntidad();
            chequesEntidad.CodTipoPersona = Convert.ToInt32(codTipoPersona);
            chequesEntidad.NumeroCheque = numeroCheque;
            chequesEntidad.CodTipoDocumentoIdentidadPersona = Convert.ToInt32(codTipoDocIdentidadPersona);
            chequesEntidad.NumeroDocumentoIdentidadPersona = numeroDocIdentidad;
            chequesEntidad.NombreCompletoPersona = nombresCompletosPersona;
            chequesEntidad.CodTipoMoneda = Convert.ToInt32(codTipoMoneda);
            chequesEntidad.MontoTotalNumerico = double.Parse(montoTotalNumerico, CultureInfo.InvariantCulture);
            chequesEntidad.MontoTotalLetras = montoTotalLetras;
            chequesEntidad.FechaPago = fechaPago;
            chequesEntidad.CodBanco = Convert.ToInt32(codBanco);
            chequesEntidad.CodUsuarioModifica = Convert.ToInt32(Session["CodUsuarioInterno"].ToString());
            chequesEntidad.CodCheque = Convert.ToInt32(codCheque);
            return procesoCheques.ModificarCheque(chequesEntidad);
        }
        private string GuardarPagoContratista(string codContrato, string montoPagar, string numMedioPago, string fechaPago, string codTipoReciboPago, string numReciboPago, string codCheque)
        {
            ContratistasEntidad contratistasEntidad = new ContratistasEntidad();
            contratistasEntidad.CodCheque = Convert.ToInt32(codCheque);
            contratistasEntidad.CodContrato = Convert.ToInt32(codContrato);
            contratistasEntidad.MontoPagar = double.Parse(montoPagar, CultureInfo.InvariantCulture);         
            contratistasEntidad.NumMedioPago = numMedioPago;
            contratistasEntidad.FechaPago = fechaPago;
            contratistasEntidad.CodTipoReciboPago = Convert.ToInt32(codTipoReciboPago);
            contratistasEntidad.NumReciboPago = numReciboPago;
            contratistasEntidad.CodUsuarioRegistro = Convert.ToInt32(Session["CodUsuarioInterno"].ToString());
            return procesoContratista.RegistrarPagoContratistasPorCheque(contratistasEntidad);
        }
        private string GuardarPagoProveedor(string codProveedorProyecto, string montoPagar, string numMedioPago, string fechaPago, string codTipoReciboPago, string numReciboPago, string codCheque)
        {
            ProveedoresEntidad proveedoresEntidad = new ProveedoresEntidad();
            proveedoresEntidad.CodCheque = Convert.ToInt32(codCheque);
            proveedoresEntidad.CodProveedorProyectoPlanilla = Convert.ToInt32(codProveedorProyecto);
            proveedoresEntidad.MontoPagar = double.Parse(montoPagar, CultureInfo.InvariantCulture);
            proveedoresEntidad.NumMedioPago = numMedioPago;
            proveedoresEntidad.FechaPago = fechaPago;
            proveedoresEntidad.CodTipoReciboPago = Convert.ToInt32(codTipoReciboPago);
            proveedoresEntidad.NumReciboPago = numReciboPago;
            proveedoresEntidad.CodUsuarioRegistro = Convert.ToInt32(Session["CodUsuarioInterno"].ToString());
            return procesoProveedor.RegistrarPagoProveedoresPorCheque(proveedoresEntidad);
        }
        private string GuardarPagoProveedorSinProyecto(string codProveedor, string montoPagar, string numMedioPago, string fechaPago, string codTipoReciboPago, string numReciboPago, string codCheque)
        {
            ProveedoresEntidad proveedoresEntidad = new ProveedoresEntidad();
            proveedoresEntidad.CodCheque = Convert.ToInt32(codCheque);
            proveedoresEntidad.CodProveedor = Convert.ToInt32(codProveedor);
            proveedoresEntidad.MontoPagar = double.Parse(montoPagar, CultureInfo.InvariantCulture);
            proveedoresEntidad.NumMedioPago = numMedioPago;
            proveedoresEntidad.FechaPago = fechaPago;
            proveedoresEntidad.CodTipoReciboPago = Convert.ToInt32(codTipoReciboPago);
            proveedoresEntidad.NumReciboPago = numReciboPago;
            proveedoresEntidad.CodUsuarioRegistro = Convert.ToInt32(Session["CodUsuarioInterno"].ToString());
            return procesoProveedor.RegistrarPagoProveedoresSinProyectoPorCheque(proveedoresEntidad);
        }
        private string EmitirCheque(string codCheque)
        {
            return procesoCheques.EmitirCheque(Convert.ToInt32(codCheque), Convert.ToInt32(Session["CodUsuarioInterno"].ToString()));
        }
        private string AnularCheque(string codCheque)
        {
            return procesoCheques.AnularCheque(Convert.ToInt32(codCheque), Convert.ToInt32(Session["CodUsuarioInterno"].ToString()));
        }
        private void CargarDatosCheque(string codCheque)
        {
            try
            {
                LimpiarFormulario1();
                LimpiarFormulario2();
                LimpiarSeleccion();

                DataTable dt = procesoCheques.ObtenerDatosCheque(Convert.ToInt32(codCheque));
                DataTable parametros;

                if (dt.Rows.Count > 0)
                {
                    cboTipoPagoChequeRegistro.SelectedValue = dt.Rows[0][1].ToString();
                    cboTipoPagoChequeRegistro_SelectedIndexChanged(cboTipoPagoChequeRegistro.SelectedValue, new EventArgs());
                    txtNumeroCheque.Text = dt.Rows[0][2].ToString();
                    cboTipoDocumentoOrdenPagar.SelectedValue = dt.Rows[0][3].ToString();
                    txtNumeroDocumentoIdentidadOrdenPagar.Text = dt.Rows[0][4].ToString();
                    txtNombreCompletoOrdenPagar.Text = dt.Rows[0][5].ToString();
                    cboTipoMoneda.SelectedValue = dt.Rows[0][6].ToString();
                    if (dt.Rows[0][6].ToString() == "1") { txtMontoPagar.Enabled = false; } else { txtMontoPagar.Enabled = true; }
                    txtMontoPagar.Text = dt.Rows[0][7].ToString();
                    txtMontoPagarEnLetras.Text = dt.Rows[0][8].ToString();
                    txtFechaPago.Text = dt.Rows[0][9].ToString();

                    if (dt.Rows[0][1].ToString() == "1")
                    {
                        parametros = procesoCheques.ObtenerDatosChequePersonaContratista(Convert.ToInt32(codCheque));

                        if (parametros.Rows.Count > 0)
                        {
                            cboTipoPersonaPagoChequeRegistro.SelectedValue = parametros.Rows[0][0].ToString();
                            cboTipoPersonaPagoChequeRegistro_SelectedIndexChanged(cboTipoPersonaPagoChequeRegistro.SelectedValue, new EventArgs());
                            txtValoresCheckBox.Text = "," + parametros.Rows[0][1].ToString();
                            txtValoresContratos.Text = "," + parametros.Rows[0][2].ToString();
                            cboTipoReciboPago.SelectedValue = parametros.Rows[0][3].ToString();
                            txtNumeroReciboPago.Text = parametros.Rows[0][4].ToString();

                            GenerarListadoPagarContratista(parametros.Rows[0][1].ToString());
                            MantenerValoresPago("," + parametros.Rows[0][2].ToString());                            

                            gpFormularioBusqueda.Visible = false;
                            gpFormularioCheque.Visible = true;
                            gpResumenChequeEmitir.Visible = false;
                            gpButtonNuevoCheque.Visible = false;
                            gpReciboPagoCheque.Visible = true;
                            gpListadoChequesBusqueda.Visible = false;

                            gpSeleccionTipoPersonaPersonaPagar.Visible = false;
                            gpSeleccionListadoTipoPersonaPagar.Visible = false;
                            gpSeleccionFormularioFinalPersonaPagar.Visible = true;

                            Session["CodChequeModificar"] = codCheque;
                            Session["MontoTotalCheque"] = dt.Rows[0][7].ToString();
                            Session["EstadoCheque"] = dt.Rows[0][10].ToString(); ;
                        }
                    }
                    else if (dt.Rows[0][1].ToString() == "2")
                    {
                        parametros = procesoCheques.ObtenerDatosChequePersonaProveedor(Convert.ToInt32(codCheque));

                        if (parametros.Rows.Count > 0)
                        {
                            cboTipoPersonaPagoChequeRegistro.SelectedValue = parametros.Rows[0][0].ToString();
                            cboTipoPersonaPagoChequeRegistro_SelectedIndexChanged(cboTipoPersonaPagoChequeRegistro.SelectedValue, new EventArgs());                                                 
                            cboTipoReciboPago.SelectedValue = parametros.Rows[0][3].ToString();
                            txtNumeroReciboPago.Text = parametros.Rows[0][4].ToString();

                            if (parametros.Rows[0][1].ToString() != string.Empty)
                            {
                                txtValoresCheckBox.Text = "," + parametros.Rows[0][1].ToString();
                                GenerarListadoPagarProveedores(parametros.Rows[0][1].ToString());
                            }

                            if (parametros.Rows[0][2].ToString() != string.Empty)
                            {
                                txtValoresContratos.Text = "," + parametros.Rows[0][2].ToString();
                                MantenerValoresPago("," + parametros.Rows[0][2].ToString());
                            }                                                   

                            gpFormularioBusqueda.Visible = false;
                            gpFormularioCheque.Visible = true;
                            gpResumenChequeEmitir.Visible = false;
                            gpButtonNuevoCheque.Visible = false;
                            gpReciboPagoCheque.Visible = true;
                            gpListadoChequesBusqueda.Visible = false;

                            gpSeleccionTipoPersonaPersonaPagar.Visible = false;
                            gpSeleccionListadoTipoPersonaPagar.Visible = false;
                            gpSeleccionFormularioFinalPersonaPagar.Visible = true;

                            Session["CodChequeModificar"] = codCheque;
                            Session["MontoTotalCheque"] = dt.Rows[0][7].ToString();
                            Session["EstadoCheque"] = dt.Rows[0][10].ToString(); ;
                        }
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('No se pudo obtener los datos del cheque seleccionado.','info');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }           
        }
        private void BuscarChequeTipoPago(string codTipoPago, string fechaInicio, string fechaFin)
        {
            try
            {
                DataTable dt = procesoCheques.BuscarCheques(Convert.ToInt32(codTipoPago), fechaInicio, fechaFin);
                dgvContratistas.DataSource = dt;
                dgvContratistas.DataBind();
                lblResultadoBusqueda.InnerHtml = "" + dt.Rows.Count.ToString() + " registros(s) encontrado(s)";
                gpListadoChequesBusqueda.Visible = true;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }          
        }
        private void GenerarResumenCheque(string codCheque)
        {
            DataTable dt = procesoCheques.ObtenerDatosChequeResumen(Convert.ToInt32(codCheque));

            if (dt.Rows.Count > 0)
            {
                lblFechaPagoChequeResumen.Text = dt.Rows[0][0].ToString().Trim();
                lblNumeroChequeResumen.Text = dt.Rows[0][1].ToString().Trim();
                lblTipoPersonaPagoChequeResumen.Text = dt.Rows[0][2].ToString().Trim();
                lblPagoOrdenDeChequeResumen.Text = dt.Rows[0][3].ToString().Trim();
                lblTipoMonedaChequeResumen.Text = dt.Rows[0][4].ToString().Trim();
                lblSubTotalPagarChequeResumen.Text = dt.Rows[0][5].ToString().Trim();
                lblTotalPagarChequeResumen.InnerText = dt.Rows[0][5].ToString().Trim();
            }
        }
        private void CargarTipoPagoCheque()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = procesoCheques.CargarTipoPagoCheque();
                cboTipoPagoChequeBusqueda.DataSource = dt;
                cboTipoPagoChequeBusqueda.DataTextField = "DescripcionTipoPago";
                cboTipoPagoChequeBusqueda.DataValueField = "CodTipoPago";
                cboTipoPagoChequeBusqueda.DataBind();
                cboTipoPagoChequeBusqueda.Items.Insert(0, "Seleccione");

                cboTipoPagoChequeRegistro.DataSource = dt;
                cboTipoPagoChequeRegistro.DataTextField = "DescripcionTipoPago";
                cboTipoPagoChequeRegistro.DataValueField = "CodTipoPago";
                cboTipoPagoChequeRegistro.DataBind();
                cboTipoPagoChequeRegistro.Items.Insert(0, "Seleccione");
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }    
        private void CargarContratistas()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = procesoContratista.CargarContratistas();
                cboTipoPersonaPagoChequeRegistro.DataSource = dt;
                cboTipoPersonaPagoChequeRegistro.DataTextField = "DescripcionProveedor";
                cboTipoPersonaPagoChequeRegistro.DataValueField = "CodProveedor";
                cboTipoPersonaPagoChequeRegistro.DataBind();
                cboTipoPersonaPagoChequeRegistro.Items.Insert(0, new ListItem("Seleccione", "0"));
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }
        private void CargarProveedores()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = procesoProveedor.CargarProveedores();
                cboTipoPersonaPagoChequeRegistro.DataSource = dt;
                cboTipoPersonaPagoChequeRegistro.DataTextField = "DescripcionProveedor";
                cboTipoPersonaPagoChequeRegistro.DataValueField = "CodProveedor";
                cboTipoPersonaPagoChequeRegistro.DataBind();
                cboTipoPersonaPagoChequeRegistro.Items.Insert(0, new ListItem("Seleccione", "0"));
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }
        private void CargarTipoMoneda()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = procesoCheques.CargarTipoMoneda();
                cboTipoMoneda.DataSource = dt;
                cboTipoMoneda.DataTextField = "DescripcionTipoMoneda";
                cboTipoMoneda.DataValueField = "CodTipoMoneda";
                cboTipoMoneda.DataBind();
                cboTipoMoneda.Items.Insert(0, new ListItem("Seleccione", "0"));     
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }
        private void CargarTipoDocumentoIdentidad()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = procesoTrabajador.CargarTipoDocumentoIdentidad();
                cboTipoDocumentoOrdenPagar.DataSource = dt;
                cboTipoDocumentoOrdenPagar.DataTextField = "DescTipDocumentoIdent";
                cboTipoDocumentoOrdenPagar.DataValueField = "CodTipDocumentoIdent";
                cboTipoDocumentoOrdenPagar.DataBind();
                cboTipoDocumentoOrdenPagar.Items.Insert(0, new ListItem("Seleccione", "0"));
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
                dt = procesoContratista.CargarTipoRecibosPago();
                cboTipoReciboPago.DataSource = dt;
                cboTipoReciboPago.DataTextField = "DescripcionReciboPago";
                cboTipoReciboPago.DataValueField = "CodReciboPago";
                cboTipoReciboPago.DataBind();
                cboTipoReciboPago.Items.Insert(0, new ListItem("Seleccione", "0"));
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }
        private void ObtenerPagosContratistas(string codContratista)
        {
            try
            {
                DataTable dt = procesoContratista.CargarContratosPagarContratista(Convert.ToInt32(codContratista));
                string html = "<table class='m-datatable' id='base_column_width'><thead><tr><th>#</th><th>CONTRATO</th><th>PROYECTO</th><th>MONTO CONTRATO</th><th>MONTO PAGADO</th><th>MONTO PENDIENTE</th><th>OPCIÓN</th></tr></thead>";
                try
                {
                    html += "<tbody>";
                    if (dt.Rows.Count > 0)
                    {
                        int contadorfilas = 1;
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            string codContrato = dt.Rows[i][0].ToString();
                            string contrato = dt.Rows[i][1].ToString();
                            string proyecto = dt.Rows[i][2].ToString();
                            string montoTotalContrato = dt.Rows[i][3].ToString();
                            string montoPagado = dt.Rows[i][4].ToString();
                            string montoPendiente = dt.Rows[i][5].ToString();                            

                            html += "<tr id='" + contadorfilas + "'><td class='m-datatable__cell--center m-datatable__cell'>" + contadorfilas + "</td>";
                            
                            html += "<td style='text-align:center;vertical-align:middle'>" + contrato + "</td>";
                            html += "<td style='text-align:center;vertical-align:middle'>" + proyecto + "</td>";
                            html += "<td style='text-align:center;vertical-align:middle'>" + montoTotalContrato + "</td>";
                            html += "<td style='text-align:center;vertical-align:middle'>" + montoPagado + "</td>";
                            html += "<td style='text-align:center;vertical-align:middle'>" + montoPendiente + "</td>";

                            html += "<td><input type='checkbox' class='i-checks clickme' id='C_" + codContrato + "' value='" + codContrato + "'/></td>";                            

                            html += "</tr>";

                            contadorfilas += 1;
                        }
                    }

                    html += "</tbody></table>";
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    lblResultadoTipoPersonaPagar.InnerText = dt.Rows.Count.ToString() + " registros(s) encontrado(s)";
                    gpTablaResultadoTipoPersonarPagar.InnerHtml = html;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }
        private void ObtenerPagosProveedores(string codProveedor)
        {
            try
            {
                DataTable dt = procesoProveedor.CargarProyectosPagarProveedores(Convert.ToInt32(codProveedor));
                string html = "<table class='m-datatable' id='base_column_width'><thead><tr><th>#</th><th>PROYECTO</th><th>PRESUPUESTO</th><th>MONTO PAGADO</th><th>OPCIÓN</th></tr></thead>";
                try
                {
                    html += "<tbody>";
                    if (dt.Rows.Count > 0)
                    {
                        int contadorfilas = 1;
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            string codProyectoProveedor = dt.Rows[i][0].ToString();                            
                            string proyecto = dt.Rows[i][1].ToString();
                            string presupuesto = dt.Rows[i][2].ToString();
                            string montoPagado = dt.Rows[i][3].ToString();                            

                            html += "<tr id='" + contadorfilas + "'><td class='m-datatable__cell--center m-datatable__cell'>" + contadorfilas + "</td>";
                            
                            html += "<td style='text-align:center;vertical-align:middle'>" + proyecto + "</td>";
                            html += "<td style='text-align:center;vertical-align:middle'>" + presupuesto + "</td>";
                            html += "<td style='text-align:center;vertical-align:middle'>" + montoPagado + "</td>";                            

                            html += "<td><input type='checkbox' class='i-checks clickme' id='C_" + codProyectoProveedor + "' value='" + codProyectoProveedor + "'/></td>";

                            html += "</tr>";

                            contadorfilas += 1;
                        }
                    }

                    html += "</tbody></table>";
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    lblResultadoTipoPersonaPagar.InnerText = dt.Rows.Count.ToString() + " registros(s) encontrado(s)";
                    gpTablaResultadoTipoPersonarPagar.InnerHtml = html;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }
        private void GenerarListadoPagarContratista(string textoValores)
        {
            DataTable dt = procesoCheques.ObtenerContratosPagarContratistaCheque(textoValores);
            string html = string.Empty;

            try
            {                
                if (dt.Rows.Count > 0)
                {
                    html = "<div class='col-md-12'> <div class='m-demo'> <div class='m-demo__preview'> <div class='m-list-search'> <div class='m-list-search__results'> <div class='row'> <div class='col-md-1'> <span class='m-list-search__result-category m-list-search__result-category--first'> </span> </div><div class='col-md-5'> <span class='m-list-search__result-category m-list-search__result-category--first'>LISTA POR PAGAR </span> </div><div class='col-md-2'> <span class='m-list-search__result-category m-list-search__result-category--first'>MONTO PENDIENTE </span> </div><div class='col-md-4'> <span class='m-list-search__result-category m-list-search__result-category--first'>IMPORTE A PAGAR S/.</span> </div></div>";
                    int contadorfilas = 1;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string codContrato = dt.Rows[i][0].ToString();
                        string contratoProyecto = dt.Rows[i][1].ToString();  
                        string montoPendiente = dt.Rows[i][2].ToString();

                        html += "<span class='m-list-search__result-item'> <div class='row'> <div class='col-md-1'> <span class='m-list-search__result-item-icon'> <i class='la la-plus m--font-danger'></i> </span> </div><div class='col-md-5'> <span class='m-list-search__result-item-text'>" + contratoProyecto + "</span> </div><div class='col-md-2'> <span class='m-list-search__result-item-text'>" + montoPendiente + "</span> </div><div class='col-md-4'> <div class='m-input-icon m-input-icon--right'> <input type='text' class='form-control form-control-sm m-input onlynumbers montopendientecalcular' id=" + codContrato + " onchange='CalcularMontoTotal()' /> <span class='m-input-icon__icon m-input-icon__icon--right'> <span> <i class='la la-money'></i> </span> </span> </div></div></div></span>";

                        contadorfilas += 1;
                    }
                }
                //<div class='m-cheque_subtotal'><div class='m-cheque_subtotal__body'> <span class='m-cheque_subtotal__titulo'>TOTAL CALCULADO</span> <span class='m-cheque_subtotal__importe'>S/. <span class='subtotalimporte'>0</span></span> </div></div>
                html += "</div></div></div></div></div>";
            }
            catch (Exception ex)
            {

            }
            finally
            {                
                gpListadoPagarChequeResumen.InnerHtml = html;
            }
        }
        private void GenerarListadoPagarProveedores(string textoValores)
        {
            DataTable dt = procesoCheques.ObtenerProyectosPagarProveedoresCheque(textoValores);
            string html = string.Empty;

            try
            {
                if (dt.Rows.Count > 0)
                {
                    html = "<div class='col-md-12'> <div class='m-demo'> <div class='m-demo__preview'> <div class='m-list-search'> <div class='m-list-search__results'> <div class='row'> <div class='col-md-1'> <span class='m-list-search__result-category m-list-search__result-category--first'> </span> </div><div class='col-md-5'> <span class='m-list-search__result-category m-list-search__result-category--first'>LISTA POR PAGAR </span> </div><div class='col-md-2'> <span class='m-list-search__result-category m-list-search__result-category--first'>PRESUPUESTO </span> </div><div class='col-md-4'> <span class='m-list-search__result-category m-list-search__result-category--first'>IMPORTE A PAGAR S/.</span> </div></div>";
                    int contadorfilas = 1;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string codProyectoProveedor = dt.Rows[i][0].ToString();
                        string proyecto = dt.Rows[i][1].ToString();
                        string presupuesto = dt.Rows[i][2].ToString();

                        html += "<span class='m-list-search__result-item'> <div class='row'> <div class='col-md-1'> <span class='m-list-search__result-item-icon'> <i class='la la-plus m--font-danger'></i> </span> </div><div class='col-md-5'> <span class='m-list-search__result-item-text'>" + proyecto + "</span> </div><div class='col-md-2'> <span class='m-list-search__result-item-text'>" + presupuesto + "</span> </div><div class='col-md-4'> <div class='m-input-icon m-input-icon--right'> <input type='text' class='form-control form-control-sm m-input onlynumbers montopendientecalcular' id=" + codProyectoProveedor + " onchange='CalcularMontoTotal()' /> <span class='m-input-icon__icon m-input-icon__icon--right'> <span> <i class='la la-money'></i> </span> </span> </div></div></div></span>";

                        contadorfilas += 1;
                    }
                }
                
                html += "</div></div></div></div></div>";
            }
            catch (Exception ex)
            {

            }
            finally
            {
                gpListadoPagarChequeResumen.InnerHtml = html;
            }
        }
        private void GenerarCambioMonedaMontoTotal(string codTipoMoneda, string montoTotalCheque)
        {
            DataTable dt = procesoCheques.ObtenerTipoCambio(Convert.ToInt32(codTipoMoneda), double.Parse(montoTotalCheque, CultureInfo.InvariantCulture));

            if (dt.Rows.Count > 0)
            {
                txtMontoPagar.Text = dt.Rows[0][0].ToString().Trim();
                btnGenerarMontoLetras_Click(btnGenerarMontoLetras, new EventArgs());
            }
        }
        private void GenerarMontoEnLetras(string montoTotal)
        {
            DataTable dt = procesoCheques.ObtenerMontoPagarLetras(double.Parse(montoTotal, CultureInfo.InvariantCulture));

            if (dt.Rows.Count > 0)
            {
                txtMontoPagarEnLetras.Text = dt.Rows[0][0].ToString().Trim();              
            }
        }
        private void LimpiarFormulario1()
        {
            txtValoresCheckBox.Text = string.Empty;                   
            cboTipoPersonaPagoChequeRegistro.ClearSelection();
            cboTipoPagoChequeRegistro.ClearSelection();
        }
        private void LimpiarFormulario2()
        {          
            txtValoresContratos.Text = string.Empty;
            txtFechaPago.Text = string.Empty;
            txtNumeroCheque.Text = string.Empty;
            txtMontoPagar.Text = string.Empty;
            txtMontoPagarEnLetras.Text = string.Empty;
            txtNumeroDocumentoIdentidadOrdenPagar.Text = string.Empty;
            txtNombreCompletoOrdenPagar.Text = string.Empty;
            txtNumeroReciboPago.Text = string.Empty;
            cboTipoMoneda.ClearSelection();
            cboTipoDocumentoOrdenPagar.ClearSelection();
            cboTipoReciboPago.ClearSelection();
            cboBancoCheque.ClearSelection();

            gpListadoPagarChequeResumen.InnerHtml = string.Empty;
            txtMontoPagar.Enabled = true;

            Session["MontoTotalCheque"] = "0";
        }
        private void LimpiarSeleccion()
        {
            DataTable dt = new DataTable();
            cboTipoPersonaPagoChequeRegistro.DataSource = dt;
            cboTipoPersonaPagoChequeRegistro.DataBind();

            lblResultadoTipoPersonaPagar.InnerText = string.Empty;
            gpTablaResultadoTipoPersonarPagar.InnerHtml = string.Empty;
            gpListadoPagarChequeResumen.InnerHtml = string.Empty;

            gpSeleccionTipoPersonaPersonaPagar.Visible = true;
            gpSeleccionListadoTipoPersonaPagar.Visible = false;
            gpSeleccionFormularioFinalPersonaPagar.Visible = false;
        }
        private void ReloadJqueryCheckBox(string parametros)
        {
            string parametrosCadena = parametros.Trim();
            string parametrosValido = parametros.Substring(1, parametros.Length - 1);
            string[] Lista_Parametros = parametrosValido.Split(',');
            string scriptJquery = "";

            for (int i = 0; i < Lista_Parametros.Length; i++)
            {
                scriptJquery += "$('#C_" + Lista_Parametros[i].ToString() + "').prop('checked',true).iCheck('update');";
            }

            scriptJquery += "function pageLoad() { $(function () { ReloadJquery(); RebuildDataTable(); " + scriptJquery + " });}";

            ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "MantenerSeleccionados", scriptJquery, true);
        }
        private void ReloadiChecks()
        {
            string scriptJquery = string.Empty;
            scriptJquery = "function pageLoad() { $(function () { ReloadJquery(); RebuildDataTable(); $('.i-checks').iCheck('uncheck'); });}";

            ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "MantenerSeleccionados", scriptJquery, true);
        }
        private void MantenerValoresPago(string parametros)
        {
            string parametrosCadena = parametros.Trim();
            string parametrosValido = parametros.Substring(1, parametros.Length - 1);
            string[] Lista_Parametros = parametrosValido.Split(',');
            string scriptJquery = "";
            double sumavalores = 0;

            for (int i = 0; i < Lista_Parametros.Length; i++)
            {
                scriptJquery += "$('#" + Lista_Parametros[i].ToString().Split('_')[0] + "').val('" + Lista_Parametros[i].ToString().Split('_')[1] + "');";

                sumavalores += double.Parse(Lista_Parametros[i].ToString().Split('_')[1].ToString(), CultureInfo.InvariantCulture);
            }

            scriptJquery += scriptJquery + "$('.subtotalimporte').text('" + sumavalores.ToString() + "');";

            ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "MantenerValores", scriptJquery, true);
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

            gpFormularioBusqueda.Visible = false;
            gpFormularioCheque.Visible = false;
            gpResumenChequeEmitir.Visible = false;
            gpButtonNuevoCheque.Visible = false;
            gpReciboPagoCheque.Visible = false;
            gpListadoChequesBusqueda.Visible = false;

            gpSeleccionTipoPersonaPersonaPagar.Visible = false;
            gpSeleccionListadoTipoPersonaPagar.Visible = false;
            gpSeleccionFormularioFinalPersonaPagar.Visible = false;

            gpReporteRVIndividual.Style.Add("display", "normal");
        }
        private void CargarBancos()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = procesoTrabajador.ObtenerBancos();
                cboBancoCheque.DataSource = dt;
                cboBancoCheque.DataTextField = "DescBanco";
                cboBancoCheque.DataValueField = "CodBanco";
                cboBancoCheque.DataBind();
                cboBancoCheque.Items.Insert(0, new ListItem("SELECCIONE UN BANCO", "0"));
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }
        private void AutoCompletarInformacion(string codPersona, string codTipoPersona)
        {
            DataTable dt = new DataTable();
            dt = procesoCheques.ObtenerDatosPersonaCheque(Convert.ToInt32(codPersona), Convert.ToInt32(codTipoPersona));

            if (dt.Rows.Count > 0)
            {
                cboTipoDocumentoOrdenPagar.SelectedValue = dt.Rows[0][1].ToString().Trim();
                txtNumeroDocumentoIdentidadOrdenPagar.Text = dt.Rows[0][0].ToString().Trim();
                txtNombreCompletoOrdenPagar.Text = dt.Rows[0][2].ToString().Trim();
            }
        }
    }
}