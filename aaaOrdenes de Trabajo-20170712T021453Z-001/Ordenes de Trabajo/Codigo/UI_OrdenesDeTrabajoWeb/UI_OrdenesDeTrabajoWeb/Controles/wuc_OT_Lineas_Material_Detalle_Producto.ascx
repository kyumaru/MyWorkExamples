<%@ Control Language="VB" AutoEventWireup="false" CodeFile="wuc_OT_Lineas_Material_Detalle_Producto.ascx.vb" Inherits="Controles_wuc_OT_Lineas_Material_Detalle_Producto" %>

<article class="listado sinBorde">

    <asp:Repeater runat="server" ID="rpLineas">
        <HeaderTemplate>
            <table>
                <tr>
                    <th>No. de OT</th>
                    <th>Código</th>
                    <th>Descripción</th>
                    <th>Cantidad</th>
                    <th>&nbsp;</th>
                </tr>
        </HeaderTemplate>
        <ItemTemplate>
            <tr class="lineaDelListado">
                <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_DETALLE_MATERIAL.ID_ORDEN_TRABAJO)%></td>
                <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_DETALLE_MATERIAL.ID_MATERIAL)%></td>
                <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_DETALLE_MATERIAL.DESCRIPCION)%></td>
                <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_DETALLE_MATERIAL.CANTIDAD_SOLICITADA_MEDIDA)%></td>
                <td>
                    <asp:ImageButton runat="server" ID="ibBorrar" AlternateText="Borrar"
                        CommandArgument='<%# String.Format("{0},{1}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_DETALLE_MATERIAL.ID_DETALLE_MATERIAL), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_DETALLE_MATERIAL.POSEE_GESTION_COMPRA))%>' OnClick="ibBorrar_Click"
                        Visible='<%# IIf(Me.GestionCompra.Estado = Utilerias.OrdenesDeTrabajo.EstadoGestionCompra.CREADA, True, False)%>'
                        ImageUrl='<%#AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Borrar.png")%>'
                        onmouseover='<%# String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Icono_Borrar.png"))%>'
                        onmouseout='<%# String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Borrar.png"))%>' />
                </td>
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
