using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.OleDb;
using System.Data;
using System.Diagnostics;
using System.Data.SqlClient;

namespace Servicios_Reservados_2
{
    public class ControladoraBDServicios
    {

        private AdaptadorBD adaptador;
        DataTable dt;

        public ControladoraBDServicios()
        {
            adaptador = new AdaptadorBD();
            dt = new DataTable();
        }

        /*
         * Efecto: Crea la consulta SQL que obtiene el estado de una comida extra  
         * Requiere: id de la reservacion y id de la comida extra
         * Modifica: el dataTable dt
         */
        internal DataTable obtenerEstadoComidaExtra(String idReservacion, String idCE)
        {
            String consultaSQL = "select estado from servicios_reservados.servicio_especial where idreservacion = '" + idReservacion + "' and idserviciosextras = '" + idCE + "'";
            dt = adaptador.consultar(consultaSQL);
            return dt;
        }


        /**Efecto: Crea la consulta SQL que obtiene el estado de una comida de campo  
         * Requiere: id de la comida de campo  
         * Modifica: el dataTable dt
         */
        internal DataTable obtenerEstadoComidaCampo(String idCC)
        {
            String consultaSQL = "select estado from servicios_reservados.comida_campo where idcomidacampo = '" + idCC + "'";
            dt = adaptador.consultar(consultaSQL);
            return dt;
        }

        /**Efecto: Crea la consulta SQL que obtiene las tuplas de los servicios de una reservacion y la retorna en forma de datatable  
         * Requiere: id de la reservaciones 
         * Modifica: el dataTable dt
         */
        internal DataTable solicitarServicios(String id)
        {
            String consultaSQL = "select s.idreservacion, e.idservicio, e.tipo, s.descripcion, s.hora, s.fecha, s.estado, s.pax, s.id from servicios_reservados.servicio_especial s JOIN servicios_reservados.servicios_extras e ON e.idservicio = s.idserviciosextras AND s.idreservacion= '" + id + "' and s.estado <>  'Cancelado'";
            dt = adaptador.consultar(consultaSQL);
            return dt;

        }

        internal DataTable solicitarComidaCampo(String id)
        {
            String consultaSQL = "select c.idcomidacampo, c.idreservacion, c.fecha, c.estado, c.opcion, c.relleno, c.pan, c.bebida, c.pax, c.hora from servicios_reservados.comida_campo c where c.idreservacion = '" + id + "' and c.estado <>  'Cancelado'";
            dt = adaptador.consultar(consultaSQL);
            return dt;

        }

        internal DataTable obtenerPaquete(string idReservacion)
        {
            String consultaSQL = "select ri.id, vr.nombre, ri.pax from reservas.reservacionitem ri, reservas.v_reservable vr where ri.reservacion ='" + idReservacion + "' and ri.reservable= vr.id and vr.categoria='ANURA7249245184.5851916019'";
            dt = adaptador.consultar(consultaSQL);
            return dt;
        }


        internal DataTable solicitarReservItem(string id)
        {
            String consultaSQL = "select ri.pax, r.notas, vr.siglas, vr.estacion, c.nombre FROM reservas.reservacionitem ri, reservas.reservacion r, reservas.vr_reservacion vr, reservas.contacto c where ri.id ='" + id + "' and ri.reservacion=r.id and r.numero=vr.numero and r.solicitante = c.id";
            dt = adaptador.consultar(consultaSQL);
            return dt;
        }
        internal DataTable vecesConsumidoPaquete(string id)
        {
            String consultaSQL = "select vecesconsumido from reservas.reservacionitem where id ='"+id+"'";
            dt = adaptador.consultar(consultaSQL);
            return dt;
        }


        internal void actualizarVecesConsumidoPaquete(string idServicio, int vecesConsumido)
        {
            String consultaSQL = "update reservas.reservacionitem set vecesconsumido= " + vecesConsumido + " where id ='" + idServicio + "'";
            dt = adaptador.consultar(consultaSQL);
        }
    }
}