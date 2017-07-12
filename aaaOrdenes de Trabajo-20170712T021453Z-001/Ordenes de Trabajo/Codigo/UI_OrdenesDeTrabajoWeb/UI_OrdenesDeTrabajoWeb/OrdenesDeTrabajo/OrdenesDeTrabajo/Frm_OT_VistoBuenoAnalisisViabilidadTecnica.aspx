<%@ Page Title="" ValidateRequest="false" Language="VB" MasterPageFile="~/MasterPage/Mp_Formulario.master" AutoEventWireup="false" CodeFile="Frm_OT_VistoBuenoAnalisisViabilidadTecnica.aspx.vb" Inherits="OrdenesDeTrabajo_Frm_OT_VistoBuenoAnalisisViabilidadTecnica" %>

<%@ Register TagPrefix="wuc" TagName="InformacionGeneral" Src="~/ControlesDeUsuario/Wuc_OT_InformacionGeneral.ascx" %>
<%@ Register TagPrefix="wuc" Namespace="Utilerias.WebForms.Controles" Assembly="Utilerias.WebForms.Controles" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphFormulario" runat="Server">
    <script type="text/javascript" src="<%=AdministradorRecursos.ObtenerRutaScript("nicEdit.js")%>"></script>
    <header>
        <h2>
            <asp:Label runat="server" ID="lblAccion" Text="Revisión de Análisis de Viabilidad Técnica"></asp:Label>
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
        </table>
    </article>
    <br />

    <article class="tituloSeccion">
        Colaboradores
    </article>
    <article data-grupo="Listado" class="listado sinBorde">
        <asp:Repeater runat="server" ID="rpColaboradores">
            <HeaderTemplate>
                <table>
                    <tr>
                        <th>
                            <asp:Label runat="server" ID="lnkIdentificacion" Text="Identificación"></asp:Label>
                        </th>
                        <th>
                            <asp:Label runat="server" ID="lnkNombre" Text="Nombre"></asp:Label>
                        </th>
                        <th>
                            <asp:Label runat="server" ID="lnkArea" Text="Área"></asp:Label>
                        </th>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr class="lineaDelListado">
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.CEDULA)%></td>
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.NOMBRE_EMPLEADO)%></td>
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.DESC_AREA)%></td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
    </article>

    <br />
    <br />

    <article class="formulario">
        <table>
            <tr>
                <th>Indicador de Viabilidad</th>
                <td>
                    <asp:RadioButton ID="rbSiViabilidad" runat="server" GroupName="grpViabilidad" Text="Si" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
	                <asp:RadioButton ID="rbNoViabilidad" runat="server" GroupName="grpViabilidad" Text="No" />
                </td>
            </tr>
            <tr>
                <th>Estimación Presupuestaria</th>
                <td>
                    <asp:TextBox ID="txtEstimacionPresup" runat="server" Width="95px" Font-Bold="true" onkeypress="mascaraMoneda(this)" onpaste="return false" MaxLength="13"></asp:TextBox>
                </td>
            </tr>
        </table>
    </article>

    <article class="tituloSeccion">
        Detalle
    </article>
    <br />

    <article id="MainContent_lblHtml">
        <textarea style="height: 300px; width: 98%" data-tipocontrol="texto" id="txtDetalle"></textarea>
        <asp:HiddenField ID="hdnNicEdit" runat="server" />
    </article>

    <article class="areaBotones">
        <asp:Button runat="server" OnClientClick="javascript: cambioTxt()" ID="btnGuardar" Text="Actualizar" />
    </article>

    <br />
    <br />
    <article class="tituloSeccion">
        Documentos Adjuntos
    </article>
    <article class="listado sinBorde">
        <asp:Repeater runat="server" ID="rpAdjunto">
            <HeaderTemplate>
                <table>
                    <tr>
                        <th>Archivo</th>
                        <th>Descripción</th>
                        <th>Tipo</th>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr class="lineaDelListado">
                    <td>
                        <asp:LinkButton runat="server" ID="lnkArchivo"
                            CommandArgument='<%#Container.ItemIndex%>'
                            Style="text-decoration: underline; color: blue;"
                            OnCommand="lnkArchivo_Command"
                            Text="<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ADJUNTO_ORDEN_TRABAJOLST.NOMBRE_ARCHIVO)%>"></asp:LinkButton>
                    </td>
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ADJUNTO_ORDEN_TRABAJOLST.DESCRIPCION)%></td>
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ADJUNTO_ORDEN_TRABAJOLST.DESC_TIPO_DOCUMENTO)%></td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
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
                    <asp:RadioButton ID="rbtAprobada" runat="server" Text="Aprobada" GroupName="Condicion" AutoPostBack="true" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:RadioButton ID="rbtDevuelta" runat="server" Text="Devuelta" GroupName="Condicion" AutoPostBack="true" />
                </td>
            </tr>
            <tr>
                <th>Observaciones</th>
                <td>
                    <asp:TextBox Width="59%" runat="server" ID="txtJustificacion" data-tipocontrol="texto" TextMode="MultiLine" Rows="5"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ID="rfvTxtJustificacion" Enabled="false" ControlToValidate="txtJustificacion" Display="Dynamic" ErrorMessage="Las Observaciones son Obligatorias." ValidationGroup="Aceptar"></asp:RequiredFieldValidator>
                </td>
            </tr>
        </table>
    </article>

    <article class="areaBotones">
        <asp:Button runat="server" OnClientClick="javascript: cambioTxt()" ID="btnGuardarEnviar" Text="Guardar y Enviar" ValidationGroup="Aceptar" />
        <input id="btnCancelar" type="button" value="Regresar" />
    </article>

    <article id="arPopupDelFormulario"></article>
    <article id="arAlertasDelFormulario"></article>
    <article id="arpopupConfirmaDeseaBorrar"></article>
    <article id="arPopupGenerico"></article>
    <script type="text/javascript">

        function mostrarAlertaActualizacionExitosa() {
            deshabilitarControl('#<%=btnGuardar.ClientID%>');
            deshabilitarControl('#<%=btnGuardarEnviar.ClientID%>');
            $('.formulario').attr('disabled', 'disabled');

            mostrarAlerta(
            '#arAlertasDelFormulario',
            {
                mensaje: 'Se ha actualizado la información de la viabilidad.',
                tipo: "exito",
                transparencia: 0.9,
                posicion: 'center',
                onClosed: function () { regresarAlListado(); }
            });
        };

        function mostrarAlertaActualizacionExitosa2() {

            mostrarAlerta(
            '#arAlertasDelFormulario',
            {
                mensaje: 'Se ha actualizado la información de la viabilidad.',
                tipo: "exito",
                transparencia: 0.9,
                posicion: 'center'
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

            configurarLongitudMaximaTexto('#<%=Me.txtEstimacionPresup.ClientID%>', '<%=Utilerias.OrdenesDeTrabajo.Modelo.OTT_VIABILIDAD_TECNICA.ESTIMACION_PRESUPUESTARIA_BD_TAMANO%>');

        });

        function regresarAlListado() {
            window.location = 'Lst_OT_VistoBuenoAnalisisViabilidadTecnica.aspx';
        };

        function GoDown() {
            window.scrollTo(0, document.body.scrollHeight);
        };

    </script>

</asp:Content>

