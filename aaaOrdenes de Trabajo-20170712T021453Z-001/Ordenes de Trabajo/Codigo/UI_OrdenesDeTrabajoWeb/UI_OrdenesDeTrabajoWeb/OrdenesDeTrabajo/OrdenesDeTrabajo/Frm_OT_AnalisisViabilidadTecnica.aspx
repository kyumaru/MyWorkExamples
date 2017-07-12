<%@ Page Language="VB" ValidateRequest="false" MaintainScrollPositionOnPostback="true" EnableEventValidation="false" Title="Registro de análisis de viabilidad técnica por profesional" MasterPageFile="~/MasterPage/Mp_Formulario.master" AutoEventWireup="false" CodeFile="Frm_OT_AnalisisViabilidadTecnica.aspx.vb" Inherits="OrdenesDeTrabajo_Frm_OT_AnalisisViabilidadTecnica" %>

<%@ Register TagPrefix="wuc" TagName="InformacionGeneral" Src="~/ControlesDeUsuario/Wuc_OT_InformacionGeneral.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content runat="server" ID="Content1" ContentPlaceHolderID="cphFormulario">
    <script type="text/javascript" src="<%=AdministradorRecursos.ObtenerRutaScript("nicEdit.js")%>"></script>
    <header>
        <h2>Informe para valoración de Viabilidad Técnica de Ordenes de Trabajo
        </h2>
    </header>

    <article class="formulario areaBotones">
        <table>
            <tr>
                <th>Nombre Del Proyecto</th>
                <td>
                    <asp:TextBox runat="server" ID="txtNombreProyecto" Width="59%"></asp:TextBox>
                    <asp:RequiredFieldValidator ValidationGroup="Modificar" runat="server" ID="rfvtxtNombreProyecto" ControlToValidate="txtNombreProyecto" Display="Dynamic" ErrorMessage="Debe indicar el nombre del proyecto">&nbsp;</asp:RequiredFieldValidator>
                    <asp:Button CssClass="areaBotones" ValidationGroup="Modificar" runat="server" ID="btnModificar" Text="Actualizar" />
                </td>
            </tr>
        </table>

    </article>
    <article class="tituloSeccion">
        Información general de la orden de trabajo
    </article>
    <asp:UpdatePanel runat="server" ID="upControlOrdenTrabajo" UpdateMode="Conditional">
        <ContentTemplate>
            <wuc:InformacionGeneral ID="ctrl_InfoGeneral" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>

    <article id="formularioTotal" runat="server">

        <ul class="encabezadoTabPanel">
            <li id="liRecursosEvaluadores" runat="server" class="activo"><a class="tituloTabPanel" href="#tpEvaluadores">Evaluadores</a></li>
            <li id="liFichaEvaluacion" runat="server" class=""><a class="tituloTabPanel" href="#tpFichaEvaluacion">Ficha de Evaluacion</a></li>
        </ul>

        <article class="cuerpoTabPanel">
            <article id="tpEvaluadores" runat="server" class="tabPanel">

                <article class="tituloSeccion">
                    Profesionales encargados de la evaluación
                </article>
                <article class="formulario">

                    <asp:UpdatePanel runat="server" ID="upcamposdetextoProfencargado" RenderMode="Inline" UpdateMode="Conditional">
                        <ContentTemplate>
                            <table>
                                <tr>
                                    <th>Funcionario:</th>
                                    <td>
                                        <asp:DropDownList runat="server" ValidationGroup="AgregarAListado" ID="ddlFuncionario" Width="59%" data-tipoControl="combo" AppendDataBoundItems="true" AutoPostBack="true"></asp:DropDownList>
                                        <br />
                                        <asp:RequiredFieldValidator runat="server" ID="rfvddlFuncionario" ValidationGroup="AgregarAListado" ControlToValidate="ddlFuncionario" Display="Dynamic" ErrorMessage="Debe indicar al menos un funcionario que realizó la evaluación del trabajo.">&nbsp;</asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <th>Area Profesional: </th>
                                    <td>
                                        <asp:UpdatePanel runat="server" ID="upAreaProfesional" UpdateMode="Conditional">
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="ddlFuncionario" EventName="SelectedIndexChanged" />
                                            </Triggers>
                                            <ContentTemplate>
                                                <td>
                                                    <asp:Label runat="server" ID="lblAreaProfesional"></asp:Label></td>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr>
                                    <th>Fecha en que realizó la evaluación:</th>
                                    <td>
                                        <asp:TextBox ValidationGroup="AgregarAListado" runat="server" Width="59%" ID="txtDPFechaEvaluacionRealizada"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ValidationGroup="AgregarAListado" ID="rfvtxtDPFechaEvaluacionRealizada" ControlToValidate="txtDPFechaEvaluacionRealizada" Display="Dynamic" ErrorMessage="Debe indicar la fecha en que se realizó la evaluación.">&nbsp;</asp:RequiredFieldValidator>
                                        <asp:CompareValidator runat="server" ID="cmpvtxtDPFechaEvaluacionRealizada" ControlToValidate="txtDPFechaEvaluacionRealizada" Display="Dynamic" ErrorMessage="La fecha es inválida" Operator="DataTypeCheck" Type="Date" ValidationGroup="Aceptar">&nbsp;</asp:CompareValidator>
                                    </td>
                                </tr>

                                <tr>
                                    <th>Tiempo invertido en la evaluación:</th>
                                    <td>

                                        <asp:TextBox runat="server" ValidationGroup="AgregarAListado" ID="txtTiempoInvertidoEvaluacion" Width="9%" data-tipoControl="texto"></asp:TextBox>
                                        <asp:DropDownList Width="50%" runat="server" ValidationGroup="AgregarAListado" ID="ddlUnidadTiempoInvertido" AppendDataBoundItems="true" data-tipoControl="combo"></asp:DropDownList>
                                        <br />
                                        <asp:RequiredFieldValidator runat="server" ValidationGroup="AgregarAListado" ID="rvfTxtTiempoEstimado" ControlToValidate="txtTiempoInvertidoEvaluacion" Display="Dynamic" ErrorMessage="Debe indicar el tiempo en que se efectuó la evaluación.">&nbsp;</asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator runat="server" ValidationGroup="AgregarAListado" ID="rfvDdlUnidad" ControlToValidate="ddlUnidadTiempoInvertido" Display="Dynamic" ErrorMessage="La unidad de tiempo es requerida">&nbsp;</asp:RequiredFieldValidator>
                                    </td>

                                </tr>

                            </table>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnAgregarFuncionario" />
                        </Triggers>
                    </asp:UpdatePanel>
                </article>
                <article class="areaBotones">
                    <asp:Button runat="server" Text="Agregar" ValidationGroup="AgregarAListado" CausesValidation="true" ID="btnAgregarFuncionario" />
                </article>
                <article class="listado sinBorde">
                    <asp:UpdatePanel runat="server" ID="upRpEncargados" RenderMode="Inline" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:Repeater runat="server" ID="rpEncargados">

                                <HeaderTemplate>
                                    <table>
                                        <tr>
                                            <th>Identificación</th>
                                            <th>Nombre</th>
                                            <th>Area</th>
                                            <th>Fecha</th>
                                            <th>Tiempo</th>
                                            <th>&nbsp;</th>
                                        </tr>
                                </HeaderTemplate>

                                <ItemTemplate>
                                    <tr class="lineaDelListado">
                                        <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.CEDULA)%></td>
                                        <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.NOMBRE_EMPLEADO)%></td>
                                        <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.DESC_AREA)%></td>
                                        <td><%#CType(Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.FECHA_EJECUTA), DateTime).ToString(Utilerias.OrdenesDeTrabajo.Constantes.FORMATO_FECHA_UI)%></td>
                                        <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.DESC_TIEMPO_REAL)%></td>
                                        <td>
                                            <asp:ImageButton ID="ibBorrar" runat="server" CausesValidation="false" ToolTip="Borrar" data-tipo="borrarRegistro"
                                                CommandName='<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.CEDULA)%>'
                                                ImageUrl='<%#AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Borrar.png")%>'
                                                onmouseover='<%#String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Icono_Borrar.png"))%>'
                                                onmouseout='<%#String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Borrar.png"))%>'
                                                OnClick="ibBorrar_Click" />
                                        </td>
                                    </tr>
                                </ItemTemplate>

                                <FooterTemplate>
                                    </table>
                                </FooterTemplate>

                            </asp:Repeater>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnAgregarFuncionario" />
                        </Triggers>
                    </asp:UpdatePanel>
                </article>

            </article>
        </article>

        <article class="cuerpoTabPanel">
            <article id="tpFichaEvaluacion" runat="server" class="tabPanel">
                <table>
                    <tr>
                        <th>Indicador de Viabilidad:</th>
                        <td>
                            <asp:RadioButtonList ID="rbtnViabilidad" runat="server">
                                <asp:ListItem Value="1">Sí</asp:ListItem>
                                <asp:ListItem Value="0">No</asp:ListItem>
                            </asp:RadioButtonList>
                            <asp:RequiredFieldValidator runat="server" ValidationGroup="Modificar" ID="rfvviabilidad" ControlToValidate="rbtnViabilidad" Display="Dynamic" ErrorMessage="Debe indicar si el proyecto es viable o no">&nbsp;</asp:RequiredFieldValidator>
                        </td>
                    </tr>

                    <tr>
                        <th>Estimación Presupuestaria:</th>
                        <td>
                            <asp:TextBox ID="txtEstimacionPres" runat="server" Width="95px" Font-Bold="true" onkeypress="mascaraMoneda(this)" onpaste="return false" MaxLength="13"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ValidationGroup="Modificar" ID="rfvtxtEstimaciónPres" ControlToValidate="txtEstimacionPres" Display="Dynamic" ErrorMessage="Debe indicar el tiempo en que se efectuó la evaluación.">&nbsp;</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                </table>


                <article class="tituloSeccion">
                    Detalle
                </article>

                <article id="MainContent_lblHtml">
                    <textarea class="textoACopiar" style="height: 230px; width: 98%" data-tipocontrol="texto" id="txtTemario"></textarea>
                    <asp:HiddenField ID="hdnNicEdit" runat="server" />
                </article>

                <article>
                    <article class="tituloSeccion">
                        Documentos Adjuntos
                    </article>
                    <table>
                        <tr>
                            <th>Tipo de Archivo:</th>
                            <td>
                                <asp:DropDownList runat="server" ValidationGroup="Archivos" ID="ddlTipoArchivo" Width="59%" data-tipoControl="combo" AppendDataBoundItems="true" AutoPostBack="true" onchange="javascript:guardarContenidoNicEditors();"></asp:DropDownList>
                                <br />
                                <asp:RequiredFieldValidator runat="server" ID="rfvddlTipoArchivo" ValidationGroup="Archivos" ControlToValidate="ddlTipoArchivo" Display="Dynamic" ErrorMessage="Debe indicar el tipo de archivo">&nbsp;</asp:RequiredFieldValidator>
                            </td>
                        </tr>

                        <tr  runat="server" id="trArchivosTipo" visible="false">
                            <th>Archivo(s) Adjunto(s)</th>
                            <td>
                               <%-- <asp:UpdatePanel runat="server">
                                    <ContentTemplate>--%>
                                        <asp:FileUpload Width="59%" runat="server" ID="ifInfo" onchange="validaArchivo();" />
                                        <img runat="server" id="imgExtensiones" data-tipo="tooltipExtensiones" class="tooltip" />
                              <%--      </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="ddlTipoArchivo" EventName="SelectedIndexChanged" />
                                    </Triggers>
                                </asp:UpdatePanel>--%>
                                <asp:RequiredFieldValidator runat="server" ID="rfvIfInfo" ControlToValidate="ifInfo" ValidationGroup="Archivos" Display="Dynamic" ErrorMessage="Agregue un documento.">&nbsp;</asp:RequiredFieldValidator>                              
                            </td>
                        </tr>

                        <tr>
                            <th>Descripción:</th>
                            <td>
                                <asp:TextBox Width="59%" runat="server" ID="txtDescripcion" ValidationGroup="Archivos"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ValidationGroup="Archivos" ID="rfvtxtDescripcion" ControlToValidate="txtDescripcion" Display="Dynamic" ErrorMessage="Debe indicar la descipción para identificar el archivo.">&nbsp;</asp:RequiredFieldValidator>
                                <asp:Button runat="server" ID="btnAgregarArchivo" Text="Agregar" ValidationGroup="Archivos" OnClientClick="javascript:guardarContenidoNicEditors();" />
                                <%--<button onclick="javascript: CopyHTMLToClipboard()" title="Copiar al portapapeles">Copiar al portapapeles</button>--%>
                                <img class="tooltip" src="<%= AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Icono_Ayuda.png")%>"
                                    title="Archivos generados en el proceso de evaluación" />
                            </td>
                        </tr>
                    </table>
                </article>

                <article class="listado sinBorde">
                    <asp:UpdatePanel runat="server" ID="upRpAdjunto" RenderMode="Inline" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:Repeater runat="server" ID="rpAdjunto">
                                <HeaderTemplate>
                                    <table>
                                        <tr>
                                            <th>Archivo</th>
                                            <th>Descripción</th>
                                            <th>Tipo</th>
                                            <%--<th>&nbsp;</th>--%>
                                            <th>&nbsp;</th>
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
                                        <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ADJUNTO_ORDEN_TRABAJOLST.DESCRIPCION)%></td>
                                        <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ADJUNTO_ORDEN_TRABAJOLST.DESC_TIPO_DOCUMENTO)%></td>
                                        <td>
                                            <asp:ImageButton runat="server" ID="ibBorrar" data-tipo="borrarRegistro" AlternateText="Borrar"
                                                CommandArgument='<%# String.Format("{0}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ADJUNTO_ORDEN_TRABAJOLST.ID_ADJUNTO_ORDEN_TRABAJO))%>' OnClick="ibBorrarAdjunto_Click"
                                                CommandName='<%#Container.ItemIndex%>'
                                                ImageUrl='<%#AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Borrar.png")%>'
                                                onmouseover='<%# String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Icono_Borrar.png"))%>'
                                                onmouseout='<%# String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Borrar.png"))%>' />
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                    </table>
                                </FooterTemplate>
                            </asp:Repeater>
                        </ContentTemplate>
                        <%-- <Triggers>
                           <asp:AsyncPostBackTrigger ControlID="btnAgregarArchivo" />
                        </Triggers>--%>
                    </asp:UpdatePanel>

                </article>


            </article>
        </article>

    </article>

    <article class="areaBotones">
        <asp:Button runat="server" OnClientClick="javascript: cambioTxt()" ValidationGroup="Modificar" ID="btnGuardar" Text="Guardar" />
        <asp:Button runat="server" ValidationGroup="Modificar" OnClientClick="javascript: cambioTxt()" ID="btnGuardarYFinalizar" Text="Guardar y Finalizar" />
        <asp:Button runat="server" ID="btnRegresar" Text="Regresar" />
    </article>

    <article style="visibility: hidden">
        <asp:TextBox runat="server" ID="txtEnunciado" Width="1px" TextMode="MultiLine" Rows="1"></asp:TextBox>
    </article>
    
    <article id="arAlertasDelFormulario"></article>
    <article id="arpopupConfirmaDeseaContinuar"></article>
    <script type="text/javascript">

        function guardarContenidoNicEditors() {
            if(nicEditors.findEditor('txtTemario')){
                document.getElementById("<%=Me.txtEnunciado.ClientID%>").value = nicEditors.findEditor('txtTemario').getContent();            
                $("#<%=hdnNicEdit.ClientID%>").val(document.getElementById("<%=Me.txtEnunciado.ClientID%>").value); 
            }
        };

        function cargarExtensiones(){        
            $('[data-tipo="tooltipExtensiones"]').attr('src', '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Icono_Ayuda.png")%>');
        };

        $(document).ready(function () {

            /*Control TabPanel*/
            configurarTabPanel();

            $('[data-tipo="tooltipExtensiones"]').attr('src', '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Icono_Ayuda.png")%>');

            $('#<%=Me.liRecursosEvaluadores.ClientID%>').click(function () {
                ActivarEvaluadores();
            });

            $('#<%=Me.liFichaEvaluacion.ClientID%>').click(function () {
                ActivarEvaluacion();
            });

            /*DatePicker con Fecha Mínima (hoy)*/
            configurarDatePicker('#<%=Me.txtDPFechaEvaluacionRealizada.ClientID%>');

            configurarLongitudMaximaTexto('#<%=Me.txtEstimacionPres.ClientID%>', '<%=Utilerias.OrdenesDeTrabajo.Modelo.OTT_VIABILIDAD_TECNICA.ESTIMACION_PRESUPUESTARIA_BD_TAMANO%>');

            establecerFechaMaximaDatePicker('#<%=Me.txtDPFechaEvaluacionRealizada.ClientID%>', new Date());

        });
        
        function fechasConfiguracion(){
            /*DatePicker con Fecha Mínima (hoy)*/
            configurarDatePicker('#<%=Me.txtDPFechaEvaluacionRealizada.ClientID%>');
            establecerFechaMaximaDatePicker('#<%=Me.txtDPFechaEvaluacionRealizada.ClientID%>', new Date());
        };

        function mostrarAlertaGuardadoExitoso() {
            deshabilitarControl('#<%=btnGuardar.ClientID%>');
            deshabilitarControl('#<%=btnGuardarYFinalizar.ClientID%>');
            $('.formulario').attr('disabled', 'disabled');

            mostrarAlerta(
            '#arAlertasDelFormulario',
            {
                mensaje: 'Se ha guardado los datos indicados para la Viabilidad Técnica',
                tipo: "exito",
                transparencia: 0.9,
                posicion: 'center',
                onClosed: function () { regresarAlListado(); }
            });
        };

        function ActivarEvaluadores(){
            $('#<%=Me.tpEvaluadores.ClientID%>').addClass('activo');
            $('#<%=Me.tpFichaEvaluacion.ClientID%>').removeClass('activo');
        };

        function ActivarEvaluacion(){
            $('#<%=Me.tpEvaluadores.ClientID%>').removeClass('activo');
            $('#<%=Me.tpFichaEvaluacion.ClientID%>').addClass('activo');
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

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        var area2;
        bkLib.onDomLoaded(function () {
            area2 = new nicEditor({ maxHeight: 300 }).panelInstance('txtTemario');
            $(".nicEdit-main").html($("#<%=hdnNicEdit.ClientID %>").val());
        });

        function cambioTxt() {
            $("#<%=hdnNicEdit.ClientID %>").val($(".nicEdit-main").html());
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


        function quitarContenidoTemario() {
            area2.removeInstance('txtTemario');
            document.getElementById('txtTemario').value = "";
            area2 = new nicEditor({ maxHeight: 300 }).panelInstance('txtTemario');
        };

        function regresarAlListado() {
            var vlb_CambiosRealizados = <%= IIf(Me.BanderaCambios, "true", "false")%>;
            if (vlb_CambiosRealizados == true){          
                var vlo_ConfiguracionPopup = {
                    titulo: 'Analisis de Viabilidad Técnica',
                    mensaje: 'Al regresar se perderán los datos no guardados, ¿Desea Continuar?',
                    botones:
                            [
                                {
                                    idControl: "btnSi",
                                    textoBoton: "Si",
                                    onClick: function () { window.location = 'Lst_OT_GestionProfesionalesDisenio.aspx'; }
                                },
                            {
                                idControl: "btnNo",
                                textoBoton: "No",
                                onClick: function () { cerrarPopup(); }
                            },
                            {
                                idControl: "btnCancelar",
                                textoBoton: "Cancelar",
                                onClick: function () { cerrarPopup(); }
                            }
                            ]
                };

                $('#arAlertasDelFormulario').popup(vlo_ConfiguracionPopup);

                window.location = '#arAlertasDelFormulario';

                return false;
            }else{
                window.location = 'Lst_OT_GestionProfesionalesDisenio.aspx';
            };
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
            var vlc_Llaves;
           
            var vln_TamanoNombre = <%=Utilerias.OrdenesDeTrabajo.Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.NOMBRE_ARCHIVO_BD_TAMANO%>;
          
            if(vlo_InputArchivo.value != ''){
                
                vln_Indice =  vlc_NombreArchivo.lastIndexOf(vlc_Limiter) + 1;
                vlc_NombreCorto = vlc_NombreArchivo.substr(vln_Indice, vlc_NombreArchivo.length - vln_Indice);
                vln_IndiceExtension =  vlc_NombreArchivo.lastIndexOf(vlc_LimiterExtension) + 1;
                vlc_Extension = vlc_NombreArchivo.substr(vln_IndiceExtension, vlc_NombreArchivo.length);    
                
                vlc_ExtencionesPerimtidas = '<%=Me.ExtensionesPermitidas%>';
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


        function mostrarPopupConfirmaDeseaContinuar1(pvc_Mensaje) {    
            var vlo_ConfiguracionPopup = {
                titulo: 'Viabilidad Técnica',
                mensaje: pvc_Mensaje,
                botones:
                        [
                            {
                                idControl: "btnSi",
                                textoBoton: "Si",
                                onClick:
                                    function (e) {
                                        $(this).attr("disabled", "disabled");
                                        __doPostBack('<%=btnGuardar.UniqueID%>', 'true');
                                    }
                            },
                            {
                                idControl: "btnNo",
                                textoBoton: "No",
                                onClick: function () { cerrarPopup(); }
                            },
                            {
                                idControl: "btnCancelar",
                                textoBoton: "Cancelar",
                                onClick: function () { cerrarPopup(); }
                            }
                        ]
            };

                            $('#arpopupConfirmaDeseaContinuar').popup(vlo_ConfiguracionPopup);

                            window.location = '#arpopupConfirmaDeseaContinuar';

                            return false;
                        };

                        function mostrarPopupConfirmaDeseaContinuar2(pvc_Mensaje) {    
                            var vlo_ConfiguracionPopup = {
                                titulo: 'Viabilidad Técnica',
                                mensaje: pvc_Mensaje,
                                botones:
                                        [
                                            {
                                                idControl: "btnSi",
                                                textoBoton: "Si",
                                                onClick:
                                                    function (e) {
                                                        $(this).attr("disabled", "disabled");
                                                        __doPostBack('<%=btnGuardarYFinalizar.UniqueID%>', 'true');
                                                    }
                                            },
                            {
                                idControl: "btnNo",
                                textoBoton: "No",
                                onClick: function () { cerrarPopup(); }
                            },
                            {
                                idControl: "btnCancelar",
                                textoBoton: "Cancelar",
                                onClick: function () { cerrarPopup(); }
                            }
                                        ]
                            };

                                            $('#arpopupConfirmaDeseaContinuar').popup(vlo_ConfiguracionPopup);

                                            window.location = '#arpopupConfirmaDeseaContinuar';

                                            return false;
                                        };

                                        function deshabilitar(){
                                            deshabilitarJerarquiaDeControles(document.getElementById('<%=formularioTotal.ClientID%>'));
            
                                            deshabilitarControl('#<%=btnAgregarFuncionario.ClientID%>');
                                            deshabilitarControl('#<%=btnGuardar.ClientID%>');
                                            deshabilitarControl('#<%=btnGuardarYFinalizar.ClientID%>');
                                            deshabilitarControl('#<%=btnModificar.ClientID%>');
                                        };

                                        function CopyHTMLToClipboard() {   
            
                                            try{
                  
                                                var copyTextarea = document.querySelector('.textoACopiar');
                                                copyTextarea.select();
                                                document.execCommand('Copy');

                                            }catch(e){
                
                                            }

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


    </script>
</asp:Content>
