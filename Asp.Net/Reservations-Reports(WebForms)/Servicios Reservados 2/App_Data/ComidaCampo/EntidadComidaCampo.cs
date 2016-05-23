using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace Servicios_Reservados_2
{

    public class EntidadComidaCampo
    {
        private String idComidaCampo;
        private String idEmpleado;
        private String idReservacion;
        private String fecha;
        private String estado;
        private int opcion;
        private String relleno;
        private String pan;
        private String bebida;
        private String tipoPago;
        private int pax;
        private String hora;
        private String estacion;
        private List<String> adicionales;

        public EntidadComidaCampo(Object[] datos, List<String>lista)
        {
            this.idComidaCampo = datos[0].ToString();
            this.idEmpleado = datos[1].ToString();
            this.idReservacion = datos[2].ToString();
            this.fecha = datos[3].ToString();
            this.estado = datos[4].ToString();
            this.opcion = int.Parse(datos[5].ToString());
            this.relleno = datos[6].ToString();
            this.pan = datos[7].ToString();
            this.bebida = datos[8].ToString();
            this.tipoPago = datos[9].ToString();
            this.pax = int.Parse(datos[10].ToString());
            this.hora = datos[11].ToString();
            this.estacion = datos[12].ToString();
            adicionales = lista;
        }

        public String IdComidaCampo
        {
            get { return idComidaCampo; }
            set { IdComidaCampo = value; }
        }

        public String IdEmpleado
        {
            get { return idEmpleado; }
            set { IdEmpleado = value; }
        }

        public String IdReservacion
        {
            get { return idReservacion; }
            set { IdReservacion = value; }
        }

        public String Fecha
        {
            get { return fecha; }
            set { Fecha = value; }
        }

        public String Estado
        {
            get { return estado; }
            set { Estado = value; }
        }
        public int Opcion
        {
            get { return opcion; }
            set { Opcion = value; }
        }
        public String Relleno
        {
            get { return relleno; }
            set { Relleno = value; }
        }
        public String Pan
        {
            get { return pan; }
            set { Pan = value; }
        }
        public String Bebida
        {
            get { return bebida; }
            set { Bebida = value; }
        }
        public String TipoPago
        {
            get { return tipoPago; }
            set { TipoPago = value; }
        }
        public int Pax
        {
            get { return pax; }
            set { Pax = value; }
        }
        public String Hora
        {
            get { return hora; }
            set { Hora = value; }
        }
        public String Estacion
        {
            get { return estacion; }
            set { estacion = value; }
        }
        public List<String> Adicionales 
        {
            get { return adicionales; }
            set { adicionales = value; }
        }
    }
}
