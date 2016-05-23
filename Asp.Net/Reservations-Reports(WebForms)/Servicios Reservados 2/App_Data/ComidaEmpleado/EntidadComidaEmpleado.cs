using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Servicios_Reservados_2
{
    public class EntidadComidaEmpleado
    {
        //IDEMPLEADO*, FECHA*, PAGADO*, NOTAS*, DESAYUNO*, ALMUERZO*, CENA*, IDCOMIDAEMPLEADO*     
        private int idComida;
        internal int IdComida
        {
            get { return idComida; }
            set { idComida = value; }
        }
        private String idEmpleado;
        internal String IdEmpleado
        {
            get { return idEmpleado; }
            set { idEmpleado = value; }
        }
        private List<DateTime> fechas;
        internal List<DateTime> Fechas
        {
            get { return fechas; }
            set { fechas = value; }
        }
        private char[] turnos;
        internal char[] Turnos//[0] Desayuno [1] Almuerzo [2] Cena
        {
            get { return turnos; }
            set { turnos = value; }
        }
        private bool pagado;
        internal bool Pagado
        {
            set { pagado = value; }
            get { return pagado; }
        }
        private String notas;
        internal String Notas
        {
            set { notas = value; }
            get { return notas; }
        }
        private String estacion;
        internal String Estacion
        {
            set { estacion = value; }
            get { return estacion; }
        }

        public EntidadComidaEmpleado(String idEmpleado, String estacion, List<DateTime> fechasReserva, char[] turnos, bool pagado, String notas, int id = -1)
        {
            this.idComida = id;
            this.idEmpleado = idEmpleado;
            fechas = new List<DateTime>();
            foreach (DateTime fecha in fechasReserva)
            {
                fechas.Add(fecha);
            }
            this.turnos = new char[3];
            this.turnos[0] = turnos[0];
            this.turnos[1] = turnos[1];
            this.turnos[2] = turnos[2];
            this.pagado = pagado;
            this.notas = notas;
            this.estacion = estacion;
        }

    }
}