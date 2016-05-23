using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Servicios_Reservados_2
{
    public class ControladoraReporteCocina
    {
        private static ControladoraBDReporteCocina controladoraBD;

        public ControladoraReporteCocina()
        {
            controladoraBD = new ControladoraBDReporteCocina();
        }

        internal DataTable solicitarTurnoDiaTresComidas(String sigla, String inicio, String final)
        {
            return controladoraBD.solicitarTurnoDiaTresComidas(sigla, inicio, final);
        }

        internal DataTable solicitarTurnoDiaDosComidas(String sigla, String inicio, String final)
        {
            return controladoraBD.solicitarTurnoDiaDosComidas(sigla,inicio,final);
        }

        internal DataTable reservaEntrante(String sigla, String inicio, String final)
        {
            return controladoraBD.reservaEntrante(sigla, inicio, final);
        }

        internal DataTable solicitarCE(String estacion, String inicio, String final)
        {
            return controladoraBD.solicitarCE(estacion,inicio,final);

        }

        internal DataTable solicitarCC(String estacion, String inicio, String final)
        {
            return controladoraBD.solicitarCC(estacion, inicio, final);

        }

        internal DataTable solicitarBebidas(String estacion, String inicio, String final)
        {
            return controladoraBD.solicitarBebidas(estacion, inicio, final);

        }

        internal DataTable solicitarAdicionales(String estacion, String inicio, String final)
        {
            return controladoraBD.solicitarAdicionales(estacion, inicio, final);

        }
    }
}