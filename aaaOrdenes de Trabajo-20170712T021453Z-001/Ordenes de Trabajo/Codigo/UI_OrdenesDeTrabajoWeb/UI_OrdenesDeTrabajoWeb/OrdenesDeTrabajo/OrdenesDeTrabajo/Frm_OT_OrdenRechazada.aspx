<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/Mp_Formulario.master" AutoEventWireup="false" CodeFile="Frm_OT_OrdenRechazada.aspx.vb" Inherits="OrdenesDeTrabajo_Frm_OT_OrdenRechazada" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphFormulario" runat="Server">

    <header>
        <h2>
            <asp:Label runat="server" ID="lblAccion" Text="Rechazo de Orden de Trabajo"></asp:Label>
        </h2>
    </header>

    <article class="tituloSeccion">
        Rechazo de Orden de Trabajo
    </article>

    <article class="formulario">
        <table>
            <tr>
                <th>No. de Orden de Trabajo</th>
                <td>
                    <asp:Label runat="server" ID="lblNumeroOt"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>No. de Orden de Trabajo en PDAGO</th>
                <td>
                    <asp:Label runat="server" ID="lblNumeroOtPDAGO"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>Descripción del Trabajo</th>
                <td>
                    <asp:TextBox runat="server" ID="txtDescTrabajo" TextMode="MultiLine" Rows="5" Width="59%" ReadOnly="true"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>Motivo de Rechazo</th>
                <td>
                    <asp:DropDownList Width="59%" runat="server" ID="ddlMotivoRechazo" AppendDataBoundItems="true"></asp:DropDownList>
                    <asp:RequiredFieldValidator runat="server" ID="rfvDdlMotivoRechazo" ControlToValidate="ddlMotivoRechazo" Display="Dynamic" ErrorMessage="El motivo de rechazo es obligatorio." ValidationGroup="Aceptar"></asp:RequiredFieldValidator>
                </td>
            </tr>
        </table>
    </article>

    <article class="areaBotones">
        <asp:Button runat="server" ID="btnAceptar" Text="Aceptar" ValidationGroup="Aceptar" />
        <%--<input id="btnLimpiarFormulario" type="button" data-tipo="limpiarFormulario" value="Limpiar formulario" />--%>
        <input id="btnCancelar" type="button" value="Cancelar" />
    </article>

    <article id="arPopupDelFormulario"></article>
    <article id="arAlertasDelFormulario"></article>
    <article id="arPopupGenerico"></article>

    <script type="text/javascript">

        $(document).ready(function () {
            $('#btnCancelar').click(function () {
                regresarAlListado();
            });

        });

        function mostrarAlertaNoEncontrado() {
            mostrarAlerta(
                '#arAlertasDelFormulario',
                {
                    mensaje: "No se han encontrado regitro con el las llaves indicadas",
                    tipo: "advertencia",
                    transparencia: 0.9,
                    posicion: 'center',
                    permiteCerrar: true
                }
                );
        };

        function regresarAlListado() {
            window.location = 'Lst_OT_GestionOrdenTrabajoCordinadorSectorTaller.aspx';
        };

        function mostrarPopUp(pvc_IdPopup) {
            window.location = pvc_IdPopup;
        };

        function mostrarAlertaActualizacionExitosa() {
            deshabilitarControl('#<%=btnAceptar.ClientID%>');
            //deshabilitarControl('#btnLimpiarFormulario');
            deshabilitarControl('#btnCancelar');
            $('.formulario').attr('disabled', 'disabled');

            mostrarAlerta(
                '#arAlertasDelFormulario',
                {
                    mensaje: 'Se ha actualizado la información.',
                    tipo: "exito",
                    transparencia: 0.9,
                    posicion: 'center',
                    onClosed: function () { regresarAlListado(); }
                });
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

        function mostrarAlertaLlaveIncorrecta() {
            deshabilitarControl('#<%=btnAceptar.ClientID%>');
            //deshabilitarControl('#btnLimpiarFormulario');
            deshabilitarControl('#btnCancelar');
            $('.formulario').attr('disabled', 'disabled');

            mostrarAlerta(
                '#arAlertasDelFormulario',
                {
                    mensaje: 'El identificador provisto no pertenece a ningun registro del sistema.',
                    tipo: "peligro",
                    transparencia: 0.9,
                    posicion: 'center',
                    onClosed: function () { regresarAlListado(); }
                });
        };

    </script>

</asp:Content>

