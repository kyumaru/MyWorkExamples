using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Servicios_Reservados_2
{
    public class EntidadServicios
    {
        private String idSolicitante;
        private String tipoSolicitante;
        private String idServicio;
        private String categoria;
        private String estado;
        private String fecha;
        private int pax;
        private String notas;
        private String hora;


        public EntidadServicios(String idSol, String tipoSolicitante, String id, String categoria, String fecha, String estado, int pax, String notas, String hora)
        {
            this.idSolicitante = idSol;
            this.tipoSolicitante = tipoSolicitante;
            this.idServicio = id;
            this.categoria = categoria;
            this.estado = estado;
            this.fecha = fecha;
            this.pax = pax;
            this.notas = notas;
            this.hora = hora;
        }
        public String IdSolicitante
        {
            get { return idSolicitante; }
            set { idSolicitante = value; }
        }
        public String TipoSolicitante
        {
            get { return tipoSolicitante; }
            set { tipoSolicitante = value; }
        }
        public String IdServicio
        {
            get { return idServicio; }
            set { idServicio = value; }
        }
        public String Categoria
        {
            get { return categoria; }
            set { categoria = value; }
        }
        public String Estado
        {
            get { return estado; }
            set { estado = value; }
        }

        public String Fecha
        {
            get { return fecha; }
            set { fecha = value; }
        }
        public int Pax
        {
            get { return pax; }
            set { pax = value; }
        }
        public String Notas
        {
            get { return notas; }
            set { notas = value; }
        }
        public String Hora
        {
            get { return hora; }
            set { hora = value; }
        }
    }


}