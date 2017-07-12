<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/Mp_Listado.master" AutoEventWireup="false" CodeFile="Lst_OT_AprobacionReingresoMateriales.aspx.vb" Inherits="OrdenesDeTrabajo_Almacen_Lst_OT_AprobacionReingresoMateriales" %>

<%@ Register TagPrefix="wuc" Namespace="Utilerias.WebForms.Controles" Assembly="Utilerias.WebForms.Controles" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphFormulario" runat="Server">

    <header>
        <h2>Reingreso de Materiales</h2>
    </header>

    <article data-grupo="FiltrosDeBusqueda" class="tituloSeccion">
        Criterios de búsqueda
    </article>

    <article data-grupo="FiltrosDeBusqueda" class="formulario">
        <table>
            <tr>
                <th>Número de Orden</th>
                <td>
                    <asp:TextBox runat="server" ID="txtNumOT" data-tipocontrol="texto" Width="59%" MaxLength="10"></asp:TextBox>
                    <ajax:FilteredTextBoxExtender ID="ftbTxtNumOT" runat="server" TargetControlID="txtNumOT" FilterMode="ValidChars" ValidChars="-" FilterType="Numbers, Custom"></ajax:FilteredTextBoxExtender>
                </td>
            </tr>
            <tr>
                <th>Fecha solicitud desde</th>
                <td>
                    <asp:TextBox Width="59%" runat="server" ID="txtFiltroFechaDesde" data-tipocontrol="texto"></asp:TextBox>
                </td>
                <th>Fecha solicitud hasta</th>
                <td>
                    <asp:TextBox Width="59%" runat="server" ID="txtFiltroFechaHasta" data-tipocontrol="texto"></asp:TextBox>
                </td>
            </tr>
        </table>
    </article>

    <article data-grupo="FiltrosDeBusqueda" class="areaBotones">
        <asp:Button runat="server" ID="btnBuscar" Text="Buscar" />
        <input type="button" data-tipo="limpiarFiltros" value="Limpiar Filtros" />
        <input id="btnCancelarBusqueda" type="button" data-tipo="cancelarBusqueda" value="Cancelar" />
    </article>

    <article data-grupo="Listado" class="tituloSeccion" style="width: 450px !important;">
        Listado de Aprobación Reingreso de Materiales
    </article>

    <article data-grupo="Listado" class="listado sinBorde">
        <article class="areaBotonesListado">
            <img id="imgMostrarFiltros" alt="Mostrar criterios de Búsqueda" title="Mostrar criterios de Búsqueda" data-tipo="mostrarFiltros" src="" />
        </article>

        <asp:Repeater runat="server" ID="rpOrdenTrabajo">
            <HeaderTemplate>
                <table>
                    <tr>
                        <th>
                            <asp:LinkButton runat="server" ID="lnkIdOrdenTrabajo" Text="Número de OT" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OT_ORDEN_SOLIC_REING.ID_ORDEN_TRABAJO%>" CommandArgument="ASC" OnCommand="lnkRpOrdenTrabajo_Command"></asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton runat="server" ID="lnkCantLineas" Text="Cantidad de Lineas" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OT_ORDEN_SOLIC_REING.CANT_SOLICITUDES_PEN%>" CommandArgument="ASC" OnCommand="lnkRpOrdenTrabajo_Command"></asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton runat="server" ID="lnkFechaReingreso" Text="Fecha Solicitud Reingreso" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OT_ORDEN_SOLIC_REING.FECHA_ULTIMA_SOLICITUD%>" CommandArgument="ASC" OnCommand="lnkRpOrdenTrabajo_Command"></asp:LinkButton>
                        </th>
                        <th>&nbsp;</th>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr class="lineaDelListado" id="trTabla" runat="server">
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_ORDEN_SOLIC_REING.ID_ORDEN_TRABAJO)%></td>
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_ORDEN_SOLIC_REING.CANT_SOLICITUDES_PEN)%></td>
                    <td><%#CType(Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_ORDEN_SOLIC_REING.FECHA_ULTIMA_SOLICITUD), DateTime).ToString(Utilerias.OrdenesDeTrabajo.Constantes.FORMATO_FECHA_UI)%></td>
                    <td>
                        <asp:ImageButton runat="server" ID="ibVerDetalle" AlternateText="Trámitar Reingreso" ToolTip="Trámitar Reingreso"
                            CommandArgument='<%# String.Format("{0}¬{1}¬{2}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_ORDEN_SOLIC_REING.ID_UBICACION), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_ORDEN_SOLIC_REING.ID_ORDEN_TRABAJO), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_ORDEN_SOLIC_REING.ANNO))%>'
                            ImageUrl='<%#AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Ver_Detalle.png")%>'
                            onmouseover='<%#String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Icono_Ver_Detalle.png"))%>'
                            onmouseout='<%#String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Ver_Detalle.png"))%>'
                            OnClick="ibVerDetalle_Click" />
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>

    </article>

    <article data-grupo="Listado" class="areaPaginadorListado">
        <wuc:PaginadorNumerico runat="server" ID="pnRpOrdenTrabajo" />
    </article>

    <article data-grupo="Listado" class="areaCantidadDeRegistro">
        <asp:Label runat="server" ID="lblCantidadDeRegistros" Text="" Visible="true"></asp:Label>
    </article>

    <article id="arAlerta"></article>
    <article id="arpopupConfirmaEnviar"></article>
    <article id="arPopupGenerico"></article>

    <script type="text/javascript">

        function mostrarAlertaAdvertencia(pvc_Mensaje) {
            mostrarAlerta(
                '#arAlerta',
                {
                    mensaje: pvc_Mensaje,
                    tipo: 'advertencia',
                    transparencia: 0.9,
                    posicion: 'center'
                }
            );
            ocultarAreaFiltrosDeBusqueda();
        };

        function mostrarAlertaAdvertenciaFiltro(pvc_Mensaje) {
            mostrarAlerta(
                '#arAlerta',
                {
                    mensaje: pvc_Mensaje,
                    tipo: 'advertencia',
                    transparencia: 0.9,
                    posicion: 'center'
                }
            );
            ocultarAreaDeListado();
            mostrarAreaFiltrosDeBusqueda();
        };

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
            ocultarAreaFiltrosDeBusqueda();
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

        function mostrarAlertaActualizacionExitosa() {

            mostrarAlerta(
                '#arAlerta',
                {
                    mensaje: 'Se ha actualizado la información de la orden.',
                    tipo: "exito",
                    transparencia: 0.9,
                    posicion: 'center'
                });
            ocultarAreaFiltrosDeBusqueda();
        };

        $(document).ready(function () {
            aplicarConfiguracionBasicaListado({ alternarVisibilidadFiltroLista: true });

            configurarDatePickerRango('#<%=Me.txtFiltroFechaDesde.ClientID%>', '#<%=Me.txtFiltroFechaHasta.ClientID%>');

             $('[data-tipo="tooltip"]').each(function () {
                 $('#' + this.id).tooltipster({
                     interactive: true,
                     interactiveTolerance: 400,
                     theme: 'tooltipster-light'
                 });
             });
         });

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

    </script>

</asp:Content>

