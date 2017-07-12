<%@ Page Language="VB" MaintainScrollPositionOnPostback="true"  AutoEventWireup="false" MasterPageFile="~/MasterPage/Mp_Formulario.master" CodeFile="Frm_OT_GestionContratacionProyecto.aspx.vb" Inherits="OrdenesDeTrabajo_Frm_OT_GestionContratacionProyecto" %>


<%@ Register TagPrefix="wuc" TagName="InformacionGeneral" Src="~/ControlesDeUsuario/Wuc_OT_InformacionGeneral.ascx" %>
<%@ Register TagPrefix="wuc" TagName="Aclaraciones" Src="~/Controles/wuc_OT_Aclaraciones.ascx" %>
<%@ Register TagName="Ofertas" TagPrefix="wuc" Src="~/Controles/wuc_OT_Ofertas.ascx" %>
<%@ Register Src="~/Controles/wuc_OT_ExpedienteTecContrataciones.ascx" TagName="wuc_ExpedienteTecnico" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register TagPrefix="wuc" Namespace="Utilerias.WebForms.Controles" Assembly="Utilerias.WebForms.Controles" %>



<asp:Content runat="server" ID="Content1" ContentPlaceHolderID="cphFormulario">

    <header>
        <h2>Gestión de la contratación del proyecto</h2>
    </header>

    <article class="tituloSeccion">
        Crear una nueva versión
    </article>
    <article class="formulario">
        <table>
            <tr class="areaBotones">
                <th>Version:</th>
                <td>
                    
                    <asp:DropDownList ID="ddlVersion" OnSelectedIndexChanged="ddlVersion_SelectedIndexChanged" Width="50%" runat="server" AutoPostBack="true" validationgroup="NuevaVersion"></asp:DropDownList>
                    <asp:Button runat="server" ID="btnNuevaVersion" ValidationGroup="NuevaVersion" Text="Crear Nueva Versión" />
                </td>
            </tr>
            <tr>
                <th>Monto del Proyecto</th>
                <td>
                    <asp:label runat="server" Width="50%" ID="lblMonto" ></asp:label>
                </td>
            </tr>
            <tr>
                <th>Via de contratación</th>
                <td>
                    <asp:DropDownList runat="server" Width="50%" ID="ddlViaContrato" ></asp:DropDownList>
                    <asp:RequiredFieldValidator runat="server" ID="rfvDdlViaContrato" ValidationGroup="NuevaVersion" ControlToValidate="ddlViaContrato" Display="Dynamic" ErrorMessage="Debe indicar la via de contratación">&nbsp;</asp:RequiredFieldValidator>                                
                </td>
            </tr>
            <tr>
                <th>Observaciones</th>
                <td>
                    <asp:TextBox TextMode="MultiLine" Rows="4" runat="server" ID="txtObservaciones" Width="80%"></asp:TextBox><br />
                    <span id="spContadorTxtObservaciones" class="contadorCaracteresRestantes"></span>
                </td>
            </tr>
        </table>
    </article>
    <article class="tituloSeccion">
        Información general de la orden de trabajo
    </article>
    <article>
        <wuc:InformacionGeneral ID="ctrl_InfoGeneral" runat="server" />
    </article>

    <article class="tituloSeccion">
        Etapas de Contratación
    </article>
    <article class="formulario">

        <b><asp:Label ID="lblEtapaActual" runat="server"></asp:Label></b>
        <br />

        <ul class="encabezadoTabPanel">
            <li id="liExpediente" visible="false"  runat="server"><a class="tituloTabPanel" style="background: linear-gradient(to top,#adb7bc,#FFFFFF);padding: 9px 3px;" href="#tpExpediente">Expediente Técnico</a></li>
            <li id="liInicio" visible="false" runat="server"><a class="tituloTabPanel" style="background: linear-gradient(to top,#adb7bc,#FFFFFF);padding: 9px 3px;" href="#tpInicio">Inicio</a></li>
            <li id="liPublicacionCartel" visible="false" runat="server"><a class="tituloTabPanel" style="background: linear-gradient(to top,#adb7bc,#FFFFFF);padding: 9px 3px;" href="#tpPublicacionCartel">Publicacion del Cartel</a></li>
            <li id="liAclaraciones" visible="false" runat="server"><a class="tituloTabPanel" style="background: linear-gradient(to top,#adb7bc,#FFFFFF);padding: 9px 3px;" href="#tpAclaraciones">Aclaraciones</a></li>
            <li id="liOfertas" visible="false" runat="server"><a class="tituloTabPanel" style="background: linear-gradient(to top,#adb7bc,#FFFFFF);padding: 9px 3px;" href="#tpOfertas">Ofertas</a></li>
            <li id="liRecomendaciónTécnica" visible="false" runat="server"><a class="tituloTabPanel" style="background: linear-gradient(to top,#adb7bc,#FFFFFF);padding: 9px 3px;" href="#tpRecomendaciónTécnica">Recomendación Técnica</a></li>
            <li id="liAdjudicación" visible="false" runat="server"><a class="tituloTabPanel" style="background: linear-gradient(to top,#adb7bc,#FFFFFF);padding: 9px 3px;" href="#tpAdjudicación">Adjudicación</a></li>
        </ul>

        <article class="cuerpoTabPanel"><article id="tpExpediente" runat="server" class="tabPanel">
                
            <article class="tituloSeccion">
                Etapas de orden de trabajo
            </article>
            <article class="formulario">
                <ul class="encabezadoTabPanel">
                    <asp:Repeater runat="server" ID="rpListaTapsTitulos">
                        <ItemTemplate>
                            <li runat="server" id="liEncabezado">
                                <a runat="server" class="tituloTabPanel" id="cuerpoTabPanel"><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_ETAPA_ORDEN_TRABAJO.DESCRIPCION)%></a>
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>

                <article class="cuerpoTabPanel">
                    <asp:Repeater runat="server" ID="rpListaTapsContenidos">
                        <ItemTemplate>
                            <article runat="server" class="tabPanel" id="cuerpoTabPanel">
                                <h3><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_ETAPA_ORDEN_TRABAJO.DESCRIPCION)%></h3>
                                <br />
                                <uc1:wuc_ExpedienteTecnico runat="server" ID="wucExpedienteTecnico" IdEtapaOrdenTrabajo='<%# Eval("ID_ETAPA_ORDEN_TRABAJO")%>' />
                            </article>
                        </ItemTemplate>
                    </asp:Repeater>
                </article>
            </article>
            
            <article class="formulario">
                <table>
                    <tr>
                        <th>Motivo de devolucion</th>
                        <td>
                            <asp:TextBox runat="server" ID="txtMotivo" Visible='<%# IIf(Me.Contratacion.Editable = Utilerias.OrdenesDeTrabajo.Version.EDITABLE AndAlso Me.EtapaActual = Utilerias.OrdenesDeTrabajo.EtapaContratacion.EXPEDIENTE_TECNICO, True, False)%>'
                                 textmode="MultiLine" Rows="4" Columns="40" ></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ID="rfvTxtMotivo" ControlToValidate="txtMotivo" ValidationGroup="Devolver" Display="Dynamic" ErrorMessage="Agregue un motivo para la devolución">&nbsp;</asp:RequiredFieldValidator> 
                        </td>
                    </tr>
                </table>
                <article class="areaBotones">
                    <asp:Button runat="server" Visible="false" OnClick="DevolverExpediente_Click" ID="DevolverExpediente" Text="Devolver Expediente" ValidationGroup="Devolver" />
                    <asp:Button runat="server" Visible="false" ID="btnCerrarEtapa" OnClick="btnCerrarEtapa_Click" Text="Cerrar Etapa" />
                    <input type="button" value="Regresar" data-tipo="volverAlListado" id="Button2" />
                </article>
            </article>                

        </article></article>
        <article class="cuerpoTabPanel"><article id="tpInicio" runat="server" class="tabPanel">

            <article>
                <table>
                    <tr>
                        <th>N° de solicitud (GECO)</th>
                        <td><asp:TextBox runat="server" ID="txtNSolicitud" Width="30%" ValidationGroup="GuardarInicio"></asp:TextBox></td>
                        <asp:RequiredFieldValidator runat="server" ID="rfvTxtSolicitud" ControlToValidate="txtNSolicitud" ValidationGroup="GuardarInicio" Display="Dynamic" ErrorMessage="Agregue el número de solicitud">&nbsp;</asp:RequiredFieldValidator>   
                        <ajax:FilteredTextBoxExtender ID="ftbTxtNSolicitud" runat="server" TargetControlID="txtNSolicitud" FilterMode="ValidChars" FilterType="Numbers,Custom" ValidChars="-"></ajax:FilteredTextBoxExtender>
                    </tr>
                    <tr>
                        <th>N° de decisión inicial (GECO)</th>
                        <td><asp:TextBox runat="server" ID="txtDescicionInicial" Width="30%" ValidationGroup="GuardarInicio"></asp:TextBox></td>
                        <asp:RequiredFieldValidator runat="server" ID="rfvTxtDescicionInicial" ControlToValidate="txtDescicionInicial" ValidationGroup="GuardarInicio" Display="Dynamic" ErrorMessage="Agregue el número de decisión inicial.">&nbsp;</asp:RequiredFieldValidator>   
                    </tr>
                    <tr>
                        <th>Documento</th>
                        <td>
                            <asp:FileUpload runat="server" ID="fuDocumento" onchange="validaArchivoInicio()" />
                            <asp:RequiredFieldValidator runat="server" ID="rfvFuDocumento" ControlToValidate="fuDocumento" ValidationGroup="AgregarDocumento" Display="Dynamic" ErrorMessage="Agregue un documento.">&nbsp;</asp:RequiredFieldValidator>                              
                            <asp:Button runat="server" ID="btnAgregarDocumento" ValidationGroup="AgregarDocumento" text="Agregar"/>
                            <img runat="server" ID="imgExtensionesInicio" data-tipo="tooltipExtension" class="tooltip"/>
                        </td>
                    </tr>
                </table>

            </article>

            <article class="listado sinBorde">
                <asp:Repeater runat="server" ID="rpAdjuntosInicio">
                    <HeaderTemplate>
                        <table>
                            <tr>
                                <th>Documento</th>
                                <th>&nbsp;</th>
                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr class="lineaDelListado">

                            <td>
                                <asp:LinkButton runat="server" ID="lnkArchivo"
                                    CommandArgument='<%#Container.ItemIndex%>'
                                    Style="text-decoration: underline; color: blue;"
                                    OnCommand="lnkArchivo_Command"
                                    Text="<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DOCUMENTO_CONTRATLST.NOMBRE_ARCHIVO)%>"></asp:LinkButton>
                            </td>
                            <td>
                                <asp:ImageButton runat="server" ID="ibBorrar" data-tipo="borrarRegistro" AlternateText="Borrar"
                                    CommandArgument='I' OnClick="ibBorrarAdjunto_Click"
                                    CommandName='<%#Container.ItemIndex%>' Visible='<%#IIf(Me.EtapaActual = Utilerias.OrdenesDeTrabajo.EtapaContratacion.INICIO, True, False)%>' />
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
            </article> 

            <article class="areaBotones">
                <asp:Button runat="server" Visible="false" ValidationGroup="GuardarInicio" ID="btnGuardarInicio" OnClick="btnGuardarInicio_Click" Text="Guardar y cerrar etapa" />
                <input type="button" value="Regresar" data-tipo="volverAlListado" id="Button3" />
            </article>
        </article></article>
        <article class="cuerpoTabPanel"><article id="tpPublicacionCartel" runat="server" class="tabPanel">

            <article>

                <table>
                    <tr>
                        <th>Documento</th>
                        <td>
                            <asp:FileUpload runat="server" ID="fuCartel" onchange="validaArchivoCartel();" />
                            <asp:Button runat="server" ID="btnAgregarCartel" ValidationGroup="AgregarDocumentoCartel" OnClick="btnAgregarCartel_Click" text="Agregar"/>
                            <asp:RequiredFieldValidator runat="server" ID="rfvBtnAgregarCartel" ControlToValidate="fuCartel" ValidationGroup="AgregarDocumentoCartel" Display="Dynamic" ErrorMessage="Agregue un documento.">&nbsp;</asp:RequiredFieldValidator>                              
                            <asp:LinkButton runat="server" ID="lnkArchivoCartel" Visible="false" OnCommand="lnkArchivoCartel_Command"></asp:LinkButton>&nbsp;&nbsp;                        
                            <asp:ImageButton ID="btnEliminarCartel" runat="server" ToolTip="Borrar" data-tipo="borrarRegistro" Visible="false"/>
                            <img runat="server" ID="imgExtensionesCartel" data-tipo="tooltipExtension" class="tooltip"/>
                        </td>
                    </tr>
                    <tr>
                        <th>N° de contrato</th>
                        <td>
                            <asp:TextBox runat="server" ID="txtNumContrato" Width="69%" ></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ID="rfvTxtNumContrato" ControlToValidate="txtNumContrato" ValidationGroup="GuardarCartel" Display="Dynamic" ErrorMessage="Agregue el número de contrato">&nbsp;</asp:RequiredFieldValidator>   
                            <span id="spContadorTxtNumContrato" class="contadorCaracteresRestantes"></span>
                        </td>
                    </tr>
                    <tr>
                        <th>Nombre del contrato</th>
                        <td>
                            <asp:TextBox TextMode="MultiLine" Rows="4" runat="server" ID="txtNombreContrato" Width="69%"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ID="rfvTxtNombreContrato" ControlToValidate="txtNombreContrato" ValidationGroup="GuardarCartel" Display="Dynamic" ErrorMessage="Agregue el nombre del contrato">&nbsp;</asp:RequiredFieldValidator>   
                            <span id="spContadorTxtNombreContrato" class="contadorCaracteresRestantes"></span>

                        </td>
                    </tr>
                </table>

            </article>

            <article class="areaBotones">
                <asp:Button runat="server" Visible="false" ValidationGroup="GuardarCartel" ID="btnGuardarCartel" OnClick="btnGuardarCartel_Click" Text="Guardar y cerrar etapa" />
                <input type="button" value="Regresar" data-tipo="volverAlListado" id="Button1" />
            </article>

        </article></article>
        <article class="cuerpoTabPanel"><article id="tpAclaraciones" runat="server" class="tabPanel">
            
            <wuc:Aclaraciones runat="server" id="ctrl_Aclaraciones" ></wuc:Aclaraciones>

        </article></article>
        <article class="cuerpoTabPanel"><article id="tpOfertas" runat="server" class="tabPanel">

            <wuc:Ofertas runat="server" ID="ctrl_Ofertas" />

        </article></article>
        <article class="cuerpoTabPanel"><article id="tpRecomendaciónTécnica" runat="server" class="tabPanel">
            <asp:CheckBox ID="chkContratacionInfructuosa" runat="server" Text="Contratación Infructuosa" />
            <article runat="server" id="arRecomendacion">
                <table>
                    <tr>
                        <th>Documento</th>
                        <td>
                            <asp:FileUpload runat="server" ID="fuRecomendacion" onchange="validaArchivoOficio();" />
                            <asp:RequiredFieldValidator runat="server" ID="rfvFuRecomendacion" ControlToValidate="fuRecomendacion" ValidationGroup="AgregarDocumentoRecomendacion" Display="Dynamic" ErrorMessage="Agregue un documento.">&nbsp;</asp:RequiredFieldValidator>                              
                            <asp:Button runat="server" ID="btnAgregarRecomendacion" ValidationGroup="AgregarDocumentoRecomendacion" Visible="false" OnClick="btnAgregarRecomendacion_Click" text="Agregar"/>
                            <asp:LinkButton runat="server" ID="lnkRecomendacion" Visible="false" OnCommand="lnkRecomendacion_Command"></asp:LinkButton>&nbsp;&nbsp;                        
                            <asp:ImageButton ID="btnEliminarRecomendacion" runat="server" ToolTip="Borrar" data-tipo="borrarRegistro" Visible="false"/>
                            <img runat="server" ID="imgExtensionesRecomendacion" data-tipo="tooltipExtension" class="tooltip"/>
                        </td>
                    </tr>
                </table>
                
            </article>

            <article class="areaBotones">
                <asp:Button runat="server" Visible="false" ValidationGroup="GuardarInicio" ID="btnCerrarRecomendacion" OnClick="btnCerrarRecomendacion_Click" Text="Guardar y cerrar etapa" />
                <input type="button" value="Regresar" data-tipo="volverAlListado" id="btnRegresar" />
            </article>

        </article></article>
        <article class="cuerpoTabPanel"><article id="tpAdjudicación" runat="server" class="tabPanel">

            <ul class="encabezadoTabPanel">
                <li id="liTabDocumentos" runat="server"><a class="tituloTabPanel" style="background: linear-gradient(to top,#adb7bc,#FFFFFF);padding: 10px 9px;" href="#tpDocumentos">Documentos</a></li>
                <li id="liTabLineas" runat="server" ><a class="tituloTabPanel" style="background: linear-gradient(to top,#adb7bc,#FFFFFF);padding: 10px 9px;" href="#tpLineas">Líneas</a></li>
            </ul>

            <article class="cuerpoTabPanel"><article id="tpDocumentos" runat="server" class="tabPanel">
            <article>
                <table>
                    <tr>
                        <th>Documento</th>
                        <td>
                            <asp:FileUpload runat="server" ID="fuDocumentosAdjudicacion" onchange="validaArchivoAdjudicacion()" />
                            <asp:RequiredFieldValidator runat="server" ID="rfvFuDocumentosAdjudicacion" ControlToValidate="fuDocumentosAdjudicacion" ValidationGroup="AgregarDocumentoAdjudicacion" Display="Dynamic" ErrorMessage="Agregue un documento.">&nbsp;</asp:RequiredFieldValidator>                              
                            <asp:Button runat="server" Enabled="false" ID="btnAgregardjudicacion" ValidationGroup="AgregarDocumentoAdjudicacion" text="Agregar"/>
                            <img runat="server" ID="imgExtensionesAdjudicacion" data-tipo="tooltipExtension" class="tooltip"/>
                        </td>
                    </tr>
                </table>
            </article> 

            <article class="listado sinBorde">
                <asp:Repeater runat="server" ID="rpDocumentosAdjudicacion">
                    <HeaderTemplate>
                        <table>
                            <tr>
                                <th>Documento</th>
                                <th>&nbsp;</th>
                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr class="lineaDelListado">

                            <td>
                                <asp:LinkButton runat="server" ID="lnkArchivo"
                                    CommandArgument='<%#Container.ItemIndex%>'
                                    Style="text-decoration: underline; color: blue;"
                                    OnCommand="lnkArchivoAdjudicacion_Command"
                                    Text="<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DOCUMENTO_CONTRATLST.NOMBRE_ARCHIVO)%>"></asp:LinkButton>
                            </td>
                            <td>
                                <asp:ImageButton runat="server" ID="ibBorrar" data-tipo="borrarRegistro" AlternateText="Borrar"
                                    CommandArgument='A' OnClick="ibBorrarAdjunto_Click"
                                    CommandName='<%#Container.ItemIndex%>' Visible='<%#IIf(Me.EtapaActual = Utilerias.OrdenesDeTrabajo.EtapaContratacion.ADJUDICACION, True, False)%>' />
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
            </article> 
            </article></article>

            <article class="cuerpoTabPanel"><article id="tpLineas" runat="server" class="tabPanel">
            <article>
                <table>
                    <tr>
                        <th>
                            N° de línea
                        </th>
                        <td>
                            <asp:TextBox runat="server" id="txtNumLinea"></asp:TextBox>
                            <asp:RequiredFieldValidator ValidationGroup="GuardarAdjudicacion" runat="server" id="rvfTxtNumLinea" ControlToValidate="txtNumLinea" display="Dynamic" ErrorMessage="El numero de línea es requerida.">&nbsp;</asp:RequiredFieldValidator>
                            <ajax:FilteredTextBoxExtender ID="ftbetxtNumLinea" runat="server" TargetControlID="txtNumLinea" FilterMode="ValidChars" ValidChars="" FilterType="Numbers"></ajax:FilteredTextBoxExtender>
                        </td>
                        <th>
                            Monto
                        </th>
                        <td>
                            <asp:TextBox runat="server" id="txtMonto"></asp:TextBox>
                            <asp:RequiredFieldValidator ValidationGroup="GuardarAdjudicacion" runat="server" id="rfvTxtMonto" ControlToValidate="txtMonto" display="Dynamic" ErrorMessage="El monto es requerido.">&nbsp;</asp:RequiredFieldValidator>
                            <ajax:FilteredTextBoxExtender ID="ftbeTxtMonto" runat="server" TargetControlID="txtMonto" FilterMode="ValidChars" ValidChars="" FilterType="Numbers"></ajax:FilteredTextBoxExtender>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            Adjudicatario
                        </th>
                        <td colspan="3">
                            <asp:TextBox width="100%" runat="server" id="txtAdjudicatario"></asp:TextBox>
                            <asp:RequiredFieldValidator ValidationGroup="GuardarAdjudicacion" runat="server" id="rfvTxtAdjudicatario" ControlToValidate="txtAdjudicatario" display="Dynamic" ErrorMessage="El adjudicatario es requerido.">&nbsp;</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <th>Doc. inicio de obra</th>
                        <td colspan="3">
                            <asp:FileUpload runat="server" ID="fuDocAdjudicacion" onchange="validaArchivoOficioInicio();" />
                            <asp:RequiredFieldValidator runat="server" ID="rfvFuDocAdjudicacion" ControlToValidate="fuDocAdjudicacion" ValidationGroup="AgregarDocInicio" Display="Dynamic" ErrorMessage="Agregue un documento.">&nbsp;</asp:RequiredFieldValidator>                              
                            <asp:Button runat="server" OnClick="btnAgregarDocAdjudicacion_Click" Enabled="false" ID="btnAgregarDocAdjudicacion" ValidationGroup="AgregarDocInicio" text="Agregar"/>
                            <asp:LinkButton runat="server" ID="lnkAdjudicacion" Visible="false" OnCommand="lnkAdjudicacion_Command"></asp:LinkButton>&nbsp;&nbsp;           
                            <asp:ImageButton ID="btnEliminarArchivoLinea" runat="server" ToolTip="Borrar" data-tipo="borrarRegistro" Visible="false"/>             
                            <img runat="server" ID="imgExtensionesInicioObra" data-tipo="tooltipExtension" class="tooltip"/>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            Fecha de inicio
                        </th>
                        <td colspan="2" style="padding: 0px;">
                            <table>
                                <tr>
                                    <td style="width: 13%;">
                                        <asp:TextBox width="204%" runat="server" ID="txtFechaInicio" data-tipo="calcularFecha" ValidationGroup="GuardarAdjudicacion" AutoPostBack="true"></asp:TextBox>
                                        <asp:RequiredFieldValidator ValidationGroup="GuardarAdjudicacion" runat="server" id="RequiredFieldValidator1" ControlToValidate="txtFechaInicio" display="Dynamic" ErrorMessage="Fecha de inicio es requerida.">&nbsp;</asp:RequiredFieldValidator>
                                    </td>
                                    <th>
                                        Plazo
                                    </th>
                                    <td style="width: 13%;">
                                        <asp:TextBox width="204%" runat="server" id="txtPlazo" data-tipo="calcularFecha"></asp:TextBox>
                                        <asp:RequiredFieldValidator ValidationGroup="GuardarAdjudicacion" runat="server" id="rfvTxtPlazo" ControlToValidate="txtPlazo" display="Dynamic" ErrorMessage="El plazo es requerido.">&nbsp;</asp:RequiredFieldValidator>
                                        <ajax:FilteredTextBoxExtender ID="ftbxTxtPlazo" runat="server" TargetControlID="txtMonto" FilterMode="ValidChars" ValidChars="" FilterType="Numbers"></ajax:FilteredTextBoxExtender>
                                    </td>
                                    <th>
                                        días
                                    </th>
                                    <td style="width: 13%;">
                                        <asp:DropDownList runat="server" ID="ddlDias" AutoPostBack="true" OnSelectedIndexChanged="FinFechaEstimada" ></asp:DropDownList>
                                        <asp:RequiredFieldValidator ValidationGroup="GuardarAdjudicacion" runat="server" id="rfvDdlDias" ControlToValidate="ddlDias" display="Dynamic" ErrorMessage="Los días son requeridos">&nbsp;</asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        
                    </tr>
                    <tr>
                        <th>Fecha de fin estimada</th>
                        <td><asp:label ID="lblFinEstimada" runat="server" ></asp:label></td>
                    </tr>
                </table>
            </article> 
            <article class="areaBotones">
                <asp:Button runat="server" Visible="false" ValidationGroup="GuardarAdjudicacion" ID="btnAgregarLinea" OnClick="btnAgregarLinea_Click" Text="Agregar línea" />
                <asp:Button runat="server" Visible="false" ID="btnCancelar" OnClick="btnCancelar_Click" Text="Cancelar" />
                <asp:Button runat="server" Visible="false" ValidationGroup="GuardarAdjudicacion" ID="btnModificarLinea" OnClick="btnModificarLinea_Click" Text="Modificar línea" />
            </article>


            <article class="listado sinBorde">
                <asp:Repeater runat="server" ID="rpLineas">
                    <HeaderTemplate>
                        <table style="width: 80%;float: right;" >
                            <tr>
                                <th>N° Linea</th>
                                <th>Adjudicatario</th>
                                <th>Monto</th>
                                <th style="width: 5%;">&nbsp;</th>
                                <th style="width: 5%;">&nbsp;</th>
                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr class="lineaDelListado">

                            <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_LINEA_ADJUDICACIONLST.NUMERO_LINEA)%></td>
                            <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_LINEA_ADJUDICACIONLST.ADJUDICATARIO)%></td>
                            <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_LINEA_ADJUDICACIONLST.MONTO_ADJUDICADO)%></td>
                            <td>
                                <asp:ImageButton runat="server" ID="ibConsultar" AlternateText="Consultar Linea"
                                    OnClick="ibConsultar_Click" data-tipo="consultarRegistro"
                                    CommandName='<%#Container.ItemIndex%>'/>
                            </td>
                            <td>
                                <asp:ImageButton runat="server" ID="ibBorrar" data-tipo="borrarLinea" AlternateText="Borrar"
                                    CommandArgument='A' OnClick="ibBorrarLinea_Click"
                                    CommandName='<%#Container.ItemIndex%>' Visible='<%#IIf(Me.EtapaActual = Utilerias.OrdenesDeTrabajo.EtapaContratacion.ADJUDICACION, True, False)%>' />
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
            </article> 

            </article></article>

            <article class="areaBotones">
                <asp:Button runat="server" Visible="false" ID="btnCerrarAdjudicacion" OnClick="btnCerrarAdjudicacion_Click" Text="Guardar y cerrar etapa" />
                <input type="button" value="Regresar" data-tipo="volverAlListado" id="Button6" />
            </article>
        </article></article>
    </article>
    
    <article style='display: <%=IIf(Me.OrdenTrabajo.EstadoOrdenTrabajo = Utilerias.OrdenesDeTrabajo.EstadoOrden.PENDIENTE_REVISION_CONTRATACIONES, "block", "none")%>' class="areaBotones">
        <input type="button" value="Regresar" data-tipo="volverAlListado" id="Button5" />
    </article>

    <%--Popup para generar la nueva versión--%>
    <article id="PopUpNuevaVersion" class="ventanaEmergente" >
        <article class="formulario" style="width: 192px!important;">
            <a href="#CerrarPopUpBusquedaFuncionario" title="Cerrar Ventana" class="botonCerrarVentanaEmergente">X</a>
            <br />
            La nueva versión deberá continuar a partir de la siguiente etapa:
            <asp:DropDownList runat="server" ID="ddlEtapaVersion" AutoPostBack="false" ></asp:DropDownList>
            <div class="areaBotones">
                <asp:Button runat="server" ID="btnAceptarVersion" text="Aceptar" />
                <a href="#CerrarPopUpBusquedaFuncionario" title="Regresar">Regresar</a>
            </div>
        </article>
    </article>
    <%--Popup para generar la nueva versión--%>

    <article id="arAlerta"></article>
    <article id="arAlertasDelFormulario"></article>
    <article id="arpopupConfirmaDeseaContinuar"></article>
    <article id="arpopupConfirmaDeseaBorrar"></article>
    <article id="popupConfirmacionDeseaBorrar"></article>


    <script type="text/javascript">
        $(document).ready(function () {
            /*Control TabPanel*/
            configurarTabPanel();

            
            $('#<%=Me.txtFechaInicio.ClientID%>').datepicker({
                changeMonth: true,
                changeYear: true,
                showOtherMonths: true,
                selectOtherMonths: true,
                dateFormat: 'dd/mm/yy'
            });


            $('#<%=btnNuevaVersion.ClientID%>').click(function () { return confirmacionNuevaVersion(); });

            $('[data-tipo="calcularFecha"]').on({'change': function() {__doPostBack('<%=Me.txtPlazo.UniqueID%>', '');} });

            $('[data-tipo="tooltipExtension"]').attr('src', '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Icono_Ayuda.png")%>');
            
            $('[data-tipo="borrarRegistro"]').click(function () { return mostrarPopupConfirmacionDeseaBorrarRegistro($(this).data("uniqueid")); }); //busca cualquier control con ese data tipo

            $('[data-tipo="borrarLinea"]').click(function () { return mostrarPopupConfirmacionDeseaBorrarLinea($(this).data("uniqueid")); }); //busca cualquier control con ese data tipo
            $('[data-tipo="borrarLinea"]').attr('src', '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Borrar.png")%>');
            $('[data-tipo="borrarLinea"]').on({
                'mouseover': function () { $(this).attr('src', '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Icono_Borrar.png")%>'); },
                'mouseout': function () { $(this).attr('src', '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Borrar.png")%>'); }
            });  

            $('[data-tipo="borrarRegistro"]').attr('src', '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Borrar.png")%>');
            $('[data-tipo="borrarRegistro"]').on({
                'mouseover': function () { $(this).attr('src', '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Icono_Borrar.png")%>'); },
                'mouseout': function () { $(this).attr('src', '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Borrar.png")%>'); }
            });    
            
            $('[data-tipo="consultarRegistro"]').attr('src', '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Editar.png")%>');
            $('[data-tipo="consultarRegistro"]').on({
                'mouseover': function () { $(this).attr('src', '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Icono_Editar.png")%>'); },
                'mouseout': function () { $(this).attr('src', '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Editar.png")%>'); }
            }); 

            $('[data-tipo="volverAlListado"]').click(function () {
                regresarAlListado();
            });

            configurarLongitudMaximaTexto('#<%=Me.txtNSolicitud.ClientID%>','<%=Utilerias.OrdenesDeTrabajo.Modelo.OTT_CONTRATACION.NUMERO_SOLICITUD_BD_TAMANO%>');
            configurarLongitudMaximaTexto('#<%=Me.txtDescicionInicial.ClientID%>','<%=Utilerias.OrdenesDeTrabajo.Modelo.OTT_CONTRATACION.NUMERO_DECISION_INICIAL_BD_TAMANO%>');
            configurarLongitudMaximaTexto('#<%=Me.txtNumContrato.ClientID%>','<%=Utilerias.OrdenesDeTrabajo.Modelo.OTT_CONTRATACION.NUMERO_CONTRATO_BD_TAMANO%>');
            configurarLongitudMaximaTexto('#<%=Me.txtNombreContrato.ClientID%>','<%=Utilerias.OrdenesDeTrabajo.Modelo.OTT_CONTRATACION.NOMBRE_CONTRATO_BD_TAMANO %>');
            configurarLongitudMaximaTexto('#<%=Me.txtNumLinea.ClientID%>', '<%=Utilerias.OrdenesDeTrabajo.Modelo.OTT_LINEA_ADJUDICACION.NUMERO_LINEA_BD_TAMANO%>');
            configurarLongitudMaximaTexto('#<%=Me.txtMonto.ClientID%>', '<%=Utilerias.OrdenesDeTrabajo.Modelo.OTT_LINEA_ADJUDICACION.MONTO_ADJUDICADO_BD_TAMANO%>');
            configurarLongitudMaximaTexto('#<%=Me.txtPlazo.ClientID%>', '<%=Utilerias.OrdenesDeTrabajo.Modelo.OTT_LINEA_ADJUDICACION.PLAZO_EN_DIAS_BD_TAMANO%>');
            configurarLongitudMaximaTexto('#<%=Me.txtObservaciones.ClientID%>', '<%=Utilerias.OrdenesDeTrabajo.Modelo.OTT_CONTRATACION.OBSERVACIONES_BD_TAMANO%>');

            configurarContadorCaracteresRestantes('#<%=Me.txtNumContrato.ClientID%>','#spContadorTxtNumContrato');
            configurarContadorCaracteresRestantes('#<%=Me.txtObservaciones.ClientID%>','#spContadorTxtObservaciones');
            configurarContadorCaracteresRestantes('#<%=Me.txtNombreContrato.ClientID%>','#spContadorTxtNombreContrato');


            configurarSpinnerNumericoRango('#<%= Me.txtNumLinea.ClientID%>', 1, 1, 99, true);

            
            
            $('#<%=Me.liExpediente.ClientID%>').click(function () {
                $('#<%=Me.tpExpediente.ClientID%>').addClass('activo');
                $('#<%=Me.tpInicio.ClientID%>').removeClass('activo');
                $('#<%=Me.tpPublicacionCartel.ClientID%>').removeClass('activo');
                $('#<%=Me.tpAclaraciones.ClientID%>').removeClass('activo');
                $('#<%=Me.tpOfertas.ClientID%>').removeClass('activo');
                $('#<%=Me.tpRecomendaciónTécnica.ClientID%>').removeClass('activo');
                $('#<%=Me.tpAdjudicación.ClientID%>').removeClass('activo');
            });

            $('#<%=Me.liInicio.ClientID%>').click(function () {
                activarInicio();
            });

            $('#<%=Me.liPublicacionCartel.ClientID%>').click(function () {
                activarCartel();
            });


            $('#<%=Me.liAclaraciones.ClientID%>').click(function () {

                activarAclaraciones();
            });

            $('#<%=Me.liOfertas.ClientID%>').click(function () {

                activarOfertas();

            });

            $('#<%=Me.liRecomendaciónTécnica.ClientID%>').click(function () {

                activarRecTecnica();

            });

            $('#<%=Me.liAdjudicación.ClientID%>').click(function () {

                activarAdjudicacion();

            });

            $('#<%=Me.liTabDocumentos.ClientID%>').click(function () {

                activarAdjudicacionDocumentos();

            });

            $('#<%=Me.liTabLineas.ClientID%>').click(function () {

                activarAdjudicacionLineas();

            });


        });

        function ocultarFiltroFuncionario() {
            window.location = '#CerrarPopUpBusquedaFuncionario';
        };

        function etapaInvalida(){
            alert("Debe seleccionar una etapa de contratación");
            return false;
        };

        function activarAdjudicacionDocumentos(){
            $('#<%=Me.liExpediente.ClientID%>').removeClass('activo');
            $('#<%=Me.liInicio.ClientID%>').removeClass('activo');
            $('#<%=Me.liPublicacionCartel.ClientID%>').removeClass('activo');
            $('#<%=Me.liAclaraciones.ClientID%>').removeClass('activo');
            $('#<%=Me.liOfertas.ClientID%>').removeClass('activo');
            $('#<%=Me.liRecomendaciónTécnica.ClientID%>').removeClass('activo');
            $('#<%=Me.liAdjudicación.ClientID%>').attr('style','font-weight: bold');
            $('#<%=Me.liTabDocumentos.ClientID%>').attr('style','font-weight: bold');
            $('#<%=Me.liAdjudicación.ClientID%>').addClass('activo');
            $('#<%=Me.tpExpediente.ClientID%>').removeClass('activo');
            $('#<%=Me.tpInicio.ClientID%>').removeClass('activo');
            $('#<%=Me.tpPublicacionCartel.ClientID%>').removeClass('activo');
            $('#<%=Me.tpAclaraciones.ClientID%>').removeClass('activo');
            $('#<%=Me.tpOfertas.ClientID%>').removeClass('activo');
            $('#<%=Me.tpRecomendaciónTécnica.ClientID%>').removeClass('activo');
            $('#<%=Me.tpAdjudicación.ClientID%>').addClass('activo');

            $('#<%=Me.liTabDocumentos.ClientID%>').addClass('activo');
            $('#<%=Me.liTabLineas.ClientID%>').removeClass('activo');

            $('#<%=Me.tpDocumentos.ClientID%>').addClass('activo');

            $('#<%=Me.tpLineas.ClientID%>').removeClass('activo');
        };

        function activarAdjudicacionLineas(){
            $('#<%=Me.liExpediente.ClientID%>').removeClass('activo');
            $('#<%=Me.liInicio.ClientID%>').removeClass('activo');
            $('#<%=Me.liPublicacionCartel.ClientID%>').removeClass('activo');
            $('#<%=Me.liAclaraciones.ClientID%>').removeClass('activo');
            $('#<%=Me.liOfertas.ClientID%>').removeClass('activo');
            $('#<%=Me.liRecomendaciónTécnica.ClientID%>').removeClass('activo');
            $('#<%=Me.liAdjudicación.ClientID%>').attr('style','font-weight: bold');
            $('#<%=Me.liTabLineas.ClientID%>').attr('style','font-weight: bold');
            $('#<%=Me.liAdjudicación.ClientID%>').addClass('activo');
            $('#<%=Me.tpExpediente.ClientID%>').removeClass('activo');
            $('#<%=Me.tpInicio.ClientID%>').removeClass('activo');
            $('#<%=Me.tpPublicacionCartel.ClientID%>').removeClass('activo');
            $('#<%=Me.tpAclaraciones.ClientID%>').removeClass('activo');
            $('#<%=Me.tpOfertas.ClientID%>').removeClass('activo');
            $('#<%=Me.tpRecomendaciónTécnica.ClientID%>').removeClass('activo');
            $('#<%=Me.tpAdjudicación.ClientID%>').addClass('activo');

            $('#<%=Me.liTabDocumentos.ClientID%>').removeClass('activo');
            $('#<%=Me.liTabLineas.ClientID%>').addClass('activo');

            $('#<%=Me.tpDocumentos.ClientID%>').removeClass('activo');

            $('#<%=Me.tpLineas.ClientID%>').addClass('activo');
        };

        function activarInicio(){
            $('#<%=Me.liExpediente.ClientID%>').removeClass('activo');
            $('#<%=Me.liInicio.ClientID%>').addClass('activo');
            $('#<%=Me.liInicio.ClientID%>').attr('style','font-weight: bold');
            $('#<%=Me.liPublicacionCartel.ClientID%>').removeClass('activo');
            $('#<%=Me.liAclaraciones.ClientID%>').removeClass('activo');
            $('#<%=Me.liOfertas.ClientID%>').removeClass('activo');
            $('#<%=Me.liRecomendaciónTécnica.ClientID%>').removeClass('activo');
            $('#<%=Me.liAdjudicación.ClientID%>').removeClass('activo');
            $('#<%=Me.tpExpediente.ClientID%>').removeClass('activo');
            $('#<%=Me.tpInicio.ClientID%>').addClass('activo');
            $('#<%=Me.tpPublicacionCartel.ClientID%>').removeClass('activo');
            $('#<%=Me.tpAclaraciones.ClientID%>').removeClass('activo');
            $('#<%=Me.tpOfertas.ClientID%>').removeClass('activo');
            $('#<%=Me.tpRecomendaciónTécnica.ClientID%>').removeClass('activo');
            $('#<%=Me.tpAdjudicación.ClientID%>').removeClass('activo');
        };

        function activarCartel(){
            $('#<%=Me.liExpediente.ClientID%>').removeClass('activo');
            $('#<%=Me.liInicio.ClientID%>').removeClass('activo');
            $('#<%=Me.liPublicacionCartel.ClientID%>').addClass('activo');
            $('#<%=Me.liPublicacionCartel.ClientID%>').attr('style','font-weight: bold');
            $('#<%=Me.liAclaraciones.ClientID%>').removeClass('activo');
            $('#<%=Me.liOfertas.ClientID%>').removeClass('activo');
            $('#<%=Me.liRecomendaciónTécnica.ClientID%>').removeClass('activo');
            $('#<%=Me.liAdjudicación.ClientID%>').removeClass('activo');
            $('#<%=Me.tpExpediente.ClientID%>').removeClass('activo');
            $('#<%=Me.tpInicio.ClientID%>').removeClass('activo');
            $('#<%=Me.tpPublicacionCartel.ClientID%>').addClass('activo');
            $('#<%=Me.tpAclaraciones.ClientID%>').removeClass('activo');
            $('#<%=Me.tpOfertas.ClientID%>').removeClass('activo');
            $('#<%=Me.tpRecomendaciónTécnica.ClientID%>').removeClass('activo');
            $('#<%=Me.tpAdjudicación.ClientID%>').removeClass('activo');
        };

        function activarAclaraciones(){
            $('#<%=Me.liExpediente.ClientID%>').removeClass('activo');
            $('#<%=Me.liInicio.ClientID%>').removeClass('activo');
            $('#<%=Me.liPublicacionCartel.ClientID%>').removeClass('activo');
            $('#<%=Me.liAclaraciones.ClientID%>').attr('style','font-weight: bold');
            $('#<%=Me.liAclaraciones.ClientID%>').addClass('activo');
            $('#<%=Me.liOfertas.ClientID%>').removeClass('activo');
            $('#<%=Me.liRecomendaciónTécnica.ClientID%>').removeClass('activo');
            $('#<%=Me.liAdjudicación.ClientID%>').removeClass('activo');
            $('#<%=Me.tpExpediente.ClientID%>').removeClass('activo');
            $('#<%=Me.tpInicio.ClientID%>').removeClass('activo');
            $('#<%=Me.tpPublicacionCartel.ClientID%>').removeClass('activo');
            $('#<%=Me.tpAclaraciones.ClientID%>').addClass('activo');
            $('#<%=Me.tpOfertas.ClientID%>').removeClass('activo');
            $('#<%=Me.tpRecomendaciónTécnica.ClientID%>').removeClass('activo');
            $('#<%=Me.tpAdjudicación.ClientID%>').removeClass('activo');

        };

        function activarOfertas(){
            $('#<%=Me.liExpediente.ClientID%>').removeClass('activo');
            $('#<%=Me.liInicio.ClientID%>').removeClass('activo');
            $('#<%=Me.liPublicacionCartel.ClientID%>').removeClass('activo');
            $('#<%=Me.liAclaraciones.ClientID%>').removeClass('activo');
            $('#<%=Me.liOfertas.ClientID%>').addClass('activo');
            $('#<%=Me.liOfertas.ClientID%>').attr('style','font-weight: bold');
            $('#<%=Me.liRecomendaciónTécnica.ClientID%>').removeClass('activo');
            $('#<%=Me.liAdjudicación.ClientID%>').removeClass('activo');
            $('#<%=Me.tpExpediente.ClientID%>').removeClass('activo');
            $('#<%=Me.tpInicio.ClientID%>').removeClass('activo');
            $('#<%=Me.tpPublicacionCartel.ClientID%>').removeClass('activo');
            $('#<%=Me.tpAclaraciones.ClientID%>').removeClass('activo');
            $('#<%=Me.tpOfertas.ClientID%>').addClass('activo');
            $('#<%=Me.tpRecomendaciónTécnica.ClientID%>').removeClass('activo');
            $('#<%=Me.tpAdjudicación.ClientID%>').removeClass('activo');
        };

        function activarRecTecnica(){
            $('#<%=Me.liExpediente.ClientID%>').removeClass('activo');
            $('#<%=Me.liInicio.ClientID%>').removeClass('activo');
            $('#<%=Me.liPublicacionCartel.ClientID%>').removeClass('activo');
            $('#<%=Me.liAclaraciones.ClientID%>').removeClass('activo');
            $('#<%=Me.liOfertas.ClientID%>').removeClass('activo');
            $('#<%=Me.liRecomendaciónTécnica.ClientID%>').addClass('activo');
            $('#<%=Me.liRecomendaciónTécnica.ClientID%>').attr('style','font-weight: bold');
            $('#<%=Me.liAdjudicación.ClientID%>').removeClass('activo');
            $('#<%=Me.tpExpediente.ClientID%>').removeClass('activo');
            $('#<%=Me.tpInicio.ClientID%>').removeClass('activo');
            $('#<%=Me.tpPublicacionCartel.ClientID%>').removeClass('activo');
            $('#<%=Me.tpAclaraciones.ClientID%>').removeClass('activo');
            $('#<%=Me.tpOfertas.ClientID%>').removeClass('activo');
            $('#<%=Me.tpRecomendaciónTécnica.ClientID%>').addClass('activo');
            $('#<%=Me.tpAdjudicación.ClientID%>').removeClass('activo');
        };

        function activarAdjudicacion(){
            $('#<%=Me.liExpediente.ClientID%>').removeClass('activo');
            $('#<%=Me.liInicio.ClientID%>').removeClass('activo');
            $('#<%=Me.liPublicacionCartel.ClientID%>').removeClass('activo');
            $('#<%=Me.liAclaraciones.ClientID%>').removeClass('activo');
            $('#<%=Me.liOfertas.ClientID%>').removeClass('activo');
            $('#<%=Me.liRecomendaciónTécnica.ClientID%>').removeClass('activo');
            $('#<%=Me.liAdjudicación.ClientID%>').attr('style','font-weight: bold');
            $('#<%=Me.liAdjudicación.ClientID%>').addClass('activo');
            $('#<%=Me.tpExpediente.ClientID%>').removeClass('activo');
            $('#<%=Me.tpInicio.ClientID%>').removeClass('activo');
            $('#<%=Me.tpPublicacionCartel.ClientID%>').removeClass('activo');
            $('#<%=Me.tpAclaraciones.ClientID%>').removeClass('activo');
            $('#<%=Me.tpOfertas.ClientID%>').removeClass('activo');
            $('#<%=Me.tpRecomendaciónTécnica.ClientID%>').removeClass('activo');
            $('#<%=Me.tpAdjudicación.ClientID%>').addClass('activo');
        };

        function mostrarPopupConfirmacionDeseaBorrarRegistro(pvc_UniqueIdControl) {
            var vlo_ConfiguracionPopup = {
                titulo: "<em>Gestion de Contratación</em>",
                mensaje: "¿Realmente desea borrar el archivo seleccionado?",
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

        function mostrarPopupConfirmacionDeseaBorrarLinea(pvc_UniqueIdControl) {
            var vlo_ConfiguracionPopup = {
                titulo: "<em>Gestion de Contratación</em>",
                mensaje: "¿Realmente desea borrar la linea de contratación seleccionada?",
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

       
        function validaArchivoInicio() {
            var vlo_InputArchivo = document.getElementById('<%=fuDocumento.ClientID%>');
            var vlc_NombreArchivo = vlo_InputArchivo.value;
            var vlc_NombreCorto = " ";
            var vlc_Limiter = '\\';
            var vlc_Extension = '';
            var vln_Indice = 0;
            var vln_IndiceExtension = 0;
            var vlc_LimiterExtension = '.';
            var vln_limiteTamArchivo = <%=Me.TamanoArchivoInicio%>;
            var vln_TamArchivo;
            var vln_TamBytes = vln_limiteTamArchivo * <%=Utilerias.OrdenesDeTrabajo.Constantes.TAMANNO_BYTES_A_MEGAS%>;
            var vlc_Llaves;
           
            var vln_TamanoNombre = <%=Utilerias.OrdenesDeTrabajo.Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.NOMBRE_ARCHIVO_BD_TAMANO%>;
          
            if(vlo_InputArchivo.value != ''){
                
                vln_Indice =  vlc_NombreArchivo.lastIndexOf(vlc_Limiter) + 1;
                vlc_NombreCorto = vlc_NombreArchivo.substr(vln_Indice, vlc_NombreArchivo.length - vln_Indice);
                vln_IndiceExtension =  vlc_NombreArchivo.lastIndexOf(vlc_LimiterExtension) + 1;
                vlc_Extension = vlc_NombreArchivo.substr(vln_IndiceExtension, vlc_NombreArchivo.length);    
                
                vlc_ExtencionesPerimtidas = '<%=Me.ExtensionesPermitidasInicio%>';
                vlc_Llaves = vlc_ExtencionesPerimtidas.split(",");
            
                if(vlc_Llaves.indexOf(vlc_Extension.toUpperCase()) == -1){
                    mostrarAlertaError('No es una extensión permitida','');
                    vlo_InputArchivo.value = "";
                    return false;
                }
                
                if (vlc_NombreCorto.length > vln_TamanoNombre){
                    mostrarAlertaError('El nombre del archivo es demasiado largo','');
                    vlo_InputArchivo.value = "";
                    return false;
                }
                
                vln_TamArchivo = vlo_InputArchivo.files[0].size;
                if (vln_TamArchivo > vln_TamBytes){
                    mostrarAlertaError('El tamaño del archivo excede el máximo permitido.','');
                    vlo_InputArchivo.value = "";
                    return false;
                }

                return true;
            }
            return false;
        };

        function validaArchivoCartel(){
            var vlo_InputArchivo = document.getElementById('<%=fuCartel.ClientID%>');
            var vlc_NombreArchivo = vlo_InputArchivo.value;
            var vlc_NombreCorto = " ";
            var vlc_Limiter = '\\';
            var vlc_Extension = '';
            var vln_Indice = 0;
            var vln_IndiceExtension = 0;
            var vlc_LimiterExtension = '.';
            var vln_limiteTamArchivo = <%=Me.TamanoArchivoCartel%>;
            var vln_TamArchivo;
            var vln_TamBytes = vln_limiteTamArchivo * <%=Utilerias.OrdenesDeTrabajo.Constantes.TAMANNO_BYTES_A_MEGAS%>;
            var vlc_Llaves;
           
            var vln_TamanoNombre = <%=Utilerias.OrdenesDeTrabajo.Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.NOMBRE_ARCHIVO_BD_TAMANO%>;
          
            if(vlo_InputArchivo.value != ''){
                
                vln_Indice =  vlc_NombreArchivo.lastIndexOf(vlc_Limiter) + 1;
                vlc_NombreCorto = vlc_NombreArchivo.substr(vln_Indice, vlc_NombreArchivo.length - vln_Indice);
                vln_IndiceExtension =  vlc_NombreArchivo.lastIndexOf(vlc_LimiterExtension) + 1;
                vlc_Extension = vlc_NombreArchivo.substr(vln_IndiceExtension, vlc_NombreArchivo.length);    
                
                vlc_ExtencionesPerimtidas = '<%=Me.ExtensionesPermitidasCartel%>';
                vlc_Llaves = vlc_ExtencionesPerimtidas.split(",");
            
                if(vlc_Llaves.indexOf(vlc_Extension.toUpperCase()) == -1){
                    mostrarAlertaError('No es una extensión permitida','');
                    vlo_InputArchivo.value = "";
                    return false;
                }
                
                if (vlc_NombreCorto.length > vln_TamanoNombre){
                    mostrarAlertaError('El nombre del archivo es demasiado largo','');
                    vlo_InputArchivo.value = "";
                    return false;
                }
                
                vln_TamArchivo = vlo_InputArchivo.files[0].size;
                if (vln_TamArchivo > vln_TamBytes){
                    mostrarAlertaError('El tamaño del archivo excede el máximo permitido.','');
                    vlo_InputArchivo.value = "";
                    return false;
                }

                return true;
            }
            return false;
        };


        function validaArchivoOficio(){
            var vlo_InputArchivo = document.getElementById('<%=fuRecomendacion.ClientID%>');
            var vlc_NombreArchivo = vlo_InputArchivo.value;
            var vlc_NombreCorto = " ";
            var vlc_Limiter = '\\';
            var vlc_Extension = '';
            var vln_Indice = 0;
            var vln_IndiceExtension = 0;
            var vlc_LimiterExtension = '.';
            var vln_limiteTamArchivo = <%=Me.TamanoArchivoOficio%>;
            var vln_TamArchivo;
            var vln_TamBytes = vln_limiteTamArchivo * <%=Utilerias.OrdenesDeTrabajo.Constantes.TAMANNO_BYTES_A_MEGAS%>;
            var vlc_Llaves;
           
            var vln_TamanoNombre = <%=Utilerias.OrdenesDeTrabajo.Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.NOMBRE_ARCHIVO_BD_TAMANO%>;
          
            if(vlo_InputArchivo.value != ''){
                
                vln_Indice =  vlc_NombreArchivo.lastIndexOf(vlc_Limiter) + 1;
                vlc_NombreCorto = vlc_NombreArchivo.substr(vln_Indice, vlc_NombreArchivo.length - vln_Indice);
                vln_IndiceExtension =  vlc_NombreArchivo.lastIndexOf(vlc_LimiterExtension) + 1;
                vlc_Extension = vlc_NombreArchivo.substr(vln_IndiceExtension, vlc_NombreArchivo.length);    
                
                vlc_ExtencionesPerimtidas = '<%=Me.ExtensionesPermitidasOficio%>';
                vlc_Llaves = vlc_ExtencionesPerimtidas.split(",");
            
                if(vlc_Llaves.indexOf(vlc_Extension.toUpperCase()) == -1){
                    mostrarAlertaError('No es una extensión permitida','');
                    vlo_InputArchivo.value = "";
                    return false;
                }
                
                if (vlc_NombreCorto.length > vln_TamanoNombre){
                    mostrarAlertaError('El nombre del archivo es demasiado largo','');
                    vlo_InputArchivo.value = "";
                    return false;
                }
                
                vln_TamArchivo = vlo_InputArchivo.files[0].size;
                if (vln_TamArchivo > vln_TamBytes){
                    mostrarAlertaError('El tamaño del archivo excede el máximo permitido.','');
                    vlo_InputArchivo.value = "";
                    return false;
                }

                return true;
            }
            return false;
        };

        function validaArchivoOficioInicio(){
            var vlo_InputArchivo = document.getElementById('<%=fuDocAdjudicacion.ClientID%>');
            var vlc_NombreArchivo = vlo_InputArchivo.value;
            var vlc_NombreCorto = " ";
            var vlc_Limiter = '\\';
            var vlc_Extension = '';
            var vln_Indice = 0;
            var vln_IndiceExtension = 0;
            var vlc_LimiterExtension = '.';
            var vln_limiteTamArchivo = <%=Me.TamanoArchivoOficio%>;
            var vln_TamArchivo;
            var vln_TamBytes = vln_limiteTamArchivo * <%=Utilerias.OrdenesDeTrabajo.Constantes.TAMANNO_BYTES_A_MEGAS%>;
            var vlc_Llaves;
           
            var vln_TamanoNombre = <%=Utilerias.OrdenesDeTrabajo.Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.NOMBRE_ARCHIVO_BD_TAMANO%>;
          
            if(vlo_InputArchivo.value != ''){
                
                vln_Indice =  vlc_NombreArchivo.lastIndexOf(vlc_Limiter) + 1;
                vlc_NombreCorto = vlc_NombreArchivo.substr(vln_Indice, vlc_NombreArchivo.length - vln_Indice);
                vln_IndiceExtension =  vlc_NombreArchivo.lastIndexOf(vlc_LimiterExtension) + 1;
                vlc_Extension = vlc_NombreArchivo.substr(vln_IndiceExtension, vlc_NombreArchivo.length);    
                
                vlc_ExtencionesPerimtidas = '<%=Me.ExtensionesPermitidasOficio%>';
                vlc_Llaves = vlc_ExtencionesPerimtidas.split(",");
            
                if(vlc_Llaves.indexOf(vlc_Extension.toUpperCase()) == -1){
                    mostrarAlertaError('No es una extensión permitida','');
                    vlo_InputArchivo.value = "";
                    return false;
                }
                
                if (vlc_NombreCorto.length > vln_TamanoNombre){
                    mostrarAlertaError('El nombre del archivo es demasiado largo','');
                    vlo_InputArchivo.value = "";
                    return false;
                }
                
                vln_TamArchivo = vlo_InputArchivo.files[0].size;
                if (vln_TamArchivo > vln_TamBytes){
                    mostrarAlertaError('El tamaño del archivo excede el máximo permitido.','');
                    vlo_InputArchivo.value = "";
                    return false;
                }

                return true;
            }
            return false;
        };

        function validaArchivoAdjudicacion(){
            var vlo_InputArchivo = document.getElementById('<%=fuDocumentosAdjudicacion.ClientID%>');
            var vlc_NombreArchivo = vlo_InputArchivo.value;
            var vlc_NombreCorto = " ";
            var vlc_Limiter = '\\';
            var vlc_Extension = '';
            var vln_Indice = 0;
            var vln_IndiceExtension = 0;
            var vlc_LimiterExtension = '.';
            var vln_limiteTamArchivo = <%=Me.TamanoArchivoOficio%>;
            var vln_TamArchivo;
            var vln_TamBytes = vln_limiteTamArchivo * <%=Utilerias.OrdenesDeTrabajo.Constantes.TAMANNO_BYTES_A_MEGAS%>;
            var vlc_Llaves;
           
            var vln_TamanoNombre = <%=Utilerias.OrdenesDeTrabajo.Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.NOMBRE_ARCHIVO_BD_TAMANO%>;
          
            if(vlo_InputArchivo.value != ''){
                
                vln_Indice =  vlc_NombreArchivo.lastIndexOf(vlc_Limiter) + 1;
                vlc_NombreCorto = vlc_NombreArchivo.substr(vln_Indice, vlc_NombreArchivo.length - vln_Indice);
                vln_IndiceExtension =  vlc_NombreArchivo.lastIndexOf(vlc_LimiterExtension) + 1;
                vlc_Extension = vlc_NombreArchivo.substr(vln_IndiceExtension, vlc_NombreArchivo.length);    
                
                vlc_ExtencionesPerimtidas = '<%=Me.ExtensionesPermitidasOficio%>';
                vlc_Llaves = vlc_ExtencionesPerimtidas.split(",");
            
                if(vlc_Llaves.indexOf(vlc_Extension.toUpperCase()) == -1){
                    mostrarAlertaError('No es una extensión permitida','');
                    vlo_InputArchivo.value = "";
                    return false;
                }
                
                if (vlc_NombreCorto.length > vln_TamanoNombre){
                    mostrarAlertaError('El nombre del archivo es demasiado largo','');
                    vlo_InputArchivo.value = "";
                    return false;
                }
                
                vln_TamArchivo = vlo_InputArchivo.files[0].size;
                if (vln_TamArchivo > vln_TamBytes){
                    mostrarAlertaError('El tamaño del archivo excede el máximo permitido.','');
                    vlo_InputArchivo.value = "";
                    return false;
                }

                return true;
            }
            return false;
        }


        function regresarAlListado() {
            window.location = 'Lst_OT_Contrataciones.aspx';
        };

        function mostrarAlertaGuardadoExitoso() {
            mostrarAlerta(
            '#arAlerta',
            {
                mensaje: 'Se han guardado los datos indicados para la contratación',
                tipo: "exito",
                transparencia: 0.9,
                posicion: 'center',
                onClosed: function () { window.location = 'Frm_OT_GestionContratacionProyecto.aspx'; }
            });
        };

        function MostrarExitoAlListado(){
            mostrarAlerta(
            '#arAlerta',
            {
                mensaje: 'Se ha enviado la orden devuelta al profesional de Diseño.',
                tipo: "exito",
                transparencia: 0.9,
                posicion: 'center',
                onClosed: function () { window.location = 'Lst_OT_Contrataciones.aspx'; }
            });
        }

        function mostrarAlertaDevueltaExitoso() {
            mostrarAlerta(
            '#arAlerta',
            {
                mensaje: 'Se ha devuelto el expediente de manera correcta',
                tipo: "exito",
                transparencia: 0.9,
                posicion: 'center',
                onClosed: function () { window.location = 'Frm_OT_GestionContratacionProyecto.aspx'; }
            });
        };

        function mostrarAlertaError(pvc_Msj) {
            mostrarAlerta(
                '#arAlerta',
                {
                    mensaje: pvc_Msj,
                    tipo: "peligro",
                    transparencia: 0.9,
                    posicion: 'center',
                    permiteCerrar: true
                }
            );
        };

        function mostrarNuevaLineaGuardada() {
            
            mostrarAlerta(
            '#arAlerta',
            {
                mensaje: 'Se ha guardado la linea de contratacion correctamente',
                tipo: "exito",
                transparencia: 0.9,
                posicion: 'center',
                permiteCerrar: true
            });
        };

        function mostrarAlertaNuevaVersion() {
            deshabilitarControl('#<%=btnNuevaVersion.ClientID%>');

            mostrarAlerta(
            '#arAlerta',
            {
                mensaje: 'Se ha generado una nueva versión de la contratación',
                tipo: "exito",
                transparencia: 0.9,
                posicion: 'center',
                onClosed: function () { window.location = 'Frm_OT_GestionContratacionProyecto.aspx'; }
            });
        };

        function confirmacionNuevaVersion() {
            var vln_Version = <%=Me.Contratacion.Version%>;
            var vln_VersionActual = <%=Me.ddlVersion.SelectedValue%>;
            var vlc_EstadoOrden = '<%=Me.OrdenTrabajo.EstadoOrdenTrabajo%>';
            var vlc_Estado = '<%=Utilerias.OrdenesDeTrabajo.EstadoOrden.PENDIENTE_REVISION_CONTRATACIONES%>';
            var vln_Existe = '<%=Me.Contratacion.Existe%>';

            var vlo_ConfiguracionPopup = {
                titulo: 'Gestion de Contrataciones',
                mensaje: '¿Está seguro que desea cambiar la versión?',
                botones:
                        [
                            {
                                idControl: "btnSi",
                                textoBoton: "Si",
                                onClick:
                                    function (e) {
                                        if (vlc_EstadoOrden != vlc_Estado){
                                            window.location = '#PopUpNuevaVersion';
                                        }else{
                                            __doPostBack('<%=btnAceptarVersion.UniqueID%>', '');
                                        }
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
                
            if (vln_Existe.toLowerCase() == 'false'){
                __doPostBack('<%=btnAceptarVersion.UniqueID%>', '');
            }else{

                $('#arpopupConfirmaDeseaContinuar').popup(vlo_ConfiguracionPopup);
                window.location = '#arpopupConfirmaDeseaContinuar';
                
            }
            return false;
        };

        function cambioVersion() {
            var vlo_ConfiguracionPopup = {
                titulo: 'Gestion de Contrataciones',
                mensaje: 'Los cambios pendientes de aplicar para la versión actual se perderán ¿Está seguro que desea cambiar la versión?',
                botones:
                        [
                            {
                                idControl: "btnSi",
                                textoBoton: "Si",
                                onClick:
                                    function (e) {
                                        $(this).attr("disabled", "disabled");
                                        __doPostBack('<%=btnNuevaVersion.UniqueID%>', '');
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