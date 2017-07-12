<%@ Page Title="Catálogo de Actividades por Categoria de Servicio" Language="VB" MasterPageFile="~/MasterPage/Mp_Formulario.master" AutoEventWireup="false" CodeFile="Frm_OT_ActividadesPorCategoriaServicio.aspx.vb" Inherits="Catalogos_Frm_OT_ActividadesPorCategoriaServicio" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphFormulario" runat="Server">
    <header>
        <h2>
            <asp:Label runat="server" ID="lblAccion"></asp:Label></h2>
    </header>
    <article class="tituloSeccion">
        Datos de la Actividad
    </article>
    <article class="formulario">
        <table>
            <tr>
                <th>Categoría</th>
                <td>
                    <asp:Label runat="server" ID="lblCategoria"></asp:Label>
                    <br />
                </td>
            </tr>
            <tr>
                <th>Descripción</th>
                <td>
                    <asp:TextBox runat="server" ID="txtDescripcion" Width="97%" data-tipoControl="texto"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ID="rfvTxtDescripcion" ControlToValidate="txtDescripcion" Display="Dynamic" ErrorMessage="La descripción de la Actividad es requerida.">&nbsp;</asp:RequiredFieldValidator>
                    <br />
                    <span id="spContadorTxtDescripcion" class="contadorCaracteresRestantes"></span>
                </td>
            </tr>
            <tr>
                <th>Descripción Ampliada</th>
                <td>
                    <asp:TextBox runat="server" ID="txtResumen" Width="97%" TextMode="MultiLine" Rows="6" data-tipoControl="texto"></asp:TextBox>
                    <br />
                    <span id="spContadorTxtResumen" class="contadorCaracteresRestantes"></span>
                </td>
            </tr>
            <tr>
                <th>Sector</th>
                <td>
                    <asp:DropDownList Width="97%" runat="server" ID="ddlSector" AppendDataBoundItems="true" data-tipoControl="combo"></asp:DropDownList>
                </td>
            </tr>
            <tr runat="server" id="trEstado">
                <th>Estado</th>
                <td>
                    <asp:DropDownList Width="97%" runat="server" ID="ddlEstado" AppendDataBoundItems="true" data-tipoControl="combo"></asp:DropDownList>
                    <asp:RequiredFieldValidator runat="server" ID="rfvDdlEstado" ControlToValidate="ddlEstado" Display="Dynamic" ErrorMessage="Debe seleccionar el estado.">&nbsp;</asp:RequiredFieldValidator>
                </td>
            </tr>
        </table>
    </article>

    <article class="areaBotones">
        <asp:Button runat="server" ID="btnAceptar" Text="Aceptar" />
        <input id="btnLimpiarFormulario" type="button" data-tipo="limpiarFormulario" value="Limpiar Formulario" />
        <input id="btnCancelar" type="button" value="Cancelar" />
    </article>

    <article>
        <asp:Label runat="server" ID="lblParametroIdCategoria" hiddenfield="true"></asp:Label>
    </article>

    <article id="arPopupDelFormulario"></article>
    <article id="arAlertasDelFormulario"></article>

    <script type="text/javascript">
        function regresarAlListado() {
            var IdCategoria = document.getElementById('<%=lblParametroIdCategoria.ClientID%>').textContent; 
            window.location = 'Lst_OT_ActividadesPorCategoriaServicio.aspx?pvc_IdCategoria='+IdCategoria;
        }

        function deshabilitarFormulario() {
            deshabilitarControl('#<%=Me.btnAceptar.ClientID%>');
            deshabilitarControl('#btnLimpiarFormulario');
            deshabilitarControl('#btnCancelar');

            $('.formulario').attr('disabled','disabled');
        }

        function mostrarPopupRegistroExitoso () {
            var IdCategoria = document.getElementById('<%=lblParametroIdCategoria.ClientID%>').textContent; 
            var vlo_ConfiguracionPopup = {
                titulo: 'Catálogo de Actividades',
                mensaje: 'Se a registrado la informacion de la actividad.<br/><strong>Desea registrar otra actividad?</strong>',
                botones:
                    [  //en corchetes indican arreglos y los parentesis cuadrados indican inicio o fin de funcionalidad o objeto, $librerias de jquery
                        {
                            idControl: "btnSi",
                            textoBoton: "Si",
                            onClick:
                                function (e) {
                                    window.location = 'Frm_OT_ActividadesPorCategoriaServicio.aspx?pvn_Operacion=<%= Utilerias.OrdenesDeTrabajo.eOperacion.Agregar%>&pvc_IdCategoria='+IdCategoria;
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

        function mostrarAlertaActualizacionExitosa() {
            deshabilitarFormulario();

            mostrarAlerta(
                '#arAlertasDelFormulario',
                {
                    mensaje: 'Se ha actualizado la información de la actividad',
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
                    mensaje: 'El número de identificación provisto no pertenece a ninguna actividad registrada en el sistema',
                    tipo: "peligro",
                    transparencia: 0.9,
                    posicion: 'center',
                    permiteCerrar: true,
                    onClosed: function () { regresarAlListado(); }
                }
                );
        }

        $(document).ready(function () {
            $('#btnCancelar').click(function () {
                regresarAlListado();
            });

            configurarLongitudMaximaTexto('#<%=Me.txtDescripcion.ClientID%>', <%=Utilerias.OrdenesDeTrabajo.Modelo.OTM_ACTIVIDAD.DESCRIPCION_BD_TAMANO%>);
            configurarContadorCaracteresRestantes('#<%=Me.txtDescripcion.ClientID%>', '#spContadorTxtDescripcion');

            configurarLongitudMaximaTexto('#<%=Me.txtResumen.ClientID%>', <%=Utilerias.OrdenesDeTrabajo.Modelo.OTM_ACTIVIDAD.DESCRIPCION_AMPLIADA_BD_TAMANO%>);
            configurarContadorCaracteresRestantes('#<%=Me.txtResumen.ClientID%>', '#spContadorTxtResumen');
                        
        });

    </script>
</asp:Content>

