<%@ Control Language="VB" AutoEventWireup="false" CodeFile="wuc_OT_Adjunto_Orden_Trabajo.ascx.vb" Inherits="Controles_wuc_OT_Adjunto_Orden_Trabajo" %>

<article class="formulario" runat="server" id="frmVersion" visible="false">
    <table>
        <tr>
            <th>Versión</th>
            <td>
                <asp:DropDownList runat="server" ID="ddlVersion" AutoPostBack="true" AppendDataBoundItems="true"></asp:DropDownList>
            </td>
        </tr>
    </table>
</article>
<br />

<article class="listado sinBorde">

    <asp:Repeater runat="server" ID="rpAdjunto">
        <HeaderTemplate>
            <table>
                <tr>
                    <th>Archivo</th>
                    <th>Descripcion</th>
                    <th>Tipo</th>
                </tr>
        </HeaderTemplate>
        <ItemTemplate>
            <tr class="lineaDelListado">
                <td>
                    <asp:LinkButton runat="server" ID="lnkArchivo"
                        CommandArgument='<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_ADJUNTO.ID_ADJUNTO_ORDEN_TRABAJO)%>'
                        Style="text-decoration: underline; color: blue;"
                        OnCommand="lnkArchivo_Command"
                        Text="<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_ADJUNTO.NOMBRE_ARCHIVO)%>"></asp:LinkButton>
                </td>
                <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_ADJUNTO.DESCRIPCION)%></td>
                <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_ADJUNTO.DESC_TIPO_DOCUMENTO)%></td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            </table>
        </FooterTemplate>
    </asp:Repeater>

    <script type="text/javascript">

        $(document).ready(function () {

        });

    </script>

</article>
