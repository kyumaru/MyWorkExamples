<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/MasterPage/Mp_Formulario.master" CodeFile="Frm_OT_GestionMateriales.aspx.vb" Inherits="OrdenesDeTrabajo_Almacen_Frm_OT_GestionMateriales" %>

<%@ Register TagPrefix="wuc" TagName="InformacionGeneral" Src="~/ControlesDeUsuario/Wuc_OT_InformacionGeneral.ascx" %>
<%@ Register TagPrefix="wuc" TagName="GestionMaterial" Src="~/Controles/wuc_OT_GestionMaterial.ascx" %>

<asp:Content runat="server" ID="Content1" ContentPlaceHolderID="cphFormulario">

    <header>
        <h2>Gestión de Materiales</h2>
    </header>

    <article>
        <wuc:InformacionGeneral ID="ctrl_InfoGeneral" runat="server" />
    </article>

    <article class="tituloSeccion">
        Materiales Solicitados
    </article>
    <br />
    <article style="float: right">
        <b>
            <asp:LinkButton runat="server" ID="lnkMaterialesAdicionales" Text="Solicitar Materiales Adicionales" OnClick="lnkMaterialesAdicionales_Click"></asp:LinkButton>
        </b>
    </article>
    <br />
    <article class="listado" data-grupo="Listado">
        <asp:Repeater runat="server" ID="rpPedidos">
            <HeaderTemplate>
                <table>
                    <tr>
                        <th>
                            <asp:Label runat="server" ID="lnkCodigo" Text="Código"></asp:Label>
                        </th>
                        <th>
                            <asp:Label runat="server" ID="lnkDescripcion" Text="Descripción"></asp:Label>
                        </th>
                        <th>
                            <asp:Label runat="server" ID="lnCantidadSolicitada" Text="Cantidad Solicitada"></asp:Label>
                        </th>
                        <th>
                            <asp:Label runat="server" ID="lnkCantidadDisponible" Text="Cantidad Disponible"></asp:Label>
                        </th>
                        <th>
                            <asp:Label runat="server" ID="lblViaCompra" Text="En proceso de compra"></asp:Label>
                        </th>
                        <th>
                            <asp:Label runat="server" ID="lnkCantidadASolicitar" Text="Cantidad a solicitar"></asp:Label>
                        </th>
                        <th>
                            <asp:Label runat="server" ID="lnkEstado" Text="Estado"></asp:Label>
                        </th>
                        <th style="text-align: center;">
                            <asp:Label runat="server" ID="lnkMonto" Text="Monto de la solicitud"></asp:Label>
                        </th>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GESTION_MATERIAL.ID_MATERIAL)%></td>
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GESTION_MATERIAL.DESCRIPCION)%></td>
                    <td><%#String.Format("{0} {1}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GESTION_MATERIAL.CANTIDAD_SOLICITADA), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GESTION_MATERIAL.UNIDAD_DESCRIPCION))%></td>
                    <td>

                        <article style="display: <%#IIf((CType(Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GESTION_MATERIAL.ESTADO), String) <> Utilerias.OrdenesDeTrabajo.EstadoRevision.DENEGADA), "block", "none")%>">
                            <%#String.Format("{0} {1}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GESTION_MATERIAL.DISPONIBLE), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GESTION_MATERIAL.UNIDAD_DESCRIPCION))%>
                        </article>

                        <article style="display: <%#IIf((CType(Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GESTION_MATERIAL.ESTADO), String) = Utilerias.OrdenesDeTrabajo.EstadoRevision.DENEGADA), "block", "none")%>">

                            <%#String.Format("0 {0}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GESTION_MATERIAL.UNIDAD_DESCRIPCION))%>
                        </article>



                    </td>
                    <td>
                        <asp:Label ID="Label1" runat="server" data-tipo="tooltipViaCompra"
                            title='<%#String.Format("{0} {1}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GESTION_MATERIAL.DESCRIPCION_COMPRA), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GESTION_MATERIAL.DESCRIPCION_ALMACEN))%>'
                            Text='<%#String.Format("{0} {1}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GESTION_MATERIAL.PENDIENTE_COMPRA), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GESTION_MATERIAL.UNIDAD_DESCRIPCION))%>'></asp:Label>

                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtCantidad" Width="54%" Text='<%#String.Format("{0}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GESTION_MATERIAL.DISPONIBLE_RETIRO))%>'
                            Visible='<%#IIf((Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GESTION_MATERIAL.ESTADO).ToString <> Utilerias.OrdenesDeTrabajo.EstadoRegistro.PENDIENTE_APROBACION.ToString) And 
                                            (Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GESTION_MATERIAL.ESTADO).ToString <> Utilerias.OrdenesDeTrabajo.EstadoRegistro.DENEGADA.ToString) And 
                                            (Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GESTION_MATERIAL.PENDIENTE_COMPRA).ToString = "0"), True, False)%>'
                            data-disponible='<%#DataBinder.Eval(Container.DataItem, Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GESTION_MATERIAL.DISPONIBLE_RETIRO)%>'
                            data-idDetalleMaterial='<%#DataBinder.Eval(Container.DataItem, Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GESTION_MATERIAL.ID_DETALLE_MATERIAL)%>'
                            data-costoPromedio='<%#DataBinder.Eval(Container.DataItem, Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GESTION_MATERIAL.COSTO_PROMEDIO)%>'
                            data-idMaterial='<%#DataBinder.Eval(Container.DataItem, Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GESTION_MATERIAL.ID_MATERIAL)%>'
                            data-idAlmacenBodega='<%#DataBinder.Eval(Container.DataItem, Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GESTION_MATERIAL.ID_ALMACEN_BODEGA)%>'
                            data-estado='<%#DataBinder.Eval(Container.DataItem, Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GESTION_MATERIAL.ESTADO)%>'>
                        </asp:TextBox>
                    </td>
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GESTION_MATERIAL.DESC_ESTADO)%></td>
                    <td style="text-align: right;">
                        <asp:Label ID="Label2" runat="server" data-tipo="tooltip"
                            title='<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GESTION_MATERIAL.COSTO_PROMEDIO, "{0:n2}")%>'
                            Text='<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GESTION_MATERIAL.COSTO_PROMEDIO_TOTAL, "{0:n2}")%>'></asp:Label>
                    </td>
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

    <article class="formulario">
        <table>
            <tr>
                <th>Fecha de Retiro: 
                </th>
                <td style="width: 30%;">
                    <asp:TextBox ID="txtFechaRetiro" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvTxtFechaRetiro" runat="server" ErrorMessage="Seleccione una fecha de retiro para su material." ValidationGroup="Tramitar" ControlToValidate="txtFechaRetiro"></asp:RequiredFieldValidator>
                </td>
                <td>
                    <asp:RadioButtonList ID="rbtnTurno" runat="server">
                        <asp:ListItem Value='1'>Mañana</asp:ListItem>
                        <asp:ListItem Value="0">Tarde</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator runat="server" ValidationGroup="Tramitar" ID="rfvTurno" ControlToValidate="rbtnTurno" Display="Dynamic" ErrorMessage="Jornada es requerida.">&nbsp;</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <article class="areaBotones" style="float: right">
                        <asp:Button runat="server" ID="btnTramitar" ValidationGroup="Tramitar" Text="Tramitar solicitud de materiales" OnClick="btnTramitar_Click" />
                    </article>
                </td>
            </tr>
        </table>
    </article>

    <article class="tituloSeccion">
        Solicitudes de salida
    </article>

    <asp:UpdatePanel runat="server" ID="upTabs" UpdateMode="Conditional">
        <ContentTemplate>

            <article class="formulario">
                <ul class="encabezadoTabPanel">
                    <asp:Repeater runat="server" ID="rpListaTapsTitulos">
                        <ItemTemplate>
                            <li runat="server" id="liEncabezado">
                                <a runat="server" class="tituloTabPanel" id="cuerpoTabPanel"><%#String.Format("Solicitud {0}", Container.ItemIndex + 1)%></a>
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>

                <article class="cuerpoTabPanel">
                    <asp:Repeater runat="server" ID="rpListaTapsContenidos">
                        <ItemTemplate>
                            <article runat="server" class="tabPanel" id="cuerpoTabPanel">
                                <wuc:GestionMaterial runat="server" ID="wuc_GestionMaterial" SolicitudRetiro='<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_SOLICITUD_RETIROLST.ID_SOLICITUD_RETIRO)%>'
                                    Anno='<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_SOLICITUD_RETIROLST.ANNO)%>'
                                    FechaRetiro='<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_SOLICITUD_RETIROLST.FECHA_HORA_RETIRO)%>'
                                    JornadaRetiro='<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_SOLICITUD_RETIROLST.JORNADA_RETIRO)%>' />

                            </article>
                        </ItemTemplate>
                    </asp:Repeater>
                </article>
            </article>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="rpListaTapsContenidos" />
        </Triggers>
    </asp:UpdatePanel>
    <article class="areaBotones">
        <input type="button" value="Regresar" id="btnRegresar" />
    </article>

    <article id="popupConfirmacionDeseaBorrar"></article>
    <article id="arAlertasDelFormulario"></article>

    <script type="text/javascript">

        function ValidateRadio(sender, args) {
            args.IsValid = $("[name=Turno]:checked").length > 0;
        }

        function mostrarPopUp(pvc_IdPopup) {
            window.location = pvc_IdPopup;
        };

        function ocultarFiltroMateriales() {
            window.location = '#CerrarPopUpBusquedaMateriales';
        };

        function regresarAlListado() {
            window.location = '../OrdenesDeTrabajo/Lst_OT_GestionOrdenTrabajoCordinadorSectorTaller.aspx';
        };

        $(document).ready(function () {

            $('#btnRegresar').click(function () {
                regresarAlListado();
            });

            configurarDatePicker('#<%=Me.txtFechaRetiro.ClientID%>');
            establecerFechaMinimaDatePicker('#<%=Me.txtFechaRetiro.ClientID%>', new Date());
            /*Control TabPanel*/
            configurarTabPanel();

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
        });

        function ocultarFiltroFuncionario() {
            window.location = '#CerrarPopUpBusquedaFuncionario';
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

