<%@ Page Title="Listado de Libros" Language="VB" MasterPageFile="~/MasterPage/Mp_Listado.master" AutoEventWireup="false" CodeFile="Lst_TC_Libro.aspx.vb" Inherits="Catalogos_Lst_TC_Libro" %>

<%@ Register TagPrefix="wuc" Namespace="Utilerias.WebForms.Controles" Assembly="Utilerias.WebForms.Controles" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphFormulario" runat="Server">
    <header>
        <h2>Catálogo de Libros</h2>
    </header>

    <article data-grupo="FiltrosDeBusqueda" class="tituloSeccion">
        Criterios de búsqueda
    </article>

    <article data-grupo="FiltrosDeBusqueda" class="formulario">
        <table>
            <tr>
                <th>ISBN</th>
                <td>
                    <asp:TextBox runat="server" ID="txtFiltroIsbn" data-tipocontrol="texto"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>Título</th>
                <td>
                    <asp:TextBox runat="server" ID="txtFiltroTitulo" data-tipocontrol="texto"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>Condición</th>
                <td>
                    <asp:DropDownList runat="server" ID="ddlFiltroCondicionLibro" AppendDataBoundItems="true" data-tipocontrol="combo"></asp:DropDownList>
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
        Listado de Libros
    </article>

    <article data-grupo="Listado" class="listado">
        <article class="areaBotonesListado">
            <a href="Frm_TC_Libro.aspx?pvn_Operacion=<%= Utilerias.TallerCapacitacion.eOperacion.Agregar%>">
                <img alt="Registrar nuevo libro" data-tipo="nuevoRegistro" src="" />
            </a>
            <img alt="Mostrar criterios de búsqueda" data-tipo="mostrarFiltros" src="" id="imgMostrarFiltros" />
        </article>
        <asp:Repeater runat="server" ID="rpLibros">
            <HeaderTemplate>
                <table>
                    <tr>
                        <th>
                            <asp:LinkButton runat="server" ID="lnkIsbn" Text="ISBN" CommandName="<%# Utilerias.TallerCapacitacion.Modelo.V_TCM_LIBROLST.ISBN%>" CommandArgument="ASC" OnCommand="lnkRpLibros_Command"></asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton runat="server" ID="lnkTitulo" Text="Título" CommandName="<%# Utilerias.TallerCapacitacion.Modelo.V_TCM_LIBROLST.TITULO%>" CommandArgument="ASC" OnCommand="lnkRpLibros_Command"></asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton runat="server" ID="lnkTotalPaginas" Text="Total de Páginas" CommandName="<%# Utilerias.TallerCapacitacion.Modelo.V_TCM_LIBROLST.TOTAL_PAGINAS%>" CommandArgument="ASC" OnCommand="lnkRpLibros_Command"></asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton runat="server" ID="lnkCondicion" Text="Condición" CommandName="<%# Utilerias.TallerCapacitacion.Modelo.V_TCM_LIBROLST.DESC_CONDICION_LIBRO%>" CommandArgument="ASC" OnCommand="lnkRpLibros_Command"></asp:LinkButton>
                        </th>
                        <th>&nbsp;</th>
                        <th>&nbsp;</th>
                        <th>&nbsp;</th>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr class="lineaDelListado">
                    <td><%#Eval(Utilerias.TallerCapacitacion.Modelo.V_TCM_LIBROLST.ISBN)%></td>
                    <td><%#Eval(Utilerias.TallerCapacitacion.Modelo.V_TCM_LIBROLST.TITULO)%></td>
                    <td><%#Eval(Utilerias.TallerCapacitacion.Modelo.V_TCM_LIBROLST.TOTAL_PAGINAS)%></td>
                    <td><%#Eval(Utilerias.TallerCapacitacion.Modelo.V_TCM_LIBROLST.DESC_CONDICION_LIBRO)%></td>
                    <td>
                        <a href="Frm_TC_Libro.aspx?pvn_Operacion=<%#Utilerias.TallerCapacitacion.eOperacion.Modificar%>&pvc_Isbn=<%#Eval(Utilerias.TallerCapacitacion.Modelo.V_TCM_LIBROLST.ISBN)%>">
                            <img alt="Modificar datos del libro" data-tipo="modificarRegistro" src="" />
                        </a>
                    </td>
                    <td>
                        <asp:ImageButton runat="server" ID="ibBorrar" AlternateText="Borrar el libro" data-tipo="borrarRegistro" CommandArgument="<%#Eval(Utilerias.TallerCapacitacion.Modelo.V_TCM_LIBROLST.ISBN)%>" OnClick="ibBorrar_Click" />
                    </td>
                    <td>
                        <a href="Frm_TC_AutoresPorLibro.aspx?pvc_Isbn=<%#Eval(Utilerias.TallerCapacitacion.Modelo.V_TCM_LIBROLST.ISBN)%>">
                            <img alt="Asociar autores al libro" class="tooltip" title="Asociar autores al libro" data-tipo="asociarAutor" src="" />
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
        <wuc:PaginadorNumerico runat="server" ID="pnRpLibros" />
    </article>

    <article data-grupo="Listado" class="areaCantidadDeRegistro">
        <asp:Label runat="server" ID="lblCantidadRegistro" Text=""></asp:Label>
    </article>

    <article id="arAlerta"></article>
    <article id="popupConfirmacionDeseaBorrar"></article>

    <script type="text/javascript">
        function mostrarAlertaError(pvc_Mensaje) {
            mostrarAlerta(
                '#arAlerta',
                {
                    mensaje: pvc_Mensaje,
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
                    mensaje: 'Se ha borrado el Libro seleccionado.',
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
                    mensaje: 'No ha sido posible borrar el Libro seleccionado',
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
                    mensaje: 'No se cuenta con libros que cumplan con la condición indicada',
                    tipo: "advertencia",
                    transparencia: 0.9,
                    posicion: 'center'
                }
            );

            deshabilitarControl('#btnCancelarBusqueda');
            ocultarAreaDeListado();
            mostrarAreaFiltrosDeBusqueda();
        }

        function mostrarPopupConfirmaDeseaBorrarRegistro(pvc_UniqueIdControl) {
            var vlo_ConfiguracionPopup = {
                titulo: 'Catálogo de Libros',
                mensaje: 'Realmente desea borra el libro seleccionado?',
                botones:
                    [
                        {
                            idControl: "btnSi",
                            textoBoton: "Si",
                            onClick:
                                function (e) {
                                    $(this).attr("disabled", "disabled");
                                    __doPostBack(pvc_UniqueIdControl, '');
                                }
                        },
                        {
                            idControl: "btnNo",
                            textoBoton: "No",
                            onClick: function (e) { cerrarPopup(); }
                        },
                        {
                            idControl: "btnCancelar",
                            textoBoton: "Cancelar",
                            onClick: function (e) { cerrarPopup(); }
                        }
                    ]
            };

            $('#popupConfirmacionDeseaBorrar').popup(vlo_ConfiguracionPopup);

            window.location = '#popupConfirmacionDeseaBorrar';

            return false;
        }

        $(document).ready(function () {
            aplicarConfiguracionBasicaListado({ alternarVisibilidadFiltroLista: true });
            $('[data-tipo="borrarRegistro"]').click(function () { return mostrarPopupConfirmaDeseaBorrarRegistro($(this).data("uniqueid")); });
            $('[data-tipo="asociarAutor"]').attr('src', '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Usuarios.png")%>');
            $('[data-tipo="asociarAutor"]').on({
                'mouseover': function () {$(this).attr('src', '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Icono_Usuarios.png")%>');},
                'mouseout': function () {$(this).attr('src', '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Usuarios.png")%>');}
            });

            habilitarTooltipGenerico();
        })
    </script>
</asp:Content>
