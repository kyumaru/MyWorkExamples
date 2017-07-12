<%@ Page Language="VB" Title="Asignación de profesional para acompañamientos en la valoración de la viabilidad técnica" MasterPageFile="~/MasterPage/Mp_Formulario.master" AutoEventWireup="false" CodeFile="Frm_OT_AsignacionProfViabilidadTecnica.aspx.vb" Inherits="OrdenesDeTrabajo_Frm_OT_AsignacionProfViabilidadTecnica" %>

<%@ Register TagPrefix="wuc" TagName="InformacionGeneral" Src="~/ControlesDeUsuario/Wuc_OT_InformacionGeneral.ascx" %>
<%@ Register TagPrefix="wuc" Namespace="Utilerias.WebForms.Controles" Assembly="Utilerias.WebForms.Controles" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content runat="server" ID="Content1" ContentPlaceHolderID="cphFormulario">
    
    <header>
        <h2>Gestión de recursos para Evaluación y ejecución de órdenes de trabajo</h2>
    </header>

    <article class="formulario areaBotones">
        <table>
            <tr>
                <th>Nombre Del Proyecto</th>
                <td>
                    <asp:TextBox runat="server" ID="txtNombreProyecto" Width="59%" ></asp:TextBox>
                    <asp:RequiredFieldValidator ValidationGroup="Modificar" runat="server" ID="rfvtxtNombreProyecto" ControlToValidate="txtNombreProyecto" Display="Dynamic" ErrorMessage="Debe indicar el nombre del proyecto">&nbsp;</asp:RequiredFieldValidator>
                    <asp:Button CssClass="areaBotones" ValidationGroup="Modificar" runat="server" ID="btnModificar" Text="Actualizar" />
                </td>
            </tr>
        </table>
        
    </article>

    <article class="tituloSeccion">
        Información General de la OT
    </article>
    <asp:UpdatePanel runat="server" ID="upControlOrdenTrabajo" UpdateMode="Conditional">
        <ContentTemplate>
            <wuc:InformacionGeneral ID="ctrl_InfoGeneral" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
        <br />

    
   
    <article class="tituloSeccion">
            Recursos Para Evaluación / Recurso Humano / Encargados del proyecto
    </article>
    <article class="listado">
        <asp:Repeater runat="server" ID="rpEncargados">

            <HeaderTemplate>
                <table>
                    <tr>
                        <th>Identificación</th>
                        <th>Nombre</th>
                        <th>Area</th>
                        <th>Fecha Desde</th>
                        <th>Fecha Hasta</th>
                    </tr>
            </HeaderTemplate>

            <ItemTemplate>
                <tr class="lineaDelListado">
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.CEDULA)%></td>
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.NOMBRE_EMPLEADO)%></td>
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.DESC_AREA)%></td>
                    <td><%#CType(Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.FECHA_DESDE), DateTime).ToString(Utilerias.OrdenesDeTrabajo.Constantes.FORMATO_FECHA_UI)%></td>
                    <td><%#CType(Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.FECHA_HASTA), DateTime).ToString(Utilerias.OrdenesDeTrabajo.Constantes.FORMATO_FECHA_UI)%></td>
                </tr>
            </ItemTemplate>

            <FooterTemplate>
                </table>
            </FooterTemplate>

        </asp:Repeater>
    </article>
    <br />

    <article class="tituloSeccion">
        Profesionales para valoración
    </article>
    
    <article class="formulario">
        <table>
            <tr>
                <th>Funcionario</th>
                <td>
                    <asp:dropdownlist runat="server" ValidationGroup="Agregar" id="ddlProfesional" Width="50%" data-tipoControl="combo" AppendDataBoundItems="true" AutoPostBack="true"></asp:dropdownlist> <br />
                    <asp:RequiredFieldValidator runat="server" ValidationGroup="Agregar" ID="rfvddlFuncionario" ControlToValidate="ddlProfesional" Display="Dynamic" ErrorMessage="Debe indicar al menos un funcionario para realizar la evaluación del trabajo">&nbsp;</asp:RequiredFieldValidator>
                    
                </td>
                </tr>
            <tr>
                <th>Area Profesional:</th> 
                <td>
                    <asp:UpdatePanel runat="server" ID="upAreaProfesional" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlProfesional" EventName="SelectedIndexChanged" />
                        </Triggers>
                        <ContentTemplate>
                            <asp:Label runat="server" ID="lblAreaProfesional" data-tipocontrol="etiqueta" ></asp:Label>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <th>Tiempo estimado para realizar la evaluación</th>
                <td>
                    <asp:TextBox runat="server" id="txtTiempoEstimado" ValidationGroup="Agregar" Width="10%" data-tipoControl="texto"></asp:TextBox> 
                    <asp:DropDownList Width="40%" runat="server" ID="ddlUnidad" ValidationGroup="Agregar" AppendDataBoundItems="true" data-tipoControl="combo"></asp:DropDownList> <br />
                    <asp:RequiredFieldValidator runat="server" id="rvfTxtTiempoEstimado" ValidationGroup="Agregar" ControlToValidate="txtTiempoEstimado" display="Dynamic" ErrorMessage="Debe indicar el tiempo estimado para efectuar la evaluación del trabajo.">&nbsp;</asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator runat="server" ID="rfvDdlUnidad" ValidationGroup="Agregar" ControlToValidate="ddlUnidad" Display="Dynamic" ErrorMessage="La unidad de tiempo es requerida">&nbsp;</asp:RequiredFieldValidator>
                </td>  
            </tr>
        </table>
        <article class="areaBotones"><asp:Button ValidationGroup="Agregar" runat="server" Text="Agregar" ID="btnAgregarFuncionario" /></article>      
    </article>
    <article class="tituloSeccion">
        Listado de profesionales para valoración
    </article>
    <article class="listado">
        <asp:Repeater runat="server" ID="rpProfesionales">

            <HeaderTemplate>
                <table>
                    <tr>
                        <th>Identificación</th>
                        <th>Nombre</th>
                        <th>Area</th>
                        <th>Tiempo</th>
                        <th>&nbsp;</th>
                    </tr>
            </HeaderTemplate>

            <ItemTemplate>
                <tr class="lineaDelListado">
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.CEDULA)%></td>
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.NOMBRE_EMPLEADO)%></td>
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.DESC_AREA)%></td>
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.DESC_TIEMPO_REAL)%></td>
                    <%--<td runat="server" id="tdBorrar">
                        <asp:ImageButton ID="ibBorrar" runat="server" CausesValidation="false" ToolTip="Borrar" data-tipo="borrarRegistro"
                            CommandName='<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTF_OPERARIO_AREALST.CEDULA)%>'
                            ImageUrl='<%#AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Borrar.png")%>'
                            onmouseover='<%#String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Icono_Borrar.png"))%>'
                            onmouseout='<%#String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Borrar.png"))%>'
                            OnClick="ibBorrar_Click" />
                    </td>--%>
                </tr>
            </ItemTemplate>

            <FooterTemplate>
                </table>
            </FooterTemplate>

        </asp:Repeater>
    </article>

    <article class="areaBotones">
        <asp:Button runat="server" ValidationGroup="Modificar" ID="btnGuardar" Text="Guardar" OnClick="btnGuardar_Click" />
        <asp:Button runat="server" ValidationGroup="Modificar" ID="btnGuardarYFinalizar" OnClick="btnGuardarYFinalizar_Click" text="Guardar y Finalizar" />
        <input type="button" value="Regresar" id="btnRegresar" />
    </article>
    <article id="arAlertasDelFormulario"></article>

    <script type="text/javascript">

        $(document).ready(function () {

            $('#btnRegresar').click(function () {
                window.location = 'Lst_OT_GestionProfesionalesDisenio.aspx';

            });

        });

        function regresarAlListado() {
            var vlb_CambiosRealizados = <%= IIf(Me.BanderaCambios, "true", "false")%>;
            if (vlb_CambiosRealizados == true){          
                var vlo_ConfiguracionPopup = {
                    titulo: 'Asignacion de profesionales para viabilidad técnica',
                    mensaje: 'Al regresar se perderán los datos no guardados, ¿Desea Continuar?',
                    botones:
                            [
                                {
                                    idControl: "btnSi",
                                    textoBoton: "Si",
                                    onClick: function () { window.location = 'Lst_OT_GestionProfesionalesDisenio.aspx'; }
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

                $('#arAlertasDelFormulario').popup(vlo_ConfiguracionPopup);

                window.location = '#arAlertasDelFormulario';

                return false;
            }else{
                window.location = 'Lst_OT_GestionProfesionalesDisenio.aspx';
            };
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

        function mostrarAlertaEvaluacionExitosaGuardar() {
            deshabilitarControl('#<%=btnGuardar.ClientID%>');
             //   deshabilitarControl('#btnLimpiarFormulario');
             deshabilitarControl('#<%=btnGuardarYFinalizar.ClientID%>');
             $('.formulario').attr('disabled', 'disabled');

             mostrarAlerta(
             '#arAlertasDelFormulario',
             {
                 mensaje: 'Se ha guardado los datos indicados para la evaluación de tiempo',
                 tipo: "exito",
                 transparencia: 0.9,
                 posicion: 'center',
                 onClosed: function () { window.location = 'Lst_OT_GestionProfesionalesDisenio.aspx'; }
             });
         };

        function mostrarAlertaEvaluacionExitosa() {
            deshabilitarControl('#<%=btnGuardar.ClientID%>');
            //   deshabilitarControl('#btnLimpiarFormulario');
            deshabilitarControl('#<%=btnGuardarYFinalizar.ClientID%>');
            $('.formulario').attr('disabled', 'disabled');

            mostrarAlerta(
            '#arAlertasDelFormulario',
            {
                mensaje: 'Se ha guardado los datos indicados para la evaluación de tiempo y se ha notificado a cada uno de los profesionales',
                tipo: "exito",
                transparencia: 0.9,
                posicion: 'center',
                onClosed: function () { window.location = 'Lst_OT_GestionProfesionalesDisenio.aspx'; }
            });
        };

        function mostrarAlertaNombreProyectoExitoso() {
            mostrarAlerta(
                '#arAlertasDelFormulario',
                {
                    mensaje: 'Se ha actualizado el nombre de proyecto.',
                    tipo: "exito",
                    transparencia: 0.9,
                    posicion: 'center',
                    permiteCerrar: true
                }
                );
        };

        



    </script>

</asp:Content>