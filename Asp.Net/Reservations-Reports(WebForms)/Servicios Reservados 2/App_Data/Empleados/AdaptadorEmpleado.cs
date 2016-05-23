using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Web;

namespace Servicios_Reservados_2
{
    public class AdaptadorEmpleado
    {
         OracleConnection adaptador= new OracleConnection();
        DataTable dt;
        /*
         * Requiere: N/A
         * Efectúa : Crea la hilera de conección con la base de datos.
         * Retorna : N/A
         */
        public AdaptadorEmpleado(){
             adaptador.ConnectionString = "Data Source=10.1.4.93;User ID=servicios_reservados;Password=servicios;Unicode=True";   
        }
        /*
         * Requiere: Una hilera con la consulta a realizar.
         * Efectúa : Crea una conección con la base de datos hace la consulta a la base de datos, cierra la conección. Llena una tabla de datos con el resultado
         * Retorna : la tabla de datos con el resultado de la consulta.
         */
        internal DataTable consultar(String consultaSQL)
        {
            dt = new DataTable();
            adaptador.Open();
            OracleCommand comando = adaptador.CreateCommand();
            comando.CommandText = consultaSQL;
            OracleDataReader reader = comando.ExecuteReader();
            dt.Load(reader);           
            adaptador.Close();
            return dt;
        }
    }
}