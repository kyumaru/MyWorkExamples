<%@ Page Title="Cerrar sesión" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Logout.aspx.cs" Inherits="Gyoza.Account.Logout" Async="true" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <div class="col-lg-5">
        <div id="Alerta" class="alert alert-danger fade in" runat="server" hidden="hidden">
            <button type="button" class="close" data-dismiss="alert" aria-hidden="true"></button>
            <strong>
                <asp:Label ID="labelTipoAlerta" runat="server" Text="Alerta! "></asp:Label></strong><asp:Label ID="labelAlerta" runat="server" Text="Mensaje de alerta"></asp:Label>
        </div>
    </div>

</asp:Content>


