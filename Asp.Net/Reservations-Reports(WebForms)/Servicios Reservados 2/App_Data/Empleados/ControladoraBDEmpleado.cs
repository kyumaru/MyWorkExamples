using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace Servicios_Reservados_2
{
    public class ControladoraBDEmpleado
    {
        private AdaptadorEmpleado adaptador;
        DataTable dt;
    /*
     * Requiere: N/A
     * Efectúa : inicializa las variables globales de la clase
     * retorna : N/A
     */
        public ControladoraBDEmpleado()
        {
            adaptador = new AdaptadorEmpleado();
            dt = new DataTable();
            
        }
        /*
         * Requiere: N/A
         * Efectúa : Obtiene la fecha actual. Crea la consulta para obtener las cosultas activas con la fecha actual. Guarda en una tabla de datos el resultado a la consulta al adaptador.
         * Retorna : la tabla de datos con el resultado de la consulta.
         */
        internal DataTable consultarTodosEmpleados() {

            String consultaSQL = "select idempleado,numempleado,nombre,apellidos from financiero.empleados where estado = 1 order by apellidos desc";
            dt = adaptador.consultar(consultaSQL);
            
            return dt;
        }
        /*
         * Requiere: una hilera con el identificador de reservación a consultar.
         * Efectúa : Crea la hilera de consulta concatenando el identificador. Guarda en una tabla de datos el resultado de la consulta que se hizo con el adaptador de base de datos.
         * retorna : La tabla de datos con los resultados de  la consulta.
         */
        internal DataTable consultarUnEmpleado(String id)
        {
            String consultaSQL = "select idempleado,numempleado,nombre,apellidos from financiero.empleados where idempleado = '" + id + "' order by apellidos desc";
            dt = adaptador.consultar(consultaSQL);
            return dt;
        }

        /*
         * Requiere: Hilera con la Anfitriona seleccionada, Hilera con la estación seleccionada y una hilera con el solicitante. 
         * Efectúa : Crea la hilera de consulta a partir de los parámetros dados, valida uno a uno cuáles son diferentes de vacío y estos los agrega como parámetros a la consulta. Una vez creada la consulta, la efectúa con el adaptador y gurada el resultado en una tabla de datos.
         * retorna : La tabla de datos con los resultados de  la consulta.
         */
        internal DataTable consultarEmpleados(String nombre,String iden)
        {
            String consulta = "select idempleado,numempleado,nombre,apellidos from financiero.empleados";
            String condicional = " where ";
            if (nombre.CompareTo("vacio") != 0) {
                nombre = nombre.ToLower();
                consulta += condicional+"LOWER(nombre) like '"+nombre+"%'";
                condicional = " and ";
            }
            if (iden.CompareTo("vacio") != 0)
            {

                consulta += condicional + "numempleado = '" + iden + "'";
                condicional = " and ";
            }
            Debug.WriteLine(consulta);
            dt = adaptador.consultar(consulta);
            return dt;
        }
    }
}