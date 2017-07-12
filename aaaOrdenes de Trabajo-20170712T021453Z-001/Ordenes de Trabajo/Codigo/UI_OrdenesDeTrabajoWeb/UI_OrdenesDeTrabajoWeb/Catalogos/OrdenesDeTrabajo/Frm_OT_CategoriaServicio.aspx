<%@ Page Title="Catálogo de Categorias de Servicio" Language="VB" MasterPageFile="~/MasterPage/Mp_Formulario.master" AutoEventWireup="false" CodeFile="Frm_OT_CategoriaServicio.aspx.vb" Inherits="Catalogos_Frm_OT_CategoriaServicio" %>

<%@ Register Src="~/Controles/wucEmpleadosEU.ascx" TagName="wuc_EmpleadosEU" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphFormulario" runat="Server">
    <header>
        <h2>
            <asp:Label runat="server" ID="lblAccion"></asp:Label></h2>
    </header>
    <article class="tituloSeccion">
        Datos de la Categoría de Servicio
    </article>
    <article class="formulario">
        <table>
            <tr>
                <th>Descripción</th>
                <td>
                    <asp:TextBox runat="server" ID="txtDescripcion" Width="40%" data-tipoControl="texto"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ID="rfvTxtDescripcion" ControlToValidate="txtDescripcion" Display="Dynamic" ErrorMessage="La descripción de la Categoria de Servicio es requerida." ValidationGroup="Aceptar">&nbsp;</asp:RequiredFieldValidator>
                    <br />
                    <span id="spContadorTxtDescripcion" class="contadorCaracteresRestantes"></span>
                </td>
            </tr>
            <tr>
                <th>Supervisor</th>
                <td>
                    <asp:UpdatePanel runat="server" ID="upTxtSupervisor" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:TextBox runat="server" ID="txtSupervisor" Width="40%" data-tipoControl="texto" AutoPostBack="true" ></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ID="rfvTxtSupervisor" ControlToValidate="txtSupervisor" Display="Dynamic" ErrorMessage="El numero de Identificación del Supervisor es requerido." ValidationGroup="Aceptar">&nbsp;</asp:RequiredFieldValidator>
                            <img id="imgMostrarFiltroFuncionarios" title="Buscar Funcionarios" alt="Buscar Funcionarios" src="" />
                            <br />
                            <span id="spContadorTxtSupervisor" class="contadorCaracteresRestantes"></span>
                        </ContentTemplate>
                        <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="txtSupervisor" EventName="TextChanged" />
                            <asp:AsyncPostBackTrigger ControlID="btnLimpiarFormulario" EventName="Click" />
                         </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <th>Nombre</th>
                <td>
                    <asp:UpdatePanel runat="server" ID="upTxtNombre" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:Label runat="server" ID="lblNombre"></asp:Label>
                        </ContentTemplate>
                        <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="txtSupervisor" EventName="TextChanged" />
                        <asp:AsyncPostBackTrigger ControlID="wuc_EmpleadosEU" />
                         </Triggers>
                    </asp:UpdatePanel>

                </td>
            </tr>
            <tr>
                <th>Siglas</th>
                <td>
                    <asp:TextBox Width="40%" runat="server" ID="txtSiglas" data-tipoControl="texto" MaxLength="3"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ID="rfvTxtSiglas" ControlToValidate="txtSiglas" Display="Dynamic" ErrorMessage="Las Siglas de la Categoria de Servicio son requeridas." ValidationGroup="Aceptar">&nbsp;</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <th>Taller</th>
                <td>
                    <asp:DropDownList Width="40%" runat="server" ID="ddlTaller" AppendDataBoundItems="true" data-tipoControl="combo"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <th>Requiere Ficha</th>
                <td>
                    <asp:CheckBox runat="server" ID="chkFicha" data-tipoControl="checkbox"></asp:CheckBox>
                </td>
            </tr>
            <tr runat="server" id="trEstado">
                <th>Estado</th>
                <td>
                    <asp:DropDownList Width="40%" runat="server" ID="ddlEstado" AppendDataBoundItems="true" data-tipoControl="combo"></asp:DropDownList>
                    <asp:RequiredFieldValidator runat="server" ID="rfvDdlEstado" ControlToValidate="ddlEstado" Display="Dynamic" ErrorMessage="Debe seleccionar el estado." ValidationGroup="Aceptar">&nbsp;</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <th>Visibilidad de la Categoria</th>
                <td>
                    <asp:DropDownList Width="40%" runat="server" ID="ddlCategoriasOcultas" AppendDataBoundItems="true" data-tipocontrol="combo"></asp:DropDownList>
                </td>
            </tr>
        </table>
    </article>

    <article>
        <asp:Label runat="server" ID="lblNumeroEmpleado" hiddenfield="true"></asp:Label>
    </article>

    <article class="areaBotones">
        <asp:Button runat="server" ID="btnAceptar" Text="Aceptar" ValidationGroup="Aceptar" />
        <asp:Button runat="server" ID="btnLimpiarFormulario" Text="Limpiar Formulario"  data-tipo="limpiarFormulario" />  <%--se cambia tipo de boton para controlar el evento click en el update panel de id supervisor para correcto funcionamiento del text changed--%>
        <%--<input id="btnLimpiarFormulario" type="button" data-tipo="limpiarFormulario" value="Limpiar Formulario" />--%>
        <input id="btnCancelar" type="button" value="Cancelar" />
    </article>

    <article id="arPopupDelFormulario"></article>
    <article id="arAlertasDelFormulario"></article>

    <%--Popup para búsqueda de funcionario--%>
    <article id="PopUpBusquedaFuncionario" class="ventanaEmergente">
        <article class="formulario" style="width: 850px!important;">
            <a href="#CerrarPopUpBusquedaFuncionario" title="Cerrar Ventana" class="botonCerrarVentanaEmergente">X</a>
            <br />
            <asp:UpdatePanel ID="upContactoConv" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <uc1:wuc_EmpleadosEU runat="server" ID="wuc_EmpleadosEU" />
                    <div class="areaBotones">
                        <a href="#CerrarPopUpBusquedaFuncionario" title="Regresar">Regresar</a>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </article>
    </article>
    <%--Fin Popup para búsqueda de funcionario--%>

    <script type="text/javascript">
        function regresarAlListado() {
            window.location = 'Lst_OT_CategoriaServicio.aspx';
        }

        function deshabilitarFormulario() {
            deshabilitarControl('#<%=Me.btnAceptar.ClientID%>');
            deshabilitarControl('#btnLimpiarFormulario');
            deshabilitarControl('#btnCancelar');

            $('.formulario').attr('disabled','disabled');
        }

        function mostrarPopupRegistroExitoso () {
            var vlo_ConfiguracionPopup = {
                titulo: 'Catálogo de Categorias de Servicio',
                mensaje: 'Se a registrado la informacion de la categoría.<br/><strong>Desea registrar otra categoría?</strong>',
                botones:
                    [  //en corchetes indican arreglos y los parentesis cuadrados indican inicio o fin de funcionalidad o objeto, $librerias de jquery
                        {
                            idControl: "btnSi",
                            textoBoton: "Si",
                            onClick:
                                function (e) {
                                    window.location = 'Frm_OT_CategoriaServicio.aspx?pvn_Operacion=<%= Utilerias.OrdenesDeTrabajo.eOperacion.Agregar%>'; //pvn_Operacion????
                                }
                        },
                        {
                            idControl: "btnNo",
                            textoBoton: "No",
                            onClick: function (e) { regresarAlListado();}
                                
                        },
                        
                    ]
            };

            $('#arPopupDelFormulario').popup(vlo_ConfiguracionPopup);
            window.location = '#arPopupDelFormulario';
            
        }

        function mostrarAlertaNoEncontrado() {
            mostrarAlerta(
                '#arAlertasDelFormulario',
                {
                    mensaje: "No se han encontrado funcionarios con el número de cédula indicado",
                    tipo: "advertencia",
                    transparencia: 0.9,
                    posicion: 'center',
                    permiteCerrar: true
                }
                );

        }

        function mostrarAlertaError(pvc_Mensaje) {
            mostrarAlerta(
                '#arAlertasDelFormulario',
                {
                    mensaje: pvc_Mensaje,
                    tipo: "peligro",
                    transparencia: 0.9,
                    posicion: 'center',
                    permiteCerrar: true
                }
                );
        }

        function mostrarAlertaActualizacionExitosa() {
            deshabilitarFormulario();

            mostrarAlerta(
                '#arAlertasDelFormulario',
                {
                    mensaje: 'Se ha actualizado la información de la categoría',
                    tipo: "exito",
                    transparencia: 0.9,
                    posicion: 'center',
                    permiteCerrar: true,
                    onClosed: function () { regresarAlListado(); }
                }
                );
        }
            
        function mostrarAlertaLlaveIncorrecta() {
            deshabilitarFormulario();

            mostrarAlerta(
                '#arAlertasDelFormulario',
                {
                    mensaje: 'El número de identificación provisto no pertenece a ninguna categoría de servicio registrada en el sistema',
                    tipo: "peligro",
                    transparencia: 0.9,
                    posicion: 'center',
                    permiteCerrar: true,
                    onClosed: function () { regresarAlListado(); }
                }
                );

        }

        function ocultarFiltroFuncionario(){
            window.location = '#CerrarPopUpBusquedaFuncionario';
        }

        function establecerControles(){
            $('#imgMostrarFiltroFuncionarios').click(function(){
                window.location = '#PopUpBusquedaFuncionario';
            });

            configurarLongitudMaximaTexto('#<%=Me.txtSupervisor.ClientID%>', <%=Utilerias.OrdenesDeTrabajo.Constantes.TAMANNO_CEDULA%>);
            configurarContadorCaracteresRestantes('#<%=Me.txtSupervisor.ClientID%>', '#spContadorTxtSupervisor');

            cargarLupa()
        }

        function cargarLupa(){
            permutarImagenes('#imgMostrarFiltroFuncionarios',
                            '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Lupa.png")%>',
                            '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Lupa.png")%>',
                            '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Lupa.png")%>'
                        );
        }

        function inicializarFormulario(){
            $('#btnCancelar').click(function () {
                regresarAlListado();
            });

            // Para evitar que al dar enter se disparara el summit de 
            $(window).keypress(function (e) {
                if (e.keyCode == 13) {
                    return false;
                }
            });

            $(window).keydown(function a(e) {

                if (e.keyCode == 13) {
                    return false;
                }
            });

            establecerControles();

            configurarLongitudMaximaTexto('#<%=Me.txtDescripcion.ClientID%>', <%=Utilerias.OrdenesDeTrabajo.Modelo.OTM_CATEGORIA_SERVICIO.DESCRIPCION_BD_TAMANO%>);
            configurarContadorCaracteresRestantes('#<%=Me.txtDescripcion.ClientID%>', '#spContadorTxtDescripcion');
                        

            configurarBotonPorDefecto('<%= btnAceptar.ClientID%>');
        }

        $(document).ready(function () {
            inicializarFormulario();
                        
        });
    </script>
</asp:Content>

