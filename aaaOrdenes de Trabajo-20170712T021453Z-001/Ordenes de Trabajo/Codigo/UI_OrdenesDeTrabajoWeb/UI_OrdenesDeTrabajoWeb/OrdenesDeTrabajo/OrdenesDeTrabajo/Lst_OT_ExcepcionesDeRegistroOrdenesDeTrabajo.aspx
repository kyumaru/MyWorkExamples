<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/Mp_Listado.master" AutoEventWireup="false" CodeFile="Lst_OT_ExcepcionesDeRegistroOrdenesDeTrabajo.aspx.vb" Inherits="OrdenesDeTrabajo_Lst_OT_ExcepcionesDeRegistroOrdenesDeTrabajo" %>

<%@ Register TagPrefix="wuc" Namespace="Utilerias.WebForms.Controles" Assembly="Utilerias.WebForms.Controles" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphFormulario" runat="Server">

    <header>
        <h2>Excepciones para el registro de OT´s</h2>
    </header>

    <article data-grupo="FiltrosDeBusqueda" class="tituloSeccion">
        Criterios de búsqueda
    </article>

    <article data-grupo="FiltrosDeBusqueda" class="formulario">
        <article class="areaBotonesListado">
            <a id="lnkNuevoRegistro2" href="Frm_OT_ExcepcionesDeRegistroOrdenesDeTrabajo.aspx?pvn_Operacion=<%=Utilerias.OrdenesDeTrabajo.eOperacion.Agregar%>">
                <img id="imgNuevoRegistro2" data-tipo="nuevoRegistro" title="Agregar nueva orden" alt="Agregar nueva orden" src="" />
            </a>
        </article>

        <table>
            <tr>
                <th>Identificación</th>
                <td>
                    <asp:TextBox Width="59%" runat="server" ID="txtIdentificacion" data-tipocontrol="texto"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>Nombre</th>
                <td>
                    <asp:TextBox Width="59%" runat="server" ID="txtNombre" data-tipocontrol="texto"></asp:TextBox>
                </td>
            </tr>
        </table>
    </article>

    <article data-grupo="FiltrosDeBusqueda" class="areaBotones">
        <asp:Button runat="server" ID="btnBuscar" Text="Buscar" />
        <input type="button" data-tipo="limpiarFiltros" value="Limpiar Filtros" />
        <input id="btnCancelarBusqueda" type="button" data-tipo="cancelarBusqueda" value="Cancelar" />
    </article>

    <article data-grupo="Listado" class="tituloSeccion">
        Listado de Excepciones para el registro de OT´s
    </article>

    <article data-grupo="Listado" class="listado">
        <article class="areaBotonesListado">
            <a id="lnkNuevoRegistro" href="Frm_OT_ExcepcionesDeRegistroOrdenesDeTrabajo.aspx?pvn_Operacion=<%=Utilerias.OrdenesDeTrabajo.eOperacion.Agregar%>">
                <img id="imgNuevoRegistro" data-tipo="nuevoRegistro" title="Agregar nueva orden" alt="Agregar nueva orden" src="" />
            </a>
            <img id="imgMostrarFiltros" alt="Mostrar criterios de Búsqueda" title="Mostrar criterios de Búsqueda" data-tipo="mostrarFiltros" src="" />
        </article>

        <asp:Repeater runat="server" ID="rpExcepcionOrdenTrabajo">
            <HeaderTemplate>
                <table>
                    <tr>
                        <th>
                            <asp:LinkButton runat="server" ID="lnkIdentificacion" Text="Identificación" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OTF_EXCEPCION_PERIODOLST.IDENTIFICACION%>" CommandArgument="ASC" OnCommand="lnkRpExcepcionOrdenTrabajo_Command"></asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton runat="server" ID="lnkNombreFuncionario" Text="Nombre del Funcionario" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OTF_EXCEPCION_PERIODOLST.NOMBRE_FUNCIONARIO%>" CommandArgument="ASC" OnCommand="lnkRpExcepcionOrdenTrabajo_Command"></asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton runat="server" ID="lnkTiempoAsignado" Text="Tiempo Asignado" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OTF_EXCEPCION_PERIODOLST.TIEMPO_ASIGNADO%>" CommandArgument="ASC" OnCommand="lnkRpExcepcionOrdenTrabajo_Command"></asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton runat="server" ID="lnkVigencia" Text="Vigencia" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OTF_EXCEPCION_PERIODOLST.VIGENCIA%>" CommandArgument="ASC" OnCommand="lnkRpExcepcionOrdenTrabajo_Command"></asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton runat="server" ID="lnkTiempoRestante" Text="Tiempo Restante" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OTF_EXCEPCION_PERIODOLST.TIEMPO_RESTANTE%>" CommandArgument="ASC" OnCommand="lnkRpExcepcionOrdenTrabajo_Command"></asp:LinkButton>
                        </th>
                        <th>&nbsp;</th>
                        <th>&nbsp;</th>
                    </tr>
            </HeaderTemplate>

            <ItemTemplate>
                <tr class="lineaDelListado">
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTF_EXCEPCION_PERIODOLST.IDENTIFICACION)%></td>
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTF_EXCEPCION_PERIODOLST.NOMBRE_FUNCIONARIO)%></td>
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTF_EXCEPCION_PERIODOLST.TIEMPO_ASIGNADO)%></td>
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTF_EXCEPCION_PERIODOLST.VIGENCIA)%></td>
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTF_EXCEPCION_PERIODOLST.TIEMPO_RESTANTE)%></td>
                    <td>
                        <asp:ImageButton runat="server" ID="ibBorrar" AlternateText="Borrar registro" data-tipo="borrarRegistro"
                            ToolTip="Borrar registro"
                            CommandArgument="<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTF_EXCEPCION_PERIODOLST.ID_EXCEPCION_PERIODO)%>"
                            OnClick="ibBorrar_Click" />
                    </td>
                    <td>
                        <a href="Frm_OT_ExcepcionesDeRegistroOrdenesDeTrabajo.aspx?pvn_Operacion=<%#Utilerias.OrdenesDeTrabajo.eOperacion.Modificar%>&pvn_IdExcepcionPeriodo=<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTF_EXCEPCION_PERIODOLST.ID_EXCEPCION_PERIODO)%>">
                            <img title="Modificar registro" alt="Modificar registro" data-tipo="modificarRegistro" src="" />
                        </a>
                    </td>
                </tr>
            </ItemTemplate>

            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
    </article>

    <article data-grupo="Listado" class="areaPaginadorListado">
        <wuc:PaginadorNumerico runat="server" ID="pnRpExcepcionOrdenTrabajo" />
    </article>

    <article data-grupo="Listado" class="areaCantidadDeRegistro">
        <asp:Label runat="server" ID="lblCantidadDeRegistros" Text="" Visible="true"></asp:Label>
    </article>

    <article id="arAlerta"></article>
    <article id="arpopupConfirmaDeseaBorrar"></article>
    <article id="arPopupGenerico"></article>

    <script type="text/javascript">
        function mostrarAlertaError(pvc_Mensaje) {
            mostrarAlerta(
                '#arAlerta',
                {
                    mensaje: pvc_Mensaje,
                    tipo: 'peligro',
                    transparencia: 0.9,
                    posicion: 'center'
                }
            );
        };

        function mostrarAlertaNoHayDatos() {
            mostrarAlerta(
                '#arAlerta',
                {
                    mensaje: 'No se cuenta con información para mostrar.',
                    tipo: "advertencia",
                    transparencia: 0.9,
                    posicion: 'center'
                }
            );

            deshabilitarControl('#btnCancelarBusqueda');

            ocultarAreaDeListado();
            mostrarAreaFiltrosDeBusqueda();
        };

        function mostrarAlertaRegistroBorrado() {
            mostrarAlerta(
                '#arAlerta',
                {
                    mensaje: 'Se ha borrado el registro seleccionado.',
                    tipo: "exito",
                    transparencia: 0.9,
                    posicion: 'center'
                }
            );
        };

        function mostrarAlertaRegistroNoBorrado() {
            mostrarAlerta(
                '#arAlerta',
                {
                    mensaje: 'No ha sido posible borrar el registro seleccionado.',
                    tipo: "advertencia",
                    transparencia: 0.9,
                    posicion: 'center'
                }
            );
        }

        function mostrarAlertaActualizacionExitosa() {

            mostrarAlerta(
                '#arAlerta',
                {
                    mensaje: 'Se ha actualizado la información del registro.',
                    tipo: "exito",
                    transparencia: 0.9,
                    posicion: 'center'
                });

        };
        
        function mostrarPopupConfirmaDeseaBorrarRegistro(pvo_UniqueIdControl) {
            var vlo_ConfiguracionPopup = {
                titulo: 'Catálogo de excepciones de registro para Ordenes de Trabajo',
                mensaje: '¿Desea borrar el registro seleccionado?',
                botones:
                        [
                            {
                                idControl: "btnSi",
                                textoBoton: "Si",
                                onClick:
                                    function (e) {
                                        $(this).attr("disabled", "disabled");
                                        __doPostBack(pvo_UniqueIdControl, '');
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
                            }
                        ]
            };

            $('#arpopupConfirmaDeseaBorrar').popup(vlo_ConfiguracionPopup);

            window.location = '#arpopupConfirmaDeseaBorrar';

            return false;
        };

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

        $(document).ready(function () {
            aplicarConfiguracionBasicaListado({ alternarVisibilidadFiltroLista: true });

            $('[data-tipo="borrarRegistro"]').click(function () { return mostrarPopupConfirmaDeseaBorrarRegistro($(this).data("uniqueid")); });

        });

    </script>

</asp:Content>

