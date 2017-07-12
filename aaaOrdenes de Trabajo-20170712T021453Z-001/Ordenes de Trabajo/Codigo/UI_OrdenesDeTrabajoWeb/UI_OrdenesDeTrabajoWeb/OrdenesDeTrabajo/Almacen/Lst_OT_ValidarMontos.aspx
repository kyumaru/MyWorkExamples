<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/Mp_Listado.master" AutoEventWireup="false" CodeFile="Lst_OT_ValidarMontos.aspx.vb" Inherits="OrdenesDeTrabajo_Almacen_Lst_OT_ValidarMontos" %>

<%@ Register TagPrefix="wuc" Namespace="Utilerias.WebForms.Controles" Assembly="Utilerias.WebForms.Controles" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphFormulario" Runat="Server">
    <header>
        <h2>Gestión de Ingreso de Material a Almacén</h2>
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
    
    <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>--%>
    <article data-grupo="Listado" class="listado sinBorde">


        <article class="tituloSeccion">
            Listado de Gestiones de Material
        </article>

        <article id="Article1" runat="server">
            <asp:Repeater runat="server" ID="RpSuministros">
                <HeaderTemplate>
                    <table>
                        <tr>
                            <th style="width: 30%;">
                                <asp:LinkButton runat="server" ID="lnkNumeroGestion" Text="N° de Gestión" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GESTION_INGRESO_MATERLST.NUMERO_GESTION_COMPRA%>" CommandArgument="ASC" OnCommand="lnkRpSuministros_Command"></asp:LinkButton>
                            </th>
                            <th style="width: 20%;">
                                <asp:LinkButton runat="server" ID="lnkObservaciones" Text="Observaciones" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GESTION_INGRESO_MATERLST.OBSERVACIONES%>" CommandArgument="ASC" OnCommand="lnkRpSuministros_Command"></asp:LinkButton>
                            </th>
                            <th style="width: 20%;">
                                <asp:LinkButton runat="server" ID="lnkFecha" Text="Fecha" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GESTION_INGRESO_MATERLST.FECHA_INGRESO_REGISTRO%>" CommandArgument="ASC" OnCommand="lnkRpSuministros_Command"></asp:LinkButton>
                            </th>
                            <th style="width: 30%;">
                                <asp:LinkButton runat="server" ID="lnkEstado" Text="Estado" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GESTION_INGRESO_MATERLST.DESC_ESTADO%>" CommandArgument="ASC" OnCommand="lnkRpSuministros_Command"></asp:LinkButton>
                            </th>
                            <th>&nbsp;</th>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr class="lineaDelListado">
                        <td style="width: 30%;"><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GESTION_INGRESO_MATERLST.NUMERO_GESTION_COMPRA)%></td>
                        <td style="width: 20%; text-align: center;">
                            <span style='<%#String.Format("display:{0};", IIf(Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GESTION_INGRESO_MATERLST.OBSERVACIONES).ToString = "" Or Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GESTION_INGRESO_MATERLST.OBSERVACIONES).ToString = "-", "none", "block"))%>'>
                                <img id="ImgObservacion" class="tooltip" data-tipo="ObservacionRegistro" scr="" title="<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GESTION_INGRESO_MATERLST.OBSERVACIONES)%>" />
                        </td>
                        <td style="width: 20%;">
                            <%#CType(Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GESTION_INGRESO_MATERLST.FECHA_INGRESO_REGISTRO), DateTime).ToString(Utilerias.OrdenesDeTrabajo.Constantes.FORMATO_FECHA_UI)%></span>
                        </td>
                        <td style="width: 30%;">
                            <asp:LinkButton runat="server" ID="lnkEstadoSM" Text="<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GESTION_INGRESO_MATERLST.DESC_ESTADO)%>" CommandArgument='<%#String.Format("{0}%{1}%{2}%{3}%{4}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GESTION_INGRESO_MATERLST.ID_UBICACION), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GESTION_INGRESO_MATERLST.ANNO), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GESTION_INGRESO_MATERLST.ID_VIA_COMPRA_CONTRATO), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GESTION_INGRESO_MATERLST.NUMERO_GESTION), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GESTION_INGRESO_MATERLST.CONSECUTIVO))%>' OnCommand="lnkTrazabilidadGestion"></asp:LinkButton>
                        </td>
                        <td>
                            <asp:ImageButton runat="server" class="tooltip" ToolTip="Consultar" ID="ibConsultarGestion" AlternateText="Consultar" OnClick="ibConsultarGestion_Click" 
                                CommandArgument='<%#String.Format("{0}%{1}%{2}%{3}%{4}%{5}%{6}%{7}%{8}%{9}%{10}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GESTION_INGRESO_MATERLST.ID_UBICACION),
                                                                                         Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GESTION_INGRESO_MATERLST.ANNO),
                                                                                         Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GESTION_INGRESO_MATERLST.ID_VIA_COMPRA_CONTRATO),
                                                                                         Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GESTION_INGRESO_MATERLST.NUMERO_GESTION),
                                                                                         Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GESTION_INGRESO_MATERLST.CONSECUTIVO),
                                                                                         Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GESTION_INGRESO_MATERLST.NUMERO_GESTION_COMPRA),
                                                                                         Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GESTION_INGRESO_MATERLST.NOMBRE_PROVEEDOR),
                                                                                         Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GESTION_INGRESO_MATERLST.FECHA_INGRESO_REGISTRO),
                                                                                         Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GESTION_INGRESO_MATERLST.DESC_VIA_COMPRA),
                                                                                         Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GESTION_INGRESO_MATERLST.OBSERVACIONES),
                                                                                         Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GESTION_INGRESO_MATERLST.RESPONSABLE))%>' 
                                src='<%#AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Check.png")%>' 
                                onmouseover='<%# String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Icono_Check.png"))%>' 
                                onmouseout='<%# String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Check.png"))%>'  />
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </table>
                </FooterTemplate>
            </asp:Repeater>

            <article data-grupo="Listado" class="areaPaginadorListado">
                <wuc:PaginadorNumerico runat="server" ID="pnRpSuministros" />
            </article>

            <article data-grupo="Listado" class="areaCantidadDeRegistro">
                <asp:Label runat="server" ID="lblCantidadDeRegistrosSM" Text="" Visible="true"></asp:Label>
            </article>
        </article>
    </article>
    <%--</ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ddlTipoAjuste" EventName="SelectedIndexChanged" />
        </Triggers>
    </asp:UpdatePanel>--%>

    <article class="areaBotones">
        <input id="btnCancelar" type="button" title="Regresar" value="Regresar" />
    </article>

    <article id="arAlerta"></article>
    <article id="arPopupGenerico"></article>

    <script type="text/javascript">

        $(document).ready(function () {
            aplicarConfiguracionBasicaListado({ alternarVisibilidadFiltroLista: true });
            $('#btnCancelar').click(function () {
                regresarAlListado();
            });

            inicializarScript();
        });

        function regresarAlListado() {
            window.location = 'Lst_OT_RegistroIngresoInventario.aspx';
        }


        function inicializarScript() {
            habilitarTooltipGenerico();
            $('#btnCancelar').click(function () {
                regresarAlListadoPrincipal();
            });

            $('[data-tipo="ObservacionRegistro"]').attr('src', '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Informacion.png")%>');
            $('[data-tipo="ObservacionRegistro"]').on({
                'mouseover': function () { $(this).attr('src', '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Icono_Informacion.png")%>'); },
                'mouseout': function () { $(this).attr('src', '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Informacion.png")%>'); }
            });

        }

        //********* Data Picker************
        $("#datepicker").datepicker({
            inline: true
        });

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

            //deshabilitarControl('#btnCancelarBusqueda');
            ocultarAreaDeListado();
            mostrarAreaFiltrosDeBusqueda();

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

        function mostrarAlertaMensajeRedirecionar(pvcMensaje) {
            mostrarAlerta(
                '#arAlerta',
                {
                    mensaje: pvcMensaje,
                    tipo: "advertencia",
                    transparencia: 0.9,
                    posicion: 'top',
                    onClosed: function () { regresarAlListado(); }
                }
            );
        }

        function regresarAlListado() {
            window.location = '../Genericos/Frm_MenuPrincipal.aspx';
        }

        function regresarAlListadoPrincipal() {
            window.location = 'Lst_OT_IngresoMaterialAlmacen.aspx';
        }

        function mostrarAlertaSinRol() {
            mostrarAlerta(
                '#arAlerta',
                {
                    mensaje: 'El usuario conectado no cuenta con los permisos necesarios para el acceso a este formulario.',
                    tipo: "advertencia",
                    transparencia: 0.9,
                    posicion: 'top',
                    onClosed: function () { regresarAlListado(); }
                }
            );
        }
    </script>
</asp:Content>

