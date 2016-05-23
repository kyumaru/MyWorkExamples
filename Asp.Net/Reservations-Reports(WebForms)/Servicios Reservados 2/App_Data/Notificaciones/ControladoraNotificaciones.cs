using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Servicios_Reservados_2
{
    public class ControladoraNotificaciones
    {
        private ControladoraBDNotificaciones controladoraNotificaciones;
        static public DateTime ultimaRevision=new DateTime() ;


        public ControladoraNotificaciones()
        {
            controladoraNotificaciones = new ControladoraBDNotificaciones();
            ultimaRevision = ultimaRevision.AddHours(-12);
          
        }

        /*
         * Requiere: N/A
         * Efectua : pide el conteo de notificaciones desde la ultima vez a la controladora de base de datos
         * Retorna : un entero con el numero de notificaciones.
         */
        public int getNumeroDeNotificaciones()
        {
            DataTable resultado = controladoraNotificaciones.numeroDeNotificaciones(ultimaRevision);
            int notificaiones = int.Parse(resultado.Rows[0][0].ToString());
            ultimaRevision = DateTime.Now;
            return notificaiones;
        }
        /*
         * Requiere:N/A
         * Efectua :Pide a la controladora de Base de datos las notificaciones de las ultimas 12 horas hasta las proximas 12 horas
         * Retorna :un Data table con las notificaciones
         */
        public DataTable getNotificaciones()
        {
            return controladoraNotificaciones.getNotificaciones();
        }
    }
}