<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/Mp_Formulario.master" AutoEventWireup="false" CodeFile="Frm_OT_OrdenTrabajo.aspx.vb" Inherits="OrdenesDeTrabajo_Frm_OT_OrdenTrabajo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphFormulario" runat="Server">

    <header>
        <h2>
            <asp:Label runat="server" ID="lblAccion"></asp:Label>
        </h2>
    </header>

    <article class="tituloSeccion">
        Mantenimiento de la Orden de Trabajo
    </article>

    <article class="formulario" id="formularioTotal" runat="server">
        <table>
            <tr>
                <th>Solicitante</th>
                <td>
                    <asp:Label runat="server" ID="lblSolicitante" data-tipocontrol="etiqueta"></asp:Label>
                </td>
            </tr>
            <tr runat="server" id="trUnidad">
                <th>Unidad que Aprueba</th>
                <td>
                    <asp:DropDownList runat="server" ID="ddlUnidad" data-tipocontrol="combo" AppendDataBoundItems="true" AutoPostBack="true"></asp:DropDownList>
                    <asp:RequiredFieldValidator runat="server" ID="rfvDdlUnidad" ControlToValidate="ddlUnidad" Display="Dynamic" ErrorMessage="La unidad que aprueba es obligatoria." ValidationGroup="Aceptar"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr runat="server" id="trUnidadLabel">
                <th>Unidad que Aprueba</th>
                <td>
                    <asp:Label runat="server" ID="lblNombreUnidad" data-tipocontrol="etiqueta"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>Persona Contacto</th>
                <td>
                    <asp:TextBox runat="server" ID="txtPersonaContacto" data-tipocontrol="texto" TextMode="MultiLine" Rows="1" Width="59%"></asp:TextBox>
                    <img class="tooltip" src="<%= AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Icono_Ayuda.png")%>"
                        title="Nombre de la persona que podrá ser contactada para brindar información con respecto a la orden de trabajo solicitada." />
                </td>
            </tr>
            <tr>
                <th>Teléfono</th>
                <td>
                    <asp:TextBox runat="server" ID="txtTelefono" data-tipocontrol="texto" MaxLength="10" Width="59%"></asp:TextBox>
                    <ajax:FilteredTextBoxExtender ID="ftbTxtTelefono" runat="server" FilterType="Numbers, Custom" TargetControlID="txtTelefono" ValidChars="-()+"></ajax:FilteredTextBoxExtender>
                </td>
            </tr>
            <tr>
                <th>Edificio o Sitio</th>
                <td>
                    <asp:UpdatePanel runat="server" ID="upLugarTrabajo" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:DropDownList runat="server" ID="ddlLugarTrabajo" data-tipocontrol="combo" AppendDataBoundItems="true" Width="59%"></asp:DropDownList>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlUnidad" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                    <asp:RequiredFieldValidator runat="server" ID="rfvDdlLugarTrabajo" ControlToValidate="ddlLugarTrabajo" Display="Dynamic" ErrorMessage="Edificio o Sitio es obligatorio." ValidationGroup="Aceptar"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <th>Lugar Exacto</th>
                <td>
                    <asp:TextBox runat="server" ID="txtLugarExacto" data-tipocontrol="texto" TextMode="MultiLine" Rows="4" Width="59%"></asp:TextBox>
                    <img class="tooltip" src="<%= AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Icono_Ayuda.png")%>"
                        title="Señas exactas del lugar donde se requiere la realización del trabajo." />
                    <asp:RequiredFieldValidator runat="server" ID="rfvTxtLugarExacto" ControlToValidate="txtLugarExacto" Display="Dynamic" ErrorMessage="Lugar Exacto es obligatorio." ValidationGroup="Aceptar"></asp:RequiredFieldValidator>
                    <span id="spContadorTxtLugarExacto" class="contadorCaracteresRestantes"></span>
                </td>
            </tr>
            <tr>
                <th>Categoría</th>
                <td>
                    <asp:DropDownList runat="server" ID="ddlCategoria" data-tipocontrol="combo" AppendDataBoundItems="true" AutoPostBack="true" Width="59%"></asp:DropDownList>
                    <asp:RequiredFieldValidator runat="server" ID="rfvDdlCategoria" ControlToValidate="ddlCategoria" Display="Dynamic" ErrorMessage="Categoría es obligatoria." ValidationGroup="Aceptar"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr runat="server" id="trTallerSector">
                <th>Taller o Sector al que pertenece</th>
                <td>
                    <asp:Label runat="server" ID="lblTallerSector"></asp:Label>
                </td>
            </tr>
            <tr runat="server" id="trResponsable">
                <th>Responsable</th>
                <td>
                    <asp:Label runat="server" ID="lblResponsable"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>Actividad</th>
                <td>
                    <asp:UpdatePanel runat="server" ID="upActividad" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:DropDownList Width="59%" runat="server" ID="ddlActividad" data-tipocontrol="combo" AppendDataBoundItems="true" AutoPostBack="true"></asp:DropDownList>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlCategoria" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                    <asp:RequiredFieldValidator runat="server" ID="rfvDdlActividad" ControlToValidate="ddlActividad" Display="Dynamic" ErrorMessage="Actividad es obligatoria." ValidationGroup="Aceptar"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <th>&nbsp;</th>
                <td>
                    <asp:UpdatePanel runat="server" ID="upTxtDescripcionActividad" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:TextBox Width="59%" runat="server" ID="txtDescripcionActividad" TextMode="MultiLine" Rows="4" Columns="40" Enabled="false"></asp:TextBox>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlActividad" EventName="SelectedIndexChanged" />
                            <asp:AsyncPostBackTrigger ControlID="ddlCategoria" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <th>Descripción de Trabajo</th>
                <td>
                    <asp:TextBox runat="server" ID="txtDescTrabajo" data-tipocontrol="texto" TextMode="MultiLine" Rows="9" Width="59%"></asp:TextBox>
                    <img class="tooltip" src="<%= AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Icono_Ayuda.png")%>"
                        title="Detalle amplio y suficiente del trabajo requerido." />
                    <asp:RequiredFieldValidator runat="server" ID="rfvTxtDescTrabajo" ControlToValidate="txtDescTrabajo" Display="Dynamic" ErrorMessage="Descripción de trabajo es obligatoria." ValidationGroup="Aceptar"></asp:RequiredFieldValidator>
                    <span id="spContadorTxtDescTrabajo" class="contadorCaracteresRestantes"></span>
                </td>
            </tr>
            
            <tr runat="server" id="trArchivo">
                <th>Fotografía(s)</th>
                <td>
                    <asp:FileUpload Width="59%" runat="server" ID="ifInfo" onchange="validaArchivo()" />
                    <asp:RequiredFieldValidator runat="server" ID="rfvIfInfo" ControlToValidate="ifInfo" ValidationGroup="Archivos" Display="Dynamic" ErrorMessage="Agregue un documento.">&nbsp;</asp:RequiredFieldValidator>
                    <asp:Button runat="server" ID="btnAgregarArchivo" Text="Agregar" ValidationGroup="Archivos" />
                    <img runat="server" data-tipo="tooltipExtensiones" class="tooltip" id="imgExtensiones" />
                </td>
            </tr>
            <tr>
                <th>&nbsp;</th>
                <td>
                    <br />
                    <article class="listado sinBorde">
                        <asp:Repeater runat="server" ID="rpAdjunto">
                            <HeaderTemplate>
                                <table>
                                    <tr>
                                        <th>&nbsp;</th>
                                        <th>Adjunto</th>
                                    </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr class="lineaDelListado">
                                    <td>
                                        <asp:ImageButton runat="server" ID="ibBorrar" data-tipo="borrarRegistro" AlternateText="Borrar"
                                            CommandArgument='<%# String.Format("{0}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.ID_ADJUNTO_ORDEN_TRABAJO))%>' OnClick="ibBorrar_Click"
                                            CommandName='<%#Container.ItemIndex%>'
                                            Visible='<%# IIf(Me.Operacion = Utilerias.OrdenesDeTrabajo.eOperacion.Consultar, False, True)%>'
                                            ImageUrl='<%#AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Borrar.png")%>'
                                            onmouseover='<%# String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Icono_Borrar.png"))%>'
                                            onmouseout='<%# String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Borrar.png"))%>' />
                                    </td>
                                    <td>
                                        <asp:LinkButton runat="server" ID="lnkArchivo"
                                            CommandArgument='<%#Container.ItemIndex%>'
                                            Style="text-decoration: underline; color: blue;"
                                            OnCommand="lnkArchivo_Command"
                                            Text="<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.NOMBRE_ARCHIVO)%>"></asp:LinkButton>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                </table>
                            </FooterTemplate>
                        </asp:Repeater>
                    </article>
                </td>
            </tr>
        </table>
    </article>

    <article class="areaBotones">
        <asp:UpdatePanel runat="server" ID="upAceptarEnviar" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:Button runat="server" ID="btnAceptar" Text="Guardar" ValidationGroup="Aceptar" OnClientClick="javascript:return validarGuardar();" />
                <asp:Button runat="server" ID="btnAceptarEnviar" Text="Aceptar y Enviar" ValidationGroup="Aceptar" OnClientClick="javascript:return validarGuardarEnviar();"/>
                <asp:Button runat="server" OnClick="btnCancelar_Click" ID="btnCancelar" Text="Cancelar" />
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ddlCategoria" EventName="SelectedIndexChanged" />
            </Triggers>
        </asp:UpdatePanel>
    </article>

    <article style="visibility:hidden">
        <asp:Button runat="server" ID="btnAceptarOculto" Text="Guardar" ValidationGroup="Aceptar" />
         <asp:Button runat="server" ID="btnAceptarEnviarOculto" Text="Aceptar y Enviar" ValidationGroup="Aceptar" />
    </article>

    <article id="arPopupDelFormulario"></article>
    <article id="arAlertasDelFormulario"></article>
    <article id="arpopupConfirmaDeseaBorrar"></article>
    <article id="arPopupGenerico"></article>

    <script type="text/javascript">

        function validarGuardarModificar(){
            if(Page_ClientValidate("Aceptar"))  {
                document.getElementById('<%=btnAceptar.ClientID%>').disabled = "true";
            }
            __doPostBack('<%=Me.btnAceptarOculto.UniqueID%>', '');
              return false;
          };

        function validarGuardarWizard(){
                        
            if(Page_ClientValidate("Aceptar"))  {
                document.getElementById('<%=btnAceptar.ClientID%>').disabled = "true";                       
            }
            __doPostBack('<%=Me.btnAceptarOculto.UniqueID%>', '');
            return false;
        };

        function validarGuardar(){
                        
            if(Page_ClientValidate("Aceptar"))  {
                document.getElementById('<%=btnAceptar.ClientID%>').disabled = "true";
                document.getElementById('<%=btnAceptarEnviar.ClientID%>').disabled = "true";
                       
            }
            __doPostBack('<%=Me.btnAceptarOculto.UniqueID%>', '');
        return false;
        };

        function validarGuardarEnviar(){
            if(Page_ClientValidate("Aceptar"))  {
                document.getElementById('<%=btnAceptar.ClientID%>').disabled = "true";
                document.getElementById('<%=btnAceptarEnviar.ClientID%>').disabled = "true";
            }

            __doPostBack('<%=Me.btnAceptarEnviarOculto.UniqueID%>', '');
            return false;
        }

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

        function regresarAlListado() {
            window.location = 'Lst_OT_OrdenTrabajo.aspx';
        };

        function mostrarPopupRegistroExitoso() {
            var vlo_ConfiguracionPopup = {
                titulo: 'Catálogo de Ordenes de Trabajo',
                mensaje: 'Se ha registrado la información.. <br/><strong>¿Desea registrar otra Orden?</strong>',
                onClosed: function () { regresarAlListado(); },
                botones:
                [
                    {
                        idControl: "btnSi",
                        textoBoton: "Sí",
                        onClick: function () { window.location = 'Frm_OT_OrdenTrabajo.aspx?pvn_Operacion=<%=Utilerias.OrdenesDeTrabajo.eOperacion.Agregar%>'; }
                    },
                    {
                        idControl: "btnNo",
                        textoBoton: "No",
                        onClick: function () { regresarAlListado(); }
                    }
                ]
            };

                $('#arPopupDelFormulario').popup(vlo_ConfiguracionPopup);

                window.location = '#arPopupDelFormulario';
            };

            function mostrarAlertaWizard(pvc_Mensaje) {
                Wizard(); 
            };

            function Wizard(){
                window.location = 'Frm_OT_FichaTecnicaGeneral.aspx?pvn_Operacion=<%=Utilerias.OrdenesDeTrabajo.eOperacion.Agregar%>';
            };
                
            function mostrarAlertaActualizacionExitosa() {
                deshabilitarControl('#<%=btnAceptar.ClientID%>');
                deshabilitarControl('#<%=btnCancelar.ClientID%>');
                $('.formulario').attr('disabled', 'disabled');

                mostrarAlerta(
                '#arAlertasDelFormulario',
                {
                    mensaje: 'Se ha actualizado la información de la orden.',
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

            function mostrarAlertaLlaveIncorrecta() {
                deshabilitarControl('#<%=btnAceptar.ClientID%>');
                deshabilitarControl('#<%=btnAceptarEnviar.ClientID%>');
                deshabilitarControl('#<%=btnCancelar.ClientID%>');
                $('.formulario').attr('disabled', 'disabled');

                mostrarAlerta(
                    '#arAlertasDelFormulario',
                    {
                        mensaje: 'El identificador provisto no pertenece a ninguna orden de trabajo.',
                        tipo: "peligro",
                        transparencia: 0.9,
                        posicion: 'center',
                        onClosed: function () { regresarAlListado(); }
                    });
            };

            function mostrarPopupConfirmaDeseaBorrarRegistro(pvo_UniqueIdControl) {
                var vlo_ConfiguracionPopup = {
                    titulo: 'Catálogo de Ordenes de Trabajo',
                    mensaje: '¿Desea borrar el registro seleccionado?',
                    botones:
                            [
                                {
                                    idControl: "btnSi",
                                    textoBoton: "Si",
                                    onClick:
                                        function (e) {
                                            $(this).attr("disabled", "disabled");
                                            __doPostBack(pvo_UniqueIdControl, '');
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

            $(document).ready(function () {

                $('[data-tipo="tooltipExtensiones"]').attr('src', '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Icono_Ayuda.png")%>');
                
                $('[data-tipo="borrarRegistro"]').click(function () { return mostrarPopupConfirmaDeseaBorrarRegistro($(this).data("uniqueid")); });

                habilitarTooltipGenerico();

                configurarLongitudMaximaTexto('#<%=Me.txtLugarExacto.ClientID%>',<%=Utilerias.OrdenesDeTrabajo.Modelo.OTT_ORDEN_TRABAJO.SENNAS_EXACTAS_BD_TAMANO%>);
                configurarContadorCaracteresRestantes('#<%=Me.txtLugarExacto.ClientID%>','#spContadorTxtLugarExacto');

                configurarLongitudMaximaTexto('#<%=Me.txtDescTrabajo.ClientID%>',<%=Utilerias.OrdenesDeTrabajo.Modelo.OTT_ORDEN_TRABAJO.DESCRIPCION_TRABAJO_BD_TAMANO%>);
                configurarContadorCaracteresRestantes('#<%=Me.txtDescTrabajo.ClientID%>','#spContadorTxtDescTrabajo');

            });

            function BotonCancelar(){
                $('#<%=btnCancelar.ClientID%>').click(function () {
                    regresarAlListado();
                });
            };

            function MensajePopup(pvc_Mensaje, pvc_PaginaDestino) {

                var vlo_ConfiguracionPopup = {
                    titulo: '<em>Mensajes del Sistema</em>',
                    mensaje: pvc_Mensaje,
                    onClosed: function (e) {
                        $(this).removeAttr('href');
                        cerrarPopup();
                        if (pvc_PaginaDestino != '') {
                            redireccionarListado(pvc_PaginaDestino);
                        }
                    },

                    botones:
                [
                    {
                        idControl: "btnAceptar",
                        textoBoton: "Aceptar",
                        onClick: function () {
                            cerrarPopup();
                            if (pvc_PaginaDestino != '') {
                                redireccionarListado(pvc_PaginaDestino);
                            }
                        }
                    }
                ]
                };

                $('#arPopupGenerico').popup(vlo_ConfiguracionPopup);

                window.location = '#arPopupGenerico';
            };

            function redireccionarListado(pvc_PaginaDestino) {
                window.location = pvc_PaginaDestino
            };
        
            function deshabilitar(){
                deshabilitarJerarquiaDeControles(document.getElementById('<%=formularioTotal.ClientID%>'));
                deshabilitarControl('#<%=btnAceptar.ClientID%>');
                deshabilitarControl('#<%=btnAceptarEnviar.ClientID%>');
                document.getElementById('#<%=btnCancelar.ClientID%>').value = "Regresar";
            };

    </script>

</asp:Content>

