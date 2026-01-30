<%@ Page  uiCulture="es" culture="es-PE" Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="GenerarPlanillaEmpleados.aspx.cs" Inherits="SistemaGestionPlanilla.Planilla.GenerarPlanillaEmpleados" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript"> 
        function pageLoad() {
            $(function () {
                $('.m-select2-general').select2({
                    placeholder: "Seleccione una opcion"
                });

                CargarDateRange();
                CargarDateRangeReporte();
                ReloadJqueryDatePicker();

                $('#<%=txtFechaAnoPeriodo.ClientID%>').val($('#<%=txtFechaAnoPeriodoSelect.ClientID%>').val());

                $(".param").on("keypress keyup blur", function (event) {
                    $(this).val($(this).val().replace(/[^0-9\.]/g, ''));
                    if ((event.which != 46 || $(this).val().indexOf('.') != -1) && (event.which < 48 || event.which > 57)) {
                        event.preventDefault();
                    }
                });
            });
        }
    </script>
    <div class="m-grid__item m-grid__item--fluid m-wrapper">
        <asp:UpdatePanel runat="server" ID="UpdatePanelPrincipal" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="m-content">
                    <div class="m-alert m-alert--icon m-alert--air m-alert--square alert alert-dismissible m--margin-bottom-30" role="alert">
                        <div class="m-alert__icon">
                            <i class="flaticon-exclamation m--font-brand"></i>
                        </div>
                        <div class="m-alert__text">
                            Genere su boleta digital antes de guardar la planilla del trabajador para tener una idea del monto a pagar.
                        </div>
                    </div>
                    <div class="m-portlet m-portlet--mobile" id="gpFormularioGenerarPlanilla" runat="server">
                        <div class="m-portlet__head">
                            <div class="m-portlet__head-caption">
                                <div class="m-portlet__head-title">
                                    <h3 class="m-portlet__head-text">Generar Planilla Empleados
                                    </h3>
                                </div>
                            </div>

                        </div>
                        <div id="gpFormularioPerfilPlanilla" runat="server">
                            <div class="m-form m-form--fit m-form--label-align-right m-form--group-seperator-dashed">
                                <div id="gpFormularioCalculoPlanilla" runat="server">
                                    <div class="m-portlet__body">
                                        <div id="gpFormularioPeriodo" runat="server">
                                            <div class="form-group m-form__group row" style="padding-bottom: 0; border-bottom: none">
                                                <div class="col-lg-6">
                                                    <label>Periodo:</label>
                                                    <div class="m-input-icon m-input-icon--right">
                                                        <asp:TextBox runat="server" CssClass="form-control m-input fechaano" ReadOnly="true" placeholder="Seleccione" ID="txtFechaAnoPeriodo" onchange="SelectFecha()" />
                                                        <span class="m-input-icon__icon m-input-icon__icon--right">
                                                            <span>
                                                                <i class="la la-calendar"></i>
                                                            </span>
                                                        </span>
                                                        <asp:TextBox runat="server" Style="display: none" ID="txtFechaAnoPeriodoSelect"></asp:TextBox>
                                                    </div>
                                                    <span class="m-form__help">Seleccionar año del periodo.</span>
                                                </div>
                                                <div class="col-lg-6">
                                                    <label>&nbsp;</label>
                                                    <div class="m-input-icon m-input-icon--right">
                                                        <asp:DropDownList runat="server" CssClass="form-control m-select2 m-select2-general" ID="cboMesPeriodo" OnSelectedIndexChanged="cboMesPeriodo_SelectedIndexChanged" AutoPostBack="true">
                                                            <asp:ListItem Value="0" Text="Seleccione"></asp:ListItem>
                                                            <asp:ListItem Value="1" Text="Enero"></asp:ListItem>
                                                            <asp:ListItem Value="2" Text="Febrero"></asp:ListItem>
                                                            <asp:ListItem Value="3" Text="Marzo"></asp:ListItem>
                                                            <asp:ListItem Value="4" Text="Abril"></asp:ListItem>
                                                            <asp:ListItem Value="5" Text="Mayo"></asp:ListItem>
                                                            <asp:ListItem Value="6" Text="Junio"></asp:ListItem>
                                                            <asp:ListItem Value="7" Text="Julio"></asp:ListItem>
                                                            <asp:ListItem Value="8" Text="Agosto"></asp:ListItem>
                                                            <asp:ListItem Value="9" Text="Setiembre"></asp:ListItem>
                                                            <asp:ListItem Value="10" Text="Octubre"></asp:ListItem>
                                                            <asp:ListItem Value="11" Text="Noviembre"></asp:ListItem>
                                                            <asp:ListItem Value="12" Text="Diciembre"></asp:ListItem>
                                                        </asp:DropDownList>
                                                        <span class="m-input-icon__icon m-input-icon__icon--right">
                                                            <span>
                                                                <i class="la la-calendar"></i>
                                                            </span>
                                                        </span>
                                                    </div>
                                                    <span class="m-form__help">Seleccionar mes del periodo.</span>
                                                </div>
                                            </div>
                                            <div class="form-group m-form__group row" style="border-bottom: 1px dashed #ebedf2;">
                                                <div class="col-lg-6">
                                                    <label>Semana a calcular</label>
                                                    <div class="m-input-icon m-input-icon--right">
                                                        <asp:TextBox runat="server" CssClass="form-control m-input rangofechaplanilla" ReadOnly="true" placeholder="Seleccione" ID="txtFechaHastaPlanilla" />
                                                        <span class="m-input-icon__icon m-input-icon__icon--right">
                                                            <span>
                                                                <i class="la la-calendar"></i>
                                                            </span>
                                                        </span>
                                                        <asp:TextBox runat="server" Style="display: none" ID="txtFechaInicial" CssClass="fechainicial"></asp:TextBox>
                                                        <asp:TextBox runat="server" Style="display: none" ID="txtFechaFinal" CssClass="fechafinal"></asp:TextBox>
                                                    </div>
                                                    <span class="m-form__help">Seleccionar rango de fechas</span>
                                                </div>
                                            </div>
                                        </div>
                                        <div id="gpFormularioTrabajador" runat="server">
                                            <div class="form-group m-form__group row" style="padding-bottom: 0; border-bottom: none">
                                                <div class="col-lg-6">
                                                    <label>Trabajador</label>
                                                    <div class="m-input-icon m-input-icon--right">
                                                        <asp:DropDownList runat="server" CssClass="form-control m-select2 m-select2-general" ID="cboTrabajadores" AutoPostBack="true" OnSelectedIndexChanged="cboTrabajadores_SelectedIndexChanged">
                                                            <asp:ListItem Text="Seleccione" Value="Seleccione"></asp:ListItem>
                                                        </asp:DropDownList>
                                                        <span class="m-input-icon__icon m-input-icon__icon--right">
                                                            <span>
                                                                <i class="la la-user-plus"></i>
                                                            </span>
                                                        </span>
                                                    </div>
                                                    <span class="m-form__help">Seleccionar el trabajador a generar planilla.</span>
                                                </div>
                                                <div class="col-lg-6">
                                                    <label class="">&nbsp;</label>
                                                    <div class="m-input-icon m-input-icon--right">
                                                        <asp:TextBox runat="server" CssClass="form-control m-input" placeholder="Cargo del trabajador" ReadOnly="true" ID="txtCargoTrabajador"></asp:TextBox>
                                                        <span class="m-input-icon__icon m-input-icon__icon--right">
                                                            <span>
                                                                <i class="la la-wrench"></i>
                                                            </span>
                                                        </span>
                                                    </div>
                                                    <span class="m-form__help">Cargo del trabajador</span>
                                                </div>
                                            </div>
                                            <div class="form-group m-form__group row" style="border-bottom: 1px dashed #ebedf2;">
                                                <div class="col-lg-6">
                                                    <div class="m-input-icon m-input-icon--right">
                                                        <asp:TextBox runat="server" CssClass="form-control m-input" placeholder="Haber Mensual" ReadOnly="true" ID="txtHaberMensualTrabajador"></asp:TextBox>
                                                        <span class="m-input-icon__icon m-input-icon__icon--right">
                                                            <span>
                                                                <i class="la la-money"></i>
                                                            </span>
                                                        </span>
                                                    </div>
                                                    <span class="m-form__help">Haber mensual del trabajador</span>
                                                </div>
                                                <div class="col-lg-6">
                                                    <div class="m-input-icon m-input-icon--right">
                                                        <asp:TextBox runat="server" CssClass="form-control m-input" placeholder="Jornal del trabajador" ReadOnly="true" ID="txtJornalTrabajador"></asp:TextBox>
                                                        <span class="m-input-icon__icon m-input-icon__icon--right">
                                                            <span>
                                                                <i class="la la-dollar"></i>
                                                            </span>
                                                        </span>
                                                    </div>
                                                    <span class="m-form__help">Haber diario</span>
                                                </div>
                                            </div>
                                        </div>
                                        <div id="gpFormularioParametros" runat="server">
                                            <div class="form-group m-form__group row">
                                                <div class="col-lg-6">
                                                    <label class="">Dias laborados</label>
                                                    <div class="row">
                                                        <div class="col-lg-6">
                                                            <div class="m-input-icon">
                                                                <asp:TextBox runat="server" ID="txtDiasLaborados" Style="text-align: center" CssClass="form-control m_touchspin_1" value="0" AutoPostBack="true" OnTextChanged="txtDiasLaborados_TextChanged"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-6">
                                                            <div class="m-input-icon m-input-icon--right">
                                                                <asp:TextBox runat="server" CssClass="form-control m-input" placeholder="Total calculado" ID="txtTotalCalculadoDiasLaborados" ReadOnly="true" Text="0.00"></asp:TextBox>
                                                                <span class="m-input-icon__icon m-input-icon__icon--right">
                                                                    <span>
                                                                        <i class="la la-truck"></i>
                                                                    </span>
                                                                </span>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <span class="m-form__help">Dias laborados para el calculo de planilla</span>
                                                </div>
                                                <div class="col-lg-6">
                                                    <label class="">Domingos y feriados</label>
                                                    <div class="row">
                                                        <div class="col-lg-6">
                                                            <div class="m-input-icon">
                                                                <asp:TextBox runat="server" ID="txtDiasDomingosFeriados" Style="text-align: center" CssClass="form-control m_touchspin_1" value="0" AutoPostBack="true" OnTextChanged="txtDiasDomingosFeriados_TextChanged"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-6">
                                                            <div class="m-input-icon m-input-icon--right">
                                                                <asp:TextBox runat="server" CssClass="form-control m-input" placeholder="Domingos y Feriados" ID="txtCalculoDiasDomingosFeriados" ReadOnly="true" Text="0.00"></asp:TextBox>
                                                                <span class="m-input-icon__icon m-input-icon__icon--right">
                                                                    <span>
                                                                        <i class="la la-clipboard"></i>
                                                                    </span>
                                                                </span>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <span class="m-form__help">Domingos y feriados laborados para el calculo de planilla</span>
                                                </div>
                                            </div>
                                            <div class="form-group m-form__group row">
                                                <div class="col-lg-4">
                                                    <label class="">INGRESOS</label>
                                                    <div class="col-lg-12 nopadding-sides">
                                                        <label class="">Asignaciones</label>
                                                        <div class="m-input-icon m-input-icon--right">
                                                            <asp:TextBox runat="server" CssClass="form-control m-input param" placeholder="0.00" ID="txtAsignacionFamiliar" AutoPostBack="true" OnTextChanged="txtAsignacionFamiliar_TextChanged"></asp:TextBox>
                                                            <span class="m-input-icon__icon m-input-icon__icon--right">
                                                                <span>
                                                                    <i class="la la-child"></i>
                                                                </span>
                                                            </span>
                                                        </div>
                                                        <span class="m-form__help">Asignacion familiar</span>
                                                    </div>

                                                </div>
                                                <div class="col-lg-4">
                                                    <label class="">&nbsp;</label>
                                                    <div class="col-lg-12 nopadding-sides">
                                                        <label class="">Horas Extras</label>
                                                        <div class="m-input-icon m-input-icon--right input-group">
                                                            <div class="input-group-prepend">
                                                                <span class="input-group-text">
                                                                    <asp:Label runat="server" ID="txtCalculoHorasSimple">0.00</asp:Label>
                                                                </span>
                                                            </div>
                                                            <asp:TextBox runat="server" CssClass="form-control m-input param" placeholder="0.00" ID="txtHorasExtrasSimple" AutoPostBack="true" OnTextChanged="txtHorasExtrasSimple_TextChanged"></asp:TextBox>
                                                            <span class="m-input-icon__icon m-input-icon__icon--right">
                                                                <span>
                                                                    <i class="la la-clock-o"></i>
                                                                </span>
                                                            </span>
                                                        </div>
                                                        <span class="m-form__help">Horas Extras Simple</span>
                                                    </div>

                                                </div>
                                                <div class="col-lg-4">
                                                    <label class="">&nbsp;</label>

                                                    <div class="col-lg-12 nopadding-sides">
                                                        <label class="">Otros</label>
                                                        <div class="m-input-icon m-input-icon--right input-group">
                                                            <asp:TextBox runat="server" CssClass="form-control m-input" placeholder="0.00" ID="txtPasajesTrabajador" AutoPostBack="true" OnTextChanged="txtPasajesTrabajador_TextChanged"></asp:TextBox>
                                                            <span class="m-input-icon__icon m-input-icon__icon--right">
                                                                <span>
                                                                    <i class="la la-bus"></i>
                                                                </span>
                                                            </span>
                                                        </div>
                                                        <span class="m-form__help">Movilidad</span>
                                                    </div>
                                                    <div class="col-lg-12 nopadding-sides">
                                                        <label class="">&nbsp;</label>
                                                        <div class="m-input-icon m-input-icon--right">
                                                            <asp:TextBox runat="server" CssClass="form-control m-input param" placeholder="0.00" ID="txtOtrosIngresos" AutoPostBack="true" OnTextChanged="txtOtrosIngresos_TextChanged"></asp:TextBox>
                                                            <span class="m-input-icon__icon m-input-icon__icon--right">
                                                                <span>
                                                                    <i class="la la-cart-plus"></i>
                                                                </span>
                                                            </span>
                                                        </div>
                                                        <span class="m-form__help">Otros ingresos</span>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="m-invoice-1">
                                                <div class="m-invoice__wrapper">

                                                    <div class="m-invoice__footer" style="margin-top: 0px">
                                                        <div class="m-invoice__container m-invoice__container--centered" style="padding: 20px 0 20px 0;">
                                                            <div class="m-invoice__content">
                                                                <span>TOTAL INGRESOS</span>
                                                                <span class="m-invoice__price" style="font-size: 1.5rem;" runat="server" id="txtTotalIngresos">S/. 00.00</span>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="form-group m-form__group row">
                                                <div class="col-lg-4">
                                                    <label class="">BENEFICIOS</label>

                                                    <div class="col-lg-12 nopadding-sides">
                                                        <label class="">Beneficios para el trabajador</label>
                                                        <div class="m-input-icon m-input-icon--right input-group">
                                                            <div class="input-group-prepend">
                                                                <span class="input-group-text">
                                                                    <asp:Label runat="server" ID="txtCalculoVacacional">0.00</asp:Label>
                                                                </span>
                                                            </div>
                                                            <asp:TextBox runat="server" CssClass="form-control m-input" placeholder="0.00" ID="txtVacacionalTrabajador" AutoPostBack="true" ReadOnly="true" Enabled="false"></asp:TextBox>
                                                            <span class="m-input-icon__icon m-input-icon__icon--right">
                                                                <span>
                                                                    <i class="la la-briefcase"></i>
                                                                </span>
                                                            </span>
                                                        </div>
                                                        <span class="m-form__help">Remuneracion vacacional</span>
                                                    </div>
                                                    <div class="col-lg-12 nopadding-sides">
                                                        <label class="">&nbsp;</label>
                                                        <div class="m-input-icon m-input-icon--right input-group">
                                                            <div class="input-group-prepend">
                                                                <span class="input-group-text">
                                                                    <asp:Label runat="server" ID="txtCalculoGratificacion">0.00</asp:Label>
                                                                </span>
                                                            </div>
                                                            <asp:TextBox runat="server" CssClass="form-control m-input" placeholder="0.00" ID="txtGratificacionTrabajador" AutoPostBack="true" ReadOnly="true" Enabled="false"></asp:TextBox>
                                                            <span class="m-input-icon__icon m-input-icon__icon--right">
                                                                <span>
                                                                    <i class="la la-money"></i>
                                                                </span>
                                                            </span>
                                                        </div>
                                                        <span class="m-form__help">Gratificacion</span>
                                                    </div>

                                                </div>
                                                <div class="col-lg-4">
                                                    <label class="">&nbsp;</label>
                                                    <div class="col-lg-12 nopadding-sides">
                                                        <label class="">Bonificacion Extra</label>
                                                        <div class="m-input-icon m-input-icon--right input-group">
                                                            <div class="input-group-prepend">
                                                                <span class="input-group-text">
                                                                    <asp:Label runat="server" ID="txtCalculoBonifExtraEssalud">0.00</asp:Label>
                                                                </span>
                                                            </div>
                                                            <asp:TextBox runat="server" CssClass="form-control m-input" placeholder="0.00" ID="txtBonifExtraEssaludTrabajador" ReadOnly="true" Enabled="false"></asp:TextBox>
                                                            <span class="m-input-icon__icon m-input-icon__icon--right">
                                                                <span>
                                                                    <i class="la la-money"></i>
                                                                </span>
                                                            </span>
                                                        </div>
                                                        <span class="m-form__help">Bonificacion Extra EsSalud</span>
                                                    </div>
                                                    <div class="col-lg-12 nopadding-sides">
                                                        <label class="">&nbsp;</label>
                                                        <div class="m-input-icon m-input-icon--right input-group">
                                                            <div class="input-group-prepend">
                                                                <span class="input-group-text">
                                                                    <asp:Label runat="server" ID="txtCalculoAdicionalGraticacion">0.00</asp:Label>
                                                                </span>
                                                            </div>
                                                            <asp:TextBox runat="server" CssClass="form-control m-input" placeholder="0.00" ID="txtAdicionalGratificacionTrabajador" ReadOnly="true" Enabled="false"> </asp:TextBox>
                                                            <span class="m-input-icon__icon m-input-icon__icon--right">
                                                                <span>
                                                                    <i class="la la-money"></i>
                                                                </span>
                                                            </span>
                                                        </div>
                                                        <span class="m-form__help">Art. 3 Ley 29351/29714</span>
                                                    </div>
                                               <%--     <div class="col-lg-12 nopadding-sides">
                                                        <label class="">&nbsp;</label>
                                                        <div class="m-input-icon m-input-icon--right input-group">
                                                            <asp:TextBox runat="server" CssClass="form-control m-input" placeholder="0.00" ID="txtVacacionesTruncas" AutoPostBack="true" OnTextChanged="txtVacacionesTruncas_TextChanged"></asp:TextBox>
                                                            <span class="m-input-icon__icon m-input-icon__icon--right">
                                                                <span>
                                                                    <i class="la la-briefcase"></i>
                                                                </span>
                                                            </span>
                                                        </div>
                                                        <span class="m-form__help">Vacaciones truncas</span>
                                                    </div>  --%>
                                                    <div class="col-lg-12 nopadding-sides">
                                                        <label class="">&nbsp;</label>
                                                        <div class="m-input-icon m-input-icon--right input-group">
                                                            <asp:TextBox runat="server" CssClass="form-control m-input" placeholder="0.00" ID="txtOtrosBeneficios" AutoPostBack="true" OnTextChanged="txtOtrosBeneficios_TextChanged"></asp:TextBox>
                                                            <span class="m-input-icon__icon m-input-icon__icon--right">
                                                                <span>
                                                                    <i class="la la-certificate"></i>
                                                                </span>
                                                            </span>
                                                        </div>
                                                        <span class="m-form__help">Otros beneficios</span>
                                                    </div>
                                                </div>
                                                <div class="col-lg-4">
                                                    <label class="">&nbsp;</label>
                                                    
                                                    <div class="col-lg-12 nopadding-sides">
                                                        <label class="">&nbsp;</label>
                                                        <div class="m-input-icon m-input-icon--right input-group">
                                                            <label class="m-checkbox m-checkbox--solid m-checkbox--state-success">
                                                                <input type="checkbox" runat="server" id="chkBonificacionExtra" onchange="ActivarCheckBox()">
                                                                Sin descuentos Ley 30334
											                <span></span>
                                                            </label>
                                                        </div>
                                                        <span class="m-form__help">Seleccione si desea agregarlos al calculo de planilla.</span>
                                                    </div>
                                                     <div class="col-lg-12 nopadding-sides">
                                                        <label class="">&nbsp;</label>
                                                        <div class="m-input-icon m-input-icon--right input-group">
                                                            <label class="m-checkbox m-checkbox--solid m-checkbox--state-success">
                                                                <input type="checkbox" runat="server" id="chkVacaciones" onchange="ActivarVacaciones()">
                                                                Vacaciones
											                <span></span>
                                                            </label>
                                                        </div>
                                                        <span class="m-form__help">Seleccione si desea agregarlos al calculo de planilla.</span>
                                                    </div>
                                                     <div class="col-lg-12 nopadding-sides">
                                                        <label class="">&nbsp;</label>
                                                        <div class="m-input-icon m-input-icon--right input-group">
                                                            <label class="m-checkbox m-checkbox--solid m-checkbox--state-success">
                                                                <input type="checkbox" runat="server" id="chkGratificacion" onchange="ActivarGratificacion()">
                                                                Gratificación trunca
											                <span></span>
                                                            </label>
                                                        </div>
                                                        <span class="m-form__help">Seleccione si desea agregarlos al calculo de planilla.</span>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="m-invoice-1">
                                                <div class="m-invoice__wrapper">

                                                    <div class="m-invoice__footer" style="margin-top: 0px">
                                                        <div class="m-invoice__container m-invoice__container--centered" style="padding: 20px 0 20px 0;">
                                                            <div class="m-invoice__content">
                                                                <span>TOTAL BENEFICIOS</span>
                                                                <span class="m-invoice__price" style="font-size: 1.5rem;" runat="server" id="txtTotalBeneficios">S/. 00.00</span>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="form-group m-form__group row">
                                                <div class="col-lg-4">
                                                    <label class="">DESCUENTOS</label>
                                                    <div class="col-lg-12 nopadding-sides">
                                                        <label class="">Aportaciones del trabajador y descuentos</label>
                                                        <div class="m-input-icon m-input-icon--right input-group">
                                                            <div class="input-group-prepend">
                                                                <span class="input-group-text">
                                                                    <asp:Label runat="server" ID="txtCalculoSNP">0.00</asp:Label>
                                                                </span>
                                                            </div>
                                                            <asp:TextBox runat="server" CssClass="form-control m-input" placeholder="0.00" ID="txtSNPTrabajador" AutoPostBack="true" ReadOnly="true"></asp:TextBox>
                                                            <span class="m-input-icon__icon m-input-icon__icon--right">
                                                                <span>
                                                                    <i class="la la-bank"></i>
                                                                </span>
                                                            </span>
                                                        </div>
                                                        <span class="m-form__help">SNP</span>
                                                    </div>
                                                    <div class="col-lg-12 nopadding-sides">
                                                        <label class="">&nbsp;</label>
                                                        <div class="m-input-icon m-input-icon--right input-group">
                                                            <div class="input-group-prepend">
                                                                <span class="input-group-text">
                                                                    <asp:Label runat="server" ID="txtCalculoAporteObligatorio">0.00</asp:Label>
                                                                </span>
                                                            </div>
                                                            <asp:TextBox runat="server" CssClass="form-control m-input" placeholder="0.00" ID="txtAporteObligatorioTrabajador" AutoPostBack="true" ReadOnly="true"></asp:TextBox>
                                                            <span class="m-input-icon__icon m-input-icon__icon--right">
                                                                <span>
                                                                    <i class="la la-bank"></i>
                                                                </span>
                                                            </span>
                                                        </div>
                                                        <span class="m-form__help">Aporte Obligatorio</span>
                                                    </div>

                                                    <div class="col-lg-12 nopadding-sides">
                                                        <label class="">&nbsp;</label>
                                                        <div class="m-input-icon m-input-icon--right input-group">
                                                            <div class="input-group-prepend">
                                                                <span class="input-group-text">
                                                                    <label class="m-checkbox m-checkbox--single m-checkbox--state m-checkbox--state-success" style="margin-top: -5px;">
                                                                        <input type="checkbox" id="chkComisionFlujo" runat="server" onchange="ActivarComision()">
                                                                        <span></span>
                                                                    </label>
                                                                </span>
                                                                <span class="input-group-text">
                                                                    <asp:Label runat="server" ID="txtCalculoComisionFlujoTrabajador">0.00</asp:Label>
                                                                </span>
                                                            </div>
                                                            <asp:TextBox runat="server" CssClass="form-control m-input" placeholder="0.00" ID="txtComisionFlujoTrabajador" AutoPostBack="true" ReadOnly="true"></asp:TextBox>
                                                            <span class="m-input-icon__icon m-input-icon__icon--right">
                                                                <span>
                                                                    <i class="la la-bank"></i>
                                                                </span>
                                                            </span>

                                                        </div>

                                                        <span class="m-form__help">Comisión por Flujo</span>
                                                    </div>
                                                    <div class="col-lg-12 nopadding-sides">
                                                        <label class="">&nbsp;</label>
                                                        <div class="m-input-icon m-input-icon--right input-group">
                                                            <div class="input-group-prepend">
                                                                <span class="input-group-text">
                                                                    <label class="m-checkbox m-checkbox--single m-checkbox--state m-checkbox--state-success" style="margin-top: -5px;">
                                                                        <input type="checkbox" id="chkComisionMixta" runat="server" onchange="ActivarComision()">
                                                                        <span></span>
                                                                    </label>
                                                                </span>
                                                                <span class="input-group-text">
                                                                    <asp:Label runat="server" ID="txtCalculoComisionMixtaTrabajador">0.00</asp:Label>
                                                                </span>
                                                            </div>
                                                            <asp:TextBox runat="server" CssClass="form-control m-input" placeholder="0.00" ID="txtComisionMixtaTrabajador" AutoPostBack="true" ReadOnly="true"></asp:TextBox>
                                                            <span class="m-input-icon__icon m-input-icon__icon--right">
                                                                <span>
                                                                    <i class="la la-bank"></i>
                                                                </span>
                                                            </span>
                                                        </div>
                                                        <span class="m-form__help">Comisión Mixta</span>
                                                    </div>
                                                </div>
                                                <div class="col-lg-4">
                                                    <label class="">&nbsp;</label>                          
                                                    <div class="col-lg-12 nopadding-sides">
                                                        <label class="">&nbsp;</label>
                                                        <div class="m-input-icon m-input-icon--right input-group">
                                                            <div class="input-group-prepend">
                                                                <span class="input-group-text">
                                                                    <asp:Label runat="server" ID="txtCalculoPrimaSeguroTrabajador">0.00</asp:Label>
                                                                </span>
                                                            </div>
                                                            <asp:TextBox runat="server" CssClass="form-control m-input" placeholder="0.00" ID="txtPrimaSeguroTrabajador" AutoPostBack="true" ReadOnly="true"></asp:TextBox>
                                                            <span class="m-input-icon__icon m-input-icon__icon--right">
                                                                <span>
                                                                    <i class="la la-bank"></i>
                                                                </span>
                                                            </span>
                                                        </div>
                                                        <span class="m-form__help">Prima Seguro</span>
                                                    </div>                                               
                                                </div>
                                                <div class="col-lg-4">
                                                    <label class="">&nbsp;</label>
                                                    <div class="col-lg-12 nopadding-sides">
                                                        <label class="">&nbsp;</label>
                                                        <div class="m-input-icon m-input-icon--right">

                                                            <asp:TextBox runat="server" CssClass="form-control m-input param" placeholder="0.00" ID="txtRenta5taCategoria" ReadOnly="false" OnTextChanged="txtRenta5taCategoria_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                            <span class="m-input-icon__icon m-input-icon__icon--right">
                                                                <span>
                                                                    <i class="la la-gavel"></i>
                                                                </span>
                                                            </span>
                                                        </div>
                                                        <span class="m-form__help">Renta 5ta Categoria</span>
                                                    </div>
                                                    <div class="col-lg-12 nopadding-sides">
                                                        <label class="">&nbsp;</label>
                                                        <div class="m-input-icon m-input-icon--right">

                                                            <asp:TextBox runat="server" CssClass="form-control m-input param" placeholder="0.00" ID="txtEpsTrabajador" ReadOnly="false" OnTextChanged="txtEpsTrabajador_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                            <span class="m-input-icon__icon m-input-icon__icon--right">
                                                                <span>
                                                                    <i class="la la-ambulance"></i>
                                                                </span>
                                                            </span>
                                                        </div>
                                                        <span class="m-form__help">EPS</span>
                                                    </div>
                                                    <div class="col-lg-12 nopadding-sides">
                                                        <label class="">&nbsp;</label>
                                                        <div class="m-input-icon m-input-icon--right">
                                                            <asp:TextBox runat="server" CssClass="form-control m-input param" placeholder="0.00" ID="txtOtrosDescuentosTrabajador" ReadOnly="false" OnTextChanged="txtOtrosDescuentosTrabajador_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                            <span class="m-input-icon__icon m-input-icon__icon--right">
                                                                <span>
                                                                    <i class="la la-bolt"></i>
                                                                </span>
                                                            </span>
                                                        </div>
                                                        <span class="m-form__help">Otros descuentos</span>
                                                    </div>

                                                </div>
                                            </div>
                                            <div class="m-invoice-1">
                                                <div class="m-invoice__wrapper">

                                                    <div class="m-invoice__footer" style="margin-top: 0px">
                                                        <div class="m-invoice__container m-invoice__container--centered" style="padding: 20px 0 20px 0;">
                                                            <div class="m-invoice__content">
                                                                <span>TOTAL DESCUENTOS</span>
                                                                <span class="m-invoice__price" style="font-size: 1.5rem;" runat="server" id="txtTotalDescuentos">S/. 00.00</span>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="m-invoice-1 borderdashed">
                                                <div class="m-invoice__wrapper">

                                                    <div class="m-invoice__footer" style="margin-top: 0px; background-color: #fff">
                                                        <div class="m-invoice__container m-invoice__container--centered" style="padding: 30px 0 30px 0;">
                                                            <div class="m-invoice__content" style="text-align: right; padding-top: 20px;">
                                                                <h4 style="float: left; position: absolute">Total a Pagar Trabajador</h4>
                                                                <span class="m-invoice__price" style="font-size: 2.2rem; color: #333;" runat="server" id="txtTotalPagar">S/. 00.00</span>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group m-form__group row">
                                                <div class="col-lg-4">
                                                    <label class="">APORTACIONES DEL EMPLEADOR</label>
                                                    <div class="col-lg-12 nopadding-sides">
                                                        <label class="">EsSalud</label>
                                                        <div class="m-input-icon m-input-icon--right input-group">
                                                            <div class="input-group-prepend">
                                                                <span class="input-group-text">
                                                                    <asp:Label runat="server" ID="txtCalculoEsSaludTrabajador">0.00</asp:Label>
                                                                </span>
                                                            </div>
                                                            <asp:TextBox runat="server" CssClass="form-control m-input" placeholder="0.00" ID="txtEsSaludTrabajador" AutoPostBack="true" ReadOnly="true"></asp:TextBox>
                                                            <span class="m-input-icon__icon m-input-icon__icon--right">
                                                                <span>
                                                                    <i class="la la-hospital-o"></i>
                                                                </span>
                                                            </span>
                                                        </div>
                                                        <span class="m-form__help">EsSalud</span>
                                                    </div>
                                                </div>
                                                <div class="col-lg-4">
                                                    <label class="">&nbsp;</label>
                                                    <div class="col-lg-12 nopadding-sides">
                                                        <label class="">SCTR</label>
                                                        <div class="m-input-icon m-input-icon--right input-group">
                                                            <div class="input-group-prepend">
                                                                <span class="input-group-text">
                                                                    <asp:Label runat="server" ID="txtCalculoSCTRSaludTrabajador">0.00</asp:Label>
                                                                </span>
                                                            </div>
                                                            <asp:TextBox runat="server" CssClass="form-control m-input" placeholder="0.00" ID="txtSCTRSaludTrabajador" AutoPostBack="true" ReadOnly="true"></asp:TextBox>
                                                            <span class="m-input-icon__icon m-input-icon__icon--right">
                                                                <span>
                                                                    <i class="la la-heartbeat"></i>
                                                                </span>
                                                            </span>
                                                        </div>
                                                        <span class="m-form__help">SCTR Salud</span>
                                                    </div>
                                                    <div class="col-lg-12 nopadding-sides">
                                                        <label class="">&nbsp;</label>
                                                        <div class="m-input-icon m-input-icon--right input-group">
                                                            <div class="input-group-prepend">
                                                                <span class="input-group-text">
                                                                    <asp:Label runat="server" ID="txtCalculoSCTRPensionTrabajador">0.00</asp:Label>
                                                                </span>
                                                            </div>
                                                            <asp:TextBox runat="server" CssClass="form-control m-input" placeholder="0.00" ID="txtSCTRPensionTrabajador" AutoPostBack="true" ReadOnly="true"></asp:TextBox>
                                                            <span class="m-input-icon__icon m-input-icon__icon--right">
                                                                <span>
                                                                    <i class="la la-home"></i>
                                                                </span>
                                                            </span>
                                                        </div>
                                                        <span class="m-form__help">SCTR Pensión</span>
                                                    </div>

                                                </div>
                                                <div class="col-lg-4">
                                                    <label class="">&nbsp;</label>
                                                    <div class="col-lg-12 nopadding-sides">
                                                        <label class="">&nbsp;</label>
                                                        <div class="m-input-icon m-input-icon--right input-group">
                                                            <div class="input-group-prepend">
                                                                <span class="input-group-text">
                                                                    <asp:Label runat="server" ID="txtCalculoEssaludVidaTrabajador">0.00</asp:Label>
                                                                </span>
                                                            </div>
                                                            <asp:TextBox runat="server" CssClass="form-control m-input" placeholder="0.00" ID="txtEssaludVidaTrabajador" ReadOnly="true"></asp:TextBox>
                                                            <span class="m-input-icon__icon m-input-icon__icon--right">
                                                                <span>
                                                                    <i class="la la-hospital-o"></i>
                                                                </span>
                                                            </span>
                                                        </div>
                                                        <span class="m-form__help">EsSalud Vida</span>
                                                    </div>
                                                </div>

                                            </div>
                                            <div class="m-invoice-1">
                                                <div class="m-invoice__wrapper">

                                                    <div class="m-invoice__footer" style="margin-top: 0px">
                                                        <div class="m-invoice__container m-invoice__container--centered" style="padding: 20px 0 20px 0;">
                                                            <div class="m-invoice__content">
                                                                <span>TOTAL APORTACIONES DEL EMPLEADOR</span>
                                                                <span class="m-invoice__price" style="font-size: 1.5rem;" runat="server" id="txtTotalAportacionEmpleador">S/. 00.00</span>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="m-invoice-1 borderdashed">
                                                <div class="m-invoice__wrapper">

                                                    <div class="m-invoice__footer" style="margin-top: 0px; background-color: #fff">
                                                        <div class="m-invoice__container m-invoice__container--centered" style="padding: 30px 0 30px 0;">
                                                            <div class="m-invoice__content" style="text-align: right; padding-top: 20px;">
                                                                <h4 style="float: left; position: absolute">Costo Total del Trabajador</h4>
                                                                <span class="m-invoice__price" style="font-size: 2.2rem; color: #333;" runat="server" id="txtCostoTotalTrabajador">S/. 00.00</span>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div id="gpBtnGenerarBoleta" runat="server">
                                        <div class="m-portlet__foot m-portlet__no-border m-portlet__foot--fit">
                                            <div class="m-form__actions m-form__actions--solid">
                                                <div class="row">
                                                    <div class="col-lg-6">
                                                        <asp:LinkButton CssClass="btn btn-secondary" runat="server" ID="btnCancelarBoleta" OnClick="btnCancelarBoleta_Click">Cancelar</asp:LinkButton>
                                                    </div>
                                                    <div class="col-lg-6 m--align-right">
                                                        <asp:LinkButton CssClass="btn btn-primary" runat="server" ID="btnGenerarBoleta" OnClick="btnGenerarBoleta_Click">Generar boleta</asp:LinkButton>
                                                        <asp:LinkButton CssClass="btn btn-secondary" runat="server" ID="btnLimpiar" OnClick="btnLimpiar_Click">Limpiar</asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div id="gpBoletaPlanilla" runat="server">
                                    <div class="m-invoice-1">
                                        <div class="m-invoice__wrapper">
                                            <div class="m-invoice__head" style="background-image: url(/planilla/assets/images/bg-6.jpg);">
                                                <div class="m-invoice__container m-invoice__container--centered">
                                                    <div class="m-invoice__logo">
                                                        <a href="#">
                                                            <h1>BOLETA DE PAGO</h1>
                                                        </a>
                                                    </div>
                                                    <span class="m-invoice__desc">
                                                        <span>Calle Condorama 136, Santiago de Surco</span>
                                                        <span>Lima</span>
                                                    </span>
                                                    <div class="m-invoice__items">
                                                        <div class="m-invoice__item">
                                                            <span class="m-invoice__subtitle">PERIÓDO</span>
                                                            <span class="m-invoice__text">
                                                                <asp:Label runat="server" Style="text-transform: capitalize" ID="lblPeriodoBoleta"></asp:Label></span>
                                                        </div>
                                                        <div class="m-invoice__item">
                                                            <span class="m-invoice__subtitle">SEMANA</span>
                                                            <span class="m-invoice__text">
                                                                <asp:Label runat="server" ID="lblSemanaBoleta"></asp:Label></span>
                                                        </div>
                                                        <div class="m-invoice__item">
                                                            <span class="m-invoice__subtitle">
                                                                <asp:Label runat="server" ID="lblCargoTrabajadorBoleta"></asp:Label></span>
                                                            <span class="m-invoice__text">
                                                                <asp:Label runat="server" ID="lblNombreTrabajadorBoleta"></asp:Label>
                                                                <br>
                                                                <asp:Label runat="server" ID="lblDocumentoTrabajadorBoleta"></asp:Label></span>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="m-invoice__body m-invoice__body--centered">
                                                <div class="table-responsive">
                                                    <table class="table">
                                                        <thead>
                                                            <tr>
                                                                <th>DESCRIPCIÓN</th>
                                                                <th>DÍAS LAB.</th>
                                                                <th>DOM/FER.</th>
                                                                <th>TOTAL</th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <tr>
                                                                <td>Ingresos del Trabajador</td>
                                                                <td>
                                                                    <asp:Label runat="server" ID="lblDiasLaboradosIngresos"></asp:Label></td>
                                                                <td>
                                                                    <asp:Label runat="server" ID="lblDiasDominicalIngresos"></asp:Label></td>
                                                                <td>S/.
                                                                    <asp:Label runat="server" ID="lblTotalIngreso"></asp:Label></td>
                                                            </tr>
                                                            <tr>
                                                                <td>Beneficios del Trabajador</td>
                                                                <td>
                                                                    <asp:Label runat="server" ID="lblDiasLaboradosBeneficios"></asp:Label></td>
                                                                <td>
                                                                    <asp:Label runat="server" ID="lblDiasDominicalBeneficios"></asp:Label></td>
                                                                <td>S/.
                                                                    <asp:Label runat="server" ID="lblTotalBeneficios"></asp:Label></td>
                                                            </tr>
                                                            <tr>
                                                                <td>Descuentos del Trabajador</td>
                                                                <td>
                                                                    <asp:Label runat="server" ID="lblDiasLaboradosDescuentos"></asp:Label></td>
                                                                <td>
                                                                    <asp:Label runat="server" ID="lblDiasDominicalDescuentos"></asp:Label></td>
                                                                <td>S/.
                                                                    <asp:Label runat="server" ID="lblTotalDescuentos"></asp:Label></td>
                                                            </tr>
                                                            <tr>
                                                                <td>Aporte del Empleador</td>
                                                                <td>
                                                                    <asp:Label runat="server" ID="lblDiasLaboradosAporteEmp"></asp:Label></td>
                                                                <td>
                                                                    <asp:Label runat="server" ID="lblDiasDominicalAporteEmp"></asp:Label></td>
                                                                <td>S/.
                                                                    <asp:Label runat="server" ID="lblTotalAporteEmp"></asp:Label></td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </div>
                                            </div>
                                            <div class="m-invoice__footer">
                                                <div class="m-invoice__container m-invoice__container--centered">
                                                    <div class="m-invoice__content">
                                                        <span>TRANSFERENCIA BANCARIA</span>
                                                        <span>
                                                            <span>Banco:</span>
                                                            <span>
                                                                <asp:Label runat="server" ID="lblNombreBancoBoleta"></asp:Label></span>
                                                        </span>
                                                        <span>
                                                            <span>Número de cuenta:</span>
                                                            <span>
                                                                <asp:Label runat="server" ID="lblNroCuentaBoleta"></asp:Label></span>
                                                        </span>
                                                        <span>
                                                            <span>CCI:</span>
                                                            <span>
                                                                <asp:Label runat="server" ID="lblNroCCIBoleta"></asp:Label></span>
                                                        </span>
                                                    </div>
                                                    <div class="m-invoice__content">
                                                        <span>TOTAL COSTO</span>
                                                        <span class="m-invoice__price costototal" runat="server" id="lblTotalCostoEmpresa"></span>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="m-portlet__foot m-portlet__no-border m-portlet__foot--fit">
                                        <div class="m-form__actions m-form__actions--solid">
                                            <div class="row">
                                                <div class="col-lg-6">

                                                    <asp:LinkButton runat="server" class="btn btn-secondary" ID="btnRegresarCalculoPlanilla" OnClick="btnRegresarCalculoPlanilla_Click">Regresar</asp:LinkButton>
                                                </div>
                                                <div class="col-lg-6 m--align-right">
                                                    <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#gpGenerarPlanilla">Guardar Planilla</button>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div runat="server" id="gpPlanillaRecientes">
                            <div class="m-portlet__body m-portlet__body--no-padding">
                                <div class="m-portlet__body" runat="server">
                                    <!--begin: Search Form -->
                                    <div class="m-portlet__head" style="margin-top: -20px; border-bottom: none">
                                        <div class="m-portlet__head-caption">
                                            <div class="m-portlet__head-title">
                                                <span class="m-portlet__head-icon">
                                                    <i class="flaticon-statistics"></i>
                                                </span>
                                                <h3 class="m-portlet__head-text" style="font-weight: 400; font-size: 1.2rem" id="lblResultadoBusqueda" runat="server"></h3>

                                            </div>
                                        </div>

                                    </div>
                                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                        <div class="col-lg-6 col-md-12 col-sm-12 col-xs-12 nopadding-sides">
                                            <label>Rango de fechas de boletas generadas</label>
                                            <div class="m-input-icon m-input-icon--right">
                                                <asp:TextBox runat="server" CssClass="form-control m-input rangofechareporte" ReadOnly="true" placeholder="Seleccione" ID="txtRangoFechasReporte" />
                                                <span class="m-input-icon__icon m-input-icon__icon--right">
                                                    <span>
                                                        <i class="la la-calendar"></i>
                                                    </span>
                                                </span>
                                                <asp:TextBox runat="server" Style="display: none" ID="txtFechaInicialReporte" CssClass="fechainicial"></asp:TextBox>
                                                <asp:TextBox runat="server" Style="display: none" ID="txtFechaFinalReporte" CssClass="fechafinal"></asp:TextBox>
                                            </div>
                                            <span class="m-form__help">&nbsp;</span>
                                        </div>
                                    </div>
                                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                        <div class="m-datable table-responsive">
                                            <asp:GridView runat="server" ID="dgvPlanillaRecientes" Width="100%" AutoGenerateColumns="false" CssClass="table table-bordered" RowStyle-BackColor="#FFFFFF" AlternatingRowStyle-BackColor="#F5F5F6" HeaderStyle-BackColor="#F5F5F6" HeaderStyle-Wrap="true" RowStyle-Wrap="true" ShowHeaderWhenEmpty="true" AllowPaging="True" PageSize="5" OnPageIndexChanging="dgvPlanillaRecientes_PageIndexChanging" OnRowCommand="dgvPlanillaRecientes_RowCommand">
                                                <Columns>
                                                    <asp:BoundField DataField="CodPlanillaEmpleados" HeaderText="CodPlanillaEmpleados" HtmlEncode="false" HeaderStyle-CssClass="hidden" Visible="false" ItemStyle-CssClass="hidden" />
                                                    <asp:BoundField DataField="Nombres" HeaderText="NOMBRE TRABAJADOR" HtmlEncode="false" ItemStyle-Width="200" />
                                                    <asp:BoundField DataField="DocumentoIdentidad" HeaderText="DOCUMENTO" HtmlEncode="false" HeaderStyle-CssClass="hidden" Visible="false" ItemStyle-CssClass="hidden" />
                                                    <asp:BoundField DataField="MesPeriodo" HeaderText="PERIODO" HtmlEncode="false" />
                                                    <asp:BoundField DataField="FechaInicio" HeaderText="FECHA INICIAL" HtmlEncode="false" />
                                                    <asp:BoundField DataField="FechaFin" HeaderText="FECHA FINAL" HtmlEncode="false" />

                                                    <asp:BoundField DataField="DiasLaborados" HeaderText="DIAS LAB." HtmlEncode="false" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="header-center" />


                                                    <asp:BoundField DataField="TotalIngresos" HeaderText="TOTAL INGRESO" HtmlEncode="false" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="header-center" />
                                                    <asp:BoundField DataField="TotalBeneficios" HeaderText="TOTAL BENEF." HtmlEncode="false" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="header-center" />
                                                    <asp:BoundField DataField="TotalDescuentos" HeaderText="TOTAL DSCTOS" HtmlEncode="false" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="header-center" />
                                                    <asp:BoundField DataField="TotalAporteEmpresa" HeaderText="TOTAL APOR. EMP." HtmlEncode="false" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="header-center" />
                                                    <asp:BoundField DataField="TotalPagarTrabajador" HeaderText="TOTAL PAGAR TRAB." HtmlEncode="false" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="header-center" />
                                                    <asp:BoundField DataField="TotalCostoTrabajador" HeaderText="TOTAL COSTO TRAB." HtmlEncode="false" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="header-center" />
                                                    <asp:TemplateField HeaderText="OPCION" ItemStyle-Width="150">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lblDetalles" runat="server" CssClass="btn btn-sm btn-default"
                                                                Text='Modificar'
                                                                CommandName="ModificarPlanilla"
                                                                CommandArgument='<%# Eval("CodPlanillaEmpleados")%>'>
                                                            </asp:LinkButton>
                                                            <asp:LinkButton CssClass="btn btn-secondary" runat="server"
                                                                CommandName="ImprimirPlanilla"
                                                                CommandArgument='<%# Eval("CodPlanillaEmpleados")%>'>
                                                                <i class="fa flaticon-technology-1"></i> 
                                                            </asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <EmptyDataTemplate>
                                                    No hay datos para mostrar
                                                </EmptyDataTemplate>
                                                <PagerStyle HorizontalAlign="Right" CssClass="grid-pager-silver" />
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="m-portlet m-portlet--mobile" id="gpReporteRVIndividual" runat="server">
                        <div class="m-portlet__head">
                            <div class="m-portlet__head-caption">
                                <div class="m-portlet__head-title">
                                    <h3 class="m-portlet__head-text">Boleta de Pago
                                    </h3>
                                </div>
                            </div>

                        </div>
                        <div class="m-portlet m-portlet--head-overlay m-portlet--full-height   m-portlet--rounded-force">
                            <div class="m-portlet__head m-portlet__head--fit" style="margin-top: -20px">
                                <div class="m-portlet__head-caption">
                                    <div class="m-portlet__head-title">
                                        <h3 class="m-portlet__head-text m--font-light">Filtros de búsqueda
                                        </h3>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-12">
                                <div class="col-md-12">
                                    <center>  
                                        <rsweb:ReportViewer ID="ReportViewer1" runat="server" font-names="Verdana" font-size="8pt" waitmessagefont-names="Verdana" waitmessagefont-size="14pt" backcolor="#F2F2F2" width="780px" height="700px" bordercolor="#2F4050" borderstyle="Solid" borderwidth="1px">
                                            <LocalReport ReportPath="Reportes\RViewer\BoletaPagoEmpleados.rdlc">
                                                <DataSources>
                                                    <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" Name="DataSet1" />
                                                </DataSources>
                                            </LocalReport>
                                        </rsweb:ReportViewer>                                                                   
                                        <div style="margin: 20px auto;">
                                            <div class="col-md-12 text-center">
                                                <asp:LinkButton ID="btnRegresarCalculos" CssClass="btn btn-primary" runat="server" OnClick="btnRegresarCalculos_Click"  ><span class="icono-boton"><i class="la la-chevron-circle-left"></i></span> REGRESAR</asp:LinkButton>

                                            </div>
                                        </div>
                                    </center>

                                    <asp:SqlDataSource ID="SqlDataSource1" runat="server"></asp:SqlDataSource>
                                    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetData" TypeName="SistemaGestionPlanilla.PlanillaDataSetTableAdapters.PA_gen_ObtenerListadoReportePlanillaTableAdapter"></asp:ObjectDataSource>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 no-padding" id="gpEnlaceExterno" style="display: none" runat="server">
                        <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                            <div class="form-group">
                                <asp:Label runat="server" ID="lblEnlaceExterno">Enlace Externo</asp:Label>
                                <asp:TextBox runat="server" ID="txtEnlaceExterno" CssClass="form-control"></asp:TextBox>
                                <asp:TextBox runat="server" ID="txtParametrosValores" CssClass="form-control"></asp:TextBox>
                                <asp:LinkButton runat="server" ID="btnMostrarListaTrabajador" OnClick="btnMostrarListaTrabajador_Click"></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnActivarComision" OnClick="btnActivarComision_Click"></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnActivarChckBox" OnClick="btnActivarChckBox_Click"></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnActivarVacaciones" OnClick="btnActivarVacaciones_Click" ></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnActivarGratificacion" OnClick="btnActivarGratificacion_Click" ></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnBuscarReporteReciente" OnClick="btnBuscarReporteReciente_Click"></asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>
                <asp:UpdateProgress runat="server" ID="PageUpdateProgress">
                    <ProgressTemplate>
                        <div class="loader_planillon"></div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div class="modal inmodal fade" id="gpGenerarPlanilla" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLabel">Confirmación</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">×</span>
                    </button>
                </div>
                <div class="modal-body">
                    <p style="font-weight: 400">¿Está seguro(a) que desea guardar el <strong>registro de planilla</strong> del trabajador.</p>
                </div>
                <div class="modal-footer">
                    <asp:LinkButton runat="server" data-dismiss="modal" CssClass="btn btn-sm btn-secondary btn-icon"><i class="fa fa-arrow-circle-left" aria-hidden="true"></i> REGRESAR</asp:LinkButton>
                    <asp:LinkButton ID="btnGuardarPlanilla" CssClass="btn btn-sm btn-primary btn-icon" runat="server" OnClick="btnGuardarPlanilla_Click" ><i class="fa fa-check-circle" aria-hidden="true"></i> CONFIRMAR</asp:LinkButton>
                    
                </div>
            </div>
        </div>
    </div>
    <script src="<%=ConfigurationManager.AppSettings["AssetsUrl"]%>/assets/js/jquery-3.2.1.js" type="text/javascript"></script>
    <script>
        $(document).ready(function () {
            $('#navMenuPlanilla').addClass("m-menu__item--open");
            $('#navMenuPlanilla').addClass("m-menu__item--expanded");
            $('#navSubMenuGenerarEmpleados').addClass("m-menu__item--active");
        })

        function ReloadJqueryDatePicker() {
            var script = document.createElement("script");
            script.setAttribute("type", "text/javascript");
            script.setAttribute("src", "/planilla/assets/js/bootstrap-datepicker.js");
            document.getElementsByTagName("head")[0].appendChild(script);
        }
        function disableReadOnly() {
            $("#name").prop('readonly', false);
        }
        function Mensaje(mensaje, tipo) {
            setTimeout(function () {
                toastr.options = {
                    closeButton: true,
                    progressBar: true,
                    showMethod: 'slideDown',
                    timeOut: 5000
                };
                if (tipo == 'success') {
                    toastr.success(mensaje);
                } else if (tipo == 'info') {
                    toastr.info(mensaje);
                } else if (tipo == 'error') {
                    toastr.error(mensaje);
                } else if (tipo == 'warning') {
                    toastr.warning(mensaje);
                }

            }, 300);
        }

        function CargarDateRange() {

            var FechaInicial = $('#<%=txtFechaInicial.ClientID%>').val();
            var FechaFinal = $('#<%=txtFechaFinal.ClientID%>').val();

            $('.rangofechaplanilla').daterangepicker({
                autoUpdateInput: true,
                buttonClasses: 'm-btn btn',
                applyClass: 'btn-primary',
                cancelClass: 'btn-secondary',
                startDate: FechaInicial,
                endDate: FechaFinal,
                locale: {
                    format: 'DD/MM/YYYY',
                    applyLabel: 'Aplicar',
                    cancelLabel: 'Cancelar',
                    daysOfWeek: ['Do', 'Lu', 'Ma', 'Mi', 'Ju', 'Vi', 'Sa'],
                    monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Setiembre', 'Octubre', 'Noviembre', 'Diciembre']
                }
            });
            $('.rangofechaplanilla').on('apply.daterangepicker', function (ev, picker) {

                var myString = $('.rangofechaplanilla').val();

                var index = myString.split('-');
                var FechaInicial = index[0]; // Gets the first part
                var FechaFinal = index[1];; //Gets second part

                $('#<%=txtFechaInicial.ClientID%>').val(FechaInicial);
                $('#<%=txtFechaFinal.ClientID%>').val(FechaFinal);

                $('#<%=btnMostrarListaTrabajador.ClientID%>')[0].click();
            });
        }
        function CargarDateRangeReporte() {

            var FechaInicial = $('#<%=txtFechaInicialReporte.ClientID%>').val();
             var FechaFinal = $('#<%=txtFechaFinalReporte.ClientID%>').val();

             $('.rangofechareporte').daterangepicker({
                 autoUpdateInput: true,
                 buttonClasses: 'm-btn btn',
                 applyClass: 'btn-primary',
                 cancelClass: 'btn-secondary',
                 startDate: FechaInicial,
                 endDate: FechaFinal,
                 locale: {
                     format: 'DD/MM/YYYY',
                     applyLabel: 'Aplicar',
                     cancelLabel: 'Cancelar',
                     daysOfWeek: ['Do', 'Lu', 'Ma', 'Mi', 'Ju', 'Vi', 'Sa'],
                     monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Setiembre', 'Octubre', 'Noviembre', 'Diciembre']
                 }
             });
             $('.rangofechareporte').on('apply.daterangepicker', function (ev, picker) {

                 var myString = $('.rangofechareporte').val();

                 var index = myString.split('-');
                 var FechaInicial = index[0]; // Gets the first part
                 var FechaFinal = index[1];; //Gets second part

                 $('#<%=txtFechaInicialReporte.ClientID%>').val(FechaInicial);
                $('#<%=txtFechaFinalReporte.ClientID%>').val(FechaFinal);

                $('#<%=btnBuscarReporteReciente.ClientID%>')[0].click();
            });
        }
        function SelectFecha() {
            $('#<%=txtFechaAnoPeriodoSelect.ClientID%>').val($('#<%=txtFechaAnoPeriodo.ClientID%>').val());
        }
         function ActivarCheckBox() {
            $('#<%=btnActivarChckBox.ClientID%>')[0].click();
        }
  
        function ActivarComision() {
            $('#<%=btnActivarComision.ClientID%>')[0].click();
        }
        function ActivarVacaciones() {
            $('#<%=btnActivarVacaciones.ClientID%>')[0].click();
        }
        function ActivarGratificacion() {
            $('#<%=btnActivarGratificacion.ClientID%>')[0].click();
        }
        function GotoTop() {

            $('#m_scroll_top')[0].click();

        }
    </script>
</asp:Content>
