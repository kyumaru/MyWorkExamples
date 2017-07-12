<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/Mp_Formulario.master" AutoEventWireup="false" CodeFile="Frm_OT_ConsultaGestiónCompraFondoTrabajo.aspx.vb" Inherits="OrdenesDeTrabajo_GestionesDeCompra_Frm_OT_ConsultaGestiónCompraFondoTrabajo" %>

<%@ Register TagPrefix="wuc" Namespace="Utilerias.WebForms.Controles" Assembly="Utilerias.WebForms.Controles" %>
<%@ Register Src="~/Controles/wuc_OT_Lineas_Material_Gestion_Compra.ascx" TagName="wuc_LineasMaterialGestionCompra" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphFormulario" runat="Server">

    <header>
        <h2>Gestión de Compras por Fondo de Trabajo</h2>
    </header>

    <article class="tituloSeccion">
        Gestión de Compras por Fondo de Trabajo
    </article>

    <article class="formulario">
        <table>
            <tr>
                <th>Observaciones</th>
                <td>
                    <asp:TextBox Width="70%" runat="server" ID="txtObservaciones" TextMode="MultiLine" Rows="5" ReadOnly="true"></asp:TextBox>
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
                        <asp:Label runat="server" ID="lblCodigoMaterial" Text='<%# String.Format("Código: {0}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_LINEA_GC_GROUP_FONDO.ID_MATERIAL))%>'></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Label runat="server" ID="lblDescMaterial" Text='<%# String.Format("Descripción: {0}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_LINEA_GC_GROUP_FONDO.DESCRIPCION))%>'></asp:Label>   &nbsp;&nbsp;&nbsp;&nbsp;                     
                        <asp:Label runat="server" ID="lblCantidad" Text='<%# String.Format("Cantidad: {0}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_LINEA_GC_GROUP_FONDO.CANTIDAD_SOLICITADA_MEDIDA))%>'></asp:Label>
                    </a>
                </article>
                <article runat="server" id="cuerpoAcordeon1" class="cuerpoAcordeon">
                    <uc1:wuc_LineasMaterialGestionCompra runat="server" ID="wucLineasMaterialGestionCompra" IdUbicacion='<%# Eval("ID_UBICACION")%>' IdViaCompraContrato='<%# Eval("ID_VIA_COMPRA_CONTRATO")%>' IdAnnio='<%# Eval("ANNO")%>' IdNumeroGestion='<%# Eval("NUMERO_GESTION")%>' IdMaterial='<%# Eval("ID_MATERIAL")%>'/>
                </article>
            </article>
        </ItemTemplate>
    </asp:Repeater>
    <br />
    
    <article class="areaBotones">
        <asp:Button runat="server" ID="btnCancelar"/>
        <%--<asp:Button runat="server" ID="btnGuardar" Text="Guardar" />--%>
        <asp:Button runat="server" ID="btnSiguiente" Text="Siguiente" />
    </article>

    <article id="arAlerta"></article>
    <article id="arpopupConfirmaDeseaBorrar"></article>
    <article id="arPopupGenerico"></article>
    <article id="arAlertasDelFormulario"></article>
    <article id="arpopupConfirmaEnviar"></article>    

    <script type="text/javascript">

        function irASeleccion() {
            window.location = 'Frm_OT_GestionCompraFondoTrabajo.aspx';
        };

        function irAInclusionProveedores() {
            window.location = 'Frm_OT_InclusionProveedores.aspx';
        };

        function irAListado() {
            window.location = 'Lst_OT_GestiónCompraListado.aspx';
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

            $('#<%=btnSiguiente.ClientID%>').click(function () { return mostrarPopupConfirmaDeseaEnviarRegistro(); });

        });

        function mostrarPopupConfirmaDeseaEnviarRegistro() {
            var vlo_ConfiguracionPopup = {
                titulo: 'Gestión de Compras por Fondo de Trabajo',
                mensaje: 'Esta acción dará por concluida la selección de materiales para Gestionar la Orden, ¿Desea Continuar?',
                botones:
                        [
                            {
                                idControl: "btnSi",
                                textoBoton: "Si",
                                onClick:
                                    function (e) {
                                        $(this).attr("disabled", "disabled");
                                        __doPostBack('<%=btnSiguiente.UniqueID%>', '');
                                    }
                            },
                            {
                                idControl: "btnNo",
                                textoBoton: "No",
                                onClick: function () { cerrarPopup(); }
                            },
                            {
                                idControl: "btnCancelar",
                                textoBoton: "Cancelar",
                                onClick: function () { cerrarPopup(); }
                            }
                        ]
            };

            $('#arpopupConfirmaEnviar').popup(vlo_ConfiguracionPopup);

            window.location = '#arpopupConfirmaEnviar';

            return false;
        };

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

