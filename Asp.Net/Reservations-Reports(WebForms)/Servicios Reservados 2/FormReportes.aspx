<%@ Page Title="Reportes" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FormReportes.aspx.cs" Inherits="Servicios_Reservados_2.FormReportes" %>



<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

<link rel="stylesheet" href="http://code.jquery.com/ui/1.10.3/themes/smoothness/jquery-ui.css">
<script type="text/javascript" src="http://code.jquery.com/jquery-1.9.1.js"></script>
<script type="text/javascript" src="http://code.jquery.com/ui/1.10.3/jquery-ui.js"></script>

    <nav>
        <ul>
            <li class="item-navegacion"><a href="Default.aspx" title="Página principal"><i class="glyphicon glyphicon-home"></i></a></li>
            <li class="item-navegacion"><a href="FormReservaciones.aspx" title="Reservaciones" class="seleccionado">Reservaciones</a></li>
            <li class="item-navegacion"><a href="FormEmpleado.aspx" title="Empleados">Empleados</a></li>
            <li class="item-navegacion">Notificaciones <span class="notificacion">0</span></li>
            <li class="item-navegacion"><a href="FormReportesComedor.aspx" title="Reportes">Reportes</a></li>
        </ul>
    </nav>
    <legend> <h2>Reportes</h2></legend>


    <div id="jcDiv0" class="well bs-component">

         <asp:UpdatePanel ID="UpdatePanelyyy" runat="server">
        <ContentTemplate> 
            <legend style="color: #7BC143">Criterios del reporte</legend>
           <asp:DropDownList ID="cbxFecha" CssClass="jcInlineElement" runat="server" AutoPostBack="true" OnSelectedIndexChanged="mostrarFechas"></asp:DropDownList>
            <p id="delLabel" class="jcInlineElement" runat="server">Fecha Inicio : <input type="text" runat="server" id="dateFechaInicio"  required class="datepicker" placeholder='dd/mm/aaaa'  ></p>
            <p id="alLabel" class="jcInlineElement" runat="server">&nbsp;&nbsp;Fecha Final : <input type="text" id="dateFechaFin"  required class="datepicker2" runat="server" placeholder='dd/mm/aaaa' ></p>             
            <asp:Button Text="Generar Reporte" class="btn btn-success jcInlineElement"  ID="BotonGenerar" runat="server" OnClick="BotonGenerar_Click" />
        </ContentTemplate> 
        </asp:UpdatePanel>
    </div>

   

    <div id="anotherTablecontainerDiv" class="well bs-component" runat="server">
        <legend style="color: #7BC143">Desglose</legend>
        <asp:UpdatePanel ID="UpdatePanel3" runat="server">       
            <ContentTemplate>
             <fieldset>
                <asp:GridView ID="GridViewReportes"  runat="server" AllowPaging="true" AllowSorting="false" PageSize="20" OnPageIndexChanging="GridViewReportes_PageIndexChanging">
                                            <AlternatingRowStyle BorderStyle="None" />
                                            <HeaderStyle Font-Size="1.2em" />
                                            <SelectedRowStyle BackColor="#7BC143"
                                            ForeColor="Black"
                                            Font-Bold="true" BorderStyle="Dotted" BorderWidth="1px" />
                                            <Columns>
                                               <asp:TemplateField>
                                                  <ItemTemplate>
                                                    <asp:LinkButton ID="btnConsultar" ToolTip="Consultar" OnClick="clickVerDetalle" runat="server" class="btn btn-default"><i class="glyphicon glyphicon-search"></i></asp:LinkButton>
                                                  </ItemTemplate>
                                               </asp:TemplateField>
                                           </Columns>

                </asp:GridView>
              </fieldset>
             </ContentTemplate>
        </asp:UpdatePanel>
    </div>
 
    <div class="well bs-component" >
        <asp:UpdatePanel ID="UpdatePanelxxx" runat="server">
        <ContentTemplate>    
           <asp:Button id="btnDesayunar" class=" btn btn-success jcInlineElement" Enabled="false" onclick="clickDesayunoFiltro" text="Desayuno" runat="server" />
           <asp:Button id="btnAlmuerzo" class=" btn btn-success jcInlineElement"  Enabled="false" onclick="clickAlmuerzoFiltro"  text="Almuerzo" runat="server" />
           <asp:Button  id="btnCena" class=" btn btn-success jcInlineElement" Enabled="false" onclick="clickCenaFiltro"  text="Cena" runat="server" />
        </ContentTemplate> 
        </asp:UpdatePanel>
    </div>


     <div id="reportContainerDiv" class="well bs-component">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">       
            <ContentTemplate>
             <fieldset>
                <asp:GridView ID="GridViewDetalles" OnPageIndexChanging="GridViewDetalles_PageIndexChanging" Class="Gridcontenedor" runat="server" AllowPaging="true" AllowSorting="false" PageSize="20">
                                            <AlternatingRowStyle BorderStyle="None" />
                                            <HeaderStyle Font-Size="1.1em" />
                                            <SelectedRowStyle BackColor="#7BC143"
                                            ForeColor="Black"
                                            Font-Bold="true" BorderStyle="Dotted" BorderWidth="1px" />
                </asp:GridView>
              </fieldset>
             </ContentTemplate>
        </asp:UpdatePanel>
    </div>

</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

    <style type="text/css">

  #jcDiv0{    
        margin:auto;
        width:auto;
        height:200px;
        font-size: larger;
     }

    .jcInlineElement {
        display:inline-block;
        margin-right:50px;
    }

    .ui-datepicker-trigger{
        margin-left:10px;
    }

 
    </style>


<script type="text/javascript">

    function Func() {
        document.getElementById(datepicker).value = DateTime.Now.ToString("mm/dd/yyyy");
    }

</script>



<script type="text/javascript">
    $(function () {
        $(".datepicker").datepicker({
            showOn: 'button', buttonImage: '../Images/imagenes_OET/calendarjc.png', buttonImageOnly: true,
            changeMonth: true,
            changeYear: true,
        });


        $(".datepicker2").datepicker({
            showOn: 'button', buttonImage: '../Images/imagenes_OET/calendarjc.png', buttonImageOnly: true,
            changeMonth: true,
            changeYear: true
        });

     });
</script>


<script>
    $.datepicker.regional['es'] = {
        closeText: 'Cerrar',
        prevText: '<Ant',
        nextText: 'Sig>',
        currentText: 'Hoy',
        monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
        monthNamesShort: ['Ene', 'Feb', 'Mar', 'Abr', 'May', 'Jun', 'Jul', 'Ago', 'Sep', 'Oct', 'Nov', 'Dic'],
        dayNames: ['Domingo', 'Lunes', 'Martes', 'Miércoles', 'Jueves', 'Viernes', 'Sábado'],
        dayNamesShort: ['Dom', 'Lun', 'Mar', 'Mié', 'Juv', 'Vie', 'Sáb'],
        dayNamesMin: ['Do', 'Lu', 'Ma', 'Mi', 'Ju', 'Vi', 'Sá'],
        weekHeader: 'Sm',
        dateFormat: 'dd/mm/yy',
        firstDay: 1,
        isRTL: false,
        showMonthAfterYear: false,
        yearSuffix: ''
    };
    $.datepicker.setDefaults($.datepicker.regional['es']);
    $(function () {
        $("#fecha").datepicker();
    });
</script>





</asp:Content>
