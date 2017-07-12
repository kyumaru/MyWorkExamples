<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/Mp_Listado.master" AutoEventWireup="false" CodeFile="Lst_OT_UnidadesPorSede.aspx.vb" Inherits="Catalogos_Lst_OT_UnidadesPorSede" %>

<%@ Register TagPrefix="wuc" Namespace="Utilerias.WebForms.Controles" Assembly="Utilerias.WebForms.Controles" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphFormulario" runat="Server">

    <header>
        <h2>Unidades por Sede</h2>
    </header>

    <article data-grupo="FiltrosDeBusqueda" class="tituloSeccion">
        Criterios de búsqueda
    </article>

    <article data-grupo="FiltrosDeBusqueda" class="formulario">
        <article class="areaBotonesListado">
            <a id="lnkNuevoRegistro2" href="Frm_OT_UnidadesPorSede.aspx?pvn_Operacion=<%=Utilerias.OrdenesDeTrabajo.eOperacion.Agregar%>">
                <img id="imgNuevoRegistro2" data-tipo="nuevoRegistro" title="Agregar registro" alt="Agregar registro" src="" />
            </a>
        </article>

        <table>
            <tr>
                <th>Sede</th>
                <td>
                    <asp:DropDownList Width="70%" runat="server" ID="ddlSede" data-tipocontrol="combo" AppendDataBoundItems="true"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <th>Unidad</th>
                <td>
                    <asp:TextBox Width="70%" runat="server" ID="txtUnidad" data-tipocontrol="texto"></asp:TextBox>
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
        Listado de Unidades por Sede
    </article>

    <article data-grupo="Listado" class="listado">
        <article class="areaBotonesListado">
            <a id="lnkNuevoRegistro" href="Frm_OT_UnidadesPorSede.aspx?pvn_Operacion=<%=Utilerias.OrdenesDeTrabajo.eOperacion.Agregar%>">
                <img id="imgNuevoRegistro" data-tipo="nuevoRegistro" title="Agregar registro" alt="Agregar registro" src="" />
            </a>
            <img id="imgMostrarFiltros" alt="Mostrar criterios de Búsqueda" title="Mostrar criterios de Búsqueda" data-tipo="mostrarFiltros" src="" />
        </article>

        <asp:Repeater runat="server" ID="rpUnidadSede">
            <HeaderTemplate>
                <table>
                    <tr>
                        <th>
                            <asp:LinkButton runat="server" ID="lnkUbicacion" Text="Ubicación" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_UNIDAD_UBICACIONLST.DESC_UBICACION%>" CommandArgument="ASC" OnCommand="lnkRpUnidadSede_Command"></asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton runat="server" ID="lnkUnidad" Text="Unidad" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_UNIDAD_UBICACIONLST.DESC_COD_UNIDAD_SIRH%>" CommandArgument="ASC" OnCommand="lnkRpUnidadSede_Command"></asp:LinkButton>
                        </th>
                        <th>&nbsp;</th>
                        <th>&nbsp;</th>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr class="lineaDelListado">
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_UNIDAD_UBICACIONLST.DESC_UBICACION)%></td>
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_UNIDAD_UBICACIONLST.DESC_COD_UNIDAD_SIRH)%></td>
                    <td>
                        <asp:ImageButton runat="server" ID="ibBorrar" AlternateText="Borrar registro" data-tipo="borrarRegistro"
                            ToolTip="Borrar registro"
                            CommandArgument="<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_UNIDAD_UBICACIONLST.COD_UNIDAD_SIRH)%>"
                            OnClick="ibBorrar_Click" />
                    </td>
                    <td>
                        <a href="Frm_OT_UnidadesPorSede.aspx?pvn_Operacion=<%#Utilerias.OrdenesDeTrabajo.eOperacion.Modificar%>&pvn_CodUnidadSirh=<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_UNIDAD_UBICACIONLST.COD_UNIDAD_SIRH)%>">
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
        <wuc:PaginadorNumerico runat="server" ID="pnRpUnidadSede" />
    </article>

    <article data-grupo="Listado" class="areaCantidadDeRegistro">
        <asp:Label runat="server" ID="lblCantidadDeRegistros" Text="" Visible="true"></asp:Label>
    </article>

    <article id="arAlerta"></article>
    <article id="arpopupConfirmaDeseaBorrar"></article>

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
        }

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

            //mostrarAreaDeListado();
            //ocultarAreaFiltrosDeBusqueda();
        }

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
        }

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
                    posicion: 'center',
                    onClosed: function () { regresarAlListado(); }
                });

        };

        function mostrarPopupConfirmaDeseaBorrarRegistro(pvo_UniqueIdControl) {
            var vlo_ConfiguracionPopup = {
                titulo: 'Catálogo de Unidades por Sede',
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
        }

        $(document).ready(function () {
            aplicarConfiguracionBasicaListado({ alternarVisibilidadFiltroLista: true });

            $('[data-tipo="borrarRegistro"]').click(function () { return mostrarPopupConfirmaDeseaBorrarRegistro($(this).data("uniqueid")); });

        });

    </script>

</asp:Content>

