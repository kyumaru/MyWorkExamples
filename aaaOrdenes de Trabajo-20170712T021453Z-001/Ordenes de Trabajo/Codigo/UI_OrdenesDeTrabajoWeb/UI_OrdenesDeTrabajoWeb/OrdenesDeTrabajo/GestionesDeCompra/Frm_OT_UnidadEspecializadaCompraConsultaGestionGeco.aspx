<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/Mp_Formulario.master" AutoEventWireup="false" CodeFile="Frm_OT_UnidadEspecializadaCompraConsultaGestionGeco.aspx.vb" Inherits="OrdenesDeTrabajo_GestionesDeCompra_Frm_OT_UnidadEspecializadaCompraConsultaGestionGeco" %>

<%@ Register TagPrefix="wuc" Namespace="Utilerias.WebForms.Controles" Assembly="Utilerias.WebForms.Controles" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphFormulario" runat="Server">

    <header>
        <h2>Gestión de Compras por Unidad Especializada de Compra</h2>
    </header>

    <article class="listado">
        <asp:Repeater runat="server" ID="rpGrupos">
            <HeaderTemplate>
                <table>
                    <tr>
                        <th>
                            <asp:LinkButton runat="server" ID="lnkNumLinea" Text="No. de Linea" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GRUPO_GESTION_COMPRALST.NUMERO_LINEA%>" CommandArgument="ASC" OnCommand="lnkRpGrupos_Command"></asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton runat="server" ID="lnkPartida" Text="Partida" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GRUPO_GESTION_COMPRALST.PARTIDA_PRESUPUESTARIA%>" CommandArgument="ASC" OnCommand="lnkRpGrupos_Command"></asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton runat="server" ID="lnkCodigo" Text="Código" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GRUPO_GESTION_COMPRALST.ID_MATERIAL%>" CommandArgument="ASC" OnCommand="lnkRpGrupos_Command"></asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton runat="server" ID="lnkDescripcion" Text="Descripción" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GRUPO_GESTION_COMPRALST.DESCRIPCION%>" CommandArgument="ASC" OnCommand="lnkRpGrupos_Command"></asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton runat="server" ID="lnkCantidad" Text="Cantidad" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GRUPO_GESTION_COMPRALST.CANTIDAD_SOLICITADA_MEDIDA%>" CommandArgument="ASC" OnCommand="lnkRpGrupos_Command"></asp:LinkButton>
                        </th>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr class="lineaDelListado">
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GRUPO_GESTION_COMPRALST.NUMERO_LINEA)%></td>
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GRUPO_GESTION_COMPRALST.PARTIDA_PRESUPUESTARIA)%></td>
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GRUPO_GESTION_COMPRALST.ID_MATERIAL)%></td>
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GRUPO_GESTION_COMPRALST.DESCRIPCION)%></td>
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GRUPO_GESTION_COMPRALST.CANTIDAD_SOLICITADA_MEDIDA)%></td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
    </article>

    <article class="areaPaginadorListado">
        <wuc:PaginadorNumerico runat="server" ID="pnRpRevision" />
    </article>

    <article class="areaCantidadDeRegistro">
        <asp:Label runat="server" ID="lblCantidadDeRegistros" Text="" Visible="true"></asp:Label>
    </article>

    <br />

    <article class="tituloSeccion">
        Solicitud en GECO
    </article>

    <article class="formulario">
        <table>
            <tr>
                <th>Número de Gestión de GECO</th>
                <td>
                    <asp:Label runat="server" ID="lblNumeroGECO"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>Archivo</th>
                <td>
                    <asp:LinkButton runat="server" ID="lnkArchivo" Style="text-decoration: underline;"></asp:LinkButton>
                </td>
            </tr>
        </table>
    </article>

    <article class="areaBotones">
        <asp:Button runat="server" ID="btnCancelar" Text="Cancelar" />
        <asp:Button runat="server" ID="btnCotizar" Text="Cotizar" />
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

        $(document).ready(function () {

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

        function irAListado() {
            window.location = 'Lst_OT_UnidadEspecializadaCompraCotizacion.aspx';
        };

    </script>

</asp:Content>

