<%@ Page Language="C#" Title="Servicio para empleados" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="FormEmpleadoReserva.aspx.cs" Inherits="Servicios_Reservados_2.FormEmpleadoReserva" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <link rel="stylesheet" href="Content/EmpleadoReserva.css" />
    <nav>
        <ul>
            <li class="item-navegacion"><a href="Default.aspx" title="Página principal"><i class="glyphicon glyphicon-home"></i></a></li>
            <li class="item-navegacion"><a href="FormReservaciones.aspx" title="Reservaciones">Reservaciones</a></li>
            <li class="item-navegacion"><a href="FormEmpleado.aspx" title="Empleados" class="seleccionado">Empleados</a></li>
            <li class="item-navegacion">Notificaciones <span class="notificacion" id="contador" runat="server">0</span></li>
            <li class="item-navegacion"><a href="FormReportesComedor.aspx" title="Reportes">Reportes</a></li>

        </ul>
    </nav>
    <legend>
        <h2>Servicio para empleados</h2>
    </legend>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
    <div class="well bs-component">
             <legend style="color: #7BC143">Información del empleado</legend>
            Nombre:
                <input id="txtNombre" value="{Nombre No recuperado}" runat="server" />
            Apellido:<input id="txtApellido" value="{Apellido No recuperado}" runat="server" />
    </div>

    <div class="well bs-component">
            <legend style="color: #7BC143">Agregar Servicios</legend>
            <table>
                <tr>
                    <td>
                        <input type="button" class="btn btn-Naranja" value="Comida Regular" runat="server" onserverclick="btnAgregarCR_Click" />
                    </td>
                    <td>
                        <input type="button" class="btn btn-Naranja" value="Comida de Campo" runat="server" onserverclick="btnAgregarCC_Click" />
                    </td>
                </tr>
            </table>        
    </div>



            <div class="well bs-component">
                <legend style="color: #7BC143">Listado de servicios</legend>

                    <asp:GridView ID="GridComidasReservadas" Class="Gridcontenedor" runat="server" AllowPaging="true" AllowSorting="true" OnPageIndexChanging="GridViewReservaciones_PageIndexChanging" Width="100%">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnActivarTiquete" runat="server" class="btn btn-default" OnClick="clickActivarTiquetes" ToolTip="Activar tiquetes"><i  class="glyphicon glyphicon-tags"></i></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnConsultar" OnClick="btnVer_Click" runat="server" class="btn btn-default" ToolTip="Consultar"><i class="glyphicon glyphicon-search"></i></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnModificar" runat="server" class="btn btn-default" OnClick="btnEditar_Click" ToolTip="Editar"><i class="glyphicon glyphicon-edit"></i></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnCancelar" runat="server" class="btn btn-default" OnClick="btnCancelar_Click" ToolTip="Anular"><i  class="glyphicon glyphicon-remove"></i></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <SelectedRowStyle BackColor="#7BC143" />
                    </asp:GridView>
            </div>
           
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
