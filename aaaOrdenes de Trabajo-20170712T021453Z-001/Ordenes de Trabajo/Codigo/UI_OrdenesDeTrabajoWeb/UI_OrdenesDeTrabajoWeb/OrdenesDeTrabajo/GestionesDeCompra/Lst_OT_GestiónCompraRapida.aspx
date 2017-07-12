<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/Mp_Listado.master" AutoEventWireup="false" CodeFile="Lst_OT_GestiónCompraRapida.aspx.vb" Inherits="OrdenesDeTrabajo_GestionesDeCompra_Lst_OT_GestiónCompraRapida" %>

<%@ Register TagPrefix="wuc" Namespace="Utilerias.WebForms.Controles" Assembly="Utilerias.WebForms.Controles" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphFormulario" runat="Server">

    <header>
        <h2>Gestión de Compras por Unidad de Compra</h2>
    </header>

    <article data-grupo="FiltrosDeBusqueda" class="tituloSeccion">
        Criterios de búsqueda
    </article>

    <article data-grupo="FiltrosDeBusqueda" class="formulario">

        <article class="areaBotonesListado">
            <a id="lnkNuevoRegistroFiltro" href="Frm_OT_GestiónCompraRapida.aspx?pvn_Operacion=<%=Utilerias.OrdenesDeTrabajo.eOperacion.Agregar%>">
                <img id="imgNuevoRegistro2" data-tipo="nuevoRegistro" title="Agregar nuevo registro" alt="Agregar nuevo registro" src="" />
            </a>
        </article>

        <table>
            <tr>
                <th>Número Gestión</th>
                <td>
                    <asp:TextBox runat="server" ID="txtNumeroGestion" data-tipocontrol="texto" Width="59%" MaxLength="10"></asp:TextBox>
                    <ajax:FilteredTextBoxExtender ID="ftbTxtNumeroGestion" runat="server" TargetControlID="txtNumeroGestion" FilterType="Numbers"></ajax:FilteredTextBoxExtender>
                </td>
            </tr>
            <tr>
                <th>Fecha desde</th>
                <td>
                    <asp:TextBox runat="server" ID="txtFiltroFechaDesde" Width="145px" data-tipocontrol="texto"></asp:TextBox>
                    <asp:CompareValidator runat="server" ID="cmpvtxtFiltroDesde" ControlToValidate="txtFiltroFechaDesde" Display="Dynamic" ErrorMessage="Fecha desde inválida" Operator="DataTypeCheck" Type="Date">&nbsp;</asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <th>Fecha hasta</th>
                <td>
                    <asp:TextBox runat="server" ID="txtFiltroFechaHasta" Width="145px" data-tipocontrol="texto"></asp:TextBox>
                    <asp:CompareValidator runat="server" ID="cmpvtxtFiltroHasta" ControlToValidate="txtFiltroFechaHasta" Display="Dynamic" ErrorMessage="Fecha hasta inválida" Operator="DataTypeCheck" Type="Date">&nbsp;</asp:CompareValidator>
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
        Gestión de Compras por Unidad de Compra
    </article>

    <article data-grupo="Listado" class="listado">

        <article class="areaBotonesListado">
            <a id="lnkNuevoRegistro" href="Frm_OT_GestiónCompraRapida.aspx?pvn_Operacion=<%=Utilerias.OrdenesDeTrabajo.eOperacion.Agregar%>">
                <img id="imgNuevoRegistro" data-tipo="nuevoRegistro" title="Agregar nuevo registro" alt="Agregar nuevo registro" src="" />
            </a>
            <img id="imgMostrarFiltros" alt="Mostrar criterios de Búsqueda" title="Mostrar criterios de Búsqueda" data-tipo="mostrarFiltros" src="" />
        </article>

        <asp:Repeater runat="server" ID="rpCompraRapida">
            <HeaderTemplate>
                <table>
                    <tr>
                        <th>
                            <asp:LinkButton runat="server" ID="lnkNumeroGestion" Text="No. de Gestión" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GESTION_COMPRALST.NUMERO_GESTION%>" CommandArgument="ASC" OnCommand="lnkRpCompraRapida_Command"></asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton runat="server" ID="lnkFecha" Text="Fecha" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GESTION_COMPRALST.FECHA_REGISTRO_SOLICITUD%>" CommandArgument="ASC" OnCommand="lnkRpCompraRapida_Command"></asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton runat="server" ID="lnkEstado" Text="Estado" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GESTION_COMPRALST.DESC_ESTADO%>" CommandArgument="ASC" OnCommand="lnkRpCompraRapida_Command"></asp:LinkButton>
                        </th>
                        <th>&nbsp;</th>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr class="lineaDelListado">
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GESTION_COMPRALST.NUMERO_GESTION)%></td>
                    <td><%#CType(Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GESTION_COMPRALST.FECHA_REGISTRO_SOLICITUD), DateTime).ToString(Utilerias.OrdenesDeTrabajo.Constantes.FORMATO_FECHA_UI)%></td>
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GESTION_COMPRALST.DESC_ESTADO)%></td>
                    <td>
                        <a href="Lst_OT_RevisionGestiónCompraRapida.aspx?pvn_IdUbicacion=<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GESTION_COMPRALST.ID_UBICACION)%>&pvn_IdViaCompraContrato=<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GESTION_COMPRALST.ID_VIA_COMPRA_CONTRATO)%>&pvn_Annio=<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GESTION_COMPRALST.ANNO)%>&pvn_NumeroGestion=<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GESTION_COMPRALST.NUMERO_GESTION)%>">
                            <img title="Consultar registro" alt="Consultar registro" 
                                src='<%#AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Lista_Requisitos.png")%>' 
                                onmouseover='<%#String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Icono_Lista_Requisitos.png"))%>' 
                                onmouseout='<%#String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Lista_Requisitos.png"))%>' />
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
        <wuc:PaginadorNumerico runat="server" ID="pnRpCompraRapida" />
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

        $(document).ready(function () {
            aplicarConfiguracionBasicaListado({ alternarVisibilidadFiltroLista: true });

            configurarDatePickerRango("#<%=Me.txtFiltroFechaDesde.ClientID%>", "#<%=Me.txtFiltroFechaHasta.ClientID%>");

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

