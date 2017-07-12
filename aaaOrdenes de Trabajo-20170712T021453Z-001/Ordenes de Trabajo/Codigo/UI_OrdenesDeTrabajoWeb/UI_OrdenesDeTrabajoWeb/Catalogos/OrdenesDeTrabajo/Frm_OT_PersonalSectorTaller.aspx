<%@ Page Language="VB" MasterPageFile="~/MasterPage/Mp_Formulario.master" AutoEventWireup="false" CodeFile="Frm_OT_PersonalSectorTaller.aspx.vb" Inherits="Catalogos_Frm_OT_PersonalSectorTaller" %>

<%@ Register TagPrefix="wuc" Namespace="Utilerias.WebForms.Controles" Assembly="Utilerias.WebForms.Controles" %>
<%@ Register Src="~/Controles/wucEmpleadosEU.ascx" TagName="wuc_EmpleadosEU" TagPrefix="uc1" %>

<asp:Content runat="server" ContentPlaceHolderID="cphFormulario" id="Content1" >
    <header>
        <h2><asp:Label runat="server" ID="lblTitulo"></asp:Label></h2>
    </header>

    <article class="tituloSeccion">
        Datos del Sector o Taller
    </article>
    <article class="formulario">
        <table>
            <tr>
                <th>Sector</th>
                <td>
                    <asp:Label runat="server" ID="lblSector"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>Coordinador</th>
                <td>
                    <asp:Label runat="server" ID="lblCoordinador"></asp:Label>
                </td>
            </tr>
            </table>
        </article>

    <article class="tituloSeccion">
        Datos del Funcionario
    </article>
        <article class="formulario">
        <table>
            <tr>
                <th>Identificación</th>
                <td>
                    <asp:UpdatePanel runat="server" ID="upTxtIdentificacion" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:TextBox runat="server" ID="txtIdentificacion" Width="30%" data-tipoControl="texto" AutoPostBack="true"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ID="rfvTxtIdentificacion" ControlToValidate="txtIdentificacion" Display="Dynamic" ErrorMessage="El numero de Identificación es requerido." ValidationGroup="Aceptar">&nbsp;</asp:RequiredFieldValidator>
                            <asp:LinkButton ID="lnkEjecutarBusquedaFuncionario" runat="server">
                                <img id="imgBuscarFuncionario" title="Buscar Registro" alt="Buscar Registro" src="" />
                            </asp:LinkButton>
                            <br />
                            <span id="spContadorTxtIdentificacion" class="contadorCaracteresRestantes"></span>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="txtIdentificacion" EventName="TextChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <th>Nombre</th>
                <td>
                    <asp:UpdatePanel runat="server" ID="upTxtNombre" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:Label runat="server" ID="lblNombre"></asp:Label>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="txtIdentificacion" EventName="TextChanged" />
                            <asp:AsyncPostBackTrigger ControlID="wuc_EmpleadosEU" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <th>Puesto</th>
                <td>
                    <asp:UpdatePanel runat="server" ID="upTxtPuesto" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:Label runat="server" ID="lblPuesto"></asp:Label>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="txtIdentificacion" EventName="TextChanged" />
                            <asp:AsyncPostBackTrigger ControlID="wuc_EmpleadosEU" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
    </article>

    <article class="formulario">
        <table>
            <tr>
                <th>Categoria Laboral</th>
                <td>
                    <asp:DropDownList Width="40%" runat="server" ID="ddlCategoriaLaboral" AppendDataBoundItems="true" data-tipocontrol="combo" AutoPostBack="true"></asp:DropDownList>
                    <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator1" ControlToValidate="ddlCategoriaLaboral" display="Dynamic" ErrorMessage="Ingrese una categoria laboral" ValidationGroup="Aceptar">&nbsp;</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr id="trArea" runat="server">
                <th>Area</th>
                <td>
                    <asp:DropDownList Width="40%" runat="server" ID="ddlArea" AppendDataBoundItems="true" data-tipocontrol="combo" AutoPostBack="true"></asp:DropDownList>                    
                    <asp:RequiredFieldValidator runat="server" id="rvfdllArea" ControlToValidate="ddlArea" display="Dynamic" ErrorMessage="Ingrese un área de trabajo." ValidationGroup="Aceptar">&nbsp;</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <th>Estado</th>
                <td>
                    <asp:DropDownList Width="40%" runat="server" ID="ddlEstado" AppendDataBoundItems="true" data-tipocontrol="combo"></asp:DropDownList>
                    <asp:RequiredFieldValidator runat="server" id="rfvddlEstado" ControlToValidate="ddlEstado" display="Dynamic" ErrorMessage="Ingrese un estado para el personal" ValidationGroup="Aceptar">&nbsp;</asp:RequiredFieldValidator>
                </td>
            </tr>
        </table>
    </article>

        <%--Popup para búsqueda de funcionario--%>
    <article id="PopUpBusquedaFuncionario" class="ventanaEmergente">
        <article class="formulario" style="width: 850px!important;">
            <a href="#CerrarPopUpBusquedaFuncionario" title="Cerrar Ventana" class="botonCerrarVentanaEmergente">X</a>
            <br />
            <asp:UpdatePanel ID="upContactoConv" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <uc1:wuc_EmpleadosEU runat="server" ID="wuc_EmpleadosEU" />
                    <div class="areaBotones">
                        <a href="#CerrarPopUpBusquedaFuncionario" title="Regresar">Regresar</a>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </article>
    </article>
    <%--Fin Popup para búsqueda de funcionario--%>

    <article class="areaBotones">
        <asp:Button runat="server" ID="btnAceptar" Text="Aceptar" ValidationGroup="Aceptar" />
        <input type="button" data-tipo="limpiarFormulario" value="Limpiar Formulario" id="btnLimpiarFormulario"/>
        <asp:Button runat="server" type="button" text="Cancelar" ID="btnCancelar" OnClick="btnCancelar_Click" />
    </article>


     <article id="arPopupDelFormulario"></article>
    <article id="arAlerta"></article>

    <script type="text/javascript">
        function mostrarAlertaNoHayDatos() {
            mostrarAlerta(
                '#arAlerta',
                {
                    mensaje: 'No se cuenta con personal que cumpla con la condición indicada',
                    tipo: "advertencia",
                    transparencia: 0.9,
                    posicion: 'center',
                    permiteCerrar: true
                }
                );
        };

        function mostrarAlertaError(pvc_Mensaje) {
            mostrarAlerta(
                '#arAlerta',
                {
                    mensaje: pvc_Mensaje,
                    tipo: "peligro",
                    transparencia: 0.9,
                    posicion: 'center',
                    permiteCerrar: true
                }
                );

        };

        function mostrarAlertaExitoso() {
            mostrarAlerta(
                '#arAlerta',
                {
                    mensaje: "Se ha ingresado la persona seleccionada",
                    tipo: "exito",
                    transparencia: 0.9,
                    posicion: 'center',
                    permiteCerrar: true
                }
                );

        };

        function mostrarPopupRegistroExitoso() {
            var vlo_ConfiguracionPopup = {
                titulo: 'Catálogo de Personal Sector o taller',
                mensaje: 'Se ha registrado la información del Profesional o Operario.<br /><strong>¿Desea registrar otra Persona?</strong>',
                onClosed: function () { regresarAlListado(); },
                botones:
                    [
                        {
                            idControl: "btnSi",
                            textoBoton: "Sí",
                            onClick: function () { window.location = 'Frm_OT_PersonalSectorTaller.aspx?pvn_Operacion=<%=Utilerias.OrdenesDeTrabajo.eOperacion.Agregar%>'; }
                        },
                        {
                            idControl: "btnNo",
                            textoBoton: "No",
                            onClick: function () { regresarAlListado(); }
                        }
                    ]

            };

            $('#arPopupDelFormulario').popup(vlo_ConfiguracionPopup);
            window.location = '#arPopupDelFormulario';

        };

        function mostrarAlertaRegistroModificado() {
            deshabilitarControl('#<%=btnAceptar.ClientID%>');
            //   deshabilitarControl('#btnLimpiarFormulario');
            deshabilitarControl('#<%=btnCancelar.ClientID%>');
            $('.formulario').attr('disabled', 'disabled');

            mostrarAlerta(
            '#arAlerta',
            {
                mensaje: 'Se ha actualizado la información de la persona.',
                tipo: "exito",
                transparencia: 0.9,
                posicion: 'center',
                onClosed: function () { regresarAlListado(); }
            });

        };

        function mostrarAlertaRegistroNoBorrado() {
            mostrarAlerta(
                '#arAlerta',
                {
                    mensaje: "No ha sido posible borrar la persona seleccionada",
                    tipo: "advertencia",
                    transparencia: 0.9,
                    posicion: 'center',
                    permiteCerrar: true
                }
                );

        };

        function mostrarAlertaNoEncontrado() {
            mostrarAlerta(
                '#arAlerta',
                {
                    mensaje: "No se han encontrado funcionarios con el número de cédula indicado",
                    tipo: "advertencia",
                    transparencia: 0.9,
                    posicion: 'center',
                    permiteCerrar: true
                }
                );

        };

        function mostrarPopupConfirmaDeseaBorrarRegistro(pvc_UniqueIdControl) {
            var vlo_ConfiguracionPopup = {
                titulo: 'Catálogo de Personal por Sector o Taller',
                mensaje: 'Realmente desea borrar a la persona seleccionada?',
                botones:
                    [  //en corchetes indican arreglos y los parentesis cuadrados indican inicio o fin de funcionalidad o objeto, $librerias de jquery
                        {
                            idControl: "btnSi",
                            textoBoton: "Si",
                            onClick:
                                function () {
                                    $(this).attr("disabled", "disabled");  //cuando le de click al boton desabilitelo
                                    __doPostBack(pvc_UniqueIdControl, ''); //control de .net para q vaya al servidor a eliminar
                                }
                        },
                        {
                            idControl: "btnNo",
                            textoBoton: "No",
                            onClick: function () { cerrarPopup(); }

                        },
                        {
                            idControl: "btnCancelar",
                            textoBoton: "Cancelar",
                            onClick: function () { cerrarPopup(); }

                        },
                    ]
            };

            $('#popupConfirmacionDeseaBorrar').popup(vlo_ConfiguracionPopup);
            window.location = '#popupConfirmacionDeseaBorrar';
            return false; //detener comportamiento de ir al servidor hasta aceptar
        };

        function regresarAlListado() {
            window.location = 'Lst_OT_PersonalSectorTaller.aspx';
        };

        function deshabilitarFormulario() {
            deshabilitarControl('#btnAgregar');

            $('.formulario').attr('disabled', 'disabled');
            $('.listado').attr('disabled', 'disabled');
        };

        function mostrarAlertaLlaveIncorrecta() {
            deshabilitarFormulario();

            mostrarAlerta(
                '#arAlerta',
                {
                    mensaje: 'El número de identificación de Sector o Taller provisto no pertenece a ningún Empleado registrado en el sistema',
                    tipo: "peligro",
                    transparencia: 0.9,
                    posicion: 'center',
                    permiteCerrar: true,
                    onClosed: function () { regresarAlListado(); }
                }
                );

        };

        function ocultarFiltroFuncionario(){
            window.location = '#CerrarPopUpBusquedaFuncionario';
        };

        function mostrarPopUp(pvc_IdPopup) {
            window.location = pvc_IdPopup;
        };

        function establecerControles() {
            $('#imgMostrarFiltroFuncionarios').click(function () {
                window.location = '#PopUpBusquedaFuncionario';
            });

            configurarLongitudMaximaTexto('#<%=Me.txtIdentificacion.ClientID%>', <%=Utilerias.OrdenesDeTrabajo.Constantes.TAMANNO_CEDULA%>);
            configurarContadorCaracteresRestantes('#<%=Me.txtIdentificacion.ClientID%>', '#spContadorTxtIdentificacion');

            cargarLupa();
        };

        function cargarLupa() {
            permutarImagenes('#imgBuscarFuncionario',
                '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Lupa.png")%>',
                            '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Lupa.png")%>',
                            '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Lupa.png")%>'
                        );

        };

        function inicializarFormulario() {
            
            $('#btnCancelar').click(function () {
                regresarAlListado();
            });


            $(window).keydown(function a(e) {

                if (e.keyCode == 13) {
                    return false;
                }
            });

            establecerControles();

        };


        $(document).ready(function () {

            inicializarFormulario()


        });

    </script>


</asp:Content>

