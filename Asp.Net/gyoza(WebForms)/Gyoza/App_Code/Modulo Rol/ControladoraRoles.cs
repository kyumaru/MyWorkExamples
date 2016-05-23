using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Gyoza.Modulo_Rol
{
    public class ControladoraRoles
    {
        ControladoraBDRoles controladoraBDRol;

        public ControladoraRoles()
        {
            controladoraBDRol = new ControladoraBDRoles();
        }

        public DataTable consultarRoles()
        {
            return controladoraBDRol.consultarRoles();
        }

        public EntidadRol consultarRol(String idRol)
        {
            return controladoraBDRol.consultarRol(idRol);
        }

        public String[] modificarRol(Object[] datosNuevos, EntidadRol viejaCuenta)
        {
            EntidadRol nuevaRol = new EntidadRol(datosNuevos);

            return controladoraBDRol.modificarRol(nuevaRol, viejaCuenta);
        }
    }
}