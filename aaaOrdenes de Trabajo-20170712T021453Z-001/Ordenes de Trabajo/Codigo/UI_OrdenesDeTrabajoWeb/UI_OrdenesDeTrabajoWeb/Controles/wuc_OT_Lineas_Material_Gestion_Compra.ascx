<%@ Control Language="VB" AutoEventWireup="false" CodeFile="wuc_OT_Lineas_Material_Gestion_Compra.ascx.vb" Inherits="Controles_wuc_OT_Lineas_Material_Gestion_Compra" %>

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
                <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_LINEA_GESTION_COMPRALST.NUMERO_OT)%></td>
                <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_LINEA_GESTION_COMPRALST.ID_MATERIAL)%></td>
                <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_LINEA_GESTION_COMPRALST.DESCRIPCION)%></td>
                <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_LINEA_GESTION_COMPRALST.CANTIDAD_SOLICITADA_MEDIDA)%></td>
                <td>
                    <asp:ImageButton runat="server" ID="ibBorrar" AlternateText="Borrar"
                        CommandArgument='<%# String.Format("{0}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_LINEA_GESTION_COMPRALST.ID_LINEA_GESTION_COMPRA))%>' OnClick="ibBorrar_Click"
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
