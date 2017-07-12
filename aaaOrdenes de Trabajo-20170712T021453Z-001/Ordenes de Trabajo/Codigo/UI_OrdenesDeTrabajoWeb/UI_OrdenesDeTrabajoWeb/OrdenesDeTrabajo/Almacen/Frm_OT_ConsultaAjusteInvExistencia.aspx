<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/Mp_Formulario.master" AutoEventWireup="false" CodeFile="Frm_OT_ConsultaAjusteInvExistencia.aspx.vb" Inherits="OrdenesDeTrabajo_Almacen_Frm_OT_ConsultaAjusteInvExistencia" %>

<%@ Register TagPrefix="wuc" Namespace="Utilerias.WebForms.Controles" Assembly="Utilerias.WebForms.Controles" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphFormulario" Runat="Server">
    <header>
        <h2>Ajuste de inventario por existencia</h2>
    </header>

    <article class="tituloSeccion">
        Ajuste de inventario global
    </article>

    <article class="formulario sinBorde">
        <table>
            <tr>
                <th>Almacén o Bodega</th>
                <td>
                    <asp:Label runat="server" ID="lblAlmacen"></asp:Label>
                    
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
        Listado de materiales
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
                        <th>Nueva Cantidad</th>
                        <th>Tipo</th>
                        <th>&nbsp;</th>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr class="lineaDelListado">
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTF_INVENTARIOLST.ID_MATERIAL)%></td>
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTF_INVENTARIOLST.DESC_MATERIAL)%></td>
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTF_INVENTARIOLST.CANTIDAD_DISPONIBLE)%></td>
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTF_INVENTARIOLST.DISPONIBLE)%></td>
                    <td>
                        <asp:TextBox runat="server" ID="txtCantidad" Width="50%" Text='<%#Eval("CANTIDAD")%>' AutoPostBack="true" OnTextChanged="txtCantidad_OnTextChanged" data-inf='<%#String.Format("{0}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTF_INVENTARIOLST.ID_MATERIAL))%>'></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label runat="server" ID="lblTipo" Text='<%#Eval("TIPO")%>'></asp:Label>
                    </td>
                    <td>
                        <asp:ImageButton runat="server" ID="ibBorrar" AlternateText="Borrar" data-tipo="borrarRegistros" CommandArgument='<%#String.Format("{0}%{1}%{2}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTF_INVENTARIOLST.ID_MATERIAL), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTF_INVENTARIOLST.ID_ALMACEN_BODEGA), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTF_INVENTARIOLST.ID_UBICACION_ADMINISTRA))%>' OnClick="ibBorrar_Click" />
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
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
            window.location = 'Frm_OT_AjusteInvGlobal.aspx';
        };

        function regresarAlListado() {
            window.location = 'Lst_OT_AjusteInventario.aspx';
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
                    mensaje: 'Se ha tramitado el ajuste de inventario.',
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

