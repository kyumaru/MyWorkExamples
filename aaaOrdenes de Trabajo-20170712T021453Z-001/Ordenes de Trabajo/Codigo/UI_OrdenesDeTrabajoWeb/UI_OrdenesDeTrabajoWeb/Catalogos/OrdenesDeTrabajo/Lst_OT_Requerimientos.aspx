<%@ Page Title="Listado de Requerimientos" MasterPageFile="~/MasterPage/Mp_Listado.master" Language="VB" AutoEventWireup="false" CodeFile="Lst_OT_Requerimientos.aspx.vb" Inherits="Catalogos_Lst_OT_Requerimientos" %>

<%@ Register TagPrefix="wuc" Namespace="Utilerias.WebForms.Controles" Assembly="Utilerias.WebForms.Controles" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %> 

<asp:Content runat="server" ID="Content1" ContentPlaceHolderID="cphFormulario">
    <link href="../CSS/site.css" rel="stylesheet" />

    <header>
        <h2>Catálogo de Requerimientos</h2> 
    </header>

    <article data-grupo="Listado" class="tituloSeccion">
        Listado de Requerimientos
    </article>

    <article style="width:50%;float:left" data-grupo="Listado">
        
        <article class="areaBotonesListado">
            <asp:CheckBox runat="server" AutoPostBack="true" ID="ckbMostrarInactivos" Text="Mostrar Inactivos" OnCheckedChanged="ckbMostrarInactivos_CheckedChanged" />
            <asp:LinkButton runat="server" id="imgbtnExpandir" Text="Expandir árbol" OnClick="expandir_Click" />
            <asp:LinkButton runat="server" id="imgbtnRecoger" Text="Recoger árbol" OnClick="recoger_Click" />
            <asp:imagebutton runat="server" OnClick="Agregar_Click" id="btnAgregar" alt="Registrar nuevos Requerimientos" data-tipo="nuevoRegistro" src="" />
        </article>
        <table>
            <tr>
                <td>
                    <article id="arArbol">
                        <asp:TreeView ID="tvListado" runat="server"></asp:TreeView>
                    </article>
                </td>
            </tr>
        </table>
    </article>

    <article class="areaCantidadDeRegistros" data-grupo="Listado">
        <asp:Label runat="server" ID="lblRequerimientoActual" Text=""></asp:Label>
    </article>

    <article style="width:50%;float:right">
        <article runat="server" class="formulario" id="frmRequerimiento">
        <table>
            <tr runat="server" id="trRNS">
                <th>Requerimiento de Nivel Superior:</th>
                <td><asp:Label runat="server" ID="lblReqNivSup"></asp:Label></td>
            </tr>
            <tr>
                <th>Descripción</th>
                <td>
                    <asp:TextBox runat="server" id="txtDescripcion" Width="100%" data-tipoControl="texto"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" id="rvfTxtDescripcion" ControlToValidate="txtDescripcion" display="Dynamic" ErrorMessage="La Descripción del espacio a ingresar es requerida.">&nbsp;</asp:RequiredFieldValidator>
                    <br />
                    <span id="Span1" class="contadorCaracteresRestantes">

                    </span>
                </td>
            </tr>
            <tr runat="server" id="trNivel">
                <th>
                    Nivel
                </th>
                <td><asp:Label runat="server" ID="lblNivel"></asp:Label></td>
            </tr>
            <tr runat="server" id="trTipoValor">
                <th>Tipo de Valor</th>
                <td><asp:RadioButtonList RepeatDirection="Horizontal" RepeatLayout="Table"  runat="server" ID="rbtnlValor">
                    <asp:ListItem Text="Numérico" Value="NUM"></asp:ListItem>
                    <asp:ListItem Text="Caracter" Value="CAR"></asp:ListItem>
                    <asp:ListItem Text="Sí/No" Value="IND"></asp:ListItem>
                </asp:RadioButtonList></td>
            </tr>
            <tr runat="server" id="trEstado">
                <th>Estado</th>
                <td>
                    <asp:DropDownList runat="server" ID="ddlEstado" AppendDataBoundItems="true" data-tipoControl="combo"></asp:DropDownList>
                    <asp:RequiredFieldValidator runat="server" ID="rfvDdlEstado" ControlToValidate="ddlEstado" Display="Dynamic" ErrorMessage="El estado del Requerimiento es requerido.">&nbsp;</asp:RequiredFieldValidator>
                </td>
            </tr>
        </table>
            <article class="areaBotones">
                <asp:Button runat="server" ID="btnAceptar" Text="Modificar" />
                <%--<input type="button" data-tipo="limpiarFormulario" value="Limpiar Formulario" id="btnLimpiarFormulario" />--%>
                <asp:Button runat="server" ID="Eliminar" Text="Eliminar" data-tipo="borrarRegistro" OnClick="Eliminar_Click"/>
                <br />
                <asp:Button style="margin-top:2px;" runat="server" ID="Ordenar" Text="Ordenar" OnClick="Ordenar_Click" ImageUrl='<%# AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Comparar.png") %>' />
                <asp:Button style="margin-top:2px;" runat="server" ID="AgregarSubnivel" Text="Agregar Subnivel" OnClick="AgregarSubnivel_Click" ImageUrl='<%# AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Agregar.png")%>' />
            </article> 
               
            
    </article>
    </article>

    <article class="areaCantidadDeRegistros" data-grupo="Listado">
        <asp:Label runat="server" ID="lblCantidadDeRegistros" Text=""></asp:Label>
    </article>

        <article id="arAlerta">
    </article>

    <article id="arPopupDelFormulario"></article>
    <article id="arAlertasDelFormulario"></article>
    <article id="popupConfirmacionDeseaBorrar">
    </article>
    <article id="arPopupGenerico"></article>

    <script type="text/javascript">
        
        function regresarAlListado() {
            window.location = 'Lst_OT_Requerimientos.aspx';
        }
        function deshabilitarFormulario() {
            deshabilitarControl('#<%=Me.btnAceptar.ClientID%>');
            deshabilitarControl('#btnLimpiarFormulario');
            deshabilitarControl('#btnCancelar');
            $('.formulario').attr('disabled', 'disabled');
        }

        function mostrarPopupRegistroExitoso() {
            var vlo_ConfiguracionPopup = {
                titulo: 'Catálogo de Requerimientos',
                mensaje: 'Se ha registrado la información del requerimiento.<br /><strong>¿Desea registrar otro requerimiento?</strong>',
                onClosed: function () { regresarAlListado(); },
                botones:
                    [
                        {
                            idControl: "btnSi",
                            textoBoton: "Sí",
                            onClick: function () { regresarAlListado(); }
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

        }

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
            }

            function mostrarAlertaActualizacionExitosa() {
                deshabilitarFormulario();

                mostrarAlerta(
                    '#arAlertasDelFormulario',
                    {
                        mensaje: 'Se ha actualizado la información del requerimiento',
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
                        mensaje: 'El número de identificación provisto no pertenece a ningun requerimiento registrado en el sistema',
                        tipo: "peligro",
                        transparencia: 0.9,
                        posicion: 'center',
                        permiteCerrar: true,
                        onClosed: function () { regresarAlListado(); }
                    }
                );
            }

        function mostrarAlertaRegistroBorrado() {
            mostrarAlerta(
                '#arAlerta',
                {
                    mensaje: "Se ha borrado el Requerimiento",
                    tipo: "exito",
                    transparencia: 0.9,
                    posicion: 'center'
                }
            );
        }

        function mostrarAlertaSeleccionarRegistro() {
            mostrarAlerta(
                '#arAlerta',
                {
                    mensaje: "Debe seleccionar un Requerimiento de la lista",
                    tipo: "advertencia",
                    transparencia: 0.9,
                    posicion: 'center'
                }
            );
        }

        function mostrarAlertaRegistroNoBorrado() {
            mostrarAlerta(
                '#arAlerta',
                {
                    mensaje: "No ha sido posible borrar el requerimiento seleccionado",
                    tipo: "advertencia",
                    transparencia: 0.9,
                    posicion: 'center'
                }
            );
        }

        function mostrarAlertaNoHayDatos() {
            mostrarAlerta(
                '#arAlerta',
                {
                    mensaje: "No se cuenta con requerimientos que cumplan con la(s) condicion(es) indicada(s)",
                    tipo: "advertencia",
                    transparencia: 0.9,
                    posicion: 'center'
                }
            );

            deshabilitarControl('#btnCancelarBusqueda');
            ocultarAreaDeListado();
            mostrarAreaFiltrosDeBusqueda();
        }

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

        function mostrarPopupConfirmacionDeseaBorrarRegistro(pvc_UniqueIdControl) {
            var vlo_ConfiguracionPopup = {
                titulo: "<em>Catalogo de requerimientos</em>",
                mensaje: "¿Realmente desea borrar el requerimiento seleccionado?",
                botones:
                    [
                        {
                            idControl: "btnSi",
                            textoBoton: "Sí",
                            onClick:
                                function (e) {
                                    $(this).attr("disabled", "disabled");
                                    __doPostBack(pvc_UniqueIdControl, '');
                                }
                        },
                    {
                        idControl: "btnNo",
                        textoBoton: "No",
                        onClick:
                            function (e) { cerrarPopup(); }
                    },
                    {
                        idControl: "btnCancelar",
                        textoBoton: "Cancelar",
                        onClick:
                            function (e) { cerrarPopup(); }
                    }

                    ]
            };

            $('#popupConfirmacionDeseaBorrar').popup(vlo_ConfiguracionPopup);
            window.location = "#popupConfirmacionDeseaBorrar";

            return false;
        }

        function expandirArbol(){
            var treeView = $find("<%= Me.tvListado.ClientID%>");
            var nodes = treeView.get_allNodes();
            for (var i = 0; i < nodes.length; i++) {
                if (nodes[i].get_nodes() != null) {
                    nodes[i].expand();
                }
            }
        }

        function replegarArbol() {
            var treeView = $find("<%= Me.tvListado.ClientID%>");
            var nodes = treeView.get_allNodes();
            for (var i = 0; i < nodes.length; i++) {
                if (nodes[i].get_nodes() != null) {
                    nodes[i].collapse();
                }
            }
        }
        function redireccionarListado(pvc_PaginaDestino) {
            window.location = pvc_PaginaDestino
        }

        $(document).ready(function () {

            aplicarConfiguracionBasicaListado({ alternarVisibilidadFiltroLista: true });
            $('[data-tipo="borrarRegistro"]').click(function () { return mostrarPopupConfirmacionDeseaBorrarRegistro($(this).data("uniqueid")); });
           
        });

    </script>
</asp:Content>
