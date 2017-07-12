<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/Mp_Listado.master" AutoEventWireup="false" CodeFile="Lst_OT_AprobacionTrasladoMaterialesABodega.aspx.vb" Inherits="OrdenesDeTrabajo_Almacen_Lst_OT_AprobacionTrasladoMaterialesABodega" %>

<%@ Register TagPrefix="wuc" Namespace="Utilerias.WebForms.Controles" Assembly="Utilerias.WebForms.Controles" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphFormulario" runat="Server">
    <header>
        <h2>Aprobación de Traslado de Material a Bodega</h2>
    </header>
    <article data-grupo="FiltrosDeBusqueda" class="formulario sinBorde">
        <article class="tituloSeccion">
            Condición de búsqueda
        </article>   

        <table>
            <tr>
                <th>Número de Solicitud</th>
                <td>
                    <ajax:FilteredTextBoxExtender ID="ftbtxtFiltroSolicitud" runat="server" TargetControlID="txtFiltroSolicitud" ValidChars="1234567890-"></ajax:FilteredTextBoxExtender>
                    <asp:TextBox Width="40%" runat="server" ID="txtFiltroSolicitud" data-tipocontrol="texto"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>Fecha de Solicitud</th>
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
        </table>
    </article>

    <article data-grupo="FiltrosDeBusqueda" class="areaBotones">
        <asp:Button runat="server" ID="btnBuscar" Text="Buscar" />
        <input type="button" data-tipo="limpiarFiltros" value="Limpiar Filtros" />
        <input id="btnCancelarBusqueda" type="button" data-tipo="cancelarBusqueda" value="Cancelar" />
    </article>


    <article data-grupo="Listado" class="listado sinBorde">

        <article class="areaBotonesListado">           
            <img id="imgMostrarFiltros" alt="Mostrar criterios de Búsqueda" title="Mostrar criterios de Búsqueda" data-tipo="mostrarFiltros" src="" />
        </article>
        <article class="tituloSeccion">
            Solicitudes de Bodega
        </article>
        <asp:Repeater runat="server" ID="rpGestionSolicitud">
            <HeaderTemplate>
                <table>
                    <tr>
                        <th style="width:20%;">
                            <asp:LinkButton runat="server" ID="lnkNumeroSolicitud" Text="N° de Solicitud" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_SOLICITUD_TRASLADOLST.NUMERO_SOLICITUD%>" CommandArgument="ASC" OnCommand="lnkRpProveedores_Command"></asp:LinkButton>
                        </th>
                        <th style="width:30%;">
                            <asp:LinkButton runat="server" ID="lnkNombreBodega" Text="Bodega" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_SOLICITUD_TRASLADOLST.NOMBRE_BODEGA%>" CommandArgument="ASC" OnCommand="lnkRpProveedores_Command"></asp:LinkButton>
                        </th>
                        <th style="width:5%;">
                            <asp:LinkButton runat="server" ID="lnkObservaciones" Text="Observaciones" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_SOLICITUD_TRASLADOLST.OBSERVACIONES%>" CommandArgument="ASC" OnCommand="lnkRpProveedores_Command"></asp:LinkButton>
                        </th>
                        <th style="width:10%;">
                            <asp:LinkButton runat="server" ID="lnkFechaSolicitud" Text="Fecha Solicitud" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_SOLICITUD_TRASLADOLST.FECHA_REGISTRO_SOLICITUD%>" CommandArgument="ASC" OnCommand="lnkRpProveedores_Command"></asp:LinkButton>
                        </th>
                        <th style="width:30%;">
                            <asp:LinkButton runat="server" ID="lnkEstado" Text="Estado" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_SOLICITUD_TRASLADOLST.ESTADO_TRASLADO%>" CommandArgument="ASC" OnCommand="lnkRpProveedores_Command"></asp:LinkButton>
                        </th>
                        <th style="width:5%;">&nbsp;</th>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr class="lineaDelListado"> 
                    <td style="width:20%;"><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_SOLICITUD_TRASLADOLST.NUMERO_SOLICITUD)%></td>
                      <td style="width:30%;"><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_SOLICITUD_TRASLADOLST.NOMBRE_BODEGA)%></td>
                    <td style="width:5%; text-align: center;">
                        <span style='<%#String.Format("display:{0};", IIf(Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_SOLICITUD_TRASLADOLST.OBSERVACIONES).ToString = "" Or Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GESTION_COMPRALST.OBSERVACIONES).ToString = "-", "none", "block"))%>'>
                        <img id="ImgObservacion" class="tooltip" data-tipo="ObservacionRegistro" scr="" title="<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_SOLICITUD_TRASLADOLST.OBSERVACIONES)%>" />
                           </span> </td>
                    <td style="width:10%;"><%#CType(Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_SOLICITUD_TRASLADOLST.FECHA_REGISTRO_SOLICITUD), DateTime).ToString(Utilerias.OrdenesDeTrabajo.Constantes.FORMATO_FECHA_UI)%></td>
                    <td style="width:30%;"><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_SOLICITUD_TRASLADOLST.DESC_ESTADO_TRASLADO)%></td>
                    <td style="width:5%;">

                        <asp:ImageButton runat="server" ID="ibRevision" AlternateText="Gestión de Solicitud" data-tipo="SolicitudRegistro" ToolTip="Gestión de Solicitud"
                            CommandArgument='<%# String.Format("{0}¬{1}¬{2}¬{3}¬{4}¬{5}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_SOLICITUD_TRASLADOLST.ID_UBICACION),
                     Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_SOLICITUD_TRASLADOLST.ID_SOLICITUD_TRASLADO), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_SOLICITUD_TRASLADOLST.ANNO),
                     Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_SOLICITUD_TRASLADOLST.ID_BODEGA), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_SOLICITUD_TRASLADOLST.ID_ALMACEN),
                     Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_SOLICITUD_TRASLADOLST.NOMBRE_BODEGA))%>'
                            OnClick="ibRevisionSolicitud_Click"
                            Visible="<%#IIf(Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_SOLICITUD_TRASLADOLST.ESTADO_TRASLADO).Equals(Utilerias.OrdenesDeTrabajo.EstadoTraslado.PENDIENTE_APROBACION), True, False)%>" />
                    </td>                   
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>

    </article>

    <article data-grupo="Listado" class="areaPaginadorListado">
        <wuc:PaginadorNumerico runat="server" ID="pnRpGesionSolicitud" />
    </article>

    <article data-grupo="Listado" class="areaCantidadDeRegistro">
        <asp:Label runat="server" ID="lblCantidadDeRegistros" Text="" Visible="true"></asp:Label>
    </article>

    <article id="arAlerta"></article>
    <article id="arpopupConfirmaDeseaBorrar"></article>
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

        function mostrarAlertaSinRoleTramitadorOAF() {
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

