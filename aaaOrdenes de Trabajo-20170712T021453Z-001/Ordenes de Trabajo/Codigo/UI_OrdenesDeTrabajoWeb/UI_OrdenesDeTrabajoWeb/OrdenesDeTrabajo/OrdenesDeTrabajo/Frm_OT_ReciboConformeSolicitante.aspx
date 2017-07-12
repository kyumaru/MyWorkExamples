<%@ Page Title="Recibido Conforme del Solicitante" Language="VB" MasterPageFile="~/MasterPage/Mp_Formulario.master" AutoEventWireup="false" CodeFile="Frm_OT_ReciboConformeSolicitante.aspx.vb" Inherits="OrdenesDeTrabajo_Frm_OT_ReciboConformeSolicitante" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphFormulario" runat="Server">
    <header>
        <h2>
            <asp:Label runat="server" ID="lblAccion"></asp:Label></h2>
    </header>
    <article class="tituloSeccion">
        Datos de la Orden de Trabajo
    </article>
    <article class="formulario">
        <table>
             <tr>
                <th>Historial</th>
                <td>
                    <asp:TextBox runat="server" ID="txtDescripciones" TextMode="MultiLine" Columns="70" Rows="15" Enabled="false"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>Descripción Trabajo</th>
                <td>
                    <asp:Label runat="server" ID="lblDescripcion" Width="60%"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>Se realizó el trabajo?</th>
                <td>
                    <asp:RadioButton runat="server" ID="rdbSi" Text="Sí" GroupName="Clasificacion" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:RadioButton runat="server" ID="rdbNo" Text="No" GroupName="Clasificacion" />                                     
                </td>
            </tr>
            <tr>
                <th>Observaciones</th>
                <td>
                    <asp:TextBox runat="server" ID="txtObservaciones" Width="60%" TextMode="MultiLine" Rows="6" data-tipoControl="texto"></asp:TextBox>

                    <br />
                    <span id="spContadorTxtObservaciones" class="contadorCaracteresRestantes"></span>
                </td>
            </tr>
        </table>
    </article>

    <article class="areaBotones">
        <asp:Button runat="server" ID="btnAceptar" Text="Aceptar" ValidationGroup="Aceptar" />
        <asp:Button runat="server" ID="btnLimpiarFormulario" Text="Limpiar Formulario" data-tipo="limpiarFormulario" />
        <input id="btnCancelar" type="button" value="Cancelar" />
    </article>

    <article id="arPopupDelFormulario"></article>
    <article id="arAlertasDelFormulario"></article>

    <script type="text/javascript">
        function regresarAlListado() {
            window.location = 'Lst_OT_OrdenTrabajo.aspx';
        };

        function deshabilitarFormulario() {
            deshabilitarControl('#<%=Me.btnAceptar.ClientID%>');
            deshabilitarControl('#btnLimpiarFormulario');
            deshabilitarControl('#btnCancelar');

            $('.formulario').attr('disabled','disabled');
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

        function mostrarAlertaNoEncontrado() {
            mostrarAlerta(
                '#arAlertasDelFormulario',
                {
                    mensaje: "No se han encontrado ordenes de trabajo con las condiciones indicadas",
                    tipo: "advertencia",
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
                    mensaje: 'Se ha actualizado la información de recibido conforme',
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
                    mensaje: 'El número de identificación provisto no pertenece a ninguna orden de trabajo registrada en el sistema',
                    tipo: "peligro",
                    transparencia: 0.9,
                    posicion: 'center',
                    permiteCerrar: true,
                    onClosed: function () { regresarAlListado(); }
                }
                );
        };

        function verificaSeleccionado(source, clientside_arguments)  {   
            var vlo_Textbox = document.getElementById("<%=Me.txtObservaciones.ClientID%>");
            vlo_Textbox.style.backgroundColor = "white";

            if(document.getElementById("<%=Me.rdbSi.ClientID%>").checked || document.getElementById("<%=Me.rdbNo.ClientID%>").checked) {
                
                if (document.getElementById("<%=Me.rdbNo.ClientID%>").checked && vlo_Textbox.value ==  "") {
                    vlo_Textbox.style.backgroundColor = "#F5838A";
                    source.errormessage = "Las observaciones son obligatorias cuando no se realizó el trabajo correctamente";
                    return clientside_arguments.IsValid=false;
                }
                else {
                    return clientside_arguments.IsValid=true;
                }
            }
            else
            {                  
                return clientside_arguments.IsValid=false;
            }

        };        

        function inicializarFormulario(){
            $('#btnCancelar').click(function () {
                regresarAlListado();
            });
            
            configurarLongitudMaximaTexto('#<%=Me.txtObservaciones.ClientID%>', <%=Utilerias.OrdenesDeTrabajo.Modelo.OTT_TRAZABILIDAD_PROCESO.OBSERVACIONES_BD_TAMANO%>);
            configurarContadorCaracteresRestantes('#<%=Me.txtObservaciones.ClientID%>', '#spContadorTxtObservaciones');
                        
        };

        $(document).ready(function () {
            inicializarFormulario();
        });
        
    </script>
</asp:Content>

