using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace Servicios_Reservados_2
{
    public class ControladoraReservaciones
    {
        private static EntidadReservaciones reservacionSeleccionada;
        private static ControladoraBDReservaciones controladoraBD;
        /*
         * Requiere: N/A
         * Efectúa : Inicializa las variables globales de la clase. 
         * Retorna : N/A
         */
        public ControladoraReservaciones()
        {
            controladoraBD = new ControladoraBDReservaciones();
        }
        /*
         * Requiere: N/A
         * Efectúa : Pide a la controladora de base de datos la información de todas las reservaciones y las guarda en una tabla de datos. 
         * Retorna : la tabla de datos que se crea.
         */
        internal DataTable solicitarTodasReservaciones()
        {
            DataTable todas = controladoraBD.consultarTodasReservaciones();

            return todas;

        }
        /*
         * Requiere: N/A
         * Efectúa : Pide a la controladora de base de datos la información de todas las anfitrionas y las guarda en una tabla de datos. 
         * Retorna : la tabla de datos que se crea.
         */
        internal DataTable solicitarAnfitriones()
        {
            DataTable anfitrion = controladoraBD.solicitarAnfitriones();
            return anfitrion;
        }
        /*
         * Requiere: N/A
         * Efectúa : Pide a la controladora de base de datos la información de todas las reservaciones y las guarda en una tabla de datos. 
         * Retorna : la tabla de datos que se crea.
         */
        internal DataTable solicitarEstaciones()
        {
            DataTable estacion = controladoraBD.solicitarEstaciones();
            return estacion;
        }
        /*
         * Requiere: Una hilera con la anfitriona, la estación y solicitante.
         * Efectúa : Pide a la controladora de base de datos la información de todas las reservaciones con los parámetros que reicibió y las guarda en una tabla de datos. 
         * Retorna : la tabla de datos que se crea.
         */
        internal DataTable consultarReservaciones(String anfitriona, String estacion, String solicitante)
        {
            
            DataTable reservaciones = controladoraBD.consultarReservaciones(anfitriona, estacion, solicitante);
            return reservaciones;
        }

        /*
         * Requiere: Un identificador de reservacion.
         * Efectúa : Pide a la controladora de base de datos la información de la reservación específica, extre los datos de la tabla de datos y la encapsula. Guarda la entidad encapsulada en la variable global de la clase reservacionSeleccionada
         * Retorna : N/A.
         */
        internal void seleccionarReservacion(String id)
        {
            DataTable reservacion = controladoraBD.consultarUnaReservacion(id);

            String anfitriona = reservacion.Rows[0][1].ToString();
            String estacion = reservacion.Rows[0][2].ToString();
            String numero = reservacion.Rows[0][3].ToString();
            String solicitante = reservacion.Rows[0][4].ToString();
            DateTime fechaInicio = DateTime.Parse(reservacion.Rows[0][5].ToString());
            DateTime fechaSalida = DateTime.Parse(reservacion.Rows[0][6].ToString());

          reservacionSeleccionada = new EntidadReservaciones(id, anfitriona, estacion, numero, solicitante, fechaInicio, fechaSalida);
        }
        /*
         * Requiere: N/A.
         * Efectúa : N/A
         * Retorna : la vaible glbal de la clase reservacionSeleccionada.
         */
        public EntidadReservaciones getReservacionSeleccionada() {
            return reservacionSeleccionada;
        }
        /*
         * Requiere: Un identificador de reservacion.
         * Efectúa : Pide a la controladora de base de datos la información de la reservación específica y lo guarda en una tabla de datos.
         * Retorna : La tabla de datos con el resultado.
         */
        internal DataTable solicitarInfo(String id)
        {
            DataTable reservInfo = controladoraBD.solicitarInfo(id);
            return reservInfo;


        }

        public String obtenerPax(String id)
        {
            DataTable paxConsultado = controladoraBD.obtenerPax(id);
            String pax = paxConsultado.Rows[0][0].ToString();
            return pax;

        }
    }
}