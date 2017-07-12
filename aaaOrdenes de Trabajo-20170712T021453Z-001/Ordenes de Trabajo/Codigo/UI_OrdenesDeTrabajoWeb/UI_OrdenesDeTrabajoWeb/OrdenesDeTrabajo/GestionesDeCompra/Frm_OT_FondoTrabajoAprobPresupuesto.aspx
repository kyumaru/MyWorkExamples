<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/Mp_Formulario.master" AutoEventWireup="false" CodeFile="Frm_OT_FondoTrabajoAprobPresupuesto.aspx.vb" Inherits="OrdenesDeTrabajo_GestionesDeCompra_Frm_FondoTrabajoAprobPresupuesto" %>

<%@ Register TagPrefix="wuc" Namespace="Utilerias.WebForms.Controles" Assembly="Utilerias.WebForms.Controles" %>
<%@ Register Src="~/Controles/wuc_OT_DetalleGestionCompraFondo.ascx" TagName="wuc_DetalleGestionCompraFondo" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphFormulario" Runat="Server">
    <header>
        <h2>Gestión de Compras por Fondo de Trabajo</h2>

    </header>


    <article class="tituloSeccion">
        Cotizaciones Registradas
    </article>

    <article class="formulario sinBorde " style="overflow-x:auto;overflow-x:scroll">
        <asp:Repeater runat="server" ID="rpMateriales">
        <ItemTemplate>
            <article runat="server" id="arAcordeon" class="formulario sinBorde" data-tipo="acordeon">
                <article class="encabezadoAcordeon">
                    <a runat="server" id="ancorAcordeon" class="tituloAcordeon">
                        <asp:Label runat="server" ID="lblDescMaterial" Text='<%# String.Format("Descripción: {0}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_LINEA_GEST_COMP_GROUP.DESCRIPCION))%>'></asp:Label> &nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Label runat="server" ID="lblCodigoMaterial" Text='<%# String.Format("Código: {0}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_LINEA_GEST_COMP_GROUP.ID_MATERIAL))%>'></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Label runat="server" ID="lblCantidad" Text='<%# String.Format("Cantidad: {0}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_LINEA_GEST_COMP_GROUP.CANTIDAD_SOLICITADA_MEDIDA))%>'></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Label runat="server" ID="lblProveedores" Text='<%# String.Format("{0}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_LINEA_GEST_COMP_GROUP.PROVEEDORES))%>'></asp:Label>
                    </a>
                </article>
                <article runat="server" id="cuerpoAcordeon1" class="cuerpoAcordeon">
                    <uc1:wuc_DetalleGestionCompraFondo runat="server" ID="wucDetalleGestionCompraFondo" IdUbicacion='<%# Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_LINEA_GEST_COMP_GROUP.ID_UBICACION)%>' IdViaCompraContrato='<%# Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_LINEA_GEST_COMP_GROUP.ID_VIA_COMPRA_CONTRATO)%>' IdAnnio='<%# Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_LINEA_GEST_COMP_GROUP.ANNO)%>' IdNumeroGestion='<%# Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_LINEA_GEST_COMP_GROUP.NUMERO_GESTION)%>' IdMaterial='<%# Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_LINEA_GEST_COMP_GROUP.ID_MATERIAL)%>'/>
                </article>
            </article>
        </ItemTemplate>
        </asp:Repeater>
    </article>


    <article class="tituloSeccion">
        Cotización Adjudicada
    </article>

    <article data-grupo="Listado" class="listado sinBorde">
        <asp:Repeater runat="server" ID="rpArchivo">
            <HeaderTemplate>
                <table>
                    <tr>
                        <th>
                            <asp:LinkButton runat="server" ID="lnkProveedor" Text="Proveedor" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ADJUNTO_COTIZACIONLST.NOMBRE_PROVEEDOR%>" OnCommand="lnkArchivo_Command"></asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton runat="server" ID="lnkNombre" Text="Archivo" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ADJUNTO_COTIZACIONLST.NOMBRE_ARCHIVO%>" OnCommand="lnkArchivo_Command"></asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton runat="server" ID="lnkDescripcion" Text="Descripción" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ADJUNTO_COTIZACIONLST.DESCRIPCION%>" OnCommand="lnkArchivo_Command"></asp:LinkButton>
                        </th>
                    </tr>
            </HeaderTemplate>

            <ItemTemplate>
                <tr class="lineaDelListado">
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ADJUNTO_COTIZACIONLST.NOMBRE_PROVEEDOR)%></td>
                    <td>
                        <asp:LinkButton runat="server" ID="lnkDescargarArchivo_Command"
                                            CommandArgument='<%#Container.ItemIndex%>'
                                            Style="text-decoration: underline; color: blue;"
                                            OnCommand="lnkDescargarArchivo_Command"
                                            Text="<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ADJUNTO_COTIZACIONLST.NOMBRE_ARCHIVO)%>"></asp:LinkButton>
                        <%--<asp:LinkButton runat="server" ID="lnkDescripcion" Text="<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ADJUNTO_COTIZACIONLST.NOMBRE_ARCHIVO)%>" CommandName='<%# String.Format("{0}%{1}%{2}%{3}%{4}%{5}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ADJUNTO_COTIZACIONLST.IDENTIFICACION), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ADJUNTO_COTIZACIONLST.ID_UBICACION), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ADJUNTO_COTIZACIONLST.ID_VIA_COMPRA_CONTRATO), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ADJUNTO_COTIZACIONLST.ANNO), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ADJUNTO_COTIZACIONLST.NUMERO_GESTION), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ADJUNTO_COTIZACIONLST.NOMBRE_ARCHIVO))%>' OnCommand="lnkDescargarArchivo_Command"></asp:LinkButton>--%>
                    </td>

                    <td>
                        <article style="display: <%#IIf(Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ADJUNTO_COTIZACIONLST.DESCRIPCION).ToString.StartsWith("-"), "none", "block")%>">
                            <asp:ImageButton runat="server" ID="ImageButton1" data-tipo="tooltip"
                                CssClass="centradoEnRow"
                                title='<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ADJUNTO_COTIZACIONLST.DESCRIPCION)%>'
                                ImageUrl='<%#AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Informacion.png")%>' />
                        </article>
                    </td>
                </tr>
            </ItemTemplate>

            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
    </article>

    <article data-grupo="Listado" class="areaPaginadorListado">
        <asp:Label runat="server" ID="lblNoHayDAtosArchivo" Text="No se cuenta con información para mostrar" Visible="false"></asp:Label>
        <wuc:PaginadorNumerico runat="server" ID="pnRpArchivo" />
    </article>

    <article data-grupo="Listado" class="areaCantidadDeRegistro">
        <asp:Label runat="server" ID="lblCantidadDeRegistrosArchivo" Text="" Visible="true"></asp:Label>
    </article>

    <article class="tituloSeccion">
        Datos de la Revisión
    </article>
    <article class="formulario ">
                <table>
                    <tr>
                        <th style="width: 14%;"></th>
                        <td>
                            <asp:RadioButton ID="rdbAprobado" runat="server"  GroupName="grpAprobo" Text="Aprobar Presupuesto" />
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:RadioButton ID="rdbDevuelto" runat="server"  GroupName="grpAprobo" Text="Devolver" />
                            <asp:CustomValidator ID="cvcgrpEstadoRevision" runat="server" ControlToValidate="txtValidaciones" ErrorMessage="Debe indicar si decide aprobar el presupuesto o devolver la gestión de compra" ValidationGroup="Grupo1" ClientValidationFunction="ValidarEstadoRevision">&nbsp;</asp:CustomValidator>
                        </td>
                    </tr>
                    <tr runat="server" id="trObservaciones">
                                    <th style="width: 14%;">Observaciones</th>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtObservaciones" Width="100%" TextMode="MultiLine" Rows="9" data-tipocontrol="texto"></asp:TextBox>
                                        <br />
                                        <span id="spContadorTxtObservacionesspContadorTxtObservaciones" class="contadorCaracteresRestantes"></span>
                                        <asp:CustomValidator ID="cvstxtObservacionesPrimeraRevision" runat="server" ControlToValidate="txtValidaciones" ErrorMessage="Debe indicar una observación si decide devolver la gestión de compra" ValidationGroup="Grupo1" ClientValidationFunction="ValidadorDeCampoRequeridoPersonalizado">&nbsp;</asp:CustomValidator>
                                    </td>
                                </tr>
                </table>
        <br />
    </article>

    <article style="visibility: hidden">
        <asp:TextBox ID="txtValidaciones" runat="server" Text="Validar"></asp:TextBox>
    </article>

    <article class="areaBotones">
        <asp:Button ID="btnTramitar" runat="server" Text="Aceptar" ToolTip="Aceptar" ValidationGroup="Grupo1"></asp:Button>
        <input id="btnLimpiarFormulario" type="button" data-tipo="limpiarFormulario" value="Limpiar formulario" />
        <input id="btnCancelar" type="button" title="Regresar" value="Regresar" />
    </article>

    <article id="arAlertasDelFormulario"></article>

    <script type="text/javascript">

        $(document).ready(function () {
            $('#btnCancelar').click(function () {
                regresarAlListado();
            });

            $('article[data-tipo="acordeon"]').each(function () {
                configurarAcordeon('#' + this.id, { seleccionMultiple: false, eventoApertura: 'click' });
            });

            configurarLongitudMaximaTexto('#<%=Me.txtObservaciones.ClientID%>',<%=Utilerias.OrdenesDeTrabajo.Modelo.OTT_GESTION_COMPRA.OBSERVACIONES_BD_TAMANO%>);
            configurarContadorCaracteresRestantes('#<%=Me.txtObservaciones.ClientID%>','#spContadorTxtObservaciones');

        })


        function ValidadorDeCampoRequeridoPersonalizado(source, clientside_arguments) {
            if (document.getElementById('<%=rdbDevuelto.ClientID%>').checked && document.getElementById('<%=txtObservaciones.ClientID%>').value.trim() == "") {
                document.getElementById('<%=txtObservaciones.ClientID%>').style.backgroundColor = "#F5838A"
                return clientside_arguments.IsValid = false;
            }
            else {
                document.getElementById('<%=txtObservaciones.ClientID%>').style.backgroundColor = "white"
                return clientside_arguments.IsValid = true;
            }
        }

        function mostrarAlertaError(pvc_Mensaje) {
            mostrarAlerta(
                '#arAlertasDelFormulario',
                {
                    mensaje: pvc_Mensaje,
                    tipo: 'peligro',
                    transparencia: 1,
                    posicion: 'top',
                    permiteCerrar: true
                }
            );
        }


        /*
          Autor: Mauricio Salas
          Fecha:02/09/2016
          Descripcion: Función que valida que se haya seleccionado si fue aprovado o devuelta la gestion
          Parametros: source, clientside_arguments: parametros de la Función CustomValidator
          */
        function ValidarEstadoRevision(source, clientside_arguments) {
            return clientside_arguments.IsValid = (document.getElementById('<%=rdbAprobado.ClientID%>').checked || document.getElementById('<%=rdbDevuelto.ClientID%>').checked);
        }

        function mostrarAlertaRegistroExitoso() {
            deshabilitarControl('#<%=btnTramitar.ClientID%>');
            deshabilitarControl('#btnLimpiarFormulario');
            deshabilitarControl('#btnCancelar');
            $('.formulario').attr('disabled', 'disabled');

            mostrarAlerta(
                '#arAlertasDelFormulario',
                {
                    mensaje: 'Se ha tramitado la gestión de compra.',
                    tipo: "exito",
                    transparencia: 0.9,
                    posicion: 'top',
                    onClosed: function () { regresarAlListado(); }
                });
        }

        function regresarAlListado() {
            window.location = 'Lst_OT_FondoTrabajoAprobPresupuesto.aspx';
        }

    </script>
</asp:Content>

