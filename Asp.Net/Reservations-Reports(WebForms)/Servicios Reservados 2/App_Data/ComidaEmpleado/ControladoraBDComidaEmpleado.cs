using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace Servicios_Reservados_2
{
    public class ControladoraBDComidaEmpleado
    {
        private AdaptadorBD adaptador = new AdaptadorBD();
        /*
         * Requiere: Una Entidad Comida Empleado con los datos a insertar
         * Efectua: Crea la insercion a traves de los datos proporcionados en la entidad encapsulada e inserta los datos a traves del adaptador.
         * Retorna: Un arreglo de hileras con el mensaje de confirmacion u error de la consulta.
         */
        public String[] agregar(EntidadComidaEmpleado nuevo)
        {
            String[] resultado = new String[3];
            String turnos="" ;
            turnos += "'" + (nuevo.Turnos[0]) + "'";
            turnos += ",";
            turnos += "'" + (nuevo.Turnos[1]) + "'";
            turnos += ", ";
            turnos += "'" + (nuevo.Turnos[1]) + "'";
            try
            {
                foreach (DateTime fecha in nuevo.Fechas)
                {
                    //Crea la sentencia en sql para insertar.
                    String insercion = " Insert into Reserva_EMPLEADO (idEmpleado,fecha, Pagado, notas, desayuno, almuerzo, cena, estacion)values (";
                    insercion += ("'" + nuevo.IdEmpleado + "',");
                    insercion += ("TO_DATE('" + fecha.ToString() + "' ,'MM/dd/yyyy hh:mi:ss AM') ,");
                    insercion += ("'" + ((nuevo.Pagado)?'T':'F') + "',");
                    insercion += ("'" + nuevo.Notas + "',");
                    insercion += turnos + ", '";
                    insercion += nuevo.Estacion + "' )";
                    resultado=adaptador.insertar(insercion);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
            }
            return resultado;
        }
        /*
         * Requiere: Una entidad de comida Empleado con los datos antiguos y una Entidad de comida empleado con los datos encapsulados.
         * Efectua : Crea una consulta de actualizacion de los datos y lo actualiza en la base de datos.
         * Retorna : Retorna: Un arreglo de hileras con el mensaje de confirmacion u error de la consulta.
         */
        internal String[] modificar(EntidadComidaEmpleado seleccionada, EntidadComidaEmpleado nuevo)
        {
            String[] resultado = new String[3];
            try
            {
                foreach (DateTime fecha in nuevo.Fechas)
                {
                    string update = "UPDATE RESERVA_EMPLEADO SET ";
                    if (seleccionada.Turnos[0] == 'C' && nuevo.Turnos[0] != 'C')//DESAYUNO
                    {//Si ya se sirvio no se puede cancelar. 
                        throw new Exception();
                    }
                    else
                    {
                        update += "Desayuno = '" + nuevo.Turnos[0] + "' ,";
                        //R = Reservado C= Consumido N=No reservado X=Cancelado

                    }
                    if (seleccionada.Turnos[1] == 'C' && nuevo.Turnos[1] != 'C')//ALMUERZO
                    {//Si ya se sirvio no se puede cancelar. 
                        throw new Exception();
                    }
                    else
                    {
                        update += "Almuerzo = '" + nuevo.Turnos[1] + "' ,";
                        //R = Reservado C= Consumido N=No reservado X=Cancelado

                    }
                    if (seleccionada.Turnos[2] == 'C' && nuevo.Turnos[2] != 'C')//CENA
                    {//Si ya se sirvio no se puede cancelar. 
                        throw new Exception();
                    }
                    else
                    {
                        update += "CENA = '" + nuevo.Turnos[1] + "', pagado =";
                        //R = Reservado C= Consumido N=No reservado X=Cancelado

                    }
                    update += (nuevo.Pagado) ? "'T'," : "'F',";//Si esta o no pagado.
                    update += " notas = '" + nuevo.Notas + "' "; // predicado del update completo.
                    update += "estacion = '" + nuevo.Estacion + "' ";

                    //----------------------------------------------------------------------
                    update += "WHERE IDCOMIDAEMPLEADO =" + seleccionada.IdComida ;
                    adaptador.insertar(update);
                    resultado[0] = "SUCCESS";
                    resultado[1] = "Exito: ";
                    resultado[2] = "Los datos se Modificaron correctamente.";
                }
            }
            catch (Exception e)
            {
                resultado[0] = "DANGER";
                resultado[1] = "ERROR: ";
                resultado[2] = "No se pudo modificar el elemento, por favor verifique los datos, No se puede cancelar comidas que ya han sido servidas.";
            }
            return resultado;
        }
        /*
         * Requiere: una hilera con el identificador del empleado y una fecha con la fecha que se quiere consultar.
         * Efectua : Crea una consulta para consultar los datos individuales de una reservacion de comida de empleado. 
         * Retorna : un data table con los estados de la  reservacion de los turnos y si ya fue pagado.
        */
        internal DataTable getInformacionReservacionEmpleado(int idReservacionComida)
        {
            string consulta = "SELECT IDEMPLEADO, FECHA, PAGADO, NOTAS, DESAYUNO, ALMUERZO, CENA, IDCOMIDAEMPLEADO, ESTACION FROM RESERVA_EMPLEADO Where idcomidaempleado =" + idReservacionComida ;
            DataTable dt = adaptador.consultar(consulta);
            return dt;
        }
        /*
         * Requiere: una hilera con el identificador del empleado.
         * Efectua : Crea una consulta para consultar los datos de las reservaciones de comida de un empleado. 
         * Retorna : un data table con los estados de la  reservacion de los turnos y si ya fue pagado.
        */

        internal DataTable getReservacionesEmpleado(string idEmpleado)
        {
            DataTable dt = new DataTable();
            String consulta = "SELECT IDCOMIDAEMPLEADO,'Comida regular',IDEMPLEADO,FECHA,PAGADO FROM servicios_reservados.RESERVA_EMPLEADO WHERE IDEMPLEADO = '" + idEmpleado + "'AND FECHA >= ADD_MONTHS(SYSDATE, - 1) AND NOT(CENA='X' And ALMUERZO='X' AND DESAYUNO ='X' ) Order By IDCOMIDAEMPLEADO DESC";
            dt = adaptador.consultar(consulta);
            return dt;
        }
        /*
        * Requiere: una entidad comida empleado.
        * Efectua : Crea una consulta para poner en cancelado los datos de una comida empleado. 
        * Retorna : un arreglo de hileras con el resultado.
        */
        internal String[] cancelar(EntidadComidaEmpleado entidadComidaEmpleado)
        {
            String[] resultado = new String[3];

            try
            {
                if (entidadComidaEmpleado.Turnos[0] == 'C' || entidadComidaEmpleado.Turnos[1] == 'C' || entidadComidaEmpleado.Turnos[2] == 'C')//si alguna ya fue consumida
                {
                    throw new Exception(); //no se puede cancelar

                }
                else
                {
                    String update = "UPDATE RESERVA_EMPLEADO SET Desayuno ='X', Almuerzo ='X', cena='X' WHERE IDCOMIDAEMPLEADO = " + entidadComidaEmpleado.IdComida ;
                    adaptador.insertar(update);
                    resultado[0] = "SUCCESS";
                    resultado[1] = "Exito: ";
                    resultado[2] = "Los datos se Modificaron correctamente.";
                }
            }
            catch (Exception e)
            {

                resultado[0] = "DANGER";
                resultado[1] = "ERROR: ";
                resultado[2] = "No se pudo cancelar el elemento, por favor verifique los datos, No se puede cancelar comidas que ya han sido servidas.";
            }
            return resultado;
        }
        /*
        * Requiere: una hilera con el idServicio.
        * Efectua : Crea una consulta para consultar las veces consumido de un servico de comida empleado. 
        * Retorna : un arreglo de hileras con el resultado.
        */
        internal DataTable vecesConsumido(string idServicio)
        {
            String consultaSQL = "select vecesconsumido from servicios_reservados.reserva_empleado where idcomidaempleado ='" + idServicio+ "'";
            return adaptador.consultar(consultaSQL); 
        }
        /*
        * Requiere: una hilera con el idServicio y un entero con numero de veces.
        * Efectua : Crea una consulta para actualizar las veces consumido de un servico de comida empleado. 
        * Retorna : un arreglo de hileras con el resultado.
        */

        internal void actualizarVecesConsumido(string idServicio, int vecesConsumido)
        {
            String consultaSQL = "update servicios_reservados.reserva_empleado set vecesconsumido= " + vecesConsumido + " where idcomidaempleado ='" + idServicio +  "'";
            adaptador.consultar(consultaSQL); 
        }
    }
}