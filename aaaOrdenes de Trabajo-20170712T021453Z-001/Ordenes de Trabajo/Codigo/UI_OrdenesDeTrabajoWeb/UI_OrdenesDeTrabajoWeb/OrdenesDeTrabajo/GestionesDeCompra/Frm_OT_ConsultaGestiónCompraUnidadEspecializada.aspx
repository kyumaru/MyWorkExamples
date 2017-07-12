<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/Mp_Listado.master" AutoEventWireup="false" CodeFile="Frm_OT_ConsultaGestiónCompraUnidadEspecializada.aspx.vb" Inherits="OrdenesDeTrabajo_GestionesDeCompra_Frm_OT_ConsultaGestiónCompraUnidadEspecializada" %>

<%@ Register TagPrefix="wuc" Namespace="Utilerias.WebForms.Controles" Assembly="Utilerias.WebForms.Controles" %>
<%@ Register Src="~/Controles/wuc_OT_Lineas_Material_Gestion_Compra.ascx" TagName="wuc_LineasMaterialGestionCompra" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphFormulario" runat="Server">
    
    <header>
        <h2>Gestión de Compras por Unidad Especializada de Compra</h2>
    </header>

    <article class="tituloSeccion">
        Gestión de Compras por Unidad Especializada de Compra
    </article>

    <article class="formulario">
        <table>
            <tr>
                <th>Observaciones</th>
                <td>
                    <asp:TextBox Width="70%" runat="server" ID="txtObservaciones" TextMode="MultiLine" Rows="5" ReadOnly="true"></asp:TextBox>
                </td>
            </tr>
        </table>
    </article>

    <article class="tituloSeccion">
        Materiales a Gestionar
    </article>

    <br />
    <asp:Repeater runat="server" ID="rpMateriales">
        <ItemTemplate>
            
            <article class="encabezadoAcordeon">

                <article runat="server" id="arAcordeon" class="formulario sinBorde" data-tipo="acordeon">
                    <asp:ImageButton runat="server" ToolTip="Subir" ID="ibSubir"
                        AlternateText="Subir" OnClick="ibSubir_Click"
                        Enabled='<%#IIf((Container.ItemIndex = 0), False, True)%>'  
                        CommandArgument='<%# String.Format("{0}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GRUPO_GESTION_COMPRALST.NUMERO_LINEA))%>'
                        src='<%#AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Flecha_Arriba.png")%>'
                        onmouseover='<%# String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Flecha_Arriba.png"))%>'
                        onmouseout='<%# String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Flecha_Arriba.png"))%>' />

                    &nbsp;&nbsp;

                    <asp:ImageButton runat="server" ToolTip="Subir" ID="ibBajar"
                        AlternateText="Bajar" OnClick="ibBajar_Click"
                        Enabled='<%#IIf((Me.MaximoLinea = (Container.ItemIndex + 1)), False, True)%>'                        
                        CommandArgument='<%# String.Format("{0}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GRUPO_GESTION_COMPRALST.NUMERO_LINEA))%>'
                        src='<%#AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Flecha_Abajo.png")%>'
                        onmouseover='<%# String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Flecha_Abajo.png"))%>'
                        onmouseout='<%# String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Flecha_Abajo.png"))%>' />

                      &nbsp;&nbsp;
                    <a runat="server" id="ancorAcordeon" class="tituloAcordeon">
                        <asp:Label runat="server" ID="lblNumeroLinea" Text='<%# String.Format("No. de Linea: {0}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GRUPO_GESTION_COMPRALST.NUMERO_LINEA))%>'></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Label runat="server" ID="lblPartida" Text='<%# String.Format("Partida: {0}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GRUPO_GESTION_COMPRALST.PARTIDA_PRESUPUESTARIA))%>'></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Label runat="server" ID="lblCodigoMaterial" Text='<%# String.Format("Código: {0}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GRUPO_GESTION_COMPRALST.ID_MATERIAL))%>'></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Label runat="server" ID="lblDescMaterial" Text='<%# String.Format("Descripción: {0}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GRUPO_GESTION_COMPRALST.DESCRIPCION))%>'></asp:Label>
                        &nbsp;&nbsp;&nbsp;&nbsp;                     
                        <asp:Label runat="server" ID="lblCantidad" Text='<%# String.Format("Cantidad: {0}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GRUPO_GESTION_COMPRALST.CANTIDAD_SOLICITADA_MEDIDA))%>'></asp:Label>
                    </a>
                </article>
                <article runat="server" id="cuerpoAcordeon1" class="cuerpoAcordeon">
                    <uc1:wuc_LineasMaterialGestionCompra runat="server" ID="wucLineasMaterialGestionCompra" IdUbicacion='<%# Eval("ID_UBICACION")%>' IdViaCompraContrato='<%# Eval("ID_VIA_COMPRA_CONTRATO")%>' IdAnnio='<%# Eval("ANNO")%>' IdNumeroGestion='<%# Eval("NUMERO_GESTION")%>' IdMaterial='<%# Eval("ID_MATERIAL")%>' />
                </article>
            </article>
        </ItemTemplate>
    </asp:Repeater>
    <br />

    <article class="areaBotones">
        <asp:Button runat="server" ID="btnCancelar" />
        <asp:Button runat="server" ID="btnGuardar" Text="Guardar" />
        <asp:Button runat="server" ID="btnEnviarAprobacion" Text="Enviar a Aprobación" />
    </article>

    <article id="arAlerta"></article>
    <article id="arpopupConfirmaDeseaBorrar"></article>
    <article id="arPopupGenerico"></article>
    <article id="arAlertasDelFormulario"></article>
    <article id="arpopupConfirmaEnviar"></article>

    <script type="text/javascript">

        function irASeleccion() {
            window.location = 'Frm_OT_GestionCompraUnidadEspecializada.aspx';
        };

        function irAListado() {
            window.location = 'Lst_OT_GestiónCompraListado.aspx';
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

            $('article[data-tipo="acordeon"]').each(function () {
                configurarAcordeon('#' + this.id, { seleccionMultiple: false, eventoApertura: 'click' });
            });

            $('#<%=btnEnviarAprobacion.ClientID%>').click(function () { return mostrarPopupConfirmaDeseaEnviarRegistro(); });

        });

        function mostrarPopupConfirmaDeseaEnviarRegistro() {
            var vlo_ConfiguracionPopup = {
                titulo: 'Gestión de Compras por Unidad Especializada de Compra',
                mensaje: 'Esta acción dará por concluida la selección de materiales para Gestionar la Orden, ¿Desea Continuar?',
                botones:
                        [
                            {
                                idControl: "btnSi",
                                textoBoton: "Si",
                                onClick:
                                    function (e) {
                                        $(this).attr("disabled", "disabled");
                                        __doPostBack('<%=btnEnviarAprobacion.UniqueID%>', '');
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
        };

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

    </script>

</asp:Content>

