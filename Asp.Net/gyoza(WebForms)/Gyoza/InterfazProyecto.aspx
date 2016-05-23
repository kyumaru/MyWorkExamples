 <%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="True" CodeBehind="InterfazProyecto.aspx.cs" Inherits="Gyoza.InterfazProyecto" MaintainScrollPositionOnPostback="true" EnableEventValidation="false" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron" style="border: medium outset #3992D8">
        <h1 style="font-size: larger">Administración de Proyectos</h1>
        <div>

            <!-- Botones -->
            <div class="well bs-component" style="border: medium outset #0099FF">
                <fieldset style="text-align: center">
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
                                     <a ID="botonRequerimiento" runat="server" class="btn btn-primary" role="button" onserverclick="cambioARequerimiento">
                                        <i class="glyphicon glyphicon-list"></i><br />
                                        Requerimientos
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
                        <div id="Alerta" class="alert alert-danger fade in" runat="server" hidden="hidden">
                            <button type="button" class="close" data-dismiss="alert" aria-hidden="true"></button>
                            <strong>
                                <asp:Label ID="labelTipoAlerta" runat="server" Text="Alerta! "></asp:Label></strong><asp:Label ID="labelAlerta" runat="server" Text="Mensaje de alerta"></asp:Label>
                        </div>
                    </div>
                    </fieldset>
            </div>

            <!-- Nombre y Objetivo General -->
            <div class="well bs-component" style="background-color: #F5F5F5">
                <fieldset style="border: medium outset #0099FF">
                    <div class="col-sm-4">
                        <strong>Información general</strong><br />
                        <asp:Label ID="Label1" runat="server" Text="Nombre de proyecto"></asp:Label>
                        <asp:TextBox ID="textNombre" runat="server" pattern="^[A-Za-z0-9 \-\/\'\*\+]{3,25}$" TextMode="SingleLine" ></asp:TextBox>
                        <br />
                        <asp:Label ID="Label6" runat="server" Text="Estado del proyecto"></asp:Label>
                        <asp:DropDownList ID="textEstado" runat="server">
                            <asp:ListItem Enabled="true" selected="True" Text="Iniciado" Value="Iniciado" />
                            <asp:ListItem Enabled="true" selected="false" Text="Planteamiento" Value="Planteamiento" />
                            <asp:ListItem Enabled="true" selected="false" Text="En desarrollo" Value="En desarrollo" />
                            <asp:ListItem Enabled="true" selected="false" Text="Etapa Pruebas" Value="Etapa Pruebas" />
                            <asp:ListItem Enabled="true" selected="false" Text="Finalizado" Value="Finalizado" />
                        </asp:DropDownList>
                    </div>
                    <div class="col-sm-4">
                        <asp:Label ID="Label2" runat="server" Text="Objetivo general"></asp:Label>
                        <asp:TextBox ID="textObjetivo" runat="server" Rows="3" Columns="55" TextMode="MultiLine" ></asp:TextBox>
                    </div>

                    <div class="col-lg-12">
                            <div class="col-sm-6" style="vertical-align: middle; text-align: center">
                                <asp:Label ID="Label24" runat="server" Text="Iteración"></asp:Label>
                                <br />
                                <asp:DropDownList ID="ListaIteracion" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ListaIteracion_SelectedIndexChanged" />
                                <a id="botonAgregarIteracion" runat="server" onserverclick="clickAgregarIteracion"  AutoPostBack="True"   class="btn btn-primary" role="button">
                                    <i class="glyphicon glyphicon-plus"></i>
                                </a>
                                <a id="botonEliminarIteracion" runat="server" onserverclick="clickEliminarIteracion"  AutoPostBack="True"  class="btn btn-primary" role="button">
                                    <i class="glyphicon glyphicon-minus"></i>
                                </a>
                            </div>
                            <div class="col-sm-6" style="vertical-align: middle; text-align: center">
                                <asp:Label ID="Label25" runat="server" Text="Módulo"></asp:Label>
                                <br />
                                <asp:DropDownList ID="ListaModulo" AutoPostBack="true" OnSelectedIndexChanged="ListaModulo_SelectedIndexChanged"  runat="server" />
                                <a id="botonAgregarModulo" data-toggle="modal" href="#modalAgregarModulo" runat="server" class="btn btn-primary" role="button">
                                    <i class="glyphicon glyphicon-plus"></i>
                                </a>
                                <a id="botonEliminarModulo" AutoPostBack="true" runat="server" class="btn btn-primary" onserverclick="clickEliminarModulo"  role ="button">
                                    <i class="glyphicon glyphicon-minus"></i>
                                </a>
                            </div>
                        </div>
                </fieldset>
            </div>



            <!-- Calendarios para fechas de asignacion, inicio y fin -->
            <div class="well bs-component" style="border: medium outset #0099FF">
                <fieldset>
                    <legend style="font-weight: 700">Fechas del proyecto</legend>
                    <div class="col-sm-4">
                        <div class="form-group" style="background-color: #F5F5F5">
                            <label for="textCedula" class="col-sm-3 control-label"></label>
                            <div class="col-sm-9">
                                <asp:Label ID="Label13" runat="server" Text="Fecha de asignación"></asp:Label>
                                <br />
                                <asp:Button ID="calendario1" runat="server" OnClick="calendario1_Click" Text="Asignar" />
                                <asp:Calendar ID="fechaAsignacion" runat="server" BackColor="White" BorderColor="#999999" CellPadding="4" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" Height="180px" Width="200px" OnSelectionChanged="fechaAsignacion_SelectionChanged" Visible="False" >
                                    <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="7pt" />
                                    <NextPrevStyle VerticalAlign="Bottom" />
                                    <OtherMonthDayStyle ForeColor="#808080" />
                                    <SelectedDayStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
                                    <SelectorStyle BackColor="#CCCCCC" />
                                    <TitleStyle BackColor="#999999" BorderColor="Black" Font-Bold="True" />
                                    <TodayDayStyle BackColor="#CCCCCC" ForeColor="Black" />
                                    <WeekendDayStyle BackColor="#FFFFCC" />
                                </asp:Calendar>
                                <asp:Label ID="Label16" runat="server" style="text-align: center"></asp:Label>
                            </div>
                        </div>
                    </div>

                    <div class="col-sm-4">
                        <div class="form-group">
                            <label for="textEncargado" class="col-sm-3 control-label"></label>
                            <div class="col-sm-9">
                                <asp:Label ID="Label14" runat="server" Text="Fecha de inicio"></asp:Label>
                                <br />
                                <asp:Button ID="calendario2" runat="server" OnClick="calendario2_Click" Text="Asignar" />
                                <asp:Calendar ID="fechaInicio" runat="server" BackColor="White" BorderColor="#999999" CellPadding="4" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" Height="180px" Width="200px" OnSelectionChanged="fechaInicio_SelectionChanged" Visible="False">
                                    <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="7pt" />
                                    <NextPrevStyle VerticalAlign="Bottom" />
                                    <OtherMonthDayStyle ForeColor="#808080" />
                                    <SelectedDayStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
                                    <SelectorStyle BackColor="#CCCCCC" />
                                    <TitleStyle BackColor="#999999" BorderColor="Black" Font-Bold="True" />
                                    <TodayDayStyle BackColor="#CCCCCC" ForeColor="Black" />
                                    <WeekendDayStyle BackColor="#FFFFCC" />
                                </asp:Calendar>
                                <asp:Label ID="Label17" runat="server" style="text-align: center"></asp:Label>
                            </div>
                        </div>
                    </div>
                        
                    <div class="col-sm-4">
                        <div class="form-group">
                            <label for="textEmail" class="col-sm-3 control-label"></label>
                            <div class="col-sm-9">
                                <asp:Label ID="Label15" runat="server" Text="Fecha de finalización"></asp:Label>
                                <br />
                                <asp:Button ID="calendario3" runat="server" OnClick="calendario3_Click" Text="Asignar" />
                                <asp:Calendar ID="fechaFinalizacion" runat="server" BackColor="White" BorderColor="#999999" CellPadding="4" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" Height="180px" Width="200px"  OnSelectionChanged="fechaFinalizacion_SelectionChanged" Visible="False">
                                    <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="7pt" />
                                    <NextPrevStyle VerticalAlign="Bottom" />
                                    <OtherMonthDayStyle ForeColor="#808080" />
                                    <SelectedDayStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
                                    <SelectorStyle BackColor="#CCCCCC" />
                                    <TitleStyle BackColor="#999999" BorderColor="Black" Font-Bold="True" />
                                    <TodayDayStyle BackColor="#CCCCCC" ForeColor="Black" />
                                    <WeekendDayStyle BackColor="#FFFFCC" />
                                </asp:Calendar>
                                <asp:Label ID="Label18" runat="server" style="text-align: center"></asp:Label>
                            </div>
                        </div>
                    </div>
                </fieldset>
            </div>

            <!-- Informacion del Cliente -->
            <div class="well bs-component" style="border: medium outset #0099FF">
                <fieldset>
                    <legend style="font-weight: 700">Información de cliente</legend>
                    <div class="row">
                        <div class="col-sm-4">
                            <asp:Label ID="Label3" runat="server" Text="Oficina propietaria"></asp:Label>
                            <br />
                            <asp:TextBox ID="textOficina" runat="server" OnTextChanged="textOficina_TextChanged" pattern="^[A-Za-z0-9 \-\/\'\*\+\,\.\:\;\@\#\$\|\?\_]{3,25}$" TextMode="SingleLine"></asp:TextBox>
                            
                        </div>

                        <div class="col-sm-4">
                            <asp:Label ID="Label5" runat="server" Text="Teléfono de Oficina"></asp:Label>
                            <br />
                            <asp:TextBox ID="textTelefono" runat="server" pattern="^[0-9]{0,8}$" TextMode="SingleLine" placeholder ="999999999"></asp:TextBox >

                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-4">
                            <asp:Label ID="Label4" runat="server" Text="Representante del usuario"></asp:Label>
                            <br />  
                            <asp:TextBox ID="textRepresentante" runat="server" pattern="^[A-Za-z \-]{0,8}$" TextMode="SingleLine"></asp:TextBox>
                            <br />
                
                           
                        </div>
                        <div class="col-sm-4">
                            <asp:Label ID="Label20" runat="server" Text="Correo del representante"></asp:Label>
                            <br />
                            <asp:TextBox ID="textCorreo" runat="server" pattern="^([a-z]|[A-Z]|\.|[0-9])+\@[a-z](\.[a-b])+*$" TextMode="Email" maxlength="30" placeholder="admin@red.com"></asp:TextBox>
                            <br />
                            <br />
                             <asp:Label ID="Label19" runat="server" Text="Celular del Representante"></asp:Label>
                            <br />
                            <asp:TextBox ID="textCelular" runat="server" pattern="^[0-9]{0,8}$" TextMode="SingleLine" placeholder ="999999999"></asp:TextBox>
                        </div>
                    </div>
                </fieldset>
            </div>

                

            <!-- Miembros del Equipo -->
            <div class="well bs-component" style="border: medium outset #0099FF" id="divisionAsignacion">
                <fieldset>
                    <legend style="font-weight: 700">Miembros de equipo</legend>
                    <fieldset>
                        <div class="col-lg-10">
                            <div class="row row-div">
                    <div class="col-sm-6">
                        <asp:Label ID="miembrosNoAsociados" runat="server" Text="Miembros sin asociar"></asp:Label>
                        <asp:GridView ID="gridMiembrosNoAsociados" CssClass="table able-responsive table-condensed" OnRowCommand="gridMiembrosNoAsociados_Seleccion" OnPageIndexChanging="gridMiembrosNoAsociados_CambioPagina" OnRowDeleting="dummy" runat="server" AllowPaging="True" PageSize="8" BorderColor="Transparent">
                            <Columns>
                                <asp:ButtonField ButtonType="Button" ControlStyle-CssClass="btn-default" CommandName="X" Text=">">
                                    <ControlStyle CssClass="btn-default disabled"></ControlStyle>
                                </asp:ButtonField>
                            </Columns>
                            <RowStyle Font-Size="small" BackColor="White" ForeColor="Black" />
                            <PagerStyle CssClass="paging" HorizontalAlign="Center" />
                            <AlternatingRowStyle BackColor="#EBEBEB" />
                            <SelectedRowStyle CssClass="info" Font-Bold="true" ForeColor="White" />
                            <HeaderStyle CssClass="active" Font-Size="Medium" Font-Bold="true" />
                        </asp:GridView>
                    </div>
                    
                    <div class="col-sm-6">
                        <asp:Label ID="miembrosAsociados" runat="server" Text="Miembros asociados al equipo"></asp:Label>
                        <br />
                        <asp:GridView ID="gridMiembrosAsociados" CssClass="table able-responsive table-condensed" OnRowCommand="gridMiembrosAsociados_Seleccion" OnPageIndexChanging="gridMiembrosAsociados_CambioPagina" OnRowDeleting="dummy" runat="server" AllowPaging="True" PageSize="16" BorderColor="Transparent">
                            <Columns>
                                <asp:ButtonField ButtonType="Button" ControlStyle-CssClass="btn-default" CommandName="X" Text="<">
                                    <ControlStyle CssClass="btn-default disabled"></ControlStyle>
                                </asp:ButtonField>
                                <asp:TemplateField HeaderText="Lider">
                                    <ItemTemplate>
                                        <asp:RadioButton ID="RadioButton1" runat="server" GroupName="Foo" AutoPostBack="True" OnCheckedChanged="Seleccion_Radio" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <RowStyle Font-Size="small" BackColor="White" ForeColor="Black" />
                            <PagerStyle CssClass="paging" HorizontalAlign="Center" />
                            <AlternatingRowStyle BackColor="#EBEBEB" />
                            <SelectedRowStyle CssClass="info" Font-Bold="true" ForeColor="White" />
                            <HeaderStyle CssClass="active" Font-Size="Medium" Font-Bold="true" />
                        </asp:GridView>
                    </div>
                                </div>
                            </div>
                        </fieldset>
                </fieldset>
            </div>

            <!-- Botones de confirmacion -->
            <div class="row">
                <div class="col-lg-12">
                    <div class="text-center">
                        <button runat="server" onserverclick="clickAceptar" id="botonAceptar" class="btn btn-success" type="submit">Aceptar</button>
                        <button runat="server" onserverclick="clickCancelar" id="botonCancelar" class="btn btn-danger" type="reset">Cancelar</button>
                    </div>
                </div>
            </div>
        </div>
    </div>

                 <!--Modal Agregar Modulo-->
                <div class="modal fade" id="modalAgregarModulo" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                    <div class="modal-dialog modal-lg">
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                                <h4 class="modal-title" id="H111"><i class="fa fa-search"></i>Agregar Módulo</h4>
                            </div>
                            <div class="modal-body">
                                
                                
                                    <asp:TextBox ID="textAgregarModulo" runat="server" pattern="^[A-Za-z0-9 \-\/\'\*\+]{3,25}$" TextMode="SingleLine" Height="30px"></asp:TextBox>

                            </div>
                            <div class="modal-footer">
                                <button type="button" id="btnAgregarModulo" class="btn btn-succes"  runat="server"  data-dismiss="modal" onserverclick="clickAgregarModulo">Agregar</button>
                            </div>
                        </div>
                    </div>
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
                    <b>Módulo de proyectos</b><br>
                   <p style="font-size:14px">El módulo de proyectos es donde se realizan las actividades del manejo de proyectos, incluyendo creaciones, modificaciones, consultas, y eliminaciones.<br><br>
                    Si se es un cliente o miembro de equipo, es aquí donde se consultan y modifican los proyectos asociados a su cuenta.<br><br>
                    Además, se tiene un ligue a la página de requerimientos, para el manejo de los mismos. Para más información, referirse a la ayuda en línea disponible en esa página.
                    Si se es administrador, además de lo antes mencionado, es acá donde se maneja la creación y eliminación de proyectos.<br><br></p>
                     <p style="font-size:16px"><b>Para crear un proyecto</b><br></p>
                     <p style="font-size:14px"><b>+</b>Dele click al botón de Agregar en el panel de control.<br><br>
                         <b>+</b>Llene las casillas de información sobre el nuevo proyecto.<br><br>
                         <b>+</b>Para agregar las fechas de asignación, inicio y finalización, dele click al botón de Asignar, busque la fecha que desea seleccionar en el calendario que se le despliega, y dele click a dicha fecha.<br><br>
                         <b>+</b>Recuerde que los teléfonos se deben seguir un formato de ocho números, sin guiones.<br><br>
                         <b>+</b>A la hora de asociar miembros de equipo al proyecto, identifique el miembro de equipo que quiere asociar, y dele click al botón de ">" en la fila del miembro escogido. Al darle click, lo asociará; realice esto para todos los miembros que desee asociar.<br><br>
                         <b>+</b>De la misma manera, para desasociar un miembro de equipo, seleccionelo de la lista de miembros asociados, y dele click al botón de "<", desasociándolo así del proyecto.<br><br>
                         <b>+</b>Puede, además, marcar la casilla de líder para indicar cuál miembro de equipo será el lider del proyecto.<br><br>
                         Una vez que ya se ingresaron todos los valores, dele click al botón de Aceptar al pie de la página, y su proyecto será creado.<br><br />
                     </p>
                    <p style="font-size:16px"><b>Para consultar un proyecto</b><br></p>
                     <p style="font-size:14px"><b>+</b>Dele click al botón de Consultar en el panel de control.<br><br>
                         <b>+</b>Cuando se le despliegue el grid de proyectos, busque el proyecto que desea consultar.<br><br>
                         <b>+</b>Una vez que lo encontró, dele click al botón de Consultar en la fila del proyecto que quiere consultar.<br><br>
                         Una vez que le dió click, se le cargará en la interfaz los datos del proyecto.<br><br />
                     </p>
                    <p style="font-size:16px"><b>Para modificar un proyecto</b><br></p>
                     <p style="font-size:14px"><b>+</b>Consulte el proyecto que desea modificar, siguiendo los pasos ya descritos.<br><br>
                         <b>+</b>Una vez consultado el proyecto, dele click al botón de Modificar en el panel de control.<br><br>
                         <b>+</b>Modifique las casillas de información que desee alterar. asocie o desasocie, suba o baje archivos, o realice cualquier modificación que desee.<br><br>
                         Una vez que modificados todos los valores, dele click al botón de Aceptar al pie de la página.<br><br />
                     </p>
                    <p style="font-size:16px"><b>Para eliminar un proyecto</b><br></p>
                     <p style="font-size:14px"><b>+</b>Consulte el proyecto que desea eliminar, siguiendo los pasos ya descritos.<br><br>
                         <b>+</b>Una vez consultado, dele click al botón de Eliminar del panel de control.<br><br>
                         <b>+</b>En la ventana que le aparece, confirme que desea eliminar el proyecto, dando click en Aceptar.<br><br>
                         Al dar click, se eliminará el proyecto, y se le limpiarán los campos en pantalla.<br><br />
                     </p>

                    <p style="font-size:16px"><b>Requerimientos</b><br></p>
                     <p style="font-size:14px"><b>+</b>Consulte el proyecto cuyos requerimientos desea trabajar, siguiendo los pasos ya descritos para realizar una consulta.<br><br>
                         <b>+</b>Dele click al botón de Requerimientos del panel de control, lo cual le enviará a la página de administración de requerimientos.<br><br>
                         Para más información, refiérase a la ayuda en línea disponible en esa página.
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
                    ¿Eliminar el proyecto seleccionado?
                </div>
                <div class="modal-footer">
                    <button type="button" id="botonCancelarModal" class="btn btn-danger" data-dismiss="modal" onserverclick="cancelarConsultar">Cancelar</button>
                    <button type="button" id="botonAceptarModal" class="btn btn-succes" runat="server" onserverclick="clickAceptarEliminar" data-dismiss="modal">Aceptar</button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

