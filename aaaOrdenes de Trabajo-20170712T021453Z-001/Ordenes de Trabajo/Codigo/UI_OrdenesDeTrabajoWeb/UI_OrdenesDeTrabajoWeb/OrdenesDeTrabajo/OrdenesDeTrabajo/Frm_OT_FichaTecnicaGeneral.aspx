<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/Mp_Formulario.master" AutoEventWireup="false" CodeFile="Frm_OT_FichaTecnicaGeneral.aspx.vb" Inherits="OrdenesDeTrabajo_Frm_OT_FichaTecnicaGeneral" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphFormulario" runat="Server">

    <header>
        <h2>
            <asp:Label runat="server" ID="lblAccion" Text=" Ficha Técnica - Datos Generales"></asp:Label>
        </h2>
    </header>

    <article class="tituloSeccion">
        Requerimientos de Mobiliario
    </article>

    <article class="formulario">
        <table>
            <tr>
                <th>¿Cuenta con presupuesto?</th>
                <td>
                    <asp:RadioButton ID="rbSiPresup" runat="server" GroupName="grpPresupuesto" Text="Si" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
	                <asp:RadioButton ID="rbNoPresup" runat="server" GroupName="grpPresupuesto" Text="No" />
                </td>
            </tr>
            <tr>
                <th>¿Requieren conservar el mobiliario existente?</th>
                <td>
                    <asp:RadioButton ID="rdbSiMe" runat="server" GroupName="grpMobiliarioExistente" Text="Si" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
	                <asp:RadioButton ID="rdbNoMe" runat="server" GroupName="grpMobiliarioExistente" Text="No" />
                </td>
            </tr>
            <tr>
                <th>Requiere mobiliario nuevo?</th>
                <td>
                    <asp:RadioButton ID="rdbSiMo" runat="server" GroupName="grpMobiliarioNuevo" Text="Si" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
	                <asp:RadioButton ID="rdbNoMo" runat="server" GroupName="grpMobiliarioNuevo" Text="No" />
                </td>
            </tr>
           <%-- <tr>
                <th>Otros (especifique)</th>
                <td>
                    <asp:TextBox runat="server" ID="txtOtros" TextMode="MultiLine" Rows="5" Width="50%"></asp:TextBox>
                </td>
            </tr>--%>
            <tr>
                <th>Otro tipo de requerimientos</th>
                <td>
                    <asp:TextBox runat="server" ID="txtOtrosReq" TextMode="MultiLine" Rows="5" Width="50%"></asp:TextBox>
                </td>
            </tr>
        </table>
    </article>

    <article>
        <asp:Label ID="lblInfo" runat="server" Text="<br />   Adjuntar una lista del equipo que posea la Unidad, tanto de su estado actual como del equipo que se proyecte adquirir a corto plazo." Style="font-weight: bold;"></asp:Label>
    </article>

    <article class="formulario sinBorde">
        <table>
            <tr>
                <th>Archivo</th>
                <td>
                    <asp:FileUpload Width="50%" runat="server" ID="ifArchivo" onchange="validaArchivo()"  Visible="false"/>
                    <asp:LinkButton runat="server" ID="lnkArchivo" Visible="false" Style="text-decoration: underline;"></asp:LinkButton>&nbsp;&nbsp;                        
                    <asp:ImageButton ID="btnEliminarArchivo" runat="server" ToolTip="Borrar" OnClientClick="return confirmacionEliminarArchivo();" Visible="false"/>
                </td>
            </tr>
        </table>
    </article>

    <article class="tituloSeccion">
        Alarmas
    </article>

    <article class="formulario sinBorde">
        <table>
            <tr>
                <th>¿Cuenta con alarma?</th>
                <td>
                    <asp:RadioButton ID="rdbSiCa" runat="server" GroupName="grpCuentaAlarma" Text="Si" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
	                <asp:RadioButton ID="rdbNoCa" runat="server" GroupName="grpCuentaAlarma" Text="No" />
                </td>
            </tr>
            <tr>
                <th>¿Requiere alarma?</th>
                <td>
                    <asp:RadioButton ID="rdbSiRa" runat="server" GroupName="grpRequiereAlarma" Text="Si" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
	                <asp:RadioButton ID="rdbNoRa" runat="server" GroupName="grpRequiereAlarma" Text="No" />
                </td>
            </tr>
        </table>
    </article>

    <article class="areaBotones">
        <input id="btnRegresar" type="button" value="Regresar" />
        <asp:Button runat="server" ID="btnAceptar" Text="Siguiente" OnClientClick="javascript:return validarGuardar();" />        
        <input id="btnCancelar" type="button" value="Cancelar" />        
    </article>

    <article style="visibility:hidden">
        <asp:Button runat="server" ID="btnAceptarOculto" Text="Guardar" />
    </article>

    <article id="arPopupDelFormulario"></article>
    <article id="arAlertasDelFormulario"></article>
    <article id="arPopupGenerico"></article>
    <article id="arpopupConfirmaDeseaBorrar"></article>

    <script type="text/javascript">

        function validarGuardar(){
             document.getElementById('<%=btnAceptar.ClientID%>').disabled = "true";           
            __doPostBack('<%=Me.btnAceptarOculto.UniqueID%>', '');
             return false;
         }

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
            var vln_TamMegas = vln_limiteTamArchivo / 1048576;
           
            var vln_TamanoNombre = <%=Utilerias.OrdenesDeTrabajo.Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.NOMBRE_ARCHIVO_BD_TAMANO%>;
          
             if(vlo_InputArchivo.value != ''){
                
                 vln_Indice =  vlc_NombreArchivo.lastIndexOf(vlc_Limiter) + 1;
                 vlc_NombreCorto = vlc_NombreArchivo.substr(vln_Indice, vlc_NombreArchivo.length - vln_Indice);
                 vln_IndiceExtension =  vlc_NombreArchivo.lastIndexOf(vlc_LimiterExtension) + 1;
                 vlc_Extension = vlc_NombreArchivo.substr(vln_IndiceExtension, vlc_NombreArchivo.length);                         
                
                 if (vlc_NombreCorto.length > vln_TamanoNombre){
                     mostrarAlertaError('El nombre del archivo es demasiado largo','');
                     vlo_InputArchivo.value = "";
                     return false;
                 }
                
                 vln_TamArchivo = vlo_InputArchivo.files[0].size;
                 if (vln_TamArchivo > vln_limiteTamArchivo){
                     mostrarAlertaError('El tamaño del archivo excede el máximo permitido.','');
                     vlo_InputArchivo.value = "";
                     return false;
                 }

                 return true;
             }
             return false;
         };

         function mostrarAlertaNoEncontrado() {
             mostrarAlerta(
                 '#arAlertasDelFormulario',
                 {
                     mensaje: "No se han encontrado ficha técnica con el las llaves indicadas",
                     tipo: "advertencia",
                     transparencia: 0.9,
                     posicion: 'center',
                     permiteCerrar: true
                 }
                 );
         };

         function regresarAlListado() {
             window.location = '<%=Me.PantallaRetorno%>';
         };

         function irSeleccionEspacios(){
             window.location = 'Lst_OT_SeleccionEspacios.aspx';
         }

         function mostrarPopUp(pvc_IdPopup) {
             window.location = pvc_IdPopup;
         };

         function mostrarAlertaActualizacionExitosa() {
       
            irSeleccionEspacios();

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

        function mostrarAlertaLlaveIncorrecta() {
            deshabilitarControl('#<%=btnAceptar.ClientID%>');
            deshabilitarControl('#btnRegresar');
            deshabilitarControl('#btnCancelar');
            $('.formulario').attr('disabled', 'disabled');

            mostrarAlerta(
                '#arAlertasDelFormulario',
                {
                    mensaje: 'El identificador provisto no pertenece a ningun registro del sistema.',
                    tipo: "peligro",
                    transparencia: 0.9,
                    posicion: 'center',
                    onClosed: function () { regresarAlListado(); }
                });
        };

        $(document).ready(function () {
            $('#btnCancelar').click(function () {
                regresarAlListado();
            
            });

            $('#btnRegresar').click(function () {
                regresarAlListado();
            
            });

            permutarImagenes('#<%=btnEliminarArchivo.ClientID%>',
                '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Borrar.png")%>',
                '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Icono_Borrar.png")%>',
                '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16,AdministradorRecursos.COLOR_IMAGEN.GRIS,"Icono_Borrar.png")%>'
            );             

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
                                        __doPostBack('<%=btnEliminarArchivo.UniqueID%>', '');
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

    </script>

</asp:Content>

