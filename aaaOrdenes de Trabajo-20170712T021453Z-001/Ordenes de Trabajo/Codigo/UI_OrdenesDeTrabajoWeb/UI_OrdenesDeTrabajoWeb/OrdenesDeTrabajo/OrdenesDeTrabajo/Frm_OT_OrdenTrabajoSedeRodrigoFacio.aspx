<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/Mp_Formulario.master" AutoEventWireup="false" CodeFile="Frm_OT_OrdenTrabajoSedeRodrigoFacio.aspx.vb" Inherits="OrdenesDeTrabajo_Frm_OT_OrdenTrabajoSedeRodrigoFacio" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Src="~/Controles/wucEmpleadosEU.ascx" TagName="wuc_EmpleadosEU" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphFormulario" runat="Server">

    <header>
        <h2>
            <asp:Label runat="server" ID="lblAccion"></asp:Label>
        </h2>
    </header>

    <article class="tituloSeccion">
        Mantenimiento de la Orden de Trabajo a sede Rofrigo Facio
    </article>

    <article class="formulario" id="formularioTotal" runat="server">
        <table>
            <tr>
                <th>Registrada Por</th>
                <td>
                    <asp:Label runat="server" ID="lblRegistradaPor" data-tipocontrol="etiqueta"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>Fecha</th>
                <td>
                    <asp:Label runat="server" ID="lblFechaRegistro" data-tipocontrol="etiqueta"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>ID Solicitante</th>
                <td>
                    <asp:UpdatePanel runat="server" ID="upTxtIdSolicitante" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:TextBox Width="59%" runat="server" ID="txtIdSolicitante" data-tipocontrol="texto" AutoPostBack="true"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ID="rfvTxtIdSolicitante" ControlToValidate="txtIdSolicitante" Display="Dynamic" ErrorMessage="El número de Identificación del Solicitante es requerido." ValidationGroup="Aceptar">&nbsp;</asp:RequiredFieldValidator>
                            <asp:LinkButton ID="lnkEjecutarBusquedaSolicitante" runat="server">
                                <img id="imgBuscarSolicitante" title="Buscar Registro" alt="Buscar Registro" src="" />
                            </asp:LinkButton>
                            <br />
                            <span id="spContadorTxtIdSolicitante" class="contadorCaracteresRestantes"></span>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="txtIdSolicitante" EventName="TextChanged" />
                            <asp:AsyncPostBackTrigger ControlID="btnLimpiarFormulario" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <th>Nombre</th>
                <td>
                    <asp:UpdatePanel runat="server" ID="upLblNombre" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:Label runat="server" ID="lblNombre" data-tipocontrol="etiqueta"></asp:Label>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="txtIdSolicitante" EventName="TextChanged" />
                            <asp:AsyncPostBackTrigger ControlID="wuc_EmpleadosEU" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr runat="server" id="trUnidad">
                <th>Unidad que Aprueba</th>
                <td>
                    <asp:DropDownList Width="59%" runat="server" ID="ddlUnidad" data-tipocontrol="combo" AppendDataBoundItems="true" AutoPostBack="true"></asp:DropDownList>
                    <asp:RequiredFieldValidator runat="server" ID="rfvDdlUnidad" ControlToValidate="ddlUnidad" Display="Dynamic" ErrorMessage="La unidad que aprueba es obligatoria." ValidationGroup="Aceptar"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <th>Persona Contacto</th>
                <td>
                    <asp:UpdatePanel runat="server" ID="upTxtPersonaContacto" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:TextBox Width="59%" runat="server" ID="txtPersonaContacto" data-tipocontrol="texto" TextMode="MultiLine" Rows="1"></asp:TextBox>
                            <img class="tooltip" src="<%= AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Icono_Ayuda.png")%>"
                                title="Nombre de la persona que podrá ser contactada para brindar información con respecto a la orden de trabajo solicitada." />
                            <span id="spContadortxtPersonaContacto" class="contadorCaracteresRestantes"></span>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="txtIdSolicitante" EventName="TextChanged" />
                            <asp:AsyncPostBackTrigger ControlID="wuc_EmpleadosEU" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <th>Teléfono</th>
                <td>
                    <asp:TextBox Width="59%" runat="server" ID="txtTelefono" data-tipocontrol="texto" MaxLength="10"></asp:TextBox>
                    <ajax:FilteredTextBoxExtender ID="ftbTxtTelefono" runat="server" FilterType="Numbers, Custom" TargetControlID="txtTelefono" ValidChars="-()+"></ajax:FilteredTextBoxExtender>
                </td>
            </tr>
            <tr>
                <th>Edificio o Sitio</th>
                <td>
                    <asp:DropDownList Width="59%" runat="server" ID="ddlLugarTrabajo" data-tipocontrol="combo" AppendDataBoundItems="true"></asp:DropDownList>
                    <asp:RequiredFieldValidator runat="server" ID="rfvDdlLugarTrabajo" ControlToValidate="ddlLugarTrabajo" Display="Dynamic" ErrorMessage="Edificio o Sitio es obligatorio." ValidationGroup="Aceptar"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <th>Lugar Exacto</th>
                <td>
                    <asp:TextBox Width="59%" runat="server" ID="txtLugarExacto" data-tipocontrol="texto" TextMode="MultiLine" Rows="4" ></asp:TextBox>
                    <img class="tooltip" src="<%= AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Icono_Ayuda.png")%>"
                        title="Señas exactas del lugar donde se requiere la realización del trabajo." />
                    <asp:RequiredFieldValidator runat="server" ID="rfvTxtLugarExacto" ControlToValidate="txtLugarExacto" Display="Dynamic" ErrorMessage="Lugar Exacto es obligatorio." ValidationGroup="Aceptar"></asp:RequiredFieldValidator>
                    <span id="spContadorTxtLugarExacto" class="contadorCaracteresRestantes"></span>
                </td>
            </tr>
            <tr>
                <th>Categoría</th>
                <td>
                    <asp:DropDownList Width="59%" runat="server" ID="ddlCategoria" data-tipocontrol="combo" AppendDataBoundItems="true" AutoPostBack="true"></asp:DropDownList>
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
            <tr runat="server" id="trRepeater">
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
                                            CommandArgument='<%# String.Format("{0}¬{1}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.ID_UBICACION), Eval(Utilerias.OrdenesDeTrabajo.Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.ID_ORDEN_TRABAJO))%>' OnClick="ibBorrar_Click"
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
        <asp:Button runat="server" ID="btnRegistrar" Text="Aceptar" ValidationGroup="Aceptar" OnClientClick="javascript:return validarGuardar();" />
        <asp:Button runat="server" ID="btnRegistrarEnviar" Text="Aceptar y Enviar" ValidationGroup="Aceptar" OnClientClick="javascript:return validarGuardarEnviar();"/>
        <asp:Button runat="server" ID="btnLimpiarFormulario" Text="Limpiar Formulario" data-tipo="limpiarFormulario" />
        <input id="btnCancelar" type="button" value="Cancelar" />
    </article>

    <article style="visibility:hidden">
        <asp:Button runat="server" ID="btnAceptarOculto" Text="Guardar" ValidationGroup="Aceptar" />
         <asp:Button runat="server" ID="btnAceptarEnviarOculto" Text="Aceptar y Enviar" ValidationGroup="Aceptar" />
    </article>

    <article id="arPopupDelFormulario"></article>
    <article id="arAlertasDelFormulario"></article>
    <article id="arAlerta"></article>
    <article id="arpopupConfirmaDeseaBorrar"></article>
    <article id="arPopupGenerico"></article>

    <%--Popup para búsqueda de funcionario--%>
    <article id="PopUpBusquedaFuncionario" class="ventanaEmergente">
        <article class="formulario" style="width: 850px!important;">
            <a href="#CerrarPopUpBusquedaFuncionario" title="Cerrar Ventana" class="botonCerrarVentanaEmergente">X</a>
            <br />
            <asp:UpdatePanel ID="upContactoConv" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <uc1:wuc_EmpleadosEU runat="server" ID="wuc_EmpleadosEU" />
                    <div class="areaBotones">
                        <a href="#CerrarPopUpBusquedaFuncionario" title="Regresar">Regresar</a>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </article>
    </article>
    <%--Fin Popup para búsqueda de funcionario--%>

    <script type="text/javascript">
        
        function validarModificar(){
            if(Page_ClientValidate("Aceptar"))  {
                document.getElementById('<%=btnRegistrar.ClientID%>').disabled = "true";
            }
            __doPostBack('<%=Me.btnAceptarOculto.UniqueID%>', '');
            return false;
        }

        function validarGuardar(){
            if(Page_ClientValidate("Aceptar"))  {
                document.getElementById('<%=btnRegistrar.ClientID%>').disabled = "true";
                document.getElementById('<%=btnRegistrarEnviar.ClientID%>').disabled = "true";
            }
            __doPostBack('<%=Me.btnAceptarOculto.UniqueID%>', '');
             return false;
         }

         function validarGuardarEnviar(){
             if(Page_ClientValidate("Aceptar"))  {
                 document.getElementById('<%=btnRegistrar.ClientID%>').disabled = "true";
                document.getElementById('<%=btnRegistrarEnviar.ClientID%>').disabled = "true";
            }
            __doPostBack('<%=Me.btnAceptarEnviarOculto.UniqueID%>', '');
            return false;
        }

        function mostrarAlertaNoEncontrado() {
            mostrarAlerta(
                '#arAlertasDelFormulario',
                {
                    mensaje: "No se han encontrado funcionarios con el número de cédula indicado",
                    tipo: "advertencia",
                    transparencia: 0.9,
                    posicion: 'center',
                    permiteCerrar: true
                }
                );
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

        function regresarAlListado() {
            window.location = 'Lst_OT_OrdenTrabajoSedeRodrigoFacio.aspx';
        };

        function mostrarPopUp(pvc_IdPopup) {
            window.location = pvc_IdPopup;
        };

        function mostrarPopupRegistroExitoso() {
            var vlo_ConfiguracionPopup = {
                titulo: 'Catálogo de Ordenes de Trabajo',
                mensaje: 'Se ha registrado la información.. <br/><strong>¿Desea registrar otra orden de trabajo?</strong>',
                onClosed: function () { regresarAlListado(); },
                botones:
                        [
                            {
                                idControl: "btnSi",
                                textoBoton: "Sí",
                                onClick: function () { window.location = 'Frm_OT_OrdenTrabajoSedeRodrigoFacio.aspx?pvn_Operacion=<%=Utilerias.OrdenesDeTrabajo.eOperacion.Agregar%>'; }
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

        function mostrarAlertaActualizacionExitosa() {
            deshabilitarControl('#<%=btnRegistrar.ClientID%>');
            deshabilitarControl('#<%=btnRegistrarEnviar.ClientID%>');
            deshabilitarControl('#btnLimpiarFormulario');
            deshabilitarControl('#btnCancelar');
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
            deshabilitarControl('#<%=btnRegistrar.ClientID%>');
            deshabilitarControl('#<%=btnRegistrarEnviar.ClientID%>');
            deshabilitarControl('#btnLimpiarFormulario');
            deshabilitarControl('#btnCancelar');
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
                titulo: 'Catálogo de Ordenes de Trabajode recepción',
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


            inicializarFormulario();

        });

        function inicializarFormulario(){
            
            $('#btnCancelar').click(function () {
                regresarAlListado();
            });

            $(window).keydown(function a(e) {

                if (e.keyCode == 13) {
                    return false;
                }
            });

            $('[data-tipo="borrarRegistro"]').click(function () { return mostrarPopupConfirmaDeseaBorrarRegistro($(this).data("uniqueid")); });

            habilitarTooltipGenerico();

            configurarLongitudMaximaTexto('#<%=Me.txtLugarExacto.ClientID%>',<%=Utilerias.OrdenesDeTrabajo.Modelo.OTF_PRE_ORDEN_TRABAJO.SENNAS_EXACTAS_BD_TAMANO%>);
            configurarContadorCaracteresRestantes('#<%=Me.txtLugarExacto.ClientID%>','#spContadorTxtLugarExacto');

            configurarLongitudMaximaTexto('#<%=Me.txtDescTrabajo.ClientID%>',<%=Utilerias.OrdenesDeTrabajo.Modelo.OTF_PRE_ORDEN_TRABAJO.DESCRIPCION_TRABAJO_BD_TAMANO%>);
            configurarContadorCaracteresRestantes('#<%=Me.txtDescTrabajo.ClientID%>','#spContadorTxtDescTrabajo');

            configurarLongitudMaximaTexto('#<%=Me.txtPersonaContacto.ClientID%>',<%=Utilerias.OrdenesDeTrabajo.Modelo.OTF_PRE_ORDEN_TRABAJO.NOMBRE_PERSONA_CONTACTO_BD_TAMANO%>);
            configurarContadorCaracteresRestantes('#<%=Me.txtPersonaContacto.ClientID%>','#spContadortxtPersonaContacto');                     

            cargarLupa();
        };

        function ocultarFiltroFuncionario(){
            window.location = '#CerrarPopUpBusquedaFuncionario';
        }

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
            deshabilitarControl('#<%=btnRegistrar.ClientID%>');
            deshabilitarControl('#<%=btnRegistrarEnviar.ClientID%>');
            deshabilitarControl('#btnLimpiarFormulario');
            document.getElementById("btnCancelar").value = "Regresar";
        };

        function cargarLupa(){
            permutarImagenes('#imgBuscarSolicitante',
                '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Lupa.png")%>',
                '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Lupa.png")%>',
                '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Lupa.png")%>'
            );

            permutarImagenes('#imgBuscarSolicitante',
                '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Lupa.png")%>',
                '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Lupa.png")%>',
                '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Lupa.png")%>'
            );
        };

        function sinUnidadesAsociadas() {
            mostrarAlerta(
                '#arAlerta',
                {
                    mensaje: 'La unidad seleccionada aún no posee ningún registro asociado en el sistema de órdenes de trabajo. Para solventar esta situación contacte al administrador del sistema.',
                    tipo: "advertencia",
                    transparencia: 0.9,
                    posicion: 'center'
                }
            );
        }

    </script>

</asp:Content>

