<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/Mp_Listado.master" AutoEventWireup="false" CodeFile="Lst_OT_Aprovisionamiento.aspx.vb" Inherits="OrdenesDeTrabajo_Almacen_Lst_OT_Aprovisionamiento" %>

<%@ Register TagPrefix="wuc" Namespace="Utilerias.WebForms.Controles" Assembly="Utilerias.WebForms.Controles" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphFormulario" Runat="Server">
    <header>
        <h2>Aprovisionamiento</h2>
    </header>

    <article data-grupo="FiltrosDeBusqueda" class="tituloSeccion">
        Criterios de búsqueda
    </article>

    <article data-grupo="FiltrosDeBusqueda" class="formulario">

        <article class="areaBotonesListado">
            <asp:ImageButton runat="server" ID="lnk_NuevoRegistro" class="tooltip" ToolTip="Agregar nuevo registro" AlternateText="Agregar nuevo registro" data-tipo="nuevoRegistro" OnClick="ibAgregar_Click" />
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
                <th>Vía de compra</th>
                <td>
                    <asp:DropDownList Width="40%" runat="server" ID="ddlFiltroViaCompra" AppendDataBoundItems="true" data-tipocontrol="combo"></asp:DropDownList>
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

    <article data-grupo="Listado" class="tituloSeccion">
        Aprovisionamiento
    </article>

    <article data-grupo="Listado" class="listado">

        <article data-grupo="Listado" class="areaBotones">
            <asp:ImageButton runat="server" ID="lnkNuevoRegistro" class="tooltip" ToolTip="Agregar nuevo registro" AlternateText="Agregar nuevo registro" data-tipo="nuevoRegistro" OnClick="ibAgregar_Click" />
            <img id="imgMostrarFiltros" alt="Mostrar criterios de Búsqueda" title="Mostrar criterios de Búsqueda" data-tipo="mostrarFiltros" src="" />
        </article>

        <asp:Repeater runat="server" ID="rpAprovisionamiento">
            <HeaderTemplate>
                <table>
                    <tr>
                        <th style="width: 30%;">
                            <asp:LinkButton runat="server" ID="lnkNumeroGestion" Text="N° de Gestión" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_APROVISIONAMIENTOLST.NUMERO_GESTION%>" CommandArgument="ASC" OnCommand="lnkRpAprovisionamiento_Command"></asp:LinkButton>
                        </th>
                        <th style="width: 30%;">
                            <asp:LinkButton runat="server" ID="lnkViaCompra" Text="Vía de Compra" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_APROVISIONAMIENTOLST.DESC_VIA_COMPRA%>" CommandArgument="ASC" OnCommand="lnkRpAprovisionamiento_Command"></asp:LinkButton>
                        </th>
                        <th style="width: 20%;">
                            <asp:LinkButton runat="server" ID="lnkObservaciones" Text="Observaciones" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_APROVISIONAMIENTOLST.OBSERVACIONES%>" CommandArgument="ASC" OnCommand="lnkRpAprovisionamiento_Command"></asp:LinkButton>
                        </th>
                        <th style="width: 20%;">
                            <asp:LinkButton runat="server" ID="lnkFecha" Text="Fecha" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_APROVISIONAMIENTOLST.FECHA_REGISTRO_SOLICITUD%>" CommandArgument="ASC" OnCommand="lnkRpAprovisionamiento_Command"></asp:LinkButton>
                        </th>
                        <th style="width: 30%;">
                            <asp:LinkButton runat="server" ID="lnkEstado" Text="Estado" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_APROVISIONAMIENTOLST.ESTADO_DESC%>" CommandArgument="ASC" OnCommand="lnkRpAprovisionamiento_Command"></asp:LinkButton>
                        </th>
                        <th>&nbsp;</th>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr class="lineaDelListado">
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_APROVISIONAMIENTOLST.NUMERO_GESTION)%></td>
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_APROVISIONAMIENTOLST.DESC_VIA_COMPRA)%></td>
                    <td style="width: 20%; text-align: center;">
                        <span style='<%#String.Format("display:{0};", IIf(Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_APROVISIONAMIENTOLST.OBSERVACIONES).ToString = "" Or Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_APROVISIONAMIENTOLST.OBSERVACIONES).ToString = "-", "none", "block"))%>'>
                            <img id="ImgObservacion" class="tooltip" data-tipo="ObservacionRegistro" scr="" title="<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_APROVISIONAMIENTOLST.OBSERVACIONES)%>" />
                        </span>
                    </td>
                    <td style="width: 20%;"><%#CType(Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_APROVISIONAMIENTOLST.FECHA_REGISTRO_SOLICITUD), DateTime).ToString(Utilerias.OrdenesDeTrabajo.Constantes.FORMATO_FECHA_UI)%></td>
                    
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_APROVISIONAMIENTOLST.ESTADO_DESC)%></td>
                    <td>
                        <asp:ImageButton runat="server" class="tooltip" ToolTip="Consultar Gestión" ID="ibConsultarGestion" AlternateText="Consultar Gestión" 
                            OnClick="ibConsultarGestion_Click" CommandArgument='<%#String.Format("{0}%{1}%{2}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_APROVISIONAMIENTOLST.ID_UBICACION), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GESTION_COMPRALST.ANNO), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GESTION_COMPRALST.NUMERO_GESTION))%>' 
                            src='<%#AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Check.png")%>' 
                            onmouseover='<%# String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Icono_Check.png"))%>' 
                            onmouseout='<%# String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Check.png"))%>' 
                            Visible="<%#IIf(CType(Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_APROVISIONAMIENTOLST.ESTADO), String) = Utilerias.OrdenesDeTrabajo.EstadoAprovisionamiento.CREADO, True, False)%>"/>
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
    </article>

    <article data-grupo="Listado" class="areaPaginadorListado">
        <wuc:PaginadorNumerico runat="server" ID="pnrpAprovisionamiento" />
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

            configurarDatePickerRango("#<%=Me.txtFiltroFechaInicio.ClientID%>", "#<%=Me.txtFiltroFechaFin.ClientID()%>");

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

