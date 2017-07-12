<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/Mp_Listado.master" AutoEventWireup="false" CodeFile="Lst_OT_IngresoMaterialAlmacen.aspx.vb" Inherits="OrdenesDeTrabajo_Almacen_Lst_OT_IngresoMaterialAlmacen" %>

<%@ Register TagPrefix="wuc" Namespace="Utilerias.WebForms.Controles" Assembly="Utilerias.WebForms.Controles" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphFormulario" Runat="Server">
    <header>
        <h2>Ingreso de Material a Almacén</h2>
    </header>

    <article class="tituloSeccion">
        Vía de Compra
    </article>

    <article data-grupo="Formulario" class="Listado">
        <table>
            <tr>
                <th style="width: 15%">Vía de Compra</th>
                <td style="width: 65%">
                    <asp:DropDownList runat="server" ID="ddlViaCompra" AppendDataBoundItems="true" Width="87%" AutoPostBack="true"></asp:DropDownList>
                </td>
            </tr>
        </table>
    </article>
    <br />
    <article data-grupo="FiltrosDeBusqueda" class="formulario sinBorde">
        <article class="tituloSeccion">
            Condición de búsqueda
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
                <th>Número de OT</th>
                <td>
                    <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtFiltroNumeroOt" ValidChars="1234567890-"></ajax:FilteredTextBoxExtender>
                    <asp:TextBox Width="40%" runat="server" ID="txtFiltroNumeroOt" data-tipocontrol="texto"></asp:TextBox>
                </td>
            </tr>
        </table>
    </article>

    <article data-grupo="FiltrosDeBusqueda" class="areaBotones">
        <asp:Button runat="server" ID="btnBuscar" Text="Buscar" />
        <input type="button" data-tipo="limpiarFiltros" value="Limpiar Filtros" />
        <input id="btnCancelarBusqueda" type="button" data-tipo="cancelarBusqueda" value="Cancelar" />
    </article>

    <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>--%>
    <article data-grupo="Listado" class="listado sinBorde">


        <article class="tituloSeccion">
            Listado de Gestiones de Material
        </article>

        <article id="Article1" runat="server">
            <asp:Repeater runat="server" ID="RpSuministros">
                <HeaderTemplate>
                    <table>
                        <tr>
                            <th style="width: 30%;">
                                <asp:LinkButton runat="server" ID="lnkNumeroGestion" Text="N° de Ajuste" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GESTION_COMPRALST.NUMERO_GESTION_COMPRA%>" CommandArgument="ASC" OnCommand="lnkRpSuministros_Command"></asp:LinkButton>
                            </th>
                            <th style="width: 20%;">
                                <asp:LinkButton runat="server" ID="lnkObservaciones" Text="Observaciones" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GESTION_COMPRALST.OBSERVACIONES%>" CommandArgument="ASC" OnCommand="lnkRpSuministros_Command"></asp:LinkButton>
                            </th>
                            <th style="width: 20%;">
                                <asp:LinkButton runat="server" ID="lnkFecha" Text="Fecha" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GESTION_COMPRALST.FECHA_REGISTRO_SOLICITUD%>" CommandArgument="ASC" OnCommand="lnkRpSuministros_Command"></asp:LinkButton>
                            </th>
                            <th style="width: 30%;">
                                <asp:LinkButton runat="server" ID="lnkEstado" Text="Estado" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GESTION_COMPRALST.DESC_ESTADO%>" CommandArgument="ASC" OnCommand="lnkRpSuministros_Command"></asp:LinkButton>
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
                            <%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GESTION_COMPRALST.DESC_ESTADO)%>
                        </td>
                        <td>
                            <asp:ImageButton runat="server" class="tooltip" ToolTip="Registrar" ID="ibConsultarGestion" AlternateText="Registrar" OnClick="ibConsultarGestion_Click" CommandArgument='<%#String.Format("{0}%{1}%{2}%{3}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GESTION_COMPRALST.ID_UBICACION), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GESTION_COMPRALST.ANNO), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GESTION_COMPRALST.ID_VIA_COMPRA_CONTRATO), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GESTION_COMPRALST.NUMERO_GESTION))%>' src='<%#AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Check.png")%>' onmouseover='<%# String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Icono_Check.png"))%>' onmouseout='<%# String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Check.png"))%>' />
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
    </article>
    <%--</ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ddlTipoAjuste" EventName="SelectedIndexChanged" />
        </Triggers>
    </asp:UpdatePanel>--%>

    <article id="arAlerta"></article>
    <article id="arPopupGenerico"></article>

    <script type="text/javascript">

        $(document).ready(function () {
            aplicarConfiguracionBasicaListado({ alternarVisibilidadFiltroLista: true });

            inicializarScript();
        });


        function inicializarScript() {
            habilitarTooltipGenerico();
            configurarDatePicker("#<%=Me.txtFiltroFechaInicio.ClientID%>");
            configurarDatePicker("#<%=Me.txtFiltroFechaFin.ClientID%>");

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

