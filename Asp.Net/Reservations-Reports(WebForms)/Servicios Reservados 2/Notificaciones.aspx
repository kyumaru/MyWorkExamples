<%@ Page Title="Notificaciones -servicios Reservados" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Notificaciones.aspx.cs" Inherits="Servicios_Reservados_2.Notificaciones" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h1>
    Notificaciones del día
</h1>
     <div class="well">
        <asp:GridView ID="GridNotificaciones" Class="Gridcontenedor" runat="server"  Width="80%">

        </asp:GridView>
            <input type="button" value="cancelar" class="btn btn-danger" />
    </div>
</asp:Content>
