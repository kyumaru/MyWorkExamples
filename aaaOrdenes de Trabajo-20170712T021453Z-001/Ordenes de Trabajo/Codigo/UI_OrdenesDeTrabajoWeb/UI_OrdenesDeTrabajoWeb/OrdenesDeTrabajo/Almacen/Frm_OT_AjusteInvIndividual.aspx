<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/Mp_Formulario.master" AutoEventWireup="false" CodeFile="Frm_OT_AjusteInvIndividual.aspx.vb" Inherits="OrdenesDeTrabajo_Almacen_Frm_OT_AjusteInvIndividual" %>

<%@ Register TagPrefix="wuc" Namespace="Utilerias.WebForms.Controles" Assembly="Utilerias.WebForms.Controles" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register TagPrefix="wuc" TagName="Materiales" Src="~/Controles/wuc_OT_Materiales.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphFormulario" runat="Server">
    <header>
        <h2>Ajustes de Inventario Individual</h2>
    </header>

    <article class="tituloSeccion">
        Detalle del material
    </article>

    <article class="formulario">
        <table>
            <tr>
                <th>Almacén Bodega</th>
                <td>
                    <asp:DropDownList runat="server" ID="ddlBodegaAlmacen" AppendDataBoundItems="true" AutoPostBack="true"></asp:DropDownList>
                    <asp:RequiredFieldValidator runat="server" ID="rfvDdlTipo" ControlToValidate="ddlBodegaAlmacen" Display="Dynamic" ErrorMessage="El almacén o bodega es requerido." ValidationGroup="Aceptar"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <th>Código</th>
                <td>
                    <asp:UpdatePanel runat="server" ID="upTxtCodigo" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:TextBox runat="server" ID="txtCodigo" data-tipocontrol="texto" AutoPostBack="true"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ID="rfvTxtCodigo" ControlToValidate="txtCodigo"
                                Display="Dynamic" ErrorMessage="El código del material es requerido." ValidationGroup="Aceptar">&nbsp;</asp:RequiredFieldValidator>
                            <ajax:FilteredTextBoxExtender ID="ftbTxtCodigo" runat="server" TargetControlID="txtCodigo" FilterType="Numbers"></ajax:FilteredTextBoxExtender>
                            <asp:LinkButton ID="lnkEjecutarBusquedaMaterial" runat="server">
                            <img id="imgBuscarMaterial" title="Buscar Material" alt="Buscar Material" src="" />
                            </asp:LinkButton>
                            <br />
                            <span id="spContadorTxtIdSolicitante" class="contadorCaracteresRestantes"></span>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="txtCodigo" EventName="TextChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
    </article>

    <asp:UpdatePanel runat="server" ID="upControlDatosMaterial" UpdateMode="Conditional">
        <ContentTemplate>
            <article class="formulario sinBorde" runat="server" id="arDatosMat">
                <table>
                    <tr>
                        <th>Descripción</th>
                        <td>
                            <asp:Label runat="server" ID="lblDescripcion" data-tipocontrol="etiqueta"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <th>Categoría</th>
                        <td>
                            <asp:Label runat="server" ID="lblCategoria"></asp:Label>
                        </td>
                        <th>Sub Categoría</th>
                        <td>
                            <asp:Label runat="server" ID="lblSubCategoria"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <th>Existencia</th>
                        <td>
                            <asp:Label runat="server" ID="lblExistencia"></asp:Label>
                        </td>
                        <th>Disponible</th>
                        <td>
                            <asp:Label runat="server" ID="lblDisponible"></asp:Label>
                        </td>
                        <th>Reservado</th>
                        <td>
                            <asp:Label runat="server" ID="lblReservado"></asp:Label>
                        </td>
                    </tr>
                </table>
            </article>

            <%--<article data-grupo="Listado" class="tituloSeccion" runat="server" id="arAjuste">
                Ajuste
            </article>--%>

            <article class="formulario sinBorde" runat="server" id="arDatosAjuste">
                <table>
                    <tr>
                        <th>Cantidad Ajuste</th>
                        <td>
                            <asp:TextBox runat="server" ID="txtCantidad" data-tipocontrol="texto" AutoPostBack="true"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="txtCantidad"
                                Display="Dynamic" ErrorMessage="La cantidad del ajuste es requerida." ValidationGroup="Aceptar">&nbsp;</asp:RequiredFieldValidator>
                            <asp:Label runat="server" ID="lblUnidadMedida"></asp:Label>
                            <ajax:FilteredTextBoxExtender ID="ftbTxtCantidad" runat="server" TargetControlID="txtCantidad" FilterType="Numbers, Custom" FilterMode="ValidChars" ValidChars="."></ajax:FilteredTextBoxExtender>

                        </td>
                    </tr>
                    <tr>
                        <th>Tipo</th>
                        <td>
                            <asp:RadioButtonList ID="rblTipo" runat="server" AutoPostBack="true" RepeatDirection="Vertical" RepeatLayout="Table">
                                <asp:ListItem Text="Incremento" Value="INC"></asp:ListItem>
                                <asp:ListItem Text="Decremento" Value="DEC"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <th>Observaciones</th>
                        <td>
                            <asp:TextBox runat="server" ID="txtObs" Width="100%" TextMode="MultiLine" Rows="9" data-tipocontrol="texto"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </article>

        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="txtCodigo" EventName="TextChanged" />
            <asp:AsyncPostBackTrigger ControlID="ctrl_Materiales" />
        </Triggers>
    </asp:UpdatePanel>


    <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Conditional">
        <ContentTemplate>
            <article data-grupo="Listado" class="tituloSeccion" runat="server" id="arTituloDetalle">
                Detalle del ajuste
            </article>

            <article class="formulario sinBorde" runat="server" id="arDetalle">
                <table>
                    <tr>
                        <th colspan="2" style="text-align:left">Datos posteriores al ajuste de material</th>
                    </tr>
                    <tr>
                        <th>Existencia</th>
                        <td>
                            <asp:Label runat="server" ID="lblExistenciaPost"></asp:Label>
                        </td>
                        <th>Disponible</th>
                        <td>
                            <asp:Label runat="server" ID="lblDisponiblePost"></asp:Label>
                        </td>
                        <th>Reservado</th>
                        <td>
                            <asp:Label runat="server" ID="lblReservadoPost"></asp:Label>
                        </td>
                    </tr>
                </table>
            </article>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="txtCantidad" EventName="TextChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlBodegaAlmacen" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="rblTipo" />
        </Triggers>
    </asp:UpdatePanel>

    <article class="areaBotones">
        <asp:Button runat="server" ID="btnCancelar" Text="Regresar" />
        <asp:Button runat="server" ID="btnGuardar" Text="Guardar" ValidationGroup="Aceptar" />
        <asp:Button runat="server" ID="btnFinalizar" Text="Finalizar" ValidationGroup="Aceptar" />
    </article>

    <%--Popup para búsqueda de materiales--%>
    <article id="PopUpBusquedaMateriales" class="ventanaEmergente">
        <article class="formulario" style="width: 850px!important;">
            <a href="#CerrarPopUpBusquedaMateriales" title="Cerrar Ventana" class="botonCerrarVentanaEmergente">X</a>
            <br />
            <asp:UpdatePanel ID="upContactoConv" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <wuc:Materiales ID="ctrl_Materiales" runat="server"></wuc:Materiales>
                    <div class="areaBotones">
                        <a href="#CerrarPopUpBusquedaMateriales" title="Regresar">Regresar</a>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </article>
    </article>
    <%--Fin Popup para búsqueda de materiales--%>

    <article id="arAlerta"></article>
    <article id="arpopupConfirmaDeseaBorrar"></article>
    <article id="arPopupGenerico"></article>
    <article id="arAlertasDelFormulario"></article>

    <script type="text/javascript">
        function mostrarPopUp(pvc_IdPopup) {
            window.location = pvc_IdPopup;
        };

        function cargarLupa() {
            permutarImagenes('#imgBuscarMaterial',
                '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Lupa.png")%>',
                '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Lupa.png")%>',
                '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Lupa.png")%>'
            );
        };

        function InhabilitarCodigo() {

            document.getElementById("imgBuscarMaterial").style.visibility = "hidden";

        };

        function HabilitarCodigo() {

            document.getElementById("imgBuscarMaterial").style.visibility = "visible";

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

        function mostrarAlertaNoHayDatos() {
            mostrarAlerta(
                '#arAlerta',
                {
                    mensaje: 'No se cuenta con información para mostrar.',
                    tipo: "advertencia",
                    transparencia: 0.9,
                    posicion: 'center'
                }
            );

            ocultarAreaDeListado();
        };

        $(document).ready(function () {
            inicializarFormulario();

        });

        function inicializarFormulario() {

            $('#btnRegresar').click(function () {
                regresarAlListado();
            });

            $(window).keydown(function a(e) {

                if (e.keyCode == 13) {
                    return false;
                }
            });

            cargarLupa();

            $('#<%=btnFinalizar.ClientID%>').click(function () { return mostrarPopupConfirmaDeseaEnviarRegistro(); });
        };

        function mostrarPopupConfirmaDeseaEnviarRegistro() {
            var vlo_ConfiguracionPopup = {
                titulo: "<em>Ajuste de inventario individual</em>",
                mensaje: "¿Confirma que desea enviar el registro?",
                botones:
                    [
                        {
                            idControl: "btnSi",
                            textoBoton: "Sí",
                            onClick:
                                function (e) {
                                    $(this).attr("disabled", "disabled");
                                    __doPostBack('<%=Me.btnFinalizar.UniqueID%>', '');
                                }
                        },
                    {
                        idControl: "btnNo",
                        textoBoton: "No",
                        onClick:
                            function (e) { cerrarPopup(); }
                    },
                    {
                        idControl: "btnCancelar",
                        textoBoton: "Cancelar",
                        onClick:
                            function (e) { cerrarPopup(); }
                    }

                    ]
            };

            $('#arPopupGenerico').popup(vlo_ConfiguracionPopup);
            window.location = "#arPopupGenerico";

            return false;
        };

        function regresarAlListado() {
            window.location = 'Lst_OT_AjusteInventario.aspx';
        };

        function ocultarFiltroMateriales() {
            window.location = '#CerrarPopUpBusquedaMateriales';
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

        function deshabilitarFormulario() {
            deshabilitarControl('#<%=btnCancelar.ClientID%>');
            deshabilitarControl('#<%=btnGuardar.ClientID%>');
            deshabilitarControl('#<%=btnFinalizar.ClientID%>');
            $('.formulario').attr('disabled', 'disabled');
        }

        function mostrarAlertaRegistroExitoso() {
            deshabilitarControl('#<%=btnCancelar.ClientID%>');
            deshabilitarControl('#<%=btnGuardar.ClientID%>');
            deshabilitarControl('#<%=btnFinalizar.ClientID%>');
            $('.formulario').attr('disabled', 'disabled');

            mostrarAlerta(
                '#arAlertasDelFormulario',
                {
                    mensaje: 'Se ha tramitado el ajuste del inventario.',
                    tipo: "exito",
                    transparencia: 0.9,
                    posicion: 'top',
                    onClosed: function () { regresarAlListado(); }
                });
        }

        function mostrarAlertaGuardarExitoso() {
            deshabilitarControl('#<%=btnCancelar.ClientID%>');
            deshabilitarControl('#<%=btnGuardar.ClientID%>');
            deshabilitarControl('#<%=btnFinalizar.ClientID%>');
            $('.formulario').attr('disabled', 'disabled');

            mostrarAlerta(
                '#arAlertasDelFormulario',
                {
                    mensaje: 'Se ha guardado el ajuste del inventario.',
                    tipo: "exito",
                    transparencia: 0.9,
                    posicion: 'top',
                    onClosed: function () { regresarAlListado(); }
                });
        }

    </script>
</asp:Content>

