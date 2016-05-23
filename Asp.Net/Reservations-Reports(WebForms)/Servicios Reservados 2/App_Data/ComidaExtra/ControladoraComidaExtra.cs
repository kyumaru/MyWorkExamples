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

namespace Servicios_Reservados_2
{
    public class ControladoraComidaExtra
    {
      private ControladoraBDComidaExtra controladoraBD;//instancia de la controladora de BD comida extra.
      public static EntidadReservaciones servicios;
      public static ControladoraReservaciones controladoraReserv;
      public static EntidadComidaExtra servicioSeleccionado;//instancia entidad comida extra.
      public static int paxSeleccionados;
      public static String idReservacionSelccionada;
       
      public ControladoraComidaExtra()
        {
            controladoraBD = new ControladoraBDComidaExtra();
            controladoraReserv = new ControladoraReservaciones();
        }
      /*
         * Efecto: solicita a la controladora de BD los diferentes tipos de comida extra.
         * Requiere: 
         * Modifica: 
        */
      public DataTable solicitarTipo() {
          DataTable tipos = controladoraBD.solicitarTipos();
          return tipos;
      }

      /*
       * Efecto: encapsula los datos y los envía a la controladora para que sean insertados.
       * Requiere: un objeto con los datos.
       * Modifica: pasa los datos de un objeto a una entidad (encapsularlos).
      */
      public String[] agregarServicioExtra(Object[] datos)
      {
          EntidadComidaExtra entidad = new EntidadComidaExtra(datos);
          String[] resultado = controladoraBD.agregarServicioExtra(entidad);//llamado a la controladora de base de datos
          return resultado;
      }

      /*
       * Efecto: encapsula los datos de la entidad y los envía a la controladora para que sean insertados.
       * Requiere: un objeto con los datos y la entidad vieja a modificar.
       * Modifica: pasa los datos de un objeto a una entidad (encapsularlos).
      */
      public String[] modificarServicioExtra(Object[] datos, EntidadComidaExtra entidadVieja)
      {
          EntidadComidaExtra entidad = new EntidadComidaExtra(datos);
          String[] resultado = controladoraBD.modificarServicioExtra(entidad, entidadVieja);//llamado a la controladora de base de datos
          return resultado;
      }

      /*
       * Efecto: obtener el id de la reservación consultada.
       * Requiere: la consulta de una reservación.
       * Modifica: la variable servicios.
      */
      public EntidadReservaciones informacionServicio()
      {
          servicios = controladoraReserv.getReservacionSeleccionada();
          return servicios;

      }

      /*
       * Efecto: comuncación con la controladora de servicios para guardar el servicio seleccionado.
       * Requiere: la entrada de los datos.
       * Modifica: la variable servicioSeleccionado, en la que se almacena el servicio consultado.
      */
      public EntidadComidaExtra guardarServicioSeleccionado(String idComidaExtra)
      {
          DataTable servicios = controladoraBD.seleccionarServicio(idComidaExtra);

          Object[] nuevoServicio = new Object[9];

          nuevoServicio[0] = servicios.Rows[0][0];
          nuevoServicio[1] = servicios.Rows[0][1];
          nuevoServicio[2] = servicios.Rows[0][3];
          nuevoServicio[3] = servicios.Rows[0][4];
          nuevoServicio[4] = servicios.Rows[0][5];
          nuevoServicio[5] = servicios.Rows[0][2];
          nuevoServicio[7] = servicios.Rows[0][6];
          nuevoServicio[6] = servicios.Rows[0][7];
          nuevoServicio[8] = servicios.Rows[0][9];

          servicioSeleccionado = new EntidadComidaExtra(nuevoServicio);

          return servicioSeleccionado;
      }

      /*
       * Efecto: comuncación con la controladora de servicios para guardar los pax de la reservación seleccionada.
       * Requiere: la entrada de los datos.
       * Modifica: la variable paxSeleccionados, en la que se almacena el servicio consultado.
       */
      public String paxConsultado(String id)
      {
          String pax = controladoraReserv.obtenerPax(id);
          return pax;
      }

      /*
       * Efecto: devuelve la entidad consultada.
       * Requiere: que la entidad esté inicializada.
       * Modifica: 
      */
      public EntidadComidaExtra servicioSeleccionados()
      {
          return servicioSeleccionado;
      }

      /*
       * Efecto: devuelve los pax consultados.
       * Requiere: que la variable pax esté inicializada.
       * Modifica: 
       */
      public int paxSeleccionado()
      {
          return paxSeleccionados;
      }

      /*
     * Efecto: devuelve la reservación consultada.
     * Requiere: que la variable idReservacionSelccionada esté inicializada.
     * Modifica: 
     */
      public EntidadReservaciones reservacionSeleccionada()
      {
          return controladoraReserv.getReservacionSeleccionada();
         
      } 

      /*
       * Efecto: consulta el tipo de comida extra de un id específico.
       * Requiere: que la entidad esté inicializada.
       * Modifica: el datatable aux, que se llena con el tipo seleccionado. 
      */
        public String consultarTipo(String id)
        {
            DataTable aux = controladoraBD.consultarTipo(id);
            return aux.Rows[0][0].ToString();
        }

      /*
       * Efecto: consulta las fechas de inicio y fin de una reservación con un id específico.
       * Requiere: que la entidad esté inicializada.
       * Modifica: el datatable fecha, que se llena con el tipo seleccionado. 
      */
        public DataTable consultarFechas(String id)
        {
            DataTable fechas = controladoraBD.consultarFechas(id);
            return fechas;
        }

        /*
         * Efecto: recibe los ids y los manda a la controladora de BD para cancelar el servicio.
         * Requiere: los ids.
         * Modifica:
         */
        internal String[] cancelarComidaExtra(String idComidaExtra)
        {
            String[] resultado = controladoraBD.cancelarComidaExtra(idComidaExtra);
            return resultado;
        }



        internal DataTable solicitarVecesConsumido(string idComidaExtra)
        {
            return controladoraBD.vecesConsumido(idComidaExtra);
        }

        internal void actualizarVecesConsumido(string idComidaExtra, int vecesConsumido)
        {
            controladoraBD.actualizarVecesConsumido(idComidaExtra, vecesConsumido);
        }
    }
}