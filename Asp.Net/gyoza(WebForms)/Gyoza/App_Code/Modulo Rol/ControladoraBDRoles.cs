using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Gyoza.DataSet1TableAdapters;
using System.IO;
using System.Configuration;

namespace Gyoza.Modulo_Rol
{
    public class ControladoraBDRoles
    {
        RolTableAdapter adaptadorRol;

        public ControladoraBDRoles()
        {
            adaptadorRol = new RolTableAdapter();
        }

        public DataTable consultarRoles()
        {
            DataTable resultado = new DataTable();

            try
            {
                resultado = this.adaptadorRol.GetData();
            }
            catch (Exception e)
            {
                resultado = null;
            }
            //finalmente retorna los datos obtenidos.
            return resultado;
        }

        public EntidadRol consultarRol(String idRol)
        {
            EntidadRol rolConsultado = null;
            DataTable resultado = new DataTable();
            Object[] datosConsultados = new Object[5];

            try
            {
                resultado = adaptadorRol.consultarRol(idRol);

                if (resultado.Rows.Count == 1)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        // obtiene los atributos y los guarda en datosConsultados
                        datosConsultados[i] = resultado.Rows[0][i].ToString();
                    }

                    //Se encapsulan los datos utilizando la clase entidadProveedor
                    rolConsultado = new EntidadRol(datosConsultados);
                }
            }
            catch (Exception e) { }

            return rolConsultado;
        }

        public String[] modificarRol(EntidadRol nuevoRol, EntidadRol viejoRol)
        {
            String[] resultado = new String[3];
            try
            {
                this.adaptadorRol.Update(nuevoRol.IdRol, nuevoRol.PermisosProyecto, nuevoRol.PermisosCuenta, nuevoRol.PermisosRequerimiento, nuevoRol.PermisosSeguridad,
                                         viejoRol.IdRol, viejoRol.PermisosProyecto, viejoRol.PermisosCuenta, viejoRol.PermisosRequerimiento, viejoRol.PermisosSeguridad);

                resultado[0] = "success";
                resultado[1] = "Éxito. ";
                resultado[2] = "El requerimiento se ha modificado correctamente";

            }
            catch
            {
                resultado[0] = "danger";
                resultado[1] = "Error. ";
                resultado[2] = "No se pudo modificar el requerimiento";
            }
            return resultado;
        }
    }
}