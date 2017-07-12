<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/Mp_Formulario.master" AutoEventWireup="false" CodeFile="Frm_OT_TrasladoDeMaterialABodega.aspx.vb" Inherits="OrdenesDeTrabajo_Almacen_Frm_OT_TrasladoDeMaterialABodega" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register TagPrefix="wuc" TagName="DatosMaterial" Src="~/Controles/wuc_OT_DatosMaterial.ascx" %>
<%@ Register TagPrefix="wuc" TagName="Materiales" Src="~/Controles/wuc_OT_Materiales.ascx" %>
<%@ Register TagPrefix="wuc" Namespace="Utilerias.WebForms.Controles" Assembly="Utilerias.WebForms.Controles" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphFormulario" runat="Server">

    <header>
        <h2>Solicitud de Traslado de Material a 
            <asp:Label runat="server" ID="lblNombreBodega"></asp:Label>
        </h2>
    </header>

    <article class="tituloSeccion">
        Traslado de Material a Bodega
    </article>

    <asp:UpdatePanel runat="server" ID="upPanelMateriales">

        <ContentTemplate>
            <article class="formulario">
                <table>
                    <tr>
                        <th> <asp:Label runat="server" ID="lblEtiquetaEncargado" Text="Encargado"></asp:Label></th>
                        <td>
                            <asp:Label runat="server" ID="lblNombreEncargado"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <th>Código</th>
                        <td>
                            <asp:UpdatePanel runat="server" ID="upTxtCodigo" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:TextBox runat="server" ID="txtCodigo" data-tipocontrol="texto" AutoPostBack="true"></asp:TextBox>
                                    <asp:RequiredFieldValidator runat="server" ID="rfvTxtCodigo" ControlToValidate="txtCodigo"
                                        Display="Dynamic" ErrorMessage="El código del material es requerido." ValidationGroup="AgregarListado">&nbsp;</asp:RequiredFieldValidator>
                                    <ajax:FilteredTextBoxExtender ID="ftbTxtCodigo" runat="server" TargetControlID="txtCodigo" FilterType="Numbers"></ajax:FilteredTextBoxExtender>
                                    <asp:LinkButton ID="lnkEjecutarBusquedaMaterial" runat="server">
                            <img id="imgBuscarMaterial" title="Buscar Material" alt="Buscar Material" src="" />
                                    </asp:LinkButton>
                                    <br />
                                    <span id="spContadorTxtIdSolicitante" class="contadorCaracteresRestantes"></span>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="txtCodigo" EventName="TextChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </article>

            <asp:UpdatePanel runat="server" ID="upControlDatosMaterial" UpdateMode="Conditional">
                <ContentTemplate>
                    <wuc:DatosMaterial ID="WucDatosMaterial" runat="server"></wuc:DatosMaterial>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="txtCodigo" EventName="TextChanged" />
                    <asp:AsyncPostBackTrigger ControlID="ctrl_Materiales" />
                </Triggers>
            </asp:UpdatePanel>

            <article class="areaBotones">
                <asp:Button runat="server" ID="btnAgregarMaterial" Text="Agregar" ValidationGroup="AgregarListado" />
                <asp:Button runat="server" ID="btnModificarMaterial" Text="Modificar" Visible="false" />
                <asp:Button runat="server" ID="btnCancelarMaterial" Text="Cancelar" Visible="false" />
            </article>

            <br />
            <article class="tituloSeccion">
                Materiales Solicitados
            </article>
            <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Conditional">
                <ContentTemplate>
                    <%--acá utilizar vista de hizo cesar--%>
                    <article data-grupo="Listado" class="listado sinBorde">
                        <asp:Repeater runat="server" ID="rpPedidos">
                            <HeaderTemplate>
                                <table>
                                    <tr>
                                        <th>Código</th>
                                        <th>Descripción</th>
                                        <th>Detalle</th>
                                        <th>Disp. Almacen</th>
                                        <th>Cantidad Solicitada</th>
                                        <th>Cantidad Retirada</th>
                                        <th>Monto</th>
                                        <th>&nbsp;</th>
                                        <th>&nbsp;</th>
                                    </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_LINEA_TRASLADOLST.ID_MATERIAL)%></td>
                                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_LINEA_TRASLADOLST.DESCRIPCION)%></td>
                                    <td style="text-align: center">
                                        <asp:Image runat="server" ID="imgDetalle" data-tipo="tooltipDetalleMaterial"
                                            title='<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_LINEA_TRASLADOLST.DETALLE)%>'
                                            ImageUrl='<%#AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Informacion.png")%>' />
                                    </td>
                                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_LINEA_TRASLADOLST.CANTIDAD_DISPONIBLE)%></td>
                                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_LINEA_TRASLADOLST.CANTIDAD_REQUERIDA)%></td>
                                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_LINEA_TRASLADOLST.CANTIDAD_RETIRADA)%></td>
                                    <td style="text-align: right !important;"><%#CType(Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_LINEA_TRASLADOLST.COSTO_PROMEDIO), Double).ToString("N2")%></td>
                                    <td>
                                        <asp:ImageButton runat="server" data-tipo="modificarRegistro" ID="ibModificarMaterial" AlternateText="Modificar el pedido"                                           
                                            CommandArgument='<%#String.Format("{0}¬{1}¬{2}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_LINEA_TRASLADOLST.ID_MATERIAL), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_LINEA_TRASLADOLST.CANTIDAD_REQUERIDA), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_LINEA_TRASLADOLST.DETALLE))%>'
                                            OnClick="ibModificarMaterial_Click"
                                            ImageUrl='<%#AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Editar.png")%>'
                                            onmouseover='<%# String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Icono_Editar.png"))%>'
                                            onmouseout='<%# String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Editar.png"))%>' />
                                    </td>
                                    <td>
                                        <asp:ImageButton runat="server" ID="ibBorrarMaterial" AlternateText="Borrar el pedido" data-tipo="borrarRegistroMaterial"
                                            CommandArgument="<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_LINEA_TRASLADOLST.ID_MATERIAL)%>"
                                            OnClick="ibBorrarMaterial_Click"
                                            ImageUrl='<%#AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Borrar.png")%>'
                                            onmouseover='<%# String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Icono_Borrar.png"))%>'
                                            onmouseout='<%# String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Borrar.png"))%>' />
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
                    <asp:AsyncPostBackTrigger ControlID="btnAgregarMaterial" />
                    <asp:AsyncPostBackTrigger ControlID="btnModificarMaterial" />
                </Triggers>
            </asp:UpdatePanel>
            <article data-grupo="Listado" class="areaPaginadorListado">
            </article>

            <article data-grupo="Listado" class="areaCantidadDeRegistro">
                <asp:Label runat="server" ID="lblMontoTotal" Text=""></asp:Label>
                <br />
                <asp:Label runat="server" ID="lblCantidadDeRegistros" Text="" Visible="true"></asp:Label>
            </article>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnAgregarMaterial" />
            <asp:AsyncPostBackTrigger ControlID="btnModificarMaterial" />
        </Triggers>
    </asp:UpdatePanel>

    <br />

    <article class="formulario sinBorde">
        <table>
            <tr>
                <th>Fecha</th>
                <td>
                    <asp:TextBox runat="server" ID="txtFecha" data-tipocontrol="texto" placeholder="Fecha"></asp:TextBox>&nbsp;&nbsp;
                     <asp:RequiredFieldValidator runat="server" ID="rfvtxtFecha" ControlToValidate="txtFecha"
                        Display="Dynamic" ErrorMessage="La fecha es requerida." ValidationGroup="Aceptar">&nbsp;</asp:RequiredFieldValidator>
                   
                </td>
                  <td style="text-align: left; vertical-align: middle; font-weight: bold;">Jornada</td>
                <td>
                    <asp:RadioButtonList ID="rblJornada" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table">
                        <asp:ListItem Text="Mañana" Value="MAN"></asp:ListItem>
                        <asp:ListItem Text="Tarde" Value="TAR"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <th>Observaciones Generales</th>
                <td colspan="3">
                    <asp:TextBox runat="server" ID="txtObservacione" TextMode="MultiLine" Columns="70" Rows="9" Style="overflow: scroll"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ID="rfvTxttxtObservacione" ControlToValidate="txtObservacione"
                        Display="Dynamic" ErrorMessage="La observación es requerida." ValidationGroup="Aceptar">&nbsp;</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <th><asp:Label runat="server" ID="txtlblDevolucion" Text="Observaciones de la Devolución" ></asp:Label></th>
                <td colspan="3">
                    <asp:TextBox runat="server" ID="txtObservacionesDevolucion" TextMode="MultiLine" Rows="4" Columns="70"></asp:TextBox>

                </td>
            </tr>
        </table>
    </article>

    <article class="areaBotones">
        <asp:Button runat="server" ID="btnGuardar" ValidationGroup="Aceptar" Text="Guardar" Style="height: 26px" />
        <asp:Button runat="server" ID="btnEnviarAprobacion" ValidationGroup="Aceptar" Text="Enviar a aprobación" />
        <asp:Button runat="server" ID="btnRegresar" Text="Regresar" />
    </article>

       <asp:UpdatePanel runat="server" ID="UpdatePanel5" UpdateMode="Conditional" AutoPostBack="true">
        <ContentTemplate>
            <article style="visibility: hidden">
                <asp:TextBox runat="server" ID="txtMaterialValidacion" Text=""></asp:TextBox>

                <asp:RequiredFieldValidator ID="rfvtxtMaterialValidacion" runat="server" ControlToValidate="txtMaterialValidacion" Display="Dynamic" ErrorMessage="Es obligatorio al menos un Material." ValidationGroup="Aceptar">&nbsp;</asp:RequiredFieldValidator>
            </article>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="rpPedidos" />
            <asp:AsyncPostBackTrigger ControlID="btnAgregarMaterial" />
            <asp:AsyncPostBackTrigger ControlID="btnModificarMaterial" />
            <asp:AsyncPostBackTrigger ControlID="btnCancelarMaterial" />
        </Triggers>
    </asp:UpdatePanel>

    <article id="arAlerta"></article>
    <article id="arpopupConfirmaDeseaBorrar"></article>
    <article id="popupConfirmaDeseaBorrar"></article>
    <article id="popupConfirmacionDeseaBorrar"></article>
    <article id="arPopupGenerico"></article>
    <article id="arPopupDelFormulario"></article>
    <article id="arAlertasDelFormulario"></article>

    <%--Popup para búsqueda de materiales--%>
    <article id="PopUpBusquedaMateriales" class="ventanaEmergente">
        <article class="formulario" style="width: 850px!important;">
            <a href="#CerrarPopUpBusquedaMateriales" title="Cerrar Ventana" class="botonCerrarVentanaEmergente">X</a>
            <br />
            <asp:UpdatePanel ID="upContactoConv" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <wuc:Materiales ID="ctrl_Materiales" runat="server"></wuc:Materiales>
                    <div class="areaBotones">
                        <a href="#CerrarPopUpBusquedaMateriales" title="Regresar">Regresar</a>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </article>
    </article>
    <%--Fin Popup para búsqueda de materiales--%>

    <script type="text/javascript">
        $(document).ready(function () {
            inicializarFormulario();
            $('[data-tipo="tooltipDetalleMaterial"]').each(function () {
                //habilitarTooltipPorControl('#' + this.id);
                $('#' + this.id).tooltipster({
                    interactive: true,
                    interactiveTolerance: 400,
                    theme: 'tooltipster-light'
                });
            });
            $('[data-tipo="borrarRegistroMaterial"]').click(function () { return mostrarPopupConfirmacionDeseaBorrarRegistro($(this).data("uniqueid")); });
        });


        function inicializarFormulario() {
            $(window).keydown(function a(e) {

                if (e.keyCode == 13) {
                    return false;
                }
            });
            cargarLupa();
            $('[data-tipo="ObservacionRegistro"]').attr('src', '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Informacion.png")%>');
            $('[data-tipo="ObservacionRegistro"]').on({
                'mouseover': function () { $(this).attr('src', '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Icono_Informacion.png")%>'); },
                'mouseout': function () { $(this).attr('src', '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Informacion.png")%>'); }
            });
            configurarDatePicker("#<%=Me.txtFecha.ClientID%>");
            establecerMinyMaxDate()
        };


        //********* Data Picker************
        $("#datepicker").datepicker({
            inline: true
        });

        function establecerMinyMaxDate() {
            if (document.getElementById('<%=txtFecha.ClientID%>'))
                establecerFechaMinimaDatePicker("#<%=Me.txtFecha.ClientID%>", 'today');
        }
        //********* Data Picker************
        
        function InhabilitarCodigo() {

            document.getElementById("imgBuscarMaterial").style.visibility = "hidden";

        };

        function HabilitarCodigo() {

            document.getElementById("imgBuscarMaterial").style.visibility = "visible";

        };

        function mostrarPopupRegistroExitoso() {
            var vlo_ConfiguracionPopup = {
                titulo: 'Catálogo de Solicitud',
                mensaje: 'Se ha registrado la información.. <br/><strong>¿Desea registrar otra Solicitud?</strong>',
                onClosed: function () { regresarAlListado(); },
                botones:
                        [
                            {
                                idControl: "btnSi",
                                textoBoton: "Sí",
                                onClick: function () { window.location = 'Frm_OT_TrasladoDeMaterialABodega.aspx?pvn_Operacion=<%=Utilerias.OrdenesDeTrabajo.eOperacion.Agregar%>&pvn_IdBodega=<%=Me.IdBodega%>&pvn_IdUbicacion=<%=Me.IdUbicacion%>&pvc_IdEstado=<%=Utilerias.OrdenesDeTrabajo.EstadoTraslado.CREADA%>'; }
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

        function mostrarPopupConfirmacionDeseaBorrarRegistro(pvc_UniqueIdControl) {
            var vlo_ConfiguracionPopup = {
                titulo: "<em>Catalogo de requisición de materiales</em>",
                mensaje: '<%=String.Format("¿Está seguro de eliminar el material {0} del listado?",  Me.WucDatosMaterial.RetornaDescripcion)%>',
                botones:
                    [
                        {
                            idControl: "btnSi",
                            textoBoton: "Sí",
                            onClick:
                                function (e) {
                                    $(this).attr("disabled", "disabled");
                                    __doPostBack(pvc_UniqueIdControl, '');
                                }
                        },
                    {
                        idControl: "btnNo",
                        textoBoton: "No",
                        onClick:
                            function (e) { cerrarPopup(); }
                    },
                    {
                        idControl: "btnCancelar",
                        textoBoton: "Cancelar",
                        onClick:
                            function (e) { cerrarPopup(); }
                    }

                    ]
            };

            $('#popupConfirmacionDeseaBorrar').popup(vlo_ConfiguracionPopup);
            window.location = "#popupConfirmacionDeseaBorrar";

            return false;
        };

        function mostrarAlertaActualizacionExitosa() {
            deshabilitarControl('#<%=btnEnviarAprobacion.ClientID%>');
            deshabilitarControl('#<%=btnGuardar.ClientID%>');
            deshabilitarControl('#<%=btnRegresar.ClientID%>');
            $('.formulario').attr('disabled', 'disabled');

            mostrarAlerta(
            '#arAlertasDelFormulario',
            {
                mensaje: 'La información ha sido enviada a aprobación.',
                tipo: "exito",
                transparencia: 0.9,
                posicion: 'center',
                onClosed: function () { regresarAlListado(); }
            });
        };

        function regresarAlListado() {
            __doPostBack('<%=Me.btnRegresar.UniqueID%>', '');
        };

        function ocultarFiltroMateriales() {
            window.location = '#CerrarPopUpBusquedaMateriales';
            $('#PopUpBusquedaMateriales').hide();
        };

        function mostrarPopUpBusquedaFactura() {
            mostrarPopUp('#PopUpBusquedaMateriales')
            $('#PopUpBusquedaMateriales').show();
        }

        function mostrarPopUp(pvc_IdPopup) {
            window.location = pvc_IdPopup;
        };

        function cargarLupa() {
            permutarImagenes('#imgBuscarMaterial',
                '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Lupa.png")%>',
                '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Lupa.png")%>',
                '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Lupa.png")%>'
            );
        };

        function mostrarAlertSinCantidadDisponible() {
            mostrarAlerta(
                '#arAlertasDelFormulario',
                {
                    mensaje: "La bodega o almacén seleccionado no posee suficiente material",
                    tipo: "advertencia",
                    transparencia: 0.9,
                    posicion: 'center',
                    permiteCerrar: true
                }
                );
        };

        function mostrarAlertCantidadCero() {
            mostrarAlerta(
                '#arAlertasDelFormulario',
                {
                    mensaje: "La cantidad solicitada debe ser mayor a cero.",
                    tipo: "advertencia",
                    transparencia: 0.9,
                    posicion: 'center',
                    permiteCerrar: true
                }
                );
        };


        function mostrarAlertaNoEncontrado() {
            mostrarAlerta(
                '#arAlertasDelFormulario',
                {
                    mensaje: "Ningúna bodega o almacén poseen el código solicitado.",
                    tipo: "advertencia",
                    transparencia: 0.9,
                    posicion: 'center',
                    permiteCerrar: true
                }
                );
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

