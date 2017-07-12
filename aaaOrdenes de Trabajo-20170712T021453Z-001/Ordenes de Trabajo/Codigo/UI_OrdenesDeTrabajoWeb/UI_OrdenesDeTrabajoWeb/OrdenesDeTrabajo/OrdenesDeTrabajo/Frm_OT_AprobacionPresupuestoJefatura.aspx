<%@ Page Title="" ValidateRequest="false" Language="VB" MasterPageFile="~/MasterPage/Mp_Formulario.master" AutoEventWireup="false" CodeFile="Frm_OT_AprobacionPresupuestoJefatura.aspx.vb" Inherits="OrdenesDeTrabajo_Frm_OT_AprobacionPresupuestoJefatura" %>

<%@ Register TagPrefix="wuc" TagName="InformacionGeneral" Src="~/ControlesDeUsuario/Wuc_OT_InformacionGeneral.ascx" %>
<%@ Register TagPrefix="wuc" Namespace="Utilerias.WebForms.Controles" Assembly="Utilerias.WebForms.Controles" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphFormulario" runat="Server">
    <script type="text/javascript" src="<%=AdministradorRecursos.ObtenerRutaScript("nicEdit.js")%>"></script>
    <header>
        <h2>
            <asp:Label runat="server" ID="lblAccion" Text="Revisión de Aprobación de Presupuesto"></asp:Label>
        </h2>
    </header>

    <article class="tituloSeccion">
        Información General de la OT
    </article>
    <article class="formulario sinBorde">
        <wuc:InformacionGeneral ID="ctrl_InfoGeneral" runat="server" />
    </article>
    <br />

    <article class="formulario">
        <table>
            <tr>
                <th>Encargado del Proyecto</th>
                <td>
                    <asp:Label runat="server" ID="lblEncargadoProyecto"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>Presupuesto Total</th>
                <td>
                    <asp:Label ID="lblPresupuesto" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <th style="width: 80px">Informe de valoración presupuestaria del proyecto</th>
                <td colspan="3">
                    <textarea style="height: 300px; width: 100%" id="txtDetalle"></textarea>
                    <br />
                    <asp:HiddenField ID="hdnNicEdit" runat="server" />
                </td>
            </tr>
        </table>
    </article>
    <br />

    <article class="tituloSeccion">
        Revisión
    </article>

    <article class="formulario">
        <table>
            <tr>
                <th>Condición</th>
                <td>
                    <asp:RadioButton ID="rbtAprobada" runat="server" Text="Aprobar" GroupName="Condicion" AutoPostBack="true" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:RadioButton ID="rbtDevuelta" runat="server" Text="Devolver" GroupName="Condicion" AutoPostBack="true" />
                </td>
            </tr>
            <tr runat="server" id="trJustificacion">
                <th>Observaciones</th>
                <td>
                    <asp:TextBox Width="59%" runat="server" ID="txtJustificacion" data-tipocontrol="texto" TextMode="MultiLine" Rows="5"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ID="rfvTxtJustificacion" Enabled="false" ControlToValidate="txtJustificacion" Display="Dynamic" ErrorMessage="Las Observaciones son Obligatorias." ValidationGroup="Aceptar"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr  runat="server" id="trOficio">
                <th>Oficio</th>
                <td>
                    <asp:FileUpload Width="59%" runat="server" ID="ifInfo" onchange="validaArchivo()" />
                    <img runat="server" ID="imgExtensiones" data-tipo="tooltipExtensiones" class="tooltip"/>
                    <asp:RequiredFieldValidator runat="server" ID="rfvIfInfo" ControlToValidate="ifInfo" ValidationGroup="Aceptar" Display="Dynamic" ErrorMessage="Agregue un documento.">&nbsp;</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr  runat="server" id="trTiempoRespuesta">
                <th>Tiempo para respuesta</th>
                <td>

                    <asp:TextBox runat="server" ID="txtTiempo" Width="9%" data-tipoControl="texto" MaxLength="3"></asp:TextBox>
                    <asp:DropDownList Width="50%" runat="server" ID="ddlUnidadTiempo" AppendDataBoundItems="true" data-tipoControl="combo"></asp:DropDownList>
                    <br />
                    <ajax:FilteredTextBoxExtender ID="ftbTxtTiempo" runat="server" TargetControlID="txtTiempo" FilterMode="ValidChars" FilterType="Numbers"></ajax:FilteredTextBoxExtender>
                    <asp:RequiredFieldValidator runat="server" ValidationGroup="Aceptar" ID="rfvTxtTiempo" ControlToValidate="txtTiempo" Display="Dynamic" ErrorMessage="Debe indicar el tiempo.">&nbsp;</asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator runat="server" ValidationGroup="Aceptar" ID="rfvDdlUnidadTiempo" ControlToValidate="ddlUnidadTiempo" Display="Dynamic" ErrorMessage="La unidad de tiempo es requerida">&nbsp;</asp:RequiredFieldValidator>
                </td>
            </tr>
        </table>
    </article>

    <article class="areaBotones">
        <asp:Button runat="server" ID="btnGuardarEnviar" Text="Guardar y Enviar" ValidationGroup="Aceptar" />
        <input id="btnCancelar" type="button" value="Regresar" />
    </article>

    <article id="arPopupDelFormulario"></article>
    <article id="arAlertasDelFormulario"></article>
    <article id="arpopupConfirmaDeseaBorrar"></article>
    <article id="arPopupGenerico"></article>

    <script type="text/javascript">

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
            var vlc_ExtencionesPermitidas;
            var vlc_Llaves;
           
            var vln_TamanoNombre = <%=Utilerias.OrdenesDeTrabajo.Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.NOMBRE_ARCHIVO_BD_TAMANO%>;
          
            if(vlo_InputArchivo.value != ''){
                
                vln_Indice =  vlc_NombreArchivo.lastIndexOf(vlc_Limiter) + 1;
                vlc_NombreCorto = vlc_NombreArchivo.substr(vln_Indice, vlc_NombreArchivo.length - vln_Indice);
                vln_IndiceExtension =  vlc_NombreArchivo.lastIndexOf(vlc_LimiterExtension) + 1;
                vlc_Extension = vlc_NombreArchivo.substr(vln_IndiceExtension, vlc_NombreArchivo.length);            
               
                vlc_ExtencionesPermitidas = '<%=Me.ExtensionesArchivo%>';
                
                
                vlc_Llaves = vlc_ExtencionesPermitidas.split(","); 
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


        function GoDown() {
            window.scrollTo(0, document.body.scrollHeight);
        };

        function mostrarAlertaLlaveIncorrecta() {
            deshabilitarControl('#<%=btnGuardarEnviar.ClientID%>');
            deshabilitarControl('#btnCancelar');
            $('.formulario').attr('disabled', 'disabled');

            mostrarAlerta(
                '#arAlertasDelFormulario',
                {
                    mensaje: 'El identificador provisto no pertenece a ningun registro del sistema',
                    tipo: "peligro",
                    transparencia: 0.9,
                    posicion: 'center',
                    onClosed: function () { regresarAlListado(); }
                });
        };

        function mostrarAlertaActualizacionExitosa() {
            deshabilitarControl('#<%=btnGuardarEnviar.ClientID%>');
            $('.formulario').attr('disabled', 'disabled');

            mostrarAlerta(
            '#arAlertasDelFormulario',
            {
                mensaje: 'Se ha actualizado la información exitosamente.',
                tipo: "exito",
                transparencia: 0.9,
                posicion: 'center',
                onClosed: function () { regresarAlListado(); }
            });
        };

        function mostrarAlertaError(pvc_Mensaje) {
            mostrarAlerta(
                '#arAlertasDelFormulario',
                {
                    mensaje: pvc_Mensaje,
                    tipo: 'peligro',
                    transparencia: 1,
                    posicion: 'center',
                    permiteCerrar: true
                }
            );
        };

        //Descripcion: Funcion que establece la configuración inicial  del text area enriquesido

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        var area2;
        bkLib.onDomLoaded(function () {

            area2 = new nicEditor({ maxHeight: 300 }).panelInstance('txtDetalle');
            $(".nicEdit-main").html($("#<%=hdnNicEdit.ClientID %>").val());
            document.getElementsByClassName('nicEdit-main')[0].removeAttribute('contenteditable');
            document.getElementsByClassName('nicEdit-panelContain')[0].style.visibility = "hidden";

            document.getElementsByClassName('nicEdit-main')[0].parentNode.style.borderWidth = "1px";
            document.getElementsByClassName('nicEdit-main')[0].parentNode.style.borderStyle = "solid";
            document.getElementsByClassName('nicEdit-main')[0].parentNode.style.borderStyle = "solid";
            document.getElementsByClassName('nicEdit-main')[0].parentNode.style.borderColor = "rgb(204, 204, 204)";
        });

        function limpiarControles() {
            quitarContenidoTemario();
        };

        //Descripcion: Funcion que limpia el text area enriquesido
        function quitarContenidoTemario() {
            area2.removeInstance('txtDetalle');
            document.getElementById('txtDetalle').value = "";
            area2 = new nicEditor({ maxHeight: 300 }).panelInstance('txtDetalle');
        };

        //Descripcion: Funcion que valida el contenido del editor "txtDetalle"

        function validarContenidoNicEditor(source, clientside_arguments) {
            var vlc_CadenaaValidar = nicEditors.findEditor('txtDetalle').getContent().replace(/&nbsp;/g, '').replace(/<br>/g, '').trim();

            if (vlc_CadenaaValidar == '')
                return clientside_arguments.IsValid = false;
            else
                return clientside_arguments.IsValid = true;

        };

        $(document).ready(function () {
            $('[data-tipo="tooltipExtensiones"]').attr('src', '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Icono_Ayuda.png")%>');

            habilitarTooltipGenerico();
            $('#btnCancelar').click(function () {
                regresarAlListado();
            });
        });

        function regresarAlListado() {
            window.location = 'Lst_OT_AprobacionPresupuestoJefatura.aspx';
        };

    </script>

</asp:Content>

