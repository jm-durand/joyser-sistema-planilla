using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogicaNegocio;
using Entidad;
using Microsoft.Reporting.WebForms;
using System.Drawing;
using System.Configuration;

namespace SistemaGestionPlanilla.Planilla
{
    public partial class GenerarPlanillaEmpleados : System.Web.UI.Page
    {
        PlanillaNegocio procesoPlanilla = new PlanillaNegocio();
        TrabajadorNegocio procesosTrabajador = new TrabajadorNegocio();
        ReporteNegocio procesoReporte = new ReporteNegocio();
        CentroCostosNegocio procesoCentroCostos = new CentroCostosNegocio();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["CodUsuarioInterno"] != null)
                {
                    CargarListadoTrabajadores();

                    txtFechaInicial.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    txtFechaFinal.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    txtFechaInicialReporte.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    txtFechaFinalReporte.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    txtFechaAnoPeriodoSelect.Text = DateTime.Now.Year.ToString();

                    gpBoletaPlanilla.Visible = false;
                    gpFormularioTrabajador.Visible = false;
                    gpFormularioParametros.Visible = false;
                    gpBtnGenerarBoleta.Visible = false;

                    Session["CodPlanillaModificar"] = "";
                    Session["CodTipoAportacion"] = "";
                    gpReporteRVIndividual.Style.Add("display", "none");
                    PlanillasRecientes(txtFechaInicialReporte.Text.Trim(), txtFechaFinalReporte.Text.Trim());
                }
                else
                {
                    Response.Redirect(ConfigurationManager.AppSettings["AssetsUrl"] + "/Seguridad/Logout.aspx");
                }
            }
        }
        protected void btnMostrarListaTrabajador_Click(object sender, EventArgs e)
        {
            gpFormularioTrabajador.Visible = true;
            gpPlanillaRecientes.Visible = false;
        }
        protected void cboTrabajadores_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cboTrabajadores.SelectedValue.ToString() != "Seleccione")
                {
                    CargarDatosTrabajadorPlanilla(cboTrabajadores.SelectedValue.ToString(), "3");
                    ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Rangofecha", "CargarDateRange();", true);

                    gpFormularioParametros.Visible = true;
                    gpBtnGenerarBoleta.Visible = true;

                    LimpiarParametros();
                }
                else
                {
                    gpFormularioParametros.Visible = false;
                    gpBtnGenerarBoleta.Visible = false;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }
        protected void txtDiasLaborados_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (cboTrabajadores.SelectedValue != "Seleccione")
                {
                    ObtenerIngresos(cboTrabajadores.SelectedValue.ToString(), txtDiasLaborados.Text.Trim(), "3");
                   
                    ObtenerTotalIngresos();
                    ObtenerTotalBeneficios();

                    ObtenerDescuentos(cboTrabajadores.SelectedValue.ToString(), txtDiasLaborados.Text.Trim(), txtDiasDomingosFeriados.Text.Trim(), Session["TotalVacacionesTruncas"].ToString(), Session["TotalIngresos"].ToString(), "3");
                    ObtenerTotalDescuentos();

                    if (chkBonificacionExtra.Checked)
                    {
                        ObtenerBeneficiosExtras(cboTrabajadores.SelectedValue.ToString(), txtDiasLaborados.Text.Trim(), "3");
                        ObtenerTotalBeneficios();
                    }

                    if (chkVacaciones.Checked)
                    {                        
                        ObtenerBeneficiosVacaciones(cboTrabajadores.SelectedValue.ToString(), txtDiasLaborados.Text.Trim(), txtDiasDomingosFeriados.Text.Trim(), cboMesPeriodo.SelectedValue.ToString().Trim(), "3");
                        ObtenerTotalBeneficios();
                    }

                    if (chkGratificacion.Checked)
                    {
                        ObtenerBeneficiosGratificacion(cboTrabajadores.SelectedValue.ToString(), txtDiasLaborados.Text.Trim(), cboMesPeriodo.SelectedValue.ToString().Trim(), "3");
                        ObtenerTotalBeneficios();
                    }

                    ObtenerTotalPagar();
                    ObtenerAportacionEmpleador(cboTrabajadores.SelectedValue.ToString(), txtDiasLaborados.Text.Trim(), Session["TotalIngresos"].ToString(), "3");
                    ObtenerTotalaAportacionesEmpleador();
                    ObtenerCostoTotalTrabajador();
                }
                else
                {

                    txtDiasLaborados.Text = "0";
                    ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('Debe seleccionar un trabajador para generar el calculo','info');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }
        protected void txtDiasDomingosFeriados_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (cboTrabajadores.SelectedValue != "Seleccione")
                {
                    ObtenerDomingoFeriados(cboTrabajadores.SelectedValue.ToString(), txtDiasDomingosFeriados.Text.Trim(), "3");
                    ObtenerTotalIngresos();
                    ObtenerTotalBeneficios();
                    ObtenerDescuentos(cboTrabajadores.SelectedValue.ToString(), txtDiasLaborados.Text.Trim(), txtDiasDomingosFeriados.Text.Trim(), Session["TotalVacacionesTruncas"].ToString(), Session["TotalIngresos"].ToString(), "3");
                    ObtenerTotalDescuentos();

                    if (chkBonificacionExtra.Checked)
                    {
                        ObtenerBeneficiosExtras(cboTrabajadores.SelectedValue.ToString(), txtDiasLaborados.Text.Trim(), "3");
                        ObtenerTotalBeneficios();
                    }

                    if (chkVacaciones.Checked)
                    {
                        ObtenerBeneficiosVacaciones(cboTrabajadores.SelectedValue.ToString(), txtDiasLaborados.Text.Trim(), txtDiasDomingosFeriados.Text.Trim(), cboMesPeriodo.SelectedValue.ToString().Trim(), "3");
                        ObtenerTotalBeneficios();
                    }

                    ObtenerTotalPagar();
                    ObtenerAportacionEmpleador(cboTrabajadores.SelectedValue.ToString(), txtDiasLaborados.Text.Trim(), Session["TotalIngresos"].ToString(), "3");
                    ObtenerTotalaAportacionesEmpleador();
                    ObtenerCostoTotalTrabajador();
                }
                else
                {
                    txtDiasDomingosFeriados.Text = "0";
                    ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('Debe seleccionar un trabajador para generar el calculo','info');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }
        protected void txtAsignacionFamiliar_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ObtenerTotalIngresos();
                ObtenerTotalBeneficios();
                ObtenerDescuentos(cboTrabajadores.SelectedValue.ToString(), txtDiasLaborados.Text.Trim(), txtDiasDomingosFeriados.Text.Trim(), Session["TotalVacacionesTruncas"].ToString(), Session["TotalIngresos"].ToString(), "3");
                ObtenerTotalDescuentos();

                if (chkBonificacionExtra.Checked)
                {
                    ObtenerBeneficiosExtras(cboTrabajadores.SelectedValue.ToString(), txtDiasLaborados.Text.Trim(), "3");
                    ObtenerTotalBeneficios();
                }

                ObtenerTotalPagar();
                ObtenerAportacionEmpleador(cboTrabajadores.SelectedValue.ToString(), txtDiasLaborados.Text.Trim(), Session["TotalIngresos"].ToString(), "3");
                ObtenerTotalaAportacionesEmpleador();
                ObtenerCostoTotalTrabajador();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }      
        protected void txtHorasExtrasSimple_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ObtenerTotalIngresos();
                ObtenerTotalBeneficios();
                ObtenerDescuentos(cboTrabajadores.SelectedValue.ToString(), txtDiasLaborados.Text.Trim(), txtDiasDomingosFeriados.Text.Trim(), Session["TotalVacacionesTruncas"].ToString(), Session["TotalIngresos"].ToString(), "3");
                ObtenerTotalDescuentos();

                if (chkBonificacionExtra.Checked)
                {
                    ObtenerBeneficiosExtras(cboTrabajadores.SelectedValue.ToString(), txtDiasLaborados.Text.Trim(), "3");
                    ObtenerTotalBeneficios();
                }

                ObtenerTotalPagar();
                ObtenerAportacionEmpleador(cboTrabajadores.SelectedValue.ToString(), txtDiasLaborados.Text.Trim(), Session["TotalIngresos"].ToString(), "3");
                ObtenerTotalaAportacionesEmpleador();
                ObtenerCostoTotalTrabajador();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }   
        protected void txtPasajesTrabajador_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ObtenerTotalIngresos();
                ObtenerTotalBeneficios();
                ObtenerDescuentos(cboTrabajadores.SelectedValue.ToString(), txtDiasLaborados.Text.Trim(), txtDiasDomingosFeriados.Text.Trim(), Session["TotalVacacionesTruncas"].ToString(), Session["TotalIngresos"].ToString(), "3");
                ObtenerTotalDescuentos();

                if (chkBonificacionExtra.Checked)
                {
                    ObtenerBeneficiosExtras(cboTrabajadores.SelectedValue.ToString(), txtDiasLaborados.Text.Trim(), "3");
                    ObtenerTotalBeneficios();
                }

                ObtenerTotalPagar();
                ObtenerAportacionEmpleador(cboTrabajadores.SelectedValue.ToString(), txtDiasLaborados.Text.Trim(), Session["TotalIngresos"].ToString(), "3");
                ObtenerTotalaAportacionesEmpleador();
                ObtenerCostoTotalTrabajador();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }
        protected void txtOtrosIngresos_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ObtenerTotalIngresos();
                ObtenerTotalBeneficios();
                ObtenerDescuentos(cboTrabajadores.SelectedValue.ToString(), txtDiasLaborados.Text.Trim(), txtDiasDomingosFeriados.Text.Trim(), Session["TotalVacacionesTruncas"].ToString(), Session["TotalIngresos"].ToString(), "3");
                ObtenerTotalDescuentos();

                if (chkBonificacionExtra.Checked)
                {
                    ObtenerBeneficiosExtras(cboTrabajadores.SelectedValue.ToString(), txtDiasLaborados.Text.Trim(), "3");
                    ObtenerTotalBeneficios();
                }

                ObtenerTotalPagar();
                ObtenerAportacionEmpleador(cboTrabajadores.SelectedValue.ToString(), txtDiasLaborados.Text.Trim(), Session["TotalIngresos"].ToString(), "3");
                ObtenerTotalaAportacionesEmpleador();
                ObtenerCostoTotalTrabajador();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }
        protected void txtVacacionesTruncas_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ObtenerTotalIngresos();
                ObtenerTotalBeneficios();
                ObtenerDescuentos(cboTrabajadores.SelectedValue.ToString(), txtDiasLaborados.Text.Trim(), txtDiasDomingosFeriados.Text.Trim(), Session["TotalVacacionesTruncas"].ToString(), Session["TotalIngresos"].ToString(), "3");
                ObtenerTotalDescuentos();

                if (chkBonificacionExtra.Checked)
                {
                    ObtenerBeneficiosExtras(cboTrabajadores.SelectedValue.ToString(), txtDiasLaborados.Text.Trim(), "3");
                    ObtenerTotalBeneficios();
                }

                ObtenerTotalPagar();
                ObtenerAportacionEmpleador(cboTrabajadores.SelectedValue.ToString(), txtDiasLaborados.Text.Trim(), Session["TotalIngresos"].ToString(), "3");
                ObtenerTotalaAportacionesEmpleador();
                ObtenerCostoTotalTrabajador();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }  
        protected void txtOtrosBeneficios_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ObtenerTotalIngresos();
                ObtenerTotalBeneficios();
                ObtenerDescuentos(cboTrabajadores.SelectedValue.ToString(), txtDiasLaborados.Text.Trim(), txtDiasDomingosFeriados.Text.Trim(), Session["TotalVacacionesTruncas"].ToString(), Session["TotalIngresos"].ToString(), "3");
                ObtenerTotalDescuentos();

                if (chkBonificacionExtra.Checked)
                {
                    ObtenerBeneficiosExtras(cboTrabajadores.SelectedValue.ToString(), txtDiasLaborados.Text.Trim(), "3");
                    ObtenerTotalBeneficios();
                }

                ObtenerTotalPagar();
                ObtenerAportacionEmpleador(cboTrabajadores.SelectedValue.ToString(), txtDiasLaborados.Text.Trim(), Session["TotalIngresos"].ToString(), "3");
                ObtenerTotalaAportacionesEmpleador();
                ObtenerCostoTotalTrabajador();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }
        protected void txtRenta5taCategoria_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ObtenerTotalIngresos();
                ObtenerTotalBeneficios();
                ObtenerDescuentos(cboTrabajadores.SelectedValue.ToString(), txtDiasLaborados.Text.Trim(), txtDiasDomingosFeriados.Text.Trim(), Session["TotalVacacionesTruncas"].ToString(), Session["TotalIngresos"].ToString(), "3");
                ObtenerTotalDescuentos();

                if (chkBonificacionExtra.Checked)
                {
                    ObtenerBeneficiosExtras(cboTrabajadores.SelectedValue.ToString(), txtDiasLaborados.Text.Trim(), "3");
                    ObtenerTotalBeneficios();
                }

                ObtenerTotalPagar();
                ObtenerAportacionEmpleador(cboTrabajadores.SelectedValue.ToString(), txtDiasLaborados.Text.Trim(), Session["TotalIngresos"].ToString(), "3");
                ObtenerTotalaAportacionesEmpleador();
                ObtenerCostoTotalTrabajador();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }

        protected void txtEpsTrabajador_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ObtenerTotalIngresos();
                ObtenerTotalBeneficios();
                ObtenerDescuentos(cboTrabajadores.SelectedValue.ToString(), txtDiasLaborados.Text.Trim(), txtDiasDomingosFeriados.Text.Trim(), Session["TotalVacacionesTruncas"].ToString(), Session["TotalIngresos"].ToString(), "3");
                ObtenerTotalDescuentos();

                if (chkBonificacionExtra.Checked)
                {
                    ObtenerBeneficiosExtras(cboTrabajadores.SelectedValue.ToString(), txtDiasLaborados.Text.Trim(), "3");
                    ObtenerTotalBeneficios();
                }

                ObtenerTotalPagar();
                ObtenerAportacionEmpleador(cboTrabajadores.SelectedValue.ToString(), txtDiasLaborados.Text.Trim(), Session["TotalIngresos"].ToString(), "3");
                ObtenerTotalaAportacionesEmpleador();
                ObtenerCostoTotalTrabajador();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }

        protected void txtOtrosDescuentosTrabajador_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ObtenerTotalIngresos();
                ObtenerTotalBeneficios();
                ObtenerDescuentos(cboTrabajadores.SelectedValue.ToString(), txtDiasLaborados.Text.Trim(), txtDiasDomingosFeriados.Text.Trim(), Session["TotalVacacionesTruncas"].ToString(), Session["TotalIngresos"].ToString(), "3");
                ObtenerTotalDescuentos();

                if (chkBonificacionExtra.Checked)
                {
                    ObtenerBeneficiosExtras(cboTrabajadores.SelectedValue.ToString(), txtDiasLaborados.Text.Trim(), "3");
                    ObtenerTotalBeneficios();
                }

                ObtenerTotalPagar();
                ObtenerAportacionEmpleador(cboTrabajadores.SelectedValue.ToString(), txtDiasLaborados.Text.Trim(), Session["TotalIngresos"].ToString(), "3");
                ObtenerTotalaAportacionesEmpleador();
                ObtenerCostoTotalTrabajador();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }
        protected void btnActivarComision_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkComisionFlujo.Checked)
                {
                    ObtenerTotalIngresos();
                    DataTable dt = procesoPlanilla.ObtenerParametrosDescuentosPlanilla(Convert.ToInt32(cboTrabajadores.SelectedValue.ToString()), Convert.ToInt32(txtDiasLaborados.Text.Trim()), Convert.ToInt32(txtDiasDomingosFeriados.Text.Trim()), Convert.ToDouble(Session["TotalIngresos"].ToString()), 3);

                    txtCalculoComisionFlujoTrabajador.Text = Convert.ToDouble(dt.Rows[0][5]).ToString("N", CultureInfo.GetCultureInfo("es-PE"));
                    txtComisionFlujoTrabajador.Text = Convert.ToDouble(dt.Rows[0][6]).ToString("N", CultureInfo.GetCultureInfo("es-PE"));

                    ObtenerTotalBeneficios();
                    ObtenerTotalDescuentos();
                    ObtenerTotalPagar();
                    ObtenerAportacionEmpleador(cboTrabajadores.SelectedValue.ToString(), txtDiasLaborados.Text.Trim(), Session["TotalIngresos"].ToString(), "3");
                    ObtenerTotalaAportacionesEmpleador();
                    ObtenerCostoTotalTrabajador();
                }
                else
                {
                    txtCalculoComisionFlujoTrabajador.Text = "0.00";
                    txtComisionFlujoTrabajador.Text = string.Empty;

                    ObtenerTotalIngresos();
                    ObtenerTotalBeneficios();
                    ObtenerTotalDescuentos();
                    ObtenerTotalPagar();
                    ObtenerAportacionEmpleador(cboTrabajadores.SelectedValue.ToString(), txtDiasLaborados.Text.Trim(), Session["TotalIngresos"].ToString(), "3");
                    ObtenerTotalaAportacionesEmpleador();
                    ObtenerCostoTotalTrabajador();
                }

                if (chkComisionMixta.Checked)
                {
                    ObtenerTotalIngresos();
                    DataTable dt = procesoPlanilla.ObtenerParametrosDescuentosPlanilla(Convert.ToInt32(cboTrabajadores.SelectedValue.ToString()), Convert.ToInt32(txtDiasLaborados.Text.Trim()), Convert.ToInt32(txtDiasDomingosFeriados.Text.Trim()), Convert.ToDouble(Session["TotalIngresos"].ToString()), 3);

                    txtCalculoComisionMixtaTrabajador.Text = Convert.ToDouble(dt.Rows[0][11]).ToString("N", CultureInfo.GetCultureInfo("es-PE"));
                    txtComisionMixtaTrabajador.Text = Convert.ToDouble(dt.Rows[0][12]).ToString("N", CultureInfo.GetCultureInfo("es-PE"));

                    ObtenerTotalBeneficios();
                    ObtenerTotalDescuentos();
                    ObtenerTotalPagar();
                    ObtenerAportacionEmpleador(cboTrabajadores.SelectedValue.ToString(), txtDiasLaborados.Text.Trim(), Session["TotalIngresos"].ToString(), "3");
                    ObtenerTotalaAportacionesEmpleador();
                    ObtenerCostoTotalTrabajador();
                }
                else
                {
                    txtCalculoComisionMixtaTrabajador.Text = "0.00";
                    txtComisionMixtaTrabajador.Text = string.Empty;

                    ObtenerTotalIngresos();
                    ObtenerTotalBeneficios();
                    ObtenerTotalDescuentos();
                    ObtenerTotalPagar();
                    ObtenerAportacionEmpleador(cboTrabajadores.SelectedValue.ToString(), txtDiasLaborados.Text.Trim(), Session["TotalIngresos"].ToString(), "3");
                    ObtenerTotalaAportacionesEmpleador();
                    ObtenerCostoTotalTrabajador();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }
        protected void btnCancelarBoleta_Click(object sender, EventArgs e)
        {
            try
            {
                LimpiarFomulario();

                gpFormularioCalculoPlanilla.Visible = true;
                gpBoletaPlanilla.Visible = false;

                gpFormularioTrabajador.Visible = false;
                gpFormularioParametros.Visible = false;
                gpBtnGenerarBoleta.Visible = false;

                gpPlanillaRecientes.Visible = true;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }
        protected void btnGenerarBoleta_Click(object sender, EventArgs e)
        {
            try
            {                
                if (cboTrabajadores.SelectedValue != "Seleccione")
                {
                    if (cboMesPeriodo.SelectedValue != "0")
                    {
                        gpFormularioCalculoPlanilla.Visible = false;
                        gpBoletaPlanilla.Visible = true;

                        ObtenerTotalIngresos();
                        ObtenerTotalBeneficios();
                        ObtenerTotalDescuentos();
                        ObtenerTotalPagar();
                        ObtenerTotalaAportacionesEmpleador();
                        ObtenerCostoTotalTrabajador();

                        GenerarBoletaTrabajadorPrevio(cboTrabajadores.SelectedValue.ToString());

                        ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "GotoTop", "GotoTop();", true);

                        ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('Vista de Boleta generada correctamente','info');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('Debe seleccionar el mes del periodo de planilla para generar el cálculo','info');", true);
                    }                   
                }
                else
                {
                    ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('Debe seleccionar un trabajador para generar el calculo','info');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }
        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            try
            {
                LimpiarFomulario();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }

        protected void btnRegresarCalculos_Click(object sender, EventArgs e)
        {
            gpFormularioGenerarPlanilla.Style.Add("display", "normal");
            gpReporteRVIndividual.Style.Add("display", "none");
        }       
        protected void btnGuardarPlanilla_Click(object sender, EventArgs e)
        {
            try
            {
                string codTrabajador = cboTrabajadores.SelectedValue.ToString();
                string fechaInicio = txtFechaInicial.Text.Trim();
                string fechaFinal = txtFechaFinal.Text.Trim();
                string periodoMes = cboMesPeriodo.SelectedValue.ToString().Trim();
                string periodoAno = txtFechaAnoPeriodoSelect.Text.Trim();
                string diasLaborado = txtDiasLaborados.Text.Trim();
                string diasDominical = txtDiasDomingosFeriados.Text.Trim();
                string asignacionFamiliar = txtAsignacionFamiliar.Text.Trim();
                string horasExtraSimple = txtHorasExtrasSimple.Text.Trim();
                string pasajes = txtPasajesTrabajador.Text.Trim();
                string otrosIngresos = txtOtrosIngresos.Text.Trim();                
                string vacacional = txtVacacionalTrabajador.Text.Trim();
                string gratificacion = txtGratificacionTrabajador.Text.Trim();
                string ley29351 = txtAdicionalGratificacionTrabajador.Text.Trim();
                string bonificacionExtraSalud = txtBonifExtraEssaludTrabajador.Text.Trim();
                string otrosBeneficios = txtOtrosBeneficios.Text.Trim();
                string snp = txtSNPTrabajador.Text.Trim();
                string aporteObligatorio = txtAporteObligatorioTrabajador.Text.Trim();
                string comisionFlujo = txtComisionFlujoTrabajador.Text.Trim();
                string comisionMixta = txtComisionMixtaTrabajador.Text.Trim();
                string primaSeguro = txtPrimaSeguroTrabajador.Text.Trim();
                string essaludVida = txtEssaludVidaTrabajador.Text.Trim();                
                string renta5taCategoria = txtRenta5taCategoria.Text.Trim();
                string eps = txtEpsTrabajador.Text.Trim();
                string otrosDescuentos = txtOtrosDescuentosTrabajador.Text.Trim();
                string essalud = txtEsSaludTrabajador.Text.Trim();
                string sctrSalud = txtSCTRSaludTrabajador.Text.Trim();
                string sctrPension = txtSCTRPensionTrabajador.Text.Trim();
                string totalIngresos = Session["TotalIngresos"].ToString();
                string totalBeneficios = Session["TotalBeneficios"].ToString();
                string totalDescuentos = Session["TotalDescuentos"].ToString();
                string totalAporteEmpresa = Session["TotalAportacionesEmpleador"].ToString();
                string totalPagarTrabajador = Session["TotalPagar"].ToString();
                string totalCostoTrabajador = Session["CostoTotalTrabajador"].ToString();
                string codUsuarioRegistrador = Session["CodUsuarioInterno"].ToString();

                if (asignacionFamiliar == string.Empty) { asignacionFamiliar = "0"; }
                if (horasExtraSimple == string.Empty) { horasExtraSimple = "0"; }            
                if (pasajes == string.Empty) { pasajes = "0"; }
                if (otrosIngresos == string.Empty) { otrosIngresos = "0"; }                
                if (vacacional == string.Empty) { vacacional = "0"; }
                if (gratificacion == string.Empty) { gratificacion = "0"; }
                if (ley29351 == string.Empty) { ley29351 = "0"; }
                if (bonificacionExtraSalud == string.Empty) { bonificacionExtraSalud = "0"; }
                if (otrosBeneficios == string.Empty) { otrosBeneficios = "0"; }
                if (snp == string.Empty) { snp = "0"; }
                if (aporteObligatorio == string.Empty) { aporteObligatorio = "0"; }
                if (comisionFlujo == string.Empty) { comisionFlujo = "0"; }
                if (comisionMixta == string.Empty) { comisionMixta = "0"; }
                if (primaSeguro == string.Empty) { primaSeguro = "0"; }
                if (essaludVida == string.Empty) { essaludVida = "0"; }                
                if (renta5taCategoria == string.Empty) { renta5taCategoria = "0"; }
                if (eps == string.Empty) { eps = "0"; }
                if (otrosDescuentos == string.Empty) { otrosDescuentos = "0"; }
                if (essalud == string.Empty) { essalud = "0"; }
                if (sctrSalud == string.Empty) { sctrSalud = "0"; }
                if (sctrPension == string.Empty) { sctrPension = "0"; }

                string mensaje;
                if (Session["CodPlanillaModificar"].ToString() == string.Empty)
                {
                    mensaje = RegistrarPlanillaEmpleados(codTrabajador, fechaInicio, fechaFinal, periodoMes, periodoAno, diasLaborado, diasDominical, asignacionFamiliar,
                    horasExtraSimple, pasajes, otrosIngresos, vacacional, gratificacion, ley29351, bonificacionExtraSalud, otrosBeneficios, snp, aporteObligatorio, comisionFlujo,
                    comisionMixta, primaSeguro, renta5taCategoria, eps,essaludVida , otrosDescuentos, essalud, sctrSalud,sctrPension,
                    totalIngresos, totalBeneficios, totalDescuentos, totalAporteEmpresa, totalPagarTrabajador, totalCostoTrabajador, codUsuarioRegistrador);
                }
                else
                {
                    mensaje = ActualizarPlanillaEmpleados(codTrabajador, fechaInicio, fechaFinal, periodoMes, periodoAno, diasLaborado, diasDominical, asignacionFamiliar,
                    horasExtraSimple, pasajes, otrosIngresos, vacacional, gratificacion, ley29351, bonificacionExtraSalud, otrosBeneficios, snp, aporteObligatorio, comisionFlujo,
                    comisionMixta, primaSeguro, renta5taCategoria, eps, essaludVida, otrosDescuentos, essalud, sctrSalud, sctrPension,
                    totalIngresos, totalBeneficios, totalDescuentos, totalAporteEmpresa, totalPagarTrabajador, totalCostoTrabajador, codUsuarioRegistrador, Session["CodPlanillaModificar"].ToString());
                }

                if (mensaje == "EXITO")
                {
                    ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('Planilla registrada correctamente.','success');", true);

                    PlanillasRecientes(txtFechaInicialReporte.Text.Trim(), txtFechaFinalReporte.Text.Trim());

                    gpFormularioCalculoPlanilla.Visible = true;
                    gpBoletaPlanilla.Visible = false;

                    gpFormularioTrabajador.Visible = false;
                    gpFormularioParametros.Visible = false;
                    gpBtnGenerarBoleta.Visible = false;

                    gpPlanillaRecientes.Visible = true;

                    LimpiarFomulario();
                }
                else if (mensaje == "REPETIDO")
                {
                    ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('Ya hay una planilla registrada para este trabajador en el rango de fechas especificado.','info');", true);
                }
                else if (mensaje == "CESADO")
                {
                    ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('No puede registrar planilla a un trabajador cesado.','info');", true);
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
        protected void btnRegresarCalculoPlanilla_Click(object sender, EventArgs e)
        {
            gpFormularioCalculoPlanilla.Visible = true;
            gpBoletaPlanilla.Visible = false;
        }
        protected void dgvPlanillaRecientes_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                dgvPlanillaRecientes.PageIndex = e.NewPageIndex;
                PlanillasRecientes(txtFechaInicialReporte.Text.Trim(), txtFechaFinalReporte.Text.Trim());
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }
        protected void dgvPlanillaRecientes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "ModificarPlanilla")
                {
                    string codPlanilla = e.CommandArgument.ToString();
                    CargarDatosPlanilla(codPlanilla);
                }
                else if (e.CommandName == "ImprimirPlanilla")
                {
                    string codPlanilla = e.CommandArgument.ToString();
                    gpFormularioGenerarPlanilla.Style.Add("display", "none");
                    gpReporteRVIndividual.Style.Add("display", "normal");
                    GenerarReporteBoletaPagoInvidual(codPlanilla);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }
        protected void btnBuscarReporteReciente_Click(object sender, EventArgs e)
        {
            try
            {
                int contadoVacio = 0;
                string fechaInicial = txtFechaInicialReporte.Text.Trim();
                string fechaFinal = txtFechaFinalReporte.Text.Trim();

                if (fechaInicial == string.Empty) { contadoVacio += 1; txtFechaInicial.BorderWidth = 1; txtFechaInicial.BorderStyle = BorderStyle.Solid; txtFechaInicial.BorderColor = Color.Red; } else { txtFechaInicial.BorderColor = ColorTranslator.FromHtml("#ebedf2"); }
                if (fechaFinal == string.Empty) { contadoVacio += 1; txtFechaFinal.BorderWidth = 1; txtFechaFinal.BorderStyle = BorderStyle.Solid; txtFechaFinal.BorderColor = Color.Red; } else { txtFechaFinal.BorderColor = ColorTranslator.FromHtml("#ebedf2"); }

                if (contadoVacio == 0)
                {
                    PlanillasRecientes(fechaInicial, fechaFinal);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('Debe completar los campos marcados con rojo','info');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }
        protected void btnActivarChckBox_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkBonificacionExtra.Checked)
                {
                    txtBonifExtraEssaludTrabajador.Enabled = true;                    
                    ObtenerTotalIngresos();
                    ObtenerBeneficiosExtras(cboTrabajadores.SelectedValue.ToString(), txtDiasLaborados.Text.Trim(), "3");
                    ObtenerTotalBeneficios();
                    ObtenerTotalDescuentos();
                    ObtenerTotalPagar();
                    ObtenerAportacionEmpleador(cboTrabajadores.SelectedValue.ToString(), txtDiasLaborados.Text.Trim(), Session["TotalIngresos"].ToString(), "3");
                    ObtenerTotalaAportacionesEmpleador();
                    ObtenerCostoTotalTrabajador();
                }
                else
                {
                    txtBonifExtraEssaludTrabajador.Enabled = false;                    

                    txtCalculoBonifExtraEssalud.Text = "0.00";
                    txtBonifExtraEssaludTrabajador.Text = string.Empty;      
                    ObtenerTotalIngresos();
                    ObtenerTotalBeneficios();
                    ObtenerTotalDescuentos();
                    ObtenerTotalPagar();
                    ObtenerAportacionEmpleador(cboTrabajadores.SelectedValue.ToString(), txtDiasLaborados.Text.Trim(), Session["TotalIngresos"].ToString(), "3");
                    ObtenerTotalaAportacionesEmpleador();
                    ObtenerCostoTotalTrabajador();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }
        protected void btnActivarVacaciones_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkVacaciones.Checked)
                {
                    txtVacacionalTrabajador.Enabled = true;
                    ObtenerTotalIngresos();
                    ObtenerBeneficiosVacaciones(cboTrabajadores.SelectedValue.ToString(), txtDiasLaborados.Text.Trim(), txtDiasDomingosFeriados.Text.Trim(), cboMesPeriodo.SelectedValue.ToString().Trim(), "3");
                    ObtenerTotalBeneficios();
                    ObtenerTotalDescuentos();
                    ObtenerTotalPagar();
                    ObtenerAportacionEmpleador(cboTrabajadores.SelectedValue.ToString(), txtDiasLaborados.Text.Trim(), Session["TotalIngresos"].ToString(), "3");
                    ObtenerTotalaAportacionesEmpleador();
                    ObtenerCostoTotalTrabajador();
                }
                else
                {
                    txtVacacionalTrabajador.Enabled = false;

                    txtCalculoVacacional.Text = "0.00";
                    txtVacacionalTrabajador.Text = string.Empty;
                    ObtenerTotalIngresos();
                    ObtenerTotalBeneficios();
                    ObtenerTotalDescuentos();
                    ObtenerTotalPagar();
                    ObtenerAportacionEmpleador(cboTrabajadores.SelectedValue.ToString(), txtDiasLaborados.Text.Trim(), Session["TotalIngresos"].ToString(), "3");
                    ObtenerTotalaAportacionesEmpleador();
                    ObtenerCostoTotalTrabajador();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }
        protected void btnActivarGratificacion_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkGratificacion.Checked)
                {
                    txtGratificacionTrabajador.Enabled = true;
                    txtAdicionalGratificacionTrabajador.Enabled = true;

                    ObtenerTotalIngresos();
                    ObtenerBeneficiosGratificacion(cboTrabajadores.SelectedValue.ToString(), txtDiasLaborados.Text.Trim(), cboMesPeriodo.SelectedValue.ToString().Trim(), "3");
                    ObtenerTotalBeneficios();
                    ObtenerTotalDescuentos();
                    ObtenerTotalPagar();
                    ObtenerAportacionEmpleador(cboTrabajadores.SelectedValue.ToString(), txtDiasLaborados.Text.Trim(), Session["TotalIngresos"].ToString(), "3");
                    ObtenerTotalaAportacionesEmpleador();
                    ObtenerCostoTotalTrabajador();
                }
                else
                {
                    txtGratificacionTrabajador.Enabled = false;
                    txtAdicionalGratificacionTrabajador.Enabled = false;

                    txtCalculoGratificacion.Text = "0.00";
                    txtGratificacionTrabajador.Text = string.Empty;

                    txtCalculoAdicionalGraticacion.Text = "0.00";
                    txtAdicionalGratificacionTrabajador.Text = string.Empty;

                    ObtenerTotalIngresos();
                    ObtenerTotalBeneficios();
                    ObtenerTotalDescuentos();
                    ObtenerTotalPagar();
                    ObtenerAportacionEmpleador(cboTrabajadores.SelectedValue.ToString(), txtDiasLaborados.Text.Trim(), Session["TotalIngresos"].ToString(), "3");
                    ObtenerTotalaAportacionesEmpleador();
                    ObtenerCostoTotalTrabajador();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }
        protected void cboMesPeriodo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cboTrabajadores.SelectedValue != "Seleccione")
                {
                    ObtenerTotalIngresos();

                    if (chkVacaciones.Checked)
                    {
                        ObtenerBeneficiosVacaciones(cboTrabajadores.SelectedValue.ToString(), txtDiasLaborados.Text.Trim(), txtDiasDomingosFeriados.Text.Trim(), cboMesPeriodo.SelectedValue.ToString().Trim(), "3");
                        ObtenerTotalBeneficios();
                    }

                    if (chkGratificacion.Checked)
                    {
                        ObtenerBeneficiosGratificacion(cboTrabajadores.SelectedValue.ToString(), txtDiasLaborados.Text.Trim(), cboMesPeriodo.SelectedValue.ToString().Trim(), "3");
                        ObtenerTotalBeneficios();
                    }

                    ObtenerTotalBeneficios();

                    ObtenerTotalDescuentos();

                    ObtenerTotalPagar();

                    ObtenerAportacionEmpleador(cboTrabajadores.SelectedValue.ToString(), txtDiasLaborados.Text.Trim(), Session["TotalIngresos"].ToString(), "3");

                    ObtenerTotalaAportacionesEmpleador();

                    ObtenerCostoTotalTrabajador();
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
        private string RegistrarPlanillaEmpleados(string codTrabajador, string fechaInicio, string fechaFin, string mesPeriodo, string anoPeriodo, string diasLaborados, string diasDominical, string asignacionFamiliar, string horasExtraSiemple, string pasajes, string otrosIngresos,
          string vacacional, string gratificacion, string ley29351, string bonificacionExtraSalud, string otrosBeneficios, string snp, string aporteObligatorio, string comisionFlujo, string comisionMixta, string primaSeguro, string renta5taCategoria,
          string eps, string essaludVida, string otrosDsctos, string essalud, string sctrSalud, string sctrPension, string totalIngresos, string totalBeneficios, string totalDescuentos, string totalAporteEmpresa, string totalPagarTrabajador, string totalCostoTrabajador, string codUsuarioRegistro)
        {
            PlanillaEntidad planillaEntidad = new PlanillaEntidad();
            planillaEntidad.CodTrabajador = Convert.ToInt32(codTrabajador);
            planillaEntidad.FechaInicial = Convert.ToDateTime(fechaInicio);
            planillaEntidad.FechaFinal = Convert.ToDateTime(fechaFin);
            planillaEntidad.MesPeriodo = mesPeriodo;
            planillaEntidad.AnoPeriodo = anoPeriodo;
            planillaEntidad.DiasLaborados = double.Parse(diasLaborados, CultureInfo.InvariantCulture);
            planillaEntidad.DiasDominical = double.Parse(diasDominical, CultureInfo.InvariantCulture);
            planillaEntidad.AsignacionFamiliar = double.Parse(asignacionFamiliar, CultureInfo.InvariantCulture);         
            planillaEntidad.HorasExtraSimple = double.Parse(horasExtraSiemple, CultureInfo.InvariantCulture);                    
            planillaEntidad.Pasajes = double.Parse(pasajes, CultureInfo.InvariantCulture);
            planillaEntidad.OtrosIngresos = double.Parse(otrosIngresos, CultureInfo.InvariantCulture);            
            planillaEntidad.Vacacional = double.Parse(vacacional, CultureInfo.InvariantCulture);
            planillaEntidad.Gratificacion = double.Parse(gratificacion, CultureInfo.InvariantCulture);
            planillaEntidad.Ley29351 = double.Parse(ley29351, CultureInfo.InvariantCulture);
            planillaEntidad.OtrosBeneficios = double.Parse(otrosBeneficios, CultureInfo.InvariantCulture);      
            planillaEntidad.Snp = double.Parse(snp, CultureInfo.InvariantCulture);
            planillaEntidad.AporteObligatorio = double.Parse(aporteObligatorio, CultureInfo.InvariantCulture);
            planillaEntidad.ComisionFlujo = double.Parse(comisionFlujo, CultureInfo.InvariantCulture);
            planillaEntidad.ComisionMixta = double.Parse(comisionMixta, CultureInfo.InvariantCulture);
            planillaEntidad.PrimaSeguro = double.Parse(primaSeguro, CultureInfo.InvariantCulture);                 
            planillaEntidad.Renta5taCategoria = double.Parse(renta5taCategoria, CultureInfo.InvariantCulture);
            planillaEntidad.Eps = double.Parse(eps, CultureInfo.InvariantCulture);
            planillaEntidad.EsSaludVida = double.Parse(essaludVida, CultureInfo.InvariantCulture);
            planillaEntidad.OtrosDescuentos = double.Parse(otrosDsctos, CultureInfo.InvariantCulture);
            planillaEntidad.EsSalud = double.Parse(essalud, CultureInfo.InvariantCulture);
            planillaEntidad.SctrSalud = double.Parse(sctrSalud, CultureInfo.InvariantCulture);
            planillaEntidad.SctrPension = double.Parse(sctrPension, CultureInfo.InvariantCulture);
            planillaEntidad.TotalIngresos = double.Parse(totalIngresos.Replace(",", "."), CultureInfo.InvariantCulture);
            planillaEntidad.TotalBeneficios = double.Parse(totalBeneficios.Replace(",", "."), CultureInfo.InvariantCulture);
            planillaEntidad.TotalDescuentos = double.Parse(totalDescuentos.Replace(",", "."), CultureInfo.InvariantCulture);
            planillaEntidad.TotalAporteEmpresa = double.Parse(totalAporteEmpresa.Replace(",", "."), CultureInfo.InvariantCulture);
            planillaEntidad.TotalPagarTrabajador = double.Parse(totalPagarTrabajador.Replace(",", "."), CultureInfo.InvariantCulture);
            planillaEntidad.TotalCostoTrabajador = double.Parse(totalCostoTrabajador.Replace(",", "."), CultureInfo.InvariantCulture);
            planillaEntidad.CodUsuarioRegistro = Convert.ToInt32(codUsuarioRegistro);

            return procesoPlanilla.RegistrarPlanillaEmpleados(planillaEntidad);
        }
        private string ActualizarPlanillaEmpleados(string codTrabajador, string fechaInicio, string fechaFin, string mesPeriodo, string anoPeriodo, string diasLaborados, string diasDominical, string asignacionFamiliar, string horasExtraSiemple, string pasajes, string otrosIngresos,
           string vacacional, string gratificacion, string ley29351, string bonificacionExtraSalud, string otrosBeneficios, string snp, string aporteObligatorio, string comisionFlujo, string comisionMixta, string primaSeguro, string renta5taCategoria,
           string eps, string essaludVida, string otrosDsctos, string essalud, string sctrSalud, string sctrPension, string totalIngresos, string totalBeneficios, string totalDescuentos, string totalAporteEmpresa, string totalPagarTrabajador, string totalCostoTrabajador, string codUsuarioModificacion, string codPlanillaEmpleados)
        {
            PlanillaEntidad planillaEntidad = new PlanillaEntidad();
            planillaEntidad.CodTrabajador = Convert.ToInt32(codTrabajador);
            planillaEntidad.FechaInicial = Convert.ToDateTime(fechaInicio);
            planillaEntidad.FechaFinal = Convert.ToDateTime(fechaFin);
            planillaEntidad.MesPeriodo = mesPeriodo;
            planillaEntidad.AnoPeriodo = anoPeriodo;
            planillaEntidad.DiasLaborados = double.Parse(diasLaborados, CultureInfo.InvariantCulture);
            planillaEntidad.DiasDominical = double.Parse(diasDominical, CultureInfo.InvariantCulture);
            planillaEntidad.AsignacionFamiliar = double.Parse(asignacionFamiliar, CultureInfo.InvariantCulture);
            planillaEntidad.HorasExtraSimple = double.Parse(horasExtraSiemple, CultureInfo.InvariantCulture);
            planillaEntidad.Pasajes = double.Parse(pasajes, CultureInfo.InvariantCulture);
            planillaEntidad.OtrosIngresos = double.Parse(otrosIngresos, CultureInfo.InvariantCulture);            
            planillaEntidad.Vacacional = double.Parse(vacacional, CultureInfo.InvariantCulture);
            planillaEntidad.Gratificacion = double.Parse(gratificacion, CultureInfo.InvariantCulture);
            planillaEntidad.Ley29351 = double.Parse(ley29351, CultureInfo.InvariantCulture);
            planillaEntidad.BonifacionExtraSalud = double.Parse(bonificacionExtraSalud, CultureInfo.InvariantCulture);
            planillaEntidad.OtrosBeneficios = double.Parse(otrosBeneficios, CultureInfo.InvariantCulture);          
            planillaEntidad.Snp = double.Parse(snp, CultureInfo.InvariantCulture);
            planillaEntidad.AporteObligatorio = double.Parse(aporteObligatorio, CultureInfo.InvariantCulture);
            planillaEntidad.ComisionFlujo = double.Parse(comisionFlujo, CultureInfo.InvariantCulture);
            planillaEntidad.ComisionMixta = double.Parse(comisionMixta, CultureInfo.InvariantCulture);
            planillaEntidad.PrimaSeguro = double.Parse(primaSeguro, CultureInfo.InvariantCulture);                  
            planillaEntidad.Renta5taCategoria = double.Parse(renta5taCategoria, CultureInfo.InvariantCulture);
            planillaEntidad.Eps = double.Parse(eps, CultureInfo.InvariantCulture);
            planillaEntidad.EsSaludVida = double.Parse(essaludVida, CultureInfo.InvariantCulture);
            planillaEntidad.OtrosDescuentos = double.Parse(otrosDsctos, CultureInfo.InvariantCulture);
            planillaEntidad.EsSalud = double.Parse(essalud, CultureInfo.InvariantCulture);
            planillaEntidad.SctrSalud = double.Parse(sctrSalud, CultureInfo.InvariantCulture);
            planillaEntidad.SctrPension = double.Parse(sctrPension, CultureInfo.InvariantCulture);
            planillaEntidad.TotalIngresos = double.Parse(totalIngresos.Replace(",", "."), CultureInfo.InvariantCulture);
            planillaEntidad.TotalBeneficios = double.Parse(totalBeneficios.Replace(",", "."), CultureInfo.InvariantCulture);
            planillaEntidad.TotalDescuentos = double.Parse(totalDescuentos.Replace(",", "."), CultureInfo.InvariantCulture);
            planillaEntidad.TotalAporteEmpresa = double.Parse(totalAporteEmpresa.Replace(",", "."), CultureInfo.InvariantCulture);
            planillaEntidad.TotalPagarTrabajador = double.Parse(totalPagarTrabajador.Replace(",", "."), CultureInfo.InvariantCulture);
            planillaEntidad.TotalCostoTrabajador = double.Parse(totalCostoTrabajador.Replace(",", "."), CultureInfo.InvariantCulture);
            planillaEntidad.CodUsuarioModificacion = Convert.ToInt32(codUsuarioModificacion);
            planillaEntidad.CodPlanillaEmpleados = Convert.ToInt32(codPlanillaEmpleados);

            return procesoPlanilla.ActualizarPlanillaEmpleados(planillaEntidad);
        }
        private void CargarDatosPlanilla(string codPlanilla)
        {
            DataTable dt = procesoPlanilla.ObtenerDatosPlanillaEmpleados(Convert.ToInt32(codPlanilla));

            if (dt.Rows.Count > 0)
            {
                LimpiarFomulario();

                gpFormularioCalculoPlanilla.Visible = true;
                gpBoletaPlanilla.Visible = false;

                gpFormularioTrabajador.Visible = true;
                gpFormularioParametros.Visible = true;
                gpBtnGenerarBoleta.Visible = true;

                gpPlanillaRecientes.Visible = false;

                txtFechaAnoPeriodo.Text = dt.Rows[0][0].ToString();
                txtFechaAnoPeriodoSelect.Text = dt.Rows[0][0].ToString();
                cboMesPeriodo.SelectedValue = dt.Rows[0][1].ToString().Trim();
                txtFechaInicial.Text = dt.Rows[0][2].ToString();
                txtFechaFinal.Text = dt.Rows[0][3].ToString();

                cboTrabajadores.SelectedValue = dt.Rows[0][4].ToString().Trim();
                cboTrabajadores_SelectedIndexChanged(cboTrabajadores, new EventArgs());

                txtDiasLaborados.Text = Convert.ToInt32(dt.Rows[0][5]).ToString();
                txtDiasDomingosFeriados.Text = Convert.ToInt32(dt.Rows[0][6]).ToString();
                txtAsignacionFamiliar.Text = dt.Rows[0][7].ToString();
                txtPasajesTrabajador.Text = dt.Rows[0][8].ToString();
                txtOtrosIngresos.Text = dt.Rows[0][9].ToString();                
                txtRenta5taCategoria.Text = dt.Rows[0][12].ToString();
                txtEpsTrabajador.Text = dt.Rows[0][13].ToString();
                txtOtrosDescuentosTrabajador.Text = dt.Rows[0][14].ToString();
                txtOtrosBeneficios.Text = dt.Rows[0][17].ToString();

                txtDiasLaborados_TextChanged(txtDiasLaborados.Text, new EventArgs());
                txtDiasDomingosFeriados_TextChanged(txtDiasDomingosFeriados.Text, new EventArgs());

                if (dt.Rows[0][15].ToString() != "0.00")
                {
                    chkGratificacion.Checked = true;                    
                    btnActivarGratificacion_Click(btnActivarGratificacion, new EventArgs());
                }
                else
                {
                    chkGratificacion.Checked = false;
                }

                if (dt.Rows[0][16].ToString() != "0.00")
                {
                    chkVacaciones.Checked = true;                    
                    btnActivarVacaciones_Click(btnActivarVacaciones, new EventArgs());
                }
                else
                {
                    chkVacaciones.Checked = false;
                }

                if (dt.Rows[0][10].ToString() != "0.00")
                {
                    chkComisionFlujo.Checked = true;
                    btnActivarComision_Click(btnActivarComision, new EventArgs());
                }
                else
                {
                    chkComisionFlujo.Checked = false;
                }

                if (dt.Rows[0][11].ToString() != "0.00")
                {
                    chkComisionMixta.Checked = true;
                    btnActivarComision_Click(btnActivarComision, new EventArgs());
                }
                else
                {
                    chkComisionMixta.Checked = false;
                }                

                if (dt.Rows[0][19].ToString() != "0.00")
                {
                    chkBonificacionExtra.Checked = true;
                    btnActivarChckBox_Click(btnActivarChckBox, new EventArgs());
                }
                else
                {
                    chkBonificacionExtra.Checked = false;
                }
              
                Session["CodPlanillaModificar"] = codPlanilla;
            }
        }

        private void PlanillasRecientes(string fechaInicial, string fechaFinal)
        {
            DataTable dt = procesoPlanilla.ObtenerPlanillasEmpleadosReciente(fechaInicial, fechaFinal);
            dgvPlanillaRecientes.DataSource = dt;
            dgvPlanillaRecientes.DataBind();

            lblResultadoBusqueda.InnerHtml = "" + dt.Rows.Count.ToString() + " registros(s) reciente(s) encontrado(s)";
        }
        private void CargarDatosTrabajadorPlanilla(string codTrabajador, string codTipoPlanilla)
        {
            DataTable dt = procesoPlanilla.CargarDatosTrabajadorPlanilla(Convert.ToInt32(codTrabajador), Convert.ToInt32(codTipoPlanilla));

            txtCargoTrabajador.Text = dt.Rows[0][0].ToString();
            txtHaberMensualTrabajador.Text = "S/. " + Convert.ToDouble(dt.Rows[0][1]).ToString("N", CultureInfo.GetCultureInfo("es-PE"));
            txtJornalTrabajador.Text = "S/. " + Convert.ToDouble(dt.Rows[0][2]).ToString("N", CultureInfo.GetCultureInfo("es-PE"));
            txtCalculoHorasSimple.Text = Convert.ToDouble(dt.Rows[0][3]).ToString("N", CultureInfo.GetCultureInfo("es-PE"));

            Session["CodTipoAportacion"] = dt.Rows[0]["CodTipoAportacion"].ToString();
        }
        private void ObtenerDomingoFeriados(string codTrabajador, string cantDiasLaborados, string codTipoPlanilla)
        {
            DataTable dt = procesoPlanilla.ObtenerParametrosIngresoPlanilla(Convert.ToInt32(codTrabajador), Convert.ToInt32(cantDiasLaborados), Convert.ToInt32(codTipoPlanilla));
            txtCalculoDiasDomingosFeriados.Text = Convert.ToDouble(dt.Rows[0][0]).ToString("N", CultureInfo.GetCultureInfo("es-PE"));
        }
        private void ObtenerIngresos(string codTrabajador, string cantDiasLaborados, string codTipoPlanilla)
        {
            DataTable dt = procesoPlanilla.ObtenerParametrosIngresoPlanilla(Convert.ToInt32(codTrabajador), Convert.ToInt32(cantDiasLaborados), Convert.ToInt32(codTipoPlanilla));
            txtTotalCalculadoDiasLaborados.Text = Convert.ToDouble(dt.Rows[0][0]).ToString("N", CultureInfo.GetCultureInfo("es-PE"));
        }
        private void ObtenerBeneficiosVacaciones(string codTrabajador, string cantDiasLaborados, string cantiDiasDominical, string periodo, string codTipoPlanilla)
        {
            int diaslaborados = Convert.ToInt32(cantDiasLaborados);
            int diasdomimical = Convert.ToInt32(cantiDiasDominical);
            int totalCantidadDias = diaslaborados + diasdomimical;

            DataTable dt = procesoPlanilla.ObtenerParametrosBeneficiosEspecialePlanilla(Convert.ToInt32(codTrabajador), totalCantidadDias, Convert.ToInt32(periodo), Convert.ToInt32(codTipoPlanilla));

            txtCalculoVacacional.Text = Convert.ToDouble(dt.Rows[0][1]).ToString("N", CultureInfo.GetCultureInfo("es-PE"));
            txtVacacionalTrabajador.Text = Convert.ToDouble(dt.Rows[0][2]).ToString("N", CultureInfo.GetCultureInfo("es-PE"));         
        }
        private void ObtenerBeneficiosGratificacion(string codTrabajador, string cantDiasLaborados, string periodo, string codTipoPlanilla)
        {
            DataTable dt = procesoPlanilla.ObtenerParametrosBeneficiosPlanilla(Convert.ToInt32(codTrabajador), Convert.ToInt32(cantDiasLaborados), Convert.ToInt32(periodo), Convert.ToInt32(codTipoPlanilla));

            txtCalculoGratificacion.Text = Convert.ToDouble(dt.Rows[0][5]).ToString("N", CultureInfo.GetCultureInfo("es-PE"));
            txtGratificacionTrabajador.Text = Convert.ToDouble(dt.Rows[0][6]).ToString("N", CultureInfo.GetCultureInfo("es-PE"));
            txtCalculoAdicionalGraticacion.Text = Convert.ToDouble(dt.Rows[0][7]).ToString("N", CultureInfo.GetCultureInfo("es-PE"));
            txtAdicionalGratificacionTrabajador.Text = Convert.ToDouble(dt.Rows[0][8]).ToString("N", CultureInfo.GetCultureInfo("es-PE"));
        }
        private void ObtenerBeneficiosExtras(string codTrabajador, string cantDiasLaborados, string codTipoPlanilla)
        {
            DataTable dt = procesoPlanilla.ObtenerParametrosBeneficiosExtrasPlanilla(Convert.ToInt32(codTrabajador), Convert.ToInt32(cantDiasLaborados), Convert.ToInt32(codTipoPlanilla));

            txtCalculoBonifExtraEssalud.Text = Convert.ToDouble(dt.Rows[0][1]).ToString("N", CultureInfo.GetCultureInfo("es-PE"));
            txtBonifExtraEssaludTrabajador.Text = (Convert.ToDouble(Session["TotalIngresos"].ToString()) * ((Convert.ToDouble(dt.Rows[0][1])) / 100)).ToString("N", CultureInfo.GetCultureInfo("es-PE"));            
        }
        private void ObtenerDescuentos(string codTrabajador, string cantDiasLaborados, string cantDiasDominical,string totalVacacionesTruncas, string totalIngresos, string codTipoPlanilla)
        {
            double totalIngreso = Convert.ToDouble(totalIngresos);
            double totalVacacionesTrunca = Convert.ToDouble(totalVacacionesTruncas);
            double totalAfecto = totalIngreso + totalVacacionesTrunca;

            DataTable dt = procesoPlanilla.ObtenerParametrosDescuentosPlanilla(Convert.ToInt32(codTrabajador), Convert.ToInt32(cantDiasLaborados), Convert.ToInt32(cantDiasDominical), totalAfecto, Convert.ToInt32(codTipoPlanilla));

            txtCalculoSNP.Text = Convert.ToDouble(dt.Rows[0][1]).ToString("N", CultureInfo.GetCultureInfo("es-PE"));
            txtSNPTrabajador.Text = Convert.ToDouble(dt.Rows[0][2]).ToString("N", CultureInfo.GetCultureInfo("es-PE"));
            txtCalculoAporteObligatorio.Text = Convert.ToDouble(dt.Rows[0][3]).ToString("N", CultureInfo.GetCultureInfo("es-PE"));
            txtAporteObligatorioTrabajador.Text = Convert.ToDouble(dt.Rows[0][4]).ToString("N", CultureInfo.GetCultureInfo("es-PE"));
            txtCalculoPrimaSeguroTrabajador.Text = Convert.ToDouble(dt.Rows[0][7]).ToString("N", CultureInfo.GetCultureInfo("es-PE"));
            txtPrimaSeguroTrabajador.Text = Convert.ToDouble(dt.Rows[0][8]).ToString("N", CultureInfo.GetCultureInfo("es-PE"));              
        }
        private void ObtenerAportacionEmpleador(string codTrabajador, string cantDiasLaborados, string totalPagar, string codTipoPlanilla)
        {
            DataTable dt = procesoPlanilla.ObtenerParametrosAportacionesEmpleadorPlanilla(Convert.ToInt32(codTrabajador), Convert.ToInt32(cantDiasLaborados), Convert.ToDouble(totalPagar), Convert.ToInt32(codTipoPlanilla));

            txtCalculoEsSaludTrabajador.Text = Convert.ToDouble(dt.Rows[0][1]).ToString("N", CultureInfo.GetCultureInfo("es-PE"));
            txtEsSaludTrabajador.Text = Convert.ToDouble(dt.Rows[0][2]).ToString("N", CultureInfo.GetCultureInfo("es-PE"));
            txtCalculoSCTRSaludTrabajador.Text = Convert.ToDouble(dt.Rows[0][5]).ToString("N", CultureInfo.GetCultureInfo("es-PE"));
            txtSCTRSaludTrabajador.Text = Convert.ToDouble(dt.Rows[0][6]).ToString("N", CultureInfo.GetCultureInfo("es-PE"));
            txtCalculoSCTRPensionTrabajador.Text = Convert.ToDouble(dt.Rows[0][7]).ToString("N", CultureInfo.GetCultureInfo("es-PE"));
            txtSCTRPensionTrabajador.Text = Convert.ToDouble(dt.Rows[0][8]).ToString("N", CultureInfo.GetCultureInfo("es-PE"));
            txtCalculoEssaludVidaTrabajador.Text = Convert.ToDouble(dt.Rows[0][9]).ToString("N", CultureInfo.GetCultureInfo("es-PE"));
            txtEssaludVidaTrabajador.Text = Convert.ToDouble(dt.Rows[0][10]).ToString("N", CultureInfo.GetCultureInfo("es-PE"));
        }
        private void ObtenerTotalIngresos()
        {
            string calculoDiasLaborado = txtTotalCalculadoDiasLaborados.Text.Trim();
            string calculoDominical = txtCalculoDiasDomingosFeriados.Text.Trim();
            string asignacionFamiliar = txtAsignacionFamiliar.Text.Trim();      
            string pasajes = txtPasajesTrabajador.Text.Trim();
            string horasExSimples = txtHorasExtrasSimple.Text.Trim();
            string otroIngresos = txtOtrosIngresos.Text.Trim();

            if (calculoDiasLaborado == string.Empty) { calculoDiasLaborado = "0"; }
            if (calculoDominical == string.Empty) { calculoDominical = "0"; }
            if (asignacionFamiliar == string.Empty) { asignacionFamiliar = "0"; }         
            if (pasajes == string.Empty) { pasajes = "0"; }
            if (horasExSimples == string.Empty) { horasExSimples = "0"; }
            if (otroIngresos == string.Empty) { otroIngresos = "0"; }

            double totalLaborado = double.Parse(calculoDiasLaborado, CultureInfo.InvariantCulture);
            double totalDominical = double.Parse(calculoDominical, CultureInfo.InvariantCulture);
            double asignacionFamiliarDoub = double.Parse(asignacionFamiliar, CultureInfo.InvariantCulture);          
            double pasajesDoub = double.Parse(pasajes, CultureInfo.InvariantCulture);
            double otroIngresosDoub = double.Parse(otroIngresos, CultureInfo.InvariantCulture);
            double horasExSimplesDoub = double.Parse(horasExSimples, CultureInfo.InvariantCulture) * double.Parse(txtCalculoHorasSimple.Text.Trim(), CultureInfo.InvariantCulture);
            
            Session["TotalIngresos"] = (totalLaborado + totalDominical + asignacionFamiliarDoub + pasajesDoub + horasExSimplesDoub + otroIngresosDoub).ToString();
            txtTotalIngresos.InnerText = "S/. " + (totalLaborado + totalDominical + asignacionFamiliarDoub + pasajesDoub + horasExSimplesDoub + otroIngresosDoub).ToString("N", CultureInfo.GetCultureInfo("es-PE"));
        }
        private void ObtenerTotalBeneficios()
        {                               
            string vacacional = txtVacacionalTrabajador.Text.Trim();
            string gratificacion = txtGratificacionTrabajador.Text.Trim();
            string adicionalGratificacion = txtAdicionalGratificacionTrabajador.Text.Trim();
            string bonificacionEssalud = txtBonifExtraEssaludTrabajador.Text.Trim();
            string otrosBeneficios = txtOtrosBeneficios.Text.Trim();
            
            if (gratificacion == string.Empty) { gratificacion = "0"; }
            if (adicionalGratificacion == string.Empty) { adicionalGratificacion = "0"; }
            if (otrosBeneficios == string.Empty) { otrosBeneficios = "0"; }
            if (vacacional == string.Empty) { vacacional = "0"; }                  
            if (bonificacionEssalud == string.Empty) { bonificacionEssalud = "0"; }            

            Session["TotalVacacionesTruncas"] = double.Parse(vacacional, CultureInfo.InvariantCulture);
            
            double gratificacionDoub = double.Parse(gratificacion, CultureInfo.InvariantCulture);
            double adicionalGratificacionDoub = double.Parse(adicionalGratificacion, CultureInfo.InvariantCulture);
            double otrosBeneficiosDoub = double.Parse(otrosBeneficios, CultureInfo.InvariantCulture);
            double vacacionalDoub = double.Parse(vacacional, CultureInfo.InvariantCulture);                        
            double bonificacionEssaludDoub = double.Parse(bonificacionEssalud, CultureInfo.InvariantCulture);

            Session["TotalBeneficios"] = (gratificacionDoub + adicionalGratificacionDoub + otrosBeneficiosDoub+ vacacionalDoub + bonificacionEssaludDoub).ToString();
            txtTotalBeneficios.InnerText = "S/. " + (gratificacionDoub + adicionalGratificacionDoub + otrosBeneficiosDoub + vacacionalDoub + bonificacionEssaludDoub).ToString("N", CultureInfo.GetCultureInfo("es-PE"));
        }
        private void ObtenerTotalDescuentos()
        {
            string aportacionSNP = txtSNPTrabajador.Text.Trim();
            string aportacionAFPAO = txtAporteObligatorioTrabajador.Text.Trim();
            string aportacionAFPCF = txtComisionFlujoTrabajador.Text.Trim();
            string aportacionAFPPS = txtPrimaSeguroTrabajador.Text.Trim();            
            string aportacionAFPCM = txtComisionMixtaTrabajador.Text.Trim();
            string renta5taCategoria = txtRenta5taCategoria.Text.Trim();
            string eps = txtEpsTrabajador.Text.Trim();           
            string otroDescuentos = txtOtrosDescuentosTrabajador.Text.Trim();

            if (aportacionSNP == string.Empty) { aportacionSNP = "0"; }
            if (aportacionAFPAO == string.Empty) { aportacionAFPAO = "0"; }
            if (aportacionAFPCF == string.Empty) { aportacionAFPCF = "0"; }
            if (aportacionAFPPS == string.Empty) { aportacionAFPPS = "0"; }            
            if (aportacionAFPCM == string.Empty) { aportacionAFPCM = "0"; }
            if (renta5taCategoria == string.Empty) { renta5taCategoria = "0"; }
            if (eps == string.Empty) { eps = "0"; }
            
            if (otroDescuentos == string.Empty) { otroDescuentos = "0"; }

            double aportacionSNPDoub = double.Parse(aportacionSNP, CultureInfo.InvariantCulture);
            double aportacionAFPAODoub = double.Parse(aportacionAFPAO, CultureInfo.InvariantCulture);
            double aportacionAFPCFDoub = double.Parse(aportacionAFPCF, CultureInfo.InvariantCulture);
            double aportacionAFPPSDoub = double.Parse(aportacionAFPPS, CultureInfo.InvariantCulture);            
            double aportacionAFPCMDoub = double.Parse(aportacionAFPCM, CultureInfo.InvariantCulture);
            double renta5taCategoriaDoub = double.Parse(renta5taCategoria, CultureInfo.InvariantCulture);
            double epsDoub = double.Parse(eps, CultureInfo.InvariantCulture);            
            double otroDescuentosDoub = double.Parse(otroDescuentos, CultureInfo.InvariantCulture);

            Session["TotalDescuentos"] = (aportacionSNPDoub + aportacionAFPAODoub + aportacionAFPCFDoub + aportacionAFPPSDoub + aportacionAFPCMDoub + renta5taCategoriaDoub + epsDoub + otroDescuentosDoub).ToString();
            txtTotalDescuentos.InnerText = "S/. " + (aportacionSNPDoub + aportacionAFPAODoub + aportacionAFPCFDoub + aportacionAFPPSDoub + aportacionAFPCMDoub + renta5taCategoriaDoub + epsDoub + otroDescuentosDoub).ToString("N", CultureInfo.GetCultureInfo("es-PE"));
        }
        private void ObtenerTotalaAportacionesEmpleador()
        {
            string essalud = txtEsSaludTrabajador.Text.Trim();
            string sctrSalud = txtSCTRSaludTrabajador.Text.Trim();
            string sctrPension = txtSCTRPensionTrabajador.Text.Trim();
            string essaludVida = txtEssaludVidaTrabajador.Text.Trim();

            if (essalud == string.Empty) { essalud = "0"; }
            if (sctrSalud == string.Empty) { sctrSalud = "0"; }
            if (sctrPension == string.Empty) { sctrPension = "0"; }
            if (essaludVida == string.Empty) { essaludVida = "0"; }

            double essaludDoub = double.Parse(essalud, CultureInfo.InvariantCulture);
            double sctrSaludDoub = double.Parse(sctrSalud, CultureInfo.InvariantCulture);
            double sctrPensionDoub = double.Parse(sctrPension, CultureInfo.InvariantCulture);
            double essaludVidaDoub = double.Parse(essaludVida, CultureInfo.InvariantCulture);

            Session["TotalAportacionesEmpleador"] = (essaludDoub + sctrSaludDoub + sctrPensionDoub + essaludVidaDoub);
            txtTotalAportacionEmpleador.InnerText = "S/. " + (essaludDoub + sctrSaludDoub + sctrPensionDoub + essaludVidaDoub).ToString("N", CultureInfo.GetCultureInfo("es-PE"));
        }
        private void ObtenerTotalPagar()
        {
            string totalIngresos = Session["TotalIngresos"].ToString().Trim();
            string totalBeneficios = Session["TotalBeneficios"].ToString().Trim();
            string totalDescuentos = Session["TotalDescuentos"].ToString().Trim();

            if (totalIngresos == string.Empty) { totalIngresos = "0"; }
            if (totalBeneficios == string.Empty) { totalBeneficios = "0"; }
            if (totalDescuentos == string.Empty) { totalDescuentos = "0"; }

            double totalIngresosDoub = double.Parse(totalIngresos);
            double totalBeneficiosDoub = double.Parse(totalBeneficios);
            double totalDescuentosDoub = double.Parse(totalDescuentos);

            Session["TotalPagar"] = (totalIngresosDoub + totalBeneficiosDoub - totalDescuentosDoub);
            txtTotalPagar.InnerText = "S/. " + (totalIngresosDoub + totalBeneficiosDoub - totalDescuentosDoub).ToString("N", CultureInfo.GetCultureInfo("es-PE"));
        }
        private void ObtenerCostoTotalTrabajador()
        {
            string totalIngresos = Session["TotalIngresos"].ToString().Trim();
            string totalBeneficios = Session["TotalBeneficios"].ToString().Trim();
            string totalPagarEmmpleador = Session["TotalAportacionesEmpleador"].ToString().Trim();

            if (totalIngresos == string.Empty) { totalIngresos = "0"; }
            if (totalBeneficios == string.Empty) { totalBeneficios = "0"; }
            if (totalPagarEmmpleador == string.Empty) { totalPagarEmmpleador = "0"; }

            double totalIngresoDoub = double.Parse(totalIngresos);
            double totalBeneficiosDoub = double.Parse(totalBeneficios);
            double totalPagarEmmpleadorDoub = double.Parse(totalPagarEmmpleador);

            Session["CostoTotalTrabajador"] = (totalIngresoDoub + totalBeneficiosDoub + totalPagarEmmpleadorDoub);
            txtCostoTotalTrabajador.InnerText = "S/. " + (totalIngresoDoub + totalBeneficiosDoub + totalPagarEmmpleadorDoub).ToString("N", CultureInfo.GetCultureInfo("es-PE"));
        }
        private void CargarListadoTrabajadores()
        {
            try
            {
                DataTable dt = procesosTrabajador.ObtenerListaTrabajadoresEmpleados();
                cboTrabajadores.DataSource = dt;
                cboTrabajadores.DataTextField = "NombreTrabajador";
                cboTrabajadores.DataValueField = "CodTrabajador";
                cboTrabajadores.DataBind();
                cboTrabajadores.Items.Insert(0, "Seleccione");
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }
        private void GenerarBoletaTrabajadorPrevio(string codTrabajador)
        {
            DataTable dt = procesosTrabajador.ObtenerDatosTrabajadorBoletaPlanillaPrevio(Convert.ToInt32(codTrabajador));
            lblNombreTrabajadorBoleta.Text = dt.Rows[0][0].ToString();
            lblDocumentoTrabajadorBoleta.Text = dt.Rows[0][1].ToString();
            lblCargoTrabajadorBoleta.Text = dt.Rows[0][2].ToString();
            lblNombreBancoBoleta.Text = dt.Rows[0][3].ToString();
            lblNroCCIBoleta.Text = dt.Rows[0][4].ToString();
            lblNroCuentaBoleta.Text = dt.Rows[0][4].ToString();

            lblPeriodoBoleta.Text = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(Convert.ToInt32(cboMesPeriodo.SelectedValue)) + ", " + txtFechaAnoPeriodoSelect.Text;
            lblSemanaBoleta.Text = txtFechaInicial.Text + " - " + txtFechaFinal.Text;

            lblDiasLaboradosAporteEmp.Text = txtDiasLaborados.Text + " días";
            lblDiasLaboradosBeneficios.Text = txtDiasLaborados.Text + " días";
            lblDiasLaboradosDescuentos.Text = txtDiasLaborados.Text + " días";
            lblDiasLaboradosIngresos.Text = txtDiasLaborados.Text + " días";

            lblDiasDominicalAporteEmp.Text = txtDiasDomingosFeriados.Text + " días";
            lblDiasDominicalBeneficios.Text = txtDiasDomingosFeriados.Text + " días";
            lblDiasDominicalDescuentos.Text = txtDiasDomingosFeriados.Text + " días";
            lblDiasDominicalIngresos.Text = txtDiasDomingosFeriados.Text + " días";

            lblTotalIngreso.Text = Session["TotalIngresos"].ToString();
            lblTotalBeneficios.Text = Session["TotalBeneficios"].ToString();
            lblTotalDescuentos.Text = Session["TotalDescuentos"].ToString();
            lblTotalAporteEmp.Text = Session["TotalAportacionesEmpleador"].ToString();

            lblTotalCostoEmpresa.InnerText = "S/. " + Session["CostoTotalTrabajador"].ToString();

        }
        private void LimpiarFomulario()
        {
            cboTrabajadores.ClearSelection();
            txtFechaInicial.Text = DateTime.Now.ToShortDateString();
            txtFechaFinal.Text = DateTime.Now.ToShortDateString();
            cboMesPeriodo.ClearSelection();
            txtFechaAnoPeriodoSelect.Text = string.Empty;
            txtDiasLaborados.Text = "0";
            txtDiasDomingosFeriados.Text = "0";
            txtAsignacionFamiliar.Text = string.Empty;
            txtPasajesTrabajador.Text = string.Empty;
            txtOtrosIngresos.Text = string.Empty;
            txtHorasExtrasSimple.Text = string.Empty;
            txtCalculoHorasSimple.Text = "0.00";           
            txtGratificacionTrabajador.Text = string.Empty;
            txtCalculoGratificacion.Text = "0.00";
            txtAdicionalGratificacionTrabajador.Text = string.Empty;
            txtCalculoAdicionalGraticacion.Text = "0.00";
            txtVacacionalTrabajador.Text = string.Empty;
            txtCalculoVacacional.Text = "0.00";
            txtBonifExtraEssaludTrabajador.Text = string.Empty;
            txtCalculoBonifExtraEssalud.Text = "0.00";
            txtOtrosBeneficios.Text = string.Empty;
            txtSNPTrabajador.Text = string.Empty;
            txtAporteObligatorioTrabajador.Text = string.Empty;
            txtComisionFlujoTrabajador.Text = string.Empty;
            txtComisionMixtaTrabajador.Text = string.Empty;
            txtPrimaSeguroTrabajador.Text = string.Empty;            
            txtRenta5taCategoria.Text = string.Empty;
            txtEpsTrabajador.Text = string.Empty;
            txtEssaludVidaTrabajador.Text = string.Empty;
            txtOtrosDescuentosTrabajador.Text = string.Empty;
            txtEsSaludTrabajador.Text = string.Empty;
            txtCalculoEsSaludTrabajador.Text = "0.00";
            txtSCTRSaludTrabajador.Text = string.Empty;
            txtCalculoSCTRSaludTrabajador.Text = "0.00";
            txtSCTRPensionTrabajador.Text = string.Empty;
            txtCalculoSCTRPensionTrabajador.Text = "0.00";
            txtCargoTrabajador.Text = string.Empty;
            txtHaberMensualTrabajador.Text = string.Empty;
            txtJornalTrabajador.Text = string.Empty;
            txtTotalCalculadoDiasLaborados.Text = "0.00";
            txtCalculoDiasDomingosFeriados.Text = "0.00";
            txtTotalIngresos.InnerText = "0.00";
            txtTotalBeneficios.InnerText = "0.00";
            txtTotalDescuentos.InnerText = "0.00";
            txtTotalAportacionEmpleador.InnerText = "0.00";
            txtTotalPagar.InnerText = "0.00";
            txtCostoTotalTrabajador.InnerText = "0.00";

            chkComisionFlujo.Checked = false;
            chkComisionMixta.Checked = false;

            Session["TotalIngresos"] = "";
            Session["TotalBeneficios"] = "";
            Session["TotalDescuentos"] = "";
            Session["TotalAportacionesEmpleador"] = "";
            Session["TotalPagar"] = "";
            Session["CostoTotalTrabajador"] = "";
            Session["CodPlanillaModificar"] = "";
        }

        private void LimpiarParametros()
        {
            txtDiasLaborados.Text = "0";
            txtDiasDomingosFeriados.Text = "0";
            txtAsignacionFamiliar.Text = string.Empty;
            txtPasajesTrabajador.Text = string.Empty;
            txtOtrosIngresos.Text = string.Empty;
            txtHorasExtrasSimple.Text = string.Empty;
            txtCalculoHorasSimple.Text = "0.00";            
            txtGratificacionTrabajador.Text = string.Empty;
            txtCalculoGratificacion.Text = "0.00";
            txtAdicionalGratificacionTrabajador.Text = string.Empty;
            txtCalculoAdicionalGraticacion.Text = "0.00";
            txtVacacionalTrabajador.Text = string.Empty;
            txtCalculoVacacional.Text = "0.00";
            txtBonifExtraEssaludTrabajador.Text = string.Empty;
            txtCalculoBonifExtraEssalud.Text = "0.00";
            txtOtrosBeneficios.Text = string.Empty;
            txtSNPTrabajador.Text = string.Empty;
            txtAporteObligatorioTrabajador.Text = string.Empty;
            txtComisionFlujoTrabajador.Text = string.Empty;
            txtComisionMixtaTrabajador.Text = string.Empty;
            txtPrimaSeguroTrabajador.Text = string.Empty;            
            txtRenta5taCategoria.Text = string.Empty;
            txtEpsTrabajador.Text = string.Empty;
            txtEssaludVidaTrabajador.Text = string.Empty;
            txtOtrosDescuentosTrabajador.Text = string.Empty;
            txtEsSaludTrabajador.Text = string.Empty;
            txtCalculoEsSaludTrabajador.Text = "0.00";
            txtSCTRSaludTrabajador.Text = string.Empty;
            txtCalculoSCTRSaludTrabajador.Text = "0.00";
            txtSCTRPensionTrabajador.Text = string.Empty;
            txtCalculoSCTRPensionTrabajador.Text = "0.00";
            txtTotalCalculadoDiasLaborados.Text = "0.00";
            txtCalculoDiasDomingosFeriados.Text = "0.00";
            txtTotalIngresos.InnerText = "0.00";
            txtTotalBeneficios.InnerText = "0.00";
            txtTotalDescuentos.InnerText = "0.00";
            txtTotalAportacionEmpleador.InnerText = "0.00";
            txtTotalPagar.InnerText = "0.00";
            txtCostoTotalTrabajador.InnerText = "0.00";

            chkComisionFlujo.Checked = false;
            chkComisionMixta.Checked = false;

            Session["TotalIngresos"] = "";
            Session["TotalBeneficios"] = "";
            Session["TotalDescuentos"] = "";
            Session["TotalAportacionesEmpleador"] = "";
            Session["TotalPagar"] = "";
            Session["CostoTotalTrabajador"] = "";
            Session["CodPlanillaModificar"] = "";
        }
        private void GenerarReporteBoletaPagoInvidual(string codPlanilla)
        {
            /*CARGAMOS PRIMER DATASET*/

            System.Data.DataSet DataSetPlanilla = new System.Data.DataSet();

            DataSetPlanilla.Tables.Add(procesoReporte.ObtenerReporteBoletaPagoIndividualEmpleados(Convert.ToInt32(codPlanilla)));

            ReportDataSource datosSolicitante = new ReportDataSource("PlanillaBoletaPagoEmpleadosDataSet", DataSetPlanilla.Tables[0]);


            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(datosSolicitante);
            ReportViewer1.LocalReport.Refresh();
        }

      
    }
}