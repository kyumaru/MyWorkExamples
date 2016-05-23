<%@ Page Title="Página principal" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Servicios_Reservados_2._Default" %>


<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <link rel="stylesheet" href="Content/default.css" />
    <div class="jumbotron">
        <div class="container">
            <h1>Sistema de servicios reservados</h1>
            <p>Con este sistema podrá llevar un control de los servicios consumidos dentro de la organización</p>
        </div>
    </div>
    <table>
        <tr>
            <td>
                <div class="well bs-component" id="Recepcionista" runat="server">

                    <h2>Recepcion</h2>
                    <p>Reserve servicios de alimentación</p>
                    <h4>
                        <a href="FormReservaciones.aspx">Reservaciones</a>
                    </h4>
                    <h4>
                        <a href="FormEmpleado.aspx">Empleados</a>
                    </h4>
                    <h4>
                        <a href="FormReportes.aspx">Reportes general</a>
                    </h4>

                </div>
            </td>
            <td>
                <div class="well bs-component" id="Cocina" runat="server">
                    <h2>Cocina</h2>
                    <p>Lleve el control de los servicios consumidos</p>
                    <h4>
                        <a href="FormServirPlatos.aspx">Servir platos</a>
                    </h4>
                    <h4>
                        <a href="FormReporteCocina.aspx">Reportes de cocina</a>
                    </h4>
                    <h4>
                        <a href="FormReportes.aspx">Reportes general</a>
                    </h4>
                </div>
            </td>
            <td>
                <div class="well bs-component" id="Financiero" runat="server">
                    <h2>Administración</h2>
                    <p>Consulte información que ayudará en la toma de decisiones</p>
                    <h4>
                        <a href="FormReportes.aspx">Reportes general</a>
                    </h4>
                </div>
            </td>
            <td>
                <div class="well bs-component" runat="server">
                    <h2>Sistema</h2>
                    <p>Maneje los usuario y sus roles dentro del sistema</p>
                    <h4>
                        <a href="Notificaciones.aspx">Notificaciones</a>
                    </h4>
                    <h4 id="Usuario" runat="server">
                        <a href="FormUsuario.aspx">Administración de usuarios</a>
                    </h4>

                </div>

            </td>
        </tr>
    </table>




</asp:Content>
