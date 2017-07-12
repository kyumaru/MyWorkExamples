<%@ Page Language="VB" AutoEventWireup="false" MaintainScrollPositionOnPostback="true" CodeFile="Frm_OT_Requisicion.aspx.vb" MasterPageFile="~/MasterPage/Mp_Formulario.master" Inherits="OrdenesDeTrabajo_Almacen_Frm_OT_Requisicion" %>

<%@ Register TagPrefix="wuc" TagName="InformacionGeneral" Src="~/ControlesDeUsuario/Wuc_OT_InformacionGeneral.ascx" %>
<%@ Register TagPrefix="wuc" Namespace="Utilerias.WebForms.Controles" Assembly="Utilerias.WebForms.Controles" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register TagPrefix="wuc" TagName="Materiales" Src="~/Controles/wuc_OT_Materiales.ascx" %>
<%@ Register TagPrefix="wuc" TagName="DatosMaterial" Src="~/Controles/wuc_OT_DatosMaterial.ascx" %>

<asp:Content runat="server" ID="Content1" ContentPlaceHolderID="cphFormulario">

    <header>
        <h2>Revisión de Requisición de Materiales</h2>
    </header>

    <article>
        <wuc:InformacionGeneral ID="ctrl_InfoGeneral" runat="server" />
    </article>

    <article class="tituloSeccion">
        Materiales Solicitados
    </article>
    <article class="formulario">
        <table>
            <tr>
                <th>Observaciones Generales</th>
                <td>
                    <asp:TextBox runat="server" ID="txtObservacionesGenerales" TextMode="MultiLine" Rows="4" Columns="60" Enabled="false"></asp:TextBox></td>
            </tr>
        </table>
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
                <asp:Button runat="server" ID="btnAgregar" ValidationGroup="AgregarListado" Text="Agregar" OnClick="btnAgregar_Click" />
                <asp:Button runat="server" ID="btnModificar" Visible="false" Text="Modificar" OnClick="btnModificar_Click" />
                <asp:Button runat="server" ID="btnCancelar" Text="Cancelar" Visible="false" OnClick="btnCancelar_Click" />
            </article>

            <article class="tituloSeccion" visible="false" id="tituloListado" runat="server">
                Listado de Materiales
            </article>
            <article class="listado" data-grupo="Listado">
                <asp:Repeater runat="server" ID="rpPedidos">
                    <HeaderTemplate>
                        <table>
                            <tr>
                                <th>
                                    <asp:LinkButton runat="server" ID="lnkCodigo" Text="Código"></asp:LinkButton>
                                </th>
                                <th>
                                    <asp:LinkButton runat="server" ID="lnkDescripcion" Text="Descripción"></asp:LinkButton>
                                </th>
                                <th>
                                    <asp:LinkButton runat="server" ID="lnkDetalle" Text="Detalle"></asp:LinkButton>
                                </th>
                                <th>
                                    <asp:LinkButton runat="server" ID="lnkDisponible" Text="Disp. Almacen"></asp:LinkButton>
                                </th>
                                <th>
                                    <asp:LinkButton runat="server" ID="lnkCantidadSolicitada" Text="Cantidad Solicitada"></asp:LinkButton>
                                </th>
                                <th>
                                    <asp:LinkButton runat="server" ID="lnkMonto" Text="Monto"></asp:LinkButton>
                                </th>
                                <th>
                                    <asp:LinkButton runat="server" ID="lnkAlmacen" Text="Vía de compra / Almacen"></asp:LinkButton>
                                </th>
                                <th>&nbsp;</th>
                                <th>&nbsp;</th>
                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_MATERIALLST.ID_MATERIAL)%></td>
                            <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_MATERIALLST.DESCRIPCION)%></td>
                            <td style="text-align: center">
                                <asp:Image runat="server" ID="imgDetalle" data-tipo="tooltip"
                                    title="<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_MATERIALLST.DETALLE)%>"
                                    ImageUrl='<%#AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Informacion.png")%>' />
                            </td>
                            <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_MATERIALLST.DISP_ALM_SOLIC_MEDIDA_RESERV)%></td>
                            <td>
                                <asp:Label
                                    runat="server"
                                    Text="<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_MATERIALLST.CANTIDAD_SOLICITADA_MEDIDA)%>"
                                    Visible='<%#IIf(CType(Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_MATERIALLST.CANTIDAD_RESERVADA), String) = "0", True, False)%>'></asp:Label>

                                <asp:Label
                                    runat="server"
                                    CssClass="colorVerde"
                                    Text="<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_MATERIALLST.CANTIDAD_SOLICITADA_MEDIDA)%>"
                                    Visible='<%#IIf(CType(Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_MATERIALLST.CANTIDAD_RESERVADA), String) <> "0", True, False)%>'></asp:Label>

                            </td>
                            <td><%#CType(Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_MATERIALLST.COSTO_PROMEDIO), Double).ToString("N2")%></td>
                            <td>
                                <asp:DropDownList runat="server"
                                    data-idMaterial='<%#DataBinder.Eval(Container.DataItem, Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_MATERIALLST.ID_MATERIAL)%>'
                                    data-idUbicacion='<%#DataBinder.Eval(Container.DataItem, Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_MATERIALLST.ID_UBICACION_ADMINISTRA)%>'
                                    data-IdAlmacenBodegaCombo='<%#DataBinder.Eval(Container.DataItem, Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_MATERIALLST.ID_ALMACEN_BODEGA)%>'
                                    data-IdViaCompraContratoCombo='<%#DataBinder.Eval(Container.DataItem, Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_MATERIALLST.ID_VIA_COMPRA_CONTRATO)%>'
                                    data-IdViaCompraCombo='<%#DataBinder.Eval(Container.DataItem, Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_MATERIALLST.VIA_DESPACHO)%>'
                                    data-DispAlmacenBodega='<%#DataBinder.Eval(Container.DataItem, Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_MATERIALLST.DISP_ALMACEN_SOLICITUD_MEDIDA)%>'
                                    data-CantidadSolicitadaMedida='<%#DataBinder.Eval(Container.DataItem, Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_MATERIALLST.CANTIDAD_SOLICITADA_MEDIDA)%>'
                                    ID="ibAlmacen"
                                    OnSelectedIndexChanged="ibAlmacen_SelectedIndexChanged"
                                    AutoPostBack="true"
                                    AppendDataBoundItems="true">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:ImageButton runat="server" data-tipo="modificarRegistro" ID="ibModificar" AlternateText="Modificar el pedido"
                                    CommandArgument="<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_MATERIALLST.ID_DETALLE_MATERIAL)%>"
                                    OnClick="ibModificar_Click"
                                    ImageUrl='<%#AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Editar.png")%>'
                                    onmouseover='<%# String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Icono_Editar.png"))%>'
                                    onmouseout='<%# String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Editar.png"))%>' />
                            </td>
                            <td>
                                <asp:ImageButton runat="server" ID="ibBorrar" AlternateText="Borrar el pedido" data-tipo="borrarRegistro"
                                    CommandArgument="<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_MATERIALLST.ID_DETALLE_MATERIAL)%>"
                                    OnClick="ibBorrar_Click"
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

    <article class="tituloSeccion">
        Solicitud de presupuesto a la unidad Solicitante
    </article>

    <article class="formulario">
        <table>
            <tr runat="server" visible="false" id="tr1">
                <th>Adjunto para solicitud de presupuesto</th>
                <td>
                    <asp:LinkButton runat="server" ID="lnkSolicitudPresupuestaria"></asp:LinkButton>
                </td>
            </tr>
            <tr runat="server" visible="false" id="tr2">
                <th>Adjunto de respuesta a la solicitud de presupuesto</th>
                <td>
                    <asp:LinkButton runat="server" ID="lnkSolicitudPrep"></asp:LinkButton>
                </td>
            </tr>
        </table>
    </article>

    <article class="tituloSeccion">
        Revisión de la solicitud
    </article>


    <asp:UpdatePanel runat="server" ID="upObservaciones" UpdateMode="Conditional">
        <ContentTemplate>
            <article class="formulario">
                <table>
                    <tr>
                        <td>
                            <asp:RadioButton ID="rbtAprobada" ValidationGroup="Aceptar" Checked="true" runat="server" Text="Aprobar" GroupName="Condicion" AutoPostBack="true" />
                        </td>
                        <td>
                            <asp:RadioButton ID="rbtDevuelta" ValidationGroup="Aceptar" runat="server" Text="Solicitar Revisión de Presupuesto al Supervisor" GroupName="Condicion" AutoPostBack="true" />
                        </td>
                        <td>
                            <asp:RadioButton ID="rbtRechazada" ValidationGroup="Aceptar" runat="server" Text="Rechazar" GroupName="Condicion" AutoPostBack="true" />
                        </td>
                    </tr>
                    <tr runat="server" id="trObservaciones">
                        <th>Observaciones</th>
                        <td>
                            <asp:TextBox runat="server" ID="txtObservacion" TextMode="MultiLine" Rows="4" Columns="60"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" Enabled="false" ID="rfvTxtObservaciones" ControlToValidate="txtObservacion"
                                Display="Dynamic" ErrorMessage="Las observaciones son obligatorias." ValidationGroup="Aceptar">&nbsp;</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                </table>
            </article>
        </ContentTemplate>
    </asp:UpdatePanel>

    <article class="areaBotones">
        <asp:Button runat="server" ID="btnAceptar" ValidationGroup="Aceptar" Text="Aceptar" OnClick="btnAceptar_Click" />
        <input type="button" id="btnRegresar" value="Regresar" />
    </article>

    <article id="popupConfirmacionDeseaBorrar"></article>
    <article id="arAlertasDelFormulario"></article>

    <style>
        .colorVerde {
            /*color: green !important;*/
            background-color: #FFFF00 !important;
        }
    </style>

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

        function cargarTooltip() {

            $('[data-tipo="tooltip"]').each(function () {
                $('#' + this.id).tooltipster({
                    interactive: true,
                    interactiveTolerance: 400,
                    theme: 'tooltipster-light'
                });
            });

            $('[data-tipo="borrarRegistro"]').click(function () { return mostrarPopupConfirmacionDeseaBorrarRegistro($(this).data("uniqueid")); });
        };

        function mostrarPopupConfirmacionDeseaBorrarRegistro(pvc_UniqueIdControl) {
            var vlo_ConfiguracionPopup = {
                titulo: "<em>Catalogo de requisición de materiales</em>",
                mensaje: '<%=String.Format("¿Está seguro de eliminar el material {0} del listado?", Me.WucDatosMaterial.RetornaDescripcion)%>',
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

        function mostrarPopUp(pvc_IdPopup) {
            window.location = pvc_IdPopup;
        };

        function ocultarFiltroMateriales() {
            window.location = '#CerrarPopUpBusquedaMateriales';
        };

        function inicializarFormulario() {

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

        function regresarAlListado() {
            window.location = 'Lst_OT_Requisiciones.aspx';
        };

        $(document).ready(function () {
            inicializarFormulario();

            $('#btnRegresar').click(function () {
                regresarAlListado();
            });

            cargarTooltip();
        });

        function ocultarFiltroFuncionario() {
            window.location = '#CerrarPopUpBusquedaFuncionario';
        };

        function cargarLupa() {
            permutarImagenes('#imgBuscarMaterial',
                '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Lupa.png")%>',
                '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Lupa.png")%>',
                '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Lupa.png")%>'
            );

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

        function mostrarAlertaGuardadoExitoso() {
            deshabilitarControl('#<%=btnAceptar.ClientID%>');
            deshabilitarControl('#<%=btnAgregar.ClientID%>');
            $('.formulario').attr('disabled', 'disabled');

            mostrarAlerta(
            '#arAlertasDelFormulario',
            {
                mensaje: 'Se ha guardado los datos indicados para la requisición',
                tipo: "exito",
                transparencia: 0.9,
                posicion: 'center',
                onClosed: function () { window.location = 'Lst_OT_Requisiciones.aspx'; }
            });
        };

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
