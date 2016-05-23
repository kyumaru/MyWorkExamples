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
    public partial class FormUsuario : System.Web.UI.Page
    {
        private ControladoraUsuario controladora = new ControladoraUsuario();
        public static String[] ids;
        private static int indice = -1;

        protected void Page_Load(object sender, EventArgs e)
        {
            ArrayList listaRoles = (ArrayList)Session["Roles"];
            string userid = (string)Session["username"];

            if (!IsPostBack)
            {
                if (userid == "" || userid == null)
                {
                    Response.Redirect("~/Ingresar.aspx");
                }
                if (!listaRoles.Contains("administrador sistema"))
                {
                    Response.Redirect("ErrorPermiso.aspx");
                }
                llenarGridUsuarios();
                llenarEstaciones();
            }
        }

        private void llenarGridUsuarios()
        {
            DataTable tabla = crearTablaUsuarios();
            Object[] datos = new Object[5];
            DataTable usuarios = controladora.solicitarUsuarios();// se consultan todos
            ids = new String[usuarios.Rows.Count];
            //agrega los servicios incluidos en el paquete
            if (usuarios.Rows.Count > 0)
            {
                int i = 0;
                foreach (DataRow fila in usuarios.Rows)
                {
                    ids[i++] = fila[0].ToString();//username
                    datos[0] = fila[0].ToString();//username
                    datos[1] = fila[1].ToString();//nombre
                    EntidadUsuario usuarioSeleccionado = controladora.solicitarUsuario(fila[0].ToString());
                    string roles = "";
                    foreach (string rol in usuarioSeleccionado.Rol)
                    {
                        roles += rol + ", ";
                    }
                    datos[2] = roles;//rol
                    datos[3] = fila[2].ToString();//estacion

                    datos[4] = "Activo";//estado
                    if ("0".Equals(fila[3].ToString()))
                    {
                        datos[4] = "Inactivo";//estado
                    }


                    tabla.Rows.Add(datos);// cargar en la tabla los datos de cada proveedor

                }
                GridUsuarios.DataBind();
            }
        }
        private void llenarEstaciones()
        {
            cbxEstacion.Items.Add("Todas");
            cbxEstacion.Items.Add("La Selva");
            cbxEstacion.Items.Add("Palo Verde");
            cbxEstacion.Items.Add("Las Cruces");
            cbxEstacion.Items.Add("Palo Verde");
            cbxEstacion.Items.Add("North American Offices");
            cbxEstacion.Items.Add("Costa Rican Offices");            
        }

        private DataTable crearTablaUsuarios()
        {
            DataTable tabla = new DataTable();
            DataColumn columna;

            columna = new DataColumn();
            columna.DataType = System.Type.GetType("System.String");
            columna.ColumnName = "Username";
            tabla.Columns.Add(columna);

            columna = new DataColumn();
            columna.DataType = System.Type.GetType("System.String");
            columna.ColumnName = "Nombre";
            tabla.Columns.Add(columna);

            columna = new DataColumn();
            columna.DataType = System.Type.GetType("System.String");
            columna.ColumnName = "Roles";
            tabla.Columns.Add(columna);

            columna = new DataColumn();
            columna.DataType = System.Type.GetType("System.String");
            columna.ColumnName = "Estación";
            tabla.Columns.Add(columna);

            columna = new DataColumn();
            columna.DataType = System.Type.GetType("System.String");
            columna.ColumnName = "Estado";
            tabla.Columns.Add(columna);

            GridUsuarios.DataSource = tabla;
            GridUsuarios.AllowSorting = false;
            GridUsuarios.DataBind();

            return tabla;
        }
        /*
     * Efecto: capta el evento del botón para agregar
     * Requiere: presionar el botón.
     * Modifica: el seleccionado.
     */
        protected void clickAgregar(object sender, EventArgs e)
        {
            FormRegistro.usernameSeleccionado = "";
            FormRegistro.modo = 1;
            Response.Redirect("FormRegistro");
        }
        /*
      * Efecto: capta el evento del botón para consultar 
      * Requiere: presionar el botón.
      * Modifica: el seleccionado.
      */
        protected void clickConsultar(object sender, EventArgs e)
        {
            indice = obtenerIndex(sender, e) + (GridUsuarios.PageIndex * 20);//se obtiene la cedula a consultar
            FormRegistro.usernameSeleccionado = ids[indice];
            FormRegistro.modo = 0;
            Response.Redirect("FormRegistro");
        }

        protected int obtenerIndex(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            int index = Convert.ToInt32(row.RowIndex);
            return index;

        }

        /*
         *  Requiere: Controladores de eventos de la interfaz.
         *  Efectúa:  Cambia el contenido de la tabla al índice seleccionado.
         *  Retrona:  N/A
         */
        protected void PageIndexChanging(Object sender, GridViewPageEventArgs e)
        {
            GridUsuarios.PageIndex = e.NewPageIndex;
            GridUsuarios.DataBind();
        }
        protected void clickBuscar(object sender, EventArgs e)
        {
            String estacion = "";
            String nombre = "";
            String nombreUsuario = "";
            if (cbxEstacion.SelectedIndex != 0)
            {
                estacion = cbxEstacion.Value.ToString();
            }
            if (!"".Equals(txtUsername.Value.ToString()))
            {
                nombreUsuario = txtUsername.Value.ToString();
            }
            if (!"".Equals(txtNombre.Value.ToString()))
            {
                nombre = txtNombre.Value.ToString();
            }

            DataTable tabla = crearTablaUsuarios();
            Object[] datos = new Object[5];
            DataTable usuarios = controladora.solicitarUsuariosFiltro(estacion, nombreUsuario, nombre);// se consultan todos
            ids = new String[usuarios.Rows.Count];

            if (usuarios.Rows.Count > 0)
            {
                int i = 0;
                foreach (DataRow fila in usuarios.Rows)
                {
                    ids[i++] = fila[0].ToString();//username
                    datos[0] = fila[0].ToString();//username
                    datos[1] = fila[1].ToString();//nombre
                    EntidadUsuario usuarioSeleccionado = controladora.solicitarUsuario(fila[0].ToString());
                    string roles = "";
                    foreach (string rol in usuarioSeleccionado.Rol)
                    {
                        roles += rol + ", ";
                    }
                    datos[2] = roles;//rol
                    datos[3] = fila[2].ToString();//estacion

                    datos[4] = "Activo";//estado
                    if ("0".Equals(fila[3].ToString()))
                    {
                        datos[4] = "Inactivo";//estado
                    }


                    tabla.Rows.Add(datos);// cargar en la tabla los datos de cada proveedor

                }
                GridUsuarios.DataBind();
            }
        }
        protected void clickModificar(object sender, EventArgs e)
        {
            indice = obtenerIndex(sender, e) + (GridUsuarios.PageIndex * 20);//se obtiene la cedula a consultar
            FormRegistro.usernameSeleccionado = ids[indice];
            FormRegistro.modo = 2;
            Response.Redirect("FormRegistro");
        }
        protected void clickDesactivar(object sender, EventArgs e)
        {
            String[] error= controladora.desactivarUsuario(ids[indice]);
            mostrarMensaje(error[0], error[1], error[2]); // se muestra el resultado
            Response.Redirect("FormUsuario");
        }

        protected void seleccionarDesactivar(object sender, EventArgs e)
        {
            indice = obtenerIndex(sender, e) + (GridUsuarios.PageIndex * 20);//se obtiene la cedula a consultar
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>$('#modalDesactivar').modal('show');</script>", false);
        }

        /*
         * Efecto: mostrar en pantalla los mensajes del sistema, ya sean de error o de éxito.
         * Requiere: que se inicie y se active alguna de las funcionalidades.
         * Modifica: 
        */
        protected void mostrarMensaje(String tipoAlerta, String alerta, String mensaje)
        {
            alertAlerta.Attributes["class"] = "alert alert-" + tipoAlerta + " alert-dismissable fade in";
            labelTipoAlerta.Text = alerta + " ";
            labelAlerta.Text = mensaje;
            alertAlerta.Attributes.Remove("hidden");
            this.SetFocus(alertAlerta);
        }

    }
}