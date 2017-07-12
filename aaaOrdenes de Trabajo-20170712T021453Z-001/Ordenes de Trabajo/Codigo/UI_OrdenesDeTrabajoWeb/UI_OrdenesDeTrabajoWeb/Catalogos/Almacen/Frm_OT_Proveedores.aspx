<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/Mp_Formulario.master" AutoEventWireup="false" CodeFile="Frm_OT_Proveedores.aspx.vb" Inherits="Catalogos_Almacen_Frm_OT_Proveedores" %>

<%@ Register TagPrefix="wuc" Namespace="Utilerias.WebForms.Controles" Assembly="Utilerias.WebForms.Controles" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphFormulario" runat="Server">
    <header>
        <h2>
            <asp:Label runat="server" ID="lblAccion"></asp:Label>
        </h2>
    </header>

    <article class="tituloSeccion">
        Mantenimiento de Proveedores
    </article>

    <article id="Article1" class="formulario" runat="server">
        <table>
            <tr>
                <th>Nombre</th>
                <td>
                    <asp:TextBox Width="40%" runat="server" ID="txtNombre" data-tipocontrol="texto" placeholder="Nombre Completo" autocomplete="off"></asp:TextBox>
                    <span id="spContadortxtNombre" class="contadorCaracteresRestantes"></span>
                    <asp:RequiredFieldValidator runat="server" ID="rfvTxtNombre" ControlToValidate="txtNombre" Display="Dynamic" ErrorMessage="El nombre es obligatorio." ValidationGroup="Aceptar">&nbsp;</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr runat="server" id="trTipo">
                <th>Tipo de Proveedor</th>
                <td>
                    <asp:DropDownList Width="40%" runat="server" ID="ddlTipoProveedor" data-tipocontrol="combo" AppendDataBoundItems="true"></asp:DropDownList>
                    <asp:RequiredFieldValidator ID="cvddlTipoProveedor" runat="server" ControlToValidate="ddlTipoProveedor" Display="Dynamic" ErrorMessage="El tipo de proveedor el obligatorio." ValidationGroup="Aceptar">&nbsp;</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <th>Cédula</th>
                <td>
                    <asp:TextBox Width="40%" runat="server" ID="txtCedula" data-tipocontrol="texto" placeholder="Cédula" autocomplete="off"></asp:TextBox>
                    <span id="spContadortxtCedula" class="contadorCaracteresRestantes"></span>
                    <asp:RequiredFieldValidator runat="server" ID="rfvTxtCedula" ControlToValidate="txtCedula" Display="Dynamic" ErrorMessage="La cédula es obligatorio." ValidationGroup="Aceptar">&nbsp;</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <th>Teléfono</th>
                <td>
                    <asp:UpdatePanel runat="server" ID="UpdatePanel2" UpdateMode="Conditional" AutoPostBack="true">
                        <ContentTemplate>
                            <ajax:FilteredTextBoxExtender ID="ftbtxtTelefono" runat="server" TargetControlID="txtTelefono" ValidChars="1234567890"></ajax:FilteredTextBoxExtender>
                            <asp:TextBox Width="40%" runat="server" ID="txtTelefono" data-tipocontrol="texto" placeholder="número telefónico" autocomplete="off"></asp:TextBox>
                            <asp:Button runat="server" ID="btnAgregaTelefonor" Text="Agregar" ValidationGroup="Agregar" />
                            <asp:Button runat="server" ID="btnModificaTelefono" Text="Modificar" ValidationGroup="Agregar" />
                            <asp:Button runat="server" ID="btnCancelarTelefono" Text="Cancelar" />
                            <br />
                            <span id="spContadortxtTelefono" class="contadorCaracteresRestantes"></span>
                            <asp:RequiredFieldValidator runat="server" ID="rfvTxtTelefono" ControlToValidate="txtTelefono" Display="Dynamic" ErrorMessage="El teléfono es obligatorio." ValidationGroup="Agregar">&nbsp;</asp:RequiredFieldValidator>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="rpTelefono" />
                            <asp:AsyncPostBackTrigger ControlID="btnAgregaTelefonor" />
                            <asp:AsyncPostBackTrigger ControlID="btnModificaTelefono" />
                            <asp:AsyncPostBackTrigger ControlID="btnCancelarTelefono" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <th></th>
                <td>
                    <asp:UpdatePanel runat="server" ID="upRpResponsables" UpdateMode="Conditional" AutoPostBack="true">
                        <ContentTemplate>
                            <article data-grupo="Listado" class="listado">
                                <asp:Repeater runat="server" ID="rpTelefono">
                                    <HeaderTemplate>
                                        <table>
                                            <tr>
                                                <th>Teléfono
                                                </th>
                                                <th>&nbsp;</th>
                                                <th>&nbsp;</th>
                                            </tr>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr class="lineaDelListado">
                                            <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.OTM_TELEFONO_PROVEEDOR.TELEFONO)%></td>
                                            <td>
                                                <asp:ImageButton runat="server" ID="ibModificarTelefono" AlternateText="Modificar Registro" data-tipo="modificarRegistro" ToolTip="Modificar registro"
                                                    CommandArgument='<%# String.Format("{0}¬{1}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.OTM_TELEFONO_PROVEEDOR.IDENTIFICACION),Eval(Utilerias.OrdenesDeTrabajo.Modelo.OTM_TELEFONO_PROVEEDOR.TELEFONO))%>' OnClick="ibModificarTelefono_Click"
                                                    src='<%#AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Editar.png")%>' onmouseover='<%# String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Icono_Editar.png"))%>' onmouseout='<%# String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Editar.png"))%>' />
                                            </td>
                                            <td>
                                                <asp:ImageButton runat="server" ID="ibBorrarTelefono" AlternateText="Borrar Registro" data-tipo="borrarRegistros" ToolTip="Borrar registro"
                                                    CommandArgument='<%# String.Format("{0}¬{1}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.OTM_TELEFONO_PROVEEDOR.IDENTIFICACION),Eval(Utilerias.OrdenesDeTrabajo.Modelo.OTM_TELEFONO_PROVEEDOR.TELEFONO))%>' OnClick="ibBorrarTelefono_Click" />
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        </table>
                                    </FooterTemplate>
                                </asp:Repeater>

                                <article data-grupo="Listado" class="areaPaginadorListado">
                                </article>

                                <article data-grupo="Listado" class="areaCantidadDeRegistro">
                                    <asp:Label runat="server" ID="lblCantidadDeRegistros" Text="" Visible="true"></asp:Label>
                                </article>
                            </article>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="rpTelefono" />
                            <asp:AsyncPostBackTrigger ControlID="btnAgregaTelefonor" />
                            <asp:AsyncPostBackTrigger ControlID="btnModificaTelefono" />
                            <asp:AsyncPostBackTrigger ControlID="btnCancelarTelefono" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <th>Correos Contacto:</th>
                <td></td>
            </tr>
            <tr>
                <th>Nombre</th>
                <td>
                    <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Conditional" AutoPostBack="true">
                        <ContentTemplate>
                            <asp:TextBox Width="40%" runat="server" ID="txtNombreCorreo" data-tipocontrol="texto" autocomplete="off" placeholder="Nombre del remitente"></asp:TextBox>
                            <span id="spContadortxtNombreCorreo" class="contadorCaracteresRestantes"></span>
                            <asp:RequiredFieldValidator runat="server" ID="rfvTxtNombreCorreo" ControlToValidate="txtNombreCorreo" Display="Dynamic" ErrorMessage="El Nombre del correo es obligatorio." ValidationGroup="correo">&nbsp;</asp:RequiredFieldValidator>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="rpCorreo" />
                            <asp:AsyncPostBackTrigger ControlID="btnAgregarCorreo" />
                            <asp:AsyncPostBackTrigger ControlID="btnModificarCorreo" />
                            <asp:AsyncPostBackTrigger ControlID="btnCancelarCorreo" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <th>Correo</th>
                <td>
                    <asp:UpdatePanel runat="server" ID="UpdatePanel3" UpdateMode="Conditional" AutoPostBack="true">
                        <ContentTemplate>
                            <asp:TextBox Width="40%" runat="server" ID="txtCorreo" data-tipocontrol="texto" autocomplete="off" placeholder="Correo Electrónico"></asp:TextBox>
                            <asp:Button runat="server" ID="btnAgregarCorreo" Text="Agregar" ValidationGroup="correo" />
                            <asp:Button runat="server" ID="btnModificarCorreo" Text="Modificar" ValidationGroup="correo" />
                            <asp:Button runat="server" ID="btnCancelarCorreo" Text="Cancelar" Style="width: 70px;" />
                            <br />
                            <span id="spContadortxtCorreo" class="contadorCaracteresRestantes"></span>
                            <asp:RequiredFieldValidator runat="server" ID="rfvTxtCorreo" ControlToValidate="txtCorreo" Display="Dynamic" ErrorMessage="El Correo es obligatorio." ValidationGroup="correo">&nbsp;</asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revCorreo" D="revEmail" runat="server" ControlToValidate="txtCorreo" ValidationGroup="correo" ErrorMessage="El formato de correo no es válido" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">&nbsp;</asp:RegularExpressionValidator>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="rpCorreo" />
                            <asp:AsyncPostBackTrigger ControlID="btnAgregarCorreo" />
                            <asp:AsyncPostBackTrigger ControlID="btnModificarCorreo" />
                            <asp:AsyncPostBackTrigger ControlID="btnCancelarCorreo" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <th></th>
                <td>
                    <asp:UpdatePanel runat="server" ID="UpdatePanel4" UpdateMode="Conditional" AutoPostBack="true">
                        <ContentTemplate>
                            <article data-grupo="Listado" class="listado">
                                <asp:Repeater runat="server" ID="rpCorreo">
                                    <HeaderTemplate>
                                        <table>
                                            <tr>
                                                <th style="width: 40%">Nombre</th>
                                                <th style="width: 40%">Correo</th>
                                                <th style="width: 10%">&nbsp;</th>
                                                <th style="width: 10%">&nbsp;</th>
                                            </tr>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr class="lineaDelListado">
                                            <td style="width: 40%"><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.OTM_CORREO_PROVEEDOR.NOMBRE)%></td>
                                            <td style="width: 40%"><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.OTM_CORREO_PROVEEDOR.CORREO)%></td>
                                            <td style="width: 10%">
                                                <asp:ImageButton runat="server" ID="ibModificarCorreo" AlternateText="Modificar Registro" data-tipo="modificarRegistro" ToolTip="Modificar registro"
                                                    CommandArgument='<%# String.Format("{0}¬{1}¬{2}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.OTM_CORREO_PROVEEDOR.IDENTIFICACION), Eval(Utilerias.OrdenesDeTrabajo.Modelo.OTM_CORREO_PROVEEDOR.CORREO),Eval(Utilerias.OrdenesDeTrabajo.Modelo.OTM_CORREO_PROVEEDOR.NOMBRE))%>' OnClick="ibModificarCorreo_Click"
                                                    src='<%#AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Editar.png")%>' onmouseover='<%# String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Icono_Editar.png"))%>' onmouseout='<%# String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Editar.png"))%>' />
                                            </td>
                                            <td style="width: 10%">
                                                <asp:ImageButton runat="server" ID="ibBorrarCorreo" AlternateText="Borrar Registro" data-tipo="borrarRegistro" ToolTip="Borrar registro"
                                                    CommandArgument='<%# String.Format("{0}¬{1}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.OTM_CORREO_PROVEEDOR.IDENTIFICACION), Eval(Utilerias.OrdenesDeTrabajo.Modelo.OTM_CORREO_PROVEEDOR.CORREO))%>' OnClick="ibBorrarCorreo_Click" />
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        </table>
                                    </FooterTemplate>
                                </asp:Repeater>

                                <article data-grupo="Listado" class="areaPaginadorListado">
                                </article>

                                <article data-grupo="Listado" class="areaCantidadDeRegistro">
                                    <asp:Label runat="server" ID="lblCantidadRegistros" Text="" Visible="true"></asp:Label>
                                </article>
                            </article>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="rpCorreo" />
                            <asp:AsyncPostBackTrigger ControlID="btnAgregarCorreo" />
                            <asp:AsyncPostBackTrigger ControlID="btnModificarCorreo" />
                            <asp:AsyncPostBackTrigger ControlID="btnCancelarCorreo" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <th>Sitio Web</th>
                <td>
                    <asp:TextBox Width="40%" runat="server" ID="txtSitioWeb" data-tipocontrol="texto" placeholder="Sitio Web" autocomplete="off"></asp:TextBox>
                    <span id="spContadortxtSitioWeb" class="contadorCaracteresRestantes"></span>
                </td>
            </tr>
            <tr runat="server" id="trEstado">
                <th>Estado</th>
                <td>
                    <asp:DropDownList Width="40%" runat="server" ID="ddlEstado"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <th>Dirección</th>
                <td>
                    <asp:TextBox runat="server" ID="txtDireccion" data-tipocontrol="texto" autocomplete="off" Width="100%" TextMode="MultiLine" Rows="2" placeholder="Dirección"></asp:TextBox>
                    <br />
                    <span id="spContadortxtDireccion" class="contadorCaracteresRestantes"></span>
                    <asp:RequiredFieldValidator runat="server" ID="rfvtxtDireccion" ControlToValidate="txtDireccion" Display="Dynamic" ErrorMessage="La dirección es obligatorio." ValidationGroup="Aceptar">&nbsp;</asp:RequiredFieldValidator>
                </td>
            </tr>
        </table>
    </article>

    <article class="areaBotones">
        <asp:Button runat="server" ID="btnAceptar" Text="Aceptar" ValidationGroup="Aceptar" />
        <input id="btnLimpiarFormulario" type="button" data-tipo="limpiarFormulario" value="Limpiar formulario" />
        <input id="btnCancelar" type="button" value="Cancelar" />

        <asp:CustomValidator ID="cvtxtCedulaAgrega" runat="server" ControlToValidate="txtCedula" ErrorMessage="Debe agregar la Cédula." ValidateEmptyText="true" ClientValidationFunction="validarCampo" ValidationGroup="Agregar">&nbsp;</asp:CustomValidator>
        <asp:CustomValidator ID="CustomValidator2" runat="server" ControlToValidate="txtCedula" ErrorMessage="Debe agregar la Cédula." ValidateEmptyText="true" ClientValidationFunction="validarCampo" ValidationGroup="correo">&nbsp;</asp:CustomValidator>
    </article>

    <asp:UpdatePanel runat="server" ID="UpdatePanel5" UpdateMode="Conditional" AutoPostBack="true">
        <ContentTemplate>
            <article style="visibility: hidden">
                <asp:TextBox runat="server" ID="txtCorreoValidacion" Text=""></asp:TextBox>
                <asp:TextBox runat="server" ID="txtTelefonoValidacion" Text=""></asp:TextBox>

                <asp:RequiredFieldValidator ID="rfvtxtCorreoValidador" runat="server" ControlToValidate="txtCorreoValidacion" Display="Dynamic" ErrorMessage="Es obligatorio al menos un Correo." ValidationGroup="Aceptar">&nbsp;</asp:RequiredFieldValidator>
                <asp:RequiredFieldValidator ID="rfvtxtTelefonoValidador" runat="server" ControlToValidate="txtTelefonoValidacion" Display="Dynamic" ErrorMessage="Es obligatorio al menos un Teléfono." ValidationGroup="Aceptar">&nbsp;</asp:RequiredFieldValidator>
            </article>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="rpCorreo" />
            <asp:AsyncPostBackTrigger ControlID="btnAgregarCorreo" />
            <asp:AsyncPostBackTrigger ControlID="btnModificarCorreo" />
            <asp:AsyncPostBackTrigger ControlID="btnCancelarCorreo" />
            <asp:AsyncPostBackTrigger ControlID="rpTelefono" />
            <asp:AsyncPostBackTrigger ControlID="btnAgregaTelefonor" />
            <asp:AsyncPostBackTrigger ControlID="btnModificaTelefono" />
            <asp:AsyncPostBackTrigger ControlID="btnCancelarTelefono" />
        </Triggers>
    </asp:UpdatePanel>

    <article id="arPopupDelFormulario"></article>
    <article id="arAlertasDelFormulario"></article>
    <article id="arPopupGenerico"></article>
    <article id="arpopupConfirmaDeseaBorrar"></article>

    <script type="text/javascript">

        $(document).ready(function () {
            AgregarImagenBorrar();
            MensajeConfirmacionDeseaBorrar();

            $('#btnCancelar').click(function () {
                regresarAlListado();
            });

            configurarLongitudMaximaTexto('#<%=Me.txtNombre.ClientID%>',<%=Utilerias.OrdenesDeTrabajo.Modelo.OTM_PROVEEDOR.NOMBRE_BD_TAMANO%>);
            configurarContadorCaracteresRestantes('#<%=Me.txtNombre.ClientID%>','#spContadortxtNombre');      

            configurarLongitudMaximaTexto('#<%=Me.txtCedula.ClientID%>',<%=Utilerias.OrdenesDeTrabajo.Modelo.OTM_PROVEEDOR.IDENTIFICACION_BD_TAMANO%>);
            configurarContadorCaracteresRestantes('#<%=Me.txtCedula.ClientID%>','#spContadortxtCedula');      

            configurarLongitudMaximaTexto('#<%=Me.txtTelefono.ClientID%>',<%=Utilerias.OrdenesDeTrabajo.Modelo.OTM_TELEFONO_PROVEEDOR.TELEFONO_BD_TAMANO%>);
            configurarContadorCaracteresRestantes('#<%=Me.txtTelefono.ClientID%>','#spContadortxtTelefono');  

            configurarLongitudMaximaTexto('#<%=Me.txtNombreCorreo.ClientID%>',<%=Utilerias.OrdenesDeTrabajo.Modelo.OTM_CORREO_PROVEEDOR.NOMBRE_BD_TAMANO%>);
            configurarContadorCaracteresRestantes('#<%=Me.txtNombreCorreo.ClientID%>','#spContadortxtNombreCorreo');  

            configurarLongitudMaximaTexto('#<%=Me.txtCorreo.ClientID%>',<%=Utilerias.OrdenesDeTrabajo.Modelo.OTM_CORREO_PROVEEDOR.CORREO_BD_TAMANO%>);
            configurarContadorCaracteresRestantes('#<%=Me.txtCorreo.ClientID%>','#spContadortxtCorreo');  

            configurarLongitudMaximaTexto('#<%=Me.txtSitioWeb.ClientID%>',<%=Utilerias.OrdenesDeTrabajo.Modelo.OTM_PROVEEDOR.SITIO_WEB_BD_TAMANO%>);
            configurarContadorCaracteresRestantes('#<%=Me.txtSitioWeb.ClientID%>','#spContadortxtSitioWeb');  

            configurarLongitudMaximaTexto('#<%=Me.txtDireccion.ClientID%>',<%=Utilerias.OrdenesDeTrabajo.Modelo.OTM_PROVEEDOR.DIRECCION_BD_TAMANO%>);
            configurarContadorCaracteresRestantes('#<%=Me.txtDireccion.ClientID%>','#spContadortxtDireccion');  
        });
          
        function MensajeConfirmacionDeseaBorrar() {
            $('[data-tipo="borrarRegistros"]').click(function () { return mostrarPopupConfirmaDeseaBorrarRegistro($(this).data("uniqueid")); });
            $('[data-tipo="borrarRegistro"]').click(function () { return mostrarPopupConfirmaDeseaBorrarRegistro($(this).data("uniqueid")); });
        }

        function AgregarImagenBorrar() {
         
            $('[data-tipo="borrarRegistros"]').attr('src', '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Borrar.png")%>');
            $('[data-tipo="borrarRegistros"]').on({
                'mouseover': function () { $(this).attr('src', '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Icono_Borrar.png")%>'); },
                'mouseout': function () { $(this).attr('src', '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Borrar.png")%>'); }
            });

            $('[data-tipo="borrarRegistro"]').attr('src', '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Borrar.png")%>');
            $('[data-tipo="borrarRegistro"]').on({
                'mouseover': function () { $(this).attr('src', '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Icono_Borrar.png")%>'); },
                'mouseout': function () { $(this).attr('src', '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Borrar.png")%>'); }
            });

            configurarLongitudMaximaTexto('#<%=Me.txtTelefono.ClientID%>',<%=Utilerias.OrdenesDeTrabajo.Modelo.OTM_TELEFONO_PROVEEDOR.TELEFONO_BD_TAMANO%>);
            configurarContadorCaracteresRestantes('#<%=Me.txtTelefono.ClientID%>','#spContadortxtTelefono');  


            configurarLongitudMaximaTexto('#<%=Me.txtCorreo.ClientID%>',<%=Utilerias.OrdenesDeTrabajo.Modelo.OTM_CORREO_PROVEEDOR.CORREO_BD_TAMANO%>);
            configurarContadorCaracteresRestantes('#<%=Me.txtCorreo.ClientID%>','#spContadortxtCorreo');  
            MensajeConfirmacionDeseaBorrar();
        }
        
        function validarCampo(source, clientside_arguments) {
            var vlo_txtTextoCedula = document.getElementById(source.controltovalidate);
           
            if (vlo_txtTextoCedula.value == '') {
                return clientside_arguments.IsValid = false;
            }
            else {

                return clientside_arguments.IsValid = true;
            }         
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

        function regresarAlListado() {
            window.location = 'Lst_OT_Proveedores.aspx';
        };

        function mostrarPopupRegistroExitoso() {
            var vlo_ConfiguracionPopup = {
                titulo: 'Catálogo de Proveedores',
                mensaje: 'Se ha registrado la información.. <br/><strong>¿Desea registrar otro Proveedor?</strong>',
                onClosed: function () { regresarAlListado(); },
                botones:
                        [
                            {
                                idControl: "btnSi",
                                textoBoton: "Sí",
                                onClick: function () { window.location = 'Frm_OT_Proveedores.aspx?pvn_Operacion=<%=Utilerias.OrdenesDeTrabajo.eOperacion.Agregar%>'; }
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

                    function mostrarAlertaActualizacionExitosa() {
                        deshabilitarControl('#<%=btnAceptar.ClientID%>');
                        deshabilitarControl('#btnLimpiarFormulario');
                        deshabilitarControl('#btnCancelar');
                        $('.formulario').attr('disabled', 'disabled');

                        mostrarAlerta(
                            '#arAlertasDelFormulario',
                            {
                                mensaje: 'Se ha actualizado la información del registro',
                                tipo: "exito",
                                transparencia: 0.9,
                                posicion: 'center',
                                onClosed: function () { regresarAlListado(); }
                            });
                    };

                    function mostrarAlertaLlaveIncorrecta() {
                        deshabilitarControl('#<%=btnAceptar.ClientID%>');
                        deshabilitarControl('#btnLimpiarFormulario');
                        deshabilitarControl('#btnCancelar');
                        $('.formulario').attr('disabled', 'disabled');

                        mostrarAlerta(
                            '#arAlertasDelFormulario',
                            {
                                mensaje: 'El identificador provisto no pertenece a ningun registro.',
                                tipo: "peligro",
                                transparencia: 0.9,
                                posicion: 'center',
                                onClosed: function () { regresarAlListado(); }
                            });
                    };

                    function mostrarPopUp(pvc_IdPopup) {
                        window.location = pvc_IdPopup;
                    };

                    function mostrarPopupConfirmaDeseaBorrarRegistro(pvo_UniqueIdControl) {
                        var vlo_ConfiguracionPopup = {
                            titulo: 'Catálogo de Proveedores',
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
                    }

    </script>
</asp:Content>

