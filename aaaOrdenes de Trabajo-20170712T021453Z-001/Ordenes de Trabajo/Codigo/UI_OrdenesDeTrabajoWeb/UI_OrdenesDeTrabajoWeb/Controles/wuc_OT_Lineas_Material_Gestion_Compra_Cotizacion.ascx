<%@ Control Language="VB" AutoEventWireup="false" CodeFile="wuc_OT_Lineas_Material_Gestion_Compra_Cotizacion.ascx.vb" Inherits="Controles_wuc_OT_Lineas_Material_Gestion_Compra_Cotizacion" %>

<article class="listado sinBorde">

    <asp:Repeater runat="server" ID="rpProveedores">
        <ItemTemplate>
            <asp:Label runat="server" ID="lblNombreProveedor" Text='<%# String.Format("{0}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_PROVEEDOR_COTIZACIONLST.NOMBRE_PROVEEDOR))%>'></asp:Label>
            &nbsp;&nbsp;
            <asp:TextBox runat="server" ID="txtCantidadProveedor" 
                OnTextChanged="txtCantidadProveedor_TextChanged"                
                AutoPostBack="true"
                Enabled="<%#IIf((CType(Me.GestionCompra.Estado, String) = Utilerias.OrdenesDeTrabajo.EstadoGestionCompra.REGISTRO_DE_COTIZACIONES), True, False)%>"></asp:TextBox>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:HiddenField runat="server" Value='<%# String.Format("{0}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_PROVEEDOR_COTIZACIONLST.IDENTIFICACION))%>' ID="hdfIdentificacion"/>
        </ItemTemplate>
    </asp:Repeater>
    
    <br />
    <br />
    <asp:Repeater runat="server" ID="rpLineas">
        <HeaderTemplate>
            <table>
                <tr>
                    <th>No. de OT</th>
                    <th>Código</th>
                    <th>Descripción</th>
                    <th>Cantidad</th>
                    <th>Proveedor</th>
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
