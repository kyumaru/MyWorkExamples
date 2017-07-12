<%@ Page Title="Consulta de usuario de la trazabilidad de la OT" Language="VB" MasterPageFile="~/MasterPage/Mp_Formulario.master" AutoEventWireup="false" CodeFile="Frm_OT_ConsultaTrazabilidad.aspx.vb" Inherits="OrdenesDeTrabajo_Frm_OT_ConsultaTrazabilidad" %>

<%@ Register TagPrefix="wuc" Namespace="Utilerias.WebForms.Controles" Assembly="Utilerias.WebForms.Controles" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphFormulario" runat="Server">
    <header>
        <h2>Seguimiento de Orden de Trabajo</h2>
    </header>
    <article class="tituloSeccion">
        Datos de la Orden de Trabajo
    </article>
    <article class="formulario">
        <table>
            <tr>
                <th>N° Orden Trabajo</th>
                <td>
                    <asp:Label runat="server" ID="lblNumeroOrden"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>N° de Orden de Trabajo en PDAGO</th>
                <td>
                    <asp:Label runat="server" ID="lblPdago"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>Estado</th>
                <td>
                    <asp:Label runat="server" ID="lblEstado"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>Edificio</th>
                <td>
                    <asp:Label runat="server" ID="lblEdificio"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>Descripción de Trabajo</th>
                <td>
                    <asp:Label runat="server" ID="lblDescripcion" Width="600px"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>Categoría</th>
                <td>
                    <asp:Label runat="server" ID="lblCategoria"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>Actividad</th>
                <td>
                    <asp:Label runat="server" ID="lblActivididad"></asp:Label>
                </td>
            </tr>
            <tr runat="server" id="trEncargado">
                <th>Encargado Trámite</th>
                <td>
                    <asp:Label runat="server" ID="lblEncargado"></asp:Label>
                </td>
            </tr>
        </table>
    </article>

    <article data-grupo="Listado" class="tituloSeccion">
        Listado de Trámites
    </article>

    <article data-grupo="Listado" class="listado">
        <asp:Repeater runat="server" ID="rpAccion">
            <HeaderTemplate>
                <table>
                    <tr>
                        <th>
                            <asp:LinkButton runat="server" ID="lnkTramite" Text="Tramite" CommandName="<%# Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_TRAZABILIDAD_PROCESOLST.DESC_ESTADO_ORDEN%>" CommandArgument="ASC" OnCommand="lnkRpAcciones_Command"></asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton runat="server" ID="lnkFecEjecucion" Text="Fecha Tramite" CommandName="<%# Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_TRAZABILIDAD_PROCESOLST.FECHA_HORA_EJECUCION%>" CommandArgument="ASC" OnCommand="lnkRpAcciones_Command"></asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton runat="server" ID="lnkResponsable" Text="Responsable" CommandName="<%# Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_TRAZABILIDAD_PROCESOLST.RESPONSABLE%>" CommandArgument="ASC" OnCommand="lnkRpAcciones_Command"></asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton runat="server" ID="lnkObservaciones" Text="Observaciones" CommandName="<%# Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_TRAZABILIDAD_PROCESOLST.OBSERVACIONES%>" CommandArgument="ASC" OnCommand="lnkRpAcciones_Command"></asp:LinkButton>
                        </th>
                        <th>
                            <article style="display: <%#IIf((Roles.IsUserInRole(Membership.GetUser.UserName, Utilerias.OrdenesDeTrabajo.RolesSistema.OT_REGISTRO_SOLICITUD) Or
                                                             Roles.IsUserInRole(Membership.GetUser.UserName, Utilerias.OrdenesDeTrabajo.RolesSistema.OT_PROFESIONAL_DISENIO) Or
                                                             Roles.IsUserInRole(Membership.GetUser.UserName, Utilerias.OrdenesDeTrabajo.RolesSistema.OT_SUPERVISOR)), "block", "none")%>">
                                <asp:LinkButton runat="server" ID="lnkObservacionesInternas" Text="Observaciones Internas" CommandName="<%# Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_TRAZABILIDAD_PROCESOLST.OBSERVACIONES_INTERNAS%>" CommandArgument="ASC" OnCommand="lnkRpAcciones_Command"></asp:LinkButton>
                            </article>
                        </th>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr class="lineaDelListado">
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_TRAZABILIDAD_PROCESOLST.DESC_ESTADO_ORDEN)%></td>
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_TRAZABILIDAD_PROCESOLST.FECHA_HORA_EJECUCION)%></td>
                    <td>

                        <asp:Label runat="server" ID="lblResponsable"
                            Text="<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_TRAZABILIDAD_PROCESOLST.RESPONSABLE)%>"
                            Visible="<%#IIf((Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_TRAZABILIDAD_PROCESOLST.ESTADO_ORDEN_TRABAJO).ToString = Utilerias.OrdenesDeTrabajo.EstadoOrden.CONTRATACION_ADJUDICACION.ToString) Or
                                       (Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_TRAZABILIDAD_PROCESOLST.ESTADO_ORDEN_TRABAJO).ToString = Utilerias.OrdenesDeTrabajo.EstadoOrden.CONTRATACION_INICIO.ToString) Or
                                       (Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_TRAZABILIDAD_PROCESOLST.ESTADO_ORDEN_TRABAJO).ToString = Utilerias.OrdenesDeTrabajo.EstadoOrden.CONTRATACION_PUBLICACION_CARTEL.ToString) Or
                                       (Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_TRAZABILIDAD_PROCESOLST.ESTADO_ORDEN_TRABAJO).ToString = Utilerias.OrdenesDeTrabajo.EstadoOrden.CONTRATACION_REVISIÓN_EXPEDIENTE.ToString) Or
                                       (Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_TRAZABILIDAD_PROCESOLST.ESTADO_ORDEN_TRABAJO).ToString = Utilerias.OrdenesDeTrabajo.EstadoOrden.CONTRATACION_RECOMENDACION_TECNICA.ToString) Or
                                       (Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_TRAZABILIDAD_PROCESOLST.ESTADO_ORDEN_TRABAJO).ToString = Utilerias.OrdenesDeTrabajo.EstadoOrden.PENDIENTE_REVISION_CONTRATACIONES.ToString) Or
                                       (Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_TRAZABILIDAD_PROCESOLST.ESTADO_ORDEN_TRABAJO).ToString = Utilerias.OrdenesDeTrabajo.EstadoOrden.CONTRATACION_OFERTAS.ToString), False, True)%>"></asp:Label>

                        <asp:Image runat="server" ID="imgTooltipEspecificoPorControl" data-tipo="tooltipResponsable"
                            CssClass="centradoEnRow"
                            title="<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_TRAZABILIDAD_PROCESOLST.RESPONSABLE)%>"
                            ImageUrl='<%#AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Usuarios.png")%>'
                            Visible="<%#IIf((Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_TRAZABILIDAD_PROCESOLST.ESTADO_ORDEN_TRABAJO).ToString = Utilerias.OrdenesDeTrabajo.EstadoOrden.CONTRATACION_ADJUDICACION.ToString) Or
                                       (Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_TRAZABILIDAD_PROCESOLST.ESTADO_ORDEN_TRABAJO).ToString = Utilerias.OrdenesDeTrabajo.EstadoOrden.CONTRATACION_INICIO.ToString) Or
                                       (Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_TRAZABILIDAD_PROCESOLST.ESTADO_ORDEN_TRABAJO).ToString = Utilerias.OrdenesDeTrabajo.EstadoOrden.CONTRATACION_PUBLICACION_CARTEL.ToString) Or
                                       (Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_TRAZABILIDAD_PROCESOLST.ESTADO_ORDEN_TRABAJO).ToString = Utilerias.OrdenesDeTrabajo.EstadoOrden.CONTRATACION_REVISIÓN_EXPEDIENTE.ToString) Or
                                       (Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_TRAZABILIDAD_PROCESOLST.ESTADO_ORDEN_TRABAJO).ToString = Utilerias.OrdenesDeTrabajo.EstadoOrden.CONTRATACION_RECOMENDACION_TECNICA.ToString) Or
                                       (Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_TRAZABILIDAD_PROCESOLST.ESTADO_ORDEN_TRABAJO).ToString = Utilerias.OrdenesDeTrabajo.EstadoOrden.PENDIENTE_REVISION_CONTRATACIONES.ToString) Or
                                       (Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_TRAZABILIDAD_PROCESOLST.ESTADO_ORDEN_TRABAJO).ToString = Utilerias.OrdenesDeTrabajo.EstadoOrden.CONTRATACION_OFERTAS.ToString), True, False)%>" />
                    </td>
                    <td>
                        <article style="display: <%#IIf(Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_TRAZABILIDAD_PROCESOLST.OBSERVACIONES).ToString.StartsWith("-"), "none", "block")%>">
                            <asp:ImageButton runat="server" ID="ImageButton1" data-tipo="tooltip"
                                CssClass="centradoEnRow"
                                title='<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_TRAZABILIDAD_PROCESOLST.OBSERVACIONES)%>'
                                ImageUrl='<%#AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Informacion.png")%>' />
                        </article>
                    </td>
                    <td>
                        <article style="display: <%#IIf((Roles.IsUserInRole(Membership.GetUser.UserName, Utilerias.OrdenesDeTrabajo.RolesSistema.OT_REGISTRO_SOLICITUD) Or
                                                         Roles.IsUserInRole(Membership.GetUser.UserName, Utilerias.OrdenesDeTrabajo.RolesSistema.OT_PROFESIONAL_DISENIO) Or
                                                             Roles.IsUserInRole(Membership.GetUser.UserName, Utilerias.OrdenesDeTrabajo.RolesSistema.OT_SUPERVISOR)), "block", "none")%>">
                            <article style="display: <%#IIf(Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_TRAZABILIDAD_PROCESOLST.OBSERVACIONES_INTERNAS).ToString.StartsWith("-"), "none", "block")%>">
                                <asp:ImageButton runat="server" ID="ImageButton2" data-tipo="tooltip"
                                    CssClass="centradoEnRow"
                                    title='<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_TRAZABILIDAD_PROCESOLST.OBSERVACIONES_INTERNAS)%>'
                                    ImageUrl='<%#AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Informacion.png")%>' />
                            </article>
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
        <wuc:PaginadorNumerico runat="server" ID="pnRpAcciones" />
        <%--Paginador del repeater RpCategorias--%>
    </article>
    <article data-grupo="Listado" class="areaCantidadDeRegistro">
        <asp:Label runat="server" ID="lblCantidadRegistro" Text=""> </asp:Label>
    </article>

    <article class="areaBotones">
        <input id="btnRegresar" type="button" value="Regresar" />
    </article>

    <article id="arAlerta"></article>
    <article id="popupConfirmacionDeseaBorrar"></article>

    <style>
        .centradoEnRow {
            margin-left: 25% !important;
        }
    </style>

    <script type="text/javascript">
        function mostrarAlertaNoHayDatos() {
            mostrarAlerta(
                '#arAlerta',
                {
                    mensaje: 'No se cuenta con movimientos para la orden indicada',
                    tipo: "advertencia",
                    transparencia: 0.9,
                    posicion: 'center',
                    permiteCerrar: true
                }
                );
        };

        function mostrarAlertaError(pvc_Mensaje) {
            mostrarAlerta(
                '#arAlerta',
                {
                    mensaje: pvc_Mensaje,
                    tipo: "peligro",
                    transparencia: 0.9,
                    posicion: 'center',
                    permiteCerrar: true
                }
                );

        };

        function mostrarAlertaNoEncontrado() {
            mostrarAlerta(
                '#arAlerta',
                {
                    mensaje: "No se han encontrado acciones con el número de orden indicado",
                    tipo: "advertencia",
                    transparencia: 0.9,
                    posicion: 'center',
                    permiteCerrar: true
                }
                );

        };

        function regresarAlListado() {
            window.location = '<%=Me.PantallaRetorno%>';
        };

        function mostrarAlertaLlaveIncorrecta() {
            deshabilitarFormulario();

            mostrarAlerta(
                '#arAlerta',
                {
                    mensaje: 'Revisar!!',
                    tipo: "peligro",
                    transparencia: 0.9,
                    posicion: 'center',
                    permiteCerrar: true,
                    onClosed: function () { regresarAlListado(); }
                }
                );
        };

        $(document).ready(function () {

            $('#btnRegresar').click(function () {
                regresarAlListado();
            });

            $('[data-tipo="tooltip"]').each(function () {
                habilitarTooltipPorControl('#' + this.id);
            });


            $('[data-tipo="tooltipResponsable"]').each(function () {
                $('#' + this.id).tooltipster({
                    interactive: true,
                    interactiveTolerance: 400,
                    theme: 'tooltipster-light'
                });
            });

        });

    </script>
</asp:Content>

