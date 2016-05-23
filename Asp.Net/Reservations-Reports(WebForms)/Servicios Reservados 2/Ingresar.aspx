<%@ Page Title="Ingresar" Language="C#" AutoEventWireup="true" CodeBehind="Ingresar.aspx.cs" Inherits="Servicios_Reservados_2.Ingresar" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
        <link href="~/faviconOET.ico" rel="shortcut icon" type="image/x-icon" />
    <link rel="stylesheet" href="Content/bootstrap-3.3.4-dist/css/bootstrap.css" />
    <link rel="stylesheet" href="Content/Ingresar.css" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>
        Inicio de sesion
    </title>
</head>
<body>
    <header>
         <img src="/Images/logo_oet_español_todo_blanco.png" style="width:225px" title="Página principal">
    </header>
    <section>
        <form id="form1" runat="server">
            <div class="well bs-component" >
                 <legend style="color: #7BC143">Inicio de sesión</legend>
                    <h5>Usuario:</h5>
                    <asp:TextBox ID="txtUsuario" runat="server"></asp:TextBox>
                    <h5>Contraseña:</h5>           
                    <asp:TextBox ID="txtContraseña" TextMode="Password" runat="server"></asp:TextBox>           
                    <asp:Button id="btnSalir" class="btn btn-success" runat="server" Text="Iniciar sesión" OnClick="btnIniciar_Click"/> 
                    <asp:Label ID="lblMensaje" runat="server" ForeColor="#996600"></asp:Label>
             </div>
            <aside>
                <h1>Sistema de servicios reservados</h1>
             <p>Bienvienido al sistema de control de servicios reservados de la organizacion de estudios tropicales. Inicie sesion con una cuenta de la OET para acceder</p>
            </aside>
             
        </form>
    </section>
</body>
</html>
