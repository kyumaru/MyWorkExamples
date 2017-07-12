<%@ Page Title="Historial de denegaciones de órdenes de trabajo hijas" Language="VB" MasterPageFile="~/MasterPage/Mp_Listado.master" AutoEventWireup="false" CodeFile="Lst_OT_OrdenesTrabajoSupervisor.aspx.vb" Inherits="OrdenesDeTrabajo_Lst_OT_OrdenesTrabajoSupervisor" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register TagPrefix="wuc" Namespace="Utilerias.WebForms.Controles" Assembly="Utilerias.WebForms.Controles" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphFormulario" runat="Server">

    <header>
        <h2>Historial de denegaciones de órdenes de trabajo hijas</h2>
    </header>

    <article data-grupo="FiltrosDeBusqueda" class="tituloSeccion">
        Criterios de búsqueda
    </article>

    <article data-grupo="FiltrosDeBusqueda" class="formulario">

        <table>
            <tr>
                <th>Número de Orden</th>
                <td>
                    <asp:TextBox runat="server" ID="txtNumOrden" data-tipocontrol="texto" TextMode="MultiLine" Rows="1" Width="59%"></asp:TextBox>
                    
                </td>
            </tr>
            <tr>
                <th>Número de orden en PDAGO</th>
                <td>
                    <asp:TextBox Width="59%" runat="server" ID="txtPdago" data-tipocontrol="texto" AppendDataBoundItems="true"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>Categoría de Servicio</th>
                <td>
                    <asp:DropDownList Width="59%" runat="server" ID="ddlCategoriaServicio" data-tipocontrol="combo" AppendDataBoundItems="true"></asp:DropDownList>
                </td>
            </tr>
            <%--<tr>
                <th>Estado</th>
                <td>
                    <asp:DropDownList runat="server" ID="ddlFiltroEstado" data-tipocontrol="combo" AppendDataBoundItems="true"></asp:DropDownList>
                </td>
            </tr>--%>
            <tr>
                <th>Tipo de Orden</th>
                <td>
                    <asp:DropDownList Width="59%" runat="server" ID="ddlTipoOrden" data-tipocontrol="combo" AppendDataBoundItems="true"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <th>Sector o Taller</th>
                <td>
                    <asp:DropDownList Width="59%" runat="server" ID="ddlSectorTaller" data-tipocontrol="combo" AppendDataBoundItems="true"></asp:DropDownList>
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
        Listado de Ordenes de Trabajo de Mantenimiento y Construcción
    </article>

    <article data-grupo="Listado" class="listado">
        <article class="areaBotonesListado">
            <img alt="Mostrar criterios de búsqueda" data-tipo="mostrarFiltros" src="" id="img1" />
        </article>

        <asp:Repeater runat="server" ID="rpOrdenTrabajo">
            <HeaderTemplate>
                <table>
                    <tr>
                        <th style="width:15%">
                            <asp:LinkButton runat="server" ID="lnkNoOt" Text="No. OT" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ORDEN_TRABAJOLST.ID_ORDEN_TRABAJO%>" CommandArgument="ASC" OnCommand="lnkRpOrdenTrabajo_Command"></asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton runat="server" ID="lnkTipoOrden" Text="Fecha de Solicitud" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ORDEN_TRABAJOLST.DESC_TIPO_ORDEN%>" CommandArgument="ASC" OnCommand="lnkRpOrdenTrabajo_Command"></asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton runat="server" ID="lnkFechaSolicita" Text="Fecha de Solicitud" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ORDEN_TRABAJOLST.FECHA_HORA_SOLICITA%>" CommandArgument="ASC" OnCommand="lnkRpOrdenTrabajo_Command"></asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton runat="server" ID="lnkSolicitante" Text="Solicitante" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ORDEN_TRABAJOLST.NOMBRE_SOLICITANTE%>" CommandArgument="ASC" OnCommand="lnkRpOrdenTrabajo_Command"></asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton runat="server" ID="lnkCategServ" Text="Categoría de Servicio" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ORDEN_TRABAJOLST.DESC_CATEGORIA_SERVICIO%>" CommandArgument="ASC" OnCommand="lnkRpOrdenTrabajo_Command"></asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton runat="server" ID="lnkNumeroOrden" Text="No. OT PDAGO" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ORDEN_TRABAJOLST.NUMERO_ORDEN%>" CommandArgument="ASC" OnCommand="lnkRpOrdenTrabajo_Command"></asp:LinkButton>
                        </th>
                        <th>Lugar Exacto</th>
                        <th>
                            <asp:LinkButton runat="server" ID="lnkEstado" Text="Estado" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ORDEN_TRABAJOLST.DESC_ESTADO_ORDEN%>" CommandArgument="ASC" OnCommand="lnkRpOrdenTrabajo_Command"></asp:LinkButton>
                        </th>
                        <th>Desc. Trab</th>
                        <th>&nbsp;</th>
                        <th>&nbsp;</th>
                        <th>&nbsp;</th>
                        <th>&nbsp;</th>
                        <th>&nbsp;</th>
                        <th>&nbsp;</th>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr class="lineaDelListado" id="trTabla" runat="server">
                    <td style="width:15%"><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ORDEN_TRABAJOLST.ID_ORDEN_TRABAJO)%></td>
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ORDEN_TRABAJOLST.DESC_TIPO_ORDEN)%></td>
                    <td><%#CType(Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ORDEN_TRABAJOLST.FECHA_HORA_SOLICITA), DateTime).ToString(Utilerias.OrdenesDeTrabajo.Constantes.FORMATO_FECHA_UI)%></td>
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ORDEN_TRABAJOLST.NOMBRE_SOLICITANTE)%></td>
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ORDEN_TRABAJOLST.DESC_CATEGORIA_SERVICIO)%></td>
                    <td><%#IIf(Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ORDEN_TRABAJOLST.NUMERO_ORDEN).ToString = "0", "", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ORDEN_TRABAJOLST.NUMERO_ORDEN).ToString)%></td>
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ORDEN_TRABAJOLST.PARTE_SENNAS_EXACTAS)%></td>
                    <td style="text-align:center">
                        <asp:LinkButton runat="server" ID="lnkTrazabilidad" Style="text-decoration: underline; color: blue;"
                            Text="<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ORDEN_TRABAJOLST.DESC_ESTADO_ORDEN)%>"
                            CommandArgument='<%# String.Format("{0}¬{1}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ORDEN_TRABAJOLST.ID_UBICACION), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ORDEN_TRABAJOLST.ID_ORDEN_TRABAJO))%>'
                            OnCommand="lnkTrazabilidad_Command"></asp:LinkButton>
                    </td>
                    <td>
                        <asp:Image runat="server" ID="imgTooltipEspecificoPorControl" data-tipo="tooltip"
                            title="<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ORDEN_TRABAJOLST.DESCRIPCION_TRABAJO)%>"
                            ImageUrl='<%#AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Informacion.png")%>' />
                    </td>
                    <td>
                        <article style="display: <%#IIf((CType(Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ORDEN_TRABAJOLST.INCLUIDA_EN_RECEPCION), Integer) = 1), "block", "none")%>">
                            <asp:ImageButton ID="imgModificar" runat="server" AlternateText="Modificar" ToolTip="Modificar" data-tipo="modificarRegistro"
                                Visible="<%#IIf(Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ORDEN_TRABAJOLST.ESTADO_ORDEN_TRABAJO).ToString = Utilerias.OrdenesDeTrabajo.EstadoOrden.PENDIENTE_DE_ENVIO.ToString, True, False) %>"
                                CommandArgument='<%# String.Format("{0}¬{1}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ORDEN_TRABAJOLST.ID_UBICACION), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ORDEN_TRABAJOLST.ID_ORDEN_TRABAJO))%>'
                                OnClick="ibModificar_Click" />
                        </article>

                        <asp:ImageButton ID="imgConsultar" runat="server" AlternateText="Consultar" ToolTip="Consultar" data-tipo="consultarRegistro"
                            Visible="<%#IIf((Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ORDEN_TRABAJOLST.ESTADO_ORDEN_TRABAJO).ToString <> Utilerias.OrdenesDeTrabajo.EstadoOrden.PENDIENTE_DE_ENVIO.ToString) OR (CType(Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ORDEN_TRABAJOLST.INCLUIDA_EN_RECEPCION), Integer) = 0), True, False) %>"
                            CommandArgument='<%# String.Format("{0}¬{1}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ORDEN_TRABAJOLST.ID_UBICACION), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ORDEN_TRABAJOLST.ID_ORDEN_TRABAJO))%>'
                            ImageUrl='<%#AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Lupa.png")%>'
                            onmouseover='<%#String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Lupa.png"))%>'
                            onmouseout='<%#String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Lupa.png"))%>'
                            OnClick="ibConsultar_Click" />
                    </td>
                    
                    <td>
                        <article style="display: <%#IIf(Roles.IsUserInRole(Membership.GetUser.UserName, Utilerias.OrdenesDeTrabajo.RolesSistema.OT_REGISTRO_SOLICITUD), "block", "none")%>">
                        <asp:ImageButton runat="server" ID="ibBorrar" AlternateText="Borrar la Orden" ToolTip="Borrar la Orden" data-tipo="borrarRegistro"
                            CommandArgument='<%# String.Format("{0}¬{1}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ORDEN_TRABAJOLST.ID_UBICACION), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ORDEN_TRABAJOLST.ID_ORDEN_TRABAJO))%>'
                            Visible="<%#IIf((CType(Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ORDEN_TRABAJOLST.INCLUIDA_EN_RECEPCION), Integer) = 1) And (CType(Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ORDEN_TRABAJOLST.ESTADO_ORDEN_TRABAJO), String) = Utilerias.OrdenesDeTrabajo.EstadoOrden.PENDIENTE_DE_ENVIO), True, False)%>"
                            OnClick="ibBorrar_Click" />
                        </article>
                    </td>
                    <td>     
                        <article style="display: <%#IIf(Roles.IsUserInRole(Membership.GetUser.UserName, Utilerias.OrdenesDeTrabajo.RolesSistema.OT_REGISTRO_SOLICITUD), "block", "none")%>">                   
                          <asp:ImageButton ID="imgAsignarNumeroOt" runat="server" AlternateText="Asignar No. Orden Trabajo" ToolTip="Asignar No. Orden Trabajo"                           
                            CommandArgument='<%# String.Format("{0}¬{1}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ORDEN_TRABAJOLST.ID_UBICACION), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ORDEN_TRABAJOLST.ID_ORDEN_TRABAJO))%>'
                            ImageUrl='<%#AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Agregar.png")%>'
                            onmouseover='<%#String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Icono_Agregar.png"))%>'
                            onmouseout='<%#String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Agregar.png"))%>'
                            OnClick="ibAsignarNumeroOt_Click" />
                        </article>

                    </td>
                    <td>
                        <article style="display: <%#IIf(Roles.IsUserInRole(Membership.GetUser.UserName, Utilerias.OrdenesDeTrabajo.RolesSistema.OT_REGISTRO_SOLICITUD), "block", "none")%>">
                            <asp:ImageButton runat="server" ID="ibEnviar" AlternateText="Enviar Orden a Sector o Taller" ToolTip="Enviar Orden a Sector o Taller" data-tipo="enviarRegistro"
                                CommandArgument='<%# String.Format("{0}¬{1}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ORDEN_TRABAJOLST.ID_UBICACION), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ORDEN_TRABAJOLST.ID_ORDEN_TRABAJO))%>'
                                ImageUrl='<%#AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Flecha_Derecha.png")%>'
                                onmouseover='<%#String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Flecha_Derecha.png"))%>'
                                onmouseout='<%#String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Flecha_Derecha.png"))%>'
                                OnClick="ibEnviar_Click"
                                Visible="<%#IIf(Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ORDEN_TRABAJOLST.ESTADO_ORDEN_TRABAJO).ToString = Utilerias.OrdenesDeTrabajo.EstadoOrden.PENDIENTE_DE_ENVIO, True, False)%>" />
                        </article>
                    </td>
                    <td>
                        <article style="display: <%#IIf(Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ORDEN_TRABAJOLST.ESTADO_ORDEN_TRABAJO).ToString = Utilerias.OrdenesDeTrabajo.EstadoOrden.PARA_IMPRESION, "block", "none")%>">
                        <asp:ImageButton runat="server" ID="ibImprimir" AlternateText="Imprimir Orden" ToolTip="Imprimir Orden"
                            CommandArgument='<%# String.Format("{0}_{1}_{2}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ORDEN_TRABAJOLST.ID_UBICACION), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ORDEN_TRABAJOLST.ID_ORDEN_TRABAJO), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ORDEN_TRABAJOLST.ESTADO_ORDEN_TRABAJO))%>'
                            ImageUrl='<%#AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Impresora.png")%>'
                            onmouseover='<%#String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Impresora.png"))%>'
                            onmouseout='<%#String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Impresora.png"))%>'
                            OnClick="ibImprimir_Click" />
                        </article>
                    </td>
                    <td>
                        <asp:HiddenField runat="server" ID="hdfTipoOrden" Value="<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ORDEN_TRABAJOLST.TIPO_ORDEN_TRABAJO)%>" />
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
            window.location = 'Lst_OT_OrdenesTrabajoRecepcion.aspx';
        };

        function mostrarAlertaAdvertencia(pvc_Mensaje) {
            mostrarAlerta(
                '#arAlerta',
                {
                    mensaje: pvc_Mensaje,
                    tipo: 'advertencia',
                    transparencia: 0.9,
                    posicion: 'top'
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
                    posicion: 'top'
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
                    posicion: 'top'
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
                    posicion: 'top'
                }
            );

            deshabilitarControl('#btnCancelarBusqueda');

            ocultarAreaDeListado();
            mostrarAreaFiltrosDeBusqueda();
        };

        function mostrarAlertaRegistroBorrado() {
            mostrarAlerta(
                '#arAlerta',
                {
                    mensaje: 'Se ha borrado el registro seleccionado.',
                    tipo: "exito",
                    transparencia: 0.9,
                    posicion: 'top'
                }
            );
        };

        function mostrarAlertaRegistroNoBorrado() {
            mostrarAlerta(
                '#arAlerta',
                {
                    mensaje: 'No ha sido posible borrar el registro seleccionado.',
                    tipo: "advertencia",
                    transparencia: 0.9,
                    posicion: 'top'
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
                    posicion: 'top'
                });
            ocultarAreaFiltrosDeBusqueda();
        };

        function mostrarPopupConfirmaDeseaBorrarRegistro(pvo_UniqueIdControl) {
            var vlo_ConfiguracionPopup = {
                titulo: 'Ordenes de Trabajo Recepción',
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
                titulo: 'Ordenes de Trabajo Recepción',
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

            //$('[data-tipo="tooltip"]').each(function () {
            //    habilitarTooltipPorControl('#' + this.id);
            //});

            $('[data-tipo="tooltip"]').each(function () {
                //habilitarTooltipPorControl('#' + this.id);
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

