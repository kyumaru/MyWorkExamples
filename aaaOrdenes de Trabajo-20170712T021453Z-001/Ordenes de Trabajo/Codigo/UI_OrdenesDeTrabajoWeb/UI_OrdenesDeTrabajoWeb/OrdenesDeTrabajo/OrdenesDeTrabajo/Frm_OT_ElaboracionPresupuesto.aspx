<%@ Page Title=""  ValidateRequest="false" Language="VB" MasterPageFile="~/MasterPage/Mp_Formulario.master" AutoEventWireup="false" CodeFile="Frm_OT_ElaboracionPresupuesto.aspx.vb" Inherits="OrdenesDeTrabajo_Frm_OT_ElaboracionPresupuesto" %>

<%@ Register TagPrefix="wuc" TagName="InformacionGeneral" Src="~/ControlesDeUsuario/Wuc_OT_InformacionGeneral.ascx" %>
<%@ Register TagPrefix="wuc" Namespace="Utilerias.WebForms.Controles" Assembly="Utilerias.WebForms.Controles" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphFormulario" Runat="Server">
    <script type="text/javascript" src="<%=AdministradorRecursos.ObtenerRutaScript("nicEdit.js")%>"></script>
    <header>
        <h2>
            <asp:Label runat="server" ID="lblAccion" Text="Informe de valoración presupuestaria para la realización de la obra"></asp:Label>
        </h2>
    </header>

    <article class="tituloSeccion">
        Información General de la OT
    </article>
    <article class="formulario sinBorde">
        <wuc:InformacionGeneral ID="ctrl_InfoGeneral" runat="server" />
    </article>
    <br />

    <article class="formulario" id="formularioTotal" runat="server">
        <table>
            <tr>
                <th>Presupuesto Total</th>
                <td>
                    <asp:TextBox ID="txtPresupuesto" runat="server" Width="95px" Font-Bold="true" onkeypress="mascaraMoneda(this)" onpaste="return false" MaxLength="13"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ID="rfvTxtPresupuesto" ControlToValidate="txtPresupuesto" Display="Dynamic" ErrorMessage="El presupuesto total es obligatorio." ValidationGroup="Aceptar"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>

                <th style="width: 80px">Informe de valoración presupuestaria del proyecto</th>
                <td colspan="3">
                    <textarea style="height: 300px; width: 100%" data-tipocontrol="texto" id="txtDetalle"></textarea>
                    <br />
                    <asp:HiddenField ID="hdnNicEdit" runat="server" />

                </td>
            </tr>
        </table>
    </article>

    <article class="areaBotones">
        <asp:Button runat="server" ID="btnGuardar" Text="Guardar" ValidationGroup="Aceptar"   OnClientClick="javascript: cambioTxt()" />
        <asp:Button runat="server" ID="btnGuardarEnviar" Text="Guardar y Enviar" ValidationGroup="Aceptar"   OnClientClick="javascript: cambioTxt()" />
        <input id="btnCancelar" type="button" value="Regresar" />
    </article>

    <article id="arPopupDelFormulario"></article>
    <article id="arAlertasDelFormulario"></article>
    <article id="arpopupConfirmaDeseaBorrar"></article>
    <article id="arPopupGenerico"></article>

    <script type="text/javascript">
                     
         function mostrarAlertaLlaveIncorrecta() {
             deshabilitarControl('#<%=btnGuardar.ClientID%>');
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
            deshabilitarControl('#<%=btnGuardar.ClientID%>');
            deshabilitarControl('#<%=btnGuardarEnviar.ClientID%>');
            $('.formulario').attr('disabled', 'disabled');

            mostrarAlerta(
            '#arAlertasDelFormulario',
            {
                mensaje: 'Se ha actualizado la información del presupuesto.',
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
        });

        function cambioTxt() {
            $("#<%=hdnNicEdit.ClientID %>").val($(".nicEdit-main").html());
        };
        
        /*Mascara para formato de montos*/
        function mascaraMoneda(pvo_CampoReferencia) {
            vgo_CampoReferencia = pvo_CampoReferencia;
            setTimeout("ejecutarMascara()", 1);
        };

        function ejecutarMascara() {
            vgo_CampoReferencia.value = convertirCadena(vgo_CampoReferencia.value);
        };

        function convertirCadena(pvc_Cadena) {
            pvc_Cadena = pvc_Cadena.replace(/([^0-9\.]+)/g, '');
            pvc_Cadena = pvc_Cadena.replace(/^[\.]/, '');
            pvc_Cadena = pvc_Cadena.replace(/[\.][\.]/g, '');
            pvc_Cadena = pvc_Cadena.replace(/\.(\d)(\d)(\d)/g, '.$1$2');
            pvc_Cadena = pvc_Cadena.replace(/\.(\d{1,2})\./g, '.$1');
            pvc_Cadena = pvc_Cadena.toString().split('').reverse().join('').replace(/(\d{3})/g, '$1,');
            pvc_Cadena = pvc_Cadena.split('').reverse().join('').replace(/^[\,]/, '');
            return pvc_Cadena;
        };
        /* Fin Mascara para formato de montos */

        $(document).ready(function () {

            $('#btnCancelar').click(function () {
                regresarAlListado();
            });

        });


        function CopiarTextoAcuerdo() {
            try {
                var copyTextarea = document.querySelector('.textoACopiar');
                copyTextarea.select();
                document.execCommand('copy');
            }
            catch (err) {  }
        }
        
        function regresarAlListado() {
            window.location = '<%=PaginaRegresar%>';
        };

        function deshabilitar() {
            deshabilitarJerarquiaDeControles(document.getElementById('<%=formularioTotal.ClientID%>'));
                    deshabilitarControl('#<%=btnGuardar.ClientID%>');
                    deshabilitarControl('#<%=btnGuardarEnviar.ClientID%>');
                };

    </script>

</asp:Content>

