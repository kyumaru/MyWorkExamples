<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/Mp_Base.master" AutoEventWireup="false" CodeFile="Frm_Logout.aspx.vb" Inherits="Seguridad_Frm_Logout" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="cphContenidoFormulario" runat="Server">
    <article style="text-align: center">
        <h3>Gracias por utilizar <%= ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_NOMBRE_DE_LA_APLICACION).ToString()%>.</h3>
        <h3>Si desea ingresar nuevamente, por favor <a href="Frm_Login.aspx">haga clic aquí</a>.</h3>
    </article>
</asp:Content>
