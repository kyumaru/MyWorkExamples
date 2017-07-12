<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/Mp_Formulario.master" AutoEventWireup="false" CodeFile="Frm_OT_OrdenTrabajoHija.aspx.vb" Inherits="OrdenesDeTrabajo_Frm_OT_OrdenTrabajoHija" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphFormulario" runat="Server">

    <header>
        <h2>
            <asp:Label runat="server" ID="lblAccion" Text="Orden de Trabajo Hija"></asp:Label>
        </h2>
    </header>

    <article class="tituloSeccion">
        Orden de Trabajo Hija
    </article>

    <article class="formulario" id="formularioTotal" runat="server">
        <table>
            <tr>
                <th>Solicitante</th>
                <td>
                    <asp:Label runat="server" ID="lblSolicitante" data-tipocontrol="etiqueta"></asp:Label>
                </td>
            </tr>
             <tr>
                <th>Unidad que Aprueba</th>
                <td>
                    <asp:DropDownList Width="59%" runat="server" ID="ddlUnidad" data-tipocontrol="combo" AppendDataBoundItems="true" Enabled="false"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <th>Persona Contacto</th>
                <td>
                    <asp:TextBox runat="server" ID="txtPersonaContacto" data-tipocontrol="texto" TextMode="MultiLine" Rows="1" Width="59%" Enabled="false"></asp:TextBox>
                    <img class="tooltip" src="<%= AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Icono_Ayuda.png")%>"
                        title="Nombre de la persona que podrá ser contactada para brindar información con respecto a la orden de trabajo solicitada." />
                </td>
            </tr>
            <tr>
                <th>Teléfono</th>
                <td>
                    <asp:TextBox Width="59%" runat="server" ID="txtTelefono" data-tipocontrol="texto" MaxLength="10" Enabled="false"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>Edificio o Sitio</th>
                <td>
                    <asp:DropDownList Width="59%" runat="server" ID="ddlLugarTrabajo" data-tipocontrol="combo" AppendDataBoundItems="true" Enabled="false"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <th>Lugar Exacto</th>
                <td>
                    <asp:TextBox runat="server" ID="txtLugarExacto" data-tipocontrol="texto" TextMode="MultiLine" Rows="4" Width="59%" Enabled="false"></asp:TextBox>
                    <img class="tooltip" src="<%= AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Icono_Ayuda.png")%>"
                        title="Señas exactas del lugar donde se requiere la realización del trabajo." />
                    <span id="spContadorTxtLugarExacto" class="contadorCaracteresRestantes"></span>
                </td>
            </tr>
        </table>
    </article>

    <article class="formulario sinBorde">
        <table>
            <tr>
                <th>Categoría</th>
                <td>
                    <asp:DropDownList Width="59%" runat="server" ID="ddlCategoria" data-tipocontrol="combo" AppendDataBoundItems="true" AutoPostBack="true"></asp:DropDownList>
                    <asp:RequiredFieldValidator runat="server" ID="rfvDdlCategoria" ControlToValidate="ddlCategoria" Display="Dynamic" ErrorMessage="Categoría es obligatoria." ValidationGroup="Aceptar"></asp:RequiredFieldValidator>
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
                            <asp:TextBox runat="server" ID="txtDescripcionActividad" TextMode="MultiLine" Rows="4" Width="59%" Enabled="false"></asp:TextBox>
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
        </table>
    </article>

    <article class="areaBotones">
        <asp:Button runat="server" ID="btnAceptarEnviar" Text="Aceptar y Enviar" ValidationGroup="Aceptar"  OnClientClick="javascript:return validarGuardarEnviar();"/>
        <%--<asp:Button runat="server" ID="btnCancelar" Text="Cancelar" />--%>
        <input id="btnCancelar" type="button" value="Cancelar" />
    </article>


    <article style="visibility:hidden">
         <asp:Button runat="server" ID="btnAceptarEnviarOculto" Text="Aceptar y Enviar" ValidationGroup="Aceptar" />
    </article>

    <article id="arPopupDelFormulario"></article>
    <article id="arAlertasDelFormulario"></article>
    <article id="arpopupConfirmaDeseaBorrar"></article>
    <article id="arPopupGenerico"></article>

    <script type="text/javascript">

        function validarGuardarEnviar(){
            if(Page_ClientValidate("Aceptar"))  {
                document.getElementById('<%=btnAceptarEnviar.ClientID%>').disabled = "true";
            }
            __doPostBack('<%=Me.btnAceptarEnviarOculto.UniqueID%>', '');
             return false;
         }

        function regresarAlListado() {
            window.location = 'Lst_OT_GestionOrdenTrabajoCordinadorSectorTaller.aspx';
        };

        function mostrarPopupRegistroExitoso() {
            var vlo_ConfiguracionPopup = {
                titulo: 'Catálogo de Ordenes de Trabajo Hijas',
                mensaje: 'Se ha registrado la información.. <br/><strong>¿Desea registrar otra Orden hija?</strong>',
                onClosed: function () { regresarAlListado(); },
                botones:
                [
                    {
                        idControl: "btnSi",
                        textoBoton: "Sí",
                        onClick: function () { window.location = 'Frm_OT_OrdenTrabajoHija.aspx'; }
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
                deshabilitarControl('#<%=btnAceptarEnviar.ClientID%>');
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
        
            $(document).ready(function () {
                $('#btnCancelar').click(function () {
                    regresarAlListado();
                });
                
                habilitarTooltipGenerico();

                configurarLongitudMaximaTexto('#<%=Me.txtLugarExacto.ClientID%>',<%=Utilerias.OrdenesDeTrabajo.Modelo.OTT_ORDEN_TRABAJO.SENNAS_EXACTAS_BD_TAMANO%>);
                configurarContadorCaracteresRestantes('#<%=Me.txtLugarExacto.ClientID%>','#spContadorTxtLugarExacto');

                configurarLongitudMaximaTexto('#<%=Me.txtDescTrabajo.ClientID%>',<%=Utilerias.OrdenesDeTrabajo.Modelo.OTT_ORDEN_TRABAJO.DESCRIPCION_TRABAJO_BD_TAMANO%>);
                configurarContadorCaracteresRestantes('#<%=Me.txtDescTrabajo.ClientID%>','#spContadorTxtDescTrabajo');

            });

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
            };

    </script>

</asp:Content>


