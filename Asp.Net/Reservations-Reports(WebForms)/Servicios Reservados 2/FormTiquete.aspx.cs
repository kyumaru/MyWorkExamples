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
    public partial class FormTiquete : System.Web.UI.Page
    {
        private static ControladoraTiquete controladora = new ControladoraTiquete();
        private static EntidadReservaciones reservacion;
        private static EntidadEmpleado empleado;
        private static EntidadServicios servicio;
        private static int modo;
        public static string retorno;

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
                if (!listaRoles.Contains("administrador sistema") && !listaRoles.Contains("recepcion"))
                {
                    Response.Redirect("ErrorPermiso.aspx");
                }
                llenarInfoServicio();
                llenarListaTiquetes();
                bloquearEdicionInfo();
                cambiarModo();
            }

        }
        protected void cambiarModo()
        {
            if (modo == 0)
            {

            }
            else if (modo == 1)
            {

            }
        }

        private void bloquearEdicionInfo()
        {
            anfitriona.Disabled = true;
            estacion.Disabled = true;
            numero.Disabled = true;
            solicitante.Disabled = true;
            categoria.Disabled = true;
            estado.Disabled = true;
            pax.Disabled = true;
        }

        private void llenarListaTiquetes()
        {
            DataTable tabla = crearTablaTiquetes();
            Object[] datos = new Object[2];
            DataTable tiquetes = controladora.solicitarTiquetes(servicio.IdServicio, servicio.IdSolicitante, servicio.Fecha, servicio.Hora);// se consultan todos
            if (tiquetes.Rows.Count > 0)
            {
                foreach (DataRow fila in tiquetes.Rows)
                {
                    datos[0] = fila[0].ToString();
                    datos[1] = fila[1].ToString();
                    tabla.Rows.Add(datos);// cargar en la tabla los datos de cada proveedor
                }
            }
            GridViewTiquetes.DataBind();
        }

        private void llenarInfoServicio()
        {
            servicio = controladora.solicitarInfoServicio();
            categoria.Value = servicio.Categoria;
            estado.Value = servicio.Estado;
            pax.Value = servicio.Pax.ToString();

            if ("empleado".Equals(servicio.TipoSolicitante))
            {
                empleado = controladora.solicitarInfoEmpleado();
                anfitriona.Value = "No disponible";
                estacion.Value = "No disponible";
                numero.Value = empleado.Id;
                solicitante.Value = empleado.Nombre + " " + empleado.Apellido;


            }
            else if ("reservacion".Equals(servicio.TipoSolicitante))
            {
                reservacion = controladora.solicitarInfoReservacion();
                anfitriona.Value = reservacion.Anfitriona;
                estacion.Value = reservacion.Estacion;
                numero.Value = reservacion.Numero;
                solicitante.Value = reservacion.Solicitante;
            }


        }
        protected void clickAgregar(object sender, EventArgs e)
        {
            String[] error = controladora.activarTiquete(int.Parse(numTiquete.Value));// se le pide a la controladora que lo inserte
            if ("La informacion ingresada ya existe".Equals(error[2]))
            {
                mostrarMensaje(error[0], error[1], "Este tiquete ya se encuentra asociado a un servicio"); // se muestra el resultado     

            }
            else
            {
                Response.Redirect(Request.Url.AbsoluteUri);
            }

        }
        protected void seleccionarTiquete(int index)
        {
          GridViewTiquetes.SelectedIndex = index;
          controladora.seleccionarTiquete(int.Parse(GridViewTiquetes.SelectedRow.Cells[1].Text));
        }
        protected void clickQuitar(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            int i = Convert.ToInt32(row.RowIndex);
            seleccionarTiquete(i);
            controladora.desactivarTiquete();
            Response.Redirect(Request.Url.AbsoluteUri);

        }
        protected DataTable crearTablaTiquetes()//consultar
        {
            DataTable tabla = new DataTable();
            DataColumn columna;

            columna = new DataColumn();
            columna.DataType = System.Type.GetType("System.String");
            columna.ColumnName = "Número";
            tabla.Columns.Add(columna);

            columna = new DataColumn();
            columna.DataType = System.Type.GetType("System.String");
            columna.ColumnName = "Consumido";
            tabla.Columns.Add(columna);

            GridViewTiquetes.DataSource = tabla;
            GridViewTiquetes.DataBind();

            return tabla;
        }
        protected void clickCancelar(object sender, EventArgs e)
        {
            Response.Redirect(retorno);

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