<%@ Page Title="Catálogo de Lugares de Ubicación" Language="VB" MasterPageFile="~/MasterPage/Mp_Formulario.master" AutoEventWireup="false" CodeFile="Frm_OT_TipoLugarUbicacion.aspx.vb" Inherits="Catalogos_Frm_OT_TipoLugarUbicacion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphFormulario" Runat="Server">
    <header>
        <h2>
            <asp:Label runat="server" ID="lblAccion"></asp:Label></h2>
    </header>
    <article class="tituloSeccion">
        Datos del Lugar de Ubicación
    </article>
    <article class="formulario">
        <table>
            <tr>
                <th>Descripción</th>
                <td>
                    <asp:TextBox runat="server"  ID="txtDescripcion" Width="70%" data-tipoControl="texto"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ID="rfvTxtDescripcion" ControlToValidate="txtDescripcion" Display="Dynamic" ErrorMessage="La descripción del lugar de ubicación es requerido.">&nbsp;</asp:RequiredFieldValidator>
                    <br />
                    <span id="spContadorTxtDescripcion" class="contadorCaracteresRestantes"></span>
                </td>
            </tr>
            <tr runat="server" ID="trEstado" >
                <th>Estado</th>
                <td>
                    <asp:DropDownList Width="70%" runat="server" ID="ddlEstado" AppendDataBoundItems="true" data-tipoControl="combo"></asp:DropDownList>
                    <asp:RequiredFieldValidator runat="server" ID="rfvDdlEstado" ControlToValidate="ddlEstado" Display="Dynamic" ErrorMessage="Debe seleccionar el estado.">&nbsp;</asp:RequiredFieldValidator>

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
            window.location = 'Lst_OT_TipoLugarUbicacion.aspx';
        }

        function deshabilitarFormulario() {
            deshabilitarControl('#<%=Me.btnAceptar.ClientID%>');
            deshabilitarControl('#btnLimpiarFormulario');
            deshabilitarControl('#btnCancelar');

            $('.formulario').attr('disabled','disabled');
        }

        function mostrarPopupRegistroExitoso () {
            var vlo_ConfiguracionPopup = {
                titulo: 'Catálogo de Lugares de Ubicacion',
                mensaje: 'Se a registrado la informacion del lugar de ubicación.<br/><strong>Desea registrar otro lugar de ubicación?</strong>',
                botones:
                    [  //en corchetes indican arreglos y los parentesis cuadrados indican inicio o fin de funcionalidad o objeto, $librerias de jquery
                        {
                            idControl: "btnSi",
                            textoBoton: "Si",
                            onClick:
                                function (e) {
                                    window.location = 'Frm_OT_TipoLugarUbicacion.aspx?pvn_Operacion=<%= Utilerias.OrdenesDeTrabajo.eOperacion.Agregar%>'; //pvn_Operacion????
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
                                mensaje: 'Se ha actualizado la información del lugar de ubicación',
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
                                mensaje: 'El número de identificación provisto no pertenece a ningun lugar de ubicación registrado en el sistema',
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

                        configurarLongitudMaximaTexto('#<%=Me.txtDescripcion.ClientID%>', <%=Utilerias.OrdenesDeTrabajo.Modelo.OTM_TIPO_LUGAR_UBICACION.DESCRIPCION_BD_TAMANO%>);
                        configurarContadorCaracteresRestantes('#<%=Me.txtDescripcion.ClientID%>', '#spContadorTxtDescripcion');

                    });
    </script>
</asp:Content>

