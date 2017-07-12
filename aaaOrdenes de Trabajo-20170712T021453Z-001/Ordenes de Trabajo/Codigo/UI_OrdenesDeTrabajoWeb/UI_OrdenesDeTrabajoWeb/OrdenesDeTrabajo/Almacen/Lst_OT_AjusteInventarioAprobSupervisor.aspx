<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/Mp_Listado.master" AutoEventWireup="false" CodeFile="Lst_OT_AjusteInventarioAprobSupervisor.aspx.vb" Inherits="OrdenesDeTrabajo_Almacen_Lst_OP_AjusteInventarioAprobSupervisor" %>

<%@ Register TagPrefix="wuc" Namespace="Utilerias.WebForms.Controles" Assembly="Utilerias.WebForms.Controles" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphFormulario" Runat="Server">
    <header>
        <h2>Aprobación de ajustes de inventario por parte del supervisor</h2>
    </header>

    <article data-grupo="FiltrosDeBusqueda" class="tituloSeccion">
        Criterios de búsqueda
    </article>

    <article data-grupo="FiltrosDeBusqueda" class="formulario">

        <table>
            <tr>
                <th>Número Ajuste</th>
                <td>
                    <asp:TextBox runat="server" ID="txtFiltroNumeroAjuste" data-tipocontrol="texto" Width="59%" MaxLength="10"></asp:TextBox>
                    <ajax:FilteredTextBoxExtender ID="ftbTxtNumeroGestion" runat="server" TargetControlID="txtFiltroNumeroAjuste" FilterType="Numbers, Custom" FilterMode="ValidChars" ValidChars="-"></ajax:FilteredTextBoxExtender>
                </td>
            </tr>
            <tr>
                <th>Tipo</th>
                <td>
                    <asp:DropDownList Width="40%" runat="server" ID="ddlFiltroTipo" AppendDataBoundItems="true" data-tipocontrol="combo"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <th>Fecha de creación</th>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:TextBox runat="server" ID="txtFiltroFechaInicio" data-tipocontrol="texto" placeholder="Fecha Inicio"></asp:TextBox>&nbsp; Hasta&nbsp;
                          <asp:TextBox runat="server" ID="txtFiltroFechaFin" data-tipocontrol="texto" placeholder="Fecha Final"></asp:TextBox>
                            <asp:CustomValidator ID="cvstxtFiltroFechaInicio" ControlToValidate="txtFiltroFechaInicio" ValidateEmptyText="true" runat="server" ErrorMessage="Debe indicar el rango de fechas si desea realizar la consulta" ValidationGroup="Grupo7" ClientValidationFunction="ValidadorDeCampoRequeridoPersonalizadoFecha">&nbsp;*</asp:CustomValidator>
                            <asp:CustomValidator ID="cvstxtFiltroFechaFin" ControlToValidate="txtFiltroFechaFin" ValidateEmptyText="true" runat="server" ErrorMessage="Debe indicar el rango de fechas si desea realizar la consulta" ValidationGroup="Grupo7" ClientValidationFunction="ValidadorDeCampoRequeridoPersonalizadoFecha">&nbsp;*</asp:CustomValidator>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="txtFiltroFechaInicio" />
                            <asp:AsyncPostBackTrigger ControlID="txtFiltroFechaFin" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <%--<tr>
                <th>Estado</th>
                <td>
                    <asp:DropDownList Width="40%" runat="server" ID="ddlFiltroEstado" AppendDataBoundItems="true" data-tipocontrol="combo"></asp:DropDownList>
                </td>
            </tr>--%>
        </table>
    </article>

    <article data-grupo="FiltrosDeBusqueda" class="areaBotones">
        <asp:Button runat="server" ID="btnBuscar" Text="Buscar" />
        <input type="button" data-tipo="limpiarFiltros" value="Limpiar Filtros" />
        <input id="btnCancelarBusqueda" type="button" data-tipo="cancelarBusqueda" value="Cancelar" />
    </article>

    <article data-grupo="Listado" class="tituloSeccion">
        Aprobación de ajustes de inventario por parte del supervisor
    </article>

    <article data-grupo="Listado" class="listado">

        <article class="areaBotonesListado">
            <img id="imgMostrarFiltros" alt="Mostrar criterios de Búsqueda" title="Mostrar criterios de Búsqueda" data-tipo="mostrarFiltros" src="" />
        </article>

        <asp:Repeater runat="server" ID="rpRevision">
            <HeaderTemplate>
                <table>
                    <tr>
                        <th>
                            <asp:LinkButton runat="server" ID="lnkNumeroAjuste" Text="No. de Ajuste" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_AJUSTE_INVENTARIOLST.NUMERO_AJUSTE%>" CommandArgument="ASC" OnCommand="lnkRpRevision_Command"></asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton runat="server" ID="lnkObservaciones" Text="Observaciones" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_AJUSTE_INVENTARIOLST.OBSERVACIONES%>" CommandArgument="ASC" OnCommand="lnkRpRevision_Command"></asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton runat="server" ID="lnkFecha" Text="Fecha" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_AJUSTE_INVENTARIOLST.FECHA_REGISTRO_SOLICITUD%>" CommandArgument="ASC" OnCommand="lnkRpRevision_Command"></asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton runat="server" ID="lnkEstado" Text="Estado" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_AJUSTE_INVENTARIOLST.DESC_ESTADO%>" CommandArgument="ASC" OnCommand="lnkRpRevision_Command"></asp:LinkButton>
                        </th>
                        <th>&nbsp;</th>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr class="lineaDelListado">
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_AJUSTE_INVENTARIOLST.NUMERO_AJUSTE)%></td>
                    <td>
                        <article style="display: <%#IIf(Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_AJUSTE_INVENTARIOLST.OBSERVACIONES).ToString.StartsWith("-"), "none", "block")%>">
                            <asp:ImageButton runat="server" ID="ImageButton1" data-tipo="tooltip"
                                CssClass="centradoEnRow"
                                title='<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_AJUSTE_INVENTARIOLST.OBSERVACIONES)%>'
                                ImageUrl='<%#AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Informacion.png")%>' />
                        </article>
                    </td>
                    <td><%#CType(Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_AJUSTE_INVENTARIOLST.FECHA_REGISTRO_SOLICITUD), DateTime).ToString(Utilerias.OrdenesDeTrabajo.Constantes.FORMATO_FECHA_UI)%></td>
                    <td>
                        <asp:LinkButton runat="server" ID="lnkEstado" Text="<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_AJUSTE_INVENTARIOLST.DESC_ESTADO)%>" CommandArgument='<%#String.Format("{0}%{1}%{2}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_AJUSTE_INVENTARIOLST.ID_UBICACION), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_AJUSTE_INVENTARIOLST.ANNO), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_AJUSTE_INVENTARIOLST.CONSECUTIVO_AJUSTE))%>' OnCommand="lnkTrazabilidadGestion"></asp:LinkButton>
                    <td>
                        <asp:ImageButton runat="server"  class="tooltip"  ToolTip="Revisar"  ID="ibRevisar"  AlternateText="Revisar" OnClick="ibContenidoTematico_Click" CommandArgument='<%#String.Format("{0}%{1}%{2}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_AJUSTE_INVENTARIOLST.ID_UBICACION), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_AJUSTE_INVENTARIOLST.ANNO), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_AJUSTE_INVENTARIOLST.CONSECUTIVO_AJUSTE))%>'  src='<%#AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Check.png")%>' onmouseover='<%# String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Icono_Check.png"))%>' onmouseout='<%# String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Check.png"))%>'  />
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
    </article>

    <article data-grupo="Listado" class="areaPaginadorListado">
        <wuc:PaginadorNumerico runat="server" ID="pnRpRevision" />
    </article>

    <article data-grupo="Listado" class="areaCantidadDeRegistro">
        <asp:Label runat="server" ID="lblCantidadDeRegistros" Text="" Visible="true"></asp:Label>
    </article>

    <article id="arAlerta"></article>
    <article id="arpopupConfirmaDeseaBorrar"></article>
    <article id="arPopupGenerico"></article>

    <script type="text/javascript">

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

            configurarDatePickerRango("#<%=Me.txtFiltroFechaInicio.ClientID%>", "#<%=Me.txtFiltroFechaFin.ClientID%>");

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

