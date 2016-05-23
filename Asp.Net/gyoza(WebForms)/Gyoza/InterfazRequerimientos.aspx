<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="True" CodeBehind="InterfazRequerimientos.aspx.cs" Inherits="Gyoza.InterfazRequerimientos" MaintainScrollPositionOnPostback="true" EnableEventValidation="false" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron" style="border: medium outset #3992D8">
        <h1 style="font-size:large">Administración de Requerimientos</h1>
        <div>
            <!-- Botones -->
            <fieldset>
                <div class="well bs-component" style="border: medium outset #0099FF">
                    <fieldset style="text-align: center">
                        <legend style="font-weight: 700">Panel de control</legend>
                        <div class="col-lg-12">
                            <div class="row row-botones">
                                <div class="btn-group btn-group-justified">
                                    <div class="col-sm-2">
                                        <button id="botonInsertar" runat="server" onserverclick="clickInsertar" class="btn btn-primary" type="button">
                                            <i class="glyphicon glyphicon-plus"></i>
                                            <br />
                                            Agregar
                                        </button>
                                    </div>
                                    <div class="col-sm-2">
                                        <button id="botonModificar" runat="server" onserverclick="clickModificar" class="btn btn-primary" type="button">
                                            <i class="glyphicon glyphicon-pencil"></i>
                                            <br />
                                            Modificar
                                        </button>
                                    </div>
                                    <div class="col-sm-2">
                                        <a id="botonEliminar" href="#modalEliminar" data-toggle="modal" runat="server" class="btn btn-primary" type="button">
                                            <i class="glyphicon glyphicon-trash"></i>
                                            <br />
                                            Eliminar
                                        </a>
                                    </div>
                                    <div class="col-sm-2">
                                        <a id="botonConsultar" href="#modalConsultar" data-toggle="modal" runat="server" class="btn btn-primary" role="button">
                                            <i class="glyphicon glyphicon-list"></i>
                                            <br />
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
                            <div id="Alert" class="alert alert-danger fade in" runat="server" hidden="hidden">
                                <button type="button" class="close" data-dismiss="alert" aria-hidden="true"></button>
                                <strong>
                                    <asp:Label ID="LabelAlert" runat="server" Text="Alerta! "></asp:Label></strong><asp:Label ID="label10" runat="server" Text="Mensaje de alerta"></asp:Label>
                            </div>
                        </div>
                    </fieldset>
                        
                </div>
            </fieldset>


            <div class="well bs-component" style="border: medium outset #0099FF">
                <fieldset style="max-width: inherit; display: inherit">
                    <asp:Label ID="Label1" runat="server" Text="Proyecto"></asp:Label>
                    <a id="A1" href="#modalConsultarProyecto" data-toggle="modal" runat="server" class="btn btn-primary" role="button" style="margin-left: 10px">
                        <i class="glyphicon glyphicon-list"></i>
                        <br />
                        Consultar</a>
                </fieldset>
                <strong>Información general</strong><br />
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
                </fieldset>
            </div>
                
            <div class="well bs-component" style="border: medium outset #0099FF">
                <asp:Label ID="Label2" runat="server" Text="Datos del requerimiento" Font-Bold="True"></asp:Label>

                <br />

                <fieldset>
                    <div class="col-lg-10">
                        <div class="row row-div">
                            <div class="col-sm-5">
                                <fieldset>
                                    <asp:Label ID="Label3" runat="server" Text="Nombre"></asp:Label>
                                    <asp:TextBox ID="textNombreRequerimiento" runat="server" Height="30px" pattern="^[A-Za-z0-9 \-\/\'\*\+]{3,25}$"></asp:TextBox>
                                </fieldset>
                             </div>
                             <div class="col-sm-6">
                                <fieldset>
                                    <asp:Label ID="Label4" runat="server" Text="Prioridad"></asp:Label>
                                    <asp:TextBox ID="textPrioridad" runat="server" Height="30px" pattern ="[0-9]*" placeholder ="999"></asp:TextBox>
                                </fieldset>
                             </div>
                                <div class="col-sm-7">
                                <fieldset>
                                    <asp:Label ID="Label12" runat="server" Text="Estado"></asp:Label>
                                    <asp:DropDownList ID="textEstadoRequerimiento" runat="server">
                                        <asp:ListItem Enabled="true" Selected="True" Text="Iniciado" Value="Iniciado" />
                                        <asp:ListItem Enabled="true" Selected="false" Text="Planteamiento" Value="Planteamiento" />
                                        <asp:ListItem Enabled="true" Selected="false" Text="Terminado" Value="Terminado" />
                                    </asp:DropDownList>
                                </fieldset>
                                <br />
                            </div>
                        </div>
                    </div>
                </fieldset>

                <!-- Modulos y Sprints -->
               

                <fieldset>
                    <div class="col-lg-10" style="margin-top:20px">
                        <div class="row row-div">
                            <div class="col-sm-4" style="">
                                <fieldset>
                                <asp:Label ID="Label9" runat="server" Text="Rol"></asp:Label>
                                <asp:TextBox ID="textRol" runat="server" Height="30px" ></asp:TextBox>
                                </fieldset>
                            </div>
                                   
                            <div class="col-sm-4"style="">
                                <fieldset>
                                <asp:Label ID="Label19" runat="server" Text="Contenido"></asp:Label>
                                <asp:TextBox ID="textContenido" runat="server" Height="30px" pattern="[A-Za-z0-9 ]{3,60}" placeholder ="Texto"></asp:TextBox>
                                </fieldset>
                                </div>
                                
                                <div class="col-sm-4"style="">
                                <fieldset>
                                    
                                        <asp:Label ID="Label23" runat="server" Text="Razon"></asp:Label>
                                <asp:TextBox ID="textRazon" runat="server" Height="30px" pattern="[A-Za-z0-9 ]{3,60}"  placeholder ="Texto"></asp:TextBox>
                                </fieldset>
                            </div>
                            </div>
                    </div>
                <div class="col-lg-10" style="margin-top:20px">
                        <div class="row row-div">
                            <div class="col-sm-4" style="">
                                <fieldset>
                                <asp:Label ID="Label14" runat="server" Text="Estimacion" ></asp:Label>
                                <asp:TextBox ID="textEstimacion" runat="server" Height="30px" pattern ="[0-9]*"  placeholder ="999"></asp:TextBox>
                                </fieldset>
                            </div>
                        </div>
                    </div>
                    </fieldset>
                    <fieldset>
                <div class="col-lg-10" style="margin-top: 20px ">
                        <div class="row row-div">
                            <div class="col-sm-4" style="">
                                <fieldset>
                                <asp:Label ID="Label16" runat="server" Text="Primer Encargado"></asp:Label>
                                <asp:DropDownList ID="textPrimerEncargado" runat="server" OnSelectedIndexChanged="textPrimerEncargado_SelectedIndexChanged"  AutoPostBack="true"></asp:DropDownList>
                                </fieldset>
                            </div>
                                <div class="col-sm-4"style="">
                                    <fieldset>
                                <asp:Label ID="Label17" runat="server" Text="Segundo Encargado"></asp:Label>
                                <asp:DropDownList ID="textSegundoEncargado" runat="server" OnSelectedIndexChanged="textSegundoEncargado_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                </fieldset>
                            </div>
                    </div>
                    </div>
                    </fieldset>
                                <fieldset>

                        <div class="col-lg-10">
                        <div class="row row-div">
                            <div class="col-sm-4" style="">
                                <asp:RadioButtonList ID="radioButtonList1" runat="server" OnSelectedIndexChanged="radioButtonList1_SelectedIndexChanged">
                                    <asp:ListItem Text="Funcional"></asp:ListItem>
                                    <asp:ListItem Text="No Funcional"></asp:ListItem>
                                </asp:RadioButtonList>
                                </div>
                            </div>
                            </div>
                    </fieldset>
                 <fieldset>

                    </fieldset>
               
                 <fieldset>
                        <div class="col-sm-3" style="text-align">
                            <asp:Label ID="archivosLabel2" runat="server" Text="Archivos asociados" style="text-align:center"></asp:Label>
                            <asp:GridView ID="gridArchivos" CssClass="table able-responsive table-condensed" OnRowCommand="gridArchivos_Seleccion" OnPageIndexChanging="dummy" OnRowDeleting="dummy" runat="server" AllowPaging="false"  BorderColor="Transparent" OnSelectedIndexChanged="gridArchivos_SelectedIndexChanged">
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775"></AlternatingRowStyle>
                                <Columns>
                                    <asp:ButtonField ButtonType="Button" ControlStyle-CssClass="btn-default" CommandName="Descargar" Text="Descargar">
                                        <ControlStyle CssClass="btn-default disabled"></ControlStyle>
                                    </asp:ButtonField>
                                    <asp:ButtonField ButtonType="Button" ControlStyle-CssClass="btn-default" CommandName="Eliminar" Text="X">
                                        <ControlStyle CssClass="btn-default disabled"></ControlStyle>
                                    </asp:ButtonField>
                                </Columns>
                                <EditRowStyle BackColor="#999999"></EditRowStyle>

                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White"></FooterStyle>

                                <HeaderStyle BackColor="#5D7B9D" CssClass="active" Font-Bold="True" Font-Size="Medium" ForeColor="Blue"></HeaderStyle>

                                <PagerStyle HorizontalAlign="Center" BackColor="#284775" CssClass="paging" ForeColor="White"></PagerStyle>

                                <RowStyle Font-Size="small" BackColor="#F7F6F3" ForeColor="#333333" />
                                <PagerStyle CssClass="paging" HorizontalAlign="Center" />
                                <AlternatingRowStyle BackColor="#EBEBEB" />
                                <SelectedRowStyle CssClass="info" Font-Bold="true" ForeColor="#333333" BackColor="#E2DED6" />
                                <SortedAscendingCellStyle BackColor="#E9E7E2"></SortedAscendingCellStyle>

                                <SortedAscendingHeaderStyle BackColor="#506C8C"></SortedAscendingHeaderStyle>

                                <SortedDescendingCellStyle BackColor="#FFFDF8"></SortedDescendingCellStyle>

                                <SortedDescendingHeaderStyle BackColor="#6F8DAE"></SortedDescendingHeaderStyle>
                                <HeaderStyle CssClass="active" Font-Size="Medium" Font-Bold="true" />
                            </asp:GridView>
                        </div>

                        
                    </fieldset>
                <fieldset style="text-align:center">
                            <button runat="server" id="botonAgregarArchivo" class="btn btn-success" onserverclick="clickBotonSubir" type="submit"><i class="glyphicon glyphicon-arrow-up"></i></button>
                        </fieldset>
            </div>

            <!-- Grid archivos -->
            <div class="well bs-component" style="border: medium outset #0099FF">
                                    <div class="col-lg-12" style="margin-top:20px">
                        <div class="row row-div">
     
                                <fieldset>
                                <asp:Label ID="Label21" runat="server" Text="Escenario"></asp:Label>
                                <asp:TextBox ID="textEscenario" runat="server" pattern ="[0-9]+"></asp:TextBox>
                                <asp:Label ID="Label22" runat="server" Text="Criterio de aceptacion"></asp:Label>
                                <asp:TextBox ID="textCriterioAceptacion" runat="server" pattern="[A-Za-z0-9 ]{3,60}"></asp:TextBox>
                                <button runat="server" id="botonCriterio" class="btn btn-success" onserverclick="clickAceptarCriterio" type="submit"><i class="glyphicon glyphicon-ok"></i></button>
                                </fieldset>

                            </div>
                    </div>
                   <fieldset style ="text-align:center">
                            <asp:Label ID="Label20" runat="server" Text="Criterios de Aceptacion"></asp:Label>
                            <asp:GridView ID="gridCriteriosAceptacion" CssClass="table able-responsive table-condensed" OnRowCommand="gridCriteriosAceptacion_Seleccion" OnPageIndexChanging="gridCriteriosAceptacion_CambioPagina" OnRowDeleting="dummy" runat="server" AllowPaging="True" PageSize="15" BorderColor="Transparent">
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775"></AlternatingRowStyle>
                                <Columns>
                                    <asp:ButtonField ButtonType="Button" ControlStyle-CssClass="btn-default" CommandName="delete" Text="-">
                                        <ControlStyle CssClass="btn-default disabled"></ControlStyle>
                                    </asp:ButtonField>
                                </Columns>
                                <EditRowStyle BackColor="#999999"></EditRowStyle>

                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White"></FooterStyle>

                                <HeaderStyle BackColor="#5D7B9D" CssClass="active" Font-Bold="True" Font-Size="Medium" ForeColor="Black"></HeaderStyle>

                                <PagerStyle HorizontalAlign="Center" BackColor="#284775" CssClass="paging" ForeColor="White"></PagerStyle>

                                <RowStyle Font-Size="small" BackColor="#F7F6F3" ForeColor="#333333" />
                                <PagerStyle CssClass="paging" HorizontalAlign="Center" />
                                <AlternatingRowStyle BackColor="#EBEBEB" />
                                <SelectedRowStyle CssClass="info" Font-Bold="true" ForeColor="#333333" BackColor="#E2DED6" />
                                <SortedAscendingCellStyle BackColor="#E9E7E2"></SortedAscendingCellStyle>

                                <SortedAscendingHeaderStyle BackColor="#506C8C"></SortedAscendingHeaderStyle>

                                <SortedDescendingCellStyle BackColor="#FFFDF8"></SortedDescendingCellStyle>

                                <SortedDescendingHeaderStyle BackColor="#6F8DAE"></SortedDescendingHeaderStyle>
                                <HeaderStyle CssClass="active" Font-Size="Medium" Font-Bold="true" />
                            </asp:GridView>
                           
                    </fieldset>
                </div>
                    
                <!-- Botones de confirmacion -->
                <div class="col-lg-12">
                    <div class="text-center">
                        <button runat="server" id="botonAceptar" class="btn btn-success" onserverclick="clickAceptar" type="submit">Aceptar</button>
                        <button runat="server" id="botonCancelar" class="btn btn-danger" onserverclick="clickCancelar" type="reset">Cancelar</button>
                    </div>
                </div>

                <!--Modal Consultar Requerimientos-->
                <div class="modal fade" id="modalConsultar" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                    <div class="modal-dialog modal-lg">
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                                <h4 class="modal-title" id="H1"><i class="fa fa-search"></i>Consultar Requerimiento</h4>
                            </div>
                            <div class="modal-body">
                                <div class="col-lg-12">
                                    <asp:UpdatePanel ID="UpdatePanelPruebas" runat="server">
                                        <ContentTemplate>
                                            <asp:GridView ID="gridViewRequerimientos" CssClass="table able-responsive table-condensed" OnRowCommand="gridViewRequerimientos_Seleccion" OnPageIndexChanging="gridViewRequerimientos_CambioPagina" runat="server" AllowPaging="True" PageSize="15" BorderColor="Transparent">
                                                <Columns>
                                                    <asp:ButtonField ButtonType="Button" ControlStyle-CssClass="btn-default" CommandName="Select" Text="Consultar">
                                                        <ControlStyle CssClass="btn-default disabled"></ControlStyle>
                                                    </asp:ButtonField>
                                                </Columns>
                                                <RowStyle Font-Size="small" BackColor="White" ForeColor="Black" />
                                                <PagerStyle CssClass="paging" HorizontalAlign="Center" />
                                                <AlternatingRowStyle BackColor="#EBEBEB" />
                                                <SelectedRowStyle CssClass="info" Font-Bold="true" ForeColor="White" />
                                                <HeaderStyle CssClass="active" Font-Size="Medium" Font-Bold="true" Forecolor="Black"/>
                                            </asp:GridView>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="gridViewRequerimientos" EventName="RowCommand" />
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


                <!--Modal Consultar Proyecto-->
                <div class="modal fade" id="modalConsultarProyecto" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                    <div class="modal-dialog modal-lg">
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                                <h4 class="modal-title" id="H1"><i class="fa fa-search"></i>Consultar Proyecto</h4>
                            </div>
                            <div class="modal-body">
                                <div class="col-lg-12">
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <asp:GridView ID="gridViewProyectos" CssClass="table able-responsive table-condensed" OnRowCommand="gridViewProyectos_Seleccion" OnPageIndexChanging="gridViewProyectos_CambioPagina" runat="server" AllowPaging="True" PageSize="15" BorderColor="Transparent">
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
                                            <asp:AsyncPostBackTrigger ControlID="gridViewRequerimientos" EventName="RowCommand" />
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

            </div>
        </div>
</asp:Content>

