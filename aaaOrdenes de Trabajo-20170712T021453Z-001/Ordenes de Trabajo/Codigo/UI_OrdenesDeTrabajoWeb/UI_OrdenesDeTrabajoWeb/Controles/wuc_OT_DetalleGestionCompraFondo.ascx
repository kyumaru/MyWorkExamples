<%@ Control Language="VB" AutoEventWireup="false" CodeFile="wuc_OT_DetalleGestionCompraFondo.ascx.vb" Inherits="Controles_wuc_OT_DetalleGestionCompraFondo" %>

<article class="listado sinBorde" style="overflow-x:auto;overflow-x:scroll">

    <asp:Repeater runat="server" ID="rpLineas">
        <HeaderTemplate>
            <table>
                <tr>
                    <th>No. de OT</th>
                    <th>Código</th>
                    <th>Descripción</th>
                    <th>Cantidad</th>
                    <th>Proveedores</th>
                </tr>
        </HeaderTemplate>
        <ItemTemplate>
            <tr class="lineaDelListado">
                <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_LINEA_GESTION_COMPRALST.NUMERO_OT)%></td>
                <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_LINEA_GESTION_COMPRALST.ID_MATERIAL)%></td>
                <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_LINEA_GESTION_COMPRALST.DESCRIPCION)%></td>
                <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_LINEA_GESTION_COMPRALST.CANTIDAD_SOLICITADA_MEDIDA)%></td>
                <td><%#Eval("PROVEEDORES")%></td>
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
