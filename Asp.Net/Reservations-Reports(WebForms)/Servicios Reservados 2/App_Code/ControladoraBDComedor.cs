using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Diagnostics;
using Oracle.DataAccess.Client; //this the non deprecated version library or equivalent is needed for oracle https://msdn.microsoft.com/en-us/library/system.data.oracleclient%28v=vs.110%29.aspx
using System.IO;




namespace Servicios_Reservados_2
{
    public class ControladoraBDComedor
    {


        internal Object consultarNotasTiquete(string numero)
        {


            String connectionString = "Data Source=10.1.4.93;User ID=grupo01;Password=servicios123;Unicode=True";

            String consultaSQL = "select concat(concat(c.nombre,' '),c.apellidos) as nombreCliente ,q4.siglas as anfitriona ,q4.nombre as estacion ,q4.consumido,q4.notas from(select q3.solicitante,nombre,q3.notas,q3.siglas,q3.consumido from(select q2.solicitante,q2.estacion,notas,siglas,q2.consumido from ( select q1.idreservacion,q1.solicitante,q1.estacion,notas,q1.anfitriona, rp.consumido from (select idreservacion,solicitante,estacion,notas, anfitriona from pax join reservacion on pax.idreservacion=reservacion.id and idcliente=" + numero + ")q1 join reservado_pax rp on q1.idreservacion=rp.idreservacion )q2 join anfitriona a on a.id=q2.anfitriona )q3 join estacion e on e.id=q3.estacion)q4 join contacto c on c.id=q4.solicitante";



            /*string connectionString = ConfigurationManager.ConnectionStrings["ora"].ConnectionString;*/

            using (OracleConnection connection = new OracleConnection(connectionString))//creates the connection to the DB where query will execute

            using (var sqlQuery = new OracleCommand(consultaSQL, connection))//this is the format of querys on sql server,@varID is a query variable
            {
                connection.Open();//semms reopening connection is making issues//connection should be openned else it will complain it is closed

                OracleDataReader reader = sqlQuery.ExecuteReader();
                reader.Read();// https://msdn.microsoft.com/en-us/library/system.data.oracleclient.oracledatareader.read(v=vs.110).aspx

                Object[] contenedor = new Object[5];

                while (reader.Read())
                {
                    contenedor[0] = reader.GetOracleValue(0);
                    contenedor[1] = reader.GetOracleValue(1);
                    contenedor[2] = reader.GetOracleValue(2);
                    contenedor[3] = reader.GetOracleValue(3);
                    contenedor[4] = reader.GetOracleValue(4);
                }

                connection.Close();//conection to DB should be closed after work is done 

                return contenedor;
            }

        }//method end
    }
}