<%@ Page  Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="True" CodeBehind="InterfazConsultar.aspx.cs" Inherits="Gyoza.InterfazConsultar" %>




<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron" style="border: medium outset #3992D8; background-color: #F5F5F5">
        <h1 style="font-size: larger">Administración de consultas</h1>
        <a ID="botonConsultar" runat="server" class="btn btn-primary" role="button" data-toggle="modal" href="#modalConsultar">
                                        <i class="glyphicon glyphicon-list"></i><br />
                                        Consultar
                                    </a>

         <div class="col-sm-3">
                                    <a ID="botonAyuda" href="#modalAyuda" data-toggle="modal" runat="server" class="btn btn-primary" role="button">
                                        <i class="glyphicon glyphicon-list"></i><br />
                                        Ayuda en línea
                                    </a>
                                </div>

        <fieldset>

                <fieldset>
                                    <asp:Label ID="Label5" runat="server" Text="Nombre de proyecto"></asp:Label>
                                    <asp:TextBox ID="textNombre" runat="server" pattern="^[A-Za-z0-9 \-\/\'\*\+]{3,25}$" TextMode="SingleLine" Height="30px"></asp:TextBox>
                                    <asp:Label ID="Label7" runat="server" Text="Estado"></asp:Label>
                                    <asp:TextBox ID="textEstado" runat="server" Height="30px"></asp:TextBox>
                    </fieldset>      
                       
                                <fieldset>
                                    <asp:Label ID="Label6" runat="server" Text="Descripcion"></asp:Label>
                                    <asp:TextBox ID="textDescripcionProyecto" runat="server" Rows="3" Columns="75" TextMode="MultiLine" Width="660px" Height="82px"></asp:TextBox>
                                </fieldset>
                                                            <fieldset>
                 
                </fieldset>
                </fieldset>

                <asp:CheckBoxList ID="permisosProyecto" runat="server"  AutoPostBack="true"  >
                    <asp:ListItem>Datos del Proyecto</asp:ListItem>
                    <asp:ListItem>Historias de Usuario</asp:ListItem>
                    <asp:ListItem>Jerarquía de iteraciones y módulos</asp:ListItem>
                    <asp:ListItem  >Requerimiento Particular</asp:ListItem>

                </asp:CheckBoxList>


        <div class="col-lg-12" runat="server" visible="false" id="comboboxes">
                        <div class="col-sm-6" style="vertical-align: middle; text-align: center">
                            <asp:Label ID="Label24" runat="server" Text="Iteración"></asp:Label>
                            <br />
                            <asp:DropDownList ID="ListaIteracion" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ListaIteracion_SelectedIndexChanged" />
                           
                        </div>
                        <div class="col-sm-6" style="vertical-align: middle; text-align: center">
                            <asp:Label ID="Label25" runat="server" Text="Módulo"></asp:Label>
                            <br />
                            <asp:DropDownList ID="ListaModulo" AutoPostBack="true" OnSelectedIndexChanged="ListaModulo_SelectedIndexChanged"  runat="server" />
                           
                        </div>
            
                        <div class="col-sm-6" style="vertical-align: middle; text-align: center">
                            <asp:Label ID="LabelX" runat="server" Text="Requerimiento"></asp:Label>
                            <br />
                            <asp:DropDownList ID="ListaRequerimientos" AutoPostBack="true" OnSelectedIndexChanged="ListaRequerimientos_SelectedIndexChanged"  runat="server" />
                           
                        </div>
                    </div>
        <fieldset>

                       <div class="col-lg-5">
                        <div id="Alerta" class="alert alert-danger fade in" runat="server" hidden="hidden">
                            <button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
                            <strong>
                                <asp:Label ID="labelTipoAlerta" runat="server" Text="Alerta! "></asp:Label></strong><asp:Label ID="labelAlerta" runat="server" Text="Mensaje de alerta"></asp:Label>
                        </div>
                    </div>
            </fieldset>

        <fieldset style="text-align:center">
        <a ID="BotonGenerarInforme" runat="server" class="btn btn-primary" role="button" AutoPostBack="True"  onserverclick="clickGenerarInforme">Generar Informe</a> 
       <a ID="BotonDescargarArchivo" runat="server" class="btn btn-primary" role="button" AutoPostBack="true" onserverclick="clickBotonBajar" style="margin-left:20px">Descargar informe</a>
         </fieldset>
            <asp:TextBox ID="textObjetivo" runat="server" Rows="20" Columns="100" TextMode="MultiLine"  style="margin-top:20px;" ></asp:TextBox> 
        <fieldset style="text-align:center">
        <a ID="botonDescargarAbajo" runat="server" class="btn btn-primary" AutoPostBack="true" role="button" onserverclick="clickBotonBajar">Descargar informe</a> 
         </fieldset>
    </div>
    
    <!-- Modal de Consultar-->
    <div class="modal fade" id="modalConsultar" tabindex="-1" role="dialog" aria-labelledby="customLabel" aria-hidden="true" >
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4 class="modal-title" id="H1"><i class="fa fa-search"></i>Consultar Proyectos</h4>
                </div>
                <div class="modal-body">
                    <div class="col-lg-12">
                        <asp:UpdatePanel ID="UpdatePanelPruebas" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="gridViewProyecto" CssClass="table able-responsive table-condensed" OnRowCommand="gridViewProyecto_Seleccion" OnPageIndexChanging="gridViewProyecto_CambioPagina" runat="server" AllowPaging="True" PageSize="16" BorderColor="Transparent">
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
                                <asp:AsyncPostBackTrigger ControlID="gridViewProyecto" EventName="RowCommand" />
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
                    <b>Módulo de consultas</b><br>
                   <p style="font-size:14px">Es aquí donde se generan algunos reportes particulares de los proyectos.<br><br>
                       Si se es cliente o miembro de equipo, se podrán generar reportes mediante consultas de los proyectos asociados a su cuenta.<br><br>
                       Si se es administrador, se podrán generar reportes de cualquier proyecto en el sistema.<br><br><br></p>
                     
                    <p style="font-size:16px"><b>Para generar un reporte </b><br></p>
                     <p style="font-size:14px"><b>+</b>Consulte el proyecto sobre el cual quiere generar un reporte. La consulta se hace de igual manera a como se ha hecho en otras páginas.<br><br>
                         <b>+</b>Marque las casillas de las consultas que quiere en el reporte.<br><br>
                         <b>+</b>Es posible consultar los detalles de un requerimiento particular. Al seleccionar esta casilla, se desplegarán menús para que indique cuál es el requerimiento que desea consultar.
                          La búsqueda del requerimiento se hace mediante un filtro de iteración, moódulo, y nombre del requerimiento<br><br>
                         <b>+</b>Una vez seleccionadas las consultas, dele click al botón de Generar Informe para ver en pantalla los resultados de su consulta.<br><br>
                         <b>+</b>Adicionalmente, es posible exportar su reporte en un archivo PDF. Si le da click al botón Descargar Informe, le aparecerá una ventana para que indique donde quiere guardar el archivo. 
                         Le solicitará un nombre, y al darle Aceptar, su archivo se generará y se guardará en su computadora.<br><br>
                         Una vez que le dió click al rol, se le cargará en la interfaz los permisos de ese rol.<br><br />
                     </p>
                   
                </div>
                <div class="modal-footer">
                    <button type="button" id="Button2" class="btn btn-succes" runat="server" data-dismiss="modal">Aceptar</button>
                </div>
            </div>
        </div>
    </div>



</asp:Content>

