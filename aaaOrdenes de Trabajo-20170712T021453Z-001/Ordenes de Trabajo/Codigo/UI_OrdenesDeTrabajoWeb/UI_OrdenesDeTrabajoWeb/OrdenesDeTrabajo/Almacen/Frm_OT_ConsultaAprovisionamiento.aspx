<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/Mp_Formulario.master" AutoEventWireup="false" CodeFile="Frm_OT_ConsultaAprovisionamiento.aspx.vb" Inherits="OrdenesDeTrabajo_Almacen_Frm_OP_ConsultaAprovisionamiento" %>

<%@ Register TagPrefix="wuc" Namespace="Utilerias.WebForms.Controles" Assembly="Utilerias.WebForms.Controles" %>
<%@ Register Src="~/Controles/wuc_OT_Lineas_Material_Detalle_Producto.ascx" TagName="wuc_OT_Lineas_Material_Detalle_Producto" TagPrefix="uc1" %>
<%@ Register Src="~/Controles/wuc_OT_Lineas_Material_Gestion_Compra.ascx" TagName="wuc_LineasMaterialGestionCompra" TagPrefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphFormulario" runat="Server">
    <header>
        <h2>Aprovisionamiento</h2>
    </header>

    <article class="tituloSeccion">
        Aprovisionamiento
    </article>

    <article class="formulario sinBorde">
        <table>
            <tr>
                <th>Vía de compra</th>
                <td>
                    <asp:DropDownList Width="40%" runat="server" ID="ddlViaCompra" AppendDataBoundItems="true" data-tipocontrol="combo"></asp:DropDownList>
                    <asp:RequiredFieldValidator runat="server" ID="rfvDdlTipo" ControlToValidate="ddlViaCompra" Display="Dynamic" ErrorMessage="La vía de compra es obligatoria." ValidationGroup="Aceptar"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <th>Observaciones</th>
                <td>
                    <asp:TextBox Width="40%" runat="server" ID="txtObservaciones"></asp:TextBox>
                </td>
            </tr>
        </table>
    </article>

    <article class="tituloSeccion" runat="server" id="trMatGestionar">
        Materiales a Gestionar
    </article>

    <br />
    <article data-grupo="Listado" class="listado">
        <asp:Repeater runat="server" ID="rpMateriales">
            <HeaderTemplate>
                <table>
                    <tr>
                        <th>Código</th>
                        <th>Descripción</th>
                        <th>Existencia</th>
                        <th>Disponible</th>
                        <th>Máximo Almacén</th>
                        <th>Punto de Reorden</th>
                        <th>Cantidad</th>
                        <th>Observaciones</th>
                        <th>Precio</th>
                        <th>&nbsp;</th>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr class="lineaDelListado">
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_MATERIALLST.ID_MATERIAL)%></td>
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_MATERIALLST.DESCRIPCION)%></td>
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_MATERIALLST.CANTIDAD_EXISTENCIA)%></td>
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_MATERIALLST.CANTIDAD_DISPONIBLE)%></td>
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_MATERIALLST.MAXIMO_ALMACEN)%></td>
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_MATERIALLST.PUNTO_REORDEN)%></td>
                    <td>
                        <asp:TextBox runat="server" ID="txtCantidad" Width="50%" Text='<%#Eval("CANTIDAD")%>' AutoPostBack="true" OnTextChanged="txtCantidad_OnTextChanged" data-inf='<%#String.Format("{0}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_MATERIALLST.ID_MATERIAL))%>'></asp:TextBox>

                    </td>
                    <td style="width: 8%;">
                        <asp:TextBox runat="server" ID="txtObs" Text='<%#Eval("OBSERVACIONES")%>' AutoPostBack="true" OnTextChanged="txtObs_OnTextChanged" data-inf='<%#String.Format("{0}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_MATERIALLST.ID_MATERIAL))%>'></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtPrecio" ToolTip='<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_MATERIALLST.COSTO_PROMEDIO)%>' ReadOnly="true" Width="80%" Text='<%#Eval("PRECIO")%>'></asp:TextBox></td>
                    <td>
                        <asp:ImageButton runat="server" ID="ibBorrar" AlternateText="Borrar" data-tipo="borrarRegistros" CommandArgument='<%#String.Format("{0}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_MATERIALLST.ID_MATERIAL))%>' OnClick="ibBorrar_Click" />
                    </td>


                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
    </article>
    <article class="areaCantidadDeRegistro" data-grupo="Listado">
        <asp:Label runat="server" ID="lblMontoTotal" Text=""></asp:Label>
    </article>
    <br />

    <article class="areaBotones">
        <asp:Button runat="server" ID="btnCancelar" />
        <asp:Button runat="server" ID="btnGuardar" Text="Guardar" ValidationGroup="Aceptar" />
        <asp:Button runat="server" ID="btnFinalizar" Text="Finalizar" ValidationGroup="Aceptar" />
    </article>

    <article id="arAlerta"></article>
    <article id="arpopupConfirmaDeseaBorrar"></article>
    <article id="arPopupGenerico"></article>
    <article id="arAlertasDelFormulario"></article>
    <article id="arpopupConfirmaEnviar"></article>

    <article style="visibility: hidden">
        <asp:LinkButton runat="server" ID="lnkAceptarAux"></asp:LinkButton>
    </article>

    <script type="text/javascript">

        function irASeleccion() {
            window.location = 'Frm_OT_Aprovisionamiento.aspx';
        };

        function regresarAlListado() {
            window.location = 'Lst_OT_Aprovisionamiento.aspx';
        };


        function mostrarAlertaError(pvc_Mensaje) {
            mostrarAlerta(
                '#arAlerta',
                {
                    mensaje: pvc_Mensaje,
                    tipo: 'peligro',
                    transparencia: 0.9,
                    posicion: 'center'
                }
            );
        };

        $(document).ready(function () {

            $('#<%=btnFinalizar.ClientID%>').click(function () { return mostrarPopupConfirmaDeseaEnviarRegistro(); });

            $('[data-tipo="borrarRegistros"]').attr('src', '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Borrar.png")%>');
            $('[data-tipo="borrarRegistros"]').on({
                'mouseover': function () { $(this).attr('src', '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Icono_Borrar.png")%>'); },
                'mouseout': function () { $(this).attr('src', '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Borrar.png")%>'); }
            });

            $('[data-tipo="borrarRegistros"]').click(function () { return mostrarPopupConfirmaDeseaBorrarRegistro($(this).data("uniqueid")); });

            habilitarTooltipGenerico();
        });

        function mostrarPopupConfirmaDeseaBorrarRegistro(pvo_UniqueIdControl) {
            var vlo_ConfiguracionPopup = {
                titulo: 'Aprovisionamiento',
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
        }

        function mostrarPopupConfirmaDeseaEnviarRegistro() {
            var EstadoGestion = '<%= Aprovisionamiento.Estado()%>';
            if (EstadoGestion = "CRE")
            {
                    var vlo_ConfiguracionPopup = {
                        titulo: 'Aprovisionamiento',
                        mensaje: 'Esta acción dará por concluida la selección de materiales para Gestionar la Orden, ¿Desea Continuar?',
                        botones:
                                    [
                                        {
                                            idControl: "btnSi",
                                            textoBoton: "Si",
                                            onClick:
                                                function (e) {
                                                    $(this).attr("disabled", "disabled");
                                                    var vlo_Boton = document.getElementById('<%=lnkAceptarAux.ClientID%>');
                                                vlo_Boton.click();
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

                                    $('#arpopupConfirmaEnviar').popup(vlo_ConfiguracionPopup);

                                    window.location = '#arpopupConfirmaEnviar';

                                    return false;
                                }
            else {
                var vlo_Boton = document.getElementById('<%=lnkAceptarAux.ClientID%>');
                vlo_Boton.click();

            };
        }




        function redireccionarListado(pvc_PaginaDestino) {
            window.location = pvc_PaginaDestino
        };

        function mostrarAlertaRegistroExitoso() {
            deshabilitarControl('#<%=btnCancelar.ClientID%>');
            deshabilitarControl('#<%=btnGuardar.ClientID%>');
            deshabilitarControl('#<%=btnFinalizar.ClientID%>');
            $('.formulario').attr('disabled', 'disabled');

            mostrarAlerta(
                '#arAlertasDelFormulario',
                {
                    mensaje: 'Se ha tramitado la orden de aprovisionamiento.',
                    tipo: "exito",
                    transparencia: 0.9,
                    posicion: 'top',
                    onClosed: function () { regresarAlListado(); }
                });
        }

        function mostrarAlertaNoHayDatos() {
            mostrarAlerta(
                '#arAlerta',
                {
                    mensaje: 'No se cuenta con información para mostrar.',
                    tipo: "advertencia",
                    transparencia: 0.9,
                    posicion: 'center'
                }
            );

            deshabilitarControl('#btnCancelarBusqueda');
            ocultarAreaDeListado();
            mostrarAreaFiltrosDeBusqueda();

        };

    </script>
</asp:Content>

