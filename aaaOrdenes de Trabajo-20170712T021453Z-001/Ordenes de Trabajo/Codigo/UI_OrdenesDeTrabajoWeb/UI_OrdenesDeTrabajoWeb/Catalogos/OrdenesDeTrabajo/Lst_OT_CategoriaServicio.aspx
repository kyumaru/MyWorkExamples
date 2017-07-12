<%@ Page Title="Listado de Categorías de Servicio" Language="VB" MasterPageFile="~/MasterPage/Mp_Listado.master" AutoEventWireup="false" CodeFile="Lst_OT_CategoriaServicio.aspx.vb" Inherits="Catalogos_Lst_OT_CategoriaServicio" %>

<%@ Register TagPrefix="wuc" Namespace="Utilerias.WebForms.Controles" Assembly="Utilerias.WebForms.Controles" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphFormulario" Runat="Server">
    <header>
        <h2>Catálogo de Categorías de Servicio</h2>
    </header>
    <article data-grupo="FiltrosDeBusqueda" class="tituloSeccion">
        <%--'grupo es para yo entender que es--%>
        Criterios de busqueda
    </article>
    <article data-grupo="FiltrosDeBusqueda" class="formulario">
        <article class="areaBotonesListado">
            <a href="Frm_OT_CategoriaServicio.aspx?pvn_Operacion=<%= Utilerias.OrdenesDeTrabajo.eOperacion.Agregar%>">
                <img alt="Registrar nueva Categoría de Servicio" data-tipo="nuevoRegistro" src="" />
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
                <th>Identificación del Supervisor</th>
                <td>
                    <asp:TextBox Width="40%" runat="server" ID="txtFiltroIdSupervisor" data-tipocontrol="texto"></asp:TextBox>
                    <br />
                    <span id="spContadorTxtSupervisor" class="contadorCaracteresRestantes"></span>
                </td>
            </tr>
            <tr>
                <th>Taller</th>
                <td>
                    <asp:DropDownList Width="40%" runat="server" ID="ddlFiltroTaller" AppendDataBoundItems="true" data-tipocontrol="combo"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <th>Estado</th>
                <td>
                    <asp:DropDownList Width="40%" runat="server" ID="ddlFiltroEstado" AppendDataBoundItems="true" data-tipocontrol="combo"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <th>Categorias Ocultas</th>
                <td>
                    <asp:DropDownList Width="40%" runat="server" ID="ddlCategoriasOcultas" AppendDataBoundItems="true" data-tipocontrol="combo"></asp:DropDownList>
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
        Listado de Categorías de Servicio
    </article>

    <article data-grupo="Listado" class="listado">
        <article class="areaBotonesListado">
            <a href="Frm_OT_CategoriaServicio.aspx?pvn_Operacion=<%= Utilerias.OrdenesDeTrabajo.eOperacion.Agregar%>">
                <img alt="Registrar nueva Categoría de Servicio" data-tipo="nuevoRegistro" src="" />
            </a>
            <img alt="Mostrar criterios de búsqueda" data-tipo="mostrarFiltros" src="" id="imgMostrarFiltros" />
        </article>
        <asp:Repeater runat="server" ID="rpCategorias">
            <HeaderTemplate>
                <table>
                    <tr>
                        <th>
                            <asp:LinkButton runat="server" ID="lnkDescripcion" Text="Descripción" CommandName="<%# Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_CATEGORIA_SERVICIOLST.DESCRIPCION%>" CommandArgument="ASC" OnCommand="lnkRpCategorias_Command" ></asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton runat="server" ID="lnkTaller" Text="Taller" CommandName="<%# Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_CATEGORIA_SERVICIOLST.ID_SECTOR_TALLER%>" CommandArgument="ASC" OnCommand="lnkRpCategorias_Command"></asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton runat="server" ID="lnkEstado" Text="Estado" CommandName="<%# Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_CATEGORIA_SERVICIOLST.ESTADO%>" CommandArgument="ASC" OnCommand="lnkRpCategorias_Command"></asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton runat="server" ID="lnkNombreSupervisor" Text="Supervisor" CommandName="<%# Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_CATEGORIA_SERVICIOLST.NOMBRE_EMPLEADO%>" CommandArgument="ASC" OnCommand="lnkRpCategorias_Command"></asp:LinkButton>
                        </th>
                        <th>&nbsp</th>
                        <th>&nbsp</th>
                        <th>&nbsp</th>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr class="lineaDelListado">
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_CATEGORIA_SERVICIOLST.DESCRIPCION)%></td>
                    <%--traer datos del source con eval y se lleva un valor--%>
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_CATEGORIA_SERVICIOLST.TALLER)%></td>
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_CATEGORIA_SERVICIOLST.DESCRIPCION_ESTADO)%></td>
                    <td><%#String.Format("{0} ({1})", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_CATEGORIA_SERVICIOLST.NOMBRE_EMPLEADO), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_CATEGORIA_SERVICIOLST.CEDULA))%></td>
                    <td>
                        <a href="Frm_OT_CategoriaServicio.aspx?pvn_Operacion=<%# Utilerias.OrdenesDeTrabajo.eOperacion.Modificar%>&pvc_IdCategoria=<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_CATEGORIA_SERVICIOLST.ID_CATEGORIA_SERVICIO)%>"> 
                            <img alt="Modificar datos de la Categoría" data-tipo="modificarRegistro" src="" />
                        </a>

                    </td>
                    <td>
                        <asp:ImageButton runat="server" ID="ibBorrar" AlternateText="Borrar la Categoría" data-tipo="borrarRegistro" CommandArgument="<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_CATEGORIA_SERVICIOLST.ID_CATEGORIA_SERVICIO)%>" OnClick="ibBorrar_Click" />
                    </td>
                    <td>
                        <a href="Lst_OT_ActividadesPorCategoriaServicio.aspx?pvc_IdCategoria=<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_CATEGORIA_SERVICIOLST.ID_CATEGORIA_SERVICIO)%>">
                            <img alt="Actividades de la Categoría" class="tooltip" title="Actividades de la Categoría" data-tipo="asociarActividad" src=""  />
                        </a>
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
    </article>

    <article data-grupo="Listado" class="areaPaginadorListado">
        <wuc:PaginadorNumerico runat="server" ID="pnRpCategorias" /> <%--Paginador del repeater RpCategorias--%>
    </article>
    <article data-grupo="Listado" class="areaCantidadDeRegistro">
        <asp:Label runat="server" ID="lblCantidadRegistro" Text=""> </asp:Label> 
    </article>

    <article id="arAlerta"></article>
    <article id="arPopupGenerico"></article>  
    <article id="popupConfirmacionDeseaBorrar"></article>

    <script type="text/javascript" >
        function mostrarAlertaNoHayDatos() {
            mostrarAlerta(
                '#arAlerta',
                {
                    mensaje: 'No se cuenta con categorias de servicio que cumplan con la condición indicada',
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

        function mostrarAlertaRegistroBorrado() {
            mostrarAlerta(
                '#arAlerta',
                {
                    mensaje: "Se ha borrado la categoría de servicio seleccionada",
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
                    mensaje: "No ha sido posible borrar la categoría de servicio seleccionada",
                    tipo: "advertencia",
                    transparencia: 0.9,
                    posicion: 'center',
                    permiteCerrar: true
                }
                );

        }

        function mensajePopup(pvc_Mensaje, pvc_PaginaDestino) {

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

        function mostrarPopupConfirmaDeseaBorrarRegistro(pvc_UniqueIdControl) {
            var vlo_ConfiguracionPopup = {
                titulo: 'Catálogo de Categorías de Servicio',
                mensaje: 'Realmente desea borrar la categoria de servicio seleccionada?',
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
            $('[data-tipo="asociarActividad"]').attr('src', '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Agregar_Detalle.png")%>');

            configurarLongitudMaximaTexto('#<%=Me.txtFiltroDescripcion.ClientID%>', <%=Utilerias.OrdenesDeTrabajo.Modelo.OTM_CATEGORIA_SERVICIO.DESCRIPCION_BD_TAMANO%>);
            configurarContadorCaracteresRestantes('#<%=Me.txtFiltroDescripcion.ClientID%>', '#spContadorTxtDescripcion');

            configurarLongitudMaximaTexto('#<%=Me.txtFiltroIdSupervisor.ClientID%>', <%=Utilerias.OrdenesDeTrabajo.Constantes.TAMANNO_CEDULA%>);
            configurarContadorCaracteresRestantes('#<%=Me.txtFiltroIdSupervisor.ClientID%>', '#spContadorTxtSupervisor');

            $('[data-tipo="asociarActividad"]').on({
                'mouseover': function () { $(this).attr('src', '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Icono_Agregar_Detalle.png")%>'); },

                'mouseout': function () { $(this).attr('src', '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Agregar_Detalle.png")%>'); }
            });

            habilitarTooltipGenerico()
        }
            )
    </script>
</asp:Content>

