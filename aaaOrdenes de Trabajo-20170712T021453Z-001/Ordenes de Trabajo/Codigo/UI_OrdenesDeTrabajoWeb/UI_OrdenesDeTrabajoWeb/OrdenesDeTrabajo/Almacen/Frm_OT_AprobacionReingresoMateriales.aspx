<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/Mp_Formulario.master" AutoEventWireup="false" CodeFile="Frm_OT_AprobacionReingresoMateriales.aspx.vb" Inherits="OrdenesDeTrabajo_Almacen_Frm_OT_AprobacionReingresoMateriales" %>

<%@ Register TagPrefix="wuc" TagName="InformacionGeneral" Src="~/ControlesDeUsuario/Wuc_OT_InformacionGeneral.ascx" %>
<%@ Register TagPrefix="wuc" Namespace="Utilerias.WebForms.Controles" Assembly="Utilerias.WebForms.Controles" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphFormulario" runat="Server">

    <header>
        <h2>
            <asp:Label runat="server" ID="lblAccion" Text="Reingreso de Materiales"></asp:Label>
        </h2>
    </header>

    <article class="tituloSeccion">
        Información General de la OT
    </article>
    <article class="formulario sinBorde">
        <wuc:InformacionGeneral runat="server" ID="ctrl_InfoGeneral" />
    </article>
    <br />

    <article class="tituloSeccion">
        Reingreso de Materiales
    </article>
    <article class="listado">
        <br />
        <asp:Repeater runat="server" ID="rpSolicitudes">
            <HeaderTemplate>
                <table>
                    <tr>
                        <th>
                            Código
                        </th>
                        <th>
                            Descripción
                        </th>
                        <th>
                            Cantidad Solicitada
                        </th>
                        <th>
                            Cantidad Retirada
                        </th>
                        <th>
                            Tipo de Solicitud
                        </th>
                        <th>
                            Cantidad a Reingresar
                        </th>
                        <th>Cantidad Recibida
                        </th>
                        <th>&nbsp;</th>
                    </tr>
            </HeaderTemplate>

            <ItemTemplate>
                <tr class="lineaDelListado">
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_SOLICITUD_REINGRESOLST.ID_MATERIAL)%></td>
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_SOLICITUD_REINGRESOLST.DESCRIPCION)%></td>
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_SOLICITUD_REINGRESOLST.CANTIDAD_SOLICITADA_MEDIDA)%></td>
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_SOLICITUD_REINGRESOLST.CANTIDAD_RETIRADA_MEDIDA)%></td>
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_SOLICITUD_REINGRESOLST.DESC_TIPO_SOL_REIN)%></td>
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_SOLICITUD_REINGRESOLST.CANTIDAD_REINGRESO_MEDIDA)%></td>
                    <td>
                        <asp:TextBox runat="server" ID="txtCantidadRecibida" Width="40%" ></asp:TextBox>
                    </td>
                    <td>
                        <asp:HiddenField runat="server" ID="hdfAnno" Value="<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_SOLICITUD_REINGRESOLST.ANNO)%>" />
                        <asp:HiddenField runat="server" ID="hdfIdSolicitudIngreso" Value="<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_SOLICITUD_REINGRESOLST.ID_SOLICITUD_REINGRESO)%>" />
                    </td>                    
                </tr>
            </ItemTemplate>

            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
    </article>

    <article class="areaCantidadDeRegistro">
        <asp:Label runat="server" ID="lblCantidadDeRegistros" Text="" Visible="true"></asp:Label>
    </article>

    <br />

    <article class="areaBotones">
        <asp:Button runat="server" ID="btnGuardarYFinalizar" Text="Reingresar Material" />
        <asp:Button runat="server" ID="btnRegresar" Text="Regresar" />
    </article>

    <article id="arAlerta"></article>
    <article id="arpopupConfirmaDeseaBorrar"></article>
    <article id="popupConfirmaDeseaBorrar"></article>
    <article id="popupConfirmacionDeseaBorrar"></article>
    <article id="arPopupGenerico"></article>
    <article id="arPopupDelFormulario"></article>
    <article id="arAlertasDelFormulario"></article>

    <script type="text/javascript">

        function mostrarAlertaActualizacionExitosa() {
            deshabilitarControl('#<%=btnGuardarYFinalizar.ClientID%>');
            deshabilitarControl('#<%=btnRegresar.ClientID%>');
            $('.formulario').attr('disabled', 'disabled');

            mostrarAlerta(
            '#arAlertasDelFormulario',
            {
                mensaje: 'La información ha sido aprobada correctamente.',
                tipo: "exito",
                transparencia: 0.9,
                posicion: 'center',
                onClosed: function () { regresarAlListado(); }
            });
        };

        function regresarAlListado() {
            __doPostBack('<%=Me.btnRegresar.UniqueID%>', '');
        };

        $(document).ready(function () {

        });

        function mostrarPopUp(pvc_IdPopup) {
            window.location = pvc_IdPopup;
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

    </script>

</asp:Content>

