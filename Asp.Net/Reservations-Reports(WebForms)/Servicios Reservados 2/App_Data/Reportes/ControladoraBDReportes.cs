using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Servicios_Reservados_2
{
    public class ControladoraBDReportes
    {
        AdaptadorBD adaptador = new AdaptadorBD();

        internal DataTable cargarEstaciones()
        {
            DataTable estaciones;
            String consultaSQL = "select nombre from reservas.estacion";
            estaciones = adaptador.consultar(consultaSQL);
            return estaciones;
        }

        internal DataTable obtenerComidaPax(String estacion, int opcion, int anfitriona, String fecha, String fechaFinal)
        {
            DataTable comidaCampo;

            String consultaSQL = "select distinct campo.fecha, SUM(campo.pax), sum(vecesconsumido) from (reservas.vr_reservacion vista JOIN reservas.reservacion reserva ON vista.numero = reserva.numero) JOIN comida_campo campo ON reserva.id = campo.idreservacion WHERE opcion = "+ opcion +" and vista.estacion = '" + estacion + "' and vista.idanfitriona = " + anfitriona + " and to_date(campo.fecha, 'MM/dd/yyyy') <= to_date('"+ fechaFinal +"', 'MM/dd/yyyy') and to_date(campo.fecha, 'MM/dd/yyyy') >= to_date('"+ fecha +"', 'MM/dd/yyyy') group by campo.fecha";
            comidaCampo = adaptador.consultar(consultaSQL);
            return comidaCampo;
        }
        internal DataTable obtenerComidaPaxEmp(String estacion, int opcion, String fecha, String fechaFinal)
        {
            DataTable comidaCampoEmp;

            String consultaSQL = "select distinct fecha, sum(pax), sum(vecesconsumido) from comida_campo where estacion = '" + estacion + "' and opcion = " + opcion + " and to_date(fecha, 'MM/dd/yyyy') <= to_date('" + fechaFinal + "', 'MM/dd/yyyy') and to_date(fecha, 'MM/dd/yyyy') >= to_date('" + fecha + "', 'MM/dd/yyyy') and idempleado is not null group by fecha";
            comidaCampoEmp = adaptador.consultar(consultaSQL);
            return comidaCampoEmp;
        }

        internal DataTable obtenerComidaEmp(String estacion, String opcion, String fecha, String fechaFinal)
        {
            DataTable comidaCampoEmp;

            String consultaSQL = "select distinct fecha, count("+opcion+"), count(vecesconsumido) from reserva_empleado where "+opcion+" = 'R' and fecha >= to_date('" + fecha + "', 'mm/dd/yyyy') and fecha <= to_date('"+fechaFinal+"', 'mm/dd/yyyy') and estacion ='" + estacion + "' group by fecha";
            comidaCampoEmp = adaptador.consultar(consultaSQL);
            return comidaCampoEmp;
        }

        
    }
}