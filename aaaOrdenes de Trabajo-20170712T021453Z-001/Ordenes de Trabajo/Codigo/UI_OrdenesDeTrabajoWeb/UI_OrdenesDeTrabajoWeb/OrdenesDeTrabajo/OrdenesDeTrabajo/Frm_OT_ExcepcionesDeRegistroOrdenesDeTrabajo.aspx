<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/Mp_Formulario.master" AutoEventWireup="false" CodeFile="Frm_OT_ExcepcionesDeRegistroOrdenesDeTrabajo.aspx.vb" Inherits="OrdenesDeTrabajo_Frm_OT_ExcepcionesDeRegistroOrdenesDeTrabajo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Src="~/Controles/wucEmpleadosEU.ascx" TagName="wuc_EmpleadosEU" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphFormulario" runat="Server">

    <header>
        <h2>
            <asp:Label runat="server" ID="lblAccion"></asp:Label>
        </h2>
    </header>

    <article class="tituloSeccion">
        Mantenimiento de Excepciones de Registro para Ordenes de Trabajo
    </article>

    <article class="formulario">
        <table>
            <tr>
                <th>ID Solicitante</th>
                <td>
                    <asp:UpdatePanel runat="server" ID="upTxtIdSolicitante" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:TextBox Width="59%" runat="server" ID="txtIdSolicitante" data-tipocontrol="texto" AutoPostBack="true"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ID="rfvTxtIdSolicitante" ControlToValidate="txtIdSolicitante" Display="Dynamic" ErrorMessage="El número de Identificación del Solicitante es requerido." ValidationGroup="Aceptar">&nbsp;</asp:RequiredFieldValidator>
                            <asp:LinkButton ID="lnkEjecutarBusquedaSolicitante" runat="server">
                                <img id="imgBuscarSolicitante" title="Buscar Registro" alt="Buscar Registro" src="" />
                            </asp:LinkButton>
                            <br />
                            <span id="spContadorTxtIdSolicitante" class="contadorCaracteresRestantes"></span>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="txtIdSolicitante" EventName="TextChanged" />
                            <asp:AsyncPostBackTrigger ControlID="btnLimpiarFormulario" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <th>Nombre</th>
                <td>
                    <asp:UpdatePanel runat="server" ID="upLblNombre" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:Label runat="server" ID="lblNombre" data-tipocontrol="etiqueta"></asp:Label>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="txtIdSolicitante" EventName="TextChanged" />
                            <asp:AsyncPostBackTrigger ControlID="wuc_EmpleadosEU" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <th>Unidad  Interna</th>
                <td>
                    <asp:DropDownList Width="59%" runat="server" ID="ddlUnidadInterna" data-tipocontrol="combo" AppendDataBoundItems="true"></asp:DropDownList>
                    <asp:RequiredFieldValidator runat="server" ID="rfvDdlUnidadInterna" ControlToValidate="ddlUnidadInterna" Display="Dynamic" ErrorMessage="Unidad Interna es Obligatoria." ValidationGroup="Aceptar"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <th>Unidad  Tiempo</th>
                <td>
                    <asp:DropDownList Width="59%" runat="server" ID="ddlUnidadTiempo" data-tipocontrol="combo" AppendDataBoundItems="true"></asp:DropDownList>
                    <asp:RequiredFieldValidator runat="server" ID="rfvDdlUnidadTiempo" ControlToValidate="ddlUnidadTiempo" Display="Dynamic" ErrorMessage="Unidad Tiempo es Obligatoria." ValidationGroup="Aceptar"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <th>Vigencia</th>
                <td>
                    <asp:TextBox runat="server" ID="txtVigencia" data-tipocontrol="texto"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ID="rfvTxtVigencia" ControlToValidate="txtVigencia" Display="Dynamic" ErrorMessage="La vigencia es Obligatoria." ValidationGroup="Aceptar"></asp:RequiredFieldValidator>
                    <ajax:FilteredTextBoxExtender ID="ftbTxtVigencia" runat="server" TargetControlID="txtVigencia" FilterMode="ValidChars" FilterType="Numbers"></ajax:FilteredTextBoxExtender>
                </td>
            </tr>
        </table>
    </article>

    <article class="areaBotones">
        <asp:Button runat="server" ID="btnAceptar" Text="Aceptar" ValidationGroup="Aceptar" OnClientClick="javascript:return validarGuardar();" />
        <asp:Button runat="server" ID="btnLimpiarFormulario" Text="Limpiar Formulario" data-tipo="limpiarFormulario" />
        <input id="btnCancelar" type="button" value="Cancelar" />
    </article>

    <article style="visibility:hidden">
        <asp:Button runat="server" ID="btnAceptarOculto" Text="Guardar" ValidationGroup="Aceptar" />
    </article>

    <article id="arPopupDelFormulario"></article>
    <article id="arAlertasDelFormulario"></article>
    <article id="arPopupGenerico"></article>

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

    <script type="text/javascript">

        function validarGuardar() {
            if (Page_ClientValidate("Aceptar")) {
                document.getElementById('<%=btnAceptar.ClientID%>').disabled = "true";
            }
            __doPostBack('<%=Me.btnAceptarOculto.UniqueID%>', '');
            return false;
        }
        function regresarAlListado() {
            window.location = 'Lst_OT_ExcepcionesDeRegistroOrdenesDeTrabajo.aspx';
        }

        function deshabilitarFormulario() {
            deshabilitarControl('#<%=Me.btnAceptar.ClientID%>');
            deshabilitarControl('#<%=Me.btnLimpiarFormulario.ClientID%>');
            deshabilitarControl('#btnCancelar');

            $('.formulario').attr('disabled', 'disabled');
        };

        function mostrarPopupRegistroExitoso() {
            var vlo_ConfiguracionPopup = {
                titulo: 'Excepciones de registro para Ordenes de Trabajo',
                mensaje: 'Se ha registrado la información.<br/><strong>Desea registrar otra excepción?</strong>',
                botones:
                    [  //en corchetes indican arreglos y los parentesis cuadrados indican inicio o fin de funcionalidad o objeto, $librerias de jquery
                        {
                            idControl: "btnSi",
                            textoBoton: "Si",
                            onClick:
                                function (e) {
                                    window.location = 'Frm_OT_ExcepcionesDeRegistroOrdenesDeTrabajo.aspx?pvn_Operacion=<%= Utilerias.OrdenesDeTrabajo.eOperacion.Agregar%>'; //pvn_Operacion????
                                }
                        },
                       {
                           idControl: "btnNo",
                           textoBoton: "No",
                           onClick: function (e) { regresarAlListado(); }

                       },

                    ]
            };

            $('#arPopupDelFormulario').popup(vlo_ConfiguracionPopup);
            window.location = '#arPopupDelFormulario';

        };

        function mostrarAlertaError(pvc_Mensaje) {
            mostrarAlerta(
                '#arAlertasDelFormulario',
                {
                    mensaje: pvc_Mensaje,
                    tipo: "peligro",
                    transparencia: 0.9,
                    posicion: 'center',
                    permiteCerrar: true
                }
                );
        };

        function mostrarAlertaActualizacionExitosa() {
            deshabilitarFormulario();

            mostrarAlerta(
                '#arAlertasDelFormulario',
                {
                    mensaje: 'Se ha actualizado la información del registro.',
                    tipo: "exito",
                    transparencia: 0.9,
                    posicion: 'center',
                    permiteCerrar: true,
                    onClosed: function () { regresarAlListado(); }
                }
                );
        };

        function mostrarAlertaLlaveIncorrecta() {
            deshabilitarFormulario();

            mostrarAlerta(
                '#arAlertasDelFormulario',
                {
                    mensaje: 'El número de identificación provisto no pertenece a ningún registro del sistema.',
                    tipo: "peligro",
                    transparencia: 0.9,
                    posicion: 'center',
                    permiteCerrar: true,
                    onClosed: function () { regresarAlListado(); }
                }
                );
        };

        function mostrarAlertaNoEncontrado() {
            mostrarAlerta(
                '#arAlertasDelFormulario',
                {
                    mensaje: "No se han encontrado funcionarios con el número de cédula indicado",
                    tipo: "advertencia",
                    transparencia: 0.9,
                    posicion: 'center',
                    permiteCerrar: true
                }
                );

        };

        $(document).ready(function () {
            inicializarFormulario();
        });

        function inicializarFormulario() {
            $('#btnCancelar').click(function () {
                regresarAlListado();
            });

            $(window).keydown(function a(e) {

                if (e.keyCode == 13) {
                    return false;
                }
            });

            habilitarTooltipGenerico();

            cargarLupa();

        };

        function mostrarPopUp(pvc_IdPopup) {
            window.location = pvc_IdPopup;
        };

        function cargarLupa() {
            permutarImagenes('#imgBuscarSolicitante',
                '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Lupa.png")%>',
                '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Lupa.png")%>',
                '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Lupa.png")%>'
            );
        };

        function ocultarFiltroFuncionario() {
            window.location = '#CerrarPopUpBusquedaFuncionario';
        };

    </script>

</asp:Content>

