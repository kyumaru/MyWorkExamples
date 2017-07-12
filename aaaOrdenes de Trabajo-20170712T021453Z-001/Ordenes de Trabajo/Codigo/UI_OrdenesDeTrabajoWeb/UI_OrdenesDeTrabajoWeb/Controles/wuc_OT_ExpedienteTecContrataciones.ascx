<%@ Control Language="VB" AutoEventWireup="false" CodeFile="wuc_OT_ExpedienteTecContrataciones.ascx.vb" Inherits="Controles_wuc_OT_ExpedienteTecContrataciones" %>

<article class="listado sinBorde">

    <article class="areaBotones">
        <asp:Button runat="server" ID="btnDescargarTodos" Text="Descargar Todos" />
    </article> 

    <asp:UpdatePanel runat="server" UpdateMode="Conditional" ID="upRpAdjunto">
        <ContentTemplate>
            <asp:Repeater runat="server" ID="rpAdjunto">
                <HeaderTemplate>
                    <table>
                        <tr>
                            <th>Archivo</th>
                            <th>Descripcion</th>
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
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
        </ContentTemplate>
        <Triggers>
        </Triggers>

    </asp:UpdatePanel>

</article>