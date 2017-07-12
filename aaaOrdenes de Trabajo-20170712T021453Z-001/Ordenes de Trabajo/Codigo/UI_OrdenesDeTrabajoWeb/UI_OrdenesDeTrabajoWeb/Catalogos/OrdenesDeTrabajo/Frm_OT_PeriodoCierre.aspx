<%@ Page Title="Mantenimiento de periodos de cierre de recepción" MasterPageFile="~/MasterPage/Mp_Formulario.master" Language="VB" AutoEventWireup="false" CodeFile="Frm_OT_PeriodoCierre.aspx.vb" Inherits="Catalogos_Frm_OT_PeriodoCierre" %>

<asp:Content runat="server" ContentPlaceHolderID="cphFormulario" ID="Content1">

    <header>
        <h2>
            <asp:Label runat="server" ID="lblAccion"></asp:Label>
        </h2>
    </header>
    <article class="tituloSeccion">
        Datos del periodo de cierre
    </article>

    <article class="formulario">
        <table>
            <tr>
                <th>Unidad Interna</th>
                <td>
                    <asp:DropDownList Width="40%" runat="server" ID="ddlUnidadInterna" AppendDataBoundItems="true" data-tipoControl="combo"></asp:DropDownList>
                    <asp:RequiredFieldValidator runat="server" ID="rfvDdlUnidad" ControlToValidate="ddlUnidadInterna" Display="Dynamic" ErrorMessage="La unidad es requerida">&nbsp;</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <th>Fecha desde</th>
                <td>
                    <asp:TextBox Width="40%" runat="server" ID="txtFiltroFechaDesde" data-tipocontrol="texto"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ID="rfvfechadesde" ControlToValidate="txtFiltroFechaDesde" Display="Dynamic" ErrorMessage="La fecha de inicio es requerida">&nbsp;</asp:RequiredFieldValidator>
                    <asp:CompareValidator runat="server" ID="cmpvtxtFiltroDesde" ControlToValidate="txtFiltroFechaDesde" Display="Dynamic" ErrorMessage="Fecha desde inválida" Operator="DataTypeCheck" Type="Date" ValidationGroup="Grupo7">&nbsp;</asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <th>Fecha hasta</th>
                <td>
                    <asp:TextBox Width="40%" runat="server" ID="txtFiltroFechaHasta" data-tipocontrol="texto"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ID="rfvfechahasta" ControlToValidate="txtFiltroFechaHasta" Display="Dynamic" ErrorMessage="La fecha final es requerida">&nbsp;</asp:RequiredFieldValidator>
                    <asp:CompareValidator runat="server" ID="cmpvtxtFiltroHasta" ControlToValidate="txtFiltroFechaHasta" Display="Dynamic" ErrorMessage="Fecha hasta inválida" Operator="DataTypeCheck" Type="Date" ValidationGroup="Grupo7">&nbsp;</asp:CompareValidator>
                </td>
                <td></td>
            </tr>
        </table>
    </article>

    <article class="areaBotones">
        <asp:Button runat="server" ID="btnAceptar" Text="Aceptar" />
        <input type="button" data-tipo="limpiarFormulario" value="Limpiar Formulario" id="btnLimpiarFormulario"/>
        <input type="button" value="Cancelar" id="btnCancelar" />
    </article>

    <article id="arPopupDelFormulario"></article>
    <article id="arAlertasDelFormulario"></article>

    <script type="text/javascript">

        function regresarAlListado() {
            window.location = 'Lst_OT_PeriodoCierre.aspx';
        };
        function deshabilitarFormulario() {
            deshabilitarControl('#<%=Me.btnAceptar.ClientID%>');
            deshabilitarControl('#btnLimpiarFormulario');
            deshabilitarControl('#btnCancelar');
            $('.formulario').attr('disabled', 'disabled');
        };

        function mostrarPopupRegistroExitoso() {
            var vlo_ConfiguracionPopup = {
                titulo: 'Catálogo de Periodos de cierre',
                mensaje: 'Se ha registrado la información del periodo de cierre.<br /><strong>¿Desea registrar otro periodo?</strong>',
                onClosed: function () { regresarAlListado(); },
                botones:
                    [
                        {
                            idControl: "btnSi",
                            textoBoton: "Sí",
                            onClick: function () { window.location = 'Frm_OT_PeriodoCierre.aspx?pvn_Operacion=<%=Utilerias.OrdenesDeTrabajo.eOperacion.Agregar%>'; }
                        },
                        {
                            idControl: "btnNo",
                            textoBoton: "No",
                            onClick: function () { regresarAlListado(); }
                        }
                    ]

            };

            $('#arPopupDelFormulario').popup(vlo_ConfiguracionPopup);
            window.location = '#arPopupDelFormulario';

        };


        function mostrarAlertaError(pvc_Msj) {
            mostrarAlerta(
                '#arAlertasDelFormulario',
                {
                    mensaje: pvc_Msj,
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
                    mensaje: 'Se ha actualizado la información del periodo de cierre',
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
                    mensaje: 'El número de identificación provisto no pertenece a ningun periodo registrado en el sistema',
                    tipo: "peligro",
                    transparencia: 0.9,
                    posicion: 'center',
                    permiteCerrar: true,
                    onClosed: function () { regresarAlListado(); }
                }
            );
        };

        function establecerMinyMaxDate() {
            if (document.getElementById('<%=txtFiltroFechaHasta.ClientID%>'))
                establecerFechaMaximaDatePicker("#<%=Me.txtFiltroFechaDesde.ClientID%>", document.getElementById('<%=txtFiltroFechaHasta.ClientID%>').value);

            if (document.getElementById('<%=txtFiltroFechaDesde.ClientID%>'))
                establecerFechaMinimaDatePicker("#<%=Me.txtFiltroFechaHasta.ClientID%>", document.getElementById('<%=txtFiltroFechaDesde.ClientID%>').value);

        };

        $(document).ready(function () {

            $('#btnCancelar').click(function () {
                regresarAlListado();
            });

            configurarDatePickerRango("#<%=Me.txtFiltroFechaDesde.ClientID%>", "#<%=Me.txtFiltroFechaHasta.ClientID%>");

        });
    </script>

</asp:Content>

