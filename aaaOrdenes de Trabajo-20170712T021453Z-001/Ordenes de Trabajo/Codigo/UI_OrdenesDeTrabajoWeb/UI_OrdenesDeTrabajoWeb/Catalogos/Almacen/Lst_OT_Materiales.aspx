<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/MasterPage/Mp_Listado.master" CodeFile="Lst_OT_Materiales.aspx.vb" Inherits="Catalogos_Lst_OT_Materiales" %>

<%@ Register TagPrefix="wuc" Namespace="Utilerias.WebForms.Controles" Assembly="Utilerias.WebForms.Controles" %>

<asp:Content runat="server" ID="Content1" ContentPlaceHolderID="cphFormulario">

    <header>
        <h2>Catálogo de materiales</h2>
    </header>

    <article data-grupo="FiltrosDeBusqueda" class="tituloSeccion">
        Criterios de busqueda
    </article>
    <article data-grupo="FiltrosDeBusqueda" class="formulario">
        <article class="areaBotonesListado">
            <a href="Frm_OT_Materiales.aspx?pvn_Operacion=<%=Utilerias.OrdenesDeTrabajo.eOperacion.Agregar%>&pvc_IdUbicacion=<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_MATERIALLST.ID_UBICACION_ADMINISTRA)%>">
                <img alt="Registrar nuevas vias de contrato" data-tipo="nuevoRegistro" src="" title="Agregar nuevo registro" alt="Agregar nuevo registro"/>
            </a>
        </article>
        <table>
            <tr>
                <th>Código</th>
                <td>
                    <asp:TextBox Width="40%" runat="server" ID="txtFiltroCodigo" data-tipocontrol="texto"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>Descripción</th>
                <td>
                    <asp:TextBox Width="40%" runat="server" ID="txtFiltroDescripcion" data-tipocontrol="texto"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>Estado</th>
                <td>
                    <asp:DropDownList Width="40%" runat="server" ID="ddlFiltroEstado" AppendDataBoundItems="true" data-tipocontrol="combo"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <th>Categoría</th>
                <td>
                    <asp:UpdatePanel runat="server" ID="upCategoria" UpdateMode="Conditional" >
                        <ContentTemplate>
                            <asp:DropDownList Width="40%" runat="server" ID="ddlFiltroCategoria" AppendDataBoundItems="true" AutoPostBack="true"></asp:DropDownList>
                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="ddlFiltroCategoria" />
                        </Triggers>
                    </asp:UpdatePanel>
                    
                </td>
            </tr>
            <tr>
                <th>Sub Categoría</th>
                <td>
                    <asp:UpdatePanel runat="server" ID="upSubcategoria" UpdateMode="Conditional" >
                        <ContentTemplate>
                            <asp:DropDownList Width="40%" runat="server" ID="ddlFiltroSubCategoria" AppendDataBoundItems="true" AutoPostBack="true"></asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>

    </article>

    <article data-grupo="FiltrosDeBusqueda" class="areaBotones">
        <asp:Button runat="server" ID="btnBuscar" Text="Buscar" />
        <asp:Button runat="server" ID="btnLimpiarFiltros" OnClick="btnLimpiarFiltros_Click" data-tipo="limpiarFiltros" text="Limpiar Filtros" />
        <input type="button" data-tipo="cancelarBusqueda" value="Cancelar" id="btnCancelarBusqueda" />
    </article>

    <article data-grupo="Listado" class="tituloSeccion">
        Listado de materiales
    </article>
    <article data-grupo="Listado" class="listado">
        
        <article class="areaBotonesListado">
            <a href="Frm_OT_Materiales.aspx?pvn_Operacion=<%=Utilerias.OrdenesDeTrabajo.eOperacion.Agregar%>">
                <img alt="Registrar nuevos materiales" data-tipo="nuevoRegistro" src=""  title="Agregar nuevo registro" alt="Agregar nuevo registro" />
            </a>
            <img alt="Mostrar criterios de búsqueda" data-tipo="mostrarFiltros" src="" id="imgMostrarFiltros" />
        </article>


        <asp:Repeater ID="rpMateriales" runat="server">

            <HeaderTemplate>
                <table>
                    <tr>
                        <th>
                            <asp:LinkButton runat="server" text ="Código" ID="lnkCodigo" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_MATERIALLST.ID_MATERIAL%>" CommandArgument="ASC" OnCommand="lnkRPMateriales_Command"></asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton runat="server" text ="Descripción" ID="lnkDescripcion" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_MATERIALLST.DESCRIPCION%>" CommandArgument="ASC" OnCommand="lnkRPMateriales_Command"></asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton runat="server" text ="Cantidad en Existencia" ID="lnkCantidad" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_MATERIALLST.CANTIDAD_EXISTENCIA_UNIDAD%>" CommandArgument="ASC" OnCommand="lnkRPMateriales_Command"></asp:LinkButton>
                        </th>

                        <th>
                            <asp:LinkButton runat="server" text ="Costo Promedio" ID="lnkCostoPromedio" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_MATERIALLST.COSTO_PROMEDIO%>" CommandArgument="ASC" OnCommand="lnkRPMateriales_Command"></asp:LinkButton>
                        </th>

                        <th>
                            <asp:LinkButton runat="server" text ="Costo Total" ID="lnkCostoTotal" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_MATERIALLST.COSTO_TOTAL%>" CommandArgument="ASC" OnCommand="lnkRPMateriales_Command"></asp:LinkButton>
                        </th>

                        <th>
                            <asp:LinkButton runat="server" text ="Estado" ID="lnkEstado" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_MATERIALLST.ESTADO%>" CommandArgument="ASC" OnCommand="lnkRPMateriales_Command"></asp:LinkButton>
                        </th>
                        
                        <th>&nbsp;</th><%-- MODIFICAR --%>
                        <th>&nbsp;</th><%-- BORRAR --%>

                    </tr>
            </HeaderTemplate>

            <ItemTemplate>
                <tr class="lineaDelListado">
                    <td><%# Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_MATERIALLST.ID_MATERIAL)%></td>
                    <td><%# Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_MATERIALLST.DESCRIPCION)%></td>
                    <td><%# Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_MATERIALLST.CANTIDAD_EXISTENCIA)%></td>
                    <td><%# Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_MATERIALLST.COSTO_PROMEDIO)%></td>
                    <td><%# Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_MATERIALLST.COSTO_TOTAL)%></td>
                    <td><%# Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_MATERIALLST.DESC_ESTADO)%></td>
                    <td>
                        <a href="Frm_OT_Materiales.aspx?pvn_Operacion=<%#Utilerias.OrdenesDeTrabajo.eOperacion.Modificar%>&pvc_IdMaterial=<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_MATERIALLST.ID_MATERIAL)%>&pvc_Cantidad=<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_MATERIALLST.CANTIDAD_EXISTENCIA)%>&pvc_CostoPromedio=<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_MATERIALLST.COSTO_PROMEDIO)%>&pvc_FiltroBusquedaForm=<%= Me.FiltroBusquedaForm%>">
                            <img alt="Modificar datos del material" data-tipo="modificarRegistro" src="" />
                        </a>
                    </td>
                    <td>
                        <asp:ImageButton runat="server" ID="ibBorrar" AlternateText="Borrar el material" data-tipo="borrarRegistro" 
                            CommandArgument="<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_MATERIALLST.ID_MATERIAL)%>"
                            Visible="<%#IIf(CType(Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_MATERIALLST.POSEE_REGISTROS_ASOCIADOS), Integer) = 0, True, False)%>"
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
        <wuc:PaginadorNumerico runat="server" ID="pnRpMateriales" />
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

        function mostrarPopUp(pvc_IdPopup) {
            window.location = pvc_IdPopup;
        };


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
                    mensaje: "Se ha borrado el material",
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
                    mensaje: "No ha sido posible borrar el material",
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
                    mensaje: "No se cuenta con material(s) que cumplan con la(s) condicion(es) indicada(s)",
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
                titulo: "<em>Catalogo de materiales</em>",
                mensaje: "¿Realmente desea borrar el material seleccionado?",
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


