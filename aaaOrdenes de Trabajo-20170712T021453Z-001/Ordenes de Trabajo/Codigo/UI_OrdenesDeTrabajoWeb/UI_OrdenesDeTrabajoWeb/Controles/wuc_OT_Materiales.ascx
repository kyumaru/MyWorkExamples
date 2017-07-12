<%@ Control Language="VB" AutoEventWireup="false" CodeFile="wuc_OT_Materiales.ascx.vb" Inherits="Controles_wuc_OT_Materiales" %>

<%@ Register TagPrefix="wuc" Namespace="Utilerias.WebForms.Controles" Assembly="Utilerias.WebForms.Controles" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<article class="formulario">
    <table>
        <tr runat="server" id="trAlmacenBodega">
            <th style="text-align: left; width: 15%">Almacén/ Bodega</th>
            <td colspan="3">
                <asp:DropDownList runat="server" ID="ddlAlmacen"></asp:DropDownList></td>
        </tr>
        <tr runat="server" id="trBuscarFiltro">
            <th style="text-align: left;">Filtrar Partidas</th>
            <td colspan="3">
                <asp:TextBox runat="server" ID="txtFiltroPart"></asp:TextBox>
                &nbsp;&nbsp;&nbsp;
                     <asp:Button runat="server" ID="btnBuscarPart" Text="Filtrar Partidas" />
                &nbsp;&nbsp;&nbsp;
                     <asp:Button runat="server" ID="btnLimpiarFiltroPart" Text="Limpiar Filtro" />

            </td>
        </tr>
        <tr runat="server" id="trPartidaPresupuestaria">
            <th style="text-align: left;">Partida Presupuestaria</th>
            <td colspan="3">
                <asp:DropDownList runat="server" Width="100%" ID="ddlPartidaPresupuestaria"></asp:DropDownList></td>
        </tr>
        <tr>
            <th style="text-align: left;">Código</th>
            <td>
                <asp:TextBox runat="server" ID="txtCodigo"></asp:TextBox>
                <ajax:FilteredTextBoxExtender ID="ftbTxtCodigo" runat="server" TargetControlID="txtCodigo" FilterType="Numbers"></ajax:FilteredTextBoxExtender>
            </td>

            <th style="text-align: left;">Categoría</th>
            <td style="text-align: left;">
                <asp:DropDownList runat="server"
                    ID="ddlCategoria" AutoPostBack="true">
                </asp:DropDownList></td>
        </tr>
        <tr>
            <th style="text-align: left;">Descripción</th>
            <td>
                <asp:TextBox runat="server" Width="100%" ID="txtDescripcion"></asp:TextBox></td>
            <th style="text-align: left;">Sub Categoría</th>
            <td>
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                        <asp:DropDownList runat="server" ID="ddlSubcategoria" AppendDataBoundItems="true"></asp:DropDownList>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddlCategoria" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</article>

<article class="areaBotones">
    <asp:Button runat="server" ID="btnBuscar" Text="Buscar" />
    <asp:Button runat="server" ID="btnLimpiar" Text="Limpiar Filtros" />
</article>

<article id="articleTitulo" visible="true" runat="server" class="tituloSeccion">
    Lista de Materiales
</article>
<article data-grupo="Listado" class="listado">
    <div style="height: 200px; overflow: auto;">
        <asp:GridView ID="grdEmpleados" runat="server" AutoGenerateColumns="False"
            EmptyDataText="No existen registros de Materiales con el criterio de búsqueda"
            AllowPaging="True" Width="100%">
            <RowStyle CssClass="lineaDelListado" />
            <AlternatingRowStyle CssClass="lineaDelListado" />
            <Columns>
                <asp:TemplateField HeaderText="Código">
                    <ItemStyle HorizontalAlign="Left" />
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkIdMaterial" runat="server" Text='<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_MATERIALLST.ID_MATERIAL)%>'
                            CommandArgument='<%#String.Format("{0}_{1}_{2}_{3}_{4}_{5}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_MATERIALLST.ID_MATERIAL), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_MATERIALLST.DESCRIPCION),
                            Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_MATERIALLST.ID_CATEGORIA_MATERIAL), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_MATERIALLST.ID_SUBCATEGORIA_MATERIAL),
                            Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_MATERIALLST.COSTO_PROMEDIO), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_MATERIALLST.ID_UNIDAD_MEDIDA))%>'
                            CommandName="Cargar" OnCommand="lnkGrid_Command">
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Descripción" ItemStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_MATERIALLST.DESCRIPCION)%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Disponible" ItemStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_MATERIALLST.CANTIDAD_EXISTENCIA)%>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <PagerStyle HorizontalAlign="Left" />
        </asp:GridView>
    </div>
</article>

<style type="text/css">
    .ventanaEmergente table th {
        vertical-align: middle;
        padding: 5px;
        width: 12%;
    }
</style>
