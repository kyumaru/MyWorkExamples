﻿<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/Mp_Listado.master" AutoEventWireup="false" CodeFile="Frm_OT_GestionCompraSuministros.aspx.vb" Inherits="OrdenesDeTrabajo_GestionesDeCompra_Frm_OT_GestionCompraSuministros" %>

<%@ Register TagPrefix="wuc" Namespace="Utilerias.WebForms.Controles" Assembly="Utilerias.WebForms.Controles" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphFormulario" Runat="Server">
    <header>
        <h2>Gestión de Compras por Suministros</h2>
    </header>

    <article data-grupo="FiltrosDeBusqueda" class="tituloSeccion">
        Criterios de búsqueda
    </article>

    <article data-grupo="FiltrosDeBusqueda" class="formulario">
        <table>
            <tr>
                <th>Número de Orden</th>
                <td>
                    <asp:TextBox runat="server" ID="txtNumOT" data-tipocontrol="texto"  Width="145px"  MaxLength="10"></asp:TextBox>
                    <ajax:FilteredTextBoxExtender ID="ftbTxtNumOT" runat="server" TargetControlID="txtNumOT" FilterMode="ValidChars" ValidChars="-" FilterType="Numbers, Custom"></ajax:FilteredTextBoxExtender>
                </td>
            </tr>
            <tr>
                <th>Código de Material</th>
                <td>
                    <asp:TextBox runat="server" ID="txtNumeroMaterial" data-tipocontrol="texto"  Width="145px"  MaxLength="10"></asp:TextBox>
                    <ajax:FilteredTextBoxExtender ID="ftbTxtNumeroMaterial" runat="server" TargetControlID="txtNumeroMaterial" FilterType="Numbers"></ajax:FilteredTextBoxExtender>
                </td>
            </tr>
            <tr>
                <th>Descripción de Material</th>
                <td>
                    <asp:TextBox Width="70%" runat="server" ID="txtDescMaterial" data-tipocontrol="texto" TextMode="MultiLine" Rows="5"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>Fecha desde</th>
                <td>
                    <asp:TextBox runat="server" ID="txtFiltroFechaDesde" Width="145px" data-tipocontrol="texto"></asp:TextBox>
                    <asp:CompareValidator runat="server" ID="cmpvtxtFiltroDesde" ControlToValidate="txtFiltroFechaDesde" Display="Dynamic" ErrorMessage="Fecha desde inválida" Operator="DataTypeCheck" Type="Date">&nbsp;</asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <th>Fecha hasta</th>
                <td>
                    <asp:TextBox runat="server" ID="txtFiltroFechaHasta" Width="145px" data-tipocontrol="texto"></asp:TextBox>
                    <asp:CompareValidator runat="server" ID="cmpvtxtFiltroHasta" ControlToValidate="txtFiltroFechaHasta" Display="Dynamic" ErrorMessage="Fecha hasta inválida" Operator="DataTypeCheck" Type="Date">&nbsp;</asp:CompareValidator>
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
        Gestión de Compras por Suministros
    </article>

    <article data-grupo="Listado"class="formulario ">
        <table>
            <tr>
                <th style="width: 14%;">Observaciones</th>
                <td>
                    <asp:TextBox runat="server" ID="txtObservaciones" Width="100%" TextMode="MultiLine" Rows="6" data-tipocontrol="texto"></asp:TextBox>
                    <br />
                    <span id="spContadorTxtObservaciones" class="contadorCaracteresRestantes"></span>
                    <%--<asp:RequiredFieldValidator runat="server" ID="rfvTxtObservaciones" ControlToValidate="txtObservaciones" Display="Dynamic" ValidationGroup="Grupo1" ErrorMessage="Debe indicar el número de cheque.">&nbsp;</asp:RequiredFieldValidator>--%>
                </td>
            </tr>
        </table>
    </article>

    <article data-grupo="Listado" class="tituloSeccion">
        Materiales Pendientes de Compra
    </article>

    <article data-grupo="Listado" class="listado">

        <article class="areaBotonesListado">
            <img id="imgMostrarFiltros" alt="Mostrar criterios de Búsqueda" title="Mostrar criterios de Búsqueda" data-tipo="mostrarFiltros" src="" />
        </article>

        <article class="formulario sinBorde" style="float: left !important;">
            <asp:CheckBox runat="server" ID="chkSeleccionarTodos" AutoPostBack="true" />&nbsp;&nbsp;
            Seleccionar Todos
        </article>
        <br /><br />
        <asp:UpdatePanel runat="server" UpdateMode="Conditional" ID="upRpAdjunto">
            <ContentTemplate>
                <asp:Repeater runat="server" ID="rpDetalles">
                    <HeaderTemplate>
                        <table>
                            <tr>
                                <th>&nbsp;</th>
                                <th>No. de OT</th>
                                <th>Código</th>
                                <th>Descripción</th>
                                <th>Cantidad</th>
                                <th>Observaciones</th>
                                <th>Fecha Asignación</th>
                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr class="lineaDelListado">
                            <td>
                                <asp:CheckBox runat="server" ID="chkDetalle" 
                                    Checked='<%#IIf(CType(Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_DETALLE_MATERIAL.SELECCIONADO), String) = "0", False, True)%>'
                                    AutoPostBack="true" OnCheckedChanged="chkDetalle_CheckedChanged"
                                    CommandArgument='<%# String.Format("{0}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_DETALLE_MATERIAL.ID_DETALLE_MATERIAL))%>' />
                                <asp:HiddenField runat="server" ID="hdfIdDetalleMaterial" Value="<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_DETALLE_MATERIAL.ID_DETALLE_MATERIAL)%>" />
                            </td>
                            <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_DETALLE_MATERIAL.ID_ORDEN_TRABAJO)%></td>
                            <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_DETALLE_MATERIAL.ID_MATERIAL)%></td>
                            <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_DETALLE_MATERIAL.DESCRIPCION)%></td>
                            <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_DETALLE_MATERIAL.CANTIDAD_SOLICITADA_MEDIDA)%></td>
                            <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_DETALLE_MATERIAL.DETALLE)%></td>
                            <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_DETALLE_MATERIAL.FECHA_ASIGNACION)%></td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="chkSeleccionarTodos" EventName="CheckedChanged" />
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

            deshabilitarControl('#btnCancelarBusqueda');
            ocultarAreaDeListado();
            mostrarAreaFiltrosDeBusqueda();
        };

        $(document).ready(function () {
            aplicarConfiguracionBasicaListado({ alternarVisibilidadFiltroLista: true });
            configurarDatePickerRango("#<%=Me.txtFiltroFechaDesde.ClientID%>", "#<%=Me.txtFiltroFechaHasta.ClientID%>");

            $('#btnCancelar').click(function () {
                regresarAlListado();
            });

            configurarLongitudMaximaTexto('#<%=Me.txtObservaciones.ClientID%>',<%=Utilerias.OrdenesDeTrabajo.Modelo.OTT_GESTION_COMPRA.OBSERVACIONES_BD_TAMANO%>);
            configurarContadorCaracteresRestantes('#<%=Me.txtObservaciones.ClientID%>','#spContadorTxtObservaciones');

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
