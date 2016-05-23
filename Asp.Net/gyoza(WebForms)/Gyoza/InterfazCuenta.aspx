<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="True" CodeBehind="InterfazCuenta.aspx.cs" Inherits="Gyoza.InterfazCuenta" MaintainScrollPositionOnPostback="true" EnableEventValidation="false" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron" style="border: medium outset #3992D8; background-color: #F5F5F5">
        <h1 style="font-size: larger">Administración de Cuentas</h1>
        <div aria-dropeffect="popup" style="background-color: #F5F5F5">
    
        <div class="well bs-component" style="border: medium outset #1EA3FE">
                <fieldset style="text-align: center" color: "#809897">

                    <legend style="font-weight: 700">Panel de control</legend>
                    <div class="col-lg-11">
                        <div class="row row-botones">
                            <div class="btn-group btn-group-justified">
                                <div class="col-sm-2">
                                    <button ID="botonInsertar" runat="server" class="btn btn-primary" type="button" onserverclick="clickInsertar">
                                        <i class="glyphicon glyphicon-plus"></i><br />
                                        Agregar
                                    </button>
                                </div>
                                <div class="col-sm-2">
                                    <button ID="botonModificar" runat="server" class="btn btn-primary" type="button" onserverclick="clickModificar">
                                        <i class="glyphicon glyphicon-pencil"></i><br />
                                        Modificar
                                    </button>
                                </div>
                                <div class="col-sm-2">
                                    <a ID="botonEliminar" href="#modalEliminar" data-toggle="modal" runat="server" class="btn btn-primary" type="button">
                                        <i class="glyphicon glyphicon-trash"></i><br />
                                        Eliminar
                                    </a>
                                </div>
                                <div class="col-sm-2">
                                    <a ID="botonConsultar" href="#modalConsultar" data-toggle="modal" runat="server" class="btn btn-primary" role="button">
                                        <i class="glyphicon glyphicon-list"></i><br />
                                        Consultar
                                    </a>
                                </div>
                                
                                <div class="col-sm-2">
                                    <a ID="botonAyuda" href="#modalAyuda" data-toggle="modal" runat="server" class="btn btn-primary" role="button">
                                        <i class="glyphicon glyphicon-list"></i><br />
                                        Ayuda en línea
                                    </a>
                                </div>
                            </div>
                        </div>
                    </div>
                   
                </fieldset>
            <fieldset>
             <div class="col-lg-5">
                        <div id="alertAlerta" class="alert alert-danger fade in" runat="server" hidden="hidden">
                            <button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
                            <strong>
                                <asp:Label ID="labelTipoAlerta" runat="server" Text="Alerta! "></asp:Label></strong><asp:Label ID="labelAlerta" runat="server" Text="Mensaje de alerta"></asp:Label>
                        </div>
                    </div>
                </fieldset>
            </div>
                   


    <div class="well bs-component" style="border: medium outset #0099FF; background-color: #F5F5F5">
                    
<strong>Información general</strong><br />
        <fieldset>              
            <asp:Label ID="Label1" runat="server" Text="Nombre" ></asp:Label>

            <asp:TextBox ID="textNombre" required="required" pattern= "^[A-Za-z ]{0,23}$" runat="server" Height="30px"></asp:TextBox>

            <asp:Label ID="labelApellidos" required="required" pattern= "^[A-Za-z ]{0,23}$" runat="server" Text="Apellidos" ></asp:Label>

            <asp:TextBox ID="textApellidos" required="required" runat="server" Height="30px"></asp:TextBox>
        </fieldset>
        <fieldset>
<asp:Label ID="Label2" runat="server" Text="Cédula" ></asp:Label>
<asp:TextBox ID="textCedula" required="required" pattern="^[0-9]{9,9}$" runat="server" Height="30px"></asp:TextBox>
            </fieldset>
            </div>

    
           <div class="well bs-component" style="border: medium outset #0099FF; background-color: #F5F5F5">

                    <strong>Información de contacto<br /></strong>
                        <fieldset>
                        <asp:Label ID="Label13" runat="server" Text="Teléfonos Oficina"></asp:Label>
  
                        <asp:TextBox ID="textOficina" pattern="^[0-9]{8,8}$" runat="server" Height="30px" placeholder="99999999"></asp:TextBox>

                        <asp:Label ID="Label15" runat="server" Text="Celular"></asp:Label>

                        <asp:TextBox ID="textCelular" pattern="^[0-9]{8,8}$" runat="server" Height="30px" placeholder="99999999"></asp:TextBox>
                        </fieldset>
                        <br />
                        
                        <asp:Label ID="Label16" runat="server" Text="Correo electrónico"></asp:Label>
                        <asp:TextBox ID="textCorreo" required="required" pattern="^([a-z]|[A-Z]|\.|[0-9])+\@[a-z](\.[a-b])+*$" runat="server" Width="397px" Height="30px" ToolTip="ejemplo hover"  placeholder="admin@red.com"></asp:TextBox>
                    

                    
              
            </div>



    <div class="well bs-component" style="border: medium outset #0099FF; background-color: #F5F5F5">
               
                    <strong> Datos de la cuenta<br /></strong>
                        <fieldset>
                        <asp:Label ID="Label17" runat="server" Text="Nombre de Usuario"></asp:Label>

                        <asp:TextBox ID="textUsername" required="required" pattern="[A-Za-z0-9]{4,20}" runat="server" Height="30px" placeholder="Nombre"></asp:TextBox>
                        <asp:Label ID="Label24" runat="server" Font-Bold="False" Font-Names="Arabic Typesetting" Font-Underline="True" ForeColor="#999999" Text="Mínimo 4 caracteres"></asp:Label>
                        </fieldset>
                        <fieldset>
                        <asp:Label ID="Label18" runat="server" Text="Contraseña"></asp:Label>

                        <asp:TextBox ID="textPassword" required="required" pattern="[A-Za-z0-9]{4,20}" runat="server" TextMode="Password" Height="30px" placeholder="****"></asp:TextBox>
                        <asp:Label ID="Label23" runat="server" Font-Bold="False" Font-Names="Arabic Typesetting" Font-Underline="True" ForeColor="#999999" Text="Mínimo 4 caracteres"></asp:Label>
                            </fieldset>
                        <fieldset>
                        <asp:Label ID="Label19" runat="server" Text="Rol de usuario"></asp:Label>
                        <br />
                        <asp:DropDownList ID="textRol" runat="server">
                            <asp:ListItem>Administrador</asp:ListItem>
                            <asp:ListItem>Cliente</asp:ListItem>
                            <asp:ListItem>Miembro de equipo</asp:ListItem>
                        </asp:DropDownList>

                            </fieldset>
                   
            </div>

             <div class="col-lg-12">
                        <div class="text-center">
     <button runat="server" onserverclick="clickAceptar" id="botonAceptar" class="btn btn-success" type="submit">Aceptar</button>
                        <button runat="server" onserverclick="clickCancelar" id="botonCancelar" class="btn btn-danger" type="reset">Cancelar</button>
                            </div>
                 </div>
         <!--Modal Consultar-->
        <div class="modal fade" id="modalConsultar" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                        <h4 class="modal-title" id="H1"><i class="fa fa-search"></i>Consultar Cuentas</h4>
                    </div>
                    <div class="modal-body">
                        <div class="col-lg-12">
                            <asp:UpdatePanel ID="UpdatePanelPruebas" runat="server">
                                <ContentTemplate>
                                    <asp:GridView ID="gridViewCuentas" CssClass="table able-responsive table-condensed" OnRowCommand="gridViewCuentas_Seleccion" OnPageIndexChanging="gridViewCuentas_CambioPagina" runat="server" AllowPaging="True" PageSize="15" BorderColor="Transparent">
                                        <Columns>
                                            <asp:ButtonField ButtonType="Button" ControlStyle-CssClass="btn-default" CommandName="Select" Text="Consultar">
                                                <ControlStyle CssClass="btn-default disabled"></ControlStyle>
                                            </asp:ButtonField>
                                        </Columns>
                                        <RowStyle Font-Size="small" BackColor="White" ForeColor="Black" />
                                        <PagerStyle CssClass="paging" HorizontalAlign="Center" />
                                        <AlternatingRowStyle BackColor="#EBEBEB" />
                                        <SelectedRowStyle CssClass="info" Font-Bold="true" ForeColor="White" />
                                        <HeaderStyle CssClass="active" Font-Size="Medium" Font-Bold="true" />
                                    </asp:GridView>
                                </ContentTemplate>
                                    <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="gridViewCuentas" EventName="RowCommand" />
                                </Triggers>
                            </asp:UpdatePanel>

                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" id="Button1" class="btn btn-danger" data-dismiss="modal" onserverclick="cancelarConsultar">Cancelar</button>
                    </div>
                </div>
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
                    <b>Módulo de cuentas</b><br>
                   <p style="font-size:14px">El módulo de cuentas es donde se realizan las actividades del manejo de cuentas.<br><br>
                    Si se es un cliente o miembro de equipo, es aquí donde se consultan y modifican los datos propios de la cuenta.<br><br>
                    Si se es administrador, además de lo antes mencionado, es acá donde se crean nuevas cuentas o se eliminan cuentas ya existentes.<br><br><br></p>
                     <p style="font-size:16px"><b>Para crear una cuenta</b><br></p>
                     <p style="font-size:14px"><b>+</b>Dele click al botón de Agregar en el panel de control.<br><br>
                         <b>+</b>Llene las casillas de información sobre la nueva cuenta.<br><br>
                         <b>+</b>Recuerde que los teléfonos se deben seguir un formato de ocho números, sin guiones.<br><br>
                         <b>+</b>Además, la cédula debe insertarse sin guiones ni espacios; nueve dígitos seguidos.<br><br>
                         Una vez que ya se ingresaron todos los valores, dele click al botón de Aceptar al pie de la página.<br><br />
                     </p>
                    <p style="font-size:16px"><b>Para consultar una cuenta</b><br></p>
                     <p style="font-size:14px"><b>+</b>Dele click al botón de Consultar en el panel de control.<br><br>
                         <b>+</b>Cuando se le despliegue el grid de cuentas, busque la cuenta que desea consultar.<br><br>
                         <b>+</b>Una vez que la encontró, dele click al botón de Consultar en la fila de la cuenta que quiere consultar.<br><br>
                         Una vez que le dió click, se le cargará en la interfaz los datos de la cuenta.<br><br />
                     </p>
                    <p style="font-size:16px"><b>Para modificar una cuenta</b><br></p>
                     <p style="font-size:14px"><b>+</b>Consulte la cuenta que desea modificar, siguiendo los pasos ya descritos.<br><br>
                         <b>+</b>Una vez consultada la cuenta, dele click al botón de Modificar en el panel de control.<br><br>
                         <b>+</b>Modifique las casillas de información que desee alterar.<br><br>
                         Una vez que modificados todos los valores, dele click al botón de Aceptar al pie de la página.<br><br />
                     </p>
                    <p style="font-size:16px"><b>Para eliminar una cuenta</b><br></p>
                     <p style="font-size:14px"><b>+</b>Consulte la cuenta que desea eliminar, siguiendo los pasos ya descritos.<br><br>
                         <b>+</b>Una vez consultada la cuenta, dele click al botón de Eliminar del panel de control.<br><br>
                         <b>+</b>En la ventana que le aparece, confirme que desea eliminar la cuenta, dando click en Aceptar.<br><br>
                         Al dar click, se eliminará la cuenta, y se le limpiarán los campos en pantalla.<br><br>
                     </p>
                </div>
                <div class="modal-footer">
                    <button type="button" id="Button2" class="btn btn-succes" runat="server" data-dismiss="modal">Aceptar</button>
                </div>
            </div>
        </div>
    </div>


    
    <!--Modal Eliminar-->
    <div class="modal fade" id="modalEliminar" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4 class="modal-title" id="myModalLabel"><i class="fa fa-exclamation-triangle text-danger fa-2x"></i>Confirmar eliminación</h4>
                </div>
                <div class="modal-body">
                    ¿Está seguro que desea eliminar la cuenta seleccionada?
                </div>
                <div class="modal-footer">
                    <button type="button" id="botonCancelarModal" class="btn btn-danger" data-dismiss="modal" onserverclick="cancelarConsultar">Cancelar</button>
                    <button type="button" id="botonAceptarModal" class="btn btn-succes" runat="server" onserverclick="clickAceptarEliminar" data-dismiss="modal">Aceptar</button>
                </div>
            </div>
        </div>
    </div>

            </div>


    </div>
</asp:Content>

