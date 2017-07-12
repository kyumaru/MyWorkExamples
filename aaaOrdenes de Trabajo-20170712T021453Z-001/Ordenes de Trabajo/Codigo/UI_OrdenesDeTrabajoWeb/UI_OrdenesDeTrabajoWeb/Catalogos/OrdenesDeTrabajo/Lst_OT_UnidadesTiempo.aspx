<%@ Page Title="Listado de Unidades de tiempo" MasterPageFile="~/MasterPage/Mp_Listado.master" Language="VB" AutoEventWireup="false" CodeFile="Lst_OT_UnidadesTiempo.aspx.vb" Inherits="Catalogos_Lst_OT_UnidadesTiempo" %>

<%@ Register TagPrefix="wuc" Namespace="Utilerias.WebForms.Controles" Assembly="Utilerias.WebForms.Controles" %>

<asp:Content runat="server" ContentPlaceHolderID="cphFormulario" ID="Content1">
    <header>
        <h2>Catálogo de Unidades de Tiempo</h2>
    </header>

    <article data-grupo="FiltrosDeBusqueda" class="tituloSeccion">
        Criterios de búsqueda
    </article>
    <article data-grupo="FiltrosDeBusqueda" class="formulario">
        <article class="areaBotonesListado">
            <a href="Frm_OT_UnidadesDeTiempo.aspx?pvn_Operacion=<%=Utilerias.OrdenesDeTrabajo.eOperacion.Agregar%>">
                <img alt="Registrar nuevas Unidades de Tiempo" data-tipo="nuevoRegistro" src="" />
            </a>
            <img alt="Mostrar criterios de búsqueda" data-tipo="mostrarFiltros" src="" id="img1" />
        </article>
        <table>
            <tr>
                <th>Descripción</th>
                <td>
                    <asp:TextBox Width="40%" runat="server" ID="txtFiltroDescripcion" data-tipocontrol="texto"></asp:TextBox>
                    <br />
                    <span id="spContadorTxtDescripcion" class="contadorCaracteresRestantes"></span>
                </td>
            </tr>
            <tr>
                <th>Estado</th>
                <td>
                    <asp:DropDownList Width="40%" runat="server" ID="ddlFiltroEstado" AppendDataBoundItems="true" data-tipocontrol="combo"></asp:DropDownList>
                </td>
            </tr>
        </table>
    </article>
    <article data-grupo="FiltrosDeBusqueda" class="areaBotones">
        <asp:Button runat="server" ID="btnBuscar" Text="Buscar" />
        <input type="button" data-tipo="limpiarFiltros" value="Limpiar Filtros" />
        <input type="button" data-tipo="cancelarBusqueda" value="Cancelar" id="btnCancelarBusqueda" />
    </article>

    <article data-grupo="Listado" class="tituloSeccion">
        Listado de Unidades de Tiempo
    </article>

    <article data-grupo="Listado" class="listado">
        <article class="areaBotonesListado">
            <a href="Frm_OT_UnidadesDeTiempo.aspx?pvn_Operacion=<%=Utilerias.OrdenesDeTrabajo.eOperacion.Agregar%>">
                <img alt="Registrar nuevas Unidades de Tiempo" data-tipo="nuevoRegistro" src="" />
            </a>
            <img alt="Mostrar criterios de búsqueda" data-tipo="mostrarFiltros" src="" id="imgMostrarFiltros" />
        </article>

        <asp:Repeater ID="rpUnidadesTiempo" runat="server">

            <HeaderTemplate>
                <table>
                    <tr>
                        <th>
                            <asp:LinkButton runat="server" Text="Descripcion" ID="Descripcion" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_UNIDAD_TIEMPOLST.DESCRIPCION%>" CommandArgument="ASC" OnCommand="lnkRPUnidadesTiempo_Command"></asp:LinkButton>
                        </th>

                        <th>
                            <asp:LinkButton runat="server" Text="Estado" ID="Estado" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_UNIDAD_TIEMPOLST.ESTADO%>" CommandArgument="ASC" OnCommand="lnkRPUnidadesTiempo_Command"></asp:LinkButton>
                        </th>
                        <th>&nbsp;</th>
                        <th>&nbsp;</th>
                    </tr>
            </HeaderTemplate>

            <ItemTemplate>
                <tr class="lineaDelListado">
                    <td><%# Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_UNIDAD_TIEMPOLST.DESCRIPCION)%></td>
                    <td><%# Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_UNIDAD_TIEMPOLST.DESCRIPCION_ESTADO)%></td>
                    <td>
                        <asp:ImageButton runat="server" ID="ibBorrar" AlternateText="Borrar la unidad de tiempo" data-tipo="borrarRegistro"
                            CommandArgument="<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_UNIDAD_TIEMPOLST.ID_UNIDAD_TIEMPO)%>"
                            Visible="<%#IIf(CType(Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_UNIDAD_TIEMPOLST.POSEE_REGISTROS_ASOCIADOS), Integer) = 0, True, False)%>"
                            OnClick="ibBorrar_Click" />
                    </td>
                    <td>
                        <a href="Frm_OT_UnidadesDeTiempo.aspx?pvn_Operacion=<%#Utilerias.OrdenesDeTrabajo.eOperacion.Modificar%>&pvc_IdUnidad=<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_UNIDAD_TIEMPOLST.ID_UNIDAD_TIEMPO)%>">
                            <img alt="Modificar datos de la Unidad de tiempo" data-tipo="modificarRegistro" src="" />
                        </a>
                    </td>
                </tr>
            </ItemTemplate>

            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>

    </article>

    <article class="areaPaginadorListado" data-grupo="Listado">
        <wuc:PaginadorNumerico runat="server" ID="pnRpUnidadesTiempo" />
    </article>

    <article class="areaCantidadDeRegistro" data-grupo="Listado">
        <asp:Label runat="server" ID="lblCantidadDeRegistros" Text=""></asp:Label>
    </article>

    <article id="arAlerta"></article>
    <article id="popupConfirmacionDeseaBorrar"></article>
    <article id="arPopupGenerico"></article>

    <script type="text/javascript">
        function mostrarAlertaError(pvc_Msj) {
            mostrarAlerta(
                '#arAlerta',
                {
                    mensaje: pvc_Msj,
                    tipo: "peligro",
                    transparencia: 0.9,
                    posicion: 'center'
                }
            );
        };

        function mostrarAlertaRegistroBorrado() {
            mostrarAlerta(
                '#arAlerta',
                {
                    mensaje: "Se ha borrado la Unidad de Tiempo",
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
                    mensaje: "No ha sido posible borrar la Unidad de tiempo seleccionada",
                    tipo: "advertencia",
                    transparencia: 0.9,
                    posicion: 'center'
                }
            );
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

        function mostrarAlertaNoHayDatos() {
            mostrarAlerta(
                '#arAlerta',
                {
                    mensaje: "No se cuenta con Unidades de tiempo que cumplan con la(s) condicion(es) indicada(s)",
                    tipo: "advertencia",
                    transparencia: 0.9,
                    posicion: 'center'
                }
            );

            deshabilitarControl('#btnCancelarBusqueda');
            ocultarAreaDeListado();
            mostrarAreaFiltrosDeBusqueda();
        };

        function mostrarPopupConfirmacionDeseaBorrarRegistro(pvc_UniqueIdControl) {
            var vlo_ConfiguracionPopup = {
                titulo: "<em>Catalogo de Espacio</em>",
                mensaje: "¿Realmente desea borrar la unidad de tiempo seleccionada?",
                botones:
                    [
                        {
                            idControl: "btnSi",
                            textoBoton: "Sí",
                            onClick:
                                function (e) {
                                    $(this).attr("disabled", "disabled");
                                    __doPostBack(pvc_UniqueIdControl, '');
                                }
                        },
                    {
                        idControl: "btnNo",
                        textoBoton: "No",
                        onClick:
                            function (e) { cerrarPopup(); }
                    },
                    {
                        idControl: "btnCancelar",
                        textoBoton: "Cancelar",
                        onClick:
                            function (e) { cerrarPopup(); }
                    }

                    ]
            };

            $('#popupConfirmacionDeseaBorrar').popup(vlo_ConfiguracionPopup);
            window.location = "#popupConfirmacionDeseaBorrar";

            return false;
        };

        $(document).ready(function () {


            aplicarConfiguracionBasicaListado({ alternarVisibilidadFiltroLista: true });
            $('[data-tipo="borrarRegistro"]').click(function () { return mostrarPopupConfirmacionDeseaBorrarRegistro($(this).data("uniqueid")); });

        });
    </script>

</asp:Content>
