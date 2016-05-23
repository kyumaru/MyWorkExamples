using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.OleDb;
using System.Data;
using System.Diagnostics;

namespace Servicios_Reservados_2
{
    public class ControladoraBDReservaciones
    {
        private DateTime fechaHoy;         
        private AdaptadorBD adaptador;
        DataTable dt;
    /*
     * Requiere: N/A
     * Efectúa : inicializa las variables globales de la clase
     * retorna : N/A
     */
        public ControladoraBDReservaciones()
        {
            adaptador = new AdaptadorBD();
            dt = new DataTable();
            fechaHoy = DateTime.Today;
            
        }
        /*
         * Requiere: N/A
         * Efectúa : Obtiene la fecha actual. Crea la consulta para obtener las cosultas activas con la fecha actual. Guarda en una tabla de datos el resultado a la consulta al adaptador.
         * Retorna : la tabla de datos con el resultado de la consulta.
         */
        internal DataTable consultarTodasReservaciones() {
            String fechaLocal = fechaHoy.ToString("MM/dd/yyyy");
            String consultaSQL = "select r.id, v.siglas, v.estacion, v.numero, c.nombre, v.entra, v.sale FROM reservas.vr_reservacion v JOIN reservas.reservacion r ON v.numero = r.numero JOIN reservas.contacto c ON r.solicitante = c.id WHERE v.sale >= TO_DATE('" + fechaLocal + "','MM/dd/yyyy') and  v.estado = 'CNF' order by sale asc"; 
            Debug.WriteLine(consultaSQL);
            dt = adaptador.consultar(consultaSQL);
            
            return dt;
        }
        /*
         * Requiere: una hilera con el identificador de reservación a consultar.
         * Efectúa : Crea la hilera de consulta concatenando el identificador. Guarda en una tabla de datos el resultado de la consulta que se hizo con el adaptador de base de datos.
         * retorna : La tabla de datos con los resultados de  la consulta.
         */
        internal DataTable consultarUnaReservacion(String id)
        {
            String consultaSQL = "select r.id, v.siglas, v.estacion, v.numero, c.nombre, v.entra, v.sale FROM reservas.vr_reservacion v JOIN reservas.reservacion r ON v.numero = r.numero JOIN reservas.contacto c ON r.solicitante = c.id WHERE r.id = '" + id + "' order by sale asc";
            dt = adaptador.consultar(consultaSQL);

            return dt;
        }

        /*
         * Requiere: N/A
         * Efectúa : Crea la hilera de consulta de las anfitrionas. Lo guarda en una tabla de datos 
         * retorna : La tabla de datos con los resultados de  la consulta.
         */
        internal DataTable solicitarAnfitriones(){
            String consultaSQL = "select  siglas from reservas.vr_reservacion group by siglas";
            dt = adaptador.consultar(consultaSQL);
            return dt;
        }
        /*
         * Requiere: N/A
         * Efectúa : Crea la hilera de consulta de las estaciones. Lo guarda en una tabla de datos 
         * retorna : La tabla de datos con los resultados de  la consulta.
         */
        internal DataTable solicitarEstaciones()
        {
            String consultaSQL = "select  estacion from reservas.vr_reservacion group by estacion";
            dt = adaptador.consultar(consultaSQL);
            return dt;
        }

        /*
         * Requiere: un identificador de reservación.
         * Efectúa : Crea la hilera de consulta de la información concatenándolo al identificador de la reservación. Crea una tabala de datos donde almacena el resultado de la consulta.
         * retorna : La tabla de datos con los resultados de  la consulta.
         */
        internal DataTable solicitarInfo(String id)
        {
            String consultaSQL = "select r.id, v.siglas, v.estacion, v.numero, c.nombre, v.entra, v.sale FROM reservas.vr_reservacion v JOIN reservas.reservacion r ON v.numero = r.numero JOIN reservas.contacto c ON r.solicitante = c.id WHERE r.id = '" + id + "' order by sale asc";
            dt = adaptador.consultar(consultaSQL);
            return dt;

        }

        /**Efecto: Crea la consulta SQL que obtiene el numero de pax de la reservacion y la retorna en forma de datatable  
         * Requiere: id de la reservacion
         * Modifica: el dataTable dt
         */
        internal DataTable obtenerPax(String idNum)
        {
            String consultaSQL = "select PAX from reservas.vr_reservacion where numero = '" + idNum + "'";
            dt = adaptador.consultar(consultaSQL);
            return dt;

        }

        /*
         * Requiere: Hilera con la Anfitriona seleccionada, Hilera con la estación seleccionada y una hilera con el solicitante. 
         * Efectúa : Crea la hilera de consulta a partir de los parámetros dados, valida uno a uno cuáles son diferentes de vacío y estos los agrega como parámetros a la consulta. Una vez creada la consulta, la efectúa con el adaptador y gurada el resultado en una tabla de datos.
         * retorna : La tabla de datos con los resultados de  la consulta.
         */
        internal DataTable consultarReservaciones(String anfitriona,String estacion, String solicitante){
            String fechaLocal = fechaHoy.ToString("MM/dd/yyyy");
            if (anfitriona.CompareTo("vacio") != 0 && estacion.CompareTo("vacio") != 0 && solicitante.CompareTo("vacio") != 0){
                String consultaSQL = "select r.id, v.siglas, v.estacion, v.numero, c.nombre, v.entra, v.sale FROM reservas.vr_reservacion v JOIN reservas.reservacion r ON v.numero = r.numero JOIN reservas.contacto c ON r.solicitante = c.id WHERE v.sale >= TO_DATE('" + fechaLocal + "','MM/dd/yyyy')  and v.siglas ='" + anfitriona + "' and v.estacion= '" + estacion + "' and LOWER(c.nombre) like '%" + solicitante + "%' and v.estado = 'CNF' order by sale asc";
                dt = adaptador.consultar(consultaSQL);
            }
            else if (anfitriona.CompareTo("vacio") != 0 && estacion.CompareTo("vacio") != 0 && solicitante.CompareTo("vacio") == 0)
            {
                String consultaSQL = "select r.id, v.siglas, v.estacion, v.numero, c.nombre, v.entra, v.sale FROM reservas.vr_reservacion v JOIN reservas.reservacion r ON v.numero = r.numero JOIN reservas.contacto c ON r.solicitante = c.id WHERE v.sale >= TO_DATE('" + fechaLocal + "','MM/dd/yyyy') and v.siglas ='" + anfitriona + "' and v.estacion= '" + estacion + "' and v.estado = 'CNF'  order by sale asc";
                dt = adaptador.consultar(consultaSQL);
            
            }
            else if (anfitriona.CompareTo("vacio") != 0 && estacion.CompareTo("vacio") == 0 && solicitante.CompareTo("vacio") != 0)
            {
                String consultaSQL = "select r.id, v.siglas, v.estacion, v.numero, c.nombre, v.entra, v.sale FROM reservas.vr_reservacion v JOIN reservas.reservacion r ON v.numero = r.numero JOIN reservas.contacto c ON r.solicitante = c.id WHERE v.sale >= TO_DATE('" + fechaLocal + "','MM/dd/yyyy') and v.siglas ='" + anfitriona + "' and LOWER(c.nombre) like '%" + solicitante + "%' and v.estado = 'CNF' order by sale asc";
                dt = adaptador.consultar(consultaSQL);
            }
            else if (anfitriona.CompareTo("vacio") == 0 && estacion.CompareTo("vacio") != 0 && solicitante.CompareTo("vacio") != 0)
            {
                String consultaSQL = "select r.id, v.siglas, v.estacion, v.numero, c.nombre, v.entra, v.sale FROM reservas.vr_reservacion v JOIN reservas.reservacion r ON v.numero = r.numero JOIN reservas.contacto c ON r.solicitante = c.id WHERE v.sale >= TO_DATE('" + fechaLocal + "','MM/dd/yyyy') and v.estacion= '" + estacion + "' and LOWER(c.nombre) like '%" + solicitante + "%' and v.estado = 'CNF' order by sale asc";
                dt = adaptador.consultar(consultaSQL);
            }
            else if (anfitriona.CompareTo("vacio") != 0 && estacion.CompareTo("vacio") == 0 && solicitante.CompareTo("vacio") == 0)
            {
                String consultaSQL = "select r.id, v.siglas, v.estacion, v.numero, c.nombre, v.entra, v.sale FROM reservas.vr_reservacion v JOIN reservas.reservacion r ON v.numero = r.numero JOIN reservas.contacto c ON r.solicitante = c.id WHERE v.sale >= TO_DATE('" + fechaLocal + "','MM/dd/yyyy') and v.siglas ='" + anfitriona + "' and v.estado = 'CNF' order by sale asc";
                dt = adaptador.consultar(consultaSQL);
            }
            else if (anfitriona.CompareTo("vacio") == 0 && estacion.CompareTo("vacio") != 0 && solicitante.CompareTo("vacio") == 0)
            {
                Debug.WriteLine("si calcula bien este paso");
                String consultaSQL = "select r.id, v.siglas, v.estacion, v.numero, c.nombre, v.entra, v.sale FROM reservas.vr_reservacion v JOIN reservas.reservacion r ON v.numero = r.numero JOIN reservas.contacto c ON r.solicitante = c.id WHERE v.sale >= TO_DATE('" + fechaLocal + "','MM/dd/yyyy') and v.estacion= '" + estacion + "' and v.estado = 'CNF' order by sale asc";
                dt = adaptador.consultar(consultaSQL);
            }

            else if (anfitriona.CompareTo("vacio") == 0 && estacion.CompareTo("vacio") == 0 && solicitante.CompareTo("vacio") != 0)
            {
                String consultaSQL = "select r.id, v.siglas, v.estacion, v.numero, c.nombre, v.entra, v.sale FROM reservas.vr_reservacion v JOIN reservas.reservacion r ON v.numero = r.numero JOIN reservas.contacto c ON r.solicitante = c.id WHERE v.sale >= TO_DATE('" + fechaLocal + "','MM/dd/yyyy') and  LOWER(c.nombre) like '%" + solicitante + "%' and v.estado = 'CNF' order by sale asc";
                dt = adaptador.consultar(consultaSQL);
            }

            else if (anfitriona.CompareTo("vacio") != 0 && estacion.CompareTo("vacio") != 0 && solicitante.CompareTo("vacio") == 0)
            {
                String consultaSQL = "select r.id, v.siglas, v.estacion, v.numero, c.nombre, v.entra, v.sale FROM reservas.vr_reservacion v JOIN reservas.reservacion r ON v.numero = r.numero JOIN reservas.contacto c ON r.solicitante = c.id WHERE v.sale >= TO_DATE('" + fechaLocal + "','MM/dd/yyyy') and v.siglas ='" + anfitriona + "' and v.estacion= '" + estacion + "' and v.estado = 'CNF' order by sale asc";
                dt = adaptador.consultar(consultaSQL);
            }
           
            return dt;
        }
        
    }
}

