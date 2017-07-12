<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/Mp_Formulario.master" AutoEventWireup="false" CodeFile="Frm_OT_UnidadEspAprobJefeAdmin.aspx.vb" Inherits="OrdenesDeTrabajo_GestionesDeCompra_Frm_OT_UnidadEspAprobJefeAdmin" %>

<%@ Register TagPrefix="wuc" Namespace="Utilerias.WebForms.Controles" Assembly="Utilerias.WebForms.Controles" %>
<%@ Register Src="~/Controles/wuc_OT_Lineas_Material_Gestion_Compra.ascx" TagName="wuc_OT_Lineas_Material_Gestion_Compra" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphFormulario" Runat="Server">
    <header>
        <h2>Revisión de Compras por Unidad Especializada de Compra</h2>

    </header>

    <article class="tituloSeccion">
        Revisión de Compras por Unidad Especializada de Compra
    </article>

    <article data-grupo="Formulario"class="formulario ">
        <table>
            <tr>
                <th style="width: 14%;">N° de Gestión</th>
                <td>
                    <asp:Label runat="server" ID="lblNumGestion" ></asp:Label>                    
                </td>
            </tr>
            <tr runat="server" id="trObservaciones">
                <th style="width: 14%;">Observaciones</th>
                <td>
                    <asp:TextBox runat="server" ID="txtObs" Width="100%" TextMode="MultiLine" Rows="6" ReadOnly="true" ></asp:TextBox>                    
                </td>
            </tr>
        </table>
    </article>

    <article class="tituloSeccion">
        Materiales a Gestionar
    </article>

    <article class="formulario sinBorde " style="overflow-x:auto;overflow-x:scroll">
        <asp:Repeater runat="server" ID="rpMateriales">
        <ItemTemplate>
            <article runat="server" id="arAcordeon" class="formulario sinBorde" data-tipo="acordeon">
                <article class="encabezadoAcordeon">
                    <a runat="server" id="ancorAcordeon" class="tituloAcordeon">
                        <asp:Label runat="server" ID="lblNumLinea" Text='<%# String.Format("N° de Línea: {0}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GRUPO_GESTION_COMPRALST.NUMERO_LINEA))%>'></asp:Label> &nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Label runat="server" ID="lblPartida" Text='<%# String.Format("Partida: {0}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GRUPO_GESTION_COMPRALST.PARTIDA_PRESUPUESTARIA))%>'></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Label runat="server" ID="lblCodigo" Text='<%# String.Format("Código: {0}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GRUPO_GESTION_COMPRALST.ID_MATERIAL))%>'></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Label runat="server" ID="lblDescripcion" Text='<%# String.Format("Descripción: {0}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GRUPO_GESTION_COMPRALST.NOMBRE_MATERIAL))%>'></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Label runat="server" ID="lblCantidad" Text='<%# String.Format("Cantidad: {0}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GRUPO_GESTION_COMPRALST.CANTIDAD_SOLICITADA))%>'></asp:Label>
                    </a>
                </article>
                <article runat="server" id="cuerpoAcordeon1" class="cuerpoAcordeon">
                    <uc1:wuc_OT_Lineas_Material_Gestion_Compra runat="server" ID="wuc_OT_Lineas_Material_Gestion_Compra" IdUbicacion='<%# Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GRUPO_GESTION_COMPRALST.ID_UBICACION)%>' IdViaCompraContrato='<%# Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GRUPO_GESTION_COMPRALST.ID_VIA_COMPRA_CONTRATO)%>' IdAnnio='<%# Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GRUPO_GESTION_COMPRALST.ANNO)%>' IdNumeroGestion='<%# Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GRUPO_GESTION_COMPRALST.NUMERO_GESTION)%>' IdMaterial='<%# Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_LINEA_GEST_COMP_GROUP.ID_MATERIAL)%>'/>
                </article>
            </article>
        </ItemTemplate>
        </asp:Repeater>
    </article>

    <article class="tituloSeccion">
        Datos de la Revisión
    </article>
    <article class="formulario ">
                <table>
                    <tr>
                        <th style="width: 14%;"></th>
                        <td>
                            <asp:RadioButton ID="rdbAprobado" runat="server"  GroupName="grpAprobo" Text="Aprobar" AutoPostBack="true" />
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:RadioButton ID="rdbDevuelto" runat="server"  GroupName="grpAprobo" Text="Devolver" AutoPostBack="true" />
                            <asp:CustomValidator ID="cvcgrpEstadoRevision" runat="server" ControlToValidate="txtValidaciones" ErrorMessage="Debe indicar si decide enviar a aprobación o devolver la gestión de compra" ValidationGroup="Grupo1" ClientValidationFunction="ValidarEstadoRevision">&nbsp;</asp:CustomValidator>
                        </td>
                    </tr>
                    <tr>
                        <th style="width: 14%;">Observaciones</th>
                        <td>
                            <asp:TextBox runat="server" ID="txtObservaciones" Width="100%" TextMode="MultiLine" Rows="9" data-tipocontrol="texto"></asp:TextBox>
                            <br />
                            <span id="spContadorTxtObservaciones" class="contadorCaracteresRestantes"></span>
                            <asp:CustomValidator ID="CustomValidator1" runat="server" ControlToValidate="txtValidaciones" ErrorMessage="Debe indicar una observación si decide devolver la gestión de compra" ValidationGroup="Grupo1" ClientValidationFunction="ValidadorDeCampoRequeridoPersonalizado">&nbsp;</asp:CustomValidator>
                        </td>
                    </tr>
                </table>
                
        <br />
    </article>

    <article style="visibility: hidden">
        <asp:TextBox ID="txtValidaciones" runat="server" Text="Validar"></asp:TextBox>
    </article>

    <article class="areaBotones">
        <asp:Button ID="btnTramitar" runat="server" Text="Aceptar" ToolTip="Aceptar" ValidationGroup="Grupo1"></asp:Button>
        <%--<input id="btnLimpiarFormulario" type="button" data-tipo="limpiarFormulario" value="Limpiar formulario" />--%>
        <input id="btnCancelar" type="button" title="Regresar" value="Regresar" />
    </article>

    <article id="arAlertasDelFormulario"></article>

    <script type="text/javascript">

        $(document).ready(function () {
            $('#btnCancelar').click(function () {
                regresarAlListado();
            });

            $('article[data-tipo="acordeon"]').each(function () {
                configurarAcordeon('#' + this.id, { seleccionMultiple: false, eventoApertura: 'click' });
            });


        })


        function ValidadorDeCampoRequeridoPersonalizado(source, clientside_arguments) {
            if (document.getElementById('<%=rdbDevuelto.ClientID%>').checked && document.getElementById('<%=txtObservaciones.ClientID%>').value.trim() == "") {
                document.getElementById('<%=txtObservaciones.ClientID%>').style.backgroundColor = "#F5838A"
                return clientside_arguments.IsValid = false;
            }
            else {
                document.getElementById('<%=txtObservaciones.ClientID%>').style.backgroundColor = "white"
                return clientside_arguments.IsValid = true;
            }
        }

        function mostrarAlertaError(pvc_Mensaje) {
            mostrarAlerta(
                '#arAlertasDelFormulario',
                {
                    mensaje: pvc_Mensaje,
                    tipo: 'peligro',
                    transparencia: 1,
                    posicion: 'top',
                    permiteCerrar: true
                }
            );
        }


        /*
          Autor: Mauricio Salas
          Fecha:02/09/2016 
          Descripcion: Función que valida que se haya seleccionado si fue aprovado o devuelta la gestion
          Parametros: source, clientside_arguments: parametros de la Función CustomValidator
          */
        function ValidarEstadoRevision(source, clientside_arguments) {
            return clientside_arguments.IsValid = (document.getElementById('<%=rdbAprobado.ClientID%>').checked || document.getElementById('<%=rdbDevuelto.ClientID%>').checked);
        }

        function mostrarAlertaRegistroExitoso() {
            deshabilitarControl('#<%=btnTramitar.ClientID%>');
            deshabilitarControl('#btnLimpiarFormulario');
            deshabilitarControl('#btnCancelar');
            $('.formulario').attr('disabled', 'disabled');

            mostrarAlerta(
                '#arAlertasDelFormulario',
                {
                    mensaje: 'Se ha tramitado la gestión de compra.',
                    tipo: "exito",
                    transparencia: 0.9,
                    posicion: 'top',
                    onClosed: function () { regresarAlListado(); }
                });
        }

        function regresarAlListado() {
            window.location = 'Lst_OT_UnidadEspAprobJefeAdmin.aspx';
        }

    </script>
</asp:Content>

