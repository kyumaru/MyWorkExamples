<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/Mp_Listado.master" AutoEventWireup="false" CodeFile="Lst_OT_GestiónCompraListado.aspx.vb" Inherits="OrdenesDeTrabajo_Almacen_Lst_OT_GestiónCompraListado" %>

<%@ Register TagPrefix="wuc" Namespace="Utilerias.WebForms.Controles" Assembly="Utilerias.WebForms.Controles" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphFormulario" runat="Server">
    <header>
        <h2>Gestión de Compra de Materiales para Almacén</h2>
    </header>
    <article class="formulario">
        <table>
            <tr>
                <th style="width: 15%">Via de Compra</th>
                <td style="width: 65%">
                    <asp:DropDownList runat="server" ID="ddlViaCompra" AppendDataBoundItems="true" Width="87%" onchange="Buscar();"></asp:DropDownList>
                </td>
            </tr>
        </table>
    </article>
    <br />
    <article data-grupo="FiltrosDeBusqueda" class="formulario sinBorde">
        <article class="tituloSeccion">
            Condición de búsqueda
        </article>

        <article class="areaBotonesListado">
            <%--<a id="lnkNuevoRegistroFiltro" onclick="ibAgregar_Click">
                <img id="imgNuevoRegistro2" data-tipo="nuevoRegistro" title="Agregar nuevo registro" alt="Agregar nuevo registro" src="" />
            </a>--%>
            <asp:ImageButton runat="server" ID="lnkNuevoRegistroFiltro" class="tooltip" ToolTip="Agregar nuevo registro" AlternateText="Agregar nuevo registro" data-tipo="nuevoRegistro" OnClick="ibAgregar_Click" />
        </article>

        <table>
            <tr>
                <th>Número de Gestión</th>
                <td>
                    <ajax:FilteredTextBoxExtender ID="ftbFiltroNumeroGestion" runat="server" TargetControlID="txtFiltroNumeroGestion" ValidChars="1234567890-"></ajax:FilteredTextBoxExtender>
                    <asp:TextBox Width="40%" runat="server" ID="txtFiltroNumeroGestion" data-tipocontrol="texto"></asp:TextBox>
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
            <tr>
                <th>Estado</th>
                <td>
                    <asp:DropDownList Width="40%" runat="server" ID="ddlFiltroEstado" AppendDataBoundItems="true" data-tipocontrol="combo"></asp:DropDownList>
                </td>
            </tr>
        </table>
    </article>

    <article data-grupo="FiltrosDeBusqueda" class="areaBotones">
        <asp:Button runat="server" ID="btnBuscar" Text="Buscar" />
        <input type="button" data-tipo="limpiarFiltros" value="Limpiar Filtros" />
        <input id="btnCancelarBusqueda" type="button" data-tipo="cancelarBusqueda" value="Cancelar" />
    </article>

    <article data-grupo="Listado" class="listado sinBorde">
        <article class="areaBotonesListado">
            <%--<a id="lnkNuevoRegistro" onclick="ibAgregar_Click">
                    <img id="img1" data-tipo="nuevoRegistro" title="Agregar nuevo registro" alt="Agregar nuevo registro" src="" />
                </a>--%>
            <asp:ImageButton runat="server" ID="lnkNuevoRegistro" class="tooltip" ToolTip="Agregar nuevo registro" AlternateText="Agregar nuevo registro" data-tipo="nuevoRegistro" OnClick="ibAgregar_Click" />
            <img id="imgMostrarFiltros" alt="Mostrar criterios de Búsqueda" title="Mostrar criterios de Búsqueda" data-tipo="mostrarFiltros" src="" />
        </article>

        <article class="tituloSeccion">
            Listado de Gestiones de Compra
        </article>

        <article runat="server" id="artFondoTrabajo">
            <asp:Repeater runat="server" ID="rpFondoTrabajo">
                <HeaderTemplate>
                    <table>
                        <tr>
                            <th style="width: 30%;">
                                <asp:LinkButton runat="server" ID="lnkNumeroGestionFT" Text="N° de Gestión" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GESTION_COMPRALST.NUMERO_GESTION_COMPRA%>" CommandArgument="ASC" OnCommand="lnkRpFondoTrabajo_Command"></asp:LinkButton>
                            </th>
                            <th style="width: 20%;">
                                <asp:LinkButton runat="server" ID="lnkObservacionesFT" Text="Observaciones" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GESTION_COMPRALST.OBSERVACIONES%>" CommandArgument="ASC" OnCommand="lnkRpFondoTrabajo_Command"></asp:LinkButton>
                            </th>
                            <th style="width: 20%;">
                                <asp:LinkButton runat="server" ID="lnkFechaFT" Text="Fecha" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GESTION_COMPRALST.FECHA_REGISTRO_SOLICITUD%>" CommandArgument="ASC" OnCommand="lnkRpFondoTrabajo_Command"></asp:LinkButton>
                            </th>
                            <th style="width: 30%;">
                                <asp:LinkButton runat="server" ID="lnkEstadoFT" Text="Estado" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GESTION_COMPRALST.ESTADO%>" CommandArgument="ASC" OnCommand="lnkRpFondoTrabajo_Command"></asp:LinkButton>
                            </th>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr class="lineaDelListado">
                        <td style="width: 30%;"><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GESTION_COMPRALST.NUMERO_GESTION_COMPRA)%></td>
                        <td style="width: 20%; text-align: center;">
                            <span style='<%#String.Format("display:{0};", IIf(Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GESTION_COMPRALST.OBSERVACIONES).ToString = "" Or Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GESTION_COMPRALST.OBSERVACIONES).ToString = "-", "none", "block"))%>'>
                                <img id="ImgObservacion" class="tooltip" data-tipo="ObservacionRegistro" scr="" title="<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GESTION_COMPRALST.OBSERVACIONES)%>" />
                            </span></td>
                        <td style="width: 20%;"><%#CType(Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GESTION_COMPRALST.FECHA_REGISTRO_SOLICITUD), DateTime).ToString(Utilerias.OrdenesDeTrabajo.Constantes.FORMATO_FECHA_UI)%></td>
                        <td style="width: 30%;">
                            <asp:LinkButton runat="server" ID="lnkEstado" Text="<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GESTION_COMPRALST.DESC_ESTADO)%>" CommandArgument='<%#String.Format("{0}%{1}%{2}%{3}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GESTION_COMPRALST.ID_UBICACION), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GESTION_COMPRALST.ID_VIA_COMPRA_CONTRATO), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GESTION_COMPRALST.ANNO), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GESTION_COMPRALST.NUMERO_GESTION))%>' OnCommand="lnkTrazabilidadGestion"></asp:LinkButton>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </table>
                </FooterTemplate>
            </asp:Repeater>

            <article data-grupo="Listado" class="areaPaginadorListado">
                <wuc:PaginadorNumerico runat="server" ID="pnRpFondoTrabajo" />
            </article>

            <article data-grupo="Listado" class="areaCantidadDeRegistro">
                <asp:Label runat="server" ID="lblCantidadDeRegistrosFT" Text="" Visible="true"></asp:Label>
            </article>
        </article>

        <article runat="server" id="artSuministros">
            <asp:Repeater runat="server" ID="RpSuministros">
                <HeaderTemplate>
                    <table>
                        <tr>
                            <th style="width: 30%;">
                                <asp:LinkButton runat="server" ID="lnkNumeroGestionSM" Text="N° de Gestión" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GESTION_COMPRALST.NUMERO_GESTION_COMPRA%>" CommandArgument="ASC" OnCommand="lnkRpSuministros_Command"></asp:LinkButton>
                            </th>
                            <th style="width: 20%;">
                                <asp:LinkButton runat="server" ID="lnkObservacionesSM" Text="Observaciones" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GESTION_COMPRALST.OBSERVACIONES%>" CommandArgument="ASC" OnCommand="lnkRpSuministros_Command"></asp:LinkButton>
                            </th>
                            <th style="width: 20%;">
                                <asp:LinkButton runat="server" ID="lnkFechaSM" Text="Fecha" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GESTION_COMPRALST.FECHA_REGISTRO_SOLICITUD%>" CommandArgument="ASC" OnCommand="lnkRpSuministros_Command"></asp:LinkButton>
                            </th>
                            <th style="width: 30%;">
                                <asp:LinkButton runat="server" ID="lnkEstadoSM" Text="Estado" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GESTION_COMPRALST.ESTADO%>" CommandArgument="ASC" OnCommand="lnkRpSuministros_Command"></asp:LinkButton>
                            </th>
                            <th>&nbsp;</th>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr class="lineaDelListado">
                        <td style="width: 30%;"><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GESTION_COMPRALST.NUMERO_GESTION_COMPRA)%></td>
                        <td style="width: 20%; text-align: center;">
                            <span style='<%#String.Format("display:{0};", IIf(Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GESTION_COMPRALST.OBSERVACIONES).ToString = "" Or Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GESTION_COMPRALST.OBSERVACIONES).ToString = "-", "none", "block"))%>'>
                                <img id="ImgObservacion" class="tooltip" data-tipo="ObservacionRegistro" scr="" title="<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GESTION_COMPRALST.OBSERVACIONES)%>" />
                        </td>
                        <td style="width: 20%;">
                            <%#CType(Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GESTION_COMPRALST.FECHA_REGISTRO_SOLICITUD), DateTime).ToString(Utilerias.OrdenesDeTrabajo.Constantes.FORMATO_FECHA_UI)%></span>
                        </td>
                        <td style="width: 30%;">
                            <asp:LinkButton runat="server" ID="lnkEstadoSM" Text="<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GESTION_COMPRALST.DESC_ESTADO)%>" CommandArgument='<%#String.Format("{0}%{1}%{2}%{3}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GESTION_COMPRALST.ID_UBICACION), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GESTION_COMPRALST.ID_VIA_COMPRA_CONTRATO), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GESTION_COMPRALST.ANNO), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GESTION_COMPRALST.NUMERO_GESTION))%>' OnCommand="lnkTrazabilidadGestion"></asp:LinkButton>
                        </td>
                        <td>
                            <asp:ImageButton runat="server" class="tooltip" ToolTip="Consultar Gestión" ID="ibConsultarGestion" AlternateText="Consultar Gestión" OnClick="ibConsultarGestion_Click" CommandArgument='<%#String.Format("{0}%{1}%{2}%{3}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GESTION_COMPRALST.ID_UBICACION), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GESTION_COMPRALST.ID_VIA_COMPRA_CONTRATO), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GESTION_COMPRALST.ANNO), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GESTION_COMPRALST.NUMERO_GESTION))%>' src='<%#AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Check.png")%>' onmouseover='<%# String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Icono_Check.png"))%>' onmouseout='<%# String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Check.png"))%>' />
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </table>
                </FooterTemplate>
            </asp:Repeater>

            <article data-grupo="Listado" class="areaPaginadorListado">
                <wuc:PaginadorNumerico runat="server" ID="pnRpSuministros" />
            </article>

            <article data-grupo="Listado" class="areaCantidadDeRegistro">
                <asp:Label runat="server" ID="lblCantidadDeRegistrosSM" Text="" Visible="true"></asp:Label>
            </article>
        </article>

        <article runat="server" id="artUnidadDeCompra">
            <asp:Repeater runat="server" ID="RpUnidadCompra">
                <HeaderTemplate>
                    <table>
                        <tr>
                            <th style="width: 30%;">
                                <asp:LinkButton runat="server" ID="lnkNumeroGestion" Text="N° de Gestión" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GESTION_COMPRALST.NUMERO_GESTION_COMPRA%>" CommandArgument="ASC" OnCommand="lnkRpUnidadCompra_Command"></asp:LinkButton>
                            </th>
                            <th style="width: 20%;">
                                <asp:LinkButton runat="server" ID="lnkObservaciones" Text="Observaciones" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GESTION_COMPRALST.OBSERVACIONES%>" CommandArgument="ASC" OnCommand="lnkRpUnidadCompra_Command"></asp:LinkButton>
                            </th>
                            <th style="width: 20%;">
                                <asp:LinkButton runat="server" ID="lnkFecha" Text="Fecha" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GESTION_COMPRALST.FECHA_REGISTRO_SOLICITUD%>" CommandArgument="ASC" OnCommand="lnkRpUnidadCompra_Command"></asp:LinkButton>
                            </th>
                            <th style="width: 30%;">
                                <asp:LinkButton runat="server" ID="lnkEstado" Text="Estado" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GESTION_COMPRALST.ESTADO%>" CommandArgument="ASC" OnCommand="lnkRpUnidadCompra_Command"></asp:LinkButton>
                            </th>
                            <th>&nbsp;</th>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr class="lineaDelListado">
                        <td style="width: 30%;"><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GESTION_COMPRALST.NUMERO_GESTION_COMPRA)%></td>
                        <td style="width: 20%; text-align: center;">
                            <span style='<%#String.Format("display:{0};", IIf(Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GESTION_COMPRALST.OBSERVACIONES).ToString = "" Or Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GESTION_COMPRALST.OBSERVACIONES).ToString = "-", "none", "block"))%>'>
                                <img id="ImgObservacion" class="tooltip" data-tipo="ObservacionRegistro" scr="" title="<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GESTION_COMPRALST.OBSERVACIONES)%>" />
                            </span></td>
                        <td style="width: 20%;"><%#CType(Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GESTION_COMPRALST.FECHA_REGISTRO_SOLICITUD), DateTime).ToString(Utilerias.OrdenesDeTrabajo.Constantes.FORMATO_FECHA_UI)%></td>
                        <td style="width: 30%;"><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GESTION_COMPRALST.DESC_ESTADO)%></td>
                        <td>
                            <article style="display: <%#IIf((CType(Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GESTION_COMPRALST.ESTADO), String) = Utilerias.OrdenesDeTrabajo.EstadoGestionCompra.INGRESO_GESTION_GECO), "block", "none")%>">
                                <asp:ImageButton runat="server" ID="imgIngresoGestionGeco" AlternateText="Ingreso de Gestión en GECO" ToolTip="Ingreso de Gestión en GECO"
                                    CommandArgument='<%# String.Format("{0}¬{1}¬{2}¬{3}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GESTION_COMPRALST.ID_UBICACION), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GESTION_COMPRALST.ID_VIA_COMPRA_CONTRATO), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GESTION_COMPRALST.NUMERO_GESTION), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GESTION_COMPRALST.ANNO))%>'
                                    ImageUrl='<%#AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Lista_Requisitos.png")%>'
                                    onmouseover='<%#String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Icono_Lista_Requisitos.png"))%>'
                                    onmouseout='<%#String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Lista_Requisitos.png"))%>'
                                    OnClick="ibIngresoGestionGeco_Click" />
                            </article>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </table>
                </FooterTemplate>
            </asp:Repeater>

            <article data-grupo="Listado" class="areaPaginadorListado">
                <wuc:PaginadorNumerico runat="server" ID="pnRpUnidadCompra" />
            </article>

            <article data-grupo="Listado" class="areaCantidadDeRegistro">
                <asp:Label runat="server" ID="lblCantidadDeRegistrosUC" Text="" Visible="true"></asp:Label>
            </article>
        </article>
    </article>

    <article id="arAlerta"></article>
    <article id="arPopupGenerico"></article>

    <script type="text/javascript">

        $(document).ready(function () {
            aplicarConfiguracionBasicaListado({ alternarVisibilidadFiltroLista: true });

            inicializarScript();
        });


        function inicializarScript() {
            habilitarTooltipGenerico();
            /*DatePicker con Fecha Máxima (hoy)*/
            configurarDatePicker("#<%=Me.txtFiltroFechaInicio.ClientID%>");
            configurarDatePicker("#<%=Me.txtFiltroFechaFin.ClientID%>");
            //establecerMinyMaxDate()
            //establecerMinyMaxDateFin()
            /*DatePicker con Fecha Máxima (hoy)*/

            $('[data-tipo="SolicitudRegistro"]').attr('src', '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Lupa.png")%>');
            $('[data-tipo="SolicitudRegistro"]').on({
                'mouseover': function () { $(this).attr('src', '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Lupa.png")%>'); },
                'mouseout': function () { $(this).attr('src', '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Lupa.png")%>'); }
            });

            $('[data-tipo="ObservacionRegistro"]').attr('src', '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Informacion.png")%>');
            $('[data-tipo="ObservacionRegistro"]').on({
                'mouseover': function () { $(this).attr('src', '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Icono_Informacion.png")%>'); },
                'mouseout': function () { $(this).attr('src', '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Informacion.png")%>'); }
            });

        }

        //********* Data Picker************
        $("#datepicker").datepicker({
            inline: true
        });

        //function establecerMinyMaxDate() {
        //   if (document.getElementById('<%=txtFiltroFechaInicio.ClientID%>'))
        //       establecerFechaMaximaDatePicker("#<%=Me.txtFiltroFechaInicio.ClientID%>", 'today');
        // }

        //function establecerMinyMaxDateFin() {
        //    if (document.getElementById('<%=txtFiltroFechaFin.ClientID%>'))
        //       establecerFechaMaximaDatePicker("#<%=Me.txtFiltroFechaFin.ClientID%>", 'today');
        // }
        //********* Data Picker************  

        function ValidadorDeCampoRequeridoPersonalizadoFecha(source, clientside_arguments) {
            if ((document.getElementById('<%=txtFiltroFechaInicio.ClientID%>').value != "") && document.getElementById('<%=txtFiltroFechaInicio.ClientID%>').value.trim() == "") {
                document.getElementById('<%=txtFiltroFechaInicio.ClientID%>').style.backgroundColor = "#F5838A"
                return clientside_arguments.IsValid = false;
            }
            else {
                if ((document.getElementById('<%=txtFiltroFechaFin.ClientID%>').value != "") && document.getElementById('<%=txtFiltroFechaFin.ClientID%>').value.trim() == "") {
                    document.getElementById('<%=txtFiltroFechaFin.ClientID%>').style.backgroundColor = "#F5838A"
                    return clientside_arguments.IsValid = false;
                }
                else {
                    document.getElementById('<%=txtFiltroFechaInicio.ClientID%>').style.backgroundColor = "white"
                    document.getElementById('<%=txtFiltroFechaFin.ClientID%>').style.backgroundColor = "white"
                    return clientside_arguments.IsValid = true;
                }
            }
        }
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

            //deshabilitarControl('#btnCancelarBusqueda');
            ocultarAreaDeListado();
            mostrarAreaFiltrosDeBusqueda();

        };


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

        function Buscar() {
            __doPostBack('<%=Me.UniqueIDBotonBuscar%>', '');
        }

        function mostrarAlertaMensajeRedirecionar(pvcMensaje) {
            mostrarAlerta(
                '#arAlerta',
                {
                    mensaje: pvcMensaje,
                    tipo: "advertencia",
                    transparencia: 0.9,
                    posicion: 'top',
                    onClosed: function () { regresarAlListado(); }
                }
            );
        }

        function regresarAlListado() {
            window.location = '../Genericos/Frm_MenuPrincipal.aspx';
        }

        function mostrarAlertaSinRol() {
            mostrarAlerta(
                '#arAlerta',
                {
                    mensaje: 'El usuario conectado no cuenta con los permisos necesarios para el acceso a este formulario.',
                    tipo: "advertencia",
                    transparencia: 0.9,
                    posicion: 'top',
                    onClosed: function () { regresarAlListado(); }
                }
            );
        }
    </script>
</asp:Content>

