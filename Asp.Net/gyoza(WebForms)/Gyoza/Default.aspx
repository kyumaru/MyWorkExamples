<%@ Page Title="UCR" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Gyoza._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron col-lg-12">
        <h1>Gyoza</h1>
        <p class="lead">Sistema de administración de requerimientos.&nbsp;</p>
      <%--  <p><a href="http://www.asp.net" class="btn btn-primary btn-lg">Acerca de &raquo;</a></p>--%>

         <br>
        <div  class="col-sm-4">
            <h3><b>Universidad de Costa Rica</b></h3>
            <p>     
                <h4>
                <br>
                ECCI
                <hr>

                Proyecto programado para el curso Ingeniería de Software I.<br>
                Prof. Gabriela Salazar Bermúdez<br><br><br>

                Integrantes:<br><br>
                Juan Carlos Porras Quirós<br>
                Leonardo Villalobos Arias<br>
                Fernando Mata Mora<br>
                Juan Carlos Soto Esquivel<br>
                Andrés Jiménez Serrano<br>
                    </h4>
            </p>
     </div>
        <div  class="col-sm-3">
                  <%--  this column is for padding, notice the tec is to have a container div wich is then added colums,class="col-sm-5" number is weight column for the divs is a built in .css style pack--%>

            </div>

         <div  class="col-sm-5">
                 <h3>Información de contacto</h3>
   <h4> <address>
       UCR<br />
        Escuela de Ciencias de la Computación e Informática<br />
    </address>

    <address>
        <strong>Support:</strong>   <a href="mailto:ffmm.14@gmail.com">ffmm.14@gmail.com</a><br />
        <strong>Marketing:</strong> <a href="mailto:artist_artistian@hotmail.com">artist_artistian@hotmail.com</a>
    </address></h4>
         </div>

    </div>

    <div class="row">
    </div>

    <div class="col-lg-5">
        <div id="Alerta" class="alert alert-danger fade in" runat="server" hidden="hidden">
            <button type="button" class="close" data-dismiss="alert" aria-hidden="true"></button>
            <strong>
                <asp:Label ID="labelTipoAlerta" runat="server" Text="Alerta! "></asp:Label></strong><asp:Label ID="labelAlerta" runat="server" Text="Mensaje de alerta" Font-Size="XX-Large"></asp:Label>
        </div>


    
 

    </div>

</asp:Content>
