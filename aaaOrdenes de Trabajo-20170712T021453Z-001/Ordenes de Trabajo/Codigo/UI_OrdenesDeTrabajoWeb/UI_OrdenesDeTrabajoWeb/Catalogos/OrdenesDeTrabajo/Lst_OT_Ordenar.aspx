<%@ Page Title="Ordenamiento" Language="VB" AutoEventWireup="false" MasterPageFile="~/MasterPage/Mp_Listado.master" CodeFile="Lst_OT_Ordenar.aspx.vb" Inherits="Catalogos_Frm_OT_Ordenar" %>

<%@ Register TagPrefix="wuc" Namespace="Utilerias.WebForms.Controles" Assembly="Utilerias.WebForms.Controles" %>

<asp:Content runat="server" ContentPlaceHolderID="cphFormulario" ID="Content1" >
    <header>
        <h2>Mantenimiento de Requerimientos</h2> 
    </header>

    <article data-grupo="FiltrosDeBusqueda" class="tituloSeccion">
        <asp:Label runat="server" ID="lblNivel"></asp:Label>
    </article>

    <article data-grupo="Listado" class="listado">
        <asp:Repeater ID="rpRequerimiento" runat="server">
            <HeaderTemplate>
                <table>
                    <tr>
                        <th>
                            <asp:LinkButton runat="server" text ="Descripción" ID="Descripcion" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_REQUERIMIENTOLST.DESCRIPCION%>" ></asp:LinkButton>
                        </th>
                        <th>&nbsp;</th>
                        <th>&nbsp;</th>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr class="lineaDelListado">
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_REQUERIMIENTOLST.DESCRIPCION)%></td>
                    <td style="width: 12px;">
                        <asp:ImageButton runat="server" CommandArgument='<%#String.Format("{0},{1},{2}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_REQUERIMIENTOLST.ID_REQUERIMIENTO), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_REQUERIMIENTOLST.NIVEL), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_REQUERIMIENTOLST.ORDEN))%>'  OnClick="imgSubirNuevo_Click" id="imgSubirNuevo" class="subirItem" src='<%#AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Flecha_Arriba.png")%>' onmouseover='<%#String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Flecha_Arriba.png"))%>' onmouseout='<%#String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Flecha_Arriba.png"))%>' />
                    </td>
                    <td style="width: 12px;">
                        <asp:ImageButton runat="server" CommandArgument='<%#String.Format("{0},{1},{2}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_REQUERIMIENTOLST.ID_REQUERIMIENTO), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_REQUERIMIENTOLST.NIVEL), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_REQUERIMIENTOLST.ORDEN))%>' OnClick="imgBajarNuevo_Click" id="imgBajarNuevo" class="bajarItem" src='<%#AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Flecha_Abajo.png")%>' onmouseover='<%#String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Flecha_Abajo.png"))%>' onmouseout='<%#String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Flecha_Abajo.png"))%>' />
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
                <article class="areaBotones">
                <asp:Button runat="server" Text="Regresar al Listado" id="btnRegresar" OnClick="btnRegresar_Click" />
                    </article>
            </FooterTemplate>
        </asp:Repeater>
    </article>

    <article class="areaCantidadDeRegistros" data-grupo="Listado">
        <asp:Label runat="server" ID="lblCantidadDeRegistros" Text=""></asp:Label>
    </article>

    <article id="arAlerta">
    </article>

    <script type="text/javascript">
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