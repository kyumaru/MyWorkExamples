<%@ Page Title="Asignación de Autores a Libros" Language="VB" MasterPageFile="~/MasterPage/Mp_Formulario.master" AutoEventWireup="false" CodeFile="Frm_TC_AutoresPorLibro.aspx.vb" Inherits="Catalogos_Frm_TC_AutoresPorLibro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphFormulario" Runat="Server">
    <header>
        <h2>Asociar Autores a un Libro</h2>
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
                </td>
            </tr>
            <tr>
                <th>Título</th>
                <td>
                    <asp:Label runat="server" ID="lblTitulo"></asp:Label>
                </td>
            </tr>
        </table>
    </article>

    <article class="tituloSeccion">
        Autores
    </article>

    <article class="listado">
        <table>
            <tr>
                <th>Autores No Asignados</th>
                <th>&nbsp;</th>
                <th>Autores del Libro</th>
            </tr>
            <tr>
                <td>
                    <asp:CheckBoxList runat="server" ID="ckblAutoresNoAsociados"></asp:CheckBoxList>
                </td>
                <td class="areaBotonesInvertido">
                    <asp:Button runat="server" ID="btnAsignar" Text=">" />
                    <br />
                    <br />
                    <asp:Button runat="server" ID="btnQuitar" Text="<" />
                </td>
                <td>
                    <asp:CheckBoxList runat="server" ID="ckblAutoresDelLibro"></asp:CheckBoxList>
                </td>
            </tr>
        </table>
    </article>

    <article class="areaBotones">
        <input id="btnFinalizar" type="button" value="Finalizar" />
    </article>

    <article id="arAlertasDelFormulario"></article>

    <script type="text/javascript">
        function regresarAlListado() {
            window.location = 'Lst_TC_Libro.aspx';
        }

        function deshabilitarFormulario() {
            deshabilitarControl('#btnFinalizar');
            $('.formulario').attr('disabled', 'disabled');
            $('.listado').attr('disabled', 'disabled');
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
                    onClosed: function () { regresarAlListado(); }
                }
            );
        }

        $(document).ready(function () {
            $('#btnFinalizar').click(function () {
                regresarAlListado();
            });
        });
    </script>
</asp:Content>