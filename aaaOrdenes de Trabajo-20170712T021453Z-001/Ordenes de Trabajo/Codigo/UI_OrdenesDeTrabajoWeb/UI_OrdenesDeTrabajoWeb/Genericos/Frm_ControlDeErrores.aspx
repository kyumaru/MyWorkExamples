<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/Mp_Formulario.master" AutoEventWireup="false" CodeFile="Frm_ControlDeErrores.aspx.vb" Inherits="Genericos_Frm_ControlDeErrores" %>

<%--<%@ Register Src="~/Controles/wuc_MensajePopUp.ascx" TagPrefix="uc1" TagName="wuc_MensajePopUp" %>--%>


<asp:Content ID="Content1" ContentPlaceHolderID="cphFormulario" runat="Server">
    <script type="text/javascript">
        function mostrarMensaje(pvc_Mensaje, pvb_Redireccionar) {
            var vlo_ConfiguracionPopup;

            if (pvb_Redireccionar) {
                vlo_ConfiguracionPopup = {
                    titulo: 'Administración de Errores del Sistema',
                    mensaje: pvc_Mensaje,
                    onClosed: function (e) { $(this).removeAttr('href'); window.location = '<%= ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_RUTA_PAGINA_MOSTRAR_LUEGO_DE_ERROR)%>'; },
                    botones:
                            [
                                {
                                    idControl: "btnAceptar",
                                    textoBoton: "Aceptar",
                                    onClick: function () { cerrarPopup(); window.location = '<%= ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_RUTA_PAGINA_MOSTRAR_LUEGO_DE_ERROR)%>'; }
                                }
                            ]
                };
            } else {
                vlo_ConfiguracionPopup = {
                    titulo: 'Administración de Errores del Sistema',
                    mensaje: pvc_Mensaje,
                    botones:
                        [
                            {
                                idControl: "btnAceptar",
                                textoBoton: "Aceptar",
                                onClick: function () { cerrarPopup(); }
                            }
                        ]                                
                };
            }

            $('#arErrorDelSistema').popup(vlo_ConfiguracionPopup);

            window.location = '#arErrorDelSistema';
        }
    </script>
    <article id="Encabezado" class="tituloSeccion">
        Información de Errores del Sistema
    </article>

    <article id="Cuerpo" class="formulario">
        <br />
        Se ha producido un error en la aplicación.
        <br />
        <br />
        Por favor intente nuevamente en unos minutos, si el problema persiste
        <asp:LinkButton ID="btnNotificarError" runat="server" Text="notifique al administrador del sistema"></asp:LinkButton>.
        <br />
        <br />
    </article>
    <article id="DetalleTecnicoError">
        <asp:Label runat="server" ID="lblError" EnableViewState="false" Visible="false"></asp:Label>
    </article>
    <article id="arErrorDelSistema"></article>
</asp:Content>
