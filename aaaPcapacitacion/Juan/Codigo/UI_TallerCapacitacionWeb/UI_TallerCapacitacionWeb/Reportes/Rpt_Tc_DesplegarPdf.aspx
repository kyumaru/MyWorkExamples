<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/Mp_Base.master" AutoEventWireup="false" CodeFile="Rpt_Tc_DesplegarPdf.aspx.vb" Inherits="Reportes_Rpt_Tc_DesplegarPdf" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContenidoFormulario" Runat="Server">
    <asp:TextBox runat="server" ID="txtIsbn" />  
    
  
<br />

    <asp:Button Text="Mostrar" runat="server" ID="btnMostrar" /> 

</asp:Content>

