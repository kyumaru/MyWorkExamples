<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Wuc_OT_InformacionGeneral.ascx.vb" Inherits="ControlesDeUsuario_Wuc_OT_InformacionGeneral" %>

<article class="formulario">
    <table runat="server">
        <tr>
            <th>No. de Orden de Trabajo:</th>
            <td style="width: 30%;">
                <asp:Label runat="server" ID="lblNOrden" Visible="true" Text=""></asp:Label>
            </td>
            <th style="text-align: left;">Tipo de Orden:</th>
            <td style="text-align: left;">
                <asp:Label runat="server" ID="lblTipoOrden" Visible="true" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <th>Unidad Solicitante:</th>
            <td style="width: 30%;">
                <asp:Label runat="server" ID="lblUnidadSolicitante" Visible="true" Text=""></asp:Label>
            </td>
            <th style="text-align: left;">Categoría:</th>
            <td style="text-align: left;">
                <asp:Label runat="server" ID="lblCategoria" Visible="true" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <th>Taller o Sector:</th>
            <td style="width: 30%;">
                <asp:Label runat="server" ID="lblTaller" Visible="true" Text=""></asp:Label>
            </td>
            <th style="text-align: left;">Descripción del trabajo:</th>
            <td>
                <img runat="server" ID="imgDescripcion" data-tipo="tooltip"  class="tooltip"  />
            </td>
        </tr>
        <tr>
            <th>Responsable:</th>
            <td colspan="2">
                <asp:Label runat="server" ID="lblResponsable" Visible="true" Text=""></asp:Label>
            </td>
        </tr>
        <tr id="trNombreProyecto" runat="server">
            <th>Nombre Del Proyecto:</th>
            <td colspan="3">
                <asp:Label runat="server" ID="lblNombreProyecto" Visible="true" Text=""></asp:Label>
            </td>
        </tr>
        <tr id="trNumContrato" runat="server">
            <th>N° de Contrato</th>
            <td colspan="3">
                <asp:Label runat="server" ID="lblNContrato" Visible="true" Text=""></asp:Label>
            </td>
        </tr>
               
        <tr runat="server" id="trNombreContrato">
            <th>Nombre del contrato</th>
            <td colspan="3">
                <asp:Label runat="server" ID="lblNombreContrato" Visible="true" Text=""></asp:Label>
            </td>
        </tr>
    </table>
</article>

<script type="text/javascript">
    $(document).ready(function () {
            
        $('[data-tipo="tooltip"]').attr('src', '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x24, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Informacion.png")%>')

        habilitarTooltipGenerico();

    });
</script>

