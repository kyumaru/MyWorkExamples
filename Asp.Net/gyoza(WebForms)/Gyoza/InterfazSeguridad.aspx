<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="InterfazSeguridad.aspx.cs" Inherits="Gyoza.InterfazSeguridad" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron" style="border: medium outset #3992D8">

        <!-- Botones -->
        <div class="well bs-component" style="border: medium outset #0099FF">
            <fieldset style="text-align: center">
                <legend style="font-weight: 700">Panel de control</legend>

                <div class="col-sm-3">
                                    <a ID="botonAyuda" href="#modalAyuda" data-toggle="modal" runat="server" class="btn btn-primary" role="button">
                                        <i class="glyphicon glyphicon-list"></i><br />
                                        Ayuda en línea
                                    </a>
                                </div>

                <div class="col-lg-5">
                    <div id="Alerta" class="alert alert-danger fade in" runat="server" hidden="hidden">
                        <button type="button" class="close" data-dismiss="alert" aria-hidden="true"></button>
                        <strong>
                            <asp:Label ID="labelTipoAlerta" runat="server" Text="Alerta! "></asp:Label></strong><asp:Label ID="labelAlerta" runat="server" Text="Mensaje de alerta"></asp:Label>
                    </div>
                </div>
            </fieldset>
        </div>



        <fieldset>
            <asp:Label ID="Label12" runat="server" Text="Selecionar Rol"></asp:Label>
            <asp:DropDownList ID="textEstadoRequerimiento" runat="server" AutoPostBack="True" OnSelectedIndexChanged="textEstadoRequerimiento_SelectedIndexChanged">
                <asp:ListItem Text="" Value="" />
                <asp:ListItem>Administrador</asp:ListItem>
                <asp:ListItem>Cliente</asp:ListItem>
                <asp:ListItem Value="Miembro">Miembro de equipo</asp:ListItem>
            </asp:DropDownList>
        </fieldset>


         <fieldset style="margin-top:30px">
        <div class="col-lg-18">
            <div class="row row-div">
                <div class="col-sm-3">
                    <div class="well bs-component" style="border: medium outset #0099FF">
            <fieldset>
            <fieldset>
                <asp:Label ID="Label9" runat="server" Text="Proyecto" Font-Bold="True"></asp:Label>
            </fieldset>
            
                <asp:CheckBoxList ID="permisosProyecto" runat="server">
                    <asp:ListItem>Consultar</asp:ListItem>
                    <asp:ListItem>Modificar</asp:ListItem>
                    <asp:ListItem>Eliminar</asp:ListItem>
                    <asp:ListItem>Agregar</asp:ListItem>
                    <asp:ListItem>Manejo miembros</asp:ListItem>
                </asp:CheckBoxList>
            </fieldset>
        </div>

                    </div>
             <div class="col-sm-3">
                    <div class="well bs-component" style="border: medium outset #0099FF">
           
             <fieldset>
            <fieldset>
                <asp:Label ID="Label1" runat="server" Text="Cuenta" Font-Bold="True"></asp:Label>
            </fieldset>
           
                <asp:CheckBoxList ID="permisosCuenta" runat="server">
                    <asp:ListItem>Consultar</asp:ListItem>
                    <asp:ListItem>Modificar</asp:ListItem>
                    <asp:ListItem>Eliminar</asp:ListItem>
                    <asp:ListItem>Agregar</asp:ListItem>
                </asp:CheckBoxList>
            </fieldset>
        </div>
</div>
                 <div class="col-sm-3">
                    <div class="well bs-component" style="border: medium outset #0099FF">
         
             <fieldset>
            <fieldset>

                <asp:Label ID="Label3" runat="server" Text="Requerimientos" Font-Bold="True"></asp:Label>
            </fieldset>
           
                <asp:CheckBoxList ID="permisosRequerimiento" runat="server">
                    <asp:ListItem>Consultar</asp:ListItem>
                    <asp:ListItem>Modificar</asp:ListItem>
                    <asp:ListItem>Eliminar</asp:ListItem>
                    <asp:ListItem>Agregar</asp:ListItem>
                </asp:CheckBoxList>
            </fieldset>
        </div>
</div>

 <div class="col-sm-3">
                    <div class="well bs-component" style="border: medium outset #0099FF">
           
             <fieldset>
            <fieldset>
                <asp:Label ID="Label4" runat="server" Text="Seguridad" Font-Bold="True"></asp:Label>
            </fieldset>
            
                <asp:CheckBoxList ID="permisosSeguridad" runat="server">
                    <asp:ListItem>Consultar</asp:ListItem>
                </asp:CheckBoxList>
            </fieldset>
        </div>
     </div>
                </div>
            </div>
             </fieldset>
        <div class="col-lg-12">
            <div class="text-center">
                <button runat="server" id="botonAceptar" onserverclick="clickAceptar" class="btn btn-success" type="submit">Aceptar</button>
                <button runat="server" id="botonCancelar" class="btn btn-danger" type="reset">Cancelar</button>
            </div>
        </div>

        
     <!--Modal Ayudar-->
    <div class="modal fade" id="modalAyuda" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4 class="modal-title" id="myModalLabel1212"><i class="fa fa-exclamation-triangle text-danger fa-2x"></i>Ayuda en línea</h4>
                </div>
                <div class="modal-body">
                    <b>Módulo de seguridad</b><br>
                   <p style="font-size:14px">Es donde se manejan los permisos de los diferentes tipos de roles que pueden tener una cuenta.<br><br>
                    Este módulo es propio para el administrador. Sólo él podrá accesar al módulo para modificar los permisos de los diferentes tipos de roles.<br><br><br></p>
                     
                    <p style="font-size:16px"><b>Para consultar los permisos de un rol</b><br></p>
                     <p style="font-size:14px"><b>+</b>Seleccione el rol cuyos permisos quiere consultar del menú desplegable en pantalla.<br><br>
                         Una vez que le dió click al rol, se le cargará en la interfaz los permisos de ese rol.<br><br />
                     </p>
                    <p style="font-size:16px"><b>Para modificar los permisos de un rol</b><br></p>
                     <p style="font-size:14px"><b>+</b>Consulte los permisos de un rol, siguiendo los pasos ya descritos.<br><br>
                         <b>+</b>Marque o desmarque las casillas de permisos según la modificación que quiera hacer.<br><br>
                         Una vez que modificados todos los valores, dele click al botón de Aceptar al pie de la página.<br><br />
                     </p>
                    
                </div>
                <div class="modal-footer">
                    <button type="button" id="Button2" class="btn btn-succes" runat="server" data-dismiss="modal">Aceptar</button>
                </div>
            </div>
        </div>
    </div>



    </div>
</asp:Content>

