<%@ Page Language="VB" Title="Gestión de OT's por profesionales de diseño" MasterPageFile="~/MasterPage/Mp_Listado.master" AutoEventWireup="false" CodeFile="Lst_OT_GestionProfesionalesDisenio.aspx.vb" Inherits="OrdenesDeTrabajo_Lst_OT_GestionProfesionalesDisenio" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register TagPrefix="wuc" Namespace="Utilerias.WebForms.Controles" Assembly="Utilerias.WebForms.Controles" %>

<asp:Content runat="server" ID="Content1" ContentPlaceHolderID="cphFormulario">

    <header>
        <h2>Gestión de Ordenes de Trabajo por Profesionales de diseño</h2>
    </header>

    <article data-grupo="FiltrosDeBusqueda" class="tituloSeccion">
        Criterios de búsqueda
    </article>

    <article data-grupo="FiltrosDeBusqueda" class="formulario">
        <table>
            <tr>
                <th>Número de Orden</th>
                <td>
                    <asp:TextBox Width="59%" runat="server" ID="txtFiltroNumOrden" data-tipocontrol="texto" TextMode="MultiLine" Rows="1"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>Nombre del proyecto</th>
                <td>
                    <asp:TextBox Width="59%" runat="server" ID="txtFiltroNombreProyecto" data-tipocontrol="texto" TextMode="MultiLine" Rows="1"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>Fecha</th>
                <td>
                    <asp:TextBox Width="59%" runat="server" ID="txtFiltroFecha" data-tipocontrol="texto" TextMode="MultiLine" Rows="1"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>Estado</th>
                <td>
                    <asp:DropDownList Width="59%" runat="server" ID="ddlfiltroEstado" data-tipocontrol="combo" AppendDataBoundItems="true"></asp:DropDownList>
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
        Listado de Ordenes de Trabajo por profesionales de diseño
    </article>

    <article class="listado sinBorde" data-grupo="Listado">
        <article class="areaBotonesListado">
            <img id="imgMostrarFiltros" alt="Mostrar criterios de Búsqueda" title="Mostrar criterios de Búsqueda" data-tipo="mostrarFiltros" src="" />
        </article>
        <asp:Repeater runat="server" ID="rpOrdenTrabajo">
            <HeaderTemplate>
                <table>
                    <tr>
                        <th>
                            <asp:LinkButton runat="server" ID="lnkNumOt" Text="No. OT" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ORDEN_TRABAJOLST.ID_ORDEN_TRABAJO%>" CommandArgument="ASC" OnCommand="lnkRpOrdenTrabajo_Command"></asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton runat="server" ID="lnkNombreProyecto" Text="Nombre Del Proyecto" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ORDEN_TRABAJOLST.NOMBRE_PROYECTO%>" OnCommand="lnkRpOrdenTrabajo_Command"></asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton runat="server" ID="lnkFecha" Text="Fecha" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ORDEN_TRABAJOLST.FECHA_HORA_SOLICITA%>" OnCommand="lnkRpOrdenTrabajo_Command"></asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton runat="server" ID="lnkEstado" Text="Estado" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ORDEN_TRABAJOLST.ESTADO_ORDEN_TRABAJO%>" OnCommand="lnkRpOrdenTrabajo_Command"></asp:LinkButton>
                        </th>
                        <th>&nbsp;</th>
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
                <tr>
                    <td>
                        <asp:LinkButton runat="server" ID="lnkNumOt" Text="<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ORDEN_TRABAJOLST.ID_ORDEN_TRABAJO)%>"
                            CommandArgument='<%# String.Format("{0}¬{1}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ORDEN_TRABAJOLST.ID_UBICACION), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ORDEN_TRABAJOLST.ID_ORDEN_TRABAJO))%>'
                            OnClick="lnkNumOt_Click"></asp:LinkButton>
                    </td>
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ORDEN_TRABAJOLST.NOMBRE_PROYECTO)%></td>
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ORDEN_TRABAJOLST.FECHA_HORA_SOLICITA)%></td>
                    <td>
                        <asp:LinkButton runat="server" ID="lnkTrazabilidad" Style="text-decoration: underline; color: blue;"
                            Text="<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ORDEN_TRABAJOLST.DESC_ESTADO_ORDEN)%>"
                            CommandArgument='<%# String.Format("{0}¬{1}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ORDEN_TRABAJOLST.ID_UBICACION), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ORDEN_TRABAJOLST.ID_ORDEN_TRABAJO))%>'
                            OnCommand="lnkTrazabilidad_Command"></asp:LinkButton>
                    </td>
                    <td>
                        <asp:ImageButton runat="server" ID="ibImprimir" AlternateText="Imprimir Orden" ToolTip="Imprimir Orden"
                            CommandArgument='<%# String.Format("{0}_{1}_{2}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_GESTION_SECTOR_TALLER.ID_UBICACION), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_GESTION_SECTOR_TALLER.ID_ORDEN_TRABAJO), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_GESTION_SECTOR_TALLER.ESTADO_ORDEN_TRABAJO))%>'
                            ImageUrl='<%#AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Impresora.png")%>'
                            onmouseover='<%#String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Impresora.png"))%>'
                            onmouseout='<%#String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Impresora.png"))%>'
                            OnClick="ibImprimir_Click" />
                    </td>
                    <td>
                        <asp:ImageButton runat="server" ID="btnRH" AlternateText="Asignación de profesionales acompañantes para Viabilidad Técnica" ToolTip="Asignación de profesionales acompañantes para Viabilidad Técnica"
                            CommandArgument='<%# String.Format("{0}¬{1}¬{2}¬{3}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ORDEN_TRABAJOLST.ID_UBICACION), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ORDEN_TRABAJOLST.ID_ORDEN_TRABAJO), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ORDEN_TRABAJOLST.ANNO), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ORDEN_TRABAJOLST.ID_SECTOR_TALLER))%>'
                            ImageUrl='<%#AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Usuarios.png")%>'
                            onmouseover='<%#String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Icono_Usuarios.png"))%>'
                            onmouseout='<%#String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Usuarios.png"))%>'
                            Visible="<%#IIf(Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ORDEN_TRABAJOLST.ESTADO_ORDEN_TRABAJO).ToString = Utilerias.OrdenesDeTrabajo.EstadoOrden.EVALUACION_PRELIMINAR_PENDIENTE, True, False)%>"
                            OnClick="btnRH_Click" />
                    </td>
                    <td>
                        <asp:ImageButton runat="server" ID="btnVT" AlternateText="Registro de análisis de Viabilidad Técnica por profesional" ToolTip="Registro de análisis de Viabilidad Técnica por profesional"
                            CommandArgument='<%# String.Format("{0}¬{1}¬{2}¬{3}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ORDEN_TRABAJOLST.ID_UBICACION), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ORDEN_TRABAJOLST.ID_ORDEN_TRABAJO), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ORDEN_TRABAJOLST.ANNO), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ORDEN_TRABAJOLST.ID_SECTOR_TALLER))%>'
                            ImageUrl='<%#AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Lista_Tareas.png")%>'
                            onmouseover='<%#String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Icono_Lista_Tareas.png"))%>'
                            onmouseout='<%#String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Lista_Tareas.png"))%>'
                            Visible="<%#IIf((Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ORDEN_TRABAJOLST.ESTADO_ORDEN_TRABAJO).ToString = Utilerias.OrdenesDeTrabajo.EstadoOrden.EVALUACION_PRELIMINAR_EVALUACION) OR  (Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ORDEN_TRABAJOLST.ESTADO_ORDEN_TRABAJO).ToString = Utilerias.OrdenesDeTrabajo.EstadoOrden.EVALUACION_PRELIMINAR_DEVUELTA_COORD), True, False)%>"
                            OnClick="btnVT_Click" />
                    </td>
                    <td>
                        <asp:ImageButton runat="server" ID="btnAnteproyecto" AlternateText="Elaboración de Anteproyecto" ToolTip="Elaboración de Anteproyecto"
                            CommandArgument='<%# String.Format("{0}¬{1}¬{2}¬{3}¬{4}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ORDEN_TRABAJOLST.ID_UBICACION), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ORDEN_TRABAJOLST.ID_ORDEN_TRABAJO), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ORDEN_TRABAJOLST.ID_SECTOR_TALLER), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ORDEN_TRABAJOLST.ANNO), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ORDEN_TRABAJOLST.ESTADO_ORDEN_TRABAJO))%>'
                            ImageUrl='<%#AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Lista_Requisitos.png")%>'
                            onmouseover='<%#String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Icono_Lista_Requisitos.png"))%>'
                            onmouseout='<%#String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Lista_Requisitos.png"))%>'
                            OnClick="btnAnteproyecto_Click" />
                    </td>
                    <td>
                        <asp:ImageButton runat="server" ID="imgOrdenHija"
                            CommandArgument='<%# String.Format("{0}¬{1}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ORDEN_TRABAJOLST.ID_UBICACION), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ORDEN_TRABAJOLST.ID_ORDEN_TRABAJO))%>'
                            ImageUrl='<%#AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Agregar_Detalle.png")%>'
                            onmouseover='<%#String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Icono_Agregar_Detalle.png"))%>'
                            onmouseout='<%#String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Agregar_Detalle.png"))%>'
                            OnClick="ibOrdenHija_Click" />

                        <asp:HiddenField runat="server" ID="hdfIdUbicacion" Value="<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ORDEN_TRABAJOLST.ID_UBICACION)%>" />
                        <asp:HiddenField runat="server" ID="hdfIdOrdenTrabajo" Value="<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ORDEN_TRABAJOLST.ID_ORDEN_TRABAJO)%>" />
                    </td>
                    <td>
                        <asp:ImageButton runat="server" ID="imgPlanos" AlternateText="Elaboración de Planos" ToolTip="Elaboración de Planos"
                            Visible="<%#IIf(Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ORDEN_TRABAJOLST.ESTADO_ORDEN_TRABAJO).ToString = Utilerias.OrdenesDeTrabajo.EstadoOrden.ELABORACION_DE_PLANOS, True, False)%>"
                            CommandArgument='<%# String.Format("{0}¬{1}¬{2}¬{3}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ORDEN_TRABAJOLST.ID_UBICACION), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ORDEN_TRABAJOLST.ID_ORDEN_TRABAJO), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ORDEN_TRABAJOLST.ID_SECTOR_TALLER), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ORDEN_TRABAJOLST.ANNO))%>'
                            ImageUrl='<%#AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Temario.png")%>'
                            onmouseover='<%#String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Icono_Temario.png"))%>'
                            onmouseout='<%#String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Temario.png"))%>'
                            OnClick="imgPlanos_Click" />
                    </td>
                    <td>
                        <asp:ImageButton runat="server" ID="imgExpendiente" AlternateText="Expediente" ToolTip="Expendiente"
                            CommandArgument='<%# String.Format("{0}¬{1}¬{2}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ORDEN_TRABAJOLST.ID_UBICACION), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ORDEN_TRABAJOLST.ID_ORDEN_TRABAJO),Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ORDEN_TRABAJOLST.ANNO))%>'
                            ImageUrl='<%#AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Carpetas.png")%>'
                            onmouseover='<%#String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Icono_Carpetas.png"))%>'
                            onmouseout='<%#String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Carpetas.png"))%>'
                            OnClick="ibExpediente_Click" />
                    </td>
                    <td>
                        <asp:ImageButton runat="server" ID="imgbtnEntrega" AlternateText="Finalización y entrega de Diseño" ToolTip="Finalización y entrega de Diseño"
                            Visible="<%#IIf(Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ORDEN_TRABAJOLST.ESTADO_ORDEN_TRABAJO).ToString = Utilerias.OrdenesDeTrabajo.EstadoOrden.GESTION_CONTRATACION, True, False)%>"
                            CommandArgument='<%# String.Format("{0}¬{1}¬{2}¬{3}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ORDEN_TRABAJOLST.ID_UBICACION), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ORDEN_TRABAJOLST.ID_ORDEN_TRABAJO), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ORDEN_TRABAJOLST.ID_SECTOR_TALLER), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ORDEN_TRABAJOLST.ANNO))%>'
                            ImageUrl='<%#AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Ver_Detalle.png")%>'
                            onmouseover='<%#String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Icono_Ver_Detalle.png"))%>'
                            onmouseout='<%#String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Ver_Detalle.png"))%>'
                            OnClick="imgbtnEntrega_Click" />
                    </td>
                    <td>
                        <asp:ImageButton runat="server" ID="imgElaboracionPresupuesto" AlternateText="Elaboración de Presupuesto" ToolTip="Elaboración de Presupuesto"
                            Visible="<%#IIf((Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ORDEN_TRABAJOLST.ESTADO_ORDEN_TRABAJO).ToString = Utilerias.OrdenesDeTrabajo.EstadoOrden.ELABORACION_PRESUPUESTO) OR  (Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ORDEN_TRABAJOLST.ESTADO_ORDEN_TRABAJO).ToString = Utilerias.OrdenesDeTrabajo.EstadoOrden.PRESUPUESTO_DEVUELTO_COORDINADOR), True, False)%>"
                            CommandArgument='<%# String.Format("{0}¬{1}¬{2}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ORDEN_TRABAJOLST.ID_UBICACION), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ORDEN_TRABAJOLST.ID_ORDEN_TRABAJO), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ORDEN_TRABAJOLST.ANNO))%>'
                            ImageUrl='<%#AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Recibir_Dinero.png")%>'
                            onmouseover='<%#String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Icono_Recibir_Dinero.png"))%>'
                            onmouseout='<%#String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Recibir_Dinero.png"))%>'
                            OnClick="imgElaboracionPresupuesto_Click" />
                    </td>
                    <td>
                        <asp:ImageButton runat="server" data-tipo="gestionarCont" ID="btnGestionProfesional" AlternateText="Gestion de la contratación" ToolTip="Gestion de la contratación"
                            CommandArgument='<%# String.Format("{0}¬{1}¬{2}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ORDEN_TRABAJOLST.ID_UBICACION), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ORDEN_TRABAJOLST.ID_ORDEN_TRABAJO), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ORDEN_TRABAJOLST.ANNO))%>'
                            Visible="<%#IIf(Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ORDEN_TRABAJOLST.ESTADO_ORDEN_TRABAJO).ToString = Utilerias.OrdenesDeTrabajo.EstadoOrden.CONTRATACION_VISITA_TECNICA Or
                                             Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ORDEN_TRABAJOLST.ESTADO_ORDEN_TRABAJO).ToString = Utilerias.OrdenesDeTrabajo.EstadoOrden.CONTRATACION_ACLARACIONES Or
                                             Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ORDEN_TRABAJOLST.ESTADO_ORDEN_TRABAJO).ToString = Utilerias.OrdenesDeTrabajo.EstadoOrden.CONTRATACION_OFERTAS, True, False)%>"
                            OnClick="btnGestionProfesional_Click"
                            ImageUrl='<%#AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Agregar_Detalle.png")%>'
                            onmouseover='<%# String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Icono_Agregar_Detalle.png"))%>'
                            onmouseout='<%# String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Agregar_Detalle.png"))%>' />
                    </td>
                    <td>
                        <asp:HiddenField runat="server" ID="hdfEstado" Value="<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ORDEN_TRABAJO.ESTADO_ORDEN_TRABAJO)%>" />
                        <asp:HiddenField runat="server" ID="hdfParentesco" Value="<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ORDEN_TRABAJO.PARENTESCO)%>" />
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

        function regresarAlListado() {
            window.location = 'Lst_OT_GestionOrdenTrabajoCordinadorSectorTaller.aspx';
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

            configurarDatePicker('#<%=Me.txtFiltroFecha.ClientID%>');

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
