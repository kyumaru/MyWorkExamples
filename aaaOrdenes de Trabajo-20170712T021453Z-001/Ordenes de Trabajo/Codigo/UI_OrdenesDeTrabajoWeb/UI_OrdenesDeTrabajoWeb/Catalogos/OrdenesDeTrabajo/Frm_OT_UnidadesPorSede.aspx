<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/Mp_Formulario.master" AutoEventWireup="false" CodeFile="Frm_OT_UnidadesPorSede.aspx.vb" Inherits="Catalogos_Frm_OT_UnidadesPorSede" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphFormulario" runat="Server">

    <header>
        <h2>
            <asp:Label runat="server" ID="lblAccion"></asp:Label>
        </h2>
    </header>

    <article class="tituloSeccion">
        Mantenimiento de Unidades por Sede
    </article>

    <article class="formulario" id="formularioTotal" runat="server">
        <table>
            <tr>
                <th>Sede</th>
                <td>
                    <asp:DropDownList Width="70%" runat="server" ID="ddlSede" data-tipocontrol="combo" AppendDataBoundItems="true"></asp:DropDownList>
                    <asp:RequiredFieldValidator runat="server" ID="rfvDdlSede" ControlToValidate="ddlSede" Display="Dynamic" ErrorMessage="La Sede es obligatoria." ValidationGroup="Aceptar"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <th>Unidad</th>
                <td>
                    <asp:UpdatePanel runat="server" ID="upTxtUnidad" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:TextBox Width="70%" runat="server" ID="txtUnidad" data-tipocontrol="texto" AutoPostBack="true"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ID="rfvTxtUnidad" ControlToValidate="txtUnidad" Display="Dynamic" ErrorMessage="La unidad es requerida." ValidationGroup="Aceptar">&nbsp;</asp:RequiredFieldValidator>
                             <ajax:FilteredTextBoxExtender ID="ftbTxtUnidad" runat="server" TargetControlID="txtUnidad" FilterMode="ValidChars" FilterType="Numbers"></ajax:FilteredTextBoxExtender>
                            <asp:LinkButton ID="lnkEjecutarBusquedaUnidad" runat="server">
                                <img id="imgBuscarUnidad" title="Buscar Registro" alt="Buscar Registro" src="" />
                            </asp:LinkButton>
                            <br />
                            <span id="spContadorTxtUnidad" class="contadorCaracteresRestantes"></span>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="txtUnidad" EventName="TextChanged" />
                            <asp:AsyncPostBackTrigger ControlID="btnLimpiarFormulario" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <th>Nombre Unidad</th>
                <td>
                    <asp:UpdatePanel runat="server" ID="upLblNombreUnidad" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:Label runat="server" ID="lblNombreUnidad" data-tipocontrol="etiqueta"></asp:Label>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="txtUnidad" EventName="TextChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
    </article>

    <article class="areaBotones">
        <asp:Button runat="server" ID="btnAceptar" Text="Aceptar" ValidationGroup="Aceptar" />
        <asp:Button runat="server" ID="btnLimpiarFormulario" Text="Limpiar Formulario" data-tipo="limpiarFormulario" />
        <input id="btnCancelar" type="button" value="Cancelar" />
    </article>

    <article id="arPopupDelFormulario"></article>
    <article id="arAlertasDelFormulario"></article>
    <article id="arPopupGenerico"></article>

    <%--Popup para búsqueda de la unidad--%>
    <article id="PopUpBusquedaUnidad" class="ventanaEmergente">
        <article class="formulario" style="width: 850px!important;">
            <a href="#CerrarPopUpBusquedaUnidad" title="Cerrar Ventana" class="botonCerrarVentanaEmergente">X</a>
            <br />
            <asp:UpdatePanel ID="upContactoConv" runat="server" UpdateMode="Conditional">
                <ContentTemplate>

                    <article class="tituloSeccion">
                        Filtros de Búsqueda de Unidades
                    </article>

                    <article class="formulario">
                        <table>
                            <tr>
                                <td>
                                    <asp:Label ID="lblCodigo" runat="server" Text="Código"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblDescripcion" runat="server" Text="Descripción"></asp:Label>
                                </td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtCodigo" runat="server" TabIndex="1"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDescripcion" runat="server" TabIndex="2" TextMode="MultiLine" Columns="40" Rows="4"></asp:TextBox>
                                </td>
                                <td style="width: 24px; text-align: center; vertical-align: middle">
                                    <asp:ImageButton ID="ibBuscar" runat="server" ToolTip="Realiza la búsqueda de la unidad" TabIndex="5" />
                                </td>
                                <td style="width: 24px; text-align: center; vertical-align: middle">
                                    <asp:ImageButton ID="ibLimpiar" runat="server" TabIndex="6" ToolTip="Limpia los valores para una nueva búsqueda" />
                                </td>
                            </tr>
                        </table>
                    </article>

                    <article class="listado">
                        <asp:GridView ID="grdUnidades" runat="server" AutoGenerateColumns="False"
                            EmptyDataText="No existen registros de Unidades con el criterio de búsqueda"
                            AllowPaging="True" Width="100%">
                            <RowStyle CssClass="lineaDelListado" />
                            <AlternatingRowStyle CssClass="lineaDelListado" />
                            <Columns>
                                <asp:TemplateField HeaderText="Código">
                                    <ItemStyle HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkCodigo" runat="server" Text='<%#Eval("COD_UNIDAD_SIRH")%>'
                                            CommandArgument='<%#String.Format("{0}",  Eval("COD_UNIDAD_SIRH"))%>' CommandName="Cargar" OnCommand="lnkGrid_Command">
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Descripción">
                                    <ItemStyle HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%#Eval("DESCRIPCION")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <PagerStyle HorizontalAlign="Left" />
                        </asp:GridView>
                    </article>

                    <div class="areaBotones">
                        <a href="#CerrarPopUpBusquedaUnidad" title="Regresar">Regresar</a>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </article>
    </article>
    <%--Fin Popup para búsqueda de la unidad--%>

    <script type="text/javascript">

        function mostrarAlertaActualizacionExitosa() {
            deshabilitarControl('#<%=btnAceptar.ClientID%>');
            deshabilitarControl('#<%=btnLimpiarFormulario.ClientID%>');
            deshabilitarControl('#btnCancelar');
            $('.formulario').attr('disabled', 'disabled');

            mostrarAlerta(
                '#arAlertasDelFormulario',
                {
                    mensaje: 'Se ha actualizado la información de la unidad por sede.',
                    tipo: "exito",
                    transparencia: 0.9,
                    posicion: 'center',
                    onClosed: function () { regresarAlListado(); }
                });
        };

        function mostrarAlertaNoEncontrado() {
            mostrarAlerta(
                '#arAlertasDelFormulario',
                {
                    mensaje: "No se han encontrado unidades con el código indicado.",
                    tipo: "advertencia",
                    transparencia: 0.9,
                    posicion: 'center',
                    permiteCerrar: true
                }
                );

        };

        function regresarAlListado() {
            window.location = 'Lst_OT_UnidadesPorSede.aspx';
        };

        function mostrarPopupRegistroExitoso() {
            var vlo_ConfiguracionPopup = {
                titulo: 'Catálogo de unidades por sede',
                mensaje: 'Se ha registrado la información.. <br/><strong>¿Desea registrar otra unidad por sede?</strong>',
                onClosed: function () { regresarAlListado(); },
                botones:
                        [
                            {
                                idControl: "btnSi",
                                textoBoton: "Sí",
                                onClick: function () { window.location = 'Frm_OT_UnidadesPorSede.aspx?pvn_Operacion=<%=Utilerias.OrdenesDeTrabajo.eOperacion.Agregar%>'; }
                            },
                        {
                            idControl: "btnNo",
                            textoBoton: "No",
                            onClick: function () { regresarAlListado(); }
                        }
                        ]
            };

            $('#arPopupDelFormulario').popup(vlo_ConfiguracionPopup);

            window.location = '#arPopupDelFormulario';
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
            deshabilitarControl('#<%=btnLimpiarFormulario.ClientID%>');
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

            $(window).keydown(function a(e) {
                if (e.keyCode == 13) {
                    return false;
                }
            });

            habilitarTooltipGenerico();

            permutarImagenes('#<%= ibBuscar.ClientID%>',
                '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Lupa.png")%>',
                '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Lupa.png")%>',
                '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Lupa.png")%>'
            );

            permutarImagenes('#<%= ibLimpiar.ClientID%>',
                '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Equis.png")%>',
                '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Icono_Equis.png")%>',
                '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Equis.png")%>'
            );

            cargarLupa();

        };

        function cargarLupa() {
            permutarImagenes('#imgBuscarUnidad',
                '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Lupa.png")%>',
                '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Lupa.png")%>',
                '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Lupa.png")%>'
            );
        }

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
        function MostrarMensajeAviso(pvc_Mensaje) {
            alert(pvc_Mensaje);
        };

        function MostrarMensajeInformacion(pvc_Mensaje) {
            alert(pvc_Mensaje);
        };

        function MostrarMensajeError(pvc_Mensaje) {
            alert(pvc_Mensaje);
        };

    </script>

</asp:Content>

