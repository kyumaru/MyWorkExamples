<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/Mp_Formulario.master" AutoEventWireup="false" CodeFile="Frm_OT_SolicitudAjusteMaterial.aspx.vb" Inherits="OrdenesDeTrabajo_Almacen_Frm_OT_SolicitudAjusteMaterial" %>

<%@ Register TagPrefix="wuc" TagName="InformacionGeneral" Src="~/ControlesDeUsuario/Wuc_OT_InformacionGeneral.ascx" %>
<%@ Register TagPrefix="wuc" Namespace="Utilerias.WebForms.Controles" Assembly="Utilerias.WebForms.Controles" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register TagPrefix="wuc" TagName="Materiales" Src="~/Controles/wuc_OT_Materiales.ascx" %>
<%@ Register TagPrefix="wuc" TagName="DatosMaterial" Src="~/Controles/wuc_OT_DatosMaterial.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphFormulario" runat="Server">

    <header>
        <h2>
            <asp:Label runat="server" ID="lblAccion" Text="Solicitud de Materiales"></asp:Label>
        </h2>
    </header>

    <article class="tituloSeccion">
        Información General de la OT
    </article>
    <article class="formulario sinBorde">
        <wuc:InformacionGeneral runat="server" ID="ctrl_InfoGeneral" />
    </article>
    <br />
    <article class="tituloSeccion">
        Materiales Solicitados
    </article>
    <article class="listado">
        <asp:Repeater runat="server" ID="rpPedidosPrevios">
            <HeaderTemplate>
                <table>
                    <tr>
                        <th>
                            <asp:LinkButton runat="server" ID="lnkIdMaterial" Text="Código" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_MATERIALLST.ID_MATERIAL%>" CommandArgument="ASC" OnCommand="lnkRpPedidos_Command"></asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton runat="server" ID="lnkDescipcion" Text="Descripción" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_MATERIALLST.DESCRIPCION%>" CommandArgument="ASC" OnCommand="lnkRpPedidos_Command"></asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton runat="server" ID="lnkCantSolicitada" Text="Cantidad Solicitada" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_MATERIALLST.CANTIDAD_SOLICITADA_MEDIDA%>" CommandArgument="ASC" OnCommand="lnkRpPedidos_Command"></asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton runat="server" ID="lknDispAlmacen" Text="Disp. Almacen" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_MATERIALLST.DISPONIBLE_ALMACEN_SOLICITUD%>" CommandArgument="ASC" OnCommand="lnkRpPedidos_Command"></asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton runat="server" ID="lnkDescEstado" Text="Estado" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_MATERIALLST.DESC_ESTADO%>" CommandArgument="ASC" OnCommand="lnkRpPedidos_Command"></asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton runat="server" ID="lnkCostoPromedio" Text="Monto" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_MATERIALLST.COSTO_PROMEDIO%>" CommandArgument="ASC" OnCommand="lnkRpPedidos_Command"></asp:LinkButton>
                        </th>
                    </tr>
            </HeaderTemplate>

            <ItemTemplate>
                <tr class="lineaDelListado">
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_MATERIALLST.ID_MATERIAL)%></td>
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_MATERIALLST.DESCRIPCION)%></td>
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_MATERIALLST.CANTIDAD_SOLICITADA_MEDIDA)%></td>
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_MATERIALLST.DISPONIBLE_ALMACEN_SOLICITUD)%></td>
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_MATERIALLST.DESC_ESTADO)%></td>
                    <td style="text-align: right !important;"><%#CType(Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_MATERIALLST.COSTO_PROMEDIO), Double).ToString("N2")%></td>
                </tr>
            </ItemTemplate>

            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
    </article>

    <article class="areaPaginadorListado">
        <wuc:PaginadorNumerico runat="server" ID="pnRpPedidosPrevios" />
    </article>

    <article class="areaCantidadDeRegistro" data-grupo="Listado">
        <asp:Label runat="server" ID="lblMontoTotalPrevio" Text=""></asp:Label>
    </article>

    <article class="areaCantidadDeRegistro">
        <asp:Label runat="server" ID="lblCantidadDeRegistros" Text="" Visible="true"></asp:Label>
    </article>

    <br />

    <article class="tituloSeccion">
        Solicitud de Materiales
    </article>

    <asp:UpdatePanel runat="server" ID="upPanelMateriales">

        <ContentTemplate>
            <article class="formulario">
                <table>
                    <tr>
                        <th>Código</th>
                        <td>
                            <asp:UpdatePanel runat="server" ID="upTxtCodigo" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:TextBox runat="server" ID="txtCodigo" data-tipocontrol="texto" AutoPostBack="true"></asp:TextBox>
                                    <asp:RequiredFieldValidator runat="server" ID="rfvTxtCodigo" ControlToValidate="txtCodigo"
                                        Display="Dynamic" ErrorMessage="El código del material es requerido." ValidationGroup="AgregarListado">&nbsp;</asp:RequiredFieldValidator>
                                    <ajax:FilteredTextBoxExtender ID="ftbTxtCodigo" runat="server" TargetControlID="txtCodigo" FilterType="Numbers"></ajax:FilteredTextBoxExtender>
                                    <asp:LinkButton ID="lnkEjecutarBusquedaMaterial" runat="server">
                            <img id="imgBuscarMaterial" title="Buscar Material" alt="Buscar Material" src="" />
                                    </asp:LinkButton>
                                    <br />
                                    <span id="spContadorTxtIdSolicitante" class="contadorCaracteresRestantes"></span>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="txtCodigo" EventName="TextChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </article>

            <asp:UpdatePanel runat="server" ID="upControlDatosMaterial" UpdateMode="Conditional" Visible="false">
                <ContentTemplate>
                    <wuc:DatosMaterial ID="WucDatosMaterial" runat="server"></wuc:DatosMaterial>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="txtCodigo" EventName="TextChanged" />
                    <asp:AsyncPostBackTrigger ControlID="ctrl_Materiales" />
                </Triggers>
            </asp:UpdatePanel>

            <article class="areaBotones">
                <asp:Button runat="server" ID="btnAgregarMaterial" Text="Agregar" ValidationGroup="AgregarListado" />
                <asp:Button runat="server" ID="btnModificarMaterial" Text="Modificar" Visible="false" />
                <asp:Button runat="server" ID="btnCancelarMaterial" Text="Cancelar" Visible="false" />
            </article>

            <br />

            <%--acá utilizar vista de hizo cesar--%>
            <article data-grupo="Listado" class="listado sinBorde">
                <asp:Repeater runat="server" ID="rpPedidos">
                    <HeaderTemplate>
                        <table>
                            <tr>
                                <th>Código</th>
                                <th>Descripción</th>
                                <th>Detalle</th>
                                <th>Disp. Almacen</th>
                                <th>Cantidad Solicitada</th>
                                <th>Monto</th>
                                <th>&nbsp;</th>
                                <th>&nbsp;</th>
                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_MATERIALLST.ID_MATERIAL)%></td>
                            <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_MATERIALLST.DESCRIPCION)%></td>
                            <td style="text-align: center">
                                <asp:Image runat="server" ID="imgDetalle" data-tipo="tooltipDetalleMaterial"
                                    title='<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_MATERIALLST.DETALLE)%>'
                                    ImageUrl='<%#AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Informacion.png")%>' />
                            </td>
                            <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_MATERIALLST.DISPONIBLE_ALMACEN_SOLICITUD)%></td>
                            <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_MATERIALLST.CANTIDAD_SOLICITADA)%></td>
                            <td style="text-align: right !important;"><%#CType(Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_MATERIALLST.COSTO_PROMEDIO), Double).ToString("N2")%></td>
                            <td>
                                <asp:ImageButton runat="server" data-tipo="modificarRegistro" ID="ibModificarMaterial" AlternateText="Modificar el pedido"
                                    CommandArgument="<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_MATERIALLST.ID_DETALLE_MATERIAL)%>"
                                    OnClick="ibModificarMaterial_Click"
                                    ImageUrl='<%#AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Editar.png")%>'
                                    onmouseover='<%# String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Icono_Editar.png"))%>'
                                    onmouseout='<%# String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Editar.png"))%>' />
                            </td>
                            <td>
                                <asp:ImageButton runat="server" ID="ibBorrarMaterial" AlternateText="Borrar el pedido" data-tipo="borrarRegistroMaterial"
                                    CommandArgument="<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_MATERIALLST.ID_DETALLE_MATERIAL)%>"
                                    OnClick="ibBorrarMaterial_Click"
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

            <article class="areaCantidadDeRegistro" data-grupo="Listado">
                <asp:Label runat="server" ID="lblMontoTotal" Text=""></asp:Label>
            </article>
        </ContentTemplate>
    </asp:UpdatePanel>

    <br />

    <article class="formulario sinBorde">
        <table>
            <tr>
                <th>Historial de Justificaciones</th>
                <td>
                    <asp:TextBox runat="server" ID="lblJustficacionMaterial" TextMode="MultiLine" Columns="70" Rows="9" Enabled="false" Style="overflow:scroll"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>Justificación</th>
                <td>
                    <asp:TextBox runat="server" ID="txtJustificacionMaterial" TextMode="MultiLine" Rows="4" Columns="70"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ID="rfvTxtJustificacionMaterial" ControlToValidate="txtJustificacionMaterial"
                        Display="Dynamic" ErrorMessage="La justificación es requerida." ValidationGroup="justificacionSolicitud">&nbsp;</asp:RequiredFieldValidator>
                </td>
            </tr>
        </table>
    </article>

    <article class="areaBotones">
        <asp:Button runat="server" ID="btnGuardarYFinalizar" ValidationGroup="justificacionSolicitud" Text="Enviar a aprobación" />
        <asp:Button runat="server" ID="btnRegresar" Text="Regresar" />
    </article>

    <article id="arAlerta"></article>
    <article id="arpopupConfirmaDeseaBorrar"></article>
    <article id="popupConfirmaDeseaBorrar"></article>
    <article id="popupConfirmacionDeseaBorrar"></article>
    <article id="arPopupGenerico"></article>
    <article id="arPopupDelFormulario"></article>
    <article id="arAlertasDelFormulario"></article>

    <%--Popup para búsqueda de materiales--%>
    <article id="PopUpBusquedaMateriales" class="ventanaEmergente">
        <article class="formulario" style="width: 850px!important;">
            <a href="#CerrarPopUpBusquedaMateriales" title="Cerrar Ventana" class="botonCerrarVentanaEmergente">X</a>
            <br />
            <asp:UpdatePanel ID="upContactoConv" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <wuc:Materiales ID="ctrl_Materiales" runat="server"></wuc:Materiales>
                    <div class="areaBotones">
                        <a href="#CerrarPopUpBusquedaMateriales" title="Regresar">Regresar</a>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </article>
    </article>
    <%--Fin Popup para búsqueda de materiales--%>

    <script type="text/javascript">
        function InhabilitarCodigo() {

            document.getElementById("imgBuscarMaterial").style.visibility = "hidden";

        };

        function HabilitarCodigo() {

            document.getElementById("imgBuscarMaterial").style.visibility = "visible";

        };

        function mostrarPopupConfirmacionDeseaBorrarRegistro(pvc_UniqueIdControl) {
            var vlo_ConfiguracionPopup = {
                titulo: "<em>Catalogo de requisición de materiales</em>",
                mensaje: '<%=String.Format("¿Está seguro de eliminar el material {0} del listado?",  Me.WucDatosMaterial.RetornaDescripcion)%>',
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

        function mostrarAlertaActualizacionExitosa() {
            deshabilitarControl('#<%=btnGuardarYFinalizar.ClientID%>');
            deshabilitarControl('#<%=btnRegresar.ClientID%>');
            $('.formulario').attr('disabled', 'disabled');

            mostrarAlerta(
            '#arAlertasDelFormulario',
            {
                mensaje: 'La información ha sido enviada a aprobación.',
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

        function ocultarFiltroMateriales() {
            window.location = '#CerrarPopUpBusquedaMateriales';
        };

        function mostrarPopUp(pvc_IdPopup) {
            window.location = pvc_IdPopup;
        };

        function inicializarFormulario() {

            $('[data-tipo="tooltipDetalleMaterial"]').each(function () {

                $('#' + this.id).tooltipster({
                    interactive: true,
                    interactiveTolerance: 400,
                    theme: 'tooltipster-light'
                });
            });
            
            $('[data-tipo="borrarRegistroMaterial"]').click(function () { return mostrarPopupConfirmacionDeseaBorrarRegistro($(this).data("uniqueid")); });

            $(window).keydown(function a(e) {

                if (e.keyCode == 13) {
                    return false;
                }
            });

            cargarLupa();

            permutarImagenes('#imgBuscarMaterial',
               '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Lupa.png")%>',
                '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Lupa.png")%>',
                '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Lupa.png")%>'
            );
        };

        function cargarLupa() {
            permutarImagenes('#imgBuscarMaterial',
                '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Lupa.png")%>',
                '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Lupa.png")%>',
                '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Lupa.png")%>'
            );
        };

        function mostrarAlertSinCantidadDisponible() {
            mostrarAlerta(
                '#arAlertasDelFormulario',
                {
                    mensaje: "La bodega o almacén seleccionado no posee suficiente material",
                    tipo: "advertencia",
                    transparencia: 0.9,
                    posicion: 'center',
                    permiteCerrar: true
                }
                );
        };

        function mostrarAlertCantidadCero() {
            mostrarAlerta(
                '#arAlertasDelFormulario',
                {
                    mensaje: "La cantidad solicitada debe ser mayor a cero.",
                    tipo: "advertencia",
                    transparencia: 0.9,
                    posicion: 'center',
                    permiteCerrar: true
                }
                );
        };


        function mostrarAlertaNoEncontrado() {
            mostrarAlerta(
                '#arAlertasDelFormulario',
                {
                    mensaje: "Ningúna bodega o almacén poseen el código solicitado.",
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

