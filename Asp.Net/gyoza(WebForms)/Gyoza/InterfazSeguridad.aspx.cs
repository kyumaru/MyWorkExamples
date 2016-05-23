using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Gyoza.Modulo_Rol;
using Gyoza.Modulo_Cuenta;

namespace Gyoza
{
    public partial class InterfazSeguridad : System.Web.UI.Page
    {
        private static bool seConsulto = false;
        private static String permisos = "1";
        private static Object[] idArray;
        private static ControladoraRoles controladoraRol;

        private static EntidadCuenta cuentaConsultada;
        private static ControladoraCuentas controladora;

        private static EntidadRol rolConsultado;


        private static int resultadosPorPagina;
        private static int modo = 0;


        protected void Page_Load(object sender, EventArgs e)
        {
            controladora = new ControladoraCuentas();
            controladoraRol = new ControladoraRoles();

            ((SiteMaster)Page.Master).markModule("seguridad");//con esto se setea el tab en el site master puede q haga falta cambiarlo de lugar


            if (((SiteMaster)Page.Master).getRol() != null)
            {
                this.botonAyuda.Disabled = false;
                permisos = ((SiteMaster)Page.Master).getRol().PermisosSeguridad;
            }
            else
            {
                this.botonAyuda.Disabled = true;
                permisos = "0";
            }

            if (!IsPostBack)
            {
                if (!seConsulto)
                    modo = 0;
                else
                {
                    
                }
            }

        }


        protected void cargarPermisos()
        {

            
            String permProyecto = rolConsultado.PermisosProyecto;
            String permCuenta = rolConsultado.PermisosCuenta;
            String permRequerimiento = rolConsultado.PermisosRequerimiento;
            String permSeguridad = rolConsultado.PermisosSeguridad;
            
            //permisosProyecto
            int i = 0;
            foreach (ListItem item in permisosProyecto.Items)
              {
                  if (permProyecto[i] == '0')
                {
                   item.Selected = false;
                }
                else
                {
                    item.Selected = true;
                }
                i++;
             }
             
            //permisosCuenta
             i = 0;
             foreach (ListItem item in permisosCuenta.Items)
             {
                 if (permCuenta[i] == '0')
                 {
                     item.Selected = false;
                 }
                 else
                 {
                     item.Selected = true;
                 }
                 i++;
             }

            //permisosRequerimiento
             i = 0;
             foreach (ListItem item in permisosRequerimiento.Items)
             {
                 if (permRequerimiento[i] == '0')
                 {
                     item.Selected = false;
                 }
                 else
                 {
                     item.Selected = true;
                 }
                 i++;
             }

             //permisosSeguridad
             i = 0;
             foreach (ListItem item in permisosSeguridad.Items)
             {
                 if (permSeguridad[i] == '0')
                 {
                     item.Selected = false;
                 }
                 else
                 {
                     item.Selected = true;
                 }
                 i++;
             }
        }


        protected Object[] obtenerPermisos()
        {
            Object[] datos = new Object[5];

            String nuevosPermisosProyecto = "";
            String nuevosPermisosCuenta = "";
            String nuevosPermisosRequerimiento = "";
            String nuevosPermisosSeguridad = "";
            
            foreach (ListItem item in permisosProyecto.Items)
            {
                if (item.Selected)
                {
                    nuevosPermisosProyecto += "1";
                }
                else
                {
                    nuevosPermisosProyecto += "0";
                }
            }

            foreach (ListItem item in permisosCuenta.Items)
            {
                if (item.Selected)
                {
                    nuevosPermisosCuenta += "1";
                }
                else
                {
                    nuevosPermisosCuenta += "0";
                }
            }

            foreach (ListItem item in permisosRequerimiento.Items)
            {
                if (item.Selected)
                {
                    nuevosPermisosRequerimiento += "1";
                }
                else
                {
                    nuevosPermisosRequerimiento += "0";
                }
            }

            foreach (ListItem item in permisosSeguridad.Items)
            {
                if (item.Selected)
                {
                    nuevosPermisosSeguridad += "1";
                }
                else
                {
                    nuevosPermisosSeguridad += "0";
                }
            }
            

            datos[0] = rolConsultado.IdRol;
            datos[1]=nuevosPermisosProyecto;
            datos[2]=nuevosPermisosCuenta;
            datos[3]=nuevosPermisosRequerimiento;
            datos[4]=nuevosPermisosSeguridad;
            return datos;
        }



        protected void clickAceptar(object sender, EventArgs e)
        {

          controladoraRol.modificarRol(obtenerPermisos(),rolConsultado);
          rolConsultado = controladoraRol.consultarRol(textEstadoRequerimiento.SelectedValue);
            
        }

      
        protected void mostrarMensaje(String tipoAlerta, String alerta, String mensaje)
        {
            Alerta.Attributes["class"] = "alert alert-" + tipoAlerta + " alert-dismissable fade in";
            labelTipoAlerta.Text = alerta + " ";
            labelAlerta.Text = mensaje;
            Alerta.Attributes.Remove("hidden");
        }
    

        protected void aplicarPermisos()
        {
            char[] estados = permisos.ToCharArray();
            if (estados[0] == '0')// la posicion 1 representa modificar
            {
               
            }
        }

        protected void textEstadoRequerimiento_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (textEstadoRequerimiento.SelectedValue != "")
            {
                rolConsultado = controladoraRol.consultarRol(textEstadoRequerimiento.SelectedValue);
                cargarPermisos();
            }
            else 
            {
                limpiarCampos();
            }
        }

        protected void limpiarCampos()
        {

            foreach (ListItem item in permisosProyecto.Items)
            {
                item.Selected = false;
            }

            foreach (ListItem item in permisosCuenta.Items)
            {
                item.Selected = false;
            }

            foreach (ListItem item in permisosRequerimiento.Items)
            {
                item.Selected = false;
            }

            foreach (ListItem item in permisosSeguridad.Items)
            {
                item.Selected = false;
            }

        }
    }
    
}