<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ProveedoresRegistrar.aspx.cs" Inherits="SistemaGestionPlanilla.Proveedores.ProveedoresRegistrar" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
       <script type="text/javascript"> 
        function pageLoad() {
            $(function () {
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
                            Gestione el registro y vigencia de proveedores.
                        </div>
                    </div>
                    <div class="m-portlet m-portlet--mobile" >
                        <div class="m-portlet__head">
                            <div class="m-portlet__head-caption">
                                <div class="m-portlet__head-title">
                                    <h3 class="m-portlet__head-text">Registro de proveedores
                                    </h3>
                                </div>
                            </div>
                        </div>
                        <div class="m-portlet__body" id="gpConsultaProveedores" runat="server">
                            <!--begin: Search Form -->
                            <div class="m-form m-form--label-align-right  m--margin-bottom-30">
                                <div class="row align-items-center">
                                    <div class="col-xl-10 order-2 order-xl-1">
                                        <div class="form-group m-form__group row align-items-center">
                                            <div class="col-md-12">
                                                <div class="search-form">
                                                    <div class="input-group">
                                                        <asp:TextBox runat="server" ID="txtTextoBuscar" placeholder="Ingrese una parte del nombre del proveedore o ruc" CssClass="form-control input-lg" MaxLength="200"></asp:TextBox>
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
                                        <a runat="server" id="btnAgregarContratistas" style="color:#fff" class="btn btn-primary m-btn m-btn--custom m-btn--icon m-btn--air m-btn--pill" onserverclick="btnAgregarContratistas_ServerClick">
                                            <span>
                                                <i class="la la-plus"></i>
                                                <span>Agregar nuevo</span>
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
                                    <asp:GridView runat="server" ID="dgvProveedores" Width="100%" AutoGenerateColumns="false" CssClass="table table-bordered" RowStyle-BackColor="#FFFFFF" AlternatingRowStyle-BackColor="#F5F5F6" HeaderStyle-BackColor="#F5F5F6" HeaderStyle-Wrap="true" RowStyle-Wrap="true" ShowHeaderWhenEmpty="true" AllowPaging="True" PageSize="10"  OnRowCommand="dgvProveedores_RowCommand" OnRowDataBound="dgvProveedores_RowDataBound" OnPageIndexChanging="dgvProveedores_PageIndexChanging" >
                                        <Columns>
                                            <asp:BoundField DataField="CodProveedor" HeaderText="CodProveedor" HtmlEncode="false" HeaderStyle-CssClass="hidden" Visible="false" ItemStyle-CssClass="hidden" />
                                            <asp:BoundField DataField="FechaRegistro" HeaderText="FECHA REGISTRO" HtmlEncode="false" />
                                            <asp:BoundField DataField="DescripcionProveedor" HeaderText="DESCRIPCIÓN" HtmlEncode="false" />                                     
                                            <asp:TemplateField HeaderText="OPCION">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lblDetalles" runat="server" CssClass="btn btn-sm btn-default"
                                                        Text='Ver Detalle'
                                                        CommandName="VerDetalleProveedor"
                                                        CommandArgument='<%# Eval("CodProveedor")%>'>
                                                    </asp:LinkButton>
                                                     <asp:LinkButton ID="lblEliminar" runat="server" CssClass="btn btn-sm btn-outline-danger"
                                                        Text='Eliminar'
                                                        CommandName="EliminarContratista"
                                                        CommandArgument='<%# Eval("CodProveedor")%>'>
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
                        <div class="m-portlet__body" id="gpFormularioProveedores" runat="server" style="margin-top:-20px">
                                <!--begin: Search Form -->
                                <div class="m-form m-form--label-align-right  m--margin-bottom-30">
                                    <div class="row align-items-center">
                                        <div class="m-portlet__body"  style="width:100%">
                                            <div id="gpFormularioDatos" runat="server">
                                                <div class="form-group m-form__group row" style="padding-bottom: 0; border-bottom: none">
                                                    <div class="col-md-6">
                                                        <asp:Label runat="server" ID="lblDescripcionProveedor">Descripción Proveedor:</asp:Label>
                                                        <div class="m-input-icon m-input-icon--right">
                                                            <asp:TextBox runat="server" CssClass="form-control m-input"  placeholder="Ingrese el nombre o razón social del contratista" ID="txtDescripcionProveedor" />
                                                            <span class="m-input-icon__icon m-input-icon__icon--right">
                                                                <span>
                                                                    <i class="la la-industry"></i>
                                                                </span>
                                                            </span>                                               
                                                        </div>
                                                        <span class="m-form__help">Ingrese descripción del proveedor.</span>
                                                    </div>
                                                     <div class="col-md-6">
                                                        <asp:Label runat="server" ID="lblRucProveedor">RUC:</asp:Label>
                                                        <div class="m-input-icon m-input-icon--right">
                                                            <asp:TextBox runat="server" CssClass="form-control m-input presup"  placeholder="Ingrese el RUC" ID="txtRucProveedor" />
                                                            <span class="m-input-icon__icon m-input-icon__icon--right">
                                                                <span>
                                                                    <i class="la la-unlock"></i>
                                                                </span>
                                                            </span>                                               
                                                        </div>
                                                        <span class="m-form__help">Ingrese el RUC.</span>
                                                    </div>
                                               
                                                </div>
                                                <div class="form-group m-form__group row" style="padding-bottom: 0; border-bottom: none">
                                                    <div class="col-md-12">
                                                        <asp:Label runat="server" ID="lblRubroProveedores">Rubro:</asp:Label>
                                                        <div class="m-input-icon m-input-icon--right">
                                                            <asp:DropDownList runat="server" CssClass="form-control m-select2 m-select2-general" ID="cboRubroProveedores" >
                                                            </asp:DropDownList>
                                                            <span class="m-input-icon__icon m-input-icon__icon--right">
                                                                <span>
                                                                    <i class="la la-briefcase"></i>
                                                                </span>
                                                            </span>
                                                        </div>
                                                        <span class="m-form__help">Seleccione el rubro del proveedor.</span>
                                                    </div>
                                                </div>
                                                <div class="form-group m-form__group row" style="padding-bottom: 0; border-bottom: none">
                                                    <div class="col-md-6">
                                                        <asp:Label runat="server" ID="lblCiudadProveedor">Ciudad:</asp:Label>
                                                        <div class="m-input-icon m-input-icon--right">
                                                            <asp:TextBox runat="server" CssClass="form-control m-input" placeholder="Ingrese ciudad" ID="txtCiudadProveedor" />
                                                            <span class="m-input-icon__icon m-input-icon__icon--right">
                                                                <span>
                                                                    <i class="la la-map"></i>
                                                                </span>
                                                            </span>
                                                        </div>
                                                        <span class="m-form__help">Ingrese la ciudad del proveedor.</span>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <asp:Label runat="server" ID="lblDireccionProveedor">Dirección:</asp:Label>
                                                        <div class="m-input-icon m-input-icon--right">
                                                            <asp:TextBox runat="server" CssClass="form-control m-input" placeholder="Ingrese dirección" ID="txtDireccionProveedor" />
                                                            <span class="m-input-icon__icon m-input-icon__icon--right">
                                                                <span>
                                                                    <i class="la la-map-marker"></i>
                                                                </span>
                                                            </span>
                                                        </div>
                                                        <span class="m-form__help">Ingrese la dirección del proveedor.</span>
                                                    </div>
                                                </div>
                                                <div class="form-group m-form__group row" style="padding-bottom: 0; border-bottom: none">
                                                    <div class="col-md-6">
                                                        <asp:Label runat="server" ID="lblCorreoProveedor">Correo:</asp:Label>
                                                        <div class="m-input-icon m-input-icon--right">
                                                            <asp:TextBox runat="server" CssClass="form-control m-input" placeholder="Ingrese correo" ID="txtCorreoProveedor" />
                                                            <span class="m-input-icon__icon m-input-icon__icon--right">
                                                                <span>
                                                                    <i class="la la-envelope"></i>
                                                                </span>
                                                            </span>
                                                        </div>
                                                        <span class="m-form__help">Ingrese el correo del proveedor.</span>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <asp:Label runat="server" ID="lblTelefonoProveedor">Teléfono:</asp:Label>
                                                        <div class="m-input-icon m-input-icon--right">
                                                            <asp:TextBox runat="server" CssClass="form-control m-input" placeholder="Ingrese teléfono" ID="txtTelefonoProveedor" />
                                                            <span class="m-input-icon__icon m-input-icon__icon--right">
                                                                <span>
                                                                    <i class="la la-phone"></i>
                                                                </span>
                                                            </span>
                                                        </div>
                                                        <span class="m-form__help">Ingrese el teléfono del proveedor.</span>
                                                    </div>
                                                </div>
                                                <div class="form-group m-form__group row" style="margin-top: 20px" id="Div1" runat="server">
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
                                        </div>
                                        
                                    </div>
                                </div>
                            </div>
                    </div>
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 no-padding" id="gpEnlaceExterno" style="display: none" runat="server">
                        <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                            <div class="form-group">
                            
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
            $('#navMenuProveedores').addClass("m-menu__item--open");
            $('#navMenuProveedores').addClass("m-menu__item--expanded");
            $('#navSubMenuAgregarProveedores').addClass("m-menu__item--active");
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
        function button_click(objTextBox, objBtnID) {
            if (window.event.keyCode == 13) {
                document.getElementById(objBtnID).focus();
                document.getElementById(objBtnID).click();
            }
        }
    </script>
</asp:Content>
