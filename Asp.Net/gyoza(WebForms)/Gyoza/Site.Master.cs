using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Gyoza.Modulo_Proyecto;
using Gyoza.Modulo_Rol;
using Gyoza.Modulo_Cuenta;

/*NOTAS JCE
_activar todas las tabs de cuentas->proyectos si la cuenta consultada no es null->cuenta--permisos--entidad rol
_ activar seguridad tab solo si rol.idrol es 11111--admin
*/


namespace Gyoza
{
    public partial class SiteMaster : MasterPage
    {
        private const string AntiXsrfTokenKey = "__AntiXsrfToken";
        private const string AntiXsrfUserNameKey = "__AntiXsrfUserName";
        private string _antiXsrfTokenValue;
        static EntidadProyecto proyectoConsultado;
        public static bool banderaProyectoConsultado;
        static EntidadRol rolConsultado;
        public static bool banderaRolConsultado;
        static EntidadCuenta cuentaConsultada;
        public static bool banderaCuentaConsultada;
        public static bool banderaLogin;
        public static bool banderaCerrarSesion;

        //sets background of actual tab
        public void markModule(String name) {

            switch (name)
            {
                case "cuentas":

                    this.bCuentas.Style.Add("background-color", "LightSkyBlue ");
                    this.bCuentas.Style.Add("color", "Black");
                   this.bCuentas.Style.Add("text-transform", "uppercase");

                    break;

                case "proyectos":
                    this.bProyectos.Style.Add("background-color", "LightSkyBlue");
                    this.bProyectos.Style.Add("color", "Black");
                    this.bProyectos.Style.Add("text-transform", "uppercase");
                    break;

                case "reqs":
                    this.bReqs.Style.Add("background-color", "LightSkyBlue");
                    this.bReqs.Style.Add("color", "Black");
                    this.bReqs.Style.Add("text-transform", "uppercase");
                    break;

                case "seguridad":
                    this.bSeguridad.Style.Add("background-color", "LightSkyBlue");
                    this.bSeguridad.Style.Add("color", "Black");
                    this.bSeguridad.Style.Add("text-transform", "uppercase");
                    break;

                case "consultar":
                    this.bConsultar.Style.Add("background-color", "LightSkyBlue");
                    this.bConsultar.Style.Add("color", "Black");
                    this.bConsultar.Style.Add("text-transform", "uppercase");
                    break;

                default:
                    // Algo salio mal
                    break;
            }
            
        
        }





        //OJO borrar showTabs de Page_Init() cuando todo este listo, sino siempre se ven los tabs
        //muestra los tabs en el site master selectivamente
        public void showTabs()
        {
            this.bCuentas.Visible = this.bProyectos.Visible = this.bReqs.Visible = this.bConsultar.Visible = this.bSeguridad.Visible = (rolConsultado != null);
            if (rolConsultado != null)
            {
                this.bSeguridad.Visible = (rolConsultado.PermisosSeguridad == "1");
            }
        }


        protected void Page_Init(object sender, EventArgs e)
        {
            
            // El código siguiente ayuda a proteger frente a ataques XSRF
            var requestCookie = Request.Cookies[AntiXsrfTokenKey];
            Guid requestCookieGuidValue;
            if (requestCookie != null && Guid.TryParse(requestCookie.Value, out requestCookieGuidValue))
            {
                // Utilizar el token Anti-XSRF de la cookie
                _antiXsrfTokenValue = requestCookie.Value;
                Page.ViewStateUserKey = _antiXsrfTokenValue;
            }
            else
            {
                // Generar un nuevo token Anti-XSRF y guardarlo en la cookie
                _antiXsrfTokenValue = Guid.NewGuid().ToString("N");
                Page.ViewStateUserKey = _antiXsrfTokenValue;

                var responseCookie = new HttpCookie(AntiXsrfTokenKey)
                {
                    HttpOnly = true,
                    Value = _antiXsrfTokenValue
                };
                if (FormsAuthentication.RequireSSL && Request.IsSecureConnection)
                {
                    responseCookie.Secure = true;
                }
                Response.Cookies.Set(responseCookie);
            }

            Page.PreLoad += master_Page_PreLoad;

          //
        }

        protected void master_Page_PreLoad(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Establecer token Anti-XSRF
                ViewState[AntiXsrfTokenKey] = Page.ViewStateUserKey;
                ViewState[AntiXsrfUserNameKey] = Context.User.Identity.Name ?? String.Empty;
            }
            else
            {
                // Validar el token Anti-XSRF
                if ((string)ViewState[AntiXsrfTokenKey] != _antiXsrfTokenValue
                    || (string)ViewState[AntiXsrfUserNameKey] != (Context.User.Identity.Name ?? String.Empty))
                {
                    throw new InvalidOperationException("Error de validación del token Anti-XSRF.");
                }
            }

        
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            showTabs();
        }

        protected void Unnamed_LoggingOut(object sender, LoginCancelEventArgs e)
        {
            cerrarSesion();
            Context.GetOwinContext().Authentication.SignOut();
        }

        public void setDatosProyecto(EntidadProyecto e)
        {
            proyectoConsultado = e;
            banderaProyectoConsultado = true;
        }

        public bool getBandera()
        {
            return banderaProyectoConsultado;
        }

        public EntidadProyecto getProyecto()
        {
            banderaProyectoConsultado = false;
            return proyectoConsultado;
        }

        public void setDatosRol(EntidadRol r)
        {
            rolConsultado = r;
            banderaRolConsultado = true;
        }

        public bool getBanderaRol()
        {
            return banderaRolConsultado;
        }

        public EntidadRol getRol()
        {
            banderaRolConsultado = false;
            return rolConsultado;
        }

        public EntidadCuenta getCuenta()
        {
            banderaCuentaConsultada = false;
            return cuentaConsultada;
        }

        public bool getBanderaCuenta()
        {
            return banderaCuentaConsultada;
        }

        public void setDatosCuenta(EntidadCuenta c)
        {
            cuentaConsultada = c;
            banderaCuentaConsultada = true;
        }

        public void setBanderaLogin(bool blg) {
            banderaLogin = blg;
        }

        public bool getBanderaLogin() {
            return banderaLogin;
        }

        public void cerrarSesion()
        {
            proyectoConsultado = null;
            banderaProyectoConsultado = false;
            rolConsultado = null;
            banderaRolConsultado = false;
            cuentaConsultada = null;
            banderaCuentaConsultada = false;
        }

        public bool getBanderaCerrarSesion()
        {
            return banderaCerrarSesion;
        }

        public void setBanderaCerrarSesion(bool blg)
        {
            banderaCerrarSesion = blg;
        }

        
    }

}