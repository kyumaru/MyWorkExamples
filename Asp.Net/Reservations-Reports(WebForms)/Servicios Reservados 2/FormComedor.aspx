<%@ Page Title="Comedor" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FormComedor.aspx.cs" Inherits="Servicios_Reservados_2.FormComedor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="stylesheet" href="Content/comedor.css" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <fieldset id="form1" runat="server">
    <nav>
            <ul>
                <li class="item-navegacion">Caja</li>
                <li class="item-navegacion">Reportes</li>
                <li class="item-navegacion">Horarios</li>
                <li class="item-navegacion">Notificaciones <span class="notificacion">0</span></li>
            </ul>
        </nav>
        <h1>
        Caja
        </h1>
        <section class="principal">
            <section class="contenedor">
                <ul>
                    <li class="itemContenedor">
                     Tiquete:<input id="tiquete" />
                     </li>
                     <li class="itemContenedor">
                     <input type="button" value="Verificar" />
                     </li>
                     <li class="itemContenedor">
                     <input type="button" value="Servir" />
                     </li>
                </ul>
            </section>
                <table>
					<caption>Información de reservación</caption>
					<tr>
						<th>Cliente</th>
						<th>Anfitriona</th>
						<th>Estación</th>
						<th>Servido</th>
						<th>Notas</th>
					</tr>
					<tr>
						<td><textarea></textarea></td>
						<td></td>
						<td></td>
						<td></td>
						<td><textarea></textarea></td>
					</tr>
				</table>
         </section>
    </fieldset>
</asp:Content>
