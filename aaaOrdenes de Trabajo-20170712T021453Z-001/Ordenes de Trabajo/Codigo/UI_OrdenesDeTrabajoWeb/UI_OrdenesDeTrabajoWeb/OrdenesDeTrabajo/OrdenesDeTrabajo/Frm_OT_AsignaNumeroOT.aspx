<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/Mp_Formulario.master" AutoEventWireup="false" CodeFile="Frm_OT_AsignaNumeroOT.aspx.vb" Inherits="OrdenesDeTrabajo_Frm_OT_AsignaNumeroOT" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphFormulario" runat="Server">

    <header>
        <h2>
            <asp:Label runat="server" ID="lblAccion"></asp:Label>
        </h2>
    </header>

    <article class="tituloSeccion">
        Registro del Número de Orden de Trabajo
    </article>

    <article class="formulario" id="formularioTotal" runat="server">
        <table>
            <tr>
                <th>Solicitante</th>
                <td>
                    <asp:Label runat="server" ID="lblSolicitante" data-tipocontrol="etiqueta"></asp:Label>
                </td>
            </tr>
             <tr>
                <th>Lugar Exacto</th>
                <td>
                    <asp:TextBox Width="59%" runat="server" ID="txtLugarExacto" data-tipocontrol="texto" TextMode="MultiLine" Rows="4" Enabled="false"></asp:TextBox>
                    <img class="tooltip" src="<%= AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Icono_Ayuda.png")%>" 
                        title="Señas exactas del lugar donde se requiere la realización del trabajo." />
                </td>
            </tr>
             <tr>
                <th>No. Orden de Trabajo</th>
                <td>
                    <asp:TextBox Width="59%" runat="server" ID="txtNumeroOrden" data-tipocontrol="texto" MaxLength="10"></asp:TextBox>
                    <ajax:FilteredTextBoxExtender ID="ftbTxtNumeroOrden" runat="server" FilterType="Numbers, Custom" TargetControlID="txtNumeroOrden"></ajax:FilteredTextBoxExtender>
                </td>
            </tr>
        </table>
    </article>
    
    <article class="areaBotones">
        <asp:Button runat="server" ID="btnAceptar" Text="Aceptar" ValidationGroup="Aceptar" />
        <%--<input id="btnLimpiarFormulario" type="button" data-tipo="limpiarFormulario" value="Limpiar formulario" />--%>
        <input id="btnCancelar" type="button" value="Cancelar" />
    </article>

    <article id="arPopupDelFormulario"></article>
    <article id="arAlertasDelFormulario"></article>

    <script type="text/javascript">

        function regresarAlListado() {
            window.location = 'Lst_OT_OrdenesTrabajoRecepcion.aspx';
        };

        function mostrarAlertaActualizacionExitosa() {
            deshabilitarControl('#<%=btnAceptar.ClientID%>');
            //deshabilitarControl('#btnLimpiarFormulario');
            deshabilitarControl('#btnCancelar');
            $('.formulario').attr('disabled', 'disabled');

            mostrarAlerta(
                '#arAlertasDelFormulario',
                {
                    mensaje: 'Se ha actualizado el número de la orden.',
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
            //deshabilitarControl('#btnLimpiarFormulario');
            deshabilitarControl('#btnCancelar');
            $('.formulario').attr('disabled', 'disabled');

            mostrarAlerta(
                '#arAlertasDelFormulario',
                {
                    mensaje: 'El identificador provisto no pertenece a ninguna orden de trabajo.',
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

    </script>

</asp:Content>

