<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ProveedoresRegistroPagos.aspx.cs" Inherits="SistemaGestionPlanilla.Proveedores.ProveedoresRegistroPagos" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <script type="text/javascript"> 
        function pageLoad() {
            $(function () {           
                ReloadJqueryDataTable();
                ReloadJqueryDatePicker();
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
                            Gestione los pagos a proveedores que intervienen en su planilla.
                        </div>
                    </div>
                    <div class="m-portlet m-portlet--mobile" >
                        <div class="m-portlet__head">
                            <div class="m-portlet__head-caption">
                                <div class="m-portlet__head-title">
                                    <h3 class="m-portlet__head-text">Pago de Proveedores
                                    </h3>
                                </div>
                            </div>

                        </div>
                         <div class="m-portlet__body" id="gpFormularioPagos" runat="server" style="margin-top:-20px">
                                <!--begin: Search Form -->
                                <div class="m-form m-form--label-align-right  m--margin-bottom-30">
                                    <div class="row align-items-center">
                                        <div class="m-portlet__body"  style="width:100%">
                                            <div id="gpContratosContratista" runat="server">
                                                <div class="form-group m-form__group row" style="padding-bottom: 0; border-bottom: none">
                                                    <div class="col-md-12">
                                                        <asp:Label runat="server" ID="lblContratistas">Proveedores:</asp:Label>
                                                        <div class="m-input-icon m-input-icon--right">
                                                            <asp:DropDownList runat="server" CssClass="form-control m-select2 m-select2-general" ID="cboProveedores"  OnSelectedIndexChanged="cboProveedores_SelectedIndexChanged"  AutoPostBack="true" >
                                                            </asp:DropDownList>
                                                            <span class="m-input-icon__icon m-input-icon__icon--right">
                                                                <span>
                                                                    <i class="la la-industry"></i>
                                                                </span>
                                                            </span>                                               
                                                        </div>
                                                        <span class="m-form__help">Seleccione al contratista.</span>
                                                    </div>                                                                                             
                                                </div>
                                                <div id="gpListadoProyectos" runat="server">
                                                    <div class="m-portlet__head" style="margin-top: 10px; border-bottom: none; padding-left: 0px">
                                                        <div class="m-portlet__head-caption">
                                                            <div class="m-portlet__head-title">
                                                                <span class="m-portlet__head-icon">
                                                                    <i class="flaticon-statistics"></i>
                                                                </span>
                                                                <h3 class="m-portlet__head-text" style="font-weight: 400; font-size: 1.2rem" id="lblResultado" runat="server"></h3>

                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="padding-left: 0px">
                                                        <div class="m-datable table-responsive">
                                                            <asp:GridView runat="server" ID="dgvProyectosProveedores" Width="100%" AutoGenerateColumns="false" CssClass="table table-bordered" RowStyle-BackColor="#FFFFFF" AlternatingRowStyle-BackColor="#F5F5F6" HeaderStyle-BackColor="#F5F5F6" HeaderStyle-Wrap="true" RowStyle-Wrap="true" ShowHeaderWhenEmpty="true" AllowPaging="True" PageSize="5" OnRowCommand="dgvProyectosProveedores_RowCommand" OnRowDataBound="dgvProyectosProveedores_RowDataBound" OnPageIndexChanging="dgvProyectosProveedores_PageIndexChanging">
                                                                <Columns>
                                                                    <asp:BoundField DataField="CodProyecto" HeaderText="CodContrato" HtmlEncode="false" HeaderStyle-CssClass="hidden" Visible="false" ItemStyle-CssClass="hidden" />
                                                                    <asp:BoundField DataField="DescripcionProyecto" HeaderText="PROYECTO" HtmlEncode="false" />
                                                                    <asp:BoundField DataField="DescripcionProveedor" HeaderText="PROVEEDOR" HtmlEncode="false" />                                                                    
                                                                    <asp:BoundField DataField="MontoTotal" HeaderText="MONTO TOTAL" HtmlEncode="false" />
                                                                    <asp:BoundField DataField="TotalPagado" HeaderText="TOTAL PAGADO" HtmlEncode="false" />                                                                                                                                                                                                            
                                                                    <asp:TemplateField HeaderText="OPCION">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lblPago" runat="server" CssClass="btn btn-sm btn-default"
                                                                                Text='Generar Pago'
                                                                                CommandName="GenerarPagoContratista"
                                                                                CommandArgument='<%# Eval("CodProyecto")%>'>
                                                                                
                                                                            </asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                     <asp:TemplateField HeaderText="OPCION">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lblDetalles" runat="server" CssClass="btn btn-sm btn-default"
                                                                                Text='Ver Detalles de Pago'
                                                                                CommandName="VerDetallesPago"
                                                                                CommandArgument='<%# Eval("CodProyecto")%>'>
                                                                                
                                                                            </asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                                <EmptyDataTemplate>
                                                                    No hay contratos para mostrar
                                                                </EmptyDataTemplate>
                                                                <PagerStyle HorizontalAlign="Right" CssClass="grid-pager-silver" />
                                                            </asp:GridView>
                                                        </div>
                                                    </div>

                                                </div>
                                                <div id="gpListadoProveedorPago" runat="server" style="margin-top:20px">
                                                    <div class="form-group m-form__group row" style="padding-bottom: 0; border-bottom: none">
                                                        <div class="col-md-12">
                                                            <!--begin::Portlet-->
                                                            <div class="m-portlet m-portlet--creative m-portlet--first m-portlet--bordered-semi">
                                                                <div class="m-portlet__head">
                                                                    <div class="m-portlet__head-caption">
                                                                        <div class="m-portlet__head-title">
                                                                            <span class="m-portlet__head-icon m--hide">
                                                                                <i class="flaticon-statistics"></i>
                                                                            </span>
                                                                            <h3 class="m-portlet__head-text">Listado de pagos realizados por&nbsp;<span class="m--font-brand" style="font-size: 1.0rem;font-weight: 600;" id="lblContratoListado" runat="server"></span>.
                                                                            </h3>
                                                                            <h2 class="m-portlet__head-label m-portlet__head-label--warning">
                                                                                <span>Pagos Realizados</span>
                                                                            </h2>
                                                                        </div>
                                                                    </div>
                                                        
                                                                </div>
                                                                <div class="m-portlet__body">
                                                                    <div id="tbListadoPagosRealizados" runat="server">
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        
                                                    </div>
                                                </div>                                                  
                                            </div>
                                            <div id="gpFormularioPago" runat="server">
                                                <div class="form-group m-form__group row" style="padding-bottom: 0; border-bottom: none">
                                                    <div class="col-md-6">
                                                        <asp:Label runat="server" ID="Label1">Proveedor:</asp:Label>
                                                        <div class="m-input-icon m-input-icon--right">
                                                             <asp:TextBox runat="server" CssClass="form-control m-input" ReadOnly="true" placeholder="Número de cheque" ID="txtProveedorDetalle" />
                                                            <span class="m-input-icon__icon m-input-icon__icon--right">
                                                                <span>
                                                                    <i class="la la-building"></i>
                                                                </span>
                                                            </span>
                                                        </div>
                                                        <span class="m-form__help">Proveedor.</span>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <asp:Label runat="server" ID="Label2">Descripción Proyecto:</asp:Label>
                                                        <div class="m-input-icon m-input-icon--right">
                                                            <asp:TextBox runat="server" CssClass="form-control m-input" ReadOnly="true" placeholder="Descripción Contrato" ID="txtDescripcionProyecto" />
                                                            <span class="m-input-icon__icon m-input-icon__icon--right">
                                                                <span>
                                                                    <i class="la la-clipboard"></i>
                                                                </span>
                                                            </span>
                                                        </div>
                                                        <span class="m-form__help">Descripción Proyecto.</span>
                                                    </div>
                                                </div>
                                                <div class="form-group m-form__group row" style="padding-bottom: 0; border-bottom: none">
                                                    <div class="col-md-6">
                                                        <asp:Label runat="server" ID="lblModoPago">Modo Pago:</asp:Label>
                                                        <div class="m-input-icon m-input-icon--right">
                                                            <asp:DropDownList runat="server" CssClass="form-control m-input" ID="cboModoPago" OnSelectedIndexChanged="cboModoPago_SelectedIndexChanged" AutoPostBack="true">
                                                            </asp:DropDownList>
                                                            <span class="m-input-icon__icon m-input-icon__icon--right">
                                                                <span>
                                                                    <i class="la la-lightbulb-o"></i>
                                                                </span>
                                                            </span>
                                                        </div>
                                                        <span class="m-form__help">Seleccione un modo de pago.</span>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <asp:Label runat="server" ID="lblNumeroCheque">Nº Cheque:</asp:Label>
                                                        <div class="m-input-icon m-input-icon--right">
                                                             <asp:TextBox runat="server" CssClass="form-control m-input" ReadOnly="true" placeholder="Número de cheque" ID="txtNumeroCheque" />
                                                            <span class="m-input-icon__icon m-input-icon__icon--right">
                                                                <span>
                                                                    <i class="la la-credit-card"></i>
                                                                </span>
                                                            </span>
                                                        </div>
                                                        <span class="m-form__help">Número de cheque.</span>
                                                    </div>
                                                </div>
                                                <div class="form-group m-form__group row" style="padding-bottom: 0; border-bottom: none">                                                
                                                    <div class="col-md-6">
                                                        <asp:Label runat="server" ID="lblFechaPago">Fecha de Pago:</asp:Label>
                                                        <div class="m-input-icon m-input-icon--right">
                                                            <asp:TextBox runat="server" CssClass="form-control m-input datepickers" placeholder="Seleccione Fecha de Pago" ID="txtFechaPago" />
                                                            <span class="m-input-icon__icon m-input-icon__icon--right">
                                                                <span>
                                                                    <i class="la la-calendar"></i>
                                                                </span>
                                                            </span>
                                                        </div>
                                                        <span class="m-form__help">Seleccione Fecha de Pago.</span>
                                                    </div>
                                                </div>
                                                <div class="form-group m-form__group row" style="padding-bottom: 0; border-bottom: none">
                                                    <div class="col-md-6">
                                                        <asp:Label runat="server" ID="lblTipoReciboPago">Tipo Recibo de Pago:</asp:Label>
                                                        <div class="m-input-icon m-input-icon--right">
                                                            <asp:DropDownList runat="server" CssClass="form-control m-input" ID="cboTipoReciboPago">
                                                            </asp:DropDownList>
                                                            <span class="m-input-icon__icon m-input-icon__icon--right">
                                                                <span>
                                                                    <i class="la la-clipboard"></i>
                                                                </span>
                                                            </span>
                                                        </div>
                                                        <span class="m-form__help">Seleccione un tipo de recibo de pago.</span>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <asp:Label runat="server" ID="lblNumReciboPago">Nº Recibo de Pago:</asp:Label>
                                                        <div class="m-input-icon m-input-icon--right">
                                                            <asp:TextBox runat="server" CssClass="form-control m-input" placeholder="Ingrese el número del recibo de pago" ID="txtNumRecigoPago" />
                                                            <span class="m-input-icon__icon m-input-icon__icon--right">
                                                                <span>
                                                                    <i class="la la-pencil"></i>
                                                                </span>
                                                            </span>
                                                        </div>
                                                        <span class="m-form__help">Número de recibo de pago.</span>
                                                    </div>
                                                </div>
                                                <div class="form-group m-form__group row" style="padding-bottom: 0; border-bottom: none">                                                    
                                                    <div class="col-md-6">
                                                        <asp:Label runat="server" ID="lblMontoPagar">Monto a Pagar:</asp:Label>
                                                        <div class="m-input-icon m-input-icon--right">
                                                             <asp:TextBox runat="server" CssClass="form-control m-input" ReadOnly="true" placeholder="Monto a pagar" ID="txtMontoPagar" />
                                                            <span class="m-input-icon__icon m-input-icon__icon--right">
                                                                <span>
                                                                    <i class="la la-money"></i>
                                                                </span>
                                                            </span>
                                                        </div>
                                                        <span class="m-form__help">Ingrese monto a pagar.</span>
                                                    </div>
                                                </div>
                                                <div class="form-group m-form__group row" style="margin-top: 20px">
                                                    <div class="col-md-12" style="text-align: center">
                                                        <div class="m-btn-group m-btn-group--pill btn-group" role="group" aria-label="First group">
                                                            <asp:LinkButton runat="server" CssClass="m-btn btn btn-secondary" ID="btnRegresar" OnClick="btnRegresar_Click">
                                                            <i class="la la-mail-reply"></i> Regresar
                                                            </asp:LinkButton>

                                                            <asp:LinkButton runat="server" CssClass="m-btn btn btn-primary" ID="btnGuardar" OnClick="btnGuardar_Click">
                                                            <i class="la la-floppy-o"></i> Guardar
                                                            </asp:LinkButton>
                                                        </div>
                                                    </div>

                                                </div>
                                            </div>
                                            <div id="gpReporteRVIndividual" runat="server">
                                                    <div class="col-md-12 m--margin-top-50">
                                                        <div class="col-md-12">
                                                            <center>    
                                                            <rsweb:ReportViewer ID="ReportViewer1" runat="server" font-names="Verdana" font-size="8pt" waitmessagefont-names="Verdana" waitmessagefont-size="14pt" backcolor="#F2F2F2" width="790px" height="415px" bordercolor="#2F4050" borderstyle="Solid" borderwidth="1px">
                                                               <LocalReport ReportPath="Reportes\RViewer\Cheque.rdlc">
                                                                    <DataSources>
                                                                        <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" Name="DataSet1" />
                                                                    </DataSources>
                                                                </LocalReport>
                                                            </rsweb:ReportViewer>                                                                                                    
                                                                <div style="margin: 20px auto;">
                                                                    <div class="col-md-12 text-center">
                                                                        <asp:LinkButton ID="btnRegresarImpresion" CssClass="btn btn-primary" runat="server" OnClick="btnRegresarImpresion_Click"><span class="icono-boton"><i class="la la-chevron-circle-left"></i></span> REGRESAR</asp:LinkButton>
                                                                    </div>
                                                                </div>
                                                            </center>
                                                            <asp:SqlDataSource ID="SqlDataSource1" runat="server"></asp:SqlDataSource>
                                                        </div>
                                                    </div>
                                                </div>   
                                        </div>                                        
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
                                <asp:LinkButton runat="server" ID="btnDetallesPagoContrato" OnClick="btnDetallesPagoContrato_Click"></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnImprimirCheque" OnClick="btnImprimirCheque_Click"></asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal inmodal fade" id="gpEliminarPagoContratista" tabindex="-1" role="dialog" aria-hidden="true">
                    <div class="modal-dialog modal-lg">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h5 class="modal-title" id="exampleModalLabel">Confirmación</h5>
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">×</span>
                                </button>
                            </div>
                            <div class="modal-body">
                                <p style="font-weight: 400">¿Está seguro(a) que desea eliminar el <strong>registro de pago</strong> del proveedor?</p>
                            </div>
                            <div class="modal-footer">
                                <asp:LinkButton runat="server" data-dismiss="modal" CssClass="btn btn-sm btn-secondary btn-icon"><i class="fa fa-arrow-circle-left" aria-hidden="true"></i> REGRESAR</asp:LinkButton>
                                <asp:LinkButton ID="btnConfirmarEliminarPagoContratista" CssClass="btn btn-sm btn-primary btn-icon" runat="server" OnClick="btnConfirmarEliminarPagoContratista_Click"><i class="fa fa-check-circle" aria-hidden="true"></i> CONFIRMAR</asp:LinkButton>

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
            $('#navMenuProveedores').addClass("m-menu__item--open");
            $('#navMenuProveedores').addClass("m-menu__item--expanded");
            $('#navSubMenuPagoProveedores').addClass("m-menu__item--active");
        })

        function disableReadOnly() {
            $("#name").prop('readonly', false);
        }
        function ReloadJqueryDataTable() {
            var script = document.createElement("script");
            script.setAttribute("type", "text/javascript");
            script.setAttribute("src", "/planilla/assets/js/basic.js");
            document.getElementsByTagName("head")[0].appendChild(script);
        }
        function ReloadJqueryDatePicker() {
            var script = document.createElement("script");
            script.setAttribute("type", "text/javascript");
            script.setAttribute("src", "/planilla/assets/js/bootstrap-datepicker.js");
            document.getElementsByTagName("head")[0].appendChild(script);
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
        function AbriDetalles(event, id) {
            $.cookie("CodPagoProveedorDetalles", id);
            __doPostBack('<%=btnDetallesPagoContrato.UniqueID%>', '');
        }
        function EliminarPagoProveedor(event, id) {
            $.cookie("CodPagoProveedorEliminar", id);
              $('#gpEliminarPagoContratista').modal('toggle');
        }
          function ImprimirCheque(event, id) {
              $.cookie("CodChequeImprimir", id);              
              __doPostBack('<%=btnImprimirCheque.UniqueID%>', '');
        }
         function DesaparecerBackDrop() {
            $('.modal-backdrop').removeClass("modal-backdrop");
        }
    </script>
</asp:Content>
