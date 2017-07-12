<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/Mp_Listado.master" AutoEventWireup="false" CodeFile="Lst_OT_SeleccionEspacios.aspx.vb" Inherits="OrdenesDeTrabajo_Lst_OT_SeleccionEspacios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphFormulario" runat="Server">

    <header>
        <h2>Selección de Espacios</h2>
    </header>

    <article data-grupo="Listado" class="tituloSeccion">
        Selección de Espacios 
    </article>

    <article data-grupo="Listado" class="listado">
        <br />
        <asp:Repeater runat="server" ID="rpEspacio">
            <HeaderTemplate>
                <table>
                    <tr>
                        <th>&nbsp;</th>
                        <th>
                            <asp:LinkButton runat="server" ID="lnkOrden" Text="Orden" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_ESPACIOLST.ORDEN%>" CommandArgument="ASC" OnCommand="lnkRpEspacio_Command"></asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton runat="server" ID="lnkEspacio" Text="Espacio" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_ESPACIOLST.DESCRIPCION%>" CommandArgument="ASC" OnCommand="lnkRpEspacio_Command"></asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton runat="server" ID="lnkSubComponentes" Text="Cantidad Sub Componentes" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_ESPACIOLST.CANTIDAD_SUBCOMPONENTES%>" CommandArgument="ASC" OnCommand="lnkRpEspacio_Command"></asp:LinkButton>
                        </th>
                        <th>&nbsp;</th>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr class="lineaDelListado">
                    <td>
                        <asp:CheckBox runat="server" ID="chkEspacio" />
                    </td>
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_ESPACIOLST.ORDEN)%></td>
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_ESPACIOLST.DESCRIPCION)%></td>
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_ESPACIOLST.CANTIDAD_SUBCOMPONENTES)%></td>
                    <td>
                        <asp:HiddenField runat="server" ID="hdfIdEspacio" Value="<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_ESPACIOLST.ID_ESPACIO)%>" />
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
    </article>

    <article class="areaBotones">
        <asp:Button runat="server" ID="btnRegresar" Text="Regresar" />
        <asp:Button runat="server" ID="btnSiguiente" Text="Siguiente" />        
        <input id="btnCancelar" type="button" value="Cancelar" />
    </article>

    <article id="arAlerta"></article>
    <article id="arPopupGenerico"></article>

    <script type="text/javascript">

        function MensajePopup(pvc_Mensaje, pvc_PaginaDestino) {

            var vlo_ConfiguracionPopup = {
                titulo: '<em>Mensajes del Sistema</em>',
                mensaje: pvc_Mensaje,
                onClosed: function (e) {
                    $(this).removeAttr('href');
                    cerrarPopup();
                    if (pvc_PaginaDestino != '') {
                        redireccionar(pvc_PaginaDestino);
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
                            redireccionar(pvc_PaginaDestino);
                        }
                    }
                }
            ]
            };

            $('#arPopupGenerico').popup(vlo_ConfiguracionPopup);

            window.location = '#arPopupGenerico';
        };

        function redireccionar(pvc_PaginaDestino) {
            window.location = pvc_PaginaDestino
        };

        function mostrarAlertaActualizacionExitosa() {

            //mostrarAlerta(
            //    '#arPopupGenerico',
            //    {
            //        mensaje: 'La selección de espacio ha sido exitosa.',
            //        tipo: "exito",
            //        transparencia: 0.9,
            //        posicion: 'center',
            //        onClosed: function () { irAMatriz(); }
            //    });

            irAMatriz();
        };

        function irAMatriz() {
            window.location = 'Frm_OT_MatrizOrdenTrabajo.aspx';
        };
        
        $(document).ready(function () {
            $('#btnCancelar').click(function () {
                regresarAlListado();
            });
        });

        function regresarAlListado() {
            window.location = '<%=Me.PantallaRetorno%>';
        };

    </script>

</asp:Content>

