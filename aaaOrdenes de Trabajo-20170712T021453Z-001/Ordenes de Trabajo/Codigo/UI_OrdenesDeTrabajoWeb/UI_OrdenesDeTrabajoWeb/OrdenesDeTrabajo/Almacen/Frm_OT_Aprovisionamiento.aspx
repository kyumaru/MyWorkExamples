<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/Mp_Listado.master" AutoEventWireup="false" CodeFile="Frm_OT_Aprovisionamiento.aspx.vb" Inherits="OrdenesDeTrabajo_Almacen_Frm_OT_Aprovisionamiento" %>

<%@ Register TagPrefix="wuc" Namespace="Utilerias.WebForms.Controles" Assembly="Utilerias.WebForms.Controles" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphFormulario" runat="Server">
    <header>
        <h2>Aprovisionamiento</h2>
    </header>

    <article data-grupo="Formulario" class="tituloSeccion">
        Filtros de búsqueda
    </article>

    <article data-grupo="Formulario" class="formulario ">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <table>
                    <tr>
                        <th>Filtrar por</th>
                        <td>
                            <asp:DropDownList Width="40%" runat="server" ID="ddlFiltrarPor" AutoPostBack="true" AppendDataBoundItems="true" data-tipocontrol="combo"></asp:DropDownList>
                        </td>
                    </tr>

                    <tr id="trCategoria" runat="server">
                        <th>Categoría</th>
                        <td>
                            <asp:DropDownList runat="server" ID="ddlFiltroCategoria" AutoPostBack="true" data-tipocontrol="combo" AppendDataBoundItems="true" Width="40%"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr id="trSubCategoria" runat="server">
                        <th>Sub Categoría</th>
                        <td>
                            <asp:DropDownList runat="server" ID="ddlFiltroSubCategoria" AutoPostBack="true" data-tipocontrol="combo" AppendDataBoundItems="true" Width="40%"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr id="trPartida" runat="server">
                        <th>Partida</th>
                        <td>
                            <asp:DropDownList Width="40%" runat="server" ID="ddlFiltroPartida" AutoPostBack="true" AppendDataBoundItems="true" data-tipocontrol="combo"></asp:DropDownList>
                        </td>
                    </tr>

                </table>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ddlFiltrarPor" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="ddlFiltroCategoria" EventName="SelectedIndexChanged" />
            </Triggers>
        </asp:UpdatePanel>
    </article>

    <article data-grupo="Listado" class="tituloSeccion">
        Aprovisionamiento
    </article>

    <article data-grupo="Listado" class="listado">
        <asp:UpdatePanel runat="server" UpdateMode="Conditional" ID="upRpAdjunto">
            <ContentTemplate>
                <article class="formulario sinBorde" style="float: left !important;">
                    <asp:CheckBox runat="server" ID="chkSeleccionarTodos" AutoPostBack="true" />&nbsp;&nbsp;
            Seleccionar Todos
                </article>
                <br />
                <br />

                <asp:Repeater runat="server" ID="rpDetalles">
                    <HeaderTemplate>
                        <table>
                            <tr>
                                <th>&nbsp;</th>
                                <th>Partida</th>
                                <th>Código</th>
                                <th>Descripción</th>
                                <th>Existencia</th>
                                <th>Disponible</th>
                                <th>Máximo Almacén</th>
                                <th>Punto de Reorden</th>
                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr class="lineaDelListado">
                            <td>
                                <asp:CheckBox runat="server" ID="chkDetalle"
                                    Checked='<%#IIf(CType(Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_MATERIALLST.SELECCIONADO), String) = "0", False, True)%>'
                                    AutoPostBack="true" OnCheckedChanged="chkDetalle_CheckedChanged"
                                    CommandArgument='<%# String.Format("{0}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_MATERIALLST.ID_MATERIAL))%>' />
                                <asp:HiddenField runat="server" ID="hdfIdDetalleMaterial" Value="<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_MATERIALLST.ID_MATERIAL)%>" />
                            </td>
                            <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_MATERIALLST.PARTIDA_PRESUPUESTARIA)%></td>
                            <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_MATERIALLST.ID_MATERIAL)%></td>
                            <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_MATERIALLST.DESCRIPCION)%></td>
                            <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_MATERIALLST.CANTIDAD_EXISTENCIA)%></td>
                            <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_MATERIALLST.CANTIDAD_DISPONIBLE)%></td>
                            <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_MATERIALLST.MAXIMO_ALMACEN)%></td>
                            <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_MATERIALLST.PUNTO_REORDEN)%></td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="chkSeleccionarTodos" EventName="CheckedChanged" />
                <asp:AsyncPostBackTrigger ControlID="ddlFiltrarPor" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="ddlFiltroCategoria" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="ddlFiltroSubCategoria" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="ddlFiltroPartida" EventName="SelectedIndexChanged" />
            </Triggers>
        </asp:UpdatePanel>

        <article class="areaBotones">
            <asp:Button runat="server" ID="btnSiguiente" Text="Siguiente" />
            <asp:Button runat="server" ID="btnCancelar" Text="Regresar" />
        </article>

    </article>

    <article id="arAlerta"></article>
    <article id="arpopupConfirmaDeseaBorrar"></article>
    <article id="arPopupGenerico"></article>
    <article id="arAlertasDelFormulario"></article>

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

            ocultarAreaDeListado();
        };

        $(document).ready(function () {
            aplicarConfiguracionBasicaListado({ alternarVisibilidadFiltroLista: true });

            $('#btnCancelar').click(function () {
                regresarAlListado();
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

    </script>
</asp:Content>

