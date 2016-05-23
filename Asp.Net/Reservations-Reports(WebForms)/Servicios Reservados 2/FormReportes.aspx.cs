using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Servicios_Reservados_2
{
    public partial class FormReportes : System.Web.UI.Page
    {
        private static ControladoraReportes controladora = new ControladoraReportes();
        private String estacion;
        private int anfitriona;
        private String fechaSeleccionda;
        private String fechaInicio;
        private String fechaFinal;
        private int sumaTotalDesayuno;
        private int sumaTotalConsumidosDesayuno;
        private int sumaTotalAlmuerzo;
        private int sumaTotalConsumidosAlmuerzo;
        private int contar;
        private DataTable fechas;

        /**MY VARIABLES
         **/
        string estacionJC = "LS";
        static DataTable dt = new DataTable();//dt tabla intermedia para hacer calculos de counts
        static DataTable dt2 = new DataTable();//dt2 tabla para desplegar al final con todos los conteos
        static DataTable dt3 = new DataTable();//dt3 tabla para el detalle de un dia en dt2
        static int countDesayuno = 0;
        static int countDesayunoServ = 0;
        static int countAlmuerzo = 0;
        static int countAlmuerzoServ = 0;
        static int countCena = 0;
        static int countCenaServ = 0;
        static int countSnack = 0;
        static int countSnackServ = 0;
        static int countComidaCampo = 0;
        static int countComidaCampoServ = 0;
        /*TOTALES*/
        static int totalDesayuno = 0;
        static int totalDesayunoServ = 0;
        static int totalAlmuerzo = 0;
        static int totalAlmuerzoServ = 0;
        static int totalCena = 0;
        static int totalCenaServ = 0;
        static int totalSnack = 0;
        static int totalSnackServ = 0;
        static int totalComidaCampo = 0;
        static int totalComidaCampoServ = 0;
        static int totalTotal = 0;
        static int totalServidos = 0;
        List<DataRow> rowsToDelete = new List<DataRow>();//used to store refs to dt rows that shoud not be reported

        //string estacion = "LS";//PV,LC,NAO,CRO
        static ControladoraBDreportes controladoraJC = new ControladoraBDreportes();

     
  

        protected void Page_Load(object sender, EventArgs e)
        {
            
            estacion = (string)Session["Estacion"];
            ArrayList listaRoles = (ArrayList)Session["Roles"];
            string userid = (string)Session["username"];
            if (!IsPostBack)
            {
            
                if (userid == "" || userid == null)
                {
                    Response.Redirect("~/Ingresar.aspx");
                } if (!listaRoles.Contains("administrador local") && !listaRoles.Contains("recepcion") && !listaRoles.Contains("administrador global") && !listaRoles.Contains("administrador sistema"))
                {
                    Response.Redirect("ErrorPermiso.aspx");
                }
                cargarDatos();
            }
             
          
        }

        //check if a date is accepted by regex-- is valid
        public static bool checkDateAcceptXregex(String pattern)
        {
            return (Regex.IsMatch(pattern, @"^(?:(?:(?:0?[13578]|1[02])(\/|-|\.)31)\1|(?:(?:0?[1,3-9]|1[0-2])(\/|-|\.)(?:29|30)\2))(?:(?:1[6-9]|[2-9]\d)?\d{2})$|^(?:0?2(\/|-|\.)29\3(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00))))$|^(?:(?:0?[1-9])|(?:1[0-2]))(\/|-|\.)(?:0?[1-9]|1\d|2[0-8])\4(?:(?:1[6-9]|[2-9]\d)?\d{2})$"));
        }

        protected void clickDesayunoFiltro(object sender, EventArgs e)
        {
            this.filtrarXcomida(ref dt3, "_Desayuno");

        }

        protected void clickAlmuerzoFiltro(object sender, EventArgs e)
        {
            this.filtrarXcomida(ref dt3, "_Almuerzo");

        }
        protected void clickCenaFiltro(object sender, EventArgs e)
        {
            this.filtrarXcomida(ref dt3, "_Cena");
        }

        protected void filtrarXcomida(ref DataTable dt, string s)
        {

            DataTable dtCopy = dt.Copy();


            for (int i = 0; i < dtCopy.Rows.Count; i++)
            {
                // your row index is in i
                var row = dtCopy.Rows[i];


                if (!(row.ItemArray[1].ToString().IndexOf(s, StringComparison.OrdinalIgnoreCase) >= 0))
                    row.Delete();
                else
                    row.SetField(1, s);

            }

            this.GridViewDetalles.DataSource = dtCopy;
            this.GridViewDetalles.DataBind();//show data on gridview  
            this.GridViewDetalles.Visible = true;
        }


        protected void clickVerDetalle(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            int i = Convert.ToInt32(row.RowIndex);
            
            generarDetalle(i);
            //Response.Redirect("FormServicios");

        }


        protected void generarDetalle(int index) {

            if (index < dt2.Rows.Count - 1) {//check it is not last row TOTALES

                this.rowsToDelete.Clear();
                dt.Clear();
                //Response.Write("Hello");
                //DateTime fechTemp = DateTime.Parse(dt2.Rows[index].ItemArray[0].ToString());
                var fecha = dt2.Rows[index].ItemArray[0].ToString();

                //set OET tables detalle
                dt = controladoraJC.testDetalleOET(fecha, estacionJC);

                DateTime fechTemp = DateTime.Parse(fecha);
                var fechaPatched = fechTemp.ToString("d/M/yyyy");

                dt.Columns[6].MaxLength = 255;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    this.setDetalleOET(ref dt, dt.Rows[i], DateTime.ParseExact(fechaPatched, "d/M/yyyy", CultureInfo.InvariantCulture));//setCounts sets comidas column while counting

                }
                this.deleteRowsDiaSale(ref dt, rowsToDelete);//clean dt 

                //Note the analogy between DataTable and db tables, they are constructed similar same way

                //remove all unnecesary columns

                dt.Columns.Remove("ENTRA");
                dt.Columns.Remove("SALE");
                dt.Columns.Remove("PRIMERA_COMIDA");
                dt.Columns.Remove("CANTIDAD");
                dt.Columns.Remove("TIPO_COMIDA");

                dt.Columns["PAX"].ColumnName = "COMENSALES";
                dt.Columns["NOMBRECLIENTE"].ColumnName = "CLIENTE";
                dt.Columns["RESERVACION"].ColumnName = "REFERENCIA";

                //make a copy to dt3, removing all constrains, a dt has same constrains as a dt of a query
                dt3 = dt.Rows.Cast<DataRow>().CopyToDataTable();

                //add servicio extra rows to dt3
                dt = controladoraJC.testDetalleServiciosExtra(fechaPatched, estacionJC);

                if (dt.Rows.Count > 0)
                {//prevent an exeption on merging  an empty dt 

                    DataTable dtcopy = dt.Rows.Cast<DataRow>().CopyToDataTable();
                    dt3.Merge(dtcopy);
                }

                //add comida empleado
                dt = controladoraJC.testDetalleEmpleados(fechaPatched);

                if (dt.Rows.Count > 0)
                {//prevent an exeption on merging  an empty dt 

                    DataTable dtcopy = dt.Rows.Cast<DataRow>().CopyToDataTable();
                    dt3.Merge(dtcopy);
                }


                //show stuff
                GridViewDetalles.DataSource = dt3;
                GridViewDetalles.DataBind();

                //enable filters
                this.btnDesayunar.Enabled = true;
                this.btnAlmuerzo.Enabled = true;
                this.btnCena.Enabled = true;


                this.GridViewDetalles.Visible = true;

                
            
            }
         

        }



        //sets comidas column for each row in dt
        protected void setComidas(ref string mode, ref DataRow row)
        {

            if (mode.IndexOf("d", StringComparison.OrdinalIgnoreCase) >= 0)
            {

                if (!(row.ItemArray[6].ToString().IndexOf("Desayuno", StringComparison.OrdinalIgnoreCase) >= 0))
                {
                    string s = row.ItemArray[6].ToString();
                    s += "_Desayuno";
                    row.SetField(6, s);
                }

            }

            if (mode.IndexOf("a", StringComparison.OrdinalIgnoreCase) >= 0)
            {

                if (!(row.ItemArray[6].ToString().IndexOf("Almuerzo", StringComparison.OrdinalIgnoreCase) >= 0))
                {
                    string s = row.ItemArray[6].ToString();
                    s += "_Almuerzo";
                    row.SetField(6, s);
                }

            }

            if (mode.IndexOf("c", StringComparison.OrdinalIgnoreCase) >= 0)
            {

                if (!(row.ItemArray[6].ToString().IndexOf("Cena", StringComparison.OrdinalIgnoreCase) >= 0))
                {
                    string s = row.ItemArray[6].ToString();
                    s += "_Cena";
                    row.SetField(6, s);
                }

            }

            if (mode.IndexOf("s", StringComparison.OrdinalIgnoreCase) >= 0)
            {

                if (!(row.ItemArray[6].ToString().IndexOf("Snack", StringComparison.OrdinalIgnoreCase) >= 0))
                {
                    string s = row.ItemArray[6].ToString();
                    s += "_Snack";
                    row.SetField(6, s);
                }

            }

        }

        //used to clean dt of invlid rows due to sale date
        protected void deleteRowsDiaSale(ref DataTable dt, List<DataRow> rowsToDelete)
        {
            foreach (var r in rowsToDelete)
                dt.Rows.Remove(r);
        }



        //sets OET detalle rows
        public void setDetalleOET(ref DataTable dt, DataRow row, DateTime fecha)
        {

            //row.ItemArray[0] is the first element of this row;
            /*
             *itemArray[index] corresponds
            /*
+		[0]	{ENTRA}	object {System.Data.DataColumn}
+		[1]	{SALE}	object {System.Data.DataColumn}
+		[2]	{PRIMERA_COMIDA}	object {System.Data.DataColumn}
+		[3]	{PAX}	object {System.Data.DataColumn}
+		[4]	{CANTIDAD}	object {System.Data.DataColumn}//1-3 son validas
+		[5]	{tipo_comida}	object {System.Data.DataColumn}//dice si es dacs otras son invalid
+		[6]	{comidas}	object {System.Data.DataColumn}
+		[7]	{ANFITRIONA}	object {System.Data.DataColumn}
+		[8]	{NOMBRECLIENTE}	object {System.Data.DataColumn}
+		[9]	{NOTAS}	object {System.Data.DataColumn}
+		[10]	{RESERVACION}	object {System.Data.DataColumn}
*/


            //this is cast is necesary, debug reports entraDate as objcet datetime
            var entraDate = (DateTime)row.ItemArray[0];
            var saleDate = (DateTime)row.ItemArray[1];
            var myDate = fecha;
            string mode = "";


            //add to dacs

            //compare 2 dates disregard timestamp
            if (entraDate.Date.Equals(myDate.Date))
            {
                //check cantidad comidas
                if ((int)(decimal)row.ItemArray[4] == 1)
                {//1 dacs
                    this.helpMeSetDetalleOETDiaEntra(row, ref mode, 1);
                }
                else if ((int)(decimal)row.ItemArray[4] == 2)
                {//2 comidas
                    this.helpMeSetDetalleOETDiaEntra(row, ref mode, 2);
                }
                else if ((int)(decimal)row.ItemArray[4] == 3)
                {//3comidas
                    this.helpMeSetDetalleOETDiaEntra(row, ref mode, 3);
                }
            }
            else if (saleDate.Date.Equals(myDate.Date))//si la fecha de hoy es igual a cuando sale
            {
                //This case is about giving back what was pending from eentra date primera comida
                //add to dacs
                switch ((int)(decimal)row.ItemArray[4])//cantidad comidas/dia
                {
                    case 1:
                        {
                            //thre must a primera comida thus no pending
                            //dt.Rows.RemoveAt(dt.Rows.IndexOf(row)); does not work index are incremented
                            rowsToDelete.Add(row);

                        }
                        break;
                    case 2:
                        {
                            //if primera comida is d or a nothing pending
                            if (row.ItemArray[2].ToString().IndexOf("cena", StringComparison.OrdinalIgnoreCase) >= 0)
                            {
                                mode += "a";
                                this.setComidas(ref mode, ref row);
                            }
                            else
                            {
                                rowsToDelete.Add(row);
                            }
                        }
                        break;

                    case 3:
                        {
                            //check primera comida
                            if (row.ItemArray[2].ToString().IndexOf("almuerzo", StringComparison.OrdinalIgnoreCase) >= 0)
                            {
                                mode += "d";
                                this.setComidas(ref mode, ref row);

                            }
                            else if (row.ItemArray[2].ToString().IndexOf("cena", StringComparison.OrdinalIgnoreCase) >= 0)
                            {
                                //se le debe da
                              
                                mode += "da";
                                this.setComidas(ref mode, ref row);
                            }
                            else
                            {
                                /*delete row*/
                                rowsToDelete.Add(row);
                            }
                        }
                        break;
                }

            }
            else//myDate doesnot equal entra or sale
            {
                //check tipo de comida, desayuno, almuerzo, cena, snack son 1 comida/dia
                if (row.ItemArray[5].ToString().IndexOf("Desayuno", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    mode += "d";
                    this.setComidas(ref mode, ref row);
                }
                else if (row.ItemArray[5].ToString().IndexOf("Almuerzo", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    mode += "a";
                    this.setComidas(ref mode, ref row);
                }
                else if (row.ItemArray[5].ToString().IndexOf("CENA", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    mode += "c";
                    this.setComidas(ref mode, ref row);
                }
                else if (row.ItemArray[5].ToString().IndexOf("SNACK", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    mode += "s";
                    this.setComidas(ref mode, ref row);
                }
                else if (row.ItemArray[5].ToString().IndexOf("2 Comidas", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    //check primera comida
                    if (row.ItemArray[2].ToString().IndexOf("DESAYUNO", StringComparison.OrdinalIgnoreCase) >= 0)//SI LA PRIMERA COMIDA DICE DESYUNO
                    {
                       
                        mode += "da";
                        this.setComidas(ref mode, ref row);
                    }
                    else if (row.ItemArray[2].ToString().IndexOf("ALMUERZO", StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                       
                        mode += "ac";
                        this.setComidas(ref mode, ref row);

                    }
                    else if (row.ItemArray[2].ToString().IndexOf("CENA", StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                      
                        mode += "ac";
                        this.setComidas(ref mode, ref row);
                    }
                    else
                    {//default(if primera comida says 0 or nothing) for 2 comidas is da
                      
                        mode += "da";
                        this.setComidas(ref mode, ref row);
                    }
                }
                else if (row.ItemArray[5].ToString().IndexOf("3 Comidas", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                   
                    mode += "dac";
                    this.setComidas(ref mode, ref row);
                }

            }//else end 
        }//method end


        protected void helpMeSetDetalleOETSpecialCase(DataRow row, ref string mode, int cantidadComidas)
        {

            switch (cantidadComidas)
            {
                case 1:
                    {
                        //check tipo comida
                        if (row.ItemArray[5].ToString().IndexOf("Desayuno", StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            mode += "d";
                            this.setComidas(ref mode, ref row);
                        }
                        else if (row.ItemArray[5].ToString().IndexOf("Almuerzo", StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            mode += "a";
                            this.setComidas(ref mode, ref row);
                        }
                        else if (row.ItemArray[5].ToString().IndexOf("CENA", StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            mode += "c";
                            this.setComidas(ref mode, ref row);
                        }
                        else if (row.ItemArray[5].ToString().IndexOf("SNACK", StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            //check primera comida                    
                            mode += "s";
                            this.setComidas(ref mode, ref row);
                        }
                        else
                        {
                            //this makes no sense
                        }
                        break;
                    }
                case 2:
                    {

                        break;
                    }
                case 3:
                    {//cantidadComidas 3


                        break;
                    }

            }//switch end

        }

        protected void helpMeSetDetalleOETDiaEntra(DataRow row, ref string mode, int cantidadComidas)
        {

            switch (cantidadComidas)
            {
                case 1:
                    {
                        //check primera comida
                        if (row.ItemArray[2].ToString().IndexOf("DESAYUNO", StringComparison.OrdinalIgnoreCase) >= 0)//SI LA PRIMERA COMIDA DICE DESYUNO
                        {
                            mode += "d";
                            this.setComidas(ref mode, ref row);

                        }
                        else if (row.ItemArray[2].ToString().IndexOf("ALMUERZO", StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            mode += "a";
                            this.setComidas(ref mode, ref row);

                        }
                        else if (row.ItemArray[2].ToString().IndexOf("CENA", StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            mode += "c";
                            this.setComidas(ref mode, ref row);
                        }
                        else if (row.ItemArray[2].ToString().IndexOf("SNACK", StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            mode += "s";
                            this.setComidas(ref mode, ref row);
                        }
                        else
                        {//default(if primera comida says 0 or nothing) foo
                            this.helpMeSetDetalleOETSpecialCase(row, ref mode, cantidadComidas);
                        }
                        break;
                    }
                case 2:
                    {
                        //check primera comida
                        if (row.ItemArray[2].ToString().IndexOf("DESAYUNO", StringComparison.OrdinalIgnoreCase) >= 0)//SI LA PRIMERA COMIDA DICE DESYUNO
                        {
                           
                            mode += "da";
                            this.setComidas(ref mode, ref row);

                        }
                        else if (row.ItemArray[2].ToString().IndexOf("ALMUERZO", StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                          
                            mode += "ac";
                            this.setComidas(ref mode, ref row);

                        }
                        else if (row.ItemArray[2].ToString().IndexOf("CENA", StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            mode += "c";
                            this.setComidas(ref mode, ref row);

                        }
                        else
                        {//default(if primera comida says 0 or nothing) for 2 comidas is da
                           
                            mode += "da";
                            this.setComidas(ref mode, ref row);
                        }
                        break;
                    }
                case 3:
                    {//cantidadComidas 3
                        //check primera comida
                        if (row.ItemArray[2].ToString().IndexOf("DESAYUNO", StringComparison.OrdinalIgnoreCase) >= 0)//SI LA PRIMERA COMIDA DICE DESYUNO
                        {
                           
                            mode += "dac";
                            this.setComidas(ref mode, ref row);

                        }
                        else if (row.ItemArray[2].ToString().IndexOf("ALMUERZO", StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                          
                            mode += "ac";
                            this.setComidas(ref mode, ref row);

                        }
                        else if (row.ItemArray[2].ToString().IndexOf("CENA", StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            mode += "c";
                            this.setComidas(ref mode, ref row);
                        }
                        else
                        {
                            
                            mode += "dac";
                            this.setComidas(ref mode, ref row);
                        }

                        break;
                    }

            }

        }//method end

        /*
        *  Requiere: Controladores de eventos de la interfaz.
        *  Efectúa:  Cambia el contenido de la tabla al índice seleccionado.
        *  Retrona:  N/A
        */
        protected void GridViewReportes_PageIndexChanging(Object sender, GridViewPageEventArgs e)
        {

            GridViewReportes.PageIndex = e.NewPageIndex;
            GridViewReportes.DataSource = dt2;
            GridViewReportes.DataBind();

        }

        protected void GridViewDetalles_PageIndexChanging(Object sender, GridViewPageEventArgs e)
        {

            this.GridViewDetalles.PageIndex = e.NewPageIndex;
            this.GridViewDetalles.DataSource = dt3;
            this.GridViewDetalles.DataBind();

        } 

          protected void BotonGenerar_Click(object sender, EventArgs e)
        {

            //disable filters
            this.btnDesayunar.Enabled = false;
            this.btnAlmuerzo.Enabled = false;
            this.btnCena.Enabled = false;

               this.GridViewDetalles.DataSource = null;
                this.GridViewReportes.DataSource = null;
                this.GridViewDetalles.Visible = false;
                this.GridViewReportes.Visible = false;

              Boolean delOk = false; Boolean alOk = false;



            if (!checkDateAcceptXregex(this.dateFechaInicio.Value))
            {
                this.dateFechaInicio.Style["border-color"] = "red";
                this.dateFechaInicio.Style["color"] = "red";
               
            }
            else
            {
                delOk = true;

            }

            if (!checkDateAcceptXregex(this.dateFechaFin.Value))
            {
                this.dateFechaFin.Style["border-color"] = "red";
                this.dateFechaFin.Style["color"] = "red";

            }
            else {
                alOk = true;

            }

            if (delOk && alOk){

                this.dateFechaInicio.Style["border-color"] = "";
                this.dateFechaInicio.Style["color"] = "";
                this.dateFechaFin.Style["border-color"] = "";
                this.dateFechaFin.Style["color"] = "";

                obtenerFiltros();
                /*
                this.GridViewDetalles.DataSource = null;
                this.GridViewReportes.DataSource = null;
                this.GridViewDetalles.Visible = false;
                this.GridViewReportes.Visible = false;
                */

                if (DateTime.Parse(dateFechaInicio.Value) <= DateTime.Parse(dateFechaFin.Value))
                {
                    DateTime fechTemp = DateTime.Parse(dateFechaInicio.Value);
                    var fecha1 = fechTemp.ToString("d/M/yyyy");
                    fechTemp = DateTime.Parse(dateFechaFin.Value);
                    var fecha2 = fechTemp.ToString("d/M/yyyy");

                    this.generarReporteFechas(fecha1, fecha2, ref dt);

                }
                else
                {
                    this.dateFechaInicio.Value = dateFechaInicio.Value;
                    this.dateFechaFin.Value = dateFechaInicio.Value;
                }
            
            
            }
            

            
            

           

        }

         

        protected void resetCounts()
        {
            countDesayuno = 0;
            countDesayunoServ = 0;
            countAlmuerzo = 0;
            countAlmuerzoServ = 0;
            countCena = 0;
            countCenaServ = 0;
            countSnack = 0;
            countSnackServ = 0;
            countComidaCampo = 0;
            countComidaCampoServ = 0;
        }



        protected void resetTotales()
        {
            totalDesayuno = 0;
            totalDesayunoServ = 0;
            totalAlmuerzo = 0;
            totalAlmuerzoServ = 0;
            totalCena = 0;
            totalCenaServ = 0;
            totalSnack = 0;
            totalSnackServ = 0;
            totalComidaCampo = 0;
            totalComidaCampoServ = 0;
            totalTotal = 0;
            totalServidos = 0;
        }


        protected void saveTotales()
        {
            totalDesayuno += countDesayuno;
            totalDesayunoServ += countDesayunoServ;
            totalAlmuerzo += countAlmuerzo;
            totalAlmuerzoServ += countAlmuerzoServ;
            totalCena += countCena;
            totalCenaServ += countCenaServ;
            totalSnack += countSnack;
            totalSnackServ += countSnackServ;
            totalComidaCampo += countComidaCampo;
            totalComidaCampoServ += countComidaCampoServ;
            totalTotal += countDesayuno+countCena+countAlmuerzo+countSnack+countComidaCampo;
            totalServidos += countDesayunoServ + countAlmuerzoServ + countCenaServ + countSnackServ + countComidaCampoServ;
        }



        /*fecha2 must be >r= than fecha1*/
        public void generarReporteFechas(string fecha1, string fecha2, ref DataTable dt)
        {
            //clear all dts
            dt.Clear();
            dt2.Clear();
            dt3.Clear();
            /*
            //disable all filters
            this.btnDesayunar.Enabled = false;
            this.btnAlmuerzo.Enabled = false;
            this.btnCena.Enabled = false;
            */
            //clear and hide previous content
            this.GridViewDetalles.DataSource = null;
            this.GridViewDetalles.Visible=false;

            this.resetTotales();
            
            //dt = controladoraJC.testFechas(fecha1, fecha2, estacionJC);
            filldt2(DateTime.ParseExact(fecha1, "d/M/yyyy", CultureInfo.InvariantCulture), DateTime.ParseExact(fecha2, "d/M/yyyy", CultureInfo.InvariantCulture));

            //show stuff        

            GridViewReportes.DataSource = dt2;
            GridViewReportes.AllowSorting = false;
            GridViewReportes.DataBind();

            this.GridViewReportes.Visible = true;


        }


        //dt2 is the result table to show 
        //dt2 is made out of dt traversing each of its rows
        //fecha1 must be equal less than fecha2
        protected void filldt2(DateTime fecha1, DateTime fecha2)
        {
           

            if (!dt2.Columns.Contains("Día"))
                //create columns of dt2
                dt2=this.crearTablaServicios();

            DateTime iDate = fecha1;

            // for each day from fecha1 to fecha2 add a row to dt2 with such date and set dacs counts for such day

            while (iDate <= fecha2)
            {
                DataTable dt = controladoraJC.test(iDate.ToString("d/M/yyyy"), estacionJC);//dt is an liason table to make all counts
                resetCounts();
                DataRow dr = dt2.NewRow();

                //add to counts on OET tables
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    this.setCounts(dt.Rows[i], iDate);//setCounts for each row in dt 

                }

                //add to counts on grupo tables
                this.seCountsGrupo(iDate.ToString("d/M/yyyy"), estacionJC);

                //save totales
                this.saveTotales();

                //fill each row element
                dr[0] = iDate.ToString("M/d/yyyy");
                dr[1] = countDesayuno.ToString();
                dr[2] = countDesayunoServ.ToString();
                dr[3] = countAlmuerzo.ToString();
                dr[4] = countAlmuerzoServ.ToString();
                dr[5] = countCena.ToString();
                dr[6] = countCenaServ.ToString();
                dr[7] = countSnack.ToString();
                dr[8] = countSnackServ.ToString();
                dr[9] = countComidaCampo.ToString();
                dr[10] = countComidaCampoServ.ToString();

                dr[11] = (countSnack + countDesayuno + countAlmuerzo + countCena+countComidaCampo).ToString();
                dr[12] = (countDesayunoServ + countAlmuerzoServ + countCenaServ + countSnackServ + countComidaCampoServ).ToString();


                dt2.Rows.Add(dr);//this will add the row at the end of the datatable

                iDate = iDate.AddDays(1);//lets go to next date

            }

            //add final row with totals
            //add final row with totals
            DataRow drf = dt2.NewRow();
            //fill each row element
            drf[0] = "TOTALES";
            drf[1] = totalDesayuno.ToString();
            drf[2] = totalDesayunoServ.ToString();
            drf[3] = totalAlmuerzo.ToString();
            drf[4] = totalAlmuerzoServ.ToString();
            drf[5] = totalCena.ToString();
            drf[6] = totalCenaServ.ToString();
            drf[7] = totalSnack.ToString();
            drf[8] = totalSnackServ.ToString();
            drf[9] = totalComidaCampo.ToString();
            drf[10] = totalComidaCampoServ.ToString();

            drf[11] = totalTotal.ToString();
            drf[12] = totalServidos.ToString();


            dt2.Rows.Add(drf);//this will add the row at the end of the datatable

        }//method end

        //sets counts for Grupo tables
        protected void seCountsGrupo(string fecha, string estacionJC){

            //sets counts for servicio especial
            DataTable dt = controladoraJC.testServicioExtra(fecha, estacionJC);//dt is an liason table to make all counts
            /* row item array corrsponds to
             * 
             * 		[0]	14	object {decimal} //pax
            		[1]	"Almuerzo"	object {string} //tipo
		            [2]	0	object {decimal} //vecesconsumido

             */

            //add to counts on Grupo tables
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                helpMeSetCountsGrupo(dt.Rows[i]);
  
            }

           //sets counts for empleadoTable
            dt.Clear();
            dt = controladoraJC.testServicioEmpleado(fecha);

            //add to counts on Grupo tables
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                helpMeSetCountsGrupo(dt.Rows[i]);

            }
           
   
        }

        protected void helpMeSetCountsGrupo(DataRow row) {

            if (row.ItemArray[1].ToString().IndexOf("desayuno", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                countDesayuno += (int)(decimal)row.ItemArray[0];
                countDesayunoServ += (int)(decimal)row.ItemArray[2];

            }
            else if (row.ItemArray[1].ToString().IndexOf("almuerzo", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                countAlmuerzo += (int)(decimal)row.ItemArray[0];
                countAlmuerzoServ += (int)(decimal)row.ItemArray[2];

            }
            else if (row.ItemArray[1].ToString().IndexOf("cena", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                countCena += (int)(decimal)row.ItemArray[0];
                countCenaServ += (int)(decimal)row.ItemArray[2];

            }
            else //anything else counts as comida campo
            {
                countComidaCampo += (int)(decimal)row.ItemArray[0];
                countComidaCampoServ += (int)(decimal)row.ItemArray[2];

            }
            
        
        }


        protected void helpMeSetCountsEmpleado(DataRow row)
        {

            if (row.ItemArray[1].ToString().IndexOf("desayuno", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                countDesayuno += (int)(decimal)row.ItemArray[0];
                countDesayunoServ += (int)(decimal)row.ItemArray[2];

            }
            else if (row.ItemArray[1].ToString().IndexOf("almuerzo", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                countAlmuerzo += (int)(decimal)row.ItemArray[0];
                countAlmuerzoServ += (int)(decimal)row.ItemArray[2];

            }
            else if (row.ItemArray[1].ToString().IndexOf("cena", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                countCena += (int)(decimal)row.ItemArray[0];
                countCenaServ += (int)(decimal)row.ItemArray[2];

            }
            else //anything else counts as comida campo
            {
                countComidaCampo += (int)(decimal)row.ItemArray[0];
                countComidaCampoServ += (int)(decimal)row.ItemArray[2];

            }


        }
       



        //sets dacs counts
        public void setCounts(DataRow row, DateTime fecha)
        {

            //row.ItemArray[0] is the first element of this row;
            /*
             *itemArray[index] corresponds
            /*
+		[0]	{ENTRA}	object {System.Data.DataColumn}
+		[1]	{SALE}	object {System.Data.DataColumn}
+		[2]	{PRIMERA_COMIDA}	object {System.Data.DataColumn}
+		[3]	{PAX}	object {System.Data.DataColumn}
+		[4]	{CANTIDAD}	object {System.Data.DataColumn}//1-3 son validas
+		[5]	{tipo_comida}	object {System.Data.DataColumn}//dice si es dacs otras son invalid
+		[6]	{comidas}	object {System.Data.DataColumn}
+		[7]	{ANFITRIONA}	object {System.Data.DataColumn}
+		[8]	{NOMBRECLIENTE}	object {System.Data.DataColumn}
+		[9]	{NOTAS}	object {System.Data.DataColumn}
+		[10]	{RESERVACION}	object {System.Data.DataColumn}
*/

            //this is cast is necesary, debug reports entraDate as objcet datetime
            var entraDate = (DateTime)row.ItemArray[0];
            var saleDate = (DateTime)row.ItemArray[1];
            var myDate = fecha;

            //add to dacs

            //compare 2 dates disregard timestamp
            if (entraDate.Date.Equals(myDate.Date))
            {
                //check cantidad comidas
                if ((int)(decimal)row.ItemArray[4] == 1)
                {//1 dacs
                    this.helpMeSetCountsDiaEntra(row, 1);
                }
                else if ((int)(decimal)row.ItemArray[4] == 2)
                {//2 comidas
                    this.helpMeSetCountsDiaEntra(row, 2);
                }
                else if ((int)(decimal)row.ItemArray[4] == 3)
                {//3comidas
                    this.helpMeSetCountsDiaEntra(row,  3);
                }
            }
            else if (saleDate.Date.Equals(myDate.Date))//si la fecha de hoy es igual a cuando sale
            {
                //This case is about giving back what was pending from eentra date primera comida
                //add to dacs
                switch ((int)(decimal)row.ItemArray[4])//cantidad comidas/dia
                {
                    case 1:
                        {
                            //thre must a primera comida thus no pending
                            //dt.Rows.RemoveAt(dt.Rows.IndexOf(row)); does not work index are incremented

                        }
                        break;
                    case 2:
                        {
                            //if primera comida is d or a nothing pending
                            if (row.ItemArray[2].ToString().IndexOf("cena", StringComparison.OrdinalIgnoreCase) >= 0)
                            {
                                countAlmuerzo += (int)(decimal)row.ItemArray[3];
                                
                            }
                            else{}
                        }
                        break;

                    case 3:
                        {
                            //check primera comida
                            if (row.ItemArray[2].ToString().IndexOf("almuerzo", StringComparison.OrdinalIgnoreCase) >= 0)
                            {
                                countDesayuno += (int)(decimal)row.ItemArray[3];
                              
                            }
                            else if (row.ItemArray[2].ToString().IndexOf("cena", StringComparison.OrdinalIgnoreCase) >= 0)
                            {
                                //se le debe da
                                countDesayuno += (int)(decimal)row.ItemArray[3];
                                countAlmuerzo += (int)(decimal)row.ItemArray[3];
                                
                            }
                            else
                            {
                            }
                        }
                        break;
                }

            }
            else//myDate doesnot equal entra or sale
            {
                //check tipo de comida, desayuno, almuerzo, cena, snack son 1 comida/dia
                if (row.ItemArray[5].ToString().IndexOf("Desayuno", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    countDesayuno += (int)(decimal)row.ItemArray[3];//pax
                    
                }
                else if (row.ItemArray[5].ToString().IndexOf("Almuerzo", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    countAlmuerzo += (int)(decimal)row.ItemArray[3];
                 
                }
                else if (row.ItemArray[5].ToString().IndexOf("CENA", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    countCena += (int)(decimal)row.ItemArray[3];
                  
                }
                else if (row.ItemArray[5].ToString().IndexOf("SNACK", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    countSnack += (int)(decimal)row.ItemArray[3];
                    
                }
                else if (row.ItemArray[5].ToString().IndexOf("2 Comidas", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    //check primera comida
                    if (row.ItemArray[2].ToString().IndexOf("DESAYUNO", StringComparison.OrdinalIgnoreCase) >= 0)//SI LA PRIMERA COMIDA DICE DESYUNO
                    {
                        countDesayuno += (int)(decimal)row.ItemArray[3];
                        countAlmuerzo += (int)(decimal)row.ItemArray[3];
                       
                    }
                    else if (row.ItemArray[2].ToString().IndexOf("ALMUERZO", StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        countCena += (int)(decimal)row.ItemArray[3];
                        countAlmuerzo += (int)(decimal)row.ItemArray[3];
                       
                    }
                    else if (row.ItemArray[2].ToString().IndexOf("CENA", StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        countCena += (int)(decimal)row.ItemArray[3];
                        countAlmuerzo += (int)(decimal)row.ItemArray[3];
                      
                    }
                    else
                    {//default(if primera comida says 0 or nothing) for 2 comidas is da
                        countDesayuno += (int)(decimal)row.ItemArray[3];
                        countAlmuerzo += (int)(decimal)row.ItemArray[3];
                       
                    }
                }
                else if (row.ItemArray[5].ToString().IndexOf("3 Comidas", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    countDesayuno += (int)(decimal)row.ItemArray[3];
                    countAlmuerzo += (int)(decimal)row.ItemArray[3];
                    countCena += (int)(decimal)row.ItemArray[3];
                   
                }

            }//else end 
        }//method end


        protected void helpMeSetCountsSpecialCase(DataRow row,  int cantidadComidas)
        {

            switch (cantidadComidas)
            {
                case 1:
                    {
                        //check tipo comida
                        if (row.ItemArray[5].ToString().IndexOf("Desayuno", StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            countDesayuno += (int)(decimal)row.ItemArray[3];//pax
                           
                        }
                        else if (row.ItemArray[5].ToString().IndexOf("Almuerzo", StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            countAlmuerzo += (int)(decimal)row.ItemArray[3];//pax
                           
                        }
                        else if (row.ItemArray[5].ToString().IndexOf("CENA", StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            countCena += (int)(decimal)row.ItemArray[3];//pax
                           
                        }
                        else if (row.ItemArray[5].ToString().IndexOf("SNACK", StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            //check primera comida                    
                            countSnack += (int)(decimal)row.ItemArray[3];//pax
                            
                        }
                        else
                        {
                            //this makes no sense
                        }
                        break;
                    }
                case 2:
                    {

                        break;
                    }
                case 3:
                    {//cantidadComidas 3


                        break;
                    }

            }//switch end

        }

        protected void helpMeSetCountsDiaEntra(DataRow row, int cantidadComidas)
        {

            switch (cantidadComidas)
            {
                case 1:
                    {
                        //check primera comida
                        if (row.ItemArray[2].ToString().IndexOf("DESAYUNO", StringComparison.OrdinalIgnoreCase) >= 0)//SI LA PRIMERA COMIDA DICE DESYUNO
                        {
                            countDesayuno += (int)(decimal)row.ItemArray[3];
                            

                        }
                        else if (row.ItemArray[2].ToString().IndexOf("ALMUERZO", StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            countAlmuerzo += (int)(decimal)row.ItemArray[3];
                            

                        }
                        else if (row.ItemArray[2].ToString().IndexOf("CENA", StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            countCena += (int)(decimal)row.ItemArray[3];
                           
                        }
                        else if (row.ItemArray[2].ToString().IndexOf("SNACK", StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            countSnack += (int)(decimal)row.ItemArray[3];
                           
                        }
                        else
                        {//default(if primera comida says 0 or nothing) foo
                            this.helpMeSetCountsSpecialCase(row, cantidadComidas);
                        }
                        break;
                    }
                case 2:
                    {
                        //check primera comida
                        if (row.ItemArray[2].ToString().IndexOf("DESAYUNO", StringComparison.OrdinalIgnoreCase) >= 0)//SI LA PRIMERA COMIDA DICE DESYUNO
                        {
                            countDesayuno += (int)(decimal)row.ItemArray[3];
                            countAlmuerzo += (int)(decimal)row.ItemArray[3];
                           

                        }
                        else if (row.ItemArray[2].ToString().IndexOf("ALMUERZO", StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            countAlmuerzo += (int)(decimal)row.ItemArray[3];
                            countCena += (int)(decimal)row.ItemArray[3];
                           

                        }
                        else if (row.ItemArray[2].ToString().IndexOf("CENA", StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            countCena += (int)(decimal)row.ItemArray[3];
                           

                        }
                        else
                        {//default(if primera comida says 0 or nothing) for 2 comidas is da
                            countDesayuno += (int)(decimal)row.ItemArray[3];
                            countAlmuerzo += (int)(decimal)row.ItemArray[3];
                          
                        }
                        break;
                    }
                case 3:
                    {//cantidadComidas 3
                        //check primera comida
                        if (row.ItemArray[2].ToString().IndexOf("DESAYUNO", StringComparison.OrdinalIgnoreCase) >= 0)//SI LA PRIMERA COMIDA DICE DESYUNO
                        {
                            countDesayuno += (int)(decimal)row.ItemArray[3];
                            countAlmuerzo += (int)(decimal)row.ItemArray[3];
                            countCena += (int)(decimal)row.ItemArray[3];
                          
                        }
                        else if (row.ItemArray[2].ToString().IndexOf("ALMUERZO", StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            countCena += (int)(decimal)row.ItemArray[3];
                            countAlmuerzo += (int)(decimal)row.ItemArray[3];
                           

                        }
                        else if (row.ItemArray[2].ToString().IndexOf("CENA", StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            countCena += (int)(decimal)row.ItemArray[3];
                         
                        }
                        else
                        {
                            countDesayuno += (int)(decimal)row.ItemArray[3];
                            countAlmuerzo += (int)(decimal)row.ItemArray[3];
                            countCena += (int)(decimal)row.ItemArray[3];
                           
                        }

                        break;
                    }

            }

        }//method end



        /*
         * Efecto: carga los datos y activa los combobox. 
         * Requiere: iniciar el FormComidaExtra.
         * Modifica: no realiza modificaciones, solo carga la pantalla.
        */
        void cargarDatos()
        {
           
            cbxFecha.Items.Clear();
            cbxFecha.Items.Add("Hoy");
            cbxFecha.Items.Add("Semana");
            cbxFecha.Items.Add("Mes");

            dateFechaInicio.Value = String.Format("{0:MM/dd/yyyy}", DateTime.Today);
            dateFechaFin.Value = String.Format("{0:MM/dd/yyyy}", DateTime.Today);

        }

      


        /*
         * Efecto: obtiene los valores de los filtros seleccionados. 
         * Requiere: Click en generar reporte.
         * Modifica: el desglose del reporte.
        */
        protected void obtenerFiltros()
        {

            DateTime fechTemp = DateTime.Parse(dateFechaInicio.Value);
            Debug.WriteLine("FECHA: " + dateFechaInicio.Value);
            fechaSeleccionda = cbxFecha.Text;
            fechaInicio = fechTemp.ToString("MM/dd/yyyy");
            fechTemp = DateTime.Parse(dateFechaFin.Value);
            fechaFinal = fechTemp.ToString("MM/dd/yyyy");
        }


        

  

        /*
         * Efecto: modifica la interfaz de acuerdo a lo selecionado en las opciones de filtro.
         * Requiere: iniciar el FormReportes y seleccionar el combobox.
         * Modifica: el FormReportes.
        */
        protected void mostrarFechas(object sender, EventArgs e)
        {
            int indice = cbxFecha.SelectedIndex;
            switch (indice)
            {
                case (0):
                    dateFechaInicio.Value = DateTime.Today.ToString("MM/dd/yyyy");
                    dateFechaFin.Value = dateFechaInicio.Value;
                    break;
                case (1):
                    dateFechaInicio.Value = DateTime.Today.ToString("MM/dd/yyyy");
                    dateFechaFin.Value =  DateTime.Today.AddDays(7).ToString("MM/dd/yyyy");
                    break;
                case (2):
                    dateFechaInicio.Value = DateTime.Today.ToString("MM/dd/yyyy");
                    dateFechaFin.Value =  DateTime.Today.AddMonths(1).ToString("MM/dd/yyyy");
                    break;
                           
            }
        }
                       





        /**
     * Requiere: n/a
     * Efectua: Crea la DataTable para desplegar.
     * retorna:  un dato del tipo DataTable con la estructura para consultar.
     */
        protected DataTable crearTablaServicios()//consultar
        {
            DataTable tabla = new DataTable();
            DataColumn columna;

            columna = new DataColumn();
            columna.DataType = System.Type.GetType("System.String");
            columna.ColumnName = "Día";
            tabla.Columns.Add(columna);

            columna = new DataColumn();
            columna.DataType = System.Type.GetType("System.String");
            columna.ColumnName = "Desayuno";
            tabla.Columns.Add(columna);

            columna = new DataColumn();
            columna.DataType = System.Type.GetType("System.String");
            columna.ColumnName = "Desayunos Servidos";
            tabla.Columns.Add(columna);

            columna = new DataColumn();
            columna.DataType = System.Type.GetType("System.String");
            columna.ColumnName = "Almuerzo";
            tabla.Columns.Add(columna);

            columna = new DataColumn();
            columna.DataType = System.Type.GetType("System.String");
            columna.ColumnName = "Almuerzo Servidos";
            tabla.Columns.Add(columna);

            columna = new DataColumn();
            columna.DataType = System.Type.GetType("System.String");
            columna.ColumnName = "Cena";
            tabla.Columns.Add(columna);

            columna = new DataColumn();
            columna.DataType = System.Type.GetType("System.String");
            columna.ColumnName = "Cenas Servidas";
            tabla.Columns.Add(columna);

            columna = new DataColumn();
            columna.DataType = System.Type.GetType("System.String");
            columna.ColumnName = "Snack";
            tabla.Columns.Add(columna);

            columna = new DataColumn();
            columna.DataType = System.Type.GetType("System.String");
            columna.ColumnName = "Snack Servidos";
            tabla.Columns.Add(columna);

            columna = new DataColumn();
            columna.DataType = System.Type.GetType("System.String");
            columna.ColumnName = "Comida Campo";
            tabla.Columns.Add(columna);

            columna = new DataColumn();
            columna.DataType = System.Type.GetType("System.String");
            columna.ColumnName = "Comida Campo Servida";
            tabla.Columns.Add(columna);

            columna = new DataColumn();
            columna.DataType = System.Type.GetType("System.String");
            columna.ColumnName = "Total";
            tabla.Columns.Add(columna);

            columna = new DataColumn();
            columna.DataType = System.Type.GetType("System.String");
            columna.ColumnName = "Total Servidos";
            tabla.Columns.Add(columna);
            /*
            GridViewReportes.DataSource = tabla;
            GridViewReportes.AllowSorting = false;
            GridViewReportes.DataBind();
            */
           
            return tabla;
        }

      


        /**
         * 
         * 
         * 
         * 
         * 
         * 
         * 
         * 
         * 
         * 
         * 
         * 
         * 
         * 
         * 
         * 
         * 
         * 
         * 
         */


        /*Efecto: Crea la tabla de servicios
         * Requiere: NA
         * Modifica: la tabla servicios, si la reservacion tiene servicios asociados
         * */
        void llenarGridReportes()
        {
            DataTable tabla = crearTablaServicios();
            try
            {


                Object[] datos = new Object[13];
                if (estacion != null && fechaInicio != null && fechaFinal != null)
                {

                    DataTable paxReserv = controladora.obtenerComidaPax(estacion, 1, anfitriona, fechaInicio, fechaFinal);// se consultan desayunos de comida de campo dependiendo de fecha con estacion y anfitriona.
                    DataTable paxReservAlmuerzo = controladora.obtenerComidaPax(estacion, 2, anfitriona, fechaInicio, fechaFinal);
                    sumaTotalAlmuerzo = int.Parse(paxReservAlmuerzo.Rows[0][1].ToString());
                    sumaTotalConsumidosAlmuerzo = int.Parse(paxReservAlmuerzo.Rows[0][2].ToString());
                    contar = paxReserv.Rows.Count;
                    fechas = paxReserv;


                    if (anfitriona == 1)
                    {

                        DataTable paxEmp = controladora.obtenerComidaPaxEmp(estacion, 1, fechaInicio, fechaFinal); //desayuno comida campo reserv
                        DataTable comidaEmp = controladora.obtenerComidaEmp(estacion, "desayuno", fechaInicio, fechaFinal); //desayuno comida campo de empleados
                        int contador = comidaEmp.Rows.Count;
                        if (contador > 0)
                        {
                            sumaTotalDesayuno = int.Parse(paxReserv.Rows[0][1].ToString()) + int.Parse(paxEmp.Rows[0][1].ToString()) + int.Parse(comidaEmp.Rows[0][1].ToString());    //suma total desayuno  
                            sumaTotalConsumidosDesayuno = int.Parse(paxReserv.Rows[0][2].ToString()) + int.Parse(paxEmp.Rows[0][2].ToString()) + int.Parse(comidaEmp.Rows[0][2].ToString());
                        }
                        else
                        {
                            sumaTotalDesayuno = int.Parse(paxReserv.Rows[0][1].ToString()) + int.Parse(paxEmp.Rows[0][1].ToString());
                            sumaTotalConsumidosDesayuno = int.Parse(paxReserv.Rows[0][2].ToString()) + int.Parse(paxEmp.Rows[0][2].ToString());

                        }

                    }
                    else
                    {
                        sumaTotalDesayuno = int.Parse(paxReserv.Rows[0][1].ToString());    //suma total desayuno  
                        sumaTotalConsumidosDesayuno = int.Parse(paxReserv.Rows[0][2].ToString());
                    }
                }

                if (contar > 0)
                {
                    for (int i = 0; i < contar; i++)
                    {
                        datos[0] = fechas.Rows[i][0];
                        datos[1] = sumaTotalDesayuno;
                        datos[2] = sumaTotalConsumidosDesayuno;
                        datos[3] = sumaTotalAlmuerzo;
                        datos[4] = sumaTotalConsumidosAlmuerzo;
                        datos[5] = "-";
                        datos[6] = "-";
                        datos[7] = "-";
                        datos[8] = "-";
                        datos[9] = "-";
                        datos[10] = "-";
                        datos[11] = "-";
                        datos[12] = "-";
                        tabla.Rows.Add(datos);// cargar en la tabla los datos de cada proveedor
                    }
                }

                GridViewReportes.AllowSorting = false;
                GridViewReportes.DataBind();

            }
            catch (Exception e)
            {
                //Debug.WriteLine("No se pudo cargar las reservaciones");
            }
        }




    }
}