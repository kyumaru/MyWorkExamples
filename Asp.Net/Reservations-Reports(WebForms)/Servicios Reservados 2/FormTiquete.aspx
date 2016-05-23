<%@ Page Title="Activar tiquetes" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FormTiquete.aspx.cs" Inherits="Servicios_Reservados_2.FormTiquete" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>


<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">


    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <legend>
                <h2>Activar tiquetes</h2>
            </legend>

            <div class="well bs-component">
                <legend style="color: #7BC143">Información del servicio</legend>

                <table>
                    <tr>
                        <td>Anfitriona:</td>
                        <td>
                            <input class="textbox" style="width: 500px" id="anfitriona" runat="server" />
                        </td>
                        <td>Estación:</td>
                        <td>
                            <input class="textbox" style="width: 500px" id="estacion" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>Número de reservación:</td>
                        <td>
                            <input class="textbox" style="width: 500px" id="numero" runat="server" />
                        </td>
                        <td>Solicitante:</td>
                        <td>
                            <input class="textbox" style="width: 500px" id="solicitante" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>Categoria de servicio:</td>
                        <td>
                            <input class="textbox" style="width: 500px" id="categoria" runat="server" />
                        </td>
                        <td>Estado:</td>
                        <td>
                            <input class="textbox" style="width: 500px" id="estado" runat="server" />
                        </td>
                    </tr>
                    </tr>
                                   <tr>
                                       <td>Pax:</td>
                                       <td>
                                           <input class="textbox" style="width: 500px" id="pax" runat="server" />
                                       </td>
                                   </tr>
                </table>
            </div>

            <div class="well bs-component">
                <legend style="color: #7BC143">Información de tiquetes activos</legend>

                <asp:Panel runat="server" DefaultButton="BotonAgregar">
                    <table>
                        <tr>
                            <td>Número:</td>
                            <td>
                                <input class="textbox" id="numTiquete" runat="server" required="required" title="Inserte un número de 4 digitos" pattern="^[0-9]{1,4}$" />
                            </td>
                            <td>
                                <asp:Button Text="Agregar" class="btn btn-success" ID="BotonAgregar" OnClick="clickAgregar" runat="server" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>

                <a href="">
                    <div id="alertAlerta" class="alert alert-danger fade in" runat="server" hidden="hidden">
                        <button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
                        <strong>
                            <asp:Label ID="labelTipoAlerta" runat="server" Text="Alerta! "></asp:Label></strong><asp:Label ID="labelAlerta" runat="server" Text="Mensaje de alerta"></asp:Label>
                    </div>
                </a>

                <asp:GridView ID="GridViewTiquetes" runat="server" Class="Gridcontenedor" AllowSorting="true">
                    <AlternatingRowStyle BorderStyle="None" />
                    <HeaderStyle Font-Size="1.3em" />
                    <SelectedRowStyle BackColor="#7BC143"
                        ForeColor="Black"
                        Font-Bold="true" BorderStyle="Dotted" BorderWidth="1px" />
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="btnDesactivar" runat="server" class="btn btn-default" OnClick="clickQuitar"><i  class="glyphicon glyphicon-remove"></i></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>


            </div>
            <asp:Button href="FormServicios" Text="Aceptar" ID="btnCancelar" class="btn btn-succes" UseSubmitBehavior="false" runat="server" OnClick="clickCancelar" />

        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
