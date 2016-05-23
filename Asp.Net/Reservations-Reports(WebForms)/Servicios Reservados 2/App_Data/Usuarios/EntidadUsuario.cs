using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Servicios_Reservados_2
{
    public class EntidadUsuario
    {
        private String username;
        private String nombre;
        private String correo;
        private int estado;
        private String estacion;
        List<string> rol;
        
        public EntidadUsuario(Object[] datos)
        {
            this.username = datos[0].ToString();
            this.nombre = datos[1].ToString();
            this.correo = datos[2].ToString();
            this.estado = 1;
            if ("Inactivo".Equals(datos[2].ToString()) || "0".Equals(datos[2].ToString()))
            {
                this.estado = 0;
            }            
            this.estacion = datos[4].ToString();
            this.rol = (List<string>)datos[5];                     
        }

        public String Username
        {
            get { return username; }
            set { username = value; }
        }

        public String Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        public String Correo
        {
            get { return correo; }
            set { correo = value; }
        }

        public int Estado
        {
            get { return estado; }
            set { estado = value; }
        }
        public String Estacion
        {
            get { return estacion; }
            set { estacion = value; }
        }
        public List<string> Rol
        {
            get { return rol; }
            set { rol = value; }
        }
       
    }
}