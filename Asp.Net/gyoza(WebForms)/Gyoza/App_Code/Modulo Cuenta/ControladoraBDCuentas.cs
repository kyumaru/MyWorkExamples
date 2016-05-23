using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Gyoza.DataSet1TableAdapters;

namespace Gyoza.Modulo_Cuenta
{

    public class ControladoraBDCuentas
    {

        CuentaTableAdapter adapterCuenta;

        public ControladoraBDCuentas()
        {
            adapterCuenta = new CuentaTableAdapter();
        }

        public String[] insertarCuenta(EntidadCuenta nuevaCuenta)
        {
            //para retornar un mensaje de notificación después de intentar un insert en la base de datos.
            String[] resultado = new String[4];
            resultado[3] = nuevaCuenta.Cedula;
            try
            {
                /* se accesan los atributos de nuevaCuenta para insertarlos en 
                 * la base de datos usando el Insert del CuentaTableAdapter
                 */
                this.adapterCuenta.Insert(nuevaCuenta.Cedula, nuevaCuenta.Nombre, nuevaCuenta.Apellidos,
                                         nuevaCuenta.Correo,nuevaCuenta.Rol, nuevaCuenta.Usuario, nuevaCuenta.Password, 
                                         nuevaCuenta.TelefonoOficina, nuevaCuenta.TelefonoCelular);

                /* Al insertar exitosamente, se llena el vector
                 * de resultado con los menajes pertinentes.
                 */
                resultado[0] = "success";
                resultado[1] = "Exito. ";
                resultado[2] = "La cuenta se ha ingresado exitosamente";
            }
            catch (SqlException e)
            {
                int r = e.Number;

                if (r == 2627)
                {
                    //error = "La cedula esta repetida."
                    resultado[0] = "danger";
                    resultado[1] = "Error. ";
                    resultado[2] = "La cedula ingresada ya existe";
                }
                else
                {
                    /*en cualquier otro caso no se pudo insertar la cuenta*/
                    //resultado[0] = "danger";
                    resultado[0] = "" + e;
                    resultado[1] = "Error. ";
                    resultado[2] = "No se pudo agregar cuenta";
                }
            }

            //se devuelve el vector que lleva la información del resultado de la inserción.
            return resultado;
        }

        public String[] modificarCuenta(EntidadCuenta nuevaCuenta, EntidadCuenta viejaCuenta)
        {
            //para retornar un mensaje de notificación después de intentar un insert en la base de datos.
            String[] resultado = new String[3];

            try
            {
                /* se accesan los atributos de nuevaCuenta para insertarlos en 
                 * la base de datos usando el Insert del CuentaTableAdapter
                 */
                
                this.adapterCuenta.Update(nuevaCuenta.Cedula, nuevaCuenta.Nombre, nuevaCuenta.Apellidos,
                                         nuevaCuenta.Correo,nuevaCuenta.Rol, nuevaCuenta.Usuario, nuevaCuenta.Password, 
                                         nuevaCuenta.TelefonoOficina, nuevaCuenta.TelefonoCelular, viejaCuenta.Cedula, 
                                         viejaCuenta.Nombre, viejaCuenta.Apellidos, viejaCuenta.Correo, viejaCuenta.Rol, 
                                         viejaCuenta.Usuario, viejaCuenta.Password,viejaCuenta.TelefonoOficina, viejaCuenta.TelefonoCelular);
      
                /*
                 * 
                 * 
                 * Al insertar exitosamente, se llena el vector
                 * de resultado con los menajes pertinentes.
                 * 
                 * 
                 */
                resultado[0] = "success";
                resultado[1] = "Exito. ";
                resultado[2] = "La cuenta se ha modificado correctamente";
            }
            catch (SqlException e)
            {
                int r = e.Number;

                if (r == 2627)
                {
                    //error = "La cedula esta repetida."
                    resultado[0] = "danger";
                    resultado[1] = "Error. ";
                    resultado[2] = "La cedula ingresada ya existe";
                }
                else
                {
                    /*en cualquier otro caso no se pudo insertar la cuenta*/
                    resultado[0] = "danger";
                    resultado[1] = "Error. ";
                    resultado[2] = "No se pudo modificar cuenta";
                }
            }

            //se devuelve el vector que lleva la información del resultado de la inserción.
            return resultado;
        }

        public String[] eliminarCuenta(EntidadCuenta cuentaEliminar)
        {
            //para retornar un mensaje de notificación después de intentar un update en la base de datos.
            String[] resultado = new String[3];

            try
            {
                /* se accesan los atributos de nuevaCuenta y de viejaCuenta para eliminarlo de 
                 * la base de datos usando el Delete del CuentaTableAdapter
                 */
                adapterCuenta.Delete(cuentaEliminar.Cedula,cuentaEliminar.Nombre, cuentaEliminar.Apellidos, cuentaEliminar.Correo,
                   cuentaEliminar.Rol, cuentaEliminar.Usuario, cuentaEliminar.Password, cuentaEliminar.TelefonoOficina, cuentaEliminar.TelefonoCelular);

                /* Al eliminar exitosamente, se llena el vector
                 * de resultado con los menajes pertinentes.
                 */
                resultado[0] = "success";
                resultado[1] = "Exito. ";
                resultado[2] = "Se elimino exitosamente la cuenta";

            }
            catch (Exception e)
            {
                // error = "No ha sido posible eliminar la cuenta.";
                resultado[0] = "danger";
                resultado[1] = "Error. ";
                resultado[2] = "No se pudo eliminar la cuenta";

            }

            //se devuelve el vector que lleva la información del resultado de la modifiación.
            return resultado;
        }

        public DataTable consultarCuentas()
        {
            DataTable resultado = new DataTable();

            try
            {
                resultado = this.adapterCuenta.GetData();
            }
            catch (Exception e)
            {
                resultado = null;
            }
            //finalmente retorna los datos obtenidos.
            return resultado;
        }

        public EntidadCuenta consultarCuenta(String id)
        {
            DataTable resultado = new DataTable();
            EntidadCuenta cuentaConsultada = null; //para encpasular los datos consultados.
            Object[] datosConsultados = new Object[9]; //para guardar los datos obtenidos de la consulta temporalmente

            try
            {
                resultado = adapterCuenta.consultarFilaCuenta(id);

                if (resultado.Rows.Count == 1)
                { // si hay un valor
                    for (int i = 0; i < 9; i++)
                    {
                        // obtiene los atributos y los guarda en datosConsultados
                        datosConsultados[i] = resultado.Rows[0][i].ToString();
                    }

                    //Se encapsulan los datos utilizando la clase entidadProveedor
                    cuentaConsultada = new EntidadCuenta(datosConsultados);
                }
            }
            catch (Exception e) { }

            return cuentaConsultada;
        }

        public EntidadCuenta consultarCuentaUsuario(String usuario)
        {
            DataTable resultado = new DataTable();
            EntidadCuenta cuentaConsultada = null; //para encpasular los datos consultados.
            Object[] datosConsultados = new Object[9]; //para guardar los datos obtenidos de la consulta temporalmente

            try
            {
                resultado = adapterCuenta.consultarFilaCuentaUsuario(usuario);

                if (resultado.Rows.Count == 1)
                { // si hay un valor
                    for (int i = 0; i < 9; i++)
                    {
                        // obtiene los atributos y los guarda en datosConsultados
                        datosConsultados[i] = resultado.Rows[0][i].ToString();
                    }

                    //Se encapsulan los datos utilizando la clase entidadProveedor
                    cuentaConsultada = new EntidadCuenta(datosConsultados);
                }
            }
            catch (Exception e) { }

            return cuentaConsultada;
        }

        public DataTable obtenerMiembrosAsociados(int id)
        {
            return adapterCuenta.obtenerMiembrosAsociados(id);
        }

        public DataTable obtenerMiembrosDesasociados(int id)
        {
            return adapterCuenta.obtenerMiembrosDesasociados(id);
        }

        public DataTable obtenerCNAMiembrosAsociados(int id)
        {
            return adapterCuenta.obtenerCNAMiembrosAsociados(id);
        }
    }
}