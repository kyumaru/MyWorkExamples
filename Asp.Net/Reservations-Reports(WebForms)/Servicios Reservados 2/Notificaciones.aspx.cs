using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Servicios_Reservados_2
{
    public partial class Notificaciones : System.Web.UI.Page
    {
       private static ControladoraNotificaciones controladora = new ControladoraNotificaciones();
        protected void Page_Load(object sender, EventArgs e)
        {
            llenarNotificaciones();
            
        }
        protected void llenarNotificaciones(){
            DataTable tabla = crearTablaComidaEmpleado();
            //MOMENTO,  ESTACION,  TIPODESERVICIO,  TIPODECAMBIO,  VALORANTERIOR,  VALORACTUAL,  IDSERVICIO
            DataTable notificaciones = controladora.getNotificaciones();
            foreach (DataRow fila in notificaciones.Rows)
            {
                Object[] datos = new Object[8];
                datos[0] = fila[0];//Momento
                datos[1] = fila[1];//Estacion
                datos[2] = fila[2];//Tipo De Servicio
                datos[3] = fila[3];//Tipo De Cambio
                datos[4] = fila[4];//Valor Anterior
                datos[5] = fila[5];//valor Actual
                datos[6] = fila[6];//ID Servicio
                tabla.Rows.Add(datos);
            }


            GridNotificaciones.DataBind();
        }
        /**
        * Requiere: n/a
        * Efectua: Crea la DataTable para desplegar.
        * retorna:  un dato del tipo DataTable con la estructura para consultar.
        */
        //MOMENTO,  ESTACION,  TIPODESERVICIO,  TIPODECAMBIO,  VALORANTERIOR,  VALORACTUAL,  IDSERVICIO
        protected DataTable crearTablaComidaEmpleado()//consultar
        {
            DataTable tabla = new DataTable();
            DataColumn columna;

            columna = new DataColumn();
            columna.DataType = System.Type.GetType("System.String");
            columna.ColumnName = "Hora del cambio";
            tabla.Columns.Add(columna);

            columna = new DataColumn();
            columna.DataType = System.Type.GetType("System.String");
            columna.ColumnName = "Estacion";
            tabla.Columns.Add(columna);

            columna = new DataColumn();
            columna.DataType = System.Type.GetType("System.String");
            columna.ColumnName = "Tipo De Servicio";
            tabla.Columns.Add(columna);

            columna = new DataColumn();
            columna.DataType = System.Type.GetType("System.String");
            columna.ColumnName = "Tipo De Cambio";
            tabla.Columns.Add(columna);

            columna = new DataColumn();
            columna.DataType = System.Type.GetType("System.String");
            columna.ColumnName = "Valor Anterior";
            tabla.Columns.Add(columna);

            columna = new DataColumn();
            columna.DataType = System.Type.GetType("System.String");
            columna.ColumnName = "Valor Actual";
            tabla.Columns.Add(columna);

            columna = new DataColumn();
            columna.DataType = System.Type.GetType("System.String");
            columna.ColumnName = "Numero de Reserva / carne de empleado";
            tabla.Columns.Add(columna);

            GridNotificaciones.DataSource = tabla;
            GridNotificaciones.DataBind();

            return tabla;
        }
    }
}