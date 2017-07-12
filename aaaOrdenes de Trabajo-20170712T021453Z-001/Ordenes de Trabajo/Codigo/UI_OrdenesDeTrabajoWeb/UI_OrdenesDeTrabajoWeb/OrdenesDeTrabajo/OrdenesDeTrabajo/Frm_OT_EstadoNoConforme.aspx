<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/Mp_Formulario.master" AutoEventWireup="false" CodeFile="Frm_OT_EstadoNoConforme.aspx.vb" Inherits="OrdenesDeTrabajo_Frm_OT_EstadoNoConforme" %>

<%@ Register TagPrefix="wuc" Namespace="Utilerias.WebForms.Controles" Assembly="Utilerias.WebForms.Controles" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphFormulario" runat="Server">

    <header>
        <h2>Historial</h2>
    </header>

    <article class="tituloSeccion">
        Datos de la Orden de Trabajo
    </article>

    <article class="formulario">
        <table>
            <tr>
                <th>N° Orden Trabajo</th>
                <td>
                    <asp:Label runat="server" ID="lblNumeroOrden"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>N° de Orden de Trabajo en PDAGO</th>
                <td>
                    <asp:Label runat="server" ID="lblPdago"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>Edificio</th>
                <td>
                    <asp:Label runat="server" ID="lblEdificio"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>Descripción de Trabajo</th>
                <td>
                    <asp:Label runat="server" ID="lblDescripcion" Width="600px"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>Categoría</th>
                <td>
                    <asp:Label runat="server" ID="lblCategoria"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>Actividad</th>
                <td>
                    <asp:Label runat="server" ID="lblActivididad"></asp:Label>
                </td>
            </tr>
        </table>
    </article>

    <article class="tituloSeccion">
        Historial
    </article>

    <article class="formulario">
        <table>
            <tr>
                <th>Historial: </th>
                <td>
                    <asp:TextBox runat="server" ID="txtDescripciones" TextMode="MultiLine" Columns="70" Rows="15" Enabled="false"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>Observaciones: </th>
                <td>
                    <asp:TextBox runat="server" ID="txtObservaciones" TextMode="MultiLine" Columns="70" Rows="7"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ID="rfvTxtObservaciones" ControlToValidate="txtObservaciones" Display="Dynamic" ErrorMessage="Observaciones son obligatorias." ValidationGroup="Aceptar"></asp:RequiredFieldValidator>
                    <span id="spContadorTxtObservaciones" class="contadorCaracteresRestantes"></span>
                </td>
            </tr>
        </table>
    </article>

    <article class="areaBotones">
        <asp:Button runat="server" ID="btnEnviar" Text="Enviar para recibido conforme del solicitante" ValidationGroup="Aceptar" />        
        <input type="button"  id="btnCancelar" value="Cancelar"/>
    </article>

    <article id="arPopupDelFormulario"></article>
    <article id="arAlertasDelFormulario"></article>
    <article id="arpopupConfirmaDeseaBorrar"></article>
    <article id="arPopupGenerico"></article>

    <script type="text/javascript">
        $(document).ready(function () {
            $('#btnCancelar').click(function () {
                regresarAlListado();
            });
            
            habilitarTooltipGenerico();
        });

        function mostrarAlertaActualizacionExitosa() {
            deshabilitarControl('#<%=btnEnviar.ClientID%>');
            deshabilitarControl('#btnCancelar');
            $('.formulario').attr('disabled', 'disabled');

            mostrarAlerta(
            '#arAlertasDelFormulario',
            {
                mensaje: 'Se ha actualizado la información de la orden.',
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
        
        function regresarAlListado() {
            window.location = 'Lst_OT_GestionOrdenTrabajoCordinadorSectorTaller.aspx';
        };

    </script>

</asp:Content>

