<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/Mp_Listado.master" AutoEventWireup="false" CodeFile="Lst_OT_OrdenTrabajo.aspx.vb" Inherits="OrdenesDeTrabajo_Lst_OT_OrdenTrabajo" %>

<%@ Register TagPrefix="wuc" Namespace="Utilerias.WebForms.Controles" Assembly="Utilerias.WebForms.Controles" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphFormulario" runat="Server">

    <header>
        <h2>Ordenes de Trabajo de Mantenimiento y Construcción</h2>
    </header>

    <article data-grupo="FiltrosDeBusqueda" class="tituloSeccion">
        Criterios de búsqueda
    </article>

    <article data-grupo="FiltrosDeBusqueda" class="formulario">
        <article class="areaBotonesListado">
            <a id="lnkNuevoRegistro2" href="Frm_OT_OrdenTrabajo.aspx?pvn_Operacion=<%=Utilerias.OrdenesDeTrabajo.eOperacion.Agregar%>">
                <img id="imgNuevoRegistro2" data-tipo="nuevoRegistro" title="Agregar nueva orden" alt="Agregar nueva orden" src="" />
            </a>
        </article>

        <table>
            <tr>
                <th>Estado</th>
                <td>
                    <asp:DropDownList Width="59%" runat="server" ID="ddlFiltroEstado" data-tipocontrol="combo" AppendDataBoundItems="true"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <th>Categoría de Servicio</th>
                <td>
                    <asp:DropDownList Width="59%" runat="server" ID="ddlCategoriaServicio" data-tipocontrol="combo" AppendDataBoundItems="true"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <th>Descripción de Trabajo</th>
                <td>
                    <asp:TextBox Width="59%" runat="server" ID="txtDescTrabajo" data-tipocontrol="texto" TextMode="MultiLine" Rows="9"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>Número de Orden</th>
                <td>
                    <asp:TextBox runat="server" ID="txtNumOT" data-tipocontrol="texto" Width="59%" MaxLength="10"></asp:TextBox>
                    <ajax:FilteredTextBoxExtender ID="ftbTxtNumOT" runat="server" TargetControlID="txtNumOT" FilterMode="ValidChars" ValidChars="-" FilterType="Numbers, Custom"></ajax:FilteredTextBoxExtender>
                </td>
            </tr>
            <tr>
                <th>Número PDAGO</th>
                <td>
                    <asp:TextBox runat="server" ID="txtNumPDAGO" data-tipocontrol="texto" Width="59%" MaxLength="10"></asp:TextBox>
                    <ajax:FilteredTextBoxExtender ID="ftbTxtNumPDAGO" runat="server" TargetControlID="txtNumPDAGO" FilterType="Numbers"></ajax:FilteredTextBoxExtender>
                </td>
            </tr>
        </table>
    </article>

    <article data-grupo="FiltrosDeBusqueda" class="areaBotones">
        <asp:Button runat="server" ID="btnBuscar" Text="Buscar" />
        <input type="button" data-tipo="limpiarFiltros" value="Limpiar Filtros" />
        <input id="btnCancelarBusqueda" type="button" data-tipo="cancelarBusqueda" value="Cancelar" />
    </article>

    <article runat="server" id="OrdenesDisenio" data-grupo="Listado">
        <article data-grupo="Listado" class="tituloSeccion">
            Ordenes de Diseño
        </article>
        <article class="listado">
            <asp:Repeater runat="server" ID="rpOrdenTrabajoDisenio">
                <HeaderTemplate>
                    <table>
                        <tr>
                            <th>No de OT</th>
                            <th>Nombre del Proyecto</th>
                            <th>Archivo Adjunto</th>
                            <th>Días faltantes para responder</th>
                            <th>Estado</th>
                            <th>&nbsp;</th>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_RESPUESTAS_USUARIO.ID_ORDEN_TRABAJO)%></td>
                        <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_RESPUESTAS_USUARIO.NOMBRE_PROYECTO)%></td>
                        <td>
                            <asp:LinkButton runat="server" ID="lnkArchivo"
                                CommandArgument='<%#Container.ItemIndex%>'
                                Style="text-decoration: underline; color: blue;"
                                OnCommand="lnkArchivo_Command"
                                Text="<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ADJUNTO_ORDEN_TRABAJOLST.NOMBRE_ARCHIVO)%>"></asp:LinkButton>
                        </td>

                        <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_RESPUESTAS_USUARIO.DIAS_FALTANTES)%></td>
                        <td>
                            <asp:LinkButton runat="server" ID="lnkTrazabilidad" Style="text-decoration: underline; color: blue;"
                                Text="<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_RESPUESTAS_USUARIO.DESC_ESTADO_ORDEN)%>"
                                CommandArgument='<%# String.Format("{0}¬{1}¬0¬0", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_RESPUESTAS_USUARIO.ID_UBICACION), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_RESPUESTAS_USUARIO.ID_ORDEN_TRABAJO))%>'
                                OnCommand="lnkTrazabilidad_Command"></asp:LinkButton>
                        </td>
                        <td>
                            <asp:ImageButton ID="imgResViabilidad" runat="server" AlternateText="Revisión de análisis de Viabilidad Técnica" ToolTip="Revisión de análisis de Viabilidad Técnica"
                                CommandArgument='<%# String.Format("{0}¬{1}¬{2}¬{3}¬{4}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_RESPUESTAS_USUARIO.ID_UBICACION), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_RESPUESTAS_USUARIO.ID_ORDEN_TRABAJO), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_RESPUESTAS_USUARIO.ANNO), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_RESPUESTAS_USUARIO.ID_SECTOR_TALLER), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_RESPUESTAS_USUARIO.DIAS_FALTANTES))%>'
                                ImageUrl='<%#AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Ver_Detalle.png")%>'
                                onmouseover='<%#String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Icono_Ver_Detalle.png"))%>'
                                onmouseout='<%#String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Ver_Detalle.png"))%>'
                                OnClick="imgResViabilidad_Click"
                                Visible='<%#IIf(Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_RESPUESTAS_USUARIO.ESTADO_ORDEN_TRABAJO).ToString = Utilerias.OrdenesDeTrabajo.EstadoOrden.PENDIENTE_RESPUESTA_SOLICITANTE, True, False)%>' />

                            <asp:ImageButton ID="imgAnteProyecto" runat="server" AlternateText="Revisión de AnteProyecto" ToolTip="Revisión de AnteProyecto"
                                CommandArgument='<%# String.Format("{0}¬{1}¬{2}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_RESPUESTAS_USUARIO.ID_UBICACION), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_RESPUESTAS_USUARIO.ID_ORDEN_TRABAJO), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_RESPUESTAS_USUARIO.ANNO))%>'
                                ImageUrl='<%#AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Ver_Detalle.png")%>'
                                onmouseover='<%#String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Icono_Ver_Detalle.png"))%>'
                                onmouseout='<%#String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Ver_Detalle.png"))%>'
                                OnClick="imgAnteProyecto_Click"
                                Visible='<%#IIf(Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_RESPUESTAS_USUARIO.ESTADO_ORDEN_TRABAJO).ToString = Utilerias.OrdenesDeTrabajo.EstadoOrden.ANTEPROYECTO_PENDIENTE_REVISION_SOLICITANTE, True, False)%>' />

                            <asp:ImageButton ID="imgPresupuesto" runat="server" AlternateText="Revisión de disponibilidad presupuestaria" ToolTip="Revisión de disponibilidad presupuestaria"
                                CommandArgument='<%# String.Format("{0}¬{1}¬{2}¬{3}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_RESPUESTAS_USUARIO.ID_UBICACION), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_RESPUESTAS_USUARIO.ID_ORDEN_TRABAJO), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_RESPUESTAS_USUARIO.ANNO), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_RESPUESTAS_USUARIO.DIAS_FALTANTES))%>'
                                ImageUrl='<%#AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Recibir_Dinero.png")%>'
                                onmouseover='<%#String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Icono_Recibir_Dinero.png"))%>'
                                onmouseout='<%#String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Recibir_Dinero.png"))%>'
                                OnClick="imgPresupuesto_Click"
                                Visible='<%#IIf(Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_RESPUESTAS_USUARIO.ESTADO_ORDEN_TRABAJO).ToString = Utilerias.OrdenesDeTrabajo.EstadoOrden.PRESUPUESTO_PENDIENTE_RESPUESTA_SOLICITANTE, True, False)%>' />

                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
        </article>
        <article data-grupo="Listado" class="areaPaginadorListado">
            <wuc:PaginadorNumerico runat="server" ID="pnRpOrdenesTrabajoDisenio" />
        </article>

        <article data-grupo="Listado" class="areaCantidadDeRegistro">
            <asp:Label runat="server" ID="lblCantidadRegistrosDisenio" Text="" Visible="true"></asp:Label>
        </article>
    </article>


    <br />

    <article data-grupo="Listado" class="tituloSeccion">
        Listado de Ordenes de Trabajo de Mantenimiento y Construcción
    </article>
    <article data-grupo="Listado" class="listado">
        <article class="areaBotonesListado">
            <a id="lnkNuevoRegistro" href="Frm_OT_OrdenTrabajo.aspx?pvn_Operacion=<%=Utilerias.OrdenesDeTrabajo.eOperacion.Agregar%>">
                <img id="imgNuevoRegistro" data-tipo="nuevoRegistro" title="Agregar nueva orden" alt="Agregar nueva orden" src="" />
            </a>
            <img id="imgMostrarFiltros" alt="Mostrar criterios de Búsqueda" title="Mostrar criterios de Búsqueda" data-tipo="mostrarFiltros" src="" />
        </article>

        <asp:Repeater runat="server" ID="rpOrdenTrabajo">
            <HeaderTemplate>
                <table>
                    <tr>
                        <th>
                            <asp:LinkButton runat="server" ID="lnkNoOt" Text="No. OT" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OT_UNION_ORDEN_PREORDEN.CONSECUTIVO%>" CommandArgument="ASC" OnCommand="lnkRpOrdenTrabajo_Command"></asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton runat="server" ID="lnkFechaSolicita" Text="Fecha de Solicitud" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OT_UNION_ORDEN_PREORDEN.FECHA_HORA_SOLICITA%>" CommandArgument="ASC" OnCommand="lnkRpOrdenTrabajo_Command"></asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton runat="server" ID="lnkCategServ" Text="Categoría de Servicio" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OT_UNION_ORDEN_PREORDEN.DESC_CATEGORIA_SERVICIO%>" CommandArgument="ASC" OnCommand="lnkRpOrdenTrabajo_Command"></asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton runat="server" ID="lnkNumeroOrden" Text="No. PDAGO" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OT_UNION_ORDEN_PREORDEN.NUMERO_ORDEN%>" CommandArgument="ASC" OnCommand="lnkRpOrdenTrabajo_Command"></asp:LinkButton>
                        </th>
                        <th>Lugar Exacto
                        </th>
                        <th>
                            <asp:LinkButton runat="server" ID="lnkEstado" Text="Estado" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OT_UNION_ORDEN_PREORDEN.DESC_ESTADO_ORDEN%>" CommandArgument="ASC" OnCommand="lnkRpOrdenTrabajo_Command"></asp:LinkButton>
                        </th>
                        <th>Desc. Trab
                        </th>
                        <th>&nbsp;</th>
                        <th>&nbsp;</th>
                        <th>&nbsp;</th>
                        <th>&nbsp;</th>
                        <th>&nbsp;</th>
                    </tr>
            </HeaderTemplate>

            <ItemTemplate>
                <tr class="lineaDelListado">
                    <td style="width: 13%"><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_UNION_ORDEN_PREORDEN.ID_ORDEN_TRABAJO)%></td>
                    <td><%#CType(Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_UNION_ORDEN_PREORDEN.FECHA_HORA_SOLICITA), Datetime).ToString(Utilerias.OrdenesDeTrabajo.Constantes.FORMATO_FECHA_UI)%></td>
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_UNION_ORDEN_PREORDEN.DESC_CATEGORIA_SERVICIO)%></td>
                    <td><%#IIf(Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_UNION_ORDEN_PREORDEN.NUMERO_ORDEN).ToString = "0", "", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_UNION_ORDEN_PREORDEN.NUMERO_ORDEN).ToString)%></td>
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_UNION_ORDEN_PREORDEN.PARTE_SENNAS_EXACTAS)%></td>
                    <td>
                        <asp:LinkButton runat="server" ID="lnkTrazabilidad" Style="text-decoration: underline; color: blue;"
                            Text="<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_UNION_ORDEN_PREORDEN.DESC_ESTADO_ORDEN)%>"
                            CommandArgument='<%# String.Format("{0}¬{1}¬{2}¬{3}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_UNION_ORDEN_PREORDEN.ID_UBICACION), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_UNION_ORDEN_PREORDEN.ID_ORDEN_TRABAJO), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_UNION_ORDEN_PREORDEN.ID_PRE_ORDEN_TRABAJO), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_UNION_ORDEN_PREORDEN.ES_PRE_ORDEN))%>'
                            OnCommand="lnkTrazabilidad_Command"></asp:LinkButton>
                    </td>
                    <td>
                        <asp:Image runat="server" ID="imgTooltipEspecificoPorControl" data-tipo="tooltip"
                            title="<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_UNION_ORDEN_PREORDEN.DESCRIPCION_TRABAJO)%>"
                            ImageUrl='<%#AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Informacion.png")%>' />
                    </td>
                    <td>
                        <asp:ImageButton ID="imgModificar" runat="server" AlternateText="Modificar" ToolTip="Modificar" data-tipo="modificarRegistro"
                            Visible="<%#IIf((Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_UNION_ORDEN_PREORDEN.ESTADO_ORDEN_TRABAJO).ToString = Utilerias.OrdenesDeTrabajo.EstadoOrden.DEVUELTA.ToString) Or (Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_UNION_ORDEN_PREORDEN.ESTADO_ORDEN_TRABAJO).ToString = Utilerias.OrdenesDeTrabajo.EstadoOrden.PENDIENTE_DE_ENVIO.ToString), True, False)%>"
                            CommandArgument='<%# String.Format("{0}¬{1}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_UNION_ORDEN_PREORDEN.ID_UBICACION), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_UNION_ORDEN_PREORDEN.ID_PRE_ORDEN_TRABAJO))%>'
                            OnClick="ibModificar_Click" />

                        <asp:ImageButton ID="imgConsultar" runat="server" AlternateText="Consultar" ToolTip="Consultar" data-tipo="consultarRegistro"
                            Visible="<%#IIf((Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_UNION_ORDEN_PREORDEN.ESTADO_ORDEN_TRABAJO).ToString <> Utilerias.OrdenesDeTrabajo.EstadoOrden.PENDIENTE_DE_ENVIO.ToString) And (Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_UNION_ORDEN_PREORDEN.ESTADO_ORDEN_TRABAJO).ToString <> Utilerias.OrdenesDeTrabajo.EstadoOrden.DEVUELTA.ToString), True, False)%>"
                            CommandArgument='<%# String.Format("{0}¬{1}¬{2}¬{3}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_UNION_ORDEN_PREORDEN.ID_UBICACION), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_UNION_ORDEN_PREORDEN.ID_ORDEN_TRABAJO), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_UNION_ORDEN_PREORDEN.ID_PRE_ORDEN_TRABAJO), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_UNION_ORDEN_PREORDEN.ES_PRE_ORDEN))%>'
                            ImageUrl='<%#AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Lupa.png")%>'
                            onmouseover='<%#String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Lupa.png"))%>'
                            onmouseout='<%#String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Lupa.png"))%>'
                            OnClick="ibConsultar_Click" />
                    </td>
                    <td>
                        <asp:ImageButton runat="server" ID="ibBorrar" AlternateText="Borrar la Orden" data-tipo="borrarRegistro"
                            ToolTip="Borrar la Orden"
                            Visible='<%#IIf((Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_UNION_ORDEN_PREORDEN.ESTADO_ORDEN_TRABAJO).ToString = Utilerias.OrdenesDeTrabajo.EstadoOrden.PENDIENTE_DE_ENVIO) Or (Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_UNION_ORDEN_PREORDEN.ESTADO_ORDEN_TRABAJO).ToString = Utilerias.OrdenesDeTrabajo.EstadoOrden.DEVUELTA)  Or (Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_UNION_ORDEN_PREORDEN.ESTADO_ORDEN_TRABAJO).ToString = Utilerias.OrdenesDeTrabajo.EstadoOrden.DENEGADA), True, False)%>'
                            CommandArgument='<%# String.Format("{0}¬{1}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_UNION_ORDEN_PREORDEN.ID_UBICACION), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_UNION_ORDEN_PREORDEN.ID_PRE_ORDEN_TRABAJO))%>'
                            OnClick="ibBorrar_Click" />
                    </td>
                    <td>
                        <article style="display: <%#IIf((Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_UNION_ORDEN_PREORDEN.ESTADO_ORDEN_TRABAJO).ToString = Utilerias.OrdenesDeTrabajo.EstadoOrden.DEVUELTA.ToString) Or (Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_UNION_ORDEN_PREORDEN.ESTADO_ORDEN_TRABAJO).ToString = Utilerias.OrdenesDeTrabajo.EstadoOrden.PENDIENTE_DE_ENVIO.ToString), "block", "none")%>">
                            <asp:ImageButton runat="server" ID="ibEnviar" AlternateText="Enviar Orden" ToolTip="Enviar Orden"
                                CommandArgument='<%# String.Format("{0}¬{1}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_UNION_ORDEN_PREORDEN.ID_UBICACION), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_UNION_ORDEN_PREORDEN.ID_PRE_ORDEN_TRABAJO))%>'
                                ImageUrl='<%#AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Flecha_Derecha.png")%>'
                                onmouseover='<%#String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Flecha_Derecha.png"))%>'
                                onmouseout='<%#String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Flecha_Derecha.png"))%>'
                                OnClick="ibEnviar_Click" />
                        </article>
                    </td>
                    <td>
                        <asp:ImageButton ID="imgRecibidoConformeSolicitante" runat="server" AlternateText="Recibido Conforme Solicitante" ToolTip="Recibido Conforme Solicitante" data-tipo="consultarRegistro"
                            Visible="<%#IIf(Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_UNION_ORDEN_PREORDEN.ESTADO_ORDEN_TRABAJO).ToString = Utilerias.OrdenesDeTrabajo.EstadoOrden.RECIBIDO_CONFORME_SOLICITANTE.ToString, True, False)%>"
                            CommandArgument='<%# String.Format("{0}¬{1}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_UNION_ORDEN_PREORDEN.ID_UBICACION), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_UNION_ORDEN_PREORDEN.ID_ORDEN_TRABAJO))%>'
                            ImageUrl='<%#AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Lista_Requisitos.png")%>'
                            onmouseover='<%#String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Icono_Lista_Requisitos.png"))%>'
                            onmouseout='<%#String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Lista_Requisitos.png"))%>'
                            OnClick="ibRecibidoConformeSolicitante_Click" />
                    </td>
                    <td>
                        <article style="display: <%#IIf((Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_UNION_ORDEN_PREORDEN.ESTADO_ORDEN_TRABAJO).ToString = Utilerias.OrdenesDeTrabajo.EstadoOrden.DEVUELTA.ToString) Or (Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_UNION_ORDEN_PREORDEN.ESTADO_ORDEN_TRABAJO).ToString = Utilerias.OrdenesDeTrabajo.EstadoOrden.PENDIENTE_DE_ENVIO.ToString), "block", "none")%>">
                            <asp:ImageButton ID="imgFichaTecnica" runat="server" AlternateText="Ficha Técnica" ToolTip="Ficha Técnica" data-tipo="fichaTecnica"
                                Visible='<%#IIf((Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_UNION_ORDEN_PREORDEN.CATEG_REQUIERE_FICHA_TECNICA).ToString = "1") And (Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_UNION_ORDEN_PREORDEN.POSEE_FICHA_TECNICA).ToString = "1"), True, False)%>'
                                CommandArgument='<%# String.Format("{0}¬{1}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_UNION_ORDEN_PREORDEN.ID_UBICACION), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_UNION_ORDEN_PREORDEN.ID_PRE_ORDEN_TRABAJO))%>'
                                ImageUrl='<%#AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Ver_Detalle.png")%>'
                                onmouseover='<%#String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Icono_Ver_Detalle.png"))%>'
                                onmouseout='<%#String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Ver_Detalle.png"))%>'
                                OnClick="ibFichaTecnica_Click" />
                        </article>

                        <article style="display: <%#IIf((Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_UNION_ORDEN_PREORDEN.ESTADO_ORDEN_TRABAJO).ToString = Utilerias.OrdenesDeTrabajo.EstadoOrden.DEVUELTA.ToString) Or (Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_UNION_ORDEN_PREORDEN.ESTADO_ORDEN_TRABAJO).ToString = Utilerias.OrdenesDeTrabajo.EstadoOrden.PENDIENTE_DE_ENVIO.ToString), "block", "none")%>">
                            <asp:ImageButton ID="imgFichaAgregar" runat="server" AlternateText="Ficha Técnica" ToolTip="Ficha Técnica" data-tipo="fichaTecnica"
                                Visible='<%#IIf((Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_UNION_ORDEN_PREORDEN.CATEG_REQUIERE_FICHA_TECNICA).ToString = "1") And (Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_UNION_ORDEN_PREORDEN.POSEE_FICHA_TECNICA).ToString = "0"), True, False)%>'
                                CommandArgument='<%# String.Format("{0}¬{1}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_UNION_ORDEN_PREORDEN.ID_UBICACION), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_UNION_ORDEN_PREORDEN.ID_PRE_ORDEN_TRABAJO))%>'
                                ImageUrl='<%#AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Ver_Detalle.png")%>'
                                onmouseover='<%#String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Icono_Ver_Detalle.png"))%>'
                                onmouseout='<%#String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Ver_Detalle.png"))%>'
                                OnClick="ibFichaTecnicaAgregar_Click" />
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
        <wuc:PaginadorNumerico runat="server" ID="pnRpOrdenTrabajo" />
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
        }

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

            //mostrarAreaDeListado();
            //ocultarAreaFiltrosDeBusqueda();
        }

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
        }

        function mostrarAlertaActualizacionExitosa() {

            mostrarAlerta(
                '#arAlerta',
                {
                    mensaje: 'Se ha actualizado la información de la orden.',
                    tipo: "exito",
                    transparencia: 0.9,
                    posicion: 'center'
                });

        };


        function mostrarPopupConfirmaDeseaBorrarRegistro(pvo_UniqueIdControl) {
            var vlo_ConfiguracionPopup = {
                titulo: 'Catálogo de Ordenes de Trabajo',
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
        }

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

        $(document).ready(function () {
            aplicarConfiguracionBasicaListado({ alternarVisibilidadFiltroLista: true });

            $('[data-tipo="borrarRegistro"]').click(function () { return mostrarPopupConfirmaDeseaBorrarRegistro($(this).data("uniqueid")); });

            $('[data-tipo="tooltip"]').each(function () {
                //habilitarTooltipPorControl('#' + this.id);
                $('#' + this.id).tooltipster({
                    interactive: true,
                    interactiveTolerance: 400,
                    theme: 'tooltipster-light'
                });
            });

        });

    </script>
</asp:Content>

