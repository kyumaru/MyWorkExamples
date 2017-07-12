<%@ Page Title="Catálogo de Sectores y Talleres" Language="VB" MasterPageFile="~/MasterPage/Mp_Formulario.master" AutoEventWireup="false" CodeFile="Frm_OT_SectorTaller.aspx.vb" Inherits="Catalogos_Frm_OT_SectorTaller" %>

<%@ Register Src="~/Controles/wucEmpleadosEU.ascx" TagName="wuc_EmpleadosEU" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphFormulario" Runat="Server">
    <header>
        <h2>
            <asp:Label runat="server" ID="lblAccion"></asp:Label></h2>
    </header>
    <article class="tituloSeccion">
        Datos del Sector o Taller
    </article>
    <article class="formulario">
        <table>
            <tr>
                <th>Nombre</th>
                <td>
                    <asp:TextBox runat="server" ID="txtNombre" Width="40%" data-tipoControl="texto"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ID="rfvTxtNombre" ControlToValidate="txtNombre" Display="Dynamic" ErrorMessage="El nombre del Sector o Taller es requerido." ValidationGroup="Aceptar">&nbsp;</asp:RequiredFieldValidator>
                    <br />
                    <span id="spContadorTxtNombre" class="contadorCaracteresRestantes"></span>
                </td>
            </tr>
            <tr>
                <th>Cédula Coordinador</th>
                <td>
                    <asp:UpdatePanel runat="server" ID="upTxtIdCoordinador" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:TextBox runat="server" ID="txtIdCoordinador" Width="40%" data-tipoControl="texto" AutoPostBack="true" ></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ID="rfvTxtIdCoordinador" ControlToValidate="txtIdCoordinador" Display="Dynamic" ErrorMessage="El numero de Identificación del Coordinador es requerido." ValidationGroup="Aceptar">&nbsp;</asp:RequiredFieldValidator>
                            <asp:LinkButton ID="lnkEjecutarBusquedaCoordinador" runat="server">
                                <img id="imgBuscarCoordinador" title="Buscar Registro" alt="Buscar Registro" src="" />
                            </asp:LinkButton>
                            <br />
                            <span id="spContadorTxtIdCoordinador" class="contadorCaracteresRestantes"></span>
                        </ContentTemplate>
                        <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="txtIdCoordinador" EventName="TextChanged" />
                            <asp:AsyncPostBackTrigger ControlID="btnLimpiarFormulario" EventName="Click" />
                         </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <th>Nombre Coordinador</th>
                <td>
                    <asp:UpdatePanel runat="server" ID="upTxtNombreCoordinador" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:Label runat="server" ID="lblNombreCoordinador" data-tipocontrol="etiqueta"></asp:Label>
                        </ContentTemplate>
                        <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="txtIdCoordinador" EventName="TextChanged" />
                        <asp:AsyncPostBackTrigger ControlID="wuc_EmpleadosEU" />
                         </Triggers>
                    </asp:UpdatePanel>

                </td>
            </tr>
            <tr>
                <th>Cédula Sustituto</th>
                <td>
                    <asp:UpdatePanel runat="server" ID="upTxtIdSustituto" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:TextBox runat="server" ID="txtIdSustituto" Width="40%" data-tipoControl="texto" AutoPostBack="true" ></asp:TextBox>
                            <asp:LinkButton ID="lnkEjecutarBusquedaSustituto" runat="server">
                                <img id="imgBuscarSustituto" title="Buscar Registro" alt="Buscar Registro" src="" />
                            </asp:LinkButton>
                            <br />
                            <span id="spContadorTxtIdSustituto" class="contadorCaracteresRestantes"></span>
                        </ContentTemplate>
                        <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="txtIdSustituto" EventName="TextChanged" />
                            <asp:AsyncPostBackTrigger ControlID="btnLimpiarFormulario" EventName="Click" />
                         </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <th>Nombre Sustituto</th>
                <td>
                    <asp:UpdatePanel runat="server" ID="upTxtNombreSustituto" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:Label runat="server" ID="lblNombreSustituto" data-tipocontrol="etiqueta"></asp:Label>
                        </ContentTemplate>
                        <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="txtIdSustituto" EventName="TextChanged" />
                        <asp:AsyncPostBackTrigger ControlID="wuc_EmpleadosEU" />
                         </Triggers>
                    </asp:UpdatePanel>

                </td>
            </tr>
            <tr>
                <th>Clasificación</th>
                <td>
                    <asp:RadioButton runat="server" ID="rdbSector" Text="Sector" GroupName="Clasificacion" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:RadioButton runat="server" ID="rdbTaller" Text="Taller" GroupName="Clasificacion" />
                </td>
            </tr>
            <tr runat="server" id="trEstado">
                <th>Estado</th>
                <td>
                    <asp:DropDownList Width="40%" runat="server" ID="ddlEstado" AppendDataBoundItems="true" data-tipoControl="combo"></asp:DropDownList>
                    <asp:RequiredFieldValidator runat="server" ID="rfvDdlEstado" ControlToValidate="ddlEstado" Display="Dynamic" ErrorMessage="Debe seleccionar el estado." ValidationGroup="Aceptar">&nbsp;</asp:RequiredFieldValidator>
                </td>
            </tr>
        </table>
    </article>

    <article>
        <asp:Label runat="server" ID="lblNumeroEmpleado" hiddenfield="true"></asp:Label>
    </article>

    <article class="areaBotones">
        <asp:Button runat="server" ID="btnAceptar" Text="Aceptar" ValidationGroup="Aceptar" />
        <asp:Button runat="server" ID="btnLimpiarFormulario" Text="Limpiar Formulario"  data-tipo="limpiarFormulario" />  <%--se cambia tipo de boton para controlar el evento click en el update panel de id coordinador y id sustituto para correcto funcionamiento del text changed--%>
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
            window.location = 'Lst_OT_SectorTaller.aspx';
        }

        function deshabilitarFormulario() {
            deshabilitarControl('#<%=Me.btnAceptar.ClientID%>');
            deshabilitarControl('#btnLimpiarFormulario');
            deshabilitarControl('#btnCancelar');

            $('.formulario').attr('disabled','disabled');
        }

        function mostrarPopupRegistroExitoso () {
            var vlo_ConfiguracionPopup = {
                titulo: 'Catálogo de Sectores y Talleres',
                mensaje: 'Se a registrado la informacion del sector o taller.<br/><strong>Desea registrar otro?</strong>',
                botones:
                    [  //en corchetes indican arreglos y los parentesis cuadrados indican inicio o fin de funcionalidad o objeto, $librerias de jquery
                        {
                            idControl: "btnSi",
                            textoBoton: "Si",
                            onClick:
                                function (e) {
                                    window.location = 'Frm_OT_SectorTaller.aspx?pvn_Operacion=<%= Utilerias.OrdenesDeTrabajo.eOperacion.Agregar%>'; //pvn_Operacion????
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

                    function mostrarAlertaActualizacionExitosa() {
                        deshabilitarFormulario();

                        mostrarAlerta(
                            '#arAlertasDelFormulario',
                            {
                                mensaje: 'Se ha actualizado la información del sector o taller',
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
                                mensaje: 'El número de identificación provisto no pertenece a ningún sector o taller registrada en el sistema',
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

                    function mostrarPopUp(pvc_IdPopup) {
                        window.location = pvc_IdPopup;
                    };

                    function establecerControles(){
                        $('#imgMostrarFiltroFuncionarios').click(function(){
                            window.location = '#PopUpBusquedaFuncionario';
                        });
                        
                        $('#imgMostrarFiltroSustituto').click(function(){
                            window.location = '#PopUpBusquedaFuncionario';
                        });

                        configurarLongitudMaximaTexto('#<%=Me.txtIdCoordinador.ClientID%>', <%=Utilerias.OrdenesDeTrabajo.Constantes.TAMANNO_CEDULA%>);
                        configurarContadorCaracteresRestantes('#<%=Me.txtIdCoordinador.ClientID%>', '#spContadorTxtIdCoordinador');
                        
                        configurarLongitudMaximaTexto('#<%=Me.txtIdSustituto.ClientID%>', <%=Utilerias.OrdenesDeTrabajo.Constantes.TAMANNO_CEDULA%>);
                        configurarContadorCaracteresRestantes('#<%=Me.txtIdSustituto.ClientID%>', '#spContadorTxtIdSustituto');

                        cargarLupa();
                    }

                    function cargarLupa(){
                        permutarImagenes('#imgBuscarCoordinador',
                            '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Lupa.png")%>',
                            '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Lupa.png")%>',
                            '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Lupa.png")%>'
                        );

                        permutarImagenes('#imgBuscarSustituto',
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

                        $(window).keydown(function a(e) {

                            if (e.keyCode == 13) {
                                return false;
                            }
                        });

                        establecerControles();

                        configurarLongitudMaximaTexto('#<%=Me.txtNombre.ClientID%>', <%=Utilerias.OrdenesDeTrabajo.Modelo.OTM_SECTOR_TALLER.NOMBRE_BD_TAMANO%>);
                        configurarContadorCaracteresRestantes('#<%=Me.txtNombre.ClientID%>', '#spContadorTxtNombre');
                        

                        configurarBotonPorDefecto('<%= btnAceptar.ClientID%>');
                    }

                    $(document).ready(function () {
                        inicializarFormulario();
                    });
    </script>
</asp:Content>

