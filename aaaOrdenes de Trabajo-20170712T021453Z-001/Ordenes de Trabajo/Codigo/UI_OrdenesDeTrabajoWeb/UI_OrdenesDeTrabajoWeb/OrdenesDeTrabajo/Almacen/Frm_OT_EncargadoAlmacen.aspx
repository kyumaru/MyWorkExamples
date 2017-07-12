<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/Mp_Formulario.master" AutoEventWireup="false" CodeFile="Frm_OT_EncargadoAlmacen.aspx.vb" Inherits="OrdenesDeTrabajo_Almacen_Frm_OT_EncargadoAlmacen" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Src="~/Controles/wucEmpleadosEU.ascx" TagName="wuc_EmpleadosEU" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphFormulario" Runat="Server">
        
    <header>
        <h2>
            <asp:Label runat="server" ID="lblAccion"></asp:Label>
        </h2>
    </header>

    <article class="tituloSeccion">
        Mantenimiento de Encargados Almacén
    </article>

      <article class="formulario" id="formularioTotal" runat="server">
        <table>
            
            <tr>
                <th>Identificación del Funcionario</th>
                <td>
                    <asp:UpdatePanel runat="server" ID="upTxtIdFuncionario" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:TextBox Width="70%" runat="server" ID="txtIdFuncionario" data-tipocontrol="texto" AutoPostBack="true"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ID="rfvTxtIdFuncionario" ControlToValidate="txtIdFuncionario" Display="Dynamic" ErrorMessage="El número de Identificación del funcionario es requerido." ValidationGroup="Aceptar">&nbsp;</asp:RequiredFieldValidator>
                            <asp:LinkButton ID="lnkEjecutarBusquedaFuncionario" runat="server">
                                <img id="imgBuscarFuncionario" title="Buscar Registro" alt="Buscar Registro" src="" />
                            </asp:LinkButton>
                            <br />
                            <span id="spContadorTxtIdFuncionario" class="contadorCaracteresRestantes"></span>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="txtIdFuncionario" EventName="TextChanged" />
                            <%--<asp:AsyncPostBackTrigger ControlID="btnLimpiarFormulario" EventName="Click" />--%>
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
                        <asp:AsyncPostBackTrigger ControlID="txtIdFuncionario" EventName="TextChanged" />
                        <asp:AsyncPostBackTrigger ControlID="wuc_EmpleadosEU" />
                         </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
             <tr>
                <th>Rol</th>
                <td>
                    <asp:DropDownList Width="70%" runat="server" ID="ddlRol" data-tipocontrol="combo" AppendDataBoundItems="true"></asp:DropDownList>
                    <asp:RequiredFieldValidator runat="server" ID="rfvDdlRol" ControlToValidate="ddlRol" Display="Dynamic" ErrorMessage="El Rol es obligatorio." ValidationGroup="Aceptar"></asp:RequiredFieldValidator>
                </td>
            </tr>
             <tr runat="server" id="trEstado">
                <th>Estado</th>
                <td>
                    <asp:DropDownList Width="40%" runat="server" ID="ddlEstado"></asp:DropDownList>
                </td>
            </tr>
        </table>
      </article>

     <article class="areaBotones">
        <asp:Button runat="server" ID="btnAceptar" Text="Aceptar" ValidationGroup="Aceptar" />
         <%--<asp:Button runat="server" ID="btnLimpiarFormulario" Text="Limpiar Formulario"  data-tipo="limpiarFormulario" />--%> 
        <%--<input id="btnLimpiarFormulario" type="button" data-tipo="limpiarFormulario" value="Limpiar formulario" />--%>
        <input id="btnCancelar" type="button" value="Cancelar" />
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

        function InhabilitarCodigo() {

            document.getElementById("imgBuscarMaterial").style.visibility = "hidden";

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

        function regresarAlListado() {
            window.location = 'Lst_OT_EncargadoAlmacen.aspx';
        };

        function mostrarPopUp(pvc_IdPopup) {
            window.location = pvc_IdPopup;
        };

        function mostrarPopupRegistroExitoso() {
            var vlo_ConfiguracionPopup = {
                titulo: 'Catálogo de Encargados Almacén',
                mensaje: 'Se ha registrado la información.. <br/><strong>¿Desea registrar otro registro?</strong>',
                onClosed: function () { regresarAlListado(); },
                botones:
                        [
                            {
                                idControl: "btnSi",
                                textoBoton: "Sí",
                                onClick: function () { window.location = 'Frm_OT_EncargadoAlmacen.aspx?pvn_Operacion=<%=Utilerias.OrdenesDeTrabajo.eOperacion.Agregar%>'; }
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

        function mostrarAlertaActualizacionExitosa() {
            deshabilitarControl('#<%=btnAceptar.ClientID%>');
            //deshabilitarControl('#btnLimpiarFormulario');
            deshabilitarControl('#btnCancelar');
            $('.formulario').attr('disabled', 'disabled');

            mostrarAlerta(
                '#arAlertasDelFormulario',
                {
                    mensaje: 'Se ha actualizado la información del registro.',
                    tipo: "exito",
                    transparencia: 0.9,
                    posicion: 'center',
                    onClosed: function () { regresarAlListado(); }
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

        function mostrarAlertaLlaveIncorrecta() {
            deshabilitarControl('#<%=btnAceptar.ClientID%>');
            //deshabilitarControl('#btnLimpiarFormulario');
            deshabilitarControl('#btnCancelar');
            $('.formulario').attr('disabled', 'disabled');

            mostrarAlerta(
                '#arAlertasDelFormulario',
                {
                    mensaje: 'El identificador provisto no pertenece a ningun registro del sistema.',
                    tipo: "peligro",
                    transparencia: 0.9,
                    posicion: 'center',
                    onClosed: function () { regresarAlListado(); }
                });
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

            $('[data-tipo="borrarRegistro"]').click(function () { return mostrarPopupConfirmaDeseaBorrarRegistro($(this).data("uniqueid")); });

            habilitarTooltipGenerico();

            cargarLupa();
        };

        function ocultarFiltroFuncionario() {
            window.location = '#CerrarPopUpBusquedaFuncionario';
        }

        function MensajePopup(pvc_Mensaje, pvc_PaginaDestino) {

            var vlo_ConfiguracionPopup = {
                titulo: '<em>Mensajes del Sistema</em>',
                mensaje: pvc_Mensaje,
                onClosed: function (e) {
                    $(this).removeAttr('href');
                    cerrarPopup();
                    if (pvc_PaginaDestino != '') {
                        redireccionarListado(pvc_PaginaDestino);
                    }
                },

                botones:
            [
                {
                    idControl: "btnAceptar",
                    textoBoton: "Aceptar",
                    onClick: function () {
                        cerrarPopup();
                        if (pvc_PaginaDestino != '') {
                            redireccionarListado(pvc_PaginaDestino);
                        }
                    }
                }
            ]
            };

            $('#arPopupGenerico').popup(vlo_ConfiguracionPopup);

            window.location = '#arPopupGenerico';
        };

        function redireccionarListado(pvc_PaginaDestino) {
            window.location = pvc_PaginaDestino
        };

        function cargarLupa() {
            permutarImagenes('#imgBuscarFuncionario',
                '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Lupa.png")%>',
                '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Lupa.png")%>',
                '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Lupa.png")%>'
            );
        }

    </script>

</asp:Content>

