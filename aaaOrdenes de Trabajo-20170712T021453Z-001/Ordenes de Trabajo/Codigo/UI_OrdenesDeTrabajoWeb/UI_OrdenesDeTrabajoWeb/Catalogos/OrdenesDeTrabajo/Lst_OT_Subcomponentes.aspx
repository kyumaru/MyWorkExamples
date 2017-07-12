<%@ Page Title="Listado de Subcomponentes por espacio" Language="VB" MasterPageFile="~/MasterPage/Mp_Listado.master" AutoEventWireup="false" CodeFile="Lst_OT_Subcomponentes.aspx.vb" Inherits="Catalogos_LST_OT_Subcomponentes" %>

<%@ Register TagPrefix="wuc" Namespace="Utilerias.WebForms.Controles" Assembly="Utilerias.WebForms.Controles" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphFormulario">

    <header>
        <h2><asp:Literal runat="server" id="NombreEspacio" EnableViewState="false" /></h2>
    </header>

    <article data-grupo="FiltrosDeBusqueda" class="tituloSeccion">
        Criterios de busqueda
    </article>
    <article data-grupo="FiltrosDeBusqueda" class="formulario">
        <article class="areaBotonesListado">
             <a href="Lst_OT_Espacios.aspx">
                <img alt="Volver al Listado" data-tipo="volverListado" src='<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x32, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Flecha_Izquierda.png")%>' />
            </a>
            <a href="Frm_OT_Subcomponente.aspx?pvn_Operacion=" id="agregarSubcomp" runat="server">
                <img alt="Registrar nuevos Subcomponentes" data-tipo="nuevoRegistro" src="" />
            </a>
        </article>
        <table>
            <tr>
                <th>Descripción</th>
                <td>
                    <asp:TextBox Width="60%" runat="server" ID="txtFiltroDescripcion" data-tipocontrol="texto"></asp:TextBox>
                    <br />
                    <span id="spContadorTxtDescripcion" class="contadorCaracteresRestantes"></span>
                </td>
            </tr>
            <tr>
                <th>Estado</th>
                <td>
                    <asp:DropDownList Width="60%" runat="server" ID="ddlFiltroEstado" AppendDataBoundItems="true" data-tipocontrol="combo"></asp:DropDownList>
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
        Listado de Subcomponentes por Espacio
    </article>

    <article data-grupo="Listado" class="listado">
        <article class="areaBotonesListado">
            <a href="Lst_OT_Espacios.aspx">
                <img alt="Volver al Listado" data-tipo="volverListado" src='<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x32, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Flecha_Izquierda.png")%>' />
            </a>
            <a href="Frm_OT_Subcomponente.aspx?pvn_Operacion=" id="agregarSub" runat="server">
                <img alt="Registrar nuevos Subcomponentes" data-tipo="nuevoRegistro" src="" />
            </a>
            <img alt="Mostrar criterios de búsqueda" data-tipo="mostrarFiltros" src="" id="imgMostrarFiltros" />
            
        </article>


        <asp:Repeater ID="rpSubcomponentes" runat="server">

            <HeaderTemplate>
                <table>
                    <tr>
                        <th>
                            <asp:LinkButton runat="server" text ="Descripcion" ID="Descripcion" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_SUBCOMPONENTELST.DESCRIPCION%>" CommandArgument="ASC" OnCommand="lnkRPSubcomponentes_Command"></asp:LinkButton>
                        </th>

                        <th>
                            <asp:LinkButton runat="server" text ="Estado" ID="Estado" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_SUBCOMPONENTELST.ESTADO %>" CommandArgument="ASC" OnCommand="lnkRPSubcomponentes_Command"></asp:LinkButton>
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
                    <td><%# Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_SUBCOMPONENTELST.DESCRIPCION)%></td>
                    <td><%# Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_SUBCOMPONENTELST.DESC_ESTADO)%></td>
                    <td>
                        <a href="Frm_OT_Subcomponente.aspx?pvn_Operacion=<%#Utilerias.OrdenesDeTrabajo.eOperacion.Modificar%>&pvc_IdSubcomponente=<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_SUBCOMPONENTELST.ID_SUBCOMPONENTE)%>">
                            <img alt="Modificar datos del Subcomponente" data-tipo="modificarRegistro" src="" />
                        </a>
                    </td>
                    <td>
                        <asp:ImageButton runat="server" ID="ibBorrar" AlternateText="Borrar el Subcomponente" data-tipo="borrarRegistro" 
                            CommandArgument='<%#String.Format("{0},{1}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_SUBCOMPONENTELST.ID_ESPACIO), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_SUBCOMPONENTELST.ID_SUBCOMPONENTE))%>' 
                            OnClick="ibBorrar_Click"/>
                    </td>
                    <td style="width: 12px;">
                        <asp:ImageButton runat="server" OnClick="imgSubirNuevo_Click" id="imgSubirNuevo" class="subirItem" 
                            CommandArgument='<%#String.Format("{0},{1},{2}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_SUBCOMPONENTELST.ID_SUBCOMPONENTE), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_SUBCOMPONENTELST.ORDEN), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_SUBCOMPONENTELST.ID_ESPACIO))%>'  
                            src='<%#AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Flecha_Arriba.png")%>' 
                            onmouseover='<%#String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Flecha_Arriba.png"))%>' 
                            onmouseout='<%#String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Flecha_Arriba.png"))%>' />
                    </td>
                    <td style="width: 12px;">
                        <asp:ImageButton runat="server" OnClick="imgBajarNuevo_Click" id="imgBajarNuevo" class="bajarItem" 
                            CommandArgument='<%#String.Format("{0},{1},{2}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_SUBCOMPONENTELST.ID_SUBCOMPONENTE), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_SUBCOMPONENTELST.ORDEN),Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_SUBCOMPONENTELST.ID_ESPACIO))%>' 
                            src='<%#AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Flecha_Abajo.png")%>' 
                            onmouseover='<%#String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Flecha_Abajo.png"))%>' 
                            onmouseout='<%#String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Flecha_Abajo.png"))%>' />
                    </td>
                </tr>
            </ItemTemplate>

            <FooterTemplate>
                </table>
            </FooterTemplate>

        </asp:Repeater>

    </article>

    <article class="areaPaginadorListado" data-grupo="Listado">
        <wuc:PaginadorNumerico runat="server" ID="pnRpSubcomponentes" />
    </article>

    <article class="areaCantidadDeRegistro" data-grupo="Listado">
        <asp:Label runat="server" ID="lblCantidadDeRegistros" Text=""></asp:Label>
    </article>

    <article id="arAlerta">
    </article>

    <article id="popupConfirmacionDeseaBorrar">
    </article>

    <script type="text/javascript">
        function mostrarAlertaError(pvc_Msj) {
            mostrarAlerta(
                '#arAlerta',
                {
                    mensaje: pvc_Msj,
                    tipo: "peligro",
                    transparencia: 0.9,
                    posicion: 'center'
                }
            );
        }

        function mostrarAlertaRegistroBorrado() {
            mostrarAlerta(
                '#arAlerta',
                {
                    mensaje: "Se ha borrado el subcomponente",
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
                    mensaje: "No ha sido posible borrar el subcomponente seleccionado",
                    tipo: "advertencia",
                    transparencia: 0.9,
                    posicion: 'center'
                }
            );
        }

        function mostrarAlertaNoHayDatos() {
            mostrarAlerta(
                '#arAlerta',
                {
                    mensaje: "No se cuenta con subcomponentes que cumplan con la(s) condicion(es) indicada(s)",
                    tipo: "advertencia",
                    transparencia: 0.9,
                    posicion: 'center'
                }
            );

            deshabilitarControl('#btnCancelarBusqueda');
            ocultarAreaDeListado();
            mostrarAreaFiltrosDeBusqueda();
        }

        function mostrarPopupConfirmacionDeseaBorrarRegistro(pvc_UniqueIdControl) {
            var vlo_ConfiguracionPopup = {
                titulo: "<em>Catalogo de subcomponentes</em>",
                mensaje: "¿Realmente desea borrar el subcomponente seleccionado?",
                botones:
                    [
                        {
                            idControl: "btnSi",
                            textoBoton: "Sí",
                            onClick:
                                function (e) {
                                    $(this).attr("disabled", "disabled");
                                    __doPostBack(pvc_UniqueIdControl, '');
                                }
                        },
                    {
                        idControl: "btnNo",
                        textoBoton: "No",
                        onClick:
                            function (e) { cerrarPopup(); }
                    },
                    {
                        idControl: "btnCancelar",
                        textoBoton: "Cancelar",
                        onClick:
                            function (e) { cerrarPopup(); }
                    }

                    ]
            };

            $('#popupConfirmacionDeseaBorrar').popup(vlo_ConfiguracionPopup);
            window.location = "#popupConfirmacionDeseaBorrar";

            return false;
        }

        function configurarVisibilidadBotonSubirBajar(pvc_IdTabla) {
            var vlc_SelectorTabla;

            $('.subirItem').each(function () { $(this).show(); });
            $('.bajarItem').each(function () { $(this).show(); });

            vlc_SelectorTabla = '#' + pvc_IdTabla + ' tr:first-child';
            $(vlc_SelectorTabla).next().find('td').first().find('img').hide();

            vlc_SelectorTabla = '#' + pvc_IdTabla + ' tr:last-child';
            $(vlc_SelectorTabla).find('td').first().next().find('img').hide();
        }
        
        $(document).ready(function () {
           
            $('[data-tipo="volverListado"]').on({
                'mouseover': function () { $(this).attr('src', '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x32, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Flecha_Izquierda.png")%>'); },
                'mouseout': function () { $(this).attr('src', '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x32, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Flecha_Izquierda.png")%>'); }
            });

            aplicarConfiguracionBasicaListado({ alternarVisibilidadFiltroLista: true });
            $('[data-tipo="borrarRegistro"]').click(function () { return mostrarPopupConfirmacionDeseaBorrarRegistro($(this).data("uniqueid")); });


            $('.subirItem').click(function () {
                var vlc_IdTabla = $(this).closest('table').id;
                var vlo_FilaSeleccionada = $(this).closest('tr');
                var vlo_FilaAnterior = vlo_FilaSeleccionada.prev();
                var vln_ValorPosicionFilaSeleccionada = vlo_FilaSeleccionada.find('td').eq(3).find('span').first().text();
                var vln_ValorPosicionFilaAnterior = vlo_FilaAnterior.find('td').eq(3).find('span').first().text();

                vlo_FilaSeleccionada.find('td').eq(3).find('span').first().text(vln_ValorPosicionFilaAnterior);
                vlo_FilaSeleccionada.find('td').eq(3).find('input').first().val(vln_ValorPosicionFilaAnterior);

                vlo_FilaAnterior.find('td').eq(3).find('span').first().text(vln_ValorPosicionFilaSeleccionada);
                vlo_FilaAnterior.find('td').eq(3).find('input').first().val(vln_ValorPosicionFilaSeleccionada);

                vlo_FilaSeleccionada.prev().insertAfter(vlo_FilaSeleccionada);
                vlo_FilaSeleccionada.fadeOut();
                vlo_FilaSeleccionada.fadeIn();

                configurarVisibilidadBotonSubirBajar(vlc_IdTabla);
            });

            $('.bajarItem').click(function () {
                var vlc_IdTabla = $(this).closest('table').id;
                var vlo_FilaSeleccionada = $(this).closest('tr');
                var vlo_FilaSiguiente = vlo_FilaSeleccionada.next();
                var vln_ValorPosicionFilaSeleccionada = vlo_FilaSeleccionada.find('td').eq(3).find('span').first().text();
                var vln_ValorPosicionFilaSiguiente = vlo_FilaSiguiente.find('td').eq(3).find('span').first().text();

                vlo_FilaSeleccionada.find('td').eq(3).find('span').first().text(vln_ValorPosicionFilaSiguiente);
                vlo_FilaSeleccionada.find('td').eq(3).find('input').first().val(vln_ValorPosicionFilaSiguiente);

                vlo_FilaSiguiente.find('td').eq(3).find('span').first().text(vln_ValorPosicionFilaSeleccionada);
                vlo_FilaSiguiente.find('td').eq(3).find('input').first().val(vln_ValorPosicionFilaSeleccionada);

                vlo_FilaSeleccionada.next().insertBefore(vlo_FilaSeleccionada);
                vlo_FilaSeleccionada.fadeOut();
                vlo_FilaSeleccionada.fadeIn();

                configurarVisibilidadBotonSubirBajar(vlc_IdTabla);
            });


        });

    </script>


</asp:Content>