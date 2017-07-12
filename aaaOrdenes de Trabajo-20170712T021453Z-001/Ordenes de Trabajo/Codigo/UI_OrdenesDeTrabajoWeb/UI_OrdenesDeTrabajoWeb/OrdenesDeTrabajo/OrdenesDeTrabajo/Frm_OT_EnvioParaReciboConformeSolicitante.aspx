<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/Mp_Formulario.master" AutoEventWireup="false" CodeFile="Frm_OT_EnvioParaReciboConformeSolicitante.aspx.vb" Inherits="OrdenesDeTrabajo_Frm_OT_EnvioParaReciboConformeSolicitante" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphFormulario" runat="Server">

    <header>
        <h2>
            <asp:Label runat="server" ID="lblAccion"></asp:Label>
        </h2>
    </header>

    <article class="tituloSeccion">
        Envio para Recibido Conforme del Solicitante
    </article>

    <article class="formulario">
        <table>
           <tr>
                <th>Fecha Finalización del Trabajo: </th>
                <td>
                    <asp:TextBox ID="txtFechaFinalizacion" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ID="rfvTxtFechaFinalizacion" ControlToValidate="txtFechaFinalizacion" Display="Dynamic" ErrorMessage="La Fecha de Finalización del trabajo es requerida" ValidationGroup="Aceptar">&nbsp;</asp:RequiredFieldValidator>
                    <asp:CompareValidator runat="server" ID="cmpvTxtFechaFinalizacion" ControlToValidate="txtFechaFinalizacion" Display="Dynamic" ErrorMessage="La Fecha de Finalización del trabajo es inválida" Operator="DataTypeCheck" Type="Date" ValidationGroup="Aceptar">&nbsp;</asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <th>Tiempo Invertido en la ejecución del trabajo: </th>
                <td>
                    <asp:TextBox runat="server" ID="txtTiempoInvertido"></asp:TextBox>
                    <asp:DropDownList runat="server" ID="ddlTiempoInvertido"></asp:DropDownList>
                    <asp:RequiredFieldValidator runat="server" ID="rfvTxtTiempoInvertido" Display="Dynamic" ControlToValidate="txtTiempoInvertido" ErrorMessage="El tiempo invertido es requerido." ValidationGroup="Aceptar">&nbsp;&nbsp;</asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator runat="server" ID="rfvDdlTiempoInvertido" Display="Dynamic" ControlToValidate="ddlTiempoInvertido" ErrorMessage="La unidad de tiempo es requerida." ValidationGroup="Aceptar">&nbsp;&nbsp;</asp:RequiredFieldValidator>
                </td>
            </tr>
        </table>
    </article>

    <article class="areaBotones">
        <asp:Button runat="server" ID="btnGuardar" Text="Guardar" ValidationGroup="Aceptar"/>
        <asp:Button runat="server" ID="btnGuardarEnviar" Text="Guardar y Enviar" ValidationGroup="Aceptar"/>
        <input id="btnCancelar" type="button" value="Regresar" />
    </article>

    <article id="arPopupDelFormulario"></article>
    <article id="arAlertasDelFormulario"></article>
    <article id="arpopupConfirmaEnviar"></article>    

    <script type="text/javascript">
        function regresarAlListado() {
            window.location = 'Lst_OT_GestionOrdenTrabajoCordinadorSectorTaller.aspx';
        };

        function deshabilitarFormulario() {
            deshabilitarControl('#<%=Me.btnGuardar.ClientID%>');
            deshabilitarControl('#<%=Me.btnGuardarEnviar.ClientID%>');
            deshabilitarControl('#btnCancelar');

            $('.formulario').attr('disabled','disabled');
        };

        function mostrarAlertaError(pvc_Mensaje) {
            mostrarAlerta(
                '#arAlertasDelFormulario',
                {
                    mensaje: pvc_Mensaje,
                    tipo: "peligro",
                    transparencia: 0.9,
                    posicion: 'center',
                    permiteCerrar: true
                }
                );
        };

        function mostrarAlertaActualizacionExitosa() {
            deshabilitarFormulario();

            mostrarAlerta(
                '#arAlertasDelFormulario',
                {
                    mensaje: 'Se ha actualizado la información.',
                    tipo: "exito",
                    transparencia: 0.9,
                    posicion: 'center',
                    permiteCerrar: true,
                    onClosed: function () { regresarAlListado(); }
                }
                );
        };            

        function mostrarAlertaLlaveIncorrecta() {
            deshabilitarFormulario();

            mostrarAlerta(
                '#arAlertasDelFormulario',
                {
                    mensaje: 'El número de identificación provisto no pertenece a ningún registro en el sistema.',
                    tipo: "peligro",
                    transparencia: 0.9,
                    posicion: 'center',
                    permiteCerrar: true,
                    onClosed: function () { regresarAlListado(); }
                }
                );
        };

        function mostrarPopupConfirmaDeseaEnviarRegistro() {
            var vlo_ConfiguracionPopup = {
                titulo: 'Ordenes de Trabajo',
                mensaje: '¿Está seguro de registrar la orden de trabajo como atendida?',
                botones:
                        [
                            {
                                idControl: "btnSi",
                                textoBoton: "Si",
                                onClick:
                                    function (e) {
                                        $(this).attr("disabled", "disabled");
                                        __doPostBack('<%=btnGuardarEnviar.UniqueID%>', '');
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

            $('#arpopupConfirmaEnviar').popup(vlo_ConfiguracionPopup);

            window.location = '#arpopupConfirmaEnviar';

            return false;
        };

        $(document).ready(function () {
            
            configurarDatePicker('#<%=Me.txtFechaFinalizacion.ClientID%>');
            establecerFechaMaximaDatePicker('#<%=Me.txtFechaFinalizacion.ClientID%>', new Date());
            $('#<%=btnGuardarEnviar.ClientID%>').click(function () { return mostrarPopupConfirmaDeseaEnviarRegistro(); });


            $('#btnCancelar').click(function () {
                regresarAlListado();
            });
        });
    </script>
</asp:Content>

