<%@ Page Title="Catálogo de Parámetros del Sistema" Language="VB" MasterPageFile="~/MasterPage/Mp_Formulario.master" AutoEventWireup="false" CodeFile="Frm_OT_Parametros.aspx.vb" Inherits="Catalogos_Frm_OT_Parametros" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphFormulario" Runat="Server">
    <header>
        <h2>
            <asp:Label runat="server" ID="lblAccion"></asp:Label></h2>
    </header>
    <article class="tituloSeccion">
        Datos del Parámetro del Sistema
    </article>
    <article class="formulario">
        <table>
            <tr>
                <th>Descripción</th>
                <td>
                    <asp:TextBox runat="server"  ID="txtDescripcion" Width="97%" TextMode="MultiLine" Rows="2" data-tipoControl="texto"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ID="rfvTxtDescripcion" ControlToValidate="txtDescripcion" Display="Dynamic" ErrorMessage="La descripción del parámetro es requerida.">&nbsp;</asp:RequiredFieldValidator>
                    <br />
                    <span id="spContadorTxtDescripcion" class="contadorCaracteresRestantes"></span>
                </td>
            </tr>
            <tr>
                <th>Valor</th>
                <td>
                    <asp:TextBox runat="server"  ID="txtValor" Width="97%" data-tipoControl="texto"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ID="rfvTxtValor" ControlToValidate="txtValor" Display="Dynamic" ErrorMessage="El valor del parámetro es requerido.">&nbsp;</asp:RequiredFieldValidator>   
                    <asp:CustomValidator ID="cvtxtValor" runat="server" ControlToValidate="txtValor" ErrorMessage="Si la opción Valor Decimal se encuentra marcada solo se permiten números en este campo de texto" ClientValidationFunction="validartxtValor" SetFocusOnError="True">&nbsp;</asp:CustomValidator>
                    <br />
                    <span id="spContadorTxtValor" class="contadorCaracteresRestantes"></span>
                    <br />
                    <asp:CheckBox runat="server" ID="chkValorDecimal" text="Valor Decimal" data-tipoControl="checkbox"></asp:CheckBox>
                </td>
            </tr>
        </table>
    </article>

    <article class="areaBotones">
        <asp:Button runat="server" ID="btnAceptar" Text="Aceptar" />
        <input id="btnLimpiarFormulario" type="button" data-tipo="limpiarFormulario" value="Limpiar Formulario" />
        <input id="btnCancelar" type="button" value="Cancelar" />
    </article>

    <article id="arPopupDelFormulario"></article>
    <article id="arAlertasDelFormulario"></article>

    <script type="text/javascript">
        function regresarAlListado() {
            window.location = 'Lst_OT_Parametros.aspx';
        }

        function deshabilitarFormulario() {
            deshabilitarControl('#<%=Me.btnAceptar.ClientID%>');
            deshabilitarControl('#btnLimpiarFormulario');
            deshabilitarControl('#btnCancelar');

            $('.formulario').attr('disabled','disabled');
        }
        
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
        }

        function mostrarAlertaActualizacionExitosa() {
            deshabilitarFormulario();

            mostrarAlerta(
                '#arAlertasDelFormulario',
                {
                    mensaje: 'Se ha actualizado la información del parámetro',
                    tipo: "exito",
                    transparencia: 0.9,
                    posicion: 'center',
                    permiteCerrar: true,
                    onClosed: function () { regresarAlListado(); }
                }
                );
        }       

        function mostrarAlertaLlaveIncorrecta() {
            deshabilitarFormulario();

            mostrarAlerta(
                '#arAlertasDelFormulario',
                {
                    mensaje: 'El número de identificación provisto no pertenece a ningun parámetro registrado en el sistema',
                    tipo: "peligro",
                    transparencia: 0.9,
                    posicion: 'center',
                    permiteCerrar: true,
                    onClosed: function () { regresarAlListado(); }
                }
                );
        }

        function validartxtValor(source, clientside_arguments) {
            var vlo_ckbEsValorDecimal= document.getElementById('<%=chkValorDecimal.ClientID%>').checked;
            var vlc_Valor = document.getElementById(source.controltovalidate).value.trim();
            var vlo_Textbox = document.getElementById("<%=txtValor.ClientID%>");
            var vlc_ExpresionRegularPunto = /^[+-]?\d+(\.\d+)?$/;
            var vlc_ExpresionRegularComa = /^[+-]?\d+(\,\d+)?$/;
            var vlc_ValorSinPunto='';
            var vlc_CaracterComa=",";
            var vlc_CaracterPunto=".";
            var vln_CantDigitosEntero=13;
            var vln_DiferenciaDecimales=3;
            vlo_Textbox.style.backgroundColor = "white";

            if (vlc_ExpresionRegularComa.test(vlc_Valor)) {
                vlc_Valor = vlc_Valor.replace(vlc_CaracterComa,vlc_CaracterPunto);
                document.getElementById("<%=txtValor.ClientID%>").value  = vlc_Valor;
            }

            if (vlo_ckbEsValorDecimal && vlo_Textbox.value % 1 != 0) { //si esta chequeado que es decimal
                // si es un numero con solo una coma o un solo punto
                if(vlc_ExpresionRegularComa.test(vlc_Valor) || vlc_ExpresionRegularPunto.test(vlc_Valor)){
                    vlc_ValorSinPunto=vlc_Valor.replace(vlc_CaracterPunto,vlc_CaracterComa); //remplazamos el pusto por coma que es lo que inserta Oracle
                                
                   
                    // si no tiene coma entonces validamos que solo sean 11 numeros como maximo
                    if(vlc_ValorSinPunto.indexOf(vlc_CaracterComa) == -1 || vlc_ValorSinPunto.length > vln_CantDigitosEntero){
                        source.errormessage ="El número no puede tener más de "+vln_CantDigitosEntero+" dígitos";
                        vlo_Textbox.style.backgroundColor = "#F5838A";
                        return clientside_arguments.IsValid=false;
                       
                    }

                    //si tiene decimales validamos que maximo sean dos
                    if(vlc_ValorSinPunto.indexOf(vlc_CaracterComa) != -1 && vlc_ValorSinPunto.length - vlc_ValorSinPunto.indexOf(vlc_CaracterComa) > vln_DiferenciaDecimales){
                        source.errormessage ="El valor no puede tener más de dos decimales";
                        vlo_Textbox.style.backgroundColor = "#F5838A";
                        return clientside_arguments.IsValid=false;
                    }
                    return clientside_arguments.IsValid=true;
                }
                source.errormessage ="El valor decimal no es válido";
                vlo_Textbox.style.backgroundColor = "#F5838A";
                return clientside_arguments.IsValid=false;
               
            }

            return clientside_arguments.IsValid=true;           
        }

        $(document).ready(function () {
            $('#btnCancelar').click(function () {
                regresarAlListado();
            });

            configurarLongitudMaximaTexto('#<%=Me.txtDescripcion.ClientID%>', <%=Utilerias.OrdenesDeTrabajo.Modelo.OTP_PARAMETRO_UBICACION.DESCRIPCION_BD_TAMANO%>);
            configurarContadorCaracteresRestantes('#<%=Me.txtDescripcion.ClientID%>', '#spContadorTxtDescripcion');

            configurarLongitudMaximaTexto('#<%=Me.txtValor.ClientID%>', <%=Utilerias.OrdenesDeTrabajo.Modelo.OTP_PARAMETRO_UBICACION.VALOR_BD_TAMANO%>);
            configurarContadorCaracteresRestantes('#<%=Me.txtValor.ClientID%>', '#spContadorTxtValor');

        });
    </script>
</asp:Content>

