<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/Mp_Formulario.master" AutoEventWireup="false" CodeFile="Frm_OT_MatrizOrdenTrabajo.aspx.vb" Inherits="OrdenesDeTrabajo_Frm_OT_MatrizOrdenTrabajo" %>

<%@ Register TagPrefix="wuc" Namespace="Utilerias.WebForms.Controles" Assembly="Utilerias.WebForms.Controles" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphFormulario" runat="Server">

    <header>
        <h2>
            <asp:Label runat="server" ID="lblAccion" Text="Descripción de requerimientos de los espacios principales"></asp:Label>
        </h2>
    </header>

    <article class="tituloSeccion">
        Mantenimiento de la Orden de Trabajo
    </article>

    <article class="listado" style="overflow-x: auto !important;">
        <br />
        <asp:Table ID="tContenido" runat="server"></asp:Table>
        <asp:HiddenField runat="server" ID="hdfCadenaMatriz"/>
    </article>

    <article class="areaBotones">
        <asp:Button runat="server" ID="btnProcesar" Text="Guardar" OnClientClick="GuardarValoresMatriz()"/>
        <asp:Button runat="server" ID="btnProcesarEnviar" Text="Guardar y Enviar" OnClientClick="GuardarValoresMatriz()"/>
        <asp:Button runat="server" ID="btnRegresar" Text="Regresar"/>
        <%--<input id="btnSiguiente" type="button" value="Siguiente" />--%>
        <input id="btnCancelar" type="button" value="Cancelar" />
    </article>

    <article id="arPopupGenerico"></article>
    <article id="arAlerta"></article>

    <script type="text/javascript">

        function mostrarAlertaError(pvc_Mensaje) {
            mostrarAlerta(
                '#arAlerta',
                {
                    mensaje: pvc_Mensaje,
                    tipo: 'peligro',
                    transparencia: 0.9,
                    posicion: 'center'
                }
            );
        };

        function valideKey(evt) {
            var code = (evt.which) ? evt.which : evt.keyCode;
            if (code == 8) {
                //BORRAR
                return true;
            }
            else if (code >= 48 && code <= 57) {
                //VALOR NUMERICO
                return true;
            }
            else {
                return false;
            }
        };

        function MensajePopup(pvc_Mensaje, pvc_PaginaDestino) {

            var vlo_ConfiguracionPopup = {
                titulo: '<em>Mensajes del Sistema</em>',
                mensaje: pvc_Mensaje,
                onClosed: function (e) {
                    $(this).removeAttr('href');
                    cerrarPopup();
                    if (pvc_PaginaDestino != '') {
                        redireccionar(pvc_PaginaDestino);
                    }
                },

                botones:
            [
                {
                    idControl: "btnAceptar",
                    textoBoton: "Aceptar",
                    onClick: function () {
                        cerrarPopup();
                        if (pvc_PaginaDestino != '') {
                            redireccionar(pvc_PaginaDestino);
                        }
                    }
                }
            ]
            };

            $('#arPopupGenerico').popup(vlo_ConfiguracionPopup);

            window.location = '#arPopupGenerico';
        };

        function redireccionar(pvc_PaginaDestino) {
            window.location = pvc_PaginaDestino
        };

        function mostrarAlertaActualizacionExitosa() {
            deshabilitarControl('#btnCancelar');

            mostrarAlerta(
                '#arPopupGenerico',
                {
                    mensaje: 'Los detalles han sido registrados de manera exitosa.',
                    tipo: "exito",
                    transparencia: 0.9,
                    posicion: 'center',
                    onClosed: function () { regresarAlListado(); }
                });
        };

        function mostrarAlertaDatosFaltantes() {

            mostrarAlerta(
                '#arPopupGenerico',
                {
                    mensaje: 'Debe de completar al menos un campo de la Matriz',
                    tipo: "advertencia",
                    transparencia: 0.9,
                    posicion: 'center'
                });
        };

        $(document).ready(function () {
            $('#btnCancelar').click(function () {
                regresarAlListado();
            });

            $('#btnSiguiente').click(function () {
                GuardarValoresMatriz();
            });

            $('[data-tipo="tooltip"]').each(function () {
                $('#' + this.id).tooltipster({
                    interactive: true,
                    interactiveTolerance: 400,
                    theme: 'tooltipster-light'
                });
            });
        });
       
        function GuardarValoresMatriz() {
            var CadenaValoresMatriz = "";
            var vlo_Tabla = document.getElementById('<%=tContenido.ClientID%>');

            for (i = 3; i < vlo_Tabla.rows.length; i++) {

                for (j = 0; j < <%=Me.CantidadDeCeldas%>; j++) {

                    if (vlo_Tabla.rows[i].cells[j+2].firstChild.value == "") {
                        vlo_Tabla.rows[i].cells[j+2].firstChild.value = "¬"
                    }

                    if (CadenaValoresMatriz == ""){
                        CadenaValoresMatriz = vlo_Tabla.rows[i].cells[j+2].firstChild.value;
                    }else{
                        CadenaValoresMatriz = CadenaValoresMatriz + "," + vlo_Tabla.rows[i].cells[j+2].firstChild.value;
                    }                      
                }
            }  

            document.getElementById('<%=hdfCadenaMatriz.ClientID%>').value = CadenaValoresMatriz;
            
            return true;
        };

        function regresarAlListado() {
            window.location = '<%=Me.PantallaRetorno%>';
        }; 

    </script>

</asp:Content>

