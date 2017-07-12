<%@ Page Title="Catálogo de Edificios y Sitios Universitarios" Language="VB" MasterPageFile="~/MasterPage/Mp_Formulario.master" AutoEventWireup="false" CodeFile="Frm_OT_LugarTrabajo.aspx.vb" Inherits="Catalogos_Frm_OT_LugarTrabajo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphFormulario" Runat="Server">
    <header>
        <h2>
            <asp:Label runat="server" ID="lblAccion"></asp:Label></h2>
    </header>
    <article class="tituloSeccion">
        Datos de del Edificio o Sitio Universitario
    </article>
    <article class="formulario">
        <table>
            <tr>
                <th>Nombre</th>
                <td>
                    <asp:TextBox Width="70%" runat="server" ID="txtNombre" data-tipoControl="texto"></asp:TextBox>
                    <asp:RequiredFieldValidator  ValidationGroup="Aceptar" runat="server" ID="rfvTxtNombre" ControlToValidate="txtNombre" Display="Dynamic" ErrorMessage="El nombre del lugar de trabajo es requerido.">&nbsp;</asp:RequiredFieldValidator>
                    <br />
                    <span id="spContadorTxtNombre" class="contadorCaracteresRestantes"></span>
                </td>
            </tr>
            <tr>
                <th>Clasificación</th>
                <td>
                    <asp:RadioButton runat="server" ID="rdbEdificio" Text="Edificio" GroupName="Clasificacion" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:RadioButton runat="server" ID="rdbSitio" Text="Sitio" GroupName="Clasificacion" />
                </td>
            </tr>
            <tr>
                <th>Ubicación (Sede / Otro)</th>
                <td>
                    <asp:DropDownList Width="70%" runat="server" ID="ddlUbicacion" AppendDataBoundItems="true" data-tipoControl="combo"></asp:DropDownList>
                    <img id="imgTooltipDdlUbicacion" src="<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Icono_Ayuda.png")%>" title="Ubicación del lugar de realización de un trabajo, en términos de áreas que conforman la universidad: sedes, recintos, estaciones experimentales, otros." />
                    <asp:RequiredFieldValidator runat="server" ValidationGroup="Aceptar"  ID="rfvDdlUbicacion" ControlToValidate="ddlUbicacion" Display="Dynamic" ErrorMessage="Debe seleccionar una ubicación para el lugar de trabajo.">&nbsp;</asp:RequiredFieldValidator>

                </td>
            </tr>
            <tr>
                <th>Tipo de Lugar Ubicación</th>
                <td>
                    <asp:DropDownList Width="70%" runat="server" ID="ddlLugar" AppendDataBoundItems="true" data-tipoControl="combo"></asp:DropDownList>
                    <img id="imgTooltipDdlLugar" src="<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Icono_Ayuda.png")%>" title="Tipo de lugar de ubicación, por ejemplo: Finca 1." />
                    <asp:RequiredFieldValidator runat="server" ValidationGroup="Aceptar" ID="rfvDdlLugar" ControlToValidate="ddlLugar" Display="Dynamic" ErrorMessage="Debe seleccionar el tipo de lugar de ubicación para el lugar de trabajo.">&nbsp;</asp:RequiredFieldValidator>

                </td>
            </tr>
            <tr>
                <th>Sector responsable</th>
                <td>
                    <asp:DropDownList Width="70%" runat="server"  ID="ddlSector" AppendDataBoundItems="true" data-tipoControl="combo"></asp:DropDownList>
                    <asp:RequiredFieldValidator runat="server" ValidationGroup="Aceptar" ID="rfvDdlSector" ControlToValidate="ddlSector" Display="Dynamic" ErrorMessage="Debe seleccionar el sector.">&nbsp;</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <th>Estado</th>
                <td>
                    <asp:DropDownList Width="70%" runat="server" ID="ddlEstado" AppendDataBoundItems="true"></asp:DropDownList>
                    <asp:RequiredFieldValidator runat="server" ValidationGroup="Aceptar" ID="rfvDdlEstado" ControlToValidate="ddlEstado" Display="Dynamic" ErrorMessage="Debe seleccionar el estado.">&nbsp;</asp:RequiredFieldValidator>
                </td>
            </tr>           
             <tr>
                <th>Unidad</th>
                <td>
                    <asp:UpdatePanel runat="server" ID="upTxtUnidad" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:TextBox Width="70%" runat="server" ID="txtUnidad" data-tipocontrol="texto" AutoPostBack="true"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ID="rfvTxtUnidad" ControlToValidate="txtUnidad" Display="Dynamic" ErrorMessage="La unidad es requerida." ValidationGroup="AgregarUnidad">&nbsp;</asp:RequiredFieldValidator>
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
                            <asp:Label runat="server" ID="lblNombreUnidad" data-tipocontrol="etiqueta" Width="70%"></asp:Label>
                            <asp:Button runat="server" ID="btnAgregar" Text="Agregar Unidad" ValidationGroup="AgregarUnidad" />
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="txtUnidad" EventName="TextChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>            
        </table>
    </article>

    <article class="listado sinBorde">
        <asp:UpdatePanel runat="server" ID="upUnidades" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:Repeater runat="server" ID="rpUnidades">
                    <HeaderTemplate>
                        <table>
                            <tr>
                                <th>Unidad</th>
                                <th>&nbsp;</th>
                            </tr>
                            
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr class="lineaDelListado">

                            <td><%#Eval("COD_DESC")%></td>

                            <td runat="server" id="tdBorrar">
                                <asp:ImageButton ID="ibBorrar" runat="server" ToolTip="Borrar" data-tipo="borrarRegistro"
                                    CommandName='<%#Eval("CODIGO_UBICA")%>'
                                    ImageUrl='<%#AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Borrar.png")%>'
                                    onmouseover='<%#String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Icono_Borrar.png"))%>'
                                    onmouseout='<%#String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Borrar.png"))%>'
                                    OnClick="ibBorrar_Click" />
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnAgregar" />
            </Triggers>
        </asp:UpdatePanel>
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
                                        <asp:Label ID="Label1" runat="server" Text='<%#Eval("DESCRIPCION")%>'></asp:Label>
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
            window.location = 'Lst_OT_LugarTrabajo.aspx';
        }

        function deshabilitarFormulario() {
            deshabilitarControl('#<%=Me.btnAceptar.ClientID%>');
            deshabilitarControl('#<%=Me.btnLimpiarFormulario.ClientID%>');
            deshabilitarControl('#btnCancelar');

            $('.formulario').attr('disabled','disabled');
        }

        function mostrarPopUp(pvc_IdPopup) {
            window.location = pvc_IdPopup;
        };

        function mostrarPopupRegistroExitoso () {
            var vlo_ConfiguracionPopup = {
                titulo: 'Catálogo de Edificios y Sitios Universitarios',
                mensaje: 'Se a registrado la informacion del lugar de trabajo.<br/><strong>Desea registrar otro lugar de trabajo?</strong>',
                botones:
                    [  //en corchetes indican arreglos y los parentesis cuadrados indican inicio o fin de funcionalidad o objeto, $librerias de jquery
                        {
                            idControl: "btnSi",
                            textoBoton: "Si",
                            onClick:
                                function (e) {
                                    window.location = 'Frm_OT_LugarTrabajo.aspx?pvn_Operacion=<%= Utilerias.OrdenesDeTrabajo.eOperacion.Agregar%>'; //pvn_Operacion????
                                }
                        },
                        {
                            idControl: "btnNo",
                            textoBoton: "No",
                            onClick: function (e) { regresarAlListado();}
                                
                        },
                        
                    ]
            };

                $('#arPopupDelFormulario').popup(vlo_ConfiguracionPopup);
                window.location = '#arPopupDelFormulario';
            
            }

            function mostrarAlertaError(pvc_Mensaje) {
                mostrarAlerta(
                    '#arAlertasDelFormulario',
                    {
                        mensaje: pvc_Mensaje,
                        tipo: "peligro",
                        transparencia: 0.9,
                        posicion: 'center',
                        permiteCerrar: true
                    }
                    );
            }

            function mostrarAlertaActualizacionExitosa() {
                deshabilitarFormulario();

                mostrarAlerta(
                    '#arAlertasDelFormulario',
                    {
                        mensaje: 'Se ha actualizado la información del lugar de trabajo',
                        tipo: "exito",
                        transparencia: 0.9,
                        posicion: 'center',
                        permiteCerrar: true,
                        onClosed: function () { regresarAlListado(); }
                    }
                    );
            }       

            function mostrarAlertaLlaveIncorrecta() {
                deshabilitarFormulario();

                mostrarAlerta(
                    '#arAlertasDelFormulario',
                    {
                        mensaje: 'El número de identificación provisto no pertenece a ningun lugar de trabajo registrado en el sistema',
                        tipo: "peligro",
                        transparencia: 0.9,
                        posicion: 'center',
                        permiteCerrar: true,
                        onClosed: function () { regresarAlListado(); }
                    }
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

                habilitarTooltipPorControl('#imgTooltipDdlUbicacion');
                habilitarTooltipPorControl('#imgTooltipDdlLugar');

                configurarLongitudMaximaTexto('#<%=Me.txtNombre.ClientID%>', <%=Utilerias.OrdenesDeTrabajo.Modelo.OTM_LUGAR_TRABAJO.NOMBRE_BD_TAMANO%>);
                configurarContadorCaracteresRestantes('#<%=Me.txtNombre.ClientID%>', '#spContadorTxtNombre');

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
                inicializarScript()
            });

            function cargarLupa() {
                permutarImagenes('#imgBuscarUnidad',
                    '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Lupa.png")%>',
                '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Lupa.png")%>',
                '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Lupa.png")%>'
            );
            }
    </script>
</asp:Content>

