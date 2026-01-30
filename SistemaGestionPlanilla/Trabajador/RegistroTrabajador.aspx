<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="RegistroTrabajador.aspx.cs" Inherits="SistemaGestionPlanilla.Trabajador.RegistroTrabajador" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript"> 
        function pageLoad() {
            $(function () {

                ReloadJqueryWizard();
                ReloadJqueryDatePicker();

                $("#fuImagen").fileinput({
                    language: 'es',

                    theme: 'explorer',
                    uploadAsync: true,
                    initialPreviewAsData: true,
                    showUpload: true,
                    showCancel: false,
                    dropZoneEnabled: false,
                    maxFileCount: 1,
                    maxFileSize: 500000,
                    allowedFileTypes: ['image'],
                    allowedFileExtensions: ['jpg'],
                    browseClass: "btn btn-default",
                    uploadClass: "btn btn-default",
                    removeClass: "btn btn-default",
                    previewFileType: "jpg",
                    overwriteInitial: false,
                })

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
                                        <a href="#" data-toggle="m-tooltip" class="m-portlet__nav-link m-portlet__nav-link--icon" data-direction="left" data-width="auto" title="Cada nuevo trabajador debe ser agregado con un perfil de planilla.">
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
                                            <a runat="server" id="btnAgregarTrabajador" class="btn btn-primary m-btn m-btn--custom m-btn--icon m-btn--air m-btn--pill" onserverclick="btnAgregarTrabajador_ServerClick">
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
                                        <asp:GridView runat="server" ID="dgvTrabajadoresPlanilla" Width="100%" AutoGenerateColumns="false" CssClass="table table-bordered" RowStyle-BackColor="#FFFFFF" AlternatingRowStyle-BackColor="#F5F5F6" HeaderStyle-BackColor="#F5F5F6" HeaderStyle-Wrap="true" RowStyle-Wrap="true" ShowHeaderWhenEmpty="true" AllowPaging="True" PageSize="7" OnRowDataBound="dgvTrabajadoresPlanilla_RowDataBound" OnRowCommand="dgvTrabajadoresPlanilla_RowCommand" OnPageIndexChanging="dgvTrabajadoresPlanilla_PageIndexChanging">
                                            <Columns>
                                                <asp:BoundField DataField="CodTrabajador" HeaderText="CodTrabajador" HtmlEncode="false" HeaderStyle-CssClass="hidden" Visible="false" ItemStyle-CssClass="hidden" />
                                                <asp:BoundField DataField="FechaIngreso" HeaderText="FECHA INGRESO" HtmlEncode="false" />
                                                <asp:BoundField DataField="Nombres" HeaderText="NOMBRE TRABAJADOR" HtmlEncode="false" />
                                                <asp:BoundField DataField="Cargo" HeaderText="CARGO" HtmlEncode="false" />
                                                <asp:BoundField DataField="Planilla" HeaderText="PLANILLA" HtmlEncode="false" />
                                                <asp:BoundField DataField="DescPerfilPlanilla" HeaderText="PERFIL PLANILLA" HtmlEncode="false" />
                                                <asp:BoundField DataField="FlagVigente" HeaderText="VIGENCIA" HtmlEncode="false" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="header-center" />                                              
                                                <asp:BoundField DataField="CodTipoPlanilla" HeaderText="CodTipoPlanilla" HtmlEncode="false" HeaderStyle-CssClass="hidden" Visible="false" ItemStyle-CssClass="hidden" />
                                                <asp:TemplateField HeaderText="OPCION" ItemStyle-Width="200">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lblDetalles" runat="server" CssClass="btn btn-sm btn-default"
                                                            Text='Ver Detalle'
                                                            CommandName="VerDetalleTrabajador"
                                                            CommandArgument='<%# Eval("CodTrabajador") + "|" + Eval("CodTipoPlanilla")%>'>
                                                        </asp:LinkButton>
                                                          <asp:LinkButton ID="lblModificar" runat="server" CssClass="btn btn-sm btn-default"
                                                            Text='Modificar'
                                                            CommandName="ModificarTrabajador"
                                                            CommandArgument='<%# Eval("CodTrabajador") + "|" + Eval("CodTipoPlanilla")%>'>
                                                        </asp:LinkButton>
                                                     
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="OPCION">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lblEliminar" runat="server" CssClass="btn btn-sm btn-outline-danger "
                                                            Text='Eliminar'
                                                            CommandName="EliminarTrabajador"
                                                            CommandArgument='<%# Eval("CodTrabajador") + "|" + Eval("CodTipoPlanilla")%>'>
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
                                                                Personales
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
                                                                Datos
                                                                <br />
                                                                Planilla
                                                            </div>
                                                            <div class="m-wizard__step-icon">
                                                                <i class="la la-check"></i>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="m-wizard__step" m-wizard-target="m_wizard_form_step_3">
                                                        <div class="m-wizard__step-info">
                                                            <a href="#" class="m-wizard__step-number">
                                                                <span>
                                                                    <span>3</span>
                                                                </span>
                                                            </a>
                                                            <div class="m-wizard__step-label">
                                                                Perfil
                                                                <br />
                                                                Planilla
                                                            </div>
                                                            <div class="m-wizard__step-icon">
                                                                <i class="la la-check"></i>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="m-wizard__step" m-wizard-target="m_wizard_form_step_4">
                                                        <div class="m-wizard__step-info">
                                                            <a href="#" class="m-wizard__step-number">
                                                                <span>
                                                                    <span>4</span>
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
                                                    <asp:LinkButton runat="server"  CssClass="btn btn-secondary m-btn m-btn--custom m-btn--icon" ID="btnRegresarConsultaTrabajador" OnClick="btnRegresarConsultaTrabajador_Click">
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
                                                                <label class="col-xl-3 col-lg-3 col-form-label">* Estado Civil:</label>
                                                                <div class="col-xl-9 col-lg-9">
                                                                    <select name="estadociv" class="form-control m-input" id="cboEstadoCivil">
                                                                        <option value="">Seleccionar</option>
                                                                        <option value="1">Soltero</option>
                                                                        <option value="2">Casado</option>
                                                                        <option value="3">Divorciado</option>
                                                                        <option value="4">Conviviente</option>
                                                                    </select>
                                                                </div>
                                                            </div>
                                                            <div class="form-group m-form__group row">
                                                                <label class="col-xl-3 col-lg-3 col-form-label">* Fecha Nacimiento:</label>
                                                                <div class="col-xl-9 col-lg-9">
                                                                    <div class="input-group date">
                                                                        <input type="text" class="form-control m-input" name="fnacimiento" readonly placeholder="Seleccionar fecha" id="m_datepicker_fn" />
                                                                        <div class="input-group-append">
                                                                            <span class="input-group-text">
                                                                                <i class="la la-calendar-check-o"></i>
                                                                            </span>
                                                                        </div>
                                                                    </div>
                                                                    <span class="m-form__help">Ingrese la fecha de nacimiento</span>
                                                                </div>
                                                            </div>
                                        
                                                        </div>
                                                      <%--  <div class="m-separator m-separator--dashed m-separator--lg"></div>--%>
                                                        <div class="m-form__section" style="display:none">
                                                            <div class="m-form__heading">
                                                                <h3 class="m-form__heading-title">Foto del Trabajador
																		<i data-toggle="m-tooltip" data-width="auto" class="m-form__heading-help-icon flaticon-info" title="La carga de la foto es opcional"></i>
                                                                </h3>
                                                            </div>
                                                            <div class="form-group m-form__group row">

                                                                <div class="col-xl-12 col-lg-12">
                                                                    <input type="file" id="fuImagen" data-min-file-count="1" aria-describedby="fileHelp" accept="image/*" />
                                                                    <span class="m-form__help">Examine la foto que desea agregar al perfil del trabajador.</span>
                                                                </div>
                                                            </div>

                                                        </div>
                                                    </div>

                                                    <!--end: Form Wizard Step 1-->

                                                    <!--begin: Form Wizard Step 2-->
                                                    <div class="m-wizard__form-step" id="m_wizard_form_step_2">
                                                        <div class="m-form__section m-form__section--first">
                                                            <div class="m-form__heading">
                                                                <h3 class="m-form__heading-title">Datos Planilla</h3>
                                                            </div>
                                                            <div class="form-group m-form__group row">
                                                                <label class="col-xl-3 col-lg-3 col-form-label">* Tipo Planilla:</label>
                                                                <div class="col-xl-9 col-lg-9">
                                                                    <select name="tipplanilla" class="form-control m-input" id="cboTipoPlanilla" >
                                                                        <option value="">Seleccionar</option>
                                                                        <option value="1">Construccion</option>     
                                                                    
                                                                        <option value="3">Empleados</option>
                                                                    </select>
                                                                    <span class="m-form__help">Selecione el tipo de planilla que se aplicara al trabajador.</span>
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
                                                                <label class="col-xl-3 col-lg-3 col-form-label">* Cargo:</label>
                                                                <div class="col-xl-9 col-lg-9">
                                                                    <select name="cargo" class="form-control m-input" id="cboCargo">
                                                                        <option value="">Seleccionar</option>
                                                                        <option value="1">Jefe Logistica</option>
                                                                        <option value="2">Gerente General</option>
                                                                        <option value="3">Empleado</option>
                                                                        <option value="4">Contador</option>
                                                                        <option value="5">Administrador 1</option>
                                                                        <option value="6">Administrador 2</option>
                                                                        <option value="7">Almacenero</option>
                                                                        <option value="8">Operario</option>
                                                                        <option value="9">Oficial</option>
                                                                        <option value="10">Peon</option>
                                                                        <option value="11">Maestro de Obra</option>
                                                                        <option value="12">Soporte Tecnico</option>
                                                                        <option value="13">Guardiana</option>
                                                                        <option value="14">Peon Vigilante</option>
                                                                        <option value="15">Contratista</option>
                                                                        <option value="16">Enfermera</option>
                                                                        <option value="17">Prevencionista</option>
                                                                    </select>
                                                                    <span class="m-form__help">Selecione el cargo del trabajador.</span>
                                                                </div>
                                                            </div>
                                                            <div class="form-group m-form__group row">
                                                                <label class="col-xl-3 col-lg-3 col-form-label">* Fecha Ingreso/Cese:</label>
                                                                <div class="col-xl-4 col-lg-4">
                                                                    <div class="input-group date">
                                                                        <input type="text" class="form-control m-input" name="fingreso" readonly placeholder="Seleccionar fecha" id="m_datepicker_fi" />
                                                                        <div class="input-group-append">
                                                                            <span class="input-group-text">
                                                                                <i class="la la-calendar-check-o"></i>
                                                                            </span>
                                                                        </div>
                                                                    </div>
                                                                    <span class="m-form__help">Ingrese la fecha de Ingreso</span>
                                                                </div>
                                                                <div class="col-xl-4 col-lg-4">
                                                                    <div class="input-group date">
                                                                        <input type="text" class="form-control m-input" name="fcese" readonly placeholder="Seleccionar fecha" id="m_datepicker_fc" />
                                                                        <div class="input-group-append">
                                                                            <span class="input-group-text">
                                                                                <i class="la la-calendar-check-o"></i>
                                                                            </span>
                                                                        </div>
                                                                    </div>
                                                                    <span class="m-form__help">Ingrese la fecha de Cese</span>
                                                                </div>
                                                            </div>
                                                            <div class="form-group m-form__group row">
                                                                <label class="col-xl-3 col-lg-3 col-form-label">* Haber Mensual:</label>
                                                                <div class="col-xl-9 col-lg-9">
                                                                    <div class="input-group">
                                                                        <div class="input-group-prepend">
                                                                            <span class="input-group-text">S/.</span>
                                                                            <span class="input-group-text">0.00</span>
                                                                        </div>
                                                                        <input type="text" name="hmensual" class="form-control m-input" placeholder="" id="txtHaberMensual">
                                                                    </div>
                                                                    <span class="m-form__help">Ingrese el haber mensual del trabajador</span>
                                                                </div>
                                                            </div>
                                                            <div class="form-group m-form__group row" style="display:none">
                                                                <label class="col-xl-3 col-lg-3 col-form-label">* Asignación Familiar:</label>
                                                                <div class="col-xl-9 col-lg-9">
                                                                    <div class="input-group">
                                                                        <div class="input-group-prepend">
                                                                            <span class="input-group-text">S/.</span>
                                                                            <span class="input-group-text">0.00</span>
                                                                        </div>
                                                                        <input type="text" name="" class="form-control m-input" placeholder="" id="txtAsigFamiliar" value="0.00">
                                                                    </div>
                                                                    <span class="m-form__help">Ingrese asignacion familiar si corresponde</span>
                                                                </div>
                                                            </div>
                                                            <div class="form-group m-form__group row">
                                                                <div class="col-lg-3 m-form__group-sub">
                                                                    <label class="form-control-label">* Tipo aportación:</label>
                                                                    <select class="form-control m-input" name="tipaport" id="cboTipoAportacion">
                                                                        <option value="">Seleccionar</option>
                                                                        <option value="1">AFP Prima</option>
                                                                        <option value="2">AFP Habitat</option>
                                                                        <option value="3">AFP Integra</option>
                                                                        <option value="4">AFP ProFuturo</option>
                                                                        <option value="5">SNP</option>
                                                                        <option value="6">Ninguna</option>
                                                                    </select>
                                                                </div>
                                                                <div class="col-lg-9 m-form__group-sub">
                                                                    <label class="form-control-label">* Nro. CUSPP:</label>
                                                                    <input type="text" name="numcuspp" class="form-control m-input" placeholder="" id="txtNumCuspp">
                                                                    <span class="m-form__help">Ingrese el numero de CUSPP</span>
                                                                </div>
                                                            </div>
                                                            <div class="form-group m-form__group row">
                                                                <div class="col-lg-3 m-form__group-sub">
                                                                    <label class="form-control-label">* Banco:</label>
                                                                    <select class="form-control m-input" name="banco" id="cboBanco">
                                                                        <option value="">Seleccionar</option>
                                                                        <option value="1">Scotiabank</option>
                                                                        <option value="2">BCP</option>
                                                                        <option value="3">Interbank</option>

                                                                    </select>
                                                                </div>
                                                                <div class="col-lg-9 m-form__group-sub">
                                                                    <label class="form-control-label">* Nro. de cuenta:</label>
                                                                    <input type="text" name="numcuenta" class="form-control m-input" placeholder="" id="txtNroCuentaBanco">
                                                                    <span class="m-form__help">Ingrese el numero de cuenta del banco.</span>
                                                                </div>
                                                            </div>
                                                            <div class="form-group m-form__group row">
                                                                <label class="col-xl-3 col-lg-3 col-form-label">* Nro. cuenta CTS:</label>
                                                                <div class="col-xl-9 col-lg-9">
                                                                    <input type="text" name="cuentacts" class="form-control m-input" placeholder="" id="txtNroCuentaCTS">
                                                                    <span class="m-form__help">Ingrese el numero de cuenta de CTS</span>
                                                                </div>
                                                            </div>
                                                          
                                                        </div>
                                                      
                                                    </div>

                                                    <!--end: Form Wizard Step 2-->

                                                    <!--begin: Form Wizard Step 3-->
                                                    <div class="m-wizard__form-step" id="m_wizard_form_step_3">
                                                        <div class="m-form__section m-form__section--first">
                                                            <div class="m-form__heading">
                                                                <h3 class="m-form__heading-title">Seleccion del Perfil Planilla</h3>
                                                            </div>
                                                            <div class="form-group m-form__group row">
                                                                <label class="col-xl-3 col-lg-3 col-form-label">* Perfil Planilla:</label>
                                                                <div class="col-xl-9 col-lg-9">
                                                                    <asp:DropDownList runat="server" CssClass="form-control m-input" ID="cboPerfilPlanilla" >
                                                                    </asp:DropDownList>
                                                                    <span class="m-form__help">Selecione el tipo perfil planilla que aplicara al trabajador.</span>
                                                                </div>
                                                            </div>

                                                        </div>
                                                    


                                                    </div>

                                                    <!--end: Form Wizard Step 3-->

                                                    <!--begin: Form Wizard Step 4-->
                                                    <div class="m-wizard__form-step" id="m_wizard_form_step_4">

                                                        <!--begin::Section-->
                                                        <div class="m-accordion m-accordion--default" id="m_accordion_1" role="tablist">

                                                            <!--begin::Item-->
                                                            <div class="m-accordion__item active">
                                                                <div class="m-accordion__item-head" role="tab" id="m_accordion_1_item_1_head" data-toggle="collapse" href="#m_accordion_1_item_1_body" aria-expanded="  false">
                                                                    <span class="m-accordion__item-icon">
                                                                        <i class="fa flaticon-user-ok"></i>
                                                                    </span>
                                                                    <span class="m-accordion__item-title">1. Datos del Trabajador</span>
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
                                                                                <label class="col-xl-4 col-lg-4 col-form-label">Estado Civil</label>
                                                                                <div class="col-xl-8 col-lg-8">
                                                                                    <span class="m-form__control-static">
                                                                                        <select class="m-input noborder" id="lblEstadoCivil" disabled="disabled" runat="server">
                                                                                            <option value="">Seleccionar</option>
                                                                                            <option value="1">Soltero</option>
                                                                                            <option value="2">Casado</option>
                                                                                            <option value="3">Divorciado</option>
                                                                                            <option value="4">Conviviente</option>
                                                                                        </select>
                                                                                </div>
                                                                            </div>
                                                                            <div class="form-group m-form__group m-form__group--sm row">
                                                                                <label class="col-xl-4 col-lg-4 col-form-label">Fecha Nacimiento</label>
                                                                                <div class="col-xl-8 col-lg-8">
                                                                                    <span class="m-form__control-static">
                                                                                        <asp:Label runat="server" ID="lblFechaNacimiento"></asp:Label></span>
                                                                                </div>
                                                                            </div>
                                                                         
                                                                       
                                                                        </div>
                                                                      
                                                                    </div>

                                                                    <!--end::Section-->
                                                                </div>
                                                            </div>

                                                            <!--end::Item-->

                                                            <!--begin::Item-->
                                                            <div class="m-accordion__item">
                                                                <div class="m-accordion__item-head collapsed" role="tab" id="m_accordion_1_item_2_head" data-toggle="collapse" href="#m_accordion_1_item_2_body" aria-expanded="    false">
                                                                    <span class="m-accordion__item-icon">
                                                                        <i class="fa flaticon-piggy-bank"></i>
                                                                    </span>
                                                                    <span class="m-accordion__item-title">2. Datos de Planilla</span>
                                                                    <span class="m-accordion__item-mode"></span>
                                                                </div>
                                                                <div class="m-accordion__item-body collapse" id="m_accordion_1_item_2_body" class=" " role="tabpanel" aria-labelledby="m_accordion_1_item_2_head" data-parent="#m_accordion_1">

                                                                    <!--begin::Content-->
                                                                    <div class="tab-content  m--padding-30">
                                                                        <div class="m-form__section m-form__section--first">
                                                                            <div class="m-form__heading">
                                                                                <h4 class="m-form__heading-title">Detalles Planilla</h4>
                                                                            </div>
                                                                            <div class="form-group m-form__group m-form__group--sm row">
                                                                                <label class="col-xl-4 col-lg-4 col-form-label">Tipo Planilla:</label>
                                                                                <div class="col-xl-8 col-lg-8">
                                                                                      <select name="tipplanilla" class="m-input noborder" id="lblTipoPlanilla" runat="server" disabled="disabled" >
                                                                                        <option value="">Seleccionar</option>
                                                                                        <option value="1">Construccion</option>
                                                                                      
                                                                                        <option value="3">Empleados</option>
                                                                                    </select>
                                                                                </div>
                                                                            </div>
                                                                            <div class="form-group m-form__group m-form__group--sm row">
                                                                                <label class="col-xl-4 col-lg-4 col-form-label">Tipo Trabajo:</label>
                                                                                <div class="col-xl-8 col-lg-8">
                                                                                   <%-- <select name="tipplanilla" class="m-input noborder" id="lblTipoTrabajo" runat="server" disabled="disabled">
                                                                                        <option value="">Seleccionar</option>
                                                                                        <option value="1">Dependiente Construccion</option>
                                                                                        <option value="2">Jefe de Obra</option>
                                                                                    </select>--%>
                                                                                     <asp:DropDownList name="tipplanilla"  CssClass="m-input noborder"  ID="lblTipoTrabajo" runat="server" disabled="disabled">
                                                                                  
                                                                                    </asp:DropDownList>
                                                                                </div>
                                                                            </div>
                                                                              <div class="form-group m-form__group m-form__group--sm row">
                                                                                <label class="col-xl-4 col-lg-4 col-form-label">Cargo:</label>
                                                                                <div class="col-xl-8 col-lg-8">
                                                                                      <select name="tipplanilla" class="m-input noborder" id="lblCargo" runat="server" disabled="disabled">
                                                                                       <option value="">Seleccionar</option>
                                                                                          <option value="">Seleccionar</option>
                                                                                        <option value="1">Jefe Logistica</option>
                                                                                        <option value="2">Gerente General</option>
                                                                                        <option value="3">Empleado</option>
                                                                                        <option value="4">Contador</option>
                                                                                        <option value="5">Administrador 1</option>
                                                                                        <option value="6">Administrador 2</option>
                                                                                        <option value="7">Almacenero</option>
                                                                                        <option value="8">Operario</option>
                                                                                        <option value="9">Oficial</option>
                                                                                        <option value="10">Peon</option>
                                                                                        <option value="11">Maestro de Obra</option>
                                                                                        <option value="12">Soporte Tecnico</option>
                                                                                        <option value="13">Guardiana</option>
                                                                                        <option value="14">Peon Vigilante</option>
                                                                                        <option value="15">Contratista</option>
                                                                                        <option value="16">Enfermera</option>
                                                                                        <option value="17">Prevencionista</option>
                                                                                    </select>
                                                                                </div>
                                                                            </div>
                                                                              <div class="form-group m-form__group m-form__group--sm row">
                                                                                <label class="col-xl-4 col-lg-4 col-form-label">Fecha Ingreso:</label>
                                                                                <div class="col-xl-8 col-lg-8">
                                                                                    <span class="m-form__control-static">
                                                                                        <asp:Label runat="server" ID="lblFechaIngreso"></asp:Label></span>
                                                                                </div>
                                                                            </div>
                                                                              <div class="form-group m-form__group m-form__group--sm row">
                                                                                <label class="col-xl-4 col-lg-4 col-form-label">Fecha Cese:</label>
                                                                                <div class="col-xl-8 col-lg-8">
                                                                                    <span class="m-form__control-static">
                                                                                        <asp:Label runat="server" ID="lblFechaCese"></asp:Label></span>
                                                                                </div>
                                                                            </div>
                                                                             <div class="form-group m-form__group m-form__group--sm row">
                                                                                <label class="col-xl-4 col-lg-4 col-form-label">Haber Mensual:</label>
                                                                                <div class="col-xl-8 col-lg-8">
                                                                                    <span class="m-form__control-static">S/. 
                                                                                        <asp:Label runat="server" ID="lblHaberMensual"></asp:Label></span>
                                                                                </div>
                                                                            </div>
                                                                             <div class="form-group m-form__group m-form__group--sm row" style="display:none">
                                                                                <label class="col-xl-4 col-lg-4 col-form-label">Asignacion Familiar:</label>
                                                                                <div class="col-xl-8 col-lg-8">
                                                                                    <span class="m-form__control-static">S/. 
                                                                                        <asp:Label runat="server" ID="lblAsignacionFamiliar"></asp:Label></span>
                                                                                </div>
                                                                            </div>
                                                                             <div class="form-group m-form__group m-form__group--sm row">
                                                                                <label class="col-xl-4 col-lg-4 col-form-label">Tipo Aportacion:</label>
                                                                                <div class="col-xl-8 col-lg-8">
                                                                                    <select name="tipplanilla" class="m-input noborder" id="lblTipoAportacion" runat="server" disabled="disabled">
                                                                                       <option value="">Seleccionar</option>
                                                                                        <option value="1">AFP Prima</option>
                                                                                        <option value="2">AFP Habitat</option>
                                                                                        <option value="3">AFP Integra</option>
                                                                                        <option value="4">AFP ProFuturo</option>
                                                                                        <option value="5">SNP</option>
                                                                                        <option value="6">Ninguna</option>
                                                                                    </select>
                                                                                </div>
                                                                            </div>
                                                                             <div class="form-group m-form__group m-form__group--sm row">
                                                                                <label class="col-xl-4 col-lg-4 col-form-label">Numero de CUSPP:</label>
                                                                                <div class="col-xl-8 col-lg-8">
                                                                                    <span class="m-form__control-static"> 
                                                                                        <asp:Label runat="server" ID="lblNumeroCuspp"></asp:Label></span>
                                                                                </div>
                                                                            </div>
                                                                            <div class="form-group m-form__group m-form__group--sm row">
                                                                                <label class="col-xl-4 col-lg-4 col-form-label">Banco:</label>
                                                                                <div class="col-xl-8 col-lg-8">
                                                                                    <select name="tipplanilla" class="m-input noborder" id="lblBanco" runat="server" disabled="disabled">
                                                                                        <option value="">Seleccionar</option>
                                                                                        <option value="1">Scotiabank</option>
                                                                                        <option value="2">BCP</option>
                                                                                        <option value="3">Interbank</option>
                                                                                    </select>
                                                                                </div>
                                                                            </div>
                                                                            
                                                                             
                                                                            <div class="form-group m-form__group m-form__group--sm row">
                                                                                <label class="col-xl-4 col-lg-4 col-form-label">Numero de cuenta:</label>
                                                                                <div class="col-xl-8 col-lg-8">
                                                                                    <span class="m-form__control-static"> 
                                                                                        <asp:Label runat="server" ID="lblNumeroCuentaBanco"></asp:Label></span>
                                                                                </div>
                                                                            </div>
                                                                            <div class="form-group m-form__group m-form__group--sm row">
                                                                                <label class="col-xl-4 col-lg-4 col-form-label">Numero de cuenta CTS:</label>
                                                                                <div class="col-xl-8 col-lg-8">
                                                                                    <span class="m-form__control-static">
                                                                                        <asp:Label runat="server" ID="lblNumeroCuentaCTS"></asp:Label></span>
                                                                                </div>
                                                                            </div>




                                                                        </div>
                                                                      
                                                                    </div>

                                                                    <!--end::Content-->
                                                                </div>
                                                            </div>

                                                            <!--end::Item-->

                                                            <!--begin::Item-->
                                                            <div class="m-accordion__item">
                                                                <div class="m-accordion__item-head collapsed" role="tab" id="m_accordion_1_item_3_head" data-toggle="collapse" href="#m_accordion_1_item_3_body" aria-expanded="    false">
                                                                    <span class="m-accordion__item-icon">
                                                                        <i class="fa  flaticon-placeholder"></i>
                                                                    </span>
                                                                    <span class="m-accordion__item-title">3. Perfil Planilla</span>
                                                                    <span class="m-accordion__item-mode"></span>
                                                                </div>
                                                                <div class="m-accordion__item-body collapse" id="m_accordion_1_item_3_body" class=" " role="tabpanel" aria-labelledby="m_accordion_1_item_3_head" data-parent="#m_accordion_1">

                                                                    <!--begin::Content-->
                                                                    <div class="tab-content  m--padding-30">
                                                                        <div class="m-form__section m-form__section--first">
                                                                            <div class="m-form__heading">
                                                                                <h4 class="m-form__heading-title">Perfil Planilla</h4>
                                                                            </div>
                                                                             <div class="form-group m-form__group m-form__group--sm row">
                                                                                <label class="col-xl-4 col-lg-4 col-form-label">Perfil Planilla:</label>
                                                                                <div class="col-xl-8 col-lg-8">
                                                                                     <asp:DropDownList runat="server" CssClass="m-input noborder" Enabled="false" ID="cboPerfilPlanillaResumen" >
                                                                                    </asp:DropDownList>
                                                                             
                                                                                </div>
                                                                            </div>
                                                                      
                                                                        </div>
                                                                    
                                                                    </div>

                                                                    <!--end::Content-->
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
                                                                <a href="#" class="btn btn-primary m-btn m-btn--custom m-btn--icon" data-wizard-action="submit">
                                                                    <span>
                                                                        <i class="la la-check"></i>&nbsp;&nbsp;
																			<span>Confirmar</span>
                                                                    </span>
                                                                </a>
                                                                <a href="#" class="btn btn-success m-btn m-btn--custom m-btn--icon" data-wizard-action="next">
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
                               <asp:LinkButton runat="server" ID="btnGuardarTrabajador" OnClick="btnGuardarTrabajador_Click" ></asp:LinkButton>
                               <asp:LinkButton runat="server" ID="btnCargarDatosTrabajador"  OnClick="btnCargarDatosTrabajador_Click" ></asp:LinkButton>
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
                        <asp:TextBox ID="txtFechaNacimientoForm" runat="server"></asp:TextBox>
                        <asp:TextBox ID="txtFechaIngresoForm" runat="server"></asp:TextBox>
                        <asp:TextBox ID="txtFechaCeseForm" runat="server"></asp:TextBox>
                        <asp:TextBox ID="txtHaberMensualForm" runat="server"></asp:TextBox>
                        <asp:TextBox ID="txtNumCusppForm" runat="server"></asp:TextBox>
                        <asp:TextBox ID="txtNumCuentaBanco" runat="server"></asp:TextBox>
                        <asp:TextBox ID="txtNumCuentaCTS" runat="server"></asp:TextBox>
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
            $('#navSubMenuAgregarTrabajador').addClass("m-menu__item--active");

        
        })

        function Resumen() {
           
            $("#<%=lblNombreTrabajador.ClientID%>").text($('#txtNombres').val());
            $("#<%=lblApellidoPaterno.ClientID%>").text($('#txtApellidoPaterno').val());
            $("#<%=lblApellidoMaterno.ClientID%>").text($('#txtApellidoMaterno').val());
            $("#<%=lblTipoDocumento.ClientID%>").val($('#cboTipoDocumento').val());
            $("#<%=lblDocumentoIdentidad.ClientID%>").text($('#txtNroDocumento').val());
            $("#<%=lblSexo.ClientID%>").text($('#cboSexo').val());
            $("#<%=lblEstadoCivil.ClientID%>").val($('#cboEstadoCivil').val()); 
            $("#<%=lblFechaNacimiento.ClientID%>").text($('#m_datepicker_fn').val());
            $("#<%=lblTipoPlanilla.ClientID%>").val($('#cboTipoPlanilla').val());
          <%--  $("#<%=lblTipoTrabajo.ClientID%>").val($('#cboTipoTrabajo').val()); --%>
            $("#<%=lblCargo.ClientID%>").val($('#cboCargo').val());
            $("#<%=lblFechaIngreso.ClientID%>").text($('#m_datepicker_fi').val());
            $("#<%=lblFechaCese.ClientID%>").text($('#m_datepicker_fc').val()); 
            $("#<%=lblHaberMensual.ClientID%>").text($('#txtHaberMensual').val());
            $("#<%=lblAsignacionFamiliar.ClientID%>").text($('#txtAsigFamiliar').val());
            $("#<%=lblTipoAportacion.ClientID%>").val($('#cboTipoAportacion').val());
            $("#<%=lblNumeroCuspp.ClientID%>").text($('#txtNumCuspp').val());
            $("#<%=lblBanco.ClientID%>").val($('#cboBanco').val()); 
            $("#<%=lblNumeroCuentaBanco.ClientID%>").text($('#txtNroCuentaBanco').val());
            $("#<%=lblNumeroCuentaCTS.ClientID%>").text($('#txtNroCuentaCTS').val());
            $("#<%=cboPerfilPlanillaResumen.ClientID%>").val($("#<%=cboPerfilPlanilla.ClientID%>").val());
            $("#<%=lblTipoTrabajo.ClientID%>").val($("#<%=cboTipoTrabajo.ClientID%>").val());

            $("#<%=txtNombresForm.ClientID%>").val($("#<%=lblNombreTrabajador.ClientID%>").text());
            $("#<%=txtApellidosPaternosForm.ClientID%>").val($("#<%=lblApellidoPaterno.ClientID%>").text());
            $("#<%=txtApellidosMaternosForm.ClientID%>").val($("#<%=lblApellidoMaterno.ClientID%>").text());
            $("#<%=txtNumeroDocumentoForm.ClientID%>").val($("#<%=lblDocumentoIdentidad.ClientID%>").text());
            $("#<%=txtSexoForm.ClientID%>").val($("#<%=lblSexo.ClientID%>").text());
            $("#<%=txtFechaNacimientoForm.ClientID%>").val($("#<%=lblFechaNacimiento.ClientID%>").text());
            $("#<%=txtFechaIngresoForm.ClientID%>").val($("#<%=lblFechaIngreso.ClientID%>").text());
            $("#<%=txtFechaCeseForm.ClientID%>").val($("#<%=lblFechaCese.ClientID%>").text());
            $("#<%=txtHaberMensualForm.ClientID%>").val($("#<%=lblHaberMensual.ClientID%>").text());
            $("#<%=txtNumCusppForm.ClientID%>").val($("#<%=lblNumeroCuspp.ClientID%>").text());
            $("#<%=txtNumCuentaBanco.ClientID%>").val($("#<%=lblNumeroCuentaBanco.ClientID%>").text());
            $("#<%=txtNumCuentaCTS.ClientID%>").val($("#<%=lblNumeroCuentaCTS.ClientID%>").text());
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

        function ReloadJqueryWizard() {
            var script = document.createElement("script");
            script.setAttribute("type", "text/javascript");
            script.setAttribute("src", "/assets/js/wizard.js");
            document.getElementsByTagName("head")[0].appendChild(script);
        }
          function ReloadJqueryWizardEventuales() {
            var script = document.createElement("script");
            script.setAttribute("type", "text/javascript");
            script.setAttribute("src", "/assets/js/wizard-eventuales.js");
            document.getElementsByTagName("head")[0].appendChild(script);
        }
          function ReloadJqueryDatePicker() {
            var script = document.createElement("script");
            script.setAttribute("type", "text/javascript");
            script.setAttribute("src", "/assets/js/bootstrap-datepicker.js");
            document.getElementsByTagName("head")[0].appendChild(script);
        }


          function CargarDatosTrabajador() {
              
            $("#<%=lblNombreTrabajador.ClientID%>").text($("#<%=txtNombresForm.ClientID%>").val());
            $("#<%=lblApellidoPaterno.ClientID%>").text($("#<%=txtApellidosPaternosForm.ClientID%>").val());
            $("#<%=lblApellidoMaterno.ClientID%>").text($("#<%=txtApellidosMaternosForm.ClientID%>").val());
            $("#<%=lblDocumentoIdentidad.ClientID%>").text($("#<%=txtNumeroDocumentoForm.ClientID%>").val());
            $("#<%=lblSexo.ClientID%>").text($("#<%=txtSexoForm.ClientID%>").val());
            $("#<%=lblFechaNacimiento.ClientID%>").text($("#<%=txtFechaNacimientoForm.ClientID%>").val());
            $("#<%=lblFechaIngreso.ClientID%>").text($("#<%=txtFechaIngresoForm.ClientID%>").val());
            $("#<%=lblFechaCese.ClientID%>").text($("#<%=txtFechaCeseForm.ClientID%>").val());
            $("#<%=lblHaberMensual.ClientID%>").text($("#<%=txtHaberMensualForm.ClientID%>").val());
            $("#<%=lblNumeroCuspp.ClientID%>").text($("#<%=txtNumCusppForm.ClientID%>").val());
            $("#<%=lblNumeroCuentaBanco.ClientID%>").text($("#<%=txtNumCuentaBanco.ClientID%>").val());
            $("#<%=lblNumeroCuentaCTS.ClientID%>").text($("#<%=txtNumCuentaCTS.ClientID%>").val());

            $('#txtNombres').val($("#<%=lblNombreTrabajador.ClientID%>").text());
            $('#txtApellidoPaterno').val($("#<%=lblApellidoPaterno.ClientID%>").text());
            $('#txtApellidoMaterno').val($("#<%=lblApellidoMaterno.ClientID%>").text());
            $('#cboTipoDocumento').val($("#<%=lblTipoDocumento.ClientID%>").val());
            $('#txtNroDocumento').val($("#<%=lblDocumentoIdentidad.ClientID%>").text());
            $('#cboSexo').val($("#<%=lblSexo.ClientID%>").text());
            $('#cboEstadoCivil').val($("#<%=lblEstadoCivil.ClientID%>").val()); 
            $('#m_datepicker_fn').val($("#<%=lblFechaNacimiento.ClientID%>").text());
            $('#cboTipoPlanilla').val($("#<%=lblTipoPlanilla.ClientID%>").val());
         <%--   $('#cboTipoTrabajo').val($("#<%=lblTipoTrabajo.ClientID%>").val()); --%>
            $('#cboCargo').val($("#<%=lblCargo.ClientID%>").val());
            $('#m_datepicker_fi').val($("#<%=lblFechaIngreso.ClientID%>").text());
            $('#m_datepicker_fc').val($("#<%=lblFechaCese.ClientID%>").text()); 
            $('#txtHaberMensual').val($("#<%=lblHaberMensual.ClientID%>").text());
            $('#txtAsigFamiliar').val($("#<%=lblAsignacionFamiliar.ClientID%>").text());
            $('#cboTipoAportacion').val($("#<%=lblTipoAportacion.ClientID%>").val());
            $('#txtNumCuspp').val($("#<%=lblNumeroCuspp.ClientID%>").text());
            $('#cboBanco').val($("#<%=lblBanco.ClientID%>").val()); 
            $('#txtNroCuentaBanco').val($("#<%=lblNumeroCuentaBanco.ClientID%>").text());
            $('#txtNroCuentaCTS').val($("#<%=lblNumeroCuentaCTS.ClientID%>").text());
            $("#<%=cboPerfilPlanilla.ClientID%>").val($("#<%=cboPerfilPlanillaResumen.ClientID%>").val());
              
            $("#<%=cboTipoTrabajo.ClientID%>").val($("#<%=lblTipoTrabajo.ClientID%>").val());

              $("#txtNombres").prop('disabled', true);
              $("#txtApellidoPaterno").prop('disabled', true);
              $("#txtApellidoMaterno").prop('disabled', true);
              $("#cboTipoDocumento").prop('disabled', true);
              $("#txtNroDocumento").prop('disabled', true);
              $("#cboSexo").prop('disabled', true);
              $("#cboEstadoCivil").prop('disabled', true);
              $("#m_datepicker_fn").prop('disabled', true);
              $("#cboTipoPlanilla").prop('disabled', true);
              //$("#cboTipoTrabajo").prop('disabled', true);
              $("#cboCargo").prop('disabled', true);
              $("#m_datepicker_fi").prop('disabled', true);
              $("#m_datepicker_fc").prop('disabled', true);
              $("#txtHaberMensual").prop('disabled', true);
              $("#txtAsigFamiliar").prop('disabled', true);
              $("#cboTipoAportacion").prop('disabled', true);
              $("#txtNumCuspp").prop('disabled', true);
              $("#cboBanco").prop('disabled', true);
              $("#txtNroCuentaBanco").prop('disabled', true);
              $("#txtNroCuentaCTS").prop('disabled', true);
              $("#<%=cboPerfilPlanilla.ClientID%>").prop('disabled', true);
              $("#<%=cboTipoTrabajo.ClientID%>").prop('disabled', true);
        }

        
          function CargarDatosTrabajadorModificar() {
              
            $("#<%=lblNombreTrabajador.ClientID%>").text($("#<%=txtNombresForm.ClientID%>").val());
            $("#<%=lblApellidoPaterno.ClientID%>").text($("#<%=txtApellidosPaternosForm.ClientID%>").val());
            $("#<%=lblApellidoMaterno.ClientID%>").text($("#<%=txtApellidosMaternosForm.ClientID%>").val());
            $("#<%=lblDocumentoIdentidad.ClientID%>").text($("#<%=txtNumeroDocumentoForm.ClientID%>").val());
            $("#<%=lblSexo.ClientID%>").text($("#<%=txtSexoForm.ClientID%>").val());
            $("#<%=lblFechaNacimiento.ClientID%>").text($("#<%=txtFechaNacimientoForm.ClientID%>").val());
            $("#<%=lblFechaIngreso.ClientID%>").text($("#<%=txtFechaIngresoForm.ClientID%>").val());
            $("#<%=lblFechaCese.ClientID%>").text($("#<%=txtFechaCeseForm.ClientID%>").val());
            $("#<%=lblHaberMensual.ClientID%>").text($("#<%=txtHaberMensualForm.ClientID%>").val());
            $("#<%=lblNumeroCuspp.ClientID%>").text($("#<%=txtNumCusppForm.ClientID%>").val());
            $("#<%=lblNumeroCuentaBanco.ClientID%>").text($("#<%=txtNumCuentaBanco.ClientID%>").val());
            $("#<%=lblNumeroCuentaCTS.ClientID%>").text($("#<%=txtNumCuentaCTS.ClientID%>").val());

            $('#txtNombres').val($("#<%=lblNombreTrabajador.ClientID%>").text());
            $('#txtApellidoPaterno').val($("#<%=lblApellidoPaterno.ClientID%>").text());
            $('#txtApellidoMaterno').val($("#<%=lblApellidoMaterno.ClientID%>").text());
            $('#cboTipoDocumento').val($("#<%=lblTipoDocumento.ClientID%>").val());
            $('#txtNroDocumento').val($("#<%=lblDocumentoIdentidad.ClientID%>").text());
            $('#cboSexo').val($("#<%=lblSexo.ClientID%>").text());
            $('#cboEstadoCivil').val($("#<%=lblEstadoCivil.ClientID%>").val()); 
            $('#m_datepicker_fn').val($("#<%=lblFechaNacimiento.ClientID%>").text());
            $('#cboTipoPlanilla').val($("#<%=lblTipoPlanilla.ClientID%>").val());
         <%--   $('#cboTipoTrabajo').val($("#<%=lblTipoTrabajo.ClientID%>").val()); --%>
            $('#cboCargo').val($("#<%=lblCargo.ClientID%>").val());
            $('#m_datepicker_fi').val($("#<%=lblFechaIngreso.ClientID%>").text());
            $('#m_datepicker_fc').val($("#<%=lblFechaCese.ClientID%>").text()); 
            $('#txtHaberMensual').val($("#<%=lblHaberMensual.ClientID%>").text());
            $('#txtAsigFamiliar').val($("#<%=lblAsignacionFamiliar.ClientID%>").text());
            $('#cboTipoAportacion').val($("#<%=lblTipoAportacion.ClientID%>").val());
            $('#txtNumCuspp').val($("#<%=lblNumeroCuspp.ClientID%>").text());
            $('#cboBanco').val($("#<%=lblBanco.ClientID%>").val()); 
            $('#txtNroCuentaBanco').val($("#<%=lblNumeroCuentaBanco.ClientID%>").text());
            $('#txtNroCuentaCTS').val($("#<%=lblNumeroCuentaCTS.ClientID%>").text());
            $("#<%=cboPerfilPlanilla.ClientID%>").val($("#<%=cboPerfilPlanillaResumen.ClientID%>").val());
            $("#<%=cboTipoTrabajo.ClientID%>").val($("#<%=lblTipoTrabajo.ClientID%>").val());

              $("#txtNombres").prop('disabled', false);
              $("#txtApellidoPaterno").prop('disabled', false);
              $("#txtApellidoMaterno").prop('disabled', false);
              $("#cboTipoDocumento").prop('disabled', false);
              $("#txtNroDocumento").prop('disabled', false);
              $("#cboSexo").prop('disabled', false);
              $("#cboEstadoCivil").prop('disabled', false);
              $("#m_datepicker_fn").prop('disabled', false);
              $("#cboTipoPlanilla").prop('disabled', false);
              //$("#cboTipoTrabajo").prop('disabled', false);
              $("#cboCargo").prop('disabled', false);
              $("#m_datepicker_fi").prop('disabled', false);
              $("#m_datepicker_fc").prop('disabled', false);
              $("#txtHaberMensual").prop('disabled', false);
              $("#txtAsigFamiliar").prop('disabled', false);
              $("#cboTipoAportacion").prop('disabled', false);
              $("#txtNumCuspp").prop('disabled', false);
              $("#cboBanco").prop('disabled', false);
              $("#txtNroCuentaBanco").prop('disabled', false);
              $("#txtNroCuentaCTS").prop('disabled', false);
              $("#<%=cboPerfilPlanilla.ClientID%>").prop('disabled', false);
              $("#<%=cboTipoTrabajo.ClientID%>").prop('disabled', false);
        }

        function NuevoRegistro() {
       
            $('#txtNombres').val('');
            $('#txtApellidoPaterno').val('');
            $('#txtApellidoMaterno').val('');
            $('#cboTipoDocumento').val('');
            $('#txtNroDocumento').val('');
            $('#cboSexo').val('');
            $('#cboEstadoCivil').val('');
            $('#m_datepicker_fn').val('');
            $('#cboTipoPlanilla').val('');
            $('#cboTipoTrabajo').val('');
            $('#cboCargo').val('');
            $('#m_datepicker_fi').val('');
            $('#m_datepicker_fc').val('');
            $('#txtHaberMensual').val('');
            $('#txtAsigFamiliar').val('');
            $('#cboTipoAportacion').val('');
            $('#txtNumCuspp').val('');
            $('#cboBanco').val(''); 
            $('#txtNroCuentaBanco').val('');
            $('#txtNroCuentaCTS').val('');        

             $("#txtNombres").prop('disabled', false);
              $("#txtApellidoPaterno").prop('disabled', false);
              $("#txtApellidoMaterno").prop('disabled', false);
              $("#cboTipoDocumento").prop('disabled', false);
              $("#txtNroDocumento").prop('disabled', false);
              $("#cboSexo").prop('disabled', false);
              $("#cboEstadoCivil").prop('disabled', false);
              $("#m_datepicker_fn").prop('disabled', false);
              $("#cboTipoPlanilla").prop('disabled', false);
              //$("#cboTipoTrabajo").prop('disabled', false);
              $("#cboCargo").prop('disabled', false);
              $("#m_datepicker_fi").prop('disabled', false);
              $("#m_datepicker_fc").prop('disabled', false);
              $("#txtHaberMensual").prop('disabled', false);
              $("#txtAsigFamiliar").prop('disabled', false);
              $("#cboTipoAportacion").prop('disabled', false);
              $("#txtNumCuspp").prop('disabled', false);
              $("#cboBanco").prop('disabled', false);
              $("#txtNroCuentaBanco").prop('disabled', false);
              $("#txtNroCuentaCTS").prop('disabled', false);
              $("#<%=cboPerfilPlanilla.ClientID%>").prop('disabled', false);
              $("#<%=cboTipoTrabajo.ClientID%>").prop('disabled', false);
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
