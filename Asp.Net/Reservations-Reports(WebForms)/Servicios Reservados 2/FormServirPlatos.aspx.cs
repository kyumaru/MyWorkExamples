using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Servicios_Reservados_2
{
    public partial class FormServirPlatos : System.Web.UI.Page
    {

        ControladoraServirPlatos controladora = new ControladoraServirPlatos();
        private static int modo=0;

        protected void Page_Load(object sender, EventArgs e)
        {
            ArrayList listaRoles = (ArrayList)Session["Roles"];
            string userid = (string)Session["username"];
            if (!IsPostBack)
            {
                if (userid == "" || userid == null)
                {
                    Response.Redirect("~/Ingresar.aspx");
                } if (!listaRoles.Contains("administrador sistema") && !listaRoles.Contains("encargado cocina"))
                {
                    Response.Redirect("ErrorPermiso.aspx");
                }
                cambiarModo();
            }

        }
        protected void cambiarModo()
        {
            if (modo == 0)
            {//esperando tiquete
                infoTiquete.Visible = false;
                btnServir.Disabled = true;
                tiquete.Value = "";

            }
            else if (modo == 1)
            {//tiquete verificado
                infoTiquete.Visible = true;
                btnServir.Disabled = false;
            }
        }

        /* Requiere: La introduccion de un numero de tiquete por parte del usuario
         * Efecto: Verifica el numero de tiquete y muestra la información de la reservacion asociada
         * Modifica: Nada
         */
        protected void clickVerificar(object sender, EventArgs e)
        {
            verificar();
        }

        protected void verificar() {
            if (tiquete.Value!=null)
            {
                DataTable tabla = crearTablaTiquete();
                int numTiquete = int.Parse(tiquete.Value);
                Object[] datos = new Object[6];

                EntidadTiquete datosTiquete = controladora.solicitarTiquete(numTiquete);// se consulta
                if (datosTiquete != null)
                {
                    datos[0] = datosTiquete.Anfitriona;
                    datos[1] = datosTiquete.Estacion;
                    datos[2] = datosTiquete.NombreSolicitante;
                    datos[3] = datosTiquete.Categoria;
                    datos[4] = datosTiquete.Consumido;
                    datos[5] = datosTiquete.Notas;
                    

                    tabla.Rows.Add(datos);// cargar en la tabla los datos 

                    GridViewTiquete.DataBind();
                    modo = 1;
                    cambiarModo();
                }
                else
                {
                   mostrarMensaje("danger", "Error:", "Este tiquete no se encuentra asociado a un servicio"); // se muestra el resultado           
                    modo = 0;
                    cambiarModo();
                }  
            }            
                      
        }
        protected DataTable crearTablaTiquete()
        {
            DataTable tabla = new DataTable();
            DataColumn columna;

            columna = new DataColumn();
            columna.DataType = System.Type.GetType("System.String");
            columna.ColumnName = "Anfitriona";
            tabla.Columns.Add(columna); 

            columna = new DataColumn();
            columna.DataType = System.Type.GetType("System.String");
            columna.ColumnName = "Estación";
            tabla.Columns.Add(columna);

            columna = new DataColumn();
            columna.DataType = System.Type.GetType("System.String");
            columna.ColumnName = "Solicitante";
            tabla.Columns.Add(columna);

            columna = new DataColumn();
            columna.DataType = System.Type.GetType("System.String");
            columna.ColumnName = "Categoria";
            tabla.Columns.Add(columna);

            columna = new DataColumn();
            columna.DataType = System.Type.GetType("System.String");
            columna.ColumnName = "Consumido";
            tabla.Columns.Add(columna);

            columna = new DataColumn();
            columna.DataType = System.Type.GetType("System.String");
            columna.ColumnName = "Notas";
            tabla.Columns.Add(columna);

            GridViewTiquete.DataSource = tabla;
            GridViewTiquete.DataBind();

            return tabla;
        }
        protected void clickServir(object sender, EventArgs e)
        {
            controladora.servirTiquete();
            verificar();
            modo = 0;
            cambiarModo();
        }
        protected void clickServirDesactivar(object sender, EventArgs e)
        {
            controladora.servirTiquete();
            verificar();
            controladora.desactivarTiquete();
            modo = 0;
            cambiarModo();
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