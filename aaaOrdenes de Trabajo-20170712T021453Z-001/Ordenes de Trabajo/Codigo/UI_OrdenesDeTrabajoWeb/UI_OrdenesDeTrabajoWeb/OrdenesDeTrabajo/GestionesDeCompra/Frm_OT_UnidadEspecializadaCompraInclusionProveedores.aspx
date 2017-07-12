<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/Mp_Formulario.master" AutoEventWireup="false" CodeFile="Frm_OT_UnidadEspecializadaCompraInclusionProveedores.aspx.vb" Inherits="OrdenesDeTrabajo_GestionesDeCompra_Frm_OT_UnidadEspecializadaCompraInclusionProveedores" %>

<%@ Register TagPrefix="wuc" Namespace="Utilerias.WebForms.Controles" Assembly="Utilerias.WebForms.Controles" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphFormulario" runat="Server">

    <header>
        <h2>Gestión de Compras por Unidad Especializada de Compra</h2>
    </header>

    <article class="tituloSeccion">
        Proveedores
    </article>

    <article class="formulario">
        <table>
            <tr>
                <th>Buscar por Descripción</th>
                <td>
                    <asp:TextBox runat="server" ID="txtFiltroDescripcion"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                      <asp:LinkButton ID="lnkEjecutarBusqueda" runat="server">
                            <img id="imgBuscarProveedores" title="Buscar Proveedores" alt="Buscar Proveedores" src="" />
                      </asp:LinkButton>
                </td>
            </tr>
            <tr>
                <th>Proveedor</th>
                <td>
                    <asp:DropDownList runat="server" ID="ddlProveedores" AppendDataBoundItems="true"></asp:DropDownList>
                    <asp:RequiredFieldValidator runat="server" ID="rfvddlProveedores" ControlToValidate="ddlProveedores" Display="Dynamic" ErrorMessage="Debe seleccionar un proveedor." ValidationGroup="Agregar"></asp:RequiredFieldValidator>
                </td>
            </tr>
        </table>
    </article>

    <article class="areaBotones">
        <asp:Button runat="server" ID="btnAgregar" Text="Agregar" ValidationGroup="Agregar" />
        <asp:Button runat="server" ID="btnLimpiarFiltro" Text="Limpiar Filtro" />
    </article>

    <article class="listado sinBorde">
        <asp:Repeater runat="server" ID="rpProveedoresCotizacion">
            <HeaderTemplate>
                <table>
                    <tr>
                        <th>
                            <asp:LinkButton runat="server" ID="lnkCedula" Text="Cédula" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_PROVEEDOR_COTIZACIONLST.IDENTIFICACION%>" CommandArgument="ASC" OnCommand="lnkRpProveedoresCotizacion_Command"></asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton runat="server" ID="lnkNombre" Text="Nombre" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_PROVEEDOR_COTIZACIONLST.NOMBRE_PROVEEDOR%>" CommandArgument="ASC" OnCommand="lnkRpProveedoresCotizacion_Command"></asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton runat="server" ID="lnkCorreos" Text="Correo(s)" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_PROVEEDOR_COTIZACIONLST.CORREO_PROVEDOR%>" CommandArgument="ASC" OnCommand="lnkRpProveedoresCotizacion_Command"></asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton runat="server" ID="lnkTelefonos" Text="Teléfono(s)" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_PROVEEDOR_COTIZACIONLST.TELEFONOS_PROVEEDOR%>" CommandArgument="ASC" OnCommand="lnkRpProveedoresCotizacion_Command"></asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton runat="server" ID="lnkEstado" Text="Estado" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_PROVEEDOR_COTIZACIONLST.DESC_ESTADO%>" CommandArgument="ASC" OnCommand="lnkRpProveedoresCotizacion_Command"></asp:LinkButton>
                        </th>
                        <th>&nbsp;</th>
                        <th>&nbsp;</th>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr class="lineaDelListado">
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_PROVEEDOR_COTIZACIONLST.IDENTIFICACION)%></td>
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_PROVEEDOR_COTIZACIONLST.NOMBRE_PROVEEDOR)%></td>
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_PROVEEDOR_COTIZACIONLST.CORREO_PROVEDOR)%></td>
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_PROVEEDOR_COTIZACIONLST.TELEFONOS_PROVEEDOR)%></td>
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_PROVEEDOR_COTIZACIONLST.DESC_ESTADO)%></td>
                    <td>
                        <article style="display: <%#IIf((CType(Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_PROVEEDOR_COTIZACIONLST.ESTADO), String) = Utilerias.OrdenesDeTrabajo.EstadoProveedorCotizacion.PENDIENTE_DE_ENVIO), "block", "none")%>">
                            <asp:ImageButton runat="server" ID="ibBorrar" AlternateText="Borrar Registro" ToolTip="Borrar Registro"
                                CommandArgument='<%# String.Format("{0}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_PROVEEDOR_COTIZACIONLST.IDENTIFICACION))%>'
                                ImageUrl='<%#AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Borrar.png")%>'
                                onmouseover='<%#String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Icono_Borrar.png"))%>'
                                onmouseout='<%#String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Borrar.png"))%>'
                                OnClick="ibBorrar_Click" />
                        </article>
                    </td>
                    <td>
                        <article style="display: <%#IIf((CType(Me.GestionCompra.Estado, String) <> Utilerias.OrdenesDeTrabajo.EstadoGestionCompra.CREADA) And (CType(Me.GestionCompra.Estado, String) <> Utilerias.OrdenesDeTrabajo.EstadoGestionCompra.SOLICITUD_DE_COTIZACONES), "block", "none")%>">
                            <asp:ImageButton runat="server" ID="ibEnviar" AlternateText="Enviar Cotización" ToolTip="Enviar Cotización"
                                CommandArgument='<%# String.Format("{0}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_PROVEEDOR_COTIZACIONLST.IDENTIFICACION))%>'
                                ImageUrl='<%#AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Flecha_Derecha.png")%>'
                                onmouseover='<%#String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Flecha_Derecha.png"))%>'
                                onmouseout='<%#String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Flecha_Derecha.png"))%>'
                                OnClick="ibEnviar_Click" />
                        </article>
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
    </article>

    <article data-grupo="Listado" class="areaCantidadDeRegistro">
        <asp:Label runat="server" ID="lblCantidadDeRegistros" Text="" Visible="true"></asp:Label>
    </article>

    <article class="areaBotones">
        <asp:Button runat="server" ID="btnCancelar" Text="Cancelar" />
        <asp:Button runat="server" ID="btnCotizar" Text="Cotizar" />
    </article>

    <article id="arAlerta"></article>
    <article id="arpopupConfirmaEnviar"></article>
    <article id="arPopupGenerico"></article>
    <article id="arAlertasDelFormulario"></article>

    <script type="text/javascript">

        function regresarAlListado() {
            window.location = 'Lst_OT_UnidadEspecializadaCompraCotizacion.aspx';
        };

        function mostrarAlertaActualizacionExitosa() {
            deshabilitarControl('#<%=btnCancelar.ClientID%>');
            deshabilitarControl('#<%=btnCotizar.ClientID%>');
            $('.formulario').attr('disabled', 'disabled');
            $('.listado').attr('disabled', 'disabled');

            mostrarAlerta(
            '#arAlertasDelFormulario',
            {
                mensaje: 'Se ha actualizado la información exitosamente.',
                tipo: "exito",
                transparencia: 0.9,
                posicion: 'center',
                onClosed: function () { regresarAlListado(); }
            });
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

            cargarLupa();

        });

        function cargarLupa() {
            permutarImagenes('#imgBuscarProveedores',
                '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Lupa.png")%>',
                '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Lupa.png")%>',
                '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Lupa.png")%>'
            );
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

    </script>

</asp:Content>

