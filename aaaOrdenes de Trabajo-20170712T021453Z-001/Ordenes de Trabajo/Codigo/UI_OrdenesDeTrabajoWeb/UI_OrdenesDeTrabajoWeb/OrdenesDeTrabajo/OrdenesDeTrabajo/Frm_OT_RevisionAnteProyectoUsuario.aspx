<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/Mp_Formulario.master" EnableEventValidation="false"  AutoEventWireup="false" CodeFile="Frm_OT_RevisionAnteProyectoUsuario.aspx.vb" Inherits="OrdenesDeTrabajo_Frm_OT_RevisionAnteProyectoUsuario" %>

<%@ Register TagPrefix="wuc" TagName="InformacionGeneral" Src="~/ControlesDeUsuario/Wuc_OT_InformacionGeneral.ascx" %>
<%@ Register TagPrefix="wuc" Namespace="Utilerias.WebForms.Controles" Assembly="Utilerias.WebForms.Controles" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphFormulario" runat="Server">

    <header>
        <h2>Revisión del Anteproyecto
        </h2>
    </header>

    <article class="formulario">
        <table>
            <tr>
                <th>Versión</th>
                <td>
                    <asp:Label runat="server" ID="lblVersion" data-tipocontrol="etiqueta"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>Encargado de Proyecto</th>
                <td>
                    <asp:Label runat="server" ID="lblEncargadoProyecto" data-tipocontrol="etiqueta"></asp:Label>
                </td>
            </tr>
        </table>
    </article>

    <asp:UpdatePanel runat="server" ID="upControlOrdenTrabajo" UpdateMode="Conditional">
        <ContentTemplate>
            <wuc:InformacionGeneral ID="ctrl_InfoGeneral" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>

    <article class="formulario">
        <table>
            <tr>
                <th>Descripción</th>
                <td>
                    <asp:TextBox runat="server" ID="txtDescripción" data-tipocontrol="texto" TextMode="MultiLine" Rows="9" Width="59%" Enabled="false"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>Cantidad</th>
                <td>
                    <asp:Label runat="server" ID="lblCantidad" data-tipocontrol="etiqueta"></asp:Label>
                </td>
            </tr>
            <tr runat="server" id="trPlantaFisica" visible="false">
                <th>Aval Planta Física</th>
                <td>
                    <asp:LinkButton runat="server" ID="lnkArchivoPlantaFisica"></asp:LinkButton>
                </td>
            </tr>
            <tr runat="server" id="trForesta" visible="false">
                <th>Aval Foresta</th>
                <td>
                    <asp:LinkButton runat="server" ID="lnkArchivoForesta"></asp:LinkButton>
                </td>
            </tr>
        </table>
    </article>
    <br />

    <article class="formulario">
        <ul class="encabezadoTabPanel">
            <li class="activo"><a class="tituloTabPanel" href="#cuerpoTabPanel1">Actividades Contempladas</a></li>
            <li><a class="tituloTabPanel" href="#cuerpoTabPanel2">Documentos</a></li>
        </ul>

        <article class="cuerpoTabPanel">
            <article id="cuerpoTabPanel1" class="tabPanel activo">
                <h3>Actividades Contempladas</h3>
                <br />
                <article class="listado sinBorde">
                    <asp:Repeater runat="server" ID="rpActividadesContempladas">
                        <HeaderTemplate>
                            <table>
                                <tr>
                                    <th>Actividad</th>
                                </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr class="lineaDelListado">
                                <td><%#Eval("ACTIVIDAD")%></td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                </article>
            </article>
            <article id="cuerpoTabPanel2" class="tabPanel">
                <h3>Documentos</h3>
                <br />
                <article class="areaBotones">
                    <asp:Button runat="server" ID="btnDescargarTodos" Text="Descargar Todos" />
                </article>                
                <br />
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
                                        CommandName='<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ADJUNTO_ORDEN_TRABAJOLST.ID_ADJUNTO_ORDEN_TRABAJO)%>'
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

            </article>
        </article>
    </article>
    <br />

    <article class="formulario">
        <table>
            <tr>
                <th>Condición</th>
                <td>
                    <asp:RadioButton ID="rbtVistoBueno" runat="server" Text="Visto Bueno" GroupName="Condicion" AutoPostBack="true" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:RadioButton ID="rbtIndicarObservaciones" runat="server" Text="Indicar Observaciones" GroupName="Condicion" AutoPostBack="true" />
                </td>
            </tr>
              <tr runat="server" id="trJustificacion">
                <th>Observaciones</th>
                <td>
                    <asp:TextBox Width="59%" runat="server" ID="txtJustificacion" data-tipocontrol="texto" TextMode="MultiLine" Rows="5"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ID="rfvTxtJustificacion" Enabled="false" ControlToValidate="txtJustificacion" Display="Dynamic" ErrorMessage="Las observaciones son obligatorias." ValidationGroup="Aceptar"></asp:RequiredFieldValidator>
                </td>
            </tr>
        </table>
    </article>

    <article class="areaBotones">
        <asp:Button runat="server" ID="btnGenerarArchivoPDF" Text="Generar Archivo PDF" />
        <asp:Button runat="server" ID="btnAceptar" Text="Aceptar" ValidationGroup="Aceptar" />
        <input id="btnCancelar" type="button" value="Regresar" />
    </article>

    <article id="arAlertasDelFormulario"></article>
    <article id="arpopupConfirmaDeseaProcesar"></article>

    <script type="text/javascript">

        function GoDown() {
            window.scrollTo(0, document.body.scrollHeight);
        };

        function regresarAlListado() {
            window.location = 'Lst_OT_OrdenTrabajo.aspx';
        };

        function mostrarAlertaActualizacionExitosa(pvc_Mensaje) {
            deshabilitarControl('#<%=btnAceptar.ClientID%>');
            deshabilitarControl('#<%=btnGenerarArchivoPDF.ClientID%>');
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
            deshabilitarControl('#<%=btnGenerarArchivoPDF.ClientID%>');
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

        function mostrarAlertaLlaveIncorrectaAnteProyecto() {
            deshabilitarControl('#<%=btnAceptar.ClientID%>');
            deshabilitarControl('#<%=btnGenerarArchivoPDF.ClientID%>');
            deshabilitarControl('#btnCancelar');
            $('.formulario').attr('disabled', 'disabled');

            mostrarAlerta(
                '#arAlertasDelFormulario',
                {
                    mensaje: 'El identificador provisto no pertenece a ninguna anteproyecto del sistema.',
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

            /*Control TabPanel*/
            configurarTabPanel();
        });

    </script>

</asp:Content>

