using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace Servicios_Reservados_2
{
    public class ControladoraServirPlatos
    {
        private static EntidadTiquete seleccionado;
        private static ControladoraBDServirPlatos controladoraBD;
        private static ControladoraComidaEmpleado controladoraComidaEmp;
        private static ControladoraEmpleado controladoraEmp;
        private static ControladoraComidaExtra controladoraComidaExtra;
        private static ControladoraReservaciones controladoraReservaciones;
        private static ControladoraServicios controladoraServicios;
        private static ControladoraComidaCampo controladoraComidaCampo;
        /*
         * Requiere: N/A
         * Efectúa : Inicializa las variables globales de la clase. 
         * Retorna : N/A
         */
        public ControladoraServirPlatos()
        {
            controladoraBD = new ControladoraBDServirPlatos();
            controladoraServicios = new ControladoraServicios();
            controladoraComidaExtra = new ControladoraComidaExtra();
            controladoraReservaciones = new ControladoraReservaciones();
            controladoraComidaEmp = new ControladoraComidaEmpleado();
            controladoraEmp = new ControladoraEmpleado();
            controladoraComidaCampo = new ControladoraComidaCampo();
        }
        /*
         * Requiere: N/A
         * Efectúa : Pide a la controladora de base de datos la información del tiquete y las guarda en una tabla de datos. 
         * Retorna : la tabla de datos que se crea.
         */
        internal EntidadTiquete solicitarTiquete(int numTiquete)
        {
            DataTable tiquete = controladoraBD.consultarTiquete(numTiquete);
            if (tiquete.Rows.Count > 0)
            {
                String idServicio = tiquete.Rows[0][0].ToString();
                int consumido = int.Parse(tiquete.Rows[0][1].ToString());
                String categoria = tiquete.Rows[0][2].ToString();
                String idSolicitante = tiquete.Rows[0][3].ToString();
                String tipoSolicitante = tiquete.Rows[0][4].ToString();
                String fecha = tiquete.Rows[0][5].ToString();
                String hora= tiquete.Rows[0][6].ToString();
                String notas = "No disponible";
                String anfitriona = "No disponible";
                String estacion = "No disponible";
                String nombreSolicitante="No disponible";

                if ("empleado".Equals(tipoSolicitante) && "Comida de campo".Equals(categoria))
                {
                    EntidadEmpleado empleado= controladoraComidaEmp.getInformacionDelEmpleado(idSolicitante);
                    nombreSolicitante = empleado.Nombre +" "+ empleado.Apellido;

                }
                else if ("empleado".Equals(tipoSolicitante) && "Comida regular".Equals(categoria))
                {
                    EntidadComidaEmpleado comidaEmp= controladoraComidaEmp.consultar(int.Parse(idServicio));
                    EntidadEmpleado empleado = controladoraComidaEmp.getInformacionDelEmpleado(idSolicitante);
                    nombreSolicitante = empleado.Nombre + " " + empleado.Apellido;
                    notas = comidaEmp.Notas;                   

                }
                else if ("reservacion".Equals(tipoSolicitante) && "Paquete".Equals(categoria))
                {
                    DataTable paquete = controladoraServicios.solicitarInfoPaquete(idServicio);
                    notas = paquete.Rows[0][1].ToString();
                    anfitriona = paquete.Rows[0][2].ToString();
                    estacion = paquete.Rows[0][3].ToString();
                    nombreSolicitante = paquete.Rows[0][4].ToString();

                }
                else if ("reservacion".Equals(tipoSolicitante) && "Comida Extra".Equals(categoria))
                {
                    /*
                     * EntidadComidaExtra comidaExtra = controladoraComidaExtra.guardarServicioSeleccionado(idSolicitante, idServicio, fecha, hora);
                    notas = comidaExtra.Descripcion;                    
                    DataTable servicio = controladoraReservaciones.solicitarInfo(idSolicitante);
                    anfitriona = servicio.Rows[0][2].ToString();
                    estacion = servicio.Rows[0][3].ToString();
                    nombreSolicitante = servicio.Rows[0][4].ToString();
                     * */
                    
                }
                else if ("reservacion".Equals(tipoSolicitante) && "Comida Campo".Equals(categoria))
                {
                    EntidadComidaCampo comidaCampo = controladoraComidaCampo.guardarComidaSeleccionada(idSolicitante,idServicio);
                    
                    DataTable servicio = controladoraReservaciones.solicitarInfo(idSolicitante);
                    anfitriona = servicio.Rows[0][2].ToString();
                    estacion = servicio.Rows[0][3].ToString();
                    nombreSolicitante = servicio.Rows[0][4].ToString();
                }

                seleccionado = new EntidadTiquete(numTiquete, idServicio, tipoSolicitante, consumido, idSolicitante, categoria, notas, anfitriona, estacion, nombreSolicitante, fecha, hora);

            }
            else
            {
                seleccionado = null;
            }
            return seleccionado;
        }


        internal void servirTiquete()
        {
            int vecesConsumidoTiquete = seleccionado.Consumido + 1;
            controladoraBD.servirTiquete(seleccionado.Numero, vecesConsumidoTiquete);
            if ("empleado".Equals(seleccionado.TipoSolicitante) && "Comida de campo".Equals(seleccionado.Categoria))
                {
                    DataTable comidaCampoEmp = controladoraComidaCampo.solicitarVecesConsumido(seleccionado.IdServicio);
                    int vecesConsumido = int.Parse(comidaCampoEmp.Rows[0][0].ToString()) + 1;
                    controladoraComidaCampo.actualizarVecesConsumido(seleccionado.IdServicio, vecesConsumido);
                }
                else if ("empleado".Equals(seleccionado.TipoSolicitante) && "Comida regular".Equals(seleccionado.Categoria))
                {
                    DataTable comidaEmp = controladoraComidaEmp.solicitarVecesConsumido(seleccionado.IdServicio);
                    int vecesConsumido = int.Parse(comidaEmp.Rows[0][0].ToString()) + 1;
                    controladoraComidaEmp.actualizarVecesConsumido(seleccionado.IdServicio, vecesConsumido);
                }
                else if ("reservacion".Equals(seleccionado.TipoSolicitante) && "Paquete".Equals(seleccionado.Categoria))
                {
                    DataTable paquete = controladoraServicios.solicitarVecesConsumidoPaquete(seleccionado.IdServicio);
                    int vecesConsumido = int.Parse(paquete.Rows[0][0].ToString())+1;                   
                    controladoraServicios.actualizarVecesConsumidoPaquete(seleccionado.IdServicio, vecesConsumido);
                }
                else if ("reservacion".Equals(seleccionado.TipoSolicitante) && "Comida Extra".Equals(seleccionado.Categoria))
                {
                    /*
                     * DataTable comidaExtra = controladoraComidaExtra.solicitarVecesConsumido(seleccionado.IdServicio, seleccionado.Solicitante, seleccionado.Fecha, seleccionado.Hora);
                    int vecesConsumido = int.Parse(comidaExtra.Rows[0][0].ToString()) + 1;
                    controladoraComidaExtra.actualizarVecesConsumido(seleccionado.IdServicio, vecesConsumido, seleccionado.Solicitante, seleccionado.Fecha, seleccionado.Hora);
                     * */
                }
                else if ("reservacion".Equals(seleccionado.TipoSolicitante) && "Comida Campo".Equals(seleccionado.Categoria))
                {
                    DataTable comidaCampoRes = controladoraComidaCampo.solicitarVecesConsumido(seleccionado.IdServicio);
                    int vecesConsumido = int.Parse(comidaCampoRes.Rows[0][0].ToString()) + 1;
                    controladoraComidaCampo.actualizarVecesConsumido(seleccionado.IdServicio, vecesConsumido);
                }

        }

        internal void desactivarTiquete()
        {
            controladoraBD.eliminarTiquete(seleccionado.Numero);
        }
    }
}