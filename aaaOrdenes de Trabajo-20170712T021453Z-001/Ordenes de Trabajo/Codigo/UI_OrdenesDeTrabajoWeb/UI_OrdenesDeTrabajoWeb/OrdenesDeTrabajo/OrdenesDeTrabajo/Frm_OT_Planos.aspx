<%@ Page Language="VB" MaintainScrollPositionOnPostback="true" MasterPageFile="~/MasterPage/Mp_Formulario.master" AutoEventWireup="false" CodeFile="Frm_OT_Planos.aspx.vb" Inherits="OrdenesDeTrabajo_Frm_OT_Planos" %>

<%@ Register TagPrefix="wuc" TagName="InformacionGeneral" src="~/ControlesDeUsuario/Wuc_OT_InformacionGeneral.ascx"  %>


<asp:Content runat="server" ID="Content1" ContentPlaceHolderID="cphFormulario">

    <header>
        <h2>
            Elaboración de Planos
        </h2>
    </header>

    <article class="formulario">
        <table>
            <tr>
                <th>Encargado del proyecto:</th>
                <td colspan="2">
                    <asp:Label runat="server" ID="lblEncargado"></asp:Label>
                </td>
            </tr>
        </table>
    </article>
    <wuc:InformacionGeneral ID="ctrl_InfoGeneral" runat="server" />
    <article class="formulario" id="formularioTotal" runat="server">
        <table>
            <%--<tr>
                <th>Observaciones:</th>
                <td>
                    <asp:Label runat="server" ID="lblObservaciones" Text="El nombre del archivo agregado debe iniciar con alguna de las siguientes letras:"></asp:Label>
                    <br /><br />
                    <asp:Label runat="server" ID="lblArquitectonico" Text="A-Nombre Archivo"></asp:Label>
                    <br />
                    <asp:Label runat="server" ID="lblMecanico" Text="M-Nombre Archivo"></asp:Label>
                    <br />
                    <asp:Label runat="server" ID="lblElectrico" Text="E-Nombre Archivo"></asp:Label>
                    <br />
                    <asp:Label runat="server" ID="lblEstructural" Text="S-Nombre Archivo"></asp:Label>
                </td>
            </tr>--%>
            <tr>
                <th>Tipo de Archivo:</th>
                <td>
                    <asp:dropdownlist runat="server" ValidationGroup="Archivos" id="ddlTipoArchivo" Width="59%" data-tipoControl="combo" AppendDataBoundItems="true" AutoPostBack="true"></asp:dropdownlist> <br />
                    <asp:RequiredFieldValidator runat="server" ID="rfvddlTipoArchivo" ValidationGroup="Archivos" ControlToValidate="ddlTipoArchivo" Display="Dynamic" ErrorMessage="Debe indicar el tipo de archivo">&nbsp;</asp:RequiredFieldValidator>                                
                </td>
            </tr>

            <tr>
                <th>Archivo(s) Adjunto(s)</th>
                <td>
                    <asp:FileUpload Width="59%" runat="server" ID="ifInfo" onchange="validaArchivo()"  />
                    <img runat="server" ID="imgExtensiones" data-tipo="tooltipExtensiones" class="tooltip"/>  
                    <asp:RequiredFieldValidator runat="server" ID="rfvIfInfo" ControlToValidate="ifInfo" ValidationGroup="Archivos" Display="Dynamic" ErrorMessage="Agregue un documento.">&nbsp;</asp:RequiredFieldValidator>                              
                </td>
            </tr>
            <tr>
                <th>Descripción:</th>
                <td>
                    <asp:TextBox Width="59%" runat="server" ID="txtDescripcionArchivo" ValidationGroup="Archivos"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ValidationGroup="Archivos" id="rfvtxtDescripcionArchivo" ControlToValidate="txtDescripcionArchivo" display="Dynamic" ErrorMessage="Debe indicar la descipción para identificar el archivo.">&nbsp;</asp:RequiredFieldValidator>
                    <asp:Button runat="server" ID="btnAgregarArchivo" Text="Agregar" ValidationGroup="Archivos" />
                       
                    <img runat="server" ID="imgSiglasPermitidas" data-tipo="tooltipExtensiones" class="tooltip" />   
                    <article class="listado sinBorde">
                        <asp:Repeater runat="server" ID="rpAdjunto">
                            <HeaderTemplate>
                                <table>
                                    <tr>
                                        <th>Archivo</th>
                                        <th>Descripción</th>
                                        <th>Tipo</th>
                                        <th>&nbsp;</th>
                                    </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr class="lineaDelListado">

                                    <td>
                                        <asp:LinkButton runat="server" ID="lnkArchivo"
                                            CommandArgument='<%#Container.ItemIndex%>'
                                            Style="text-decoration: underline; color: blue;"
                                            OnCommand="lnkArchivo_Command"
                                            Text="<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_ADJUNTO.NOMBRE_ARCHIVO)%>"></asp:LinkButton>
                                    </td>
                                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_ADJUNTO.DESCRIPCION)%></td>
                                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_ADJUNTO.DESC_TIPO_DOCUMENTO)%></td>
                                    <td>
                                        <asp:ImageButton runat="server" ID="ibBorrar" data-tipo="borrarRegistro" AlternateText="Borrar"
                                            CommandArgument='<%# String.Format("{0}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_ADJUNTO.ID_ADJUNTO_ORDEN_TRABAJO))%>' OnClick="ibBorrarAdjunto_Click"
                                            CommandName='<%#Container.ItemIndex%>'
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
                </td>
            </tr>
        </table>
    </article>

    <article class="areaBotones">
        <asp:Button runat="server" ValidationGroup="GYE" ID="btnGuardar" Text="Guardar" />
        <asp:Button runat="server" ValidationGroup="GYE" ID="btnFinalizar" text="Finalizar Etapa" />
        <asp:Button runat="server" ID="btnRegresar" text="Regresar" />
    </article>


    <article id="arAlertasDelFormulario"></article>
    <article id="arpopupConfirmaDeseaContinuar"></article>
    <article id="arpopupConfirmaDeseaBorrar"></article>
        <script type="text/javascript">
            $(document).ready(function () {

                $('[data-tipo="tooltipExtensiones"]').attr('src', '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Icono_Ayuda.png")%>');


                $('[data-tipo="borrarRegistro"]').attr('src', '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Borrar.png")%>');

                $('[data-tipo="borrarRegistro"]').on({
                    'mouseover': function () { $(this).attr('src', '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Icono_Borrar.png")%>'); },

                    'mouseout': function () { $(this).attr('src', '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Borrar.png")%>'); }
                });

            });

            function validaArchivo() {
                var vlo_InputArchivo = document.getElementById('<%=ifInfo.ClientID%>');
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

            function mostrarAlertaGuardadoExitoso() {
                deshabilitarControl('#<%=btnGuardar.ClientID%>');
                deshabilitarControl('#<%=btnFinalizar.ClientID%>');
                $('.formulario').attr('disabled', 'disabled');

                mostrarAlerta(
                '#arAlertasDelFormulario',
                {
                    mensaje: 'Se han guardado los datos de los planos correctamente',
                    tipo: "exito",
                    transparencia: 0.9,
                    posicion: 'center',
                    onClosed: function () { window.location='Lst_OT_GestionProfesionalesDisenio.aspx'; }
                });
            };

            function deshabilitar(){
                deshabilitarJerarquiaDeControles(document.getElementById('<%=formularioTotal.ClientID%>'));
                deshabilitarControl('#<%=btnFinalizar.ClientID%>');
                deshabilitarControl('#<%=btnGuardar.ClientID%>');
            };


        </script>

</asp:Content>