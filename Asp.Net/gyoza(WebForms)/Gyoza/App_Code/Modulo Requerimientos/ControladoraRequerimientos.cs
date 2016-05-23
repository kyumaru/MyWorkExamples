using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Gyoza.Modulo_Cuenta;


namespace Gyoza.Modulo_Requerimientos
{
    public class ControladoraRequerimientos
    {
        ControladoraBDRequerimientos controladoraBDRequerimientos;
        ControladoraCuentas controladoraCuenta;
        

        public ControladoraRequerimientos()
        {
            controladoraBDRequerimientos = new ControladoraBDRequerimientos();
            controladoraCuenta = new ControladoraCuentas();
        }

        //this test funtion gets called by .cs of the req interfase at page loading
        //tryout inserting and getting files into_out DB

        public void saveFfileOnDrive(string proyectId, string reqName,string fileName, string path)
        {
            controladoraBDRequerimientos.getFileRead(proyectId, reqName, fileName, path);
        }
        public void testArchivo()
        {
            //controladoraBDRequerimientos.getFileRead("8", "C:\\Users\\b12422\\Desktop\\new.pdf");
           //controladoraBDRequerimientos.insertFile("C:\\Users\\b12422\\Desktop\\new.png",5,"Comprar armas","new.png");
            /*
            Object[] datos = new Object[4];
            datos[0] = 5;
            datos[1] = "Comprar armas";
            datos[2] = "new1.png";
            datos[3] = "C:\\Users\\b12422\\Desktop\\new1.png";

            insertarArchivoAsociado(datos);
            */

            //controladoraBDRequerimientos.getFileRead("5", "Comprar armas", "new1.png", "C:\\Users\\b12422\\Desktop\\new1.png");

            //consultarArchivos(5, "Comprar armas");

         }

        public String[] insertarArchivosAsociados( DataTable archivos )
        {
            return controladoraBDRequerimientos.insertarArchivos(archivos);            
        }



        public String[] insertarRequerimiento(Object[] datos)
        {
            EntidadRequerimiento cuenta = new EntidadRequerimiento(datos);

            return controladoraBDRequerimientos.insertarRequerimiento(cuenta);
        }

        public String[] modificarRequerimiento(Object[] datosNuevos, EntidadRequerimiento viejoRequerimiento)
        {
            EntidadRequerimiento nuevoRequerimiento = new EntidadRequerimiento(datosNuevos);

            return controladoraBDRequerimientos.modificarRequerimiento(nuevoRequerimiento, viejoRequerimiento);
        }

        public String[] eliminarRequerimiento(EntidadRequerimiento requerimientoEliminar)
        {

            return controladoraBDRequerimientos.eliminarRequerimiento(requerimientoEliminar);
        }

        public EntidadRequerimiento consultarRequerimiento( int idProyecto, String nombreProyecto )
        {
            return controladoraBDRequerimientos.consultarRequerimiento(idProyecto, nombreProyecto);
        }

        public DataTable consultarRequerimientos( int idProyecto )
        {
            return controladoraBDRequerimientos.consultarRequerimientos(idProyecto);
        }


        public DataTable consultarArchivos(int idProyecto, string nombreReq)
        {
            return controladoraBDRequerimientos.consultarArchivos(idProyecto,nombreReq);
        }


        public DataTable obtenerCNAMiembrosAsociados(int id)
        {
            return controladoraCuenta.obtenerCNAMiembrosAsociados(id);
        }

        public DataTable consultarRequerimientoParticular(string modulo, int iteracion, int idProyecto)
        {
            return controladoraBDRequerimientos.consultarRequerimientoParticular(modulo, iteracion, idProyecto);
        }

        public String[] agregarCriteriosAceptacion(int idProyecto, String nombreRequerimiento, DataTable datos)
        {

            DataSet1.criterio_aceptacionDataTable tabla = new DataSet1.criterio_aceptacionDataTable();
            foreach( DataRow row in datos.Rows )
            {
                Object[] nuevo = new Object[4];
                nuevo[0] = idProyecto;
                nuevo[1] = nombreRequerimiento;
                if (!row[0].Equals("-")) {
                     nuevo[2] = row[0];
                     nuevo[3] = row[1];
                     tabla.Rows.Add(nuevo);
                }
            }

            return  controladoraBDRequerimientos.agregarCriteriosAceptacion(tabla);
        }

        public DataTable consultarCriterios(int idProyecto, String nombreRequerimiento)
        {
            return controladoraBDRequerimientos.consultarCriterios(idProyecto, nombreRequerimiento);
        }



        // Modulos e Iteraciones
        public DataTable consultarIteraciones(int idProyecto)
        {
            return controladoraBDRequerimientos.consultarIteraciones(idProyecto);
        }

        public DataTable consultarModulos(int idProyecto, int numeroIteracion)
        {
            return controladoraBDRequerimientos.consultarModulos(idProyecto, numeroIteracion);
        }

        public bool insertarIteracion(int idProyecto)
        {
            return controladoraBDRequerimientos.insertarIteracion(idProyecto);
        }

        public bool insertarModulo(int idProyecto, int numeroIteracion, String nombreModulo)
        {
            return controladoraBDRequerimientos.insertarModulo(idProyecto, numeroIteracion, nombreModulo);
        }

        public bool eliminarIteracion(int idProyecto, int numeroIteracion)
        {
            return controladoraBDRequerimientos.eliminarIteracion(idProyecto, numeroIteracion);
        }

        public bool eliminarModulo(int idProyecto, int numeroIteracion, String nombreModulo)
        {
            return controladoraBDRequerimientos.eliminarModulo(idProyecto, numeroIteracion, nombreModulo);
        }
    }
}