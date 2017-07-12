﻿<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/MasterPage/Mp_Listado.master" CodeFile="Lst_OT_AutorizadosContratacion.aspx.vb" Inherits="Catalogos_Lst_OT_AutorizadosContratacion" %>

<%@ Register TagPrefix="wuc" Namespace="Utilerias.WebForms.Controles" Assembly="Utilerias.WebForms.Controles" %>


<asp:Content runat="server" ID="Content1" ContentPlaceHolderID="cphFormulario">

    <header>
        <h2>
            Catálogo de Autorizados de Proceso de Contratación
        </h2>
    </header>

    <article data-grupo="FiltrosDeBusqueda" class="tituloSeccion">
        Criterios de busqueda
    </article>

    <article data-grupo="FiltrosDeBusqueda" class="formulario">
        <article class="areaBotonesListado">
            <a href="Frm_OT_ContratacionEncargado.aspx?pvn_Operacion=<%=Utilerias.OrdenesDeTrabajo.eOperacion.Agregar%>">
                <img alt="Registrar nuevas vias de contrato" data-tipo="nuevoRegistro" src="" />
            </a>
        </article>
        <table>
            <tr>
                <th>Identificación</th>
                <td>
                    <asp:TextBox Width="40%" runat="server" ID="txtFiltroIdentificacion" data-tipocontrol="texto"></asp:TextBox>
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
        Listado de Autorizados
    </article>
    <article data-grupo="Listado" class="listado">
        

        <article class="areaBotonesListado">
            <a href="Frm_OT_ContratacionEncargado.aspx?pvn_Operacion=<%=Utilerias.OrdenesDeTrabajo.eOperacion.Agregar%>">
                <img alt="Registrar nuevas vias de contrato" data-tipo="nuevoRegistro" src="" />
            </a>
            <img alt="Mostrar criterios de búsqueda" data-tipo="mostrarFiltros" src="" id="imgMostrarFiltros" />
        </article>

        <asp:Repeater ID="rpAutorizados" runat="server">

            <HeaderTemplate>
                <table>
                    <tr>
                        <th>
                            <asp:LinkButton runat="server" text ="Nombre" ID="Nombre" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_ENCARGADO_CONTRATOLST.NOMBRE%>" CommandArgument="ASC" OnCommand="lnkRPNombre_Command"></asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton runat="server" text ="Estado" ID="Estado" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_ENCARGADO_CONTRATOLST.ESTADO%>" CommandArgument="ASC" OnCommand="lnkRPNombre_Command"></asp:LinkButton>
                        </th>
                        <th>&nbsp;</th><%-- MODIFICAR --%>
                        <th>&nbsp;</th><%-- BORRAR --%>

                    </tr>
            </HeaderTemplate>

            <ItemTemplate>
                <tr class="lineaDelListado">
                    <td><%# Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_ENCARGADO_CONTRATOLST.NOMBRE)%></td>
                    <td><%# Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_ENCARGADO_CONTRATOLST.DESC_ESTADO)%></td>
                    <%--<td>
                        <asp:CheckBox runat="server" ID="ibEstado" data-NumEmpleado='<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_ENCARGADO_CONTRATOLST.NUM_EMPLEADO)%>' data-idubicacion='<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_ENCARGADO_CONTRATOLST.ID_UBICACION)%>' data-idpersonal='<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_ENCARGADO_CONTRATOLST.ID_PERSONAL)%>' Checked='<%#IIf(CInt(Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_ENCARGADO_CONTRATOLST.ESTADO_ACTIVO)) = 1, True, False)%>' AutoPostBack="true" OnCheckedChanged="ibEstado_CheckedChanged" />
                    </td>--%>
                    <td>
                        <a href="Frm_OT_ContratacionEncargado.aspx?pvn_Operacion=<%#Utilerias.OrdenesDeTrabajo.eOperacion.Modificar%>&pvc_IdAutorizado=<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_ENCARGADO_CONTRATOLST.ID_PERSONAL)%>">
                            <img title="Modificar datos del autorizado" alt="Modificar datos del espacio" data-tipo="modificarRegistro" src="" />
                        </a>
                    </td>
                    <td>
                        <asp:ImageButton runat="server" ID="ibBorrar" AlternateText="Borrar la vía de contrato" data-tipo="borrarRegistro" 
                            CommandArgument='<%#String.Format("{0},{1},{2}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_ENCARGADO_CONTRATOLST.NUM_EMPLEADO), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_ENCARGADO_CONTRATOLST.ID_UBICACION), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_ENCARGADO_CONTRATOLST.ID_PERSONAL))%>'
                            OnClick="ibBorrar_Click" />
                    </td>
                </tr>
            </ItemTemplate>

            <FooterTemplate>
                </table>
            </FooterTemplate>

        </asp:Repeater>

    </article>

    <article class="areaPaginadorListado" data-grupo="Listado">
        <wuc:PaginadorNumerico runat="server" ID="pnRpAutorizados" />
    </article>

    <article class="areaCantidadDeRegistro" data-grupo="Listado">
        <asp:Label runat="server" ID="lblCantidadDeRegistros" Text=""></asp:Label>
    </article>

    <article id="arAlerta">
    </article>

    <article id="popupConfirmacionDeseaBorrar">
    </article>

    <article id="arPopupGenerico"></article>

    <script type="text/javascript">
        function mostrarAlertaError(pvc_Msj) {
            mostrarAlerta(
                '#arAlerta',
                {
                    mensaje: pvc_Msj,
                    tipo: "peligro",
                    transparencia: 0.9,
                    posicion: 'center',
                    permiteCerrar: true
                }
            );
        };

        function mostrarAlertaRegistroBorrado() {
            mostrarAlerta(
                '#arAlerta',
                {
                    mensaje: "Se ha borrado al Encargado de contratación",
                    tipo: "exito",
                    transparencia: 0.9,
                    posicion: 'center'
                }
            );
        };

        function mostrarAlertaRegistroNoBorrado() {
            mostrarAlerta(
                '#arAlerta',
                {
                    mensaje: "No ha sido posible borrar el encargado de Contratación",
                    tipo: "advertencia",
                    transparencia: 0.9,
                    posicion: 'center'
                }
            );
        };

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

        function mostrarAlertaNoHayDatos() {
            mostrarAlerta(
                '#arAlerta',
                {
                    mensaje: "No se cuenta con encargado(s) de Contratacion(es) que cumplan con la(s) condicion(es) indicada(s)",
                    tipo: "advertencia",
                    transparencia: 0.9,
                    posicion: 'center'
                }
            );

            deshabilitarControl('#btnCancelarBusqueda');
            ocultarAreaDeListado();
            mostrarAreaFiltrosDeBusqueda();
        };

        function mostrarPopupConfirmacionDeseaBorrarRegistro(pvc_UniqueIdControl) {
            var vlo_ConfiguracionPopup = {
                titulo: "<em>Catalogo de encargados de contratación</em>",
                mensaje: "¿Realmente desea borrar al encargado(a) seleccionado(a)?",
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
        };

        function redireccionarListado(pvc_PaginaDestino) {
            window.location = pvc_PaginaDestino
        };

        $(document).ready(function () {

            aplicarConfiguracionBasicaListado({ alternarVisibilidadFiltroLista: true });
            $('[data-tipo="borrarRegistro"]').click(function () { return mostrarPopupConfirmacionDeseaBorrarRegistro($(this).data("uniqueid")); });

        });

    </script>

</asp:Content>