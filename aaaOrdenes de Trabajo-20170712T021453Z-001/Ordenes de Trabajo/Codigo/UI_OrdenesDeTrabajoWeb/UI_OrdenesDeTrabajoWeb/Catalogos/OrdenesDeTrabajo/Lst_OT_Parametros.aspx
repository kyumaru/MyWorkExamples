<%@ Page Title="Listado de Parámetros del Sistema" Language="VB" MasterPageFile="~/MasterPage/Mp_Listado.master" AutoEventWireup="false" CodeFile="Lst_OT_Parametros.aspx.vb" Inherits="Catalogos_Lst_OT_Parametros" %>

<%@ Register TagPrefix="wuc" Namespace="Utilerias.WebForms.Controles" Assembly="Utilerias.WebForms.Controles" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphFormulario" Runat="Server">
    <header>
        <h2>Catálogo de Parámetros del Sistema</h2>
    </header>

    <article data-grupo="Listado" class="tituloSeccion">
        Listado de Parámetros del Sistema
    </article>

    <article data-grupo="Listado" class="listado">
        
        <asp:Repeater runat="server" ID="rpParametros">
            <HeaderTemplate>
                <table>
                    <tr>
                        <th style="width:45%">
                            <asp:LinkButton runat="server" ID="lnkDescripcion" Text="Descripción" CommandName="<%# Utilerias.OrdenesDeTrabajo.Modelo.V_OTP_PARAMETRO_UBICACIONLST.DESCRIPCION%>" CommandArgument="ASC" OnCommand="lnkRpParametros_Command" ></asp:LinkButton>
                        </th>
                        <th style="width:45%">
                            <asp:LinkButton runat="server" ID="lnkValor" Text="Valor" CommandName="<%# Utilerias.OrdenesDeTrabajo.Modelo.V_OTP_PARAMETRO_UBICACIONLST.VALOR_UNION%>" CommandArgument="ASC" OnCommand="lnkRpParametros_Command" ></asp:LinkButton>
                        </th>
                        <th style="width:10%">&nbsp</th>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr class="lineaDelListado">
                    <td><%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTP_PARAMETRO_UBICACIONLST.DESCRIPCION)%></td>
                    <td>
                        <%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTP_PARAMETRO_UBICACIONLST.VALOR_UNION)%>
                    </td>
                    <td>
                        <a href="Frm_OT_Parametros.aspx?pvn_Operacion=<%# Utilerias.OrdenesDeTrabajo.eOperacion.Modificar%>&pvc_IdParametro=<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTP_PARAMETRO_UBICACIONLST.ID_PARAMETRO)%>&pvn_IdUbicacion=<%#Eval(Utilerias.OrdenesDeTrabajo.Modelo.V_OTP_PARAMETRO_UBICACIONLST.ID_UBICACION_ADMINISTRA)%>"> 
                            <img alt="Modificar datos del Parámetro" data-tipo="modificarRegistro" src="" />
                        </a>
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
    </article>

    <article data-grupo="Listado" class="areaPaginadorListado">
        <wuc:PaginadorNumerico runat="server" ID="pnRpParametros" /> <%--Paginador del repeater RpUbicacion--%>
    </article>
    <article data-grupo="Listado" class="areaCantidadDeRegistro">
        <asp:Label runat="server" ID="lblCantidadRegistro" Text=""> </asp:Label> 
    </article>

    <article id="arAlerta"></article>
    <article id="arPopupGenerico"></article>

    <script type="text/javascript" >
        function mostrarAlertaNoHayDatos() {
            mostrarAlerta(
                '#arAlerta',
                {
                    mensaje: 'No se cuenta con parámetros que cumplan con la condición indicada',
                    tipo: "advertencia",
                    transparencia: 0.9,
                    posicion: 'center',
                    permiteCerrar: true,
                    onClosed: function () { regresarPrincipal(); }
                }
                );
            ocultarAreaDeListado();
        }

        function regresarPrincipal() {
            window.location = '../../Genericos/Frm_MenuPrincipal.aspx';
        };

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

        function redireccionarListado(pvc_PaginaDestino) {
            window.location = pvc_PaginaDestino
        };


        $(document).ready(function () {
            aplicarConfiguracionBasicaListado({ alternarVisibilidadFiltroLista: true });
        }
            )
    </script>
</asp:Content>

