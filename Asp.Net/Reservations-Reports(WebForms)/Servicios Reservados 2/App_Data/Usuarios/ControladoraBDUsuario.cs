using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.OleDb;
using System.Data;
using System.Diagnostics;

namespace Servicios_Reservados_2
{
    public class ControladoraBDUsuario
    {
        private AdaptadorBD adaptador;
        DataTable dt;
        /*
         * Requiere: N/A
         * Efectúa : inicializa las variables globales de la clase
         * retorna : N/A
         */
        public ControladoraBDUsuario()
        {
            adaptador = new AdaptadorBD();
            dt = new DataTable();
        }

        internal DataTable obtenerRolesDisponibles()
        {
            dt = new DataTable();
            String consultaSQL = "select nombre from rol";
            dt = adaptador.consultar(consultaSQL);
            return dt;
        }

        internal DataTable obtenerRolesAsignados()
        {
            dt = new DataTable();
            String consultaSQL = "select rol from usuariorol";
            dt = adaptador.consultar(consultaSQL);
            return dt;
        }

        internal DataTable consultarUsuarios()
        {
            dt = new DataTable();
            String consultaSQL = "select username, Nombre, estacion, activo from usuario";
            dt = adaptador.consultar(consultaSQL);
            return dt;
        }

        internal DataTable consultarTodosRoles()
        {
            dt = new DataTable();
            String consultaSQL = "select nombre from rol";
            dt = adaptador.consultar(consultaSQL);
            return dt;
        }

        /*
        * Efecto: inserta en la tabla de usuarios
        * Requiere: la entidad de usuario (datos encapsulados)
        * Modifica: la tabla usuario 
       */
        public String[] agregarUsuario(EntidadUsuario entidad, String contrasena)
        {
            String[] respuesta = new String[3];
            String consultaSQL = "insert into usuario values('" + entidad.Username + "','" + contrasena + "','" +
                    entidad.Correo + "', sysdate,'" + entidad.Estado + "','" + entidad.Estacion + "', 1,'" + entidad.Nombre + "')";
            respuesta = adaptador.insertar(consultaSQL);

            return respuesta;
        }

        /*
        * Efecto: inserta en la tabla de usuariorol
        * Requiere: la entidad de usuario (datos encapsulados), el rol
        * Modifica: la tabla usuariorol
       */
        public String[] agregarUsuarioRol(string usuario, string rol)
        {
            String[] respuesta = new String[3];
            String consultaSQL = "insert into usuariorol values('" + usuario + "','" + rol + "')";
            respuesta = adaptador.insertar(consultaSQL);
            return respuesta;
        }


        internal DataTable consultarUsuario(string usernameSeleccionado)
        {
            dt = new DataTable();
            String consultaSQL = "select username, Nombre, email, activo, estacion from usuario where username='" + usernameSeleccionado + "'";
            return adaptador.consultar(consultaSQL);
        }
        internal DataTable consultarUsuarioRol(string usernameSeleccionado)
        {
            dt = new DataTable();
            String consultaSQL = "select rol from usuariorol where usuario='" + usernameSeleccionado + "'";
            return adaptador.consultar(consultaSQL);
        }

        internal string[] modificarUsuario(EntidadUsuario entidad)
        {
            String[] respuesta = new String[3];
            String consultaSQL = "update usuario set nombre='" + entidad.Nombre + "', email='" + entidad.Correo + "', activo=" + entidad.Estado +
                ", estacion='" + entidad.Estacion + "' where username='" + entidad.Username + "'";
            respuesta = adaptador.insertar(consultaSQL);
            return respuesta;
        }

        internal string[] limpiarRoles(string usernameSeleccionado)
        {
            String[] respuesta = new String[3];
            String consultaSQL = "delete from usuariorol where usuario='" + usernameSeleccionado + "'";
            respuesta = adaptador.insertar(consultaSQL);
            return respuesta;
        }

        internal string[] desactivarUsuario(string username)
        {
            String[] respuesta = new String[3];
            String consultaSQL = "update usuario set activo =" + 0 + " where username='" + username + "'";
            respuesta = adaptador.insertar(consultaSQL);
            return respuesta;
        }

        internal string[] actualizarContrasena(string username, string contrasena)
        {
            String[] respuesta = new String[3];
            String consultaSQL = "update usuario set contrasena ='" + contrasena + "' where username='" + username + "'";
            respuesta = adaptador.insertar(consultaSQL);
            return respuesta;
        }

        internal DataTable seleccionarUsuariosFiltro(string estacion, string nombreUsuario, string nombre)
        {
            dt = new DataTable();
            String consultaSQL = "select username, Nombre, estacion, activo from usuario";
            //en caso de filtros
            if (!"".Equals(estacion) && "".Equals(nombreUsuario) && "".Equals(nombre))//un flitro
            {
                consultaSQL = "select username, Nombre, estacion, activo from usuario where estacion='" + estacion + "'";
            }
            else if ("".Equals(estacion) && !"".Equals(nombreUsuario) && "".Equals(nombre))
            {
                consultaSQL = "select username, Nombre, estacion, activo from usuario where LOWER(username) like '%" + nombreUsuario.ToLower() + "%'";
            }
            else if ("".Equals(estacion) && "".Equals(nombreUsuario) && !"".Equals(nombre))
            {
                consultaSQL = "select username, Nombre, estacion, activo from usuario where LOWER(nombre) like '%" + nombre.ToLower() + "%'";
            }
            else if (!"".Equals(estacion) && !"".Equals(nombreUsuario) && "".Equals(nombre))//dos filtros
            {
                consultaSQL = "select username, Nombre, estacion, activo from usuario where estacion='" + estacion + "' and LOWER(username) like '%" + nombreUsuario.ToLower() + "%'";
            }
            else if (!"".Equals(estacion) && "".Equals(nombreUsuario) && !"".Equals(nombre))
            {
                consultaSQL = "select username, Nombre, estacion, activo from usuario where estacion='" + estacion + "' and LOWER(nombre) like '%" + nombre.ToLower() + "%'";
            }
            else if ("".Equals(estacion) && !"".Equals(nombreUsuario) && !"".Equals(nombre))
            {
                consultaSQL = "select username, Nombre, estacion, activo from usuario where LOWER(username) like '%" + nombreUsuario.ToLower() + "%' and LOWER(nombre) like '%" + nombre.ToLower() + "%'";
            }
            else if (!"".Equals(estacion) && !"".Equals(nombreUsuario) && !"".Equals(nombre))//tres filtros
            {
                consultaSQL = "select username, Nombre, estacion, activo from usuario where estacion='" + estacion + "' and LOWER(username) like '%" + nombreUsuario.ToLower() + "%' and LOWER(nombre) like '%" + nombre.ToLower() + "%'";
            }

            return adaptador.consultar(consultaSQL);
        }
    }
}