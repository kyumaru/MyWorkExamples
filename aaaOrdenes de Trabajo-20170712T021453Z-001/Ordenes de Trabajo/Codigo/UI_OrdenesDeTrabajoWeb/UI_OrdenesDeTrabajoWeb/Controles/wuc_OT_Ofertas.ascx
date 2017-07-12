<%@ Control Language="VB" AutoEventWireup="false" CodeFile="wuc_OT_Ofertas.ascx.vb" Inherits="Controles_wuc_OT_Ofertas" %>

<article style="display: <%=IIf(Me.Cargo <> Utilerias.OrdenesDeTrabajo.Cargo.PROFESIONAL, "block", "none")%>">
    <asp:CheckBox ID="chkContratacionDesierta" runat="server" Text="Contratación Desierta" />
</article>

<article id="arAgregarArchivo" runat="server">
    <table>
        <tr>
            <th>Resumen</th>
            <td>
                <asp:textbox TextMode="MultiLine"  runat="server" id="txtResumenOferta" Width="65%" cols="98" rows="4" title="Breve descripción del contenido del documento"></asp:textbox>
                <asp:RequiredFieldValidator runat="server" ID="rfvTxtResumenOferta" ControlToValidate="txtResumenOferta" ValidationGroup="AgregarDocumentoOfertas" Display="Dynamic" ErrorMessage="Agregue un resumen del contenido del archivo">&nbsp;</asp:RequiredFieldValidator> 
                <span id="spContadorTxtResumenOferta" class="contadorCaracteresRestantes"></span>
            </td>
        </tr>
        <tr>
            <th>Documento</th>
            <td>
                <asp:FileUpload runat="server" ID="fuDocumento" onchange="validaArchivoOfertas();" />
                <asp:Button runat="server" ID="btnAgregarDocumento" ValidationGroup="AgregarDocumentoOfertas" text="Agregar"/>
                <img runat="server" ID="imgExtensionesOfertas" data-tipo="tooltipExtension" class="tooltip"/>
                <asp:RequiredFieldValidator runat="server" ID="rfvFuDocumento" ControlToValidate="fuDocumento" ValidationGroup="AgregarDocumentoOfertas" Display="Dynamic" ErrorMessage="Agregue un Documento.">&nbsp;</asp:RequiredFieldValidator> 
            </td>
        </tr>
    </table>
</article>

<article id="articleTitulo" visible="false" runat="server" class="tituloSeccion">
    Historial de aclaraciones
</article>
<article class="listado sinBorde">
    <asp:Repeater runat="server" ID="rpAdjuntos">
        <HeaderTemplate>
            <table>
                <tr>
                    <th>Tipo</th>
                    <th>Documento</th>
                    <th>Resumen</th>
                    <th>Fecha de Registro</th>
                    <th>Responsable</th>
                    <th>
                        <article style="display: <%#IIf(Me.Cargo <> Utilerias.OrdenesDeTrabajo.Cargo.PROFESIONAL, "block", "none")%>">
                                Tramitada
                        </article>
                    </th>
                    <th>&nbsp;</th>
                </tr>
        </HeaderTemplate>
        <ItemTemplate>
            <tr class="lineaDelListado">
                <td><%#IIf(Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DOCUMENTO_CONTRATLST.ORIGEN).ToString <> Utilerias.OrdenesDeTrabajo.Cargo.PROFESIONAL, "Subsanación", "Revisión")%></td>
                <td>
                    <asp:LinkButton runat="server" ID="lnkArchivo"
                        CommandArgument='<%#Container.ItemIndex%>'
                        Style="text-decoration: underline; color: blue;"
                        OnCommand="lnkArchivo_Command"
                        Text='<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DOCUMENTO_CONTRATLST.NOMBRE_ARCHIVO)%>'></asp:LinkButton>
                </td>
                <td style="text-align:center"><img runat="server" ID="imgDescripcion" class="tooltip" title='<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DOCUMENTO_CONTRATLST.DESCRIPCION)%>'
                    src='<%# AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x24, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Informacion.png")%>' /></td>
                <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DOCUMENTO_CONTRATLST.FECHA_HORA_REGISTRO)%></td>
                <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DOCUMENTO_CONTRATLST.RESPONSABLE)%></td>
                <td style="text-align:center">
                    <article style="display: <%#IIf(Me.Cargo <> Utilerias.OrdenesDeTrabajo.Cargo.PROFESIONAL, "block", "none")%>">
                    <asp:CheckBox Visible='<%#IIf(Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DOCUMENTO_CONTRATLST.ORIGEN).ToString = Utilerias.OrdenesDeTrabajo.Cargo.PROFESIONAL, True, False)%>' 
                        runat="server" ID="chkTramitado" data-idAdjunto='<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DOCUMENTO_CONTRATLST.ID_ADJUNTO_ORDEN_TRABAJO)%>' 
                        OnCheckedChanged="chkTramitado_CheckedChanged" AutoPostBack="true" Checked='<%#IIf(CInt(Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DOCUMENTO_CONTRATLST.DOCUMENTO_TRAMITADO)) = 1, True, False)%>'
                        enabled='<%#IIf(Me.EtapaActual = Utilerias.OrdenesDeTrabajo.EtapaContratacion.OFERTAS, True, False)%>' />
                    </article>
                </td>
                <td style="text-align:center">
                    <asp:ImageButton runat="server" ID="ibBorrar" data-tipo="borrarRegistro" AlternateText="Borrar"
                        Visible='<%#IIf(CInt(Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DOCUMENTO_CONTRATLST.DOCUMENTO_TRAMITADO)) = 0 AndAlso
                                                Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_DOCUMENTO_CONTRATLST.ORIGEN).ToString = Me.Cargo AndAlso
                                                Me.EtapaActual = Utilerias.OrdenesDeTrabajo.EtapaContratacion.OFERTAS, True, False)%>'
                        CommandArgument='<%# String.Format("{0}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_ADJUNTO_ORDEN_TRABAJO.NOMBRE_ARCHIVO))%>' OnClick="ibBorrarAdjuntoOfertas_Click"
                        ImageUrl='<%#AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Borrar.png")%>' CommandName='<%#Container.ItemIndex%>'
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

<article class="areaBotones">
    <asp:Button runat="server" ID="btnCerrarEtapa" Text="Cerrar Etapa" OnClick="btnCerrarEtapa_Click" />
    <input type="button" value="Regresar" data-tipo="volverAlListado" id="btnRegresar" />
</article>

<article id="arAlertasDelFormulario"></article>

<script type="text/javascript">

    $(document).ready(function () {
        
        habilitarTooltipGenerico();
    
        configurarLongitudMaximaTexto('#<%=Me.txtResumenOferta.ClientID%>',<%=Utilerias.OrdenesDeTrabajo.Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.DESCRIPCION_BD_TAMANO %>);

        configurarContadorCaracteresRestantes('#<%=Me.txtResumenOferta.ClientID%>','#spContadorTxtResumenOferta');
    });


    function mostrarAlertaGuardadoExitoso() {
        deshabilitarControl('#<%=btnCerrarEtapa.ClientID%>');
        deshabilitarControl('#<%=btnAgregarDocumento.ClientID%>');
        $('.formulario').attr('disabled', 'disabled');

        mostrarAlerta(
        '#arAlertasDelFormulario',
        {
            mensaje: 'Se ha guardado los datos indicados para la finalización y entrega de diseño',
            tipo: "exito",
            transparencia: 0.9,
            posicion: 'center',
            onClosed: function () { window.location='Lst_OT_GestionProfesionalesDisenio.aspx'; }
        });
    };

    function validaArchivoOfertas() {
        var vlo_InputArchivo = document.getElementById('<%=fuDocumento.ClientID%>');
        var vlc_NombreArchivo = vlo_InputArchivo.value;
        var vlc_NombreCorto = " ";
        var vlc_Limiter = '\\';
        var vlc_Extension = '';
        var vln_Indice = 0;
        var vln_IndiceExtension = 0;
        var vlc_LimiterExtension = '.';
        var vln_limiteTamArchivo = <%=Me.TamanoArchivo%>;
        var vln_TamArchivo;
        var vln_TamBytes = vln_limiteTamArchivo * <%=Utilerias.OrdenesDeTrabajo.Constantes.TAMANNO_BYTES_A_MEGAS%>;
        var vlc_Llaves;
           
        var vln_TamanoNombre = <%=Utilerias.OrdenesDeTrabajo.Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.NOMBRE_ARCHIVO_BD_TAMANO%>;
          
        if(vlo_InputArchivo.value != ''){
                
            vln_Indice =  vlc_NombreArchivo.lastIndexOf(vlc_Limiter) + 1;
            vlc_NombreCorto = vlc_NombreArchivo.substr(vln_Indice, vlc_NombreArchivo.length - vln_Indice);
            vln_IndiceExtension =  vlc_NombreArchivo.lastIndexOf(vlc_LimiterExtension) + 1;
            vlc_Extension = vlc_NombreArchivo.substr(vln_IndiceExtension, vlc_NombreArchivo.length);    
                
            vlc_ExtencionesPerimtidas = '<%=Me.ExtensionesPermitidas%>';
            vlc_Llaves = vlc_ExtencionesPerimtidas.split(",");
            
            if(vlc_Llaves.indexOf(vlc_Extension.toUpperCase()) == -1){
                mostrarAlertaError('No es una extensión permitida','');
                vlo_InputArchivo.value = "";
                return false;
            }
                
            if (vlc_NombreCorto.length > vln_TamanoNombre){
                mostrarAlertaError('El nombre del archivo es demasiado largo','');
                vlo_InputArchivo.value = "";
                return false;
            }
                
            vln_TamArchivo = vlo_InputArchivo.files[0].size;
            if (vln_TamArchivo > vln_TamBytes){
                mostrarAlertaError('El tamaño del archivo excede el máximo permitido.','');
                vlo_InputArchivo.value = "";
                return false;
            }

            return true;
        }
        return false;
    };
</script>