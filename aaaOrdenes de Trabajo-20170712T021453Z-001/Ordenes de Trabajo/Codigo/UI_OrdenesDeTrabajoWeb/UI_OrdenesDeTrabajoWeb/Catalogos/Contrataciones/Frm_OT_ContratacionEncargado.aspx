<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/MasterPage/Mp_Formulario.master" CodeFile="Frm_OT_ContratacionEncargado.aspx.vb" Inherits="Catalogos_Frm_OT_ContratacionEncargado" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Src="~/Controles/wucEmpleadosEU.ascx" TagName="wuc_EmpleadosEU" TagPrefix="uc1" %>


<asp:Content runat="server" ID="Content1" ContentPlaceHolderID="cphFormulario">

    <header>
        <h2><asp:Label runat="server" ID="lblAccion" ></asp:Label></h2>
    </header>

    <article class="tituloSeccion">
        Datos del Autorizado
    </article>

    <article class="formulario">
        <div style="display: none;">
            <asp:TextBox runat="server" ID="txtDatoValido"></asp:TextBox>
        </div>
        <table>
            <tr>
                <th>Identificacion</th>
                <td>
                    <asp:UpdatePanel runat="server" ID="upTxtIdSolicitante" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:TextBox runat="server" ID="txtIdSolicitante" data-tipocontrol="texto" AutoPostBack="true"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ID="rfvTxtIdSolicitante" ControlToValidate="txtIdSolicitante" Display="Dynamic" ErrorMessage="El número de Identificación del Funcionario es requerido." ValidationGroup="Aceptar">&nbsp;</asp:RequiredFieldValidator>
                            <asp:LinkButton ID="lnkEjecutarBusquedaSolicitante" runat="server">
                                <img id="imgBuscarSolicitante" title="Buscar Registro" alt="Buscar Registro" src="" />
                            </asp:LinkButton>
                            <br />
                            <span id="spContadorTxtIdSolicitante" class="contadorCaracteresRestantes"></span>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="txtIdSolicitante" EventName="TextChanged" />
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
            <tr runat="server" id="trEstado">
                <th>Estado</th>
                <td><asp:DropDownList Width="29%" runat="server" ID="ddlEstado" ></asp:DropDownList></td>
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
        <asp:Button runat="server" ValidationGroup="Aceptar" ID="btnAceptar" Text="Aceptar" />
        <input type="button" value="Cancelar" id="btnCancelar" />
    </article>

    <article id="arPopupDelFormulario"></article>
    <article id="arAlertasDelFormulario"></article>


    <script type="text/javascript">

        function deshabilitarFormulario() {
            deshabilitarControl('#<%=Me.btnAceptar.ClientID%>');
            deshabilitarControl('#btnLimpiarFormulario');
            deshabilitarControl('#btnCancelar');
            $('.formulario').attr('disabled', 'disabled');
        };
        function mostrarAlertaActualizacionExitosa() {
            deshabilitarFormulario();

            mostrarAlerta(
                '#arAlertasDelFormulario',
                {
                    mensaje: 'Se ha actualizado la información del Autorizado correctamente',
                    tipo: "exito",
                    transparencia: 0.9,
                    posicion: 'center',
                    permiteCerrar: true,
                    onClosed: function () { regresarAlListado(); }
                }
            );
        };

        function mostrarPopupRegistroExitoso() {
            var vlo_ConfiguracionPopup = {
                titulo: 'Catálogo de encargados del proceso de contratación',
                mensaje: 'Se ha registrado la información de un encargado(a) de contratación.<br /><strong>¿Desea registrar otro encargado(a)?</strong>',
                onClosed: function () { regresarAlListado(); },
                botones:
                    [
                        {
                            idControl: "btnSi",
                            textoBoton: "Sí",
                            onClick: function () { window.location = 'Frm_OT_ContratacionEncargado.aspx?pvn_Operacion=<%=Utilerias.OrdenesDeTrabajo.eOperacion.Agregar%>'; }
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

        function regresarAlListado() {
            window.location = 'Lst_OT_AutorizadosContratacion.aspx';
        };

        function mostrarPopUp(pvc_IdPopup) {
            window.location = pvc_IdPopup;
        };

        function inicializarFormulario() {

            $(window).keydown(function a(e) {

                if (e.keyCode == 13) {
                    return false;
                }
            });

            cargarLupa();
        };

        $(document).ready(function () {
            inicializarFormulario();

            $('#btnCancelar').click(function () {
                regresarAlListado();
            });


        });

        function ocultarFiltroFuncionario() {
            window.location = '#CerrarPopUpBusquedaFuncionario';
        };

        function cargarLupa() {
            permutarImagenes('#imgBuscarSolicitante',
                '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Lupa.png")%>',
                '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Lupa.png")%>',
                '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Lupa.png")%>'
            );

            permutarImagenes('#imgBuscarSolicitante',
                '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Lupa.png")%>',
                '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Lupa.png")%>',
                '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Lupa.png")%>'
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
                });
        };

        function mostrarAlertaError(pvc_Mensaje) {
            mostrarAlerta(
                '#arAlertasDelFormulario',
                {
                    mensaje: pvc_Mensaje,
                    tipo: 'peligro',
                    transparencia: 1,
                    posicion: 'center',
                    permiteCerrar: true
                }
            );
        };

    </script>

</asp:Content>