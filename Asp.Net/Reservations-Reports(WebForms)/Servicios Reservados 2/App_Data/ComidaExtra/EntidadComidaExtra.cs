using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Servicios_Reservados_2
{
    public class EntidadComidaExtra
    {
        private String idComidaExtra;
        private String idReservacion;
        private String idServiciosExtras;
        private String hora;
        private String fecha;
        private String consumido;
        private String descripcion;
        private int pax;
        private String tipoPago;
        
        
        public EntidadComidaExtra(Object[] datos)
        {
            this.idReservacion = datos[0].ToString();
            this.idServiciosExtras = datos[1].ToString();
            this.fecha = datos[2].ToString();
            this.consumido = datos[3].ToString();
            this.descripcion = datos[4].ToString();
            this.pax = int.Parse(datos[5].ToString());
            this.hora = datos[6].ToString();
            this.tipoPago = datos[7].ToString();
            this.idComidaExtra = datos[8].ToString();
        }

        public String IdReservacion
        {
            get { return idReservacion; }
            set { idReservacion = value; }
        }

        public String IdServiciosExtras
        {
            get { return idServiciosExtras; }
            set { idServiciosExtras = value; }
        }

        public String Fecha
        {
            get { return fecha; }
            set { fecha = value; }
        }

        public String Consumido
        {
            get { return consumido; }
            set { consumido = value; }
        }
        public String Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }
        public String Hora
        {
            get { return hora; }
            set { hora = value; }
        }
        public int Pax
        {
            get { return pax; }
            set { pax = value; }
        }
        public String TipoPago
        {
            get { return tipoPago; }
            set { tipoPago = value; }
        }
        public String IdComidaExtra
        {
            get { return idComidaExtra; }
            set { idComidaExtra = value; }
        }
    }
}