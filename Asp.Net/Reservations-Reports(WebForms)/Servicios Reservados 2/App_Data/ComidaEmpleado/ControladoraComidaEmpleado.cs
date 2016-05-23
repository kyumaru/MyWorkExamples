using Servicios_Reservados_2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Threading;
using System.Globalization;

namespace Servicios_Reservados_2
{
    public class ControladoraComidaEmpleado
    {
        private ControladoraEmpleado controlEmpleado = new ControladoraEmpleado();
        private ControladoraBDComidaEmpleado controladoraBD = new ControladoraBDComidaEmpleado();
        /*
         * Requiere: String con el identificador del empleado
         * Efectua : llama a seleccionar empleado de la controladora con el parametro dado y luego pide el empleado seleccionado. 
         * Retorna : El empleado retornado por la controladora
         */

        public EntidadEmpleado getInformacionDelEmpleado(String idEmpleado)
        {
            controlEmpleado.seleccionarEmpleado(idEmpleado);
            return controlEmpleado.getEmpleadoSeleccionado();
        }
        /*
         * Requiere: un identificador de empleado en una hilera, una lista de fechas para la reservacion, un arreglo de caracteres con la informacion de los turnos y una hilera con las notas
         * Efectua : encapsula los datos y los envia a la controladora de base de datos para ser insertados
         * Retorna : un arreglo de hileras con el resultado de la controladora de base de datos.
         */
        public String[] agregar(String idEmpleado, String estacion, List<DateTime> fechasReserva, char[] turnos, bool pagado, String notas)
        {
            EntidadComidaEmpleado nuevo = new EntidadComidaEmpleado(idEmpleado, estacion, fechasReserva, turnos, pagado, notas, -1);
            return controladoraBD.agregar(nuevo);
        }
        /*
         * Requiere: una entidad comida empleado ocn los datos originales, un identificador de empleado en una hilera, una lista de fechas para la reservacion, un arreglo de caracteres con la informacion de los turnos y una hilera con las notas
         * Efectua : encapsula los datos y los envia a la controladora de base de datos para ser modificados junto con los originales
         * Retorna : un arreglo de hileras con el resultado de la controladora de base de datos.
         */

        internal String[] modificar(EntidadComidaEmpleado seleccionada, string idEmpleado, String estacion, List<DateTime> fechasReserva, char[] turnos, bool pagado, String notas)
        {
            EntidadComidaEmpleado nuevo = new EntidadComidaEmpleado(idEmpleado, estacion, fechasReserva, turnos, pagado, notas, seleccionada.IdComida);
            return controladoraBD.modificar(seleccionada, nuevo);
        }
        /*
         * Requiere: String con el identificador de la comida empleado
         * Efectua : llama a seleccionar empleado de la controladora de datos con el parametro dado y luego saca los datos retornados para finalmente encpsularlos. 
         * Retorna : La entidad encapsulada.
         */         
        internal EntidadComidaEmpleado consultar(int idReservacion)
        {
            List<DateTime> list = new List<DateTime>();
            char[] turnos = new char[3];
            DataTable dt = controladoraBD.getInformacionReservacionEmpleado(idReservacion);
            //IDEMPLEADO, FECHA, PAGADO, NOTAS, DESAYUNO, ALMUERZO, CENA, IDCOMIDAEMPLEADO, ESTACION

            //list.Add(DateTime.ParseExact(dt.Rows[0][1].ToString(), "g", System.Globalization.CultureInfo.InvariantCulture));//{8/20/2015 12:00:00 AM}
            turnos[0] = dt.Rows[0][4].ToString().ToCharArray(0, 1)[0];
            turnos[1] = dt.Rows[0][5].ToString().ToCharArray(0, 1)[0];
            turnos[2] = dt.Rows[0][6].ToString().ToCharArray(0, 1)[0];
            bool pagado = (dt.Rows[0][2].ToString().Equals("T"));
            String notas = dt.Rows[0][3].ToString();
            String estacion = dt.Rows[0][8].ToString();
            DateTime fecha; 
            DateTime.TryParse(dt.Rows[0][1].ToString(),out fecha);
            list.Add(fecha);
            EntidadComidaEmpleado consultada = new EntidadComidaEmpleado(dt.Rows[0][0].ToString(), estacion, list, turnos, pagado, notas, Int32.Parse(dt.Rows[0][7].ToString()));
            //String idEmpleado, List<DateTime> fechasReserva, bool[] turnos, bool pagado, String notas, int id = -1
            return consultada;
        }
        /*
         * Requiere: EntidadComidaEmpleado con los daots a cancelar
         * Efectua : llama a cancelar de la controladora de base de datos con el parametro dado. 
         * Retorna : un arreglo de hileras con el resultado de la controladora de base de datos.
         */
        internal String[] eliminar(EntidadComidaEmpleado entidadComidaEmpleado)
        {
            return controladoraBD.cancelar(entidadComidaEmpleado);
        }
        /*
         * Requiere: hilera con el identificador del empleado
         * Efectua : llama a getReservacionesEmpleado de la controladora de base de datos con el parametro dado. 
         * Retorna : El datatable retornado por la controladora.
         */
        internal DataTable getComidaEmpleado(string idEmpleado)
        {
            return controladoraBD.getReservacionesEmpleado(idEmpleado);
        }
        /*
         * Requiere: hilera con el identificador del servicio
         * Efectua : llama a vecesconsumido de la controladora de base de datos con el parametro dado. 
         * Retorna :  El datatable retornado por la controladora.
         */
        internal DataTable solicitarVecesConsumido(string idServicio)
        {
            return controladoraBD.vecesConsumido(idServicio);
        }
        /*
         * Requiere: hilera con el identificador del servicio y un entero de veces consumido
         * Efectua : llama a actualizarVecesConsumido de la controladora de base de datos con el parametro dado. 
         * Retorna :  N/A.
         */
        internal void actualizarVecesConsumido(string idServicio, int vecesConsumido)
        {
            controladoraBD.actualizarVecesConsumido(idServicio, vecesConsumido);
        }
    }
}