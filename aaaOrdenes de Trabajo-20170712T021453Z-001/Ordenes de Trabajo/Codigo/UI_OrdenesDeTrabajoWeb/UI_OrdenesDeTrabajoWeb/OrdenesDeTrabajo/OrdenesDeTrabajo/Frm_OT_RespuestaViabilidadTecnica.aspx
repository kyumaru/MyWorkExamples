<%@ Page Language="VB" MasterPageFile="~/MasterPage/Mp_Formulario.master" AutoEventWireup="false" CodeFile="Frm_OT_RespuestaViabilidadTecnica.aspx.vb" Inherits="OrdenesDeTrabajo_Frm_OT_RespuestaViabilidadTecnica" %>

<%@ Register TagPrefix="wuc" TagName="InformacionGeneral" Src="~/ControlesDeUsuario/Wuc_OT_InformacionGeneral.ascx" %>
<%@ Register TagPrefix="wuc" Namespace="Utilerias.WebForms.Controles" Assembly="Utilerias.WebForms.Controles" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content runat="server" ContentPlaceHolderID="cphFormulario" ID="Content1">

    <header>
        <h2>Respuesta al informe de Viabilidad Técnica
        </h2>
    </header>

    <wuc:InformacionGeneral ID="ctrl_InfoGeneral" runat="server" />

    <article class="formulario">
        <article class="tituloSeccion">Respuesta</article>

        <article class="formulario sinBorde">
            &nbsp;&nbsp;&nbsp;
            <asp:Label runat="server" ID="lblDiasRestantes"></asp:Label>
            <asp:Label runat="server" ID="lblInformacion" Text="*Una vez vencido el plazo de la Orden de Trabajo se dará por Liquidada"></asp:Label>

            <br />
            <br />

            <table>
                <tr>
                    <th>Archivo Jefatura</th>
                    <td>
                        <asp:LinkButton runat="server" ID="lnkArchivo"
                            Style="text-decoration: underline; color: blue;"
                            OnCommand="lnkArchivo_Command"></asp:LinkButton>
                    </td>
                </tr>
            </table>

            <br />
            <br />
            <article class="tituloSeccion">Dispone del presupuesto para ejecutar el proyecto:</article>
            <asp:RadioButtonList ID="rbtnPresupuesto" runat="server">
                <asp:ListItem Value="1">Sí</asp:ListItem>
                <asp:ListItem Value="0">No</asp:ListItem>
            </asp:RadioButtonList>
            <asp:RequiredFieldValidator runat="server" ValidationGroup="GYF" ID="rfvviabilidad" ControlToValidate="rbtnPresupuesto" Display="Dynamic" ErrorMessage="Debe indicar si se dispone de presupuesto o no">&nbsp;</asp:RequiredFieldValidator>

            <article class="tituloSeccion">Adjunte su oficio de respuesta:</article>
            <asp:FileUpload Width="59%" runat="server" ID="ifInfo" onchange="validaArchivo()" />
            <img runat="server" id="imgExtensiones" data-tipo="tooltipExtensiones" class="tooltip" />
            <asp:RequiredFieldValidator runat="server" ID="rfvIfInfo" ControlToValidate="ifInfo" ValidationGroup="Archivos" Display="Dynamic" ErrorMessage="Agregue un documento.">&nbsp;</asp:RequiredFieldValidator>
        </article>

        <article class="areaBotones">
            <asp:Button runat="server" ValidationGroup="Modificar" ID="btnGuardarYFinalizar" OnClick="btnGuardarYFinalizar_Click" Text="Guardar y Enviar" />
            <input type="button" value="Regresar" id="btnRegresar" />
        </article>

        <article id="arPopupDelFormulario"></article>
    </article>

    <script type="text/javascript">
        $(document).ready(function () {
            $('[data-tipo="tooltipExtensiones"]').attr('src', '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Icono_Ayuda.png")%>');


            $('#btnRegresar').click(function () {
                regresarAlListado();
            });
        });

        function regresarAlListado() {
            window.location = 'Lst_OT_OrdenTrabajo.aspx';
        };

        function validaArchivo() {
            var vlo_InputArchivo = document.getElementById('<%=ifInfo.ClientID%>');
            var vlc_NombreArchivo = vlo_InputArchivo.value;
            var vlc_NombreCorto = " ";
            var vlc_Limiter = '\\';
            var vlc_Extension = '';
            var vln_Indice = 0;
            var vln_IndiceExtension = 0;
            var vlc_LimiterExtension = '.';
            var vln_limiteTamArchivo = <%=Me.TamanoArchivo%>;
            var vln_TamArchivo;
            var vln_TamBytes = vln_limiteTamArchivo * <%=Utilerias.OrdenesDeTrabajo.Constantes.TAMANNO_BYTES_A_MEGAS%>;
            var vlc_ExtencionesPerimtidas;
            var vlc_Llaves;
           
            var vln_TamanoNombre = <%=Utilerias.OrdenesDeTrabajo.Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.NOMBRE_ARCHIVO_BD_TAMANO%>;
          
            if(vlo_InputArchivo.value != ''){
                
                vln_Indice =  vlc_NombreArchivo.lastIndexOf(vlc_Limiter) + 1;
                vlc_NombreCorto = vlc_NombreArchivo.substr(vln_Indice, vlc_NombreArchivo.length - vln_Indice);
                vln_IndiceExtension =  vlc_NombreArchivo.lastIndexOf(vlc_LimiterExtension) + 1;
                vlc_Extension = vlc_NombreArchivo.substr(vln_IndiceExtension, vlc_NombreArchivo.length);            
               
                vlc_ExtencionesPerimtidas = '<%=Me.ExtensionesArchivo%>';
                vlc_Llaves = vlc_ExtencionesPerimtidas.split(",");
                
                if(vlc_Llaves.indexOf(vlc_Extension.toUpperCase()) == -1){
                    mostrarAlertaError('No es una extensión permitida','');
                    vlo_InputArchivo.value = "";
                    return false;
                }
                
                if (vlc_NombreCorto.length > vln_TamanoNombre){
                    mostrarAlertaError('El nombre del archivo es demasiado largo','');
                    vlo_InputArchivo.value = "";
                    return false;
                }
                
                vln_TamArchivo = vlo_InputArchivo.files[0].size;
                if (vln_TamArchivo > vln_TamBytes){
                    mostrarAlertaError('El tamaño del archivo excede el máximo permitido.','');
                    vlo_InputArchivo.value = "";
                    return false;
                }

                return true;
            }
            return false;
        };

        function mostrarPopupRegistroExitoso() {
            deshabilitarControl('#<%=btnGuardarYFinalizar.ClientID%>');
            $('.formulario').attr('disabled', 'disabled');

            mostrarAlerta(
            '#arPopupDelFormulario',
            {
                mensaje: 'Se ha guardado los datos indicados para la evaluación de tiempo',
                tipo: "exito",
                transparencia: 0.9,
                posicion: 'center',
                onClosed: function () { regresarAlListado(); }
            });
        };

    </script>

</asp:Content>
