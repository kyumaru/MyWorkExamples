using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Servicios_Reservados_2;
using System.Collections;
using System.Data;
using System.Diagnostics;

namespace Servicios_Reservados_2
{
    public partial class FormComidasEmpleado : System.Web.UI.Page
    {
        internal static String identificacionEmpleado = "";
        private static List<DateTime> list = new List<DateTime>();
        private static EntidadEmpleado empleadoSeleccionado;
        private static EntidadComidaEmpleado seleccionada;
        internal static int modo = 0;//0= Consultado; 1-Agregar Reservacion; 2-Modificar reservacion; 3-Cancelar
        internal static int idComida = -1;
        private ControladoraComidaEmpleado controladora = new ControladoraComidaEmpleado();
       //Atributos consultados
        private int idComidaViejo;
        private String idEmpleadoViejo;
        private List<DateTime> fechasViejo;
        private char[] turnosViejo;
        private bool pagadoViejo;
        private String notasViejo;
        public String estacion;

        /*
         * Requiere: Parametros de eventos de la GUI
         * Efectua : Valida que el ususario este registrado, en caso contrario lo envia a la pagina de inicio de sesion.
         * Retorna : N/A
         */
        protected void Page_Load(object sender, EventArgs e)
        {
            ArrayList listaRoles = (ArrayList)Session["Roles"];
            estacion = (string)Session["Estacion"];
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

                Debug.WriteLine("iniciando");
                arrancar();
            }
        }
        private void arrancar()
        {
            Debug.WriteLine("iniciando empleado");
            iniciarEmpleado();
            Debug.WriteLine("poniendo modo");
            ponerModo();
        }

        private void ponerModo()
        {
            ContenedorManejoDeHorario.Visible = true;
            Debug.WriteLine("Modo: "+modo);

            switch (modo)//0= Consultado; 1-Agregar Reservacion; 2-Modificar reservacion; 3-Cancelar
            {
                case 0: consultar();
                    break;
                case 1: GridFechasReservadas.Visible = true;
                    break;
                case 2: consultar();
                    break;
                case 3: consultar();
                    cancelar();
                    break;
                default: bloquearInterfaz();
                    break;
            }
        }

        private void cancelar()
        {
            controladora.eliminar(seleccionada);
        }
        /*
         * Requiere: Parametros de eventos de la GUI
         * Efectua : Cuando se carga el calendario en la GUI se agrega la fecha que acaba de ser seleccionada y se agrega a la lista de fechas. Si ya estaba en lista, lo remueve.
         * Retorna : N/A
         */

        protected void fechaDeEntradaCalendario_DayRender(object sender, DayRenderEventArgs e)
        {
            if (e.Day.IsSelected == true)
            {
                list.Add(e.Day.Date);
            }
            else
            {
                list.Remove(e.Day.Date);
            }
            Session["SelectedDates"] = list;
        }



        /*
         * Requiere: Parametros de eventos de la GUI
         * Efectua : Pone el modo de agregar y habilita los botones de insercion.
         * Retorna : N/A
         */
        protected void clickAgregar(object sender, EventArgs e)
        {
            modo = 1;
            ponerModo();

        }
        /*
         * Requiere: Parametros de eventos de la GUI
         * Efectua : Cambia de modo, despliega los datos y bloquea el calendario para editar.
         * Retorna : N/A
         */
        protected void clickModificar(object sender, EventArgs e)
        {
            ContenedorManejoDeHorario.Visible = true;
            //fechaDeEntradaCalendario.Enabled = false;//Solo se puede modificar una fecha a la vez
            //poner fecha seleccionada
            //fechaDeEntradaCalendario.SelectedDate = fechaElegida;
            modo = 2;
        }
        /*
         * Requiere: Parametros de eventos de la GUI
         * Efectua : limpia la interfaz y retorna a la interfaz anterior
         * Retorna : N/A
         */
        protected void clickCancelar(object sender, EventArgs e)
        {
            limpiarCalendario();
            Response.Redirect("~/FormEmpleadoReserva.aspx");
        }
        /*
         * Requiere: Parametros de eventos de la GUI
         * Efectua : envia los datos seleccionados a la controladora para ser eliminada.
         * Retorna : N/A
         */
        protected void clickEliminar(object sender, EventArgs e)
        {
            EntidadComidaEmpleado aCancelar = new EntidadComidaEmpleado(seleccionada.IdEmpleado, seleccionada.Estacion, seleccionada.Fechas, seleccionada.Turnos, seleccionada.Pagado, seleccionada.Notas, seleccionada.IdComida);
            controladora.eliminar(aCancelar);
        }
        /*
         * Requiere: Parametros de eventos de la GUI
         * Efectua : Evalua el modo en el que esta y apartir de alli llama el metodo que va a realizar la accion requerida.
         * Retorna : N/A
         */
        protected void clickAceptar(object sender, EventArgs e)
        {
            switch (modo)
            {
                case 1: agregarReservacion();   //agregar
                    break;

                case 2: modificarReservacion(); // modificar
                    break;
                default: break;
            }
            limpiarCalendario();   
        }
        /*
         * Requiere: N/A
         * Efectua : Limpia el calendario y la lista de fechas, y cambia a modo 0.
         * Retorna : N/A
         */
        protected void limpiarCalendario()
        {
            //fechaDeEntradaCalendario.SelectedDates.Clear();
            list.Clear();
            modo = 0;
        }
        /*
         * Requiere: Parametros de eventos de la GUI
         * Efectua : envia los datos de la GUI a la controladora para ser insertados.
         * Retorna : N/A
         */
        protected void agregarReservacion()
        {
            if (this.checkboxDesayuno.Checked || this.checkboxAlmuerzo.Checked || this.checkboxCena.Checked)
            {
                char[] Turnos = new char[3];
                Turnos[0] = (this.checkboxDesayuno.Checked) ? 'R' : 'N';//R = Reservado C= Consumido N=No reservado X=Cancelado
                Turnos[1] = (this.checkboxAlmuerzo.Checked) ? 'R' : 'N';//R = Reservado C= Consumido N=No reservado X=Cancelado
                Turnos[2] = (this.checkboxCena.Checked) ? 'R' : 'N';//R = Reservado C= Consumido N=No reservado X=Cancelado
                String[] resultado = controladora.agregar(identificacionEmpleado, estacion, list, Turnos, tipodePago.SelectedIndex == 1, notas.Value);
                modo = 5;
                ponerModo();
            }
            }

        /*
         * Requiere: Parametros de eventos de la GUI
         * Efectua : recupera los datos de la GUI y los manda a la controlador junto con los datos que se seleccionaron, para que se actualicen en la base de datos.
         * Retorna : N/A
         */
        protected void modificarReservacion()
        {
            char[] Turnos = new char[3];
            char valor;
            if (this.checkboxDesayuno.Checked)/*se mantuvo reservado*/
            {
                valor = (seleccionada.Turnos[0] == 'R' || seleccionada.Turnos[0] == 'N') ? 'R' : (seleccionada.Turnos[0] == 'C') ? 'C' : 'X';
            }
            else
            {
                valor = (seleccionada.Turnos[0] == 'N') ? 'N' : 'X';//Si estaba consumida, reservada o cancelada, los errores los manejaran las controladoras e informaran.
            }
            Turnos[0] = valor;
            //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------
            if (this.checkboxAlmuerzo.Checked)/*se mantuvo reservado*/
            {
                valor = (seleccionada.Turnos[1] == 'R' || seleccionada.Turnos[1] == 'N') ? 'R' : (seleccionada.Turnos[1] == 'C') ? 'C' : 'X';
            }
            else
            {
                valor = (seleccionada.Turnos[1] == 'N') ? 'N' : 'X';//Si estaba consumida, reservada o cancelada, los errores los manejaran las controladoras e informaran.
            }
            Turnos[1] = valor;
            //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------
            if (this.checkboxAlmuerzo.Checked)/*se mantuvo reservado*/
            {
                valor = (seleccionada.Turnos[1] == 'R' || seleccionada.Turnos[1] == 'N') ? 'R' : (seleccionada.Turnos[1] == 'C') ? 'C' : 'X';
            }
            else
            {
                valor = (seleccionada.Turnos[1] == 'N') ? 'N' : 'X';//Si estaba consumida, reservada o cancelada, los errores los manejaran las controladoras e informaran.
            }
            Turnos[2] = valor;
            controladora.modificar(seleccionada, empleadoSeleccionado.Id, estacion, list, Turnos, tipodePago.SelectedIndex == 1, notas.Value);
        }
        /* Requiere: N/A
         * Efectua : pide los datos a la controladora y los coloca en su posicion en la GUI.
         * Retorna : N/A
         */
        protected void consultar()
        {
            seleccionada = controladora.consultar(idComida);
            /****************************<guardarDatosViejos>********************************************/
            idComidaViejo = seleccionada.IdComida;
            idEmpleadoViejo= seleccionada.IdEmpleado;
            fechasViejo = seleccionada.Fechas;
            turnosViejo= seleccionada.Turnos;
            pagadoViejo= seleccionada.Pagado;
            notasViejo= seleccionada.Notas;
            /****************************</guardarDatosViejos>*******************************************/
            
            list = seleccionada.Fechas;
            notas.Value = seleccionada.Notas;
            Debug.WriteLine("notas: "+seleccionada.Notas);
            this.checkboxDesayuno.Checked = (seleccionada.Turnos[0] == 'R' || seleccionada.Turnos[0] == 'C');
            this.checkboxDesayuno.Disabled = (seleccionada.Turnos[0] == 'C');
            this.checkboxAlmuerzo.Checked = (seleccionada.Turnos[1] == 'R' || seleccionada.Turnos[1] == 'C');
            this.checkboxAlmuerzo.Disabled = (seleccionada.Turnos[1] == 'C');
            this.checkboxCena.Checked = (seleccionada.Turnos[2] == 'R' || seleccionada.Turnos[2] == 'C');
            this.checkboxCena.Disabled = (seleccionada.Turnos[2] == 'C');
            tipodePago.SelectedIndex = (seleccionada.Pagado) ? 0 : 1;
            this.fecha.Value = String.Format("{0:yyyy-MM-dd}", list[0]);
            bloquearInterfaz();

        }

        private void bloquearInterfaz()
        {
           switch (modo){//0= Consultado; 1-Agregar Reservacion; 2-Modificar reservacion; 3-Cancelar
               case 1:    this.fecha.Disabled = false;
                           this.btnFecha.Disabled = false;
                           this.notas.Disabled = false;
                           this.checkboxAlmuerzo.Disabled = false;
                           this.checkboxCena.Disabled = false;
                           this.checkboxDesayuno.Disabled = false;
                           this.tipodePago.Disabled = false;
                           this.btnAceptar.Disabled = false;
                           break;
               case 2:     this.fecha.Disabled = true;
                           this.btnFecha.Disabled = true;
                           this.notas.Disabled = false;
                           this.tipodePago.Disabled = false;
                           this.btnAceptar.Disabled = false;
                           this.checkboxCena.Disabled = (seleccionada.Turnos[2] == 'C');
                           this.checkboxDesayuno.Disabled = (seleccionada.Turnos[0] == 'C');
                           this.checkboxAlmuerzo.Disabled = (seleccionada.Turnos[2] == 'C');     
                           break;     
               default :this.fecha.Disabled = true;
                        this.btnFecha.Disabled = true;
                        this.notas.Disabled = true;
                        this.checkboxAlmuerzo.Disabled = true;
                        this.checkboxCena.Disabled = true;
                        this.checkboxDesayuno.Disabled = true;
                        this.tipodePago.Disabled = true;
                        this.btnAceptar.Disabled = true;
               break;
                
           }

        }
        /*
         * Requiere:N/A
         * Efectua :Pone en la etiqueta del nombre, la informacion del empleado.
         * Retrona :N/A
         */
        private void iniciarEmpleado()
        {            
            try
            {
                if (identificacionEmpleado.Length > 0)
                {
                    empleadoSeleccionado = controladora.getInformacionDelEmpleado(identificacionEmpleado); 
                   
                    txtNombre.Value = empleadoSeleccionado.Nombre + " " + empleadoSeleccionado.Apellido;
                    txtNombre.Disabled = true;
                }else{
                    Debug.WriteLine("oops");
                }
                
            }
            catch (Exception e)
            {
                //No se selecciono un empleado.

            }
        }

        protected void AgregarFecha_ServerClick(object sender, EventArgs e)
        {
            DateTime MyDateTime = DateTime.Parse(fecha.Value);
            if(MyDateTime.Date >DateTime.Now.Date ){
                
            DataTable tabla = crearTablaFechaComidaEmpleado();
            Object[] datos = new Object[1];
            datos[0] = String.Format("{0:MM-dd-yyyy}", MyDateTime);
            tabla.Rows.Add(datos);
            foreach (DateTime dt in list)
            {
                datos[0] = String.Format("{0:MM-dd-yyyy}", dt);          // "03/09/2008"
                
                tabla.Rows.Add(datos);
            }/*else{
                //deberia de enviar un error
            }*/
            GridFechasReservadas.DataBind();
            list.Add(MyDateTime);
            btnAceptar.Disabled = false;
            }
        }
        /**
         * Requiere: n/a
         * Efectua: Crea la DataTable para desplegar.
         * retorna:  un dato del tipo DataTable con la estructura para consultar.
         */
        protected DataTable crearTablaFechaComidaEmpleado()//consultar
        {
            DataTable tabla = new DataTable();
            DataColumn columna;

            columna = new DataColumn();
            columna.DataType = System.Type.GetType("System.String");
            columna.ColumnName = "Fecha Reservada";
            tabla.Columns.Add(columna);
            GridFechasReservadas.DataSource = tabla;
            GridFechasReservadas.DataBind();

            return tabla;
        }

    }
}