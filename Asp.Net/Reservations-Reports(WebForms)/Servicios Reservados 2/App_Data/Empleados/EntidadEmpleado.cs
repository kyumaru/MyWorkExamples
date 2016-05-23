using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Servicios_Reservados_2
{
    public class EntidadEmpleado
    {
        private String id;
        private String nombre;
        private String apellido;
         public EntidadEmpleado(String id, String nombre, String apellido)
        {
            Id = id;
            Nombre = nombre;
            Apellido = apellido;
            
                
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
        public String Nombre {
            get { return nombre; }
            set { nombre = value; }
        }
        /*
         * Requiere: una Hilera con el valor nuevo
         * Efectúa : Asigna a la variable global Estación con el parámetro
         * Retorna : El valor de la variable global Estación
         */
        public String Apellido {
            get { return apellido; } 
            set { apellido = value;}
        }
    }
}