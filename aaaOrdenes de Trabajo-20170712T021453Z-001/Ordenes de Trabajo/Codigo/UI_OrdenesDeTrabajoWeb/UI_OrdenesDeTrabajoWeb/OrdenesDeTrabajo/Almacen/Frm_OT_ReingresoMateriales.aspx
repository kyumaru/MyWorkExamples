<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/Mp_Formulario.master" AutoEventWireup="false" CodeFile="Frm_OT_ReingresoMateriales.aspx.vb" Inherits="OrdenesDeTrabajo_Almacen_Frm_OT_ReingresoMateriales" %>

<%@ Register TagPrefix="wuc" TagName="InformacionGeneral" Src="~/ControlesDeUsuario/Wuc_OT_InformacionGeneral.ascx" %>
<%@ Register TagPrefix="wuc" Namespace="Utilerias.WebForms.Controles" Assembly="Utilerias.WebForms.Controles" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphFormulario" runat="Server">

    <header>
        <h2>
            <asp:Label runat="server" ID="lblAccion" Text="Reingreso de Materiales"></asp:Label>
        </h2>
    </header>

    <article class="tituloSeccion">
        Información General de la OT
    </article>

    <asp:UpdatePanel runat="server" ID="upPanelMateriales">

        <ContentTemplate>

            <article class="formulario">
                <table>
                    <tr>
                        <th>Número OT</th>
                        <td>
                            <asp:UpdatePanel runat="server" ID="uptxtIdOrdenTrabajo" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:TextBox runat="server" ID="txtIdOrdenTrabajo" data-tipocontrol="texto" AutoPostBack="true"></asp:TextBox>
                                    <asp:LinkButton ID="lnkEjecutarBusquedaOrden" runat="server">
                            <img id="imgBuscarOrdenTrabajo" title="Buscar" alt="Buscar" src="" />
                                    </asp:LinkButton>
                                    <br />
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="txtIdOrdenTrabajo" EventName="TextChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </article>

            <asp:UpdatePanel runat="server" ID="upControlDatosOrden" UpdateMode="Conditional" Visible="false">
                <ContentTemplate>
                    <article class="formulario sinBorde">
                        <wuc:InformacionGeneral runat="server" ID="ctrl_InfoGeneral" />
                    </article>

                    <br />

                    <article class="tituloSeccion">
                        Reingreso de Materiales
                    </article>
                    <br />
                    <article class="listado sinBorde">
                        <asp:Repeater runat="server" ID="rpMateriales" Visible="true">
                            <HeaderTemplate>
                                <table>
                                    <tr>
                                        <th>
                                            <asp:LinkButton runat="server" ID="lnkIdMaterial" Text="Código" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_MATERIALLST.ID_MATERIAL%>" CommandArgument="ASC" OnCommand="lnkRpMateriales_Command"></asp:LinkButton>
                                        </th>
                                        <th>
                                            <asp:LinkButton runat="server" ID="lnkDescipcion" Text="Descripción" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_MATERIALLST.DESCRIPCION%>" CommandArgument="ASC" OnCommand="lnkRpMateriales_Command"></asp:LinkButton>
                                        </th>
                                        <th>Cantidad Solicitada</th>
                                        <th>Cantidad Retirada</th>
                                        <th>Tipo de Solicitud</th>
                                        <th>Cant. a Reingresar</th>
                                        <th>&nbsp;</th>
                                    </tr>
                            </HeaderTemplate>

                            <ItemTemplate>
                                <tr class="lineaDelListado">
                                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_MATERIALLST.ID_MATERIAL)%></td>
                                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_MATERIALLST.DESCRIPCION)%></td>
                                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_MATERIALLST.CANTIDAD_SOLICITADA_MEDIDA)%></td>
                                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_MATERIALLST.CANTIDAD_RETIRADA_MEDIDA)%></td>
                                    <td>
                                        <asp:DropDownList ID="ddltipoSolicitud" runat="server" AppendDataBoundItems="true">
                                            <asp:ListItem Value="" Text="Seleccione"></asp:ListItem>
                                            <asp:ListItem Value="CUS" Text="Custodia"></asp:ListItem>
                                            <asp:ListItem Value="DEV" Text="Devolución"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtCantReingresar" Width="40%" ></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:HiddenField runat="server" ID="hdfIdDetalleMaterial" Value="<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_MATERIALLST.ID_DETALLE_MATERIAL)%>" />
                                    </td>
                                </tr>
                            </ItemTemplate>

                            <FooterTemplate>
                                </table>
                            </FooterTemplate>
                        </asp:Repeater>
                    </article>

                    <article class="areaCantidadDeRegistro">
                        <asp:Label runat="server" ID="lblCantidadDeRegistros" Text="" Visible="true"></asp:Label>
                    </article>

                    <br />

                    <article class="areaBotones">
                        <asp:Button runat="server" ID="btnTramitarReingreso" Text="Tramitar Reingreso" />
                        <asp:Button runat="server" ID="btnRegresar" Text="Cancelar" />
                    </article>

                    <br />

                    <article class="tituloSeccion">
                        Tramites de Reingreso
                    </article>

                    <br />

                    <article class="listado">
                        <asp:Repeater runat="server" ID="rpSolicitudReingreso">
                            <HeaderTemplate>
                                <table>
                                    <tr>
                                        <th>
                                            <asp:LinkButton runat="server" ID="lnkIdMaterial" Text="Código" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_SOLICITUD_REINGRESOLST.ID_MATERIAL%>" CommandArgument="ASC" OnCommand="lnkRpSolicitudReingreso_Command"></asp:LinkButton>
                                        </th>
                                        <th>
                                            <asp:LinkButton runat="server" ID="lnkDescipcion" Text="Descripción" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_SOLICITUD_REINGRESOLST.DESCRIPCION%>" CommandArgument="ASC" OnCommand="lnkRpSolicitudReingreso_Command"></asp:LinkButton>
                                        </th>
                                        <th>Cantidad a Reingresar
                                        </th>
                                        <th>Tipo Solicitud
                                        </th>
                                        <th>Estado
                                        </th>
                                    </tr>
                            </HeaderTemplate>

                            <ItemTemplate>
                                <tr class="lineaDelListado">
                                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_SOLICITUD_REINGRESOLST.ID_MATERIAL)%></td>
                                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_SOLICITUD_REINGRESOLST.DESCRIPCION)%></td>
                                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_SOLICITUD_REINGRESOLST.CANTIDAD_REINGRESO_MEDIDA)%></td>
                                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_SOLICITUD_REINGRESOLST.DESC_TIPO_SOL_REIN)%></td>
                                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_SOLICITUD_REINGRESOLST.DESC_ESTADO)%></td>
                                </tr>
                            </ItemTemplate>

                            <FooterTemplate>
                                </table>
                            </FooterTemplate>
                        </asp:Repeater>
                    </article>

                    <article class="areaPaginadorListado">
                        <wuc:PaginadorNumerico runat="server" ID="pnRpSolicitudReingreso" />
                    </article>

                    <article class="areaCantidadDeRegistro">
                        <asp:Label runat="server" ID="lblCantidadDeRegistros2" Text="" Visible="true"></asp:Label>
                    </article>

                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="txtIdOrdenTrabajo" EventName="TextChanged" />
                </Triggers>
            </asp:UpdatePanel>

        </ContentTemplate>
    </asp:UpdatePanel>

    <article id="arAlerta"></article>
    <article id="arpopupConfirmaDeseaBorrar"></article>
    <article id="popupConfirmaDeseaBorrar"></article>
    <article id="popupConfirmacionDeseaBorrar"></article>
    <article id="arPopupGenerico"></article>
    <article id="arPopupDelFormulario"></article>
    <article id="arAlertasDelFormulario"></article>

    <script type="text/javascript">

        function mostrarAlertaActualizacionExitosa() {
            deshabilitarControl('#<%=btnTramitarReingreso.ClientID%>');
            deshabilitarControl('#<%=btnRegresar.ClientID%>');
            $('.formulario').attr('disabled', 'disabled');

            mostrarAlerta(
            '#arAlertasDelFormulario',
            {
                mensaje: 'La información ha sido actualizada.',
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

        });

        function redireccionarListado(pvc_PaginaDestino) {
            window.location = pvc_PaginaDestino
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

        function mostrarPopUp(pvc_IdPopup) {
            window.location = pvc_IdPopup;
        };

        function inicializarFormulario() {

            $(window).keydown(function a(e) {

                if (e.keyCode == 13) {
                    return false;
                }
            });

            cargarLupa();

            permutarImagenes('#imgBuscarOrdenTrabajo',
               '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Lupa.png")%>',
                '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Lupa.png")%>',
                '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Lupa.png")%>'
            );

            $('[data-tipo="tooltip"]').attr('src', '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x24, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Informacion.png")%>');

            
            habilitarTooltipGenerico();

        };


        function cargarLupa() {
            permutarImagenes('#imgBuscarOrdenTrabajo',
                '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Lupa.png")%>',
                '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Lupa.png")%>',
                '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Lupa.png")%>'
            );
        };


        function mostrarAlertaNoEncontrado() {
            mostrarAlerta(
                '#arAlertasDelFormulario',
                {
                    mensaje: "Ningúna orden de trabajo a su cargo posee el número solicitado.",
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

    </script>

</asp:Content>

