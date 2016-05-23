using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.OleDb;
using System.Data;
using System.Diagnostics;

namespace Servicios_Reservados_2
{
    public class ControladoraBDTiquete
    {
        private AdaptadorBD adaptador;
        DataTable dt;
        /*
         * Requiere: N/A
         * Efectúa : inicializa las variables globales de la clase
         * retorna : N/A
         */
        public ControladoraBDTiquete()
        {
            adaptador = new AdaptadorBD();
            dt = new DataTable();
        }
        /*
         * Requiere: N/A
         * Efectúa : Obtiene la fecha actual. Crea la consulta para obtener las cosultas activas con la fecha actual. Guarda en una tabla de datos el resultado a la consulta al adaptador.
         * Retorna : la tabla de datos con el resultado de la consulta.
         */
        internal DataTable solicitarEstaciones()
        {
            String consultaSQL = "select  nombre from reservas.estacion";
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
            String consultaSQL = "select r.id, a.nombre, e.nombre, r.numero, c.nombre, r.entra, r.sale FROM reservas.reservacion r, reservas.anfitriona a, reservas.estacion e,reservas.contacto c WHERE r.id = '" + id + "' and a.id = r.anfitriona  and r.estacion = e.id and r.solicitante = c.id  order by sale asc";
            dt = adaptador.consultar(consultaSQL);
            return dt;

        }



        internal DataTable obtenerTiquetes(string idServ, string idSolicitante, string fecha, string hora)
        {
            String consultaSQL = "select numero, consumido FROM tiquete WHERE idServicio = '" + idServ + "' and idSolicitante='" + idSolicitante + "' and fecha='" + fecha + "' and hora='" + hora + "'";
            dt = adaptador.consultar(consultaSQL);
            return dt;
        }

        internal String[] insertarTiquetes(string idServ, int numTiquete, String categoria, String idSolicitante, String tipoSolicitante, String fecha, String hora)
        {
            String[] respuesta = new String[3];
            String consultaSQL = "insert into tiquete values('" + numTiquete + "','" + idServ + "', 0,'" + categoria + "','" + idSolicitante + "','" + tipoSolicitante + "','" + fecha + "','" + hora + "')";
            return adaptador.insertar(consultaSQL);
        }
        /*
        * Efecto: modifica los datos de la comida extra seleccionada.
        * Requiere: la entidad de comida extra modificada, y la entidad "vieja", la entidad consultada.
        * Modifica: la table de servicio_especial.
       */
        public String[] eliminarTiquete(int numTiqueteSeleccionado)
        {
            String[] respuesta = new String[3];
            String consultaSQL = "delete FROM tiquete WHERE numero=" + numTiqueteSeleccionado.ToString();
            return adaptador.insertar(consultaSQL);
        }
        
    }
}