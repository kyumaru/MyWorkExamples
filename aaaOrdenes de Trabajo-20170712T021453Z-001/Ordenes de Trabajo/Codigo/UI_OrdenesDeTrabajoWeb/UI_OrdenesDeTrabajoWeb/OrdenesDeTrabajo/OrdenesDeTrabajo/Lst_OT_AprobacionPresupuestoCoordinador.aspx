<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/Mp_Listado.master" AutoEventWireup="false" CodeFile="Lst_OT_AprobacionPresupuestoCoordinador.aspx.vb" Inherits="OrdenesDeTrabajo_Lst_OT_AprobacionPresupuestoCoordinador" %>

<%@ Register TagPrefix="wuc" Namespace="Utilerias.WebForms.Controles" Assembly="Utilerias.WebForms.Controles" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphFormulario" Runat="Server">

    <header>
        <h2>Aprobación de Infome de valoración presupuestaria para la realización de la obra por parte del coordinador</h2>
    </header>

    <article data-grupo="FiltrosDeBusqueda" class="tituloSeccion">
        Criterios de búsqueda
    </article>

    <article data-grupo="FiltrosDeBusqueda" class="formulario">
        <table>
            <tr>
                <th>Número de Orden</th>
                <td>
                    <asp:TextBox runat="server" ID="txtNumOrden" data-tipocontrol="texto" TextMode="MultiLine" Rows="1" Width="59%" MaxLength="512"></asp:TextBox>
                    <ajax:FilteredTextBoxExtender ID="ftbtxtNumOrden" runat="server" TargetControlID="txtNumOrden" FilterMode="ValidChars" ValidChars="-" FilterType="Numbers"></ajax:FilteredTextBoxExtender>
                </td>
            </tr>
            <tr>
                <th>Nombre Proyecto</th>
                <td>
                    <asp:TextBox Width="59%" runat="server" ID="txtDescTrabajo" data-tipocontrol="texto"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>Fecha Solicitud</th>
                <td>
                    <asp:TextBox Width="59%" runat="server" ID="txtFechaSolicitud" data-tipocontrol="texto"></asp:TextBox>
                    <asp:CompareValidator runat="server" ID="cmpvTxtFechaSolicitud" ControlToValidate="txtFechaSolicitud" Display="Dynamic" ErrorMessage="La Fecha de solicitud del trabajo es inválida" Operator="DataTypeCheck" Type="Date" ValidationGroup="Buscar">&nbsp;</asp:CompareValidator>
                </td>
            </tr>
        </table>
    </article>

    <article data-grupo="FiltrosDeBusqueda" class="areaBotones">
        <asp:Button runat="server" ID="btnBuscar" Text="Buscar" ValidationGroup="Buscar" />
        <input type="button" data-tipo="limpiarFiltros" value="Limpiar Filtros" />
        <input id="btnCancelarBusqueda" type="button" data-tipo="cancelarBusqueda" value="Cancelar" />
    </article>


     <article data-grupo="Listado" class="tituloSeccion">
        Listado para Aprobación presupuestaria
    </article>

    <article data-grupo="Listado" class="listado">
        <article class="areaBotonesListado">
            <img id="imgMostrarFiltros" alt="Mostrar criterios de Búsqueda" title="Mostrar criterios de Búsqueda" data-tipo="mostrarFiltros" src="" />
        </article>

        <asp:Repeater runat="server" ID="rpOrdenTrabajo">
            <HeaderTemplate>
                <table>
                    <tr>
                        <th>
                            <asp:LinkButton runat="server" ID="lnkNumeroOT" Text="No. OT" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ORDEN_TRABAJOLST.ID_ORDEN_TRABAJO%>" CommandArgument="ASC" OnCommand="lnkRpOrdenTrabajo_Command"></asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton runat="server" ID="lnkNombreProyecto" Text="Nombre Proyecto" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ORDEN_TRABAJOLST.NOMBRE_PROYECTO%>" CommandArgument="ASC" OnCommand="lnkRpOrdenTrabajo_Command"></asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton runat="server" ID="lnkFechaSolicita" Text="Fecha Solicitud" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ORDEN_TRABAJOLST.FECHA_HORA_SOLICITA%>" CommandArgument="ASC" OnCommand="lnkRpOrdenTrabajo_Command"></asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton runat="server" ID="lnkEstado" Text="Estado" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ORDEN_TRABAJOLST.DESC_ESTADO_ORDEN%>" CommandArgument="ASC" OnCommand="lnkRpOrdenTrabajo_Command"></asp:LinkButton>
                        </th>
                        <th>&nbsp;</th>
                    </tr>
            </HeaderTemplate>

            <ItemTemplate>
                <tr class="lineaDelListado">
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ORDEN_TRABAJOLST.ID_ORDEN_TRABAJO)%></td>
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ORDEN_TRABAJOLST.NOMBRE_PROYECTO)%></td>
                    <td><%#CType(Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ORDEN_TRABAJOLST.FECHA_HORA_SOLICITA), DateTime).ToString(Utilerias.OrdenesDeTrabajo.Constantes.FORMATO_FECHA_UI)%></td>
                    <td>
                        <asp:LinkButton runat="server" ID="lnkTrazabilidad" Style="text-decoration: underline; color: blue;"
                            Text="<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ORDEN_TRABAJOLST.DESC_ESTADO_ORDEN)%>"
                            CommandArgument='<%# String.Format("{0}¬{1}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ORDEN_TRABAJOLST.ID_UBICACION), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ORDEN_TRABAJOLST.ID_ORDEN_TRABAJO))%>'
                            OnCommand="lnkTrazabilidad_Command"></asp:LinkButton>
                    </td>
                    <td>
                        <a href="Frm_OT_AprobacionPresupuestoCoordinador.aspx?pvn_IdUbicacion=<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ORDEN_TRABAJOLST.ID_UBICACION)%>&pvc_IdOrdenTrabajo=<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ORDEN_TRABAJOLST.ID_ORDEN_TRABAJO)%>&pvn_Anno=<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ORDEN_TRABAJOLST.ANNO)%>">
                            <img title="Revisar la Orden" alt="Revisar la Orden"
                                src='<%#AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Check.png")%>'
                                onmouseover='<%#String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Icono_Check.png"))%>'
                                onmouseout='<%#String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Check.png"))%>' />
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
        <wuc:PaginadorNumerico runat="server" ID="pnRpOrdenTrabajo" />
    </article>

    <article data-grupo="Listado" class="areaCantidadDeRegistro">
        <asp:Label runat="server" ID="lblCantidadDeRegistros" Text="" Visible="true"></asp:Label>
    </article>

    <article id="arAlerta"></article>
    <article id="arPopupGenerico"></article>

    <script type="text/javascript">

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
           
        }

        $(document).ready(function () {
            aplicarConfiguracionBasicaListado({ alternarVisibilidadFiltroLista: true });
            configurarDatePicker('#<%=Me.txtFechaSolicitud.ClientID%>');
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

