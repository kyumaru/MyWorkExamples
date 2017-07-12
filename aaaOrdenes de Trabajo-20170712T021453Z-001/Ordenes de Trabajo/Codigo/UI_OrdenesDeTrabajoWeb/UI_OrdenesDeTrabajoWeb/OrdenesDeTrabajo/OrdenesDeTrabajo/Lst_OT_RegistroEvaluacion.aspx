<%@ Page MasterPageFile="~/MasterPage/Mp_Listado.master" Language="VB" AutoEventWireup="false" CodeFile="Lst_OT_RegistroEvaluacion.aspx.vb" Inherits="OrdenesDeTrabajo_Lst_OT_RegistroEvaluacion" %>

<%@ Register TagPrefix="wuc" TagName="InformacionGeneral" Src="~/ControlesDeUsuario/Wuc_OT_InformacionGeneral.ascx" %>
<%@ Register TagPrefix="wuc" TagName="Materiales" Src="~/Controles/wuc_OT_Materiales.ascx" %>
<%@ Register TagPrefix="wuc" TagName="DatosMaterial" Src="~/Controles/wuc_OT_DatosMaterial.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content runat="server" ContentPlaceHolderID="cphFormulario" ID="Content1">

    <header>
        <h2>Gestión de recursos para evaluación y control de registro de datos para Ordenes de Trabajo de Mantenimiento</h2>
    </header>

    <article class="tituloSeccion">
        Información general del proyecto
    </article>
    <wuc:InformacionGeneral ID="ctrl_InfoGeneral" runat="server" />

    <article class="formulario">

        <ul class="encabezadoTabPanel">
            <li id="liRecursosEvaluacion" runat="server" class="activo"><a class="tituloTabPanel" href="#tpEvaluacion">Recursos Para Evaluación</a></li>
            <li id="liRecursosEjecucion" runat="server" class=""><a class="tituloTabPanel" href="#tpEjecucion">Recursos Para Ejecución</a></li>
        </ul>

        <article class="cuerpoTabPanel">
            <article id="tpEvaluacion" runat="server" class="tabPanel">

                <ul class="encabezadoTabPanel">
                    <li id="liRecursoHumano" runat="server" class="activo"><a class="tituloTabPanel" href="#tpRecursoHumano">Recurso Humano</a></li>
                    <%--<li id="liTiempo" runat="server" class=""><a class="tituloTabPanel" href="#tpTiempo">Tiempo</a></li>--%>
                </ul>

                <article runat="server" class="cuerpoTabPanel">
                    <article runat="server" id="tpRecursoHumano" class="tabPanel">

                        <%--LISTADO DE FUNCIONARIOS--%>
                        <article class="tituloSeccion">
                            Registro de tiempos estimados para recursos de la Evaluación
                        </article>
                        <article class="formulario">
                            <table>
                                <tr runat="server" id="trFuncionarioEvaluacion">
                                    <th>Funcionario</th>
                                    <td>
                                        <asp:DropDownList ValidationGroup="Agregar" runat="server" ID="ddlFuncionario" Width="75%" data-tipoControl="texto"></asp:DropDownList>
                                        <br />
                                        <asp:RequiredFieldValidator ValidationGroup="Agregar" runat="server" ID="rfvddlFuncionario" ControlToValidate="ddlFuncionario" Display="Dynamic" ErrorMessage="Debe indicar al menos un funcionario para realizar la evaluación del trabajo">&nbsp;</asp:RequiredFieldValidator>
                                    </td>
                                    <td>
                                        <asp:Button runat="server" ValidationGroup="Agregar" Text="Agregar" ID="btnAgregarFuncionario" /></td>
                                </tr>
                            </table>
                        </article>

                        <article class="listado sinBorde">
                            <asp:Repeater runat="server" ID="rpFuncionarios">

                                <HeaderTemplate>
                                    <table>
                                        <tr>
                                            <th>Identificación</th>
                                            <th>Nombre</th>
                                            <th>&nbsp;</th>
                                        </tr>
                                </HeaderTemplate>

                                <ItemTemplate>
                                    <tr class="lineaDelListado">

                                        <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTF_OPERARIO_AREALST.CEDULA)%></td>
                                        <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTF_OPERARIO_AREALST.NOMBRE_EMPLEADO)%></td>

                                        <td runat="server" id="tdBorrar">
                                            <asp:ImageButton ID="ibBorrar" runat="server" CausesValidation="false" ToolTip="Borrar" data-tipo="borrarRegistro"
                                                CommandName='<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTF_OPERARIO_AREALST.CEDULA)%>'
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
                        </article>

                    </article>
                </article>

                <%-- TIEMPO --%>

                <%--                <article runat="server" id="cuerpoTabPanel2" class="cuerpoTabPanel">
                    <article runat="server" id="tpTiempo" class="tabPanel">

                        <article class="formulario">
                            <table>
                                <tr>
                                    <td>Tiempo estimado para efectuar la evaluación
                                        <br />
                                        <br />
                                        <asp:TextBox runat="server" ID="txtTiempoEstimado" Width="5%" data-tipoControl="texto"></asp:TextBox>
                                        <asp:DropDownList Width="28%" runat="server" ID="ddlUnidad" AppendDataBoundItems="true" data-tipoControl="combo"></asp:DropDownList>
                                        <br />
                                        <asp:RequiredFieldValidator ValidationGroup="GYE" runat="server" ID="rvfTxtTiempoEstimado" ControlToValidate="txtTiempoEstimado" Display="Dynamic" ErrorMessage="Debe indicar el tiempo estimado para efectuar la evaluación del trabajo.">&nbsp;</asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ValidationGroup="GYE" runat="server" ID="rfvDdlUnidad" ControlToValidate="ddlUnidad" Display="Dynamic" ErrorMessage="La unidad de tiempo es requerida">&nbsp;</asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Fecha propuesta para Inicio de la Evaluación
                                        <br />
                                        <br />
                                        <asp:TextBox runat="server" Width="33%" ID="txtDPFechaPropuesta"></asp:TextBox>
                                        <asp:RequiredFieldValidator ValidationGroup="GYE" runat="server" ID="rfvDate" ControlToValidate="txtDPFechaPropuesta" Display="Dynamic" ErrorMessage="Debe indicar una fecha estimada para la realización de la evaluación.">&nbsp;</asp:RequiredFieldValidator>
                                        <asp:CompareValidator runat="server" ID="cmpvTxtDPFechaPropuesta" ControlToValidate="txtDPFechaPropuesta" Display="Dynamic" ErrorMessage="La fecha estimada es inválida" Operator="DataTypeCheck" Type="Date" ValidationGroup="Aceptar">&nbsp;</asp:CompareValidator>
                                    </td>
                                </tr>
                            </table>
                        </article>

                    </article>
                </article>--%>
            </article>
        </article>

        <article class="cuerpoTabPanel">
            <article id="tpEjecucion" class="tabPanel" runat="server">
                <ul class="encabezadoTabPanel">
                    <li id="liRHEjecucion" runat="server" class="activo"><a class="tituloTabPanel" href="#tpRHEjecucion">Recurso Humano</a></li>
                    <li id="liTiempoEjecucion" runat="server" class=""><a class="tituloTabPanel" href="#tpTiempoEjecucion">Tiempo</a></li>
                    <li id="liMEEjecucion" runat="server" class=""><a class="tituloTabPanel" href="#tpMEEjecucion">Materiales</a></li>
                </ul>

                <article id="cuerpoTabPanel3" runat="server" class="cuerpoTabPanel">
                    <article runat="server" id="tpRHEjecucion" class="tabPanel">
                        <%--LISTADO DE FUNCIONARIOS--%>
                        <article class="tituloSeccion">
                            Registro de tiempos estimados para recursos de Ejecución
                        </article>
                        <article class="formulario">
                            <table>
                                <tr runat="server" id="trFuncionarioEjecucion">
                                    <th>Funcionario</th>
                                    <td>
                                        <asp:DropDownList ValidationGroup="AgregarEjec" runat="server" ID="ddlFuncionarioEjecucion" Width="75%" data-tipoControl="texto"></asp:DropDownList>
                                        <br />
                                        <asp:RequiredFieldValidator ValidationGroup="AgregarEjec" runat="server" ID="rfvddlFuncionarioEjecucion" ControlToValidate="ddlFuncionarioEjecucion" Display="Dynamic" ErrorMessage="Debe indicar al menos un funcionario para realizar la evaluación del trabajo">&nbsp;</asp:RequiredFieldValidator>
                                    </td>
                                    <td>
                                        <asp:Button runat="server" ValidationGroup="AgregarEjec" Text="Agregar" OnClick="btnAgregarFuncionarioEjecucion_Click" ID="btnAgregarFuncionarioEjecucion" /></td>
                                </tr>
                            </table>
                        </article>

                        <article class="listado sinBorde">
                            <asp:Repeater runat="server" ID="rpFuncionariosEjecucion">

                                <HeaderTemplate>
                                    <table>
                                        <tr>
                                            <th>Identificación</th>
                                            <th>Nombre</th>
                                            <th>&nbsp;</th>
                                        </tr>
                                </HeaderTemplate>

                                <ItemTemplate>
                                    <tr class="lineaDelListado">

                                        <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTF_OPERARIO_AREALST.CEDULA)%></td>
                                        <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTF_OPERARIO_AREALST.NOMBRE_EMPLEADO)%></td>

                                        <td>
                                            <article style="display: <%#IIf((CType(Me.OrdenTrabajo.EstadoOrdenTrabajo, String) <> Utilerias.OrdenesDeTrabajo.EstadoOrden.EN_EVALUACION), "none", "block")%>">
                                                <asp:ImageButton ID="ibBorrar" runat="server" CausesValidation="false" ToolTip="Borrar" data-tipo="borrarRegistro"
                                                    CommandName='<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTF_OPERARIO_AREALST.CEDULA)%>'
                                                    ImageUrl='<%#AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Borrar.png")%>'
                                                    onmouseover='<%#String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Icono_Borrar.png"))%>'
                                                    onmouseout='<%#String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Borrar.png"))%>'
                                                    OnClick="ibBorrarEjecución_Click" />
                                            </article>
                                        </td>
                                    </tr>
                                </ItemTemplate>

                                <FooterTemplate>
                                    </table>
                                </FooterTemplate>

                            </asp:Repeater>
                        </article>


                        <%--             <article class="tituloSeccion">
                            Registro de tiempos reales de recursos para Evaluación
                        </article>

                        <article class="formulario">
                            <table>
                                <tr runat="server" id="trFuncionariosReal">
                                    <th>Funcionario</th>
                                    <td>
                                        <asp:DropDownList ValidationGroup="AgregarRegal" runat="server" ID="ddlFuncionariosReal" Width="75%" data-tipoControl="texto"></asp:DropDownList>
                                        <br />
                                        <asp:RequiredFieldValidator ValidationGroup="AgregarRegal" runat="server" ID="rfvFuncionariosReal" ControlToValidate="ddlFuncionariosReal" Display="Dynamic" ErrorMessage="Debe indicar al menos un funcionario para asignar los tiempos reales">&nbsp;</asp:RequiredFieldValidator>
                                    </td>
                                    <td>
                                        <asp:Button runat="server" ValidationGroup="AgregarRegal" Text="Agregar" ID="btnAgregarFuncionarioTR" /></td>
                                </tr>
                            </table>
                        </article>

                        <article class="listado sinBorde" style="display: <%#IIf(Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ORDEN_TRABAJOLST.ESTADO_ORDEN_TRABAJO).ToString = Utilerias.OrdenesDeTrabajo.EstadoOrden.PARA_IMPRESION OR Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ORDEN_TRABAJOLST.ESTADO_ORDEN_TRABAJO).ToString = Utilerias.OrdenesDeTrabajo.EstadoOrden.EN_EVALUACION, "block", "none")%>">
                            <asp:Repeater runat="server" ID="rpFuncionariosReal">

                                <HeaderTemplate>
                                    <table>
                                        <tr>
                                            <th>Identificación</th>
                                            <th>Nombre</th>
                                            <th>&nbsp;</th>
                                        </tr>
                                </HeaderTemplate>

                                <ItemTemplate>
                                    <tr class="lineaDelListado">

                                        <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTF_OPERARIO_AREALST.CEDULA)%></td>
                                        <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTF_OPERARIO_AREALST.NOMBRE_EMPLEADO)%></td>

                                        <td runat="server" id="tdBorrar">
                                            <asp:ImageButton ID="ibBorrar" runat="server" CausesValidation="false" ToolTip="Borrar" data-tipo="borrarRegistro"
                                                CommandName='<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTF_OPERARIO_AREALST.CEDULA)%>'
                                                ImageUrl='<%#AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Borrar.png")%>'
                                                onmouseover='<%#String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Icono_Borrar.png"))%>'
                                                onmouseout='<%#String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Borrar.png"))%>'
                                                OnClick="ibBorrarEvaluacionTR_Click" />
                                        </td>
                                    </tr>
                                </ItemTemplate>

                                <FooterTemplate>
                                    </table>
                                </FooterTemplate>

                            </asp:Repeater>
                        </article>--%>
                    </article>
                </article>

                <article id="cuerpoTabPanel5" runat="server" class="cuerpoTabPanel">
                    <article runat="server" id="tpTiempoEjecucion" class="tabPanel">
                        <%--FORMULARIO DE TIEMPO DE EJECUCIÓN--%>

                        <article class="formulario">
                            <table>
                                <tr>
                                    <td>Fecha en la que se efectuó la evaluación:
                                        <br />
                                        <br />
                                        <asp:TextBox ValidationGroup="GYE" runat="server" Width="33%" ID="txtDPFechaEfectuo"></asp:TextBox>
                                        <asp:RequiredFieldValidator ValidationGroup="GYE" runat="server" ID="rfvFechaEfectuo" ControlToValidate="txtDPFechaEfectuo" Display="Dynamic" ErrorMessage="Debe indicar una fecha estimada para la realización de la evaluación.">&nbsp;</asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <%--       <tr>
                                    <td>Tiempo real invertido en la estimación del trabajo:
                                        <br />
                                        <br />
                                        <asp:TextBox ValidationGroup="GYE" runat="server" ID="txtTiempoReal" Width="5%" data-tipoControl="texto"></asp:TextBox>
                                        <asp:DropDownList ValidationGroup="GYE" Width="28%" runat="server" ID="ddlUnidadesDeTiempo" AppendDataBoundItems="true" data-tipoControl="combo"></asp:DropDownList>
                                        <br />
                                        <asp:RequiredFieldValidator ValidationGroup="GYE" runat="server" ID="rfvTxtTiempoReal" ControlToValidate="txtTiempoReal" Display="Dynamic" ErrorMessage="Debe indicar el tiempo real invertido en la evaluación del trabajo.">&nbsp;</asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ValidationGroup="GYE" runat="server" ID="rfvDdlUnidadesDeTiempo" ControlToValidate="ddlUnidadesDeTiempo" Display="Dynamic" ErrorMessage="La unidad de tiempo es requerida">&nbsp;</asp:RequiredFieldValidator>
                                    </td>
                                </tr>--%>
                                <tr>
                                    <td>Fecha propuesta para Inicio del trabajo:
                                        <br />
                                        <br />
                                        <asp:TextBox ValidationGroup="GYE" runat="server" Width="33%" ID="txtdpFechaPropuestaInicio"></asp:TextBox>
                                        <asp:RequiredFieldValidator ValidationGroup="GYE" runat="server" ID="rfvTxtdpFechaPropuestaInicio" ControlToValidate="txtdpFechaPropuestaInicio" Display="Dynamic" ErrorMessage="Debe indicar una fecha estimada para la realización de la evaluación.">&nbsp;</asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Tiempo estimado para finalizar el trabajo:
                                        <br />
                                        <br />
                                        <asp:TextBox ValidationGroup="GYE" runat="server" ID="txtTiempoEstimadoFinaliza" Width="5%" data-tipoControl="texto"></asp:TextBox>
                                        <asp:DropDownList ValidationGroup="GYE" Width="28%" runat="server" ID="ddlUnidadTiempoEstimado" AppendDataBoundItems="true" data-tipoControl="combo"></asp:DropDownList>
                                        <br />
                                        <asp:RequiredFieldValidator ValidationGroup="GYE" runat="server" ID="rfvtxtTiempoEstimadoFinaliza" ControlToValidate="txtTiempoEstimadoFinaliza" Display="Dynamic" ErrorMessage="Debe indicar el tiempo real invertido en la evaluación del trabajo.">&nbsp;</asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ValidationGroup="GYE" runat="server" ID="rfvddlUnidadTiempoEstimado" ControlToValidate="ddlUnidadTiempoEstimado" Display="Dynamic" ErrorMessage="La unidad de tiempo es requerida">&nbsp;</asp:RequiredFieldValidator>

                                    </td>
                                </tr>
                            </table>
                        </article>

                    </article>
                </article>

                <article id="cuerpoTabPanel4" runat="server" class="cuerpoTabPanel">
                    <article runat="server" id="tpMEEjecucion" class="tabPanel">
                        <article class="formulario">
                            <table>
                                <tr runat="server" id="trNoMaterial">
                                    <th>No se requiere material</th>
                                    <td>
                                        <asp:CheckBox runat="server" ID="chkRequierMaterial" AutoPostBack="true" />
                                    </td>
                                </tr>
                            </table>
                        </article>

                        <asp:UpdatePanel runat="server" ID="upPanelMateriales">
                            <ContentTemplate>

                                <asp:Panel runat="server" ID="panelMateriales">

                                    <article class="formulario sinBorde">
                                        <table>
                                            <tr runat="server" id="trCodigo">
                                                <th>Código</th>
                                                <td>
                                                    <asp:UpdatePanel runat="server" ID="upTxtCodigo" UpdateMode="Conditional">
                                                        <ContentTemplate>
                                                            <asp:TextBox runat="server" ID="txtCodigo" data-tipocontrol="texto" AutoPostBack="true"></asp:TextBox>
                                                            <asp:RequiredFieldValidator runat="server" ID="rfvTxtCodigo" ControlToValidate="txtCodigo"
                                                                Display="Dynamic" ErrorMessage="El código del material es requerido." ValidationGroup="AgregarListado">&nbsp;</asp:RequiredFieldValidator>
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

                                    <asp:UpdatePanel runat="server" ID="upControlDatosMaterial" UpdateMode="Conditional" Visible="false">
                                        <ContentTemplate>
                                            <wuc:DatosMaterial ID="WucDatosMaterial" runat="server"></wuc:DatosMaterial>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="txtCodigo" EventName="TextChanged" />
                                            <asp:AsyncPostBackTrigger ControlID="ctrl_Materiales" />
                                        </Triggers>
                                    </asp:UpdatePanel>

                                    <article class="areaBotones">
                                        <asp:Button runat="server" ID="btnAgregarMaterial" Text="Agregar" ValidationGroup="AgregarListado" />
                                        <asp:Button runat="server" ID="btnModificarMaterial" Text="Modificar" Visible="false" />
                                        <asp:Button runat="server" ID="btnCancelarMaterial" Text="Cancelar" Visible="false" />
                                    </article>

                                    <br />

                                    <%--acá utilizar vista de hizo cesar--%>
                                    <article data-grupo="Listado" class="listado sinBorde">
                                        <asp:Repeater runat="server" ID="rpPedidos">
                                            <HeaderTemplate>
                                                <table>
                                                    <tr>
                                                        <th>
                                                            <asp:LinkButton runat="server" ID="lnkCodigo" Text="Código" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_MATERIALLST.ID_MATERIAL%>" CommandArgument="ASC" OnCommand="lnkRpPedidos_Command"></asp:LinkButton>
                                                        </th>
                                                        <th>
                                                            <asp:LinkButton runat="server" ID="lnkDescripcion" Text="Descripción" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_MATERIALLST.DESCRIPCION%>" CommandArgument="ASC" OnCommand="lnkRpPedidos_Command"></asp:LinkButton>
                                                        </th>
                                                        <th>Detalle</th>
                                                        <th>Disp. Almacen</th>
                                                        <th>Cantidad Solicitada</th>
                                                        <th>Cantidad Retirada</th>
                                                        <th>
                                                            <asp:LinkButton runat="server" ID="lnkMonto" Text="Monto" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_MATERIALLST.COSTO_PROMEDIO%>" CommandArgument="ASC" OnCommand="lnkRpPedidos_Command"></asp:LinkButton>
                                                        </th>
                                                        <th>&nbsp;</th>
                                                        <th>&nbsp;</th>
                                                    </tr>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_MATERIALLST.ID_MATERIAL)%></td>
                                                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_MATERIALLST.DESCRIPCION)%></td>
                                                    <td>
                                                        <asp:Image runat="server" ID="imgDetalle" data-tipo="tooltipDetalleMaterial"
                                                            CssClass="centradoEnRow"
                                                            title='<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_MATERIALLST.DETALLE)%>'
                                                            ImageUrl='<%#AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Informacion.png")%>' />
                                                    </td>
                                                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_MATERIALLST.DISP_ALMACEN_SOLICITUD_MEDIDA)%></td>
                                                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_MATERIALLST.CANTIDAD_SOLICITADA_MEDIDA)%></td>
                                                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_MATERIALLST.CANTIDAD_RETIRADA_MEDIDA)%></td>
                                                                                                       
                                                    <td style="text-align: right !important;">

                                                        <asp:Label runat="server" data-tipo="tooltipUnitario"
                                                            title='<%#String.Format("Costo Unitario {0}",CType(Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_MATERIALLST.COSTO_UNITARIO), Double).ToString("N2"))%>'
                                                            Text='<%#CType(Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_MATERIALLST.COSTO_PROMEDIO), Double).ToString("N2")%>'></asp:Label>
                                                                                                         
                                                    </td>
                                                    <td>
                                                        <article style="display: <%#IIf((CType(Me.OrdenTrabajo.EstadoOrdenTrabajo, String) <> Utilerias.OrdenesDeTrabajo.EstadoOrden.EN_EVALUACION), "none", "block")%>">
                                                            <asp:ImageButton runat="server" data-tipo="modificarRegistro" ID="ibModificarMaterial" AlternateText="Modificar el pedido"
                                                                CommandArgument="<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_MATERIALLST.ID_DETALLE_MATERIAL)%>"
                                                                OnClick="ibModificarMaterial_Click"
                                                                ImageUrl='<%#AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Editar.png")%>'
                                                                onmouseover='<%# String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Icono_Editar.png"))%>'
                                                                onmouseout='<%# String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Editar.png"))%>' />
                                                        </article>
                                                    </td>
                                                    <td>
                                                        <article style="display: <%#IIf((CType(Me.OrdenTrabajo.EstadoOrdenTrabajo, String) <> Utilerias.OrdenesDeTrabajo.EstadoOrden.EN_EVALUACION), "none", "block")%>">
                                                            <asp:ImageButton runat="server" ID="ibBorrarMaterial" AlternateText="Borrar el pedido" data-tipo="borrarRegistroMaterial"
                                                                CommandArgument="<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_MATERIALLST.ID_DETALLE_MATERIAL)%>"
                                                                OnClick="ibBorrarMaterial_Click"
                                                                ImageUrl='<%#AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Borrar.png")%>'
                                                                onmouseover='<%# String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Icono_Borrar.png"))%>'
                                                                onmouseout='<%# String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Borrar.png"))%>' />
                                                        </article>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                </table>
                                            </FooterTemplate>
                                        </asp:Repeater>
                                    </article>

                                    <article class="areaCantidadDeRegistro" data-grupo="Listado">
                                        <asp:Label runat="server" ID="lblMontoTotal" Text=""></asp:Label>
                                    </article>

                                </asp:Panel>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="chkRequierMaterial" EventName="CheckedChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                        <br />

                        <article class="formulario">
                            <table>
                                <tr>
                                    <th>Observaciones Generales</th>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtObservacionesMaterial" TextMode="MultiLine" Rows="4" Columns="40"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </article>
                    </article>
                </article>

            </article>
        </article>

    </article>

    <article class="areaBotones">
        <asp:Button runat="server" ID="btnGuardar" ValidationGroup="GYE" Text="Guardar" />
        <asp:Button runat="server" ID="btnGuardarYFinalizar" ValidationGroup="GYE" Text="Aceptar y Finalizar" />
        <asp:Button runat="server" ID="btnRegresar" Text="Regresar" OnClick="btnRegresar_Click" />
    </article>
    <asp:HiddenField runat="server" ID="hdfHayDatos"></asp:HiddenField>
    <article id="arPopupGenerico"></article>
    <article id="popupConfirmaDeseaBorrar"></article>
    <article id="popupConfirmacionDeseaBorrar"></article>
    <article id="arAlertasDelFormulario"></article>
    <article id="arPopupDelFormulario"></article>

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

    <style>
        .centradoEnRow {
            margin-left: 13% !important;
        }
    </style>

    <script type="text/javascript">


        function InhabilitarCodigo() {

            document.getElementById("imgBuscarMaterial").style.visibility = "hidden";

        };

        function HabilitarCodigo() {

            document.getElementById("imgBuscarMaterial").style.visibility = "visible";

        };

        function mostrarPopupConfirmacionDeseaBorrarRegistro(pvc_UniqueIdControl) {
            var vlo_ConfiguracionPopup = {
                titulo: "<em>Catalogo de requisición de materiales</em>",
                mensaje: '<%=String.Format("¿Está seguro de eliminar el material {0} del listado?",  Me.WucDatosMaterial.RetornaDescripcion)%>',
                botones:
                    [
                        {
                            idControl: "btnSi",
                            textoBoton: "Sí",
                            onClick:
                                function (e) {
                                    $(this).attr("disabled", "disabled");
                                    __doPostBack(pvc_UniqueIdControl, '');
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

            $('#popupConfirmacionDeseaBorrar').popup(vlo_ConfiguracionPopup);
            window.location = "#popupConfirmacionDeseaBorrar";

            return false;
        };


        $(document).ready(function () {

            $('#<%=Me.btnRegresar%>').click(function () {
                regresarAlListado();
            });

            /*Control TabPanel*/
            configurarTabPanel();

            /*DatePicker con Fecha Mínima (hoy)*/

            configurarDatePicker('#<%=Me.txtDPFechaEfectuo.ClientID%>');
            configurarDatePicker('#<%=Me.txtdpFechaPropuestaInicio.ClientID%>');

            establecerFechaMaximaDatePicker('#<%=Me.txtDPFechaEfectuo.ClientID%>', new Date());
            establecerFechaMinimaDatePicker('#<%=Me.txtdpFechaPropuestaInicio.ClientID%>', new Date());


            $('#<%=Me.liRecursoHumano.ClientID%>').click(function () {
                ActivarRHEvaluacion();
            });



            $('#<%=Me.liMEEjecucion.ClientID%>').click(function () {
                ActivarMaterialEjecucion();
            });

            $('#<%=Me.liRHEjecucion.ClientID%>').click(function () {
                ActivarRHEjecucion();
            });

            $('#<%=Me.liTiempoEjecucion.ClientID%>').click(function () {
                ActivarTiempoEjecucion();
            });

            $('#<%=Me.liRecursosEvaluacion.clientid%>').click(function () {
                $('#<%=Me.tpEvaluacion.ClientID%>').addClass('activo');
                $('#<%=Me.tpEjecucion.ClientID%>').removeClass('activo');
                ActivarRHEvaluacion();
            });

            $('#<%=Me.liRecursosEjecucion.ClientID%>').click(function () {
                ActivarRHEjecucion();
            });

            cargarTootipDetalles();

            $('[data-tipo="borrarRegistroMaterial"]').click(function () { return mostrarPopupConfirmacionDeseaBorrarRegistro($(this).data("uniqueid")); });
            inicializarFormulario();

        });

        function ocultarFiltroMateriales() {
            window.location = '#CerrarPopUpBusquedaMateriales';
        };

        function mostrarPopUp(pvc_IdPopup) {
            window.location = pvc_IdPopup;
        };

        function cargarTootipDetalles() {

            $('[data-tipo="tooltipDetalleMaterial"]').each(function () {
                $('#' + this.id).tooltipster({
                    interactive: true,
                    interactiveTolerance: 400,
                    theme: 'tooltipster-light'
                });
            });

            $('[data-tipo="tooltipUnitario"]').each(function () {
                $('#' + this.id).tooltipster({
                    interactive: true,
                    interactiveTolerance: 400,
                    theme: 'tooltipster-light'
                });
            });
        };

        function inicializarFormulario() {

            $(window).keydown(function a(e) {

                if (e.keyCode == 13) {
                    return false;
                }
            });

            cargarLupa();

            permutarImagenes('#imgBuscarMaterial',
               '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Lupa.png")%>',
                '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Lupa.png")%>',
                '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Lupa.png")%>'
            );
        };

        function cargarLupa() {
            permutarImagenes('#imgBuscarMaterial',
                '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Lupa.png")%>',
                '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Lupa.png")%>',
                '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Lupa.png")%>'
            );
        };

        function ActivarRHEvaluacion() {
            $('#<%=Me.tpEvaluacion.ClientID%>').addClass('activo');
            $('#<%=Me.tpRecursoHumano.ClientID%>').addClass('activo');
        };


        function ActivarMaterialEjecucion() {
            $('#<%=Me.tpRHEjecucion.ClientID%>').removeClass('activo');
            $('#<%=Me.tpTiempoEjecucion.ClientID%>').removeClass('activo');
            $('#<%=Me.tpMEEjecucion.ClientID%>').addClass('activo');
        };

        function ActivarRHEjecucion() {
            $('#<%=Me.liRecursosEjecucion.ClientID%>').addClass('activo');
            $('#<%=Me.liRecursosEvaluacion.ClientID%>').removeClass('activo');
            $('#<%=Me.tpEvaluacion.ClientID%>').removeClass('activo');
            $('#<%=Me.tpEjecucion.ClientID%>').addClass('activo');
            $('#<%=Me.tpRHEjecucion.ClientID%>').addClass('activo');
            $('#<%=Me.tpMEEjecucion.ClientID%>').removeClass('activo');
            $('#<%=Me.tpTiempoEjecucion.ClientID%>').removeClass('activo');
        };

        function ActivarTiempoEjecucion() {
            $('#<%=Me.tpRHEjecucion.ClientID%>').removeClass('activo');
            $('#<%=Me.tpMEEjecucion.ClientID%>').removeClass('activo');
            $('#<%=Me.tpTiempoEjecucion.ClientID%>').addClass('activo');
        };

        function mostrarAlertSinCantidadDisponible() {
            mostrarAlerta(
                '#arAlertasDelFormulario',
                {
                    mensaje: "La bodega o almacén seleccionado no posee suficiente material",
                    tipo: "advertencia",
                    transparencia: 0.9,
                    posicion: 'center',
                    permiteCerrar: true
                }
                );
        };

        function mostrarAlertCantidadCero() {
            mostrarAlerta(
                '#arAlertasDelFormulario',
                {
                    mensaje: "La cantidad solicitada debe ser mayor a cero.",
                    tipo: "advertencia",
                    transparencia: 0.9,
                    posicion: 'center',
                    permiteCerrar: true
                }
                );
        };

        function mostrarAlertNoRequiereMaterial() {
            mostrarAlerta(
                '#arAlertasDelFormulario',
                {
                    mensaje: "En caso de existir materiales asociados serán eliminados al guardar o guardar y finalizar.",
                    tipo: "peligro",
                    transparencia: 0.9,
                    posicion: 'center',
                    permiteCerrar: true
                }
                );
        };

        function mostrarAlertaNoEncontrado() {
            mostrarAlerta(
                '#arAlertasDelFormulario',
                {
                    mensaje: "Ningúna bodega o almacén poseen el código indicado.",
                    tipo: "advertencia",
                    transparencia: 0.9,
                    posicion: 'center',
                    permiteCerrar: true
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

        function mostrarAlertaEvaluacionExitosa() {
            deshabilitarControl('#<%=btnGuardar.ClientID%>');
            //   deshabilitarControl('#btnLimpiarFormulario');
            deshabilitarControl('#<%=btnGuardarYFinalizar.ClientID%>');
            $('.formulario').attr('disabled', 'disabled');

            mostrarAlerta(
            '#arAlertasDelFormulario',
            {
                mensaje: 'Se ha guardado los datos indicados para la evaluación.',
                tipo: "exito",
                transparencia: 0.9,
                posicion: 'center',
                onClosed: function () { regresarAlListado(); }
            });
        };

        function regresarAlListado() {
            window.location = "Lst_OT_GestionOrdenTrabajoCordinadorSectorTaller.aspx";
        };

    </script>
</asp:Content>
