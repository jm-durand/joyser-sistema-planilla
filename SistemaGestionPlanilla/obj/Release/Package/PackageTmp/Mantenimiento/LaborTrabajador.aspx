<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="LaborTrabajador.aspx.cs" Inherits="SistemaGestionPlanilla.Mantenimiento.LaborTrabajador" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <script type="text/javascript"> 
        function pageLoad() {
            $(function () {
          
             
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
                           Gestione los tipo de trabajos realizados en la empresa.
                        </div>
                    </div>
                    <div class="m-portlet m-portlet--mobile" >
                        <div class="m-portlet__head">
                            <div class="m-portlet__head-caption">
                                <div class="m-portlet__head-title">
                                    <h3 class="m-portlet__head-text">Labor Trabajador
                                    </h3>
                                </div>
                            </div>

                        </div>
                        <div class="m-portlet__body" >

                            <!--begin: Search Form -->
                            <div class="m-form m-form--label-align-right  m--margin-bottom-30">

                                <div class="form-group m-form__group col-md-6 col-xs-12" style="padding-left: 0px; padding-right: 0px">
                                    <asp:Label runat="server" ID="lblNombrePerfil" CssClass="lblValido">Nombre del labor realizado</asp:Label>
                                    <div class="m-input-icon m-input-icon--right">
                                        <asp:TextBox runat="server" CssClass="form-control m-input" ID="txtNombreLaborTrabajo" placeholder="* Nombre labor trabajo"></asp:TextBox>
                                        <span class="m-input-icon__icon m-input-icon__icon--right">
                                            <span>
                                                <i class="la la-wrench"></i>
                                            </span>
                                        </span>

                                    </div>

                                    <span style="color: #7b7e8a; font-weight: 300; font-size: 0.85rem;" class="m-form__help">Ingrese un nombre que sea de su facil uso.</span>
                                </div>
                                <asp:LinkButton runat="server" ID="btnLaborTrabajo" CssClass="btn btn-primary m-btn m-btn--custom m-btn--icon m-btn--air m-btn--pill" OnClick="btnLaborTrabajo_Click">
                                <span>
                                    <i class="la la-floppy-o"></i>
                                    <span>Guardar</span>
                                </span>
                                </asp:LinkButton>

                            </div>

                            <!--end: Search Form -->

                            <!--begin: Datatable -->
                            <div class="m-portlet__head" style="margin-top: -20px; border-bottom: none;padding-left: 0px;">
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
                                    <asp:GridView runat="server" ID="dgvLaborTrabajo" Width="100%" AutoGenerateColumns="false" CssClass="table table-bordered" RowStyle-BackColor="#FFFFFF" AlternatingRowStyle-BackColor="#F5F5F6" HeaderStyle-BackColor="#F5F5F6" HeaderStyle-Wrap="true" RowStyle-Wrap="true" ShowHeaderWhenEmpty="true" AllowPaging="True" PageSize="10"  OnRowDataBound="dgvLaborTrabajo_RowDataBound" OnRowCommand="dgvLaborTrabajo_RowCommand" OnPageIndexChanging="dgvLaborTrabajo_PageIndexChanging">
                                        <Columns>
                                            <asp:BoundField DataField="CodLaborTrabajo" HeaderText="CodJustificacion" HtmlEncode="false" HeaderStyle-CssClass="hidden" Visible="false" ItemStyle-CssClass="hidden" />
                                            <asp:BoundField DataField="Nombres" HeaderText="DESCRIPCION" HtmlEncode="false" />
                                                                              
                                            <asp:BoundField DataField="FlagActivo" HeaderText="ESTADO" HtmlEncode="false" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="header-center" />
                                            <asp:TemplateField HeaderText="OPCION">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lblDetalles" runat="server" CssClass="btn btn-sm btn-default"
                                                        Text='Modificar'
                                                        CommandName="ModificarLaborTrabajo"
                                                        CommandArgument='<%# Eval("CodLaborTrabajo")%>'>
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
            $('#navSubMenuLaborTrabajo').addClass("m-menu__item--active");
        })
   
   
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
