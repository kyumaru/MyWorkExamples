<%@ Page Language="VB" MasterPageFile="~/MasterPage/Mp_Formulario.master" AutoEventWireup="false" CodeFile="Frm_OT_FinalizacionEntregaDis.aspx.vb" Inherits="OrdenesDeTrabajo_Frm_OT_FinalizacionEntregaDis" %>

<%@ Register TagName="InformacionGeneral" TagPrefix="wuc" Src="~/ControlesDeUsuario/Wuc_OT_InformacionGeneral.ascx" %>

<asp:Content runat="server" ID="Content1" ContentPlaceHolderID="cphFormulario">

    <header>
        <h2>
            Finalización y entrega de diseño
        </h2>
    </header>
    <article class="tituloSeccion">
        Información general de la OT
    </article>
    <wuc:InformacionGeneral runat="server" ID="ctrl_InfoGeneral" />

    <article class="tituloSeccion">
        Documentos
    </article>
    <article class="formulario">
        <table>
            <tr>
                <th>Clausula Penal</th>
                <td>
                    <asp:FileUpload runat="server" ID="fuClausula" onchange="validaArchivo()" />
                    <asp:Button runat="server" ID="btnAgregarClausula" ValidationGroup="AgregarClausula" text="Agregar"/>
                    <asp:RequiredFieldValidator runat="server" ID="rfvFuClausula" ControlToValidate="fuClausula" ValidationGroup="AgregarClausula" Display="Dynamic" ErrorMessage="Agregue un documento.">&nbsp;</asp:RequiredFieldValidator>                              
                    <asp:LinkButton runat="server" ID="lnkArchivoClausula" Visible="false" OnCommand="lnkArchivoClausula_Command"></asp:LinkButton>&nbsp;&nbsp;                        
                    <asp:ImageButton ID="btnEliminarClausula" runat="server" ToolTip="Borrar" data-tipo="borrarRegistro"
                        ImageUrl='<%#AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Borrar.png")%>'
                        onmouseover='<%#String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Icono_Borrar.png"))%>'
                        onmouseout='<%#String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Borrar.png"))%>'
                            OnClientClick="return confirmacionEliminarArchivo();" Visible="false"/>
                    <img runat="server" ID="imgExtensiones" data-tipo="tooltipExtensiones" class="tooltip"/>
                </td>
            </tr>
            <tr>
                <th>&nbsp;</th>
                <td><asp:LinkButton runat="server" ID="lnkExpTecnico" Text="Expediente Técnico"></asp:LinkButton></td>
            </tr>
            <tr>
                <th>&nbsp;</th>
                <td><asp:LinkButton runat="server" ID="lnkDesInicial" Text="Desición Inicial"></asp:LinkButton></td>
            </tr>
        </table>
    </article>

    <article class="areaBotones">
        <asp:Button runat="server" ValidationGroup="GYE" ID="btnGuardar" Text="Guardar" />
        <asp:Button runat="server" ValidationGroup="GYE" ID="btnGuardarYEnviar" text="Guardar y Enviar" />
        <asp:Button runat="server" ID="btnRegresar" text="Regresar" />
    </article>


    <article id="arAlertasDelFormulario"></article>
    <article id="arpopupConfirmaDeseaContinuar"></article>
    <article id="arpopupConfirmaDeseaBorrar"></article>

    <script type="text/javascript">

        $(document).ready(function () {

            $('[data-tipo="tooltipExtensiones"]').attr('src', '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Icono_Ayuda.png")%>');

            $('[data-tipo="borrarRegistro"]').click(function () { return mostrarPopupConfirmaDeseaBorrarRegistro($(this).data("uniqueid")); });

            $('[data-tipo="borrarRegistro"]').attr('src', '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Borrar.png")%>');

            $('[data-tipo="borrarRegistro"]').on({
                'mouseover': function () { $(this).attr('src', '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Icono_Borrar.png")%>'); },

                'mouseout': function () { $(this).attr('src', '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Borrar.png")%>'); }
            });

        });


        function confirmacionEliminarArchivo() {
            var vlo_ConfiguracionPopup = {
                titulo: 'Catálogo de Ficha Técnica',
                mensaje: '¿Desea borrar el archivo seleccionado?',
                botones:
                        [
                            {
                                idControl: "btnSi",
                                textoBoton: "Si",
                                onClick:
                                    function (e) {
                                        $(this).attr("disabled", "disabled");
                                        __doPostBack('<%=btnEliminarClausula.UniqueID%>', '');
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

        function validaArchivo() {
            var vlo_InputArchivo = document.getElementById('<%=fuClausula.ClientID%>');
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
                mensaje: 'Se ha guardado los datos indicados para la finalización y entrega de diseño',
                tipo: "exito",
                transparencia: 0.9,
                posicion: 'center',
                onClosed: function () { window.location='Lst_OT_GestionProfesionalesDisenio.aspx'; }
            });
        };


    </script>
</asp:Content>