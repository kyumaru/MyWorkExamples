<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/MasterPage/Mp_Formulario.master" MaintainScrollPositionOnPostback="true" CodeFile="Frm_OT_AprobacionAjusteMateriales.aspx.vb" Inherits="OrdenesDeTrabajo_Almacen_Frm_OT_AprobacionAjusteMateriales" %>

<%@ Register TagPrefix="wuc" TagName="InformacionGeneral" Src="~/ControlesDeUsuario/Wuc_OT_InformacionGeneral.ascx" %>
<%@ Register TagPrefix="wuc" TagName="Materiales" Src="~/Controles/wuc_OT_Materiales.ascx" %>
<%@ Register TagPrefix="wuc" TagName="DatosMaterial" Src="~/Controles/wuc_OT_DatosMaterial.ascx" %>

<asp:Content runat="server" ID="Content1" ContentPlaceHolderID="cphFormulario">

    <header>
        <h2>Revisión de Requisición de Materiales</h2>
    </header>

    <article>
        <wuc:InformacionGeneral ID="ctrl_InfoGeneral" runat="server" />
    </article>

    <article class="tituloSeccion">
        Materiales Solicitados
    </article>
    <article class="listado" data-grupo="Listado">
        <asp:Repeater runat="server" ID="rpAprobados">
            <HeaderTemplate>
                <table>
                    <tr>
                        <th>
                            <asp:Label runat="server" ID="lblCodigo" Text="Código"></asp:Label>
                        </th>
                        <th>
                            <asp:Label runat="server" ID="lblDescripcion" Text="Descripción"></asp:Label>
                        </th>
                        <th>
                            <asp:Label runat="server" ID="lblCantidadSolicitada" Text="Cantidad Solicitada"></asp:Label>
                        </th>
                        <th>
                            <asp:Label runat="server" ID="lblDisponible" Text="Disp. Almacen"></asp:Label>
                        </th>
                        <th>
                            <asp:Label runat="server" ID="lblEstado" Text="Estado"></asp:Label>
                        </th>
                        <th>
                            <asp:Label runat="server" ID="lblMonto" Text="Monto"></asp:Label>
                        </th>
                        <%--<th>
                            <asp:Label runat="server" ID="lblCostoUnitario" Text="Costo Unitario"></asp:Label>
                        </th>--%>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_MATERIALLST.ID_MATERIAL)%></td>
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_MATERIALLST.DESCRIPCION)%></td>
                    <td>
                        <%--<asp:Image runat="server" ID="Image1" data-tipo="tooltipViaCompra"
                            title='<%#String.Format("{0} {1}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_MATERIALLST.DESCRIPCION_COMPRA), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_MATERIALLST.DESCRIPCION_ALMACEN))%>'                            
                            ImageUrl='<%#AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Informacion.png")%>' />
                        &nbsp;&nbsp;&nbsp;--%>

                        <asp:Label ID="Label1" runat="server" data-tipo="tooltipViaCompra"
                            title='<%#String.Format("{0} {1}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_MATERIALLST.DESCRIPCION_COMPRA), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_MATERIALLST.DESCRIPCION_ALMACEN))%>'
                            Text='<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_MATERIALLST.CANTIDAD_SOLICITADA_MEDIDA)%>'></asp:Label>

                    </td>
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_MATERIALLST.DISP_ALMACEN_SOLICITUD_MEDIDA)%></td>
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_MATERIALLST.DESC_ESTADO)%></td>
                    <td style="text-align: right;">
                        <%--<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_MATERIALLST.COSTO_PROMEDIO, "{0:n2}")%>--%>


                        <asp:Label ID="Label2" runat="server" data-tipo="tooltipUnitario"
                            title='<%#String.Format("Costo Unitario {0}",CType(Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_MATERIALLST.COSTO_UNITARIO), Double).ToString("N2"))%>'
                            Text='<%#CType(Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_MATERIALLST.COSTO_PROMEDIO), Double).ToString("N2")%>'></asp:Label>



                    </td>
                    <%-- <td style="text-align: center">
                        <asp:Image runat="server" ID="imgDetalle" data-tipo="tooltip"
                            title="<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_MATERIALLST.COSTO_UNITARIO)%>"
                            ImageUrl='<%#AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Informacion.png")%>' />
                    </td>--%>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
    </article>

    <article class="areaCantidadDeRegistro" data-grupo="Listado">
        <asp:Label runat="server" ID="lblMontoTotal" Text=""></asp:Label>
    </article>

    <article class="tituloSeccion">
        Aprobación de Ajuste de Materiales
    </article>

    <asp:UpdatePanel runat="server" ID="upPanelMateriales">

        <ContentTemplate>

            <article class="formulario">
                <table>
                    <tr>
                        <th>Historial de Justificaciones</th>
                        <td>
                            <asp:TextBox runat="server" ID="lblJustficacionMaterial" TextMode="MultiLine" Columns="70" Rows="9" Enabled="false" Style="overflow: scroll"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th>Código</th>
                        <td>
                            <asp:UpdatePanel runat="server" ID="upTxtCodigo" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:TextBox runat="server" ID="txtCodigo" data-tipocontrol="texto" AutoPostBack="true"></asp:TextBox>
                                    <asp:RequiredFieldValidator runat="server" ID="rfvTxtCodigo" ControlToValidate="txtCodigo"
                                        Display="Dynamic" ErrorMessage="El código del material es requerido." ValidationGroup="AgregarListado">&nbsp;</asp:RequiredFieldValidator>
                                    <asp:LinkButton ID="lnkEjecutarBusquedaMaterial" runat="server">
                                <img id="imgBuscarMaterial" title="Buscar Material" alt="Buscar Material" src="" />
                                    </asp:LinkButton>
                                    <br />
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="txtCodigo" EventName="TextChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </article>

            <asp:UpdatePanel runat="server" ID="upControlDatosMaterial" UpdateMode="Conditional" Visible="false">
                <ContentTemplate>
                    <wuc:DatosMaterial Visible="false" ID="WucDatosMaterial" runat="server"></wuc:DatosMaterial>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="txtCodigo" EventName="TextChanged" />
                    <asp:AsyncPostBackTrigger ControlID="ctrl_Materiales" />
                </Triggers>
            </asp:UpdatePanel>

            <article class="areaBotones">
                <asp:Button runat="server" ID="btnAgregar" ValidationGroup="AgregarListado" Text="Agregar" OnClick="btnAgregar_Click" />
                <asp:Button runat="server" ID="btnModificar" Visible="false" Text="Modificar" OnClick="btnModificar_Click" />
                <asp:Button runat="server" ID="btnCancelar" Text="Cancelar" Visible="false" OnClick="btnCancelar_Click" />
            </article>

            <%--         <article class="tituloSeccion" visible="false" id="tituloListado" runat="server">
              
            </article>--%>

            <article class="listado" data-grupo="Listado">
                <asp:Repeater runat="server" ID="rpIngresados">
                    <HeaderTemplate>
                        <table>
                            <tr>
                                <th>Denegar</th>
                                <th>Código</th>
                                <th>Descripción</th>
                                <th>Detalle</th>
                                <th>Disp. Almacen</th>
                                <th>Cantidad Solicitada</th>
                                <th>Monto</th>
                                <th>Vía de compra / Almacen</th>
                                <th>&nbsp;</th>
                                <th>&nbsp;</th>
                                <th>&nbsp;</th>
                                <th>&nbsp;</th>
                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td>
                                <asp:CheckBox runat="server" ID="ibDenegar"
                                    data-fila='<%#Container.ItemIndex%>'
                                    data-disponible='<%#DataBinder.Eval(Container.DataItem, Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_MATERIALLST.DISP_ALMACEN_SOLICITUD_MEDIDA)%>'
                                    data-solicitado='<%#DataBinder.Eval(Container.DataItem, Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_MATERIALLST.CANTIDAD_SOLICITADA_MEDIDA)%>'
                                    data-idMaterial='<%#DataBinder.Eval(Container.DataItem, Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_MATERIALLST.ID_MATERIAL)%>'
                                    data-idDetalleMaterial='<%#DataBinder.Eval(Container.DataItem, Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_MATERIALLST.ID_DETALLE_MATERIAL)%>'
                                    data-idAlmacenBodega='<%#DataBinder.Eval(Container.DataItem, Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_MATERIALLST.ID_ALMACEN_BODEGA)%>'
                                    data-Estado='<%#DataBinder.Eval(Container.DataItem, Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_MATERIALLST.ESTADO)%>'
                                    AutoPostBack="true"
                                    OnCheckedChanged="chkDenegar_CheckedChanged" />
                            </td>
                            <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_MATERIALLST.ID_MATERIAL)%></td>
                            <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_MATERIALLST.DESCRIPCION)%></td>
                            <td style="text-align: center">
                                <img runat="server" id="imgDetalle" class="tooltip" title='<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_MATERIALLST.DETALLE)%>'
                                    src='<%# AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x24, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Informacion.png")%>' />
                            </td>
                            <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_MATERIALLST.DISP_ALM_SOLIC_MEDIDA_RESERV)%></td>
                            <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_MATERIALLST.CANTIDAD_SOLICITADA_MEDIDA)%></td>
                            <td>
                                <asp:Label ID="Label2" runat="server" data-tipo="tooltipUnitario"
                                    title='<%#String.Format("Costo Unitario {0}",CType(Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_MATERIALLST.COSTO_UNITARIO), Double).ToString("N2"))%>'
                                    Text='<%#CType(Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_MATERIALLST.COSTO_PROMEDIO), Double).ToString("N2")%>'></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList runat="server"
                                    data-idMaterial='<%#DataBinder.Eval(Container.DataItem, Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_MATERIALLST.ID_MATERIAL)%>'
                                    data-idUbicacion='<%#DataBinder.Eval(Container.DataItem, Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_MATERIALLST.ID_UBICACION_ADMINISTRA)%>'
                                    data-IdAlmacenBodegaCombo='<%#DataBinder.Eval(Container.DataItem, Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_MATERIALLST.ID_ALMACEN_BODEGA)%>'
                                    data-IdViaCompraContratoCombo='<%#DataBinder.Eval(Container.DataItem, Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_MATERIALLST.ID_VIA_COMPRA_CONTRATO)%>'
                                    data-IdViaCompraCombo='<%#DataBinder.Eval(Container.DataItem, Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_MATERIALLST.VIA_DESPACHO)%>'
                                    data-idDetalleMaterial='<%#DataBinder.Eval(Container.DataItem, Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_MATERIALLST.ID_DETALLE_MATERIAL)%>'
                                    Width="80%"
                                    ID="ibAlmacen"
                                    OnSelectedIndexChanged="ibAlmacen_SelectedIndexChanged"
                                    AutoPostBack="true"
                                    AppendDataBoundItems="true">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:ImageButton runat="server" data-tipo="modificarRegistro" ID="ibModificar" AlternateText="Modificar el pedido"
                                    CommandArgument='<%# String.Format("{0}¬{1}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_MATERIALLST.ID_MATERIAL),Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_MATERIALLST.ID_DETALLE_MATERIAL))%>'
                                    OnClick="ibModificar_Click"
                                    ImageUrl='<%#AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Editar.png")%>'
                                    onmouseover='<%# String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Icono_Editar.png"))%>'
                                    onmouseout='<%# String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Editar.png"))%>'
                                    Visible='<%#IIf(Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_MATERIALLST.ESTADO).ToString = Utilerias.OrdenesDeTrabajo.EstadoRegistro.DENEGADA, False, True)%>' />
                            </td>
                            <td>
                                <asp:ImageButton runat="server" ID="ibBorrar" AlternateText="Borrar el pedido" data-tipo="borrarRegistro"
                                    CommandArgument="<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_MATERIALLST.ID_DETALLE_MATERIAL)%>"
                                    data-descripcion='<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_MATERIALLST.DESCRIPCION)%>'
                                    OnClick="ibBorrar_Click"
                                    ImageUrl='<%#AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Borrar.png")%>'
                                    onmouseover='<%# String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Icono_Borrar.png"))%>'
                                    onmouseout='<%# String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Borrar.png"))%>'
                                    Visible='<%#IIf(Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_MATERIALLST.ESTADO).ToString = Utilerias.OrdenesDeTrabajo.EstadoRegistro.DENEGADA, False, True)%>' />
                            </td>
                            <td>
                                <asp:HiddenField runat="server" ID="hdfAlmacen" Value='<%#String.Format("{0}_{1}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_MATERIALLST.ID_ALMACEN_BODEGA), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_MATERIALLST.VIA_DESPACHO))%>' />
                            </td>
                            <td>
                                <asp:HiddenField runat="server" ID="hdfDetalleMaterial" Value='<%#String.Format("{0}_{1}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_MATERIALLST.ID_VIA_COMPRA_CONTRATO), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_MATERIALLST.VIA_DESPACHO))%>' />
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
            </article>

            <article class="areaCantidadDeRegistro" data-grupo="Listado">
                <asp:Label runat="server" ID="lblMontoTotalPendientes" Text=""></asp:Label>
            </article>

        </ContentTemplate>
    </asp:UpdatePanel>

    <br />

    <article class="areaBotones">
        <asp:Button runat="server" ID="btnAceptar" ValidationGroup="Aceptar" Text="Aceptar" OnClick="btnAceptar_Click" />
        <input type="button" id="btnRegresar" value="Regresar" />
    </article>

    <article id="arAlerta"></article>
    <article id="popupConfirmacionDeseaBorrar"></article>
    <article id="arAlertasDelFormulario"></article>
    <article id="arpopupConfirmaDeseaBorrar"></article>

    <%--Popup para búsqueda de materiales--%>
    <article id="PopUpBusquedaMateriales" class="ventanaEmergente">
        <article class="formulario" style="width: 850px!important;">
            <a href="#CerrarPopUpBusquedaMateriales" title="Cerrar Ventana" class="botonCerrarVentanaEmergente">X</a>
            <br />
            <asp:UpdatePanel ID="upContactoConv" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <wuc:Materiales ID="ctrl_Materiales" runat="server"></wuc:Materiales>
                    <div class="areaBotones">
                        <a href="#CerrarPopUpBusquedaMateriales" title="Regresar">Regresar</a>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </article>
    </article>
    <%--Fin Popup para búsqueda de materiales--%>

    <script type="text/javascript">

        function InhabilitarCodigo() {

            document.getElementById("imgBuscarMaterial").style.visibility = "hidden";

        };

        function HabilitarCodigo() {

            document.getElementById("imgBuscarMaterial").style.visibility = "visible";

        };


        function mostrarAlertaRegistroBorrado() {
            mostrarAlerta(
                '#arAlerta',
                {
                    mensaje: "Se ha borrado el detalle material",
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
                    mensaje: "No ha sido posible borrar el detalle material seleccionado",
                    tipo: "advertencia",
                    transparencia: 0.9,
                    posicion: 'center'
                }
            );
        }

        String.prototype.format = function () {
            var str = this;
            for (var i = 0; i < arguments.length; i++) {
                var reg = new RegExp("\\{" + i + "\\}", "gm");
                str = str.replace(reg, arguments[i]);
            }
            return str;
        };

        function mostrarPopupConfirmacionDeseaBorrarRegistro(pvc_UniqueIdControl, pvc_Descripcion) {

            var vlo_ConfiguracionPopup = {
                titulo: "<em>Catalogo de requisición de materiales</em>",
                mensaje: "¿Está seguro de eliminar el material {0} del listado?".format(pvc_Descripcion),
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

        function mostrarPopUp(pvc_IdPopup) {
            window.location = pvc_IdPopup;
        };

        function ocultarFiltroMateriales() {
            window.location = '#CerrarPopUpBusquedaMateriales';
        };

        function cargarTooltip() {

            $('[data-tipo="tooltip"]').each(function () {
                $('#' + this.id).tooltipster({
                    interactive: true,
                    interactiveTolerance: 400,
                    theme: 'tooltipster-light'
                });
            });
            $('[data-tipo="tooltipViaCompra"]').each(function () {
                $('#' + this.id).tooltipster({
                    interactive: true,
                    interactiveTolerance: 400,
                    theme: 'tooltipster-light'
                });
            });

            $('[data-tipo="tooltipUnitario"]').each(function () {
                $('#' + this.id).tooltipster({
                    interactive: true,
                    interactiveTolerance: 400,
                    theme: 'tooltipster-light'
                });
            });

            $('[data-tipo="borrarRegistro"]').click(function () { return mostrarPopupConfirmacionDeseaBorrarRegistro($(this).data("uniqueid")); });
        };

        function inicializarFormulario() {

            $('#btnRegresar').click(function () {
                regresarAlListado();
            });

            $('[data-tipo="borrarRegistro"]').click(function () { return mostrarPopupConfirmacionDeseaBorrarRegistro($(this).data("uniqueid"), $(this).data("descripcion")); });

            $(window).keydown(function a(e) {

                if (e.keyCode == 13) {
                    return false;
                }
            });

            cargarLupa();

            permutarImagenes('#imgBuscarMaterial',
               '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Lupa.png")%>',
                '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Lupa.png")%>',
                '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Lupa.png")%>'
            );

            $('[data-tipo="tooltipViaCompra"]').each(function () {
                $('#' + this.id).tooltipster({
                    interactive: true,
                    interactiveTolerance: 400,
                    theme: 'tooltipster-light'
                });
            });

            $('[data-tipo="tooltip"]').each(function () {
                $('#' + this.id).tooltipster({
                    interactive: true,
                    interactiveTolerance: 400,
                    theme: 'tooltipster-light'
                });
            });

            $('[data-tipo="tooltipUnitario"]').each(function () {
                $('#' + this.id).tooltipster({
                    interactive: true,
                    interactiveTolerance: 400,
                    theme: 'tooltipster-light'
                });
            });
        };

        function regresarAlListado() {
            window.location = 'Lst_OT_AprobacionAjusteMateriales.aspx';
        };

        $(document).ready(function () {
            inicializarFormulario();
        });

        function ocultarFiltroFuncionario() {
            window.location = '#CerrarPopUpBusquedaFuncionario';
        };

        function cargarLupa() {
            permutarImagenes('#imgBuscarMaterial',
                '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Lupa.png")%>',
                '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Lupa.png")%>',
                '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Lupa.png")%>'
            );

            permutarImagenes('#imgBuscarMaterial',
               '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Lupa.png")%>',
                '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Lupa.png")%>',
                '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Lupa.png")%>'
            );
        };

        function mostrarAlertSinCantidadDisponible() {
            mostrarAlerta(
                '#arAlertasDelFormulario',
                {
                    mensaje: "La bodega o almacén seleccionado no posee suficiente material",
                    tipo: "advertencia",
                    transparencia: 0.9,
                    posicion: 'center',
                    permiteCerrar: true
                }
                );
        };

        function mostrarAlertaGuardadoExitoso() {
            deshabilitarControl('#<%=btnAceptar.ClientID%>');
            deshabilitarControl('#<%=btnAgregar.ClientID%>');
            $('.formulario').attr('disabled', 'disabled');

            mostrarAlerta(
            '#arAlertasDelFormulario',
            {
                mensaje: 'Se ha guardado los datos indicados para la requisición',
                tipo: "exito",
                transparencia: 0.9,
                posicion: 'center',
                onClosed: function () { regresarAlListado(); }
            });
        };

        function mostrarAlertaNoEncontrado() {
            mostrarAlerta(
                '#arAlertasDelFormulario',
                {
                    mensaje: "No se han encontrado funcionarios con el número de cédula indicado",
                    tipo: "advertencia",
                    transparencia: 0.9,
                    posicion: 'center',
                    permiteCerrar: true
                }
                );
        };

        function mostrarAlertaError(pvc_Mensaje) {
            mostrarAlerta(
                '#arAlertasDelFormulario',
                {
                    mensaje: pvc_Mensaje,
                    tipo: 'peligro',
                    transparencia: 1,
                    posicion: 'center',
                    permiteCerrar: true
                }
            );
        };
    </script>

</asp:Content>

