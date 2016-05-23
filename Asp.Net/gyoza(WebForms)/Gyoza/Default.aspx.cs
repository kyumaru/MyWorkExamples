using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Gyoza
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (((SiteMaster)Page.Master).getBanderaLogin())
            {
                ((SiteMaster)Page.Master).setBanderaLogin(false);

                mostrarMensaje("", "", "Sesión Iniciada");
            }

            if (((SiteMaster)Page.Master).getBanderaCerrarSesion())
            {
                ((SiteMaster)Page.Master).setBanderaCerrarSesion(false);
                mostrarMensaje("", "", "Sesión Cerrada");
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