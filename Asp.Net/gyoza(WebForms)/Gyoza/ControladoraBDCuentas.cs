using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Gyoza.DataSet1TableAdapters;

namespace Gyoza.App_Code.Modulo_Cuenta{

    public class ControladoraBDCuentas{

        ProveedoresTableAdapter adapterProveedor;

        public ControladoraBDCuentas(){
            adapterProveedor = new ProveedoresTableAdapter();
        }

        public String[] insertarCuenta(EntidadCuenta nuevaCuenta){

        }

        public String[] modificarCuenta(EntidadCuenta nuevaCuenta, EntidadCuenta viejaCuenta){

        }

        public String[] eliminarProveedor(EntidadCuenta cuentaEliminar){

        }

        public DataTable consultarCuentas(){

        }
    }
}