<%@ Page title="Listado de Ordenes de trabajo Rechazadas" MasterPageFile="~/MasterPage/Mp_Listado.master" Language="VB" AutoEventWireup="false" CodeFile="Lst_OT_Rechazadas.aspx.vb" Inherits="Catalogos_Lst_OT_Rechazadas" %>

<%@ Register TagPrefix="wuc" Namespace="Utilerias.WebForms.Controles" Assembly="Utilerias.WebForms.Controles" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:content runat="server" ContentPlaceHolderID="cphFormulario">

    <header>
        <h2>Ordenes de Trabajo Rechazadas</h2>
    </header>

    <article data-grupo="FiltrosDeBusqueda" class="tituloSeccion">
        Criterios de busqueda
    </article>
    <article data-grupo="FiltrosDeBusqueda" class="formulario">
        
        <table>
            <tr>
                <th>Número de órden de trabajo</th>
                <td>
                    <asp:TextBox runat="server" ID="txtFiltroNumeroOrden" data-tipocontrol="texto"></asp:TextBox>
                    <ajax:FilteredTextBoxExtender ID="ftbtxtNumOrden" runat="server" TargetControlID="txtFiltroNumeroOrden" FilterMode="ValidChars" ValidChars="-" FilterType="Numbers"></ajax:FilteredTextBoxExtender>
                    <br />
                </td>
            </tr>
            <tr>
                <th>Número de órden de trabajo en PDAGO</th>
                <td>
                    <asp:TextBox runat="server" ID="txtFiltroPDAGO" AppendDataBoundItems="true" data-tipocontrol="texto"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>Edificio</th>
                <td>
                    <asp:TextBox runat="server" ID="txtFiltroEdificio" AppendDataBoundItems="true" data-tipocontrol="texto"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>Motivo de Rechazo</th>
                <td>
                    <asp:TextBox runat="server" ID="txtFiltroMotivo" AppendDataBoundItems="true" data-tipocontrol="texto"></asp:TextBox>
                </td>
            </tr>
        </table>
    </article>
    <article data-grupo="FiltrosDeBusqueda" class="areaBotones">
        <asp:Button runat="server" ID="btnBuscar" Text="Buscar" />
        <input type="button" data-tipo="limpiarFiltros" value="Limpiar Filtros" />
        <input type="button" data-tipo="cancelarBusqueda" value="Cancelar" id="btnCancelarBusqueda" />
    </article>

    <article data-grupo="Listado" class="tituloSeccion">
        Listado de Ordenes de trabajo
    </article>

     <article data-grupo="Listado" class="listado">
        <article class="areaBotonesListado">
            <img alt="Mostrar criterios de búsqueda" data-tipo="mostrarFiltros" src="" id="imgMostrarFiltros" />
        </article>

         <asp:Repeater ID="rpRechazadas" runat="server">
             <HeaderTemplate>
                <table>
                    <tr>
                        <th style="width:15%">
                            <asp:LinkButton runat="server" text ="No. OT" ID="lnkNumeroOrden" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OT_RECHAZADAS.ID_OT%>" CommandArgument="ASC" OnCommand="lnkRpOrdenTrabajo_Command"></asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton runat="server" text ="PDAGO" ID="lnkPDAGO" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OT_RECHAZADAS.PDAGO%>" CommandArgument="ASC" OnCommand="lnkRpOrdenTrabajo_Command"></asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton runat="server" text ="Edificio" ID="lnkEdificio" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OT_RECHAZADAS.EDIFICIO%>" CommandArgument="ASC" OnCommand="lnkRpOrdenTrabajo_Command"></asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton runat="server" text ="Motivo de Rechazo" ID="lnkRechazo" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OT_RECHAZADAS.MOTIVO_DESCRIPCION%>" CommandArgument="ASC" OnCommand="lnkRpOrdenTrabajo_Command"></asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton runat="server" text ="Fecha Asignada" ID="lnkFecha" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OT_RECHAZADAS.FECHA%>" CommandArgument="ASC" OnCommand="lnkRpOrdenTrabajo_Command"></asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton runat="server" text ="Descripcion de trabajo" ID="lnkDescripcion" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OT_RECHAZADAS.DESCRIPCION%>" CommandArgument="ASC" OnCommand="lnkRpOrdenTrabajo_Command"></asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton runat="server" text ="Dias en espera" ID="lnkDias" CommandArgument="ASC" OnCommand="lnkRpOrdenTrabajo_Command"></asp:LinkButton>
                        </th>
                        
                        <%--<th>&nbsp;</th>--%>
                        <th>&nbsp;</th>
                        <%--<th>&nbsp;</th>--%>
                    </tr>
            </HeaderTemplate>

             <ItemTemplate>
                <tr class="lineaDelListado">
                    <td style="width:15%"><%# Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_RECHAZADAS.ID_OT)%></td>
                    <td><%# Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_RECHAZADAS.PDAGO)%></td>
                    <td><%# Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_RECHAZADAS.EDIFICIO)%></td>
                    <td><%# Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_RECHAZADAS.MOTIVO_DESCRIPCION)%></td>
                    <td><%# Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_RECHAZADAS.FECHA)%></td>
                    <td style="align-content:center">
                        <img title='<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_RECHAZADAS.DESCRIPCION)%>' class="tooltip tooltipstered" src='<%# AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR , "Icono_Ayuda.png")%>' />
                    </td>
                    <td><asp:label runat="server" ID="lnkDiasEnEspera" Text="<%# Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_RECHAZADAS.DIASHABILES) %>"></asp:label></td>
                   <%-- <td>
                        <a href="Frm_OT_VbDenegadaRechazada.aspx?pvc_IdOrdenTrabajo=<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_RECHAZADAS.ID_OT)%>&pvc_IdUbicacion=<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_RECHAZADAS.ID_UBICACION)%>&pvc_OP=R&pvc_NumEmpleado=<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_RECHAZADAS.NUM_EMPLEADO)%>&pvc_NumEmpleadoCoordinador=<%# Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_RECHAZADAS.NUM_EMPLEADO_COORDINADOR)%>" >
                            <img title="Reasignar órden de trabajo" alt="Reasignar" src='<%#AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Refrescar.png")%>' />
                        </a>
                    </td>--%>
                    <td><asp:ImageButton ToolTip="Revisión de Rechazo" runat="server" ImageUrl='<%#AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Check.png")%>' AlternateText="Visto Bueno" ID="imgbtnVbueno" CommandArgument='<%# String.Format("{0},{1},{2}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_RECHAZADAS.ID_OT), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_RECHAZADAS.NUM_EMPLEADO), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_RECHAZADAS.ID_UBICACION))%>' OnClick="imgbtnVbueno_Click" /> </td>
                    <%--<td><asp:ImageButton ToolTip="Denegación al motivo de rechazo" runat="server" ImageUrl='<%#AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Borrar_Detalle.png")%>' AlternateText="Denegación" ID="imgbtnDenegacion" CommandArgument='<%# String.Format("{0},{1},{2}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_RECHAZADAS.ID_OT), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_RECHAZADAS.NUM_EMPLEADO_COORDINADOR), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_RECHAZADAS.ID_UBICACION))%>' OnClick="imgbtnDenegacion_Click" /></td>--%>
                    
                </tr>
            </ItemTemplate>

             <FooterTemplate>
                 
                </table>
            </FooterTemplate>
        </asp:Repeater>
    </article>
    
    <article data-grupo="Listado" class="areaPaginadorListado">
        <wuc:PaginadorNumerico runat="server" ID="pnOT_Rechazadas" />
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
</asp:content>