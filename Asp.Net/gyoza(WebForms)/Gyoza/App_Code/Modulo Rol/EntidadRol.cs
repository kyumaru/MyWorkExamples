using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gyoza.Modulo_Rol
{
    public class EntidadRol
    {
        String idRol;
        String permisosProyecto;
        String permisosCuenta;
        String permisosRequerimiento;
        String permisosSeguridad;

        public EntidadRol(Object[] datos) {
            this.idRol = datos[0].ToString();
            this.permisosProyecto = datos[1].ToString();
            this.permisosCuenta = datos[2].ToString();
            this.permisosRequerimiento = datos[3].ToString();
            this.permisosSeguridad = datos[4].ToString();
        }

        public String IdRol
        {
            get { return idRol; }
            set { idRol = value; }
        }

        public String PermisosProyecto
        {
            get { return permisosProyecto; }
            set { permisosProyecto = value; }
        }

        public String PermisosCuenta
        {
            get { return permisosCuenta; }
            set { permisosCuenta = value; }
        }

        public String PermisosRequerimiento
        {
            get { return permisosRequerimiento; }
            set { permisosRequerimiento = value; }
        }

        public String PermisosSeguridad
        {
            get { return permisosSeguridad; }
            set { permisosSeguridad = value; }
        }
    }
}