<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/Mp_Formulario.master" AutoEventWireup="false" CodeFile="Frm_OT_ProgramacionOTPreventivo.aspx.vb" Inherits="OrdenesDeTrabajo_Frm_OT_ProgramacionOTPreventivo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphFormulario" runat="Server">

    <header>
        <h2>
            <asp:Label runat="server" ID="lblAccion"></asp:Label>
        </h2>
    </header>

    <article class="tituloSeccion">
        Programación de Orden de Trabajo Preventivo
    </article>

    <article class="formulario">
        <table>
            <tr>
                <th>No. de Orden de Trabajo</th>
                <td>
                    <asp:TextBox runat="server" ID="txtNumOrden" data-tipocontrol="texto" MaxLength="10" Columns="40"></asp:TextBox>
                    <asp:FilteredTextBoxExtender ID="ftbTxtNumOrden" runat="server" TargetControlID="txtNumOrden" FilterMode="ValidChars" FilterType="Numbers"></asp:FilteredTextBoxExtender>
                    <asp:RequiredFieldValidator runat="server" ID="rfvTxtNumOrden" ControlToValidate="txtNumOrden" Display="Dynamic" ErrorMessage="No. de orden de trabajo es obligatorio." ValidationGroup="Aceptar"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <th>Edificio o Sitio</th>
                <td>
                    <asp:DropDownList Width="55%" runat="server" ID="ddlLugarTrabajo" data-tipocontrol="combo" AppendDataBoundItems="true"></asp:DropDownList>
                    <asp:RequiredFieldValidator runat="server" ID="rfvDdlLugarTrabajo" ControlToValidate="ddlLugarTrabajo" Display="Dynamic" ErrorMessage="Edificio o Sitio es obligatorio." ValidationGroup="Aceptar"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <th>Categoría</th>
                <td>
                    <asp:DropDownList Width="55%" runat="server" ID="ddlCategoria" data-tipocontrol="combo" AppendDataBoundItems="true" AutoPostBack="true"></asp:DropDownList>                                       
                    <asp:RequiredFieldValidator runat="server" ID="rfvDdlCategoria" ControlToValidate="ddlCategoria" Display="Dynamic" ErrorMessage="Categoría es obligatoria." ValidationGroup="Aceptar"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <th>Solicitante</th>
                <td>
                    <asp:Label runat="server" ID="lblSolicitante" data-tipocontrol="etiqueta"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>Actividad</th>
                <td>
                    <asp:UpdatePanel runat="server" ID="upActividad" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:DropDownList Width="55%" runat="server" ID="ddlActividad" data-tipocontrol="combo" AppendDataBoundItems="true" AutoPostBack="true"></asp:DropDownList>   
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlCategoria" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>                    
                    <asp:RequiredFieldValidator runat="server" ID="rfvDdlActividad" ControlToValidate="ddlActividad" Display="Dynamic" ErrorMessage="Actividad es obligatoria." ValidationGroup="Aceptar"></asp:RequiredFieldValidator>
                </td>
            </tr> 
        </table>
    </article>

    <article class="areaBotones">
        <asp:Button runat="server" ID="btnAceptar" Text="Aceptar" ValidationGroup="Aceptar" OnClientClick="javascript:return validarGuardar();" />
        <input id="btnLimpiarFormulario" type="button" data-tipo="limpiarFormulario" value="Limpiar formulario" />
        <input id="btnCancelar" type="button" value="Cancelar" />
    </article>

    <article style="visibility:hidden">
        <asp:Button runat="server" ID="btnAceptarOculto" Text="Guardar" ValidationGroup="Aceptar" />
    </article>

    <article id="arPopupDelFormulario"></article>
    <article id="arAlertasDelFormulario"></article>
    
    <script type="text/javascript">

        function validarGuardar() {
            if (Page_ClientValidate("Aceptar")) {
                document.getElementById('<%=btnAceptar.ClientID%>').disabled = "true";
            }
            __doPostBack('<%=Me.btnAceptarOculto.UniqueID%>', '');
            return false;
        }

        function regresarAlListado() {
            window.location = 'Lst_OT_ProgramacionOTPreventivo.aspx';
        };

        function mostrarPopupRegistroExitoso() {
            var vlo_ConfiguracionPopup = {
                titulo: 'Programación de Ordenes de Trabajo Preventivo',
                mensaje: 'Se ha registrado la información.. <br/><strong>¿Desea registrar otra Orden?</strong>',
                onClosed: function () { regresarAlListado(); },
                botones:
                        [
                            {
                                idControl: "btnSi",
                                textoBoton: "Sí",
                                onClick: function () { window.location = 'Frm_OT_ProgramacionOTPreventivo.aspx?pvn_Operacion=<%=Utilerias.OrdenesDeTrabajo.eOperacion.Agregar%>'; }
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

        function mostrarAlertaActualizacionExitosa() {
            deshabilitarControl('#<%=btnAceptar.ClientID%>');
            deshabilitarControl('#btnLimpiarFormulario');
            deshabilitarControl('#btnCancelar');
            $('.formulario').attr('disabled', 'disabled');

            mostrarAlerta(
                '#arAlertasDelFormulario',
                {
                    mensaje: 'Se ha actualizado la información de la orden.',
                    tipo: "exito",
                    transparencia: 0.9,
                    posicion: 'center',
                    onClosed: function () { regresarAlListado(); }
                });
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
                    mensaje: 'El identificador provisto no pertenece a ninguna orden de trabajo preventiva.',
                    tipo: "peligro",
                    transparencia: 0.9,
                    posicion: 'center',
                    onClosed: function () { regresarAlListado(); }
                });
        };

        $(document).ready(function () {
            $('#btnCancelar').click(function () {
                regresarAlListado();
            });

            habilitarTooltipGenerico();
           
        });

        function HabilitarSpinnerNumerico() {
            configurarSpinnerNumericoRango('#<%= Me.txtNumOrden.ClientID%>', 1, 1, 200, true);
        };

    </script>

</asp:Content>

