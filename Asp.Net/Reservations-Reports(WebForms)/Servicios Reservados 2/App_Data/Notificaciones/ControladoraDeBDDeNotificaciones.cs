using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Servicios_Reservados_2
{
    public class ControladoraBDNotificaciones
    {
        private AdaptadorBD adaptador;
        public ControladoraBDNotificaciones()
        {
            adaptador = new AdaptadorBD();
        }
        public DataTable numeroDeNotificaciones(DateTime fecha){
            String consulta = "Select count(*)FROM NOTIFICACIONES Where  momento> " + fecha + " AND  momento< CURRENT_TIMESTAMP+interval '12' hour (1)";
            DataTable resultado = adaptador.consultar(consulta);
            return resultado;
        }
        /*
         * Requiere:N/A
         * Efectua : Pide al adaptador las notificaciones de las comidas entre las ultimas 12 horas y las proximas 12 horas.
         * Retorna : un data table con las notificaciones.
         */
        internal DataTable getNotificaciones()
        {
            string consulta = "SELECT MOMENTO, ESTACION, TIPODESERVICIO, TIPODECAMBIO, VALORANTERIOR, VALORACTUAL, IDSERVICIO FROM NOTIFICACIONES Where momento> CURRENT_TIMESTAMP-interval '12' hour (1) AND  momento< CURRENT_TIMESTAMP+interval '12' hour (1)";
            return adaptador.consultar(consulta);
        }
    }
}