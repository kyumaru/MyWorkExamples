<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/Mp_Formulario.master" AutoEventWireup="false" CodeFile="Frm_OT_FondoTrabajoGestionCheque.aspx.vb" Inherits="OrdenesDeTrabajo_GestionesDeCompra_Frm_OT_FondoTrabajoGestionCheque" %>

<%@ Register TagPrefix="wuc" Namespace="Utilerias.WebForms.Controles" Assembly="Utilerias.WebForms.Controles" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphFormulario" Runat="Server">
    <header>
        <h2>Gestión de Compras por Fondo de Trabajo</h2>

    </header>

    <article class="tituloSeccion">
        Cotización Adjudicada
    </article>

    <article data-grupo="Listado" class="listado sinBorde">
        <asp:Repeater runat="server" ID="rpArchivo">
            <HeaderTemplate>
                <table>
                    <tr>
                        <th>
                            <asp:LinkButton runat="server" ID="lnkProveedor" Text="Proveedor" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ADJUNTO_COTIZACIONLST.NOMBRE_PROVEEDOR%>" OnCommand="lnkArchivo_Command"></asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton runat="server" ID="lnkNombre" Text="Archivo" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ADJUNTO_COTIZACIONLST.NOMBRE_ARCHIVO%>" OnCommand="lnkArchivo_Command"></asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton runat="server" ID="lnkDescripcion" Text="Descripción" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ADJUNTO_COTIZACIONLST.DESCRIPCION%>" OnCommand="lnkArchivo_Command"></asp:LinkButton>
                        </th>
                    </tr>
            </HeaderTemplate>

            <ItemTemplate>
                <tr class="lineaDelListado">
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ADJUNTO_COTIZACIONLST.NOMBRE_PROVEEDOR)%></td>
                    <td>
                        <asp:LinkButton runat="server" ID="lnkDescargarArchivo_Command"
                                            CommandArgument='<%#Container.ItemIndex%>'
                                            Style="text-decoration: underline; color: blue;"
                                            OnCommand="lnkDescargarArchivo_Command"
                                            Text="<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ADJUNTO_COTIZACIONLST.NOMBRE_ARCHIVO)%>"></asp:LinkButton>
                        <%--<asp:LinkButton runat="server" ID="lnkDescripcion" Text="<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ADJUNTO_COTIZACIONLST.NOMBRE_ARCHIVO)%>" CommandName='<%# String.Format("{0}%{1}%{2}%{3}%{4}%{5}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ADJUNTO_COTIZACIONLST.IDENTIFICACION), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ADJUNTO_COTIZACIONLST.ID_UBICACION), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ADJUNTO_COTIZACIONLST.ID_VIA_COMPRA_CONTRATO), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ADJUNTO_COTIZACIONLST.ANNO), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ADJUNTO_COTIZACIONLST.NUMERO_GESTION), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ADJUNTO_COTIZACIONLST.NOMBRE_ARCHIVO))%>' OnCommand="lnkDescargarArchivo_Command"></asp:LinkButton>--%>
                    </td>

                    <td>
                        <article style="display: <%#IIf(Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ADJUNTO_COTIZACIONLST.DESCRIPCION).ToString.StartsWith("-"), "none", "block")%>">
                            <asp:ImageButton runat="server" ID="ImageButton1" data-tipo="tooltip"
                                CssClass="centradoEnRow"
                                title='<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ADJUNTO_COTIZACIONLST.DESCRIPCION)%>'
                                ImageUrl='<%#AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Informacion.png")%>' />
                        </article>
                    </td>
                </tr>
            </ItemTemplate>

            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
    </article>

    <article data-grupo="Listado" class="areaPaginadorListado">
        <asp:Label runat="server" ID="lblNoHayDAtosArchivo" Text="No se cuenta con información para mostrar" Visible="false"></asp:Label>
        <wuc:PaginadorNumerico runat="server" ID="pnRpArchivo" />
    </article>

    <article data-grupo="Listado" class="areaCantidadDeRegistro">
        <asp:Label runat="server" ID="lblCantidadDeRegistrosArchivo" Text="" Visible="true"></asp:Label>
    </article>

    <article class="tituloSeccion">
        Registrar Cheque
    </article>
    <article class="formulario ">
                <table>
                    <tr>
                        <th style="width: 14%;">N° de Cheque</th>
                        <td>
                            <asp:TextBox runat="server" ID="txtCheque" Width="40%" data-tipocontrol="texto"></asp:TextBox>
                            <br />
                            <span id="spContadorTxtCheque" class="contadorCaracteresRestantes"></span>
                            <asp:RequiredFieldValidator runat="server" ID="rfvTxtCheque" ControlToValidate="txtCheque" Display="Dynamic" ValidationGroup="Grupo1" ErrorMessage="Debe indicar el número de cheque.">&nbsp;</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                </table>
        <br />
    </article>

    <article class="areaBotones">
        <asp:Button ID="btnTramitar" runat="server" Text="Guardar y Finalizar" ToolTip="Guardar y Finalizar" ValidationGroup="Grupo1"></asp:Button>
        <%--<input id="btnLimpiarFormulario" type="button" data-tipo="limpiarFormulario" value="Limpiar formulario" />--%>
        <input id="btnCancelar" type="button" title="Regresar" value="Regresar" />
    </article>

    <article id="arAlertasDelFormulario"></article>

    <script type="text/javascript">

        $(document).ready(function () {
            $('#btnCancelar').click(function () {
                regresarAlListado();
            });

            configurarLongitudMaximaTexto('#<%=Me.txtCheque.ClientID%>',<%=Utilerias.OrdenesDeTrabajo.Modelo.OTT_GESTION_COMPRA.NUMERO_CHEQUE_BD_TAMANO%>);
            configurarContadorCaracteresRestantes('#<%=Me.txtCheque.ClientID%>','#spContadorTxtCheque');

        })


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
        }


        function mostrarAlertaRegistroExitoso() {
            deshabilitarControl('#<%=btnTramitar.ClientID%>');
            deshabilitarControl('#btnLimpiarFormulario');
            deshabilitarControl('#btnCancelar');
            $('.formulario').attr('disabled', 'disabled');

            mostrarAlerta(
                '#arAlertasDelFormulario',
                {
                    mensaje: 'Se ha tramitado la gestión de compra.',
                    tipo: "exito",
                    transparencia: 0.9,
                    posicion: 'top',
                    onClosed: function () { regresarAlListado(); }
                });
        }

        function regresarAlListado() {
            window.location = 'Lst_OT_FondoTrabajoAprobPresupuesto.aspx';
        }

    </script>
</asp:Content>

