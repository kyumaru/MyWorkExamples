using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Servicios_Reservados_2
{
    public class ControladoraEmpleado
    {
        private static EntidadEmpleado empleadoSeleccionado;
        private static ControladoraBDEmpleado controladoraBD;
        /*
         * Requiere: N/A
         * Efectúa : Inicializa las variables globales de la clase. 
         * Retorna : N/A
         */
        public ControladoraEmpleado()
        {
            controladoraBD = new ControladoraBDEmpleado();
        }
        /*
         * Requiere: N/A
         * Efectúa : Pide a la controladora de base de datos la información de todas las reservaciones y las guarda en una tabla de datos. 
         * Retorna : la tabla de datos que se crea.
         */
        internal DataTable solicitarTodosEmpleados()
        {
            DataTable todas = controladoraBD.consultarTodosEmpleados();
            return todas;
        }
       
        /*
         * Requiere: Una hilera con la anfitriona, la estación y solicitante.
         * Efectúa : Pide a la controladora de base de datos la información de todas las reservaciones con los parámetros que reicibió y las guarda en una tabla de datos. 
         * Retorna : la tabla de datos que se crea.
         */
        internal DataTable consultarEmpleados(String nombre, String iden)
        {

            DataTable empleados = controladoraBD.consultarEmpleados(nombre, iden);
            return empleados;
        }

        /*
         * Requiere: Un identificador de reservacion.
         * Efectúa : Pide a la controladora de base de datos la información de la reservación específica, extre los datos de la tabla de datos y la encapsula. Guarda la entidad encapsulada en la variable global de la clase reservacionSeleccionada
         * Retorna : N/A.
         */
        internal void seleccionarEmpleado(String id)
        {
            DataTable empleado = controladoraBD.consultarUnEmpleado(id);

            String nombre = empleado.Rows[0][2].ToString();
            String apellido = empleado.Rows[0][3].ToString();
            
          empleadoSeleccionado = new EntidadEmpleado(id, nombre, apellido);
        }
        /*
         * Requiere: N/A.
         * Efectúa : N/A
         * Retorna : 
         */
        public EntidadEmpleado getEmpleadoSeleccionado() {
            return empleadoSeleccionado;
        }

    }
}