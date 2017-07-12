<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/Mp_Formulario.master" AutoEventWireup="false" CodeFile="Frm_OT_AdministrarRoles.aspx.vb" Inherits="Seguridad_Frm_OT_AdministrarRoles" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphFormulario" runat="Server">

    <header>
        <h2>Administración de Roles 
        </h2>
    </header>

    <article class="tituloSeccion">
        <asp:Label runat="server" ID="lblAccion" Text="Agregar nuevo rol"></asp:Label>
    </article>

    <article class="formulario">
        <table>
            <tr>
                <th>Rol</th>
                <td>
                    <asp:TextBox runat="server" ID="txtRole" data-tipocontrol="texto" Width="210px"></asp:TextBox>
                    <br />
                    <span id="spContadorTxtRole" class="contadorCaracteresRestantes"></span>
                    <asp:RequiredFieldValidator runat="server" ID="rfvTxtRole" ControlToValidate="txtRole" Display="Dynamic" ErrorMessage="El nombre del rol es requerido.">&nbsp;</asp:RequiredFieldValidator>
                </td>
            </tr>
        </table>
    </article>

    <article class="areaBotones">
        <asp:Button runat="server" ID="btnAceptar" Text="Aceptar" />
        <input id="btnLimpiarFormulario" type="button" data-tipo="limpiarFormulario" value="Limpiar formulario" />
        <input id="btnCancelar" type="button" title="Regresar" value="Regresar" />
    </article>

    <article id="arPopupDelFormulario"></article>
    <article id="arAlertasDelFormulario"></article>

    <script type="text/javascript">

        $(document).ready(function () {
            $('#btnCancelar').click(function () {
                regresarAlListado();
            });

            configurarLongitudMaximaTexto('#<%=Me.txtRole.ClientID%>', 256);
            configurarContadorCaracteresRestantes('#<%=Me.txtRole.ClientID%>', '#spContadorTxtRole');

        });

        function regresarAlListado() {
            window.location = 'Lst_OT_AdministrarRoles.aspx';
        };

        function mostrarPopupRegistroExitoso() {
            var vlo_ConfiguracionPopup = {
                titulo: 'Administración de Roles ',
                mensaje: 'Se ha registrado la información del rol. <br/><strong>¿Desea registrar otro rol?</strong>',
                onClosed: function () { regresarAlListado(); },
                botones:
                    [
                        {
                            idControl: "btnSi",
                            textoBoton: "Sí",
                            onClick: function () { window.location = 'Frm_OT_AdministrarRoles.aspx'; }
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

    </script>

</asp:Content>

