<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Inicio.aspx.cs" Inherits="SistemaGestionPlanilla.Inicio" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript"> 
        function pageLoad() {
            $(function () {
                Saludo();

                $('.m-widget17__item').each(function () {
                    animationHover(this, 'pulse');
                });
            })
        }
    </script>
    <div class="m-grid__item m-grid__item--fluid m-wrapper">

        <!-- BEGIN: Subheader -->
    
        <!-- END: Subheader -->
        <div class="m-content">

            <!--Begin::Section-->
            <div class="row">
                <div class="col-xl-4">

                    <!--begin:: Widgets/Activity-->
                    <div class="m-portlet m-portlet--bordered-semi m-portlet--widget-fit m-portlet--full-height m-portlet--skin-light  m-portlet--rounded-force">
                        <div class="m-portlet__head">
                            <div class="m-portlet__head-caption">
                                <div class="m-portlet__head-title">
                                    <h3 class="m-portlet__head-text m--font-light">Actividad
                                    </h3>
                                </div>
                            </div>
                            
                        </div>
                        <div class="m-portlet__body">
                            <div class="m-widget17">
                                <div class="m-widget17__visual m-widget17__visual--chart m-portlet-fit--top m-portlet-fit--sides m--bg-danger">
                                    <div class="m-widget17__chart" style="height: 320px;">
                                        <canvas id="m_chart_activities"></canvas>
                                    </div>
                                </div>
                                <div class="m-widget17__stats">
                                    <div class="m-widget17__items m-widget17__items-col1">
                                        <div class="m-widget17__item">
                                            <a href="<%=ConfigurationManager.AppSettings["AssetsUrl"]%>/Trabajador/RegistroTrabajador.aspx" style="text-decoration: none">
                                                <span class="m-widget17__icon">
                                                    <i class="flaticon-truck m--font-brand"></i>
                                                </span>
                                                <span class="m-widget17__subtitle">Trabajadores
                                                </span>
                                                <span class="m-widget17__desc">
                                                    <asp:Label runat="server" ID="lblCantidadTrabajadores"></asp:Label>
                                                </span>
                                            </a>
                                        </div>
                                        <div class="m-widget17__item">
                                            <a href="<%=ConfigurationManager.AppSettings["AssetsUrl"]%>/Mantenimiento/UsuariosInterno.aspx" style="text-decoration: none">
                                                <span class="m-widget17__icon">
                                                    <i class="flaticon-profile m--font-info"></i>
                                                </span>
                                                <span class="m-widget17__subtitle">Usuarios
                                                </span>
                                                <span class="m-widget17__desc">
                                                    <asp:Label runat="server" ID="lblCantidadUsuarios"></asp:Label>
                                                </span>
                                            </a>
                                        </div>
                                    </div>
                                    <div class="m-widget17__items m-widget17__items-col2">
                                        <div class="m-widget17__item">
                                            <a href="<%=ConfigurationManager.AppSettings["AssetsUrl"]%>/Mantenimiento/PerfilPlanilla.aspx" style="text-decoration: none">
                                                <span class="m-widget17__icon">
                                                    <i class="flaticon-pie-chart m--font-success"></i>
                                                </span>
                                                <span class="m-widget17__subtitle">Perfiles
                                                </span>
                                                <span class="m-widget17__desc">
                                                    <asp:Label runat="server" ID="lblCantidadPerfilesPlanilla"></asp:Label>
                                                </span>
                                            </a>
                                        </div>
                                        <div class="m-widget17__item">
                                            <a href="<%=ConfigurationManager.AppSettings["AssetsUrl"]%>/Planilla/GenerarPlanillaIndividual.aspx" style="text-decoration: none">
                                                <span class="m-widget17__icon">
                                                    <i class="flaticon-time m--font-danger"></i>
                                                </span>
                                                <span class="m-widget17__subtitle">Planilla
                                                </span>
                                                <span class="m-widget17__desc">
                                                    <asp:Label runat="server" ID="lblCantidadPlanilla"></asp:Label>
                                                </span>
                                            </a>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <!--end:: Widgets/Activity-->
                </div>
            </div>
        </div>
    </div>
    
    <script src="<%=ConfigurationManager.AppSettings["AssetsUrl"]%>/assets/js/jquery-3.2.1.js" type="text/javascript"></script>
    		<!--begin::Page Snippets -->
    <script src="<%=ConfigurationManager.AppSettings["AssetsUrl"]%>/assets/js/dashboard.js" type="text/javascript"></script>
    <script>
        $(document).ready(function () {
            $('#navMenuInicio').addClass("m-menu__item--active");

        })
        function Saludo() {
            setTimeout(function () {
                toastr.options = {
                    closeButton: true,
                    progressBar: true,
                    showMethod: 'slideDown',
                    timeOut: 10000
                };
                toastr.success('Bienvenido a la Plataforma: ' + '<br/>' + $('#NombreCompletoUsuarioInterno').text());
            }, 1300);
        }
        function animationHover(element, animation) {
            element = $(element);
            element.hover(
                function () {
                    element.addClass('animated ' + animation);
                },
                function () {
                    //wait for animation to finish before removing classes
                    window.setTimeout(function () {
                        element.removeClass('animated ' + animation);
                    }, 2000);
                });
        }
    </script>
</asp:Content>
