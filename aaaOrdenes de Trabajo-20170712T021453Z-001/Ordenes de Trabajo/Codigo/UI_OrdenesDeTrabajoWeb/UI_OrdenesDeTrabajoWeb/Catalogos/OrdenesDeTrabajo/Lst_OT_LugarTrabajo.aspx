<%@ Page Title="Listado de Lugar de Trabajo" Language="VB" MasterPageFile="~/MasterPage/Mp_Listado.master" AutoEventWireup="false" CodeFile="Lst_OT_LugarTrabajo.aspx.vb" Inherits="Catalogos_Lst_OT_LugarTrabajo" %>

<%@ Register TagPrefix="wuc" Namespace="Utilerias.WebForms.Controles" Assembly="Utilerias.WebForms.Controles" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphFormulario" Runat="Server">
    <header>
        <h2>Catálogo de Edificios y Sitios Universitarios</h2>
    </header>
    <article data-grupo="FiltrosDeBusqueda" class="tituloSeccion">
        <%--'grupo es para yo entender que es--%>
        Criterios de busqueda
    </article>
    <article data-grupo="FiltrosDeBusqueda" class="formulario">
        <article class="areaBotonesListado">
            <a href="Frm_OT_LugarTrabajo.aspx?pvn_Operacion=<%= Utilerias.OrdenesDeTrabajo.eOperacion.Agregar%>">
                <img alt="Registrar nuevo Edificio o Sitio Universitario" data-tipo="nuevoRegistro" src="" />
            </a>
        </article>
        <table>
            <tr>
                <th>Nombre</th>
                <td>
                    <asp:TextBox Width="70%" runat="server" ID="txtFiltroNombre" data-tipocontrol="texto"></asp:TextBox>
                    <br />
                    <span id="spContadorTxtNombre" class="contadorCaracteresRestantes"></span>
                </td>
            </tr>
            <tr>
                <th>Clasificación</th>
                <td>
                    <asp:DropDownList Width="70%" runat="server" ID="ddlFiltroClasificacion" AppendDataBoundItems="true" data-tipocontrol="combo"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <th>Ubicación (Sede/Otro)</th>
                <td>
                    <asp:DropDownList Width="70%" runat="server" ID="ddlFiltroUbicacion" AppendDataBoundItems="true" data-tipocontrol="combo"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <th>Unidad que Administra</th>
                <td>
                    <asp:DropDownList Width="70%" runat="server" ID="ddlFiltroUnidadAdministra" AppendDataBoundItems="true" data-tipocontrol="combo"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <th>Estado</th>
                <td>
                    <asp:DropDownList Width="70%" runat="server" ID="ddlFiltroEstado" AppendDataBoundItems="true" data-tipocontrol="combo"></asp:DropDownList>
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
        Listado de Edificios y Sitios Universitarios
    </article>

    <article data-grupo="Listado" class="listado">
        <article class="areaBotonesListado">
            <a href="Frm_OT_LugarTrabajo.aspx?pvn_Operacion=<%= Utilerias.OrdenesDeTrabajo.eOperacion.Agregar%>">
                <img alt="Registrar nuevo Edificio o Sitio Universitario" data-tipo="nuevoRegistro" src="" />
            </a>
            <img alt="Mostrar criterios de búsqueda" data-tipo="mostrarFiltros" src="" id="imgMostrarFiltros" />
        </article>
        <asp:Repeater runat="server" ID="rpLugares">
            <HeaderTemplate>
                <table>
                    <tr>
                        <th>
                            <asp:LinkButton runat="server" ID="lnkNombre" Text="Nombre" CommandName="<%# Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_LUGAR_TRABAJOLST.NOMBRE%>" CommandArgument="ASC" OnCommand="lnkRpLugares_Command" ></asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton runat="server" ID="lnkSede" Text="Sede" CommandName="<%# Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_LUGAR_TRABAJOLST.ID_UBICACION_PERTENECE%>" CommandArgument="ASC" OnCommand="lnkRpLugares_Command"></asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton runat="server" ID="lnkClasificacion" Text="Clasificación" CommandName="<%# Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_LUGAR_TRABAJOLST.CLASIFICACION%>" CommandArgument="ASC" OnCommand="lnkRpLugares_Command"></asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton runat="server" ID="lnkEstado" Text="Estado" CommandName="<%# Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_LUGAR_TRABAJOLST.ESTADO%>" CommandArgument="ASC" OnCommand="lnkRpLugares_Command"></asp:LinkButton>
                        </th>
                        <th>&nbsp</th>
                        <%--<th>&nbsp</th>--%>
                        <%--'espacio en blanco--%>
                        <th>&nbsp</th>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr class="lineaDelListado">
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_LUGAR_TRABAJOLST.NOMBRE)%></td>
                    <%--traer datos del source con eval y se lleva un valor--%>
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_LUGAR_TRABAJOLST.UBICACION)%></td>
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_LUGAR_TRABAJOLST.DESCRIPCION_CLASIFICACION)%></td>
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_LUGAR_TRABAJOLST.DESCRIPCION_ESTADO)%></td>
                    <td>
                        <a href="Frm_OT_LugarTrabajo.aspx?pvn_Operacion=<%# Utilerias.OrdenesDeTrabajo.eOperacion.Modificar%>&pvc_IdLugarTrabajo=<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_LUGAR_TRABAJOLST.ID_LUGAR_TRABAJO)%>"> 
                            <img alt="Modificar datos del Edificio o Sitio Universitario" data-tipo="modificarRegistro" src="" />
                        </a>

                    </td>
                    <td>
                        <asp:ImageButton runat="server" ID="ibBorrar" AlternateText="Borrar la Ubicación" data-tipo="borrarRegistro" CommandArgument="<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_LUGAR_TRABAJOLST.ID_LUGAR_TRABAJO)%>" OnClick="ibBorrar_Click" />
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
    </article>
    <article data-grupo="Listado" class="areaPaginadorListado">
        <wuc:PaginadorNumerico runat="server" ID="pnRpLugares" /> <%--Paginador del repeater RpUbicacion--%>
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
                    mensaje: 'No se cuenta con edificios o sitios universitarios que cumplan con la condición indicada',
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
                    mensaje: "Se ha borrado el edificio o sitio universitario seleccionado",
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
                    mensaje: "No ha sido posible borrar el edificio o sitio universitario seleccionado",
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
                titulo: 'Catálogo de Edificios y Sitios Universitarios',
                mensaje: 'Realmente desea borrar el edificio o sitio universitario seleccionado?',
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

            configurarLongitudMaximaTexto('#<%=Me.txtFiltroNombre.ClientID%>', <%=Utilerias.OrdenesDeTrabajo.Modelo.OTM_LUGAR_TRABAJO.NOMBRE_BD_TAMANO%>);
            configurarContadorCaracteresRestantes('#<%=Me.txtFiltroNombre.ClientID%>', '#spContadorTxtNombre');
        }
            )
    </script>
</asp:Content>

