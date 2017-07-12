<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/Mp_Formulario.master" AutoEventWireup="false" CodeFile="Frm_OT_AutorizacionOrdenTrabajo.aspx.vb" Inherits="OrdenesDeTrabajo_Frm_OT_AutorizacionOrdenTrabajo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphFormulario" runat="Server">

    <header>
        <h2>
            <asp:Label runat="server" Text="Revisión de Orden de Trabajo"></asp:Label>
        </h2>
    </header>

    <article class="tituloSeccion">
        Datos de la Orden de Trabajo
    </article>

    <article class="formulario" id="formularioTotal">
        <table>
            <tr>
                <th>Fecha de Solicitud</th>
                <td>
                    <asp:Label runat="server" ID="lblFechaSolicitud" data-tipocontrol="etiqueta"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>Solicitante</th>
                <td>
                    <asp:Label runat="server" ID="lblSolicitante" data-tipocontrol="etiqueta"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>Persona Contacto</th>
                <td>
                    <asp:Label runat="server" ID="lblPersonaContacto" data-tipocontrol="etiqueta"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>Teléfono</th>
                <td>
                    <asp:Label runat="server" ID="lblTelefono" data-tipocontrol="etiqueta"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>Edificio</th>
                <td>
                    <asp:Label runat="server" ID="lblEdificio" data-tipocontrol="etiqueta"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>Lugar Exacto</th>
                <td>
                    <asp:Label runat="server" ID="lblLugarExacto" data-tipocontrol="etiqueta"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>Categoria de Servicio</th>
                <td>
                    <asp:Label runat="server" ID="lblCategServ" data-tipocontrol="etiqueta"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>Actividad</th>
                <td>
                    <asp:Label runat="server" ID="lblActividad" data-tipocontrol="etiqueta"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>Descripción de Trabajo</th>
                <td>
                    <asp:TextBox runat="server" ID="txtDescTrabajo" data-tipocontrol="texto" Enabled="false" TextMode="MultiLine" Rows="5" Columns="40"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>Fotografía(s)</th>
                <td>
                    <asp:Repeater runat="server" ID="rpAdjunto">
                        <ItemTemplate>
                            <asp:LinkButton runat="server" ID="lnkArchivo"
                                CommandArgument='<%#Container.ItemIndex%>'
                                Style="text-decoration: underline; color: blue;"
                                OnCommand="lnkArchivo_Command"
                                Text="<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.NOMBRE_ARCHIVO)%>"></asp:LinkButton>
                            <br />
                        </ItemTemplate>
                    </asp:Repeater>
                </td>
            </tr>
        </table>
    </article>

    <article class="tituloSeccion">
        Datos de la Revisión
    </article>

    <article class="formulario">
        <table>
            <tr>
                <th>Condición</th>
                <td>
                    <asp:RadioButton ID="rbtAprobada" runat="server" Text="Aprobada" GroupName="Condicion" AutoPostBack="true" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:RadioButton ID="rbtDevuelta" runat="server" Text="Devuelta" GroupName="Condicion" AutoPostBack="true" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:RadioButton ID="rbtDenegana" runat="server" Text="Denegada" GroupName="Condicion" AutoPostBack="true" />
                </td>
            </tr>
            <tr>
                <th>Justificación</th>
                <td>
                    <asp:TextBox Width="59%" runat="server" ID="txtJustificacion" data-tipocontrol="texto" TextMode="MultiLine" Rows="5"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ID="rfvTxtJustificacion" Enabled="false" ControlToValidate="txtJustificacion" Display="Dynamic" ErrorMessage="La justificación es Obligatoria." ValidationGroup="Aceptar"></asp:RequiredFieldValidator>
                </td>
            </tr>
        </table>
    </article>

    <article class="areaBotones">
        <asp:Button runat="server" ID="btnAceptar" Text="Aceptar" ValidationGroup="Aceptar" />
        <%--<input id="btnLimpiarFormulario" type="button" data-tipo="limpiarFormulario" value="Limpiar formulario" />--%>
        <input id="btnCancelar" type="button" value="Cancelar" />
    </article>

    <article id="arAlertasDelFormulario"></article>
    <article id="arpopupConfirmaDeseaProcesar"></article>

    <script type="text/javascript">

        function regresarAlListado() {
            window.location = 'Lst_OT_AutorizacionOrdenTrabajo.aspx';
        };

        function mostrarAlertaActualizacionExitosa(pvc_Mensaje) {
            deshabilitarControl('#<%=btnAceptar.ClientID%>');
            deshabilitarControl('#btnCancelar');
            $('.formulario').attr('disabled', 'disabled');

            mostrarAlerta(
                '#arAlertasDelFormulario',
                {
                    mensaje: pvc_Mensaje,
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

        function mostrarAlertaLlaveIncorrecta() {
            deshabilitarControl('#<%=btnAceptar.ClientID%>');
            deshabilitarControl('#btnLimpiarFormulario');
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
        });

    </script>

</asp:Content>

