<%@ Page Title="Catálogo de áreas profesionales" MasterPageFile="~/MasterPage/Mp_Listado.master" Language="VB" AutoEventWireup="false" CodeFile="Lst_OT_AreasProfesionales.aspx.vb" Inherits="Catalogos_Lst_OT_AreasDisenio" %>

<%@ Register TagPrefix="wuc" Namespace="Utilerias.WebForms.Controles" Assembly="Utilerias.WebForms.Controles" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphFormulario">

    <header>
        <h2>Catálogo de Areas Profesionales</h2>
    </header>

    <article data-grupo="FiltrosDeBusqueda" class="tituloSeccion">
        Criterios de busqueda
    </article>
    <article data-grupo="FiltrosDeBusqueda" class="formulario">
        <article class="areaBotonesListado">
            <a href="Frm_OT_AreasProfesionales.aspx?pvn_Operacion=<%=Utilerias.OrdenesDeTrabajo.eOperacion.Agregar%>">
                <img alt="Registrar nueva área" data-tipo="nuevoRegistro" src="" />
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
        Listado de Areas Profesionales
    </article>

    <article data-grupo="Listado" class="listado">
        <article class="areaBotonesListado">
            <a href="Frm_OT_AreasProfesionales.aspx?pvn_Operacion=<%=Utilerias.OrdenesDeTrabajo.eOperacion.Agregar%>">
                <img alt="Registrar nueva área" data-tipo="nuevoRegistro" src="" />
            </a>
            <img alt="Mostrar criterios de búsqueda" data-tipo="mostrarFiltros" src="" id="imgMostrarFiltros" />
        </article>


        <asp:Repeater ID="rpAreas" runat="server">

            <HeaderTemplate>
                <table>
                    <tr>
                        <th>
                            <asp:LinkButton runat="server" text ="Sufijo" ID="Sufijo" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_AREA_PROFESIONALLST.SUFIJO%>" CommandArgument="ASC" OnCommand="lnkRPAreas_Command"></asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton runat="server" text ="Descripcion" ID="Descripcion" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_AREA_PROFESIONALLST.DESCRIPCION%>" CommandArgument="ASC" OnCommand="lnkRPAreas_Command"></asp:LinkButton>
                        </th>

                        <th>
                            <asp:LinkButton runat="server" text ="Estado" ID="Estado" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_AREA_PROFESIONALLST.ESTADO%>" CommandArgument="ASC" OnCommand="lnkRPAreas_Command"></asp:LinkButton>
                        </th>

                        <th>&nbsp;</th>
                        <th>&nbsp;</th>

                    </tr>
            </HeaderTemplate>

            <ItemTemplate>
                <tr class="lineaDelListado">
                    <td><%# Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_AREA_PROFESIONALLST.SUFIJO)%></td>
                    <td><%# Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_AREA_PROFESIONALLST.DESCRIPCION)%></td>
                    <td><%# Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_AREA_PROFESIONALLST.DESCRIPCION_ESTADO)%></td>
                    <td>
                        <a href="Frm_OT_AreasProfesionales.aspx?pvn_Operacion=<%#Utilerias.OrdenesDeTrabajo.eOperacion.Modificar%>&pvc_IdArea=<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_AREA_PROFESIONALLST.ID_AREA_PROFESIONAL)%>">
                            <img alt="Modificar datos del area profesional" data-tipo="modificarRegistro" src="" />
                        </a>
                    </td>
                    <td>
                        <asp:ImageButton runat="server" ID="ibBorrar" AlternateText="Borrar el Area profesional" data-tipo="borrarRegistro" 
                            CommandArgument="<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_AREA_PROFESIONALLST.ID_AREA_PROFESIONAL)%>"
                            Visible="<%#IIf(CType(Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_AREA_PROFESIONALLST.POSEE_REGISTROS_ASOCIADOS), Integer) = 0, True, False)%>"
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
        <wuc:PaginadorNumerico runat="server" ID="pnRpAreas" />
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
                    mensaje: "Se ha borrado el Area profesional",
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
                    mensaje: "No ha sido posible borrar el Area seleccionada",
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
                    mensaje: "No se cuenta con Areas profesional(es) que cumplan con la(s) condicion(es) indicada(s)",
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
                titulo: "<em>Catalogo de Areas profesionales</em>",
                mensaje: "¿Realmente desea borrar el Area profesional seleccionada?",
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