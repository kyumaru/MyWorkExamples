using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Servicios_Reservados_2
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //roles y permisos
            Recepcionista.Visible=false;
            Cocina.Visible=false;
            Financiero.Visible = false;
            Usuario.Visible = false;

            string userid = (string)Session["username"];
            ArrayList listaRoles = (ArrayList)Session["Roles"];


            if (!IsPostBack)
            {
                if (userid == "" || userid == null)
                {
                    Response.Redirect("~/Ingresar.aspx");
                }
                if (listaRoles.Contains("administrador sistema") || listaRoles.Contains("recepcion"))
                {
                    Recepcionista.Visible = true;
                }
                if (listaRoles.Contains("administrador sistema") || listaRoles.Contains("encargado cocina"))
                {
                    Cocina.Visible = true;
                }
                if (listaRoles.Contains("administrador sistema") || listaRoles.Contains("administrador local") || listaRoles.Contains("administrador global"))
                {
                    Financiero.Visible = true;
                }
                if (listaRoles.Contains("administrador sistema") )
                {
                    Usuario.Visible = true;
                }
                
            }

        }
    }
}