using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Servicios_Reservados_2
{
    public class ControladoraEmpleadoReserva
    {
        private ControladoraEmpleado controladoraEmpleado;
        private ControladoraComidaEmpleado controladoraComidaEmpleado;
        private ControladoraComidaCampo controladoraComidaCampo;
        private ControladoraNotificaciones controladoraNotificaciones;
        private static EntidadServicios seleccionado;
        private static EntidadComidaCampo comidaSeleccionada;
        /*
         * Requiere:N/A
         * Efectua : Inicializa las tres controladoras miembro de la clase 
         * Retorna :N/A
         */
        public ControladoraEmpleadoReserva()
        {
            controladoraEmpleado = new ControladoraEmpleado();
            controladoraComidaEmpleado = new ControladoraComidaEmpleado();
            controladoraComidaCampo = new ControladoraComidaCampo();
            //controladoraNotificaciones = new ControladoraDeNotificaciones();
        }
        /*
         * Requiere: Una hilera con el identificador del empleado
         * Efectua : Pide a la controladora de comida campo las comidas relacionadas con el empleado
         * Retorna : La tabla de datos retornada por la controladora.
         */
        public DataTable obtenerComidaCampo(String id)
        {
            return controladoraComidaCampo.getComidaEmpleado(id);
                    

        }
        /*
         * Requiere: Una hilera con el identificador del empleado
         * Efectua : Pide a la controladora de comida Empleado las comidas relacionadas con el empleado
         * Retorna : La tabla de datos retornada por la controladora.
         */
        public DataTable obtenerTabla(String idEmpleado)
        {
           return  controladoraComidaEmpleado.getComidaEmpleado(idEmpleado);
           
        }
        /*
         * Requiere: Una hilera con el identificador del empleado
         * Efectua : Pide a la controladora de Empleado la informacion del empleado
         * Retorna : La entidad empleado retornada por la controladora.
         */
        public EntidadEmpleado obtenerEmpleado(string idEmpleado)
        {
            controladoraEmpleado.seleccionarEmpleado(idEmpleado);
            return controladoraEmpleado.getEmpleadoSeleccionado();
        }
        /*
         * Requiere: hileras con: el identificador del empleado, una fecha de servicio, una con la categoria, una con las notas, una con el estado y una con la hora ademas un entero con el identificador del servicio. 
         * Efectua : Encapsula todos los parametros en una entidad servicio
         * Retorna : La entidad encapsulada.
         */
        internal EntidadServicios crearServicio(string idEmpleado, int idServicio, string fechaServ, String categoria, String notas, String estado, String hora)
        {
            
            seleccionado = new EntidadServicios(idEmpleado, "empleado", idServicio.ToString(), categoria, fechaServ, estado, 1, notas, hora);
            return seleccionado;
        }
        /*
         * Requiere: una hilera con identificador del empleado y otra con identificador del servicio.
         * Efectua : Cosulta una comida de campo a traves de la controladora.
         * Retorna : la entidad comida campo retornada por la controladora.
         */
        internal EntidadComidaCampo consultarComidaCampoSeleccionada(string idEmpleado, String idServicio)
        {
            return controladoraComidaCampo.consultarComidaCampoSeleccionada(idEmpleado, idServicio);
            
        }
        /*
         * Requiere: N/A
         * Efectua : N/A
         * Retorna : La entidad servicios seleccionado que es miembro de la clase.
         */
        internal EntidadServicios servicioSeleccionado()
        {
            return seleccionado;
        }
        /*
         * Requiere:N/A
         * Efectua : Invoca el metodo setServicio de la controladora de tiquetes dando ocmo parametro, el miembro de la clase EntidadServicio llamado seleccionado
         * Retorna :N/A
         */
        internal void activarTiquete()
        {
            ControladoraTiquete.setServicio(seleccionado);
        }
        /*
         * Requiere: Un entero con el identificador de la comida empleado
         * Efectua : consulta a la contorladora de comidas con el identificador provisto
         * Retorna : La entidad comida empleado retornada por la controladora.
         */
        internal EntidadComidaEmpleado consultarComida(int p)
        {
            return controladoraComidaEmpleado.consultar(p);    
        }
        /*
         * Requiere: Un identificador comida campo en una hilera
         * Efectua : Llama el metodos de cancelar de la controladora de comidas de campo dando como parametro el valor recibido
         * Retorna : Un arreglo de hileras retornado por la controladora con el resultado.
         */
        internal String[] cancelarComidaCampo(String idCC)
        {
            String[] resultado = controladoraComidaCampo.cancelarComidaCampo(idCC);
            return resultado;
        }
        /*
         * Requiere: Un identificador comida de empleado campo en un entero
         * Efectua : Llama el metodos de cancelar de la controladora de comidas de empleado dando como parametro el valor recibido
         * Retorna : Un arreglo de hileras retornado por la controladora con el resultado.
         */
        internal String[] cancelarComidaRegular(int id)
        {
            String[] resultado = new string[3];
            EntidadComidaEmpleado cancelar = controladoraComidaEmpleado.consultar(id);
            resultado =controladoraComidaEmpleado.eliminar(cancelar);
           
            return resultado;
        }

        internal int getNotificaciones()
        {
            return controladoraNotificaciones.getNumeroDeNotificaciones();

        }
    }
}