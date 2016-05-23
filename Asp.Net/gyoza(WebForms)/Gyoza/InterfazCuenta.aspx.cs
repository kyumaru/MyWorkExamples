using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.ApplicationServices;


using Gyoza.Modulo_Cuenta;
using System.Net.Mail;

namespace Gyoza
{

    public partial class InterfazCuenta : System.Web.UI.Page
    {
        private static string cuentaConsultar = "";
        private static int confirmacion = 0;
        private static Boolean aceptado = false;
        private static bool seConsulto = false;
        private static Object[] idsGrid;
        private static ControladoraCuentas controladora;
        private static int modo = 0;
        private static EntidadCuenta cuentaConsultada;
        private static String permisos = "1111";
        private static int resultadosPorPagina;
        


        protected void Page_Load(object sender, EventArgs e)
        {
            controladora = new ControladoraCuentas();

            ((SiteMaster)Page.Master).markModule("cuentas");//con esto se setea el tab para cuentas en el site master puede q haga falta cambiarlo de lugar


            if (((SiteMaster)Page.Master).getRol() != null)
            {
                permisos = ((SiteMaster)Page.Master).getRol().PermisosCuenta;
                if (modo == 0 && ((SiteMaster)Page.Master).getRol().IdRol != "Administrador")
                {
                    consultarCuenta(((SiteMaster)Page.Master).getCuenta().Cedula);
                }
                this.botonAyuda.Disabled = true;
            }
            else
            {
                this.botonAyuda.Disabled = true;
                permisos = "0000";
            }


            resultadosPorPagina = gridViewCuentas.PageSize;
            llenarGrid();
            ocultarMensaje();


            if (!IsPostBack)
            {
                if (!seConsulto)
                    modo = 0;
                else
                {
                    if (cuentaConsultada == null)
                        mostrarMensaje("warning", "Alerta: ", "No se pudo consultar la cuenta.");
                    else
                        setDatosConsultados();
                        seConsulto = false;
                }
            }

            cambiarModo(); 
        }


        protected void cambiarModo()
        {
            switch (modo)
            {
                case 0:
                    botonAceptar.Disabled = true;
                    botonCancelar.Disabled = true;
                    botonInsertar.Disabled = false;
                    botonModificar.Disabled = true;
                    botonEliminar.Disabled = true;
                    limpiarCampos();
                    habilitarCampos(false);
                    break;
                case 1:
                case 2:
                    botonAceptar.Disabled = false;
                    botonCancelar.Disabled = false;
                    botonInsertar.Disabled = true;
                    botonModificar.Disabled = true;
                    botonEliminar.Disabled = true;
                    habilitarCampos(true);
                    break;
                case 3:
                case 4:
                    botonAceptar.Disabled = true;
                    botonCancelar.Disabled = false;
                    botonInsertar.Disabled = true;
                    botonModificar.Disabled = false;
                    botonEliminar.Disabled = false;
                    habilitarCampos(false);
                    break;

                default:
                    // Algo salio mal
                    break;
            }

            //se implementara despues
            aplicarPermisos();// se aplican los permisos del usuario para el acceso a funcionalidades

        }

        
        /*
        * Este método se encarga de aplicar los permisos del usuario(guardados en el atributo permisos)
        * Solo habilita/deshabilita los botones de insertar, modificar y eliminar.
        */
        protected void aplicarPermisos()
        {
            char[] estados = permisos.ToCharArray();
            if (estados[0] == '0')// la posicion 0 representa agregar
            {
                this.botonInsertar.Disabled = true;
            }
            if (estados[1] == '0')// la posicion 1 representa modificar
            {
                this.botonModificar.Disabled = true;
            }
            if (estados[2] == '0')// la posicion 2 representa eliminar
            {
                this.botonEliminar.Disabled = true;
            }
            if (estados[3] == '0')
            {
                this.botonConsultar.Disabled = true;
            }
        }

        protected void habilitarCampos(bool habilitar)
        {
            this.textNombre.Enabled = habilitar;
            this.textApellidos.Enabled = habilitar;
            this.textOficina.Enabled = habilitar;
            this.textCorreo.Enabled = habilitar;
            this.textUsername.Enabled = habilitar;
            this.textPassword.Enabled = habilitar;
            this.textCelular.Enabled = habilitar;
            this.textCedula.Enabled = habilitar;
            this.textRol.Enabled = habilitar;
        }

        protected void gridViewCuentas_Seleccion(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Select":
                    GridViewRow filaSeleccionada = this.gridViewCuentas.Rows[Convert.ToInt32(e.CommandArgument)];
                    String id = idsGrid[Convert.ToInt32(e.CommandArgument) + (this.gridViewCuentas.PageIndex * resultadosPorPagina)].ToString();
                    consultarCuenta(id);
                    Response.Redirect("InterfazCuenta.aspx");
                    break;
            }
        }

        protected void gridViewCuentas_CambioPagina(Object sender, GridViewPageEventArgs e)
        {
            this.gridViewCuentas.PageIndex = e.NewPageIndex;
            this.gridViewCuentas.DataBind();
        }

        protected void setDatosConsultados()
        {
            this.textNombre.Text = cuentaConsultada.Nombre;
            this.textApellidos.Text = cuentaConsultada.Apellidos;
            this.textCedula.Text = cuentaConsultada.Cedula;
            this.textOficina.Text = cuentaConsultada.TelefonoOficina;
            this.textCelular.Text = cuentaConsultada.TelefonoCelular;
            this.textCorreo.Text = cuentaConsultada.Correo;
            this.textUsername.Text = cuentaConsultada.Usuario;
            this.textPassword.Text = cuentaConsultada.Password;
        }


        protected void consultarCuenta(String id)
        {
            seConsulto = true;
            try
            {
               
                cuentaConsultada = controladora.consultarCuenta(id);
                modo = 4;
            }
            catch
            {
                cuentaConsultada = null;
                modo = 0;
            }
            cambiarModo();
        }
        protected void ocultarMensaje()
        {
            alertAlerta.Attributes.Add("hidden", "hidden");
        }


       

        protected void clickAceptar(object sender, EventArgs e)
        {

            Boolean operacionCorrecta = true;
            int idInsertado = 0;

            if (modo == 1)
            {
                idInsertado = insertar();

                if (idInsertado != 0)
                {
                    operacionCorrecta = true;
                    cuentaConsultada = controladora.consultarCuenta(this.textCedula.Text);
                    modo = 4;
                    habilitarCampos(false);
                }
            }
            else if (modo == 2)
            {
                operacionCorrecta = modificar();
            }
            if (operacionCorrecta)
            {
                cambiarModo();
            }
        }

        protected void clickCancelar(object sender, EventArgs e)
        {
            modo = 0;
            cambiarModo();
            limpiarCampos();
            cuentaConsultada = null;
        }


        protected int insertar()
        {
            int id = 0;
            Object[] usuario = obtenerDatosCuenta();

            String[] error = controladora.insertarCuenta(usuario);

            id = Convert.ToInt32(error[3]);
            mostrarMensaje(error[0], error[1], error[2]);
            if (error[0].Contains("success"))
            {
                llenarGrid();
            }
            else
            {
                id = 0;
                modo = 1;
            }

            return id;
        }

        protected Boolean modificar()
        {
            Boolean res = true;

            Object[] usuario = obtenerDatosCuenta();
            String[] error = controladora.modificarCuenta(usuario, cuentaConsultada);
            mostrarMensaje(error[0], error[1], error[2]);

            if (error[0].Contains("success"))
            {
                llenarGrid();
                cuentaConsultada= controladora.consultarCuenta(cuentaConsultada.Cedula);
                modo = 4;
            }
            else
            {
                res = false;
                modo = 2;
            }


            return res;
        }

        protected Object[] obtenerDatosCuenta()
        {
            Object[] datos = new Object[9];
            datos[0] = this.textCedula.Text;
            datos[1] = this.textNombre.Text;
            datos[2] = this.textApellidos.Text;
            datos[3] = this.textCorreo.Text;
            datos[4] = this.textRol.SelectedValue;
            datos[5] = this.textUsername.Text;
            datos[6] = this.textPassword.Text;
            datos[7] = this.textOficina.Text;
            datos[8] = this.textCelular.Text;
            return datos;
        }

        


        protected void clickAceptarEliminar(object sender, EventArgs e)
        {
            String[] error = controladora.eliminarCuenta(cuentaConsultada);
            mostrarMensaje(error[0], error[1], error[2]);

            if (error[0].Contains("success"))
            {
                modo = 0;
                cuentaConsultada = null;
                llenarGrid();
                limpiarCampos();
                cambiarModo();
            }
        }

        protected void limpiarCampos()
        {
            this.textNombre.Text = "";
            this.textApellidos.Text = "";
            this.textCedula.Text = "";
            this.textOficina.Text = "";
            this.textCorreo.Text = "";
            this.textCelular.Text = "";
            this.textUsername.Text = "";
            this.textPassword.Text = "";
            this.textCelular.Text = "";
        }
        //////////////////////////////////////////////////////////////////////////////////////////////


           protected void SendMail()

        {
            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
            mail.To.Add("juan-ca-pq@hotmail.com");
            mail.From = new MailAddress("jkdarklord@gmail.com", "Hola", System.Text.Encoding.UTF8);
            mail.Subject = "This mail is send from asp.net application";
            mail.SubjectEncoding = System.Text.Encoding.UTF8;
            mail.Body = "This is Email Body Text";
            mail.BodyEncoding = System.Text.Encoding.UTF8;
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.High;
            SmtpClient client = new SmtpClient();
            client.Credentials = new System.Net.NetworkCredential("jkdarklord@gmail.com", "KAPPAMIKEY");
            client.Port = 587;
            client.Host = "smtp.gmail.com";
            client.EnableSsl = true;
            try
            {
                client.Send(mail);
                this.textNombre.Text = "YAY :D";
            }
            catch (Exception ex)
            {
                this.textNombre.Text = "" +  ex;
            }
        }




           //////////////////////////////////////////////////////////////////////////////////////////////




        protected void clickInsertar(object sender, EventArgs e)
        {
            modo = 1;
            cambiarModo();

           // SendMail();
        }

        protected void clickModificar(object sender, EventArgs e)
        {
            modo = 2;
            cambiarModo();
        }


        protected void llenarGrid()
        {
            DataTable tabla = crearTablaCuentas(); 
            int indiceNuevoCuenta = -1;
            int i = 0;
            try
            {
                Object[] datos = new Object[4];
                DataTable cuentas = controladora.consultarCuenta();
                idsGrid = new Object[cuentas.Rows.Count];
                if (cuentas.Rows.Count > 0)
                {
                    foreach (DataRow fila in cuentas.Rows)
                    {
                        idsGrid[i] = fila[0].ToString();
                        datos[0] = fila[0].ToString(); 
                        datos[1] = fila[1].ToString();
                        datos[2] = fila[2].ToString();
                        datos[3] = fila[3].ToString();


                        tabla.Rows.Add(datos);
                        if (cuentaConsultada != null && (fila[0].Equals(cuentaConsultada.Cedula)))
                        {
                            indiceNuevoCuenta = i;
                        }
                        i++;
                    }
                }
                else 
                {
                    datos[0] = "-";
                    datos[1] = "-";
                    datos[2] = "-";
                    datos[3] = "-";
                    tabla.Rows.Add(datos);
                }

                this.gridViewCuentas.DataSource = tabla; 
                this.gridViewCuentas.DataBind();
                if (cuentaConsultada != null)
                {
                    GridViewRow filaSeleccionada = this.gridViewCuentas.Rows[indiceNuevoCuenta];
                }
                
                 
            }
            catch (Exception e)
            {
                mostrarMensaje("warning", "Alerta", "No hay conexión a la base de datos.");
            }
            
        }


        protected DataTable crearTablaCuentas()
        {
            DataTable tabla = new DataTable();
            DataColumn columna;

            columna = new DataColumn();
            columna.DataType = System.Type.GetType("System.String");
            columna.ColumnName = "Cedula";
            tabla.Columns.Add(columna);

            columna = new DataColumn();
            columna.DataType = System.Type.GetType("System.String");
            columna.ColumnName = "Nombre";
            tabla.Columns.Add(columna);

            columna = new DataColumn();
            columna.DataType = System.Type.GetType("System.String");
            columna.ColumnName = "Apellidos";
            tabla.Columns.Add(columna);

            columna = new DataColumn();
            columna.DataType = System.Type.GetType("System.String");
            columna.ColumnName = "Correo";
            tabla.Columns.Add(columna);

            return tabla;
        }


        protected void mostrarMensaje(String tipoAlerta, String alerta, String mensaje)
        { 
            alertAlerta.Attributes["class"] = "alert alert-" + tipoAlerta + " alert-dismissable fade in";
            labelTipoAlerta.Text = alerta + " ";
            labelAlerta.Text = mensaje;
            alertAlerta.Attributes.Remove("hidden");
        }



    }
}