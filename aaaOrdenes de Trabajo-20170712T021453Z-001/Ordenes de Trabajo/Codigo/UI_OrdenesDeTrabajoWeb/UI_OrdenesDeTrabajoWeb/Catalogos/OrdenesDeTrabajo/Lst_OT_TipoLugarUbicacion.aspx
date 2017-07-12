﻿<%@ Page Title="Listado de Tipos de Lugares de Ubicacion" Language="VB" MasterPageFile="~/MasterPage/Mp_Listado.master" AutoEventWireup="false" CodeFile="Lst_OT_TipoLugarUbicacion.aspx.vb" Inherits="Catalogos_Lst_OT_TipoLugarUbicacion" %>

<%@ Register TagPrefix="wuc" Namespace="Utilerias.WebForms.Controles" Assembly="Utilerias.WebForms.Controles" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphFormulario" Runat="Server">
    <header>
        <h2>Catálogo de Lugar de ubicación</h2>
    </header>
    <article data-grupo="FiltrosDeBusqueda" class="tituloSeccion">
        <%--'grupo es para yo entender que es--%>
        Criterios de busqueda
    </article>
    <article data-grupo="FiltrosDeBusqueda" class="formulario">
        <article class="areaBotonesListado">
            <a href="Frm_OT_TipoLugarUbicacion.aspx?pvn_Operacion=<%= Utilerias.OrdenesDeTrabajo.eOperacion.Agregar%>">
                <img alt="Registrar nuevo Lugar de Ubicación" data-tipo="nuevoRegistro" src="" />
            </a>
        </article>
        <table>
            <tr>
                <th>Descripción</th>
                <td>
                    <asp:TextBox Width="30%" runat="server" ID="txtFiltroDescripcion" data-tipocontrol="texto"></asp:TextBox>
                    <br />
                    <span id="spContadorTxtDescripcion" class="contadorCaracteresRestantes"></span>
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
        Listado de Lugar de Ubicación
    </article>

    <article data-grupo="Listado" class="listado">
        <article class="areaBotonesListado">
            <a href="Frm_OT_TipoLugarUbicacion.aspx?pvn_Operacion=<%= Utilerias.OrdenesDeTrabajo.eOperacion.Agregar%>">
                <img alt="Registrar nuevo Lugar de Ubicación" data-tipo="nuevoRegistro" src="" />
            </a>
            <img alt="Mostrar criterios de búsqueda" data-tipo="mostrarFiltros" src="" id="imgMostrarFiltros" />
        </article>
        <asp:Repeater runat="server" ID="rpLugarUbicaciones">
            <HeaderTemplate>
                <table>
                    <tr>
                        <th>
                            <asp:LinkButton runat="server" ID="lnkDescripcion" Text="Descripción" CommandName="<%# Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_TIPO_LUGAR_UBICACIONLST.DESCRIPCION%>" CommandArgument="ASC" OnCommand="lnkRpLugarUbicaciones_Command" ></asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton runat="server" ID="lnkEstado" Text="Estado" CommandName="<%# Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_TIPO_LUGAR_UBICACIONLST.ESTADO%>" CommandArgument="ASC" OnCommand="lnkRpLugarUbicaciones_Command"></asp:LinkButton>
                        </th>
                        <th>&nbsp</th>
                        <%--<th>&nbsp</th>--%>
                        <%--'espacio en blanco--%>
                        <th>&nbsp</th>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr class="lineaDelListado">
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_TIPO_LUGAR_UBICACIONLST.DESCRIPCION)%></td>

                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_TIPO_LUGAR_UBICACIONLST.DESCRIPCION_ESTADO)%></td>
                    <td>
                        <a href="Frm_OT_TipoLugarUbicacion.aspx?pvn_Operacion=<%# Utilerias.OrdenesDeTrabajo.eOperacion.Modificar%>&pvc_IdLugarUbicacion=<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_TIPO_LUGAR_UBICACIONLST.ID_TIPO_LUGAR_UBICACION)%>"> 
                            <img alt="Modificar datos del Lugar de Ubicación" data-tipo="modificarRegistro" src="" />
                        </a>

                    </td>
                    <td>
                        <asp:ImageButton runat="server" ID="ibBorrar" AlternateText="Borrar el Lugar de Ubicación" data-tipo="borrarRegistro" CommandArgument="<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_TIPO_LUGAR_UBICACIONLST.ID_TIPO_LUGAR_UBICACION)%>" OnClick="ibBorrar_Click" />
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
    </article>

    <article data-grupo="Listado" class="areaPaginadorListado">
        <wuc:PaginadorNumerico runat="server" ID="pnRpLugarUbicaciones" /> <%--Paginador del repeater RpLugarUbicacion--%>
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
                    mensaje: 'No se cuenta con lugares de ubicación que cumplan con la condición indicada',
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
                    mensaje: "Se ha borrado el lugar de ubicación seleccionado",
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
                    mensaje: "No ha sido posible borrar el lugar de ubicación seleccionado",
                    tipo: "advertencia",
                    transparencia: 0.9,
                    posicion: 'center',
                    permiteCerrar: true
                }
                );

        }

        function mostrarPopupConfirmaDeseaBorrarRegistro(pvc_UniqueIdControl) {
            var vlo_ConfiguracionPopup = {
                titulo: 'Catálogo de Lugar de Ubicacion',
                mensaje: 'Realmente desea borrar el lugar de ubicación seleccionado?',
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

            configurarLongitudMaximaTexto('#<%=Me.txtFiltroDescripcion.ClientID%>', <%=Utilerias.OrdenesDeTrabajo.Modelo.OTM_TIPO_LUGAR_UBICACION.DESCRIPCION_BD_TAMANO%>);
            configurarContadorCaracteresRestantes('#<%=Me.txtFiltroDescripcion.ClientID%>', '#spContadorTxtDescripcion');
        }
            )
    </script>
</asp:Content>
