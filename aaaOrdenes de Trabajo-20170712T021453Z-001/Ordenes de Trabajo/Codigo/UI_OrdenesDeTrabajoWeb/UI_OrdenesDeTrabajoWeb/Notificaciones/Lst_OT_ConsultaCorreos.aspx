<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Lst_OT_ConsultaCorreos.aspx.vb" MasterPageFile="~/MasterPage/Mp_Formulario.master" Inherits="Notificaciones_Lst_OT_ConsultaCorreos" %>

<%@ Register TagPrefix="wuc" Namespace="Utilerias.WebForms.Controles" Assembly="Utilerias.WebForms.Controles" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphFormulario" runat="Server">

    <header>
        <h2>Consulta de notificaciones</h2>
    </header>

    <article data-grupo="formulario" class="tituloSeccion">
        Consulta de notificaciones
    </article>

    <article data-grupo="formulario" class="formulario">

        <table>
            <tr>
                <th>Cuenta de correo
                </th>
                <td>
                    <asp:TextBox ID="txtCuentaCorreo" runat="server" data-tipocontrol="texto"></asp:TextBox>
                    <br />
                    <span id="spContadorTxtCuentaCorreo" class="contadorCaracteresRestantes"></span>
                    
                </td>
            </tr>
            <tr>
                <th>Asunto
                </th>
                <td>
                    <asp:TextBox ID="txtAsunto" runat="server" data-tipocontrol="texto"></asp:TextBox>
                    <br />
                    <span id="spContadorTxtAsunto" class="contadorCaracteresRestantes"></span>
                </td>
            </tr>
            <tr>
                <th>Fecha de Envío
                </th>
                <td>
                    <asp:TextBox ID="txtFecha" runat="server" data-tipocontrol="texto"></asp:TextBox>
                    <asp:CustomValidator runat="server" ID="cvFechaActual" ValidationGroup="Aceptar" ControlToValidate="txtFecha" Display="Dynamic" ErrorMessage="La fecha no puede ser mayor a la fecha actual." ClientValidationFunction="validarFechaActual">&nbsp;</asp:CustomValidator>
                    <asp:CompareValidator runat="server" ID="cmpvTxtFecha" ControlToValidate="txtFecha" Display="Dynamic" ValidationGroup="Aceptar" ErrorMessage="El formato de la fecha es incorrecto, el formato correcto es dd/mm/yyyy" Operator="DataTypeCheck" Type="Date">&nbsp;</asp:CompareValidator>

                </td>
            </tr>
            
        </table>
    </article>

    <article data-grupo="formulario" class="areaBotones">
        <asp:Button runat="server" ID="btnBuscar" Text="Buscar" ValidationGroup="Aceptar"/>
    </article>

    <article runat="server" id="arTitulo" data-grupo="Listado" class="tituloSeccion">
        Listado de Notificaciones
    </article>

    <article runat="server" id="arListado" data-grupo="Listado" class="listado">
        <asp:Repeater runat="server" ID="rpCorreos">
            <HeaderTemplate>
                <table>
                    <tr>
                        <th>&nbsp</th>
                        <th>
                            Destinatario
                        </th>
                        <th>
                            Asunto
                        </th>
                        <th>
                            Estado
                        </th>
                        <th>
                            Mensaje de error
                        </th>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr class="lineaDelListado">
                    <td>
                        <asp:ImageButton runat="server" ID="ibConsultar" AlternateText="Consultar" data-tipo="consultarRegistro" CommandArgument='<%# String.Format("{0},{1}", CType(Eval("ID_NOTIFICACION"), String), CType(Eval("ORIGEN"), String))%> ' OnClick="ibConsultar_Click" />
                    </td>
                    <td><%#Eval("DESTINATARIO")%></td>
                    <td><%#Eval("ASUNTO")%></td>
                    <td><%#Eval("DES_ESTADO")%></td>
                    <td><%#Eval("MENSAJE_ERROR")%></td>                    
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
    </article>
    <article runat="server" id="arPaginador" data-grupo="Listado" class="areaPaginadorListado">
        <wuc:PaginadorNumerico runat="server" ID="pnRpCorreos" /> 
    </article>
    <article data-grupo="Listado" class="areaCantidadDeRegistro">
        <asp:Label runat="server" ID="lblCantidadRegistro" Text=""> </asp:Label> 
    </article>

    <article data-grupo="Listado" class="Listado">
        <asp:Label ID="lblMensajeError" runat="server" ForeColor="#FF3300"></asp:Label>
    </article>
    
    <article style="visibility: hidden">
        <asp:TextBox ID="txtValidaciones" runat="server" Text="Validar"></asp:TextBox>
    </article>

    <asp:CustomValidator runat="server" ID="cvTxtValidaRequerido" ValidationGroup="Aceptar" ControlToValidate="txtValidaciones" Display="Dynamic" ErrorMessage="Debe indicar la cuenta de correo o la fecha de envío." ClientValidationFunction="validarRequerido">&nbsp;</asp:CustomValidator>

    <article id="arAlerta"></article>


    <script type="text/javascript">
        function mostrarAlertaNoHayDatos() {
            mostrarAlerta(
                '#arAlerta',
                {
                    mensaje: 'No se cuenta con correos que cumplan con la condición indicada',
                    tipo: "advertencia",
                    transparencia: 0.9,
                    posicion: 'top',
                    permiteCerrar: true
                }
                );

            ocultarAreaDeListado();
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

        function validarRequerido(source, clientside_arguments) {

            if (document.getElementById('<%=txtFecha.ClientID%>').value != '' || document.getElementById('<%=txtCuentaCorreo.ClientID%>').value != '') {

                return clientside_arguments.IsValid = true;
            }
            else {

                return clientside_arguments.IsValid = false;
            }
        }

        function validarFechaActual(source, clientside_arguments) {
            var vld_FechaActual = '<%=DateTime.Now.Date%>';
            if (document.getElementById('<%=txtFecha.ClientID%>').value > vld_FechaActual) {

                return clientside_arguments.IsValid = false;
            }
            else {

                return clientside_arguments.IsValid = true;
            }
        }

        $(document).ready(function () {
            $('[data-tipo="consultarRegistro"]').attr('src', '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Ver_Detalle.png")%>');
            $('[data-tipo="consultarRegistro"]').on({
                'mouseover': function () { $(this).attr('src', '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Icono_Ver_Detalle.png")%>'); },

                            'mouseout': function () { $(this).attr('src', '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Ver_Detalle.png")%>'); }
                        });

            configurarDatePicker('#<%=Me.txtFecha.ClientID%>');
            establecerFechaMaximaDatePicker('#<%=Me.txtFecha.ClientID%>', new Date());

            configurarLongitudMaximaTexto('#<%=Me.txtAsunto.ClientID%>', 150);
            configurarContadorCaracteresRestantes('#<%=Me.txtAsunto.ClientID%>', '#spContadorTxtAsunto');

            configurarLongitudMaximaTexto('#<%=Me.txtCuentaCorreo.ClientID%>', 100);
            configurarContadorCaracteresRestantes('#<%=Me.txtCuentaCorreo.ClientID%>', '#spContadorTxtCuentaCorreo');

            habilitarTooltipGenerico();

        });
    </script>
</asp:Content>

