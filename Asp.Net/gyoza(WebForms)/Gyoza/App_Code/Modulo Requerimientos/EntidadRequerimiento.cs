using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gyoza.Modulo_Requerimientos
{
    public class EntidadRequerimiento
    {
        int idProyecto;
        String nombre;
        int prioridad;
        String estado;
        String rol;
        String contenido;
        String razon;
        int estimacion;
        int sprint;
        String modulo;
        bool funcional;
        String idEncargado1;
        String idEncargado2;

        public EntidadRequerimiento(Object[] datos)
        {
            this.idProyecto = Convert.ToInt32(datos[0]);
            this.nombre = datos[1].ToString();
            this.prioridad = Convert.ToInt32(datos[2]);
            this.estado = datos[3].ToString();
            this.rol = datos[4].ToString();
            this.contenido = datos[5].ToString();
            this.razon = datos[6].ToString();
            this.estimacion = Convert.ToInt32(datos[7]);
            this.sprint = Convert.ToInt32(datos[8]);
            if (datos[9] != null && datos[9] != "")
                this.modulo = datos[9].ToString();
            else
                this.modulo = null;
            this.funcional = Convert.ToBoolean(datos[10]);

            if (datos[11] != null)
            {
                if (datos[11].ToString() != "-")
                    this.idEncargado1 = datos[11].ToString();
                else
                    this.idEncargado1 = null;
            } 
            else
                this.idEncargado1 = null;

            if (datos[12] != null)
            {
                if (datos[12].ToString() != "-")
                    this.idEncargado2 = datos[12].ToString();
                else
                    this.idEncargado2 = null;
            }
            else
                this.idEncargado2 = null;
        }


        public int IdProyecto
        {
            get { return idProyecto; }
            set { idProyecto = value; }
        }

        public String Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        public int Prioridad
        {
            get { return prioridad; }
            set { prioridad = value; }
        }

        public String Estado
        {
            get { return estado; }
            set { estado = value; }
        }

        public String Rol
        {
            get { return rol; }
            set { rol = value; }
        }

        public String Contenido
        {
            get { return contenido; }
            set { contenido = value; }
        }

        public String Razon
        {
            get { return razon; }
            set { razon = value; }
        }

        public int Estimacion
        {
            get { return estimacion; }
            set { estimacion = value; }
        }

        public int Sprint
        {
            get { return sprint; }
            set { sprint = value; }
        }

        public String Modulo
        {
            get { return modulo; }
            set { modulo = value; }

        }

        public Boolean Funcional
        {
            get { return funcional; }
            set { funcional = value; }
        }

        public String IdEncargado1
        {
            get { return idEncargado1; }
            set { idEncargado1 = value; }
        }

        public String IdEncargado2
        {
            get { return idEncargado2; }
            set { idEncargado2 = value; }
        }


    }
}