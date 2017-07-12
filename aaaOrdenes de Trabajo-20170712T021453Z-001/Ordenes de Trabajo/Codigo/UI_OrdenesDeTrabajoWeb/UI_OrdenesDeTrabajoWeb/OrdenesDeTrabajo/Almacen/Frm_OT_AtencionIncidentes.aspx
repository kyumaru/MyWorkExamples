﻿<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/Mp_Formulario.master" AutoEventWireup="false" CodeFile="Frm_OT_AtencionIncidentes.aspx.vb" Inherits="OrdenesDeTrabajo_Almacen_Frm_OT_AtencionIncidentes" %>

<%@ Register TagPrefix="wuc" Namespace="Utilerias.WebForms.Controles" Assembly="Utilerias.WebForms.Controles" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register TagPrefix="wuc" TagName="DatosMaterial" Src="~/Controles/wuc_OT_DatosMaterial.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphFormulario" runat="Server">

    <header>
        <h2>
            <asp:Label runat="server" ID="lblAccion" Text="Incidentes en Almacén"></asp:Label>
        </h2>
    </header>

    <article class="tituloSeccion">
        Incidentes en Almacén
    </article>

    <asp:UpdatePanel runat="server" ID="upPanelMateriales">

        <ContentTemplate>
            <article class="formulario">
                <table>
                    <tr>
                        <th>Tipo Incidente</th>
                        <td>
                            <asp:DropDownList runat="server" ID="ddlTipoIncidente" AppendDataBoundItems="true"></asp:DropDownList>                            
                        </td>
                    </tr>
                    <tr>
                        <th>Código</th>
                        <td>
                            <asp:UpdatePanel runat="server" ID="upTxtCodigo" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:TextBox runat="server" ID="txtCodigo" data-tipocontrol="texto"></asp:TextBox>
                                    <br />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </article>

            <asp:UpdatePanel runat="server" ID="upControlDatosMaterial" UpdateMode="Conditional" Visible="false">
                <ContentTemplate>
                    <article class="formulario sinBorde">
                        <table>
                            <tr>
                                <th>Descripción</th>
                                <td>
                                    <asp:Label runat="server" ID="lblDescripcion" data-tipocontrol="etiqueta"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <th>Categoria</th>
                                <td>
                                    <asp:Label runat="server" ID="lblCategoria"></asp:Label>
                                </td>

                            </tr>
                            <tr>
                                <th>SubCategoría</th>
                                <td>
                                    <asp:Label runat="server" ID="lblSubCategoria"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <th>Detalle</th>
                                <td>
                                    <asp:TextBox runat="server" ID="txtDetalle" TextMode="MultiLine" Rows="4" Columns="60"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </article>
                </ContentTemplate>
            </asp:UpdatePanel>

        </ContentTemplate>
    </asp:UpdatePanel>

    <br />
    <article class="tituloSeccion">
        Documentos Adjuntos
    </article>

    <article class="listado">
        <asp:Repeater runat="server" ID="rpArchivosPrevios">
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
                            OnCommand="lnkArchivosPrevios_Command"
                            Text="<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTF_ADJUNTO_INCIDENTELST.NOMBRE_ARCHIVO)%>"></asp:LinkButton>
                    </td>
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTF_ADJUNTO_INCIDENTELST.DESCRIPCION)%></td>
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTF_ADJUNTO_INCIDENTELST.DESC_TIPO_DOCUMENTO)%></td>
                </tr>
            </ItemTemplate>

            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
    </article>

    <article class="areaPaginadorListado">
        <wuc:PaginadorNumerico runat="server" ID="pnRpArchivosPrevios" />
    </article>

    <article class="areaCantidadDeRegistro">
        <asp:Label runat="server" ID="lblCantidadDeRegistros" Text="" Visible="true"></asp:Label>
    </article>

    <br />

    <article class="formulario sinBorde">
        <table>
            <tr>
                <th>Observaciones del Revisor</th>
                <td>
                    <asp:TextBox runat="server" ID="txtObservacionesRevisor" TextMode="MultiLine" Rows="4" Columns="60"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ID="rfvTxtObservacionesRevisor" ControlToValidate="txtObservacionesRevisor" Display="Dynamic" ErrorMessage="El detalle es requerido." ValidationGroup="Procesar">&nbsp;</asp:RequiredFieldValidator>
                </td>
            </tr>
        </table>
    </article>

    <article class="tituloSeccion">
        Documentos Adjuntos
    </article>

    <article class="formulario sinBorde">
        <table>
            <tr>
                <th>Tipo de archivo</th>
                <td>
                    <asp:DropDownList runat="server" ID="ddlTipoArchivo" data-tipocontrol="combo" AppendDataBoundItems="true" Width="59%" AutoPostBack="true"></asp:DropDownList>
                    <asp:RequiredFieldValidator runat="server" ID="rfvDdlTipoArchivo" ControlToValidate="ddlTipoArchivo" Display="Dynamic" ErrorMessage="El tipo de archivo es obligatorio." ValidationGroup="Agregar"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <th>Archivo</th>
                <td>
                    <asp:FileUpload Width="59%" runat="server" ID="ifInfo" />
                    <img runat="server" id="imgExtensiones" data-tipo="tooltipExtensiones" class="tooltip" />
                    <asp:RequiredFieldValidator runat="server" ID="rfvIfInfo" ControlToValidate="ifInfo" ValidationGroup="Agregar" Display="Dynamic" ErrorMessage="Agregue un documento.">&nbsp;</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <th>Descripción</th>
                <td>
                    <asp:TextBox runat="server" ID="txtDescripcion" data-tipocontrol="texto" Width="59%"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ID="rfvTxtDescripcion" ControlToValidate="txtDescripcion" Display="Dynamic" ErrorMessage="La descripción es obligatoria." ValidationGroup="Agregar"></asp:RequiredFieldValidator>
                </td>
            </tr>
        </table>
    </article>

    <article class="areaBotones">
        <asp:Button runat="server" ID="btnAgregar" Text="Agregar" ValidationGroup="Agregar" />
    </article>

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
                            Text="<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTF_ADJUNTO_INCIDENTELST.NOMBRE_ARCHIVO)%>"></asp:LinkButton>
                    </td>
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTF_ADJUNTO_INCIDENTELST.DESCRIPCION)%></td>
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTF_ADJUNTO_INCIDENTELST.DESC_TIPO_DOCUMENTO)%></td>
                    <td>
                        <asp:ImageButton runat="server" ID="ibBorrar" data-tipo="borrarRegistro" AlternateText="Borrar"
                            CommandArgument='<%# String.Format("{0}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTF_ADJUNTO_INCIDENTELST.ID_ADJUNTO_INCIDENTE))%>' OnClick="ibBorrar_Click"
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

    <article class="areaBotones">
        <asp:Button runat="server" ID="btnGuardar" Text="Guardar"/>
        <asp:Button runat="server" ID="btnGuardarYFinalizar" Text="Getionar Incidente" ValidationGroup="Procesar" />
        <asp:Button runat="server" ID="btnRegresar" Text="Regresar" />
    </article>

    <article id="arAlerta"></article>
    <article id="arpopupConfirmaDeseaBorrar"></article>
    <article id="popupConfirmaDeseaBorrar"></article>
    <article id="popupConfirmacionDeseaBorrar"></article>
    <article id="arPopupGenerico"></article>
    <article id="arPopupDelFormulario"></article>
    <article id="arAlertasDelFormulario"></article>

    <script type="text/javascript">

        function mostrarAlertaLlaveIncorrecta() {
            deshabilitarControl('#<%=btnGuardar.ClientID%>');
            deshabilitarControl('#<%=btnGuardarYFinalizar.ClientID%>');
            deshabilitarControl('#<%=btnRegresar.ClientID%>');
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

        function mostrarAlertaActualizacionExitosa() {
            deshabilitarControl('#<%=btnGuardar.ClientID%>');
            deshabilitarControl('#<%=btnGuardarYFinalizar.ClientID%>');
            deshabilitarControl('#<%=btnRegresar.ClientID%>');
            $('.formulario').attr('disabled', 'disabled');

            mostrarAlerta(
            '#arAlertasDelFormulario',
            {
                mensaje: 'La información ha sido actualizada exitosamente.',
                tipo: "exito",
                transparencia: 0.9,
                posicion: 'center',
                onClosed: function () { regresarAlListado(); }
            });
        };

        function regresarAlListado() {
            __doPostBack('<%=Me.btnRegresar.UniqueID%>', '');
        };

        $(document).ready(function () {

            inicializarFormulario();

            $('[data-tipo="tooltipExtensiones"]').attr('src', '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Icono_Ayuda.png")%>');

            $('[data-tipo="borrarRegistro"]').click(function () { return mostrarPopupConfirmaDeseaBorrarRegistro($(this).data("uniqueid")); });

        });

        function mostrarPopupConfirmaDeseaBorrarRegistro(pvo_UniqueIdControl) {
            var vlo_ConfiguracionPopup = {
                titulo: 'Incidentes de Almacén',
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

        function mostrarPopUp(pvc_IdPopup) {
            window.location = pvc_IdPopup;
        };

        function inicializarFormulario() {

            $(window).keydown(function a(e) {

                if (e.keyCode == 13) {
                    return false;
                }
            });


            $('[data-tipo="tooltipExtensiones"]').attr('src', '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Icono_Ayuda.png")%>');
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
    </script>

</asp:Content>

