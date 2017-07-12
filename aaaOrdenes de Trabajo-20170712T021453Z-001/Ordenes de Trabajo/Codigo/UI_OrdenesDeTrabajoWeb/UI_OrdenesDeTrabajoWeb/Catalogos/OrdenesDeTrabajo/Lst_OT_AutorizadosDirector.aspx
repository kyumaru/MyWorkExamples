<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/Mp_Listado.master" AutoEventWireup="false" CodeFile="Lst_OT_AutorizadosDirector.aspx.vb" Inherits="Catalogos_Lst_OT_AutorizadosDirector" %>

<%@ Register TagPrefix="wuc" Namespace="Utilerias.WebForms.Controles" Assembly="Utilerias.WebForms.Controles" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphFormulario" runat="Server">

    <header>
        <h2>Catálogo de Autorizados por Director</h2>
    </header>

    <article data-grupo="FiltrosDeBusqueda" class="tituloSeccion">
        Criterios de búsqueda
    </article>

    <article data-grupo="FiltrosDeBusqueda" class="formulario">

        <article class="areaBotonesListado">
            <a id="lnkNuevoRegistroFiltro" href="Frm_OT_AutorizadosDirector.aspx?pvn_Operacion=<%=Utilerias.OrdenesDeTrabajo.eOperacion.Agregar%>">
                <img id="imgNuevoRegistro2" data-tipo="nuevoRegistro" title="Agregar nuevo registro" alt="Agregar nuevo registro" src="" />
            </a>
        </article>

        <table>
            <tr>
                <th>Identificación</th>
                <td>
                    <asp:TextBox runat="server" ID="txtIdPersonal" data-tipocontrol="texto" MaxLength="20"></asp:TextBox>
                </td>
            </tr>
        </table>
    </article>

    <article data-grupo="FiltrosDeBusqueda" class="areaBotones">
        <asp:Button runat="server" ID="btnBuscar" Text="Buscar" />
        <input type="button" data-tipo="limpiarFiltros" value="Limpiar Filtros" />
        <input id="btnCancelarBusqueda" type="button" data-tipo="cancelarBusqueda" value="Cancelar" />
    </article>

    <article data-grupo="Listado" class="tituloSeccion">
        Autorizados por Director
    </article>

    <article data-grupo="Listado" class="listado">

        <article class="areaBotonesListado">
            <a id="lnkNuevoRegistro" href="Frm_OT_AutorizadosDirector.aspx?pvn_Operacion=<%=Utilerias.OrdenesDeTrabajo.eOperacion.Agregar%>">
                <img id="imgNuevoRegistro" data-tipo="nuevoRegistro" title="Agregar nuevo registro" alt="Agregar nuevo registro" src="" />
            </a>
            <img id="imgMostrarFiltros" alt="Mostrar criterios de Búsqueda" title="Mostrar criterios de Búsqueda" data-tipo="mostrarFiltros" src="" />
        </article>

        <asp:Repeater runat="server" ID="rpAutorizadoDirector">
            <HeaderTemplate>
                <table>
                    <tr>
                        <th>
                            <asp:LinkButton runat="server" ID="lnkIdPersonal" Text="Identificación" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_AUTORIZADO_DIRECTORLst.ID_PERSONAL%>" CommandArgument="ASC" OnCommand="lnkRpAutorizadoDirector_Command"></asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton runat="server" ID="lnkNombre" Text="Nombre" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_AUTORIZADO_DIRECTORLst.NOMBRE%>" CommandArgument="ASC" OnCommand="lnkRpAutorizadoDirector_Command"></asp:LinkButton>
                        </th>
                        <th>Unidad</th>
                        <%--<th>&nbsp;</th>--%>
                        <th>&nbsp;</th>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr class="lineaDelListado">
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_AUTORIZADO_DIRECTORLst.ID_PERSONAL)%></td>
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_AUTORIZADO_DIRECTORLst.NOMBRE)%></td>
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_AUTORIZADO_DIRECTORLst.DESCRIPCION_UNIDAD_SIRH)%></td>
                   <%-- <td>
                        <a href="Frm_OT_AutorizadosDirector.aspx?pvn_Operacion=<%#Utilerias.OrdenesDeTrabajo.eOperacion.Modificar%>&pvn_NumEmpleado=<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_AUTORIZADO_DIRECTORLst.NUM_EMPLEADO)%>&pvn_NumEmpleado=<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_AUTORIZADO_DIRECTORLst.COD_UNIDAD_SIRH)%>">
                            <img title="Modificar registro" alt="Modificar registro" data-tipo="modificarRegistro" src="" />
                        </a>
                    </td>--%>
                    <td>
                        <asp:ImageButton runat="server" ID="ibBorrar" AlternateText="Borrar Registro" data-tipo="borrarRegistro" ToolTip="Borrar registro"
                            CommandArgument='<%# String.Format("{0}¬{1}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_AUTORIZADO_DIRECTORLST.NUM_EMPLEADO), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_AUTORIZADO_DIRECTORLst.COD_UNIDAD_SIRH))%>'
                            OnClick="ibBorrar_Click" />
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>

    </article>

    <article data-grupo="Listado" class="areaPaginadorListado">
        <wuc:PaginadorNumerico runat="server" ID="pnRpAutorizadoDirector" />
    </article>

    <article data-grupo="Listado" class="areaCantidadDeRegistro">
        <asp:Label runat="server" ID="lblCantidadDeRegistros" Text="" Visible="true"></asp:Label>
    </article>

    <article id="arAlerta"></article>
    <article id="arpopupConfirmaDeseaBorrar"></article>
    <article id="arPopupGenerico"></article>

        <script type="text/javascript">
            function mostrarAlertaError(pvc_Mensaje) {
                mostrarAlerta(
                    '#arAlerta',
                    {
                        mensaje: pvc_Mensaje,
                        tipo: 'peligro',
                        transparencia: 0.9,
                        posicion: 'center'
                    }
                );
            };

            function mostrarAlertaNoHayDatos() {
                mostrarAlerta(
                    '#arAlerta',
                    {
                        mensaje: 'No se cuenta con información para mostrar.',
                        tipo: "advertencia",
                        transparencia: 0.9,
                        posicion: 'center'
                    }
                );

                deshabilitarControl('#btnCancelarBusqueda');
                ocultarAreaDeListado();
                mostrarAreaFiltrosDeBusqueda();

                //mostrarAreaDeListado();
                //ocultarAreaFiltrosDeBusqueda();

            };

            function mostrarAlertaRegistroBorrado() {
                mostrarAlerta(
                    '#arAlerta',
                    {
                        mensaje: 'Se ha borrado el registro seleccionado.',
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
                        mensaje: 'No ha sido posible borrar el registro seleccionado.',
                        tipo: "advertencia",
                        transparencia: 0.9,
                        posicion: 'center'
                    }
                );
            };

            function mostrarAlertaActualizacionExitosa() {

                mostrarAlerta(
                    '#arAlerta',
                    {
                        mensaje: 'Se ha actualizado la información del registro.',
                        tipo: "exito",
                        transparencia: 0.9,
                        posicion: 'center',
                        onClosed: function () { regresarAlListado(); }
                    });
            };

            function mostrarPopupConfirmaDeseaBorrarRegistro(pvo_UniqueIdControl) {
                var vlo_ConfiguracionPopup = {
                    titulo: 'Catálogo de Autorizados por Director',
                    mensaje: '¿Desea borrar el registro seleccionado?',
                    botones:
                            [
                                {
                                    idControl: "btnSi",
                                    textoBoton: "Si",
                                    onClick:
                                        function (e) {
                                            $(this).attr("disabled", "disabled");
                                            __doPostBack(pvo_UniqueIdControl, '');
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
                                }
                            ]
                };

                $('#arpopupConfirmaDeseaBorrar').popup(vlo_ConfiguracionPopup);

                window.location = '#arpopupConfirmaDeseaBorrar';

                return false;
            }

            $(document).ready(function () {
                aplicarConfiguracionBasicaListado({ alternarVisibilidadFiltroLista: true });

                $('[data-tipo="borrarRegistro"]').click(function () { return mostrarPopupConfirmaDeseaBorrarRegistro($(this).data("uniqueid")); });                
            });

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

            function redireccionarListado(pvc_PaginaDestino) {
                window.location = pvc_PaginaDestino
            };

    </script>

</asp:Content>

