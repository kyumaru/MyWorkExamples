using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gyoza.Modulo_Proyecto
{
    public class EntidadProyecto
    {
        private int identificador;
        private String nombre;
        private String objetivo_General;
        private DateTime fecha_Asignacion;
        private DateTime fecha_Inicio;
        private DateTime fecha_Finalizacion;
        private String estado;
        private String oficina_Propietaria;
        private String telefono_Oficina;
        private String representante_Usuario;
        private String correo_Representante;
        private String celular_Representante;

         public EntidadProyecto(Object[] datos)
        {
            this.identificador = (int)datos[0];
            this.nombre = datos[1].ToString();
            this.objetivo_General= datos[2].ToString();
            this.fecha_Asignacion = Convert.ToDateTime(datos[3].ToString());
            this.fecha_Inicio = Convert.ToDateTime(datos[4].ToString());
            this.fecha_Finalizacion = Convert.ToDateTime(datos[5].ToString());
            this.estado = datos[6].ToString();
            this.oficina_Propietaria = datos[7].ToString();
            this.telefono_Oficina = datos[8].ToString();
            this.representante_Usuario = datos[9].ToString();
            this.celular_Representante = datos[10].ToString();
            this.correo_Representante = datos[11].ToString();
        }
         public int Identificador 
         {
             get { return identificador; }
             set { identificador =value; }
         }

         //Setter y Getter del atributo Compania
         public String Nombre
         {
             get { return nombre; }
             set { nombre = value; }
         }

         //Setter y Getter del atributo Representante
         public String Objetivo_General
         {
             get { return objetivo_General; }
             set { objetivo_General= value; }
         }

         //Setter y Getter del atributo Telefono
         public String Telefono_Oficina
         {
             get { return telefono_Oficina; }
             set { telefono_Oficina = value; }
         }

         //Setter y Getter del atributo Celular
         public DateTime Fecha_Asignacion
         {
             get { return fecha_Asignacion; }
             set { fecha_Asignacion = value; }
         }

         public DateTime Fecha_Inicio
         {
             get { return fecha_Inicio; }
             set { fecha_Inicio = value; }
         }
         public DateTime Fecha_Finalizacion
         {
             get { return fecha_Finalizacion; }
             set { fecha_Finalizacion = value; }
         }
         //Setter y Getter del atributo Email
         public String Estado
         {
             get { return estado; }
             set { estado = value; }
         }

         //Setter y Getter del atributo Email
         public String Oficina_Propietaria
         {
             get { return oficina_Propietaria; }
             set { oficina_Propietaria= value; }
         }

         public String Representante_Usuario
         {
             get { return representante_Usuario; }
             set { representante_Usuario = value; }
         }

         public String Correo_Representante
         {
             get { return correo_Representante; }
             set { correo_Representante = value; }
         }

         public String Celular_Representante
         {
             get { return celular_Representante; }
             set { celular_Representante = value; }
         }
    }
}