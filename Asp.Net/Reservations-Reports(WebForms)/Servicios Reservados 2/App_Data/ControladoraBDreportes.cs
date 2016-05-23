using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Diagnostics;

namespace Servicios_Reservados_2
{
    public class ControladoraBDreportes
    {
        private AdaptadorBD adaptador;
        DataTable dt;

        public ControladoraBDreportes()
        {
            adaptador = new AdaptadorBD();
            dt = new DataTable();
        }

       
       
        internal DataTable test(string fecha,string estacion)
        {
            String consultaSQL = "SELECT distinct entra,sale,primera_comida,pax,cantidad,al.nombre as tipo_comida,vecesconsumido FROM RESERVAS.reservacion r INNER JOIN RESERVAS.reservacionitem ri ON  r.id=ri.reservacion and r.estado='CNF' and TO_DATE('" + fecha + "','dd/mm/yy') BETWEEN r.entra AND r.sale INNER JOIN RESERVAS.alimento al on ri.reservable=al.id and al.cantidad>0 and (al.NOMBRE Like '%0%' OR al.NOMBRE Like '%Almuer%' OR al.NOMBRE  Like '%CENA%' OR al.NOMBRE  Like '%Desay%' OR al.NOMBRE  Like '%Comida%' OR al.NOMBRE  Like '%SNACK%') INNER JOIN RESERVAS.estacion e ON  r.estacion=e.id and e.siglas='" + estacion + "' order by pax desc ";

            dt = adaptador.consultar(consultaSQL);
            return dt;
        }

        /*fehca2 should not be less than fecha1*/
        internal DataTable testFechas(string fecha1,string fecha2, string estacion)
        {
            String consultaSQL = "SELECT distinct entra,sale,primera_comida,pax,cantidad,al.nombre as tipo_comida FROM RESERVAS.reservacion r INNER JOIN RESERVAS.reservacionitem ri ON  r.id=ri.reservacion and r.estado='CNF' AND( TO_DATE('" + fecha1 + "','dd/mm/yy') between r.entra AND r.sale OR TO_DATE('" + fecha2 + "','dd/mm/yy')between r.entra AND r.sale OR TO_DATE(r.entra,'dd/mm/yy')between TO_DATE('" + fecha1 + "','dd/mm/yy') AND TO_DATE('" + fecha2 + "','dd/mm/yy') OR TO_DATE(r.sale,'dd/mm/yy')between TO_DATE('" + fecha1 + "','dd/mm/yy') AND TO_DATE('" + fecha2 + "','dd/mm/yy' ) )INNER JOIN RESERVAS.alimento al on ri.reservable=al.id and al.cantidad>0 and (al.NOMBRE Like '%0%' OR al.NOMBRE  Like '%Almuer%' OR al.NOMBRE  Like '%CENA%' OR al.NOMBRE  Like '%Desay%' OR al.NOMBRE  Like '%Comida%' OR al.NOMBRE  Like '%SNACK%') INNER JOIN RESERVAS.estacion e ON  r.estacion=e.id and e.siglas='" + estacion + "' order by pax desc ";

            dt = adaptador.consultar(consultaSQL);
            return dt;
        }

        /**CONSULTAAS PARA TABLAS DE GRUPO**/

        internal DataTable testDetalleOET(string fecha, string estacion)
        {
            String consultaSQL = "SELECT distinct entra,sale,primera_comida,pax,cantidad,al.nombre as tipo_comida,'   'as comidas,an.siglas as anfitriona,concat(concat(saludoid,' '),concat(concat(c.nombre,' '),c.apellidos))as nombreCliente,r.notas,r.numero as reservacion FROM RESERVAS.reservacion r INNER JOIN RESERVAS.reservacionitem ri ON  r.id=ri.reservacion and r.estado='CNF' and TO_DATE('" + fecha + "','mm/dd/yyyy') BETWEEN r.entra AND r.sale INNER JOIN RESERVAS.alimento al on ri.reservable=al.id and al.cantidad>0 and (al.NOMBRE Like '%0%' OR al.NOMBRE Like '%Almuer%' OR al.NOMBRE  Like '%CENA%' OR al.NOMBRE  Like '%Desay%' OR al.NOMBRE  Like '%Comida%' OR al.NOMBRE  Like '%SNACK%') INNER JOIN RESERVAS.estacion e ON  r.estacion=e.id and e.siglas='" + estacion + "' INNER JOIN RESERVAS.anfitriona an ON an.id=r.anfitriona INNER JOIN RESERVAS.contacto c ON c.id=r.solicitante order by pax desc ";
            dt = adaptador.consultar(consultaSQL);
            return dt;
        }


        internal DataTable testDetalleServiciosExtra(string fecha, string estacion)
        {
            String consultaSQL = "select pax as comensales,tipo as comidas, an.siglas as anfitriona,concat(concat(saludoid,' '),concat(concat(c.nombre,' '),c.apellidos))as cliente,r.notas,r.numero as referencia FROM servicio_especial ses INNER JOIN servicios_extras sex ON ses.IDSERVICIOSEXTRAS=sex.idservicio AND ses.estado Like '%_ctivo%' AND TO_DATE(ses.fecha,'mm/dd/yy')=TO_DATE('"+fecha+"','dd/mm/yy') INNER JOIN RESERVAS.reservacion r ON r.id=idreservacion and r.estado='CNF' INNER JOIN RESERVAS.estacion e ON  r.estacion=e.id and e.siglas='"+estacion+"' INNER JOIN RESERVAS.anfitriona an ON an.id=r.anfitriona INNER JOIN RESERVAS.contacto c ON c.id=r.solicitante order by pax desc";
            dt = adaptador.consultar(consultaSQL);
            return dt;
        }

        internal DataTable testDetalleEmpleados(string fecha)
        {
            String consultaSQL = "select TO_NUMBER('1') as comensales, tipo as comidas, '-'as anfitriona,concat(concat( emp.NOMBRE,' '),emp.APELLIDOS)as cliente, semp.DESCRIPCION as notas ,concat(concat( 'ID empleado',' '),numempleado) as referencia from servicio_empleado semp INNER JOIN servicios_extras sex ON semp.IDSERVICIOSEXTRAS=sex.idservicio AND semp.fecha=TO_DATE('"+fecha+"','dd/mm/yy') INNER JOIN FINANCIERO.empleados emp ON semp.IDEMPLEADO=emp.numempleado";
            dt = adaptador.consultar(consultaSQL);
            return dt;
        }


        internal DataTable testServicioExtra(string fecha, string estacion)
        {
            String consultaSQL = "select pax,tipo,vecesconsumido FROM servicio_especial ses INNER JOIN servicios_extras sex ON ses.IDSERVICIOSEXTRAS=sex.idservicio AND ses.estado Like '%_ctivo%' AND TO_DATE(ses.fecha,'mm/dd/yy')=TO_DATE('" + fecha + "','dd/mm/yy') INNER JOIN RESERVAS.reservacion r ON r.id=idreservacion and r.estado='CNF'INNER JOIN RESERVAS.estacion e ON  r.estacion=e.id and e.siglas='" + estacion + "'";

            dt = adaptador.consultar(consultaSQL);
            return dt;
        }


        internal DataTable testServicioEmpleado(string fecha)
        {

            String consultaSQL = "select TO_NUMBER('1') as pax, tipo, TO_NUMBER(consumido) as vecesconsumido from servicio_empleado semp INNER JOIN servicios_extras sex ON semp.IDSERVICIOSEXTRAS=sex.idservicio AND semp.fecha=TO_DATE('" + fecha + "','dd/mm/yy')";

            dt = adaptador.consultar(consultaSQL);
            return dt;
        }

    }
}