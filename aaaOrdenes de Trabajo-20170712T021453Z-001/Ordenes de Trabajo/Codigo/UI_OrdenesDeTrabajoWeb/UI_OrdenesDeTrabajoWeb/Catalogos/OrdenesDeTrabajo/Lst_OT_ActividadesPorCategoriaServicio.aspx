<%@ Page Title="Listado de Actividades por Categoría de Servicio" Language="VB" MasterPageFile="~/MasterPage/Mp_Listado.master" AutoEventWireup="false" CodeFile="Lst_OT_ActividadesPorCategoriaServicio.aspx.vb" Inherits="Catalogos_Lst_OT_ActividadesPorCategoriaServicio" %>

<%@ Register TagPrefix="wuc" Namespace="Utilerias.WebForms.Controles" Assembly="Utilerias.WebForms.Controles" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphFormulario" Runat="Server">
    <header>
        <h2>Catálogo de Actividades por Categoría de Servicio</h2>
    </header>
    
    <article data-grupo="Listado" class="tituloSeccion">
        Listado de Actividades por Categoría de Servicio
    </article>
    <article data-grupo="FiltrosDeBusqueda" class="tituloSeccion">
        <%--'grupo es para yo entender que es--%>
        Criterios de busqueda
    </article>
    <article>
        <table>
            <tr>
                <th>Categoría</th>
                <td>
                    <asp:Label runat="server" ID="lblCategoria"></asp:Label>
                    <br />
                </td>
            </tr>
        </table>
        
    </article>
    
    <article data-grupo="FiltrosDeBusqueda" class="formulario">
        <article class="areaBotonesListado">
             <a href="Lst_OT_CategoriaServicio.aspx">
                <img alt="Volver al Listado" data-tipo="volverListado" src="" />
            </a>
            &nbsp;&nbsp;&nbsp;&nbsp;
            <asp:ImageButton runat="server" ID="ibNuevo" alt="Registrar nueva Actividad por Categoría de Servicio" data-tipo="nuevoRegistro" />
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
                <th>Sector</th>
                <td>
                    <asp:DropDownList Width="40%" runat="server" ID="ddlFiltroSector" AppendDataBoundItems="true" data-tipocontrol="combo"></asp:DropDownList>
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

    
    <article data-grupo="Listado" class="listado">
        <article class="areaBotonesListado">
            <a href="Lst_OT_CategoriaServicio.aspx">
                <img alt="Volver al Listado" data-tipo="volverListado" src="" />
            </a>
            &nbsp;&nbsp;&nbsp;&nbsp;
            <asp:ImageButton runat="server" ID="ibNuevoRegistro" alt="Registrar nueva Actividad por Categoría de Servicio" data-tipo="nuevoRegistro" /> <%--se utiliza boton diferente al usual ya que se debe enviar por parametro una propiedad almacenada en el code behind--%>
          <%-- <a href="Frm_OT_ActividadesPorCategoriaServicio.aspx?pvn_Operacion=<%= Utilerias.OrdenesDeTrabajo.eOperacion.Agregar%>&pvc_IdCategoria=<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_ACTIVIDADLST.ID_CATEGORIA_SERVICIO)%>">
                <img alt="Registrar nueva Actividad por Categoría de Servicio" data-tipo="nuevoRegistro"  />
            </a>--%>               
            <img alt="Mostrar criterios de búsqueda" data-tipo="mostrarFiltros" id="imgMostrarFiltros" />
        </article>
        <asp:Repeater runat="server" ID="rpActividades">
            <HeaderTemplate>
                <table>
                    <tr>
                        <th>
                            <asp:LinkButton runat="server" ID="lnkDescripcion" Text="Descripción" CommandName="<%# Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_ACTIVIDADLST.DESCRIPCION%>" CommandArgument="ASC" OnCommand="lnkRpActividades_Command" ></asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton runat="server" ID="lnkSector" Text="Sector" CommandName="<%# Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_ACTIVIDADLST.ID_SECTOR_TALLER%>" CommandArgument="ASC" OnCommand="lnkRpActividades_Command"></asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton runat="server" ID="lnkEstado" Text="Estado" CommandName="<%# Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_ACTIVIDADLST.ESTADO%>" CommandArgument="ASC" OnCommand="lnkRpActividades_Command"></asp:LinkButton>
                        </th>
                        <th>&nbsp</th>
                        <%--<th>&nbsp</th>--%>
                        <%--'espacio en blanco--%>
                        <th>&nbsp</th>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr class="lineaDelListado">
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_ACTIVIDADLST.DESCRIPCION)%></td>
                    <%--traer datos del source con eval y se lleva un valor--%>
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_ACTIVIDADLST.SECTOR)%></td>
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_ACTIVIDADLST.DESCRIPCION_ESTADO)%></td>
                    <td>
                        <a href="Frm_OT_ActividadesPorCategoriaServicio.aspx?pvn_Operacion=<%# Utilerias.OrdenesDeTrabajo.eOperacion.Modificar%><%#String.Format("&pvc_IdCategoria={0}&pvc_IdActividad={1}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_ACTIVIDADLST.ID_CATEGORIA_SERVICIO), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_ACTIVIDADLST.ID_ACTIVIDAD))%>"> 
                            <img alt="Modificar datos de la Actividad" data-tipo="modificarRegistro" src="" />
                        </a>
                    </td>
                    <td>
                        <asp:ImageButton runat="server" ID="ibBorrar" AlternateText="Borrar la Actividad" data-tipo="borrarRegistro" CommandArgument='<%#String.Format("{0}_{1}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_ACTIVIDADLST.ID_CATEGORIA_SERVICIO), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_ACTIVIDADLST.ID_ACTIVIDAD))%>' OnClick="ibBorrar_Click" />
                   </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
    </article>

    <article data-grupo="Listado" class="areaPaginadorListado">
        <wuc:PaginadorNumerico runat="server" ID="pnRpActividades" /> <%--Paginador del repeater RpCategorias--%>
    </article>
    <article data-grupo="Listado" class="areaCantidadDeRegistro">
        <asp:Label runat="server" ID="lblCantidadRegistro" Text=""> </asp:Label> 
    </article>

    <article id="arAlerta"></article>
    <article id="popupConfirmacionDeseaBorrar"></article>

    <script type="text/javascript" >
        function mostrarAlertaNoHayDatos() {
            mostrarAlerta(
                '#arAlerta',
                {
                    mensaje: 'No se cuenta con actividades que cumplan con la condición indicada',
                    tipo: "advertencia",
                    transparencia: 0.9,
                    posicion: 'center',
                    permiteCerrar: true
                }
                );

            

            deshabilitarControl('#btnCancelarBusqueda');
            ocultarAreaDeListado();
            mostrarAreaFiltrosDeBusqueda();
            //mostrarAreaDeListado();
            //ocultarAreaFiltrosDeBusqueda();
        }

        function mostrarAlertaError(pvc_Mensaje) {
            mostrarAlerta(
                '#arAlerta',
                {
                    mensaje: pvc_Mensaje,
                    tipo: "peligro",
                    transparencia: 0.9,
                    posicion: 'center',
                    permiteCerrar: true
                }
                );

        }

        function regresarACategorias() {
            window.location = 'Lst_OT_CategoriaServicio.aspx'
        }

        function mostrarAlertaRegistroBorrado() {
            mostrarAlerta(
                '#arAlerta',
                {
                    mensaje: "Se ha borrado la actividad seleccionada",
                    tipo: "exito",
                    transparencia: 0.9,
                    posicion: 'center',
                    permiteCerrar: true
                }
                );

        }

        function mostrarAlertaRegistroNoBorrado() {
            mostrarAlerta(
                '#arAlerta',
                {
                    mensaje: "No ha sido posible borrar la actividad seleccionada",
                    tipo: "advertencia",
                    transparencia: 0.9,
                    posicion: 'center',
                    permiteCerrar: true
                }
                );

        }

        function mostrarPopupConfirmaDeseaBorrarRegistro(pvc_UniqueIdControl) {
            var vlo_ConfiguracionPopup = {
                titulo: 'Catálogo de Actividades por Categorías de Servicio',
                mensaje: 'Realmente desea borrar la actividad seleccionada?',
                botones:
                    [  //en corchetes indican arreglos y los parentesis cuadrados indican inicio o fin de funcionalidad o objeto, $librerias de jquery
                        {
                            idControl: "btnSi",
                            textoBoton: "Si",
                            onClick:
                                function () {
                                    $(this).attr("disabled", "disabled");  //cuando le de click al boton desabilitelo
                                    __doPostBack(pvc_UniqueIdControl, ''); //control de .net para q vaya al servidor a eliminar
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

                        },
                    ]
            };

            $('#popupConfirmacionDeseaBorrar').popup(vlo_ConfiguracionPopup);
            window.location = '#popupConfirmacionDeseaBorrar';
            return false; //detener comportamiento de ir al servidor hasta aceptar
        }

        $(document).ready(function () {
            aplicarConfiguracionBasicaListado({ alternarVisibilidadFiltroLista: true });
            $('[data-tipo="borrarRegistro"]').click(function () { return mostrarPopupConfirmaDeseaBorrarRegistro($(this).data("uniqueid")); }); //busca cualquier control con ese data tipo
            $('[data-tipo="volverListado"]').attr('src', '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x32, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Flecha_Izquierda.png")%>');
            $('[data-tipo="volverListado"]').on({
                'mouseover': function () { $(this).attr('src', '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x32, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Flecha_Izquierda.png")%>'); },

                'mouseout': function () { $(this).attr('src', '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x32, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Flecha_Izquierda.png")%>'); }
            });
            configurarLongitudMaximaTexto('#<%=Me.txtFiltroDescripcion.ClientID%>', <%=Utilerias.OrdenesDeTrabajo.Modelo.OTM_ACTIVIDAD.DESCRIPCION_BD_TAMANO%>);
            configurarContadorCaracteresRestantes('#<%=Me.txtFiltroDescripcion.ClientID%>', '#spContadorTxtDescripcion');
        }
            )
    </script>
</asp:Content>

