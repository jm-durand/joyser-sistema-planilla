<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ContratistasRegistroContrato.aspx.cs" Inherits="SistemaGestionPlanilla.Contratistas.ContratistasRegistroContrato" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <script type="text/javascript"> 
        function pageLoad() {
            $(function () {            

                 ReloadJqueryDatePicker();
                  $(".presup").on("keypress keyup blur", function (event) {
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
                            Gestione los contratos de los contratistas vigentes de su planilla.
                        </div>
                    </div>
                    <div class="m-portlet m-portlet--mobile" >
                        <div class="m-portlet__head">
                            <div class="m-portlet__head-caption">
                                <div class="m-portlet__head-title">
                                    <h3 class="m-portlet__head-text">Registro de contratos
                                    </h3>
                                </div>
                            </div>

                        </div>
                        <div class="m-portlet__body" id="gpConsultaContratos" runat="server">

                            <!--begin: Search Form -->
                            <div class="m-form m-form--label-align-right  m--margin-bottom-30">
                                <div class="row align-items-center">
                                    <div class="col-xl-10 order-2 order-xl-1">
                                        <div class="form-group m-form__group row align-items-center">
                                            <div class="col-md-12">
                                                <div class="search-form">
                                                    <div class="input-group">
                                                        <asp:TextBox runat="server" ID="txtTextoBuscar" placeholder="Ingrese una parte del nombre del proyecto" CssClass="form-control input-lg" MaxLength="200"></asp:TextBox>
                                                        <div class="input-group-btn">
                                                            <asp:LinkButton runat="server" CssClass="m-btn btn btn-primary" ID="btnBuscar" OnClick="btnBuscar_Click">Buscar</asp:LinkButton>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                             
                                </div>
                                <div class="row align-items-center" style="padding-top: 20px">
                                    <div class="col-xl-12 order-1 order-xl-2 m--align-left">
                                        <a runat="server" id="btnRegistrarContrato"  onserverclick="btnRegistrarContrato_ServerClick"  style="color:#fff"  class="btn btn-primary m-btn m-btn--custom m-btn--icon m-btn--air m-btn--pill">
                                            <span>
                                                <i class="la la-plus"></i>
                                                <span>Agregar contrato</span>
                                            </span>
                                        </a>
                                        <div class="m-separator m-separator--dashed d-xl-none"></div>
                                    </div>
                                </div>
                            </div>

                            <!--end: Search Form -->

                            <!--begin: Datatable -->
                            
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
                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="padding-left:0px">
                                <div class="m-datable table-responsive">
                                    <asp:GridView runat="server" ID="dgvContratos" Width="100%" AutoGenerateColumns="false" CssClass="table table-bordered" RowStyle-BackColor="#FFFFFF" AlternatingRowStyle-BackColor="#F5F5F6" HeaderStyle-BackColor="#F5F5F6" HeaderStyle-Wrap="true" RowStyle-Wrap="true" ShowHeaderWhenEmpty="true" AllowPaging="True" PageSize="10"  OnRowDataBound="dgvContratos_RowDataBound" OnRowCommand="dgvContratos_RowCommand" OnPageIndexChanging="dgvContratos_PageIndexChanging" >
                                        <Columns>
                                            <asp:BoundField DataField="CodContrato" HeaderText="CodProyecto" HtmlEncode="false" HeaderStyle-CssClass="hidden" Visible="false" ItemStyle-CssClass="hidden" />
                                            <asp:BoundField DataField="FechaInicio" HeaderText="FECHA INICIO" HtmlEncode="false" />
                                            <asp:BoundField DataField="DescripcionTrabajoContrato" HeaderText="DESCRIPCIÓN" HtmlEncode="false" />
                                            <asp:BoundField DataField="Contratista" HeaderText="CONTRATISTA" HtmlEncode="false" /> 
                                            <asp:BoundField DataField="MontoTotal" HeaderText="MONTO" HtmlEncode="false" /> 
                                            <asp:BoundField DataField="DescripcionTipoPago" HeaderText="TIPO PAGO" Visible="false" HeaderStyle-CssClass="hidden" HtmlEncode="false" /> 
                                            <asp:BoundField DataField="DescripcionProyecto" HeaderText="PROYECTO" HtmlEncode="false" /> 
                                            <asp:BoundField DataField="FlagActivo" HeaderText="ESTADO" HtmlEncode="false" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="header-center" />
                                            <asp:TemplateField HeaderText="OPCION">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lblDetalles" runat="server" CssClass="btn btn-sm btn-default"
                                                        Text='Ver Detalle'
                                                        CommandName="VerDetalleContrato"
                                                        CommandArgument='<%# Eval("CodContrato")%>'>
                                                    </asp:LinkButton>
                                                     <asp:LinkButton ID="lblEliminar" runat="server" CssClass="btn btn-sm btn-outline-danger"
                                                        Text='Eliminar'
                                                        CommandName="EliminarContrato"
                                                        CommandArgument='<%# Eval("CodContrato")%>'>
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
                           
                            <!--end: Datatable -->
                        </div>
                         <div class="m-portlet__body" id="gpFormularioContrato" runat="server" style="margin-top:-20px">
                                <!--begin: Search Form -->
                                <div class="m-form m-form--label-align-right  m--margin-bottom-30">
                                    <div class="row align-items-center">
                                        <div class="m-portlet__body"  style="width:100%">
                                            <div id="gpFormularioPeriodo" runat="server">
                                                <div class="form-group m-form__group row" style="padding-bottom: 0; border-bottom: none">
                                                    <div class="col-md-6">
                                                        <asp:Label runat="server" ID="lblContratista">Contratista:</asp:Label>
                                                        <div class="m-input-icon m-input-icon--right">
                                                            <asp:DropDownList runat="server" CssClass="form-control m-input" ID="cboContratista">
                                                            </asp:DropDownList>
                                                            <span class="m-input-icon__icon m-input-icon__icon--right">
                                                                <span>
                                                                    <i class="la la-industry"></i>
                                                                </span>
                                                            </span>                                               
                                                        </div>
                                                        <span class="m-form__help">Seleccione un contratista.</span>
                                                    </div>
                                                     <div class="col-md-6">
                                                        <asp:Label runat="server" ID="lblProyecto">Proyecto:</asp:Label>
                                                        <div class="m-input-icon m-input-icon--right">
                                                            <asp:DropDownList runat="server" CssClass="form-control m-input" ID="cboProyecto">
                                                            </asp:DropDownList>
                                                            <span class="m-input-icon__icon m-input-icon__icon--right">
                                                                <span>
                                                                    <i class="la la-money"></i>
                                                                </span>
                                                            </span>                                               
                                                        </div>
                                                        <span class="m-form__help">Seleccione un proyecto.</span>
                                                    </div>                                               
                                                </div>
                                                <div class="form-group m-form__group row" style="padding-bottom: 0; border-bottom: none">
                                                    <div class="col-md-12">
                                                        <asp:Label runat="server" ID="lblLaborContratista">Descripción Labor Contratista:</asp:Label>
                                                        <div class="m-input-icon m-input-icon--right">
                                                            <asp:TextBox runat="server" CssClass="form-control m-input" placeholder="Ingrese labor contratista" ID="txtLaborContratoContratista" />
                                                            <span class="m-input-icon__icon m-input-icon__icon--right">
                                                                <span>
                                                                    <i class="la la-wrench"></i>
                                                                </span>
                                                            </span>
                                                        </div>
                                                        <span class="m-form__help">Ingrese labor del contratista.</span>
                                                    </div>                                                 
                                                </div>
                                                <div class="form-group m-form__group row" style="padding-bottom: 0; border-bottom: none">
                                                    <div class="col-md-6">
                                                        <asp:Label runat="server" ID="lblTipoPago">Modo Pago:</asp:Label>
                                                        <div class="m-input-icon m-input-icon--right">
                                                            <asp:DropDownList runat="server" CssClass="form-control m-input" ID="cboTipoPago">
                                                            </asp:DropDownList>
                                                            <span class="m-input-icon__icon m-input-icon__icon--right">
                                                                <span>
                                                                    <i class="la la-lightbulb-o"></i>
                                                                </span>
                                                            </span>
                                                        </div>
                                                        <span class="m-form__help">Ingrese modo de pago.</span>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <asp:Label runat="server" ID="lblMontoTotal">Monto Total:</asp:Label>
                                                        <div class="m-input-icon m-input-icon--right">
                                                            <asp:TextBox runat="server" CssClass="form-control m-input presup" placeholder="Ingrese el monto total" ID="txtMontoTotal" />
                                                            <span class="m-input-icon__icon m-input-icon__icon--right">
                                                                <span>
                                                                    <i class="la la-money"></i>
                                                                </span>
                                                            </span>
                                                        </div>
                                                        <span class="m-form__help">Ingrese el monto total.</span>
                                                    </div>
                                                </div>
                                                  <div class="form-group m-form__group row" style="padding-bottom: 0; border-bottom: none">
                                                    <div class="col-md-6">
                                                        <asp:Label runat="server" ID="lblFechaInicioContrato">Fecha Inicio Contrato:</asp:Label>
                                                        <div class="m-input-icon m-input-icon--right">
                                                            <asp:TextBox runat="server" CssClass="form-control m-input datepickers" placeholder="Ingrese fecha inicio" ID="txtFechaInicioContrato" />
                                                            <span class="m-input-icon__icon m-input-icon__icon--right">
                                                                <span>
                                                                    <i class="la la-calendar"></i>
                                                                </span>
                                                            </span>
                                                        </div>
                                                        <span class="m-form__help">Ingrese fecha inicio de contrato.</span>
                                                    </div>                                                    
                                                </div>
                                                <div class="form-group m-form__group row" style="margin-top: 20px" >
                                                    <div class="col-md-12" style="text-align: center">
                                                        <div class="m-btn-group m-btn-group--pill btn-group" role="group" aria-label="First group">
                                                            <asp:LinkButton runat="server" CssClass="m-btn btn btn-secondary" ID="btnRegresar" OnClick="btnRegresar_Click" >
                                                            <i class="la la-mail-reply"></i> Regresar
                                                            </asp:LinkButton>

                                                            <asp:LinkButton runat="server" CssClass="m-btn btn btn-primary" ID="btnGuardar"  OnClick="btnGuardar_Click">
                                                            <i class="la la-floppy-o"></i> Guardar
                                                            </asp:LinkButton>
                                                        </div>
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
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    
    <script src="<%=ConfigurationManager.AppSettings["AssetsUrl"]%>/assets/js/jquery-3.2.1.js" type="text/javascript"></script>
    <script>
        $(document).ready(function () {
            $('#navMenuContratistas').addClass("m-menu__item--open");
            $('#navMenuContratistas').addClass("m-menu__item--expanded");
            $('#navSubMenuContratosContratistas').addClass("m-menu__item--active");
        })
 
        function disableReadOnly() {
            $("#name").prop('readonly', false);
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
  
    </script>
</asp:Content>
