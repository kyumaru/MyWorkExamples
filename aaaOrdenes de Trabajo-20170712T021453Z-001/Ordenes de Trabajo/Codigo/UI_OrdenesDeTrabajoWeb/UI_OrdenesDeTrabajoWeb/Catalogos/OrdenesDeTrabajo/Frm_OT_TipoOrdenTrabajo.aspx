<%@ Page Title="Catálogo de Tipos de Ordenes de Trabajo" Language="VB" MasterPageFile="~/MasterPage/Mp_Formulario.master" AutoEventWireup="false" CodeFile="Frm_OT_TipoOrdenTrabajo.aspx.vb" Inherits="Catalogos_Frm_OT_TipoOrdenTrabajo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphFormulario" Runat="Server">
    <header>
        <h2>
            <asp:Label runat="server" ID="lblAccion"></asp:Label></h2>
    </header>
    <article class="tituloSeccion">
        Datos del Tipo de Orden de Trabajo
    </article>
    <article class="formulario">
        <table>
            <tr>
                <th>Codigo Orden de Trabajo</th>
                <td>
                    <asp:TextBox runat="server"  ID="txtCodigo" Width="97%" data-tipoControl="texto" ReadOnly="true"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ID="rfvTxtCodigo" ControlToValidate="txtCodigo"  Display="Dynamic" ErrorMessage="El código del tipo de orden es requerido.">&nbsp;</asp:RequiredFieldValidator>
                    <br />
                    <span id="spContadorTxtCodigo" class="contadorCaracteresRestantes"></span>
                </td>
            </tr>
            <tr>
                <th>Descripcion</th>
                <td>
                    <asp:TextBox runat="server"  ID="txtDescripcion" Width="97%" data-tipoControl="texto"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ID="rfvTxtDescripcion" ControlToValidate="txtDescripcion" Display="Dynamic" ErrorMessage="La descripción del tipo de orden es requerido.">&nbsp;</asp:RequiredFieldValidator>
                    <br />
                    <span id="spContadorTxtDescripcion" class="contadorCaracteresRestantes"></span>
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
            window.location = 'Lst_OT_TipoOrdenTrabajo.aspx';
        }

        function deshabilitarFormulario() {
            deshabilitarControl('#<%=Me.btnAceptar.ClientID%>');
            deshabilitarControl('#btnLimpiarFormulario');
            deshabilitarControl('#btnCancelar');

            $('.formulario').attr('disabled','disabled');
        }

        function mostrarPopupRegistroExitoso () {
            var vlo_ConfiguracionPopup = {
                titulo: 'Catálogo de Tipos de Ordenes de Trabajo',
                mensaje: 'Se a registrado la informacion del tipo de orden.<br/><strong>Desea registrar otro tipo?</strong>',
                botones:
                    [  //en corchetes indican arreglos y los parentesis cuadrados indican inicio o fin de funcionalidad o objeto, $librerias de jquery
                        {
                            idControl: "btnSi",
                            textoBoton: "Si",
                            onClick:
                                function (e) {
                                    window.location = 'Frm_OT_TipoOrdenTrabajo.aspx?pvn_Operacion=<%= Utilerias.OrdenesDeTrabajo.eOperacion.Agregar%>'; //pvn_Operacion????
                                }
                        },
                        {
                            idControl: "btnNo",
                            textoBoton: "No",
                            onClick: function (e) { regresarAlListado();}
                                
                        },
                        
                    ]
            };

                        $('#arPopupDelFormulario').popup(vlo_ConfiguracionPopup);
                        window.location = '#arPopupDelFormulario';
            
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
                                mensaje: 'Se ha actualizado la información del tipo de orden',
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
                                mensaje: 'El número de identificación provisto no pertenece a ningun tipo de orden registrado en el sistema',
                                tipo: "peligro",
                                transparencia: 0.9,
                                posicion: 'center',
                                permiteCerrar: true,
                                onClosed: function () { regresarAlListado(); }
                            }
                            );

                    }

                    $(document).ready(function () {
                        $('#btnCancelar').click(function () {
                            regresarAlListado();
                        });

                        configurarLongitudMaximaTexto('#<%=Me.txtCodigo.ClientID%>', <%=Utilerias.OrdenesDeTrabajo.Modelo.OTC_TIPO_ORDEN_TRABAJO.TIPO_ORDEN_TRABAJO_BD_TAMANO%>);
                        configurarContadorCaracteresRestantes('#<%=Me.txtCodigo.ClientID%>', '#spContadorTxtCodigo');

                        configurarLongitudMaximaTexto('#<%=Me.txtDescripcion.ClientID%>', <%=Utilerias.OrdenesDeTrabajo.Modelo.OTC_TIPO_ORDEN_TRABAJO.DESCRIPCION_BD_TAMANO%>);
                        configurarContadorCaracteresRestantes('#<%=Me.txtDescripcion.ClientID%>', '#spContadorTxtDescripcion');

                    });
    </script>
</asp:Content>

