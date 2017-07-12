<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/MasterPage/Mp_Formulario.master" CodeFile="Frm_OT_VerCorreos.aspx.vb" Inherits="Notificaciones_Frm_OT_VerCorreos" %>

<%@ Register TagPrefix="wuc" Namespace="Utilerias.WebForms.Controles" Assembly="Utilerias.WebForms.Controles" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphFormulario" runat="Server">
    <header>
        <h2>Contenido de la notificación
        </h2>
    </header>

    <article class="tituloSeccion">
        <asp:Label runat="server" ID="lblAccion" Text="Contenido de la notificación"></asp:Label>
    </article>

    <article class="formulario">
        <table>

            <tr>
                <td style="width: 100%">
                    <div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: auto; text-align:left" >

                        <br />
                        <asp:Label ID="lblContenido" runat="server" Text=""></asp:Label>
                    </div>
                </td>
            </tr>



        </table>
    </article>

    <article class="areaBotones">
        <input id="btnCancelar" class="tooltip" title="Regresar al listado de consulta de notificaciones" onclick="javascript: regresarAlListado()" type="button" value="Regresar" />
    </article>
    
    <article id="arPopupDelFormulario"></article>
    <article id="arAlertasDelFormulario"></article>

    <script type="text/javascript">

        $(document).ready(function () {

            habilitarTooltipGenerico();
        });



        function mostrarAlertaError(pvc_Mensaje) {
            mostrarAlerta(
                '#arAlertasDelFormulario',
                {
                    mensaje: pvc_Mensaje,
                    tipo: 'peligro',
                    transparencia: 1,
                    posicion: 'top',
                    permiteCerrar: true
                }
            );
        }

        function regresarAlListado() {
            window.location = 'Lst_OT_ConsultaCorreos.aspx';
        }


    </script>

</asp:Content>