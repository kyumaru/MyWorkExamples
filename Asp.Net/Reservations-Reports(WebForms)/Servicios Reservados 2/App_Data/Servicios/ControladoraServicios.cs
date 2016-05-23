using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Web;



namespace Servicios_Reservados_2
{
    public class ControladoraServicios
    {
        public static EntidadReservaciones servicios;
        private static ControladoraBDServicios controladoraBD;
        public static ControladoraReservaciones controladoraReserv;
        public static ControladoraComidaExtra controladoraCE;
        public static ControladoraComidaCampo controladoraComidaCampo;
        public List<String> adicionales;
        private static EntidadServicios seleccionado;



        public ControladoraServicios()
        {
            controladoraBD = new ControladoraBDServicios();
            controladoraReserv = new ControladoraReservaciones();
            controladoraCE = new ControladoraComidaExtra();
            controladoraComidaCampo = new ControladoraComidaCampo();
        }

        public EntidadReservaciones informacionServicio()
        {
            servicios = controladoraReserv.getReservacionSeleccionada();

            return servicios;

        }

        public String idSelected()
        {
            String idReserv = controladoraReserv.getReservacionSeleccionada().Id;
            return idReserv;

        }

        public String idNumSelected()
        {
            string idNum = controladoraReserv.getReservacionSeleccionada().Numero;
            return idNum;

        }

        public String paxReserv(String id)
        {
            String pax = controladoraReserv.obtenerPax(id);
            return pax;

        }

        internal DataTable solicitarServicios(String id)
        {
            DataTable servicios = controladoraBD.solicitarServicios(id);
            return servicios;

        }

        internal DataTable solicitarComidaCampo(String id)
        {
            DataTable comidaCampo = controladoraBD.solicitarComidaCampo(id);
            return comidaCampo;
        }

        internal EntidadComidaExtra seleccionarComidaExtra(String idComidaExtra)
        {
            return controladoraCE.guardarServicioSeleccionado(idComidaExtra);
        }

        internal EntidadComidaCampo seleccionarComidaCampo(String id, String idServ)
        {
            return controladoraComidaCampo.guardarComidaSeleccionada(id, idServ);
        }

        /*
         * Efecto: recibe los ids y los manda a la controladora de BD para eliminar el servicio.
         * Requiere: los ids.
         * Modifica:
         */
        internal String[] cancelarComidaExtra(String idComidaExtra)
        {
            String[] resultado = controladoraCE.cancelarComidaExtra(idComidaExtra);
            return resultado;
        }

        /*
         * Efecto: recibe el id de la comida de campo y lo manda a cancelar a la controladora de BD.
         * Requiere: el id.
         * Modifica:
         */
        internal String[] cancelarComidaCampo(String idCC)
        {
            String[] resultado = controladoraComidaCampo.cancelarComidaCampo(idCC);
            return resultado;
        }


        internal DataTable solicitarPaquete(string idReservacion)
        {
            return controladoraBD.obtenerPaquete(idReservacion);
        }

        internal EntidadServicios crearServicio(string idRes, string id, String hora, String categoria)
        {
             if (id.Contains("."))
            {
                DataTable dt= controladoraBD.solicitarReservItem(id);
                seleccionado = new EntidadServicios(idRes, "reservacion", id, categoria, "Durante toda la estadia", "Durante toda la estadia", int.Parse(dt.Rows[0][0].ToString()), dt.Rows[0][1].ToString(), "Varias");
                
            }
            else if (id.Contains("S"))
            {
                EntidadComidaExtra servicio = seleccionarComidaExtra(id);
                seleccionado = new EntidadServicios(idRes, "reservacion", id, categoria, servicio.Fecha, servicio.Consumido, servicio.Pax, servicio.Descripcion, servicio.Hora);
            }
            else
            {
                EntidadComidaCampo comidaCampo = seleccionarComidaCampo(idRes, id);
                seleccionado = new EntidadServicios(idRes, "reservacion", id, categoria, comidaCampo.Fecha, comidaCampo.Estado, comidaCampo.Pax, "Nada",hora);
            }
             return seleccionado;
        }

        public EntidadServicios servicioSeleccionado(){ 
            return seleccionado;
        }
        internal void activarTiquete()
        {
            ControladoraTiquete.setServicio(seleccionado);
        }


        internal DataTable solicitarInfoPaquete(string idServicio)
        {
            return controladoraBD.solicitarReservItem(idServicio);
        }
        internal DataTable solicitarVecesConsumidoPaquete(string idServicio)
        {
            return controladoraBD.vecesConsumidoPaquete(idServicio);
        }


        internal void actualizarVecesConsumidoPaquete(string idServicio, int vecesConsumido)
        {
            controladoraBD.actualizarVecesConsumidoPaquete(idServicio, vecesConsumido);
        }
    }
}