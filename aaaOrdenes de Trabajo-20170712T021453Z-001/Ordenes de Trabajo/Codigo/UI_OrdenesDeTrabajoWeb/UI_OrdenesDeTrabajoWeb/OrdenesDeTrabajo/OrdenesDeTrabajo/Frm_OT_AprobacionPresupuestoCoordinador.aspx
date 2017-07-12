<%@ Page Title="" ValidateRequest="false" Language="VB" MasterPageFile="~/MasterPage/Mp_Formulario.master" AutoEventWireup="false" CodeFile="Frm_OT_AprobacionPresupuestoCoordinador.aspx.vb" Inherits="OrdenesDeTrabajo_Frm_OT_AprobacionPresupuestoCoordinador" %>

<%@ Register TagPrefix="wuc" TagName="InformacionGeneral" Src="~/ControlesDeUsuario/Wuc_OT_InformacionGeneral.ascx" %>
<%@ Register TagPrefix="wuc" Namespace="Utilerias.WebForms.Controles" Assembly="Utilerias.WebForms.Controles" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphFormulario" runat="Server">
    <script type="text/javascript" src="<%=AdministradorRecursos.ObtenerRutaScript("nicEdit.js")%>"></script>
    <header>
        <h2>
            <asp:Label runat="server" ID="lblAccion" Text="Revisión de Aprobación de Presupuesto"></asp:Label>
        </h2>
    </header>

    <article class="tituloSeccion">
        Información General de la OT
    </article>
    <article class="formulario sinBorde">
        <wuc:InformacionGeneral ID="ctrl_InfoGeneral" runat="server" />
    </article>
    <br />

    <article class="formulario">
        <table>
            <tr>
                <th>Encargado del Proyecto</th>
                <td>
                    <asp:Label runat="server" ID="lblEncargadoProyecto"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>Presupuesto Total</th>
                <td>
                    <asp:Label ID="lblPresupuesto" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <th style="width: 80px">Informe de valoración presupuestaria del proyecto</th>
                <td colspan="3">
                    <textarea style="height: 300px; width: 100%; overflow-x: scroll; overflow-y: scroll;" id="txtDetalle"></textarea>
                    <br />
                    <asp:HiddenField ID="hdnNicEdit" runat="server" />
                </td>
            </tr>
            <tr runat="server" visible="false" id="trObservacionesDevolucion">
                <th>Observaciones de la Devolución por Jefatura</th>
                <td>
                    <asp:Label ID="lblObservacionesDevolucion" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
    </article>
    <br />

    <article class="tituloSeccion">
        Revisión
    </article>

    <article class="formulario">
        <table>
            <tr>
                <th>Condición</th>
                <td>
                    <asp:RadioButton ID="rbtAprobada" runat="server" Text="Aprobar" GroupName="Condicion" AutoPostBack="true" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:RadioButton ID="rbtDevuelta" runat="server" Text="Devolver" GroupName="Condicion" AutoPostBack="true" />
                </td>
            </tr>
            <tr runat="server" id="trJustificacion">
                <th>Observaciones</th>
                <td>
                    <asp:TextBox Width="59%" runat="server" ID="txtJustificacion" data-tipocontrol="texto" TextMode="MultiLine" Rows="5"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ID="rfvTxtJustificacion" Enabled="false" ControlToValidate="txtJustificacion" Display="Dynamic" ErrorMessage="Las Observaciones son Obligatorias." ValidationGroup="Aceptar"></asp:RequiredFieldValidator>
                </td>
            </tr>
        </table>
    </article>

    <article class="areaBotones">
        <asp:Button runat="server" ID="btnGuardarEnviar" Text="Guardar y Enviar" ValidationGroup="Aceptar" />
        <input id="btnCancelar" type="button" value="Regresar" />
    </article>

    <article id="arPopupDelFormulario"></article>
    <article id="arAlertasDelFormulario"></article>
    <article id="arpopupConfirmaDeseaBorrar"></article>
    <article id="arPopupGenerico"></article>

    <script type="text/javascript">

        function GoDown() {
            window.scrollTo(0, document.body.scrollHeight);
        };

        function mostrarAlertaLlaveIncorrecta() {
            deshabilitarControl('#<%=btnGuardarEnviar.ClientID%>');
            deshabilitarControl('#btnCancelar');
            $('.formulario').attr('disabled', 'disabled');

            mostrarAlerta(
                '#arAlertasDelFormulario',
                {
                    mensaje: 'El identificador provisto no pertenece a ningun registro del sistema',
                    tipo: "peligro",
                    transparencia: 0.9,
                    posicion: 'center',
                    onClosed: function () { regresarAlListado(); }
                });
        };

        function mostrarAlertaActualizacionExitosa() {
            deshabilitarControl('#<%=btnGuardarEnviar.ClientID%>');
            $('.formulario').attr('disabled', 'disabled');

            mostrarAlerta(
            '#arAlertasDelFormulario',
            {
                mensaje: 'Se ha actualizado la información exitosamente.',
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

        //Descripcion: Funcion que establece la configuración inicial  del text area enriquesido

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        var area2;
        bkLib.onDomLoaded(function () {

            area2 = new nicEditor({ maxHeight: 300 }).panelInstance('txtDetalle');
            $(".nicEdit-main").html($("#<%=hdnNicEdit.ClientID %>").val());
            document.getElementsByClassName('nicEdit-main')[0].removeAttribute('contenteditable');
            document.getElementsByClassName('nicEdit-panelContain')[0].style.visibility = "hidden";

            document.getElementsByClassName('nicEdit-main')[0].parentNode.style.borderWidth = "1px";
            document.getElementsByClassName('nicEdit-main')[0].parentNode.style.borderStyle = "solid";
            document.getElementsByClassName('nicEdit-main')[0].parentNode.style.borderStyle = "solid";
            document.getElementsByClassName('nicEdit-main')[0].parentNode.style.borderColor = "rgb(204, 204, 204)";
        });

        function limpiarControles() {
            quitarContenidoTemario();
        };

        //Descripcion: Funcion que limpia el text area enriquesido
        function quitarContenidoTemario() {
            area2.removeInstance('txtDetalle');
            document.getElementById('txtDetalle').value = "";
            area2 = new nicEditor({ maxHeight: 300 }).panelInstance('txtDetalle');
        };

        //Descripcion: Funcion que valida el contenido del editor "txtDetalle"

        function validarContenidoNicEditor(source, clientside_arguments) {
            var vlc_CadenaaValidar = nicEditors.findEditor('txtDetalle').getContent().replace(/&nbsp;/g, '').replace(/<br>/g, '').trim();

            if (vlc_CadenaaValidar == '')
                return clientside_arguments.IsValid = false;
            else
                return clientside_arguments.IsValid = true;

        };

        $(document).ready(function () {
            habilitarTooltipGenerico();
            $('#btnCancelar').click(function () {
                regresarAlListado();
            });
        });

        function regresarAlListado() {
            window.location = 'Lst_OT_AprobacionPresupuestoCoordinador.aspx';
        };

    </script>

</asp:Content>

