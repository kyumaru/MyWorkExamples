<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Frm_OT_RetiroMateriales.aspx.vb" MasterPageFile="~/MasterPage/Mp_Formulario.master" Inherits="OrdenesDeTrabajo_Almacen_Frm_OT_" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register TagPrefix="wuc" TagName="InformacionGeneral" Src="~/ControlesDeUsuario/Wuc_OT_InformacionGeneral.ascx" %>

<asp:Content runat="server" ID="Content1" ContentPlaceHolderID="cphFormulario">

    <header>
        <h2>Verificación de retiro de materiales</h2>
    </header>
    <article class="tituloSeccion">Información general de la OT</article>
    <article>
        <wuc:InformacionGeneral ID="ctrl_InfoGeneral" runat="server" />
    </article>

    <article class="tituloSeccion">
        Materiales Solicitados
    </article>

    <article class="listado" data-grupo="Listado">
        <asp:Repeater runat="server" ID="rpPedidos">
            <HeaderTemplate>
                <table>
                    <tr>
                        <th>
                            <asp:LinkButton runat="server" ID="lnkCodigo" Text="Código"></asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton runat="server" ID="lnkDescripcion" Text="Descripción"></asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton runat="server" ID="lnkAlmacenBodega" Text="Almacén/Bodega"></asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton runat="server" ID="lnCantidadSolicitada" Text="Cantidad Solicitada"></asp:LinkButton>
                        </th>
                         <th>
                            <asp:LinkButton runat="server" ID="LinkButton1" Text="Cantidad Retirada"></asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton runat="server" ID="lnkCantidadDisponible" Text="Cantidad a Retirar"></asp:LinkButton>
                        </th>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_RETIROLST.CODIGO)%></td>
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_RETIROLST.DESCRIPCION)%></td>
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_RETIROLST.ALMACEN_BODEGA)%></td>
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_RETIROLST.CANTIDAD_SOLICITADA_MEDIDA)%></td>
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_RETIROLST.CANTIDAD_RETIRADA)%></td>
                    <td>
                        <asp:TextBox runat="server" ID="txtCantidad" Width="54%" 
                            Enabled='<%#IIf(CType(Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_RETIROLST.ESTADO), String) = "PEN", True, False)%>'
                            data-idDetalleMaterial='<%#DataBinder.Eval(Container.DataItem, Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_RETIROLST.ID_DETALLE_MATERIAL)%>'
                            data-idMaterial='<%#DataBinder.Eval(Container.DataItem, Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_RETIROLST.CODIGO)%>'
                            data-idAlmacenBodega='<%#DataBinder.Eval(Container.DataItem, Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_RETIROLST.ID_ALMACEN_BODEGA)%>'
                            data-idUbicacion='<%#DataBinder.Eval(Container.DataItem, Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_RETIROLST.ID_UBICACION)%>'
                            data-CantidadSolicitada='<%#DataBinder.Eval(Container.DataItem, Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_RETIROLST.CANTIDAD_SOLICITADA)%>'
                            data-CantidadRetirada='<%#DataBinder.Eval(Container.DataItem, Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_RETIROLST.CANTIDAD_RETIRADA)%>'
                            data-SolicitudRetiro='<%#DataBinder.Eval(Container.DataItem, Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_RETIROLST.ID_SOLICITUD_RETIRO)%>'
                            data-Anno='<%#DataBinder.Eval(Container.DataItem, Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_RETIROLST.ANNO)%>'>
                        </asp:TextBox>
                        <ajax:FilteredTextBoxExtender ID="ftbTxtCantidad" runat="server" TargetControlID="txtCantidad" FilterType="Numbers, Custom" FilterMode="ValidChars" ValidChars="."></ajax:FilteredTextBoxExtender>
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

    <article class="formulario"></article>

    <article id="ControlDeIngresoAlSistema">
        <article>
            <table style="margin: 0 auto;">
                <tr>
                    <th>Usuario</th>
                    <td>
                        <asp:TextBox runat="server" ID="txtUsuario"></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" ID="rfvTxtUsuario" ControlToValidate="txtUsuario" Display="Dynamic" ErrorMessage="Debe indicar el Usuario." ForeColor="#9D240C">&nbsp;</asp:RequiredFieldValidator>
                        <img id="imgTooltipTxtUsuario" src="<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Icono_Ayuda.png")%>" title="Indique el nombre de usuario del portal universitario, no debe incluir @ucr.ac.cr." />
                    </td>
                </tr>
                <tr>
                    <th>Clave</th>
                    <td>
                        <asp:TextBox runat="server" ID="txtClave" TextMode="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" ID="rfvTxtClave" ControlToValidate="txtClave" Display="Dynamic" ErrorMessage="Debe indicar la clave." ForeColor="#9D240C">&nbsp;</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:ValidationSummary runat="server" ID="vsDetalleErrores" DisplayMode="BulletList" />
                        <asp:BulletedList runat="server" ID="blError"></asp:BulletedList>
                    </td>
                </tr>
            </table>
            <article class="areaBotones">
                <asp:Button runat="server" ID="btnAceptar" ValidationGroup="Aceptar" Text="Aceptar" OnClientClick="javascript:return validarIngreso();" />
                <input type="button" id="btnCancelar" value="Cancelar" />
            </article>
        </article>
    </article>

    <article id="popupConfirmacionDeseaBorrar"></article>
    <article id="arAlertasDelFormulario"></article>

    <script type="text/javascript">


        function mostrarPopUp(pvc_IdPopup) {
            window.location = pvc_IdPopup;
        };

        function ocultarFiltroMateriales() {
            window.location = '#CerrarPopUpBusquedaMateriales';
        };


        function regresarAlListado() {
            window.location = 'Lst_OT_RetiroMateriales.aspx';
        };
        function validarIngreso() {
            Page_ClientValidate();
            marcarControlesInvalidos();
            return Page_IsValid;
        };


        $(document).ready(function () {

            ocultarControl('#advertenciaJavaScript');
            habilitarTooltipPorControl('#imgTooltipTxtUsuario');
            configurarBotonPorDefecto('<%=btnAceptar.ClientID%>');
             window.location = "#ControlDeIngresoAlSistema";

             $('#btnCancelar').click(function () {
                 regresarAlListado();
             });

             $('[data-tipo="borrarRegistro"]').click(function () { return mostrarPopupConfirmacionDeseaBorrarRegistro($(this).data("uniqueid")); });
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

         function mostrarAlertaGuardadoExitoso() {
             deshabilitarControl('#<%=btnAceptar.ClientID%>');
             $('.formulario').attr('disabled', 'disabled');

             mostrarAlerta(
             '#arAlertasDelFormulario',
             {
                 mensaje: 'Operación realizada satisfactoriamente.',
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

