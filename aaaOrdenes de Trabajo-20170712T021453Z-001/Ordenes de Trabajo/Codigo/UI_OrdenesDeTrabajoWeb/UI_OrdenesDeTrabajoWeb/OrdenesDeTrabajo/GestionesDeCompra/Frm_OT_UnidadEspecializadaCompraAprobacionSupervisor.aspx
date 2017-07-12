<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/Mp_Formulario.master" AutoEventWireup="false" CodeFile="Frm_OT_UnidadEspecializadaCompraAprobacionSupervisor.aspx.vb" Inherits="OrdenesDeTrabajo_GestionesDeCompra_Frm_OT_UnidadEspecializadaCompraAprobacionSupervisor" %>

<%@ Register TagPrefix="wuc" Namespace="Utilerias.WebForms.Controles" Assembly="Utilerias.WebForms.Controles" %>
<%@ Register Src="~/Controles/wuc_OT_Lineas_Material_Gestion_Compra.ascx" TagName="wuc_LineasMaterialGestionCompra" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphFormulario" runat="Server">
        
    <header>
        <h2>Gestión de Compras por Unidad Espacializada de Compra</h2>
    </header>

    <article class="tituloSeccion">
        Datos de la Gestión
    </article>

    <article class="formulario ">
        <table>
            <tr>
                <th>Número de Gestión</th>
                <td>
                    <asp:Label runat="server" ID="lblNumGestion"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>Observaciones</th>
                <td>
                    <asp:TextBox runat="server" ID="txtObservacionesConsulta" TextMode="MultiLine" ReadOnly="true" Rows="4" Columns="40"></asp:TextBox>
                </td>
            </tr>
        </table>
    </article>

    <article class="tituloSeccion">
        Materiales a Gestionar
    </article>

    <br />
    <asp:Repeater runat="server" ID="rpMateriales">
        <ItemTemplate>
            <article runat="server" id="arAcordeon" class="formulario sinBorde" data-tipo="acordeon">
                <article class="encabezadoAcordeon">
                    <a runat="server" id="ancorAcordeon" class="tituloAcordeon">
                        <asp:Label runat="server" ID="lblNumeroLinea" Text='<%# String.Format("No. de Linea: {0}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GRUPO_GESTION_COMPRALST.NUMERO_LINEA))%>'></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Label runat="server" ID="lblPartida" Text='<%# String.Format("Partida: {0}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GRUPO_GESTION_COMPRALST.PARTIDA_PRESUPUESTARIA))%>'></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Label runat="server" ID="lblCodigoMaterial" Text='<%# String.Format("Código: {0}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GRUPO_GESTION_COMPRALST.ID_MATERIAL))%>'></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Label runat="server" ID="lblDescMaterial" Text='<%# String.Format("Descripción: {0}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GRUPO_GESTION_COMPRALST.DESCRIPCION))%>'></asp:Label>
                        &nbsp;&nbsp;&nbsp;&nbsp;                     
                        <asp:Label runat="server" ID="lblCantidad" Text='<%# String.Format("Cantidad: {0}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GRUPO_GESTION_COMPRALST.CANTIDAD_SOLICITADA_MEDIDA))%>'></asp:Label>
                    </a>
                </article>
                <article runat="server" id="cuerpoAcordeon1" class="cuerpoAcordeon">
                    <uc1:wuc_LineasMaterialGestionCompra runat="server" ID="wucLineasMaterialGestionCompra" IdUbicacion='<%# Eval("ID_UBICACION")%>' IdViaCompraContrato='<%# Eval("ID_VIA_COMPRA_CONTRATO")%>' IdAnnio='<%# Eval("ANNO")%>' IdNumeroGestion='<%# Eval("NUMERO_GESTION")%>' IdMaterial='<%# Eval("ID_MATERIAL")%>' />
                </article>
            </article>
        </ItemTemplate>
    </asp:Repeater>
    <br />

    <article class="tituloSeccion">
        Datos de la Revisión
    </article>

    <asp:UpdatePanel runat="server" ID="upObservaciones" UpdateMode="Conditional">
        <ContentTemplate>
            <article class="formulario">
                <table>
                    <tr>
                        <td>
                            <asp:RadioButton ID="rbtAprobada" ValidationGroup="Aceptar" Checked="true" runat="server" Text="Aprobar" GroupName="Condicion" AutoPostBack="true" />
                        </td>
                        <td>
                            <asp:RadioButton ID="rbtDevuelta" ValidationGroup="Aceptar" runat="server" Text="Devolver" GroupName="Condicion" AutoPostBack="true" />
                        </td>
                    </tr>
                    <tr runat="server" id="trObservaciones">
                        <th>Observaciones</th>
                        <td>
                            <asp:TextBox runat="server" ID="txtObservacion" TextMode="MultiLine" Rows="4" Columns="60"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" Enabled="false" ID="rfvTxtObservaciones" ControlToValidate="txtObservacion"
                                Display="Dynamic" ErrorMessage="Las observaciones son obligatorias." ValidationGroup="Aceptar">&nbsp;</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                </table>
            </article>
        </ContentTemplate>
    </asp:UpdatePanel>

    <article class="areaBotones">
        <asp:Button runat="server" ID="btnAceptar" Text="Aceptar" ValidationGroup="Aceptar"/>
        <asp:Button runat="server" ID="btnCancelar" />
    </article>

    <article id="arAlerta"></article>
    <article id="arpopupConfirmaDeseaBorrar"></article>
    <article id="arPopupGenerico"></article>
    <article id="arAlertasDelFormulario"></article>
    <article id="arpopupConfirmaEnviar"></article>

    <script type="text/javascript">

        function mostrarAlertaGuardadoExitoso() {
            deshabilitarControl('#<%=btnAceptar.ClientID%>');
              deshabilitarControl('#<%=btnCancelar.ClientID%>');
              $('.formulario').attr('disabled', 'disabled');

              mostrarAlerta(
              '#arAlertasDelFormulario',
              {
                  mensaje: 'Se ha guardado los datos de la gestión',
                  tipo: "exito",
                  transparencia: 0.9,
                  posicion: 'center',
                  onClosed: function () { window.location = 'Lst_OT_UnidadEspecializadaCompraAprobacionSupervisor.aspx'; }
              });
          };

        function irAListado() {
            window.location = 'Lst_OT_UnidadEspecializadaCompraAprobacionSupervisor.aspx';
        };

        function mostrarAlertaError(pvc_Mensaje) {
            mostrarAlerta(
                '#arAlerta',
                {
                    mensaje: pvc_Mensaje,
                    tipo: 'peligro',
                    transparencia: 0.9,
                    posicion: 'center'
                }
            );
        };

        $(document).ready(function () {

            $('article[data-tipo="acordeon"]').each(function () {
                configurarAcordeon('#' + this.id, { seleccionMultiple: false, eventoApertura: 'click' });
            });
            
        });

        function MensajePopup(pvc_Mensaje, pvc_PaginaDestino) {

            var vlo_ConfiguracionPopup = {
                titulo: '<em>Mensajes del Sistema</em>',
                mensaje: pvc_Mensaje,
                onClosed: function (e) {
                    $(this).removeAttr('href');
                    cerrarPopup();
                    if (pvc_PaginaDestino != '') {
                        redireccionarListado(pvc_PaginaDestino);
                    }
                },

                botones:
            [
                {
                    idControl: "btnAceptar",
                    textoBoton: "Aceptar",
                    onClick: function () {
                        cerrarPopup();
                        if (pvc_PaginaDestino != '') {
                            redireccionarListado(pvc_PaginaDestino);
                        }
                    }
                }
            ]
            };

            $('#arPopupGenerico').popup(vlo_ConfiguracionPopup);

            window.location = '#arPopupGenerico';
        };

        function redireccionarListado(pvc_PaginaDestino) {
            window.location = pvc_PaginaDestino
        };

    </script>

</asp:Content>

