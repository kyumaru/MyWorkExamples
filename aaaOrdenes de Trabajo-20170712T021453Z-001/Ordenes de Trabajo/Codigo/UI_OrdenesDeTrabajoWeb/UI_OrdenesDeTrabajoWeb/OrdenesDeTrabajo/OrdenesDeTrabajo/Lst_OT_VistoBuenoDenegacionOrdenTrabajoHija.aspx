<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/Mp_Listado.master" AutoEventWireup="false" CodeFile="Lst_OT_VistoBuenoDenegacionOrdenTrabajoHija.aspx.vb" Inherits="OrdenesDeTrabajo_Lst_OT_VistoBuenoDenegacionOrdenTrabajoHija" %>

<%@ Register TagPrefix="wuc" Namespace="Utilerias.WebForms.Controles" Assembly="Utilerias.WebForms.Controles" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphFormulario" Runat="Server">

     <header>
        <h2>Visto bueno o denegación a creación de orden de trabajo hija</h2>
    </header>

    <article data-grupo="FiltrosDeBusqueda" class="tituloSeccion">
        Criterios de búsqueda
    </article>

    <article data-grupo="FiltrosDeBusqueda" class="formulario">
        <table>
            <tr>
                <th>Número de órden de trabajo madre</th>
                <td>
                    <asp:TextBox Width="40%" runat="server" ID="txtFiltroConsecutivoMadre" data-tipocontrol="texto"></asp:TextBox>
                    <br />
                </td>
            </tr>
            <tr>
                <th>Número de órden de trabajo madre en PDAGO</th>
                <td>
                    <asp:TextBox Width="40%" runat="server" ID="txtFiltroPDAGOMadre" AppendDataBoundItems="true" data-tipocontrol="texto"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>Número de órden de trabajo hija</th>
                <td>
                    <asp:TextBox Width="40%" runat="server" ID="txtFiltroConsecutivoHija" data-tipocontrol="texto"></asp:TextBox>
                    <br />
                </td>
            </tr>
            <tr>
                <th>Número de órden de trabajo madre en PDAGO</th>
                <td>
                    <asp:TextBox Width="40%" runat="server" ID="txtFiltroPDAGOHija" AppendDataBoundItems="true" data-tipocontrol="texto"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>Categoría</th>
                <td>
                    <asp:DropDownList Width="40%" runat="server" ID="ddlCategoria" data-tipocontrol="combo" AppendDataBoundItems="true" AutoPostBack="true"></asp:DropDownList>
                    <asp:RequiredFieldValidator runat="server" ID="rfvDdlCategoria" ControlToValidate="ddlCategoria" Display="Dynamic" ErrorMessage="Categoría es obligatoria." ValidationGroup="Aceptar"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <th>Actividad</th>
                <td>
                    <asp:UpdatePanel runat="server" ID="upActividad" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:DropDownList Width="40%" runat="server" ID="ddlActividad" data-tipocontrol="combo" AppendDataBoundItems="true" AutoPostBack="true"></asp:DropDownList>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlCategoria" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
    </article>

    <article data-grupo="FiltrosDeBusqueda" class="areaBotones">
        <asp:Button runat="server" ID="btnBuscar" Text="Buscar" />
        <input type="button" data-tipo="limpiarFiltros" value="Limpiar Filtros" />
        <input id="btnCancelarBusqueda" type="button" data-tipo="cancelarBusqueda" value="Cancelar" />
    </article>


    <article data-grupo="Listado" class="tituloSeccion">
        Listado para Visto bueno o denegación a creación de orden de trabajo hija
    </article>

    <article data-grupo="Listado" class="listado">
        <article class="areaBotonesListado">
            <img id="imgMostrarFiltros" alt="Mostrar criterios de Búsqueda" title="Mostrar criterios de Búsqueda" data-tipo="mostrarFiltros" src="" />
        </article>

        <asp:Repeater runat="server" ID="rpOrdenTrabajo">
            <HeaderTemplate>
                <table>
                    <tr>
                        <th>
                            <asp:LinkButton runat="server" ID="lnkIdOrdenTrabajoMadre" Text="No de OT Madre" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ORDEN_TRABAJOLST.ID_ORDEN_TRABAJO_MADRE%>" CommandArgument="ASC" OnCommand="lnkRpOrdenTrabajo_Command"></asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton runat="server" ID="lnkCategoriaMadre" Text="Categoría Servicio Madre" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ORDEN_TRABAJOLST.DESC_CATEGORIA_SERVICIO_MADRE%>" CommandArgument="ASC" OnCommand="lnkRpOrdenTrabajo_Command"></asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton runat="server" ID="lnkActividadMadre" Text="Actividad Madre" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ORDEN_TRABAJOLST.DESC_ACTIVIDAD_MADRE%>" CommandArgument="ASC" OnCommand="lnkRpOrdenTrabajo_Command"></asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton runat="server" ID="lnkIdOrdenHija" Text="No OT" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ORDEN_TRABAJOLST.ID_ORDEN_TRABAJO%>" CommandArgument="ASC" OnCommand="lnkRpOrdenTrabajo_Command"></asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton runat="server" ID="lnkCategoriaHija" Text="Categoría Servicio" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ORDEN_TRABAJOLST.DESC_CATEGORIA_SERVICIO%>" CommandArgument="ASC" OnCommand="lnkRpOrdenTrabajo_Command"></asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton runat="server" ID="lnkActividadHija" Text="Actividad" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ORDEN_TRABAJOLST.DESC_ACTIVIDAD%>" CommandArgument="ASC" OnCommand="lnkRpOrdenTrabajo_Command"></asp:LinkButton>
                        </th>
                        <th>Desc. Trab</th>
                        <th>&nbsp;</th>
                        <th>&nbsp;</th>
                    </tr>
            </HeaderTemplate>

            <ItemTemplate>
                <tr class="lineaDelListado">
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ORDEN_TRABAJOLST.ID_ORDEN_TRABAJO_MADRE)%></td>
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ORDEN_TRABAJOLST.DESC_CATEGORIA_SERVICIO_MADRE)%></td>
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ORDEN_TRABAJOLST.DESC_ACTIVIDAD_MADRE)%></td>
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ORDEN_TRABAJOLST.ID_ORDEN_TRABAJO)%></td>
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ORDEN_TRABAJOLST.DESC_CATEGORIA_SERVICIO)%></td>
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ORDEN_TRABAJOLST.DESC_ACTIVIDAD)%></td>
                    <td>
                        <asp:ImageButton runat="server" ID="imgTooltipEspecificoPorControl" data-tipo="tooltip"
                            title="<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ORDEN_TRABAJOLST.DESCRIPCION_TRABAJO)%>"
                            ImageUrl='<%#AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x24, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Informacion.png")%>' />
                    </td>
                    <td>
                        <a href="Frm_OT_VistoBuenoDenegacionOrdenTrabajoHija.aspx?pvn_Operacion=<%#Utilerias.OrdenesDeTrabajo.Constantes.VISTO_BUENO%>&pvn_IdUbicacion=<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ORDEN_TRABAJOLST.ID_UBICACION)%>&pvc_IdOrdenTrabajo=<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ORDEN_TRABAJOLST.ID_ORDEN_TRABAJO)%>&pvn_Anno=<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ORDEN_TRABAJOLST.ANNO)%>">
                            <img title="Visto Bueno" alt="Visto Bueno"
                                src='<%#AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Check.png")%>'
                                onmouseover='<%#String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Icono_Check.png"))%>'
                                onmouseout='<%#String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Check.png"))%>' />
                        </a>
                    </td>
                    <td>
                        <a href="Frm_OT_VistoBuenoDenegacionOrdenTrabajoHija.aspx?pvn_Operacion=<%#Utilerias.OrdenesDeTrabajo.Constantes.DENEGAR%>&pvn_IdUbicacion=<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ORDEN_TRABAJOLST.ID_UBICACION)%>&pvc_IdOrdenTrabajo=<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ORDEN_TRABAJOLST.ID_ORDEN_TRABAJO)%>&pvn_Anno=<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ORDEN_TRABAJOLST.ANNO)%>">
                            <img title="Denegación" alt="Denegación"
                                src='<%#AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Equis.png")%>'
                                onmouseover='<%#String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Icono_Equis.png"))%>'
                                onmouseout='<%#String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Equis.png"))%>' />
                        </a>
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
    <article id="arPopupGenerico"></article>


    <script type="text/javascript">

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

        $(document).ready(function () {
            aplicarConfiguracionBasicaListado({ alternarVisibilidadFiltroLista: true });

            $('[data-tipo="tooltip"]').each(function () {
                habilitarTooltipPorControl('#' + this.id);
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

