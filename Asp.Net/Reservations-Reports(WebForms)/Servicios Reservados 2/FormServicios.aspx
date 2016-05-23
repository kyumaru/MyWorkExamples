<%@ Page Title="Servicio para reservaciones" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FormServicios.aspx.cs" Inherits="Servicios_Reservados_2.FormServicios" %>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="stylesheet" href="Content/Servicios.css" />
    <nav>
        <ul>
            <li class="item-navegacion"><a href="FormReservaciones.aspx" class="seleccionado">Reservaciones</a></li>
            <li class="item-navegacion"><a href="FormEmpleado.aspx" >Empleados</a></li>
            <li class="item-navegacion">Notificaciones <span class="notificacion">0</span></li>
        </ul>
    </nav>
    <legend>
        <h2>Servicio para Reservaciones</h2>
    </legend>
    <div class="well bs-component">
               
                    <legend style="color: #7BC143">Información de reservación</legend>

                    <table>
                        <tr>
                            <td>Anfitriona:</td>
                            <td>
                                <input id="txtAnfitriona" runat="server" /></td>
                            <td>Estacion:</td>
                            <td>
                                <input id="txtEstacion" runat="server" />
                            </td>
                            <td>Solicitante:</td>
                            <td>
                                <input id="txtNombre" runat="server" /></td>

                        </tr>
                        <tr>
                            <td>Fecha De Entrada:</td>
                            <td>
                                <input id="fechaInicio" runat="server" /></select>
                            </td>
                            <td>Fecha De Salida:</td>
                            <td>
                                <input id="fechaFinal" runat="server" /></select>
                            </td>
                            <td>Numero de PAX:</td>
                            <td>
                                <input id="txtPax" runat="server" /></select>
                            </td>
                        </tr>
                    </table>
               
            </div>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            
            <div class="well bs-component">
               
                    <legend style="color: #7BC143">Agregar Servicios</legend>
                    <table>
                        <tr>
                            <td>
                                <input type="button" class="btn btn-Naranja" value="Comida Extra" runat="server" onserverclick="clickAgregarServicio" /></a></td>
                            <td>
                                <input type="button" class="btn btn-Naranja" value="Comida De Campo" runat="server" onserverclick="cliclAgregarComidaCampo" />
                            </td>
                           
                        </tr>
                    </table>
            </div>
            <div class="well bs-component">
              
                    <a href="">
                        <div id="alertAlerta" class="alert alert-danger fade in" runat="server" hidden="hidden">
                            <button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
                            <strong>
                                <asp:Label ID="labelTipoAlerta" runat="server" Text="Alerta! "></asp:Label></strong><asp:Label ID="labelAlerta" runat="server" Text="Mensaje de alerta"></asp:Label>
                        </div>
                    </a>
                    <legend style="color: #7BC143">Listado de servicios</legend>

                    <div class="well bs-component" style="background-color: white">
                        <table>
                            <tr>
                                <td>
                                    <asp:GridView ID="GridServicios" class="Gridcontenedor" runat="server" BorderColor="#CCCCCC" BorderStyle="Dotted" BorderWidth="2px">
                                        <SelectedRowStyle BackColor="#7BC143" />

                                          <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                      <asp:LinkButton  ID="btnActivarTiquete" CommandName="activarTiquete" runat="server" class="btn btn-default" OnClick="clickActivarTiquetes" ToolTip="Activar tiquetes"><i  class="glyphicon glyphicon-tags"></i></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>

                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                      <asp:LinkButton  ID="btnConsultar"  CommandName="consultar" onClick="clickConsultarServicio" runat="server" class="btn btn-default" ToolTip="Consultar"><i class="glyphicon glyphicon-search"></i></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>

                                            <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                      <asp:LinkButton ID="btnModificar" runat="server" CommandName="modificar" class="btn btn-default" OnClick="modificarServicio" ToolTip="Editar"><i class="glyphicon glyphicon-edit"></i></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>

                                            <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                      <asp:LinkButton  ID="btnCancelar" runat="server" CommandName="cancelar" class="btn btn-default" OnClick="clickEliminarServicio" ToolTip="Anular"><i  class="glyphicon glyphicon-remove"></i></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>

                                    </asp:GridView>

                        </table>
                    </div>

            </div>


        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
