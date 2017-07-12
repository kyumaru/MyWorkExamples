<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/Mp_Listado.master" AutoEventWireup="false" CodeFile="Lst_OT_DespachoMateriales.aspx.vb" Inherits="OrdenesDeTrabajo_Almacen_Lst_OT_DespachoMateriales" %>

<%@ Register TagPrefix="wuc" Namespace="Utilerias.WebForms.Controles" Assembly="Utilerias.WebForms.Controles" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphFormulario" Runat="Server">

    
    <header>
        <h2>Despacho de Materiales</h2>
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
                <th>Tipo</th>
                <td>
                    <asp:DropDownList Width="59%" runat="server" ID="ddlTipoOrden" data-tipocontrol="combo" AppendDataBoundItems="true"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <th>Fecha salida desde</th>
                <td>
                    <asp:TextBox Width="59%" runat="server" ID="txtFiltroFechaSalidaDesde" data-tipocontrol="texto"></asp:TextBox>
                </td>
                <th>Fecha salida hasta</th>
                <td>
                    <asp:TextBox Width="59%" runat="server" ID="txtFiltroFechaSalidaHasta" data-tipocontrol="texto"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>Fecha asignación desde</th>
                <td>
                    <asp:TextBox Width="59%" runat="server" ID="txtFiltroFechaAsignacionDesde" data-tipocontrol="texto"></asp:TextBox>
                </td>
                <th>Fecha asignación hasta</th>
                <td>
                    <asp:TextBox Width="59%" runat="server" ID="txtFiltroFechaAsignacionHasta" data-tipocontrol="texto"></asp:TextBox>
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
        Listado de Despacho de Materiales
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
                            <asp:LinkButton runat="server" ID="lnkIdOrdenTrabajo" Text="Número de OT" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_SOLICITUD_RETIROLST.ID_ORDEN_TRABAJO%>" CommandArgument="ASC" OnCommand="lnkRpOrdenTrabajo_Command"></asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton runat="server" ID="lnkTipoOrden" Text="Tipo Orden" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_SOLICITUD_RETIROLST.DESC_TIPO_ORDEN%>" CommandArgument="ASC" OnCommand="lnkRpOrdenTrabajo_Command"></asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton runat="server" ID="lnkEstado" Text="Estado Solicitud" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_SOLICITUD_RETIROLST.DESC_EST_SOL_RET%>" CommandArgument="ASC" OnCommand="lnkRpOrdenTrabajo_Command"></asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton runat="server" ID="lnkNombreTaller" Text="Sector o Taller" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_SOLICITUD_RETIROLST.NOMBRE_TALLER%>" CommandArgument="ASC" OnCommand="lnkRpOrdenTrabajo_Command"></asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton runat="server" ID="lnkSolicitante" Text="Solicitante" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_SOLICITUD_RETIROLST.COORD_ENCARGADO%>" CommandArgument="ASC" OnCommand="lnkRpOrdenTrabajo_Command"></asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton runat="server" ID="lnkFechaSalida" Text="Fecha Salida" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_SOLICITUD_RETIROLST.FECHA_HORA_RETIRO%>" CommandArgument="ASC" OnCommand="lnkRpOrdenTrabajo_Command"></asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton runat="server" ID="lnkFechaAsignacion" Text="Fecha Asignación" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_SOLICITUD_RETIROLST.FECHA_REGISTRO%>" CommandArgument="ASC" OnCommand="lnkRpOrdenTrabajo_Command"></asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton runat="server" ID="lnkCantidadLineas" Text="Cantidad Lineas" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_SOLICITUD_RETIROLST.CANTIDAD_LINEAS%>" CommandArgument="ASC" OnCommand="lnkRpOrdenTrabajo_Command"></asp:LinkButton>
                        </th>
                        <th>&nbsp;</th>
                        <th>&nbsp;</th>
                        <th>Listo Para Entrega</th>
                        <th>&nbsp;</th>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr class="lineaDelListado" id="trTabla" runat="server">
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_SOLICITUD_RETIROLST.ID_ORDEN_TRABAJO)%></td>
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_SOLICITUD_RETIROLST.DESC_TIPO_ORDEN)%></td>
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_SOLICITUD_RETIROLST.DESC_EST_SOL_RET)%></td>
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_SOLICITUD_RETIROLST.NOMBRE_TALLER)%></td>
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_SOLICITUD_RETIROLST.COORD_ENCARGADO)%></td>
                    <td><%#CType(Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_SOLICITUD_RETIROLST.FECHA_HORA_RETIRO), DateTime).ToString(Utilerias.OrdenesDeTrabajo.Constantes.FORMATO_FECHA_UI)%></td>
                    <td><%#CType(Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_SOLICITUD_RETIROLST.FECHA_REGISTRO), DateTime).ToString(Utilerias.OrdenesDeTrabajo.Constantes.FORMATO_FECHA_HORA_UI)%></td>
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_SOLICITUD_RETIROLST.CANTIDAD_LINEAS)%></td>
                    <td>
                        <asp:ImageButton runat="server" ID="ibVerDetalle" AlternateText="Ver Detalle" ToolTip="Ver Detalle"
                            CommandArgument='<%# String.Format("{0}¬{1}¬{2}¬{3}¬{4}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_SOLICITUD_RETIROLST.ID_UBICACION), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_SOLICITUD_RETIROLST.ID_ORDEN_TRABAJO),Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_SOLICITUD_RETIROLST.ANNO_ORDEN),Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_SOLICITUD_RETIROLST.ID_SOLICITUD_RETIRO),Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_SOLICITUD_RETIROLST.ANNO))%>'
                            ImageUrl='<%#AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Ver_Detalle.png")%>'
                            onmouseover='<%#String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Icono_Ver_Detalle.png"))%>'
                            onmouseout='<%#String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Ver_Detalle.png"))%>'
                            OnClick="ibVerDetalle_Click" />
                    </td>
                    <td>
                        <asp:ImageButton runat="server" ID="ibImprimir" AlternateText="Imprimir" ToolTip="Imprimir"
                            CommandArgument='<%# String.Format("{0}_{1}_{2}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_RETIROLST.ANNO), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_RETIROLST.ID_SOLICITUD_RETIRO), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_SOLICITUD_RETIROLST.ESTADO_SOLICITUD_RETIRO))%>'
                            ImageUrl='<%#AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Impresora.png")%>'
                            onmouseover='<%#String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Impresora.png"))%>'
                            onmouseout='<%#String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Impresora.png"))%>'
                            OnClick="ibImprimir_Click" />
                    </td>
                    <td>
                        <asp:CheckBox runat="server" ID="chkListoEntrega" AutoPostBack="true" OnCheckedChanged="chkListoEntrega_CheckedChanged"
                            CssClass='<%# String.Format("{0}¬{1}",  Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_SOLICITUD_RETIROLST.ANNO),Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_SOLICITUD_RETIROLST.ID_SOLICITUD_RETIRO))%>'
                            Visible='<%#IIf((CType(Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_SOLICITUD_RETIROLST.ESTADO_SOLICITUD_RETIRO), String) = "EPM") OR (CType(Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_SOLICITUD_RETIROLST.ESTADO_SOLICITUD_RETIRO), String) = "SLR"), True, False)%>' 
                            Checked='<%#IIf((CType(Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_SOLICITUD_RETIROLST.ESTADO_SOLICITUD_RETIRO), String) = "SLR"), True, False)%>' />
                    </td>
                    <td>
                        <asp:HiddenField runat="server" ID="hdfTipoOrden" Value="<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_SOLICITUD_RETIROLST.TIPO_ORDEN_TRABAJO)%>" />
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

        function mostrarAlertaActualizacionExitosaCHECKBOX1() {

            mostrarAlerta(
                '#arAlerta',
                {
                    mensaje: 'Solicitud lista para Retiro .',
                    tipo: "exito",
                    transparencia: 0.9,
                    posicion: 'center'
                });
            ocultarAreaFiltrosDeBusqueda();
        };

        function mostrarAlertaActualizacionExitosaCHECKBOX2() {

            mostrarAlerta(
                '#arAlerta',
                {
                    mensaje: 'Solicitud En Preparación de Materiales .',
                    tipo: "exito",
                    transparencia: 0.9,
                    posicion: 'center'
                });
            ocultarAreaFiltrosDeBusqueda();
        };


        $(document).ready(function () {
            aplicarConfiguracionBasicaListado({ alternarVisibilidadFiltroLista: true });

            configurarDatePickerRango('#<%=Me.txtFiltroFechaSalidaDesde.ClientID%>', '#<%=Me.txtFiltroFechaSalidaHasta.ClientID%>');
            configurarDatePickerRango('#<%=Me.txtFiltroFechaAsignacionDesde.ClientID%>', '#<%=Me.txtFiltroFechaAsignacionHasta.ClientID%>');

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

