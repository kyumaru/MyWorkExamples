<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/Mp_Listado.master" AutoEventWireup="false" CodeFile="Lst_OT_GestionOrdenTrabajoSupervisor.aspx.vb" Inherits="OrdenesDeTrabajo_OrdenesDeTrabajo_Lst_OT_GestionOrdenTrabajoSupervisor" %>

<%@ Register TagPrefix="wuc" Namespace="Utilerias.WebForms.Controles" Assembly="Utilerias.WebForms.Controles" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphFormulario" Runat="Server">

    <header>
        <h2>Gestión de Ordenes de Trabajo por Coordinador de Sector o Taller</h2>
    </header>

    <article data-grupo="FiltrosDeBusqueda" class="tituloSeccion">
        Criterios de búsqueda
    </article>

    <article data-grupo="FiltrosDeBusqueda" class="formulario">
        <table>
            <tr>
                <th>Número de Orden Pdago</th>
                <td>
                    <asp:TextBox Width="59%" runat="server" ID="txtNumOrden" data-tipocontrol="texto" TextMode="MultiLine" Rows="1"></asp:TextBox>
                    <ajax:FilteredTextBoxExtender ID="ftbTxtNumOrden" runat="server" TargetControlID="txtNumOrden" FilterMode="ValidChars" FilterType="Numbers,Custom" ValidChars="-"></ajax:FilteredTextBoxExtender>
                </td>
            </tr>
            <tr>
                <th>Estado</th>
                <td>
                    <asp:DropDownList Width="59%" runat="server" ID="ddlEstado" data-tipocontrol="combo" AppendDataBoundItems="true"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <th>Edificio</th>
                <td>
                    <asp:DropDownList Width="59%" runat="server" ID="ddlEdificio" data-tipocontrol="combo" AppendDataBoundItems="true"></asp:DropDownList>
                </td>
            </tr>
        </table>
    </article>

    <article data-grupo="FiltrosDeBusqueda" class="areaBotones">
        <asp:Button runat="server" ID="btnBuscar" Text="Buscar" />
        <input type="button" data-tipo="limpiarFiltros" value="Limpiar Filtros" />
        <input id="btnCancelarBusqueda" type="button" data-tipo="cancelarBusqueda" value="Cancelar" />
    </article>

    <article data-grupo="Listado" class="tituloSeccion" style="width: 450px !important;">
        Listado de Ordenes de Trabajo por Coordinador de Sector o Taller
    </article>

    <article data-grupo="Listado" class="formulario">
        <table>
            <tr>
                <th>Sector o Taller</th>
                <td>
                    <asp:DropDownList runat="server" ID="ddlSectorYTaller" data-tipocontrol="combo" AppendDataBoundItems="true" AutoPostBack="true"></asp:DropDownList>
                </td>
            </tr>
        </table>
    </article>

    <article data-grupo="Listado" class="listado sinBorde">
        <article class="areaBotonesListado">
            <img id="imgMostrarFiltros" alt="Mostrar criterios de Búsqueda" title="Mostrar criterios de Búsqueda" data-tipo="mostrarFiltros" src="" />
        </article>

        <asp:Repeater runat="server" ID="rpOrdenTrabajo">
            <HeaderTemplate>
                <table>
                    <tr>
                        <th>
                            <asp:LinkButton runat="server" ID="lnkIdOrdenTrabajo" Text="Número de OT" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OT_REVISION_SUPERVISOR.ID_ORDEN_TRABAJO%>" CommandArgument="ASC" OnCommand="lnkRpOrdenTrabajo_Command"></asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton runat="server" ID="lnkNumeroOrden" Text="PDAGO" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OT_REVISION_SUPERVISOR.NUMERO_ORDEN%>" CommandArgument="ASC" OnCommand="lnkRpOrdenTrabajo_Command"></asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton runat="server" ID="lnkTipoOrden" Text="Tipo Orden" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OT_REVISION_SUPERVISOR.DESCRIPCION_TRABAJO%>" CommandArgument="ASC" OnCommand="lnkRpOrdenTrabajo_Command"></asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton runat="server" ID="lnkContacto" Text="Contacto" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OT_REVISION_SUPERVISOR.NOMBRE_PERSONA_CONTACTO%>" CommandArgument="ASC" OnCommand="lnkRpOrdenTrabajo_Command"></asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton runat="server" ID="lnkLugarTrabajo" Text="Edificio" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OT_REVISION_SUPERVISOR.DESC_LUGAR_TRABAJO%>" CommandArgument="ASC" OnCommand="lnkRpOrdenTrabajo_Command"></asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton runat="server" ID="lnkEstado" Text="Estado" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OT_REVISION_SUPERVISOR.DESC_ESTADO_ORDEN%>" CommandArgument="ASC" OnCommand="lnkRpOrdenTrabajo_Command"></asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton runat="server" ID="lnkFechaAsignacion" Text="Fecha Asignación" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OT_REVISION_SUPERVISOR.FECHA_ASIGNACION%>" CommandArgument="ASC" OnCommand="lnkRpOrdenTrabajo_Command"></asp:LinkButton>
                        </th>
                        <th>Desc. Trab</th>
                        <th>Desc. Inconformidad</th>
                        <th>&nbsp;</th>
                        <th>&nbsp;</th>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr class="lineaDelListado" id="trTabla" runat="server">
                    <td>
                        <asp:LinkButton runat="server" ID="lnkNumOt" Text="<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_REVISION_SUPERVISOR.ID_ORDEN_TRABAJO)%>"
                            CommandArgument='<%# String.Format("{0}¬{1}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_REVISION_SUPERVISOR.ID_UBICACION), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_REVISION_SUPERVISOR.ID_ORDEN_TRABAJO))%>'
                            OnClick="lnkNumOt_Click"></asp:LinkButton>
                    </td>

                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_REVISION_SUPERVISOR.NUMERO_ORDEN)%></td>
                    <td style="text-align: center">
                        <asp:Image runat="server" ID="Image2" data-tipo="tooltip"
                            title="<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_REVISION_SUPERVISOR.DESC_TIPO_ORDEN)%>"
                            ImageUrl='<%#AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Informacion.png")%>' />
                    </td>
                    <td style="text-align: center">
                        <asp:Image runat="server" ID="Image1" data-tipo="tooltip"
                            title="<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_REVISION_SUPERVISOR.NOMBRE_PERSONA_CONTACTO)%>"
                            ImageUrl='<%#AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Informacion.png")%>' />
                    </td>
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_REVISION_SUPERVISOR.DESC_LUGAR_TRABAJO)%></td>
                    <td>
                        <asp:LinkButton runat="server" ID="lnkTrazabilidad" Style="text-decoration: underline; color: blue;"
                            Text="<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_REVISION_SUPERVISOR.DESC_ESTADO_ORDEN)%>"
                            CommandArgument='<%# String.Format("{0}¬{1}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_REVISION_SUPERVISOR.ID_UBICACION), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_REVISION_SUPERVISOR.ID_ORDEN_TRABAJO))%>'
                            OnCommand="lnkTrazabilidad_Command"></asp:LinkButton>
                    </td>
                    <td><%#CType(Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_REVISION_SUPERVISOR.FECHA_ASIGNACION), DateTime).ToString(Utilerias.OrdenesDeTrabajo.Constantes.FORMATO_FECHA_UI)%></td>
                    <td>
                        <asp:Image runat="server" ID="imgTooltipDesc" data-tipo="tooltip"
                            title="<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_REVISION_SUPERVISOR.DESCRIPCION_TRABAJO)%>"
                            ImageUrl='<%#AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Informacion.png")%>' />
                    </td>
                    <td>
                        <article style="display: <%#IIf((CType(Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_REVISION_SUPERVISOR.ESTADO_ORDEN_TRABAJO), String) = Utilerias.OrdenesDeTrabajo.EstadoOrden.NO_CONFORME), "block", "none")%>">
                            <asp:Image runat="server" ID="imgTooltipMotivo" data-tipo="tooltip"
                                title="<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_REVISION_SUPERVISOR.MOTIVO_NO_CONFORME)%>"
                                ImageUrl='<%#AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Advertencia.png")%>'
                                CommandArgument='<%# String.Format("{0}¬{1}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_REVISION_SUPERVISOR.ID_UBICACION), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_REVISION_SUPERVISOR.ID_ORDEN_TRABAJO))%>'/>
                        </article>
                    </td>
                    <td>
                        <asp:HiddenField runat="server" ID="hdFechaAsignacion" Value="<%#CType(Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_REVISION_SUPERVISOR.FECHA_ASIGNACION), DateTime).ToString(Utilerias.OrdenesDeTrabajo.Constantes.FORMATO_FECHA_UI)%>" />
                    </td>
                    <td>
                        <asp:HiddenField runat="server" ID="hdfTipoOrden" Value="<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_REVISION_SUPERVISOR.TIPO_ORDEN_TRABAJO)%>" />
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>

    </article>

    <article data-grupo="Listado" class="areaPaginadorListado">
        <wuc:PaginadorNumerico runat="server" ID="pnRpOrdenTrabajo" />
    </article>

    <article data-grupo="Listado" class="areaCantidadDeRegistro">
        <asp:Label runat="server" ID="lblCantidadDeRegistros" Text="" Visible="true"></asp:Label>
    </article>

    <article id="arAlerta"></article>
    <article id="arpopupConfirmaEnviar"></article>
    <article id="arPopupGenerico"></article>

    <script type="text/javascript">
           
        function mostrarAlertaAdvertencia(pvc_Mensaje) {
            mostrarAlerta(
                '#arAlerta',
                {
                    mensaje: pvc_Mensaje,
                    tipo: 'advertencia',
                    transparencia: 0.9,
                    posicion: 'center'
                }
            );
            ocultarAreaFiltrosDeBusqueda();
        };

        function mostrarAlertaAdvertenciaFiltro(pvc_Mensaje) {
            mostrarAlerta(
                '#arAlerta',
                {
                    mensaje: pvc_Mensaje,
                    tipo: 'advertencia',
                    transparencia: 0.9,
                    posicion: 'center'
                }
            );
            ocultarAreaDeListado();
            mostrarAreaFiltrosDeBusqueda();
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
            ocultarAreaFiltrosDeBusqueda();
        };

        function mostrarAlertaNoHayDatos() {
            mostrarAlerta(
                '#arAlerta',
                {
                    mensaje: 'No se cuenta con información para mostrar.',
                    tipo: "advertencia",
                    transparencia: 0.9,
                    posicion: 'center'
                }
            );

            deshabilitarControl('#btnCancelarBusqueda');

            ocultarAreaDeListado();
            mostrarAreaFiltrosDeBusqueda();
        };

        function mostrarAlertaActualizacionExitosa() {

            mostrarAlerta(
                '#arAlerta',
                {
                    mensaje: 'Se ha actualizado la información de la orden.',
                    tipo: "exito",
                    transparencia: 0.9,
                    posicion: 'center'
                });
            ocultarAreaFiltrosDeBusqueda();
        };


        $(document).ready(function () {
            aplicarConfiguracionBasicaListado({ alternarVisibilidadFiltroLista: true });

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

