using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Gyoza.DataSet1TableAdapters;
using System.IO;
using System.Configuration;


namespace Gyoza.Modulo_Requerimientos
{
    public class ControladoraBDRequerimientos
    {
        RequerimientoTableAdapter adaptadorRequerimientos;
        //TestTableAdapter adaptadorArchivos;
        Archivo_AsociadoTableAdapter adaptadorArchivos;
        criterio_aceptacionTableAdapter adaptadorCA;
        IteracionTableAdapter adaptadorI;
        ModuloTableAdapter adaptadorM;

        public ControladoraBDRequerimientos()
        {
            adaptadorRequerimientos = new RequerimientoTableAdapter();
            //adaptadorArchivos = new TestTableAdapter();
            adaptadorArchivos= new Archivo_AsociadoTableAdapter();
            adaptadorCA = new criterio_aceptacionTableAdapter();
            adaptadorI = new IteracionTableAdapter();
            adaptadorM = new ModuloTableAdapter();
        }

        //tryout inserting files into DB
        //inserts file at varFilePath into DB as a varbinary, this uses a table adapter(adaptadorArchivos) way to comunicate with the DB, the
        //other way is by .net's SqlCommand see getFileRead as example.
        //by lightWizard kyumaru
        /*select*from Archivo_Asociado
        insert into Archivo_Asociado values(5,'Comprar armas','nombreArchivo',null)*/
        
        public void insertFile(string varFilePath, int proyectId, string reqName, string fileName )
        {
            byte[] file;
            using (var stream = new FileStream(varFilePath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = new BinaryReader(stream))
                {
                    file = reader.ReadBytes((int)stream.Length);
                }
            }
            adaptadorArchivos.Insert(proyectId,reqName,fileName,file);
        }
        
//final version of insert file, function overload
        public String[] insertarArchivos(DataTable archivos)
        {
            /*
            res[2] = archivo.FileName;

            try
            {
                byte[] file;
                using (var stream = new FileStream(archivo.Path, FileMode.Open, FileAccess.Read))
                {
                    using (var reader = new BinaryReader(stream))
                    {
                        file = reader.ReadBytes((int)stream.Length);
                    }
                }
                adaptadorArchivos.Insert(archivo.ProyectId, archivo.ReqName,archivo.FileName, file);
                
                res[0] = "Exito ";
                res[1] = "El archivo fue agregado al requerimiento";
            }
            catch (SqlException e)
            {
           
                res[0] = "Error ";
                res[1] = "Fallo en la operación agregar archivo";
                
            }
            */
            String[] resultado = new String[3];

            try
            {
                adaptadorArchivos.insertarArchivos(archivos);

                resultado[0] = "success";
                resultado[1] = "Exito. ";
                resultado[2] = "Los archivos fueron agregados al requerimiento";
            }
            catch (Exception e)
            {
                resultado[0] = "danger";
                resultado[1] = "Error. ";
                resultado[2] = "Fallo en la operación agregar archivos";
            }

            return resultado;
        
        }

        //downloads a varbinary file stored int the DB into the path varPathToNewLocation, notice the file´s name is part of it, use it
        //to define extension of the file, .txt, .pdf
        //notice that var is a powerful template variable type that may store anything but only in local scope
        //by lightWizard kyumaru
        public void getFileRead(string varID, string varPathToNewLocation)
        {
            //the code below does not work for this verison of .net
            //using (var varConnection = Locale.sqlConnectOneTime(Locale.sqlDataConnectionDetails))

            //using this instead, all details about this connection to DB is in web.config, this needs the connection name
            //"ingegsjuanConnectionString"
            string connectionString = ConfigurationManager.ConnectionStrings["ingegsjuanConnectionString"].ConnectionString;
           
            using (SqlConnection connection = new SqlConnection(connectionString))//creates the connection to the DB where query will execute

            using (var sqlQuery = new SqlCommand(@"SELECT [archivo] FROM [dbo].[test] WHERE [Id] = @varID", connection))//this is the format of querys on sql server,@varID is a query variable
            {
               connection.Open();//en todas bichiyo//connection should be openned else it will complain it is closed
                
                sqlQuery.Parameters.AddWithValue("@varID", varID);//ads the query variable to the final query that will execute
                using (var sqlQueryResult = sqlQuery.ExecuteReader())//reads the file into a var called blob
                    if (sqlQueryResult != null)
                    {
                        sqlQueryResult.Read();
                        var blob = new Byte[(sqlQueryResult.GetBytes(0, 0, null, 0, int.MaxValue))];
                        sqlQueryResult.GetBytes(0, 0, blob, 0, blob.Length);
                        using (var fs = new FileStream(varPathToNewLocation, FileMode.Create, FileAccess.Write))
                            fs.Write(blob, 0, blob.Length);
                    }
                connection.Close();//conection to DB should be closed after work is done 
     
            }
        }//method end



        //consulta para una fila especifica de un archivo asociado
        /*
            SELECT [Nombre] 
            FROM [dbo].[Archivo_Asociado] 
            WHERE [IdProyecto] = 5 AND [NombreRequerimiento] = 'Comprar armas' AND [Nombre] = 'new.png'
         */
        //final version of getFileRead, function overload
        public void getFileRead(string varID,string nombreReq, string nombreFile, string varPathToNewLocation)
        {
            //the code below does not work for this verison of .net
            //using (var varConnection = Locale.sqlConnectOneTime(Locale.sqlDataConnectionDetails))

            //using this instead, all details about this connection to DB is in web.config, this needs the connection name
            //"ingegsjuanConnectionString"
            string connectionString = ConfigurationManager.ConnectionStrings["ingegsjuanConnectionString"].ConnectionString;//it should receive connection name by paramater to make it universal

            using (SqlConnection connection = new SqlConnection(connectionString))//creates the connection to the DB where query will execute

            using (var sqlQuery = new SqlCommand(@"SELECT [Contenido] FROM [dbo].[Archivo_Asociado] WHERE [IdProyecto] = @varID AND [NombreRequerimiento] = @nombreReq AND [Nombre] = @nombreFile", connection))//this is the format of querys on sql server,@varID is a query variable
            {
                connection.Open();//en todas bichiyo//connection should be openned else it will complain it is closed

                sqlQuery.Parameters.AddWithValue("@varID", varID);//ads the query variable to the final query that will execute
                sqlQuery.Parameters.AddWithValue("@nombreReq", nombreReq);//ads the query variable to the final query that will execute
                sqlQuery.Parameters.AddWithValue("@nombreFile", nombreFile);//ads the query variable to the final query that will execute

                using (var sqlQueryResult = sqlQuery.ExecuteReader())//reads the file into a var called blob
                    if (sqlQueryResult != null)
                    {
                        sqlQueryResult.Read();
                        var blob = new Byte[(sqlQueryResult.GetBytes(0, 0, null, 0, int.MaxValue))];
                        sqlQueryResult.GetBytes(0, 0, blob, 0, blob.Length);
                        using (var fs = new FileStream(varPathToNewLocation, FileMode.Create, FileAccess.Write))
                            fs.Write(blob, 0, blob.Length);
                    }
                connection.Close();//conection to DB should be closed after work is done 

            }
        }//method end



        public DataTable consultarRequerimientoParticular(string modulo, int iteracion, int idProyecto)
        {
            DataTable resultado = new DataTable();

            try
            {
                resultado = adaptadorRequerimientos.consultarRequerimientoParticular(modulo, iteracion, idProyecto);
            }
            catch (Exception e)
            {
                resultado = null;
            }
            return resultado;
        }



        public String[] insertarRequerimiento(EntidadRequerimiento nuevoRequerimiento)
        {
            String[] res = new String[4];
            res[3] = nuevoRequerimiento.Nombre;
            try
            {
                adaptadorRequerimientos.Insert(nuevoRequerimiento.IdProyecto, nuevoRequerimiento.Nombre ,nuevoRequerimiento.Prioridad,
                    nuevoRequerimiento.Estado, nuevoRequerimiento.Rol, nuevoRequerimiento.Contenido, nuevoRequerimiento.Razon, nuevoRequerimiento.Estimacion,
                    nuevoRequerimiento.IdProyecto, nuevoRequerimiento.Sprint, nuevoRequerimiento.Modulo, nuevoRequerimiento.Funcional, nuevoRequerimiento.IdEncargado1, nuevoRequerimiento.IdEncargado2);
                res[0] = "success";
                res[1] = "Exito";
                res[2] = "Requerimiento agregado";
            }
            catch (SqlException e)
            {
                res[0] = "danger";
                res[1] = "Fallo en la operacion";
                res[2] = "Intente nuevamente";
            }
            return res;
        }

        public String[] modificarRequerimiento(EntidadRequerimiento nuevoRequerimiento, EntidadRequerimiento viejoRequerimiento)
        {
            String[] resultado = new String[3];
             try
             {
                 this.adaptadorRequerimientos.Update(nuevoRequerimiento.IdProyecto, nuevoRequerimiento.Nombre, nuevoRequerimiento.Prioridad,
                    nuevoRequerimiento.Estado, nuevoRequerimiento.Rol, nuevoRequerimiento.Contenido, nuevoRequerimiento.Razon, nuevoRequerimiento.Estimacion,
                    nuevoRequerimiento.IdProyecto, nuevoRequerimiento.Sprint, nuevoRequerimiento.Modulo, nuevoRequerimiento.Funcional, nuevoRequerimiento.IdEncargado1, nuevoRequerimiento.IdEncargado2,
                        viejoRequerimiento.IdProyecto, viejoRequerimiento.Nombre, viejoRequerimiento.Prioridad,
                    viejoRequerimiento.Estado, viejoRequerimiento.Rol, viejoRequerimiento.Contenido, viejoRequerimiento.Razon, viejoRequerimiento.Estimacion,
                    viejoRequerimiento.IdProyecto, viejoRequerimiento.Sprint, viejoRequerimiento.Modulo, viejoRequerimiento.Funcional, viejoRequerimiento.IdEncargado1, viejoRequerimiento.IdEncargado2);

                 resultado[0] = "success";
                 resultado[1] = "Éxito. ";
                 resultado[2] = "El requerimiento se ha modificado correctamente";

             }
             catch{
                 resultado[0] = "danger";
                 resultado[1] = "Error. ";
                 resultado[2] = "No se pudo modificar el requerimiento";
             }
            return resultado;
        }

        public String[] eliminarRequerimiento(EntidadRequerimiento requerimientoEliminar)
        {
            String[] resultado = new String[3];
            
            try
            {
                adaptadorRequerimientos.Delete(requerimientoEliminar.IdProyecto, requerimientoEliminar.Nombre, requerimientoEliminar.Prioridad, requerimientoEliminar.Estado,
                    requerimientoEliminar.Rol, requerimientoEliminar.Contenido, requerimientoEliminar.Razon, requerimientoEliminar.Estimacion, requerimientoEliminar.IdProyecto,
                    requerimientoEliminar.Sprint, requerimientoEliminar.Modulo, requerimientoEliminar.Funcional, requerimientoEliminar.IdEncargado1, requerimientoEliminar.IdEncargado2);

                resultado[0] = "success";
                resultado[1] = "Exito. ";
                resultado[2] = "Se eliminó exitosamente el requerimiento";

            }
            catch (Exception e)
            {
                resultado[0] = "danger";
                resultado[1] = "Error. ";
                resultado[2] = "No se pudo eliminar el requerimiento";

            }
            return resultado;
        }

        public EntidadRequerimiento consultarRequerimiento( int idProyecto, String nombreProyecto )
        {
            EntidadRequerimiento proyectoConsultado = null;
            DataTable resultado = new DataTable();
            Object[] datosConsultados = new Object[13];

            try
            {
                resultado = adaptadorRequerimientos.consultarRequerimiento(idProyecto, nombreProyecto);

                if (resultado.Rows.Count == 1)
                {
                    for (int i = 0, j = 0; i < 14; i++)
                    {
                        // obtiene los atributos y los guarda en datosConsultados
                        if( i != 8 )
                        {
                            datosConsultados[j] = resultado.Rows[0][i].ToString();
                            ++j;
                        }
                            
                    }

                    //Se encapsulan los datos utilizando la clase entidadProveedor
                    proyectoConsultado = new EntidadRequerimiento(datosConsultados);
                }
            }
            catch (Exception e) { }

            return proyectoConsultado;
        }

        public DataTable consultarRequerimientos( int idProyecto )
        {
            DataTable resultado = new DataTable();

            try
            {
                resultado = adaptadorRequerimientos.consultarRequerimientosProyecto(idProyecto);
            }
            catch (Exception e)
            {
                resultado = null;
            }
            return resultado;
        }

        //devuelve un datatable con la tabla de archivos asociados a un idProyecto, nombreReq, ojo q el nombre esta en la columna 3
        public DataTable consultarArchivos(int idProyecto, string nombreReq)
        {
            DataTable resultado = new DataTable();

            try
            {
                resultado = adaptadorArchivos.consultarArchivosAsociadosRequerimiento(idProyecto,nombreReq);
            }
            catch (Exception e)
            {
                resultado = null;
            }
            return resultado;
        }

        public String[] agregarCriteriosAceptacion( DataSet1.criterio_aceptacionDataTable datos )
        {
            String[] resultado = new String[3];

            try
            {
                adaptadorCA.insertarCriterios(datos);

                resultado[0] = "success";
                resultado[1] = "Exito. ";
                resultado[2] = "Agregados existosamente los criterios de aceptación y el requerimiento";
            }
            catch (Exception e)
            {
                resultado[0] = "danger";
                resultado[1] = "Error. ";
                resultado[2] = "No se pudo eliminar agregar los criterios de aceptación";
            }

            return resultado;
        }

        public DataTable consultarCriterios( int idProyecto, String nombreRequerimiento )
        {
            return adaptadorCA.obtenerCriteriosRequerimiento(idProyecto, nombreRequerimiento);
        }


        // Modulos e Iteraciones
        public DataTable consultarIteraciones(int idProyecto)
        {
            return adaptadorI.consultarIteracionesProyecto(idProyecto);
        }

        public DataTable consultarModulos(int idProyecto, int numeroIteracion)
        {
            return adaptadorM.consultarModulosIteracion(idProyecto, numeroIteracion);
        }

        public bool insertarIteracion(int idProyecto)
        {
            try
            {
                adaptadorI.Insert(idProyecto, (int)(adaptadorI.generarIteracion(idProyecto)));
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool insertarModulo(int idProyecto, int numeroIteracion, String nombreModulo)
        {
            try
            {
                adaptadorM.Insert(idProyecto,numeroIteracion,nombreModulo);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool eliminarIteracion( int idProyecto, int numeroIteracion )
        {
            try
            {
                adaptadorI.Delete(idProyecto, numeroIteracion);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool eliminarModulo(int idProyecto, int numeroIteracion, String nombreModulo)
        {
            try
            {
                adaptadorM.Delete(idProyecto, numeroIteracion, nombreModulo);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public DataTable getRequerimientosPorEncargado(int idProyecto) {
            DataTable resultado;
            try {
                resultado = adaptadorRequerimientos.obtenerRequerimientosConEncargado(idProyecto);
            }
            catch (Exception e)
            {
                resultado = null;
            }
            return resultado;
        }
    }
}