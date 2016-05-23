using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Gyoza.Account
{
    public partial class Logout : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            cerrarSesion();
        }

        private void cerrarSesion()
        {
            ((SiteMaster)Page.Master).cerrarSesion();
            ((SiteMaster)Page.Master).setBanderaCerrarSesion(true);

            Response.Redirect("../Default.aspx");
        }
    }
}