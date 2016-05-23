using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;

namespace Gyoza.App_Code.Modulo_Consulta
{
    public class ControladoraConsultas
    {
        ControladoraBDConsultas controladoraBD;

        public ControladoraConsultas() 
        {
            controladoraBD = new ControladoraBDConsultas();
        }

      /*  public DataTable consultarJerarquia(String idProyecto) 
        {
            return controladoraBD.consultarJerarquia(idProyecto);
        }

        public DataTable consultarRequerimientoEncargado(String idProyecto)
        {
            return controladoraBD.consultarRequerimientoEncargado(idProyecto);
        }

        public DataTable consultarHistoriasUsuarios()
        {
            return controladoraBD.consultarHistoriasUsuarios();
        }*/
    }
}