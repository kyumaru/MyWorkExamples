<%@ Page Title="Catálogo de Ubicaciones" Language="VB" MasterPageFile="~/MasterPage/Mp_Formulario.master" AutoEventWireup="false" CodeFile="Frm_OT_Ubicaciones.aspx.vb" Inherits="Catalogos_Frm_OT_Ubicaciones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphFormulario" Runat="Server">
    <header>
        <h2>
            <asp:Label runat="server" ID="lblAccion"></asp:Label></h2>
    </header>
    <article class="tituloSeccion">
        Datos de la Ubicación
    </article>
    <article class="formulario">
        <table>
            <tr>
                <th>Nombre</th>
                <td>
                    <asp:TextBox runat="server"  ID="txtNombre" Width="70%" data-tipoControl="texto"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ID="rfvTxtNombre" ControlToValidate="txtNombre" Display="Dynamic" ErrorMessage="El nombre de la ubicación es requerido.">&nbsp;</asp:RequiredFieldValidator>
                    <br />
                    <span id="spContadorTxtNombre" class="contadorCaracteresRestantes"></span>
                </td>
            </tr>
            <tr>
                <th>Pertenece a sede</th>
                <td>
                    <asp:DropDownList Width="70%" runat="server" ID="ddlPerteneceSede" AppendDataBoundItems="true" data-tipoControl="combo"></asp:DropDownList>
                    <asp:RequiredFieldValidator runat="server" ID="rfvDdlPerteneceSede" ControlToValidate="ddlPerteneceSede" Display="Dynamic" ErrorMessage="Debe seleccionar si pertenece o no a una sede.">&nbsp;</asp:RequiredFieldValidator>

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
            window.location = 'Lst_OT_Ubicaciones.aspx';
        }

        function deshabilitarFormulario() {
            deshabilitarControl('#<%=Me.btnAceptar.ClientID%>');
            deshabilitarControl('#btnLimpiarFormulario');
            deshabilitarControl('#btnCancelar');

            $('.formulario').attr('disabled','disabled');
        }

        function mostrarPopupRegistroExitoso () {
            var vlo_ConfiguracionPopup = {
                titulo: 'Catálogo de Ubicaciones',
                mensaje: 'Se a registrado la informacion de la ubicación.<br/><strong>Desea registrar otra ubicación?</strong>',
                botones:
                    [  //en corchetes indican arreglos y los parentesis cuadrados indican inicio o fin de funcionalidad o objeto, $librerias de jquery
                        {
                            idControl: "btnSi",
                            textoBoton: "Si",
                            onClick:
                                function (e) {
                                    window.location = 'Frm_OT_Ubicaciones.aspx?pvn_Operacion=<%= Utilerias.OrdenesDeTrabajo.eOperacion.Agregar%>'; //pvn_Operacion????
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
                                mensaje: 'Se ha actualizado la información de la ubicación',
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
                                mensaje: 'El número de identificación provisto no pertenece a ninguna ubicación registrada en el sistema',
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

                        configurarLongitudMaximaTexto('#<%=Me.txtNombre.ClientID%>', <%=Utilerias.OrdenesDeTrabajo.Modelo.OTM_UBICACION.DESCRIPCION_BD_TAMANO%>);
                        configurarContadorCaracteresRestantes('#<%=Me.txtNombre.ClientID%>', '#spContadorTxtNombre');

                    });
    </script>
</asp:Content>

