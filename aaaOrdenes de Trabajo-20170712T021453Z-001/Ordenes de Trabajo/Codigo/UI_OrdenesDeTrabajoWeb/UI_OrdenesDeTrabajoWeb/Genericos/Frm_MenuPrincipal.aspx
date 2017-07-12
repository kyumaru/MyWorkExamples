<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/Mp_Formulario.master" AutoEventWireup="false" CodeFile="Frm_MenuPrincipal.aspx.vb" Inherits="Genericos_Frm_MenuPrincipal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphFormulario" Runat="Server">
    <article style="margin: 0 auto; text-align:center; vertical-align: central">
        <img src="<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.ESPECIAL, AdministradorRecursos.COLOR_IMAGEN.ESPECIAL, "UCR_Logo_Celeste.gif")%>" height="227" width="592" >
    </article>
</asp:Content>
