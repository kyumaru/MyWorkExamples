<%@ Page Language="VB" MasterPageFile="~/MasterPage/Mp_Listado.master" AutoEventWireup="false" CodeFile="Lst_OT_AsignacionRolDelegado.aspx.vb" Inherits="Catalogos_Lst_OT_AsignacionRolDelegado" %>

<%@ Register TagPrefix="wuc" Namespace="Utilerias.WebForms.Controles" Assembly="Utilerias.WebForms.Controles" %>
<%@ Register Src="~/Controles/wucEmpleadosEU.ascx" TagName="wuc_EmpleadosEU" TagPrefix="uc1" %>

<asp:Content runat="server" ContentPlaceHolderID="cphFormulario" ID="Content1">

    <header>
        <h2>Asignación de delegado de jefe de sección de mantenimiento y construcción</h2>
    </header>


    <article class="formulario">
        <table>
            <tr>
                <th>Identificación</th>
                <td>
                    <asp:UpdatePanel runat="server" ID="upTxtIdentificacion" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:TextBox runat="server" ID="txtIdentificacion" Width="30%" data-tipoControl="texto" AutoPostBack="true"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ID="rfvTxtIdentificacion" ControlToValidate="txtIdentificacion" Display="Dynamic" ErrorMessage="El numero de Identificación es requerido." ValidationGroup="Aceptar">&nbsp;</asp:RequiredFieldValidator>
                            <asp:LinkButton ID="lnkEjecutarBusquedaFuncionario" runat="server">
                                <img id="imgBuscarFuncionario" title="Buscar Registro" alt="Buscar Registro" src="" />
                            </asp:LinkButton>
                            <br />
                            <span id="spContadorTxtIdentificacion" class="contadorCaracteresRestantes"></span>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="txtIdentificacion" EventName="TextChanged" />
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
                            <asp:AsyncPostBackTrigger ControlID="txtIdentificacion" EventName="TextChanged" />
                            <asp:AsyncPostBackTrigger ControlID="wuc_EmpleadosEU" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
    </article>

    <article class="areaBotones">
        <asp:Button runat="server" ID="btnAceptar" Text="Aceptar" ValidationGroup="Aceptar" />
        <asp:Button runat="server" type="button" text="Cancelar" ID="btnCancelar"/>
    </article>

    <article data-grupo="Listado" class="listado">

        <asp:Repeater runat="server" ID="rpUsuarios">
            <HeaderTemplate>
                <table>
                    <tr>
                        <th>Identificación
                        </th>
                        <th>Nombre
                        </th>
                        <th>&nbsp;</th>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr class="lineaDelListado">
                    <td><%#Eval("CEDULA")%></td>
                    <td><%#Eval("NOMBRE_EMPLEADO")%></td>
                    <td>
                        <asp:ImageButton runat="server" ID="ibBorrar" AlternateText="Borrar la Persona" data-tipo="borrarRegistro" 
                            CommandArgument='<%#Eval("CEDULA")%>' src="" OnClick="ibBorrar_Click" />
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>

    </article>
        <article data-grupo="Listado" class="areaPaginadorListado">
        <wuc:PaginadorNumerico runat="server" ID="pnRpLugares" /> <%--Paginador del repeater RpCategorias--%>
    </article>
    <article data-grupo="Listado" class="areaCantidadDeRegistro">
        <asp:Label runat="server" ID="lblCantidadRegistro" Text=""> </asp:Label> 
    </article>

    
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

    <article id="arAlerta"></article>
    <article id="arPopupGenerico"></article>  
    <article id="popupConfirmacionDeseaBorrar"></article>

    <script type="text/javascript">
            function mostrarAlertaRegistroBorrado() {
                mostrarAlerta(
                    '#arAlerta',
                    {
                        mensaje: "Se ha removido el jefe delegado escogido",
                        tipo: "exito",
                        transparencia: 0.9,
                        posicion: 'center',
                        permiteCerrar: true
                    }
                    );

            };

            function mostrarAlertaRegistroNoBorrado() {
                mostrarAlerta(
                    '#arAlerta',
                    {
                        mensaje: "No ha sido posible borrar el jefe delegado escogido",
                        tipo: "advertencia",
                        transparencia: 0.9,
                        posicion: 'center',
                        permiteCerrar: true
                    }
                    );

            };

        function mostrarPopupConfirmaDeseaBorrarRegistro(pvc_UniqueIdControl) {
            var vlo_ConfiguracionPopup = {
                titulo: 'Catálogo de Asignación de delegado de jefe de sección de mantenimiento y construcción',
                mensaje: 'Realmente desea borrar a la persona seleccionada?',
                botones:
                    [  //en corchetes indican arreglos y los parentesis cuadrados indican inicio o fin de funcionalidad o objeto, $librerias de jquery
                        {
                            idControl: "btnSi",
                            textoBoton: "Si",
                            onClick:
                                function () {
                                    $(this).attr("disabled", "disabled");  //cuando le de click al boton desabilitelo
                                    __doPostBack(pvc_UniqueIdControl, ''); //control de .net para q vaya al servidor a eliminar
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

                        },
                    ]
            };

            $('#popupConfirmacionDeseaBorrar').popup(vlo_ConfiguracionPopup);
            window.location = '#popupConfirmacionDeseaBorrar';
            return false; //detener comportamiento de ir al servidor hasta aceptar
        };

        function mostrarAlertaLlaveIncorrecta() {
            deshabilitarFormulario();

            mostrarAlerta(
                '#arAlerta',
                {
                    mensaje: 'El número de identificación del usuario provisto no pertenece a ningún Empleado registrado en el sistema',
                    tipo: "peligro",
                    transparencia: 0.9,
                    posicion: 'center',
                    permiteCerrar: true,
                    onClosed: function () { regresarPrincipal(); }
                }
                );

        };

        function establecerControles() {
            $('#imgMostrarFiltroFuncionarios').click(function () {
                window.location = '#PopUpBusquedaFuncionario';
            });

            configurarLongitudMaximaTexto('#<%=Me.txtIdentificacion.ClientID%>', <%=Utilerias.OrdenesDeTrabajo.Constantes.TAMANNO_CEDULA%>);
            configurarContadorCaracteresRestantes('#<%=Me.txtIdentificacion.ClientID%>', '#spContadorTxtIdentificacion');

            cargarLupa();
        };

        function cargarLupa() {
            permutarImagenes('#imgBuscarFuncionario',
                '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Lupa.png")%>',
                            '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Lupa.png")%>',
                            '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Lupa.png")%>'
                        );

        };

        function inicializarFormulario() {
            
            


            $(window).keydown(function a(e) {

                if (e.keyCode == 13) {
                    return false;
                }
            });

            establecerControles();

        };

        function mostrarPopUp(pvc_IdPopup) {
            window.location = pvc_IdPopup;
        };


        function regresarPrincipal() {
            window.location = '../../Genericos/Frm_MenuPrincipal.aspx';
        };

        function ocultarFiltroFuncionario(){
            window.location = '#CerrarPopUpBusquedaFuncionario';
        };

        function mostrarAlertaError(pvc_Mensaje) {
            mostrarAlerta(
                '#arAlerta',
                {
                    mensaje: pvc_Mensaje,
                    tipo: 'peligro',
                    transparencia: 1,
                    posicion: 'center',
                    permiteCerrar: true
                }
            );
        };

        $(document).ready(function () {

            $('[data-tipo="borrarRegistro"]').click(function () { return mostrarPopupConfirmaDeseaBorrarRegistro($(this).data("uniqueid")); }); //busca cualquier control con ese data tipo
            $('[data-tipo="borrarRegistro"]').attr('src', '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Borrar.png")%>');

            $('[data-tipo="borrarRegistro"]').on({
                'mouseover': function () { $(this).attr('src', '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Icono_Borrar.png")%>'); },

                'mouseout': function () { $(this).attr('src', '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Borrar.png")%>'); }
            });
            inicializarFormulario()

            $('#btnCancelar').click(function () {
                regresarPrincipal();
            });

        });

    </script>
</asp:Content>