<%@ Page Title="Catálogo de Tipos de Documento" MasterPageFile="~/MasterPage/Mp_Formulario.master" Language="VB" AutoEventWireup="false" CodeFile="Frm_OT_TipoDocumento.aspx.vb" Inherits="Catalogos_Frm_OT_TipoDocumento" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %> 
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>


<asp:Content runat="server" ContentPlaceHolderID="cphFormulario" ID="Content1">

    <header>
        <h2>
            <asp:Label runat="server" ID="lblAccion"></asp:Label>
        </h2>
    </header>

    <article class="tituloSeccion">
        Datos del Tipo de Documento
    </article>

    <article class="formulario">
        <table>
            <tr>
                <th>Descripción</th>
                <td>
                    <asp:TextBox runat="server" ID="txtDescripcion" Width="40%" data-tipoControl="texto"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ID="rvfTxtDescripcion" ControlToValidate="txtDescripcion" Display="Dynamic" ErrorMessage="La Descripción del tipo de documento a ingresar es requerida.">&nbsp;</asp:RequiredFieldValidator>
                    <br />
                    <span id="spContadorTxtDescripcion" class="contadorCaracteresRestantes"></span>
                </td>
            </tr>
            <tr>
                <th>Tamaño Máximo</th>
                <td>
                    <asp:TextBox runat="server" ID="txtTamanioMaximo" Width="40%" data-tipoControl="texto"></asp:TextBox>
                    <asp:Label runat="server" ID="lblMegas">Megas</asp:Label>
                    <asp:RequiredFieldValidator runat="server" ID="rfvTamanio" ControlToValidate="txtTamanioMaximo" Display="Dynamic" ErrorMessage="El tamaño máximo del tipo de documento a ingresar es requerida.">&nbsp;</asp:RequiredFieldValidator>
                    <ajax:FilteredTextBoxExtender ID="ftbTxtTamanioMaximo" runat="server" TargetControlID="txtTamanioMaximo" FilterMode="ValidChars" FilterType="Numbers"></ajax:FilteredTextBoxExtender>
                    <br />
                    <span id="spContadortxtTamanioMaximo" class="contadorCaracteresRestantes"></span>
                </td>
            </tr>
            <tr runat="server" id="trEstado">
                <th>Estado</th>
                <td>
                    <asp:DropDownList Width="40%" runat="server" ID="ddlEstado" AppendDataBoundItems="true" data-tipoControl="combo"></asp:DropDownList>
                    <asp:RequiredFieldValidator runat="server" ID="rfvDdlEstado" ControlToValidate="ddlEstado" Display="Dynamic" ErrorMessage="El estado del espacio es requerido.">&nbsp;</asp:RequiredFieldValidator>
                </td>
            </tr>
        </table>
    </article>

    <article data-grupo="Listado" class="tituloSeccion">
        Formatos admitidos
    </article>

    <article class="formulario">
        <table>
            <tr>
                <th>Formato</th>
                <td>
                    <asp:TextBox runat="server" ID="txtFormatoDescripcion" Width="40%" data-tipoControl="texto"></asp:TextBox>
                    <asp:LinkButton runat="server" ID="lnkAgregarFormato" Style="text-decoration: underline; color: blue;" Text="Agregar Formato"></asp:LinkButton>
                    
                    <br />
                    <span id="spContadorFormatoDescripcion" class="contadorCaracteresRestantes"></span>
                </td>
            </tr>
        </table>
    </article>

    <article class="listado sinBorde" style="width: 50% !important;">
    <asp:Repeater runat="server" ID="rpFormatos">

        <HeaderTemplate>
            <table>
                <tr>
                    <th>Descripción</th>
                    <th>&nbsp;</th>
                </tr>
        </HeaderTemplate>

        <ItemTemplate>
            <tr class="lineaDelListado">
                
                <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.OTM_TIPO_DOCUMENTO.FORMATOS_ADMITIDOS)%></td>
                <td>
                    <asp:ImageButton ID="ibBorrar" runat="server" CausesValidation="false" ToolTip="Borrar" data-tipo="borrarRegistro"
                        CommandArgument='<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.OTM_TIPO_DOCUMENTO.ID_TIPO_DOCUMENTO)%>'
                        CommandName='<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.OTM_TIPO_DOCUMENTO.FORMATOS_ADMITIDOS)%>'
                        ImageUrl='<%#AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Borrar.png")%>'
                        onmouseover='<%#String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Icono_Borrar.png"))%>'
                        onmouseout='<%#String.Format("this.src=""{0}""", AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Borrar.png"))%>'
                        OnClick="ibBorrar_Click" />
                </td>
            </tr>
        </ItemTemplate>

        <FooterTemplate>
            </table>
        </FooterTemplate>

    </asp:Repeater>
    </article>

    <article class="areaBotones">
        <asp:Button runat="server" ID="btnAceptar" Text="Aceptar" />
        <input type="button" data-tipo="limpiarFormulario" value="Limpiar Formulario" id="btnLimpiarFormulario" />
        <input type="button" value="Cancelar" id="btnCancelar" />
    </article>

    <article id="arPopupGenerico"></article>
    <article id="popupConfirmaDeseaBorrar"></article>
    <article id="arAlertasDelFormulario"></article>
    <article id="arPopupDelFormulario"></article>

    <script type="text/javascript">
       
        $(document).ready(function () {

            $('#btnCancelar').click(function () {
                regresarAlListado();
            });

            configurarLongitudMaximaTexto('#<%=Me.txtFormatoDescripcion.ClientID%>', <%=Utilerias.OrdenesDeTrabajo.Modelo.OTM_TIPO_DOCUMENTO.FORMATOS_ADMITIDOS_BD_TAMANO%>);
            configurarLongitudMaximaTexto('#<%=Me.txtDescripcion.ClientID%>', <%=Utilerias.OrdenesDeTrabajo.Modelo.OTM_TIPO_DOCUMENTO.DESCRIPCION_BD_TAMANO%>);
            configurarLongitudMaximaTexto('#<%=Me.txtTamanioMaximo.ClientID%>', <%=Utilerias.OrdenesDeTrabajo.Modelo.OTM_TIPO_DOCUMENTO.TAMANIO_MAXIMO_BD_TAMANO%>);
            configurarContadorCaracteresRestantes('#<%=Me.txtDescripcion.ClientID%>', '#spContadorTxtDescripcion');
            configurarContadorCaracteresRestantes('#<%=Me.txtTamanioMaximo.ClientID%>', '#spContadortxtTamanioMaximo');
            configurarContadorCaracteresRestantes('#<%=Me.txtFormatoDescripcion.ClientID%>', '#spContadorFormatoDescripcion');
            habilitarTooltipGenerico();
            
            $('[data-tipo="borrarRegistro"]').click(function () { return mostrarPopupConfirmaDeseaBorrarRegistro($(this).data("uniqueid")); });

        });

        function deshabilitarFormulario() {
            deshabilitarControl('#<%=Me.btnAceptar.ClientID%>');
            deshabilitarControl('#btnLimpiarFormulario');
            deshabilitarControl('#btnCancelar');
            $('.formulario').attr('disabled', 'disabled');
        };

        function mostrarAlertaError(pvc_Msj) {
            mostrarAlerta(
                '#arAlertasDelFormulario',
                {
                    mensaje: pvc_Msj,
                    tipo: "peligro",
                    transparencia: 0.9,
                    posicion: 'center',
                    permiteCerrar: true
                }
            );
        };

        function mostrarAlertaActualizacionExitosa() {
            deshabilitarFormulario();

            mostrarAlerta(
                '#arAlertasDelFormulario',
                {
                    mensaje: 'Se ha actualizado la información del espacio',
                    tipo: "exito",
                    transparencia: 0.9,
                    posicion: 'center',
                    permiteCerrar: true,
                    onClosed: function () { regresarAlListado(); }
                }
            );
        };

        function mostrarAlertaError(pvc_Msj) {
            mostrarAlerta(
                '#arAlertasDelFormulario',
                {
                    mensaje: pvc_Msj,
                    tipo: "peligro",
                    transparencia: 0.9,
                    posicion: 'center',
                    permiteCerrar: true
                }
            );
        };

        function mostrarPopupRegistroExitoso() {
            var vlo_ConfiguracionPopup = {
                titulo: 'Catálogo de Tipos de Documento',
                mensaje: 'Se ha registrado la información del Tipo de Documento.<br /><strong>¿Desea registrar otro Tipo de documento?</strong>',
                onClosed: function () { regresarAlListado(); },
                botones:
                    [
                        {
                            idControl: "btnSi",
                            textoBoton: "Sí",
                            onClick: function () { window.location = 'Frm_OT_TipoDocumento.aspx?pvn_Operacion=<%=Utilerias.OrdenesDeTrabajo.eOperacion.Agregar%>'; }
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

        function mostrarAlertaActualizacionExitosa() {
            deshabilitarFormulario();

            mostrarAlerta(
                '#arAlertasDelFormulario',
                {
                    mensaje: 'Se ha actualizado la información del Tipo de Documento',
                    tipo: "exito",
                    transparencia: 0.9,
                    posicion: 'center',
                    permiteCerrar: true,
                    onClosed: function () { regresarAlListado(); }
                }
            );
        };

        
        function MensajePopup(pvc_Mensaje, pvc_PaginaDestino) {

            var vlo_ConfiguracionPopup = {
                titulo: '<em>Mensajes del Sistema</em>',
                mensaje: pvc_Mensaje,
                onClosed: function (e) { 
                    $(this).removeAttr('href'); 
                    cerrarPopup();
                    if(pvc_PaginaDestino != ''){
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
                        if(pvc_PaginaDestino != ''){
                            redireccionarListado(pvc_PaginaDestino);
                        }
                    }                                
                }
            ]
            };

            $('#arPopupGenerico').popup(vlo_ConfiguracionPopup);

            window.location = '#arPopupGenerico';

        };

        function redireccionarListado(pvc_PaginaDestino){        
            window.location = pvc_PaginaDestino
        };

        function mostrarPopupConfirmaDeseaBorrarRegistro(pvo_UniqueIdControl) {
            var vlo_ConfiguracionPopup = {
                titulo: 'Catálogo de apoyos',
                mensaje: 'Realmente desea borrar el registro seleccionado?',
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

            $('#popupConfirmaDeseaBorrar').popup(vlo_ConfiguracionPopup);

            window.location = '#popupConfirmaDeseaBorrar';

            return false;
        };

        function regresarAlListado() {
            window.location = 'Lst_OT_TipoDocumento.aspx';
        };

    </script>

</asp:Content>
