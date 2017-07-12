<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/MasterPage/Mp_Formulario.master" CodeFile="Frm_OT_Materiales.aspx.vb" Inherits="Catalogos_Frm_OT_Materiales" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<%@ Register TagPrefix="wuc" Namespace="Utilerias.WebForms.Controles" Assembly="Utilerias.WebForms.Controles" %>


<asp:Content runat="server" ID="Content1" ContentPlaceHolderID="cphFormulario">

    <header>
        <h2>
            <asp:Label runat="server" ID="lblAccion"></asp:Label>
        </h2>
    </header>

    <article class="tituloSeccion">
        Catálogo de materiales
    </article>

    <article class="formulario">
        <table>
            <tr>
                <th>Código</th>
                <td>
                    <asp:Label runat="server" ID="lblCodigo"></asp:Label></td>
            </tr>
            <tr>
                <th>Descripción</th>
                <td>
                    <asp:TextBox TextMode="MultiLine" Rows="2" Width="71%" ID="txtDescripcion" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ValidationGroup="Aceptar" ID="rfvTxtDescripcion" ControlToValidate="txtDescripcion"
                        Display="Dynamic" ErrorMessage="La descripción es obligatoria"></asp:RequiredFieldValidator>
                    <span id="spContadortxtDescripcion" class="contadorCaracteresRestantes"></span>
                </td>
            </tr>
            <tr>
                <th>Categoría</th>
                <td>
                    <asp:UpdatePanel runat="server" ID="upCategoria" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:DropDownList runat="server" ID="ddlCategoria" AppendDataBoundItems="true" AutoPostBack="true"></asp:DropDownList>
                            <asp:RequiredFieldValidator runat="server" ID="rfvDdlCategoria" ValidationGroup="Aceptar" ControlToValidate="ddlCategoria"
                                Display="Dynamic" ErrorMessage="Seleccione una categoría"></asp:RequiredFieldValidator>
                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="ddlCategoria" />
                        </Triggers>
                    </asp:UpdatePanel>

                </td>
            </tr>
            <tr>
                <th>Sub Categoría</th>
                <td>
                    <asp:UpdatePanel runat="server" ID="upSubcategoria" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:DropDownList runat="server" ID="ddlSubCategoria" AppendDataBoundItems="true" AutoPostBack="true"></asp:DropDownList>
                            <asp:RequiredFieldValidator runat="server" ID="rfvDdlSubCategoria" ValidationGroup="Aceptar" ControlToValidate="ddlSubCategoria"
                                Display="Dynamic" ErrorMessage="Seleccione una Sub Categoría"></asp:RequiredFieldValidator>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                </td>
            </tr>
            <tr>
                <th>Unidad de Medida</th>
                <td>
                    <asp:DropDownList runat="server" ID="ddlUnidadMedida" AppendDataBoundItems="true" AutoPostBack="true"></asp:DropDownList>
                    <asp:RequiredFieldValidator runat="server" ID="rfvDdlUnidadMedida" ValidationGroup="Aceptar" ControlToValidate="ddlUnidadMedida"
                        Display="Dynamic" ErrorMessage="Seleccione una medida"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <th>Partida Presupuestaria</th>
                <td>

                    <asp:UpdatePanel runat="server" ID="uptxtPartida" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:TextBox runat="server" ID="txtPartida" data-tipocontrol="texto" AutoPostBack="true"></asp:TextBox>
                            <asp:LinkButton ID="lnkEjecutarBusquedaPartida" runat="server">
                                <img id="imgBuscarPartida" title="Buscar Registro" alt="Buscar Registro" src="" />
                            </asp:LinkButton>
                            <br />
                            <br />
                            <asp:Label runat="server" ID="lblNombrePartida"></asp:Label><br />
                            <asp:RequiredFieldValidator runat="server" ID="rfvtxtPartida" ControlToValidate="txtPartida" Display="Dynamic" ErrorMessage="La partida presupuestaria es requerida." ValidationGroup="Aceptar">&nbsp;</asp:RequiredFieldValidator>
                            <ajax:FilteredTextBoxExtender ID="ftbtxtPartida" runat="server" TargetControlID="txtPartida" FilterMode="ValidChars" FilterType="Numbers"></ajax:FilteredTextBoxExtender>
                            <br />
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="txtPartida" EventName="TextChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <th>Clasificación</th>
                <td>
                    <asp:DropDownList runat="server" ID="ddlClasificacion" AppendDataBoundItems="true" AutoPostBack="true"></asp:DropDownList>
                    <asp:RequiredFieldValidator runat="server" ID="rfvDdlClasificacion" ValidationGroup="Aceptar" ControlToValidate="ddlClasificacion"
                        Display="Dynamic" ErrorMessage="Seleccione una clasificación"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr runat="server" id="trCantidadExistencia">
                <th>Cantidad en Existencia</th>
                <td>
                    <asp:Label runat="server" ID="lblCantidadExistencia"></asp:Label></td>
            </tr>
            <tr>
                <th>Punto de Reorden</th>
                <td>
                    <asp:TextBox Width="10%" ID="txtPuntoReorden" runat="server"></asp:TextBox>
                    <ajax:FilteredTextBoxExtender ID="ftbTxtPuntoReorden" runat="server" TargetControlID="txtPuntoReorden" FilterMode="ValidChars" ValidChars="" FilterType="Numbers"></ajax:FilteredTextBoxExtender>
                    <asp:RequiredFieldValidator runat="server" ID="rfvTxtPuntoReorden" ValidationGroup="Aceptar" ControlToValidate="txtPuntoReorden"
                        Display="Dynamic" ErrorMessage="Favor indique el punto de reorden"></asp:RequiredFieldValidator>

                </td>
            </tr>
            <tr>
                <th>Máximo Almacén</th>
                <td>
                    <asp:TextBox Width="10%" ID="txtMaximoAlmacen" runat="server"></asp:TextBox>
                    <ajax:FilteredTextBoxExtender ID="ftbxTxtMaximoAlmacen" runat="server" TargetControlID="txtMaximoAlmacen" FilterMode="ValidChars" ValidChars="" FilterType="Numbers"></ajax:FilteredTextBoxExtender>
                    <asp:RequiredFieldValidator runat="server" ID="rfvTxtMaximoAlmacen" ValidationGroup="Aceptar" ControlToValidate="txtMaximoAlmacen"
                        Display="Dynamic" ErrorMessage="Favor indique el máximo a almacenarse en el almacen."></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <th>Máximo Permitido en Bodegas</th>
                <td>
                    <asp:TextBox Width="10%" ID="txtMaximoBodegas" runat="server"></asp:TextBox>
                    <ajax:FilteredTextBoxExtender ID="ftbxTxtMaximoBodegas" runat="server" TargetControlID="txtMaximoBodegas" FilterMode="ValidChars" ValidChars="" FilterType="Numbers"></ajax:FilteredTextBoxExtender>
                    <asp:RequiredFieldValidator runat="server" ID="rfvTxtMaximoBodegas" ValidationGroup="Aceptar" ControlToValidate="txtMaximoBodegas"
                        Display="Dynamic" ErrorMessage="Favor ingresar el máximo permitido en bodegas."></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr runat="server" id="trCostoPromedio">
                <th>Costo Promedio</th>
                <td>
                    <asp:Label runat="server" ID="lblCostoPromedio"></asp:Label></td>
            </tr>
            <tr>
                <th>Ubicación</th>
                <td>Mueble:
                    <asp:TextBox Width="8%" ID="txtMueble" runat="server"></asp:TextBox>
                    <ajax:FilteredTextBoxExtender ID="ftbtxtMueble" runat="server" TargetControlID="txtMueble" FilterType="Numbers, UppercaseLetters, LowercaseLetters"></ajax:FilteredTextBoxExtender>
                    <asp:RequiredFieldValidator runat="server" ID="rfvTxtMueble" ControlToValidate="txtMueble"
                        Display="Dynamic" ErrorMessage="Favor ingresar La ubicación" ValidationGroup="Aceptar"></asp:RequiredFieldValidator>
                    Columna:
                    <asp:TextBox Width="8%" ID="txtColumna" runat="server"></asp:TextBox>
                    <ajax:FilteredTextBoxExtender ID="ftbtxtColumna" runat="server" TargetControlID="txtColumna" FilterType="Numbers, UppercaseLetters, LowercaseLetters"></ajax:FilteredTextBoxExtender>
                    <asp:RequiredFieldValidator runat="server" ID="rfvTxtColumna" ControlToValidate="txtColumna"
                        Display="Dynamic" ErrorMessage="Favor ingresar La ubicación" ValidationGroup="Aceptar"></asp:RequiredFieldValidator>
                    Estante:
                    <asp:TextBox Width="8%" ID="txtEstante" runat="server"></asp:TextBox>
                    <ajax:FilteredTextBoxExtender ID="ftbtxtEstante" runat="server" TargetControlID="txtEstante" FilterType="Numbers, UppercaseLetters, LowercaseLetters"></ajax:FilteredTextBoxExtender>
                    <asp:RequiredFieldValidator runat="server" ID="rfvTxtEstante" ControlToValidate="txtEstante"
                        Display="Dynamic" ErrorMessage="Favor ingresar La ubicación" ValidationGroup="Aceptar"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr runat="server" id="trEstado">
                <th>Estado</th>
                <td>
                    <asp:DropDownList Width="33%" runat="server" ID="ddlEstado" AppendDataBoundItems="true"></asp:DropDownList>
                    <asp:RequiredFieldValidator runat="server" ID="rfvDdlEstado" ValidationGroup="Aceptar" ControlToValidate="ddlEstado"
                        Display="Dynamic" ErrorMessage="Debe incluir un estado" Enabled="false"></asp:RequiredFieldValidator>
                </td>
            </tr>
        </table>
    </article>

    <article class="areaBotones">
        <asp:Button runat="server" ValidationGroup="Aceptar" ID="btnAceptar" Text="Aceptar" />
        <input type="button" data-tipo="limpiarFormulario" value="Limpiar Formulario" id="btnLimpiarFormulario" />
        <input type="button" value="Cancelar" id="btnCancelar" />
    </article>

    <article id="arPopupDelFormulario"></article>
    <article id="arAlertasDelFormulario"></article>

    <%--Popup para búsqueda de la partida presupuestaria--%>
    <article id="PopUpBusquedaPartida" class="ventanaEmergente">
        <article class="formulario" style="width: 850px!important;">
            <a href="#CerrarPopUpBusquedaPartida" title="Cerrar Ventana" class="botonCerrarVentanaEmergente">X</a>
            <br />
            <asp:UpdatePanel ID="upPartidaPresup" runat="server" UpdateMode="Conditional">
                <ContentTemplate>

                    <article class="tituloSeccion">
                        Filtros de Búsqueda de Partidas Presupuestarias
                    </article>

                    <article class="formulario">
                        <table>
                            <tr>
                                <td>
                                    <asp:Label ID="lblCodigoPopup" runat="server" Text="Código"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblDescripcionPopup" runat="server" Text="Descripción"></asp:Label>
                                </td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtCodigoPopup" runat="server" TabIndex="1"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDescripcionPopup" runat="server" TabIndex="2" TextMode="MultiLine" Columns="40" Rows="4"></asp:TextBox>
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
                        <asp:GridView ID="grdPartidas" runat="server" AutoGenerateColumns="False"
                            EmptyDataText="No existen registro(s) de Partida(s) Presupuestaria(s) con el/los criterio(s) de búsqueda"
                            AllowPaging="True" Width="100%">
                            <RowStyle CssClass="lineaDelListado" />
                            <AlternatingRowStyle CssClass="lineaDelListado" />
                            <Columns>
                                <asp:TemplateField HeaderText="Código">
                                    <ItemStyle HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkIdEgreso" runat="server" Text='<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_PARTIDAS_PRESUP.ID_EGRESO)%>'
                                            CommandArgument='<%#String.Format("{0}_{1}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_PARTIDAS_PRESUP.ID_EGRESO), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_PARTIDAS_PRESUP.NOM_EGRESO))%>' CommandName="Cargar" OnCommand="lnkGrid_Command">
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Descripción">
                                    <ItemStyle HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblNomEgreso" runat="server" Text='<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_PARTIDAS_PRESUP.NOM_EGRESO)%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <PagerStyle HorizontalAlign="Left" />
                        </asp:GridView>
                    </article>

                    <div class="areaBotones">
                        <a href="#CerrarPopUpBusquedaPartida" title="Regresar">Regresar</a>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </article>
    </article>
    <%--Fin Popup para búsqueda de la partida presupuestaria--%>

    <script type="text/javascript">

        function mostrarPopUp(pvc_IdPopup) {
            window.location = pvc_IdPopup;
        };

        function regresarAlListado() {
            window.location = 'Lst_OT_Materiales.aspx';
        }
        function deshabilitarFormulario() {
            deshabilitarControl('#<%=Me.btnAceptar.ClientID%>');
            deshabilitarControl('#btnLimpiarFormulario');
            deshabilitarControl('#btnCancelar');
            $('.formulario').attr('disabled', 'disabled');
        }

        function mostrarPopupRegistroExitoso() {
            var vlo_ConfiguracionPopup = {
                titulo: 'Catálogo de Materiales',
                mensaje: 'Se ha registrado la información del material.<br /><strong>¿Desea registrar otro material?</strong>',
                onClosed: function () { regresarAlListado(); },
                botones:
                    [
                        {
                            idControl: "btnSi",
                            textoBoton: "Sí",
                            onClick: function () { window.location = 'Frm_OT_Materiales.aspx?pvn_Operacion=<%=Utilerias.OrdenesDeTrabajo.eOperacion.Agregar%>'; }
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

                }

                function mostrarAlertaError(pvc_Msj) {
                    mostrarAlerta(
                        '#arAlertasDelFormulario',
                        {
                            mensaje: pvc_Msj,
                            tipo: "peligro",
                            transparencia: 0.9,
                            posicion: 'center',
                            permiteCerrar: true
                        }
                    );
                };

                function mostrarAlertaActualizacionExitosa() {
                    deshabilitarFormulario();

                    mostrarAlerta(
                        '#arAlertasDelFormulario',
                        {
                            mensaje: 'Se ha actualizado la información del material',
                            tipo: "exito",
                            transparencia: 0.9,
                            posicion: 'center',
                            permiteCerrar: true,
                            onClosed: function () { regresarAlListado(); }
                        }
                    );
                };

                function mostrarAlertaLlaveIncorrecta() {
                    deshabilitarFormulario();

                    mostrarAlerta(
                        '#arAlertasDelFormulario',
                        {
                            mensaje: 'El número de identificación provisto no pertenece a ningun material registrado en el sistema',
                            tipo: "peligro",
                            transparencia: 0.9,
                            posicion: 'center',
                            permiteCerrar: true,
                            onClosed: function () { regresarAlListado(); }
                        }
                    );
                };

                function cargarLupa() {
                    permutarImagenes('#imgBuscarPartida',
                        '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Lupa.png")%>',
                        '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Lupa.png")%>',
                        '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Lupa.png")%>'
                    );
                }

                function inicializarScript() {

                    $('#btnCancelar').click(function () {
                        regresarAlListado();
                    });

                    $(window).keydown(function a(e) {
                        if (e.keyCode == 13) {
                            return false;
                        }
                    });

                    configurarLongitudMaximaTexto('#<%=Me.txtDescripcion.ClientID%>', '<%=Utilerias.OrdenesDeTrabajo.Modelo.OTM_MATERIAL.DESCRIPCION_BD_TAMANO%>');
                    configurarContadorCaracteresRestantes('#<%=Me.txtDescripcion.ClientID%>', '#spContadortxtDescripcion');

                    configurarLongitudMaximaTexto('#<%=Me.txtPuntoReorden.ClientID%>', '<%=Utilerias.OrdenesDeTrabajo.Modelo.OTM_MATERIAL.PUNTO_REORDEN_BD_TAMANO%>');

                    configurarLongitudMaximaTexto('#<%=Me.txtMaximoAlmacen.ClientID%>', '<%=Utilerias.OrdenesDeTrabajo.Modelo.OTM_MATERIAL.MAXIMO_ALMACEN_BD_TAMANO%>');

                    configurarLongitudMaximaTexto('#<%=Me.txtMaximoBodegas.ClientID%>', '<%=Utilerias.OrdenesDeTrabajo.Modelo.OTM_MATERIAL.MAXIMO_BODEGA_BD_TAMANO%>');

                    configurarLongitudMaximaTexto('#<%=Me.txtMueble.ClientID%>', '2');

                    configurarLongitudMaximaTexto('#<%=Me.txtEstante.ClientID%>', '2');

                    configurarLongitudMaximaTexto('#<%=Me.txtColumna.ClientID%>', '2');

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


        $(document).ready(function () {
            inicializarScript();
        });
    </script>

</asp:Content>
