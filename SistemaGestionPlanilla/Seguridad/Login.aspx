<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="SistemaGestionPlanilla.Seguridad.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
	<head>
		<meta charset="utf-8" />
		<title>Sistema Gestion de Planilla</title>
		<meta name="description" content="Latest updates and statistic charts">
		<meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, shrink-to-fit=no">

		<!--begin::Web font -->
		<script src="https://ajax.googleapis.com/ajax/libs/webfont/1.6.16/webfont.js"></script>
		<script>
			WebFont.load({
				google: {
					"families": ["Poppins:300,400,500,600,700", "Roboto:300,400,500,600,700"]
				},
				active: function() {
					sessionStorage.fonts = true;
				}
			});
		</script>

		<!--end::Web font -->

		<!--begin::Base Styles -->
        <link href="/assets/css/vendors.bundle.css" rel="stylesheet">
        <link href="/assets/css/style.bundle.css" rel="stylesheet">

		<!--end::Base Styles -->
	    <link rel="shortcut icon" href="/assets/images/favicon_construc.png" />
        <script type="text/javascript"> 
            function pageLoad() {
                $(function () {

                 
                })
            }
        </script>
	</head>

	<!-- end::Head -->

	<!-- begin::Body -->
	<body class="m--skin- m-header--fixed m-header--fixed-mobile m-aside-left--enabled m-aside-left--skin-dark m-aside-left--fixed m-aside-left--offcanvas m-footer--push m-aside--offcanvas-default">

		<!-- begin:: Page -->
		<div class="m-grid m-grid--hor m-grid--root m-page">
			<div class="m-grid__item m-grid__item--fluid m-grid m-grid--ver-desktop m-grid--desktop m-grid--tablet-and-mobile m-grid--hor-tablet-and-mobile m-login m-login--1 m-login--signin" id="m_login">
				<div class="m-grid__item m-grid__item--order-tablet-and-mobile-2 m-login__aside">
					<div class="m-stack m-stack--hor m-stack--desktop">
						<div class="m-stack__item m-stack__item--fluid">
							<div class="m-login__wrapper" style="padding-top:25%">
								<div class="m-login__logo">
									<a href="#">
										<img class="img_login" src="../assets/images/construccion_logo.png">
									</a>
								</div>
								<div class="m-login__signin">
									<div class="m-login__head">
										<h3 class="m-login__title">Acceso a la Plataforma</h3>
									</div>
									<form class="m-login__form m-form" runat="server" id="form1" role="form">
										<div class="form-group m-form__group">
											<asp:TextBox runat="server"  CssClass="form-control m-input"  placeholder="usuario" name="user" autocomplete="off" ID="txtUsuario"></asp:TextBox>
										</div>
										<div class="form-group m-form__group">
											<asp:TextBox runat="server"  CssClass="form-control m-input m-login__form-input--last" TextMode="Password" placeholder="contraseña" name="password" ID="txtContrasema"></asp:TextBox>
										</div>
										<div class="row m-login__form-sub">
											<div class="col m--align-left">
												<label class="m-checkbox m-checkbox--focus">
													<input type="checkbox" class="checkboxlogin" name="remember" id="checkbox1"/> Recuérdame en este equipo
													<span></span>
												</label>
											</div>
										
										</div>

                                        <div class="col-md-12 p-0" style="margin-top:20px">
                                            <div class="col-md-12 p-0">
                                                <div class="alert-group" id="gpMensaje" runat="server">
                                                </div>
                                            </div>
                                        </div>
										<div class="m-login__form-action">
											<asp:LinkButton runat="server" ID="btnIngresar" CssClass="btn btn-focus m-btn m-btn--pill m-btn--custom m-btn--air" OnClick="btnIngresar_Click">Ingresar</asp:LinkButton>

                     
										</div>
									</form>
								</div>
								
							</div>
						</div>
						
					</div>
				</div>
				<div class="m-grid__item m-grid__item--fluid m-grid m-grid--center m-grid--hor m-grid__item--order-tablet-and-mobile-1	m-login__content m-grid-item--center" id="banner_login" style="background-image: url(../assets/images/bg-2.jpg);position:relative">
					
                    <div class="m-grid__item">
						<h3 class="m-login__welcome">Sistema Gestión de Planilla</h3>
						<p class="m-login__msg">
							© <%=DateTime.Now.Year %> Tecnología Comunicaciones & Sistemas
						</p>
					</div>
				</div>
			</div>
		</div>

		<!-- end:: Page -->

		<!--begin::Base Scripts -->
		 <script src="/assets/js/vendors.bundle.js" type="text/javascript"></script>
         <script src="/assets/js/scripts.bundle.js" type="text/javascript"></script>

		<!--end::Base Scripts -->

		<!--begin::Page Snippets -->
	     <script src="/assets/js/login.js" type="text/javascript"></script>

		<!--end::Page Snippets -->
        <script>
            $(document).ready(function () {

                RecordarUsuarioAdmin();

                $('.checkboxlogin').click(function () {

                    if ($('.checkboxlogin').is(':checked')) {
                
                        // save username and password
                        localStorage.username = $('#txtUsuario').val();
                        localStorage.pass = $('#txtContrasema').val();
                        localStorage.chkbox = $('.checkboxlogin').val();
                    } else {
                   
                        localStorage.username = '';
                        localStorage.pass = '';
                        localStorage.chkbox = '';
                    }
                });
            })
        </script>
       
        <script>
            function button_click(objTextBox, objBtnID) {
                if (window.event.keyCode == 13) {
                    document.getElementById(objBtnID).focus();
                    document.getElementById(objBtnID).click();
                }
            }
            function RecordarUsuarioAdmin() {
                if (localStorage.username && localStorage.pass != '') {
                    $('#checkbox1').attr('checked', 'checked');
                    $('#txtUsuario').val(localStorage.username);
                    $('#txtContrasema').val(localStorage.pass);
                } else {
                    $('#checkbox1').removeAttr('checked');
                    $('#txtUsuario').val('');
                    $('#txtContrasema').val('');
                }
            }
        </script>
	</body>
</html>
