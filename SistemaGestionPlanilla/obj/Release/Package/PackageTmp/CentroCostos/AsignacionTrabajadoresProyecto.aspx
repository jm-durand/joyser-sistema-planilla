<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="AsignacionTrabajadoresProyecto.aspx.cs" Inherits="SistemaGestionPlanilla.CentroCostos.AsignacionTrabajadoresProyecto" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript"> 
        function pageLoad() {
            $(function () {
                $('.m-checkbox').click(function () {

                    populateTextInput();
                });

                ReloadJqueryDataTable();
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
                            Asigne trabajadores a los centro de costos generados.
                        </div>
                    </div>
                    <div class="m-portlet m-portlet--mobile" >
                        <div class="m-portlet__head">
                            <div class="m-portlet__head-caption">
                                <div class="m-portlet__head-title">
                                    <h3 class="m-portlet__head-text">Asignación trabajadores
                                    </h3>
                                </div>
                            </div>

                        </div>
                        <div class="m-portlet__body" id="gpConsultaProyectoPlanilla" runat="server">

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
                                                            <asp:LinkButton runat="server" CssClass="m-btn btn btn-primary" ID="btnBuscar">Buscar</asp:LinkButton>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                             
                                </div>
                                <div class="row align-items-center" style="padding-top: 20px">
                                    <div class="col-xl-12 order-1 order-xl-2 m--align-left">
                                        <a runat="server" id="btnAsignarTrabajadores"  onserverclick="btnAsignarTrabajadores_ServerClick"  style="color:#fff"  class="btn btn-primary m-btn m-btn--custom m-btn--icon m-btn--air m-btn--pill">
                                            <span>
                                                <i class="la la-plus"></i>
                                                <span>Agregar asignación centro costos</span>
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
                                    <asp:GridView runat="server" ID="dgvProyectos" Width="100%" AutoGenerateColumns="false" CssClass="table table-bordered" RowStyle-BackColor="#FFFFFF" AlternatingRowStyle-BackColor="#F5F5F6" HeaderStyle-BackColor="#F5F5F6" HeaderStyle-Wrap="true" RowStyle-Wrap="true" ShowHeaderWhenEmpty="true" AllowPaging="True" PageSize="10" OnRowDataBound="dgvProyectos_RowDataBound" OnRowCommand="dgvProyectos_RowCommand" OnPageIndexChanging="dgvProyectos_PageIndexChanging"  >
                                        <Columns>
                                            <asp:BoundField DataField="CodProyecto" HeaderText="CodProyecto" HtmlEncode="false" HeaderStyle-CssClass="hidden" Visible="false" ItemStyle-CssClass="hidden" />
                                            <asp:BoundField DataField="FechaRegistro" HeaderText="FECHA REGISTRO" HtmlEncode="false" />
                                            <asp:BoundField DataField="Nombres" HeaderText="DESCRIPCIÓN" HtmlEncode="false" />
                                            <asp:BoundField DataField="Presupuesto" HeaderText="PRESUPUESTO" HtmlEncode="false" /> 
                                            <asp:BoundField DataField="Vigencia" HeaderText="VIGENCIA" HtmlEncode="false" /> 
                                            <asp:BoundField DataField="CantidadTrabajadores" HeaderText="CANT. TRABAJADORES" HtmlEncode="false" /> 
                                            <asp:BoundField DataField="FlagActivo" HeaderText="ESTADO" HtmlEncode="false" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="header-center" />
                                            <asp:TemplateField HeaderText="OPCION">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lblDetalles" runat="server" CssClass="btn btn-sm btn-default"
                                                        Text='Ver Detalle'
                                                        CommandName="VerDetalleProyectoPlanilla"
                                                        CommandArgument='<%# Eval("CodProyecto")%>'>
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
                         <div class="m-portlet__body" id="gpFormularioProyecto" runat="server" style="margin-top:-20px">
                                <!--begin: Search Form -->
                                <div class="m-form m-form--label-align-right  m--margin-bottom-30">
                                    <div class="row align-items-center">
                                        <div class="m-portlet__body"  style="width:100%">
                                            <div id="gpFormularioPeriodo" runat="server">
                                                <div class="form-group m-form__group row" style="padding-bottom: 0; border-bottom: none">
                                                    <div class="col-md-6">
                                                        <asp:Label runat="server" ID="lblNombreProyecto">Proyectos:</asp:Label>
                                                        <div class="m-input-icon m-input-icon--right">
                                                            <asp:DropDownList runat="server" CssClass="form-control m-input" ID="cboProyectoPlanilla" OnSelectedIndexChanged="cboProyectoPlanilla_SelectedIndexChanged" AutoPostBack="true" >
                                                            </asp:DropDownList>
                                                            <span class="m-input-icon__icon m-input-icon__icon--right">
                                                                <span>
                                                                    <i class="la la-industry"></i>
                                                                </span>
                                                            </span>                                               
                                                        </div>
                                                        <span class="m-form__help">Seleccione un proyecto.</span>
                                                    </div>
                                                     <div class="col-md-6">
                                                        <asp:Label runat="server" ID="lblPresupuesto">Presupuesto:</asp:Label>
                                                        <div class="m-input-icon m-input-icon--right">
                                                            <asp:TextBox runat="server" CssClass="form-control m-input" Enabled="false"  placeholder="Ingrese el presupuesto" ID="txtPresupuesto" />
                                                            <span class="m-input-icon__icon m-input-icon__icon--right">
                                                                <span>
                                                                    <i class="la la-money"></i>
                                                                </span>
                                                            </span>                                               
                                                        </div>
                                                        <span class="m-form__help">Presupesto.</span>
                                                    </div>
                                               
                                                </div>
                                                <div id="gpListadoTrabajadoresAsignados" runat="server">
                                                    <div class="m-portlet__head" style="margin-top: 20px; border-bottom: none; padding-left: 0px">
                                                        <div class="m-portlet__head-caption">
                                                            <div class="m-portlet__head-title">
                                                                <span class="m-portlet__head-icon">
                                                                    <i class="flaticon-statistics"></i>
                                                                </span>
                                                                <h3 class="m-portlet__head-text" style="font-weight: 400; font-size: 1.2rem" id="lblResultadoAsignados" runat="server"></h3>

                                                            </div>
                                                        </div>

                                                    </div>
                                                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="padding-left: 0px">
                                                        <div class="m-datable table-responsive">
                                                            <asp:GridView runat="server" ID="dgvTrabajadoresAsignados" Width="100%" AutoGenerateColumns="false" CssClass="table table-bordered" RowStyle-BackColor="#FFFFFF" AlternatingRowStyle-BackColor="#F5F5F6" HeaderStyle-BackColor="#F5F5F6" HeaderStyle-Wrap="true" RowStyle-Wrap="true" ShowHeaderWhenEmpty="true" AllowPaging="True" PageSize="10" OnPageIndexChanging="dgvTrabajadoresAsignados_PageIndexChanging"  OnRowCommand="dgvTrabajadoresAsignados_RowCommand" >
                                                                <Columns>
                                                                    <asp:BoundField DataField="CodTrabajador" HeaderText="CodTrabajador" HtmlEncode="false" HeaderStyle-CssClass="hidden" Visible="false" ItemStyle-CssClass="hidden" />
                                                                    <asp:BoundField DataField="Nombres" HeaderText="TRABAJADOR" HtmlEncode="false" />
                                                                    <asp:BoundField DataField="Cargo" HeaderText="CARGO" HtmlEncode="false" />
                                                                    <asp:BoundField DataField="Planilla" HeaderText="PLANILLA" HtmlEncode="false" />
                                                                    <asp:BoundField DataField="FechaIngreso" HeaderText="FECHA INICIO" HtmlEncode="false" />                                                                                                                                        
                                                                    <asp:TemplateField HeaderText="OPCION">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lblDetalles" runat="server" CssClass="btn btn-sm btn-default"
                                                                                Text='Retirar'
                                                                                CommandName="DesactivarTrabajadorProyecto"
                                                                                CommandArgument='<%# Eval("CodTrabajador")%>'>
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
                                                <div id="gpListadoTrabajadores" runat="server">
                                                    <div class="form-group m-form__group row" style="padding-bottom: 0; border-bottom: none">
                                                        <div class="col-md-12" id="tbListadoTrabajadores" runat="server">
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group m-form__group row" style="margin-top: 20px" >
                                                    <div class="col-md-12" style="text-align: center">
                                                        <div class="m-btn-group m-btn-group--pill btn-group" role="group" aria-label="First group">
                                                            <asp:LinkButton runat="server" CssClass="m-btn btn btn-secondary" ID="btnRegresar" OnClick="btnRegresar_Click" >
                                                            <i class="la la-mail-reply"></i> Regresar
                                                            </asp:LinkButton>

                                                            <asp:LinkButton runat="server" CssClass="m-btn btn btn-primary" ID="btnGuardar" OnClick="btnGuardar_Click" >
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
            $('#navMenuProyecto').addClass("m-menu__item--open");
            $('#navMenuProyecto').addClass("m-menu__item--expanded");
            $('#navSubMenuAsignacionTrabajadores').addClass("m-menu__item--active");
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
          function populateTextInput() {
            // empty text input
            $("#<%=txtEnlaceExterno.ClientID%>").val('');
           $("#<%=txtParametrosValores.ClientID%>").val('');
          

            //print out all checked inputs
            $(".clickme").each(function () {
                if ($(this).prop('checked')) {
                    $("#<%=txtEnlaceExterno.ClientID%>").val($("#<%=txtEnlaceExterno.ClientID%>").val() + ',' + $(this).val());
                    $(".parametro" + $(this).val() + "").prop('readonly', false);
                    $("#<%=txtParametrosValores.ClientID%>").val($("#<%=txtParametrosValores.ClientID%>").val() + ',' +$(".parametro" + $(this).val()).val());
                }
                else {
                    $(".parametro" + $(this).val()).val('');
                    $(".parametro" + $(this).val()).prop('readonly', true);
                }
            });
        }
    </script>
</asp:Content>
