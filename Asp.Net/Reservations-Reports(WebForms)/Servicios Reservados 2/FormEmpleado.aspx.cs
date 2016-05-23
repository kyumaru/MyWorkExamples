using Servicios_Reservados_2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Diagnostics;
using System.Collections;

namespace Servicios_Reservados_2
{
    public partial class FormEmpleado : System.Web.UI.Page
    {

        private static ControladoraEmpleado controladora = new ControladoraEmpleado();
        public static String[] ids;
        private static String id;
        private static int resultadosPorPagina;
        protected void Page_Load(object sender, EventArgs e)
        {
            ArrayList listaRoles = (ArrayList)Session["Roles"];
            resultadosPorPagina = GridViewEmpleados.PageSize;
            string userid = (string)Session["username"];
            if (!IsPostBack)
            {
                if (userid == "" || userid == null)
                {
                    Response.Redirect("~/Ingresar.aspx");
                } if (!listaRoles.Contains("administrador sistema") && !listaRoles.Contains("recepcion"))
                {
                    Response.Redirect("ErrorPermiso.aspx");
                }
                llenarGridEmpleados();
            }


            // ponerModo();
        }
        /**
        * Requiere: N/A.
        * Efectua: selecciona el empleado en el grid y obtiene su id.
        * retorna: N/A
        */

        protected void seleccionarEmpleado(int index)
        {
            id = (ids[index + (this.GridViewEmpleados.PageIndex * 20)]);//se obtiene la cedula a consultar
            try
            {
                controladora.seleccionarEmpleado(id);
            }
            catch (Exception error) { }
        }
        /*
         *  Requiere: Controladores de eventos de la interfaz.
         *  Efectúa:  Cambia el contenido de la tabla al índice seleccionado.
         *  Retrona:  N/A
         */
        protected void GridViewReservaciones_PageIndexChanging(Object sender, GridViewPageEventArgs e)
        {

            GridViewEmpleados.PageIndex = e.NewPageIndex;
            GridViewEmpleados.DataSource = Session["tablaa"];
            GridViewEmpleados.DataBind();

        }

        protected void llenarGridEmpleados()
        {
            id = "null";
            DataTable tabla = crearTablaEmpleados();

            try
            {

                Object[] datos = new Object[3];
                DataTable empleados = controladora.solicitarTodosEmpleados();// se consultan todos
                ids = new String[empleados.Rows.Count]; //crear el vector para ids en el grid

                int i = 0;
                if (empleados.Rows.Count > 0)
                {

                    foreach (DataRow fila in empleados.Rows)
                    {

                        ids[i] = fila[0].ToString();// guardar el id para su posterior consulta
                        datos[0] = fila[1].ToString();//obtener los datos a mostrar
                        datos[1] = fila[2].ToString();
                        datos[2] = fila[3].ToString();
                        tabla.Rows.Add(datos);// cargar en la tabla los datos de cada proveedor
                        i++;
                    }

                }

                Session["tablaa"] = tabla;
                GridViewEmpleados.DataBind();
                //Debug.WriteLine("hola");
            }
            catch (Exception e)
            {
                Debug.WriteLine("No se pudo cargar los empleados");
            }


        }

        /**
       * Requiere: n/a
       * Efectua: Crea la DataTable para desplegar.
       * retorna:  un dato del tipo DataTable con la estructura para consultar.
       */
        protected DataTable crearTablaEmpleados()//consultar
        {
            DataTable tabla = new DataTable();
            DataColumn columna;

            columna = new DataColumn();
            columna.DataType = System.Type.GetType("System.String");
            columna.ColumnName = "Carné";
            tabla.Columns.Add(columna);

            columna = new DataColumn();
            columna.DataType = System.Type.GetType("System.String");
            columna.ColumnName = "Nombre";
            tabla.Columns.Add(columna);

            columna = new DataColumn();
            columna.DataType = System.Type.GetType("System.String");
            columna.ColumnName = "Apellidos";
            tabla.Columns.Add(columna);

            GridViewEmpleados.DataSource = tabla;
            GridViewEmpleados.DataBind();

            return tabla;
        }

        protected void clicAgregarServicio(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            int i = Convert.ToInt32(row.RowIndex);

            seleccionarEmpleado(i);
            if (id != "null")
            {
                FormEmpleadoReserva.idEmpleado = id;
                Response.Redirect("FormEmpleadoReserva");
            }



        }

        /*
         * Requiere: parámetros de evento de la interfaz. 
         * Efectúa : verifica cuáles filtros han sido seleccionados y por cada uno que haya sido seleccionado guarda el valor, luego de esto envía estos valores (vacío si no se había seleccionado nada) a la controladora.
         *           Con los datos retornados los rellena la  tabla, en caso de error despliega un mensaje de error. 
         * Retorna : N/A
         */
        protected void clickBuscar(object sender, EventArgs e)
        {
            String nombre = "vacio";
            String iden = "vacio";


            if (inputNombre.Value.ToString() != "")
            {
                nombre = inputNombre.Value.ToString();
            }
            if (inputIdentificacion.Value.ToString() != "")
            {
                iden = inputIdentificacion.Value.ToString();
            }
            if (nombre.CompareTo("vacio") != 0 || iden.CompareTo("vacio") != 0)
            {
                DataTable tabla = crearTablaEmpleados();

                try
                {

                    Object[] datos = new Object[3];
                    DataTable empleados = controladora.consultarEmpleados(nombre, iden);// se consultan todos
                    ids = new String[empleados.Rows.Count]; //crear el vector para ids en el grid

                    int i = 0;
                    if (empleados.Rows.Count > 0)
                    {
                        foreach (DataRow fila in empleados.Rows)
                        {
                            ids[i] = fila[0].ToString();// guardar el id para su posterior consulta
                            datos[0] = fila[1].ToString();//obtener los datos a mostrar
                            datos[1] = fila[2].ToString();
                            datos[2] = fila[3].ToString();
                            tabla.Rows.Add(datos);// cargar en la tabla los datos de cada proveedor
                            Debug.WriteLine(fila[1].ToString());
                            i++;
                        }
                    }
                    Session["tablaa"] = tabla;
                    //GridViewReservaciones.DataSource = tabla;
                    GridViewEmpleados.DataBind();

                }
                catch (Exception s)
                {
                    Debug.WriteLine("No se pudo cargar las reservaciones");
                }
            }
            else
            {
                llenarGridEmpleados();
            }
        }
    }
}