<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="RegistroEventuales.aspx.cs" Inherits="SistemaGestionPlanilla.Trabajador.RegistroEventuales" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript"> 
        function pageLoad() {
            $(function () {

                ReloadJqueryWizardEventuales();

                $('.continuar').on('click', function () {
                    ResumenEventuales();
                });

                $('.guardando').on('click', function () {
                    SubirFormulario();
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
                                    <h3 class="m-portlet__head-text"><asp:Label runat="server" ID="lblFormularioTrabajador"></asp:Label> 
                                    </h3>
                                </div>
                            </div>
                            <div class="m-portlet__head-tools">
                                <ul class="m-portlet__nav">
                                    <li class="m-portlet__nav-item">
                                        <a href="#" data-toggle="m-tooltip" class="m-portlet__nav-link m-portlet__nav-link--icon" data-direction="left" data-width="auto" title="Cada nuevo trabajador eventual debe ser agregado con un tipo de labor de trabajo.">
                                            <i class="flaticon-info m--icon-font-size-lg3"></i>
                                        </a>
                                    </li>
                                </ul>
                            </div>
                        </div>

                        <!--end: Portlet Head-->

                        <!--begin: Portlet Body-->
                        <div class="m-portlet__body m-portlet__body--no-padding">

                            <div class="m-portlet__body" id="gpConsultaTrabajador" runat="server">
                                <!--begin: Search Form -->
                                <div class="m-form m-form--label-align-right  m--margin-bottom-30">
                                    <div class="row align-items-center">
                                        <div class="col-xl-10 order-2 order-xl-1">
                                            <div class="form-group m-form__group row align-items-center">
                                                <div class="col-md-12">
                                                    <div class="search-form">
                                                        <div class="input-group">
                                                            <asp:TextBox runat="server" ID="txtTextoBuscar" placeholder="Ingrese una parte del nombre" CssClass="form-control input-lg" MaxLength="200"></asp:TextBox>
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
                                            <a runat="server" id="btnAgregarTrabajador" class="btn btn-primary m-btn m-btn--custom m-btn--icon m-btn--air m-btn--pill" style="color: #ffffff;" onserverclick="btnAgregarTrabajador_ServerClick">
                                                <span>
                                                    <i class="la la-plus"></i>
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
                                                <i class="flaticon-information"></i>
                                            </span>
                                            <h3 class="m-portlet__head-text" style="font-weight: 400; font-size: 1.2rem" id="lblResultadoBusqueda" runat="server"></h3>

                                        </div>
                                    </div>

                                </div>
                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="padding-left: 0px">
                                    <div class="m-datable table-responsive">
                                        <asp:GridView runat="server" ID="dgvTrabajadoresPlanilla" Width="100%" AutoGenerateColumns="false" CssClass="table table-bordered" RowStyle-BackColor="#FFFFFF" AlternatingRowStyle-BackColor="#F5F5F6" HeaderStyle-BackColor="#F5F5F6" HeaderStyle-Wrap="true" RowStyle-Wrap="true" ShowHeaderWhenEmpty="true" AllowPaging="True" PageSize="7" OnRowCommand="dgvTrabajadoresPlanilla_RowCommand" OnRowDataBound="dgvTrabajadoresPlanilla_RowDataBound"  OnPageIndexChanging="dgvTrabajadoresPlanilla_PageIndexChanging">
                                            <Columns>
                                                <asp:BoundField DataField="CodTrabajador" HeaderText="CodTrabajador" HtmlEncode="false" HeaderStyle-CssClass="hidden" Visible="false" ItemStyle-CssClass="hidden" />
                                                <asp:BoundField DataField="FechaIngreso" HeaderText="FECHA INGRESO" HtmlEncode="false" />
                                                <asp:BoundField DataField="Nombres" HeaderText="NOMBRE TRABAJADOR" HtmlEncode="false" />
                                                <asp:BoundField DataField="Labor" HeaderText="OFICIO" HtmlEncode="false" />
                                                <asp:BoundField DataField="Planilla" HeaderText="PLANILLA" HtmlEncode="false" />                                               
                                                <asp:BoundField DataField="FlagVigente" HeaderText="VIGENCIA" HtmlEncode="false" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="header-center" />                                              
                                                <asp:TemplateField HeaderText="OPCION" ItemStyle-Width="200">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lblDetalles" runat="server" CssClass="btn btn-sm btn-default"
                                                            Text='Ver Detalle'
                                                            CommandName="VerDetalleTrabajador"
                                                            CommandArgument='<%# Eval("CodTrabajador")%>'>
                                                        </asp:LinkButton>
                                                          <asp:LinkButton ID="lblModificar" runat="server" CssClass="btn btn-sm btn-default"
                                                            Text='Modificar'
                                                            CommandName="ModificarTrabajador"
                                                            CommandArgument='<%# Eval("CodTrabajador")%>'>
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="OPCION">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lblEliminar" runat="server" CssClass="btn btn-sm btn-outline-danger "
                                                            Text='Eliminar'
                                                            CommandName="EliminarTrabajador"
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

                            <div id="gpWizardAgregarTrabajador" runat="server">
                            <!--begin: Form Wizard-->
                                 <div class="m-wizard m-wizard--4 m-wizard--brand" id="m_wizard">
                                <div class="row m-row--no-padding">
                                    <div class="col-xl-3 col-lg-12 m--padding-top-20 m--padding-bottom-15">

                                        <!--begin: Form Wizard Head -->
                                        <div class="m-wizard__head">

                                            <!--begin: Form Wizard Nav -->
                                            <div class="m-wizard__nav">
                                                <div class="m-wizard__steps">
                                                    <div class="m-wizard__step m-wizard__step--done" m-wizard-target="m_wizard_form_step_1">
                                                        <div class="m-wizard__step-info">
                                                            <a href="#" class="m-wizard__step-number">
                                                                <span>
                                                                    <span>1</span>
                                                                </span>
                                                            </a>
                                                            <div class="m-wizard__step-label">
                                                                Datos
                                                                <br />
                                                                Eventuales
                                                            </div>
                                                            <div class="m-wizard__step-icon">
                                                                <i class="la la-check"></i>
                                                            </div>
                                                        </div>
                                                    </div>                                              
                                                    <div class="m-wizard__step" m-wizard-target="m_wizard_form_step_2">
                                                        <div class="m-wizard__step-info">
                                                            <a href="#" class="m-wizard__step-number">
                                                                <span>
                                                                    <span>2</span>
                                                                </span>
                                                            </a>
                                                            <div class="m-wizard__step-label">
                                                                Confirmacion
                                                            </div>
                                                            <div class="m-wizard__step-icon">
                                                                <i class="la la-check"></i>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <!--end: Form Wizard Nav -->
                                            
                                        </div>

                                        <!--end: Form Wizard Head -->
                                        <div class="row" style="padding:20px 50px 50px 50px;" >
                                                <div class="col-lg-6 m--align-left">
                                                    <asp:LinkButton runat="server"  CssClass="btn btn-secondary m-btn m-btn--custom m-btn--icon" ID="btnRegresarConsultaTrabajador" OnClick="btnRegresarConsultaTrabajador_Click" >
                                                        <span>
                                                            <i class="la la-rotate-left"></i>&nbsp;&nbsp;
																			<span>Regresar</span>
                                                        </span>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>
                                    </div>
                                    <div class="col-xl-9 col-lg-12">

                                        <!--begin: Form Wizard Form-->
                                        <div class="m-wizard__form">

                                            <!--
							1) Use m-form--label-align-left class to alight the form input lables to the right
							2) Use m-form--state class to highlight input control borders on form validation
						-->
                                            <div class="m-form m-form--label-align-left- m-form--state-">

                                                <!--begin: Form Body -->
                                                <div class="m-portlet__body m-portlet__body--no-padding">

                                                    <!--begin: Form Wizard Step 1-->
                                                    <div class="m-wizard__form-step m-wizard__form-step--current" id="m_wizard_form_step_1">
                                                     
                                                        <div class="m-form__section m-form__section--first">
                                                            <div class="m-form__heading">
                                                                <h3 class="m-form__heading-title">Datos Personales del Trabajador</h3>
                                                            </div>
                                                            <div class="form-group m-form__group row">
                                                                <label class="col-xl-3 col-lg-3 col-form-label">* Nombre:</label>
                                                                <div class="col-xl-9 col-lg-9">
                                                                    <input type="text" name="name" class="form-control m-input" id="txtNombres" placeholder="" >
                                                                    <span class="m-form__help">Ingrese el primer y segundo nombre</span>
                                                                </div>
                                                            </div>
                                                            <div class="form-group m-form__group row">
                                                                <label class="col-xl-3 col-lg-3 col-form-label">* Apellido Paterno:</label>
                                                                <div class="col-xl-9 col-lg-9">
                                                                    <input type="text" name="apaterno" class="form-control m-input" id="txtApellidoPaterno" placeholder="">
                                                                    <span class="m-form__help">Ingrese el apellido paterno</span>
                                                                </div>
                                                            </div>
                                                            <div class="form-group m-form__group row">
                                                                <label class="col-xl-3 col-lg-3 col-form-label">* Apellido Materno:</label>
                                                                <div class="col-xl-9 col-lg-9">
                                                                    <input type="text" name="amaterno" class="form-control m-input" id="txtApellidoMaterno" placeholder="">
                                                                    <span class="m-form__help">Ingrese el apellido materno</span>
                                                                </div>
                                                            </div>
                                                            <div class="form-group m-form__group row">
                                                                <div class="col-lg-3 m-form__group-sub">
                                                                    <label class="form-control-label">* Tipo documento:</label>
                                                                    <select class="form-control m-input" name="tipdoc" id="cboTipoDocumento">
                                                                        <option value="">Seleccionar</option>
                                                                        <option value="1">DNI</option>
                                                                        <option value="2">Pasaporte</option>
                                                                        <option value="3">Carnet Extranjeria</option>

                                                                    </select>
                                                                </div>
                                                                <div class="col-lg-6 m-form__group-sub">
                                                                    <label class="form-control-label">* Nro. Documento Identidad:</label>
                                                                    <input type="text" name="numdoc" class="form-control m-input" placeholder="" id="txtNroDocumento">
                                                                    <span class="m-form__help">Ingrese el numero de documento</span>
                                                                </div>
                                                                  <div class="col-lg-3 m-form__group-sub">
                                                                    <label class="form-control-label">&nbsp;</label><br/>
                                                                    <button type="button" class="btn btn-default" title="Buscar" onclick="CargarDatos()"><i class="la la-search"></i>  Buscar</button>
                                                                    <span class="m-form__help">&nbsp;</span>
                                                                </div>
                                                            </div>
                                                            <div class="form-group m-form__group row">
                                                                <label class="col-xl-3 col-lg-3 col-form-label">* Sexo:</label>
                                                                <div class="col-xl-9 col-lg-9">
                                                                    <select name="sexos" class="form-control m-input" id="cboSexo">
                                                                        <option value="">Seleccionar</option>
                                                                        <option value="Masculino">Masculino</option>
                                                                        <option value="Femenino">Femenino</option>
                                                                    </select>
                                                                </div>
                                                            </div>
                                                            <div class="form-group m-form__group row">
                                                                <label class="col-xl-3 col-lg-3 col-form-label">* Tipo Trabajo:</label>
                                                                <div class="col-xl-9 col-lg-9">
                                                                    <asp:DropDownList runat="server" name="tiptrabajo" CssClass="form-control m-input"  ID="cboTipoTrabajo">                                                                 
                                                                    </asp:DropDownList>
                                                                    <span class="m-form__help">Selecione el tipo de labor que realizara el trabajador.</span>
                                                                </div>
                                                            </div>
                                                             <div class="form-group m-form__group row">
                                                                <label class="col-xl-3 col-lg-3 col-form-label">Perfil Planilla:</label>
                                                                <div class="col-xl-9 col-lg-9">
                                                                    <asp:DropDownList runat="server" name="perfilplanilla" CssClass="form-control m-input"  ID="cboPerfilPlanilla">                                                                 
                                                                    </asp:DropDownList>
                                                                    <span class="m-form__help">Selecione el perfil de planilla del trabajador.</span>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    
                                                    </div>

                                                    <!--end: Form Wizard Step 1-->                                              

                                                    <!--begin: Form Wizard Step 4-->
                                                    <div class="m-wizard__form-step" id="m_wizard_form_step_2">

                                                        <!--begin::Section-->
                                                        <div class="m-accordion m-accordion--default" id="m_accordion_1" role="tablist">

                                                            <!--begin::Item-->
                                                            <div class="m-accordion__item active">
                                                                <div class="m-accordion__item-head" role="tab" id="m_accordion_1_item_1_head" data-toggle="collapse" href="#m_accordion_1_item_1_body" aria-expanded="  false">
                                                                    <span class="m-accordion__item-icon">
                                                                        <i class="fa flaticon-user-ok"></i>
                                                                    </span>
                                                                    <span class="m-accordion__item-title">1. Datos Eventuales</span>
                                                                    <span class="m-accordion__item-mode"></span>
                                                                </div>
                                                                <div class="m-accordion__item-body collapse show" id="m_accordion_1_item_1_body" class=" " role="tabpanel" aria-labelledby="m_accordion_1_item_1_head" data-parent="#m_accordion_1">

                                                                    <!--begin::Content-->
                                                                    <div class="tab-content active  m--padding-30">
                                                                        <div class="m-form__section m-form__section--first">
                                                                            <div class="m-form__heading">
                                                                                <h4 class="m-form__heading-title">Datos Personales</h4>
                                                                            </div>
                                                                            <div class="form-group m-form__group m-form__group--sm row">
                                                                                <label class="col-xl-4 col-lg-4 col-form-label">Nombre:</label>
                                                                                <div class="col-xl-8 col-lg-8">
                                                                                    <span class="m-form__control-static"><asp:Label runat="server" ID="lblNombreTrabajador"></asp:Label></span>
                                                                                </div>
                                                                            </div>
                                                                              <div class="form-group m-form__group m-form__group--sm row">
                                                                                <label class="col-xl-4 col-lg-4 col-form-label">Apellidos Paterno:</label>
                                                                                <div class="col-xl-8 col-lg-8">
                                                                                    <span class="m-form__control-static"><asp:Label runat="server" ID="lblApellidoPaterno"></asp:Label></span>
                                                                                </div>
                                                                            </div>
                                                                              <div class="form-group m-form__group m-form__group--sm row">
                                                                                <label class="col-xl-4 col-lg-4 col-form-label">Apellidos Materno:</label>
                                                                                <div class="col-xl-8 col-lg-8">
                                                                                    <span class="m-form__control-static"><asp:Label runat="server" ID="lblApellidoMaterno"></asp:Label></span>
                                                                                </div>
                                                                            </div>
                                                                            <div class="form-group m-form__group m-form__group--sm row">
                                                                                <label class="col-xl-4 col-lg-4 col-form-label">Tipo Documento Identidad:</label>
                                                                                <div class="col-xl-8 col-lg-8">
                                                                                    <span class="m-form__control-static">
                                                                                        <select class="m-input noborder"  id="lblTipoDocumento" runat="server" disabled="disabled">
                                                                                            <option value="">Seleccionar</option>
                                                                                            <option value="1">DNI</option>
                                                                                            <option value="2">Pasaporte</option>
                                                                                            <option value="3">Carnet Extranjeria</option>

                                                                                        </select></span>
                                                                                </div>
                                                                            </div>
                                                                             <div class="form-group m-form__group m-form__group--sm row">
                                                                                <label class="col-xl-4 col-lg-4 col-form-label">Documento Identidad:</label>
                                                                                <div class="col-xl-8 col-lg-8">
                                                                                    <span class="m-form__control-static"><asp:Label runat="server" ID="lblDocumentoIdentidad"></asp:Label></span>
                                                                                </div>
                                                                            </div>
                                                                             <div class="form-group m-form__group m-form__group--sm row">
                                                                                <label class="col-xl-4 col-lg-4 col-form-label">Sexo</label>
                                                                                <div class="col-xl-8 col-lg-8">
                                                                                    <span class="m-form__control-static"><asp:Label runat="server" ID="lblSexo"></asp:Label></span>
                                                                                </div>
                                                                            </div>
                                                                            <div class="form-group m-form__group m-form__group--sm row">
                                                                                <label class="col-xl-4 col-lg-4 col-form-label">Tipo Trabajo:</label>
                                                                                <div class="col-xl-8 col-lg-8">
                                                                                    <asp:DropDownList name="tipplanilla"  CssClass="m-input noborder"  ID="lblTipoTrabajo" runat="server" disabled="disabled">
                                                                                  
                                                                                    </asp:DropDownList>
                                                                                </div>
                                                                            </div>
                                                                             <div class="form-group m-form__group m-form__group--sm row">
                                                                                <label class="col-xl-4 col-lg-4 col-form-label">Perfil planilla:</label>
                                                                                <div class="col-xl-8 col-lg-8">
                                                                                    <asp:DropDownList name="perfilplanilla"  CssClass="m-input noborder"  ID="lblPerfilPlanilla" runat="server" disabled="disabled">
                                                                                  
                                                                                    </asp:DropDownList>
                                                                                </div>
                                                                            </div>
                                                                         
                                                                       
                                                                        </div>
                                                                      
                                                                    </div>

                                                                    <!--end::Section-->
                                                                </div>
                                                            </div>

                                                            <!--end::Item-->

                                                   
                                                        </div>

                                                        <!--end::Section-->

                                                        <!--end::Section-->
                                              
                                                    </div>

                                                    <!--end: Form Wizard Step 4-->
                                                </div>

                                                <!--end: Form Body -->

                                                <!--begin: Form Actions -->
                                                <div class="m-portlet__foot m-portlet__foot--fit m--margin-top-40">
                                                    <div class="m-form__actions">
                                                      
                                                        <div class="row">
                                                            <div class="col-lg-6 m--align-left">
                                                                <a href="#" class="btn btn-secondary m-btn m-btn--custom m-btn--icon" data-wizard-action="prev">
                                                                    <span>
                                                                        <i class="la la-arrow-left"></i>&nbsp;&nbsp;
																			<span>Atras</span>
                                                                    </span>
                                                                </a>
                                                            </div>
                                                            <div class="col-lg-6 m--align-right">
                                                                <a href="#" class="btn btn-primary m-btn m-btn--custom m-btn--icon guardando" data-wizard-action="submit">
                                                                    <span>
                                                                        <i class="la la-check"></i>&nbsp;&nbsp;
																			<span>Confirmar</span>
                                                                    </span>
                                                                </a>
                                                                <a href="#" class="btn btn-success m-btn m-btn--custom m-btn--icon continuar" data-wizard-action="next">
                                                                    <span>
                                                                        <span>Guardar &amp; Continuar</span>&nbsp;&nbsp;
																			<i class="la la-arrow-right"></i>
                                                                    </span>
                                                                </a>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                                <!--end: Form Actions -->
                                            </div>
                                        </div>

                                        <!--end: Form Wizard Form-->
                                    </div>
                                </div>
                            </div>
                            </div>
                            <!--end: Form Wizard-->
                        </div>
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 no-padding" id="gpActionButton" style="display: none" runat="server">
                            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                               <asp:LinkButton runat="server" ID="btnGuardarTrabajador"  OnClick="btnGuardarTrabajador_Click"></asp:LinkButton>
                               <asp:LinkButton runat="server" ID="btnCargarDatosTrabajador"  OnClick="btnCargarDatosTrabajador_Click"></asp:LinkButton>
                            </div>
                        </div>

                        <!--end: Portlet Body-->
                    </div>
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 no-padding" id="Div1" style="display: none" runat="server">
                        <asp:TextBox ID="txtNombresForm" runat="server"></asp:TextBox>
                        <asp:TextBox ID="txtApellidosPaternosForm" runat="server"></asp:TextBox>
                        <asp:TextBox ID="txtApellidosMaternosForm" runat="server"></asp:TextBox>
                        <asp:TextBox ID="txtNumeroDocumentoForm" runat="server"></asp:TextBox>
                        <asp:TextBox ID="txtSexoForm" runat="server"></asp:TextBox>
                   
                    </div>
                    <!--End::Main Portlet-->
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div class="modal inmodal fade" id="gpConfirmacionEliminarTrabajador" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLabel">Confirmación</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">×</span>
                    </button>
                </div>
                <div class="modal-body">
                    <p style="font-weight: 400">¿Está seguro(a) que desea eliminar el <strong>trabajador</strong> seleccionado?.</p>
                </div>
                <div class="modal-footer">
                    <asp:LinkButton runat="server" data-dismiss="modal" CssClass="btn btn-sm btn-secondary btn-icon"><i class="fa fa-arrow-circle-left" aria-hidden="true"></i> REGRESAR</asp:LinkButton>
                    <asp:LinkButton ID="btnEliminarTrabajador" CssClass="btn btn-sm btn-primary btn-icon" runat="server" OnClick="btnEliminarTrabajador_Click" ><i class="fa fa-check-circle" aria-hidden="true"></i> CONFIRMAR</asp:LinkButton>
                    
                </div>
            </div>
        </div>
    </div>
    <script src="<%=ConfigurationManager.AppSettings["AssetsUrl"]%>/assets/js/jquery-3.2.1.js" type="text/javascript"></script>
    
    <script>
        $(document).ready(function () {
            $('#navMenuTrabajadores').addClass("m-menu__item--open");
            $('#navMenuTrabajadores').addClass("m-menu__item--expanded");
            $('#navSubMenuAgregarEventuales').addClass("m-menu__item--active");


        })

        function ResumenEventuales() {

            $("#<%=lblNombreTrabajador.ClientID%>").text($('#txtNombres').val());
            $("#<%=lblApellidoPaterno.ClientID%>").text($('#txtApellidoPaterno').val());
            $("#<%=lblApellidoMaterno.ClientID%>").text($('#txtApellidoMaterno').val());
            $("#<%=lblTipoDocumento.ClientID%>").val($('#cboTipoDocumento').val());
            $("#<%=lblDocumentoIdentidad.ClientID%>").text($('#txtNroDocumento').val());
            $("#<%=lblSexo.ClientID%>").text($('#cboSexo').val());
            $("#<%=lblTipoTrabajo.ClientID%>").val($("#<%=cboTipoTrabajo.ClientID%>").val());
            $("#<%=lblPerfilPlanilla.ClientID%>").val($("#<%=cboPerfilPlanilla.ClientID%>").val());

            $("#<%=txtNombresForm.ClientID%>").val($("#<%=lblNombreTrabajador.ClientID%>").text());
            $("#<%=txtApellidosPaternosForm.ClientID%>").val($("#<%=lblApellidoPaterno.ClientID%>").text());
            $("#<%=txtApellidosMaternosForm.ClientID%>").val($("#<%=lblApellidoMaterno.ClientID%>").text());
            $("#<%=txtNumeroDocumentoForm.ClientID%>").val($("#<%=lblDocumentoIdentidad.ClientID%>").text());
            $("#<%=txtSexoForm.ClientID%>").val($("#<%=lblSexo.ClientID%>").text());

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

        function SubirFormulario() {

            $('#<%=btnGuardarTrabajador.ClientID%>')[0].click();
        }

        function ReloadJqueryWizardEventuales() {
            var script = document.createElement("script");
            script.setAttribute("type", "text/javascript");
            script.setAttribute("src", "/planilla/assets/js/wizard-eventuales.js");
            document.getElementsByTagName("head")[0].appendChild(script);
        }



        function CargarDatosTrabajador() {

            $("#<%=lblNombreTrabajador.ClientID%>").text($("#<%=txtNombresForm.ClientID%>").val());
            $("#<%=lblApellidoPaterno.ClientID%>").text($("#<%=txtApellidosPaternosForm.ClientID%>").val());
            $("#<%=lblApellidoMaterno.ClientID%>").text($("#<%=txtApellidosMaternosForm.ClientID%>").val());
            $("#<%=lblDocumentoIdentidad.ClientID%>").text($("#<%=txtNumeroDocumentoForm.ClientID%>").val());
            $("#<%=lblSexo.ClientID%>").text($("#<%=txtSexoForm.ClientID%>").val());


            $('#txtNombres').val($("#<%=lblNombreTrabajador.ClientID%>").text());
            $('#txtApellidoPaterno').val($("#<%=lblApellidoPaterno.ClientID%>").text());
            $('#txtApellidoMaterno').val($("#<%=lblApellidoMaterno.ClientID%>").text());
            $('#cboTipoDocumento').val($("#<%=lblTipoDocumento.ClientID%>").val());
            $('#txtNroDocumento').val($("#<%=lblDocumentoIdentidad.ClientID%>").text());
            $('#cboSexo').val($("#<%=lblSexo.ClientID%>").text());

            $("#<%=cboTipoTrabajo.ClientID%>").val($("#<%=lblTipoTrabajo.ClientID%>").val());
            $("#<%=cboPerfilPlanilla.ClientID%>").val($("#<%=lblPerfilPlanilla.ClientID%>").val());

            $("#txtNombres").prop('disabled', true);
            $("#txtApellidoPaterno").prop('disabled', true);
            $("#txtApellidoMaterno").prop('disabled', true);
            $("#cboTipoDocumento").prop('disabled', true);
            $("#txtNroDocumento").prop('disabled', true);
            $("#cboSexo").prop('disabled', true);

            $("#<%=cboTipoTrabajo.ClientID%>").prop('disabled', true);
            $("#<%=cboPerfilPlanilla.ClientID%>").prop('disabled', true);
        }


        function CargarDatosTrabajadorModificar() {

            $("#<%=lblNombreTrabajador.ClientID%>").text($("#<%=txtNombresForm.ClientID%>").val());
            $("#<%=lblApellidoPaterno.ClientID%>").text($("#<%=txtApellidosPaternosForm.ClientID%>").val());
            $("#<%=lblApellidoMaterno.ClientID%>").text($("#<%=txtApellidosMaternosForm.ClientID%>").val());
            $("#<%=lblDocumentoIdentidad.ClientID%>").text($("#<%=txtNumeroDocumentoForm.ClientID%>").val());
            $("#<%=lblSexo.ClientID%>").text($("#<%=txtSexoForm.ClientID%>").val());


            $('#txtNombres').val($("#<%=lblNombreTrabajador.ClientID%>").text());
            $('#txtApellidoPaterno').val($("#<%=lblApellidoPaterno.ClientID%>").text());
            $('#txtApellidoMaterno').val($("#<%=lblApellidoMaterno.ClientID%>").text());
            $('#cboTipoDocumento').val($("#<%=lblTipoDocumento.ClientID%>").val());
            $('#txtNroDocumento').val($("#<%=lblDocumentoIdentidad.ClientID%>").text());
            $('#cboSexo').val($("#<%=lblSexo.ClientID%>").text());

            $("#<%=cboTipoTrabajo.ClientID%>").val($("#<%=lblTipoTrabajo.ClientID%>").val());
            $("#<%=cboPerfilPlanilla.ClientID%>").val($("#<%=lblPerfilPlanilla.ClientID%>").val());


            $("#txtNombres").prop('disabled', false);
            $("#txtApellidoPaterno").prop('disabled', false);
            $("#txtApellidoMaterno").prop('disabled', false);
            $("#cboTipoDocumento").prop('disabled', false);
            $("#txtNroDocumento").prop('disabled', false);
            $("#cboSexo").prop('disabled', false);

            $("#<%=cboTipoTrabajo.ClientID%>").prop('disabled', false);
            $("#<%=cboPerfilPlanilla.ClientID%>").prop('disabled', false);
        }

        function NuevoRegistro() {

            $('#txtNombres').val('');
            $('#txtApellidoPaterno').val('');
            $('#txtApellidoMaterno').val('');
            $('#cboTipoDocumento').val('');
            $('#txtNroDocumento').val('');
            $('#cboSexo').val('');


            $("#txtNombres").prop('disabled', false);
            $("#txtApellidoPaterno").prop('disabled', false);
            $("#txtApellidoMaterno").prop('disabled', false);
            $("#cboTipoDocumento").prop('disabled', false);
            $("#txtNroDocumento").prop('disabled', false);
            $("#cboSexo").prop('disabled', false);

            $("#<%=cboTipoTrabajo.ClientID%>").prop('disabled', false);
            $("#<%=cboPerfilPlanilla.ClientID%>").prop('disabled', false);
        }
        function button_click(objTextBox, objBtnID) {
            if (window.event.keyCode == 13) {
                document.getElementById(objBtnID).focus();
                document.getElementById(objBtnID).click();
            }
        }
        function abrirModalEliminarTrabajador() {
            $('#gpConfirmacionEliminarTrabajador').modal('toggle');
        }
             function CargarDatos() {

           $("#<%=lblTipoDocumento.ClientID%>").val($('#cboTipoDocumento').val());
           $("#<%=txtNumeroDocumentoForm.ClientID%>").val($('#txtNroDocumento').val());

             $('#<%=btnCargarDatosTrabajador.ClientID%>')[0].click();
        }

          function CargarDatosTrabajadorDNI() {
              
            $("#<%=lblNombreTrabajador.ClientID%>").text($("#<%=txtNombresForm.ClientID%>").val());
            $("#<%=lblApellidoPaterno.ClientID%>").text($("#<%=txtApellidosPaternosForm.ClientID%>").val());
            $("#<%=lblApellidoMaterno.ClientID%>").text($("#<%=txtApellidosMaternosForm.ClientID%>").val());
            $("#<%=lblDocumentoIdentidad.ClientID%>").text($("#<%=txtNumeroDocumentoForm.ClientID%>").val());
            $("#<%=lblSexo.ClientID%>").text($("#<%=txtSexoForm.ClientID%>").val());


            $('#txtNombres').val($("#<%=lblNombreTrabajador.ClientID%>").text());
            $('#txtApellidoPaterno').val($("#<%=lblApellidoPaterno.ClientID%>").text());
            $('#txtApellidoMaterno').val($("#<%=lblApellidoMaterno.ClientID%>").text());
            $('#cboTipoDocumento').val($("#<%=lblTipoDocumento.ClientID%>").val());
            $('#txtNroDocumento').val($("#<%=lblDocumentoIdentidad.ClientID%>").text());
            $('#cboSexo').val($("#<%=lblSexo.ClientID%>").text());
     

              $("#txtNombres").prop('disabled', true);
              $("#txtApellidoPaterno").prop('disabled', true);
              $("#txtApellidoMaterno").prop('disabled', true);
              $("#cboTipoDocumento").prop('disabled', true);
              $("#txtNroDocumento").prop('disabled', true);
              $("#cboSexo").prop('disabled', true);
            
           
        }
    </script>
</asp:Content>
