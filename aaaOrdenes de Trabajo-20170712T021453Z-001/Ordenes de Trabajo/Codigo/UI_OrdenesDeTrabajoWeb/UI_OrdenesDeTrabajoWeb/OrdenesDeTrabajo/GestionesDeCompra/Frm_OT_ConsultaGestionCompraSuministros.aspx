<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/Mp_Formulario.master" AutoEventWireup="false" CodeFile="Frm_OT_ConsultaGestionCompraSuministros.aspx.vb" Inherits="OrdenesDeTrabajo_GestionesDeCompra_Frm_OT_ConsultaGestionCompraSuministros" %>

<%@ Register TagPrefix="wuc" Namespace="Utilerias.WebForms.Controles" Assembly="Utilerias.WebForms.Controles" %>
<%@ Register Src="~/Controles/wuc_OT_Lineas_Material_Detalle_Producto.ascx" TagName="wuc_OT_Lineas_Material_Detalle_Producto" TagPrefix="uc1" %>
<%@ Register Src="~/Controles/wuc_OT_Lineas_Material_Gestion_Compra.ascx" TagName="wuc_LineasMaterialGestionCompra" TagPrefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphFormulario" runat="Server">
    <header>
        <h2>Gestión de Compras por Suministros</h2>
    </header>

    <article class="tituloSeccion" runat="server" id="trTitulo">
        Gestión de Compras por Suministros
    </article>

    <article class="tituloSeccion" runat="server" id="trMatGestionar">
        Materiales a Gestionar
    </article>

    <br />
    <asp:Repeater runat="server" ID="rpMateriales">
        <ItemTemplate>
            <article runat="server" id="arAcordeon" class="formulario sinBorde" data-tipo="acordeon">
                <article class="encabezadoAcordeon">
                    <a runat="server" id="ancorAcordeon" class="tituloAcordeon">
                        <asp:Label runat="server" ID="lblCodigoMaterial" Text='<%# String.Format("Código: {0}", Eval(Utilerias.OrdenesDeTrabajo.Tipos.TP_MAT_SIMINISTROS.ID_MATERIAL))%>'></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Label runat="server" ID="lblDescMaterial" Text='<%# String.Format("Descripción: {0}", Eval(Utilerias.OrdenesDeTrabajo.Tipos.TP_MAT_SIMINISTROS.NOMBRE_MATERIAL))%>'></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Label runat="server" ID="lblCantidad" Text='<%# String.Format("Cantidad: {0}", Eval(Utilerias.OrdenesDeTrabajo.Tipos.TP_MAT_SIMINISTROS.CANTIDAD_SOLICITADA))%>'></asp:Label>
                    </a>
                </article>
                <article runat="server" id="cuerpoAcordeon1" class="cuerpoAcordeon">
                    <uc1:wuc_OT_Lineas_Material_Detalle_Producto runat="server" ID="wuc_OT_Lineas_Material_Detalle_Producto" IdMaterial='<%# Eval(Utilerias.OrdenesDeTrabajo.Tipos.TP_MAT_SIMINISTROS.ID_MATERIAL)%>' HileraMateriales='<%# Me.HileraMateriales%>' GestionCompra='<%# Me.GestionCompra%>'/>
                </article>
            </article>
        </ItemTemplate>
    </asp:Repeater>
    <br />

    <article class="formulario sinBorde" runat="server" id="trNumGestionGeco">
        <table>
            <tr>
                <th>Número de gestión de geco</th>
                <td>
                    <asp:TextBox Width="40%" runat="server" ID="txtNumGestionGECO"></asp:TextBox>
                </td>
            </tr>
        </table>
    </article>

    <article class="areaBotones">
        <asp:Button runat="server" ID="btnCancelar" />
        <asp:Button runat="server" ID="btnGuardar" Text="Guardar" />
        <asp:Button runat="server" ID="btnFinalizar" Text="Finalizar" Visible="false" />
    </article>

    <article id="arAlerta"></article>
    <article id="arpopupConfirmaDeseaBorrar"></article>
    <article id="arPopupGenerico"></article>
    <article id="arAlertasDelFormulario"></article>
    <article id="arpopupConfirmaEnviar"></article>

    <article style="visibility:hidden">
        <asp:LinkButton runat="server" ID="lnkAceptarAux"></asp:LinkButton>
    </article>

    <script type="text/javascript">

        function irASeleccion() {
            window.location = 'Frm_OT_GestionCompraFondoTrabajo.aspx';
        };

        function regresarAlListadoGestion() {
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

            $('#<%=btnFinalizar.ClientID%>').click(function () { return mostrarPopupConfirmaDeseaEnviarRegistro(); });



        });

        function mostrarPopupConfirmaDeseaEnviarRegistro() {
            var EstadoGestion = '<%= GestionCompra.Estado()%>';
            alert(EstadoGestion);
            if (EstadoGestion = "CRE") {
                var vlo_ConfiguracionPopup = {
                    titulo: 'Gestión de Compras por Suministros',
                    mensaje: 'Esta acción dará por concluida la selección de materiales para Gestionar la Orden, ¿Desea Continuar?',
                    botones:
                                [
                                    {
                                        idControl: "btnSi",
                                        textoBoton: "Si",
                                        onClick:
                                            function (e) {
                                                $(this).attr("disabled", "disabled");
                                                var vlo_Boton = document.getElementById('<%=lnkAceptarAux.ClientID%>');
                                            vlo_Boton.click();
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
            }
            else
            {
                var vlo_Boton = document.getElementById('<%=lnkAceptarAux.ClientID%>');
                vlo_Boton.click();
            
            };}
            



        function redireccionarListado(pvc_PaginaDestino) {
            window.location = pvc_PaginaDestino
        };

        function mostrarAlertaRegistroExitoso() {
            deshabilitarControl('#<%=btnCancelar.ClientID%>');
            deshabilitarControl('#<%=btnGuardar.ClientID%>');
            deshabilitarControl('#<%=btnFinalizar.ClientID%>');
            $('.formulario').attr('disabled', 'disabled');

            mostrarAlerta(
                '#arAlertasDelFormulario',
                {
                    mensaje: 'Se ha tramitado la gestión de compra.',
                    tipo: "exito",
                    transparencia: 0.9,
                    posicion: 'top',
                    onClosed: function () { regresarAlListadoGestion(); }
                });
        }

    </script>
</asp:Content>

