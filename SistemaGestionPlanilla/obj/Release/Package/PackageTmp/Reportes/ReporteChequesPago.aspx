<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ReporteChequesPago.aspx.cs" Inherits="SistemaGestionPlanilla.Reportes.ReporteChequesPago" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript"> 
        function pageLoad() {
            $(function () {
                ReloadJquery();                
            });
        }    
    </script>
    <div class="m-grid__item m-grid__item--fluid m-wrapper">
        <asp:UpdatePanel runat="server" ID="UpdatePanelPrincipal" UpdateMode="Conditional">
             <Triggers>
                 <asp:PostBackTrigger ControlID="btnExportarExcel"/>
            </Triggers>
            <ContentTemplate>
                <div class="m-content">
                    <div class="m-alert m-alert--icon m-alert--air m-alert--square alert alert-dismissible m--margin-bottom-30" role="alert">
                        <div class="m-alert__icon">
                            <i class="flaticon-exclamation m--font-brand"></i>
                        </div>
                        <div class="m-alert__text">
                            Busque o seleccione a los filtros para generar reporte de pagos.
                        </div>
                    </div>
                    <div class="m-portlet m-portlet--mobile" id="gpConsultaReporte" runat="server">
                        <div class="m-portlet__head">
                            <div class="m-portlet__head-caption">
                                <div class="m-portlet__head-title">
                                    <h3 class="m-portlet__head-text">Generar Reporte de Pagos
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
                                                <a class="nav-link" data-toggle="pill" href="#menu11" id="PillReportePagoCheques">
                                                    <span>
                                                        <i class="fa flaticon-piggy-bank"></i>
                                                    </span>
                                                    <span>Reporte de Pagos</span>
                                                </a>
                                            </li>                                      
                                        </ul>
                                        <!-- end::Nav pills -->

                                        <!-- begin::Tab Content -->
                                        <div class="m-widget28__tab tab-content">
                                            <div id="menu11" class="m-widget28__tab-container tab-pane fade active show">
                                                <div class="m-widget28__tab-items">
                                                    <div class="form-group m-form__group row" style="padding-bottom: 0; border-bottom: none;">
                                                        <div class="col-md-6">
                                                            <asp:Label runat="server" ID="lblFechaInicio">Fecha de Inicio:</asp:Label>
                                                            <div class="m-input-icon m-input-icon--right">
                                                                <asp:TextBox runat="server" CssClass="form-control m-input datepickers" autocomplete="off" placeholder="Seleccione Fecha Inicio" ID="txtFechaInicio" onkeydown="return false;" />
                                                                <span class="m-input-icon__icon m-input-icon__icon--right">
                                                                    <span>
                                                                        <i class="la la-calendar"></i>
                                                                    </span>
                                                                </span>
                                                            </div>
                                                            <span class="m-form__help">Seleccione Fecha de Inicio.</span>
                                                        </div>
                                                        <div class="col-md-6">
                                                            <asp:Label runat="server" ID="Label2">Fecha de Fin:</asp:Label>
                                                            <div class="m-input-icon m-input-icon--right">
                                                                <asp:TextBox runat="server" CssClass="form-control m-input datepickers" autocomplete="off" placeholder="Seleccione Fecha Fin" ID="txtFechaFin" onkeydown="return false;" />
                                                                <span class="m-input-icon__icon m-input-icon__icon--right">
                                                                    <span>
                                                                        <i class="la la-calendar"></i>
                                                                    </span>
                                                                </span>
                                                            </div>
                                                            <span class="m-form__help">Seleccione Fecha de Fin.</span>
                                                        </div>                                            
                                                    </div>
                                                    <div class="form-group m-form__group row" style="padding-bottom: 0; border-bottom: none">
                                                        <div class="col-lg-6">
                                                            <asp:Label ID="txtProyecto" runat="server">Proyecto</asp:Label>
                                                            <div class="m-input-icon m-input-icon--right">
                                                                <asp:DropDownList runat="server" CssClass="form-control m-select2 m-select2-general" ID="cboProyecto">
                                                                </asp:DropDownList>
                                                                <span class="m-input-icon__icon m-input-icon__icon--right">
                                                                    <span>
                                                                        <i class="la la-black-tie"></i>
                                                                    </span>
                                                                </span>
                                                            </div>
                                                            <span class="m-form__help">Seleccionar un proyecto o todos</span>
                                                        </div>
                                                        <div class="col-lg-6">
                                                            <asp:Label ID="txtTipoPago" runat="server">Tipo de pago</asp:Label>
                                                            <div class="m-input-icon m-input-icon--right">
                                                                <asp:DropDownList runat="server" CssClass="form-control m-select2 m-select2-general" ID="cboTipoPago" >
                                                                </asp:DropDownList>
                                                                <span class="m-input-icon__icon m-input-icon__icon--right">
                                                                    <span>
                                                                        <i class="la la-bank"></i>
                                                                    </span>
                                                                </span>
                                                            </div>
                                                            <span class="m-form__help">Seleccione el tipo de pago o todos.</span>
                                                        </div>
                                                    </div>   
                                                    <div class="form-group m-form__group row" style="padding-bottom: 0; border-bottom: none">
                                                        <div class="col-lg-6">
                                                            <asp:Label ID="Label3" runat="server">Medio de pago</asp:Label>
                                                            <div class="m-input-icon m-input-icon--right">
                                                                <asp:DropDownList runat="server" CssClass="form-control m-select2 m-select2-general" ID="cboMedioPago">
                                                                </asp:DropDownList>
                                                                <span class="m-input-icon__icon m-input-icon__icon--right">
                                                                    <span>
                                                                        <i class="la la-money"></i>
                                                                    </span>
                                                                </span>
                                                            </div>
                                                            <span class="m-form__help">Seleccione el medio de pago o todos.</span>
                                                        </div>
                                                        <div class="col-lg-6">
                                                            <asp:Label ID="Label1" runat="server">Estado cheque</asp:Label>
                                                            <div class="m-input-icon m-input-icon--right">
                                                                <asp:DropDownList runat="server" CssClass="form-control m-select2 m-select2-general" ID="cboEstadoCheque" >
                                                                </asp:DropDownList>
                                                                <span class="m-input-icon__icon m-input-icon__icon--right">
                                                                    <span>
                                                                        <i class="la la-get-pocket"></i>
                                                                    </span>
                                                                </span>
                                                            </div>
                                                            <span class="m-form__help">Seleccionar si desea estado de cheque</span>
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 text-center">
                                                        <asp:LinkButton runat="server" CssClass="btn btn-sm btn-primary btn-icon" ID="btnGenerarReporte" OnClick="btnGenerarReporte_Click"><i class="fa fa-th-list" aria-hidden="true"></i>GENERAR REPORTE</asp:LinkButton>
                                                        <asp:LinkButton runat="server" CssClass="btn btn-sm btn-success btn-icon" ID="btnExportarExcel" OnClick="btnExportarExcel_Click"><i class="fa fa-table" aria-hidden="true"></i>DESCARGAR EXCEL</asp:LinkButton>
                                                    </div>
                                                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 text-center">
                                                        <asp:UpdateProgress runat="server" ID="UpdateProgress1">
                                                            <ProgressTemplate>
                                                                <div class="loader_planillon"></div>
                                                            </ProgressTemplate>
                                                        </asp:UpdateProgress>
                                                    </div>
                                                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 text-center">
                                                        <div class="hr-line-dashed"></div>
                                                    </div>
                                                    <div class="form-group m-form__group row" style="padding-bottom: 0; border-bottom: none; margin-top: 20px" id="gpResultadoReporte" runat="server">
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
                                                                            <asp:GridView runat="server" ID="dgvReporte" Width="100%" AutoGenerateColumns="false" CssClass="table table-bordered" RowStyle-BackColor="#FFFFFF" AlternatingRowStyle-BackColor="#F5F5F6" HeaderStyle-BackColor="#F5F5F6" HeaderStyle-Wrap="true" RowStyle-Wrap="true" ShowHeaderWhenEmpty="true" AllowPaging="True" PageSize="10" OnPageIndexChanging="dgvReporte_PageIndexChanging" >
                                                                                <Columns>
                                                                                    <asp:BoundField DataField="DESCRIPCIÓN" HeaderText="DESCRIPCIÓN" HtmlEncode="false" />
                                                                                    <asp:BoundField DataField="PROYECTO" HeaderText="PROYECTO" HtmlEncode="false" />
                                                                                    <asp:BoundField DataField="ESTADO" HeaderText="ESTADO" HtmlEncode="false" />
                                                                                    <asp:BoundField DataField="MEDIOPAGO" HeaderText="MEDIO PAGO" HtmlEncode="false" />
                                                                                    <asp:BoundField DataField="N° COMPROBANTE" HeaderText="N° COMPROBANTE" HtmlEncode="false" />
                                                                                    <asp:BoundField DataField="TIPO DOCUMENTO" HeaderText="TIPO DOCUMENTO" HtmlEncode="false" />
                                                                                    <asp:BoundField DataField="DOCUMENTO DE IDENTIDAD" HeaderText="DOCUMENTO DE IDENTIDAD" HtmlEncode="false" />
                                                                                    <asp:BoundField DataField="PERSONA" HeaderText="PERSONA" HtmlEncode="false" />
                                                                                    <asp:BoundField DataField="MONEDA" HeaderText="MONEDA" HtmlEncode="false" />
                                                                                    <asp:BoundField DataField="TOTAL PAGADO" HeaderText="TOTAL PAGADO" HtmlEncode="false" />
                                                                                    <asp:BoundField DataField="FECHA DE PAGO" HeaderText="FECHA DE PAGO" HtmlEncode="false" />                                                                                    
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
                                        </div>

                                        <!-- end::Tab Content -->
                                    </div>
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
    <script src="<%=ConfigurationManager.AppSettings["AssetsUrl"]%>/assets/js/cheques-reporte.js" type="text/javascript"></script>  

</asp:Content>
