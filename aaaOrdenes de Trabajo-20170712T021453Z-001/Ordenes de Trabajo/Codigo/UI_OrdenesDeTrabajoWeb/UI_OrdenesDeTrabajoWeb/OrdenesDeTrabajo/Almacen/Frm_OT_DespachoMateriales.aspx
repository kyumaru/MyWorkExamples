<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/Mp_Formulario.master" AutoEventWireup="false" CodeFile="Frm_OT_DespachoMateriales.aspx.vb" Inherits="OrdenesDeTrabajo_Almacen_Frm_OT_DespachoMateriales" %>

<%@ Register TagPrefix="wuc" TagName="InformacionGeneral" Src="~/ControlesDeUsuario/Wuc_OT_InformacionGeneralDespacho.ascx" %>
<%@ Register TagPrefix="wuc" Namespace="Utilerias.WebForms.Controles" Assembly="Utilerias.WebForms.Controles" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphFormulario" runat="Server">

    <header>
        <h2>
            <asp:Label runat="server" ID="lblAccion" Text="Detalle de Solicitud de Materiales"></asp:Label>
        </h2>
    </header>

    <article class="tituloSeccion">
        Información General de la OT
    </article>
    <article class="formulario sinBorde">
        <wuc:InformacionGeneral runat="server" ID="ctrl_InfoGeneral" />
    </article>
    <br />

    <article class="tituloSeccion">
        Materiales Solicitados
    </article>
    <br />
    <article class="listado">
        <asp:Repeater runat="server" ID="rpPedidosPrevios">
            <HeaderTemplate>
                <table>
                    <tr>
                        <th>
                            <asp:LinkButton runat="server" ID="lnkIdMaterial" Text="Código" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_RETIROLst.CODIGO%>" CommandArgument="ASC" OnCommand="lnkRpPedidos_Command"></asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton runat="server" ID="lnkDescipcion" Text="Descripción" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_RETIROLst.DESCRIPCION%>" CommandArgument="ASC" OnCommand="lnkRpPedidos_Command"></asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton runat="server" ID="lnkCantSolicitada" Text="Cantidad Solicitada" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_RETIROLST.CANTIDAD_SOLICITADA%>" CommandArgument="ASC" OnCommand="lnkRpPedidos_Command"></asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton runat="server" ID="lknDispAlmacen" Text="Disp. Almacen" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_RETIROLST.DISPONIBLE_ALMACEN_SOLICITUD%>" CommandArgument="ASC" OnCommand="lnkRpPedidos_Command"></asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton runat="server" ID="lnkUbicacion" Text="Ubicación" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_RETIROLST.DESCRIPCION_UBICACION%>" CommandArgument="ASC" OnCommand="lnkRpPedidos_Command"></asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton runat="server" ID="lnkObservaciones" Text="Observaciones" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_RETIROLST.DETALLE_MATERIAL%>" CommandArgument="ASC" OnCommand="lnkRpPedidos_Command"></asp:LinkButton>
                        </th>
                    </tr>
            </HeaderTemplate>

            <ItemTemplate>
                <tr class="lineaDelListado">
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_RETIROLST.CODIGO)%></td>
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_RETIROLST.DESCRIPCION)%></td>
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_RETIROLST.CANTIDAD_SOLICITADA)%></td>
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_RETIROLST.DISPONIBLE_ALMACEN_SOLICITUD)%></td>
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_RETIROLST.DESCRIPCION_UBICACION)%></td>
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_RETIROLST.DETALLE_MATERIAL)%></td>
                </tr>
            </ItemTemplate>

            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
    </article>

    <article class="areaPaginadorListado">
        <wuc:PaginadorNumerico runat="server" ID="pnRpPedidosPrevios" />
    </article>
    <article class="areaCantidadDeRegistro">
        <asp:Label runat="server" ID="lblCantidadDeRegistros" Text="" Visible="true"></asp:Label>
    </article>
    <article class="areaBotones">
        <asp:Button runat="server" ID="btnImprimir" Text="Imprimir" />
        <asp:Button runat="server" ID="btnRegresar" Text="Regresar" />
    </article>

    <article id="arAlerta"></article>
    <article id="arpopupConfirmaDeseaBorrar"></article>
    <article id="popupConfirmaDeseaBorrar"></article>
    <article id="popupConfirmacionDeseaBorrar"></article>
    <article id="arPopupGenerico"></article>
    <article id="arPopupDelFormulario"></article>
    <article id="arAlertasDelFormulario"></article>

    <script type="text/javascript">


        function regresarAlListado() {
            __doPostBack('<%=Me.btnRegresar.UniqueID%>', '');
        };

        $(document).ready(function () {

            inicializarFormulario();

        });

        function mostrarPopUp(pvc_IdPopup) {
            window.location = pvc_IdPopup;
        };

        function inicializarFormulario() {

            $(window).keydown(function a(e) {

                if (e.keyCode == 13) {
                    return false;
                }
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

