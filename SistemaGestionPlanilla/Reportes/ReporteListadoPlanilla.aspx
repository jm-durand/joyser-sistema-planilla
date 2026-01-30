<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ReporteListadoPlanilla.aspx.cs" Inherits="SistemaGestionPlanilla.Reportes.ReporteListadoPlanilla" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript"> 
        function pageLoad() {
            $(function () {
              $('.m-select2-general').select2({
                    placeholder: "Seleccione una opcion",
                    width: '100%'
                });

                CargarDateRange();
                ReloadJqueryDatePicker();

                $('#<%=txtFechaAnoPeriodo.ClientID%>').val($('#<%=txtFechaAnoPeriodoSelect.ClientID%>').val());
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
                            Genere su reporte de acuerdo a cualquier de los 2 tipos de búsqueda.
                        </div>
                    </div>
                    <div class="m-portlet m-portlet--mobile" id="gpConsultaReporte" runat="server">
                        <div class="m-portlet__head">
                            <div class="m-portlet__head-caption">
                                <div class="m-portlet__head-title">
                                    <h3 class="m-portlet__head-text">Generar Listado Reporte Planilla
                                    </h3>
                                </div>
                            </div>

                        </div>
                        <div class="m-portlet m-portlet--head-overlay m-portlet--full-height   m-portlet--rounded-force">
                            <div class="m-portlet__head m-portlet__head--fit">
                                <div class="m-portlet__head-caption">
                                    <div class="m-portlet__head-title">
                                        <h3 class="m-portlet__head-text m--font-light">Filtros de búsqueda
                                        </h3>
                                    </div>
                                </div>                 
                            </div>
                            <div class="m-portlet__body">
                                <div class="m-widget28">
                                    <div class="m-widget28__pic m-portlet-fit--sides" style="min-height:200px"></div>
                                    <div class="m-widget28__container">

                                        <!-- begin::Nav pills -->
                                        <ul class="m-widget28__nav-items nav nav-pills nav-fill" role="tablist">
                                            <li class="m-widget28__nav-item nav-item">
                                                <a class="nav-link" data-toggle="pill" href="#menu11" id="PillAnoMesPeriodo">
                                                    <span>
                                                        <i class="fa flaticon-clipboard"></i>
                                                    </span>
                                                    <span>Año y Mes</span>
                                                </a>
                                            </li>
                                            <li class="m-widget28__nav-item nav-item">
                                                <a class="nav-link active" data-toggle="pill" href="#menu21" id="PillRangoFechas">
                                                    <span>
                                                        <i class="fa flaticon-file-1"></i>
                                                    </span>
                                                    <span>Rango de fechas</span>
                                                </a>
                                            </li>
                                           
                                        </ul>

                                        <!-- end::Nav pills -->

                                        <!-- begin::Tab Content -->
                                        <div class="m-widget28__tab tab-content">
                                            <div id="menu11" class="m-widget28__tab-container tab-pane fade">
                                                <div class="m-widget28__tab-items">
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
                                                                <asp:DropDownList runat="server" CssClass="form-control m-select2 m-select2-general" ID="cboMesPeriodo" >
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
                                                    <div class="form-group m-form__group row" style="padding-bottom: 0; border-bottom: none">
                                                        <div class="col-lg-6">
                                                            <asp:Label  ID="lblTipoPlanillaPeriodo" runat="server">Tipo Planilla</asp:Label>
                                                            <div class="m-input-icon m-input-icon--right">
                                                                <asp:DropDownList runat="server" CssClass="form-control m-select2 m-select2-general" name="param" ID="cboTipoPlanillaPeriodo">
                                                                </asp:DropDownList>
                                                                <span class="m-input-icon__icon m-input-icon__icon--right">
                                                                    <span>
                                                                        <i class="la la-black-tie"></i>
                                                                    </span>
                                                                </span>
                                                            </div>
                                                            <span class="m-form__help">Seleccionar el tipo de planilla</span>
                                                        </div>
                                                    </div>
                                                    <div class="row align-items-center" style="padding-top: 20px">
                                                        <div class="col-xl-12 order-1 order-xl-2 m--align-left">
                                                            <a role="button" runat="server" id="btnGenerarReportePeriodoAnoMes" class="btn btn-primary m-btn m-btn--custom m-btn--icon m-btn--air m-btn--pill" style="color:#fff" onserverclick="btnGenerarReportePeriodoAnoMes_ServerClick">
                                                                <span>
                                                                    <i class="la la-search"></i>
                                                                    <span>Generar listado</span>
                                                                </span>
                                                            </a>
                                                            <div class="m-separator m-separator--dashed d-xl-none"></div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div id="menu21" class="m-widget28__tab-container tab-pane fade active show">
                                                <div class="m-widget28__tab-items">
                                                    <div class="form-group m-form__group row">
                                                        <div class="col-lg-6">
                                                            <label>Rango de fechas</label>
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
                                                        <div class="col-lg-6">
                                                            <asp:Label ID="lblTipoPlanillaRangoFechas" runat="server">Tipo Planilla</asp:Label>
                                                            <div class="m-input-icon m-input-icon--right">
                                                                <asp:DropDownList runat="server" CssClass="form-control m-select2 m-select2-general" name="param" ID="cboTipoPlanillaRangoFechas">
                                                                </asp:DropDownList>
                                                                <span class="m-input-icon__icon m-input-icon__icon--right">
                                                                    <span>
                                                                        <i class="la la-black-tie"></i>
                                                                    </span>
                                                                </span>
                                                            </div>
                                                            <span class="m-form__help">Seleccionar el tipo de planilla</span>
                                                        </div>
                                                    </div>
                                                    <div class="row align-items-center" style="padding-top: 20px">
                                                        <div class="col-xl-12 order-1 order-xl-2 m--align-left">
                                                            <a runat="server" role="button" id="btnGenerarReporteRangoFechas" class="btn btn-primary m-btn m-btn--custom m-btn--icon m-btn--air m-btn--pill" style="color:#fff" onserverclick="btnGenerarReporteRangoFechas_ServerClick">
                                                                <span>
                                                                    <i class="la la-search"></i>
                                                                    <span>Generar listado</span>
                                                                </span>
                                                            </a>
                                                            <div class="m-separator m-separator--dashed d-xl-none"></div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            
                                        </div>

                                        <!-- end::Tab Content -->
                                    </div>
                                </div>
                            </div>
                        </div>
                        

                    </div>

                    <div class="m-portlet m-portlet--mobile" id="gpReporteRV" runat="server">
                        <div class="m-portlet__head">
                            <div class="m-portlet__head-caption">
                                <div class="m-portlet__head-title">
                                    <h3 class="m-portlet__head-text">Reporte
                                    </h3>
                                </div>
                            </div>

                        </div>
                         <div class="m-portlet m-portlet--head-overlay m-portlet--full-height   m-portlet--rounded-force">
                            <div class="m-portlet__head m-portlet__head--fit" style="margin-top:-20px">
                                <div class="m-portlet__head-caption">
                                    <div class="m-portlet__head-title">
                                        <h3 class="m-portlet__head-text m--font-light">Filtros de búsqueda
                                        </h3>
                                    </div>
                                </div>                 
                            </div>
                        <div class="col-sm-12">
                            <div class="col-md-12">
                                <div>
                                    <rsweb:ReportViewer ID="ReportViewer1" runat="server" font-names="Verdana" font-size="8pt" waitmessagefont-names="Verdana" waitmessagefont-size="14pt" backcolor="#F2F2F2" width="1140px"   height="600px" bordercolor="#2F4050" borderstyle="Solid" borderwidth="1px">
                                      <%--   <LocalReport ReportPath="Reportes\RViewer\PlanillaConstruccion.rdlc">
                                            <DataSources>
                                                <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" Name="DataSet1" />
                                            </DataSources>
                                        </LocalReport>--%>
                                    </rsweb:ReportViewer>
                               
                                    <div style="margin: 20px auto;">
                                        <div class="col-md-12 text-center">
                                            <asp:LinkButton  ID="btnRegresar"  CssClass="btn btn-primary"  runat="server" OnClick="btnRegresar_Click" ><span class="icono-boton"><i class="la la-chevron-circle-left"></i></span> REGRESAR</asp:LinkButton>
                                            
                                        </div>
                                    </div>
                                </div>
             
                                <asp:SqlDataSource ID="SqlDataSource1" runat="server"></asp:SqlDataSource>
                                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetData" TypeName="SistemaGestionPlanilla.PlanillaDataSetTableAdapters.PA_gen_ObtenerListadoReportePlanillaTableAdapter"></asp:ObjectDataSource>
                            </div>
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

    <script src="<%=ConfigurationManager.AppSettings["AssetsUrl"]%>/assets/js/jquery-3.2.1.js" type="text/javascript"></script>
    <script>
        $(document).ready(function () {
            $('#navMenuReportes').addClass("m-menu__item--open");
            $('#navMenuReportes').addClass("m-menu__item--expanded");
            $('#navSubMenuPlanillaReportesListado').addClass("m-menu__item--active");
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
                   orientation: 'auto',
                   drops: 'up',
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

               
            });


        }
        function SelectFecha() {
            $('#<%=txtFechaAnoPeriodoSelect.ClientID%>').val($('#<%=txtFechaAnoPeriodo.ClientID%>').val());
        }

        function MantenerPillAnoMes(){
             $('#PillAnoMesPeriodo')[0].click();
        }
    </script>
</asp:Content>
