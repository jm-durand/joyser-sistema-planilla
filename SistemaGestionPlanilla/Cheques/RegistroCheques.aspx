<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="RegistroCheques.aspx.cs" Inherits="SistemaGestionPlanilla.Cheques.RegistroCheques" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript"> 
        function pageLoad() {
            $(function () {
                ReloadJquery();
                RebuildDataTable();     
                $('[data-toggle="m-tooltip"]').tooltip();
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
                            Gestione el registro y vigencia de cheques de su empresa.
                        </div>
                    </div>
                    <div class="m-portlet m-portlet--mobile">
                        <div class="m-portlet__head">
                            <div class="m-portlet__head-caption">
                                <div class="m-portlet__head-title">
                                    <h3 class="m-portlet__head-text">Registro de cheques
                                    </h3>
                                </div>                                
                            </div>
                            <div class="m-portlet__head-tools" id="gpButtonNuevoCheque" runat="server">
                                <asp:LinkButton runat="server" ID="btnRegistrarCheque" Style="color: #fff" CssClass="btn btn-primary m-btn m-btn--custom m-btn--icon m-btn--air m-btn--pill btn-sm" OnClick="btnRegistrarCheque_Click">
                                <span>
                                    <i class="la la-plus"></i>
                                    <span>Registrar nuevo</span>
                                </span>
                                </asp:LinkButton>
                            </div>
                        </div>
                        <div class="m-portlet__body">
                            <!--begin: Search Form -->
                            <div class="m-form m-form--label-align-right  m--margin-bottom-30">
                                <div class="row align-items-center">
                                    <div class="col-xl-12 order-2 order-xl-1">
                                        <div class="form-group m-form__group row align-items-center">
                                            <div class="col-md-12">                                                
                                                <div id="gpFormularioBusqueda" runat="server">                                                       
                                                    <div class="form-group m-form__group row" style="padding-bottom: 0; border-bottom: none">                                                        
                                                        <div class="col-lg-6">
                                                            <label>Tipo Pago</label>
                                                            <div class="m-input-icon m-input-icon--right">
                                                                <asp:DropDownList runat="server" CssClass="form-control m-select2 m-select2-general" ID="cboTipoPagoChequeBusqueda" OnSelectedIndexChanged="cboTipoPagoChequeBusqueda_SelectedIndexChanged" AutoPostBack="true">                                                                   
                                                                </asp:DropDownList>
                                                                <span class="m-input-icon__icon m-input-icon__icon--right">
                                                                    <span>
                                                                        <i class="la la-briefcase"></i>
                                                                    </span>
                                                                </span>
                                                            </div>
                                                            <span class="m-form__help">Seleccionar tipo de pago para la búsqueda.</span>
                                                        </div>
                                                        <div class="col-lg-6">
                                                            <label>Fecha rango búsqueda</label>
                                                            <div class="m-input-icon m-input-icon--right">
                                                                <asp:TextBox runat="server" CssClass="form-control m-input rangofechaplanilla" ReadOnly="true" placeholder="Seleccione" ID="txtFechaRangoBusqueda" />
                                                                <span class="m-input-icon__icon m-input-icon__icon--right">
                                                                    <span>
                                                                        <i class="la la-calendar"></i>
                                                                    </span>
                                                                </span>
                                                                <asp:TextBox runat="server" Style="display: none" ID="txtFechaInicial" CssClass="fechainicial"></asp:TextBox>
                                                                <asp:TextBox runat="server" Style="display: none" ID="txtFechaFinal" CssClass="fechafinal"></asp:TextBox>
                                                            </div>
                                                            <span class="m-form__help">Seleccionar rango de fechas para la búsqueda.</span>
                                                        </div>
                                                    </div>  
                                                    <div class="form-group m-form__group row" style="border-bottom: 1px dashed #ebedf2;">
                                                    </div>
                                                </div>
                                                <div id="gpFormularioCheque" runat="server">
                                                    <div id="gpSeleccionTipoPersonaPersonaPagar" runat="server">
                                                        <div class="form-group m-form__group row" style="border-bottom: none">
                                                            <div class="col-md-6">
                                                                <asp:Label runat="server" ID="lblTipoPagoChequeRegistro">Tipo Pago Cheque:</asp:Label>
                                                                <div class="m-input-icon m-input-icon--right">
                                                                    <asp:DropDownList runat="server" CssClass="form-control m-select2 m-select2-general" ID="cboTipoPagoChequeRegistro" OnSelectedIndexChanged="cboTipoPagoChequeRegistro_SelectedIndexChanged" AutoPostBack="true">
                                                                    </asp:DropDownList>
                                                                    <span class="m-input-icon__icon m-input-icon__icon--right">
                                                                        <span>
                                                                            <i class="la la-briefcase"></i>
                                                                        </span>
                                                                    </span>
                                                                </div>
                                                                <span class="m-form__help">Seleccione un modo de pago.</span>
                                                            </div>
                                                            <div class="col-md-6">
                                                                <asp:Label runat="server" ID="lblTipoPersonaPagoChequeRegistro">&nbsp;</asp:Label>
                                                                <div class="m-input-icon m-input-icon--right">
                                                                    <asp:DropDownList runat="server" CssClass="form-control m-select2 m-select2-general" ID="cboTipoPersonaPagoChequeRegistro" OnSelectedIndexChanged="cboTipoPersonaPagoChequeRegistro_SelectedIndexChanged" AutoPostBack="true">
                                                                    </asp:DropDownList>
                                                                    <span class="m-input-icon__icon m-input-icon__icon--right">
                                                                        <span>
                                                                            <i class="la la-black-tie"></i>
                                                                        </span>
                                                                    </span>
                                                                </div>
                                                                <span class="m-form__help">Seleccione.</span>
                                                            </div>
                                                        </div>
                                                    </div>                                                    
                                                    <div id="gpSeleccionListadoTipoPersonaPagar" runat="server">
                                                        <div class="form-group m-form__group row" style="padding-bottom: 0; border-bottom: none">                                                         
                                                            <div class="col-md-12">
                                                                <div class="m-portlet__head" style="margin-top: -10px; border-bottom: none; padding-left: 0px">
                                                                    <div class="m-portlet__head-caption">
                                                                        <div class="m-portlet__head-title">                                                                         
                                                                            <h3 class="m-portlet__head-text" style="font-weight: 300; font-size: 1.1rem" id="lblResultadoTipoPersonaPagar" runat="server"></h3>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="padding-left: 0px">
                                                                    <div id="gpTablaResultadoTipoPersonarPagar" runat="server">
                                                                    </div>
                                                                </div>
                                                                <div class="m-portlet__foot goodfooter">
                                                                    <div class="form-group m-form__group row" style="margin-top: 20px">
                                                                        <div class="col-md-12" style="text-align: center">
                                                                            <div class="m-btn-group m-btn-group--pill btn-group" role="group" aria-label="First group">
                                                                                <asp:LinkButton runat="server" CssClass="m-btn btn btn-secondary" ID="btnCancelarRegistroCheque" OnClick="btnCancelarRegistroCheque_Click">
                                                                                    <i class="la la-times"></i> Cancelar
                                                                                </asp:LinkButton>
                                                                                <asp:LinkButton runat="server" CssClass="m-btn btn btn-primary" ID="btnContinuarRegistroCheque" OnClick="btnContinuarRegistroCheque_Click">
                                                                                    <i class="la la-mail-forward"></i> Continuar
                                                                                </asp:LinkButton>
                                                                            </div>
                                                                        </div>

                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div id="gpSeleccionFormularioFinalPersonaPagar" runat="server">
                                                        <div class="form-group m-form__group row" style="padding-bottom: 0; border-bottom: none">
                                                            <div class="col-md-6">
                                                                <asp:Label runat="server" ID="lblFechaPago">Fecha de Pago:</asp:Label>
                                                                <div class="m-input-icon m-input-icon--right">
                                                                    <asp:TextBox runat="server" CssClass="form-control m-input datepickers" autocomplete="off" placeholder="Seleccione Fecha de Pago" ID="txtFechaPago" onkeydown="return false;" />
                                                                    <span class="m-input-icon__icon m-input-icon__icon--right">
                                                                        <span>
                                                                            <i class="la la-calendar"></i>
                                                                        </span>
                                                                    </span>
                                                                </div>
                                                                <span class="m-form__help">Seleccione Fecha de Pago.</span>
                                                            </div>
                                                            <div class="col-md-6">
                                                                <asp:Label runat="server" ID="lblNumeroCheque">Nº Cheque:</asp:Label>
                                                                <div class="m-input-icon m-input-icon--right">
                                                                    <asp:TextBox runat="server" CssClass="form-control m-input" placeholder="Número de cheque" ID="txtNumeroCheque" />
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
                                                                <asp:Label runat="server" ID="lblTipoDocumentoOrdenPagar">Tipo Doc. Ident.:</asp:Label>
                                                                <div class="m-input-icon m-input-icon--right">
                                                                    <asp:DropDownList runat="server" CssClass="form-control m-select2 m-select2-general" ID="cboTipoDocumentoOrdenPagar" OnSelectedIndexChanged="cboTipoDocumentoOrdenPagar_SelectedIndexChanged" AutoPostBack="true">
                                                                    </asp:DropDownList>
                                                                    <span class="m-input-icon__icon m-input-icon__icon--right">
                                                                      
                                                                    </span>
                                                                </div>
                                                                <span class="m-form__help">Tipo documento identidad.</span>
                                                            </div>
                                                            <div class="col-md-6">
                                                                <asp:Label runat="server" ID="lblNumeroDocumentoIdentidadOrdenPagar">Nº Doc. Ident. Orden a Pagar:</asp:Label>
                                                                <div class="m-input-icon m-input-icon--right">
                                                                    <asp:TextBox runat="server" CssClass="form-control m-input" placeholder="Número documento identidad" ID="txtNumeroDocumentoIdentidadOrdenPagar" OnTextChanged="txtNumeroDocumentoIdentidadOrdenPagar_TextChanged" AutoPostBack="true"/>
                                                                    <span class="m-input-icon__icon m-input-icon__icon--right">
                                                                        <span>
                                                                            <i class="la la-info-circle"></i>
                                                                        </span>
                                                                    </span>
                                                                </div>
                                                                <span class="m-form__help">Número documento identidad.</span>
                                                            </div>
                                                          
                                                        </div>
                                                          <div class="form-group m-form__group row" style="padding-bottom: 0; border-bottom: none">
                                                                 <div class="col-md-12">
                                                                <asp:Label runat="server" ID="lblNombreCompletoOrdenPagar">Nombre Completo Persona Orden a Pagar:</asp:Label>
                                                                <div class="m-input-icon m-input-icon--right">
                                                                    <asp:TextBox runat="server" CssClass="form-control m-input" placeholder="Nombre completo" ID="txtNombreCompletoOrdenPagar" />
                                                                    <span class="m-input-icon__icon m-input-icon__icon--right">
                                                                        <span>
                                                                            <i class="la la-user"></i>
                                                                        </span>
                                                                    </span>
                                                                </div>
                                                                <span class="m-form__help">Nombre completo persona.</span>
                                                            </div>
                                                              </div>
                                                        <div class="form-group m-form__group row" style="padding-bottom: 0; border-bottom: none;margin-top:10px" id="gpListadoPagarChequeResumen" runat="server">                                                            
                                                        </div>
                                                        <div class="form-group m-form__group row" style="padding-bottom: 0; border-bottom: none;padding-top:5px">
                                                            <div class="col-md-6">
                                                                <asp:Label runat="server" ID="lblTipoMoneda">Tipo Moneda:</asp:Label>
                                                                <div class="m-input-icon m-input-icon--right">
                                                                    <asp:DropDownList runat="server" CssClass="form-control m-select2 m-select2-general" ID="cboTipoMoneda" OnSelectedIndexChanged="cboTipoMoneda_SelectedIndexChanged" AutoPostBack="true">
                                                                    </asp:DropDownList>
                                                                    <span class="m-input-icon__icon m-input-icon__icon--right">
                                                                        <span>
                                                                            <i class="la la-money"></i>
                                                                        </span>
                                                                    </span>
                                                                </div>
                                                                <span class="m-form__help">Tipo moneda.</span>
                                                            </div>
                                                            <div class="col-md-6">
                                                                <asp:Label runat="server" ID="lblMontoPagar">Total a Pagar:</asp:Label>
                                                                <div class="m-input-icon m-input-icon--right">
                                                                    <asp:TextBox runat="server" CssClass="form-control m-input onlynumbers totalpagar" placeholder="Monto a pagar" ID="txtMontoPagar"  OnTextChanged="txtMontoPagar_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                                    <span class="m-input-icon__icon m-input-icon__icon--right">
                                                                        <span>
                                                                            <i class="la la-money"></i>
                                                                        </span>
                                                                    </span>
                                                                </div>
                                                                <span class="m-form__help">Ingrese monto a pagar.</span>
                                                            </div>                                                             
                                                        </div>
                                                        <div class="form-group m-form__group row" style="padding-bottom: 0; border-bottom: none">
                                                            <div class="col-md-12">
                                                                <asp:Label runat="server" ID="lblMontoPagarEnLetras">Total en Letras:</asp:Label>
                                                                <div class="m-input-icon m-input-icon--right">
                                                                    <asp:TextBox runat="server" CssClass="form-control m-input" placeholder="Monto a pagar en letras" ID="txtMontoPagarEnLetras" Enabled="false"></asp:TextBox>
                                                                    <span class="m-input-icon__icon m-input-icon__icon--right">
                                                                        <span>
                                                                            <i class="la la-certificate"></i>
                                                                        </span>
                                                                    </span>
                                                                </div>
                                                                <span class="m-form__help">Total a pagar en letras.</span>
                                                            </div>
                                                        </div>
                                                        <div class="form-group m-form__group row" style="padding-bottom: 0; border-bottom: none" id="gpReciboPagoCheque" runat="server">
                                                            <div class="col-md-6">
                                                                <asp:Label runat="server" ID="lblTipoReciboPago">Tipo Recibo Pago:</asp:Label>
                                                                <div class="m-input-icon m-input-icon--right">
                                                                    <asp:DropDownList runat="server" CssClass="form-control m-select2 m-select2-general" ID="cboTipoReciboPago">
                                                                    </asp:DropDownList>
                                                                    <span class="m-input-icon__icon m-input-icon__icon--right">
                                                                        <span>
                                                                            <i class="la la-institution"></i>
                                                                        </span>
                                                                    </span>
                                                                </div>
                                                                <span class="m-form__help">Seleccione tipo de recibo de pago.</span>
                                                            </div>
                                                            <div class="col-md-6">
                                                                <asp:Label runat="server" ID="lblNumeroReciboPago">N° Recibo Pago:</asp:Label>
                                                                <div class="m-input-icon m-input-icon--right">
                                                                    <asp:TextBox runat="server" CssClass="form-control m-input" placeholder="Número de recibo de pago" ID="txtNumeroReciboPago"></asp:TextBox>
                                                                    <span class="m-input-icon__icon m-input-icon__icon--right">
                                                                        <span>
                                                                            <i class="la la-info-circle"></i>
                                                                        </span>
                                                                    </span>
                                                                </div>
                                                                <span class="m-form__help">Ingrese el número de recibo de pago.</span>
                                                            </div>
                                                        </div>
                                                        <div class="form-group m-form__group row" style="padding-bottom: 0; border-bottom: none" >
                                                            <div class="col-md-6">
                                                                <asp:Label runat="server" ID="lblBancoCheque">Banco:</asp:Label>
                                                                <div class="m-input-icon m-input-icon--right">
                                                                    <asp:DropDownList runat="server" CssClass="form-control m-select2 m-select2-general" ID="cboBancoCheque">
                                                                    </asp:DropDownList>
                                                                    <span class="m-input-icon__icon m-input-icon__icon--right">
                                                                        <span>
                                                                            <i class="la la-bank"></i>
                                                                        </span>
                                                                    </span>
                                                                </div>
                                                                <span class="m-form__help">Seleccione el banco.</span>
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
                                                </div>
                                                <div id="gpResumenChequeEmitir" runat="server">
                                                    <div class="m-invoice-1">
                                                        <div class="m-invoice__wrapper">
                                                            <div class="m-invoice__head" style="background-image: url(/assets/images/bg-6.jpg);">
                                                                <div class="m-invoice__container m-invoice__container--centered">
                                                                    <div class="m-invoice__logo">
                                                                        <a href="#">
                                                                            <h1>EMISIÓN DE CHEQUE</h1>
                                                                        </a>
                                                                    </div>
                                                                    <span class="m-invoice__desc">
                                                                        <span>Calle Condorama 136, Santiago de Surco</span>
                                                                        <span>Lima</span>
                                                                        <span>JOYCER S.A.C.</span>
                                                                    </span>
                                                                    <div class="m-invoice__items">
                                                                        <div class="m-invoice__item">
                                                                            <span class="m-invoice__subtitle">FECHA PAGO</span>
                                                                            <span class="m-invoice__text"><asp:Label runat="server" Style="text-transform: capitalize" ID="lblFechaPagoChequeResumen"></asp:Label></span>
                                                                        </div>
                                                                        <div class="m-invoice__item">
                                                                            <span class="m-invoice__subtitle">NÚMERO CHEQUE</span>
                                                                            <span class="m-invoice__text"><asp:Label runat="server" ID="lblNumeroChequeResumen"></asp:Label></span>
                                                                        </div>
                                                                        <div class="m-invoice__item">
                                                                            <span class="m-invoice__subtitle">TIPO PERSONA PAGO</span>
                                                                            <span class="m-invoice__text"><asp:Label runat="server" ID="lblTipoPersonaPagoChequeResumen"></asp:Label>                                                                                </span>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="m-invoice__body m-invoice__body--centered">
                                                                <div class="table-responsive">
                                                                    <table class="table">
                                                                        <thead>
                                                                            <tr>
                                                                                <th>PAGO A LA ORDEN DE</th>
                                                                                <th>TIPO MONEDA.</th>                                                                                
                                                                                <th>TOTAL</th>
                                                                            </tr>
                                                                        </thead>
                                                                        <tbody>
                                                                            <tr>                                                      
                                                                                <td><asp:Label runat="server" ID="lblPagoOrdenDeChequeResumen"></asp:Label></td>
                                                                                <td><asp:Label runat="server" ID="lblTipoMonedaChequeResumen"></asp:Label></td>
                                                                                <td><asp:Label runat="server" ID="lblSubTotalPagarChequeResumen"></asp:Label></td>
                                                                            </tr>                                                               
                                                                        </tbody>
                                                                    </table>
                                                                </div>
                                                            </div>
                                                            <div class="m-invoice__footer">
                                                                <div class="m-invoice__container m-invoice__container--centered">
                                                                    <div class="m-invoice__content">
                                                                        <span>PAGO POR CHEQUE</span>                                                                  
                                                                    </div>
                                                                    <div class="m-invoice__content">
                                                                        <span>TOTAL A PAGAR</span>
                                                                        <span class="m-invoice__price costototal" runat="server" id="lblTotalPagarChequeResumen"></span>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="m-portlet__foot m-portlet__no-border m-portlet__foot--fit">
                                                        <div class="m-form__actions m-form__actions--solid">
                                                            <div class="row">
                                                                <div class="col-lg-6">
                                                                    <div class="m-btn-group m-btn-group--pill btn-group" role="group" aria-label="First group">
                                                                        <asp:LinkButton runat="server" class="btn btn-secondary" ID="btnRegresarRegistroCheque" OnClick="btnRegresarRegistroCheque_Click">
                                                                    <i class="la la-mail-reply"></i> Regresar
                                                                        </asp:LinkButton>
                                                                        <asp:LinkButton runat="server" class="btn btn-warning" ID="btnCancelarEmisionCheque" OnClick="btnCancelarEmisionCheque_Click" >
                                                                    <i class="la la-close"></i> Cancelar
                                                                        </asp:LinkButton>
                                                                    </div>                                                                                                                                 
                                                                </div>
                                                                <div class="col-lg-6 m--align-right">
                                                                    <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#gpConfirmarEmitirCheque">Emitir Cheque</button>
                                                                </div>                                                           
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div id="gpReporteRVIndividual" runat="server">
                                                    <div class="col-md-12 m--margin-top-50">
                                                        <div class="col-md-12">
                                                            <center>                                                              
                                                            <rsweb:ReportViewer ID="ReportViewer1" runat="server" font-names="Verdana" font-size="8pt" waitmessagefont-names="Verdana" waitmessagefont-size="14pt" backcolor="#F2F2F2"  bordercolor="#2F4050" borderstyle="Solid" borderwidth="1px" SizeToReportContent="true" DocumentMapWidth="100%" PromptAreaCollapsed="True">
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
                            <!--end: Search Form -->
                            <!--begin: Datatable -->
                            <div id="gpListadoChequesBusqueda" runat="server">
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
                                        <asp:GridView runat="server" ID="dgvContratistas" Width="100%" AutoGenerateColumns="false" CssClass="table table-bordered" RowStyle-BackColor="#FFFFFF" AlternatingRowStyle-BackColor="#F5F5F6" HeaderStyle-BackColor="#F5F5F6" RowStyle-Wrap="false" ShowHeaderWhenEmpty="true" AllowPaging="True" PageSize="10" OnPageIndexChanging="dgvContratistas_PageIndexChanging" OnRowDataBound="dgvContratistas_RowDataBound" OnRowCommand="dgvContratistas_RowCommand">
                                            <Columns>
                                                <asp:BoundField DataField="CodCheque" HeaderText="CodProyecto" HtmlEncode="false" HeaderStyle-CssClass="hidden" Visible="false" ItemStyle-CssClass="hidden" />
                                                <asp:BoundField DataField="DETALLE" HeaderText="PAGO A LA ORDEN DE" HtmlEncode="false" ItemStyle-Width="15%" HeaderStyle-Width="15%" />
                                                <asp:BoundField DataField="DESCRIPCION" HeaderText="DETALLE" HtmlEncode="false" ItemStyle-Width="10%" HeaderStyle-Width="10%" />
                                                <asp:BoundField DataField="NumeroCheque" HeaderText="N° CHEQUE" HtmlEncode="false" ItemStyle-Width="5%" HeaderStyle-Width="5%"/>
                                                <asp:BoundField DataField="MontoTotal" HeaderText="MONTO S/." HtmlEncode="false" ItemStyle-Width="10%"  HeaderStyle-Width="10%"/>
                                                <asp:BoundField DataField="FechaPago" HeaderText="FECHA PAGO" HtmlEncode="false" ItemStyle-Width="5%" HeaderStyle-Width="5%" />
                                                <asp:BoundField DataField="FechaEmision" HeaderText="FECHA EMISIÓN" HtmlEncode="false" ItemStyle-Width="5%" HeaderStyle-Width="5%"/>
                                                <asp:BoundField DataField="Usuario" HeaderText="USUARIO EMISIÓN" HtmlEncode="false" ItemStyle-Width="5%" HeaderStyle-Width="5%"/>
                                                <asp:BoundField DataField="CodEstado" HeaderText="ESTADO" HtmlEncode="false" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="header-center" ItemStyle-Width="10%" HeaderStyle-Width="10%"/>
                                                <asp:TemplateField HeaderText="OPCIÓN" ItemStyle-Width="10%" HeaderStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnModificarCheque" runat="server" CssClass="btn btn-default m-btn--icon btn-sm m-btn--icon-only" data-container="body" data-toggle="m-tooltip" data-placement="top" title="Modificar Cheque" data-original-title="Modificar Cheque" 
                                                            Text='<i class="la la-pencil"></i>'
                                                            CommandName="ModificarCheque"
                                                            Visible='<%#Eval("CodEstado").ToString() != "3" ? true : false %>'
                                                            CommandArgument='<%# Eval("CodCheque")%>'>
                                                        </asp:LinkButton>
                                                           <asp:LinkButton ID="btnAnularCheque" runat="server" CssClass="btn btn-danger m-btn--icon btn-sm m-btn--icon-only" data-container="body" data-toggle="m-tooltip" data-placement="top" title="Anular Cheque" data-original-title="Anular Cheque"
                                                            Text='<i class="la la-times-circle"></i>'
                                                            CommandName="AnularCheque"
                                                            Visible='<%#Eval("CodEstado").ToString() != "3" ? true : false %>'
                                                            CommandArgument='<%# Eval("CodCheque")%>'>
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                         
                                                 <asp:TemplateField HeaderText="ACCIÓN" ItemStyle-Width="15%" HeaderStyle-Width="15%">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnEmitirCheque" runat="server" CssClass="btn btn-success m-btn--icon btn-sm m-btn--icon-only" data-container="body" data-toggle="m-tooltip" data-placement="top" title="Emitir Cheque" data-original-title="Emitir Cheque"
                                                            Text='<i class="la la-lightbulb-o"></i>'
                                                            CommandName="EmitirCheque"
                                                            Visible='<%#Eval("CodEstado").ToString().Contains("1") ? true : false %>'
                                                            CommandArgument='<%# Eval("CodCheque")%>'>
                                                        </asp:LinkButton>
                                                         <asp:LinkButton ID="btnImprimirCheque" runat="server" CssClass="btn btn-default m-btn--icon btn-sm m-btn--icon-only" data-container="body" data-toggle="m-tooltip" data-placement="top" title="Imprimir Cheque" data-original-title="Imprimir Cheque"
                                                            Text='<i class="la la-print"></i>'
                                                            CommandName="ImprimirCheque"
                                                            Visible='<%#Eval("CodEstado").ToString().Contains("2") ? true : false %>'
                                                            CommandArgument='<%# Eval("CodCheque")%>'>
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
                     <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 no-padding" id="gpHiddenControls" style="display: none" runat="server">
                        <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                            <div class="form-group">
                                <asp:LinkButton runat="server" ID="btnBuscarChequeTipoPago" CssClass="btnbuscarcheque" OnClick="btnBuscarChequeTipoPago_Click"></asp:LinkButton>
                                <asp:TextBox runat="server" ID="txtValoresCheckBox" CssClass="valorescheckbox"></asp:TextBox>
                                <asp:TextBox runat="server" ID="txtValoresContratos" CssClass="valorescontratos"></asp:TextBox>
                                <asp:LinkButton runat="server" ID="btnGenerarMontoLetras" CssClass="botonmontoletras" OnClick="btnGenerarMontoLetras_Click"></asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>  
                <div class="modal inmodal fade" id="gpSeleccionarPagoContratista" tabindex="-1" role="dialog" aria-hidden="true">
                    <div class="modal-dialog modal-lg" style="max-width:1200px">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h5 class="modal-title" id="exampleModalLabel">Pago Contratistas</h5>
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">×</span>
                                </button>
                            </div>
                            <div class="modal-body">
                                <div class="m-form m-form--label-align-right m--margin-top-20 m--margin-bottom-30">
                                    <div class="row align-items-center">
                                        <div class="col-xl-12 order-2 order-xl-1">
                                            <div class="form-group m-form__group row align-items-center">
                                                <div class="col-md-6">
                                                    <div class="m-form__group m-form__group--inline">
                                                        <div class="m-form__label">
                                                            <label>Contratista:</label>
                                                        </div>
                                                        <div class="m-form__control">
                                                            <asp:DropDownList runat="server" ID="cboContratistaModalSeleccion" CssClass="form-control m-bootstrap-select cbocontratista">
                                                            </asp:DropDownList>                                                           
                                                        </div>
                                                    </div>
                                                    <div class="d-md-none m--margin-bottom-10"></div>
                                                </div>                                       
                                                <div class="col-md-6">
                                                    <div class="m-input-icon m-input-icon--left">
                                                        <input type="text" class="form-control m-input" placeholder="Buscar por nombre o documento de identidad..." id="generalSearch">
                                                        <span class="m-input-icon__icon m-input-icon__icon--left">
                                                            <span>
                                                                <i class="la la-search"></i>
                                                            </span>
                                                        </span>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>                              
                                    </div>
                                </div>
                                 <div id="gpTablaPagoContratistas" runat="server">
                                </div>
                            </div>
                            <div class="modal-footer">
                                <asp:LinkButton runat="server" data-dismiss="modal" CssClass="btn btn-sm btn-secondary btn-icon"><i class="fa fa-arrow-circle-left" aria-hidden="true"></i> REGRESAR</asp:LinkButton>                                
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
    <div class="modal inmodal fade" id="gpConfirmarEmitirCheque" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Confirmación</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">×</span>
                    </button>
                </div>
                <div class="modal-body">
                    <p style="font-weight: 400">¿Está seguro(a) que desea emitir el presente <strong>cheque</strong>?</p>
                </div>
                <div class="modal-footer">
                    <asp:LinkButton runat="server" data-dismiss="modal" CssClass="btn btn-sm btn-secondary btn-icon"><i class="fa fa-arrow-circle-left" aria-hidden="true"></i> REGRESAR</asp:LinkButton>
                    <asp:LinkButton ID="btnEmitirCheque" CssClass="btn btn-sm btn-primary btn-icon" runat="server" OnClick="btnEmitirCheque_Click"><i class="fa fa-check-circle" aria-hidden="true"></i> CONFIRMAR</asp:LinkButton>

                </div>
            </div>
        </div>
    </div>
        <div class="modal inmodal fade" id="gpConfirmarAnularCheque" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Confirmación</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">×</span>
                    </button>
                </div>
                <div class="modal-body">
                    <p style="font-weight: 400">¿Está seguro(a) que desea anular el presente <strong>cheque</strong>? Si el cheque tiene relacionado el pago a contratitas o proveedores, al ser anulado el cheque, estos pagos no se reflejaran.</p>
                </div>
                <div class="modal-footer">
                    <asp:LinkButton runat="server" data-dismiss="modal" CssClass="btn btn-sm btn-secondary btn-icon"><i class="fa fa-arrow-circle-left" aria-hidden="true"></i> REGRESAR</asp:LinkButton>
                    <asp:LinkButton ID="btnAnularCheque" CssClass="btn btn-sm btn-danger btn-icon" runat="server" OnClick="btnAnularCheque_Click" ><i class="fa fa-check-circle" aria-hidden="true"></i> CONFIRMAR</asp:LinkButton>

                </div>
            </div>
        </div>
    </div>
    <script src="<%=ConfigurationManager.AppSettings["AssetsUrl"]%>/assets/js/jquery-3.2.1.js" type="text/javascript"></script>  
    <script src="<%=ConfigurationManager.AppSettings["AssetsUrl"]%>/assets/js/cheques_partial.js" type="text/javascript"></script>  
</asp:Content>
