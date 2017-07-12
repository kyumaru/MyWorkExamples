<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/Mp_Formulario.master" AutoEventWireup="false" CodeFile="Frm_OT_UnidadMedida.aspx.vb" Inherits="Catalogos_Frm_OT_UnidadMedida" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphFormulario" Runat="Server">
        
    <header>
        <h2>
            <asp:Label runat="server" ID="lblAccion"></asp:Label>
        </h2>
    </header>

    <article class="tituloSeccion">
        Mantenimiento de Unidades de Medida para Materiales
    </article>

    <article id="Article1" class="formulario" runat="server">
        <table>
            <tr>
                <th>Descripción</th>
                <td>
                    <asp:TextBox Width="40%" runat="server" ID="txtDescripcion" data-tipocontrol="texto" MaxLength="100"></asp:TextBox>
                    <span id="spContadortxtDescripcion" class="contadorCaracteresRestantes"></span>
                    <asp:RequiredFieldValidator runat="server" ID="rfvTxtDescripcion" ControlToValidate="txtDescripcion" Display="Dynamic" ErrorMessage="Descripción es obligatoria." ValidationGroup="Aceptar"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <th>Acrónimo</th>
                <td>
                    <asp:TextBox Width="40%" runat="server" ID="txtAcronimo" data-tipocontrol="texto" MaxLength="8"></asp:TextBox>
                    <span id="spContadortxtAcronimo" class="contadorCaracteresRestantes"></span>
                    <asp:RequiredFieldValidator runat="server" ID="rfvTxtAcronimo" ControlToValidate="txtAcronimo" Display="Dynamic" ErrorMessage="Acronimo es obligatorio." ValidationGroup="Aceptar"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr runat="server" id="trEstado">
                <th>Estado</th>
                <td>
                    <asp:DropDownList Width="40%" runat="server" ID="ddlEstado"></asp:DropDownList>                    
                </td>
            </tr>
        </table>
    </article>

    <article class="areaBotones">
        <asp:Button runat="server" ID="btnAceptar" Text="Aceptar" ValidationGroup="Aceptar" />
        <input id="btnLimpiarFormulario" type="button" data-tipo="limpiarFormulario" value="Limpiar formulario" />
        <input id="btnCancelar" type="button" value="Cancelar" />
    </article>

    <article id="arPopupDelFormulario"></article>
    <article id="arAlertasDelFormulario"></article>
    <article id="arPopupGenerico"></article>

    <script type="text/javascript">

        $(document).ready(function () {

            $('#btnCancelar').click(function () {
                regresarAlListado();
            });

            configurarLongitudMaximaTexto('#<%=Me.txtDescripcion.ClientID%>',<%=Utilerias.OrdenesDeTrabajo.Modelo.OTM_UNIDAD_MEDIDA.DESCRIPCION_BD_TAMANO%>);
            configurarContadorCaracteresRestantes('#<%=Me.txtDescripcion.ClientID%>','#spContadortxtDescripcion');      

            configurarLongitudMaximaTexto('#<%=Me.txtAcronimo.ClientID%>',<%=Utilerias.OrdenesDeTrabajo.Modelo.OTM_UNIDAD_MEDIDA.ACRONIMO_BD_TAMANO%>);
            configurarContadorCaracteresRestantes('#<%=Me.txtAcronimo.ClientID%>','#spContadortxtAcronimo');      

        });
        
        function MensajePopup(pvc_Mensaje, pvc_PaginaDestino) {

            var vlo_ConfiguracionPopup = {
                titulo: '<em>Mensajes del Sistema</em>',
                mensaje: pvc_Mensaje,
                onClosed: function (e) {
                    $(this).removeAttr('href');
                    cerrarPopup();
                    if (pvc_PaginaDestino != '') {
                        redireccionarListado(pvc_PaginaDestino);
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
                            redireccionarListado(pvc_PaginaDestino);
                        }
                    }
                }
            ]
            };

            $('#arPopupGenerico').popup(vlo_ConfiguracionPopup);

            window.location = '#arPopupGenerico';
        };


        function redireccionarListado(pvc_PaginaDestino) {
            window.location = pvc_PaginaDestino
        };

        function regresarAlListado() {
            window.location = 'Lst_OT_UnidadMedida.aspx';
        };

        function mostrarPopupRegistroExitoso() {
            var vlo_ConfiguracionPopup = {
                titulo: 'Catálogo de Unidades de Medida',
                mensaje: 'Se ha registrado la información.. <br/><strong>¿Desea registrar otra unidad de medida?</strong>',
                onClosed: function () { regresarAlListado(); },
                botones:
                        [
                            {
                                idControl: "btnSi",
                                textoBoton: "Sí",
                                onClick: function () { window.location = 'Frm_OT_UnidadMedida.aspx?pvn_Operacion=<%=Utilerias.OrdenesDeTrabajo.eOperacion.Agregar%>'; }
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

        function mostrarAlertaActualizacionExitosa() {
            deshabilitarControl('#<%=btnAceptar.ClientID%>');
            deshabilitarControl('#btnLimpiarFormulario');
            deshabilitarControl('#btnCancelar');
            $('.formulario').attr('disabled', 'disabled');

            mostrarAlerta(
                '#arAlertasDelFormulario',
                {
                    mensaje: 'Se ha actualizado la información del registro',
                    tipo: "exito",
                    transparencia: 0.9,
                    posicion: 'center',
                    onClosed: function () { regresarAlListado(); }
                });
        };

        function mostrarAlertaLlaveIncorrecta() {
            deshabilitarControl('#<%=btnAceptar.ClientID%>');
            deshabilitarControl('#btnLimpiarFormulario');
            deshabilitarControl('#btnCancelar');
            $('.formulario').attr('disabled', 'disabled');

            mostrarAlerta(
                '#arAlertasDelFormulario',
                {
                    mensaje: 'El identificador provisto no pertenece a ningun registro.',
                    tipo: "peligro",
                    transparencia: 0.9,
                    posicion: 'center',
                    onClosed: function () { regresarAlListado(); }
                });
        };

        function mostrarPopUp(pvc_IdPopup) {
            window.location = pvc_IdPopup;
        };

    </script>

</asp:Content>

