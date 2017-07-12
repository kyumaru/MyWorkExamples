<%@ Page Language="VB" MasterPageFile="~/MasterPage/Mp_Formulario.master" AutoEventWireup="false" CodeFile="Frm_OT_Rubros.aspx.vb" Inherits="Catalogos_Frm_OT_Rubros" %>


<asp:Content runat="server" ID="Content1" ContentPlaceHolderID="cphFormulario">

    <header>
        <h2>
            <asp:Label runat="server" ID="lblAccion"></asp:Label>
        </h2>
    </header>

    <article class="formulario">
        <table>
            <tr>
                <th>Descripción</th>
                <td>
                    <asp:TextBox runat="server" id="txtDescripcion" Width="60%" data-tipoControl="texto"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" id="rvfTxtDescripcion" ControlToValidate="txtDescripcion" display="Dynamic" ErrorMessage="La Descripción del rubro a ingresar es requerida.">&nbsp;</asp:RequiredFieldValidator>
                    <br />
                    <span id="spContadorTxtDescripcion" class="contadorCaracteresRestantes">

                    </span>
                </td>
            </tr>
            <tr>
                <th>Estado</th>
                <td>
                    <asp:DropDownList Width="60%" runat="server" ID="ddlEstado" AppendDataBoundItems="true" data-tipoControl="combo"></asp:DropDownList>
                    <asp:RequiredFieldValidator runat="server" ID="rfvDdlEstado" ControlToValidate="ddlEstado" Display="Dynamic" ErrorMessage="El estado del rubro es requerido.">&nbsp;</asp:RequiredFieldValidator>
                </td>
            </tr>
        </table>
    </article>

    <article class="areaBotones">
        <asp:Button runat="server" ID="btnAceptar" Text="Aceptar" />
        <input type="button" data-tipo="limpiarFormulario" value="Limpiar Formulario" id="btnLimpiarFormulario"/>
        <input type="button" value="Cancelar" id="btnCancelar" />
    </article>

    <article id="arPopupDelFormulario"></article>
    <article id="arAlertasDelFormulario"></article>


     <script type="text/javascript">

         function regresarAlListado() {
             window.location = 'Lst_OT_Rubros.aspx';
         };

         function deshabilitarFormulario() {
             deshabilitarControl('#<%=Me.btnAceptar.ClientID%>');
             deshabilitarControl('#btnLimpiarFormulario');
             deshabilitarControl('#btnCancelar');
             $('.formulario').attr('disabled', 'disabled');
         };

         function mostrarPopupRegistroExitoso() {
             var vlo_ConfiguracionPopup = {
                 titulo: 'Catálogo de Rubros para descisión Inicial',
                 mensaje: 'Se ha registrado la información del rubro.<br /><strong>¿Desea registrar otro rubro?</strong>',
                 onClosed: function () { regresarAlListado(); },
                 botones:
                     [
                         {
                             idControl: "btnSi",
                             textoBoton: "Sí",
                             onClick: function () { window.location = 'Frm_OT_Rubros.aspx?pvn_Operacion=<%=Utilerias.OrdenesDeTrabajo.eOperacion.Agregar%>'; }
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
                             mensaje: 'Se ha actualizado la información del rubro',
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
                             mensaje: 'El número de identificación provisto no pertenece a ningun rubro registrado en el sistema',
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

                     configurarLongitudMaximaTexto('#<%=Me.txtDescripcion.ClientID%>', '<%=Utilerias.OrdenesDeTrabajo.Modelo.OTM_RUBRO_DECISION_INICIA.DESCRIPCION_BD_TAMANO%>');
             configurarContadorCaracteresRestantes('#<%=Me.txtDescripcion.ClientID%>', '#spContadorTxtDescripcion');

         });

    </script>

</asp:Content>
