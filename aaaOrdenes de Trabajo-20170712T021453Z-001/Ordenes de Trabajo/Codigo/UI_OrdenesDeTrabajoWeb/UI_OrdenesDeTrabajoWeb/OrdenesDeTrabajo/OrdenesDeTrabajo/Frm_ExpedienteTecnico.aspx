<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/Mp_Formulario.master" AutoEventWireup="false" CodeFile="Frm_ExpedienteTecnico.aspx.vb" Inherits="OrdenesDeTrabajo_Frm_ExpedienteTecnico" %>

<%@ Register TagPrefix="wuc" TagName="InformacionGeneral" Src="~/ControlesDeUsuario/Wuc_OT_InformacionGeneral.ascx" %>
<%@ Register Src="~/Controles/wuc_OT_Expediente_Tecnico.ascx" TagName="wuc_ExpedienteTecnico" TagPrefix="uc1" %>
<%@ Register TagPrefix="wuc" Namespace="Utilerias.WebForms.Controles" Assembly="Utilerias.WebForms.Controles" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphFormulario" runat="Server">

    <header>
        <h2>
            <asp:Label runat="server" ID="lblAccion" Text="Expediente Técnico"></asp:Label>
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
        Ordenes de Trabajo Asociadas
    </article>

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
                        <uc1:wuc_ExpedienteTecnico runat="server" ID="wucExpedienteTecnico" IdEtapaOrdenTrabajo='<%# Eval("ID_ETAPA_ORDEN_TRABAJO")%>' />
                    </article>
                </ItemTemplate>
            </asp:Repeater>
        </article>

    </article>

    <br />

    <article class="areaBotones">
        <asp:Button runat="server" ID="btnAceptar" Text="Guardar" />
        <input id="btnCancelar" type="button" value="Regresar" />
    </article>

    <article id="arPopupDelFormulario"></article>
    <article id="arAlertasDelFormulario"></article>
    <article id="arpopupConfirmaDeseaBorrar"></article>
    <article id="arPopupGenerico"></article>


    <script type="text/javascript">

        function regresarAlListado() {
            window.location = 'Frm_OT_FinalizacionEntregaDis.aspx';
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


        function deshabilitarFormulario() {
            deshabilitarControl('#<%=Me.btnAceptar.ClientID%>');
            deshabilitarControl('#btnCancelar');
            $('.formulario').attr('disabled', 'disabled');
        }

        function mostrarAlertaActualizacionExitosa() {
            deshabilitarFormulario();

            mostrarAlerta(
                '#arAlertasDelFormulario',
                {
                    mensaje: 'Se ha actualizado la información del expediente técnico.',
                    tipo: "exito",
                    transparencia: 0.9,
                    posicion: 'center',
                    permiteCerrar: true,
                    onClosed: function () { regresarAlListado(); }
                }
            );
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
            $('#btnCancelar').click(function () {
                regresarAlListado();
            });

            configurarTabPanel();

        });

    </script>

</asp:Content>

