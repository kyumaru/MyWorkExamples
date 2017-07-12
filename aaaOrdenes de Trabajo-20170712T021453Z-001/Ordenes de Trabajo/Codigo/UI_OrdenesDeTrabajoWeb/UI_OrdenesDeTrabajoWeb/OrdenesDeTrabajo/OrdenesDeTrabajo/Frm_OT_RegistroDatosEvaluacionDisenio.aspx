<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/Mp_Formulario.master" AutoEventWireup="false" CodeFile="Frm_OT_RegistroDatosEvaluacionDisenio.aspx.vb" Inherits="OrdenesDeTrabajo_Frm_OT_RegistroDatosEvaluacionDisenio" %>

<%@ Register TagPrefix="wuc" TagName="InformacionGeneral" Src="~/ControlesDeUsuario/Wuc_OT_InformacionGeneral.ascx" %>
<%@ Register TagPrefix="wuc" Namespace="Utilerias.WebForms.Controles" Assembly="Utilerias.WebForms.Controles" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphFormulario" runat="Server">

    <header>
        <h2>
            <asp:Label runat="server" ID="lblAccion" Text="Gestión de Recursos para Evaluación y Ejecución de Órdenes de Trabajo"></asp:Label>
        </h2>
    </header>

    <article class="formulario">
        <table>
            <tr>
                <th>Nombre del Proyecto</th>
                <td>
                    <asp:TextBox runat="server" ID="txtNombreProyecto" data-tipocontrol="texto" MaxLength="512" Width="59%"></asp:TextBox>
                    <asp:Button runat="server" ID="btnActualizar" Text="Actualizar" ValidationGroup="Actualizar" />
                    <br />
                    <asp:RequiredFieldValidator runat="server" ID="rfvTxtNombreProyecto" ControlToValidate="txtNombreProyecto" Display="Dynamic" ErrorMessage="Nombre Proyecto es obligatorio." ValidationGroup="Actualizar"></asp:RequiredFieldValidator>
                </td>
            </tr>
        </table>
    </article>
    <br />

    <article class="tituloSeccion">
        Información General de la OT
    </article>
    <article class="formulario sinBorde">
        <asp:UpdatePanel runat="server" ID="upControlOrdenTrabajo" UpdateMode="Conditional">
            <ContentTemplate>
                <wuc:InformacionGeneral ID="ctrl_InfoGeneral" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </article>
    <br />

    <article class="tituloSeccion">
        Profesional Responsable
    </article>
    <article class="formulario">
        <table>
            <tr>
                <th>Encargado</th>
                <td>
                    <asp:DropDownList runat="server" ID="ddlEncargado" data-tipocontrol="combo" AppendDataBoundItems="true"></asp:DropDownList>
                    <asp:RequiredFieldValidator runat="server" ID="rfvDdlEncargado" ControlToValidate="ddlEncargado" Display="Dynamic" ErrorMessage="El encargado es requerido." ValidationGroup="Aceptar">&nbsp;</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <th>Fecha Desde</th>
                <td>
                    <asp:TextBox ID="txtFechaDesde" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ID="rfvTxtFechaDesde" ControlToValidate="txtFechaDesde" Display="Dynamic" ErrorMessage="La Fecha desde del trabajo es requerida." ValidationGroup="Aceptar">&nbsp;</asp:RequiredFieldValidator>
                    <asp:CompareValidator runat="server" ID="cmpvTxtFechaDesde" ControlToValidate="txtFechaDesde" Display="Dynamic" ErrorMessage="La Fecha desde del trabajo es inválida." Operator="DataTypeCheck" Type="Date" ValidationGroup="Aceptar">&nbsp;</asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <th>Fecha Hasta</th>
                <td>
                    <asp:TextBox ID="txtFechaHasta" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ID="rfvTxtFechaHasta" ControlToValidate="txtFechaHasta" Display="Dynamic" ErrorMessage="La Fecha hasta del trabajo es requerida" ValidationGroup="Aceptar">&nbsp;</asp:RequiredFieldValidator>
                    <asp:CompareValidator runat="server" ID="cmpvTxtFechaHasta" ControlToValidate="txtFechaHasta" Display="Dynamic" ErrorMessage="La Fecha hasta del trabajo es inválida" Operator="DataTypeCheck" Type="Date" ValidationGroup="Aceptar">&nbsp;</asp:CompareValidator>
                </td>
            </tr>
        </table>
    </article>

    <article class="areaBotones">
        <asp:Button runat="server" ID="btnAgregar" Text="Agregar" ValidationGroup="Aceptar" />
        <asp:Button runat="server" ID="btnModificar" Text="Modificar" ValidationGroup="Aceptar" Visible="false" />
    </article>

    <br />

    <article data-grupo="Listado" class="listado sinBorde">
        <asp:Repeater runat="server" ID="rpEncargado">
            <HeaderTemplate>
                <table>
                    <tr>
                        <th>
                            <asp:Label runat="server" ID="lnkIdentificacion" Text="Identificación"></asp:Label>
                        </th>
                        <th>
                            <asp:Label runat="server" ID="lnkNombre" Text="Nombre"></asp:Label>
                        </th>
                        <th>
                            <asp:Label runat="server" ID="lnkArea" Text="Área"></asp:Label>
                        </th>
                        <th>
                            <asp:Label runat="server" ID="lnkFechaDesde" Text="Fecha Desde"></asp:Label>
                        </th>
                        <th>
                            <asp:Label runat="server" ID="lnkFechaHasta" Text="Fecha Hasta"></asp:Label>
                        </th>
                        <th>&nbsp</th>
                        <th>&nbsp</th>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr class="lineaDelListado">
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.CEDULA)%></td>
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.NOMBRE_EMPLEADO)%></td>
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.DESC_AREA)%></td>
                    <td><%#CType(Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.FECHA_DESDE), DateTime).ToString(Utilerias.OrdenesDeTrabajo.Constantes.FORMATO_FECHA_UI)%></td>
                    <td><%#CType(Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.FECHA_HASTA), DateTime).ToString(Utilerias.OrdenesDeTrabajo.Constantes.FORMATO_FECHA_UI)%></td>
                    <td>
                        <asp:ImageButton ID="imgModificar" runat="server" AlternateText="Modificar" ToolTip="Modificar" data-tipo="modificarRegistro"
                            Visible='<%#IIf(Me.OrdenTrabajo.EstadoOrdenTrabajo <> Utilerias.OrdenesDeTrabajo.EstadoOrden.ASIGNADA, true, false)%>'
                            CommandArgument='<%# String.Format("{0},{1}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.NUM_EMPLEADO), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.ID_SECTOR_TALLER))%>'
                            OnClick="ibModificar_Click"
                            ImageUrl='<%#AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Editar.png")%>'
                            onmouseover='<%# String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Icono_Editar.png"))%>'
                            onmouseout='<%# String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Editar.png"))%>' />
                    </td>
                    <td>
                        <asp:ImageButton runat="server" ID="ibBorrarEncargado" AlternateText="Borrar registro" ToolTip="Borrar registro" data-tipo="borrarRegistro"
                            CommandArgument='<%# String.Format("{0},{1}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.NUM_EMPLEADO), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.ID_SECTOR_TALLER))%>'
                            OnClick="ibBorrar_Click"
                            Visible='<%#IIf(Me.OrdenTrabajo.EstadoOrdenTrabajo = Utilerias.OrdenesDeTrabajo.EstadoOrden.ASIGNADA, true, false)%>'
                            ImageUrl='<%#AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Borrar.png")%>'
                            onmouseover='<%# String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Icono_Borrar.png"))%>'
                            onmouseout='<%# String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Borrar.png"))%>' />
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
    </article>

    <br />
    <br />

    <article class="tituloSeccion">
        Profesionales para Valoración
    </article>
    <article class="formulario">
        <table>
            <tr>
                <th>Funcionario</th>
                <td>
                    <asp:DropDownList runat="server" ID="ddlFuncionario" data-tipocontrol="combo" AppendDataBoundItems="true" AutoPostBack="true"></asp:DropDownList>
                    <asp:RequiredFieldValidator runat="server" ID="rfvDdlFuncionario" ControlToValidate="ddlFuncionario" Display="Dynamic" ErrorMessage="El funcionario es requerido." ValidationGroup="Aceptar2">&nbsp;</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <th>Área Profesional</th>
                <td>
                    <asp:UpdatePanel runat="server" ID="upLblAreaFuncionario" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:Label runat="server" ID="lblAreaFuncionario"></asp:Label>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlFuncionario" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <th>Tiempo estimado para realizar la evaluación</th>
                <td>
                    <asp:TextBox ID="txtTiempo" runat="server" MaxLength="8"></asp:TextBox>
                    <asp:DropDownList runat="server" ID="ddlUnidadTiempo" data-tipocontrol="combo" AppendDataBoundItems="true"></asp:DropDownList>
                    <asp:RequiredFieldValidator runat="server" ID="rfvTxtTiempo" ControlToValidate="txtTiempo" Display="Dynamic" ErrorMessage="El tiempo estimado para evaluación es requerido." ValidationGroup="Aceptar2">&nbsp;</asp:RequiredFieldValidator>
                    <ajax:FilteredTextBoxExtender ID="ftbTxtTiempo" runat="server" TargetControlID="txtTiempo" FilterMode="ValidChars" ValidChars="." FilterType="Numbers"></ajax:FilteredTextBoxExtender>
                </td>
            </tr>
        </table>
    </article>

    <article class="areaBotones">
        <asp:Button runat="server" ID="btnAgregarFuncionario" Text="Agregar" ValidationGroup="Aceptar2" />
    </article>

    <br />

    <article data-grupo="Listado" class="listado sinBorde">
        <asp:Repeater runat="server" ID="rpFuncionarios">
            <HeaderTemplate>
                <table>
                    <tr>
                        <th>
                            <asp:Label runat="server" ID="lnkIdentificacion" Text="Identificación"></asp:Label>
                        </th>
                        <th>
                            <asp:Label runat="server" ID="lnkNombre" Text="Nombre"></asp:Label>
                        </th>
                        <th>
                            <asp:Label runat="server" ID="lnkArea" Text="Área"></asp:Label>
                        </th>
                        <th>
                            <asp:Label runat="server" ID="lnkTiempo" Text="Tiempo"></asp:Label>
                        </th>
                        <th>&nbsp</th>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr class="lineaDelListado">
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_TIEMPO_OPERARIOLST.CEDULA)%></td>
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_TIEMPO_OPERARIOLST.NOMBRE_EMPLEADO)%></td>
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_TIEMPO_OPERARIOLST.DESC_AREA)%></td>
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_TIEMPO_OPERARIOLST.DESC_TIEMPO_REAL)%></td>
                    <td>
                        <asp:ImageButton runat="server" ID="ibBorrarColaborador" AlternateText="Borrar registro" ToolTip="Borrar registro" data-tipo="borrarRegistro"
                            CommandArgument='<%# String.Format("{0},{1}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_TIEMPO_OPERARIOLST.NUM_EMPLEADO), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_TIEMPO_OPERARIOLST.ID_SECTOR_TALLER))%>'
                            OnClick="ibBorrarColaborador_Click"
                            Visible='<%#IIf(Me.OrdenTrabajo.EstadoOrdenTrabajo = Utilerias.OrdenesDeTrabajo.EstadoOrden.ASIGNADA, true, false)%>'
                            ImageUrl='<%#AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Borrar.png")%>'
                            onmouseover='<%# String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Icono_Borrar.png"))%>'
                            onmouseout='<%# String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Borrar.png"))%>' />
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
    </article>

    <br />
    <br />

    <article class="areaBotones">
        <asp:Button runat="server" ID="btnGuardar" Text="Guardar" ValidationGroup="Actualizar" />
        <asp:Button runat="server" ID="btnGuardarEnviar" Text="Guardar y Finalizar" ValidationGroup="Actualizar" />
        <asp:Button runat="server" ID="btnCancelar" Text="Regresar"/>
        <%--<input id="btnCancelar" type="button" value="Regresar" />--%>
    </article>

    <article id="arPopupDelFormulario"></article>
    <article id="arAlertasDelFormulario"></article>
    <article id="arpopupConfirmaEnviar"></article>
    <article id="arpopupConfirmaDeseaBorrar"></article>

    <script type="text/javascript">

        function establecerFechaMinima() {
            if (document.getElementById('<%=txtFechaDesde.ClientID%>')) {
                configurarDatePicker('#<%=Me.txtFechaHasta.ClientID%>');
                establecerFechaMinimaDatePicker("#<%=Me.txtFechaHasta.ClientID%>", document.getElementById('<%=txtFechaDesde.ClientID%>').value);
            }
        };

        function establecerRangosFecha() {
            configurarDatePickerRango('#<%=Me.txtFechaDesde.ClientID%>', '#<%=Me.txtFechaHasta.ClientID%>');
        }

        function regresarAlListado() {
            window.location = 'Lst_OT_GestionOrdenTrabajoCordinadorSectorTaller.aspx';
        };

        function deshabilitarFormulario() {
            deshabilitarControl('#<%=Me.btnActualizar.ClientID%>');
            deshabilitarControl('#<%=Me.btnAgregar.ClientID%>');
            deshabilitarControl('#<%=Me.btnModificar.ClientID%>');
            deshabilitarControl('#<%=Me.btnAgregarFuncionario.ClientID%>');
            deshabilitarControl('#<%=Me.btnGuardar.ClientID%>');
            deshabilitarControl('#<%=Me.btnGuardarEnviar.ClientID%>');
            deshabilitarControl('#<%=Me.btnCancelar.ClientID%>');

            $('.formulario').attr('disabled', 'disabled');
        };

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
        };

        function mostrarAlertaActualizacionExitosa() {
            deshabilitarFormulario();

            mostrarAlerta(
                '#arAlertasDelFormulario',
                {
                    mensaje: 'Se ha actualizado la información.',
                    tipo: "exito",
                    transparencia: 0.9,
                    posicion: 'center',
                    permiteCerrar: true,
                    onClosed: function () { regresarAlListado(); }
                }
                );
        };


        function mostrarAlertaNombreProyectoExitoso() {
            mostrarAlerta(
                '#arAlertasDelFormulario',
                {
                    mensaje: 'Se ha actualizado la información del nombre de proyecto.',
                    tipo: "exito",
                    transparencia: 0.9,
                    posicion: 'center',
                    permiteCerrar: true
                }
                );
        };

        function mostrarAlertaLlaveIncorrecta() {
            deshabilitarFormulario();

            mostrarAlerta(
                '#arAlertasDelFormulario',
                {
                    mensaje: 'El número de identificación provisto no pertenece a ningún registro en el sistema.',
                    tipo: "peligro",
                    transparencia: 0.9,
                    posicion: 'center',
                    permiteCerrar: true,
                    onClosed: function () { regresarAlListado(); }
                }
                );
        };

        $(document).ready(function () {            
            $('#<%=btnCancelar.ClientID%>').click(function () {
                regresarAlListado();                
            });
        });

        function ActivarMensajeClick() {
            $('#<%=btnCancelar.ClientID%>').click(function () {
                return mostrarPopupConfirmacionCancelar();
            });
        };

        function mostrarPopupConfirmacionCancelar() {
            var vlo_ConfiguracionPopup = {
                titulo: 'Gestión de Recursos',
                mensaje: '¿Está seguro que desea regresar? En caso de haber realizado algún cambio no se verá reflejado',
                botones:
                        [
                            {
                                idControl: "btnSi",
                                textoBoton: "Si",
                                onClick:
                                    function (e) {
                                        $(this).attr("disabled", "disabled");
                                        __doPostBack('<%=btnCancelar.UniqueID%>', '');
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

            $('#arpopupConfirmaDeseaBorrar').popup(vlo_ConfiguracionPopup);

            window.location = '#arpopupConfirmaDeseaBorrar';

            return false;
        };



        function mostrarPopupConfirmaDeseaBorrarRegistro(pvo_UniqueIdControl) {
            var vlo_ConfiguracionPopup = {
                titulo: 'Gestión de Recursos',
                mensaje: '¿Desea borrar el registro seleccionado?',
                botones:
                        [
                            {
                                idControl: "btnSi",
                                textoBoton: "Si",
                                onClick:
                                    function (e) {
                                        $(this).attr("disabled", "disabled");
                                        __doPostBack(pvo_UniqueIdControl, '');
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

            $('#arpopupConfirmaDeseaBorrar').popup(vlo_ConfiguracionPopup);

            window.location = '#arpopupConfirmaDeseaBorrar';

            return false;
        };

    </script>

</asp:Content>

