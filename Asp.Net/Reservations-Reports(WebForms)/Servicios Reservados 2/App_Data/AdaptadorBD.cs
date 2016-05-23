using System;
using System.Collections.Generic;
using System.Web;
using System.Diagnostics;
using System.Data;
using System.Data.OracleClient;

namespace Servicios_Reservados_2
{
    public class AdaptadorBD
    {

        OracleConnection adaptador= new OracleConnection();
        DataTable dt;

        /*
         * Requiere: N/A
         * Efectúa : Crea la hilera de conección con la base de datos.
         * Retorna : N/A
         */
        public AdaptadorBD()
        {
             adaptador.ConnectionString = "Data Source=10.1.4.93:1521/XE;User ID=servicios_reservados;Password=servicios;Unicode=True";   
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

        /*
         * Efecto: ejecuta la consultaSQL en la base de datos.
         * Requiere: la entrada de la consulta.
         * Modifica: la base de datos.
         */
        internal String[] insertar(String consultaSQL)
        {
            String[] respuesta = new String[3];
            try
            {
                adaptador.Open();
                OracleCommand comando = new OracleCommand(consultaSQL, adaptador);
                comando.ExecuteNonQuery();
                adaptador.Close();

                respuesta[0] = "success";
                respuesta[1] = "Exito. ";
                respuesta[2] = "Se ha eliminado exitosamente";
            }
            catch (OracleException e)
            {
                int r = e.ErrorCode;

                if (r == -2147217873)
                {

                    respuesta[0] = "danger";
                    respuesta[1] = "Error. ";
                    respuesta[2] = "La informacion ingresada ya existe";
                }
                else
                {

                    respuesta[0] = "danger";
                    respuesta[1] = "Error. ";
                    respuesta[2] = "No se pudo agregar";
                }
            }
            return respuesta;

        }
    }
}

