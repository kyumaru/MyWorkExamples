<%@ Page Language="VB" MaintainScrollPositionOnPostback="true" AutoEventWireup="false" MasterPageFile="~/MasterPage/Mp_Formulario.master" CodeFile="Frm_OT_GestionContratacionProfesional.aspx.vb" Inherits="OrdenesDeTrabajo_Frm_OT_GestionContratacionProfesional" %>

<%@ Register TagName="InformacionGeneral" TagPrefix="wuc" Src="~/ControlesDeUsuario/Wuc_OT_InformacionGeneral.ascx" %>
<%@ Register TagPrefix="wuc" TagName="Aclaraciones" Src="~/Controles/wuc_OT_Aclaraciones.ascx" %>
<%@ Register TagName="Ofertas" TagPrefix="wuc" Src="~/Controles/wuc_OT_Ofertas.ascx" %>

<asp:Content runat="server" ID="Content1" ContentPlaceHolderID="cphFormulario">

    <header>
        <h2>
            Proceso de contratación
        </h2>
    </header>

    <article class="tituloSeccion">
        Información general de la OT
    </article>

    <wuc:InformacionGeneral runat="server" ID="ctrl_InfoGeneral" />

    <article class="formulario">

        <ul class="encabezadoTabPanel">
            <li id="liVisita" runat="server" class="activo"><a class="tituloTabPanel" href="#tpVisitaTécnica">Visita Técnica</a></li>
            <li id="liAclaraciones" runat="server"><a class="tituloTabPanel" href="#tpAclaraciones">Aclaraciones</a></li>
            <li id="liRecomendacion" runat="server"><a class="tituloTabPanel" href="#tpRecomendaciónTécnica">Recomendación Técnica</a></li>
        </ul>

        <article class="cuerpoTabPanel"><article id="tpVisitaTécnica" runat="server" class="tabPanel activo">
            <table>
            <tr>
                <th>Acta de visita técnica</th>
                <td>
                    <asp:FileUpload runat="server" ID="fuActaVisita" onchange="validaArchivo();" />
                    <asp:Button runat="server" ID="btnAgregarActa" ValidationGroup="AgregarDoc" onclick="btnAgregarActa_Click" text="Agregar"/>
                    <asp:RequiredFieldValidator runat="server" ID="rfvFuActaVisita" ControlToValidate="fuActaVisita" ValidationGroup="AgregarDoc" Display="Dynamic" ErrorMessage="El acta de visita es obligatoria.">&nbsp;</asp:RequiredFieldValidator>                              
                    <asp:LinkButton runat="server" ID="lnkArchivoActa" Visible="false" OnCommand="lnkArchivoActa_Command"></asp:LinkButton>&nbsp;&nbsp;                        
                    <asp:ImageButton ID="btnEliminarActa" runat="server" ToolTip="Borrar" data-tipo="borrarRegistro" Visible="false"/>
                    <img runat="server" ID="imgExtensiones" data-tipo="tooltipExtension" class="tooltip"/>
                </td>
            </tr>
        </table>

            <article class="areaBotones">
                <asp:Button runat="server" Visible="false" ID="btnAceptar" OnClick="btnAceptar_Click" Text="Aceptar" />
            </article>

           
        </article></article>

        <article class="cuerpoTabPanel"><article id="tpAclaraciones" runat="server" class="tabPanel">

            <wuc:Aclaraciones runat="server" id="ctrl_Aclaraciones" ></wuc:Aclaraciones>

        </article></article>
        <article class="cuerpoTabPanel"><article id="tpRecomendaciónTécnica" runat="server" class="tabPanel">

            <wuc:Ofertas runat="server" ID="ctrl_OfertasProfesional" />

        </article></article>

    </article>
        
    <article id="arAlerta"></article>
    <article id="arAlertasDelFormulario"></article>
    <article id="arpopupConfirmaDeseaContinuar"></article>
    <article id="arpopupConfirmaDeseaBorrar"></article>
    <article id="popupConfirmacionDeseaBorrar"></article>

    <script type="text/javascript" >

        $(document).ready(function () {
            /*Control TabPanel*/
            configurarTabPanel();

            $('[data-tipo="tooltipExtension"]').attr('src', '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Icono_Ayuda.png")%>');

            $('[data-tipo="borrarRegistro"]').click(function () { return mostrarPopupConfirmacionDeseaBorrarRegistro($(this).data("uniqueid")); }); //busca cualquier control con ese data tipo
            $('[data-tipo="borrarRegistro"]').attr('src', '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Borrar.png")%>');
            $('[data-tipo="borrarRegistro"]').on({
                'mouseover': function () { $(this).attr('src', '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Icono_Borrar.png")%>'); },

                'mouseout': function () { $(this).attr('src', '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Borrar.png")%>'); }
            });

            $('[data-tipo="volverAlListado"]').click(function () {
                regresarAlListado();
            });

            $('#<%=Me.liVisita.ClientID%>').click(function () {
                activarVisitaTecnica();
            });

            $('#<%=Me.liAclaraciones.ClientID%>').click(function () {
                activarAclaraciones();
            });

            $('#<%=Me.liRecomendacion.ClientID%>').click(function () {
                activarRecomendacion()
            });
        });

        function activarRecomendacion(){
            $('#<%=Me.liAclaraciones.ClientID%>').removeClass('activo');
            $('#<%=Me.liRecomendacion.ClientID%>').addClass('activo');
            $('#<%=Me.liVisita.ClientID%>').removeClass('activo');
            $('#<%=Me.tpVisitaTécnica.ClientID%>').removeClass('activo');
            $('#<%=Me.tpAclaraciones.ClientID%>').removeClass('activo');
            $('#<%=Me.tpRecomendaciónTécnica.ClientID%>').addClass('activo');
        };

        function activarAclaraciones(){
            $('#<%=Me.liAclaraciones.ClientID%>').addClass('activo');
            $('#<%=Me.liRecomendacion.ClientID%>').removeClass('activo');
            $('#<%=Me.liVisita.ClientID%>').removeClass('activo');
            $('#<%=Me.tpVisitaTécnica.ClientID%>').removeClass('activo');
            $('#<%=Me.tpAclaraciones.ClientID%>').addClass('activo');
            $('#<%=Me.tpRecomendaciónTécnica.ClientID%>').removeClass('activo');
        }

        function activarVisitaTecnica(){
            $('#<%=Me.liAclaraciones.ClientID%>').removeClass('activo');
            $('#<%=Me.liRecomendacion.ClientID%>').removeClass('activo');
            $('#<%=Me.liVisita.ClientID%>').addClass('activo');
            $('#<%=Me.tpVisitaTécnica.ClientID%>').addClass('activo');
            $('#<%=Me.tpAclaraciones.ClientID%>').removeClass('activo');
            $('#<%=Me.tpRecomendaciónTécnica.ClientID%>').removeClass('activo');
        }

        function regresarAlListado() {
            window.location = '../OrdenesDeTrabajo/Lst_OT_GestionProfesionalesDisenio.aspx';
        };

        function mostrarAlertaError(pvc_Msj) {
            mostrarAlerta(
                '#arAlerta',
                {
                    mensaje: pvc_Msj,
                    tipo: "peligro",
                    transparencia: 0.9,
                    posicion: 'center',
                    permiteCerrar: true
                }
            );
        };

        function mostrarAlertaGuardadoExitoso() {
            mostrarAlerta(
            '#arAlerta',
            {
                mensaje: 'Se han guardado los datos indicados para la contratación',
                tipo: "exito",
                transparencia: 0.9,
                posicion: 'center',
                onClosed: function () { regresarAlListado(); }
            });
        };

        function mostrarPopupConfirmaDeseaBorrarRegistro() {
            var vlo_ConfiguracionPopup = {
                titulo: 'Gestion de Contrataciones - Visita Técnica',
                mensaje: '¿Desea borrar el archivo seleccionado?',
                botones:
                        [
                            {
                                idControl: "btnSi",
                                textoBoton: "Si",
                                onClick:
                                    function (e) {
                                        $(this).attr("disabled", "disabled");
                                        __doPostBack('<%=btnEliminarActa.UniqueID%>', '');
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

        function mostrarPopupConfirmacionDeseaBorrarRegistro(pvc_UniqueIdControl) {
            var vlo_ConfiguracionPopup = {
                titulo: "<em>Gestion de Contratación</em>",
                mensaje: "¿Realmente desea borrar el archivo seleccionado?",
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

        function validaArchivo() {
            var vlo_InputArchivo = document.getElementById('<%=fuActaVisita.ClientID%>');
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


    </script>
</asp:Content>