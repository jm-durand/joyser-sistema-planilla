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
    public partial class GenerarPlanillaIndividual : System.Web.UI.Page
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
                    CargarTipoPlanilla();
                    CargarProyectoPlanilla();
                    //CargarListadoTrabajadores();

                    txtFechaInicial.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    txtFechaFinal.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    txtFechaInicialReporte.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    txtFechaFinalReporte.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    txtFechaAnoPeriodoSelect.Text = DateTime.Now.Year.ToString();

                    txtBonifExtraEssaludTrabajador.Enabled = false;
                    txtBonifExtraPensionTrabajador.Enabled = false;

                    gpBoletaPlanilla.Visible = false;
                    gpFormularioTrabajador.Visible = false;
                    gpFormularioParametros.Visible = false;
                    gpBtnGenerarBoleta.Visible = false;
       
                    Session["CodPlanillaModificar"] = "";
                    gpReporteRVIndividual.Style.Add("display", "none");
                    PlanillasRecientes(txtFechaInicialReporte.Text.Trim(), txtFechaFinalReporte.Text.Trim());
                }
                else
                {
                    Response.Redirect(ConfigurationManager.AppSettings["AssetsUrl"] + "/Seguridad/Logout.aspx");
                }              
            }
        }
        protected void cboTrabajadores_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cboTrabajadores.SelectedValue.ToString() != "Seleccione")
                {
                    CargarDatosTrabajadorPlanilla(cboTrabajadores.SelectedValue.ToString(), "1");
                    ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Rangofecha", "CargarDateRange();", true);

                    gpFormularioParametros.Visible = true;
                    gpBtnGenerarBoleta.Visible = true;
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
                    ObtenerIngresos(cboTrabajadores.SelectedValue.ToString(), txtDiasLaborados.Text.Trim(),"1");
                    ObtenerBeneficios(cboTrabajadores.SelectedValue.ToString(), txtDiasLaborados.Text.Trim(), cboMesPeriodo.SelectedValue.ToString().Trim(),"1");                    

                    ObtenerTotalIngresos();
                    ObtenerTotalBeneficios();

                    ObtenerDescuentos(cboTrabajadores.SelectedValue.ToString(), txtDiasLaborados.Text.Trim(), txtDiasDomingosFeriados.Text.Trim(), Session["TotalIngresos"].ToString(), "1");
                    ObtenerTotalDescuentos();                    

                    if (chkBonificacionExtra.Checked)
                    {
                        ObtenerBeneficiosExtras(cboTrabajadores.SelectedValue.ToString(), txtDiasLaborados.Text.Trim(), "1");
                        ObtenerTotalBeneficios();
                    }

                    ObtenerTotalPagar();
                    ObtenerAportacionEmpleador(cboTrabajadores.SelectedValue.ToString(), txtDiasLaborados.Text.Trim(), Session["TotalIngresos"].ToString(), "1");
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
                    ObtenerDomingoFeriados(cboTrabajadores.SelectedValue.ToString(), txtDiasDomingosFeriados.Text.Trim(),"1");
                    ObtenerTotalIngresos();
                    ObtenerTotalBeneficios();
                    ObtenerDescuentos(cboTrabajadores.SelectedValue.ToString(), txtDiasLaborados.Text.Trim(), txtDiasDomingosFeriados.Text.Trim(), Session["TotalIngresos"].ToString(), "1");
                    ObtenerTotalDescuentos();                    

                    if (chkBonificacionExtra.Checked)
                    {
                        ObtenerBeneficiosExtras(cboTrabajadores.SelectedValue.ToString(), txtDiasLaborados.Text.Trim(), "1");
                        ObtenerTotalBeneficios();                        
                    }

                    ObtenerTotalPagar();
                    ObtenerAportacionEmpleador(cboTrabajadores.SelectedValue.ToString(), txtDiasLaborados.Text.Trim(), Session["TotalIngresos"].ToString(), "1");
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
        protected void txtReintegros_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ObtenerTotalIngresos();
                ObtenerDescuentos(cboTrabajadores.SelectedValue.ToString(), txtDiasLaborados.Text.Trim(), txtDiasDomingosFeriados.Text.Trim(), Session["TotalIngresos"].ToString(), "1");
                ObtenerTotalDescuentos();

                if (chkBonificacionExtra.Checked)
                {
                    ObtenerBeneficiosExtras(cboTrabajadores.SelectedValue.ToString(), txtDiasLaborados.Text.Trim(), "1");
                    ObtenerTotalBeneficios();
                }

                ObtenerTotalPagar();
                ObtenerAportacionEmpleador(cboTrabajadores.SelectedValue.ToString(), txtDiasLaborados.Text.Trim(), Session["TotalIngresos"].ToString(), "1");
                ObtenerTotalaAportacionesEmpleador();
                ObtenerCostoTotalTrabajador();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }

        protected void txtBonificacion_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ObtenerTotalIngresos();
                ObtenerDescuentos(cboTrabajadores.SelectedValue.ToString(), txtDiasLaborados.Text.Trim(), txtDiasDomingosFeriados.Text.Trim(), Session["TotalIngresos"].ToString(), "1");
                ObtenerTotalDescuentos();

                if (chkBonificacionExtra.Checked)
                {
                    ObtenerBeneficiosExtras(cboTrabajadores.SelectedValue.ToString(), txtDiasLaborados.Text.Trim(), "1");
                    ObtenerTotalBeneficios();
                }

                ObtenerTotalPagar();
                ObtenerAportacionEmpleador(cboTrabajadores.SelectedValue.ToString(), txtDiasLaborados.Text.Trim(), Session["TotalIngresos"].ToString(), "1");
                ObtenerTotalaAportacionesEmpleador();
                ObtenerCostoTotalTrabajador();
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
                ObtenerDescuentos(cboTrabajadores.SelectedValue.ToString(), txtDiasLaborados.Text.Trim(), txtDiasDomingosFeriados.Text.Trim(), Session["TotalIngresos"].ToString(), "1");
                ObtenerTotalDescuentos();

                if (chkBonificacionExtra.Checked)
                {
                    ObtenerBeneficiosExtras(cboTrabajadores.SelectedValue.ToString(), txtDiasLaborados.Text.Trim(), "1");
                    ObtenerTotalBeneficios();
                }

                ObtenerTotalPagar();
                ObtenerAportacionEmpleador(cboTrabajadores.SelectedValue.ToString(), txtDiasLaborados.Text.Trim(), Session["TotalIngresos"].ToString(), "1");
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
                ObtenerDescuentos(cboTrabajadores.SelectedValue.ToString(), txtDiasLaborados.Text.Trim(), txtDiasDomingosFeriados.Text.Trim(), Session["TotalIngresos"].ToString(), "1");
                ObtenerTotalDescuentos();

                if (chkBonificacionExtra.Checked)
                {
                    ObtenerBeneficiosExtras(cboTrabajadores.SelectedValue.ToString(), txtDiasLaborados.Text.Trim(), "1");
                    ObtenerTotalBeneficios();
                }

                ObtenerTotalPagar();
                ObtenerAportacionEmpleador(cboTrabajadores.SelectedValue.ToString(), txtDiasLaborados.Text.Trim(), Session["TotalIngresos"].ToString(), "1");
                ObtenerTotalaAportacionesEmpleador();
                ObtenerCostoTotalTrabajador();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }

        protected void txtHorasExtras60_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ObtenerTotalIngresos();
                ObtenerDescuentos(cboTrabajadores.SelectedValue.ToString(), txtDiasLaborados.Text.Trim(), txtDiasDomingosFeriados.Text.Trim(), Session["TotalIngresos"].ToString(), "1");
                ObtenerTotalDescuentos();

                if (chkBonificacionExtra.Checked)
                {
                    ObtenerBeneficiosExtras(cboTrabajadores.SelectedValue.ToString(), txtDiasLaborados.Text.Trim(), "1");
                    ObtenerTotalBeneficios();
                }

                ObtenerTotalPagar();
                ObtenerAportacionEmpleador(cboTrabajadores.SelectedValue.ToString(), txtDiasLaborados.Text.Trim(), Session["TotalIngresos"].ToString(), "1");
                ObtenerTotalaAportacionesEmpleador();
                ObtenerCostoTotalTrabajador();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }

        protected void txtHorasExtras100_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ObtenerTotalIngresos();
                ObtenerDescuentos(cboTrabajadores.SelectedValue.ToString(), txtDiasLaborados.Text.Trim(), txtDiasDomingosFeriados.Text.Trim(), Session["TotalIngresos"].ToString(), "1");
                ObtenerTotalDescuentos();

                if (chkBonificacionExtra.Checked)
                {
                    ObtenerBeneficiosExtras(cboTrabajadores.SelectedValue.ToString(), txtDiasLaborados.Text.Trim(), "1");
                    ObtenerTotalBeneficios();
                }

                ObtenerTotalPagar();
                ObtenerAportacionEmpleador(cboTrabajadores.SelectedValue.ToString(), txtDiasLaborados.Text.Trim(), Session["TotalIngresos"].ToString(), "1");
                ObtenerTotalaAportacionesEmpleador();
                ObtenerCostoTotalTrabajador();
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
                    txtBonifExtraPensionTrabajador.Enabled = true;
                    ObtenerTotalIngresos();
                    ObtenerBeneficiosExtras(cboTrabajadores.SelectedValue.ToString(), txtDiasLaborados.Text.Trim(), "1");
                    ObtenerTotalBeneficios();
                    ObtenerTotalDescuentos();
                    ObtenerTotalPagar();
                    ObtenerAportacionEmpleador(cboTrabajadores.SelectedValue.ToString(), txtDiasLaborados.Text.Trim(), Session["TotalIngresos"].ToString(), "1");
                    ObtenerTotalaAportacionesEmpleador();
                    ObtenerCostoTotalTrabajador();
                }
                else
                {
                    txtBonifExtraEssaludTrabajador.Enabled = false;
                    txtBonifExtraPensionTrabajador.Enabled = false;

                    txtCalculoBonifExtraEssalud.Text = "0.00";
                    txtBonifExtraEssaludTrabajador.Text = string.Empty;
                    txtCalculoBonifExtraPension.Text = "0.00";
                    txtBonifExtraPensionTrabajador.Text = string.Empty;
                    ObtenerTotalIngresos();
                    ObtenerTotalBeneficios();
                    ObtenerTotalDescuentos();
                    ObtenerTotalPagar();
                    ObtenerAportacionEmpleador(cboTrabajadores.SelectedValue.ToString(), txtDiasLaborados.Text.Trim(), Session["TotalIngresos"].ToString(), "1");
                    ObtenerTotalaAportacionesEmpleador();
                    ObtenerCostoTotalTrabajador();
                }
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
                    DataTable dt = procesoPlanilla.ObtenerParametrosDescuentosPlanilla(Convert.ToInt32(cboTrabajadores.SelectedValue.ToString()), Convert.ToInt32(txtDiasLaborados.Text.Trim()), Convert.ToInt32(txtDiasDomingosFeriados.Text.Trim()), Convert.ToDouble(Session["TotalIngresos"].ToString()),1);

                    txtCalculoComisionFlujoTrabajador.Text = Convert.ToDouble(dt.Rows[0][5]).ToString("N", CultureInfo.GetCultureInfo("es-PE"));
                    txtComisionFlujoTrabajador.Text = Convert.ToDouble(dt.Rows[0][6]).ToString("N", CultureInfo.GetCultureInfo("es-PE"));

                    ObtenerTotalBeneficios();
                    ObtenerTotalDescuentos();
                    ObtenerTotalPagar();
                    ObtenerAportacionEmpleador(cboTrabajadores.SelectedValue.ToString(), txtDiasLaborados.Text.Trim(), Session["TotalIngresos"].ToString(), "1");
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
                    ObtenerAportacionEmpleador(cboTrabajadores.SelectedValue.ToString(), txtDiasLaborados.Text.Trim(), Session["TotalIngresos"].ToString(), "1");
                    ObtenerTotalaAportacionesEmpleador();
                    ObtenerCostoTotalTrabajador();
                }

                if (chkComisionMixta.Checked)
                {
                    ObtenerTotalIngresos();
                    DataTable dt = procesoPlanilla.ObtenerParametrosDescuentosPlanilla(Convert.ToInt32(cboTrabajadores.SelectedValue.ToString()), Convert.ToInt32(txtDiasLaborados.Text.Trim()), Convert.ToInt32(txtDiasDomingosFeriados.Text.Trim()), Convert.ToDouble(Session["TotalIngresos"].ToString()),1);

                    txtCalculoComisionMixtaTrabajador.Text = Convert.ToDouble(dt.Rows[0][11]).ToString("N", CultureInfo.GetCultureInfo("es-PE"));
                    txtComisionMixtaTrabajador.Text = Convert.ToDouble(dt.Rows[0][12]).ToString("N", CultureInfo.GetCultureInfo("es-PE"));

                    ObtenerTotalBeneficios();
                    ObtenerTotalDescuentos();
                    ObtenerTotalPagar();
                    ObtenerAportacionEmpleador(cboTrabajadores.SelectedValue.ToString(), txtDiasLaborados.Text.Trim(), Session["TotalIngresos"].ToString(), "1");
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
                    ObtenerAportacionEmpleador(cboTrabajadores.SelectedValue.ToString(), txtDiasLaborados.Text.Trim(), Session["TotalIngresos"].ToString(), "1");
                    ObtenerTotalaAportacionesEmpleador();
                    ObtenerCostoTotalTrabajador();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }
        protected void cboMesPeriodo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (cboTrabajadores.SelectedValue != "Seleccione")
                {
                    ObtenerTotalIngresos();

                    ObtenerBeneficios(cboTrabajadores.SelectedValue.ToString(), txtDiasLaborados.Text.Trim(), cboMesPeriodo.SelectedValue.ToString().Trim(),"1");

                    ObtenerTotalBeneficios();

                    ObtenerTotalDescuentos();

                    ObtenerTotalPagar();

                    ObtenerAportacionEmpleador(cboTrabajadores.SelectedValue.ToString(), txtDiasLaborados.Text.Trim(), Session["TotalIngresos"].ToString(), "1");

                    ObtenerTotalaAportacionesEmpleador();

                    ObtenerCostoTotalTrabajador();
                }
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
                ObtenerDescuentos(cboTrabajadores.SelectedValue.ToString(), txtDiasLaborados.Text.Trim(), txtDiasDomingosFeriados.Text.Trim(), Session["TotalIngresos"].ToString(), "1");
                ObtenerTotalDescuentos();
                ObtenerTotalPagar();
                ObtenerAportacionEmpleador(cboTrabajadores.SelectedValue.ToString(), txtDiasLaborados.Text.Trim(), Session["TotalIngresos"].ToString(), "1");
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
                ObtenerDescuentos(cboTrabajadores.SelectedValue.ToString(), txtDiasLaborados.Text.Trim(), txtDiasDomingosFeriados.Text.Trim(), Session["TotalIngresos"].ToString(), "1");
                ObtenerTotalDescuentos();
                ObtenerTotalPagar();
                ObtenerAportacionEmpleador(cboTrabajadores.SelectedValue.ToString(), txtDiasLaborados.Text.Trim(), Session["TotalIngresos"].ToString(), "1");
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
                ObtenerDescuentos(cboTrabajadores.SelectedValue.ToString(), txtDiasLaborados.Text.Trim(), txtDiasDomingosFeriados.Text.Trim(), Session["TotalIngresos"].ToString(), "1");
                ObtenerTotalDescuentos();
                ObtenerTotalPagar();
                ObtenerAportacionEmpleador(cboTrabajadores.SelectedValue.ToString(), txtDiasLaborados.Text.Trim(), Session["TotalIngresos"].ToString(), "1");
                ObtenerTotalaAportacionesEmpleador();
                ObtenerCostoTotalTrabajador();
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
        protected void btnMostrarListaTrabajador_Click(object sender, EventArgs e)
        {
            gpFormularioTrabajador.Visible = true;
            gpPlanillaRecientes.Visible = false;
        }
        protected void btnRegresarCalculoPlanilla_Click(object sender, EventArgs e)
        {
            gpFormularioCalculoPlanilla.Visible = true;
            gpBoletaPlanilla.Visible = false;
        }
        protected void btnGuardarPlanilla_Click(object sender, EventArgs e)
        {
            try
            {
                string codTrabajador = cboTrabajadores.SelectedValue.ToString();
                string codProyectoPlanilla = cboProyectoPlanilla.SelectedValue.ToString();
                string fechaInicio = txtFechaInicial.Text.Trim();
                string fechaFinal = txtFechaFinal.Text.Trim();
                string periodoMes = cboMesPeriodo.SelectedValue.ToString().Trim();
                string periodoAno = txtFechaAnoPeriodoSelect.Text.Trim();
                string diasLaborado = txtDiasLaborados.Text.Trim();
                string diasDominical = txtDiasDomingosFeriados.Text.Trim();
                string asignacionFamiliar = txtAsignacionFamiliar.Text.Trim();
                string reintegro = txtReintegros.Text.Trim();
                string bonificacion = txtBonificacion.Text.Trim();
                string horasExtraSimple = txtHorasExtrasSimple.Text.Trim();
                string horasExtra60 = txtHorasExtras60.Text.Trim();
                string horasExtra100 = txtHorasExtras100.Text.Trim();
                string buc = txtBucTrabajador.Text.Trim();
                string pasajes = txtPasajesTrabajador.Text.Trim();
                string vacacional = txtVacacionalTrabajador.Text.Trim();
                string gratificacion = txtGratificacionTrabajador.Text.Trim();
                string liquidacion = txtLiquidacionTrabajador.Text.Trim();
                string bonificacionExtraSalud = txtBonifExtraEssaludTrabajador.Text.Trim();
                string bonificacionExtraPension = txtBonifExtraPensionTrabajador.Text.Trim();
                string snp = txtSNPTrabajador.Text.Trim();
                string aporteObligatorio = txtAporteObligatorioTrabajador.Text.Trim();
                string conafovicer = txtConafovicerTrabajador.Text.Trim();
                string comisionFlujo = txtComisionFlujoTrabajador.Text.Trim();
                string comisionMixta = txtComisionMixtaTrabajador.Text.Trim();
                string primaSeguro = txtPrimaSeguroTrabajador.Text.Trim();
                string aporteComplementario = txtAporteComplementarioTrabajador.Text.Trim();
                string aporteSindical = txtAporteSindicalTrabajador.Text.Trim();
                string essaludVida = txtEssaludVidaTrabajador.Text.Trim();
                string renta5taCategoria = txtRenta5taCategoria.Text.Trim();
                string eps = txtEpsTrabajador.Text.Trim();
                string otrosDescuentos = txtOtrosDescuentosTrabajador.Text.Trim();
                string essalud = txtEsSaludTrabajador.Text.Trim();
                string aporteComplementarioAFP = txtAporteComplementarioAFPTrabajador.Text.Trim();
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
                if (reintegro == string.Empty) { reintegro = "0"; }
                if (bonificacion == string.Empty) { bonificacion = "0"; }
                if (horasExtraSimple == string.Empty) { horasExtraSimple = "0"; }
                if (horasExtra60 == string.Empty) { horasExtra60 = "0"; }
                if (horasExtra100 == string.Empty) { horasExtra100 = "0"; }
                if (buc == string.Empty) { buc = "0"; }
                if (pasajes == string.Empty) { pasajes = "0"; }
                if (vacacional == string.Empty) { vacacional = "0"; }
                if (gratificacion == string.Empty) { gratificacion = "0"; }
                if (liquidacion == string.Empty) { liquidacion = "0"; }
                if (bonificacionExtraSalud == string.Empty) { bonificacionExtraSalud = "0"; }
                if (bonificacionExtraPension == string.Empty) { bonificacionExtraPension = "0"; }
                if (snp == string.Empty) { snp = "0"; }
                if (aporteObligatorio == string.Empty) { aporteObligatorio = "0"; }
                if (conafovicer == string.Empty) { conafovicer = "0"; }
                if (comisionFlujo == string.Empty) { comisionFlujo = "0"; }
                if (comisionMixta == string.Empty) { comisionMixta = "0"; }
                if (primaSeguro == string.Empty) { primaSeguro = "0"; }
                if (aporteComplementario == string.Empty) { aporteComplementario = "0"; }
                if (aporteSindical == string.Empty) { aporteSindical = "0"; }
                if (essaludVida == string.Empty) { essaludVida = "0"; }
                if (renta5taCategoria == string.Empty) { renta5taCategoria = "0"; }
                if (eps == string.Empty) { eps = "0"; }
                if (otrosDescuentos == string.Empty) { otrosDescuentos = "0"; }
                if (essalud == string.Empty) { essalud = "0"; }
                if (aporteComplementarioAFP == string.Empty) { aporteComplementarioAFP = "0"; }
                if (sctrSalud == string.Empty) { sctrSalud = "0"; }
                if (sctrPension == string.Empty) { sctrPension = "0"; }

                string mensaje;
                if (Session["CodPlanillaModificar"].ToString() == string.Empty)
                {
                    mensaje = RegistrarPlanillaConstruccion(codTrabajador, fechaInicio, fechaFinal, periodoMes, periodoAno, diasLaborado, diasDominical, asignacionFamiliar, reintegro, bonificacion,
                    horasExtraSimple, horasExtra60, horasExtra100, buc, pasajes, vacacional, gratificacion, liquidacion, bonificacionExtraSalud, bonificacionExtraPension, snp, aporteObligatorio, comisionFlujo,
                    comisionMixta, primaSeguro, aporteComplementario, conafovicer, aporteSindical, essaludVida, renta5taCategoria, eps, otrosDescuentos, essalud, aporteComplementarioAFP, sctrSalud, sctrPension,
                    totalIngresos, totalBeneficios, totalDescuentos, totalAporteEmpresa, totalPagarTrabajador, totalCostoTrabajador, codUsuarioRegistrador, codProyectoPlanilla);
                }
                else
                {
                    mensaje = ActualizarPlanillaConstruccion(codTrabajador, fechaInicio, fechaFinal, periodoMes, periodoAno, diasLaborado, diasDominical, asignacionFamiliar, reintegro, bonificacion,
                    horasExtraSimple, horasExtra60, horasExtra100, buc, pasajes, vacacional, gratificacion, liquidacion, bonificacionExtraSalud, bonificacionExtraPension, snp, aporteObligatorio, comisionFlujo,
                    comisionMixta, primaSeguro, aporteComplementario, conafovicer, aporteSindical, essaludVida, renta5taCategoria, eps, otrosDescuentos, essalud, aporteComplementarioAFP, sctrSalud, sctrPension,
                    totalIngresos, totalBeneficios, totalDescuentos, totalAporteEmpresa, totalPagarTrabajador, totalCostoTrabajador, codUsuarioRegistrador, Session["CodPlanillaModificar"].ToString(), codProyectoPlanilla);
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
        protected void dgvPlanillaRecientes_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                dgvPlanillaRecientes.PageIndex = e.NewPageIndex;
                PlanillasRecientes(txtFechaInicialReporte.Text.Trim(),txtFechaFinalReporte.Text.Trim());
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
        protected void cboProyectoPlanilla_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cboProyectoPlanilla.SelectedValue != "Seleccione")
                {
                    CargarProyectoTrabajadores(cboProyectoPlanilla.SelectedValue.ToString());
                }
                else
                {
                    gpFormularioParametros.Visible = false;
                    gpBtnGenerarBoleta.Visible = false;

                    cboTrabajadores.ClearSelection();
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
        /*---------------------------------------*/
        /*---------------METODOS-----------------*/
        /*---------------------------------------*/

        private string RegistrarPlanillaConstruccion(string codTrabajador, string fechaInicio, string fechaFin, string mesPeriodo, string anoPeriodo, string diasLaborados, string diasDominical, string asignacionFamiliar, string reintegro, string bonificacion, string horasExtraSiemple, string horasExtra60, string horasExtra100, string buc, string pasajes,
            string vacacional, string gratificacion, string liquidacion, string bonificacionExtraSalud, string bonificacionExtraPension, string snp, string aporteObligatorio, string comisionFlujo, string comisionMixta, string primaSeguro, string aporteComplementario ,string conafovicer, string aporteSindical, string essaludVida, string renta5taCategoria, 
            string eps, string otrosDsctos, string essalud, string aporteComplementarioAFP, string sctrSalud, string sctrPension, string totalIngresos, string totalBeneficios, string totalDescuentos, string totalAporteEmpresa, string totalPagarTrabajador, string totalCostoTrabajador, string codUsuarioRegistro, string codProyectoPlanilla)
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
            planillaEntidad.Reintegro = double.Parse(reintegro, CultureInfo.InvariantCulture);
            planillaEntidad.Bonificacion = double.Parse(bonificacion, CultureInfo.InvariantCulture);
            planillaEntidad.HorasExtraSimple = double.Parse(horasExtraSiemple, CultureInfo.InvariantCulture);
            planillaEntidad.HorasExtra60 = double.Parse(horasExtra60, CultureInfo.InvariantCulture);
            planillaEntidad.HorasExtra100 = double.Parse(horasExtra100, CultureInfo.InvariantCulture);
            planillaEntidad.Buc = double.Parse(buc, CultureInfo.InvariantCulture);
            planillaEntidad.Pasajes = double.Parse(pasajes, CultureInfo.InvariantCulture);
            planillaEntidad.Vacacional = double.Parse(vacacional, CultureInfo.InvariantCulture);
            planillaEntidad.Gratificacion = double.Parse(gratificacion, CultureInfo.InvariantCulture);
            planillaEntidad.Liquidacion = double.Parse(liquidacion, CultureInfo.InvariantCulture);
            planillaEntidad.BonifacionExtraSalud = double.Parse(bonificacionExtraSalud, CultureInfo.InvariantCulture);
            planillaEntidad.BonificacionExtraPension = double.Parse(bonificacionExtraPension, CultureInfo.InvariantCulture);
            planillaEntidad.Snp = double.Parse(snp, CultureInfo.InvariantCulture);
            planillaEntidad.AporteObligatorio = double.Parse(aporteObligatorio, CultureInfo.InvariantCulture);
            planillaEntidad.ComisionFlujo = double.Parse(comisionFlujo, CultureInfo.InvariantCulture);
            planillaEntidad.ComisionMixta = double.Parse(comisionMixta, CultureInfo.InvariantCulture);
            planillaEntidad.PrimaSeguro = double.Parse(primaSeguro, CultureInfo.InvariantCulture);
            planillaEntidad.AporteComplementario = double.Parse(aporteComplementario, CultureInfo.InvariantCulture);
            planillaEntidad.Conafovicer = double.Parse(conafovicer, CultureInfo.InvariantCulture);
            planillaEntidad.AporteSindical = double.Parse(aporteSindical, CultureInfo.InvariantCulture);
            planillaEntidad.EsSaludVida = double.Parse(essaludVida, CultureInfo.InvariantCulture);
            planillaEntidad.Renta5taCategoria = double.Parse(renta5taCategoria, CultureInfo.InvariantCulture);
            planillaEntidad.Eps = double.Parse(eps, CultureInfo.InvariantCulture);
            planillaEntidad.OtrosDescuentos = double.Parse(otrosDsctos, CultureInfo.InvariantCulture);
            planillaEntidad.EsSalud = double.Parse(essalud, CultureInfo.InvariantCulture);
            planillaEntidad.AporteComplementarioAFP = double.Parse(aporteComplementarioAFP, CultureInfo.InvariantCulture);
            planillaEntidad.SctrSalud = double.Parse(sctrSalud, CultureInfo.InvariantCulture);
            planillaEntidad.SctrPension = double.Parse(sctrPension, CultureInfo.InvariantCulture);
            planillaEntidad.TotalIngresos = double.Parse(totalIngresos.Replace(",","."), CultureInfo.InvariantCulture);
            planillaEntidad.TotalBeneficios = double.Parse(totalBeneficios.Replace(",", "."), CultureInfo.InvariantCulture);
            planillaEntidad.TotalDescuentos = double.Parse(totalDescuentos.Replace(",", "."), CultureInfo.InvariantCulture);
            planillaEntidad.TotalAporteEmpresa = double.Parse(totalAporteEmpresa.Replace(",", "."), CultureInfo.InvariantCulture);
            planillaEntidad.TotalPagarTrabajador = double.Parse(totalPagarTrabajador.Replace(",", "."), CultureInfo.InvariantCulture);
            planillaEntidad.TotalCostoTrabajador = double.Parse(totalCostoTrabajador.Replace(",", "."), CultureInfo.InvariantCulture);
            planillaEntidad.CodUsuarioRegistro = Convert.ToInt32(codUsuarioRegistro);
            planillaEntidad.CodProyectoPlanilla = Convert.ToInt32(codProyectoPlanilla);

            return procesoPlanilla.RegistrarPlanillaConstruccion(planillaEntidad);
        }
        private string ActualizarPlanillaConstruccion(string codTrabajador, string fechaInicio, string fechaFin, string mesPeriodo, string anoPeriodo, string diasLaborados, string diasDominical, string asignacionFamiliar, string reintegro, string bonificacion, string horasExtraSiemple, string horasExtra60, string horasExtra100, string buc, string pasajes,
           string vacacional, string gratificacion, string liquidacion, string bonificacionExtraSalud, string bonificacionExtraPension, string snp, string aporteObligatorio, string comisionFlujo, string comisionMixta, string primaSeguro, string aporteComplementario, string conafovicer, string aporteSindical, string essaludVida, string renta5taCategoria,
           string eps, string otrosDsctos, string essalud, string aporteComplementarioAFP, string sctrSalud, string sctrPension, string totalIngresos, string totalBeneficios, string totalDescuentos, string totalAporteEmpresa, string totalPagarTrabajador, string totalCostoTrabajador, string codUsuarioModificacion, string codPlanillaConstruccion, string codProyectoPlanilla)
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
            planillaEntidad.Reintegro = double.Parse(reintegro, CultureInfo.InvariantCulture);
            planillaEntidad.Bonificacion = double.Parse(bonificacion, CultureInfo.InvariantCulture);
            planillaEntidad.HorasExtraSimple = double.Parse(horasExtraSiemple, CultureInfo.InvariantCulture);
            planillaEntidad.HorasExtra60 = double.Parse(horasExtra60, CultureInfo.InvariantCulture);
            planillaEntidad.HorasExtra100 = double.Parse(horasExtra100, CultureInfo.InvariantCulture);
            planillaEntidad.Buc = double.Parse(buc, CultureInfo.InvariantCulture);
            planillaEntidad.Pasajes = double.Parse(pasajes, CultureInfo.InvariantCulture);
            planillaEntidad.Vacacional = double.Parse(vacacional, CultureInfo.InvariantCulture);
            planillaEntidad.Gratificacion = double.Parse(gratificacion, CultureInfo.InvariantCulture);
            planillaEntidad.Liquidacion = double.Parse(liquidacion, CultureInfo.InvariantCulture);
            planillaEntidad.BonifacionExtraSalud = double.Parse(bonificacionExtraSalud, CultureInfo.InvariantCulture);
            planillaEntidad.BonificacionExtraPension = double.Parse(bonificacionExtraPension, CultureInfo.InvariantCulture);
            planillaEntidad.Snp = double.Parse(snp, CultureInfo.InvariantCulture);
            planillaEntidad.AporteObligatorio = double.Parse(aporteObligatorio, CultureInfo.InvariantCulture);
            planillaEntidad.ComisionFlujo = double.Parse(comisionFlujo, CultureInfo.InvariantCulture);
            planillaEntidad.ComisionMixta = double.Parse(comisionMixta, CultureInfo.InvariantCulture);
            planillaEntidad.PrimaSeguro = double.Parse(primaSeguro, CultureInfo.InvariantCulture);
            planillaEntidad.AporteComplementario = double.Parse(aporteComplementario, CultureInfo.InvariantCulture);
            planillaEntidad.Conafovicer = double.Parse(conafovicer, CultureInfo.InvariantCulture);
            planillaEntidad.AporteSindical = double.Parse(aporteSindical, CultureInfo.InvariantCulture);
            planillaEntidad.EsSaludVida = double.Parse(essaludVida, CultureInfo.InvariantCulture);
            planillaEntidad.Renta5taCategoria = double.Parse(renta5taCategoria, CultureInfo.InvariantCulture);
            planillaEntidad.Eps = double.Parse(eps, CultureInfo.InvariantCulture);
            planillaEntidad.OtrosDescuentos = double.Parse(otrosDsctos, CultureInfo.InvariantCulture);
            planillaEntidad.EsSalud = double.Parse(essalud, CultureInfo.InvariantCulture);
            planillaEntidad.AporteComplementarioAFP = double.Parse(aporteComplementarioAFP, CultureInfo.InvariantCulture);
            planillaEntidad.SctrSalud = double.Parse(sctrSalud, CultureInfo.InvariantCulture);
            planillaEntidad.SctrPension = double.Parse(sctrPension, CultureInfo.InvariantCulture);
            planillaEntidad.TotalIngresos = double.Parse(totalIngresos.Replace(",", "."), CultureInfo.InvariantCulture);
            planillaEntidad.TotalBeneficios = double.Parse(totalBeneficios.Replace(",", "."), CultureInfo.InvariantCulture);
            planillaEntidad.TotalDescuentos = double.Parse(totalDescuentos.Replace(",", "."), CultureInfo.InvariantCulture);
            planillaEntidad.TotalAporteEmpresa = double.Parse(totalAporteEmpresa.Replace(",", "."), CultureInfo.InvariantCulture);
            planillaEntidad.TotalPagarTrabajador = double.Parse(totalPagarTrabajador.Replace(",", "."), CultureInfo.InvariantCulture);
            planillaEntidad.TotalCostoTrabajador = double.Parse(totalCostoTrabajador.Replace(",", "."), CultureInfo.InvariantCulture);
            planillaEntidad.CodUsuarioModificacion = Convert.ToInt32(codUsuarioModificacion);
            planillaEntidad.CodPlanillaConstruccion = Convert.ToInt32(codPlanillaConstruccion);
            planillaEntidad.CodProyectoPlanilla = Convert.ToInt32(codProyectoPlanilla);

            return procesoPlanilla.ActualizarPlanillaConstruccion(planillaEntidad);
        }
        private void CargarDatosPlanilla(string codPlanilla)
        {
            DataTable dt = procesoPlanilla.ObtenerDatosPlanilla(Convert.ToInt32(codPlanilla));

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

                cboProyectoPlanilla.SelectedValue= dt.Rows[0][16].ToString();
                cboProyectoPlanilla_SelectedIndexChanged(cboProyectoPlanilla, new EventArgs());
                cboTrabajadores.SelectedValue = dt.Rows[0][4].ToString().Trim();
                cboTrabajadores_SelectedIndexChanged(cboTrabajadores, new EventArgs());

                txtDiasLaborados.Text = Convert.ToInt32(dt.Rows[0][5]).ToString();
                txtDiasDomingosFeriados.Text = Convert.ToInt32(dt.Rows[0][6]).ToString();
                txtAsignacionFamiliar.Text = dt.Rows[0][7].ToString();
                txtReintegros.Text = dt.Rows[0][8].ToString();
                txtBonificacion.Text = dt.Rows[0][9].ToString();
                txtRenta5taCategoria.Text = dt.Rows[0][13].ToString();
                txtEpsTrabajador.Text = dt.Rows[0][14].ToString();
                txtOtrosDescuentosTrabajador.Text = dt.Rows[0][15].ToString();

                txtDiasLaborados_TextChanged(txtDiasLaborados.Text, new EventArgs());
                txtDiasDomingosFeriados_TextChanged(txtDiasDomingosFeriados.Text, new EventArgs());

                if (dt.Rows[0][10].ToString() != "0.00")
                {
                    chkBonificacionExtra.Checked = true;
                    btnActivarChckBox_Click(btnActivarChckBox, new EventArgs());
                }
                else
                {
                    chkBonificacionExtra.Checked = false;
                }

                if (dt.Rows[0][11].ToString() != "0.00")
                {
                    chkComisionFlujo.Checked = true;
                    btnActivarComision_Click(btnActivarComision, new EventArgs());
                }
                else
                {
                    chkComisionFlujo.Checked = false;
                }

                if (dt.Rows[0][12].ToString() != "0.00")
                {
                    chkComisionMixta.Checked = true;
                    btnActivarComision_Click(btnActivarComision, new EventArgs());
                }
                else
                {
                    chkComisionMixta.Checked = false;
                }                

                Session["CodPlanillaModificar"] = codPlanilla;
            }
        }

        private void CargarTipoPlanilla()
        {
            try
            {
                DataTable dt = procesoPlanilla.CargarTipoPlanilla();
                cboTipoPlanilla.DataSource = dt;
                cboTipoPlanilla.DataTextField = "DescPlanilla";
                cboTipoPlanilla.DataValueField = "CodTipoPlanilla";
                cboTipoPlanilla.DataBind();
                cboTipoPlanilla.Items.Insert(0, "Seleccione");
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanelPrincipal, UpdatePanelPrincipal.GetType(), "Mensaje", "Mensaje('" + ex.Message.ToString().Trim().Replace("\r\n", "").Replace("'", "") + "','error');", true);
            }
        }
        private void CargarListadoTrabajadores()
        {
            try
            {
                DataTable dt = procesosTrabajador.ObtenerListaTrabajadores();
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
        private void CargarProyectoTrabajadores(string codProyectoPlanilla)
        {
            try
            {
                DataTable dt = procesosTrabajador.ObtenerProyectoPlanillaTrabajadores(Convert.ToInt32(codProyectoPlanilla));
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
        private void CargarDatosTrabajadorPlanilla(string codTrabajador, string codTipoPlanilla)
        {
            DataTable dt = procesoPlanilla.CargarDatosTrabajadorPlanilla(Convert.ToInt32(codTrabajador), Convert.ToInt32(codTipoPlanilla));

            txtCargoTrabajador.Text = dt.Rows[0][0].ToString();
            txtHaberMensualTrabajador.Text = "S/. " + Convert.ToDouble(dt.Rows[0][1]).ToString("N", CultureInfo.GetCultureInfo("es-PE"));
            txtJornalTrabajador.Text = "S/. " + Convert.ToDouble(dt.Rows[0][2]).ToString("N", CultureInfo.GetCultureInfo("es-PE"));
            txtCalculoHorasSimple.Text = Convert.ToDouble(dt.Rows[0][3]).ToString("N", CultureInfo.GetCultureInfo("es-PE"));
            txtCalculoHorasExtras60.Text = Convert.ToDouble(dt.Rows[0][4]).ToString("N", CultureInfo.GetCultureInfo("es-PE"));
            txtCalculoHorasExtras100.Text = Convert.ToDouble(dt.Rows[0][5]).ToString("N", CultureInfo.GetCultureInfo("es-PE"));
        }
        private void ObtenerIngresos(string codTrabajador, string cantDiasLaborados, string codTipoPlanilla)
        {
            DataTable dt = procesoPlanilla.ObtenerParametrosIngresoPlanilla(Convert.ToInt32(codTrabajador), Convert.ToInt32(cantDiasLaborados), Convert.ToInt32(codTipoPlanilla));

            txtTotalCalculadoDiasLaborados.Text = Convert.ToDouble(dt.Rows[0][0]).ToString("N", CultureInfo.GetCultureInfo("es-PE"));
            txtCalculoBucTrabajador.Text = Convert.ToDouble(dt.Rows[0][1]).ToString("N", CultureInfo.GetCultureInfo("es-PE"));
            txtBucTrabajador.Text = Convert.ToDouble(dt.Rows[0][2]).ToString("N", CultureInfo.GetCultureInfo("es-PE"));
            txtCalculoPasajesTrabajador.Text = Convert.ToDouble(dt.Rows[0][3]).ToString("N", CultureInfo.GetCultureInfo("es-PE"));
            txtPasajesTrabajador.Text = Convert.ToDouble(dt.Rows[0][4]).ToString("N", CultureInfo.GetCultureInfo("es-PE"));

        }
        private void ObtenerBeneficios(string codTrabajador, string cantDiasLaborados, string periodo, string codTipoPlanilla)
        {
            DataTable dt = procesoPlanilla.ObtenerParametrosBeneficiosPlanilla(Convert.ToInt32(codTrabajador), Convert.ToInt32(cantDiasLaborados), Convert.ToInt32(periodo), Convert.ToInt32(codTipoPlanilla));

            txtCalculoVacacional.Text = Convert.ToDouble(dt.Rows[0][1]).ToString("N", CultureInfo.GetCultureInfo("es-PE"));
            txtVacacionalTrabajador.Text = Convert.ToDouble(dt.Rows[0][2]).ToString("N", CultureInfo.GetCultureInfo("es-PE"));
            txtCalculoLiquidacion.Text = Convert.ToDouble(dt.Rows[0][3]).ToString("N", CultureInfo.GetCultureInfo("es-PE"));
            txtLiquidacionTrabajador.Text = Convert.ToDouble(dt.Rows[0][4]).ToString("N", CultureInfo.GetCultureInfo("es-PE"));
            txtCalculoGratificacion.Text = Convert.ToDouble(dt.Rows[0][5]).ToString("N", CultureInfo.GetCultureInfo("es-PE"));
            txtGratificacionTrabajador.Text = Convert.ToDouble(dt.Rows[0][6]).ToString("N", CultureInfo.GetCultureInfo("es-PE"));
        }
        private void ObtenerBeneficiosExtras(string codTrabajador, string cantDiasLaborados, string codTipoPlanilla)
        {
            DataTable dt = procesoPlanilla.ObtenerParametrosBeneficiosExtrasPlanilla(Convert.ToInt32(codTrabajador), Convert.ToInt32(cantDiasLaborados), Convert.ToInt32(codTipoPlanilla));

            txtCalculoBonifExtraEssalud.Text = Convert.ToDouble(dt.Rows[0][1]).ToString("N", CultureInfo.GetCultureInfo("es-PE"));
            txtBonifExtraEssaludTrabajador.Text = (Convert.ToDouble(Session["TotalIngresos"].ToString()) * ((Convert.ToDouble(dt.Rows[0][1]))/100)).ToString("N", CultureInfo.GetCultureInfo("es-PE"));
            txtCalculoBonifExtraPension.Text = Convert.ToDouble(dt.Rows[0][3]).ToString("N", CultureInfo.GetCultureInfo("es-PE"));
            txtBonifExtraPensionTrabajador.Text = (Convert.ToDouble(Session["TotalIngresos"].ToString()) * ((Convert.ToDouble(dt.Rows[0][3])) / 100)).ToString("N", CultureInfo.GetCultureInfo("es-PE"));
        }
        private void ObtenerDescuentos(string codTrabajador, string cantDiasLaborados, string cantDiasDominical, string totalAfecto, string codTipoPlanilla)
        {
            DataTable dt = procesoPlanilla.ObtenerParametrosDescuentosPlanilla(Convert.ToInt32(codTrabajador), Convert.ToInt32(cantDiasLaborados), Convert.ToInt32(cantDiasDominical), Convert.ToDouble(totalAfecto), Convert.ToInt32(codTipoPlanilla));

            txtCalculoSNP.Text = Convert.ToDouble(dt.Rows[0][1]).ToString("N", CultureInfo.GetCultureInfo("es-PE"));
            txtSNPTrabajador.Text = Convert.ToDouble(dt.Rows[0][2]).ToString("N", CultureInfo.GetCultureInfo("es-PE"));
            txtCalculoAporteObligatorio.Text = Convert.ToDouble(dt.Rows[0][3]).ToString("N", CultureInfo.GetCultureInfo("es-PE"));
            txtAporteObligatorioTrabajador.Text = Convert.ToDouble(dt.Rows[0][4]).ToString("N", CultureInfo.GetCultureInfo("es-PE"));
            txtCalculoPrimaSeguroTrabajador.Text = Convert.ToDouble(dt.Rows[0][7]).ToString("N", CultureInfo.GetCultureInfo("es-PE"));
            txtPrimaSeguroTrabajador.Text = Convert.ToDouble(dt.Rows[0][8]).ToString("N", CultureInfo.GetCultureInfo("es-PE"));
            txtCalculoAporteComplementarioTrabajador.Text = Convert.ToDouble(dt.Rows[0][9]).ToString("N", CultureInfo.GetCultureInfo("es-PE"));
            txtAporteComplementarioTrabajador.Text = Convert.ToDouble(dt.Rows[0][10]).ToString("N", CultureInfo.GetCultureInfo("es-PE"));        
            txtCalculoConafovicerTrabajador.Text = Convert.ToDouble(dt.Rows[0][13]).ToString("N", CultureInfo.GetCultureInfo("es-PE"));
            txtConafovicerTrabajador.Text = Convert.ToDouble(dt.Rows[0][14]).ToString("N", CultureInfo.GetCultureInfo("es-PE"));
            txtCalculoAporteSindicalTrabajador.Text = Convert.ToDouble(dt.Rows[0][15]).ToString("N", CultureInfo.GetCultureInfo("es-PE"));
            txtAporteSindicalTrabajador.Text = Convert.ToDouble(dt.Rows[0][16]).ToString("N", CultureInfo.GetCultureInfo("es-PE"));           
        }
        private void ObtenerAportacionEmpleador(string codTrabajador, string cantDiasLaborados, string TotalAfecto, string codTipoPlanilla)
        {
            DataTable dt = procesoPlanilla.ObtenerParametrosAportacionesEmpleadorPlanilla(Convert.ToInt32(codTrabajador), Convert.ToInt32(cantDiasLaborados), Convert.ToDouble(TotalAfecto), Convert.ToInt32(codTipoPlanilla));

            txtCalculoEsSaludTrabajador.Text = Convert.ToDouble(dt.Rows[0][1]).ToString("N", CultureInfo.GetCultureInfo("es-PE"));
            txtEsSaludTrabajador.Text = Convert.ToDouble(dt.Rows[0][2]).ToString("N", CultureInfo.GetCultureInfo("es-PE"));
            txtCalculoAporteComplementarioAFPTrabajador.Text = Convert.ToDouble(dt.Rows[0][3]).ToString("N", CultureInfo.GetCultureInfo("es-PE"));
            txtAporteComplementarioAFPTrabajador.Text = Convert.ToDouble(dt.Rows[0][4]).ToString("N", CultureInfo.GetCultureInfo("es-PE"));
            txtCalculoSCTRSaludTrabajador.Text = Convert.ToDouble(dt.Rows[0][5]).ToString("N", CultureInfo.GetCultureInfo("es-PE"));
            txtSCTRSaludTrabajador.Text = Convert.ToDouble(dt.Rows[0][6]).ToString("N", CultureInfo.GetCultureInfo("es-PE"));
            txtCalculoSCTRPensionTrabajador.Text = Convert.ToDouble(dt.Rows[0][7]).ToString("N", CultureInfo.GetCultureInfo("es-PE"));
            txtSCTRPensionTrabajador.Text = Convert.ToDouble(dt.Rows[0][8]).ToString("N", CultureInfo.GetCultureInfo("es-PE"));
            txtCalculoEssaludVidaTrabajador.Text = Convert.ToDouble(dt.Rows[0][9]).ToString("N", CultureInfo.GetCultureInfo("es-PE"));
            txtEssaludVidaTrabajador.Text = Convert.ToDouble(dt.Rows[0][10]).ToString("N", CultureInfo.GetCultureInfo("es-PE"));
        }
        private void ObtenerDomingoFeriados(string codTrabajador, string cantDiasLaborados, string codTipoPlanilla)        
        {
            DataTable dt = procesoPlanilla.ObtenerParametrosIngresoPlanilla(Convert.ToInt32(codTrabajador), Convert.ToInt32(cantDiasLaborados), Convert.ToInt32(codTipoPlanilla));
            txtCalculoDiasDomingosFeriados.Text = Convert.ToDouble(dt.Rows[0][0]).ToString("N", CultureInfo.GetCultureInfo("es-PE"));           
        }
        private void ObtenerTotalIngresos()
        {
            string calculoDiasLaborado = txtTotalCalculadoDiasLaborados.Text.Trim();
            string calculoDominical = txtCalculoDiasDomingosFeriados.Text.Trim();
            string asignacionFamiliar = txtAsignacionFamiliar.Text.Trim();
            string reintegro = txtReintegros.Text.Trim();
            string bonificacion = txtBonificacion.Text.Trim();
            string buc = txtBucTrabajador.Text.Trim();
            string pasajes = txtPasajesTrabajador.Text.Trim();
            string horasExSimples = txtHorasExtrasSimple.Text.Trim();
            string horasEx60 = txtHorasExtras60.Text.Trim();
            string horasEx100 = txtHorasExtras100.Text.Trim();

            if (calculoDiasLaborado == string.Empty) { calculoDiasLaborado = "0"; }
            if (calculoDominical == string.Empty) { calculoDominical = "0"; }
            if (asignacionFamiliar == string.Empty) { asignacionFamiliar = "0"; }
            if (reintegro == string.Empty) { reintegro = "0"; }
            if (bonificacion == string.Empty) { bonificacion = "0"; }
            if (buc == string.Empty) { buc = "0"; }
            if (pasajes == string.Empty) { pasajes = "0"; }
            if (horasExSimples == string.Empty) { horasExSimples = "0"; }
            if (horasEx60 == string.Empty) { horasEx60 = "0"; }
            if (horasEx100 == string.Empty) { horasEx100 = "0"; }

            double totalLaborado = double.Parse(calculoDiasLaborado, CultureInfo.InvariantCulture);
            double totalDominical = double.Parse(calculoDominical, CultureInfo.InvariantCulture);
            double asignacionFamiliarDoub = double.Parse(asignacionFamiliar, CultureInfo.InvariantCulture);
            double reintegroDoub = double.Parse(reintegro, CultureInfo.InvariantCulture);
            double bonificacionDoub = double.Parse(bonificacion, CultureInfo.InvariantCulture);
            double bucDoub = double.Parse(buc, CultureInfo.InvariantCulture);
            double pasajesDoub = double.Parse(pasajes, CultureInfo.InvariantCulture);
            double horasExSimplesDoub = double.Parse(horasExSimples, CultureInfo.InvariantCulture) * double.Parse(txtCalculoHorasSimple.Text.Trim(), CultureInfo.InvariantCulture);
            double horasEx60Doub = double.Parse(horasEx60, CultureInfo.InvariantCulture) * double.Parse(txtCalculoHorasExtras60.Text.Trim(), CultureInfo.InvariantCulture);
            double horasEx100Doub = double.Parse(horasEx100, CultureInfo.InvariantCulture) * double.Parse(txtCalculoHorasExtras100.Text.Trim(), CultureInfo.InvariantCulture);

            Session["TotalIngresos"] = (totalLaborado + totalDominical + asignacionFamiliarDoub + reintegroDoub + bonificacionDoub + bucDoub + pasajesDoub + horasExSimplesDoub + horasEx60Doub + horasEx100Doub).ToString();
            txtTotalIngresos.InnerText = "S/. " + (totalLaborado + totalDominical + asignacionFamiliarDoub + reintegroDoub + bonificacionDoub + bucDoub + pasajesDoub + horasExSimplesDoub + horasEx60Doub + horasEx100Doub).ToString("N", CultureInfo.GetCultureInfo("es-PE"));
        }
        private void ObtenerTotalBeneficios()
        {
            string vacacional = txtVacacionalTrabajador.Text.Trim();
            string gratificacion = txtGratificacionTrabajador.Text.Trim();
            string liquidacion = txtLiquidacionTrabajador.Text.Trim();
            string bonificacionEssalud = txtBonifExtraEssaludTrabajador.Text.Trim();
            string bonificacionPension = txtBonifExtraPensionTrabajador.Text.Trim();

            if (vacacional == string.Empty) { vacacional = "0"; }
            if (gratificacion == string.Empty) { gratificacion = "0"; }
            if (liquidacion == string.Empty) { liquidacion = "0"; }
            if (bonificacionEssalud == string.Empty) { bonificacionEssalud = "0"; }
            if (bonificacionPension == string.Empty) { bonificacionPension = "0"; }
       
            double vacacionalDoub = double.Parse(vacacional, CultureInfo.InvariantCulture);
            double gratificacionDoub = double.Parse(gratificacion, CultureInfo.InvariantCulture);
            double liquidacionDoub = double.Parse(liquidacion, CultureInfo.InvariantCulture);
            double bonificacionEssaludDoub = double.Parse(bonificacionEssalud, CultureInfo.InvariantCulture);
            double bonificacionPensionDoub = double.Parse(bonificacionPension, CultureInfo.InvariantCulture);

            Session["TotalBeneficios"] = (vacacionalDoub + gratificacionDoub + liquidacionDoub + bonificacionEssaludDoub + bonificacionPensionDoub).ToString();
            txtTotalBeneficios.InnerText = "S/. " + (vacacionalDoub + gratificacionDoub + liquidacionDoub + bonificacionEssaludDoub + bonificacionPensionDoub).ToString("N", CultureInfo.GetCultureInfo("es-PE"));
        }
        private void ObtenerTotalDescuentos()
        {
            string aportacionSNP = txtSNPTrabajador.Text.Trim();
            string aportacionAFPAO = txtAporteObligatorioTrabajador.Text.Trim();
            string aportacionAFPCF = txtComisionFlujoTrabajador.Text.Trim();
            string aportacionAFPPS = txtPrimaSeguroTrabajador.Text.Trim();
            string aportacionAFPAC = txtAporteComplementarioTrabajador.Text.Trim();
            string aportacionAFPCM = txtComisionMixtaTrabajador.Text.Trim();
            string conafovicer = txtConafovicerTrabajador.Text.Trim();
            string sindical = txtAporteSindicalTrabajador.Text.Trim();
          
            string renta5taCategoria = txtRenta5taCategoria.Text.Trim();
            string eps = txtEpsTrabajador.Text.Trim();
            string otroDescuentos = txtOtrosDescuentosTrabajador.Text.Trim();

            if (aportacionSNP == string.Empty) { aportacionSNP = "0"; }
            if (aportacionAFPAO == string.Empty) { aportacionAFPAO = "0"; }
            if (aportacionAFPCF == string.Empty) { aportacionAFPCF = "0"; }
            if (aportacionAFPPS == string.Empty) { aportacionAFPPS = "0"; }
            if (aportacionAFPAC == string.Empty) { aportacionAFPAC = "0"; }
            if (aportacionAFPCM == string.Empty) { aportacionAFPCM = "0"; }
            if (conafovicer == string.Empty) { conafovicer = "0"; }
            if (sindical == string.Empty) { sindical = "0"; }
            
            if (renta5taCategoria == string.Empty) { renta5taCategoria = "0"; }
            if (eps == string.Empty) { eps = "0"; }
            if (otroDescuentos == string.Empty) { otroDescuentos = "0"; }

            double aportacionSNPDoub = double.Parse(aportacionSNP, CultureInfo.InvariantCulture);
            double aportacionAFPAODoub = double.Parse(aportacionAFPAO, CultureInfo.InvariantCulture);
            double aportacionAFPCFDoub = double.Parse(aportacionAFPCF, CultureInfo.InvariantCulture);
            double aportacionAFPPSDoub = double.Parse(aportacionAFPPS, CultureInfo.InvariantCulture);
            double aportacionAFPACDoub = double.Parse(aportacionAFPAC, CultureInfo.InvariantCulture);
            double aportacionAFPCMDoub = double.Parse(aportacionAFPCM, CultureInfo.InvariantCulture);
            double conafovicerDoub = double.Parse(conafovicer, CultureInfo.InvariantCulture);
            double sindicalDoub = double.Parse(sindical, CultureInfo.InvariantCulture);
            
            double renta5taCategoriaDoub = double.Parse(renta5taCategoria, CultureInfo.InvariantCulture);
            double epsDoub = double.Parse(eps, CultureInfo.InvariantCulture);
            double otroDescuentosDoub = double.Parse(otroDescuentos, CultureInfo.InvariantCulture);

            Session["TotalDescuentos"] = (aportacionSNPDoub + aportacionAFPAODoub + aportacionAFPCFDoub + aportacionAFPPSDoub + aportacionAFPACDoub + aportacionAFPCMDoub + conafovicerDoub + sindicalDoub + renta5taCategoriaDoub + epsDoub + otroDescuentosDoub).ToString();
            txtTotalDescuentos.InnerText = "S/. " + (aportacionSNPDoub + aportacionAFPAODoub + aportacionAFPCFDoub + aportacionAFPPSDoub + aportacionAFPACDoub + aportacionAFPCMDoub + conafovicerDoub + sindicalDoub + renta5taCategoriaDoub + epsDoub + otroDescuentosDoub).ToString("N", CultureInfo.GetCultureInfo("es-PE"));
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
        private void ObtenerTotalaAportacionesEmpleador()
        {
            string essalud = txtEsSaludTrabajador.Text.Trim();
            string aporteComplementarioAFP = txtAporteComplementarioAFPTrabajador.Text.Trim();
            string sctrSalud = txtSCTRSaludTrabajador.Text.Trim();
            string sctrPension = txtSCTRPensionTrabajador.Text.Trim();
            string essaludVida = txtEssaludVidaTrabajador.Text.Trim();

            if (essalud == string.Empty) { essalud = "0"; }
            if (aporteComplementarioAFP == string.Empty) { aporteComplementarioAFP = "0"; }
            if (sctrSalud == string.Empty) { sctrSalud = "0"; }
            if (sctrPension == string.Empty) { sctrPension = "0"; }
            if (essaludVida == string.Empty) { essaludVida = "0"; }

            double essaludDoub = double.Parse(essalud, CultureInfo.InvariantCulture);
            double aporteComplementarioAFPDoub = double.Parse(aporteComplementarioAFP, CultureInfo.InvariantCulture);
            double sctrSaludDoub = double.Parse(sctrSalud, CultureInfo.InvariantCulture);
            double sctrPensionDoub = double.Parse(sctrPension, CultureInfo.InvariantCulture);
            double essaludVidaDoub = double.Parse(essaludVida, CultureInfo.InvariantCulture);

            Session["TotalAportacionesEmpleador"] = (essaludDoub + aporteComplementarioAFPDoub + sctrSaludDoub + sctrPensionDoub + essaludVidaDoub);
            txtTotalAportacionEmpleador.InnerText = "S/. " + (essaludDoub + aporteComplementarioAFPDoub + sctrSaludDoub + sctrPensionDoub + essaludVidaDoub).ToString("N", CultureInfo.GetCultureInfo("es-PE"));
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
        private void PlanillasRecientes(string fechaInicial, string fechaFinal)
        {
            DataTable dt = procesoPlanilla.ObtenerPlanillasReciente(fechaInicial, fechaFinal);
            dgvPlanillaRecientes.DataSource = dt;
            dgvPlanillaRecientes.DataBind();

            lblResultadoBusqueda.InnerHtml = "" + dt.Rows.Count.ToString() + " registros(s) reciente(s) encontrado(s)";
        }
        private void LimpiarFomulario()
        {
            cboTrabajadores.ClearSelection();
            cboProyectoPlanilla.ClearSelection();
            txtFechaInicial.Text = DateTime.Now.ToShortDateString();
            txtFechaFinal.Text = DateTime.Now.ToShortDateString();
            txtFechaInicialReporte.Text = DateTime.Now.ToShortDateString();
            txtFechaFinalReporte.Text = DateTime.Now.ToShortDateString();
            cboMesPeriodo.ClearSelection();
            txtFechaAnoPeriodoSelect.Text = string.Empty;
            txtDiasLaborados.Text = string.Empty;
            txtDiasDomingosFeriados.Text = string.Empty;
            txtAsignacionFamiliar.Text = string.Empty;
            txtReintegros.Text = string.Empty;
            txtBonificacion.Text = string.Empty;
            txtHorasExtrasSimple.Text = string.Empty;
            txtHorasExtras60.Text = string.Empty;
            txtHorasExtras100.Text = string.Empty;
            txtBucTrabajador.Text = string.Empty;
            txtPasajesTrabajador.Text = string.Empty;
            txtVacacionalTrabajador.Text = string.Empty;
            txtGratificacionTrabajador.Text = string.Empty;
            txtLiquidacionTrabajador.Text = string.Empty;
            txtBonifExtraEssaludTrabajador.Text = string.Empty;
            txtBonifExtraPensionTrabajador.Text = string.Empty;
            txtSNPTrabajador.Text = string.Empty;
            txtAporteObligatorioTrabajador.Text = string.Empty;
            txtConafovicerTrabajador.Text = string.Empty;
            txtComisionFlujoTrabajador.Text = string.Empty;
            txtComisionMixtaTrabajador.Text = string.Empty;
            txtPrimaSeguroTrabajador.Text = string.Empty;
            txtAporteComplementarioTrabajador.Text = string.Empty;
            txtAporteSindicalTrabajador.Text = string.Empty;
            txtEssaludVidaTrabajador.Text = string.Empty;
            txtRenta5taCategoria.Text = string.Empty;
            txtEpsTrabajador.Text = string.Empty;
            txtOtrosDescuentosTrabajador.Text = string.Empty;
            txtEsSaludTrabajador.Text = string.Empty;
            txtAporteComplementarioAFPTrabajador.Text = string.Empty;
            txtSCTRSaludTrabajador.Text = string.Empty;
            txtSCTRPensionTrabajador.Text = string.Empty;
            txtCargoTrabajador.Text = string.Empty;
            txtHaberMensualTrabajador.Text = string.Empty;
            txtJornalTrabajador.Text = string.Empty;
            txtTotalCalculadoDiasLaborados.Text = string.Empty;
            txtCalculoDiasDomingosFeriados.Text = string.Empty;
            txtTotalIngresos.InnerText = "0.00";
            txtTotalBeneficios.InnerText = "0.00";
            txtTotalDescuentos.InnerText = "0.00";
            txtTotalAportacionEmpleador.InnerText = "0.00";
            txtTotalPagar.InnerText = "0.00";
            txtCostoTotalTrabajador.InnerText = "0.00";

            chkBonificacionExtra.Checked = false;
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

            DataSetPlanilla.Tables.Add(procesoReporte.ObtenerReporteBoletaPagoIndividual(Convert.ToInt32(codPlanilla)));

            ReportDataSource datosSolicitante = new ReportDataSource("PlanillaBoletaPagoDataSet", DataSetPlanilla.Tables[0]);


            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(datosSolicitante);
            ReportViewer1.LocalReport.Refresh();
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

       
    }
}