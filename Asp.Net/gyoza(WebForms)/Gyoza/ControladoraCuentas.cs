using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Gyoza.DataSet1TableAdapters;

namespace Gyoza.App_Code.Modulo_Cuenta{
    public class ControladoraCuentas{
        ControladoraBDCuenstas controladoraBDCuenta;

        public ControladoraCuentas(){
            controladoraBDCuenta = new ControladoraBDCuentas();
        }

        public String[] insertarCuenta(Object[] datos){
            EntidadCuenta cuenta = new EntidadCuenta(datos);

            return controladoraBDCuenta.insertarCuenta(cuenta);
        }

        public String[] modificarCuenta(Object[] datosNuevos, EntidadCuenta viejaCuenta){
            EntidadCuenta nuevaCuenta = new EntidadCuenta(datosNuevos);

            return controladoraBDCuenta.modificarCuenta(nuevaCuenta, viejaCuenta);
        }

        public String[] eliminarCuenta(EntidadCuenta cuentaEliminar){

            return controladoraBDCuenta.eliminarCuenta(cuentaEliminar);
        }

        public DataTable consultarCuenta(){

            return controladoraBDCuenta.consultarCuentas();
        }

        public EntidadProveedor consultarCuenta(String cedula){
            EntidadCuenta cuentaConsultada = null; //para encpasular los datos consultados.
            Object[] datosConsultados = new Object[6]; //para guardar los datos obtenidos de la consulta temporalmente

            //utiliza el método consultarProveedor de la ControladoraBdProveedor, el cual recibe la cédula.
            DataTable filaProveedor = controladoraBDCuenta.consultarCuenta(cedula);

            if (filaProveedor.Rows.Count == 1)
            { // si hay un valor
                for (int i = 0; i < 6; i++)
                {
                    datosConsultados[i] = filaProveedor.Rows[0][i].ToString();
                }

                cuentaConsultada = new EntidadCuenta(datosConsultados);
            }

            return cuentaConsultada;
        }
    }
}