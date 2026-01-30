<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="UsuariosInterno.aspx.cs" Inherits="SistemaGestionPlanilla.Mantenimiento.UsuariosInterno" %>
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


            });
        }
    </script>
    <div class="m-grid__item m-grid__item--fluid m-wrapper">
        <asp:UpdatePanel runat="server" ID="UpdatePanelPrincipal" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="m-content">

                    <!--Begin::Main Portlet-->
                    <div class="m-portlet m-portlet--full-height">

                        <!--begin: Portlet Head-->
                        <div class="m-portlet__head">
                            <div class="m-portlet__head-caption">
                                <div class="m-portlet__head-title">
                                    <h3 class="m-portlet__head-text">
                                        Usuarios del Sistema
                                    </h3>
                                </div>
                            </div>
                            <div class="m-portlet__head-tools">
                                <ul class="m-portlet__nav">
                                    <li class="m-portlet__nav-item">
                                        <a href="#" data-toggle="m-tooltip" class="m-portlet__nav-link m-portlet__nav-link--icon" data-direction="left" data-width="auto" title="Agregue los usuarios que tendrán acceso al sistema.">
                                            <i class="flaticon-info m--icon-font-size-lg3"></i>
                                        </a>
                                    </li>
                                </ul>
                            </div>
                        </div>

                        <!--end: Portlet Head-->

                        <!--begin: Portlet Body-->
                        <div class="m-portlet__body m-portlet__body--no-padding">

                            <div class="m-portlet__body" id="gpConsultaUsuario" runat="server">
                                <!--begin: Search Form -->
                                <div class="m-form m-form--label-align-right  m--margin-bottom-30">
                                    <div class="row align-items-center">
                                        <div class="col-xl-10 order-2 order-xl-1">
                                            <div class="form-group m-form__group row align-items-center">
                                                <div class="col-md-12">
                                                    <div class="search-form">
                                                        <div class="input-group">
                                                            <asp:TextBox runat="server" ID="txtTextoBuscarUsuario" placeholder="Ingrese una parte del nombre del usuario que desea consultar" CssClass="form-control input-lg" MaxLength="200"></asp:TextBox>
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
                                            <a runat="server" id="btnAgregarUsuario" class="btn btn-primary m-btn m-btn--custom m-btn--icon m-btn--air m-btn--pill"  style="color:#fff" onserverclick="btnAgregarUsuario_ServerClick">
                                                <span>
                                                    <i class="la la-user-plus"></i>
                                                    <span>Agregar nuevo</span>
                                                </span>
                                            </a>
                                            <div class="m-separator m-separator--dashed d-xl-none"></div>
                                        </div>
                                    </div>
                                </div>
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
                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="padding-left: 0px">
                                    <div class="m-datable table-responsive">
                                        <asp:GridView runat="server" ID="dgvUsuariosInterno" Width="100%" AutoGenerateColumns="false" CssClass="table table-bordered" RowStyle-BackColor="#FFFFFF" AlternatingRowStyle-BackColor="#F5F5F6" HeaderStyle-BackColor="#F5F5F6" HeaderStyle-Wrap="true" RowStyle-Wrap="true" ShowHeaderWhenEmpty="true" AllowPaging="True" PageSize="10" OnRowCommand="dgvUsuariosInterno_RowCommand" OnRowDataBound="dgvUsuariosInterno_RowDataBound" >
                                            <Columns>
                                                <asp:BoundField DataField="CodUsuario" HeaderText="CodTrabajador" HtmlEncode="false" HeaderStyle-CssClass="hidden" Visible="false" ItemStyle-CssClass="hidden" />
                                                <asp:BoundField DataField="Usuario" HeaderText="USUARIO" HtmlEncode="false" />
                                                <asp:BoundField DataField="PerfilAcceso" HeaderText="PERFIL ACCESO" HtmlEncode="false" />
                                              
                                                <asp:BoundField DataField="Estado" HeaderText="ESTADO" HtmlEncode="false" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="header-center" />
                                                <asp:TemplateField HeaderText="OPCION" ItemStyle-Width="200">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lblDetalles" runat="server" CssClass="btn btn-sm btn-default"
                                                            Text='Modificar'
                                                            CommandName="ModificarUsuario"
                                                            CommandArgument='<%# Eval("CodUsuario")%>'>
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
                            <div class="m-portlet__body" id="gpFormularioUsuarioInterno" runat="server" style="margin-top:-20px">
                                <!--begin: Search Form -->
                                <div class="m-form m-form--label-align-right  m--margin-bottom-30">
                                    <div class="row align-items-center">
                                        <div class="m-portlet__body"  style="width:100%">
                                            <div id="gpFormularioPeriodo" runat="server">
                                                <div class="form-group m-form__group row" style="padding-bottom: 0; border-bottom: none">
                                                    <div class="col-md-6">
                                                        <asp:Label runat="server" ID="lblUsuario">Usuario:</asp:Label>
                                                        <div class="m-input-icon m-input-icon--right">
                                                            <asp:TextBox runat="server" CssClass="form-control m-input"  placeholder="Ingrese su usuario de acceso" ID="txtUsuario" />
                                                            <span class="m-input-icon__icon m-input-icon__icon--right">
                                                                <span>
                                                                    <i class="la la-user"></i>
                                                                </span>
                                                            </span>                                               
                                                        </div>
                                                        <span class="m-form__help">Ingrese su nombre de usuario.</span>
                                                    </div>
                                                     <div class="col-md-6">
                                                        <asp:Label runat="server" ID="lblNombres">Nombres:</asp:Label>
                                                        <div class="m-input-icon m-input-icon--right">
                                                            <asp:TextBox runat="server" CssClass="form-control m-input"  placeholder="Ingrese los nombres" ID="txtNombres" />
                                                            <span class="m-input-icon__icon m-input-icon__icon--right">
                                                                <span>
                                                                    <i class="la la-black-tie"></i>
                                                                </span>
                                                            </span>                                               
                                                        </div>
                                                        <span class="m-form__help">Ingrese los nombres.</span>
                                                    </div>                                               
                                                </div>
                                                <div class="form-group m-form__group row" style="padding-bottom: 0; border-bottom: none">
                                                    <div class="col-md-6">
                                                        <asp:Label runat="server" ID="lblApellidoPaterno">Apellido Paterno:</asp:Label>
                                                        <div class="m-input-icon m-input-icon--right">
                                                            <asp:TextBox runat="server" CssClass="form-control m-input" placeholder="Ingrese apellido paterno" ID="txtApellidoPaterno" />
                                                            <span class="m-input-icon__icon m-input-icon__icon--right">
                                                                <span>
                                                                    <i class="la la-black-tie"></i>
                                                                </span>
                                                            </span>
                                                        </div>
                                                        <span class="m-form__help">Ingrese apellido paterno.</span>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <asp:Label runat="server" ID="lblApellidoMaterno">Apellido Materno:</asp:Label>
                                                        <div class="m-input-icon m-input-icon--right">
                                                            <asp:TextBox runat="server" CssClass="form-control m-input" placeholder="Ingrese apellido materno" ID="txtApellidoMaterno" />
                                                            <span class="m-input-icon__icon m-input-icon__icon--right">
                                                                <span>
                                                                    <i class="la la-black-tie"></i>
                                                                </span>
                                                            </span>
                                                        </div>
                                                        <span class="m-form__help">Ingrese apellido materno.</span>
                                                    </div>
                                                </div>
                                                 <div class="form-group m-form__group row" style="padding-bottom: 0; border-bottom: none">
                                                    <div class="col-md-6">
                                                        <asp:Label runat="server" ID="lblPerfilAcceso">Perfil de Acceso:</asp:Label>
                                                        <div class="m-input-icon m-input-icon--right">
                                                            <asp:DropDownList runat="server" CssClass="form-control m-select2 m-select2-general" ID="cboPerfilAcceso" >
                                                             
                                                            </asp:DropDownList>
                                                            <span class="m-input-icon__icon m-input-icon__icon--right">
                                                                <span>
                                                                    <i class="la la-unlock-alt"></i>
                                                                </span>
                                                            </span>
                                                        </div>
                                                        <span class="m-form__help">Seleccione perfil de acceso.</span>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <asp:Label runat="server" ID="lblEstado">Estado:</asp:Label>
                                                        <div class="m-input-icon m-input-icon--right">
                                                            <asp:DropDownList runat="server" CssClass="form-control m-select2 m-select2-general" ID="cboEstado" >
                                                             <asp:ListItem Value="1" Text="ACTIVO"></asp:ListItem>
                                                             <asp:ListItem Value="0" Text="INACTIVO"></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <span class="m-input-icon__icon m-input-icon__icon--right">
                                                                <span>
                                                                    <i class="la la-power-off"></i>
                                                                </span>
                                                            </span>
                                                        </div>
                                                        <span class="m-form__help">Seleccione estado.</span>
                                                    </div>
                                                </div>                                          
                                                <div class="form-group m-form__group row" id="gpCheckCambiarContrasena" runat="server">
                                                    <div class="col-md-12">

                                                        <label class="m-checkbox m-checkbox--bold m-checkbox--state-success">
                                                            <input type="checkbox" id="chkCambiarContrasena" runat="server" onchange="HabilitarContrasena()">
                                                            Deseo modificar la contraseña
																		<span></span>
                                                        </label>
                                                    </div>
                                                </div>
                                                <div class="form-group m-form__group row" style="padding-bottom: 0; border-bottom: none;margin-top:-10px">
                                                    <div class="col-md-6">
                                                        <asp:Label runat="server" ID="lblContrasena">Contraseña:</asp:Label>
                                                        <div class="m-input-icon m-input-icon--right">
                                                            <asp:TextBox runat="server" CssClass="form-control m-input" TextMode="Password" MaxLength="20"  placeholder="Ingrese contraseña" ID="txtContrasena" onkeypress="return blockSpecialChar(event)" />
                                                            <span class="m-input-icon__icon m-input-icon__icon--right">
                                                                <span>
                                                                    <i class="la la-key"></i>
                                                                </span>
                                                            </span>
                                                        </div>
                                                        <span class="m-form__help">Ingrese su contraseña.</span>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <asp:Label runat="server" ID="lblRepiteContrasena">Repite contraseña:</asp:Label>
                                                        <div class="m-input-icon m-input-icon--right">
                                                            <asp:TextBox runat="server" CssClass="form-control m-input" TextMode="Password" MaxLength="20" placeholder="Repita contraseña" ID="txtRepiteContrasena" onkeypress="return blockSpecialChar(event)" />
                                                            <span class="m-input-icon__icon m-input-icon__icon--right">
                                                                <span>
                                                                    <i class="la la-key"></i>
                                                                </span>
                                                            </span>
                                                        </div>
                                                        <span class="m-form__help">Repite contraseña.</span>
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
                                                    <div class="col-md-12" style="display: none">
                                                        <asp:LinkButton runat="server" ID="HabilitarContrasena" OnClick="HabilitarContrasena_Click"></asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        
                                    </div>
                                </div>
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
            $('#navSubMenuUsuarioInterno').addClass("m-menu__item--active");
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
           function button_click(objTextBox, objBtnID) {
            if (window.event.keyCode == 13) {
                document.getElementById(objBtnID).focus();
                document.getElementById(objBtnID).click();
            }
        }

             function blockSpecialChar(e) {
            var k = e.keyCode;
            return ((k > 64 && k < 91) || (k > 96 && k < 123) || k == 8 || (k >= 48 && k <= 57));
        }

        function HabilitarContrasena() {

            $('#<%=HabilitarContrasena.ClientID%>')[0].click();
        }

    </script>
</asp:Content>
