<%@ Page Language="VB" MasterPageFile="~/MasterPage/Mp_Listado.master" AutoEventWireup="false" CodeFile="Lst_OT_Rubros.aspx.vb" Inherits="Catalogos_Lst_OT_Rubros" %>

<%@ Register TagPrefix="wuc" Namespace="Utilerias.WebForms.Controles" Assembly="Utilerias.WebForms.Controles" %>

<asp:Content runat="server" ID="Content1" ContentPlaceHolderID="cphFormulario">

    <header>
        <h2>
            Catálogo de Rubros para Descisión Inicial
        </h2>
    </header>

    <article data-grupo="FiltrosDeBusqueda" class="tituloSeccion">
        Criterios de Búsqueda
    </article>
    <article data-grupo="FiltrosDeBusqueda" class="formulario">
        <article class="areaBotonesListado">
            <a href="Frm_OT_Rubros.aspx?pvn_Operacion=<%=Utilerias.OrdenesDeTrabajo.eOperacion.Agregar%>">
                <img alt="Registrar nuevos Rubros para descisión inicial" data-tipo="nuevoRegistro" src="" />
            </a>
        </article>
        <table>
            <tr>
                <th>Descripción</th>
                <td>
                    <asp:TextBox Width="40%" runat="server" ID="txtFiltroDescripcion" data-tipocontrol="texto"></asp:TextBox>
                    <br />
                    <span id="spContadorTxtDescripcion" class="contadorCaracteresRestantes"></span>
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
        <input type="button" data-tipo="cancelarBusqueda" value="Cancelar" id="btnCancelarBusqueda" />
    </article>

    <article data-grupo="Listado" class="tituloSeccion">
        Listado de Rubros para Descisión inicial
    </article>
    
    <article class="listado" data-grupo="Listado">
        <article class="areaBotonesListado">
            <a href="Frm_OT_Rubros.aspx?pvn_Operacion=<%=Utilerias.OrdenesDeTrabajo.eOperacion.Agregar%>&pvn_IdOrdenamiento=<%=CStr(Me.UltimoOrden)%>">
                <img alt="Registrar nuevos Rubros" data-tipo="nuevoRegistro" src="" />
            </a>
            <img alt="Mostrar criterios de búsqueda" data-tipo="mostrarFiltros" src="" id="imgMostrarFiltros" />
            
        </article>

        <asp:Repeater runat="server" ID="rpRubros">
            <HeaderTemplate>
                <table>
                    <tr>
                        <th>
                            <asp:LinkButton runat="server" text ="Descripcion" ID="Descripcion" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_RUBRO_DECISION_INICIALST.DESCRIPCION%>" CommandArgument="ASC" OnCommand="lnkRPRubros_Command"></asp:LinkButton>
                        </th>

                        <th>
                            <asp:LinkButton runat="server" text ="Estado" ID="Estado" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_RUBRO_DECISION_INICIALST.ESTADO%>" CommandArgument="ASC" OnCommand="lnkRPRubros_Command"></asp:LinkButton>
                        </th>
                        <th>&nbsp;</th>
                        <th>&nbsp;</th>
                        <th>&nbsp;</th>
                        <th>&nbsp;</th>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td><%# Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_RUBRO_DECISION_INICIALST.DESCRIPCION)%></td>
                    <td><%# Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_RUBRO_DECISION_INICIALST.DESC_ESTADO)%></td>
                    <td>
                        <a href="Frm_OT_Rubros.aspx?pvn_Operacion=<%#Utilerias.OrdenesDeTrabajo.eOperacion.Modificar%>&pvc_IdRubro=<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_RUBRO_DECISION_INICIALST.ID_RUBRO_DECISION_INICIA)%>&pvn_IdOrdenamiento=<%#CStr(Me.UltimoOrden)%>">
                            <img alt="Modificar datos del Rubro" data-tipo="modificarRegistro" src="" />
                        </a>
                    </td>
                    <td>
                        <asp:ImageButton runat="server" ID="ibBorrar" AlternateText="Borrar el Subcomponente" data-tipo="borrarRegistro" 
                            CommandArgument='<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_RUBRO_DECISION_INICIALST.ID_RUBRO_DECISION_INICIA)%>' 
                            OnClick="ibBorrar_Click"/>
                    </td>
                    <td style="width: 12px;">
                        <article style="display: <%#IIf(Me.FiltroConsulta, "none", "block")%>">
                        <asp:ImageButton runat="server" OnClick="imgSubirNuevo_Click" id="imgSubirNuevo" class="subirItem" 
                            CommandArgument='<%#String.Format("{0},{1}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_RUBRO_DECISION_INICIALST.ID_RUBRO_DECISION_INICIA), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_RUBRO_DECISION_INICIALST.ORDEN))%>'  
                            src='<%#AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Flecha_Arriba.png")%>' 
                            onmouseover='<%#String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Flecha_Arriba.png"))%>' 
                            onmouseout='<%#String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Flecha_Arriba.png"))%>' />
                        </article>
                    </td>
                    <td style="width: 12px;">
                        <article style="display: <%#IIf(Me.FiltroConsulta, "none", "block")%>">
                        <asp:ImageButton runat="server" OnClick="imgBajarNuevo_Click" id="imgBajarNuevo" class="bajarItem" 
                            CommandArgument='<%#String.Format("{0},{1}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_RUBRO_DECISION_INICIALST.ID_RUBRO_DECISION_INICIA), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_RUBRO_DECISION_INICIALST.ORDEN))%>' 
                            src='<%#AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Flecha_Abajo.png")%>' 
                            onmouseover='<%#String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Flecha_Abajo.png"))%>' 
                            onmouseout='<%#String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Flecha_Abajo.png"))%>' />
                        </article>
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
    </article>

    <article class="areaPaginadorListado" data-grupo="Listado">
        <wuc:PaginadorNumerico runat="server" ID="pnRpRubros" />
    </article>

    <article class="areaCantidadDeRegistro" data-grupo="Listado">
        <asp:Label runat="server" ID="lblCantidadDeRegistros" Text=""></asp:Label>
    </article>

    <article id="arAlerta">
    </article>

    <article id="popupConfirmacionDeseaBorrar">
    </article>

    <script type="text/javascript" >

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
                    mensaje: "Se ha borrado el Rubro",
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
                    mensaje: "No ha sido posible borrar el rubro seleccionado",
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
                    mensaje: "No se cuenta con rubros que cumplan con la(s) condicion(es) indicada(s)",
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
                titulo: "<em>Catalogo de Rubros para Descisión Inicial</em>",
                mensaje: "¿Realmente desea borrar el rubro seleccionado?",
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