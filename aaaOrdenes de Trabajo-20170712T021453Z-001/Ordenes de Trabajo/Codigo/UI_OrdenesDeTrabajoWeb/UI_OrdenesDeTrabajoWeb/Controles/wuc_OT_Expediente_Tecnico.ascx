<%@ Control Language="VB" AutoEventWireup="false" CodeFile="wuc_OT_Expediente_Tecnico.ascx.vb" Inherits="Controles_wuc_OT_Expediente_Tecnico" %>

<article class="formulario sinBorde" style="float: right !important;">
    <asp:CheckBox runat="server" ID="chkSeleccionarTodos" AutoPostBack="true" />&nbsp;&nbsp;
    Seleccionar Todos
</article>

<article class="listado sinBorde">

    <asp:UpdatePanel runat="server" UpdateMode="Conditional" ID="upRpAdjunto">
        <ContentTemplate>
            <asp:Repeater runat="server" ID="rpAdjunto">
                <HeaderTemplate>
                    <table>
                        <tr>
                            <th>&nbsp;</th>
                            <th>Archivo</th>
                            <th>Descripcion</th>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr class="lineaDelListado">
                        <td>
                            <asp:CheckBox runat="server" ID="chkArchivo" Checked='<%#IIf(CType(Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_ADJUNTO.EXPEDIENTE_TECNICO), String) = "0", False, True)%>' />
                            <asp:HiddenField runat="server" ID="hdfIdAdjunto" Value="<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_ADJUNTO.ID_ADJUNTO_ORDEN_TRABAJO)%>" />
                        </td>
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
            <asp:AsyncPostBackTrigger ControlID="chkSeleccionarTodos" EventName="CheckedChanged" />
        </Triggers>

    </asp:UpdatePanel>

</article>
