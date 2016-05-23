using Servicios_Reservados_2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Diagnostics;
using System.Collections;

namespace Servicios_Reservados_2
{
    public partial class FormComidaExtra : System.Web.UI.Page
    {
        static DataTable tipo;//contiene los diferentes tipos de comida extra

        private static ControladoraComidaExtra controladora = new ControladoraComidaExtra();//instancia de la controladora de comida extra
        EntidadComidaExtra entidadConsultada = controladora.servicioSeleccionados();//buscamos el servicio consultado en la controladora
        public static EntidadReservaciones reservConsultada = controladora.reservacionSeleccionada();

        private static String[] idReservacion = FormReservaciones.ids;
        private static int modo;//variable para controlar el modo en el que se encuentra el sistema (modificar, consultar, agregar o eliminar)

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
                cargarDatos();
            }
        }

        /*
         * Efecto: activa las funciones necesarias para cargar los datos en la pantalla. 
         * Requiere: iniciar el FormComidaExtra.
         * Modifica: no realiza modificaciones, solo carga la pantalla.
        */
        void cargarDatos()
        {
            modo = FormServicios.modo;
            llenarInfoServicio();
            llenarComboBoxTipo();
            llenarCbxTipoPago();
            cambiarModo();
        }

        /*
         * Efecto: rellena los campos con la información de la reservación. 
         * Requiere: iniciar el FormComidaExtra y la instancia de la controladora.
         * Modifica: no realiza modificaciones, solo carga la pantalla.
        */
        void llenarInfoServicio()
        {
            txtSolicitante.Disabled = true;
            txtNumReservacion.Disabled = true;
            EntidadReservaciones res = controladora.informacionServicio();
            txtSolicitante.Value = res.Solicitante;
            txtNumReservacion.Value = res.Numero;
            DateTime fecha = DateTime.Parse(controladora.informacionServicio().FechaInicio.ToString());
            txtFechaInicial.Value = fecha.ToString("MM/dd/yyyy");
            fecha = DateTime.Parse(controladora.informacionServicio().FechaSalida.ToString());
            txtFechaFinal.Value = fecha.ToString("MM/dd/yyyy");
            txtEstacion.Value = controladora.informacionServicio().Estacion.ToString();
            txtAnfitriona.Value = controladora.informacionServicio().Anfitriona.ToString();
        }


        /*
         * Efecto: llena el cbxTipo con las diferentes opciones de comidas extras.
         * Requiere: iniciar el FormComidaExtra.
         * Modifica: los valores del cbxTipo.
        */
        void llenarComboBoxTipo()
        {
            tipo = controladora.solicitarTipo();// solicita los tipos a la controladora
            cbxTipo.Items.Clear();// limpiamos el combobox
            //cbxTipo.Items.Add("Seleccionar");// agregamos seleccionar
            if (tipo.Rows.Count > 0)
            {// agregamos cada uno de los tipos 
                foreach (DataRow fila in tipo.Rows)
                {
                    cbxTipo.Items.Add(fila[1].ToString());
                }
            }
        }

        void llenarCbxTipoPago()
        {
            cbxTipoPago.Items.Clear();// limpiamos el combobox
            cbxTipoPago.Items.Add("Efectivo");
            cbxTipoPago.Items.Add("Tarjeta");
            cbxTipoPago.Items.Add("Deducción de planilla");
        }

        /*
         * Efecto: llena los campos de la interfaz con los valores del servicio extra consultado.
         * Requiere: iniciar el FormComidaExtra en modo consultar.
         * Modifica: los valores de los componentes de la pantalla.
        */
        public Boolean consultarServicio()
        {
            Boolean res = true;
            //los desplegamos en cada uno de los componentes de la pantalla
            cbxHora.Items.Clear();
            cbxHora.Items.Add(entidadConsultada.Hora);
            txtPax.Value = entidadConsultada.Pax.ToString();
            cbxTipo.Text = controladora.consultarTipo(controladora.servicioSeleccionados().IdServiciosExtras);
            txaNotas.Value = entidadConsultada.Descripcion;
            textFecha.Value = entidadConsultada.Fecha.ToString();
            cbxTipoPago.Value = entidadConsultada.TipoPago;
            return res;
        }

        /*
         * Efecto: extrale los valores introducidos en la interfaz y los envia a la controladora para que sean insertados en la BD.
         * Requiere: que los campos estén rellenados con datos válidos (compatibles con la BD).
         * Modifica: 
        */
        protected Boolean agregarServicioExtra()
        {
            Boolean res = true;
            DateTime fechaInicio = DateTime.Parse(reservConsultada.FechaInicio.ToString());
            DateTime fechaFin = DateTime.Parse(reservConsultada.FechaSalida.ToString());
            DateTime fechaSelect = fechaDeEntradaCalendario.SelectedDate;
            DateTime fechaActual = DateTime.Today;

            if (fechaSelect < fechaInicio || fechaSelect > fechaFin || fechaSelect < fechaActual)
            {
                mostrarMensaje("danger", "Error:", "Revise la fecha selccionada, debe estar dentro de la reservación");
                res = false;
            }
            else
            {

                Object[] nuevoServicio = new Object[9];// objeto en el que se almacenan los datos para enviar a encapsular.

                nuevoServicio[0] = controladora.informacionServicio().Id;//recuperamos el id de la reservación
                //en adelante se extrae la información de cada uno de los componentes de la interfaz.
                int indice = cbxTipo.SelectedIndex - 1;
                nuevoServicio[1] = tipo.Rows[indice][0];
                nuevoServicio[2] = textFecha.Value;
                nuevoServicio[3] = "Activo";
                nuevoServicio[4] = txaNotas.Value;
                nuevoServicio[5] = txtPax.Value;
                nuevoServicio[6] = cbxHora.Value;
                nuevoServicio[7] = cbxTipoPago.Value;
                nuevoServicio[8] = "";


                String[] error = controladora.agregarServicioExtra(nuevoServicio);// se le pide a la controladora que lo inserte
                if ("danger".Equals(error[0]))
                {
                    res = false;
                }
                mostrarMensaje(error[0], error[1], error[2]); // se muestra el resultado
            }
            return res;
        }

        /*
         * Efecto: extrale los valores introducidos en la interfaz y los envia a la controladora para modificarlos en la BD.
         * Requiere: que se consulte previamente una comida extra y que los campos estén rellenados con datos válidos (compatibles con la BD).
         * Modifica: 
        */
        protected Boolean modificarServicioExtra()
        {
            Boolean res = true;
            DateTime fechaInicio = DateTime.Parse(reservConsultada.FechaInicio.ToString());
            DateTime fechaFin = DateTime.Parse(reservConsultada.FechaSalida.ToString());
            DateTime fechaSelect = DateTime.Parse(textFecha.Value);
            DateTime fechaActual = DateTime.Today;

            if (fechaSelect < fechaInicio || fechaSelect > fechaFin || fechaSelect < fechaActual)
            {
                mostrarMensaje("danger", "Error:", "Revise la fecha selccionada, debe estar dentro de lás fechas reservadas-");
                res = false;
            }
            else
            {
                Object[] nuevoServicio = new Object[9];// objeto en el que se almacenan los datos para enviar a encapsular.
                nuevoServicio[0] = controladora.informacionServicio().Id;//recuperamos el id de la reservación.
                //en adelante se extrae la información de cada uno de los componentes de la interfaz.
                int indice = cbxTipo.SelectedIndex - 1;
                nuevoServicio[1] = tipo.Rows[indice][0];
                nuevoServicio[2] = textFecha.Value;
                nuevoServicio[3] = "Activo";
                nuevoServicio[4] = txaNotas.Value;
                nuevoServicio[5] = txtPax.Value;
                nuevoServicio[6] = cbxHora.Value;
                nuevoServicio[7] = cbxTipoPago.Value;
                nuevoServicio[8] = "";

                String[] error = controladora.modificarServicioExtra(nuevoServicio, entidadConsultada);// se le pide a la controladora que lo inserte
                mostrarMensaje(error[0], error[1], error[2]); // se muestra el resultado


            }
            return res;
        }

        /*
         * Efecto: capta el evento al presionar el botón aceptar y realiza la operación de acuerdo al modo en el que se encuentra el sistema.
         * Requiere: presionar el botón.
         * Modifica: 
        */
        protected void clickAceptar(object sender, EventArgs e)
        {
            bool accion;
            switch (modo)
            {
                case 1://insertar
                    accion = agregarServicioExtra();
                    if (accion)
                    {
                        Response.Redirect("FormServicios");
                    }
                    break;
                case 2://modificar

                    accion = modificarServicioExtra();
                    if (accion)
                    {
                        Response.Redirect("FormServicios");
                    }
                    break;
            }
        }

        /*
         * Efecto: capta el evento al presionar el botón cancelar y regresa a la pantalla de servicios.
         * Requiere: presionar el botón.
         * Modifica: 
        */
        protected void clickCancelar(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "OpenModal", "OpenModal()", true);
            Response.Redirect("FormServicios");
        }


        /*
         * Efecto: cambia de modo de acuerdo a la operación a realizar (consultar=0, agrgar=1, modificar=2 y eliminar=3).
         * Requiere: presionar el botón.
         * Modifica: los estados de los componentes de pantalla y variables locales. 
        */
        protected void cambiarModo()
        {
            switch (modo)
            {
                case 0: //consultar
                    cbxHora.Disabled = true;
                    txtPax.Disabled = true;
                    txaNotas.Disabled = true;
                    textFecha.Disabled = true;
                    cbxTipo.Enabled = false;
                    cbxTipoPago.Disabled = true;
                    btnAceptar.Disabled = true;
                    fechaDeEntrada.Disabled = true;
                    consultarServicio();
                    fechaDeEntrada.Disabled = true;
                    break;
                case 1://agregar
                    txtPax.Value = controladora.paxConsultado(reservConsultada.Numero);
                    fechaDeEntradaCalendario.SelectedDate = DateTime.Today;
                    textFecha.Value = DateTime.Today.ToString("MM/dd/yyyy");
                    btnAnular.Disabled = true;
                    btnEditar.Disabled = true;
                    break;
                case 2://modificarcbxHora.Disabled = true;
                    txtPax.Disabled = false;
                    txaNotas.Disabled = false;
                    textFecha.Disabled = false;
                    cbxTipo.Enabled = true;
                    cbxTipoPago.Disabled = false;
                    btnAceptar.Disabled = false;
                    fechaDeEntrada.Disabled = false;
                    consultarServicio();
                    fechaDeEntrada.Disabled = false;
                    fechaDeEntradaCalendario.SelectedDate = DateTime.Parse(entidadConsultada.Fecha);
                    textFecha.Value = entidadConsultada.Fecha;
                    btnEditar.Disabled = true;
                    break;
            }
        }

        /*
         * Efecto: mostrar en pantalla los mensajes del sistema, ya sean de error o de éxito.
         * Requiere: que se inicie el FormComidaExtra y se active alguna de las funcionalidades.
         * Modifica: 
        */
        protected void mostrarMensaje(String tipoAlerta, String alerta, String mensaje)
        {
            alertAlerta.Attributes["class"] = "alert alert-" + tipoAlerta + " alert-dismissable fade in";
            labelTipoAlerta.Text = alerta + " ";
            labelAlerta.Text = mensaje;
            alertAlerta.Attributes.Remove("hidden");
        }

        /*
         * Efecto: capta el evento al presionar el botón del calendario para hacerlo visible.
         * Requiere: presionar el calendario.
         * Modifica: la apariencia del calendario en pantalla. 
        */
        protected void fechaDeEntrada_ServerClick(object sender, EventArgs e)
        {
            fechaDeEntradaCalendario.Visible = !fechaDeEntradaCalendario.Visible;
        }

        /*
         * Efecto: capta el evento al presionar una fecha en el calendario y lo pasa al textFexha.
         * Requiere: presionar el calendario y una de las fechas.
         * Modifica: 
        */
        protected void fechaDeEntradaCalendario_SelectionChanged(object sender, EventArgs e)
        {
            textFecha.Value = fechaDeEntradaCalendario.SelectedDate.ToString("MM/dd/yyyy");
            fechaDeEntradaCalendario.Visible = false;
        }

        /*
         * Efecto: capta el evento al seleccionar uno de los tipos de comida y filtra las horas.
         * Requiere: presionar el cbxTipo.
         * Modifica: el estado del cbxHora.
        */
        protected void clickAdaptarHora(object sender, EventArgs e)
        {
            int inicio = 0;
            int fin = 0;
            if (cbxTipo.Text == "Desayuno")
            {
                inicio = 6;
                fin = 9;
            }
            if (cbxTipo.Text == "Almuerzo")
            {
                inicio = 11;
                fin = 14;
            }
            if (cbxTipo.Text == "Cena")
            {
                inicio = 18;
                fin = 21;
            }
            if (cbxTipo.Text == "Cafe" || cbxTipo.Text == "Queque")
            {
                inicio = 9;
                fin = 21;
            }

            cbxHora.Items.Clear();// limpiamos el combobox
            for (int i = inicio; i <= fin; ++i)
            {
                String horas = i.ToString() + ":00";
                cbxHora.Items.Add(horas);
            }
        }

        /*
         * Efecto: capta el evento al seleccionar el boton editar y hace el llamado a modificar.
         * Requiere: presionar el boton editar.
         * Modifica: la base de datos.
        */
        protected void clickModificar(object sender, EventArgs e)
        {
            modo = 2;
            cambiarModo();
        }

        /*
         * Efecto: capta el evento al seleccionar anular y pide a la controladora que cancele el servicio.
         * Requiere: presionar el boton anular.
         * Modifica: la base de datos.
        */
        protected void clickAnular(object sender, EventArgs e)
        {
            controladora.cancelarComidaExtra(entidadConsultada.IdComidaExtra);
            Response.Redirect("FormServicios");
        }
    }
}