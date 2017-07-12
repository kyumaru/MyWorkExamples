<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/Mp_Formulario.master" AutoEventWireup="false" CodeFile="Frm_OT_ExclusionMaterialesGestion.aspx.vb" Inherits="OrdenesDeTrabajo_GestionesDeCompra_Frm_OT_ExclusionMaterialesGestion" %>

<%@ Register TagPrefix="wuc" Namespace="Utilerias.WebForms.Controles" Assembly="Utilerias.WebForms.Controles" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphFormulario" Runat="Server">
    <header>
        <h2>
            <asp:Label runat="server" ID="lblAccion" Text="Registro de Ingreso de Material a Almacén"></asp:Label>
        </h2>
    </header>

    <article class="tituloSeccion">
        Datos de la gestión
    </article>

    <article data-grupo="Formulario" class="Listado">
        <table>
            <tr>
                <th style="width: 15%">Vía de Compra</th>
                <td style="width: 65%">
                    <asp:Label runat="server" ID="lblViaCompra"></asp:Label>
                </td>
            </tr>
            <tr>
                <th style="width: 15%">N° de Gestión</th>
                <td style="width: 65%">
                    <asp:Label runat="server" ID="lblNumeroGestion"></asp:Label>
                </td>
            </tr>
            <tr>
                <th style="width: 15%">Fecha</th>
                <td style="width: 65%">
                    <asp:Label runat="server" ID="lblFecha"></asp:Label>
                </td>
            </tr>
            <tr>
                <th style="width: 15%">Proveedor</th>
                <td style="width: 65%">
                    <asp:LinkButton runat="server" ID="lnkArchivo" Style="text-decoration: underline;"></asp:LinkButton>
                </td>
            </tr>
            <tr>
                <th style="width: 15%">Observaciones</th>
                <td style="width: 65%">
                    <asp:TextBox runat="server" ID="txtObservaciones" Width="100%" TextMode="MultiLine" Rows="9" ReadOnly="true" data-tipocontrol="texto"></asp:TextBox>
                </td>
            </tr>
        </table>
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
                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr class="lineaDelListado">
                            <td>
                                <asp:ImageButton runat="server" class="tooltip" ToolTip="Ver detalle" ID="ibConsultarGestion" AlternateText="Ver detalle" OnClick="ibConsultarGestion_Click" CommandArgument='<%#String.Format("{0}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_LINEA_GC_GROUP_FONDO.ID_MATERIAL))%>'
                                    src='<%#AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Check.png")%>'
                                    onmouseover='<%# String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Icono_Check.png"))%>' onmouseout='<%# String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Check.png"))%>' />
                            </td>
                            <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_LINEA_GC_GROUP_FONDO.ID_MATERIAL)%></td>
                            <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_LINEA_GC_GROUP_FONDO.DESCRIPCION)%></td>
                            <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_LINEA_GC_GROUP_FONDO.CANTIDAD_RESTANTE)%></td>
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
                                <th>&nbsp;</th>
                                <th>N° OT</th>
                                <th>Código</th>
                                <th>Descripción</th>
                                <th>Cantidad Requerida</th>
                                <th>Observaciones</th>
                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr class="lineaDelListado">
                            <td>
                                <asp:CheckBox runat="server" ID="chkDetalle"
                                    Checked='<%#IIf(CType(Eval("SELECCIONADO"), String) = "0", False, True)%>'
                                    AutoPostBack="true" OnCheckedChanged="chkDetalle_CheckedChanged"
                                    CommandArgument='<%# String.Format("{0}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_LINEA_GESTION_COMPRALST.ID_LINEA_GESTION_COMPRA))%>' />
                                <asp:HiddenField runat="server" ID="hdfIdDetalleMaterial" Value="<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_LINEA_GESTION_COMPRALST.ID_LINEA_GESTION_COMPRA)%>" />
                            </td>
                            <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_LINEA_GESTION_COMPRALST.NUMERO_OT)%></td>
                            <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_LINEA_GESTION_COMPRALST.ID_MATERIAL_TABLA)%></td>
                            <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_LINEA_GESTION_COMPRALST.DESCRIPCION)%></td>
                            <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_LINEA_GESTION_COMPRALST.CANTIDAD_RESTANTE)%></td>
                            <td style="width: 20%; text-align: center;">
                                <span style='<%#String.Format("display:{0};", IIf(Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_LINEA_GESTION_COMPRALST.OBSERVACIONES).ToString = "" Or Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_LINEA_GESTION_COMPRALST.OBSERVACIONES).ToString = "-", "none", "block"))%>'>
                                    <img id="ImgObservacion" class="tooltip" data-tipo="ObservacionRegistro" scr="" title="<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_LINEA_GESTION_COMPRALST.OBSERVACIONES)%>" />
                            </td>
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

    <article class="areaBotones">
        <asp:Button runat="server" ID="btnExcluir" Text="Excluir seleccionados" />
    </article>

    <br />
    <article class="tituloSeccion">
        Materiales Excluidos
    </article>

    <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Conditional">
        <ContentTemplate>
            <article class="listado sinBorde">
                <asp:Repeater runat="server" ID="rpEncaExc">
                    <HeaderTemplate>
                        <table>
                            <tr>
                                <th>&nbsp;</th>
                                <th>Código</th>
                                <th>Descripción</th>
                                <th>Cantidad Requerida</th>
                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr class="lineaDelListado">
                            <td>
                                <asp:ImageButton runat="server" class="tooltip" ToolTip="Ver detalle" ID="ibConsultarGestionExc" AlternateText="Ver detalle" OnClick="ibConsultarGestionExc_Click" CommandArgument='<%#String.Format("{0}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_LINEA_GC_GROUP_FONDO.ID_MATERIAL))%>'
                                    src='<%#AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Check.png")%>'
                                    onmouseover='<%# String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Icono_Check.png"))%>' onmouseout='<%# String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Check.png"))%>' />
                            </td>
                            <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_LINEA_GC_GROUP_FONDO.ID_MATERIAL)%></td>
                            <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_LINEA_GC_GROUP_FONDO.DESCRIPCION)%></td>
                            <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_LINEA_GC_GROUP_FONDO.CANTIDAD_RESTANTE)%></td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>


                <asp:Repeater runat="server" ID="rpDetaExc">
                    <HeaderTemplate>
                        <table>
                            <tr>
                                <th>N° OT</th>
                                <th>Código</th>
                                <th>Descripción</th>
                                <th>Cantidad Requerida</th>
                                <th>Observaciones</th>
                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr class="lineaDelListado">
                            <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_LINEA_GESTION_COMPRALST.NUMERO_OT)%></td>
                            <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_LINEA_GESTION_COMPRALST.ID_MATERIAL_TABLA)%></td>
                            <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_LINEA_GESTION_COMPRALST.DESCRIPCION)%></td>
                            <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_LINEA_GESTION_COMPRALST.CANTIDAD_RESTANTE)%></td>
                            <td style="width: 20%; text-align: center;">
                                <span style='<%#String.Format("display:{0};", IIf(Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_LINEA_GESTION_COMPRALST.OBSERVACIONES).ToString = "" Or Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_LINEA_GESTION_COMPRALST.OBSERVACIONES).ToString = "-", "none", "block"))%>'>
                                    <img id="ImgObservacion" class="tooltip" data-tipo="ObservacionRegistro" scr="" title="<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_LINEA_GESTION_COMPRALST.OBSERVACIONES)%>" />
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>

            </article>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="rpEncaExc" />
            <asp:PostBackTrigger ControlID="btnExcluir" />
        </Triggers>
    </asp:UpdatePanel>

    <article class="areaBotones">
        <asp:Button runat="server" ID="btnAceptar" Text="Aceptar" />
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
            deshabilitarControl('#<%=btnAceptar.ClientID%>');
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
            deshabilitarControl('#<%=btnAceptar.ClientID%>');
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

        function mostrarAlertaExitosa() {
            deshabilitarControl('#<%=btnAceptar.ClientID%>');
            deshabilitarControl('#<%=btnRegresar.ClientID%>');
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

