<%@ Page Title="Usuarios" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FormUsuario.aspx.cs" Inherits="Servicios_Reservados_2.FormUsuario" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
      <link rel="stylesheet" href="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.4/css/bootstrap.min.css">
  <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.3/jquery.min.js"></script>
  <script src="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.4/js/bootstrap.min.js"></script>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

             <a href="#">
                <div id="alertAlerta" class="alert alert-danger fade in" runat="server" hidden="hidden">
                    <button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
                    <strong>
                        <asp:Label ID="labelTipoAlerta" runat="server" Text="Alerta! "></asp:Label></strong><asp:Label ID="labelAlerta" runat="server" Text="Mensaje de alerta"></asp:Label>
                </div>
            </a>

            <legend>
                <h2>Administración de usuarios</h2>
            </legend>


            <div class="well bs-component">
                <legend style="color: #7BC143">Agregar</legend>
                <table>
                    <tr>
                        <td>
                            <input type="button" class="btn btn-Naranja" value="Usuario" runat="server" onserverclick="clickAgregar" />
                        </td>
                    </tr>
                </table>
            </div>
            <div class="well bs-component">
                <legend style="color: #7BC143">Filtro de usuarios</legend>
                <asp:Panel runat="server" DefaultButton="BotonBuscar">
                    <table>
                        <tr>
                            <td style="width: 10%;">Estación:</td>
                            <td>
                                <select style="width: 176px" id="cbxEstacion" runat="server"></select>
                            </td>
                            <td>Username:</td>
                            <td>
                                <input class="textbox" style="width: 200px" id="txtUsername" runat="server" />
                            </td>
                            <td>Nombre:</td>
                            <td>
                                <input class="textbox" style="width: 200px" id="txtNombre" runat="server" />
                            </td>
                            <td>
                                <asp:Button Text="Buscar" class="btn btn-success" ID="BotonBuscar" OnClick="clickBuscar" runat="server" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>

                <legend style="color: #7BC143">Listado de usuarios</legend>

                <asp:GridView ID="GridUsuarios" runat="server" Class="Gridcontenedor" AllowSorting="true" AllowPaging="true" PageSize="20" OnPageIndexChanging="PageIndexChanging">
                    <AlternatingRowStyle BorderStyle="None" />
                    <HeaderStyle Font-Size="1.3em" />
                    <SelectedRowStyle BackColor="#7BC143"
                        ForeColor="Black"
                        Font-Bold="true" BorderStyle="Dotted" BorderWidth="1px" />
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="btnConsultar" CommandName="consultar" OnClick="clickConsultar" runat="server" class="btn btn-default" ToolTip="Consultar"><i class="glyphicon glyphicon-search"></i></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="btnModificar" runat="server" CommandName="modificar" class="btn btn-default" OnClick="clickModificar" ToolTip="Editar"><i class="glyphicon glyphicon-edit"></i></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>

                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="btnCancelar" runat="server" CommandName="cancelar" class="btn btn-default" ToolTip="Desactivar" OnClick="seleccionarDesactivar"><i  class="glyphicon glyphicon-remove"></i></asp:LinkButton>
                                <!-- Modal -->
                                <div class="modal fade" id="modalDesactivar" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
                                    <div class="modal-dialog" role="document">
                                        <div class="modal-content">
                                            <div class="modal-header">
                                                <h4 class="modal-title" id="myModalLabel">Desactivar usuario</h4>
                                            </div>
                                            <div class="modal-body">
                                                <p>¿Desea desactivar este usuario?</p>
                                            </div>
                                            <div class="modal-footer">
                                                <button type="button" class="btn btn-success" runat="server" onserverclick="clickDesactivar">Aceptar</button>
                                                <button type="button" class="btn btn-danger" data-dismiss="modal">Cancelar</button>                                                
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>

                </asp:GridView>
            </div>

        </ContentTemplate>

    </asp:UpdatePanel>

</asp:Content>
