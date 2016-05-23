using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Gyoza.Modulo_Cuenta
{
    public class ControladoraCuentas
    {
        ControladoraBDCuentas controladoraBDCuenta;

        public ControladoraCuentas()
        {
            controladoraBDCuenta = new ControladoraBDCuentas();
        }

        public String[] insertarCuenta(Object[] datos)
        {
            EntidadCuenta cuenta = new EntidadCuenta(datos);

            return controladoraBDCuenta.insertarCuenta(cuenta);
        }

        public String[] modificarCuenta(Object[] datosNuevos, EntidadCuenta viejaCuenta)
        {
            EntidadCuenta nuevaCuenta = new EntidadCuenta(datosNuevos);

            return controladoraBDCuenta.modificarCuenta(nuevaCuenta, viejaCuenta);
        }

        public String[] eliminarCuenta(EntidadCuenta cuentaEliminar)
        {

            return controladoraBDCuenta.eliminarCuenta(cuentaEliminar);
        }

        public DataTable consultarCuenta()
        {

            return controladoraBDCuenta.consultarCuentas();
        }

        public EntidadCuenta consultarCuenta(String cedula)
        {
            return controladoraBDCuenta.consultarCuenta(cedula);
        }

        public EntidadCuenta consultarCuentaUsuario(String usuario)
        {
            return controladoraBDCuenta.consultarCuentaUsuario(usuario);
        }

        public DataTable obtenerMiembrosAsociados(int id)
        {
            return controladoraBDCuenta.obtenerMiembrosAsociados(id);
        }

        public DataTable obtenerMiembrosDesasociados(int id)
        {
            return controladoraBDCuenta.obtenerMiembrosDesasociados(id);
        }

        public DataTable obtenerCNAMiembrosAsociados(int id)
        {
            return controladoraBDCuenta.obtenerCNAMiembrosAsociados(id);
        }
    }
}