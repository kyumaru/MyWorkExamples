<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/Mp_Base.master" AutoEventWireup="false" CodeFile="Frm_Login.aspx.vb" Inherits="Seguridad_Frm_Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContenidoFormulario" runat="Server">
    <article id="advertenciaJavaScript">
        <h3>La configuración actual de su navegador no soporta la ejecución de JavaScript.
        </h3>
        <h3>Para el correcto funcionamiento de la aplicación es necesario que active la ejecución de JavaScript en las opciones de configuración de su navegador.
        </h3>
        <h3>Por favor habilite la ejecución de JavaScript en su navegador e intente ingresar al sitio nuevamente.</h3>
    </article>
    <article id="ControlDeIngresoAlSistema" class="ventanaEmergente">
        <article>
            <h2><%=ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_NOMBRE_DE_LA_APLICACION)%></h2>
            <table>
                <tr>
                    <th>Usuario</th>
                    <td>
                        <asp:TextBox runat="server" ID="txtUsuario"></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" ID="rfvTxtUsuario" ControlToValidate="txtUsuario" Display="Dynamic" ErrorMessage="Debe indicar el Usuario." ForeColor="#9D240C">&nbsp;</asp:RequiredFieldValidator>
                         <img id="imgTooltipTxtUsuario" src="<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Icono_Ayuda.png")%>" title="Indique el nombre de usuario del portal universitario, no debe incluir @ucr.ac.cr." />
                    </td>
                </tr>
                <tr>
                    <th>Clave</th>
                    <td>
                        <asp:TextBox runat="server" ID="txtClave" TextMode="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" ID="rfvTxtClave" ControlToValidate="txtClave" Display="Dynamic" ErrorMessage="Debe indicar la clave." ForeColor="#9D240C">&nbsp;</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:ValidationSummary runat="server" ID="vsDetalleErrores" DisplayMode="BulletList" />
                        <asp:BulletedList runat="server" ID="blError"></asp:BulletedList>
                    </td>
                </tr>
            </table>
            <article class="areaBotonesInvertido">
                <asp:LinkButton runat="server" ID="lnkIngresarAlSistema" OnClientClick="javascript:return validarIngreso();">Ingresar</asp:LinkButton>
            </article>
        </article>
    </article>
    <script type="text/javascript">
        $(document).ready(function () {
            ocultarControl('#advertenciaJavaScript');
            habilitarTooltipPorControl('#imgTooltipTxtUsuario');
            configurarBotonPorDefecto('<%=lnkIngresarAlSistema.ClientID%>');            
            window.location = "#ControlDeIngresoAlSistema";
        })

        function validarIngreso() {
            Page_ClientValidate();
            marcarControlesInvalidos();
            return Page_IsValid;
        }
    </script>
</asp:Content>
