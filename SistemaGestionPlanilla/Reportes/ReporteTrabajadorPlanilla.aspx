<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ReporteTrabajadorPlanilla.aspx.cs" Inherits="SistemaGestionPlanilla.Reportes.ReporteTrabajadorPlanilla" %>

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

                CargarDateRangeMasivo();
                CargarDateRangeIndividual();

                $('#ReportViewer2_fixedTable tbody tr').attr("align", "center");
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
                            Busque o seleccione a los trabajadores de la compañía, para generar su boleta individual.
                        </div>
                    </div>
                    <div class="m-portlet m-portlet--mobile" id="gpConsultaReporte" runat="server">
                        <div class="m-portlet__head">
                            <div class="m-portlet__head-caption">
                                <div class="m-portlet__head-title">
                                    <h3 class="m-portlet__head-text">Generar Boleta de Pago Semanal
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
                                    <div class="m-widget28__pic m-portlet-fit--sides" style="min-height: 200px"></div>
                                    <div class="m-widget28__container">

                                        <!-- begin::Nav pills -->
                                        <ul class="m-widget28__nav-items nav nav-pills nav-fill" role="tablist">
                                            <li class="m-widget28__nav-item nav-item">
                                                <a class="nav-link" data-toggle="pill" href="#menu11" id="PillBoletaIndividual">
                                                    <span>
                                                        <i class="fa flaticon-user-ok"></i>
                                                    </span>
                                                    <span>Boleta Individual</span>
                                                </a>
                                            </li>
                                            <li class="m-widget28__nav-item nav-item">
                                                <a class="nav-link active" data-toggle="pill" href="#menu21" id="PillRangoFechas">
                                                    <span>
                                                        <i class="fa flaticon-file-1"></i>
                                                    </span>
                                                    <span>Boleta Masiva</span>
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
                                                            <label>Rango de fechas</label>
                                                            <div class="m-input-icon m-input-icon--right">
                                                                <asp:TextBox runat="server" CssClass="form-control m-input rangofechaplanilla2" ReadOnly="true" placeholder="Seleccione" ID="txtFechaHastaPlanillaIndividual" />
                                                                <span class="m-input-icon__icon m-input-icon__icon--right">
                                                                    <span>
                                                                        <i class="la la-calendar"></i>
                                                                    </span>
                                                                </span>
                                                                <asp:TextBox runat="server" Style="display: none" ID="txtFechaInicialIndividual" CssClass="fechainicial"></asp:TextBox>
                                                                <asp:TextBox runat="server" Style="display: none" ID="txtFechaFinalIndividual" CssClass="fechafinal"></asp:TextBox>
                                                            </div>
                                                            <span class="m-form__help">Seleccionar rango de fechas</span>
                                                        </div>
                                                        <div class="col-lg-6">
                                                            <asp:Label ID="Label1" runat="server">Tipo Planilla</asp:Label>
                                                            <div class="m-input-icon m-input-icon--right">
                                                                <asp:DropDownList runat="server" CssClass="form-control m-select2 m-select2-general" ID="cboTipoPlanillaReporteIndividual" AutoPostBack="true" OnSelectedIndexChanged="cboTipoPlanillaReporteIndividual_SelectedIndexChanged">
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
                                                    <div class="form-group m-form__group row" style="padding-bottom: 0; border-bottom: none">
                                                        <div class="col-lg-6">
                                                            <label>&nbsp;</label>
                                                            <div class="m-input-icon m-input-icon--right">
                                                                <asp:DropDownList runat="server" CssClass="form-control m-select2 m-select2-general" ID="cboTrabajadores" OnSelectedIndexChanged="cboTrabajadores_SelectedIndexChanged" AutoPostBack="true">
                                                                </asp:DropDownList>
                                                                <span class="m-input-icon__icon m-input-icon__icon--right">
                                                                    <span>
                                                                        <i class="la la-calendar"></i>
                                                                    </span>
                                                                </span>
                                                            </div>
                                                            <span class="m-form__help">Seleccione trabajador.</span>
                                                        </div>
                                                    </div>                                                    
                                                    <div class="form-group m-form__group row" style="padding-bottom: 0; border-bottom: none; margin-top: 20px" id="gpResultadoPlanillaTrabajador" runat="server">
                                                        <div class="col-lg-12">
                                                            <div class="">
                                                                <div class="" runat="server">
                                                                    <!--begin: Search Form -->
                                                                    <div class="m-portlet__head" style="margin-top: -20px; border-bottom: none; padding-left: 0px">
                                                                        <div class="m-portlet__head-caption">
                                                                            <div class="m-portlet__head-title">
                                                                                <span class="m-portlet__head-icon">
                                                                                    <i class="flaticon-statistics"></i>
                                                                                </span>
                                                                                <h3 class="m-portlet__head-text" style="font-weight: 400; font-size: 1.2rem" id="lblResultadoBusqueda" runat="server"></h3>

                                                                            </div>
                                                                        </div>

                                                                    </div>
                                                                    <div class="">
                                                                        <div class="m-datable table-responsive">
                                                                            <asp:GridView runat="server" ID="dgvPlanillaRecientes" Width="100%" AutoGenerateColumns="false" CssClass="table table-bordered" RowStyle-BackColor="#FFFFFF" AlternatingRowStyle-BackColor="#F5F5F6" HeaderStyle-BackColor="#F5F5F6" HeaderStyle-Wrap="true" RowStyle-Wrap="true" ShowHeaderWhenEmpty="true" AllowPaging="True" PageSize="10" OnRowCommand="dgvPlanillaRecientes_RowCommand">
                                                                                <Columns>
                                                                                    <asp:BoundField DataField="CodPlanilla" HeaderText="CodPlanillaConstruccion" HtmlEncode="false" HeaderStyle-CssClass="hidden" Visible="false" ItemStyle-CssClass="hidden" />
                                                                                    <asp:BoundField DataField="Nombres" HeaderText="NOMBRE TRABAJADOR" HtmlEncode="false" ItemStyle-Width="200" />
                                                                                    <asp:BoundField DataField="DocumentoIdentidad" HeaderText="DOCUMENTO" HtmlEncode="false" />
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
                                                                                    <asp:TemplateField HeaderText="OPCION">
                                                                                        <ItemTemplate>
                                                                                            <asp:LinkButton ID="lblReporte" runat="server" CssClass="btn btn-sm btn-default"
                                                                                                Text='Generar Boleta'
                                                                                                CommandName="GenerarBoleta"
                                                                                                CommandArgument='<%# Eval("CodPlanilla")+","+Eval("TipoPlanilla")%>'>
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
                                                            <a runat="server" role="button" id="btnGenerarBoletaRangoFechas" class="btn btn-primary m-btn m-btn--custom m-btn--icon m-btn--air m-btn--pill" style="color: #fff" onserverclick="btnGenerarBoletaRangoFechas_ServerClick">
                                                                <span>
                                                                    <i class="la la-search"></i>
                                                                    <span>Generar Boletas</span>
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
                                        <rsweb:ReportViewer ID="ReportViewer2" runat="server" font-names="Verdana" font-size="8pt" waitmessagefont-names="Verdana" waitmessagefont-size="14pt" backcolor="#F2F2F2" width="780px" height="700px" bordercolor="#2F4050" borderstyle="Solid" borderwidth="1px"  >
                                       <%--  <LocalReport ReportPath="Reportes\RViewer\BoletaPagoIndividaul.rdlc">
                                            <DataSources>
                                                <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" Name="DataSet1" />
                                            </DataSources>
                                        </LocalReport>--%>
                                    </rsweb:ReportViewer>

                                        <div style="margin: 20px auto;">
                                            <div class="col-md-12 text-center">
                                                <asp:LinkButton ID="btnRegresar" CssClass="btn btn-primary" runat="server" OnClick="btnRegresar_Click" ><span class="icono-boton"><i class="la la-chevron-circle-left"></i></span> REGRESAR</asp:LinkButton>

                                            </div>
                                        </div>
                                    </center>

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
            $('#navSubMenuPlanillaReporteTrabajador').addClass("m-menu__item--active");
        })
        function CargarDateRangeMasivo() {

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
        function CargarDateRangeIndividual() {

            var FechaInicial = $('#<%=txtFechaInicialIndividual.ClientID%>').val();
            var FechaFinal = $('#<%=txtFechaFinalIndividual.ClientID%>').val();

            $('.rangofechaplanilla2').daterangepicker({
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
            $('.rangofechaplanilla2').on('apply.daterangepicker', function (ev, picker) {

                var myString = $('.rangofechaplanilla2').val();

                var index = myString.split('-');
                var FechaInicial = index[0]; // Gets the first part
                var FechaFinal = index[1];; //Gets second part

                $('#<%=txtFechaInicialIndividual.ClientID%>').val(FechaInicial);
                $('#<%=txtFechaFinalIndividual.ClientID%>').val(FechaFinal);

                <%--if ($('#<%=cboTrabajadores.ClientID%>').val() != "Seleccione") {
                    $('#<%=btn.ClientID%>')[0].click();
                }--%>
            });
        }
        function MantenerPillIndividual() {
            $('#PillBoletaIndividual')[0].click();
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
    </script>
</asp:Content>
