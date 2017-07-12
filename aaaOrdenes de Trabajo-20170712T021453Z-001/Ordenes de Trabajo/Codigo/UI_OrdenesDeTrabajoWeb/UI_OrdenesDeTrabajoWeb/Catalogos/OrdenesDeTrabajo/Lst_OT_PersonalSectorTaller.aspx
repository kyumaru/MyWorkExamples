<%@ Page Title="Catálogo de Funcionarios por Sector o Taller" Language="VB" MasterPageFile="~/MasterPage/Mp_Listado.master" AutoEventWireup="false" CodeFile="Lst_OT_PersonalSectorTaller.aspx.vb" Inherits="Catalogos_Lst_OT_PersonalSectorTaller" %>

<%@ Register TagPrefix="wuc" Namespace="Utilerias.WebForms.Controles" Assembly="Utilerias.WebForms.Controles" %>
<%--<%@ Register Src="~/Controles/wucEmpleadosEU.ascx" TagName="wuc_EmpleadosEU" TagPrefix="uc1" %>--%>

<asp:Content ID="Content1" ContentPlaceHolderID="cphFormulario" runat="Server">
    <header>
        <h2>Catálogo de Personal por Sector o Taller</h2>
    </header>

    <article data-grupo="FiltrosDeBusqueda" class="tituloSeccion">
        Criterios de búsqueda
    </article>
    <article data-grupo="FiltrosDeBusqueda" class="formulario">
        
        <table>
            <tr>
                <th>Identificación</th>
                <td>
                    <asp:TextBox Width="40%" runat="server" ID="txtFiltroIdentificacion" data-tipocontrol="texto"></asp:TextBox>
                    <br />
                </td>
            </tr>
            <tr>
                <th>Nombre</th>
                <td>
                    <asp:TextBox Width="40%" runat="server" ID="txtFiltroNombre" AppendDataBoundItems="true" data-tipocontrol="combo"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>Area</th>
                <td>
                    <asp:DropDownList Width="40%" runat="server" ID="ddlFiltroArea" AppendDataBoundItems="true" data-tipocontrol="combo"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <th>Estado</th>
                <td>
                    <asp:DropDownList Width="40%" runat="server" ID="ddlFiltroEstado" AppendDataBoundItems="true" data-tipocontrol="combo"></asp:DropDownList>
                </td>
            </tr>
        </table>
    </article>
    <article data-grupo="FiltrosDeBusqueda" class="areaBotones">
        <asp:Button runat="server" ID="btnBuscar" Text="Buscar" />
        <input type="button" data-tipo="limpiarFiltros" value="Limpiar Filtros" />
        <input type="button" data-tipo="cancelarBusqueda" value="Cancelar" id="btnCancelarBusqueda" />
    </article>



    <article class="tituloSeccion">
        Datos del Sector o Taller
    </article>
    <article class="formulario">
        <table>
            <tr>
                <th>Sector</th>
                <td>
                    <asp:Label runat="server" ID="lblSector"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>Coordinador</th>
                <td>
                    <asp:Label runat="server" ID="lblCoordinador"></asp:Label>
                </td>
            </tr>
            </table>
        </article>
      
    
    <article data-grupo="Listado" class="tituloSeccion">
        Listado de Personal
    </article>

    

    <article data-grupo="Listado" class="listado">

        <article class="areaBotonesListado">
            <img id="btnRegresar" data-tipo="regresarListado" src='<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x32, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Flecha_Izquierda.png")%>' />
            <a href="Frm_OT_PersonalSectorTaller.aspx?pvn_Operacion=<%=Utilerias.OrdenesDeTrabajo.eOperacion.Agregar%>">
                <img alt="Registrar personal nuevo" data-tipo="nuevoRegistro" src='<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x32, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Nuevo_Documento.png")%>' />
            </a>
            <img alt="Mostrar criterios de búsqueda" data-tipo="mostrarFiltros" src='<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x32,AdministradorRecursos.COLOR_IMAGEN.GRIS,"Lupa.png") %>' id="imgMostrarFiltros" />
            
        </article>

        <asp:Repeater runat="server" ID="rpPersona">
            <HeaderTemplate>
                <table>
                    <tr>
                        <th>Identificación
                        </th>
                        <th>Nombre
                        </th>
                        <th>Area</th>
                        <th>Estado</th>
                        <th>&nbsp;</th>
                        <th>&nbsp;</th>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr class="lineaDelListado">
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTF_OPERARIO_AREALST.CEDULA)%></td>
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTF_OPERARIO_AREALST.NOMBRE_EMPLEADO)%></td>
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTF_OPERARIO_AREALST.CATEGORIA)%></td>
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTF_OPERARIO_AREALST.DESCRIPCION_ESTADO)%></td>
                    <td>
                       <a href="Frm_OT_PersonalSectorTaller.aspx?pvn_Operacion=<%# Utilerias.OrdenesDeTrabajo.eOperacion.Modificar%>&pvc_IdPersona=<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTF_OPERARIO_AREALST.CEDULA)%>"> 
                            <img alt="Modificar" data-tipo="modificarRegistro" 
                                src='<%#AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Editar.png")%>'  />
                        </a>
                    </td>
                    <td>
                        <asp:ImageButton runat="server" ID="ibBorrar" AlternateText="Borrar la Persona" data-tipo="borrarRegistro" 
                            CommandArgument='<%#String.Format("{0}_{1}_{2}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTF_OPERARIO_AREALST.ID_SECTOR_TALLER), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTF_OPERARIO_AREALST.NUM_EMPLEADO), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTF_OPERARIO_AREALST.CEDULA))%>' 
                            src="" OnClick="ibBorrar_Click" 
                            Visible="<%#IIf(CType(Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTF_OPERARIO_AREALST.POSEE_REGISTROS_ASOCIADOS), Integer) = 0, True, False)%>"/>
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
    </article>

    <article data-grupo="Listado" class="areaPaginadorListado">
        <wuc:PaginadorNumerico runat="server" ID="pnRpPersonas" />
        <%--Paginador del repeater RpCategorias--%>
    </article>
    <article data-grupo="Listado" class="areaCantidadDeRegistro">
        <asp:Label runat="server" ID="lblCantidadRegistro" Text=""> </asp:Label>
    </article>


    <article id="arAlerta"></article>
    <article id="popupConfirmacionDeseaBorrar"></article>

    <script type="text/javascript">
        function mostrarAlertaNoHayDatos() {
            mostrarAlerta(
                '#arAlerta',
                {
                    mensaje: 'No se cuenta con personal que cumpla con la condición indicada',
                    tipo: "advertencia",
                    transparencia: 0.9,
                    posicion: 'center',
                    permiteCerrar: true
                }
                );
        }

        function mostrarAlertaError(pvc_Mensaje) {
            mostrarAlerta(
                '#arAlerta',
                {
                    mensaje: pvc_Mensaje,
                    tipo: "peligro",
                    transparencia: 0.9,
                    posicion: 'center',
                    permiteCerrar: true
                }
                );

        }

        function mostrarAlertaExitoso() {
            mostrarAlerta(
                '#arAlerta',
                {
                    mensaje: "Se ha ingresado la persona seleccionada",
                    tipo: "exito",
                    transparencia: 0.9,
                    posicion: 'center',
                    permiteCerrar: true
                }
                );

        }

        function mostrarAlertaRegistroBorrado() {
            mostrarAlerta(
                '#arAlerta',
                {
                    mensaje: "Se ha borrado la persona seleccionada",
                    tipo: "exito",
                    transparencia: 0.9,
                    posicion: 'center',
                    permiteCerrar: true
                }
                );

        }

        function mostrarAlertaRegistroNoBorrado() {
            mostrarAlerta(
                '#arAlerta',
                {
                    mensaje: "No ha sido posible borrar la persona seleccionada",
                    tipo: "advertencia",
                    transparencia: 0.9,
                    posicion: 'center',
                    permiteCerrar: true
                }
                );

        }

        function mostrarAlertaNoEncontrado() {
            mostrarAlerta(
                '#arAlerta',
                {
                    mensaje: "No se han encontrado funcionarios con el número de cédula indicado",
                    tipo: "advertencia",
                    transparencia: 0.9,
                    posicion: 'center',
                    permiteCerrar: true
                }
                );

        }

        function mostrarPopupConfirmaDeseaBorrarRegistro(pvc_UniqueIdControl) {
            var vlo_ConfiguracionPopup = {
                titulo: 'Catálogo de Personal por Sector o Taller',
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
        }

        function regresarAlListado() {
            window.location = 'Lst_OT_SectorTaller.aspx';
        }

        function deshabilitarFormulario() {
            deshabilitarControl('#btnAgregar');

            $('.formulario').attr('disabled', 'disabled');
            $('.listado').attr('disabled', 'disabled');
        }

        function mostrarAlertaLlaveIncorrecta() {
            deshabilitarFormulario();

            mostrarAlerta(
                '#arAlerta',
                {
                    mensaje: 'El número de identificación de Sector o Taller provisto no pertenece a ningún Empleado registrado en el sistema',
                    tipo: "peligro",
                    transparencia: 0.9,
                    posicion: 'center',
                    permiteCerrar: true,
                    onClosed: function () { regresarAlListado(); }
                }
                );

        }

        function mostrarPopUp(pvc_IdPopup) {
            window.location = pvc_IdPopup;
        };

        function inicializarFormulario() {
            $('#btnRegresar').click(function () {
                regresarAlListado();
            });

            $(window).keydown(function a(e) {

                if (e.keyCode == 13) {
                    return false;
                }
            });


            //aplicarConfiguracionBasicaListado({ alternarVisibilidadFiltroLista: true });
            $('[data-tipo="borrarRegistro"]').click(function () { return mostrarPopupConfirmaDeseaBorrarRegistro($(this).data("uniqueid")); }); //busca cualquier control con ese data tipo
            $('[data-tipo="borrarRegistro"]').attr('src', '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Borrar.png")%>');

            $('[data-tipo="borrarRegistro"]').on({
                'mouseover': function () { $(this).attr('src', '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Icono_Borrar.png")%>'); },

                'mouseout': function () { $(this).attr('src', '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Borrar.png")%>'); }
            });

            $('[data-tipo="modificarRegistro"]').on({
                'mouseover': function () { $(this).attr('src', '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Icono_Editar.png")%>'); },
                'mouseout': function () { $(this).attr('src', '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Editar.png")%>'); }


            });

            $('[data-tipo="nuevoRegistro"]').on({
                'mouseover': function () { $(this).attr('src', '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x32, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Nuevo_documento.png")%>'); },
                'mouseout': function () { $(this).attr('src', '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x32, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Nuevo_documento.png")%>'); }

            });

            $('[data-tipo="mostrarFiltros"]').on({
                'mouseover': function () { $(this).attr('src', '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x32, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Lupa.png")%>'); },
                'mouseout': function () { $(this).attr('src', '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x32, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Lupa.png")%>'); }

            });

            $('[data-tipo="regresarListado"]').attr('src', '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x32, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Flecha_Izquierda.png")%>');

            $('[data-tipo="regresarListado"]').on({
                'mouseover': function () { $(this).attr('src', '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x32, AdministradorRecursos.COLOR_IMAGEN.COLOR, "Flecha_Izquierda.png")%>'); },
                'mouseout': function () { $(this).attr('src', '<%=AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x32, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Flecha_Izquierda.png")%>'); }

            });
        }

        $(document).ready(function () {

            aplicarConfiguracionBasicaListado({ alternarVisibilidadFiltroLista: true });
            inicializarFormulario()

        });
        
    </script>
</asp:Content>

