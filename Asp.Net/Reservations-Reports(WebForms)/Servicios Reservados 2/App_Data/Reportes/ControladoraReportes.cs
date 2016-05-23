using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Servicios_Reservados_2
{
    public class ControladoraReportes
    {
        ControladoraBDReportes controladoraBD = new ControladoraBDReportes();

        internal DataTable cargarEstaciones()
        {
            return controladoraBD.cargarEstaciones();
        }


        internal DataTable obtenerComidaPax(String estacion, int opcion,  int anfitriona, String fecha, String fechaFinal)
        {
           return controladoraBD.obtenerComidaPax(estacion, opcion, anfitriona, fecha, fechaFinal);
        }
        internal DataTable obtenerComidaPaxEmp(String estacion, int opcion, String fecha, String fechaFinal)
        {
            return controladoraBD.obtenerComidaPaxEmp(estacion, opcion, fecha, fechaFinal);
        }
        internal DataTable obtenerComidaEmp(String estacion, String opcion, String fecha, String fechaFinal)
        {
            return controladoraBD.obtenerComidaEmp(estacion,opcion, fecha, fechaFinal);
        }
        

    }
}