<%@ Page Language="VB" AutoEventWireup="false" MaintainScrollPositionOnPostback="true" MasterPageFile="~/MasterPage/Mp_Formulario.master" CodeFile="Frm_OT_RevisionRequisicionMaterial.aspx.vb" Inherits="OrdenesDeTrabajo_Almacen_Frm_OT_RevisionRequisicionMaterial" %>

<%@ Register TagPrefix="wuc" TagName="InformacionGeneral" Src="~/ControlesDeUsuario/Wuc_OT_InformacionGeneral.ascx" %>
<%@ Register TagPrefix="wuc" Namespace="Utilerias.WebForms.Controles" Assembly="Utilerias.WebForms.Controles" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register TagPrefix="wuc" TagName="Materiales" Src="~/Controles/wuc_OT_Materiales.ascx" %>
<%@ Register TagPrefix="wuc" TagName="DatosMaterial" Src="~/Controles/wuc_OT_DatosMaterial.ascx" %>

<asp:Content runat="server" ID="Content1" ContentPlaceHolderID="cphFormulario">

    <header>
        <h2>Revisión de Requisición de Materiales</h2>
    </header>

    <article class="tituloSeccion">
        Infomación General
    </article>

    <article>
        <wuc:InformacionGeneral ID="ctrl_InfoGeneral" runat="server" />
    </article>

    <article class="tituloSeccion">
        Observaciones
    </article>
    
    <article class="formulario">
        <table>
            <tr>
                <th>Motivo de la Revisión</th>
                <td>
                    <textarea runat="server" id="txtObservacionesEncargado" style="color: #adb7bc; width: 88%;" readonly="readonly" textmode="MultiLine" rows="4" columns="120"></textarea>
                </td>
            </tr>
            <tr>
                <th>Observaciones de la Solicitud</th>
                <td>
                    <textarea runat="server" id="txtObservacionesGenerales" style="color: #adb7bc; width: 88%;" readonly="readonly" textmode="MultiLine" rows="4" columns="60"></textarea>
                </td>
            </tr>
        </table>
    </article>

    <asp:UpdatePanel runat="server" ID="upPanelMateriales">
        <ContentTemplate>
            <article class="formulario">
                <table>
                    <tr>
                        <th>Código</th>
                        <td>
                            <asp:UpdatePanel runat="server" ID="upTxtCodigo" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:TextBox runat="server" ID="txtCodigo" data-tipocontrol="texto" AutoPostBack="true"></asp:TextBox>
                                    <asp:RequiredFieldValidator runat="server" ID="rfvTxtCodigo" ControlToValidate="txtCodigo"
                                        Display="Dynamic" ErrorMessage="El código del material es requerido." ValidationGroup="AgregarListado">&nbsp;</asp:RequiredFieldValidator>
                                    <asp:LinkButton ID="lnkEjecutarBusquedaMaterial" runat="server">
                                <img id="imgBuscarMaterial" title="Buscar Material" alt="Buscar Material" src="" />
                                    </asp:LinkButton>
                                    <br />
                                    <span id="spContadorTxtIdSolicitante" class="contadorCaracteresRestantes"></span>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="txtCodigo" EventName="TextChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </article>

            <asp:UpdatePanel runat="server" ID="upControlDatosMaterial" UpdateMode="Conditional" Visible="false">
                <ContentTemplate>
                    <wuc:DatosMaterial ID="WucDatosMaterial" runat="server"></wuc:DatosMaterial>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="txtCodigo" EventName="TextChanged" />
                    <asp:AsyncPostBackTrigger ControlID="ctrl_Materiales" />
                </Triggers>
            </asp:UpdatePanel>

            <article class="areaBotones">
                <asp:Button runat="server" ID="btnAgregar" ValidationGroup="AgregarListado" Text="Agregar" OnClick="btnAgregar_Click" />
                <asp:Button runat="server" ID="btnModificar" Visible="false" Text="Modificar" OnClick="btnModificar_Click" />
                <asp:Button runat="server" ID="btnCancelar" Text="Cancelar" Visible="false" OnClick="btnCancelar_Click" />
            </article>

            <article class="tituloSeccion" visible="false" id="tituloListado" runat="server">
                Listado de Materiales
            </article>
            <article class="listado" data-grupo="Listado">
                <asp:Repeater runat="server" ID="rpPedidos">
                    <HeaderTemplate>
                        <table>
                            <tr>
                                <th>
                                    <asp:LinkButton runat="server" ID="lnkCodigo" Text="Código" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_MATERIALLST.ID_MATERIAL%>" CommandArgument="ASC" OnCommand="lnkRpPedidos_Command"></asp:LinkButton>
                                </th>
                                <th>
                                    <asp:LinkButton runat="server" ID="lnkDescripcion" Text="Descripción" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_MATERIALLST.DESCRIPCION%>" CommandArgument="ASC" OnCommand="lnkRpPedidos_Command"></asp:LinkButton>
                                </th>
                                <th>
                                    <asp:LinkButton runat="server" ID="lnkDetalle" Text="Detalle"></asp:LinkButton>
                                </th>
                                <th>
                                    <asp:LinkButton runat="server" ID="lnkDisponible" Text="Disp. Almacen"></asp:LinkButton>
                                </th>
                                <th>
                                    <asp:LinkButton runat="server" ID="lnkCantidadSolicitada" Text="Cantidad Solicitada"></asp:LinkButton>
                                </th>
                                <th>
                                    <asp:LinkButton runat="server" ID="lnkMonto" Text="Monto" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_MATERIALLST.COSTO_PROMEDIO%>" CommandArgument="ASC" OnCommand="lnkRpPedidos_Command"></asp:LinkButton>
                                </th>
                                <th>&nbsp;</th>
                                <th>&nbsp;</th>
                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_MATERIALLST.ID_MATERIAL)%></td>
                            <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_MATERIALLST.DESCRIPCION)%></td>
                            <td style="text-align: center">
                                <asp:Image runat="server" ID="imgDetalle" data-tipo="tooltip"
                                    title="<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_MATERIALLST.DETALLE)%>"
                                    ImageUrl='<%#AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Informacion.png")%>' />

                            </td>
                            <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_MATERIALLST.DISP_ALMACEN_SOLICITUD_MEDIDA)%></td>
                            <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_MATERIALLST.CANTIDAD_SOLICITADA_MEDIDA)%></td>
                            <td><%#CType(Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_MATERIALLST.COSTO_PROMEDIO), Double).ToString("N2")%></td>
                            <td>
                                <asp:ImageButton runat="server" data-tipo="modificarRegistro" ID="ibModificar" AlternateText="Modificar el pedido"
                                    CommandArgument="<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_MATERIALLST.ID_MATERIAL)%>"
                                    OnClick="ibModificar_Click"
                                    ImageUrl='<%#AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Editar.png")%>'
                                    onmouseover='<%# String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Icono_Editar.png"))%>'
                                    onmouseout='<%# String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Editar.png"))%>' />
                            </td>
                            <td>
                                <asp:ImageButton runat="server" ID="ibBorrar" AlternateText="Borrar el pedido" data-tipo="borrarRegistro"
                                    CommandArgument="<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_MATERIALLST.ID_DETALLE_MATERIAL)%>"
                                    OnClick="ibBorrar_Click"
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

            <article class="areaCantidadDeRegistro" data-grupo="Listado">
                <asp:Label runat="server" ID="lblMontoTotal" Text=""></asp:Label>
            </article>

        </ContentTemplate>
    </asp:UpdatePanel>

    <br />

    <article class="tituloSeccion">
        Solicitud de presupuesto a la unidad Solicitante
    </article>

    <article class="formulario">
        <asp:CheckBox runat="server" AutoPostBack="true" ID="chkSolicitarPresupuesto" Text="Solicitar presupuesto al solicitante" OnCheckedChanged="chkSolicitarPresupuesto_CheckedChanged" />
        <table runat="server" id="tblArchivos" visible="false">
            <tr>
                <th>Adjunto para solicitud de presupuesto</th>
                <td colspan="2">
                    <asp:LinkButton runat="server" ID="lnkArchivoSolicitud" Visible="false"></asp:LinkButton>&nbsp;&nbsp;                        
                    <asp:ImageButton ID="btnEliminarArchivoSolicitud" runat="server" ToolTip="Borrar" data-tipo="borrarRegistroSolicitud"
                        OnClientClick="return confirmacionEliminarArchivoSolicitud();" Visible="false" />
                </td>
            </tr>
            <tr class="areaBotones">
                <th>&nbsp;</th>
                <td>
                    <asp:FileUpload Width="59%" runat="server" ID="fuSolicitud" Visible="false" onchange="validaArchivoSolicitud()" />
                    <asp:Button runat="server" ID="btnAgregarSolicitud" ValidationGroup="AgregarSolicitud" Text="Agregar" Visible="false" />
                    <img runat="server" id="imgExtensionesSolicitud" data-tipo="tooltipExtensiones" visible="false" class="tooltip" />
                    <asp:RequiredFieldValidator runat="server" ID="rfvFuSolicitud" ControlToValidate="fuSolicitud"
                        ValidationGroup="AgregarSolicitud" Display="Dynamic" ErrorMessage="Agregue un documento.">&nbsp;</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <th>Adjunto de respuesta a la solicitud de presupuesto</th>
                <td colspan="2">
                    <asp:LinkButton runat="server" ID="lnkArchivoRespuesta" Visible="false"></asp:LinkButton>&nbsp;&nbsp;                        
                    <asp:ImageButton ID="btnEliminarArchivoRespuesta" runat="server" ToolTip="Borrar" data-tipo="borrarRegistroRespuesta"
                        OnClientClick="return confirmacionEliminarArchivoRespuesta();" Visible="false" />
                </td>
            </tr>
            <tr class="areaBotones">
                <th>&nbsp;</th>
                <td>
                    <asp:FileUpload Width="59%" runat="server" ID="fuRespuesta" Visible="false" onchange="validaArchivoRespuesta()" />
                    <asp:Button runat="server" ID="btnAgregarRespuesta" ValidationGroup="AgregarRespuesta" Text="Agregar" Visible="false" />
                    <img runat="server" id="imgExtensionesRespuesta" data-tipo="tooltipExtensiones" visible="false" class="tooltip" />
                    <asp:RequiredFieldValidator runat="server" ID="rfvFuRespuesta" ControlToValidate="fuRespuesta" ValidationGroup="AgregarRespuesta"
                        Display="Dynamic" ErrorMessage="Agregue un documento.">&nbsp;</asp:RequiredFieldValidator>
                </td>
            </tr>
        </table>
    </article>
    <br />
    <article class="tituloSeccion">
        Revisión de la solicitud
    </article>

    <asp:UpdatePanel runat="server" ID="upObservaciones" UpdateMode="Conditional">
        <ContentTemplate>

            <br />
            <asp:RadioButton ID="rbtAprobada" ValidationGroup="Aceptar" Checked="true" runat="server" Text="Aprobar" GroupName="Condicion" AutoPostBack="true" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:RadioButton ID="rbtLiquidar" ValidationGroup="Aceptar" runat="server" Text="Liquidar" GroupName="Condicion" AutoPostBack="true" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:RadioButton ID="rbtDevuelta" ValidationGroup="Aceptar" runat="server" Text="Devolver al Coordinador" GroupName="Condicion" AutoPostBack="true" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <article class="formulario sinBorde">
                <table>
                    <tr runat="server" id="trObservaciones">
                        <th>Observaciones</th>
                        <td>
                            <asp:TextBox runat="server" ID="txtObservaciones" TextMode="MultiLine" Rows="4" Columns="60"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" Enabled="false" ID="rfvTxtObservaciones" ControlToValidate="txtObservaciones"
                                Display="Dynamic" ErrorMessage="El detalle asociado a la solicitud del material es requerido." ValidationGroup="Aceptar">&nbsp;</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                </table>
            </article>
        </ContentTemplate>
    </asp:UpdatePanel>

    <article class="areaBotones">
        <asp:Button runat="server" ID="btnAceptar" ValidationGroup="Aceptar" Text="Aceptar" OnClick="btnAceptar_Click" />
        <input type="button" id="btnRegresar" value="Regresar" />
    </article>

    <article id="popupConfirmacionDeseaBorrar"></article>
    <article id="arAlertasDelFormulario"></article>
    <article id="arpopupConfirmaDeseaBorrar"></article>

    <%--Popup para búsqueda de materiales--%>
    <article id="PopUpBusquedaMateriales" class="ventanaEmergente">
        <article class="formulario" style="width: 850px!important;">
            <a href="#CerrarPopUpBusquedaMateriales" title="Cerrar Ventana" class="botonCerrarVentanaEmergente">X</a>
            <br />
            <asp:UpdatePanel ID="upContactoConv" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <wuc:Materiales ID="ctrl_Materiales" runat="server"></wuc:Materiales>
                    <div class="areaBotones">
                        <a href="#CerrarPopUpBusquedaMateriales" title="Regresar">Regresar</a>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </article>
    </article>
    <%--Fin Popup para búsqueda de materiales--%>

    <script type="text/javascript">

        function InhabilitarCodigo() {

            document.getElementById("imgBuscarMaterial").style.visibility = "hidden";

        };

        function HabilitarCodigo() {

            document.getElementById("imgBuscarMaterial").style.visibility = "visible";

        };

        function cargarTooltip() {

            $('[data-tipo="tooltip"]').each(function () {
                $('#' + this.id).tooltipster({
                    interactive: true,
                    interactiveTolerance: 400,
                    theme: 'tooltipster-light'
                });
            });

            $('[data-tipo="borrarRegistro"]').click(function () { return mostrarPopupConfirmacionDeseaBorrarRegistro($(this).data("uniqueid")); });
        };

        function mostrarPopupConfirmacionDeseaBorrarRegistro(pvc_UniqueIdControl) {
            var vlo_ConfiguracionPopup = {
                titulo: "<em>Catalogo de requisición de materiales</em>",
                mensaje: '<%=String.Format("¿Está seguro de eliminar el material {0} del listado?", Me.WucDatosMaterial.RetornaDescripcion)%>',
                botones:
                    [
                        {
                            idControl: "btnSi",
                            textoBoton: "Sí",
                            onClick:
                                function (e) {
                                    $(this).attr("disabled", "disabled");
                                    __doPostBack(pvc_UniqueIdControl, '');
                                }
                        },
                    {
                        idControl: "btnNo",
                        textoBoton: "No",
                        onClick:
                            function (e) { cerrarPopup(); }
                    },
                    {
                        idControl: "btnCancelar",
                        textoBoton: "Cancelar",
                        onClick:
                            function (e) { cerrarPopup(); }
                    }

                    ]
            };

            $('#popupConfirmacionDeseaBorrar').popup(vlo_ConfiguracionPopup);
            window.location = "#popupConfirmacionDeseaBorrar";

            return false;
        };

        function validaArchivoSolicitud() {
            var vlo_InputArchivo = document.getElementById('<%=fuSolicitud.ClientID%>');
            var vlc_NombreArchivo = vlo_InputArchivo.value;
            var vlc_NombreCorto = " ";
            var vlc_Limiter = '\\';
            var vlc_Extension = '';
            var vln_Indice = 0;
            var vln_IndiceExtension = 0;
            var vlc_LimiterExtension = '.';
            var vln_limiteTamArchivo = '<%=Me.TamanoArchivoOficio%>';
            var vln_TamArchivo;
            var vln_TamBytes = vln_limiteTamArchivo * <%=Utilerias.OrdenesDeTrabajo.Constantes.TAMANNO_BYTES_A_MEGAS%>;
            var vlc_Llaves;
           
            var vln_TamanoNombre = <%=Utilerias.OrdenesDeTrabajo.Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.NOMBRE_ARCHIVO_BD_TAMANO%>;
          
            if(vlo_InputArchivo.value != ''){
                
                vln_Indice =  vlc_NombreArchivo.lastIndexOf(vlc_Limiter) + 1;
                vlc_NombreCorto = vlc_NombreArchivo.substr(vln_Indice, vlc_NombreArchivo.length - vln_Indice);
                vln_IndiceExtension =  vlc_NombreArchivo.lastIndexOf(vlc_LimiterExtension) + 1;
                vlc_Extension = vlc_NombreArchivo.substr(vln_IndiceExtension, vlc_NombreArchivo.length);    
                
                vlc_ExtencionesPerimtidas = '<%=Me.ExtensionesPermitidasOficio%>';
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

        function validaArchivoRespuesta() {
            var vlo_InputArchivo = document.getElementById('<%=fuRespuesta.ClientID%>');
            var vlc_NombreArchivo = vlo_InputArchivo.value;
            var vlc_NombreCorto = " ";
            var vlc_Limiter = '\\';
            var vlc_Extension = '';
            var vln_Indice = 0;
            var vln_IndiceExtension = 0;
            var vlc_LimiterExtension = '.';
            var vln_limiteTamArchivo = '<%=Me.TamanoArchivoOficio%>';
             var vln_TamArchivo;
             var vln_TamBytes = vln_limiteTamArchivo * <%=Utilerias.OrdenesDeTrabajo.Constantes.TAMANNO_BYTES_A_MEGAS%>;
             var vlc_Llaves;
           
             var vln_TamanoNombre = <%=Utilerias.OrdenesDeTrabajo.Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.NOMBRE_ARCHIVO_BD_TAMANO%>;
          
                     if(vlo_InputArchivo.value != ''){
                
                         vln_Indice =  vlc_NombreArchivo.lastIndexOf(vlc_Limiter) + 1;
                         vlc_NombreCorto = vlc_NombreArchivo.substr(vln_Indice, vlc_NombreArchivo.length - vln_Indice);
                         vln_IndiceExtension =  vlc_NombreArchivo.lastIndexOf(vlc_LimiterExtension) + 1;
                         vlc_Extension = vlc_NombreArchivo.substr(vln_IndiceExtension, vlc_NombreArchivo.length);    
                
                         vlc_ExtencionesPerimtidas = '<%=Me.ExtensionesPermitidasOficio%>';
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

        function mostrarPopUp(pvc_IdPopup) {
            window.location = pvc_IdPopup;
        };

        function ocultarFiltroMateriales() {
            window.location = '#CerrarPopUpBusquedaMateriales';
        };

        function inicializarFormulario() {

            $(window).keydown(function a(e) {

                if (e.keyCode == 13) {
                    return false;
                }
            });

            cargarLupa();

            $('[data-tipo="tooltipExtensiones"]').attr('src', '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Icono_Ayuda.png")%>');

            permutarImagenes('#imgBuscarMaterial',
               '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Lupa.png")%>',
                '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Lupa.png")%>',
                '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Lupa.png")%>'
            );
        };

        function regresarAlListado() {
            window.location = 'Lst_OT_RevisionRequisicionMateriales.aspx';
        };

        $(document).ready(function () {
            inicializarFormulario();

            $('#btnRegresar').click(function () {
                regresarAlListado();
            });

            $('[data-tipo="tooltip"]').each(function () {
                $('#' + this.id).tooltipster({
                    interactive: true,
                    interactiveTolerance: 400,
                    theme: 'tooltipster-light'
                });
            });

            $('[data-tipo="borrarRegistro"]').click(function () { return mostrarPopupConfirmacionDeseaBorrarRegistro($(this).data("uniqueid")); });

            configurarLongitudMaximaTexto('#<%=Me.txtCodigo.ClientID%>','<%=Utilerias.OrdenesDeTrabajo.Modelo.OTT_DETALLE_MATERIAL.ID_MATERIAL_BD_TAMANO%>');
             
             $('[data-tipo="borrarRegistroSolicitud"]').click(function () { return confirmacionEliminarArchivoSolicitud(); });

             $('[data-tipo="borrarRegistroRespuesta"]').click(function () { return confirmacionEliminarArchivoRespuesta(); });

             permutarImagenes('#<%=Me.btnEliminarArchivoRespuesta.ClientID%>',
                 '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Borrar.png")%>',
                '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Icono_Borrar.png")%>',
                '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Borrar.png")%>'
            );

             permutarImagenes('#<%=Me.btnEliminarArchivoSolicitud.ClientID%>',
                 '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Borrar.png")%>',
                '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Icono_Borrar.png")%>',
                '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Borrar.png")%>'
            );


         });

         function ocultarFiltroFuncionario() {
             window.location = '#CerrarPopUpBusquedaFuncionario';
         };

         function cargarLupa() {
             permutarImagenes('#imgBuscarMaterial',
                 '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Lupa.png")%>',
                '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Lupa.png")%>',
                '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Lupa.png")%>'
            );

                     permutarImagenes('#imgBuscarMaterial',
                        '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Lupa.png")%>',
                '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Lupa.png")%>',
                '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Lupa.png")%>'
            );
        };

        function mostrarAlertSinCantidadDisponible() {
            mostrarAlerta(
                '#arAlertasDelFormulario',
                {
                    mensaje: "La bodega o almacén seleccionado no posee suficiente material",
                    tipo: "advertencia",
                    transparencia: 0.9,
                    posicion: 'center',
                    permiteCerrar: true
                }
                );
        };

        function mostrarAlertaGuardadoExitoso() {
            deshabilitarControl('#<%=btnAceptar.ClientID%>');
            $('.formulario').attr('disabled', 'disabled');

            mostrarAlerta(
            '#arAlertasDelFormulario',
            {
                mensaje: 'Se han actulizado los datos indicados para la requisición.',
                tipo: "exito",
                transparencia: 0.9,
                posicion: 'center',
                onClosed: function () { window.location = 'Lst_OT_RevisionRequisicionMateriales.aspx'; }
            });
        };

        function mostrarAlertaNoEncontrado() {
            mostrarAlerta(
                '#arAlertasDelFormulario',
                {
                    mensaje: "No se han encontrado materiales con el código indicado",
                    tipo: "advertencia",
                    transparencia: 0.9,
                    posicion: 'center',
                    permiteCerrar: true
                }
                );

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

        function confirmacionEliminarArchivoSolicitud() {
            var vlo_ConfiguracionPopup = {
                titulo: 'Revisión de requisiciones',
                mensaje: '¿Desea borrar el archivo seleccionado?',
                botones:
                        [
                            {
                                idControl: "btnSi",
                                textoBoton: "Si",
                                onClick:
                                    function (e) {
                                        $(this).attr("disabled", "disabled");
                                        __doPostBack('<%=btnEliminarArchivoSolicitud.UniqueID%>', '');
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

                         function confirmacionEliminarArchivoRespuesta() {
                             var vlo_ConfiguracionPopup = {
                                 titulo: 'Revisión de requisiciones',
                                 mensaje: '¿Desea borrar el archivo seleccionado?',
                                 botones:
                                         [
                                             {
                                                 idControl: "btnSi",
                                                 textoBoton: "Si",
                                                 onClick:
                                                     function (e) {
                                                         $(this).attr("disabled", "disabled");
                                                         __doPostBack('<%=btnEliminarArchivoRespuesta.UniqueID%>', '');
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

                                         function cargarTootipDetalles() {

                                             $('[data-tipo="tooltip"]').each(function () {
                                                 $('#' + this.id).tooltipster({
                                                     interactive: true,
                                                     interactiveTolerance: 400,
                                                     theme: 'tooltipster-light'
                                                 });
                                             });

                                         };

    </script>

</asp:Content>

