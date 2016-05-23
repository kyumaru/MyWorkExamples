using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient; //this is for sql dbs no for oracle https://msdn.microsoft.com/en-us/library/system.data.sqlclient%28v=vs.110%29.aspx
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.OracleClient;//this library or equivalent is needed for oracle https://msdn.microsoft.com/en-us/library/system.data.oracleclient%28v=vs.110%29.aspx


// connet to oracle database https://msdn.microsoft.com/en-us/library/92ceczx1%28v=vs.85%29.aspx

/***************EXAMPLE****************/
/*
 https://msdn.microsoft.com/en-us/library/system.data.oracleclient.oracledatareader(v=vs.110).aspx
 
*/



namespace Servicios_Reservados_2
{
    public class controladoraBD
    {


        public String test(string varPadre)
        {
            //the code below does not work for this verison of .net
            //using (var varConnection = Locale.sqlConnectOneTime(Locale.sqlDataConnectionDetails))

            //using this instead, all details about this connection to DB is in web.config, this needs the connection name
            //"ingegsjuanConnectionString"
            string connectionString = ConfigurationManager.ConnectionStrings["ora"].ConnectionString;

            using (OracleConnection connection = new OracleConnection(connectionString))//creates the connection to the DB where query will execute

            using (var sqlQuery = new OracleCommand("SELECT * FROM alimento", connection))//this is the format of querys on sql server,@varID is a query variable
            {
                connection.Open();//semms reopening connection is making issues//connection should be openned else it will complain it is closed

                OracleDataReader reader = sqlQuery.ExecuteReader();
                reader.Read();// https://msdn.microsoft.com/en-us/library/system.data.oracleclient.oracledatareader.read(v=vs.110).aspx

                var x = "";

                while (reader.Read()) {
                    x+=reader.GetOracleValue(0);
                }
              
                connection.Close();//conection to DB should be closed after work is done 

                return x;
            }
        }//method end






    }
}