using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace Servicios_Reservados_2
{
    public class ControladoraTiquete
    {
        private static int numTiqueteSeleccionado;
        private static ControladoraServicios controladoraServ;
        private static ControladoraBDTiquete controladoraBD;
        private static ControladoraReservaciones controladoraRes;
        private static ControladoraEmpleadoReserva controladoraEmplRes;
        private static ControladoraEmpleado controladoraEmpl;
        private static EntidadServicios servicio;
        /*
         * Requiere: N/A
         * Efectúa : Inicializa las variables globales de la clase. 
         * Retorna : N/A
         */
        public ControladoraTiquete()
        {
            controladoraBD = new ControladoraBDTiquete();
            controladoraRes = new ControladoraReservaciones();
            controladoraServ = new ControladoraServicios();
            controladoraEmplRes = new ControladoraEmpleadoReserva();
            controladoraEmpl = new ControladoraEmpleado();
        }
        /*
         * Requiere: N/A
         * Efectúa : Pide a la controladora de base de datos la información de todas las reservaciones y las guarda en una tabla de datos. 
         * Retorna : la tabla de datos que se crea.
         */
        internal EntidadReservaciones solicitarInfoReservacion()
        {
            return controladoraRes.getReservacionSeleccionada();

        }
        internal EntidadServicios solicitarInfoServicio()
        {           
            return servicio;
        }

        internal DataTable solicitarTiquetes(string idServ, string idSolicitante, string fecha, string hora)
        {
            return controladoraBD.obtenerTiquetes(idServ, idSolicitante,  fecha,  hora);
        }

        internal String[] activarTiquete(int numTiquete)
        {
            return controladoraBD.insertarTiquetes(servicio.IdServicio, numTiquete, servicio.Categoria, servicio.IdSolicitante, servicio.TipoSolicitante, servicio.Fecha, servicio.Hora);
        }

        internal String[] desactivarTiquete()
        {
            return controladoraBD.eliminarTiquete(numTiqueteSeleccionado);
        }

        internal void seleccionarTiquete(int numTiquete)
        {
            numTiqueteSeleccionado = numTiquete;
        }

        internal EntidadEmpleado solicitarInfoEmpleado()
        {
            return controladoraEmpl.getEmpleadoSeleccionado();
        }

        internal static void setServicio(EntidadServicios seleccionado)
        {
            servicio = seleccionado;
        }
    }
}