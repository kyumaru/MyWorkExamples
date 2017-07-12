<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/Mp_Formulario.master" AutoEventWireup="false" CodeFile="Frm_OT_AjusteInventarioAprobSupervisor.aspx.vb" Inherits="OrdenesDeTrabajo_Almacen_Frm_OT_AjusteInventarioAprobSupervisor" %>

<%@ Register TagPrefix="wuc" Namespace="Utilerias.WebForms.Controles" Assembly="Utilerias.WebForms.Controles" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphFormulario" Runat="Server">
    <header>
        <h2>Aprobación de ajustes de inventario por parte del supervisor</h2>

    </header>


    <article class="tituloSeccion">
        Ajuste de materiales
    </article>

    <article class="formulario sinBorde">
        <table>
            <tr>
                <th style="width: 14%;">Observaciones</th>
                <td>
                    <asp:TextBox runat="server" ID="txtObs" Width="100%" TextMode="MultiLine" Rows="9" Enabled="false" data-tipocontrol="texto"></asp:TextBox>
                </td>
            </tr>

        </table>
    </article>

    <article data-grupo="Listado" class="listado sinBorde">
        <asp:Repeater runat="server" ID="rpArchivo">
            <HeaderTemplate>
                <table>
                    <tr>
                        <th>
                            <asp:LinkButton runat="server" ID="lnkCodigo" Text="Código" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_AJUSTELST.ID_MATERIAL%>" OnCommand="lnkArchivo_Command"></asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton runat="server" ID="lnkDescripcion" Text="Descripcion" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_AJUSTELST.DESC_MATERIAL%>" OnCommand="lnkArchivo_Command"></asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton runat="server" ID="lnkExistencia" Text="Existencia" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_AJUSTELST.CANTIDAD_DISPONIBLE%>" OnCommand="lnkArchivo_Command"></asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton runat="server" ID="lnkDisponible" Text="Disponible" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_AJUSTELST.DISPONIBLE%>" OnCommand="lnkArchivo_Command"></asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton runat="server" ID="lnkTipoAjuste" Text="Tipo de Ajuste" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_AJUSTELST.DIRECCION_DESC%>" OnCommand="lnkArchivo_Command"></asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton runat="server" ID="lnkCantidadAjuste" Text="Cantidad Ajuste" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_AJUSTELST.CANTIDAD%>" OnCommand="lnkArchivo_Command"></asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton runat="server" ID="lnkExistenciaPost" Text="Existencia Post Ajuste" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_AJUSTELST.EXISTENCIA_POST%>" OnCommand="lnkArchivo_Command"></asp:LinkButton>
                        </th>
                    </tr>
            </HeaderTemplate>

            <ItemTemplate>
                <tr class="lineaDelListado">
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_AJUSTELST.ID_MATERIAL)%></td>
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_AJUSTELST.DESC_MATERIAL)%></td>
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_AJUSTELST.CANTIDAD_DISPONIBLE)%></td>
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_AJUSTELST.DISPONIBLE)%></td>
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_AJUSTELST.DIRECCION_DESC)%></td>
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_AJUSTELST.CANTIDAD)%></td>
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_AJUSTELST.EXISTENCIA_POST)%></td>
                </tr>
            </ItemTemplate>

            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
    </article>

    <article data-grupo="Listado" class="areaPaginadorListado">
        <asp:Label runat="server" ID="lblNoHayDAtosArchivo" Text="No se cuenta con información para mostrar" Visible="false"></asp:Label>
        <wuc:PaginadorNumerico runat="server" ID="pnRpArchivo" />
    </article>

    <article data-grupo="Listado" class="areaCantidadDeRegistro">
        <asp:Label runat="server" ID="lblCantidadDeRegistrosArchivo" Text="" Visible="true"></asp:Label>
    </article>

    <article class="tituloSeccion">
        Datos de la Revisión
    </article>
    <article class="formulario ">
        <table>
            <tr>
                <th style="width: 14%;"></th>
                <td>
                    <asp:RadioButton ID="rdbAprobado" runat="server" GroupName="grpAprobo" Text="Enviar a Aprobación" AutoPostBack="true" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:RadioButton ID="rdbDevuelto" runat="server" GroupName="grpAprobo" Text="Devolver" AutoPostBack="true" />
                    <asp:CustomValidator ID="cvcgrpEstadoRevision" runat="server" ControlToValidate="txtValidaciones" ErrorMessage="Debe indicar si decide enviar a aprobación o devolver el ajuste de inventario" ValidationGroup="Grupo1" ClientValidationFunction="ValidarEstadoRevision">&nbsp;</asp:CustomValidator>
                </td>
            </tr>

        </table>
        <asp:UpdatePanel runat="server" ID="upDdlCurso" UpdateMode="Conditional">
            <ContentTemplate>
                <table>
                    <tr runat="server" id="trObservaciones">
                        <th style="width: 14%;">Observaciones</th>
                        <td>
                            <asp:TextBox runat="server" ID="txtObservaciones" Width="100%" TextMode="MultiLine" Rows="9" data-tipocontrol="texto"></asp:TextBox>
                            <%--<br />
                                        <span id="spContadorTxtObservaciones" class="contadorCaracteresRestantes"></span>--%>
                            <asp:CustomValidator ID="cvstxtObservacionesPrimeraRevision" runat="server" ControlToValidate="txtValidaciones" ErrorMessage="Debe indicar una observación si decide devolver la gestión de compra" ValidationGroup="Grupo1" ClientValidationFunction="ValidadorDeCampoRequeridoPersonalizado">&nbsp;</asp:CustomValidator>
                        </td>
                    </tr>

                </table>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="rdbAprobado" />
                <asp:AsyncPostBackTrigger ControlID="rdbDevuelto" />
            </Triggers>
        </asp:UpdatePanel>
        <br />
    </article>

    <article style="visibility: hidden">
        <asp:TextBox ID="txtValidaciones" runat="server" Text="Validar"></asp:TextBox>
    </article>

    <article class="areaBotones">
        <asp:Button ID="btnTramitar" runat="server" Text="Aceptar" ToolTip="Aceptar" ValidationGroup="Grupo1"></asp:Button>
        <input id="btnLimpiarFormulario" type="button" data-tipo="limpiarFormulario" value="Limpiar formulario" />
        <input id="btnCancelar" type="button" title="Regresar" value="Regresar" />
    </article>

    <article id="arAlertasDelFormulario"></article>

    <script type="text/javascript">

        $(document).ready(function () {
            $('#btnCancelar').click(function () {
                regresarAlListado();
            });


        })


        function ValidadorDeCampoRequeridoPersonalizado(source, clientside_arguments) {
            if (document.getElementById('<%=rdbDevuelto.ClientID%>').checked && document.getElementById('<%=txtObservaciones.ClientID%>').value.trim() == "") {
                document.getElementById('<%=txtObservaciones.ClientID%>').style.backgroundColor = "#F5838A"
                return clientside_arguments.IsValid = false;
            }
            else {
                document.getElementById('<%=txtObservaciones.ClientID%>').style.backgroundColor = "white"
                return clientside_arguments.IsValid = true;
            }
        }

        function mostrarAlertaError(pvc_Mensaje) {
            mostrarAlerta(
                '#arAlertasDelFormulario',
                {
                    mensaje: pvc_Mensaje,
                    tipo: 'peligro',
                    transparencia: 1,
                    posicion: 'top',
                    permiteCerrar: true
                }
            );
        }


        /*
          Autor: Mauricio Salas
          Fecha:02/09/2016 
          Descripcion: Función que valida que se haya seleccionado si fue aprovado o devuelta la gestion
          Parametros: source, clientside_arguments: parametros de la Función CustomValidator
          */
        function ValidarEstadoRevision(source, clientside_arguments) {
            return clientside_arguments.IsValid = (document.getElementById('<%=rdbAprobado.ClientID%>').checked || document.getElementById('<%=rdbDevuelto.ClientID%>').checked);
        }

        function mostrarAlertaRegistroExitoso() {
            deshabilitarControl('#<%=btnTramitar.ClientID%>');
            deshabilitarControl('#btnLimpiarFormulario');
            deshabilitarControl('#btnCancelar');
            $('.formulario').attr('disabled', 'disabled');

            mostrarAlerta(
                '#arAlertasDelFormulario',
                {
                    mensaje: 'Se ha tramitado el ajuste de inventario.',
                    tipo: "exito",
                    transparencia: 0.9,
                    posicion: 'top',
                    onClosed: function () { regresarAlListado(); }
                });
        }

        function regresarAlListado() {
            window.location = 'Lst_OT_AjusteInventarioAprobSupervisor.aspx';
        }

    </script>
</asp:Content>

