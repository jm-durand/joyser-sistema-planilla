<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Aportaciones.aspx.cs" Inherits="SistemaGestionPlanilla.Mantenimiento.Aportaciones" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <script type="text/javascript"> 
        function pageLoad() {
            $(function () {

                $(".decimal").on("keypress keyup blur", function (event) {
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
                           Gestione los parámetros de los tipos de aportaciones
                        </div>
                    </div>
                    <div class="m-portlet m-portlet--mobile" >
                        <div class="m-portlet__head">
                            <div class="m-portlet__head-caption">
                                <div class="m-portlet__head-title">
                                    <h3 class="m-portlet__head-text">Aportaciones
                                    </h3>
                                </div>
                            </div>

                        </div>
                        <div class="m-portlet__body" >

                            <!--begin: Search Form -->
                            <div class="m-form m-form--label-align-right  m--margin-bottom-30" id="gpParametrosAportacion" runat="server">
                                <div class="row">
                                    <div class="form-group m-form__group col-md-6 col-xs-12">
                                        <asp:Label runat="server" ID="lblTipoAportacion" CssClass="lblValido">Nombre aportación</asp:Label>
                                        <div class="m-input-icon m-input-icon--right">
                                            <asp:TextBox runat="server" CssClass="form-control m-input" ID="txtTipoAportacion" ReadOnly="true" placeholder="* Nombre aportación"></asp:TextBox>
                                            <span class="m-input-icon__icon m-input-icon__icon--right">
                                                <span>
                                                    <i class="la la-balance-scale"></i>
                                                </span>
                                            </span>

                                        </div>
                                        <span style="color: #7b7e8a; font-weight: 300; font-size: 0.85rem;" class="m-form__help">Tipo aportación.</span>
                                    </div>                                    
                                </div>                      
                                <div class="row m--padding-top-15">
                                    <div class="form-group m-form__group col-md-4 col-xs-12">
                                        <asp:Label runat="server" ID="lblAporteObligatorio" CssClass="lblValido">Aporte Obligatorio</asp:Label>
                                        <div class="m-input-icon m-input-icon--right">
                                            <asp:TextBox runat="server" CssClass="form-control m-input decimal" ID="txtAporteObligatorio" placeholder="* Aporte Obligatorio"></asp:TextBox>
                                            <span class="m-input-icon__icon m-input-icon__icon--right">
                                                <span>
                                                    <i class="la la-gg"></i>
                                                </span>
                                            </span>

                                        </div>
                                        <span style="color: #7b7e8a; font-weight: 300; font-size: 0.85rem;" class="m-form__help">Parámetro aporte obligatorio.</span>
                                    </div>
                                    <div class="form-group m-form__group col-md-4 col-xs-12" style="padding-top: 0px">
                                        <asp:Label runat="server" ID="lblComisionPorFlujo" CssClass="lblValido">Comisión por Flujo</asp:Label>
                                        <div class="m-input-icon m-input-icon--right">
                                            <asp:TextBox runat="server" CssClass="form-control m-input decimal" ID="txtComisionPorFlujo" placeholder="* Comisión por Flujo"></asp:TextBox>
                                            <span class="m-input-icon__icon m-input-icon__icon--right">
                                                <span>
                                                    <i class="la la-gg"></i>
                                                </span>
                                            </span>

                                        </div>

                                        <span style="color: #7b7e8a; font-weight: 300; font-size: 0.85rem;" class="m-form__help">Parámetro comisión por flujo.</span>
                                    </div>
                                    <div class="form-group m-form__group col-md-4 col-xs-12" style="padding-top: 0px">
                                        <asp:Label runat="server" ID="lblComisionMixta" CssClass="lblValido">Comisión Mixta</asp:Label>
                                        <div class="m-input-icon m-input-icon--right">
                                            <asp:TextBox runat="server" CssClass="form-control m-input decimal" ID="txtComisionMixta" placeholder="* Comisión Mixta"></asp:TextBox>
                                            <span class="m-input-icon__icon m-input-icon__icon--right">
                                                <span>
                                                    <i class="la la-gg"></i>
                                                </span>
                                            </span>

                                        </div>

                                        <span style="color: #7b7e8a; font-weight: 300; font-size: 0.85rem;" class="m-form__help">Parámetro comisión mixta.</span>
                                    </div>
                                    <div class="form-group m-form__group col-md-4 col-xs-12" style="padding-top: 0px">
                                        <asp:Label runat="server" ID="lblPrimaSeguro" CssClass="lblValido">Prima Seguro</asp:Label>
                                        <div class="m-input-icon m-input-icon--right">
                                            <asp:TextBox runat="server" CssClass="form-control m-input decimal" ID="txtPrimaSeguro" placeholder="* Prima Seguro"></asp:TextBox>
                                            <span class="m-input-icon__icon m-input-icon__icon--right">
                                                <span>
                                                    <i class="la la-gg"></i>
                                                </span>
                                            </span>

                                        </div>

                                        <span style="color: #7b7e8a; font-weight: 300; font-size: 0.85rem;" class="m-form__help">Parámetro prima seguro.</span>
                                    </div>
                                    <div class="form-group m-form__group col-md-4 col-xs-12" style="padding-top: 0px">
                                        <asp:Label runat="server" ID="lblAporteComplementario" CssClass="lblValido">Aporte Complementario</asp:Label>
                                        <div class="m-input-icon m-input-icon--right">
                                            <asp:TextBox runat="server" CssClass="form-control m-input decimal" ID="txtAporteComplementario" placeholder="* Aporte Complementario"></asp:TextBox>
                                            <span class="m-input-icon__icon m-input-icon__icon--right">
                                                <span>
                                                    <i class="la la-gg"></i>
                                                </span>
                                            </span>

                                        </div>

                                        <span style="color: #7b7e8a; font-weight: 300; font-size: 0.85rem;" class="m-form__help">Parámetro aporte complementario.</span>
                                    </div>
                                </div>

                                <div class="col-xl-12 order-1 order-xl-2 m--align-right">
                                    <div class="m-btn-group m-btn-group--pill btn-group" role="group" aria-label="First group">
                                        <asp:LinkButton runat="server" CssClass="m-btn btn btn-secondary" ID="btnRegresar" OnClick="btnRegresar_Click" >
                                          <i class="la la-mail-reply"></i> Regresar
                                        </asp:LinkButton>

                                        <asp:LinkButton runat="server" ID="btnModificarAportaciones" CssClass="btn btn-primary m-btn m-btn--custom m-btn--icon m-btn--air m-btn--pill" OnClick="btnModificarAportaciones_Click" >
                                        <span>
                                            <i class="la la-floppy-o"></i>
                                            <span>Modificar</span>
                                        </span>
                                        </asp:LinkButton>
                                    </div>
                                </div>                             
                            </div>

                            <!--end: Search Form -->

                            <!--begin: Datatable -->
                            <div id="gpResultadoBusqueda" runat="server">
                                <div class="m-portlet__head" style="margin-top: -20px; border-bottom: none; padding-left: 0px;">
                                    <div class="m-portlet__head-caption">
                                        <div class="m-portlet__head-title">
                                            <span class="m-portlet__head-icon">
                                                <i class="flaticon-statistics"></i>
                                            </span>
                                            <h3 class="m-portlet__head-text" style="font-weight: 400; font-size: 1.2rem" id="lblResultadoBusqueda" runat="server"></h3>

                                        </div>
                                    </div>

                                </div>
                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="padding-left: 0px">
                                    <div class="m-datable table-responsive">
                                        <asp:GridView runat="server" ID="dgvAportaciones" Width="100%" AutoGenerateColumns="false" CssClass="table table-bordered" RowStyle-BackColor="#FFFFFF" AlternatingRowStyle-BackColor="#F5F5F6" HeaderStyle-BackColor="#F5F5F6" HeaderStyle-Wrap="true" RowStyle-Wrap="true" ShowHeaderWhenEmpty="true" AllowPaging="True" PageSize="10" OnRowCommand="dgvAportaciones_RowCommand" OnRowDataBound="dgvAportaciones_RowDataBound">
                                            <Columns>
                                                <asp:BoundField DataField="CodTipoAportacion" HeaderText="CodTipoAportacion" HtmlEncode="false" HeaderStyle-CssClass="hidden" Visible="false" ItemStyle-CssClass="hidden" />
                                                <asp:BoundField DataField="DescTipoAportacion" HeaderText="APORTACIÓN" HtmlEncode="false" ItemStyle-Width="300" />
                                                <asp:BoundField DataField="AO" HeaderText="APORTE COMPLEMENTARIO" HtmlEncode="false" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="CO" HeaderText="COMISIÓN POR FLUJO" HtmlEncode="false" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="MI" HeaderText="COMISIÓN MIXTA" HtmlEncode="false" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="PS" HeaderText="PRIMA SEGURO" HtmlEncode="false" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="AC" HeaderText="APORTE COMPLEMENTARIO" HtmlEncode="false" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="FlagActivo" HeaderText="ESTADO" HtmlEncode="false" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="header-center" />
                                                <asp:TemplateField HeaderText="OPCION">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lblDetalles" runat="server" CssClass="btn btn-sm btn-default"
                                                            Text='Modificar'
                                                            CommandName="ModificarParametrosAportacion"
                                                            CommandArgument='<%# Eval("CodTipoAportacion")%>'>
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
            $('#navSubMenuAportaciones ').addClass("m-menu__item--active");
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
