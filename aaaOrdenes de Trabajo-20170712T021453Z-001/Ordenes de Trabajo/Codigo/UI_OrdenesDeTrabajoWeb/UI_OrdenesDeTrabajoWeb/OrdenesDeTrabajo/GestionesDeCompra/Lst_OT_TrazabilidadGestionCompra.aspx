<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/Mp_Listado.master" AutoEventWireup="false" CodeFile="Lst_OT_TrazabilidadGestionCompra.aspx.vb" Inherits="OrdenesDeTrabajo_GestionesDeCompra_Lst_OT_TrazabilidadGestionCompra" %>

<%@ Register TagPrefix="wuc" Namespace="Utilerias.WebForms.Controles" Assembly="Utilerias.WebForms.Controles" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphFormulario" Runat="Server">
    <header>
        <h2><asp:Label runat="server" ID="lblTitulo"></asp:Label></h2>
    </header>
    <article class="tituloSeccion">
        Gestión de Compra
    </article>
    <article class="formulario">
        <table>
            <tr>
                <th>N° de Gestión</th>
                <td>
                    <asp:Label runat="server" ID="lblNumeroGestion"></asp:Label>
                </td>
            </tr>
            <tr runat="server" id="trObservaciones">
                <th>Observaciones</th>
                <td>
                    <asp:Textbox runat="server" ID="txtObservaciones" TextMode="MultiLine" Rows="6" Width="100%"></asp:Textbox>
                </td>
            </tr>
        </table>
    </article>

    <article data-grupo="Listado" class="tituloSeccion">
        Listado de Trámites
    </article>

    <article data-grupo="Listado" class="listado">
        <asp:Repeater runat="server" ID="rpTramites">
            <HeaderTemplate>
                <table>
                    <tr>                       
                        <th>
                            <asp:LinkButton runat="server" ID="lnkTramite" Text="Trámite" CommandName="<%# Utilerias.OrdenesDeTrabajo.Modelo.V_OTL_TRAZABIL_GESTION_COMPLST.DESC_ESTADO%>" CommandArgument="ASC" OnCommand="lnkRpTramites_Command"></asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton runat="server" ID="lnkFecha" Text="Fecha Trámite" CommandName="<%# Utilerias.OrdenesDeTrabajo.Modelo.V_OTL_TRAZABIL_GESTION_COMPLST.TIME_STAMP%>" CommandArgument="ASC" OnCommand="lnkRpTramites_Command"></asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton runat="server" ID="lnkResponsable" Text="Responsable" CommandName="<%# Utilerias.OrdenesDeTrabajo.Modelo.V_OTL_TRAZABIL_GESTION_COMPLST.RESPONSABLE%>" CommandArgument="ASC" OnCommand="lnkRpTramites_Command"></asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton runat="server" ID="lnkObservaciones" Text="Observaciones" CommandName="<%# Utilerias.OrdenesDeTrabajo.Modelo.V_OTL_TRAZABIL_GESTION_COMPLST.OBSERVACIONES%>" CommandArgument="ASC" OnCommand="lnkRpTramites_Command"></asp:LinkButton>
                        </th>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr class="lineaDelListado">
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTL_TRAZABIL_GESTION_COMPLST.DESC_ESTADO)%></td>
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTL_TRAZABIL_GESTION_COMPLST.TIME_STAMP)%></td>
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTL_TRAZABIL_GESTION_COMPLST.RESPONSABLE)%></td>
                    <td>
                        <article style="display: <%#IIf(Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTL_TRAZABIL_GESTION_COMPLST.OBSERVACIONES).ToString.StartsWith("-"), "none", "block")%>">
                            <asp:ImageButton runat="server" ID="ImageButton1" data-tipo="tooltip"
                                CssClass="centradoEnRow"
                                title='<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTL_TRAZABIL_GESTION_COMPLST.OBSERVACIONES)%>'
                                ImageUrl='<%#AdministradorRecursos.ObtenerRutaImagen(AdministradorRecursos.TAMANO_IMAGEN.x16, AdministradorRecursos.COLOR_IMAGEN.GRIS, "Icono_Informacion.png")%>' />
                        </article>
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
    </article>

    <article data-grupo="Listado" class="areaPaginadorListado">
         <asp:Label runat="server" ID="lblNoHayDAtos" Text="No se cuenta con información para mostrar" Visible="false"></asp:Label>
        <wuc:PaginadorNumerico runat="server" ID="pnRpTramite" />
    </article>
    <article data-grupo="Listado" class="areaCantidadDeRegistro">
        <asp:Label runat="server" ID="lblCantidadRegistroAcuerdos" Text=""> </asp:Label>
    </article>


    <article class="areaBotones">
        <input id="btnRegresar" type="button" value="Regresar" />
    </article>

    <article id="arAlerta"></article>

    <article style="visibility: hidden">
        <asp:TextBox ID="txtPaginaRegreso" runat="server" ></asp:TextBox>
    </article>

    <script type="text/javascript">
        function mostrarAlertaNoHayDatos() {
            mostrarAlerta(
                '#arAlerta',
                {
                    mensaje: 'No se cuenta con datos para la gestión de compra seleccionada',
                    tipo: "advertencia",
                    transparencia: 0.9,
                    posicion: 'top',
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
                    posicion: 'top',
                    permiteCerrar: true
                }
                );

        }


        function regresarAlListado() {
            var RutaRegresar = document.getElementById('<%=txtPaginaRegreso.ClientID%>').value;
            window.location = RutaRegresar;
        }

        $(document).ready(function () {

            $('#btnRegresar').click(function () {
                regresarAlListado();
            });

            habilitarTooltipGenerico()

        });

    </script>
</asp:Content>

