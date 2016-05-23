using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Servicios_Reservados_2.ComidaExtra
{
    public class EntidadComidaExtra
    {
        private String idReservacionItem;
        private String idServiciosExtras;
        private DateTime fecha;
        private String consumido;
        private String descripcion;
        private int pax;
        
        
        public EntidadComidaExtra(Object[] datos)
        {
        
        }

        public String IdReservacionItem
        {
            get { return idReservacionItem; }
            set { idReservacionItem = value; }
        }

        public String IdServiciosExtras
        {
            get { return idServiciosExtras; }
            set { idServiciosExtras = value; }
        }

        public DateTime Fecha
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
        public int Pax
        {
            get { return pax; }
            set { pax = value; }
        }
        


}
}