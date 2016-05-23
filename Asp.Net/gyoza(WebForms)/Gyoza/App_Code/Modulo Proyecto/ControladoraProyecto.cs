using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace Gyoza.Modulo_Proyecto
{
    public class ControladoraProyecto
    {
        private ControladoraBDProyecto controladoraBDProyecto;

        public ControladoraProyecto()
        {
               controladoraBDProyecto = new ControladoraBDProyecto();
        }

       
        public EntidadProyecto consultarProyecto(int id)
        {
            //retorna los datos del proyecto
            return controladoraBDProyecto.consultarProyecto(id);
        }

        public String[] insertarDatos(Object[] datosProyecto)
        {   
           // String[] res = new String[3];
            EntidadProyecto proyecto = new EntidadProyecto(datosProyecto);
            return controladoraBDProyecto.insertarProyecto(proyecto);
            //return res;
        }

        public String[] modificarDatos(EntidadProyecto proyectoViejo, Object[] datosProyectoNuevo)
        {
           // String[] res = new String[3];
            EntidadProyecto proyectoNuevo = new EntidadProyecto(datosProyectoNuevo);
            return controladoraBDProyecto.modificarProyecto(proyectoViejo, proyectoNuevo);
            
            //return res;
        }

        public String[] eliminarProyecto(EntidadProyecto proyecto)
        {
            return controladoraBDProyecto.eliminarProyecto(proyecto);
        }

        public DataTable consultarProyectos()
        {
            return controladoraBDProyecto.consultarProyectos();
        }

        public String[] asociarMiembroEquipo(int idProyecto, Object[][] miembrosDeEquipo)
        {
            return controladoraBDProyecto.asociarMiembroEquipo(idProyecto, miembrosDeEquipo);
        }
        
        public String[] desasociarMiembroEquipo(int idProyecto, Object[][] miembrosDeEquipo)
        {
            return controladoraBDProyecto.desasociarMiembrosEquipo(idProyecto,miembrosDeEquipo);
        }
        public String[] asignarLider(int idProyecto, Object[] miembroDeEquipo)
        {
            return controladoraBDProyecto.asignar_lider(idProyecto, miembroDeEquipo);
        }

        public DataTable obtenerProyectosAsociados(String cedula)
        {
            
            return controladoraBDProyecto.obtenerProyectosAsociados(cedula);
        }
        public DataTable obtenerJerarquia(int idProyecto)
        {
            return controladoraBDProyecto.obtenerJerarquia(idProyecto);
        }
    }
}