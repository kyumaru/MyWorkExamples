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
    public partial class FormEmpleadoReserva : System.Web.UI.Page
    {
        public static String idEmpleado = String.Empty;
        private ControladoraEmpleadoReserva controladora = new ControladoraEmpleadoReserva();
        private static EntidadServicios seleccionado = null;
        public static EntidadComidaCampo comidaCampoConsultada;
        public static EntidadComidaEmpleado comidaEmpleadoSeleccionado; 
        protected void Page_Load(object sender, EventArgs e)
        {
            ArrayList listaRoles = (ArrayList)Session["Roles"];
            string userid = (string)Session["username"];
            if (!IsPostBack)
            {
                if (userid == "" || userid == null)
                {
                    Response.Redirect("~/Ingresar.aspx");
                } if (!listaRoles.Contains("administrador sistema") && !listaRoles.Contains("recepcion"))
                {
                    Response.Redirect("ErrorPermiso.aspx");
                }
                iniciarEmpleado();
                cargarComidas();
                deshabilitarBotones();
            }
        }

        protected void llenarCampos()
        {


        }

        /*
         * Requiere: N/A
         * EFECTUA :Inserta todas las columnas de la tabla que pasa la contorladora en la tabla de datos del grid ge la GUI
         * retorna: N/A
         */
        internal void cargarComidas()
        {
            DataTable tabla = crearTablaComidaEmpleado();
            DataTable data = controladora.obtenerTabla(idEmpleado);
            DataTable datosComidaC = controladora.obtenerComidaCampo(idEmpleado);
            Object[] datos = new Object[5];
            String cat = "";
            foreach (DataRow fila in data.Rows)
            {
                //SELECT IDCOMIDAEMPLEADO,IDEMPLEADO,FECHA,PAGADO
                datos[0] = fila[0].ToString(); //IDCOMIDAEMPLEADO
                datos[1] = fila[1].ToString(); //Categoria
                datos[2] = ""; //tipo
                DateTime nuevo = DateTime.Parse(fila[3].ToString());
                datos[3] = nuevo.ToString("MM/dd/yyyy"); //FECHA
                datos[4] = (fila[4].ToString().CompareTo("T") == 0) ? "Efectivo" : "Deducción de Salario"; //PAGADO es un valor booleano a nivel logico.

                tabla.Rows.Add(datos);
            }

            foreach (DataRow fila in datosComidaC.Rows)
            {

                String tipo="";
                int opcion = int.Parse(fila[5].ToString());
                switch (opcion)
                {
                    case 1:
                        tipo = "Desayuno";
                        cat = "Incluido en Paquete";
                        break;
                    case 2:
                        tipo = "Almuerzo";
                        cat = "Incluido en Paquete";
                        break;
                    case 3:
                        tipo = "Cena";
                        cat = "Incluido en Paquete";
                        break;
                    case 4:
                        tipo = "Sandwich";
                        break;
                    case 5:
                        tipo = "Gallo Pinto";
                        break;
                    default:
                        break;


                }
                //SELECT IDCOMIDAEMPLEADO,IDEMPLEADO,FECHA,PAGADO,OPCION
                datos[0] = fila[0].ToString(); //IDCOMIDAEMPLEADO
                if (cat != "")
                {
                    datos[1] = cat; //Categoria
                }
                else
                {
                    datos[1] = fila[1].ToString(); //Categoria
                }
                
                datos[2] = tipo; //Tipo
                datos[3] = fila[3].ToString(); //FECHA
                datos[4] = (fila[4].ToString().CompareTo("De contado") == 0) ? "Efectivo" : "Deduccion de Salario"; //PAGADO es un valor booleano a nivel logico.

                tabla.Rows.Add(datos);
            }
           
            GridComidasReservadas.DataBind();
        }
        /**
        * Requiere: n/a
        * Efectua: Crea la DataTable para desplegar.
        * retorna:  un dato del tipo DataTable con la estructura para consultar.
        */
        //SELECT IDCOMIDAEMPLEADO,'Comida regular',IDEMPLEADO,FECHA,PAGADO
        protected DataTable crearTablaComidaEmpleado()//consultar
        {
            DataTable tabla = new DataTable();
            DataColumn columna;

            columna = new DataColumn();
            columna.DataType = System.Type.GetType("System.String");
            columna.ColumnName = "Numero de Orden";
            tabla.Columns.Add(columna);

            columna = new DataColumn();
            columna.DataType = System.Type.GetType("System.String");
            columna.ColumnName = "Categoría";
            tabla.Columns.Add(columna);

            columna = new DataColumn();
            columna.DataType = System.Type.GetType("System.String");
            columna.ColumnName = "Tipo";
            tabla.Columns.Add(columna);

            columna = new DataColumn();
            columna.DataType = System.Type.GetType("System.String");
            columna.ColumnName = "Fecha";
            tabla.Columns.Add(columna);

            columna = new DataColumn();
            columna.DataType = System.Type.GetType("System.String");
            columna.ColumnName = "Tipo de Pago";
            tabla.Columns.Add(columna);


            GridComidasReservadas.DataSource = tabla;
            GridComidasReservadas.DataBind();

            return tabla;
        }
        /*
         * Requiere:Argumentos de eventos de la GUI
         * Efectua :Revisa que tipo de comida es y redirecciona a la pagina correspondiente en modo de consulta
         * Retorna :N/A
         */
        protected void btnVer_Click(object sender, EventArgs e)
        {
            seleccionarComida(obtenerIndex(sender, e));
            GridViewRow row = GridComidasReservadas.SelectedRow;
            String tipo = row.Cells[5].Text;
            if (tipo.Contains("Comida regular"))
            {
                //llama comida empleado en modo de consulta
                FormComidasEmpleado.idComida=Int32.Parse( row.Cells[4].Text);//saca el id de la comida seleccionada.
                Debug.WriteLine(FormComidasEmpleado.idComida);
                FormComidasEmpleado.modo = 0;//0= Consultado; 1-Agregar Reservacion; 2-Modificar reservacion; 3-Cancelar
                FormComidasEmpleado.identificacionEmpleado = idEmpleado;
                Response.Redirect("FormComidasEmpleado.aspx");

            }
            else
            {
                FormComidaCampo.modo = 4;
                FormComidaCampo.idEmpleado = idEmpleado;
                FormComidaCampo.tipoComidaCampo = 1;
                Response.Redirect("FormComidaCampo");
            }
        }
        /*
         * Requiere:Argumentos de eventos de la GUI
         * Efectua :llama la interfaz de Comida de Empleado en modo de agregar
         * Retorna :N/A
         */
        protected void btnAgregarCR_Click(object sender, EventArgs e)
        {
            FormComidasEmpleado.modo = 1;//0= Consultado; 1-Agregar Reservacion; 2-Modificar reservacion; 3-Cancelar
            FormComidasEmpleado.identificacionEmpleado = idEmpleado;
            Response.Redirect("FormComidasEmpleado");
            }
        /*
         * Requiere:Argumentos de eventos de la GUI
         * Efectua :llama la interfaz de Comida de Campo en modo de agregar
         * Retorna :N/A
         */
        protected void btnAgregarCC_Click(object sender, EventArgs e)
        {
            FormComidaCampo.idEmpleado = idEmpleado;
            FormComidaCampo.modo = 1;
            FormComidaCampo.tipoComidaCampo = 1;
            Response.Redirect("FormComidaCampo");
        }

        /*
         * Requiere:Argumentos de eventos de la GUI
         * Efectua :Revisa que tipo de comida es y redirecciona a la pagina correspondiente en modo de editar
         * Retorna :N/A
         */
        protected void btnEditar_Click(object sender, EventArgs e)
        {
            seleccionarComida(obtenerIndex(sender, e));
            GridViewRow row = GridComidasReservadas.SelectedRow;
            String tipo = row.Cells[5].Text;
            if (tipo.Contains("Comida regular"))
            {
                //llama comida empleado en modo de Editar
                FormComidasEmpleado.idComida = Int32.Parse(row.Cells[4].Text);//saca el id de la comida seleccionada.
                FormComidasEmpleado.modo = 2;//0= Consultado; 1-Agregar Reservacion; 2-Modificar reservacion; 3-Cancelar
                FormComidasEmpleado.identificacionEmpleado = idEmpleado;
                Response.Redirect("FormComidasEmpleado");

            }
            else
            {
                FormComidaCampo.idEmpleado = idEmpleado;
                FormComidaCampo.modo = 2;
                FormComidaCampo.tipoComidaCampo = 1;
                Response.Redirect("FormComidaCampo");
            }
        }
        /*
         * Requiere:Argumentos de eventos de la GUI
         * Efectua :Revisa que tipo de comida es y redirecciona a la pagina correspondiente en modo de Cancelar
         * Retorna :N/A
         */
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            seleccionarComida(obtenerIndex(sender, e));
            GridViewRow row = GridComidasReservadas.SelectedRow;
            String tipo = row.Cells[5].Text;
            String[] mensaje;
            if (tipo.Contains("Comida regular"))
            {
                //llama comida empleado en modo de cancelar
                controladora.cancelarComidaRegular(Int32.Parse(row.Cells[4].Text));
                Response.Redirect(Request.Url.AbsoluteUri);

            }
            else
            {
                String idComida = row.Cells[4].Text;
                mensaje = controladora.cancelarComidaCampo(idComida);
                Response.Redirect(Request.Url.AbsoluteUri);
            }
        }
        /*
         * Requiere:Argumentos de eventos de la GUI
         * Efectua :Pone visible el panel de botones para poder trabajar sobre la fila selecconada.
         * Retorna :N/A
         */

        protected void seleccionarComida(int index)
        {
            GridComidasReservadas.SelectedIndex = index;
           
          //  btnEditar.Disabled = false;
          //  btnCancelar.Disabled = false;

            if (GridComidasReservadas.SelectedRow.Cells[5].Text != "Comida regular")//* es mejor comparar strings con "mi string".equals()
            {
                comidaCampoConsultada = controladora.consultarComidaCampoSeleccionada(idEmpleado, GridComidasReservadas.SelectedRow.Cells[4].Text);
                seleccionado = controladora.crearServicio(idEmpleado, int.Parse(comidaCampoConsultada.IdComidaCampo), comidaCampoConsultada.Fecha, "Comida Campo", "Notas no disponibles", comidaCampoConsultada.Estado, comidaCampoConsultada.Hora);
            }
            else
            {
                comidaEmpleadoSeleccionado = controladora.consultarComida(Int32.Parse(GridComidasReservadas.SelectedRow.Cells[4].Text));
                seleccionado = controladora.crearServicio(idEmpleado, comidaEmpleadoSeleccionado.IdComida, comidaEmpleadoSeleccionado.Fechas[0].ToString(), "Comida regular", comidaEmpleadoSeleccionado.Notas, "Depende del turno", "Depende del turno");
            }

        }
        /*
         * Requiere: N/A
         * Efectua : Carga el empleado seleccionado en la etiqueta de la GUI.
         * Retorna : N/A
         */
        private void iniciarEmpleado()
        {
            if (idEmpleado.Length != 0)//la cadena tiene algo
            {
                EntidadEmpleado empleado=controladora.obtenerEmpleado(idEmpleado);                
                txtApellido.Value = empleado.Apellido;
                txtNombre.Value = empleado.Nombre;
            }
            else
            {
              
            }
        }
        private void deshabilitarBotones()
        {
           // btnVer.Disabled = true;
           // btnEditar.Disabled = true;
           // btnCancelar.Disabled = true;
        }
        protected int obtenerIndex(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            return Convert.ToInt32(row.RowIndex);

        }
        protected void clickActivarTiquetes(object sender, EventArgs e)
        {
            seleccionarComida(obtenerIndex(sender, e));

            if (seleccionado != null)
            {
                controladora.activarTiquete();
                FormTiquete.retorno = Request.Url.AbsoluteUri;
                Response.Redirect("FormTiquete");
            }

        }
        /*
         *  Requiere: Controladores de eventos de la interfaz.
         *  Efectúa:  Cambia el contenido de la tabla al índice seleccionado.
         *  Retrona:  N/A
         */
        protected void GridViewReservaciones_PageIndexChanging(Object sender, GridViewPageEventArgs e)
        {

            GridComidasReservadas.PageIndex = e.NewPageIndex;
            GridComidasReservadas.DataSource = Session["tablaa"];
            GridComidasReservadas.DataBind();

        }
        /*
         * 
         * 
         * 
         */
        private void obtenerNotificaciones(){
            int numNotificaciones = controladora.getNotificaciones();
            contador.InnerText = numNotificaciones+"";
            if (numNotificaciones > 0)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('tiene " + numNotificaciones + " notificaciones nuevas');", true);
            }
        }

    }
}