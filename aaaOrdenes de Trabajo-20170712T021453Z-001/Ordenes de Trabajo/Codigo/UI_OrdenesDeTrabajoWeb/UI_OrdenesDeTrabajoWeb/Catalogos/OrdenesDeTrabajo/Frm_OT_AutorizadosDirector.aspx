<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/Mp_Formulario.master" AutoEventWireup="false" CodeFile="Frm_OT_AutorizadosDirector.aspx.vb" Inherits="Catalogos_Frm_OT_AutorizadosDirector" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphFormulario" runat="Server">

    <header>
        <h2>
            <asp:Label runat="server" ID="lblAccion"></asp:Label>
        </h2>
    </header>

    <article class="tituloSeccion">
        Mantenimiento Autorizados por Director
    </article>

    <article class="formulario" id="formularioTotal" runat="server">
        <table>
            <tr runat="server" id="trUnidad">
                <th>Unidad</th>
                <td>
                    <asp:DropDownList Width="40%" runat="server" ID="ddlUnidad" data-tipocontrol="combo" AppendDataBoundItems="true" AutoPostBack="true"></asp:DropDownList>
                    <asp:RequiredFieldValidator runat="server" ID="rfvDdlUnidad" ControlToValidate="ddlUnidad" Display="Dynamic" ErrorMessage="La unidad es obligatoria." ValidationGroup="Aceptar"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <th>Identificación</th>
                <td>
                    <asp:UpdatePanel runat="server" ID="upIdPersonal" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:TextBox  Width="40%" runat="server" ID="txtIdentificacion" data-tipocontrol="texto" ReadOnly="true"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ID="rfvTxtIdentificacion" ControlToValidate="txtIdentificacion" Display="Dynamic" ErrorMessage="El número de Identificación es requerido." ValidationGroup="Aceptar">&nbsp;</asp:RequiredFieldValidator>
                            <asp:LinkButton ID="lnkEjecutarBusqueda" runat="server">
                                <img id="imgBuscar" title="Buscar Registro" alt="Buscar Registro" src="" />
                            </asp:LinkButton>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <th>Nombre</th>
                <td>
                    <asp:UpdatePanel runat="server" ID="uplblNombre" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:Label runat="server" ID="lblNombre" data-tipocontrol="etiqueta"></asp:Label>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
    </article>

    <article class="areaBotones">
        <asp:Button runat="server" ID="btnAceptar" Text="Aceptar" ValidationGroup="Aceptar" />
        <input id="btnLimpiarFormulario" type="button" data-tipo="limpiarFormulario" value="Limpiar formulario" />
        <input id="btnCancelar" type="button" value="Cancelar" />
    </article>

    <article id="arPopupDelFormulario"></article>
    <article id="arAlertasDelFormulario"></article>
    <article id="arPopupGenerico"></article>

    <%--Popup para búsqueda de funcionario--%>
    <article id="PopUpBusquedaFuncionario" class="ventanaEmergente">
        <article class="formulario" style="width: 850px!important;">
            <a href="#CerrarPopUpBusquedaFuncionario" title="Cerrar Ventana" class="botonCerrarVentanaEmergente">X</a>
            <br />
            <asp:UpdatePanel ID="upContactoConv" runat="server" UpdateMode="Conditional">
                <ContentTemplate>

                    <article class="tituloSeccion">
                        Filtros de Búsqueda para Empleados
                    </article>

                    <article class="formulario">
                        <table>
                            <tr>
                                <td>
                                    <asp:Label ID="lblApellido1" runat="server" Text="Primer Apellido"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblApellido2" runat="server" Text="Segundo Apellido"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="Label1" runat="server" Text="Nombre"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblIdentificacion" runat="server" Text="Identificación"></asp:Label>
                                </td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtApellido1" runat="server" TabIndex="1"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtApellido2" runat="server" TabIndex="2"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtNombre" runat="server" TabIndex="3"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtIdPersonal" runat="server" TabIndex="4"></asp:TextBox>
                                </td>
                                <td style="width: 24px; text-align: center; vertical-align: middle">
                                    <asp:ImageButton ID="ibBuscar" runat="server" ToolTip="Realiza la búsqueda del empleado" TabIndex="5" />
                                </td>
                                <td style="width: 24px; text-align: center; vertical-align: middle">
                                    <asp:ImageButton ID="ibLimpiar" runat="server" TabIndex="6" ToolTip="Limpia los valores para una nueva búsqueda" />
                                </td>
                            </tr>
                        </table>
                    </article>

                    <article class="listado">
                        <asp:GridView ID="grdEmpleados" runat="server" AutoGenerateColumns="False"
                            EmptyDataText="No existen registros de Empleados con el criterio de búsqueda"
                            AllowPaging="True" Width="100%">
                            <RowStyle CssClass="lineaDelListado" />
                            <AlternatingRowStyle CssClass="lineaDelListado" />
                            <Columns>
                                <asp:TemplateField HeaderText="Funcionario">
                                    <ItemStyle HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkNumeroEmpleado" runat="server" Text='<%#Eval("NOMBRE")%>'
                                            CommandArgument='<%#String.Format("{0}", Eval("NUM_EMPLEADO"))%>' CommandName="Cargar" OnCommand="lnkGrid_Command">
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <PagerStyle HorizontalAlign="Left" />
                        </asp:GridView>
                    </article>

                    <div class="areaBotones">
                        <a href="#CerrarPopUpBusquedaFuncionario" title="Regresar">Regresar</a>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </article>
    </article>
    <%--Fin Popup para búsqueda de funcionario--%>

    <script type="text/javascript">

        function regresarAlListado() {
            window.location = 'Lst_OT_AutorizadosDirector.aspx';
        };

        function mostrarPopupRegistroExitoso() {
            var vlo_ConfiguracionPopup = {
                titulo: 'Catálogo de Autorizados Director',
                mensaje: 'Se ha registrado la información.. <br/><strong>¿Desea registrar otra identificación?</strong>',
                onClosed: function () { regresarAlListado(); },
                botones:
                        [
                            {
                                idControl: "btnSi",
                                textoBoton: "Sí",
                                onClick: function () { window.location = 'Frm_OT_AutorizadosDirector.aspx?pvn_Operacion=<%=Utilerias.OrdenesDeTrabajo.eOperacion.Agregar%>'; }
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
            deshabilitarControl('#btnLimpiarFormulario');
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

            //$('#imgBuscar').click(function () {
            //    mostrarPopUp('#PopUpBusquedaFuncionario')
            //});

            $(window).keydown(function a(e) {

                if (e.keyCode == 13) {
                    return false;
                }
            });

            permutarImagenes('#imgBuscar',
            '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Lupa.png")%>',
            '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Lupa.png")%>',
            '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Lupa.png")%>'
            );

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
        }

        function MostrarMensajeInformacion(pvc_Mensaje) {
            alert(pvc_Mensaje);
        }

        function MostrarMensajeError(pvc_Mensaje) {
            alert(pvc_Mensaje);
        }

    </script>

</asp:Content>

