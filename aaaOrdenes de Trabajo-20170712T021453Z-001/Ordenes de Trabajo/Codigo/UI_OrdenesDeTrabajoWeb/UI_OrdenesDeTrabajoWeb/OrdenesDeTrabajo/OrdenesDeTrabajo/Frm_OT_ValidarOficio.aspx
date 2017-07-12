<%@ Page Title="" Language="VB" MaintainScrollPositionOnPostback="true" MasterPageFile="~/MasterPage/Mp_Formulario.master" AutoEventWireup="false" CodeFile="Frm_OT_ValidarOficio.aspx.vb" Inherits="OrdenesDeTrabajo_Frm_OT_ValidarOficio" %>

<%@ Register TagPrefix="wuc" TagName="InformacionGeneral" Src="~/ControlesDeUsuario/Wuc_OT_InformacionGeneral.ascx" %>
<%@ Register TagPrefix="wuc" Namespace="Utilerias.WebForms.Controles" Assembly="Utilerias.WebForms.Controles" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphFormulario" runat="Server">

    <header>
        <h2>
            <asp:Label runat="server" ID="lblAccion" Text="Validación de Oficio"></asp:Label>
        </h2>
    </header>

    <article class="tituloSeccion">
        Información General de la OT
    </article>
    <article class="formulario sinBorde">
        <wuc:InformacionGeneral ID="ctrl_InfoGeneral" runat="server" />
    </article>
    <br />

    <article class="tituloSeccion">
        Validación de Oficio
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

    <article class="formulario">
        <table>
            <tr>
                <th></th>
                <td>
                    <asp:RadioButtonList runat="server" ID="rblGestionar" AutoPostBack="true">
                        <asp:ListItem Value="1" Text="Gestionar"></asp:ListItem>
                        <asp:ListItem Value="0" Text="Liquidar"></asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator runat="server" ID="rfvRblGestionar" Enabled="false" ControlToValidate="rblGestionar" Display="Dynamic" ErrorMessage="Debe seleccionar la acción a realizar" ValidationGroup="Aceptar"></asp:RequiredFieldValidator>
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
        <asp:Button runat="server" ID="btnAceptar" Text="Aceptar" ValidationGroup="Aceptar" />
        <input id="btnCancelar" type="button" value="Regresar" />
    </article>

    <article id="arPopupDelFormulario"></article>
    <article id="arAlertasDelFormulario"></article>
    <article id="arpopupConfirmaDeseaBorrar"></article>
    <article id="arPopupGenerico"></article>

    <script type="text/javascript">

        function mostrarAlertaLlaveIncorrecta() {
            deshabilitarControl('#<%=btnAceptar.ClientID%>');
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
            deshabilitarControl('#<%=btnAceptar.ClientID%>');
            $('.formulario').attr('disabled', 'disabled');

            mostrarAlerta(
            '#arAlertasDelFormulario',
            {
                mensaje: 'Se ha actualizado la información de la validación de oficio.',
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

        });

        function regresarAlListado() {
            window.location = 'Lst_OT_VistoBuenoJefaturaSeccionMantenimiento.aspx';
        };

    </script>

</asp:Content>

