using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Servicios_Reservados_2
{
    public partial class Ingresar : System.Web.UI.Page
    {
        LoginService login = new LoginService();
        protected void Page_Load(object sender, EventArgs e)
        {


        }
        protected void btnIniciar_Click(object sender, EventArgs e)
        {

            //se declara la variable usuario de tipo string y se le indica que reemplaze los carácteres que sean:
            // ; y -- para evitar sql inyection lo mismo para contraseña.
            string usuario = this.txtUsuario.Text.Replace(";", "").Replace("--", "");
            string contraseña = this.txtContraseña.Text.Replace(";", "").Replace("--", "");
            //Se manda llamar al método Autenticar que está parametrizado y se le pasan los valores correspondientes.
            if (login.Autenticar(usuario, contraseña) == true)
            {
                //Se verifica en la base de datos el UsuarioID y se almacena en la variable tblUsuario.
                DataTable tblUsuario = login.prConsultaUsuario(usuario, contraseña);
                //se declara y se le da el valor a la variable de sesión
                Session["username"] = tblUsuario.Rows[0]["username"].ToString();

                DataTable roles = login.rolesUsuario(usuario);
                ArrayList listaRoles = new ArrayList();
                foreach (DataRow rol in roles.Rows)
                {
                    listaRoles.Add(rol[0].ToString());
                }
                Session["Roles"] = listaRoles;
                DataTable info = login.infoUsuario(usuario);
                Session["Estacion"] = info.Rows[0]["estacion"].ToString();
                if ("1".Equals(info.Rows[0]["activo"].ToString()))
                {
                    if ("1".Equals(info.Rows[0]["reestablecer"].ToString()))
                    {
                        //Manda a reestablecer contraseña
                        Response.Redirect("FormReestablecer.aspx");
                    }
                    else
                    {
                        //Manda a la principal en caso de ser correcto el login
                        Response.Redirect("Default.aspx");
                    }
                }
                else
                {
                    //Mensaje de error en caso de no ser usuario registrado
                    lblMensaje.Text = "Usuario inactivo, contacte al administrador del sistema.";
                }
               

            }
            else
            {
                //Mensaje de error en caso de no ser usuario registrado
                lblMensaje.Text = "Usuario/Contraseña incorrecta verifique por favor.";
            }
        }
    }
}