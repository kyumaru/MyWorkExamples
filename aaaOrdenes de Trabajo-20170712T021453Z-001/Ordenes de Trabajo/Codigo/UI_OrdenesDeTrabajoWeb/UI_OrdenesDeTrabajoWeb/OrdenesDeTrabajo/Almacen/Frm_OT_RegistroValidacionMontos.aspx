<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/Mp_Formulario.master" AutoEventWireup="false" CodeFile="Frm_OT_RegistroValidacionMontos.aspx.vb" Inherits="OrdenesDeTrabajo_Almacen_Frm_OT_RegistroValidacionMontos" %>

<%@ Register TagPrefix="wuc" Namespace="Utilerias.WebForms.Controles" Assembly="Utilerias.WebForms.Controles" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphFormulario" runat="Server">
    <header>
        <h2>
            <asp:Label runat="server" ID="lblAccion" Text="Ingreso de Material a Almacén"></asp:Label>
        </h2>
    </header>

    <article class="tituloSeccion">
        Ingreso de Material
    </article>

    <article class="formulario">
        <table>
            <tr>
                <th>Vía de compra</th>
                <td>
                    <asp:Label runat="server" ID="lblViaCompra"></asp:Label>
                </td>
                <th runat="server" id="thProveedor">Proveedor</th>
                <td>
                    <asp:Label runat="server" ID="lblProveedor"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>N° de Gestión</th>
                <td>
                    <asp:Label runat="server" ID="lblNumeroGestion"></asp:Label>
                </td>
                <th>Fecha</th>
                <td>
                    <asp:Label runat="server" ID="lblFecha"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>Responsable</th>
                <td>
                    <asp:Label runat="server" ID="lblResponsable"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>Observaciones</th>
                <td>
                    <asp:TextBox runat="server" ID="txtObservaciones" Width="100%" TextMode="MultiLine" Rows="9" data-tipocontrol="texto" Enabled="false"></asp:TextBox>
                </td>
            </tr>
        </table>
    </article>

    <br />
    <article class="tituloSeccion">
        Documento
    </article>

    <article class="listado sinBorde">
        <asp:Repeater runat="server" ID="rpAdjunto">
            <HeaderTemplate>
                <table>
                    <tr>
                        <th>N° Documento</th>
                        <th>Archivo</th>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr class="lineaDelListado">
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.OTT_ADJUNTO_GESTION_INGR.NUMERO_DOCUMENTO)%></td>
                    <td>
                        <asp:LinkButton runat="server" ID="lnkArchivo"
                            CommandArgument='<%#Container.ItemIndex%>'
                            Style="text-decoration: underline; color: blue;"
                            OnCommand="lnkArchivo_Command"
                            Text="<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.OTT_ADJUNTO_GESTION_INGR.NOMBRE_ARCHIVO)%>"></asp:LinkButton>
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
    </article>

    <br />
    <article class="tituloSeccion">
        Materiales
    </article>

    <asp:UpdatePanel runat="server" ID="upCategoria" UpdateMode="Conditional">
        <ContentTemplate>
            <article class="listado sinBorde">
                <asp:Repeater runat="server" ID="rpEnca">
                    <HeaderTemplate>
                        <table>
                            <tr>
                                <th>&nbsp;</th>
                                <th>Código</th>
                                <th>Descripción</th>
                                <th>Cantidad Requerida</th>
                                <th>Cantidad Ingresada</th>
                                <th>Costo</th>
                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr class="lineaDelListado">
                            <td>
                                <asp:ImageButton runat="server" class="tooltip" ToolTip="Ver detalle" ID="ibConsultarGestion" AlternateText="Ver detalle" OnClick="ibConsultarGestion_Click" CommandArgument='<%#String.Format("{0}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_DET_GEST_ING_GROUP.ID_MATERIAL))%>'
                                    src='<%#AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Check.png")%>'
                                    onmouseover='<%# String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Icono_Check.png"))%>' onmouseout='<%# String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Check.png"))%>' />
                            </td>
                            <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_DET_GEST_ING_GROUP.ID_MATERIAL)%></td>
                            <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_DET_GEST_ING_GROUP.DESC_MATERIAL)%></td>
                            <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_DET_GEST_ING_GROUP.CANTIDAD_REQUERIDA)%></td>
                            <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_DET_GEST_ING_GROUP.CANTIDAD_INGRESADA)%></td>
                            <td>
                                <asp:TextBox runat="server" ID="txtCantidadEnca" Width="50%" Text='<%#Eval("COSTO_ING")%>' AutoPostBack="true" OnTextChanged="txtCantidadEnca_OnTextChanged" data-inf='<%#String.Format("{0}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_DET_GEST_ING_GROUP.ID_MATERIAL))%>'></asp:TextBox>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>


                <asp:Repeater runat="server" ID="rpDetalle">
                    <HeaderTemplate>
                        <table>
                            <tr>
                                <th>N° OT</th>
                                <th>Código</th>
                                <th>Descripción</th>
                                <th>Cantidad Requerida</th>
                                <th>Cantidad Ingresada</th>
                                <th>Costo</th>
                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr class="lineaDelListado">
                            <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_GESTION_INGRLST.NUMERO_OT)%></td>
                            <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_GESTION_INGRLST.ID_MATERIAL_TABLA)%></td>
                            <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_GESTION_INGRLST.DESC_MATERIAL)%></td>
                            <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_GESTION_INGRLST.CANTIDAD_REQUERIDA)%></td>
                            <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_GESTION_INGRLST.CANTIDAD_INGRESA)%></td>
                            <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DETALLE_GESTION_INGRLST.COSTO_INDIVIDUAL)%></td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>

            </article>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="rpEnca" />
        </Triggers>
    </asp:UpdatePanel>

    <br />

    <article class="formulario sinBorde">
        <table>
            <tr>
                <th>
                    <asp:CheckBox runat="server" ID="chkAsignar" AutoPostBack="true" class="tooltip" ToolTip="Devolver" Text="Devolver"  />
                </th>
                <td>Archivo</td>
            </tr>
            <tr>
                <th>Observaciones</th>
                <td><asp:TextBox runat="server" ID="txtObs" Width="100%" TextMode="MultiLine" Rows="9" data-tipocontrol="texto"></asp:TextBox></td>
            </tr>
        </table>
    </article>

    <article class="areaBotones">
        <asp:Button runat="server" ID="btnDevolver" Text="Devolver" />
        <asp:Button runat="server" ID="btnGuardar" Text="Guardar" />
        <asp:Button runat="server" ID="btnFinalizar" Text="Finalizar" />
        <asp:Button runat="server" ID="btnRegresar" Text="Regresar" />
    </article>

    <article id="arAlerta"></article>
    <article id="arpopupConfirmaDeseaBorrar"></article>
    <article id="popupConfirmaDeseaBorrar"></article>
    <article id="popupConfirmacionDeseaBorrar"></article>
    <article id="arPopupGenerico"></article>
    <article id="arPopupDelFormulario"></article>
    <article id="arAlertasDelFormulario"></article>

    <script type="text/javascript">

        function mostrarAlertaLlaveIncorrecta() {
            deshabilitarControl('#<%=btnFinalizar.ClientID%>');
            deshabilitarControl('#<%=btnRegresar.ClientID%>');
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

        function mostrarAlertaActualizacionExitosa() {
            deshabilitarControl('#<%=btnFinalizar.ClientID%>');
            deshabilitarControl('#<%=btnRegresar.ClientID%>');
            $('.formulario').attr('disabled', 'disabled');

            mostrarAlerta(
            '#arAlertasDelFormulario',
            {
                mensaje: 'La información ha sido actualizada exitosamente.',
                tipo: "exito",
                transparencia: 0.9,
                posicion: 'center',
                onClosed: function () { regresarAlListado(); }
            });
        };

        function mostrarAlertaExitosa() {
            deshabilitarControl('#<%=btnFinalizar.ClientID%>');
            deshabilitarControl('#<%=btnRegresar.ClientID%>');
            deshabilitarControl('#<%=btnGuardar.ClientID%>');
            $('.formulario').attr('disabled', 'disabled');

            var vlo_Boton = document.getElementById('<%=btnRegresar.ClientID%>');

            mostrarAlerta(
                '#arAlerta',
                {
                    mensaje: "Se ha tramitado la solicitud exitosamente",
                    tipo: "exito",
                    transparencia: 0.9,
                    posicion: 'top',
                    onClosed: vlo_Boton.click()
                }
            );
        }

        function mostrarGuardadoExitoso() {
            $('.formulario').attr('disabled', 'disabled');
            mostrarAlerta(
                '#arAlerta',
                {
                    mensaje: "Se ha guardado la información exitosamente.",
                    tipo: "exito",
                    transparencia: 0.9,
                    posicion: 'top'
                }
            );
        }

        function regresarAlListado() {
            __doPostBack('<%=Me.btnRegresar.UniqueID%>', '');
        };

        $(document).ready(function () {

            inicializarFormulario();

            $('[data-tipo="tooltipExtensiones"]').attr('src', '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Icono_Ayuda.png")%>');

            $('[data-tipo="borrarRegistro"]').click(function () { return mostrarPopupConfirmaDeseaBorrarRegistro($(this).data("uniqueid")); });

            $('[data-tipo="ObservacionRegistro"]').attr('src', '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Informacion.png")%>');
            $('[data-tipo="ObservacionRegistro"]').on({
                'mouseover': function () { $(this).attr('src', '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Icono_Informacion.png")%>'); },
                'mouseout': function () { $(this).attr('src', '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Informacion.png")%>'); }
            });

        });

        function mostrarPopupConfirmaDeseaBorrarRegistro(pvo_UniqueIdControl) {
            var vlo_ConfiguracionPopup = {
                titulo: 'Ingreso de Material a Almacén',
                mensaje: '¿Desea borrar el registro seleccionado?',
                botones:
                        [
                            {
                                idControl: "btnSi",
                                textoBoton: "Si",
                                onClick:
                                    function (e) {
                                        $(this).attr("disabled", "disabled");
                                        __doPostBack(pvo_UniqueIdControl, '');
                                    }
                            },
                            {
                                idControl: "btnNo",
                                textoBoton: "No",
                                onClick: function () { cerrarPopup(); }
                            },
                            {
                                idControl: "btnCancelar",
                                textoBoton: "Cancelar",
                                onClick: function () { cerrarPopup(); }
                            }
                        ]
            };

            $('#arpopupConfirmaDeseaBorrar').popup(vlo_ConfiguracionPopup);

            window.location = '#arpopupConfirmaDeseaBorrar';

            return false;
        };

        function mostrarPopUp(pvc_IdPopup) {
            window.location = pvc_IdPopup;
        };

        function inicializarFormulario() {

            $(window).keydown(function a(e) {

                if (e.keyCode == 13) {
                    return false;
                }
            });


            $('[data-tipo="tooltipExtensiones"]').attr('src', '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Icono_Ayuda.png")%>');
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

