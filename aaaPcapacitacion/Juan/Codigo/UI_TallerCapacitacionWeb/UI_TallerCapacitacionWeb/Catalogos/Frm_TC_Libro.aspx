<%@ Page Title="Catálogos de Libros" Language="VB" MasterPageFile="~/MasterPage/Mp_Formulario.master" AutoEventWireup="false" CodeFile="Frm_TC_Libro.aspx.vb" Inherits="Catalogos_Frm_TC_Libro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphFormulario" runat="Server">
    <header>
        <h2>
            <asp:Label runat="server" ID="lblAccion"></asp:Label></h2>
    </header>

    <article class="tituloSeccion">
        Datos del Libro
    </article>

    <article class="formulario">
        <table>
            <tr>
                <th>ISBN</th>
                <td>
                    <asp:Label runat="server" ID="lblIsbn"></asp:Label>
                    <asp:TextBox runat="server" ID="txtIsbn" Width="200px" data-tipocontrol="texto"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ID="rfvTxtIsbn" ControlToValidate="txtIsbn" Display="Dynamic" ErrorMessage="El ISBN es requerido.">&nbsp;</asp:RequiredFieldValidator>
                    <br />
                    <span id="spContadorTxtIsbn" class="contadorCaracteresRestantes"></span>
                </td>
            </tr>

            <tr>
                <th>Título</th>
                <td>
                    <asp:TextBox runat="server" ID="txtTitulo" Width="97%" data-tipocontrol="texto"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ID="rfvTxtTitulo" ControlToValidate="txtTitulo" Display="Dynamic" ErrorMessage="El Título es requerido.">&nbsp;</asp:RequiredFieldValidator>
                    <br />
                    <span id="spContadorTxtTitulo" class="contadorCaracteresRestantes"></span>
                </td>
            </tr>

            <tr>
                <th>Resumen</th>
                <td>
                    <asp:TextBox runat="server" ID="txtResumen" Width="97%" TextMode="MultiLine" Rows="6" data-tipocontrol="texto"></asp:TextBox>
                    <br />
                    <span id="spContadorTxtResumen" class="contadorCaracteresRestantes"></span>
                </td>
            </tr>

            <tr>
                <th>Total de Páginas</th>
                <td>
                    <asp:TextBox runat="server" ID="txtTotalPaginas" Width="50px" data-tipocontrol="texto" data-valordefecto="0"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ID="rfvTxtTotalPaginas" ControlToValidate="txtTotalPaginas" Display="Dynamic" ErrorMessage="El Total de Páginas es requerido.">&nbsp;</asp:RequiredFieldValidator>
                    <asp:CompareValidator runat="server" ID="cmpvTxtTotalPaginas" ControlToValidate="txtTotalPaginas" Display="Dynamic" ErrorMessage="El Total de Páginas debe ser un valor numérico." Operator="DataTypeCheck" Type="Integer">&nbsp;</asp:CompareValidator>
                </td>
            </tr>

            <tr>
                <th>Fecha y Hora de Impresión</th>
                <td>
                    <asp:TextBox runat="server" ID="txtFechaImpresion" Width="100px" data-tipocontrol="texto"></asp:TextBox>
                    &nbsp;&nbsp;
                    <asp:TextBox runat="server" ID="txtHoraImpresion" Width="50px" data-tipocontrol="texto" data-valordefecto="0"></asp:TextBox>
                    :
                    <asp:TextBox runat="server" ID="txtMinutosImpresion" Width="50px" data-tipocontrol="texto" data-valordefecto="0"></asp:TextBox>
                    
                    <asp:RequiredFieldValidator runat="server" ID="rfvTxtFechaImpresion" ControlToValidate="txtFechaImpresion" Display="Dynamic" ErrorMessage="La Fecha de Impresión es requerida.">&nbsp;</asp:RequiredFieldValidator>
                    <asp:CompareValidator runat="server" ID="cmpvTxtFechaImpresion" ControlToValidate="txtFechaImpresion" Display="Dynamic" ErrorMessage="El formato indicado para la fecha es inválido. El formato correcto es dd/mm/yyyy." Operator="DataTypeCheck" Type="Date">&nbsp;</asp:CompareValidator>
                    
                    <asp:RequiredFieldValidator runat="server" ID="rfvTxtHoraImpresion" ControlToValidate="txtHoraImpresion" Display="Dynamic" ErrorMessage="La Hora de impresión es requerida.">&nbsp;</asp:RequiredFieldValidator>
                    <asp:CompareValidator runat="server" ID="cmpvTxtHoraImpresion" ControlToValidate="txtHoraImpresion" Display="Dynamic" ErrorMessage="La Hora de impresión debe ser un valor numérico." Operator="DataTypeCheck" Type="Integer">&nbsp;</asp:CompareValidator>

                    <asp:RequiredFieldValidator runat="server" ID="rfvTxtMinutosImpresion" ControlToValidate="txtMinutosImpresion" Display="Dynamic" ErrorMessage="Debe indicar la hora exacta de impresión.">&nbsp;</asp:RequiredFieldValidator>
                    <asp:CompareValidator runat="server" ID="cmpvTxtMinutosImpresion" ControlToValidate="txtMinutosImpresion" Display="Dynamic" ErrorMessage="Los Minutos de impresión deben ser un valor numérico." Operator="DataTypeCheck" Type="Integer">&nbsp;</asp:CompareValidator>
                </td>
            </tr>

            <tr>
                <th>Condición</th>
                <td>
                    <asp:DropDownList runat="server" ID="ddlCondicionLibro" AppendDataBoundItems="true" data-tipocontrol="combo"></asp:DropDownList>
                    <asp:RequiredFieldValidator runat="server" ID="rfvDdlCondicionLibro" ControlToValidate="ddlCondicionLibro" Display="Dynamic" ErrorMessage="Debe indicar la condición del libro.">&nbsp;</asp:RequiredFieldValidator>
                </td>
            </tr>
        </table>
    </article>

    <article class="areaBotones">
        <asp:Button runat="server" ID="btnAceptar" Text="Aceptar" />
        <input id="btnLimpiarFormulario" type="button" data-tipo="limpiarFormulario" value="Limpiar Formulario" />
        <input id="btnCancelar" type="button" value="Cancelar" />
    </article>

    <article id="arPopupDelFormulario"></article>
    <article id="arAlertasDelFormulario"></article>

    <script type="text/javascript">
        function regresarAlListado() {
            window.location = 'Lst_TC_Libro.aspx';
        }

        function deshabilitarFormulario(){
            deshabilitarControl('#<%=Me.btnAceptar.ClientID%>');
            deshabilitarControl('#btnLimpiarFormulario');
            deshabilitarControl('#btnCancelar');
            $('.formulario').attr('disabled', 'disabled');
        }

        function mostrarPopupRegistroExitoso(){
            var vlo_ConfiguracionPopup = {
                titulo: 'Catálogo de Libros',
                mensaje: 'Se ha registrado la información del libro.<br/><strong>¿Desea registrar otro libro?</strong>',
                botones:
                    [
                        {
                            idControl: "btnSi",
                            textoBoton: "Si",
                            onClick: function () { window.location = 'Frm_TC_Libro.aspx?pvn_Operacion=<%= Utilerias.TallerCapacitacion.eOperacion.Agregar%>'; }
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
                    mensaje: 'Se ha actualizado la información del libro',
                    tipo: "exito",
                    transparencia: 0.9,
                    posicion: 'center',
                    permiteCerrar: true,
                    onClosed: function() { regresarAlListado(); }
                }
            );
        }

        function mostrarAlertaLlaveIncorrecta() {
            deshabilitarFormulario();
            
            mostrarAlerta(
                '#arAlertasDelFormulario',
                {
                    mensaje: 'El ISBN provisto no pertenece a ningún libro registrado en el sistema.',
                    tipo: "peligro",
                    transparencia: 0.9,
                    posicion: 'center',
                    permiteCerrar: true,
                    onClosed: function() { regresarAlListado(); }
                }
            );
        }

        $(document).ready(function () {
            $('#btnCancelar').click(function () {
                regresarAlListado();
            });

            configurarLongitudMaximaTexto('#<%=Me.txtIsbn.ClientID%>',<%=Utilerias.TallerCapacitacion.Modelo.TCM_LIBRO.ISBN_TAMANO_BD%>);
            configurarContadorCaracteresRestantes('#<%=Me.txtIsbn.ClientID%>', '#spContadorTxtIsbn');

            configurarLongitudMaximaTexto('#<%=Me.txtTitulo.ClientID%>',<%=Utilerias.TallerCapacitacion.Modelo.TCM_LIBRO.TITULO_TAMANO_BD%>);
            configurarContadorCaracteresRestantes('#<%=Me.txtTitulo.ClientID%>', '#spContadorTxtTitulo');

            configurarLongitudMaximaTexto('#<%=Me.txtResumen.ClientID%>',<%=Utilerias.TallerCapacitacion.Modelo.TCM_LIBRO.RESUMEN_TAMANO_BD%>);
            configurarContadorCaracteresRestantes('#<%=Me.txtResumen.ClientID%>', '#spContadorTxtResumen');

            configurarSpinnerNumericoRango('#<%=Me.txtTotalPaginas.ClientID%>', 1, <%=Utilerias.TallerCapacitacion.Modelo.TCM_LIBRO.TOTAL_PAGINAS_MINIMO_BD%>, <%=Utilerias.TallerCapacitacion.Modelo.TCM_LIBRO.TOTAL_PAGINAS_MAXIMO_BD%>, true);

            configurarDatePicker('#<%=Me.txtFechaImpresion.ClientID%>');
            establecerFechaMaximaDatePicker('#<%=Me.txtFechaImpresion.ClientID%>', new Date());
            configurarSpinnerTimePicker('#<%=Me.txtHoraImpresion.ClientID%>', '#<%=Me.txtMinutosImpresion.ClientID%>', true);
        });
    </script>
</asp:Content>











