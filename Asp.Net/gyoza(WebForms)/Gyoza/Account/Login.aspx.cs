using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.ApplicationServices;
using Gyoza.Modulo_Proyecto;
using Gyoza.Modulo_Cuenta;
using Gyoza.Modulo_Rol;

namespace Gyoza.Account
{
    public partial class Login : Page
    {
        private static EntidadCuenta cuentaConsultada;
        private static EntidadRol rolConsultado;
        private static ControladoraCuentas controladoraC;
        private static ControladoraRoles controladoraR;

        protected void Page_Load(object sender, EventArgs e)
        {
            controladoraC = new ControladoraCuentas();
            controladoraR = new ControladoraRoles();
        }

        protected void enviarCuentaRol()
        {
            ((SiteMaster)Page.Master).setDatosCuenta(cuentaConsultada);
            ((SiteMaster)Page.Master).setDatosRol(rolConsultado);
            ((SiteMaster)Page.Master).setBanderaLogin(true);
            ((SiteMaster)Page.Master).showTabs();
         
            Response.Redirect("../Default.aspx");
           
        }

        protected Object[] obtenerDatos(){
            Object[] datos = new Object[2];
            datos[0] = this.Email.Text.Trim();
            datos[1] = this.Password.Text.Trim();

            return datos;
        }

        protected void controlDeCambio(object sender, EventArgs e)
        {
            Object[] datos = obtenerDatos();

            //consulta cuenta y rol
            cuentaConsultada = controladoraC.consultarCuentaUsuario(datos[0].ToString());
            if (cuentaConsultada != null)
            {
                if (cuentaConsultada.Password.Equals(datos[1].ToString()))//si la contraseña es correcta
                {
                    rolConsultado = controladoraR.consultarRol(cuentaConsultada.Rol);
                    enviarCuentaRol();
                   
                }
                else//contraseña incorrecta
                {
                    mostrarMensaje("warning", "Alerta", "Nombre de Usuario o Contraseña no válidos");
                }
            }
            else//no existe la cuenta
            {
                mostrarMensaje("warning", "Alerta", "Nombre de Usuario o Contraseña no válidos");
            }
        }

        protected void mostrarMensaje(String tipoAlerta, String alerta, String mensaje)
        {
            Alerta.Attributes["class"] = "alert alert-" + tipoAlerta + " alert-dismissable fade in";
            labelTipoAlerta.Text = alerta + " ";
            labelAlerta.Text = mensaje;
            Alerta.Attributes.Remove("hidden");
        }
    }
}