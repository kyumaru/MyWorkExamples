<%@ Page Title="Listado de Ubicaciones" Language="VB" MasterPageFile="~/MasterPage/Mp_Listado.master" AutoEventWireup="false" CodeFile="Lst_OT_Ubicaciones.aspx.vb" Inherits="Catalogos_Lst_OT_Ubicaciones" %>

<%@ Register TagPrefix="wuc" Namespace="Utilerias.WebForms.Controles" Assembly="Utilerias.WebForms.Controles" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphFormulario" Runat="Server">
    <header>
        <h2>Catálogo de Ubicaciones (Sedes u Otros)</h2>
    </header>
    <article data-grupo="FiltrosDeBusqueda" class="tituloSeccion">
        <%--'grupo es para yo entender que es--%>
        Criterios de busqueda
    </article>
    <article data-grupo="FiltrosDeBusqueda" class="formulario">

        <article class="areaBotonesListado">
            <a href="Frm_OT_Ubicaciones.aspx?pvn_Operacion=<%= Utilerias.OrdenesDeTrabajo.eOperacion.Agregar%>">
                <img alt="Registrar nueva Ubicación" data-tipo="nuevoRegistro" src="" />
            </a>
        </article>

        <table>
            <tr>
                <th>Nombre</th>
                <td>
                    <asp:TextBox Width="30%" runat="server" ID="txtFiltroNombre" data-tipocontrol="texto"></asp:TextBox>
                    <br />
                    <span id="spContadorTxtNombre" class="contadorCaracteresRestantes"></span>
                </td>
            </tr>
            <tr>
                <th>Pertenece a Sede</th>
                <td>
                    <asp:DropDownList Width="30%" runat="server" ID="ddlFiltroPerteneceSede" AppendDataBoundItems="true" data-tipocontrol="combo"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <th>Estado</th>
                <td>
                    <asp:DropDownList Width="30%" runat="server" ID="ddlFiltroEstado" AppendDataBoundItems="true" data-tipocontrol="combo"></asp:DropDownList>
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
        Listado de Ubicaciones (Sedes u Otros)
    </article>

    <article data-grupo="Listado" class="listado">
        <article class="areaBotonesListado">
            <a href="Frm_OT_Ubicaciones.aspx?pvn_Operacion=<%= Utilerias.OrdenesDeTrabajo.eOperacion.Agregar%>">
                <img alt="Registrar nueva Ubicación" data-tipo="nuevoRegistro" src="" />
            </a>
            <img alt="Mostrar criterios de búsqueda" data-tipo="mostrarFiltros" src="" id="imgMostrarFiltros" />
        </article>
        <asp:Repeater runat="server" ID="rpUbicaciones">
            <HeaderTemplate>
                <table>
                    <tr>
                        <th>
                            <asp:LinkButton runat="server" ID="lnkNombre" Text="Nombre" CommandName="<%# Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_UBICACIONLST.DESCRIPCION%>" CommandArgument="ASC" OnCommand="lnkRpUbicaciones_Command" ></asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton runat="server" ID="lnkPerteneceSede" Text="Pertenece a Sede" CommandName="<%# Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_UBICACIONLST.PERTENECE_A_SEDE%>" CommandArgument="ASC" OnCommand="lnkRpUbicaciones_Command"></asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton runat="server" ID="lnkEstado" Text="Estado" CommandName="<%# Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_UBICACIONLST.ESTADO%>" CommandArgument="ASC" OnCommand="lnkRpUbicaciones_Command"></asp:LinkButton>
                        </th>
                        <th>&nbsp</th>
                        <%--<th>&nbsp</th>--%>
                        <%--'espacio en blanco--%>
                        <th>&nbsp</th>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr class="lineaDelListado">
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_UBICACIONLST.DESCRIPCION)%></td>
                    <%--traer datos del source con eval y se lleva un valor--%>
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_UBICACIONLST.DESCRIPCION_PERTENECE)%></td>
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_UBICACIONLST.DESCRIPCION_ESTADO)%></td>
                    <td>
                        <a href="Frm_OT_Ubicaciones.aspx?pvn_Operacion=<%# Utilerias.OrdenesDeTrabajo.eOperacion.Modificar%>&pvc_IdUbicacion=<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_UBICACIONLST.ID_UBICACION)%>"> 
                            <img alt="Modificar datos de la Ubicación" data-tipo="modificarRegistro" src="" />
                        </a>

                    </td>
                    <td>
                        <asp:ImageButton runat="server" ID="ibBorrar" AlternateText="Borrar la Ubicación" data-tipo="borrarRegistro" CommandArgument="<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_UBICACIONLST.ID_UBICACION)%>" OnClick="ibBorrar_Click" />
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
    </article>
    <article data-grupo="Listado" class="areaPaginadorListado">
        <wuc:PaginadorNumerico runat="server" ID="pnRpUbicaciones" /> <%--Paginador del repeater RpUbicacion--%>
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
                    mensaje: 'No se cuenta con ubicaciones que cumplan con la condición indicada',
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
                    mensaje: "Se ha borrado la ubicación seleccionada",
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
                    mensaje: "No ha sido posible borrar la ubicación seleccionada",
                    tipo: "advertencia",
                    transparencia: 0.9,
                    posicion: 'center',
                    permiteCerrar: true
                }
                );

        }

        function mostrarPopupConfirmaDeseaBorrarRegistro(pvc_UniqueIdControl) {
            var vlo_ConfiguracionPopup = {
                titulo: 'Catálogo de Ubicaciones',
                mensaje: 'Realmente desea borrar la ubicación seleccionada?',
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

            configurarLongitudMaximaTexto('#<%=Me.txtFiltroNombre.ClientID%>', <%=Utilerias.OrdenesDeTrabajo.Modelo.OTM_UBICACION.DESCRIPCION_BD_TAMANO%>);
            configurarContadorCaracteresRestantes('#<%=Me.txtFiltroNombre.ClientID%>', '#spContadorTxtNombre');
        }
            )
    </script>
</asp:Content>

