<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="PerfilPlanilla.aspx.cs" Inherits="SistemaGestionPlanilla.Mantenimiento.PerfilPlanilla" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript"> 
        function pageLoad() {
            $(function () {

                $('.m-checkbox').click(function () {

                    populateTextInput();
                });
                $('.perfil').on('change', function () {

                    populateTextInput();
                });

                $(".perfil").on("keypress keyup blur", function (event) {
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
                            El Perfil de Planilla tienen la finalidad de gestionar los parámetros que se aplicarán al trabajador.
                        </div>
                    </div>
                    <div class="m-portlet m-portlet--mobile" >
                        <div class="m-portlet__head">
                            <div class="m-portlet__head-caption">
                                <div class="m-portlet__head-title">
                                    <h3 class="m-portlet__head-text">Perfil de Planilla
                                    </h3>
                                </div>
                            </div>

                        </div>
                        <div class="m-portlet__body" id="gpConsultaPerfilPlanilla" runat="server">

                            <!--begin: Search Form -->
                            <div class="m-form m-form--label-align-right  m--margin-bottom-30">
                                <div class="row align-items-center">
                                    <div class="col-xl-10 order-2 order-xl-1">
                                        <div class="form-group m-form__group row align-items-center">
                                            <div class="col-md-12">
                                                <div class="search-form">
                                                    <div class="input-group">
                                                        <asp:TextBox runat="server" ID="txtTextoBuscarPapeleta" placeholder="Ingrese una parte del nombre del concepto justificación o nombre de mes" CssClass="form-control input-lg" MaxLength="200"></asp:TextBox>
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
                                        <a runat="server" id="btnAgregarPerfilPlanilla" onserverclick="btnAgregarPerfilPlanilla_ServerClick" class="btn btn-primary m-btn m-btn--custom m-btn--icon m-btn--air m-btn--pill">
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

                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="padding-left:0px">
                                <div class="m-datable table-responsive">
                                    <asp:GridView runat="server" ID="dgvPerfilPlanilla" Width="100%" AutoGenerateColumns="false" CssClass="table table-bordered" RowStyle-BackColor="#FFFFFF" AlternatingRowStyle-BackColor="#F5F5F6" HeaderStyle-BackColor="#F5F5F6" HeaderStyle-Wrap="true" RowStyle-Wrap="true" ShowHeaderWhenEmpty="true" AllowPaging="True" PageSize="5" OnRowDataBound="dgvPerfilPlanilla_RowDataBound" OnRowCommand="dgvPerfilPlanilla_RowCommand" OnPageIndexChanging="dgvPerfilPlanilla_PageIndexChanging">
                                        <Columns>
                                            <asp:BoundField DataField="CodPerfilPlanilla" HeaderText="CodJustificacion" HtmlEncode="false" HeaderStyle-CssClass="hidden" Visible="false" ItemStyle-CssClass="hidden" />
                                            <asp:BoundField DataField="FechaRegistro" HeaderText="FECHA REGISTRO" HtmlEncode="false" />
                                            <asp:BoundField DataField="Nombres" HeaderText="DESCRIPCIÓN" HtmlEncode="false" />
                                            <asp:BoundField DataField="Jornal" HeaderText="JORNAL" HtmlEncode="false" />                                            
                                            <asp:BoundField DataField="FlagActivo" HeaderText="ESTADO" HtmlEncode="false" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="header-center" />
                                            <asp:TemplateField HeaderText="OPCION">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lblDetalles" runat="server" CssClass="btn btn-sm btn-default"
                                                        Text='Ver Detalle'
                                                        CommandName="VerDetallePerfilPlanilla"
                                                        CommandArgument='<%# Eval("CodPerfilPlanilla")%>'>
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="OPCION">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lblEliminar" runat="server" CssClass="btn btn-sm btn-outline-danger "
                                                            Text='Eliminar'
                                                            CommandName="EliminarPerfilPlanilla"
                                                            CommandArgument='<%# Eval("CodPerfilPlanilla")%>'>
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
                        <div class="m-portlet__body" id="gpFormularioPerfilPlanilla" runat="server">
                            <div class="form-group m-form__group col-lg-4 col-md-6 col-sm-12 col-xs-12" style="padding-left:0px;padding-right:0px">
                                <asp:Label runat="server" ID="lblNombrePerfil" CssClass="lblValido" >Nombre del Perfil</asp:Label>
                                <div class="m-input-icon m-input-icon--right">
                                      <asp:TextBox runat="server" CssClass="form-control m-input" ID="txtNombrePerfil" placeholder="* Nombre Perfil Planilla"></asp:TextBox>
                                    <span class="m-input-icon__icon m-input-icon__icon--right">
                                        <span>
                                            <i class="la la-truck"></i>
                                        </span>
                                    </span>
                                </div>
                              
                                <span style="color: #7b7e8a;font-weight: 300;font-size: 0.85rem;" class="m-form__help">Ingrese un nombre que sea de su facil uso.</span>
                            </div>
                            <div class="form-group m-form__group col-lg-4 col-md-6 col-sm-12 col-xs-12" style="padding-left:0px;padding-right:0px">
                                <asp:Label runat="server" ID="lblJornal" CssClass="lblValido">Jornal</asp:Label>
                                <div class="m-input-icon m-input-icon--right">
                                    <asp:TextBox runat="server" CssClass="form-control m-input" ID="txtJornal" placeholder="* Costo Jornal"></asp:TextBox>
                                    <span class="m-input-icon__icon m-input-icon__icon--right">
                                        <span>
                                            <i class="la la-money"></i>
                                        </span>
                                    </span>
                                </div>
                                
                                <span style="color: #7b7e8a;font-weight: 300;font-size: 0.85rem;" class="m-form__help">Ingrese el costo del jornal.</span>
                            </div>
                            <!--begin::Section-->
                            <div class="m-section">
                          
                                <span class="m-section__sub">Seleccion de parametros.
                                </span>
                                <div class="form-group m-form__group">
                                    <%--<label for="example_input_full_name">&nbsp;</label>--%>
                                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="padding:0px" id="gpParametros" runat="server">
                          
                                    </div>
                                </div>
                                <div class="col-xl-12 order-1 order-xl-2 m--align-right">
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

                            <!--end::Section-->
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
            $('#navMenuMantenimiento').addClass("m-menu__item--open");
            $('#navMenuMantenimiento').addClass("m-menu__item--expanded");
            $('#navSubMenuPerfilPlanilla').addClass("m-menu__item--active");
        })
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
    </script>
</asp:Content>
