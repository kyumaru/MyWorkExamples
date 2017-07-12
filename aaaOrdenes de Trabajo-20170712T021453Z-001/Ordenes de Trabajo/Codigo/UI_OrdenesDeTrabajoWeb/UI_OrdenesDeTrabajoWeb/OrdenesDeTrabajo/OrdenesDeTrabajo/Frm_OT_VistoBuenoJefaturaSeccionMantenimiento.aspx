<%@ Page Title="" ValidateRequest="false" Language="VB" MasterPageFile="~/MasterPage/Mp_Formulario.master" AutoEventWireup="false" CodeFile="Frm_OT_VistoBuenoJefaturaSeccionMantenimiento.aspx.vb" Inherits="OrdenesDeTrabajo_Frm_OT_VistoBuenoJefaturaSeccionMantenimiento" %>

<%@ Register TagPrefix="wuc" TagName="InformacionGeneral" Src="~/ControlesDeUsuario/Wuc_OT_InformacionGeneral.ascx" %>
<%@ Register TagPrefix="wuc" Namespace="Utilerias.WebForms.Controles" Assembly="Utilerias.WebForms.Controles" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphFormulario" runat="Server">
    <script type="text/javascript" src="<%=AdministradorRecursos.ObtenerRutaScript("nicEdit.js")%>"></script>
    <header>
        <h2>
            <asp:Label runat="server" ID="lblAccion" Text="Visto bueno de la jefatura Sección de Mantenimiento"></asp:Label>
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
        </table>
    </article>
    <br />

    <article class="tituloSeccion">
        Colaboradores
    </article>
    <article data-grupo="Listado" class="listado sinBorde">
        <asp:Repeater runat="server" ID="rpColaboradores">
            <HeaderTemplate>
                <table>
                    <tr>
                        <th>
                            <asp:Label runat="server" ID="lnkIdentificacion" Text="Identificación"></asp:Label>
                        </th>
                        <th>
                            <asp:Label runat="server" ID="lnkNombre" Text="Nombre"></asp:Label>
                        </th>
                        <th>
                            <asp:Label runat="server" ID="lnkArea" Text="Área"></asp:Label>
                        </th>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr class="lineaDelListado">
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.CEDULA)%></td>
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.NOMBRE_EMPLEADO)%></td>
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.DESC_AREA)%></td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
    </article>

    <br />
    <br />

    <article class="formulario">
        <table>
            <tr>
                <th>Indicador de Viabilidad</th>
                <td>
                    <asp:Label runat="server" ID="lblIndicadorViabilidad" data-tipocontrol="etiqueta"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>Estimación Presupuestaria</th>
                <td>
                    <asp:Label runat="server" ID="lblEstimacionPresup" data-tipocontrol="etiqueta"></asp:Label>
                </td>
            </tr>
            <tr>

                <th style="width: 80px">Detalle</th>
                <td colspan="3">
                    <textarea style="height: 300px; width: 100%" data-tipocontrol="texto" id="txtDetalle"></textarea>
                    <br />
                    <asp:HiddenField ID="hdnNicEdit" runat="server" />

                </td>
            </tr>
        </table>
    </article>
       <article class="areaBotones">
        <asp:Button runat="server" ID="btnActualizar" Text="Actualizar Detalle"  OnClientClick="javascript: cambioTxt()"  />
    </article>

    <br />
    <br />
    <article class="tituloSeccion">
        Documentos Adjuntos
    </article>
    <article class="listado sinBorde">
        <asp:Repeater runat="server" ID="rpAdjunto">
            <HeaderTemplate>
                <table>
                    <tr>
                        <th>Archivo</th>
                        <th>Descripción</th>
                        <th>Tipo</th>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr class="lineaDelListado">
                    <td>
                        <asp:LinkButton runat="server" ID="lnkArchivo"
                            CommandArgument='<%#Container.ItemIndex%>'
                            Style="text-decoration: underline; color: blue;"
                            OnCommand="lnkArchivo_Command"
                            Text="<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ADJUNTO_ORDEN_TRABAJOLST.NOMBRE_ARCHIVO)%>"></asp:LinkButton>
                    </td>
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ADJUNTO_ORDEN_TRABAJOLST.NOMBRE_ARCHIVO)%></td>
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ADJUNTO_ORDEN_TRABAJOLST.DESC_TIPO_DOCUMENTO)%></td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
    </article>

    <br />

    <article class="tituloSeccion">
        Revisión
    </article>

    <article class="formulario">
        <table>
            <tr>
                <th></th>
                <td>
                    <asp:RadioButton ID="rbtAprobada" runat="server" Text="Aprobar" GroupName="Condicion" AutoPostBack="true" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:RadioButton ID="rbtDevuelta" runat="server" Text="Devolver" GroupName="Condicion" AutoPostBack="true" />
                </td>
            </tr>
            <tr>
                <th>Observaciones</th>
                <td>
                    <asp:TextBox Width="59%" runat="server" ID="txtJustificacion" data-tipocontrol="texto" TextMode="MultiLine" Rows="5"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ID="rfvTxtJustificacion" Enabled="false" ControlToValidate="txtJustificacion" Display="Dynamic" ErrorMessage="Las Observaciones son Obligatorias." ValidationGroup="Aceptar"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr runat="server" id="trOficio">
                <th>Oficio</th>
                <td>
                    <asp:FileUpload Width="50%" runat="server" ID="ifArchivo" onchange="validaArchivo()"  />
                    <asp:RequiredFieldValidator runat="server" ID="rfvIfArchivo" ControlToValidate="ifArchivo" Enabled="false" ValidationGroup="Aceptar" Display="Dynamic" ErrorMessage="Agregue un oficio.">&nbsp;</asp:RequiredFieldValidator>
                    <img runat="server" ID="imgExtensiones" data-tipo="tooltipExtensiones" class="tooltip"/>    
                </td>
            </tr>
            <tr runat="server" id="trTiempoRespuesta">
                <th>Tiempo para Respuesta</th>
                <td>
                    <asp:TextBox runat="server" ID="txtTiempoRespuesta" data-tipocontrol="texto" Width="20%"></asp:TextBox>
                    <ajax:FilteredTextBoxExtender ID="ftbTxtTiempoRespuesta" runat="server" TargetControlID="txtTiempoRespuesta" FilterMode="ValidChars" FilterType="Numbers"></ajax:FilteredTextBoxExtender>
                    <asp:DropDownList Width="30%" runat="server" ID="ddlUnidadTiempo" data-tipocontrol="combo" AppendDataBoundItems="true"></asp:DropDownList>
                    <asp:RequiredFieldValidator runat="server" ID="rfvTxtTiempoRespuesta" ControlToValidate="txtTiempoRespuesta" Display="Dynamic" ErrorMessage="El Tiempo es Obligatorio." ValidationGroup="Aceptar"></asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator runat="server" ID="rfvDdlUnidadTiempo" ControlToValidate="ddlUnidadTiempo" Display="Dynamic" ErrorMessage="Unidad Tiempo es Obligatoria." ValidationGroup="Aceptar"></asp:RequiredFieldValidator>
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

        function GoDown() {
            window.scrollTo(0, document.body.scrollHeight);
        };
        
        function cambioTxt() {
            $("#<%=hdnNicEdit.ClientID %>").val($(".nicEdit-main").html());
         };

        function validaArchivo() {
            var vlo_InputArchivo = document.getElementById('<%=ifArchivo.ClientID%>');
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
               // alert(vln_TamArchivo);
                if (vln_TamArchivo > vln_TamBytes){
                    mostrarAlertaError('El tamaño del archivo excede el máximo permitido.','');
                    vlo_InputArchivo.value = "";
                    return false;
                }

                return true;
            }
            return false;
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
                mensaje: 'Se ha actualizado la información de la viabilidad.',
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
        });

        function cambioTxt() {
            $("#<%=hdnNicEdit.ClientID %>").val($(".nicEdit-main").html());
        };

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

        /*Mascara para formato de montos*/
        function mascaraMoneda(pvo_CampoReferencia) {
            vgo_CampoReferencia = pvo_CampoReferencia;
            setTimeout("ejecutarMascara()", 1);
        };

        function ejecutarMascara() {
            vgo_CampoReferencia.value = convertirCadena(vgo_CampoReferencia.value);
        };

        function convertirCadena(pvc_Cadena) {
            pvc_Cadena = pvc_Cadena.replace(/([^0-9\.]+)/g, '');
            pvc_Cadena = pvc_Cadena.replace(/^[\.]/, '');
            pvc_Cadena = pvc_Cadena.replace(/[\.][\.]/g, '');
            pvc_Cadena = pvc_Cadena.replace(/\.(\d)(\d)(\d)/g, '.$1$2');
            pvc_Cadena = pvc_Cadena.replace(/\.(\d{1,2})\./g, '.$1');
            pvc_Cadena = pvc_Cadena.toString().split('').reverse().join('').replace(/(\d{3})/g, '$1,');
            pvc_Cadena = pvc_Cadena.split('').reverse().join('').replace(/^[\,]/, '');
            return pvc_Cadena;
        };
        /* Fin Mascara para formato de montos */

        $(document).ready(function () {

            $('[data-tipo="tooltipExtensiones"]').attr('src', '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Icono_Ayuda.png")%>');

            $('#btnCancelar').click(function () {
                regresarAlListado();
            });

        });

        function actualizarContenidoBloqueado() {
            vgo_NicEditorOferente = new nicEditor({maxHeight : 300}).panelInstance('txtDetalle');
            nicEditors.findEditor('txtDetalle').setContent($("#<%=hdnNicEdit.ClientID %>").val());

            document.getElementsByClassName('nicEdit-main')[1].removeAttribute('contenteditable');
            document.getElementsByClassName('nicEdit-panelContain')[1].style.visibility = "hidden";
            
            document.getElementsByClassName('nicEdit-main')[1].parentNode.style.borderWidth = "1px";
            document.getElementsByClassName('nicEdit-main')[1].parentNode.style.borderStyle = "solid";
            document.getElementsByClassName('nicEdit-main')[1].parentNode.style.borderStyle = "solid";
            document.getElementsByClassName('nicEdit-main')[1].parentNode.style.borderColor = "rgb(204, 204, 204)";
        }

        function CopiarTextoAcuerdo() {
            try {
                var copyTextarea = document.querySelector('.textoACopiar');
                copyTextarea.select();
                document.execCommand('copy');
            }
            catch (err) {  }
        }
        
        function regresarAlListado() {
            window.location = 'Lst_OT_VistoBuenoJefaturaSeccionMantenimiento.aspx';
        };

    </script>

</asp:Content>



