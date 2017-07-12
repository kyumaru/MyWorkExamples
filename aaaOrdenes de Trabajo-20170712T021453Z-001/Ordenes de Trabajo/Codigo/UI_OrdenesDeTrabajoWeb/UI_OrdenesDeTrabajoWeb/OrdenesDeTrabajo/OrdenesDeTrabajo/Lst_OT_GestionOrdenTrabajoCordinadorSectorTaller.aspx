<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/Mp_Listado.master" AutoEventWireup="false" CodeFile="Lst_OT_GestionOrdenTrabajoCordinadorSectorTaller.aspx.vb" Inherits="OrdenesDeTrabajo_Lst_OT_GestionOrdenTrabajoCordinadorSectorTaller" %>

<%@ Register TagPrefix="wuc" Namespace="Utilerias.WebForms.Controles" Assembly="Utilerias.WebForms.Controles" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphFormulario" runat="Server">

    <header>
        <h2>Gestión de Ordenes de Trabajo por Coordinador de Sector o Taller</h2>
    </header>

    <article data-grupo="FiltrosDeBusqueda" class="tituloSeccion">
        Criterios de búsqueda
    </article>

    <article data-grupo="FiltrosDeBusqueda" class="formulario">
        <table>
            <tr>
                <th>Número de Orden</th>
                <td>
                    <asp:TextBox runat="server" ID="txtNumOT" data-tipocontrol="texto" Width="59%" MaxLength="10"></asp:TextBox>
                    <ajax:FilteredTextBoxExtender ID="ftbTxtNumOT" runat="server" TargetControlID="txtNumOT" FilterMode="ValidChars" ValidChars="-" FilterType="Numbers, Custom"></ajax:FilteredTextBoxExtender>
                </td>
            </tr>
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
            <tr runat="server" id="trCheck" visible="false">
                <th>Mostrar solamente Ordenes en Evaluación</th>
                <td>
                    <asp:CheckBox runat="server" ID="chkOtEvaluacion" />
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
                    <asp:Label runat="server" ID="lblNombreSectorTaller" data-tipocontrol="etiqueta"></asp:Label>
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
                            <asp:LinkButton runat="server" ID="lnkIdOrdenTrabajo" Text="Número de OT" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OT_GESTION_SECTOR_TALLER.CONSECUTIVO%>" CommandArgument="ASC" OnCommand="lnkRpOrdenTrabajo_Command"></asp:LinkButton>
                        </th>
                        <th></th>
                        <th>
                            <asp:LinkButton runat="server" ID="lnkNumeroOrden" Text="PDAGO" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OT_GESTION_SECTOR_TALLER.NUMERO_ORDEN%>" CommandArgument="ASC" OnCommand="lnkRpOrdenTrabajo_Command"></asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton runat="server" ID="lnkTipoOrden" Text="Tipo Orden" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OT_GESTION_SECTOR_TALLER.DESCRIPCION_TRABAJO%>" CommandArgument="ASC" OnCommand="lnkRpOrdenTrabajo_Command"></asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton runat="server" ID="lnkContacto" Text="Contacto" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OT_GESTION_SECTOR_TALLER.NOMBRE_PERSONA_CONTACTO%>" CommandArgument="ASC" OnCommand="lnkRpOrdenTrabajo_Command"></asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton runat="server" ID="lnkLugarTrabajo" Text="Edificio" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OT_GESTION_SECTOR_TALLER.DESC_LUGAR_TRABAJO%>" CommandArgument="ASC" OnCommand="lnkRpOrdenTrabajo_Command"></asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton runat="server" ID="lnkEstado" Text="Estado" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OT_GESTION_SECTOR_TALLER.DESC_ESTADO_ORDEN%>" CommandArgument="ASC" OnCommand="lnkRpOrdenTrabajo_Command"></asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton runat="server" ID="lnkFechaAsignacion" Text="Fecha Asignación" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OT_GESTION_SECTOR_TALLER.FECHA_ASIGNACION%>" CommandArgument="ASC" OnCommand="lnkRpOrdenTrabajo_Command"></asp:LinkButton>
                        </th>
                        <th>Desc. Trab</th>
                        <th>Desc. Inconform</th>
                        <th>&nbsp;</th>
                        <th>&nbsp;</th>
                        <th>&nbsp;</th>
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
                    <td>
                        <asp:LinkButton runat="server" ID="lnkNumOt"
                            Text="<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_GESTION_SECTOR_TALLER.ID_ORDEN_TRABAJO)%>"
                            CommandArgument='<%# String.Format("{0}¬{1}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_GESTION_SECTOR_TALLER.ID_UBICACION), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_GESTION_SECTOR_TALLER.ID_ORDEN_TRABAJO))%>'
                            OnClick="lnkNumOt_Click"></asp:LinkButton>
                    </td>
                    <td>                        
                        <asp:Image runat="server" ID="Image4" data-tipo="tooltip"
                            Visible='<%#IIf(CType(Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_GESTION_SECTOR_TALLER.HISTORIAL_EN_EVALUACION), String) = "1", True, False)%>'
                            title="<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_GESTION_SECTOR_TALLER.HISTORIAL_OBS_EEV_SUPERV)%>"
                            ImageUrl='<%#AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Advertencia.png")%>' />
                     </td>
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_GESTION_SECTOR_TALLER.NUMERO_ORDEN)%></td>
                    <td style="text-align: center">
                        <asp:Image runat="server" ID="Image2" data-tipo="tooltip"
                            title="<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_GESTION_SECTOR_TALLER.DESC_TIPO_ORDEN)%>"
                            ImageUrl='<%#AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Informacion.png")%>' />
                    </td>
                    <td style="text-align: center">
                        <asp:Image runat="server" ID="Image1" data-tipo="tooltip"
                            title="<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_GESTION_SECTOR_TALLER.NOMBRE_PERSONA_CONTACTO)%>"
                            ImageUrl='<%#AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Informacion.png")%>' />
                    </td>
                    <td style="text-align: center">
                        <asp:Image runat="server" ID="Image3" data-tipo="tooltip"
                            title="<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_GESTION_SECTOR_TALLER.DESC_LUGAR_TRABAJO)%>"
                            ImageUrl='<%#AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Informacion.png")%>' />
                    </td>
                    <td>
                        <asp:LinkButton runat="server" ID="lnkTrazabilidad" Style="text-decoration: underline; color: blue;"
                            Text="<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_GESTION_SECTOR_TALLER.DESC_ESTADO_ORDEN)%>"
                            CommandArgument='<%# String.Format("{0}¬{1}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_GESTION_SECTOR_TALLER.ID_UBICACION), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_GESTION_SECTOR_TALLER.ID_ORDEN_TRABAJO))%>'
                            OnCommand="lnkTrazabilidad_Command"></asp:LinkButton>
                    </td>
                    <td><%#CType(Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_GESTION_SECTOR_TALLER.FECHA_ASIGNACION), DateTime).ToString(Utilerias.OrdenesDeTrabajo.Constantes.FORMATO_FECHA_UI)%></td>
                    <td>
                        <asp:Image runat="server" ID="imgTooltipDesc" data-tipo="tooltip"
                            title="<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_GESTION_SECTOR_TALLER.DESCRIPCION_TRABAJO)%>"
                            ImageUrl='<%#AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Informacion.png")%>' />
                    </td>
                    <td>
                        <article style="display: <%#IIf((CType(Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_GESTION_SECTOR_TALLER.ESTADO_ORDEN_TRABAJO), String) = Utilerias.OrdenesDeTrabajo.EstadoOrden.NO_CONFORME), "block", "none")%>">
                            <asp:ImageButton runat="server" ID="imgTooltipMotivo" data-tipo="tooltip"
                                title="<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_GESTION_SECTOR_TALLER.MOTIVO_NO_CONFORME)%>"
                                ImageUrl='<%#AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Advertencia.png")%>'
                                CommandArgument='<%# String.Format("{0}¬{1}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_GESTION_SECTOR_TALLER.ID_UBICACION), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_GESTION_SECTOR_TALLER.ID_ORDEN_TRABAJO))%>'
                                OnClick="ibTooltipMotivo_Click" />
                        </article>
                    </td>
                    <td>

                        <article style="display: <%#IIf((CType(Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_GESTION_SECTOR_TALLER.ESTADO_ORDEN_TRABAJO), String) = Utilerias.OrdenesDeTrabajo.EstadoOrden.EN_PROCESO), "block", "none")%>">

                            <asp:ImageButton runat="server" ID="ibEnviar" AlternateText="Enviar Para Recibido Conforme del Solicitante" ToolTip="Enviar Para Recibido Conforme del Solicitante" data-tipo="enviarRegistro"
                                CommandArgument='<%# String.Format("{0}¬{1}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_GESTION_SECTOR_TALLER.ID_UBICACION), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_GESTION_SECTOR_TALLER.ID_ORDEN_TRABAJO))%>'
                                ImageUrl='<%#AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Check.png")%>'
                                onmouseover='<%#String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Icono_Check.png"))%>'
                                onmouseout='<%#String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Check.png"))%>'
                                Visible='<%#IIf(CType(Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_GESTION_SECTOR_TALLER.CATEG_REQUIERE_FICHA_TECNICA), String) = "0", True, False)%>'
                                OnClick="ibEnviar_Click" />

                            <asp:ImageButton runat="server" ID="ibEnviarDisenio" AlternateText="Enviar Para Recibido Conforme del Solicitante" ToolTip="Enviar Para Recibido Conforme del Solicitante" data-tipo="enviarRegistroDisenio"
                                CommandArgument='<%# String.Format("{0}¬{1}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_GESTION_SECTOR_TALLER.ID_UBICACION), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_GESTION_SECTOR_TALLER.ID_ORDEN_TRABAJO))%>'
                                ImageUrl='<%#AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Check.png")%>'
                                onmouseover='<%#String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Icono_Check.png"))%>'
                                onmouseout='<%#String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Check.png"))%>'
                                Visible='<%#IIf(CType(Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_GESTION_SECTOR_TALLER.CATEG_REQUIERE_FICHA_TECNICA), String) = "1", True, False)%>'
                                OnClick="ibEnviarDisenio_Click" />

                        </article>
                    </td>
                    <td>
                        <article style="display: <%#IIf((CType(Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_GESTION_SECTOR_TALLER.CATEG_REQUIERE_FICHA_TECNICA), String) = "0"), "block", "none")%>">
                            <asp:ImageButton runat="server" ID="ibImprimir" AlternateText="Imprimir Orden" ToolTip="Imprimir Orden"
                                CommandArgument='<%# String.Format("{0}_{1}_{2}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_GESTION_SECTOR_TALLER.ID_UBICACION), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_GESTION_SECTOR_TALLER.ID_ORDEN_TRABAJO), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_GESTION_SECTOR_TALLER.ESTADO_ORDEN_TRABAJO))%>'
                                ImageUrl='<%#AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Impresora.png")%>'
                                onmouseover='<%#String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Impresora.png"))%>'
                                onmouseout='<%#String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Impresora.png"))%>'
                                Visible='<%#IIf(Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_GESTION_SECTOR_TALLER.ESTADO_ORDEN_TRABAJO).ToString = Utilerias.OrdenesDeTrabajo.EstadoOrden.PARA_IMPRESION Or CType(Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_GESTION_SECTOR_TALLER.PARA_IMPRESION_RECEPCION), Integer) = 1, True, False)%>'
                                OnClick="ibImprimir_Click" />
                        </article>

                        <article style="display: <%#IIf((CType(Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_GESTION_SECTOR_TALLER.CATEG_REQUIERE_FICHA_TECNICA), String) = "1"), "block", "none")%>">
                            <asp:ImageButton runat="server" ID="ImageButton1" AlternateText="Imprimir Orden" ToolTip="Imprimir Orden"
                                CommandArgument='<%# String.Format("{0}_{1}_{2}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_GESTION_SECTOR_TALLER.ID_UBICACION), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_GESTION_SECTOR_TALLER.ID_ORDEN_TRABAJO), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_GESTION_SECTOR_TALLER.ESTADO_ORDEN_TRABAJO))%>'
                                ImageUrl='<%#AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Impresora.png")%>'
                                onmouseover='<%#String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Impresora.png"))%>'
                                onmouseout='<%#String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Impresora.png"))%>'
                                OnClick="ibImprimir_Click" />
                        </article>

                    </td>
                    <td>
                        <article style="display: <%#IIf((CType(Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_GESTION_SECTOR_TALLER.ESTADO_ORDEN_TRABAJO), String) = Utilerias.OrdenesDeTrabajo.EstadoOrden.ASIGNADA) Or (CType(Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_GESTION_SECTOR_TALLER.ESTADO_ORDEN_TRABAJO), String) = Utilerias.OrdenesDeTrabajo.EstadoOrden.EN_EVALUACION), "block", "none")%>">
                            <asp:ImageButton runat="server" ID="imgOrdenRechazada" AlternateText="Rechazar la Orden" ToolTip="Rechazar la Orden"
                                CommandArgument='<%# String.Format("{0}¬{1}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_GESTION_SECTOR_TALLER.ID_UBICACION), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_GESTION_SECTOR_TALLER.ID_ORDEN_TRABAJO))%>'
                                ImageUrl='<%#AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Flecha_Izquierda.png")%>'
                                onmouseover='<%#String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Flecha_Izquierda.png"))%>'
                                onmouseout='<%#String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Flecha_Izquierda.png"))%>'
                                OnClick="ibRechazarOrden_Click" />

                        </article>
                    </td>
                    <td>
                        <article style="display: <%#IIf((CType(Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_GESTION_SECTOR_TALLER.ESTADO_ORDEN_TRABAJO), String) = Utilerias.OrdenesDeTrabajo.EstadoOrden.EN_ESTUDIO), "none", "block")%>">
                            <asp:ImageButton runat="server" ID="imgOrdenHija"
                                Visible='<%#IIf(Not Utilerias.OrdenesDeTrabajo.Modelo.V_OT_GESTION_SECTOR_TALLER.ESTADO_ORDEN_TRABAJO = Utilerias.OrdenesDeTrabajo.EstadoOrden.ASIGNADA AndAlso CType(Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_GESTION_SECTOR_TALLER.PARENTESCO), String) <> "HIJ", True, False)%>'
                                CommandArgument='<%# String.Format("{0}¬{1}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_GESTION_SECTOR_TALLER.ID_UBICACION), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_GESTION_SECTOR_TALLER.ID_ORDEN_TRABAJO))%>'
                                ImageUrl='<%#AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Agregar_Detalle.png")%>'
                                onmouseover='<%#String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Icono_Agregar_Detalle.png"))%>'
                                onmouseout='<%#String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Agregar_Detalle.png"))%>'
                                OnClick="ibOrdenHija_Click" />
                        </article>
                        <asp:HiddenField runat="server" ID="hdfIdUbicacion" Value="<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_GESTION_SECTOR_TALLER.ID_UBICACION)%>" />
                        <asp:HiddenField runat="server" ID="hdfIdOrdenTrabajo" Value="<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_GESTION_SECTOR_TALLER.ID_ORDEN_TRABAJO)%>" />
                    </td>
                    <td>
                        <article style="display: <%#IIf((CType(Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_GESTION_SECTOR_TALLER.ESTADO_ORDEN_TRABAJO), String) = Utilerias.OrdenesDeTrabajo.EstadoOrden.EN_ESTUDIO), "none", "block")%>">
                            <asp:ImageButton runat="server" ID="imgRecursos" AlternateText="Gestión de Recursos para Evaluación y Ejecución de Órdenes de Trabajo" ToolTip="Gestión de Recursos para Evaluación y Ejecución de Órdenes de Trabajo"
                                CommandArgument='<%# String.Format("{0}¬{1}¬{2}¬{3}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_GESTION_SECTOR_TALLER.ID_UBICACION), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_GESTION_SECTOR_TALLER.ID_ORDEN_TRABAJO), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_GESTION_SECTOR_TALLER.ANNO), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_GESTION_SECTOR_TALLER.ID_SECTOR_TALLER))%>'
                                ImageUrl='<%#AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Usuarios.png")%>'
                                onmouseover='<%#String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Icono_Usuarios.png"))%>'
                                onmouseout='<%#String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Usuarios.png"))%>'
                                Visible='<%#IIf(CType(Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_GESTION_SECTOR_TALLER.CATEG_REQUIERE_FICHA_TECNICA), String) = "0", True, False)%>'
                                OnClick="ibEnviarAEvaluacion_Click" />
                        </article>
                    </td>
                    <td>
                        <article style="display: <%#IIf((CType(Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_GESTION_SECTOR_TALLER.ESTADO_ORDEN_TRABAJO), String) = Utilerias.OrdenesDeTrabajo.EstadoOrden.EN_ESTUDIO), "none", "block")%>">
                            <asp:ImageButton runat="server" ID="ibRegistroDatosDisenio" AlternateText="Gestión de Recursos para Evaluación y Ejecución de Órdenes de Trabajo (Diseño)" ToolTip="Gestión de Recursos para Evaluación y Ejecución de Órdenes de Trabajo (Diseño)" data-tipo="registroDatosDisenio"
                                CommandArgument='<%# String.Format("{0}¬{1}¬{2}¬{3}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_GESTION_SECTOR_TALLER.ID_UBICACION), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_GESTION_SECTOR_TALLER.ID_ORDEN_TRABAJO), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_GESTION_SECTOR_TALLER.ANNO), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_GESTION_SECTOR_TALLER.ID_SECTOR_TALLER))%>'
                                ImageUrl='<%#AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Usuarios.png")%>'
                                onmouseover='<%#String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Icono_Usuarios.png"))%>'
                                onmouseout='<%#String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Usuarios.png"))%>'
                                Visible='<%#IIf(CType(Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_GESTION_SECTOR_TALLER.CATEG_REQUIERE_FICHA_TECNICA), String) = "1", True, False)%>'
                                OnClick="ibRegistroDatosDisenio_Click" />
                        </article>
                    </td>
                    <td>
                        <asp:ImageButton runat="server" ID="imgGestionMateriales" AlternateText="Gestión de materiales por coordinador sector o taller" ToolTip="Gestión de materiales por coordinador sector o taller" data-tipo="registroDatosDisenio"
                            CommandArgument='<%# String.Format("{0}¬{1}¬{2}¬{3}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_GESTION_SECTOR_TALLER.ID_UBICACION), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_GESTION_SECTOR_TALLER.ID_ORDEN_TRABAJO), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_GESTION_SECTOR_TALLER.ANNO), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_GESTION_SECTOR_TALLER.ID_SECTOR_TALLER))%>'
                            ImageUrl='<%#AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Ver_Detalle.png")%>'
                            onmouseover='<%#String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Icono_Ver_Detalle.png"))%>'
                            onmouseout='<%#String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Ver_Detalle.png"))%>'
                            Visible='<%#IIf((CType(Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_GESTION_SECTOR_TALLER.ESTADO_ORDEN_TRABAJO), String) = Utilerias.OrdenesDeTrabajo.EstadoOrden.PARA_RETIRO_MATERIAL Or
                                                CType(Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_GESTION_SECTOR_TALLER.ESTADO_ORDEN_TRABAJO), String) = Utilerias.OrdenesDeTrabajo.EstadoOrden.EN_PROCESO Or
                                                CType(Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_GESTION_SECTOR_TALLER.ESTADO_ORDEN_TRABAJO), String) = Utilerias.OrdenesDeTrabajo.EstadoOrden.MATERIAL_PENDIENTE_COMPRA) And
                                               (CType(Me.ParametroUbicacionTerceraEtapa.Valor, Integer) = 1) And
                                               (CType(Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_GESTION_SECTOR_TALLER.CATEG_REQUIERE_FICHA_TECNICA), String) = "0"), True, False)%>'
                            OnClick="imgGestionMateriales_Click" />

                    </td>
                    <td>
                        <asp:HiddenField runat="server" ID="hdFechaAsignacion" Value="<%#CType(Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_GESTION_SECTOR_TALLER.FECHA_ASIGNACION), DateTime).ToString(Utilerias.OrdenesDeTrabajo.Constantes.FORMATO_FECHA_UI)%>" />
                    </td>
                    <td>
                        <asp:HiddenField runat="server" ID="hdfTipoOrden" Value="<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_GESTION_SECTOR_TALLER.TIPO_ORDEN_TRABAJO)%>" />
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

        function mostrarPopupConfirmaDeseaEnviarRegistro(pvo_UniqueIdControl) {
            var vlo_ConfiguracionPopup = {
                titulo: 'Ordenes de Trabajo',
                mensaje: '¿Está seguro de registrar la orden de trabajo como atendida?',
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

            $('[data-tipo="enviarRegistroDisenio"]').click(function () { return mostrarPopupConfirmaDeseaEnviarRegistro($(this).data("uniqueid")); });

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


