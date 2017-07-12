<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/Mp_Formulario.master" AutoEventWireup="false" CodeFile="Frm_OT_RegistroIngresoCotizacion.aspx.vb" Inherits="OrdenesDeTrabajo_GestionesDeCompra_Frm_OT_RegistroIngresoCotizacion" %>

<%@ Register TagPrefix="wuc" Namespace="Utilerias.WebForms.Controles" Assembly="Utilerias.WebForms.Controles" %>
<%@ Register Src="~/Controles/wuc_OT_Lineas_Material_Gestion_Compra_Cotizacion.ascx" TagName="wuc_LineasMaterialGestionCompraCotizacion" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphFormulario" runat="Server">

    <header>
        <h2>Gestión de Compras por Fondo de Trabajo</h2>
    </header>

    <article class="tituloSeccion">
        Registro de Cotizaciones
    </article>

    <br />
    <asp:Repeater runat="server" ID="rpMateriales">
        <ItemTemplate>
            <article runat="server" id="arAcordeon" class="formulario sinBorde" data-tipo="acordeon">
                <article class="encabezadoAcordeon">
                    <a runat="server" id="ancorAcordeon" class="tituloAcordeon">
                        <asp:Label runat="server" ID="lblDescMaterial" Text='<%# String.Format("Descripción: {0}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GRUPO_GESTION_COMPRALST.DESCRIPCION))%>'></asp:Label>
                        <br />
                        <asp:Label runat="server" ID="lblCodigoMaterial" Text='<%# String.Format("Código: {0}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GRUPO_GESTION_COMPRALST.ID_MATERIAL))%>'></asp:Label><br />
                        <asp:Label runat="server" ID="lblCantidad" Text='<%# String.Format("Cantidad: {0}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GRUPO_GESTION_COMPRALST.CANTIDAD_SOLICITADA_MEDIDA))%>'></asp:Label>
                    </a>
                </article>
                <article runat="server" id="cuerpoAcordeon1" class="cuerpoAcordeon">
                    <uc1:wuc_LineasMaterialGestionCompraCotizacion runat="server" ID="wucLineasMaterialGestionCompraCotizacion" IdUbicacion='<%# Eval("ID_UBICACION")%>' IdViaCompraContrato='<%# Eval("ID_VIA_COMPRA_CONTRATO")%>' IdAnnio='<%# Eval("ANNO")%>' IdNumeroGestion='<%# Eval("NUMERO_GESTION")%>' IdMaterial='<%# Eval("ID_MATERIAL")%>' />
                </article>
            </article>
        </ItemTemplate>
    </asp:Repeater>
    <br />

    <article class="tituloSeccion">
        Ingreso de Cotizaciones
    </article>

    <article class="listado sinBorde">
        <asp:Repeater runat="server" ID="rpAdjuntos">
            <HeaderTemplate>
                <table>
                    <tr>
                        <th>Proveedor</th>
                        <th>Archivo</th>
                        <th>Descripción</th>
                        <th>&nbsp;</th>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr class="lineaDelListado">
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_ADJUNTO_PROVEEDOR.NOMBRE_PROVEEDOR)%></td>
                    <td>
                        <asp:LinkButton runat="server" ID="lnkArchivo"
                            Visible='<%#IIf(CType(Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_ADJUNTO_PROVEEDOR.NOMBRE_ARCHIVO), String) = "", False, True)%>'
                            CommandArgument='<%# String.Format("{0}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_ADJUNTO_PROVEEDOR.IDENTIFICACION))%>'
                            CommandName='<%#Container.ItemIndex%>'
                            Style="text-decoration: underline; color: blue;"
                            OnCommand="lnkArchivo_Command"
                            Text="<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_ADJUNTO_PROVEEDOR.NOMBRE_ARCHIVO)%>"></asp:LinkButton>

                        <asp:LinkButton runat="server" ID="lnkCargar"
                            Visible='<%#IIf(CType(Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_ADJUNTO_PROVEEDOR.NOMBRE_ARCHIVO), String) = "", True , False)%>'
                            CommandArgument='<%# String.Format("{0}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_ADJUNTO_PROVEEDOR.IDENTIFICACION))%>'
                            Style="text-decoration: underline; color: blue;"
                            OnCommand="lnkCargar_Command"
                            Text="Adjuntar Archivo"></asp:LinkButton>
                    </td>
                    <td style="text-align: center">
                        <asp:Image runat="server" ID="Image1" data-tipo="tooltip"
                            Visible='<%#IIf(CType(Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_ADJUNTO_PROVEEDOR.NOMBRE_ARCHIVO), String) = "", False , True)%>'
                            title="<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_ADJUNTO_PROVEEDOR.DESCRIPCION)%>"
                            ImageUrl='<%#AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Informacion.png")%>' />
                    </td>
                    <td>
                        <asp:ImageButton runat="server" ID="ibBorrar" data-tipo="borrarRegistro" AlternateText="Borrar"
                            CommandArgument='<%# String.Format("{0}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_ADJUNTO_PROVEEDOR.IDENTIFICACION))%>' OnClick="ibBorrar_Click"
                            Visible='<%#IIf(CType(Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_ADJUNTO_PROVEEDOR.NOMBRE_ARCHIVO), String) = "", False , True)%>'
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
        <asp:Button runat="server" ID="btnCancelar" />
        <asp:Button runat="server" ID="btnGuardar" Text="Guardar" Visible="<%#IIf((CType(Me.GestionCompra.Estado, String) = Utilerias.OrdenesDeTrabajo.EstadoGestionCompra.REGISTRO_DE_COTIZACIONES), True, False)%>"/>
        <asp:Button runat="server" ID="btnEnviarAprobacion" Text="Enviar a Aprobación" Visible="<%#IIf((CType(Me.GestionCompra.Estado, String) = Utilerias.OrdenesDeTrabajo.EstadoGestionCompra.REGISTRO_DE_COTIZACIONES), True, False)%>"/>
    </article>

    <article id="arAlerta"></article>
    <article id="arpopupConfirmaDeseaBorrar"></article>
    <article id="arPopupGenerico"></article>
    <article id="arAlertasDelFormulario"></article>
    <article id="arpopupConfirmaEnviar"></article>

    <%--Popup para búsqueda de Archivos--%>
    <article id="PopUpBusquedaArchivos" class="ventanaEmergente">
        <article class="formulario" style="width: 850px!important;">
            <a href="#CerrarPopUpBusquedaArchivos" title="Cerrar Ventana" class="botonCerrarVentanaEmergente">X</a>
            <br />
            <asp:UpdatePanel ID="upArchivos" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <article class="tituloSeccion">
                        Adjuntar Archivo
                    </article>
                    <article class="formulario sinBorde">
                        <table>
                            <tr>
                                <th>Archivo</th>
                                <td>
                                    <asp:FileUpload Width="50%" runat="server" ID="ifArchivo" />
                                </td>
                            </tr>
                            <tr>
                                <th>Descripción</th>
                                <td>
                                    <asp:TextBox runat="server" ID="txtDescripcionArchivo" TextMode="MultiLine" Rows="5" Width="50%"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </article>
                    <div class="areaBotones">
                        <asp:Button runat="server" ID="btnAgregarArchivo" Text="Agregar" />
                        <a href="#CerrarPopUpBusquedaArchivos" title="Regresar">Regresar</a>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </article>
    </article>
    <%--Fin Popup para búsqueda de Archivos--%>

    <script type="text/javascript">

        function mostrarAlertaCamposIncompletos() {
            mostrarAlerta(
                '#arAlerta',
                {
                    mensaje: 'El archivo y la descripción son campos obligatorios.',
                    tipo: "peligro",
                    transparencia: 0.9,
                    posicion: 'center',
                    onClosed: function () { window.location = '#PopUpBusquedaArchivos'; }
                }
            );
        };

        function ocultarFiltroArchivo() {
            window.location = '#CerrarPopUpBusquedaArchivos';
        };

        function mostrarPopUp(pvc_IdPopup) {
            window.location = pvc_IdPopup;
        };

        function mostrarAlertaError(pvc_Mensaje) {
            mostrarAlerta(
                '#arAlerta',
                {
                    mensaje: pvc_Mensaje,
                    tipo: 'peligro',
                    transparencia: 0.9,
                    posicion: 'center'
                }
            );
        };

        $(document).ready(function () {

            $('article[data-tipo="acordeon"]').each(function () {
                configurarAcordeon('#' + this.id, { seleccionMultiple: false, eventoApertura: 'click' });
            });

            $('[data-tipo="tooltip"]').each(function () {
                $('#' + this.id).tooltipster({
                    interactive: true,
                    interactiveTolerance: 400,
                    theme: 'tooltipster-light'
                });
            });

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

    </script>

</asp:Content>

