<%@ Page title="Listado de Ordenes de trabajo hijas rechazadas" MasterPageFile="~/MasterPage/Mp_Listado.master" Language="VB" AutoEventWireup="false" CodeFile="Lst_OT_HijasRechazadas.aspx.vb" Inherits="OrdenesDeTrabajo_Lst_OT_HijasRechazadas" %>

<%@ Register TagPrefix="wuc" Namespace="Utilerias.WebForms.Controles" Assembly="Utilerias.WebForms.Controles" %>

<asp:content runat="server" ContentPlaceHolderID="cphFormulario">

    <header>
        <h2>Ordenes de Trabajo Hijas</h2>
    </header>

    <article data-grupo="FiltrosDeBusqueda" class="tituloSeccion">
        Criterios de busqueda
    </article>
    <article data-grupo="FiltrosDeBusqueda" class="formulario">
        
        <table>
            <tr>
                <th>Número de órden de trabajo madre</th>
                <td>
                    <asp:TextBox Width="40%" runat="server" ID="txtFiltroConsecutivoMadre" data-tipocontrol="texto"></asp:TextBox>
                    <br />
                </td>
            </tr>
            <tr>
                <th>Número de órden de trabajo madre en PDAGO</th>
                <td>
                    <asp:TextBox Width="40%" runat="server" ID="txtFiltroPDAGOMadre" AppendDataBoundItems="true" data-tipocontrol="texto"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>Número de órden de trabajo hija</th>
                <td>
                    <asp:TextBox Width="40%" runat="server" ID="txtFiltroConsecutivoHija" data-tipocontrol="texto"></asp:TextBox>
                    <br />
                </td>
            </tr>
            <tr>
                <th>Número de órden de trabajo madre en PDAGO</th>
                <td>
                    <asp:TextBox Width="40%" runat="server" ID="txtFiltroPDAGOHija" AppendDataBoundItems="true" data-tipocontrol="texto"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>Categoría</th>
                <td>
                    <asp:DropDownList Width="40%" runat="server" ID="ddlCategoria" data-tipocontrol="combo" AppendDataBoundItems="true" AutoPostBack="true"></asp:DropDownList>
                    <asp:RequiredFieldValidator runat="server" ID="rfvDdlCategoria" ControlToValidate="ddlCategoria" Display="Dynamic" ErrorMessage="Categoría es obligatoria." ValidationGroup="Aceptar"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <th>Actividad</th>
                <td>
                    <asp:UpdatePanel runat="server" ID="upActividad" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:DropDownList Width="40%" runat="server" ID="ddlActividad" data-tipocontrol="combo" AppendDataBoundItems="true" AutoPostBack="true"></asp:DropDownList>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlCategoria" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                    <asp:RequiredFieldValidator runat="server" ID="rfvDdlActividad" ControlToValidate="ddlActividad" Display="Dynamic" ErrorMessage="Actividad es obligatoria." ValidationGroup="Aceptar"></asp:RequiredFieldValidator>
                </td>
            </tr>
        </table>
    </article>
    <article data-grupo="FiltrosDeBusqueda" class="areaBotones">
        <asp:Button runat="server" ID="btnBuscar" Text="Buscar" />
        <input type="button" data-tipo="limpiarFiltros" value="Limpiar Filtros" />
        <input type="button" data-tipo="cancelarBusqueda" value="Cancelar" id="btnCancelarBusqueda" />
    </article>

    <article data-grupo="Listado" class="tituloSeccion">
        Listado de Ordenes de Trabajo
    </article>

     <article data-grupo="Listado" class="listado">
        <article class="areaBotonesListado">
            <img alt="Mostrar criterios de búsqueda" data-tipo="mostrarFiltros" src="" id="imgMostrarFiltros" />
        </article>

         <asp:Repeater ID="rpRechazadas" runat="server">
             <HeaderTemplate>
                <table>
                    <tr>
                        
                        <th>
                            <asp:LinkButton runat="server" text ="Consecutivo Madre" ID="lnkConsecutivoMadre" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OT_HIJAS_RECHAZADAS.CONSECUTIVO_MADRE%>" CommandArgument="ASC" OnCommand="lnkRpOrdenTrabajo_Command"></asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton runat="server" text ="PDAGO Madre" ID="lnkPDAGOMadre" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OT_HIJAS_RECHAZADAS.PDAGO_MADRE%>" CommandArgument="ASC" OnCommand="lnkRpOrdenTrabajo_Command"></asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton runat="server" text ="Consecutivo Hija" ID="lnkConsecutivoHija" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OT_HIJAS_RECHAZADAS.CONSECUTIVO_HIJA%>" CommandArgument="ASC" OnCommand="lnkRpOrdenTrabajo_Command"></asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton runat="server" text ="PDAGO Hija" ID="lnkPdagoHija" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OT_HIJAS_RECHAZADAS.PDAGO_HIJA%>" CommandArgument="ASC" OnCommand="lnkRpOrdenTrabajo_Command"></asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton runat="server" text ="Categoría Madre" ID="lnkCatMadre" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OT_HIJAS_RECHAZADAS.CATEGORIA_MADRE%>" CommandArgument="ASC" OnCommand="lnkRpOrdenTrabajo_Command"></asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton runat="server" text ="Actividad Madre" ID="lnkActMadre" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OT_HIJAS_RECHAZADAS.ACTIVIDAD_MADRE%>" CommandArgument="ASC" OnCommand="lnkRpOrdenTrabajo_Command"></asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton runat="server" text ="Categoría Hija" ID="LnkCatHija" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OT_HIJAS_RECHAZADAS.CATEGORIA_HIJA%>" CommandArgument="ASC" OnCommand="lnkRpOrdenTrabajo_Command"></asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton runat="server" text ="Actividad Hija" ID="LnkActHija" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OT_HIJAS_RECHAZADAS.ACTIVIDAD_HIJA%>" CommandArgument="ASC" OnCommand="lnkRpOrdenTrabajo_Command"></asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton runat="server" text ="Descripcion de trabajo" ID="LnkDescripcion" CommandName="<%#Utilerias.OrdenesDeTrabajo.Modelo.V_OT_HIJAS_RECHAZADAS.DESCRIPCION%>" CommandArgument="ASC" OnCommand="lnkRpOrdenTrabajo_Command"></asp:LinkButton>
                        </th>
                        <th>&nbsp;</th>
                        <th>&nbsp;</th>

                    </tr>
            </HeaderTemplate>

             <ItemTemplate>
                <tr class="lineaDelListado">
                    
                    <td><%# Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_HIJAS_RECHAZADAS.CONSECUTIVO_MADRE)%></td>
                    <td><%# Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_HIJAS_RECHAZADAS.PDAGO_MADRE)%></td>
                    <td><%# Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_HIJAS_RECHAZADAS.CONSECUTIVO_HIJA)%></td>
                    <td><%# Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_HIJAS_RECHAZADAS.PDAGO_HIJA)%></td>
                    <td><%# Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_HIJAS_RECHAZADAS.CATEGORIA_MADRE)%></td>
                    <td><%# Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_HIJAS_RECHAZADAS.ACTIVIDAD_MADRE)%></td>
                    <td><%# Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_HIJAS_RECHAZADAS.CATEGORIA_HIJA)%></td>
                    <td><%# Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_HIJAS_RECHAZADAS.ACTIVIDAD_HIJA)%></td>
                    <td>
                        <img title="<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_HIJAS_RECHAZADAS.DESCRIPCION)%>" class="tooltip tooltipstered" src='<%#AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.COLOR , "Icono_Ayuda.png")%>' />
                    </td>
                    <td><asp:ImageButton ToolTip="Visto Bueno al motivo de rechazo" runat="server" ImageUrl='<%#AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Check.png")%>' ID="imgbtnVbueno" CommandArgument='<%# String.Format("{0},{1},{2}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_HIJAS_RECHAZADAS.ID_OT), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_HIJAS_RECHAZADAS.NUM_EMPLEADO), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_HIJAS_RECHAZADAS.ID_UBICACION))%>' OnClick="imgbtnVbueno_Click" /> </td>
                    <td><asp:ImageButton ToolTip="Denegación al motivo de rechazo" runat="server" ImageUrl='<%#AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Borrar_Detalle.png")%>' ID="imgbtnDenegacion" CommandArgument='<%# String.Format("{0},{1},{2}", Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_HIJAS_RECHAZADAS.ID_OT), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_HIJAS_RECHAZADAS.NUM_EMPLEADO_COORDINADOR), Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_HIJAS_RECHAZADAS.ID_UBICACION))%>' OnClick="imgbtnDenegacion_Click" /></td>
                    
                </tr>
            </ItemTemplate>

             <FooterTemplate>
                 
                </table>
            </FooterTemplate>
        </asp:Repeater>
    </article>
    <%--<article runat="server" id="arMotivo" >
        <asp:Label runat="server">Motivo o Justificación</asp:Label>
        <asp:TextBox data-tipoControl="texto" Height="30" Width="40%" runat="server" ID="txtMotivo" ></asp:TextBox>
        <asp:RequiredFieldValidator runat="server" id="rvftxtMotivo" ControlToValidate="txtMotivo" display="Dynamic" ErrorMessage="Ingrese una justificación">&nbsp;</asp:RequiredFieldValidator>
    </article>--%>
    <article data-grupo="Listado" class="areaPaginadorListado">
        <wuc:PaginadorNumerico runat="server" ID="pnOT_Rechazadas" />
    </article>

    <article data-grupo="Listado" class="areaCantidadDeRegistro">
        <asp:Label runat="server" ID="lblCantidadDeRegistros" Text="" Visible="true"></asp:Label>
    </article>

    <article id="arAlerta"></article>
    <article id="arpopupConfirmaDeseaBorrar"></article>
    <article id="arPopupGenerico"></article>

    <script type="text/javascript">


        function mostrarAlertaError(pvc_Mensaje) {
            mostrarAlerta(
                '#arAlerta',
                {
                    mensaje: pvc_Mensaje,
                    tipo: 'peligro',
                    transparencia: 0.9,
                    posicion: 'center'
                }
            );
        }

        function mostrarAlertaNoHayDatos() {
            mostrarAlerta(
                '#arAlerta',
                {
                    mensaje: 'No se cuenta con información para mostrar.',
                    tipo: "advertencia",
                    transparencia: 0.9,
                    posicion: 'center'
                }
            );

            deshabilitarControl('#btnCancelarBusqueda');

            ocultarAreaDeListado();
            mostrarAreaFiltrosDeBusqueda();

        }

        function mostrarAlertaOperacionVistoBuenoExitosa() {
            mostrarAlerta(
                '#arAlerta',
                {
                    mensaje: "Operación de visto bueno exitosa",
                    tipo: "exito",
                    transparencia: 0.9,
                    posicion: 'center'
                }
            );
        }

        function mostrarAlertaOperacionDenegacionExitosa() {
            mostrarAlerta(
                '#arAlerta',
                {
                    mensaje: "Operación de denegación exitosa",
                    tipo: "exito",
                    transparencia: 0.9,
                    posicion: 'center'
                }
            );
        }

        function redireccionarListado(pvc_PaginaDestino) {
            window.location = pvc_PaginaDestino
        };

        $(document).ready(function () {
            aplicarConfiguracionBasicaListado({ alternarVisibilidadFiltroLista: true });



            $('[data-tipo="tooltip"]').each(function () {
                $('#' + this.id).tooltipster({
                    interactive: true,
                    interactiveTolerance: 400,
                    theme: 'tooltipster-light'
                });
            });

        });
        </script>
</asp:content>