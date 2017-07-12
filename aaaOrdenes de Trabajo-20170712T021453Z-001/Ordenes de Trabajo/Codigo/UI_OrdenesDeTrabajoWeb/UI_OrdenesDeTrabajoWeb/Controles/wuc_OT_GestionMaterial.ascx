<%@ Control Language="VB" AutoEventWireup="false" CodeFile="wuc_OT_GestionMaterial.ascx.vb" Inherits="Controles_wuc_OT_GestionMaterial" %>


<article class="formulario">
<asp:Label runat="server" ID="lblFecha"></asp:Label><br /><br />
    <asp:Label runat="server" ID="lblJornada"></asp:Label>

</article>



<article class="listado sinBorde">

    <asp:UpdatePanel runat="server" UpdateMode="Conditional" ID="upRp">
        <ContentTemplate>
            <asp:Repeater runat="server" ID="rpPedidos">
                <HeaderTemplate>
                    <table>
                        <tr>
                            <th>
                                <asp:LinkButton runat="server" ID="lnkCodigo" Text="Código"></asp:LinkButton>
                            </th>
                            <th>
                                <asp:LinkButton runat="server" id="lnkDescripcion" Text="Descripción"></asp:LinkButton>
                            </th>
                            <th>
                                <asp:LinkButton runat="server" id="lnCantidadSolicitada" Text="Cantidad Solicitada"></asp:LinkButton>
                            </th>
                            <th>
                                <asp:LinkButton runat="server" id="lnkCantidadDisponible" Text="Cantidad Retirada"></asp:LinkButton>
                            </th>
                            <th>
                                <asp:LinkButton runat="server" id="lnkEstado" Text="Estado"  ></asp:LinkButton>
                            </th>
                            <th>
                                <asp:LinkButton runat="server" id="lnkMontoSalida" Text="Monto Salida"  ></asp:LinkButton>
                            </th>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr class="lineaDelListado">
                        <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_RETIROLST.CODIGO)%></td>
                        <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_RETIROLST.DESCRIPCION)%></td>
                        <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_RETIROLST.CANTIDAD_SOLICITADA)%></td>
                        <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_RETIROLST.CANTIDAD_RETIRADA)%></td>
                        <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_RETIROLST.DESC_ESTADO)%></td>
                        <td style="text-align: right;"><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_RETIROLST.COSTO_CALCULADO, "{0:n2}")%></td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
        </ContentTemplate>

    </asp:UpdatePanel>

</article>

<article class="areaCantidadDeRegistro" data-grupo="Listado">
        <asp:Label runat="server" ID="lblMontoTotal" Text=""></asp:Label>
    </article>
