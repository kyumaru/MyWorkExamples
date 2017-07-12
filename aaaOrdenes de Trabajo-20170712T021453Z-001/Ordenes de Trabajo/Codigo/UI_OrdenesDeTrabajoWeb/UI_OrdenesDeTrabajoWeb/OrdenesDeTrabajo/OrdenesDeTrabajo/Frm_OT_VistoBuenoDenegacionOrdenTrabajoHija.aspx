<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/Mp_Formulario.master" AutoEventWireup="false" CodeFile="Frm_OT_VistoBuenoDenegacionOrdenTrabajoHija.aspx.vb" Inherits="OrdenesDeTrabajo_Frm_OT_VistoBuenoDenegacionOrdenTrabajoHija" %>

<%@ Register TagPrefix="wuc" TagName="InformacionGeneral" Src="~/ControlesDeUsuario/Wuc_OT_InformacionGeneral.ascx" %>
<%@ Register TagPrefix="wuc" Namespace="Utilerias.WebForms.Controles" Assembly="Utilerias.WebForms.Controles" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphFormulario" Runat="Server">

    <header>
        <h2>
            <asp:Label ID="Label1" runat="server" Text="Revisión de Orden de Trabajo Hija"></asp:Label>
        </h2>
    </header>

    <article class="tituloSeccion">
        Información General de la OT Hija
    </article>
     <article class="formulario sinBorde">
        <wuc:InformacionGeneral ID="ctrl_InfoGeneral" runat="server" />
    </article>
    <br />

    <article class="tituloSeccion">
        Datos de la Revisión
    </article>

    <article class="formulario">
        <table>
            <tr>
                <th>Condición</th>
                <td>
                    <asp:RadioButton ID="rbtVistoBueno" runat="server" Text="Visto Bueno" GroupName="Condicion" AutoPostBack="true" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:RadioButton ID="rbtDenegacion" runat="server" Text="Denegación" GroupName="Condicion" AutoPostBack="true"/>
                </td>
            </tr>
            <tr runat="server" ID="trJustificacion">
                <th>Justificación</th>
                <td>
                    <asp:TextBox Width="59%" runat="server" ID="txtJustificacion" data-tipocontrol="texto" TextMode="MultiLine" Rows="5"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ID="rfvTxtJustificacion" Enabled="false" ControlToValidate="txtJustificacion" Display="Dynamic" ErrorMessage="La justificación es Obligatoria." ValidationGroup="Aceptar"></asp:RequiredFieldValidator>
                </td>
            </tr>
        </table>
    </article>

    <article class="areaBotones">
        <asp:Button runat="server" ID="btnAceptar" Text="Aceptar" ValidationGroup="Aceptar" />
        <%--<input id="btnLimpiarFormulario" type="button" data-tipo="limpiarFormulario" value="Limpiar formulario" />--%>
        <input id="btnCancelar" type="button" value="Cancelar" />
    </article>

    <article id="arAlertasDelFormulario"></article>
    <article id="arpopupConfirmaDeseaProcesar"></article>

    <script type="text/javascript">

        function regresarAlListado() {
            window.location = 'Lst_OT_VistoBuenoDenegacionOrdenTrabajoHija.aspx';
        };

        function mostrarAlertaActualizacionExitosa(pvc_Mensaje) {
            deshabilitarControl('#<%=btnAceptar.ClientID%>');
            deshabilitarControl('#btnCancelar');
            $('.formulario').attr('disabled', 'disabled');

            mostrarAlerta(
                '#arAlertasDelFormulario',
                {
                    mensaje: pvc_Mensaje,
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
            deshabilitarControl('#btnLimpiarFormulario');
            deshabilitarControl('#btnCancelar');
            $('.formulario').attr('disabled', 'disabled');

            mostrarAlerta(
                '#arAlertasDelFormulario',
                {
                    mensaje: 'El identificador provisto no pertenece a ninguna orden de trabajo.',
                    tipo: "peligro",
                    transparencia: 0.9,
                    posicion: 'center',
                    onClosed: function () { regresarAlListado(); }
                });
        };

        $(document).ready(function () {
            $('#btnCancelar').click(function () {
                regresarAlListado();
            });
        });

    </script>

</asp:Content>

