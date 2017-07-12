<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/MasterPage/Mp_Listado.master" CodeFile="Lst_OT_RetiroMateriales.aspx.vb" Inherits="OrdenesDeTrabajo_Almacen_Lst_OT_RetiroMateriales" %>

<%@ Register TagPrefix="wuc" Namespace="Utilerias.WebForms.Controles" Assembly="Utilerias.WebForms.Controles" %>


<asp:content runat="server" ID="Content1" ContentPlaceHolderID="cphFormulario">

    <header>
        <h2>Despacho de materiales</h2>
    </header>

    <article data-grupo="FiltrosDeBusqueda" class="tituloSeccion">
        Criterios de búsqueda
    </article>

    <article data-grupo="FiltrosDeBusqueda" class="formulario">
        <table>
            <tr>
                <th>Número de Orden</th>
                <td>
                    <asp:TextBox Width="59%" runat="server" ID="txtFiltroNumOrden" data-tipocontrol="texto" TextMode="MultiLine" Rows="1"></asp:TextBox>
                    
                </td>
            </tr>
            <tr>
                <th>Tipo de Orden de Trabajo</th>
                <td>
                    <asp:TextBox Width="59%" runat="server" ID="txtFiltroTipoOrden" data-tipocontrol="texto" TextMode="MultiLine" Rows="1"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>Taller o Sector</th>
                <td>
                    <asp:DropDownList Width="59%" runat="server" ID="ddlfiltroTallerSector" data-tipocontrol="combo" AppendDataBoundItems="true"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <th>Solicitante</th>
                <td>
                    <asp:TextBox Width="59%" runat="server" ID="txtFiltroSolcitante" data-tipocontrol="texto" TextMode="MultiLine" Rows="1"></asp:TextBox>
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
        Listado de solicitudes de despacho
    </article>

    <article data-grupo="Listado">
        <table runat="server" id="trTaller">
            <tr>
                <th>Taller o Sector</th>
                <td>
                    <asp:DropDownList Width="59%" runat="server" ID="ddlFiltroSectorTaller" data-tipocontrol="combo" AutoPostBack="true" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlFiltroSectorTaller_SelectedIndexChanged"></asp:DropDownList>
                </td>
            </tr>
        </table>
    </article>

    <article class="listado sinBorde" data-grupo="Listado">
        <article class="areaBotonesListado">
            <img id="imgMostrarFiltros" alt="Mostrar criterios de Búsqueda" title="Mostrar criterios de Búsqueda" data-tipo="mostrarFiltros" src="" />
        </article>
        <asp:Repeater runat="server" ID="rpOrdenTrabajo">
            <HeaderTemplate>
                <table>
                    <tr>
                        <th>
                            <asp:LinkButton runat="server" ID="lnkNumOt" Text="No. OT" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_SOLICITUD_RETIROLST.ID_ORDEN_TRABAJO%>" CommandArgument="ASC" OnCommand="lnkRpOrdenTrabajo_Command"></asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton runat="server" id="lnkTipoOrden" Text="Tipo Orden" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_SOLICITUD_RETIROLST.TIPO_ORDEN_TRABAJO%>" OnCommand="lnkRpOrdenTrabajo_Command"></asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton runat="server" id="lnkTallerSector" Text="Taller o Sector" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_SOLICITUD_RETIROLST.ID_SECTOR_TALLER%>" OnCommand="lnkRpOrdenTrabajo_Command"></asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton runat="server" id="lnkSolicitante" Text="Solicitante" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_SOLICITUD_RETIROLST.SOLICITANTE_GESTION_SALIDA%>" OnCommand="lnkRpOrdenTrabajo_Command"></asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton runat="server" id="lnkFechaSalida" Text="Fecha Salida" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_SOLICITUD_RETIROLST.FECHA_HORA_RETIRO%>" OnCommand="lnkRpOrdenTrabajo_Command"></asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton runat="server" id="lnkFechaAsignacion" Text="Fecha Asignación" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_SOLICITUD_RETIROLST.FECHA_REGISTRO%>" OnCommand="lnkRpOrdenTrabajo_Command"></asp:LinkButton>
                        </th>
                        <th>Entregado</th>
                        <th>&nbsp;</th>
                    </tr>
                
            </HeaderTemplate>
            <ItemTemplate>
                <tr runat="server" id="trOrden" class="lineaDelListado">
                    <td><asp:LinkButton runat="server" ID="lnkNumOt" Text="<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_SOLICITUD_RETIROLST.ID_ORDEN_TRABAJO)%>"
                        CommandArgument='<%# String.Format("{0}¬{1}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_SOLICITUD_RETIROLST.ID_UBICACION), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_SOLICITUD_RETIROLST.ID_ORDEN_TRABAJO))%>'
                        OnClick="lnkNumOt_Click"></asp:LinkButton>
                    </td>
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_SOLICITUD_RETIROLST.DESC_TIPO_ORDEN)%></td>
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_SOLICITUD_RETIROLST.NOMBRE_TALLER)%></td>
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_SOLICITUD_RETIROLST.SOLICITANTE_GESTION_SALIDA)%></td>
                    <td><%#CType(Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_SOLICITUD_RETIROLST.FECHA_HORA_RETIRO), DateTime).ToString(Utilerias.OrdenesDeTrabajo.Constantes.FORMATO_FECHA_UI)%></td>
                    <td><%#CType(Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_SOLICITUD_RETIROLST.FECHA_REGISTRO), DateTime).ToString(Utilerias.OrdenesDeTrabajo.Constantes.FORMATO_FECHA_HORA_UI)%></td>
                    
                    <td><asp:CheckBox runat="server" ID="chkEntregado" 
                        data-fila='<%#Container.ItemIndex%>' 
                        data-idOrdenTrabajo='<%#DataBinder.Eval(Container.DataItem, Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_SOLICITUD_RETIROLST.ID_ORDEN_TRABAJO)%>'
                        data-idUbicacion='<%#DataBinder.Eval(Container.DataItem, Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_SOLICITUD_RETIROLST.ID_UBICACION)%>'
                        data-idSectorTaller='<%#DataBinder.Eval(Container.DataItem, Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_SOLICITUD_RETIROLST.ID_SECTOR_TALLER)%>'
                        data-anno='<%#DataBinder.Eval(Container.DataItem, Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_SOLICITUD_RETIROLST.ANNO)%>'
                        data-idSolicitudRetiro='<%#DataBinder.Eval(Container.DataItem, Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_SOLICITUD_RETIROLST.ID_SOLICITUD_RETIRO)%>'
                        Autopostback="true" OnCheckedChanged="chkEntregado_CheckedChanged" /></td>
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

        function mostrarAlertaNoHayDatosSinFiltro() {
            mostrarAlerta(
                '#arAlerta',
                {
                    mensaje: 'No existen registros asociados al sector o taller seleccionado.',
                    tipo: "advertencia",
                    transparencia: 0.9,
                    posicion: 'center'
                }
            );
            ocultarAreaFiltrosDeBusqueda();
        };


        $(document).ready(function () {

            aplicarConfiguracionBasicaListado({ alternarVisibilidadFiltroLista: true });

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

</asp:content>