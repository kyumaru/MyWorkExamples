using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Gyoza.DataSet1TableAdapters;

namespace Gyoza.Modulo_Proyecto
{
    public class ControladoraBDProyecto
    {
        ProyectoTableAdapter adaptadorProyecto;
        Trabaja_EnTableAdapter adaptadorTrabajaEn;
        ModuloTableAdapter modulo;
        public ControladoraBDProyecto()
        {
           adaptadorProyecto = new ProyectoTableAdapter();
           adaptadorTrabajaEn = new Trabaja_EnTableAdapter();
           modulo = new ModuloTableAdapter();
        }
        public String[] insertarProyecto(EntidadProyecto proyecto)
        {
            object id = adaptadorProyecto.generarID();
            if (id != null)
            {
                proyecto.Identificador = (int)adaptadorProyecto.generarID();
            }
            else
            {
                proyecto.Identificador = 1;
            }
            String[] res = new String[4];
            res[3] = proyecto.Identificador.ToString();
            try
            { 
                adaptadorProyecto.Insert(proyecto.Identificador, proyecto.Nombre, proyecto.Objetivo_General, proyecto.Fecha_Asignacion, proyecto.Fecha_Inicio, proyecto.Fecha_Finalizacion,
                    proyecto.Estado, proyecto.Oficina_Propietaria, proyecto.Telefono_Oficina, proyecto.Representante_Usuario, proyecto.Celular_Representante, proyecto.Correo_Representante);
                res[0] = "success";
                res[1] = "Exito";
                res[2] = "Proyecto Agregado";
            }
            catch (SqlException e)
            {
                // Como la llave es generada se puede volver a intentar
                res[0] = "danger";
                res[1] = "Fallo en la operacion";
                res[2] = "Intente nuevamente";
            }
            return res;
        }


         public String[] modificarProyecto(EntidadProyecto proyecto, EntidadProyecto nuevoProyecto)
         {
             String[] res = new String[3];
             try
             {
                adaptadorProyecto.Update(nuevoProyecto.Identificador,nuevoProyecto.Nombre,nuevoProyecto.Objetivo_General,nuevoProyecto.Fecha_Asignacion,nuevoProyecto.Fecha_Inicio,nuevoProyecto.Fecha_Finalizacion,nuevoProyecto.Estado,
                    nuevoProyecto.Oficina_Propietaria, nuevoProyecto.Telefono_Oficina, nuevoProyecto.Representante_Usuario, nuevoProyecto.Celular_Representante, nuevoProyecto.Correo_Representante, proyecto.Identificador, proyecto.Nombre, proyecto.Objetivo_General, proyecto.Fecha_Asignacion,
                    proyecto.Fecha_Inicio, proyecto.Fecha_Finalizacion, proyecto.Estado, proyecto.Oficina_Propietaria, proyecto.Telefono_Oficina, proyecto.Representante_Usuario, proyecto.Celular_Representante, proyecto.Correo_Representante);
                res[0] = "success";
                res[1] = "Exito";
                res[2] = "Proyecto modificado";
             }
             catch (SqlException e)
             {
                 if (e.Number == 2627)
                 {
                     res[0] = "danger";
                     res[1] = "Fallo";
                     res[2] = "Error al modificar";
                 }
             }
             return res;
         }
 
 
         public String[] eliminarProyecto(EntidadProyecto proyecto)
         {
             String[] res = new String[3];
             try
             {
                 
                 adaptadorProyecto.Delete(proyecto.Identificador,proyecto.Nombre, proyecto.Objetivo_General, proyecto.Fecha_Asignacion, proyecto.Fecha_Inicio, proyecto.Fecha_Finalizacion,
                     proyecto.Estado, proyecto.Oficina_Propietaria, proyecto.Telefono_Oficina, proyecto.Representante_Usuario, proyecto.Celular_Representante, proyecto.Correo_Representante);
                 
                 res[0] = "success";
                 res[1] = "Exito";
                 res[2] = "Proyecto eliminado";
             }
            catch (SqlException e)
            {
                res[1] = "danger";
                res[2] = "Fallo";
                res[3] = "Error al eliminar";
            }
                 return res;
          }

        public DataTable consultarProyectos()
        {
            DataTable resultado = new DataTable();

            try
            {
                resultado = adaptadorProyecto.GetData().CopyToDataTable();
            }
            catch (Exception e)
            {
                resultado = null;
            }
            return resultado;
        }

        /* Consulta un proveedor en específico.
         * Recibe la cédula del proveedor a consultar
         * Retorna un data table con los datos del proveedor
         * consultado.
         */
       
        public String[] asociarMiembroEquipo(int idProyecto, Object[][] miembrosDeEquipo)
        {
            string[] res = new String[3];
            bool error = false;
            res[0] = "success";
            res[1] = "Exito";
            res[2] = "Miembros asociados correctamente";
                for (int i = 0; i < miembrosDeEquipo.Length && !error; ++i)
                {
                    
                    try { 
                        adaptadorTrabajaEn.Insert(idProyecto, miembrosDeEquipo[i][0].ToString(),miembrosDeEquipo[i][1].ToString());
                    }
                    catch(SqlException e){
                        
                        if (e.Number != 2627)
                        {
                            res[1] = "danger";
                            res[2] = "Fallo";
                            res[3] = "Error al asociar miembros";
                            error = true;
                        }
                        else
                        {
                            adaptadorTrabajaEn.Update(miembrosDeEquipo[i][1].ToString(), idProyecto, miembrosDeEquipo[i][0].ToString());
                        }

                    }
                }
                
       
            return res;
        }
        public String[] asignar_lider(int idProyecto, Object[] miembroDeEquipo)
        {
             string[] res = new String[3];
             res[0] = "success";
             res[1] = "Exito";
             res[2] = "Miembros asociados correctamente";

             try
             {
                 adaptadorTrabajaEn.Update(idProyecto, miembroDeEquipo[0].ToString(), "Lider", idProyecto, miembroDeEquipo[0].ToString());
             }
             catch (SqlException e)
             {
                 res[1] = "danger";
                 res[2] = "Fallo";
                 res[3] = "Error al asociar miembros";
             }
             return res;
        }
        public String[] desasociarMiembrosEquipo(int idProyecto, Object[][] miembrosDeEquipo)
        {
            string[] res = new String[3];
            bool error = false;
            res[0] = "success";
            res[1] = "Exito";
            res[2] = "Miembros desasociados correctamente"; ;
            try
            {
                for (int i = 0; i < miembrosDeEquipo.Length && !error; ++i)
                {
                    adaptadorTrabajaEn.Delete(idProyecto, miembrosDeEquipo[i][0].ToString());
                }

            }
            catch (SqlException e)
            {
                res[1] = "danger";
                res[2] = "Fallo";
                res[3] = "Error al desasociar miembros";
            }
            return res;
        }

        public EntidadProyecto consultarProyecto(int id)
        {
            DataTable resultado = new DataTable();
            EntidadProyecto proyectoConsultado = null; //para encpasular los datos consultados.
            Object[] datosConsultados = new Object[12]; //para guardar los datos obtenidos de la consulta temporalmente

            try
            {
                resultado = adaptadorProyecto.consultarFilaProyecto(id);

                if (resultado.Rows.Count == 1)
                { // si hay un valor
                    datosConsultados[0] = id;
                    for (int i = 1; i < 12; i++)
                    {
                        // obtiene los atributos y los guarda en datosConsultados
                        datosConsultados[i] = resultado.Rows[0][i].ToString();
                    }

                    //Se encapsulan los datos utilizando la clase entidadProveedor
                    proyectoConsultado = new EntidadProyecto(datosConsultados);
                }
            }
            catch (Exception e) { }

            return proyectoConsultado;
        }

        public DataTable obtenerProyectosAsociados( String cedula )
        {
            DataTable resultado;

            try
            {
                resultado = adaptadorProyecto.obtenerProyectosAsociados(cedula);
            }
            catch (Exception e)
            {
                resultado = null;
            }
            return resultado;
        }

        public DataTable obtenerJerarquia(int idProyecto)
        {
            DataTable resultado;

            try
            {
               resultado= modulo.obtenerJerarquia(idProyecto);
            }
            catch (Exception e)
            {
                resultado= null;
            }
            return resultado;
        }
    }
}