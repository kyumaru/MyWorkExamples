<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/MasterPage/Mp_Formulario.master" CodeFile="Rpt_OT_AlertasTiempoAtencion.aspx.vb" Inherits="Reportes_Rpt_OT_AlertasTiempoAtencion" %>

<asp:Content runat="server" ID="Content1" ContentPlaceHolderID="cphFormulario">

    <header>
        <h2>
            Reporte de alertas de tiempo de atención
        </h2>
    </header>

    <article class="tituloSeccion">
        Area de filtros
    </article>

    <article data-grupo="FiltrosDeBusqueda" class="formulario">
        <table>
            <tr>
                <th>Taller o Sector</th>
                <td>
                    <asp:DropDownList Width="65%" runat="server" ID="ddlFiltroTallerSector"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <th>Estado de la orden de trabajo</th>
                <td>
                    <asp:DropDownList Width="65%" runat="server" ID="ddlFiltroEstado" data-tipocontrol="combo" AppendDataBoundItems="true"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <th>Fecha desde</th>
                <td>
                    <asp:TextBox runat="server" ID="txtFiltroFechaDesde" Width="145px" data-tipocontrol="texto"></asp:TextBox>
                    <asp:CompareValidator runat="server" ID="cmpvtxtFiltroDesde" ControlToValidate="txtFiltroFechaDesde" Display="Dynamic" ErrorMessage="Fecha desde inválida" Operator="DataTypeCheck" Type="Date" ValidationGroup="Grupo7">&nbsp;</asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <th>Fecha hasta</th>
                <td>
                    <asp:TextBox runat="server" ID="txtFiltroFechaHasta" Width="145px" data-tipocontrol="texto"></asp:TextBox>
                    <asp:CompareValidator runat="server" ID="cmpvtxtFiltroHasta" ControlToValidate="txtFiltroFechaHasta" Display="Dynamic" ErrorMessage="Fecha hasta inválida" Operator="DataTypeCheck" Type="Date" ValidationGroup="Grupo7">&nbsp;</asp:CompareValidator>
                </td>
                <td></td>
            </tr>
            <tr>
                <th>Categoría</th>
                <td>
                    <asp:DropDownList Width="65%" runat="server" ID="ddlFiltroCategoria" data-tipocontrol="combo" AppendDataBoundItems="true" AutoPostBack="true"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <th>Actividad</th>
                <td>
                    <asp:DropDownList Width="65%" runat="server" ID="ddlFiltroActividad" data-tipocontrol="combo" AppendDataBoundItems="true" AutoPostBack="true"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <th>Tipo de orden de trabajo</th>
                <td>
                    <asp:DropDownList Width="65%" runat="server" ID="ddlFiltroTipoOrden" data-tipocontrol="combo" AppendDataBoundItems="true" AutoPostBack="true"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <th>Edificio o Sitio</th>
                <td>
                    <asp:DropDownList Width="65%" runat="server" ID="ddlFiltroLugarTrabajo" data-tipocontrol="combo" AutoPostBack="true" AppendDataBoundItems="true"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <th>Ordenamiento</th>
                <td>
                    <asp:RadioButton ID="rbtFecha" runat="server" Text="Fecha de Asignación" GroupName="Orden" AutoPostBack="true" Checked="true" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:RadioButton ID="rbtEstado" runat="server" Text="Estado" GroupName="Orden" AutoPostBack="true"/>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:RadioButton ID="rbtNumOt" runat="server" Text="Número de OT" GroupName="Orden" AutoPostBack="true" />
                </td>
            </tr>

            <tr>
                <th>Tipo de categorías</th>
                <td>
                    <article style="display: <%#IIf(Roles.IsUserInRole(Membership.GetUser.UserName, Utilerias.OrdenesDeTrabajo.RolesSistema.OT_SUPERVISOR) And Me.DisYMantenimiento, "none", "block")%>">
                            <asp:RadioButton ID="rbtMantenimiento" runat="server" Text="Mantenimiento" GroupName="Condicion" AutoPostBack="true" Checked="true" />
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:RadioButton ID="rbtDisenio" runat="server" Text="Diseño" GroupName="Condicion" AutoPostBack="true"/>
                    </article>
                    
                </td>
            </tr>

        </table>
    </article>

    <article data-grupo="FiltrosDeBusqueda" class="areaBotones">
        <asp:Button runat="server" ID="btnGenerar" Text="Generar Reporte" />
        <input id="btnCancelar" type="button" data-tipo="cancelarBusqueda" value="Cancelar" />
    </article>

    <article id="arPopupGenerico"></article>

    <script type="text/javascript">
        /*DatePicker con Rango*/
        function establecerMinyMaxDate() {
            if (document.getElementById('<%=txtFiltroFechaHasta.ClientID%>'))
                establecerFechaMaximaDatePicker("#<%=Me.txtFiltroFechaDesde.ClientID%>", document.getElementById('<%=txtFiltroFechaHasta.ClientID%>').value);

            if (document.getElementById('<%=txtFiltroFechaDesde.ClientID%>'))
                establecerFechaMinimaDatePicker("#<%=Me.txtFiltroFechaHasta.ClientID%>", document.getElementById('<%=txtFiltroFechaDesde.ClientID%>').value);

        };
        function regresarAPrincipal() {
            window.location = '../Genericos/Frm_MenuPrincipal.aspx';
        };

        $(document).ready(function () {
            $('#btnCancelar').click(function () {
                regresarAPrincipal();
            });

            configurarDatePickerRango("#<%=Me.txtFiltroFechaDesde.ClientID%>", "#<%=Me.txtFiltroFechaHasta.ClientID%>");
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