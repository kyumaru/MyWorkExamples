<%@ Control Language="VB" AutoEventWireup="false" CodeFile="wucEmpleadosEU.ascx.vb" Inherits="Controles_wucEmpleadosEU" %>
<article class="tituloSeccion">
    Filtros de Búsqueda para Empleados
</article>

<article class="formulario">
    <table>
        <tr>
            <td>
                <asp:Label ID="lblApellido1" runat="server" Text="Primer Apellido"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblApellido2" runat="server" Text="Segundo Apellido"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblNombre" runat="server" Text="Nombre"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblIdentificacion" runat="server" Text="Identificación"></asp:Label>
            </td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="txtApellido1" runat="server" TabIndex="1"></asp:TextBox>
            </td>
            <td>
                <asp:TextBox ID="txtApellido2" runat="server" TabIndex="2"></asp:TextBox>
            </td>
            <td>
                <asp:TextBox ID="txtNombre" runat="server" TabIndex="3"></asp:TextBox>
            </td>
            <td>
                <asp:TextBox ID="txtIdentificacion" runat="server" TabIndex="4"></asp:TextBox>
            </td>
            <td style="width: 24px; text-align: center; vertical-align: middle">
                <asp:ImageButton ID="ibBuscar" runat="server" ToolTip="Realiza la búsqueda del empleado" TabIndex="5" />
            </td>
            <td style="width: 24px; text-align: center; vertical-align: middle">
                <asp:ImageButton ID="ibLimpiar" runat="server" TabIndex="6" ToolTip="Limpia los valores para una nueva búsqueda" />
            </td>
        </tr>
    </table>
</article>

<article class="listado">
    <asp:GridView ID="grdEmpleados" runat="server" AutoGenerateColumns="False"
        EmptyDataText="No existen registros de Empleados con el criterio de búsqueda"
        AllowPaging="True" Width="100%">
        <RowStyle CssClass="lineaDelListado" />
        <AlternatingRowStyle CssClass="lineaDelListado" />
        <Columns>
            <asp:TemplateField HeaderText="Identificación">
                <ItemStyle HorizontalAlign="Left" />
                <ItemTemplate>
                    <asp:LinkButton ID="lnkNumeroEmpleado" runat="server" Text='<%#Eval("Identificación")%>'
                        CommandArgument='<%#String.Format("{0}_{1}_{2}_{3}_{4}", Eval("NúmeroEmpleado"), Eval("Identificación"), Eval("Nombre"), Eval("PrimerApellido"), Eval("SegundoApellido"))%>' CommandName="Cargar" OnCommand="lnkGrid_Command">
                    </asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Nombre" ItemStyle-HorizontalAlign="Left">
                <ItemTemplate>
                    <%#String.Format("{0} {1} {2}", Eval("PrimerApellido"), Eval("SegundoApellido"), Eval("Nombre"))%>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <PagerStyle HorizontalAlign="Left" />
    </asp:GridView>
</article>

<script type="text/javascript">
    function MostrarMensajeAviso(pvc_Mensaje) {
        alert(pvc_Mensaje);
    }

    function MostrarMensajeInformacion(pvc_Mensaje) {
        alert(pvc_Mensaje);
    }

    function MostrarMensajeError(pvc_Mensaje) {
        alert(pvc_Mensaje);
    }

    function inicializarControl() {

        permutarImagenes('#<%= ibBuscar.ClientID%>',
                           '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Lupa.png")%>',
                            '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Lupa.png")%>',
                            '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Lupa.png")%>'
                        );

        permutarImagenes('#<%= ibLimpiar.ClientID%>',
               '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Equis.png")%>',
                            '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Icono_Equis.png")%>',
                            '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Equis.png")%>'
                        );
    }

    $(document).ready(function () {
        inicializarControl();
        
    });
</script>