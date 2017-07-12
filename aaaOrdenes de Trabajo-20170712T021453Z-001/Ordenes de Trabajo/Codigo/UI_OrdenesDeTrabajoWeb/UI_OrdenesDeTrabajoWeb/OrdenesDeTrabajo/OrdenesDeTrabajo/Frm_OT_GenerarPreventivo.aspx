<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/Mp_Formulario.master" AutoEventWireup="false" CodeFile="Frm_OT_GenerarPreventivo.aspx.vb" Inherits="OrdenesDeTrabajo_Frm_OT_GenerarPreventivo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphFormulario" runat="Server">

    <header>
        <h2>
            <asp:Label runat="server" ID="lblAccion">Generación de Orden de Trabajo Preventivo</asp:Label>
        </h2>
    </header>

    <article class="tituloSeccion">
        Generación de Orden de Trabajo Preventivo
    </article>

    <article class="formulario">
        <table>
            <tr>
                <th>Año</th>
                <td>
                    <asp:TextBox  runat="server" ID="txtAnno" data-tipocontrol="texto" MaxLength="4"></asp:TextBox>
                    <asp:FilteredTextBoxExtender ID="ftbTxtAnno" runat="server" TargetControlID="txtAnno" FilterMode="ValidChars" FilterType="Numbers"></asp:FilteredTextBoxExtender>
                    <asp:RequiredFieldValidator runat="server" ID="rfvTxtAnno" ControlToValidate="txtAnno" Display="Dynamic" ErrorMessage="El año es obligatorio." ValidationGroup="Generar"></asp:RequiredFieldValidator>
                </td>
            </tr>
        </table>
    </article>

    <article class="areaBotones">
        <asp:Button runat="server" ID="btnAceptar" Text="Generar" ValidationGroup="Generar" />
        <input id="btnLimpiarFormulario" type="button" data-tipo="limpiarFormulario" value="Limpiar formulario" />
        <input id="btnCancelar" type="button" value="Cancelar" />
    </article>

    <article id="arAlertasDelFormulario"></article>

    <script type="text/javascript">

        function regresarAlListado() {
            window.location = 'Lst_OT_ProgramacionOTPreventivo.aspx';
        };

        function mostrarAlertaActualizacionExitosa() {
            deshabilitarControl('#<%=btnAceptar.ClientID%>');
            deshabilitarControl('#btnLimpiarFormulario');
            deshabilitarControl('#btnCancelar');
            $('.formulario').attr('disabled', 'disabled');

            mostrarAlerta(
                '#arAlertasDelFormulario',
                {
                    mensaje: 'Cantidad de ordenes de trabajo generadas ' + '<%= Me.Cantidad%>',
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

        $(document).ready(function () {
            $('#btnCancelar').click(function () {
                regresarAlListado();
            });

            configurarSpinnerNumericoRango('#<%= Me.txtAnno.ClientID%>', 1, 2000, 3000, true);

        });

    </script>

</asp:Content>

