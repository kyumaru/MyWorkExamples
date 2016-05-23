using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Servicios_Reservados_2
{
    public class EntidadReservaciones
    {
        private String id;
        private String anfitriona;
        private String estacion;
        private String numero;
        private String solicitante;
        private DateTime fechaInicio;
        private DateTime fechaSalida;
        /*
         * Requiere: Un identificador de reservacion, una hilera con la anfritriona, una hilera con la estación, una hilera con el número, una hilera con el solicitante, una fecha con el inicio y una fecha con la salida
         * Efectúa : Inicializa las variables globales de la clase con los parámetros.
         * Retorna : N/A.
         */
        public EntidadReservaciones(String id, String anfitriona, String estacion, String numero, String solicitante, DateTime fechaInicio, DateTime fechaSalida)
        {
            Id = id;
            Anfitriona = anfitriona;
            Estacion = estacion;
            Numero = numero;
            Solicitante = solicitante;
            FechaInicio = fechaInicio;
            FechaSalida = fechaSalida;
                
        } 
        /*
         * Requiere: una Hilera con el valor nuevo
         * Efectúa : Asigna a la variable global id con el parámetro
         * Retorna : El valor de la variable global id
         */
        public String Id {
            get { return id; } 
            set { id = value;}
        }
        /*
         * Requiere: una Hilera con el valor nuevo
         * Efectúa : Asigna a la variable global Anftritriona con el parámetro
         * Retorna : El valor de la variable global Anfitriona
         */
        public String Anfitriona {
            get { return anfitriona; }
            set { anfitriona = value; }
        }
        /*
         * Requiere: una Hilera con el valor nuevo
         * Efectúa : Asigna a la variable global Estación con el parámetro
         * Retorna : El valor de la variable global Estación
         */
        public String Estacion {
            get { return estacion; } 
            set { estacion = value;}
        }
        /*
         * Requiere: una Hilera con el valor nuevo
         * Efectúa : Asigna a la variable global Numero con el parámetro
         * Retorna : El valor de la variable global Numero
         */
        public String Numero
        {
            get { return numero; }
            set { numero = value; }
        }
        /*
         * Requiere: una Fecha con el valor nuevo
         * Efectúa : Asigna a la variable global FechaInicio con el parámetro
         * Retorna : El valor de la variable global FechaInicio
         */
        public DateTime FechaInicio {
            get { return fechaInicio; } 
            set { fechaInicio = value;}
        }
        /*
         * Requiere: una Fecha con el valor nuevo
         * Efectúa : Asigna a la variable global FechaSalida con el parámetro
         * Retorna : El valor de la variable global FechaSalida
         */
        public DateTime FechaSalida {
            get { return fechaSalida; } 
            set { fechaSalida = value;}
        }
        /*
         * Requiere: una Hilera con el valor nuevo
         * Efectúa : Asigna a la variable global Solicitante con el parámetro
         * Retorna : El valor de la variable global Solicitante
         */
        public String Solicitante {
            get { return solicitante; } 
            set { solicitante = value;}
        }
    }
   

}