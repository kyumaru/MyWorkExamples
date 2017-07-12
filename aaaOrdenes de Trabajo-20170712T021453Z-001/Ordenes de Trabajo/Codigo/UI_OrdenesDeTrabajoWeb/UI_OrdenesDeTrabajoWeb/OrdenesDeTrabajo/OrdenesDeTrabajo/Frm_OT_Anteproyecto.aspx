<%@ Page Language="VB" MaintainScrollPositionOnPostback="true" MasterPageFile="~/MasterPage/Mp_Formulario.master" AutoEventWireup="false" CodeFile="Frm_OT_Anteproyecto.aspx.vb" Inherits="OrdenesDeTrabajo_Frm_OT_Anteproyecto" %>

<%@ Register TagPrefix="wuc" TagName="InformacionGeneral" Src="~/ControlesDeUsuario/Wuc_OT_InformacionGeneral.ascx" %>

<asp:Content runat="server" ContentPlaceHolderID="cphFormulario" ID="Content1">

    <header>
        <h2>Elaboración del Anteproyecto
        </h2>
    </header>

    <article class="formulario areaBotones">
        <table>
            <tr>
                <th>Version:</th>
                <td>
                    <asp:DropDownList ID="ddlVersion" Width="30%" runat="server" AutoPostBack="true" ValidationGroup="NuevaVersion"></asp:DropDownList>
                    <asp:Button runat="server" ID="btnNuevaVersion" OnClick="btnNuevaVersion_Click" ValidationGroup="NuevaVersion" Text="Generar Nueva Versión" />
                </td>
            </tr>
        </table>
    </article>

    <article class="tituloSeccion">
        Información general del proyecto
    </article>

    <article class="formulario" id="formularioTotal" runat="server">
        <article class="formulario">

            <table>
                <tr>
                    <th>Encargado del proyecto:</th>
                    <td colspan="2">
                        <asp:Label runat="server" ID="lblEncargado"></asp:Label>
                    </td>
                </tr>
                <tr runat="server" id="trRevisionUsuario">
                    <th>Enviado a aprobación</th>
                    <td>
                        <asp:Label runat="server" ID="lblFechaEnvia"></asp:Label>
                    </td>
                    <th>Respuesta el:</th>
                    <td>
                        <asp:Label runat="server" ID="lblFechaRespuesta"></asp:Label>
                    </td>
                    <th>Estado:</th>
                    <td>
                        <asp:Label runat="server" ID="lblObservaciones"></asp:Label>
                        <asp:Image runat="server" ID="imgObservaciones" data-tipo="tooltip"
                            ImageUrl='<%#AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Informacion.png")%>' />
                    </td>
                </tr>
            </table>
            <wuc:InformacionGeneral ID="ctrl_InfoGeneral" runat="server" />
            <article class="tituloSeccion">
                Información del Anteproyecto
            </article>
            <article class="formulario">

                <table>
                    <tr>
                        <th>Descripción</th>
                        <td colspan="2">
                            <asp:TextBox runat="server" ID="txtDescripcion" data-tipocontrol="texto" TextMode="MultiLine" Rows="8" Width="59%"></asp:TextBox>
                            <img class="tooltip" src="<%= AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Icono_Ayuda.png")%>"
                                title="Descripción del trabajo para el cual se elebora el anteproyecto." />
                            <asp:RequiredFieldValidator runat="server" ID="rfvTxtDescripcion" ControlToValidate="txtDescripcion" Display="Dynamic" ErrorMessage="La descripción es obligatoria" ValidationGroup="GYE"></asp:RequiredFieldValidator>
                            <span id="spContadorTxtDescripcion" class="contadorCaracteresRestantes"></span>
                        </td>
                    </tr>
                    <tr>
                        <th>Cantidad</th>
                        <td colspan="2">
                            <asp:TextBox runat="server" ID="txtCantidad" Width="15%" data-tipoControl="texto" MaxLength="5"></asp:TextBox>
                            <asp:DropDownList Width="44%" runat="server" ID="ddlUnidadMedida" AppendDataBoundItems="true" data-tipoControl="combo"></asp:DropDownList>
                            <br />
                            <asp:RequiredFieldValidator runat="server" ID="rfvtxtCantidad" ControlToValidate="txtCantidad" Display="Dynamic" ErrorMessage="La cantidad es requerida" ValidationGroup="GYE"></asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator runat="server" ID="rfvddlUnidadMedida" ControlToValidate="ddlUnidadMedida" Display="Dynamic" ErrorMessage="Por favor seleccione una unidad" ValidationGroup="GYE"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <th>&nbsp;</th>
                        <td colspan="2">
                            <asp:CheckBox runat="server" AutoPostBack="true" ID="chkPlantaFisica" Text="Necesita Aval de Planta Física" />
                            <asp:LinkButton runat="server" ID="lnkArchivoPlantaFisica" Visible="false"></asp:LinkButton>&nbsp;&nbsp;                        
                        <asp:ImageButton ID="btnEliminarArchivoPlanta" runat="server" ToolTip="Borrar" data-tipo="borrarRegistro"
                            OnClientClick="return confirmacionEliminarArchivoPlanta();" Visible="false" />
                        </td>
                    </tr>
                    <tr class="areaBotones">
                        <th>&nbsp;</th>
                        <td>
                            <asp:FileUpload Width="59%" runat="server" ID="fuPlantaFisica" onchange="validaArchivoPlantaFisica()" />
                            <asp:Button runat="server" ID="btnAgregarPlanta" ValidationGroup="AgregarPlanta" Text="Agregar" Visible="false" />
                            <img runat="server" id="imgExtensionesPlanta" data-tipo="tooltipExtensiones" class="tooltip" />
                            <asp:RequiredFieldValidator runat="server" ID="rfvFuPlantaFisica" ControlToValidate="fuPlantaFisica" ValidationGroup="AgregarPlanta" Display="Dynamic" ErrorMessage="Agregue un documento.">&nbsp;</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <th>&nbsp;</th>
                        <td colspan="2">
                            <asp:CheckBox runat="server" AutoPostBack="true" ID="chkForesta" Text="Necesita Aval de Foresta" />
                            <asp:LinkButton runat="server" ID="lnkArchivoForesta" Visible="false"></asp:LinkButton>&nbsp;&nbsp;                        
                        <asp:ImageButton ID="btnEliminarArchivoForesta" runat="server" ToolTip="Borrar" data-tipo="borrarRegistro"
                            OnClientClick="return confirmacionEliminarArchivoForesta();" Visible="false" />
                        </td>
                    </tr>
                    <tr class="areaBotones">
                        <th>&nbsp;</th>
                        <td>
                            <asp:FileUpload Width="59%" runat="server" ID="fuForesta" onchange="validaArchivoForesta()" />
                            <asp:Button runat="server" ID="btnAgregarForesta" ValidationGroup="AgregarForesta" Text="Agregar" Visible="false" />
                            <img runat="server" id="imgExtensionesForesta" data-tipo="tooltipExtensiones" class="tooltip" />
                            <asp:RequiredFieldValidator runat="server" ID="rfvFuForesta" ControlToValidate="fuForesta" ValidationGroup="AgregarForesta" Display="Dynamic" ErrorMessage="Agregue un documento.">&nbsp;</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <th>Tiempo para Respuesta</th>
                        <td colspan="2">
                            <asp:TextBox runat="server" ID="txtTiempoRespuesta" Width="15%" data-tipoControl="texto" MaxLength="3"></asp:TextBox>
                            <asp:DropDownList runat="server" ID="ddlUnidadTiempoRespuesta" Width="44%"></asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </article>
        </article>
        <article class="formulario">

            <ul class="encabezadoTabPanel">
                <li id="liActividadesContempladas" runat="server" class="activo"><a class="tituloTabPanel" href="#tpActividadesContempladas">Actividades Contempladas</a></li>
                <li id="liDocumentos" runat="server"><a class="tituloTabPanel" href="#tpDocumentos">Documentos</a></li>
            </ul>

            <article class="cuerpoTabPanel">
                <article id="tpActividadesContempladas" runat="server" class="tabPanel">
                    <asp:UpdatePanel runat="server" ID="upActContempladas" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:TextBox runat="server" ID="txtActividad" Width="59%" data-tipoControl="texto" ValidationGroup="Actividad"></asp:TextBox>
                            <asp:Button runat="server" ID="btnAgregarActividad" Text="Agregar" ValidationGroup="Actividad" OnClick="btnAgregarActividad_Click" />
                            <asp:Button runat="server" ID="btnModificarActividad" Visible="false" Text="Modificar" ValidationGroup="Actividad" OnClick="btnModificarActividad_Click" />
                            <%--<asp:Button runat="server" ID="btnModificar" Text="Modificar" ValidationGroup="Actividad" OnClick="btnAgregarActividad_Click" />--%>
                            <span id="sptxtActividadContador" class="contadorCaracteresRestantes"></span>
                            <article class="listado sinBorde">
                                <asp:Repeater runat="server" ID="rpActividades">

                                    <HeaderTemplate>
                                        <table>
                                            <tr>
                                                <th>Actividad</th>
                                                <th>&nbsp;</th>
                                                <th>&nbsp;</th>
                                            </tr>
                                    </HeaderTemplate>

                                    <ItemTemplate>
                                        <tr class="lineaDelListado">

                                            <td><%#Eval("ACTIVIDAD")%></td>

                                            <td runat="server" id="tdBorrar">
                                                <asp:ImageButton ID="ibBorrar" runat="server" CausesValidation="false" ToolTip="Borrar" data-tipo="borrarRegistro"
                                                    CommandName='<%#Eval("ACTIVIDAD")%>'
                                                    ImageUrl='<%#AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Borrar.png")%>'
                                                    onmouseover='<%#String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Icono_Borrar.png"))%>'
                                                    onmouseout='<%#String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Borrar.png"))%>'
                                                    OnClick="ibBorrar_Click" />
                                            </td>
                                            <td runat="server" id="tdModificar">
                                                <asp:ImageButton ID="ibmodificar" runat="server" CausesValidation="false" ToolTip="Modificar" data-tipo="modificarRegistro"
                                                    CommandName='<%#Eval("ACTIVIDAD")%>'
                                                    ImageUrl='<%#AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Editar.png")%>'
                                                    onmouseover='<%#String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Icono_Editar.png"))%>'
                                                    onmouseout='<%#String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Editar.png"))%>'
                                                    OnClick="ibmodificar_Click" />
                                            </td>
                                        </tr>
                                    </ItemTemplate>

                                    <FooterTemplate>
                                        </table>
                                    </FooterTemplate>

                                </asp:Repeater>
                            </article>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnAgregarActividad" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btnModificarActividad" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>

                </article>
            </article>

            <article class="cuerpoTabPanel">
                <article id="tpDocumentos" runat="server" class="tabPanel">

                    <table>
                        <tr>
                            <th>Tipo de Archivo:</th>
                            <td>
                                <asp:DropDownList runat="server" ValidationGroup="Archivos" ID="ddlTipoArchivo" Width="59%" data-tipoControl="combo" AppendDataBoundItems="true" AutoPostBack="true"></asp:DropDownList>
                                <br />
                                <asp:RequiredFieldValidator runat="server" ID="rfvddlTipoArchivo" ValidationGroup="Archivos" ControlToValidate="ddlTipoArchivo" Display="Dynamic" ErrorMessage="Debe indicar el tipo de archivo">&nbsp;</asp:RequiredFieldValidator>
                            </td>
                        </tr>

                        <tr runat="server" id="trArchivosTipo" visible="false">
                            <th>Archivo(s) Adjunto(s)</th>
                            <td>
                                <asp:FileUpload Width="59%" runat="server" ID="ifInfo" onchange="validaArchivo()" />
                                <%--<Upload:InputFile id="ifInfo" runat="server" />--%>

                                <img runat="server" id="imgExtensiones" data-tipo="tooltipExtensiones" class="tooltip" />
                                <asp:RequiredFieldValidator runat="server" ID="rfvIfInfo" ControlToValidate="ifInfo" ValidationGroup="Archivos" Display="Dynamic" ErrorMessage="Agregue un documento.">&nbsp;</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <th>Descripción:</th>
                            <td>
                                <asp:TextBox Width="47%" runat="server" ID="txtDescripcionArchivo" ValidationGroup="Archivos"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ValidationGroup="Archivos" ID="rfvtxtDescripcionArchivo" ControlToValidate="txtDescripcionArchivo" Display="Dynamic" ErrorMessage="Debe indicar la descipción para identificar el archivo.">&nbsp;</asp:RequiredFieldValidator>
                                <asp:Button runat="server" ID="btnAgregarArchivo" Text="Agregar" ValidationGroup="Archivos" />
                                <img class="tooltip" src="<%= AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Icono_Ayuda.png")%>"
                                    title="Archivos generados en el proceso de evaluación" />
                                <span id="spContadorDescripcionArchivo" class="contadorCaracteresRestantes"></span>
                            </td>
                        </tr>

                    </table>

                    <article class="listado sinBorde">
                        <asp:Repeater runat="server" ID="rpAdjunto">
                            <HeaderTemplate>
                                <table>
                                    <tr>
                                        <th>Archivo</th>
                                        <th>Descripción</th>
                                        <th>Tipo</th>
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
                                            Text="<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DOCUMENTO_ANTEPROYECTLST.NOMBRE_ARCHIVO)%>"></asp:LinkButton>
                                    </td>
                                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DOCUMENTO_ANTEPROYECTLST.DESCRIPCION)%></td>
                                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DOCUMENTO_ANTEPROYECTLST.DESC_TIPO_DOCUMENTO)%></td>
                                    <td>
                                        <asp:ImageButton runat="server" ID="ibBorrar" data-tipo="borrarRegistro" AlternateText="Borrar"
                                            CommandArgument='<%# String.Format("{0}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DOCUMENTO_ANTEPROYECTLST.ID_ADJUNTO_ORDEN_TRABAJO))%>' OnClick="ibBorrarAdjunto_Click"
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
                    </article>                    
                </article>
            </article>
        </article>
    </article>

    <article class="areaBotones">
        <asp:Button runat="server" ValidationGroup="GYE" ID="btnPDF" Text="Generar Archivo PDF" />
        <asp:Button runat="server" ValidationGroup="GYE" ID="btnGuardar" Text="Guardar" />
        <asp:Button runat="server" ValidationGroup="GYE" ID="btnGuardarYEnviar" Text="Guardar y Enviar" />
        <input type="button" value="Regresar" id="btnRegresar" />
    </article>
    
    <asp:UpdatePanel runat="server">
        <Triggers>
            <asp:AsyncPostBackTrigger  ControlID="btnPDF" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>

    <article id="arAlertasDelFormulario"></article>
    <article id="arpopupConfirmaDeseaContinuar"></article>
    <article id="arpopupConfirmaDeseaBorrar"></article>

    <script type="text/javascript">

        function regresarConsulta(){
        
            $('#btnRegresar').click(function () {
                regresarAlListadoConsulta();
            });

        };

        function regresarNormal(){
        
            $('#btnRegresar').click(function () {
                regresarAlListado();
            });

        };  
        
        function regresar(){
        
            $('#btnRegresar').click(function () {
                regresarAlListadoSinMod();
            });

        };  

        $(document).ready(function () {

         

            /*Control TabPanel*/
            configurarTabPanel();

            $('#<%=Me.liActividadesContempladas.ClientID%>').click(function () {
                activarContempladas();
            });

            $('#<%=Me.liDocumentos.ClientID%>').click(function () {
                activarDocumentos();
            });

            $('#<%=Me.ifInfo.ClientID%>').bind('change', function(){
                validaArchivo();
            });

            configurarLongitudMaximaTexto('#<%=Me.txtDescripcion.ClientID%>',<%=Utilerias.OrdenesDeTrabajo.Modelo.OTT_ANTEPROYECTO.DESCRIPCION_BD_TAMANO%>);
            configurarContadorCaracteresRestantes('#<%=Me.txtDescripcion.ClientID%>','#spContadorTxtDescripcion');

            configurarLongitudMaximaTexto('#<%=Me.txtDescripcionArchivo.ClientID%>',<%=Utilerias.OrdenesDeTrabajo.Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.DESCRIPCION_BD_TAMANO%>);
            configurarContadorCaracteresRestantes('#<%=Me.txtDescripcionArchivo.ClientID%>','#spContadorDescripcionArchivo');

            configurarLongitudMaximaTexto('#<%=me.txtActividad.ClientID%>',<%=Utilerias.OrdenesDeTrabajo.Modelo.OTT_ANTEPROYECTO.DESCRIPCION_BD_TAMANO - ActividadCaracteresRestantes%>);
            configurarContadorCaracteresRestantes('#<%=Me.txtActividad.ClientID%>','#sptxtActividadContador');

            $('[data-tipo="tooltipExtensiones"]').attr('src', '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Icono_Ayuda.png")%>');

            $('[data-tipo="borrarRegistro"]').attr('src', '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Borrar.png")%>');

            $('[data-tipo="borrarRegistro"]').on({
                'mouseover': function () { $(this).attr('src', '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Icono_Borrar.png")%>'); },

                'mouseout': function () { $(this).attr('src', '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Borrar.png")%>'); }
            });

        });

        function activarContempladas(){
            $('#<%=Me.tpActividadesContempladas.ClientID%>').addClass('activo');
            $('#<%=Me.tpDocumentos.ClientID%>').removeClass('activo');
            $('#<%=Me.liDocumentos.ClientID%>').removeClass('activo');
        };

        function activarDocumentos(){
            $('#<%=Me.tpActividadesContempladas.ClientID%>').removeClass('activo');
            $('#<%=Me.tpDocumentos.ClientID%>').addClass('activo');
            $('#<%=Me.liActividadesContempladas.ClientID%>').removeClass('activo');
        };

        function validaArchivoPlantaFisica() {
            var vlo_InputArchivo = document.getElementById('<%=fuPlantaFisica.ClientID%>');
            var vlc_NombreArchivo = vlo_InputArchivo.value;
            var vlc_NombreCorto = " ";
            var vlc_Limiter = '\\';
            var vlc_Extension = '';
            var vln_Indice = 0;
            var vln_IndiceExtension = 0;
            var vlc_LimiterExtension = '.';
            var vln_limiteTamArchivo = <%=Me.TamanoArchivoPlanta%>;
            var vln_TamArchivo;
            var vln_TamBytes = vln_limiteTamArchivo * <%=Utilerias.OrdenesDeTrabajo.Constantes.TAMANNO_BYTES_A_MEGAS%>;
             var vlc_Llaves;
           
             var vln_TamanoNombre = <%=Utilerias.OrdenesDeTrabajo.Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.NOMBRE_ARCHIVO_BD_TAMANO%>;
          
            if(vlo_InputArchivo.value != ''){
                
                vln_Indice =  vlc_NombreArchivo.lastIndexOf(vlc_Limiter) + 1;
                vlc_NombreCorto = vlc_NombreArchivo.substr(vln_Indice, vlc_NombreArchivo.length - vln_Indice);
                vln_IndiceExtension =  vlc_NombreArchivo.lastIndexOf(vlc_LimiterExtension) + 1;
                vlc_Extension = vlc_NombreArchivo.substr(vln_IndiceExtension, vlc_NombreArchivo.length);    
                
                vlc_ExtencionesPerimtidas = '<%=Me.ExtensionesPermitidasPlanta%>';
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

        function validaArchivoForesta() {
            var vlo_InputArchivo = document.getElementById('<%=fuPlantaFisica.ClientID%>');
            var vlc_NombreArchivo = vlo_InputArchivo.value;
            var vlc_NombreCorto = " ";
            var vlc_Limiter = '\\';
            var vlc_Extension = '';
            var vln_Indice = 0;
            var vln_IndiceExtension = 0;
            var vlc_LimiterExtension = '.';
            var vln_limiteTamArchivo = <%=Me.TamanoArchivoForesta%>;
            var vln_TamArchivo;
            var vln_TamBytes = vln_limiteTamArchivo * <%=Utilerias.OrdenesDeTrabajo.Constantes.TAMANNO_BYTES_A_MEGAS%>;
            var vlc_Llaves;
           
            var vln_TamanoNombre = <%=Utilerias.OrdenesDeTrabajo.Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.NOMBRE_ARCHIVO_BD_TAMANO%>;
          
             if(vlo_InputArchivo.value != ''){
                
                 vln_Indice =  vlc_NombreArchivo.lastIndexOf(vlc_Limiter) + 1;
                 vlc_NombreCorto = vlc_NombreArchivo.substr(vln_Indice, vlc_NombreArchivo.length - vln_Indice);
                 vln_IndiceExtension =  vlc_NombreArchivo.lastIndexOf(vlc_LimiterExtension) + 1;
                 vlc_Extension = vlc_NombreArchivo.substr(vln_IndiceExtension, vlc_NombreArchivo.length);    
                
                 vlc_ExtencionesPerimtidas = '<%=Me.ExtensionesPermitidasForesta%>';
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

        function mostrarAlertaGuardadoExitoso() {
            deshabilitarControl('#<%=btnGuardar.ClientID%>');
            deshabilitarControl('#<%=btnGuardarYEnviar.ClientID%>');
            $('.formulario').attr('disabled', 'disabled');

            mostrarAlerta(
            '#arAlertasDelFormulario',
            {
                mensaje: 'Se ha guardado los datos indicados para el anteproyecto',
                tipo: "exito",
                transparencia: 0.9,
                posicion: 'center',
                onClosed: function () { window.location='Lst_OT_GestionProfesionalesDisenio.aspx'; }
            });
        };

        function mostrarAlertaNuevaVersion() {
        

            mostrarAlerta(
            '#arAlertasDelFormulario',
            {
                mensaje: 'Se ha generado una nueva versión del anteproyecto',
                tipo: "exito",
                transparencia: 0.9,
                posicion: 'center'
            });
        };

        function confirmacionEliminarArchivoPlanta() {
            var vlo_ConfiguracionPopup = {
                titulo: 'Creación de Anteproyecto',
                mensaje: '¿Desea borrar el archivo seleccionado?',
                botones:
                        [
                            {
                                idControl: "btnSi",
                                textoBoton: "Si",
                                onClick:
                                    function (e) {
                                        $(this).attr("disabled", "disabled");
                                        __doPostBack('<%=btnEliminarArchivoPlanta.UniqueID%>', '');
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

            $('#arpopupConfirmaDeseaBorrar').popup(vlo_ConfiguracionPopup);

            window.location = '#arpopupConfirmaDeseaBorrar';

            return false;
        };

        function confirmacionEliminarArchivoForesta() {
            var vlo_ConfiguracionPopup = {
                titulo: 'Creación de Anteproyecto',
                mensaje: '¿Desea borrar el archivo seleccionado?',
                botones:
                        [
                            {
                                idControl: "btnSi",
                                textoBoton: "Si",
                                onClick:
                                    function (e) {
                                        $(this).attr("disabled", "disabled");
                                        __doPostBack('<%=btnEliminarArchivoForesta.UniqueID%>', '');
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

            $('#arpopupConfirmaDeseaBorrar').popup(vlo_ConfiguracionPopup);

            window.location = '#arpopupConfirmaDeseaBorrar';

            return false;
        };

        function regresarAlListadoConsulta() {
            window.location = '<%=Me.PaginaRegresar%>'; 
        };

        function regresarAlListadoSinMod(){
            window.location = 'Lst_OT_GestionProfesionalesDisenio.aspx'; 
        };

        function regresarAlListado() {
            
                
            var vlo_ConfiguracionPopup = {
                titulo: 'Creación de Anteproyecto',
                mensaje: 'Al regresar se perderán los datos no guardados, ¿Desea Continuar?',
                botones:
                        [
                            {
                                idControl: "btnSi",
                                textoBoton: "Si",
                                onClick: function () { window.location = '<%=Me.PaginaRegresar%>'; }
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
       
        };

        function deshabilitar(){
            deshabilitarJerarquiaDeControles(document.getElementById('<%=formularioTotal.ClientID%>'));
                deshabilitarControl('#<%=btnAgregarArchivo.ClientID%>');
                deshabilitarControl('#<%=btnAgregarActividad.ClientID%>');
                deshabilitarControl('#<%=btnAgregarForesta.ClientID%>');
                deshabilitarControl('#<%=btnAgregarPlanta.ClientID%>');
                deshabilitarControl('#<%=btnEliminarArchivoForesta.ClientID%>');
                deshabilitarControl('#<%=btnEliminarArchivoPlanta.ClientID%>');
                deshabilitarControl('#<%=btnGuardar.ClientID%>');
                deshabilitarControl('#<%=btnGuardarYEnviar.ClientID%>');
                deshabilitarControl('#<%=btnModificarActividad.ClientID%>');
                deshabilitarControl('#<%=btnNuevaVersion.ClientID%>');
        };

        function habilitarVersion(){
            habilitarJerarquiaDeControles(document.getElementById('<%=formularioTotal.ClientID%>'));
                    habilitarControl('#<%=btnAgregarArchivo.ClientID%>');
                    habilitarControl('#<%=btnAgregarActividad.ClientID%>');
                    habilitarControl('#<%=btnAgregarForesta.ClientID%>');
                    habilitarControl('#<%=btnAgregarPlanta.ClientID%>');
                    habilitarControl('#<%=btnEliminarArchivoForesta.ClientID%>');
                    habilitarControl('#<%=btnEliminarArchivoPlanta.ClientID%>');
                    habilitarControl('#<%=btnGuardar.ClientID%>');
                    habilitarControl('#<%=btnGuardarYEnviar.ClientID%>');
                    habilitarControl('#<%=btnModificarActividad.ClientID%>');
                };


        function deshabilitarVersion(){
            deshabilitarJerarquiaDeControles(document.getElementById('<%=formularioTotal.ClientID%>'));
                deshabilitarControl('#<%=btnAgregarArchivo.ClientID%>');
                deshabilitarControl('#<%=btnAgregarActividad.ClientID%>');
                deshabilitarControl('#<%=btnAgregarForesta.ClientID%>');
                deshabilitarControl('#<%=btnAgregarPlanta.ClientID%>');
                deshabilitarControl('#<%=btnEliminarArchivoForesta.ClientID%>');
                deshabilitarControl('#<%=btnEliminarArchivoPlanta.ClientID%>');
                deshabilitarControl('#<%=btnGuardar.ClientID%>');
                deshabilitarControl('#<%=btnGuardarYEnviar.ClientID%>');
                deshabilitarControl('#<%=btnModificarActividad.ClientID%>');
            };

    </script>

</asp:Content>
