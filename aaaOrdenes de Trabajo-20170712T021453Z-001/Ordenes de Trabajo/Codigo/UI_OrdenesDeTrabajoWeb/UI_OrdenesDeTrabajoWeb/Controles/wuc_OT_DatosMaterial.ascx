<%@ Control Language="VB" AutoEventWireup="false" CodeFile="wuc_OT_DatosMaterial.ascx.vb" Inherits="Controles_wuc_OT_DatosMaterial" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<article class="formulario sinBorde">
    <table>
        <tr>
            <th>Descripción</th>
            <td>
                <asp:Label runat="server" ID="lblDescripcion" data-tipocontrol="etiqueta"></asp:Label>
            </td>
        </tr>
        <tr>
            <th>Categoria</th>
            <td>
                <asp:Label runat="server" ID="lblCategoria"></asp:Label>
            </td>

        </tr>
        <tr>
            <th>SubCategoría</th>
            <td>
                <asp:Label runat="server" ID="lblSubCategoria"></asp:Label>
            </td>
        </tr>
        <tr>
            <th>Cantidad</th>
            <td>
                <asp:TextBox runat="server" ID="txtCantidad" data-tipocontrol="texto"></asp:TextBox>
                <asp:Label runat="server" ID="lblUnidadMedida"></asp:Label>
                <ajax:FilteredTextBoxExtender ID="ftbTxtCantidad" runat="server" TargetControlID="txtCantidad" FilterType="Numbers, Custom" FilterMode="ValidChars" ValidChars="."></ajax:FilteredTextBoxExtender>
                
            </td>
        </tr>
        <tr>
            <th>Monto Promedio</th>
            <td>
                <asp:Label runat="server" ID="lblMontoPromedio"></asp:Label>
            </td>
        </tr>
        <tr>
            <th>Detalle</th>
            <td>
                <asp:TextBox runat="server" ID="txtDetalle" TextMode="MultiLine" Rows="4" Columns="60"></asp:TextBox>
            </td>
            <td>
                <asp:HiddenField runat="server" ID="hdfMonto"/>
            </td>
        </tr>
    </table>
</article>
