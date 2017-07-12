﻿<%@ Page Language="VB" MasterPageFile="~/MasterPage/Mp_Formulario.master" AutoEventWireup="false" CodeFile="Frm_OT_VbDenegadaRechazada.aspx.vb" Inherits="OrdenesDeTrabajo_Frm_OT_VbDenegadaRechazada" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphFormulario" runat="Server">

    <header>
        <h2>
            <asp:Label runat="server" ID="lblAccion" data-tipocontrol="etiqueta"></asp:Label>
            
        </h2>
    </header>

    <article class="tituloSeccion">
        Datos de la orden de trabajo
    </article>

    <article class="formulario" id="formularioTotal" runat="server">
        <table>
            <tr>
                <th>Fecha de Solicitud</th>
                <td>
                    <asp:Label runat="server" ID="lblFechaSolicitud" data-tipocontrol="etiqueta"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>Solicitante</th>
                <td>
                    <asp:Label runat="server" ID="lblSolicitante" data-tipocontrol="etiqueta"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>Persona Contacto</th>
                <td>
                    <asp:Label runat="server" ID="lblPersonaContacto" data-tipocontrol="etiqueta"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>Teléfono</th>
                <td>
                    <asp:Label runat="server" ID="lblTelefono" data-tipocontrol="etiqueta"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>Edificio</th>
                <td>
                    <asp:Label runat="server" ID="lblEdificio" data-tipocontrol="etiqueta"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>Lugar Exacto</th>
                <td>
                    <asp:Label runat="server" ID="lblLugarExacto" data-tipocontrol="etiqueta"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>Categoria de Servicio</th>
                <td>
                    <asp:Label runat="server" ID="lblCategServ" data-tipocontrol="etiqueta"></asp:Label>
                </td>
            </tr>
             <tr>
                <th>Actividad</th>
                <td>
                    <asp:Label runat="server" ID="lblActividad" data-tipocontrol="etiqueta"></asp:Label>
                </td>
            </tr>
             <tr>
                <th>Descripción de Trabajo</th>
                <td>
                    <asp:Label runat="server" ID="lblDescTrabajo" data-tipocontrol="texto" Enabled="false" TextMode="MultiLine" Rows="5" Columns="40"></asp:Label>
                </td>
            </tr>
             <tr>
                 <th>Sector o Taller actual</th>
                <td>
                    <asp:Label runat="server" ID="lblSectorTaller" data-tipocontrol="texto" Enabled="false" TextMode="MultiLine" Rows="5" Columns="40"></asp:Label>
                </td>
             </tr>
            <tr>
                 <th>Motivo de rechazo</th>
                <td>
                    <asp:Label runat="server" ID="lblMotivoRechazo" data-tipocontrol="texto" Enabled="false" TextMode="MultiLine" Rows="5" Columns="40"></asp:Label>
                </td>
             </tr>
        </table>
    </article>

    <article class="tituloSeccion">
        Datos de la Reasignación
    </article>

    <article class="formulario">
        <table>
            <tr>
                <th>Condición</th>
                <td>
                    <asp:RadioButton ID="rbtVistoBueno" runat="server" Text="Visto Bueno" GroupName="Condicion" AutoPostBack="true" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:RadioButton ID="rbtDenegada" runat="server" Text="Denegada " GroupName="Condicion" AutoPostBack="true"/>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:RadioButton ID="rbtReasignada" runat="server" Text="Reasignada" GroupName="Condicion" AutoPostBack="true" />
                </td>
            </tr>
            <tr>
                <th>Categoría</th>
                <td>
                    <asp:DropDownList Width="50%" runat="server" ID="ddlCategoria" data-tipocontrol="combo" AppendDataBoundItems="true" AutoPostBack="true"></asp:DropDownList>
                    <asp:RequiredFieldValidator runat="server" ID="rfvDdlCategoria" ControlToValidate="ddlCategoria" Display="Dynamic" ErrorMessage="Categoría es obligatoria." ValidationGroup="Aceptar"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <th>Actividad</th>
                <td>
                    <asp:UpdatePanel runat="server" ID="upActividad" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:DropDownList Width="50%" runat="server" ID="ddlActividad" data-tipocontrol="combo" AppendDataBoundItems="true" AutoPostBack="true"></asp:DropDownList>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlCategoria" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                    <asp:RequiredFieldValidator runat="server" ID="rfvDdlActividad" ControlToValidate="ddlActividad" Display="Dynamic" ErrorMessage="Actividad es obligatoria." ValidationGroup="Aceptar"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <th>Edificio o Sitio</th>
                <td>
                    <asp:UpdatePanel runat="server" ID="upLugarTrabajo" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:DropDownList Width="50%" runat="server" ID="ddlLugarTrabajo" data-tipocontrol="combo" AutoPostBack="true" AppendDataBoundItems="true"></asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <asp:RequiredFieldValidator runat="server" ID="rfvddlLugarTrabajo" ControlToValidate="ddlLugarTrabajo" Display="Dynamic" ErrorMessage="Edificio es obligatorio." ValidationGroup="Aceptar"></asp:RequiredFieldValidator>
                </td>
            </tr>
			
			<tr>
                <th>Sector o Taller</th>
                <td>
                    <asp:UpdatePanel runat="server" ID="upTallerSector" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:Label runat="server" ID="lblNombreTallerSector"></asp:Label>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlLugarTrabajo" EventName="SelectedIndexChanged" />
                            <asp:AsyncPostBackTrigger ControlID="ddlActividad" EventName="SelectedIndexChanged" />
                            <asp:AsyncPostBackTrigger ControlID="ddlCategoria" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <th>Motivo o Justificación al rechazo</th>
                <td>
                    <asp:TextBox TextMode="MultiLine" data-tipoControl="texto" Width="50%" runat="server" ID="txtMotivo" ></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" id="rvftxtMotivo" ControlToValidate="txtMotivo" display="Dynamic" ErrorMessage="Ingrese una justificación del rechazo." ValidationGroup="Aceptar">&nbsp;</asp:RequiredFieldValidator>
                </td>
            </tr>
        </table>
    </article>

     <article class="areaBotones">
        <asp:Button runat="server" ID="btnAceptar" Text="Aceptar" ValidationGroup="Aceptar" />
        <input id="btnCancelar" type="button" value="Cancelar" />
    </article>

    <article id="arPopupDelFormulario"></article>
    <article id="arAlertasDelFormulario"></article>
    <article id="arpopupConfirmaDeseaBorrar"></article>
    <article id="arPopupGenerico"></article>

     <script type="text/javascript">

         function regresarAlListado() {
             window.location = 'Lst_OT_Rechazadas.aspx';
         };

         function mostrarAlertaActualizacionExitosa() {
             deshabilitarControl('#<%=btnAceptar.ClientID%>');
             deshabilitarControl('#btnLimpiarFormulario');
             deshabilitarControl('#btnCancelar');
             $('.formulario').attr('disabled', 'disabled');

             mostrarAlerta(
                 '#arAlertasDelFormulario',
                 {
                     mensaje: 'Se ha actualizado la información de la orden.',
                     tipo: "exito",
                     transparencia: 0.9,
                     posicion: 'top',
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
                     posicion: 'top',
                     permiteCerrar: true
                 }
             );
         };

         function mostrarAlertaLlaveIncorrecta() {
             deshabilitarControl('#<%=btnAceptar.ClientID%>');
             deshabilitarControl('#btnLimpiarFormulario');
             deshabilitarControl('#btnCancelar');
             $('.formulario').attr('disabled', 'disabled');

             mostrarAlerta(
                 '#arAlertasDelFormulario',
                 {
                     mensaje: 'El identificador provisto no pertenece a ninguna orden de trabajo.',
                     tipo: "peligro",
                     transparencia: 0.9,
                     posicion: 'top',
                     onClosed: function () { regresarAlListado(); }
                 });
         };

         function mostrarAlertaRegistroIdentico() {
             deshabilitarControl('#<%=btnAceptar.ClientID%>');
             deshabilitarControl('#btnLimpiarFormulario');
             deshabilitarControl('#btnCancelar');
             $('.formulario').attr('disabled', 'disabled');

             mostrarAlerta(
                 '#arAlertasDelFormulario',
                 {
                     mensaje: 'Para reasignar la orden de trabajo debe variar al menos uno de los criterios de asignación',
                     tipo: "advertencia",
                     transparencia: 0.9,
                     posicion: 'top'
                 });
         };

         $(document).ready(function () {
             $('#btnCancelar').click(function () {
                 regresarAlListado();
             });

             $('[data-tipo="borrarRegistro"]').click(function () { return mostrarPopupConfirmaDeseaBorrarRegistro($(this).data("uniqueid")); });

             habilitarTooltipGenerico();

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

         function deshabilitar() {
             deshabilitarJerarquiaDeControles(document.getElementById('<%=formularioTotal.ClientID%>'));
            deshabilitarControl('#<%=btnAceptar.ClientID%>');
            deshabilitarControl('#btnLimpiarFormulario');
            document.getElementById("btnCancelar").value = "Regresar";
        };

    </script>




</asp:Content> 

