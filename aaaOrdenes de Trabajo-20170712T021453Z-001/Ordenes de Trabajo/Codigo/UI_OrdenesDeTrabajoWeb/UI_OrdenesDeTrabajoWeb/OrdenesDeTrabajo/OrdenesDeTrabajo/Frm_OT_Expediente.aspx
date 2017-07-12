<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/Mp_Formulario.master" AutoEventWireup="false" CodeFile="Frm_OT_Expediente.aspx.vb" Inherits="OrdenesDeTrabajo_Frm_OT_Expediente" %>

<%@ Register TagPrefix="wuc" TagName="InformacionGeneral" Src="~/ControlesDeUsuario/Wuc_OT_InformacionGeneral.ascx" %>
<%@ Register Src="~/Controles/wuc_OT_Adjunto_Orden_Trabajo.ascx" TagName="wuc_AdjuntoOrdenTrabajo" TagPrefix="uc1" %>
<%@ Register TagPrefix="wuc" Namespace="Utilerias.WebForms.Controles" Assembly="Utilerias.WebForms.Controles" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphFormulario" runat="Server">

    <header>
        <h2>
            <asp:Label runat="server" ID="lblAccion" Text="Expediente"></asp:Label>
        </h2>
    </header>

    <article class="tituloSeccion">
        Expendiente
    </article>

    <article class="formulario">
        <table>
            <tr>
                <th>Encargado de Proyecto</th>
                <td>
                    <asp:Label runat="server" ID="lblEncargadoProyecto" data-tipocontrol="etiqueta"></asp:Label>
                </td>
            </tr>
        </table>
    </article>

    <article class="tituloSeccion">
        Información General de la OT
    </article>
    <article class="formulario sinBorde">
        <wuc:InformacionGeneral runat="server" ID="ctrl_InfoGeneral" />
    </article>
    <br />

    <article class="tituloSeccion">
        Ordenes de Trabajo Asociadas
    </article>
    <article class="formulario" id="formularioTotal" runat="server">
        <article class="formulario">
            <asp:LinkButton runat="server" ID="lnkOrdenMadre"></asp:LinkButton>
            <br />
            <asp:Repeater runat="server" ID="rpOrdenTrabajoHija">
                <ItemTemplate>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:LinkButton ID="lnkIdOrdenTrabajoHija" runat="server" OnCommand="lnkRpOrdenTrabajoHija_Command"
                    Text="<%# Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ORDEN_TRABAJOLST.ID_ORDEN_TRABAJO)%>"
                    CommandArgument='<%# String.Format("{0}¬{1}¬{2}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ORDEN_TRABAJOLST.ID_UBICACION), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ORDEN_TRABAJOLST.ID_ORDEN_TRABAJO), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ORDEN_TRABAJOLST.ANNO))%>'></asp:LinkButton>
                    <br />
                </ItemTemplate>
            </asp:Repeater>
        </article>
        <br />

        <article class="tituloSeccion">
            Adjuntar Documentos
        </article>

        <article class="formulario">
            <table>
                <tr>
                    <th>Etapa</th>
                    <td>
                        <asp:DropDownList runat="server" ID="ddlEtapa" data-tipocontrol="combo" AppendDataBoundItems="true" Width="59%"></asp:DropDownList>
                        <asp:RequiredFieldValidator runat="server" ID="rfvDdlEtapa" ControlToValidate="ddlEtapa" Display="Dynamic" ErrorMessage="La etapa es obligatoria." ValidationGroup="Agregar"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <th>Tipo de archivo</th>
                    <td>
                        <asp:DropDownList runat="server" ID="ddlTipoArchivo" data-tipocontrol="combo" AppendDataBoundItems="true" Width="59%"></asp:DropDownList>
                        <asp:RequiredFieldValidator runat="server" ID="rfvDdlTipoArchivo" ControlToValidate="ddlTipoArchivo" Display="Dynamic" ErrorMessage="El tipo de archivo es obligatorio." ValidationGroup="Agregar"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <th>Archivo</th>
                    <td>
                        <asp:FileUpload Width="59%" runat="server" ID="ifInfo" />
                        <img runat="server" id="imgExtensiones" data-tipo="tooltipExtensiones" class="tooltip" />
                        <asp:RequiredFieldValidator runat="server" ID="rfvIfInfo" ControlToValidate="ifInfo" ValidationGroup="Agregar" Display="Dynamic" ErrorMessage="Agregue un documento.">&nbsp;</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <th>Descripción</th>
                    <td>
                        <asp:TextBox runat="server" ID="txtDescripcion" data-tipocontrol="texto" Width="59%"></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" ID="rfvTxtDescripcion" ControlToValidate="txtDescripcion" Display="Dynamic" ErrorMessage="La descripción es obligatoria." ValidationGroup="Agregar"></asp:RequiredFieldValidator>
                    </td>
                </tr>
            </table>
        </article>

        <article class="areaBotones">
            <asp:Button runat="server" ID="btnAgregar" Text="Agregar" ValidationGroup="Agregar" />
        </article>

        <br />

        <article class="formulario">
            <ul class="encabezadoTabPanel">
                <asp:Repeater runat="server" ID="rpListaTapsTitulos">
                    <ItemTemplate>
                        <li runat="server" id="liEncabezado">
                            <a runat="server" class="tituloTabPanel" id="cuerpoTabPanel"><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_ETAPA_ORDEN_TRABAJO.DESCRIPCION)%></a>
                        </li>
                    </ItemTemplate>
                </asp:Repeater>
            </ul>

            <article class="cuerpoTabPanel">
                <asp:Repeater runat="server" ID="rpListaTapsContenidos">
                    <ItemTemplate>
                        <article runat="server" class="tabPanel" id="cuerpoTabPanel">
                            <h3><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_ETAPA_ORDEN_TRABAJO.DESCRIPCION)%></h3>
                            <br />
                            <uc1:wuc_AdjuntoOrdenTrabajo runat="server" ID="wucAdjuntoOrdenTrabajo" IdEtapaOrdenTrabajo='<%# Eval("ID_ETAPA_ORDEN_TRABAJO")%>' />
                        </article>
                    </ItemTemplate>
                </asp:Repeater>
                <article class="areaCantidadDeRegistro">
                    <asp:Label runat="server" ID="lblCantidadDeRegistros" Text="No existen documentos asociados al expediente de este proyecto." Visible="false"></asp:Label>
                </article>
            </article>

        </article>
    </article>
    <br />

    <article class="areaBotones">
        <input id="btnCancelar" type="button" value="Regresar" />
    </article>

    <article id="arPopupDelFormulario"></article>
    <article id="arAlertasDelFormulario"></article>
    <article id="arpopupConfirmaDeseaBorrar"></article>
    <article id="arPopupGenerico"></article>

    <script type="text/javascript">

        function regresarAlListado() {
            window.location = '<%=Me.PaginaRegresar%>';
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

        function mostrarAlertaActualizacionExitosa() {
            mostrarAlerta(
            '#arAlertasDelFormulario',
            {
                mensaje: 'Se ha registro el archivo exitosamente.',
                tipo: "exito",
                transparencia: 0.9,
                posicion: 'center'
            });
        };

        function mostrarAlertaLlaveIncorrecta() {
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

            $('[data-tipo="tooltipExtensiones"]').attr('src', '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Icono_Ayuda.png")%>');


            $('#btnCancelar').click(function () {
                regresarAlListado();
            });

            configurarTabPanel();

        });

            function deshabilitar() {
                deshabilitarJerarquiaDeControles(document.getElementById('<%=formularioTotal.ClientID%>'));
            deshabilitarControl('#<%=btnAgregar.ClientID%>');
        };

    </script>

</asp:Content>

