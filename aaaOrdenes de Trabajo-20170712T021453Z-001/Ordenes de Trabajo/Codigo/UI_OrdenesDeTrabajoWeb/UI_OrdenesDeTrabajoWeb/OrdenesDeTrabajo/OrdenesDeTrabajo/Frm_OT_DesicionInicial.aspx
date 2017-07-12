<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/Mp_Formulario.master" AutoEventWireup="false" CodeFile="Frm_OT_DesicionInicial.aspx.vb" Inherits="OrdenesDeTrabajo_Frm_OT_DesicionInicial" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphFormulario" runat="Server">

    <header>
        <h2>
            <asp:Label runat="server" ID="lblAccion" Text="Desición Inicial"></asp:Label>
        </h2>
    </header>

    <article class="tituloSeccion">
        Encabezado
    </article>

    <article class="formulario">
        <table>
            <tr>
                <th>Nombre del Proyecto</th>
                <td>
                    <asp:Label runat="server" ID="lblNombreProyecto" data-tipocontrol="etiqueta"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>Número de Orden de Trabajo</th>
                <td>
                    <asp:Label runat="server" ID="lblNumOT" data-tipocontrol="etiqueta"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>Fecha</th>
                <td>
                    <asp:Label runat="server" ID="lblFecha" data-tipocontrol="etiqueta"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>Tipo de Obra</th>
                <td>
                    <asp:DropDownList Width="40%" runat="server" ID="ddlTipoObra" AppendDataBoundItems="true" data-tipoControl="combo"></asp:DropDownList>
                    <asp:RequiredFieldValidator runat="server" ID="rfvDdlTipoObra" ControlToValidate="ddlTipoObra" Display="Dynamic" ErrorMessage="El tipo obra es requerido." ValidationGroup="Aceptar">&nbsp;</asp:RequiredFieldValidator>
                </td>
            </tr>
        </table>
    </article>

    <br />

    <article class="tituloSeccion">
        Rubros
    </article>
    
   
    <article class="listado">
         <br />
        <asp:Repeater runat="server" ID="rpRubro">
            <HeaderTemplate>
                <table>
                    <tr>
                        <th>Rubro</th>
                        <th>Descripción</th>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr class="lineaDelListado">
                    <td>
                        <%#Container.ItemIndex + 1%>
                        )
                        <%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_RUBRO_DECISION_INICIALST.DESCRIPCION)%>
                    </td>
                    <td>
                        <asp:HiddenField  runat="server" ID="hdfIdRubroDecInic" Value="<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_RUBRO_DECISION_INICIALST.ID_RUBRO_DECISION_INICIA)%>" />
                        <asp:TextBox runat="server" ID="txtDescripcionRubro" TextMode="MultiLine" Rows="4" Columns="50"></asp:TextBox>
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>

    </article>

    <article class="areaBotones">
        <asp:Button runat="server" ID="btnAceptar" Text="Guardar" ValidationGroup="Aceptar" />
        <asp:Button runat="server" ID="btnReporte" Text="Imprimir Reporte" />
        <input id="btnCancelar" type="button" value="Regresar" />
    </article>

    <article id="arPopupDelFormulario"></article>
    <article id="arAlertasDelFormulario"></article>
    <article id="arPopupGenerico"></article>

    <script type="text/javascript">

        function regresarAlListado() {
            window.location = 'Frm_OT_FinalizacionEntregaDis.aspx';
        };

        function deshabilitarFormulario() {
            deshabilitarControl('#<%=Me.btnAceptar.ClientID%>');
            deshabilitarControl('#<%=Me.btnReporte.ClientID%>');
            deshabilitarControl('#btnCancelar');
            $('.formulario').attr('disabled', 'disabled');
        }

        function mostrarAlertaActualizacionExitosa() {
            deshabilitarFormulario();

            mostrarAlerta(
                '#arAlertasDelFormulario',
                {
                    mensaje: 'Se ha actualizado la información de la desición inicial.',
                    tipo: "exito",
                    transparencia: 0.9,
                    posicion: 'center',
                    permiteCerrar: true,
                    onClosed: function () { regresarAlListado(); }
                }
            );
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

        function mostrarAlertaLlaveIncorrecta() {
            deshabilitarControl('#<%=btnAceptar.ClientID%>');
            deshabilitarControl('#<%=Me.btnReporte.ClientID%>');
            deshabilitarControl('#btnCancelar');
            $('.formulario').attr('disabled', 'disabled');

            mostrarAlerta(
                '#arAlertasDelFormulario',
                {
                    mensaje: 'El identificador provisto no pertenece a ningun registro.',
                    tipo: "peligro",
                    transparencia: 0.9,
                    posicion: 'center',
                    onClosed: function () { regresarAlListado(); }
                });
        };

        function mostrarPopUp(pvc_IdPopup) {
            window.location = pvc_IdPopup;
        };

        $(document).ready(function () {
            inicializarScript();
        });

        function inicializarScript() {

            $('#btnCancelar').click(function () {
                regresarAlListado();
            });

            habilitarTooltipGenerico();
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

