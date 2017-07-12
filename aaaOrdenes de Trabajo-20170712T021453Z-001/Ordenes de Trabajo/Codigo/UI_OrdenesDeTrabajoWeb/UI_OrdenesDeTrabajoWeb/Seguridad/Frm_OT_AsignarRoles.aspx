<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/Mp_Formulario.master" AutoEventWireup="false" CodeFile="Frm_OT_AsignarRoles.aspx.vb" Inherits="Seguridad_Frm_OT_AsignarRoles" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphFormulario" runat="Server">

    <header>
        <h2>Asignación de Roles por Usuario</h2>
    </header>

    <article class="tituloSeccion">
        Roles
    </article>

    <article class="formulario">
        <table>
            <tr>
                <th>Usuario</th>
                <td>
                    <asp:Label runat="server" ID="lblUsuario"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>Roles</th>
                <td>
                    <asp:UpdatePanel runat="server" ID="upRoles" UpdateMode="Always">
                        <ContentTemplate>
                            <asp:CheckBoxList runat="server" ID="chkLstRoles" AutoPostBack="true"></asp:CheckBoxList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
    </article>

     <article class="areaBotones">
        <asp:LinkButton ID="lnkVolver" runat="server" Text="Regresar" PostBackUrl="~/Seguridad/Frm_OT_RoleUsuario.aspx" CausesValidation="false"></asp:LinkButton>
    </article>

</asp:Content>

