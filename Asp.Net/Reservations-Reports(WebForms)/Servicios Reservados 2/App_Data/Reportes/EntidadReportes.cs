using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Servicios_Reservados_2
{
    public class EntidadReportes
    {
        String estacion;
        String anfitriona;
        String idReservacion;
        String fechaInicial;
        String fechaFinal;

        public EntidadReportes(Object[] datos)
        {
            this.estacion = datos[0].ToString();
            this.anfitriona = datos[1].ToString();
            this.idReservacion = datos[2].ToString();
            this.fechaInicial = datos[3].ToString();
            this.fechaFinal = datos[4].ToString();
        }

        public String IdReservacion
        {
            get { return idReservacion; }
            set { idReservacion = value; }
        }

        public String Estacion
        {
            get { return estacion; }
            set { estacion = value; }
        }

        public String Anfitriona
        {
            get { return anfitriona; }
            set { anfitriona = value; }
        }

        public String FechaInicio
        {
            get { return fechaInicial; }
            set { fechaInicial = value; }
        }

        public String FechaFinal
        {
            get { return fechaFinal; }
            set { fechaFinal = value; }
        }
    }
}