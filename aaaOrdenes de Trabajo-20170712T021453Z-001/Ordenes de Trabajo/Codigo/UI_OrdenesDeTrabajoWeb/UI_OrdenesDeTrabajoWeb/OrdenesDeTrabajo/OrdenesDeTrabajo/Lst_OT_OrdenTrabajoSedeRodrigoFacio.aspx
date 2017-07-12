<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/Mp_Listado.master" AutoEventWireup="false" CodeFile="Lst_OT_OrdenTrabajoSedeRodrigoFacio.aspx.vb" Inherits="OrdenesDeTrabajo_Lst_OT_OrdenTrabajoSedeRodrigoFacio" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register TagPrefix="wuc" Namespace="Utilerias.WebForms.Controles" Assembly="Utilerias.WebForms.Controles" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphFormulario" runat="Server">

    <header>
        <h2>Solicitud de orden de trabajo a sede Rodrigo Facio</h2>
    </header>

    <article data-grupo="FiltrosDeBusqueda" class="tituloSeccion">
        Criterios de búsqueda
    </article>

    <article data-grupo="FiltrosDeBusqueda" class="formulario">
        <article class="areaBotonesListado">
            <a id="lnkNuevoRegistro2" href="Frm_OT_OrdenTrabajoSedeRodrigoFacio.aspx?pvn_Operacion=<%=Utilerias.OrdenesDeTrabajo.eOperacion.Agregar%>">
                <img id="imgNuevoRegistro2" data-tipo="nuevoRegistro" title="Agregar nueva orden" alt="Agregar nueva orden" src="" />
            </a>
        </article>

        <table>
            <tr>
                <th>Número de Orden</th>
                <td>
                    <asp:TextBox Width="59%" runat="server" ID="txtNumOrden" data-tipocontrol="texto" TextMode="MultiLine" Rows="1"></asp:TextBox>
                    <ajax:FilteredTextBoxExtender ID="ftbTxtNumOrden" runat="server" TargetControlID="txtNumOrden" FilterMode="ValidChars" FilterType="Numbers"></ajax:FilteredTextBoxExtender>
                </td>
            </tr>
            <tr>
                <th>Categoría de Servicio</th>
                <td>
                    <asp:DropDownList Width="59%" runat="server" ID="ddlCategoriaServicio" data-tipocontrol="combo" AppendDataBoundItems="true"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <th>Condición de la Orden</th>
                <td>
                    <asp:DropDownList Width="59%" runat="server" ID="ddlCondiconOrden" data-tipocontrol="combo" AppendDataBoundItems="true"></asp:DropDownList>
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
        Listado de ordenes de trabajo a sede Rodrigo Facio
    </article>

    <article data-grupo="Listado" class="formulario">
        <table>
            <tr runat="server" id="trUnidad">
                <th>Unidad</th>
                <td>
                    <asp:DropDownList runat="server" ID="ddlUnidad" data-tipocontrol="combo" AppendDataBoundItems="true" AutoPostBack="true"></asp:DropDownList>
                </td>
            </tr>
            <tr runat="server" id="trLblUnidad">
                <th>Unidad</th>
                <td>
                    <asp:Label runat="server" ID="lblNombreUnidad" data-tipocontrol="etiqueta"></asp:Label>
                </td>
            </tr>
        </table>
    </article>

    <article data-grupo="Listado" class="listado sinBorde">
        <article class="areaBotonesListado">
            <a id="lnkNuevoRegistro" href="Frm_OT_OrdenTrabajoSedeRodrigoFacio.aspx?pvn_Operacion=<%=Utilerias.OrdenesDeTrabajo.eOperacion.Agregar%>">
                <img id="imgNuevoRegistro" data-tipo="nuevoRegistro" title="Agregar nueva orden" alt="Agregar nueva orden" src="" />
            </a>
            <img id="imgMostrarFiltros" alt="Mostrar criterios de Búsqueda" title="Mostrar criterios de Búsqueda" data-tipo="mostrarFiltros" src="" />
        </article>

        <asp:Repeater runat="server" ID="rpOrdenTrabajo">
            <HeaderTemplate>
                <table>
                    <tr>
                        <th>
                            <asp:LinkButton runat="server" ID="lnkNoOt" Text="No. OT" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OT_UNION_PRE_TRANS_HIST.ID_ORDEN_TRABAJO%>" CommandArgument="ASC" OnCommand="lnkRpOrdenTrabajo_Command"></asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton runat="server" ID="lnkFechaSolicita" Text="Fecha de Solicitud" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OT_UNION_PRE_TRANS_HIST.FECHA_HORA_SOLICITA%>" CommandArgument="ASC" OnCommand="lnkRpOrdenTrabajo_Command"></asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton runat="server" ID="lnkCategServ" Text="Categoría de Servicio" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OT_UNION_PRE_TRANS_HIST.DESC_CATEGORIA_SERVICIO%>" CommandArgument="ASC" OnCommand="lnkRpOrdenTrabajo_Command"></asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton runat="server" ID="lnkNumeroOrden" Text="No. OT PDAGO" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OT_UNION_PRE_TRANS_HIST.NUMERO_ORDEN%>" CommandArgument="ASC" OnCommand="lnkRpOrdenTrabajo_Command"></asp:LinkButton>
                        </th>
                        <th>Lugar Exacto</th>
                        <th>
                            <asp:LinkButton runat="server" ID="lnkEstado" Text="Estado" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OT_UNION_PRE_TRANS_HIST.DESC_ESTADO_ORDEN%>" CommandArgument="ASC" OnCommand="lnkRpOrdenTrabajo_Command"></asp:LinkButton>
                        </th>
                        <th>Desc. Trab</th>
                        <th>&nbsp;</th>
                        <th>&nbsp;</th>
                        <th>&nbsp;</th>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr class="lineaDelListado" id="trTabla" runat="server">
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_UNION_PRE_TRANS_HIST.ID_ORDEN_TRABAJO)%></td>
                    <td><%#CType(Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_UNION_PRE_TRANS_HIST.FECHA_HORA_SOLICITA), DateTime).ToString(Utilerias.OrdenesDeTrabajo.Constantes.FORMATO_FECHA_UI)%></td>
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_UNION_PRE_TRANS_HIST.DESC_CATEGORIA_SERVICIO)%></td>
                    <td><%#IIf(Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_UNION_PRE_TRANS_HIST.NUMERO_ORDEN).ToString = "0", "", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_UNION_PRE_TRANS_HIST.NUMERO_ORDEN).ToString)%></td>
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_UNION_PRE_TRANS_HIST.PARTE_SENNAS_EXACTAS)%></td>
                    <td>
                        <asp:LinkButton runat="server" ID="lnkTrazabilidad" Style="text-decoration: underline; color: blue;"
                            Text="<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_UNION_PRE_TRANS_HIST.DESC_ESTADO_ORDEN)%>"
                            CommandArgument='<%# String.Format("{0}¬{1}¬{2}¬{3}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_UNION_PRE_TRANS_HIST.ID_UBICACION), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_UNION_PRE_TRANS_HIST.ID_ORDEN_TRABAJO), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_UNION_PRE_TRANS_HIST.ID_PRE_ORDEN_TRABAJO), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_UNION_PRE_TRANS_HIST.ES_PRE_ORDEN))%>'
                            OnCommand="lnkTrazabilidad_Command"
                            Visible='<%# IIf(Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_UNION_PRE_TRANS_HIST.ES_HISTORICO).ToString = "0", True, False)%>'></asp:LinkButton>

                        <asp:Label runat="server" 
                            Visible='<%# IIf(Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_UNION_PRE_TRANS_HIST.ES_HISTORICO).ToString = "1", True, False)%>'
                              Text="<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_UNION_PRE_TRANS_HIST.DESC_ESTADO_ORDEN)%>"></asp:Label>
                    </td>
                    <td>
                        <asp:Image runat="server" ID="imgTooltipEspecificoPorControl" data-tipo="tooltip"
                            title="<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_UNION_PRE_TRANS_HIST.DESCRIPCION_TRABAJO)%>"
                            ImageUrl='<%#AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x24, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Informacion.png")%>' />
                    </td>
                    <td>
                       <asp:ImageButton ID="imgModificar" runat="server" AlternateText="Modificar" ToolTip="Modificar" data-tipo="modificarRegistro" 
                            Visible="<%#IIf(Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_UNION_PRE_TRANS_HIST.ESTADO_ORDEN_TRABAJO).ToString = Utilerias.OrdenesDeTrabajo.EstadoOrden.PENDIENTE_DE_ENVIO.ToString, True, False)%>"
                            CommandArgument='<%# String.Format("{0}¬{1}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_UNION_PRE_TRANS_HIST.ID_UBICACION), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_UNION_PRE_TRANS_HIST.ID_PRE_ORDEN_TRABAJO))%>'
                            OnClick="ibModificar_Click"/>
                          
                        <asp:ImageButton ID="imgConsultar" runat="server" AlternateText="Consultar" ToolTip="Consultar" data-tipo="consultarRegistro"
                            Visible='<%#IIf((Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_UNION_PRE_TRANS_HIST.ESTADO_ORDEN_TRABAJO).ToString <> Utilerias.OrdenesDeTrabajo.EstadoOrden.PENDIENTE_DE_ENVIO.ToString) AND (Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_UNION_PRE_TRANS_HIST.ES_HISTORICO).ToString = "0"), True, False)%>'
                            CommandArgument='<%# String.Format("{0}¬{1}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_UNION_PRE_TRANS_HIST.ID_UBICACION), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_UNION_PRE_TRANS_HIST.ID_ORDEN_TRABAJO))%>'
                            ImageUrl='<%#AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Lupa.png")%>'
                            onmouseover='<%#String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Lupa.png"))%>'
                            onmouseout='<%#String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Lupa.png"))%>'
                            OnClick="ibConsultar_Click" />
                    </td>
                    <td>
                        <asp:ImageButton runat="server" ID="ibBorrar" AlternateText="Borrar la Orden" ToolTip="Borrar la Orden" data-tipo="borrarRegistro"
                            CommandArgument='<%# String.Format("{0}¬{1}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_UNION_PRE_TRANS_HIST.ID_UBICACION), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_UNION_PRE_TRANS_HIST.ID_PRE_ORDEN_TRABAJO))%>'
                            Visible="<%#IIf((CType(Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_UNION_PRE_TRANS_HIST.ESTADO_ORDEN_TRABAJO), String) = Utilerias.OrdenesDeTrabajo.EstadoOrden.PENDIENTE_DE_ENVIO), True, False)%>"
                            OnClick="ibBorrar_Click" />
                    </td>
                    <td>
                        <asp:ImageButton runat="server" ID="ibEnviar" AlternateText="Enviar Orden a Sector o Taller" ToolTip="Enviar Orden a Sector o Taller" data-tipo="enviarRegistro"
                            CommandArgument='<%# String.Format("{0}¬{1}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_UNION_PRE_TRANS_HIST.ID_UBICACION), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_UNION_PRE_TRANS_HIST.ID_PRE_ORDEN_TRABAJO))%>'
                            ImageUrl='<%#AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Flecha_Derecha.png")%>'
                            onmouseover='<%#String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Flecha_Derecha.png"))%>'
                            onmouseout='<%#String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Flecha_Derecha.png"))%>'
                            OnClick="ibEnviar_Click"
                            Visible="<%#IIf(Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_UNION_PRE_TRANS_HIST.ESTADO_ORDEN_TRABAJO).ToString = Utilerias.OrdenesDeTrabajo.EstadoOrden.PENDIENTE_DE_ENVIO, True, False)%>" />
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
    <article id="arpopupConfirmaDeseaBorrar"></article>
    <article id="arpopupConfirmaEnviar"></article>
    <article id="arPopupGenerico"></article>

    <script type="text/javascript">

        function regresarAlListado() {
            window.location = 'Lst_OT_OrdenTrabajoSedeRodrigoFacio.aspx';
        };

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

        function sinUnidadesAsociadas() {
            mostrarAlerta(
                '#arAlerta',
                {
                    mensaje: 'La unidad seleccionada aún no posee ningún registro asociado en el sistema de órdenes de trabajo. Para solventar esta situación contacte al administrador del sistema.',
                    tipo: "advertencia",
                    transparencia: 0.9,
                    posicion: 'center'
                }
            );
            ocultaFiltrosCombo();
        }

        function deshabilitarLupaBusqueda() {
            deshabilitarControl('#imgMostrarFiltros');
        };

        function habilitarLupaBusqueda() {
            habilitarControl('#imgMostrarFiltros');
        }

        function mostrarAlertaNoHayDatosCombo() {
            mostrarAlerta(
                '#arAlerta',
                {
                    mensaje: 'No se cuenta con información para mostrar.',
                    tipo: "advertencia",
                    transparencia: 0.9,
                    posicion: 'center'
                }
            );

            ocultarAreaFiltrosDeBusqueda();
        };

        function mostrarAlertaRegistroBorrado() {
            mostrarAlerta(
                '#arAlerta',
                {
                    mensaje: 'Se ha borrado el registro seleccionado.',
                    tipo: "exito",
                    transparencia: 0.9,
                    posicion: 'center'
                }
            );
        };

        function ocultaFiltrosCombo() {
            ocultarAreaFiltrosDeBusqueda();
            mostrarAreaDeListado();
        }

        function mostrarAlertaRegistroNoBorrado() {
            mostrarAlerta(
                '#arAlerta',
                {
                    mensaje: 'No ha sido posible borrar el registro seleccionado.',
                    tipo: "advertencia",
                    transparencia: 0.9,
                    posicion: 'center'
                }
            );
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

        function mostrarPopupConfirmaDeseaBorrarRegistro(pvo_UniqueIdControl) {
            var vlo_ConfiguracionPopup = {
                titulo: 'Ordenes de Trabajo',
                mensaje: '¿Desea borrar el registro seleccionado?',
                botones:
                        [
                            {
                                idControl: "btnSi",
                                textoBoton: "Si",
                                onClick:
                                    function (e) {
                                        $(this).attr("disabled", "disabled");
                                        __doPostBack(pvo_UniqueIdControl, '');
                                    }
                            },
                            {
                                idControl: "btnNo",
                                textoBoton: "No",
                                onClick: function () { cerrarPopup(); }
                            },
                            {
                                idControl: "btnCancelar",
                                textoBoton: "Cancelar",
                                onClick: function () { cerrarPopup(); }
                            }
                        ]
            };

            $('#arpopupConfirmaDeseaBorrar').popup(vlo_ConfiguracionPopup);

            window.location = '#arpopupConfirmaDeseaBorrar';

            return false;
        };

        function mostrarPopupConfirmaDeseaEnviarRegistro(pvo_UniqueIdControl) {
            var vlo_ConfiguracionPopup = {
                titulo: 'Ordenes de Trabajo',
                mensaje: '¿Desea enviar a sector o taller el registro seleccionado?',
                botones:
                        [
                            {
                                idControl: "btnSi",
                                textoBoton: "Si",
                                onClick:
                                    function (e) {
                                        $(this).attr("disabled", "disabled");
                                        __doPostBack(pvo_UniqueIdControl, '');
                                    }
                            },
                            {
                                idControl: "btnNo",
                                textoBoton: "No",
                                onClick: function () { cerrarPopup(); }
                            },
                            {
                                idControl: "btnCancelar",
                                textoBoton: "Cancelar",
                                onClick: function () { cerrarPopup(); }
                            }
                        ]
            };

            $('#arpopupConfirmaEnviar').popup(vlo_ConfiguracionPopup);

            window.location = '#arpopupConfirmaEnviar';

            return false;
        };

        $(document).ready(function () {
            aplicarConfiguracionBasicaListado({ alternarVisibilidadFiltroLista: true });

            $('[data-tipo="borrarRegistro"]').click(function () { return mostrarPopupConfirmaDeseaBorrarRegistro($(this).data("uniqueid")); });

            $('[data-tipo="enviarRegistro"]').click(function () { return mostrarPopupConfirmaDeseaEnviarRegistro($(this).data("uniqueid")); });

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

