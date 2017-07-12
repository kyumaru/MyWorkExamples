<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/Mp_Listado.master" AutoEventWireup="false" CodeFile="Lst_OT_AtencionIncidentes.aspx.vb" Inherits="OrdenesDeTrabajo_Almacen_Lst_OT_AtencionIncidentes" %>

<%@ Register TagPrefix="wuc" Namespace="Utilerias.WebForms.Controles" Assembly="Utilerias.WebForms.Controles" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphFormulario" runat="Server">

    <header>
        <h2>Incidentes en Almacén</h2>
    </header>

    <article data-grupo="FiltrosDeBusqueda" class="tituloSeccion">
        Criterios de búsqueda
    </article>

    <article data-grupo="FiltrosDeBusqueda" class="formulario">

        <table>
            <tr>
                <th>Detalle</th>
                <td>
                    <asp:TextBox Width="59%" runat="server" ID="txtDetalle" data-tipocontrol="texto" MaxLength="100"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>Tipo</th>
                <td>
                    <asp:DropDownList Width="59%" runat="server" ID="ddlTipo" data-tipocontrol="combo" AppendDataBoundItems="true"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <th>Estado</th>
                <td>
                    <asp:DropDownList Width="59%" runat="server" ID="ddlFiltroEstado" data-tipocontrol="combo" AppendDataBoundItems="true"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <th>Fecha</th>
                <td>
                    <asp:TextBox runat="server" ID="txtFiltroFecha" Width="145px" data-tipocontrol="texto"></asp:TextBox>
                    <asp:CompareValidator runat="server" ID="cmpvtxtFiltroFecha" ControlToValidate="txtFiltroFecha" Display="Dynamic" ErrorMessage="Fecha es inválida" Operator="DataTypeCheck" Type="Date">&nbsp;</asp:CompareValidator>
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
        Incidentes en Almacén
    </article>

    <article data-grupo="Listado" class="listado">

        <article class="areaBotonesListado">
            <img id="imgMostrarFiltros" alt="Mostrar criterios de Búsqueda" title="Mostrar criterios de Búsqueda" data-tipo="mostrarFiltros" src="" />
        </article>

        <asp:Repeater runat="server" ID="rpIncidentes">
            <HeaderTemplate>
                <table>
                    <tr>
                        <th>
                            <asp:LinkButton runat="server" ID="lnkDescTipoIncidente" Text="Tipo Incidente" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OTF_INCIDENTE_ALMACENLST.DESC_TIPO_INCIDENTE%>" CommandArgument="ASC" OnCommand="lnkRpIncidentes_Command"></asp:LinkButton>
                        </th>
                        <th>Detalle
                        </th>
                        <th>
                            <asp:LinkButton runat="server" ID="lnkFecha" Text="Fecha" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OTF_INCIDENTE_ALMACENLST.FECHA_INCLUSION%>" CommandArgument="ASC" OnCommand="lnkRpIncidentes_Command"></asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton runat="server" ID="lnkEstado" Text="Estado" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OTF_INCIDENTE_ALMACENLST.DESC_ESTADO%>" CommandArgument="ASC" OnCommand="lnkRpIncidentes_Command"></asp:LinkButton>
                        </th>
                        <th>&nbsp;</th>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr class="lineaDelListado">
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTF_INCIDENTE_ALMACENLST.DESC_TIPO_INCIDENTE)%></td>
                    <td>
                        <asp:Image runat="server" ID="Image2" data-tipo="tooltip" CssClass="centradoEnRow"
                            title="<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTF_INCIDENTE_ALMACENLST.DETALLE)%>"
                            ImageUrl='<%#AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Informacion.png")%>' />
                    </td>
                    <td><%#CType(Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTF_INCIDENTE_ALMACENLST.FECHA_INCLUSION), DateTime).ToString(Utilerias.OrdenesDeTrabajo.Constantes.FORMATO_FECHA_UI)%></td>
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTF_INCIDENTE_ALMACENLST.DESC_ESTADO)%></td>
                    <td>
                        <asp:ImageButton runat="server" ID="ibAtenderIncidente" AlternateText="Atender Incidente" ToolTip="Atender Incidente"
                            CommandArgument='<%# String.Format("{0}",Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTF_INCIDENTE_ALMACENLST.ID_INCIDENTE_ALMACEN))%>'
                            ImageUrl='<%#AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Check.png")%>'
                            onmouseover='<%#String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Icono_Check.png"))%>'
                            onmouseout='<%#String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Check.png"))%>'
                            Visible='<%#IIf(Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTF_INCIDENTE_ALMACENLST.ESTADO).ToString = Utilerias.OrdenesDeTrabajo.EstadoIncidente.PENDIENTE, True, False)%>'
                            OnClick="ibAtenderIncidente_Click" />
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>

    </article>

    <article data-grupo="Listado" class="areaPaginadorListado">
        <wuc:PaginadorNumerico runat="server" ID="pnRpIncidentes" />
    </article>

    <article data-grupo="Listado" class="areaCantidadDeRegistro">
        <asp:Label runat="server" ID="lblCantidadDeRegistros" Text="" Visible="true"></asp:Label>
    </article>

    <article id="arAlerta"></article>
    <article id="arpopupConfirmaDeseaBorrar"></article>
    <article id="arPopupGenerico"></article>

    <style>
        .centradoEnRow {
            margin-left: 25% !important;
        }
    </style>

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

            configurarDatePicker("#<%=Me.txtFiltroFecha.ClientID%>");

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

