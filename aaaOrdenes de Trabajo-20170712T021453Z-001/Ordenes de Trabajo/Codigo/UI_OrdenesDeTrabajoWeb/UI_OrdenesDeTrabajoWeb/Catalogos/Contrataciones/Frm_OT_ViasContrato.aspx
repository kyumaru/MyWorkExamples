<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/MasterPage/Mp_Formulario.master" CodeFile="Frm_OT_ViasContrato.aspx.vb" Inherits="Catalogos_Frm_OT_ViasContrato" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content runat="server" ID="Content1" ContentPlaceHolderID="cphFormulario">

    <header>
        <h2>
            <asp:Label runat="server" ID="lblAccion"></asp:Label>
        </h2>
    </header>

    <article class="tituloSeccion">
        Datos Generales
    </article>

    <article class="formulario">
        <table>
            <tr>
                <th>Descripción</th>
                <td>
                    <asp:TextBox runat="server" id="txtDescripcion" Width="35%" data-tipoControl="texto"></asp:TextBox>
                    <asp:RequiredFieldValidator ValidationGroup="Aceptar" runat="server" id="rvfTxtDescripcion" ControlToValidate="txtDescripcion" display="Dynamic" ErrorMessage="La Descripción de la vía de contrato a ingresar es requerida.">&nbsp;</asp:RequiredFieldValidator>
                    <br />
                    <span id="spContadorTxtDescripcion" class="contadorCaracteresRestantes">

                    </span>
                </td>
            </tr>
            <tr>
                <th>Tope Económico</th>
                <td>
                    <asp:UpdatePanel runat="server" ID="UpTope" UpdateMode="Conditional"  >
                        <ContentTemplate>
                            <asp:TextBox id="txtTopeEconomico" runat="server" Width="35%"></asp:TextBox>
                            <asp:RequiredFieldValidator ValidationGroup="Aceptar" Enabled="false" runat="server" id="rfvTxtTopeEconomico" ControlToValidate="txtTopeEconomico" display="Dynamic" ErrorMessage="El tope económico de la vía de contrato a ingresar es requerida.">&nbsp;</asp:RequiredFieldValidator>
                            <br />
                            <ajax:FilteredTextBoxExtender ID="ftbtxtNumOrden" runat="server" TargetControlID="txtTopeEconomico" FilterMode="ValidChars" ValidChars="" FilterType="Numbers"></ajax:FilteredTextBoxExtender>
                            <span id="spContadortxtTopeEconomico" class="contadorCaracteresRestantes">
                            </span>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    

                    
                </td>
            </tr>
            <tr runat="server" id="trEstado">
                <th>Estado</th>
                <td>
                    <asp:DropDownList Width="35%" runat="server" ID="ddlEstado" AppendDataBoundItems="true" data-tipoControl="combo"></asp:DropDownList>
                    <asp:RequiredFieldValidator ValidationGroup="Aceptar" runat="server" ID="rfvDdlEstado" ControlToValidate="ddlEstado" Display="Dynamic" ErrorMessage="El estado de la vía de contrato es requerido.">&nbsp;</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <th>Ambito</th>
                <td>
                    <asp:UpdatePanel runat="server" ID="upAmbito" UpdateMode="Conditional"  >
                        <ContentTemplate>
                            <asp:DropDownList Width="35%" runat="server" ID="ddlAmbito" AppendDataBoundItems="true" data-tipoControl="combo" AutoPostBack="true"></asp:DropDownList>
                            <asp:RequiredFieldValidator ValidationGroup="Aceptar" runat="server" ID="rfvDdlAmbito" ControlToValidate="ddlAmbito" Display="Dynamic" ErrorMessage="El ambito es requerido.">&nbsp;</asp:RequiredFieldValidator>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlAmbito" />
                        </Triggers>
                    </asp:UpdatePanel>
                    
                </td>
            </tr>
        </table>
    </article>

    <article class="areaBotones">
        <asp:Button runat="server" ValidationGroup="Aceptar" ID="btnAceptar" Text="Aceptar" />
        <input type="button" data-tipo="limpiarFormulario" value="Limpiar Formulario" id="btnLimpiarFormulario"/>
        <input type="button" value="Cancelar" id="btnCancelar" />
    </article>

    <article id="arPopupDelFormulario"></article>
    <article id="arAlertasDelFormulario"></article>

    <script type="text/javascript">

        function regresarAlListado() {
            window.location = 'Lst_OT_ViasContrato.aspx';
        }
        function deshabilitarFormulario() {
            deshabilitarControl('#<%=Me.btnAceptar.ClientID%>');
            deshabilitarControl('#btnLimpiarFormulario');
            deshabilitarControl('#btnCancelar');
            $('.formulario').attr('disabled', 'disabled');
        }

        function mostrarPopupRegistroExitoso() {
            var vlo_ConfiguracionPopup = {
                titulo: 'Catálogo de vías de compra contrato',
                mensaje: 'Se ha registrado la información de la vía de compra contrato.<br /><strong>¿Desea registrar otra vía de compra contrato?</strong>',
                onClosed: function () { regresarAlListado(); },
                botones:
                    [
                        {
                            idControl: "btnSi",
                            textoBoton: "Sí",
                            onClick: function () { window.location = 'Frm_OT_ViasContrato.aspx?pvn_Operacion=<%=Utilerias.OrdenesDeTrabajo.eOperacion.Agregar%>'; }
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
                    mensaje: 'Se ha actualizado la información de la Vía de compra contrato',
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
                    mensaje: 'El número de identificación provisto no pertenece a ninguna vía de compra contrato registrado en el sistema',
                    tipo: "peligro",
                    transparencia: 0.9,
                    posicion: 'center',
                    permiteCerrar: true,
                    onClosed: function () { regresarAlListado(); }
                }
            );
        };

        $(document).ready(function () {

            $('#btnCancelar').click(function () {
                regresarAlListado();
            });

            configurarSpinnerNumerico('#<%= Me.txtTopeEconomico.ClientID%>', 100000);

            configurarLongitudMaximaTexto('#<%=Me.txtTopeEconomico.ClientID%>', '<%=Utilerias.OrdenesDeTrabajo.Modelo.OTM_VIA_COMPRA_CONTRATO.TOPE_ECONOMICO_BD_TAMANO%>');
            configurarContadorCaracteresRestantes('#<%=Me.txtTopeEconomico.ClientID%>', '#spContadortxtTopeEconomico');

            configurarLongitudMaximaTexto('#<%=Me.txtDescripcion.ClientID%>', '<%=Utilerias.OrdenesDeTrabajo.Modelo.OTM_VIA_COMPRA_CONTRATO.DESCRIPCION_BD_TAMANO%>');
            configurarContadorCaracteresRestantes('#<%=Me.txtDescripcion.ClientID%>', '#spContadorTxtDescripcion');

        });
    </script>

</asp:Content>