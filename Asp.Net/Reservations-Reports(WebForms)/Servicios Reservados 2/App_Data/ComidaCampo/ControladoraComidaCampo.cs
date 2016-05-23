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
    public class ControladoraComidaCampo
    {
        private ControladoraBDComidaCampo controladoraBD;//instancia de la controladora de BD comida extra.
        private ControladoraReservaciones controladoraReserv;
        private ControladoraEmpleado controladoraEmp;
        public static EntidadComidaCampo comidaCampoConsultada;
        private static List<String> adicionales;

        public ControladoraComidaCampo()
        {
            controladoraBD = new ControladoraBDComidaCampo();
            controladoraReserv = new ControladoraReservaciones();
            controladoraEmp = new ControladoraEmpleado();

        }

        public EntidadComidaCampo guardarComidaSeleccionada(String id, String idServ)
        {
            DataTable comidaCampo = controladoraBD.seleccionarComidaCampo(id, idServ);
            DataTable adicional = controladoraBD.seleccionarAdicional(idServ);
            adicionales = new List<String>();

            Object[] nuevoComidaC = new Object[13];

            nuevoComidaC[0] = comidaCampo.Rows[0][0];
            nuevoComidaC[1] = comidaCampo.Rows[0][1];
            nuevoComidaC[2] = comidaCampo.Rows[0][2];
            nuevoComidaC[3] = comidaCampo.Rows[0][3];
            nuevoComidaC[4] = comidaCampo.Rows[0][4];
            nuevoComidaC[5] = comidaCampo.Rows[0][5];
            nuevoComidaC[6] = comidaCampo.Rows[0][6];
            nuevoComidaC[7] = comidaCampo.Rows[0][7];
            nuevoComidaC[8] = comidaCampo.Rows[0][8];
            nuevoComidaC[9] = comidaCampo.Rows[0][9];
            nuevoComidaC[10] = comidaCampo.Rows[0][10];
            nuevoComidaC[11] = comidaCampo.Rows[0][11];
            nuevoComidaC[12] = comidaCampo.Rows[0][12];
            int i = 0;
            if (adicional.Rows.Count > 0)
            {
                foreach (DataRow fila in adicional.Rows)
                {
                    String ad = adicional.Rows[i][0].ToString();
                    adicionales.Add(ad);
                    i++;

                }
            }


            comidaCampoConsultada = new EntidadComidaCampo(nuevoComidaC, adicionales);
            return comidaCampoConsultada;
        }

        public EntidadComidaCampo consultarComidaCampoSeleccionada(String id, String idServicio)
        {
            DataTable comidaCampo = controladoraBD.seleccionarComidaCampoEmpleado(id, idServicio);
            DataTable adicional = controladoraBD.seleccionarAdicional(idServicio);
            adicionales = new List<String>();

            Object[] nuevoComidaC = new Object[13];

            nuevoComidaC[0] = comidaCampo.Rows[0][0];
            nuevoComidaC[1] = comidaCampo.Rows[0][1];
            nuevoComidaC[2] = comidaCampo.Rows[0][2];
            nuevoComidaC[3] = comidaCampo.Rows[0][3];
            nuevoComidaC[4] = comidaCampo.Rows[0][4];
            nuevoComidaC[5] = comidaCampo.Rows[0][5];
            nuevoComidaC[6] = comidaCampo.Rows[0][6];   //relleno
            nuevoComidaC[7] = comidaCampo.Rows[0][7];  //pan
            nuevoComidaC[8] = comidaCampo.Rows[0][8];
            nuevoComidaC[9] = comidaCampo.Rows[0][9];
            nuevoComidaC[10] = comidaCampo.Rows[0][10];
            nuevoComidaC[11] = comidaCampo.Rows[0][11];
            nuevoComidaC[12] = comidaCampo.Rows[0][12];
            int i = 0;
            if (adicional.Rows.Count > 0)
            {
                foreach (DataRow fila in adicional.Rows)
                {
                    String ad = adicional.Rows[i][0].ToString();
                    adicionales.Add(ad);
                    i++;

                }
            }


            comidaCampoConsultada = new EntidadComidaCampo(nuevoComidaC, adicionales);
            return comidaCampoConsultada;
        }
        public DataTable getComidaEmpleado(String id)
        {
            DataTable dt = controladoraBD.getComidaEmpleado(id);
            return dt;
        }
        public String[] agregarComidaCampo(Object[] dato, List<String> lista)
        {
            EntidadComidaCampo nuevaComidaCampo = new EntidadComidaCampo(dato, lista);
            String[] resultado = controladoraBD.agregarComidaCampo(nuevaComidaCampo);
            return resultado;
        }

        public String[] modificarComidaCampo(Object[] dato, List<String> lista, EntidadComidaCampo entidadConsultada)
        {
            EntidadComidaCampo nuevaComidaCampo = new EntidadComidaCampo(dato, lista);
            String[] resultado = controladoraBD.modificarComidaCampo(nuevaComidaCampo, entidadConsultada);
            return resultado;
        }

        public EntidadComidaCampo entidadSeleccionada()
        {
            return comidaCampoConsultada;
        }

        /*
         * Efecto: recibe el id de la comida de campo y lo manda a cancelar a la controladora de BD.
         * Requiere: el id.
         * Modifica:
         */
        internal String[] cancelarComidaCampo(String id)
        {
            String[] resultado = controladoraBD.cancelarComidaCampo(id);
            return resultado;
        }

        public EntidadReservaciones reservConsultada()
        {
            return controladoraReserv.getReservacionSeleccionada();
        }

        public String paxReserv(String id)
        {
            String pax = controladoraReserv.obtenerPax(id);
            return pax;

        }
        internal DataTable solicitarVecesConsumido(string idServicio)
        {
            return controladoraBD.vecesConsumido(idServicio);
        }
        internal void actualizarVecesConsumido(string idServicio, int vecesConsumido)
        {
            controladoraBD.actualizarVecesConsumido(idServicio, vecesConsumido);
        }
        internal EntidadReservaciones infoServicioRes()
        {
            return controladoraReserv.getReservacionSeleccionada();

        }
        internal EntidadEmpleado infoServicioEmp()
        {
            return controladoraEmp.getEmpleadoSeleccionado();
        }
    }
}