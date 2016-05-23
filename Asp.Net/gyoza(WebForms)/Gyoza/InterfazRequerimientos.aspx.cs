using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.ApplicationServices;
using Gyoza.Modulo_Proyecto;
using Gyoza.Modulo_Cuenta;
using Gyoza.Modulo_Requerimientos;
using System.Windows.Forms;
using System.Threading;
using System.IO;

namespace Gyoza
{
    public partial class InterfazRequerimientos : System.Web.UI.Page
    {

        private static string requerimientoConsultar = "";
        private static int confirmacion = 0;
        private static Boolean aceptado = false;
        private static bool seConsulto = false;
        private static Object[] idsGrid;
        private static Object[] idsCriterio;
        private static DataTable criteriosEnMemoria;
        private static Object pEncargado;
        private static Object sEncargado;
        private static ControladoraRequerimientos controladora;
        private static ControladoraProyecto controladoraP;
        private static int modo = 0;
        private static EntidadProyecto proyectoConsultado;
        private static EntidadRequerimiento requerimientoConsultado;
        private static String permisos = "1111";
        private static int resultadosPorPagina;
        private static Object[] idArray;
        private static bool tieneArchivos = false;
        private static DataTable tablaArchivosEnMemoria;
        private static DataTable listadoArchivos;



        protected void Page_Load(object sender, EventArgs e)
        {


            controladora = new ControladoraRequerimientos();
            controladoraP = new ControladoraProyecto();
            resultadosPorPagina = gridViewRequerimientos.PageSize;
            llenarGridProyecto();

            ((SiteMaster)Page.Master).markModule("reqs");//con esto se setea el tab en el site master puede q haga falta cambiarlo de lugar


            if (((SiteMaster)Page.Master).getBandera())
            {
                proyectoConsultado = ((SiteMaster)Page.Master).getProyecto();
                setDatosConsultadosProyecto();
                modo = 1;
            }

            if (((SiteMaster)Page.Master).getRol() != null)
            {
                this.botonAyuda.Disabled = false;
                permisos = ((SiteMaster)Page.Master).getRol().PermisosRequerimiento;
            }
            else
            {
                permisos = "0000";
                modo = 0;
                proyectoConsultado = null;
                this.botonAyuda.Disabled = true;
            }

            cambiarModo();

            if (!IsPostBack)
            {
                if (proyectoConsultado != null)
                {
                    setDatosConsultadosProyecto();
                    llenarListMiembros();
                    llenarGridRequerimiento();
                    cargarIteraciones();

                    botonAgregarIteracion.Disabled = false;
                    botonEliminarIteracion.Disabled = false;


                    botonAgregarModulo.Disabled = false;
                    botonEliminarModulo.Disabled = false;

                    if (!seConsulto)
                        modo = 1;
                    else
                    {
                        if (requerimientoConsultado != null)
                        {
                            setDatosConsultadosRequerimiento();
                            llenaGridArchivos();
                            inicializarGridCriterio();
                        }
                        else
                            mostrarMensaje("warning", "Alerta: ", "No se pudo consultar el requerimiento.");
                        seConsulto = false;
                    }
                }
            }


        }

        protected void aplicarPermisos()
        {
            // La posicion 0 representa agregar
            if (permisos[0] == '0')
            {
                this.botonInsertar.Disabled = true;
            }
            // La posicion 1 representa modificar
            if (permisos[1] == '0')
            {
                this.botonModificar.Disabled = true;
            }
            // La posicion 2 representa eliminar
            if (permisos[2] == '0')
            {
                this.botonEliminar.Disabled = true;
            }
            // La posicion 3 representa consultar
            if (permisos[3] == '0')
            {
                this.botonConsultar.Disabled = true;
                this.A1.Disabled = true;
            }
        }

        protected void habilitarCampos(bool habilitar)
        {
            // Siempre inhabilitados los campos de consulta
            textNombre.Enabled = false;
            textEstado.Enabled = false;
            //textOficinaProp.Enabled = false;
            textDescripcionProyecto.Enabled = false;

            textNombreRequerimiento.Enabled = habilitar;
            textPrioridad.Enabled = habilitar;
            textEstadoRequerimiento.Enabled = habilitar;
            textRol.Enabled = habilitar;
            textContenido.Enabled = habilitar;
            textRazon.Enabled = habilitar;
            textEstimacion.Enabled = habilitar;
            textPrimerEncargado.Enabled = habilitar;
            textSegundoEncargado.Enabled = habilitar;
            radioButtonList1.Enabled = habilitar;
            //gridArchivos.Enabled = habilitar;
            gridArchivos.Columns[1].Visible = habilitar;
            gridCriteriosAceptacion.Enabled = habilitar;
            textEscenario.Enabled = habilitar;
            textCriterioAceptacion.Enabled = habilitar;
            botonCriterio.Disabled = !habilitar;
            botonAgregarArchivo.Disabled = !habilitar;

            ListaIteracion.Enabled = habilitar;
            botonAgregarIteracion.Disabled = !habilitar;
            botonEliminarIteracion.Disabled = !habilitar;

            ListaModulo.Enabled = habilitar;
            botonAgregarModulo.Disabled = !habilitar;
            botonEliminarModulo.Disabled = !habilitar;


        }

        protected void consultarRequerimiento(String id)
        {
            seConsulto = true;
            try
            {
                requerimientoConsultado = controladora.consultarRequerimiento(proyectoConsultado.Identificador, id);
                modo = 5;
            }
            catch
            {
                requerimientoConsultado = null;
                modo = 1;
            }
            cambiarModo();
        }
        protected void ocultarMensaje()
        {
            Alert.Attributes.Add("hidden", "hidden");
        }

        protected void clickAceptar(object sender, EventArgs e)
        {

            Boolean operacionCorrecta = true;
            int idInsertado = 0;

            if (modo == 2)
            {
                idInsertado = insertar();

                if (idInsertado != 0)
                {
                    operacionCorrecta = true;
                    requerimientoConsultado = controladora.consultarRequerimiento(proyectoConsultado.Identificador, this.textNombreRequerimiento.Text);
                    modo = 5;
                    habilitarCampos(false);
                }
            }
            else if (modo == 3)
            {
                operacionCorrecta = modificar();
            }
            if (operacionCorrecta)
            {
                seConsulto = true;
                cambiarModo();
            }
           // Response.Redirect("InterfazRequerimientos.aspx");
        }





        protected void clickCancelar(object sender, EventArgs e)
        {
            modo = 1;
            cambiarModo();
            limpiarCampos();
            limpiarListMiembros();
            requerimientoConsultado = null;
            limpiarCriterios();
            limpiarGridArchivos();
        }

        protected void clickAceptarCriterio(object sender, EventArgs e)
        {
            insertarCriterio();
            textEscenario.Text = "";
            textCriterioAceptacion.Text = "";
        }

        protected void clickCancelarProyecto(object sender, EventArgs e)
        {
            modo = 0;
            cambiarModo();
            limpiarCampos();
            requerimientoConsultado = null;
            proyectoConsultado = null;
        }

        protected int insertar()
        {
            //:3
            int id = 0;
            Object[] requerimiento = obtenerDatosRequerimiento();

            String[] error = controladora.insertarRequerimiento(requerimiento);

            // id = Convert.ToInt32(error[3]);
            mostrarMensaje(error[0], error[1], error[2]);
            if (error[0].Contains("success"))
            {
                consolidarCriterios();
                foreach (DataRow row in tablaArchivosEnMemoria.Rows)
                    row[1] = textNombreRequerimiento.Text;
                agregarArchivos();
                llenarGridRequerimiento();
                id = 1;
            }
            else
            {
                id = 0;
                modo = 2;
            }

            return id;
        }


        protected Boolean modificar()
        {
            Boolean res = true;

            Object[] usuario = obtenerDatosRequerimiento();
            String[] error = controladora.modificarRequerimiento(usuario, requerimientoConsultado);
            mostrarMensaje(error[0], error[1], error[2]);

            if (error[0].Contains("success"))
            {
                consolidarCriterios();
                //eliminarArchivosSeleccionados();
                foreach (DataRow row in tablaArchivosEnMemoria.Rows)
                    row[1] = textNombreRequerimiento.Text;
                agregarArchivos();

                llenarGridRequerimiento();

                requerimientoConsultado = controladora.consultarRequerimiento(proyectoConsultado.Identificador, requerimientoConsultado.Nombre);
                modo = 5;
            }
            else
            {
                res = false;
                modo = 3;
            }
            return res;
        }


        protected void mostrarMensaje(String tipoAlerta, String alerta, String mensaje)
        {

            Alert.Attributes["class"] = "alert alert-" + tipoAlerta + " alert-dismissable fade in";
            LabelAlert.Text = alerta + " ";
            //Revisar lo de LabelTipoAlert del color
            LabelAlert.Text = mensaje;
            Alert.Attributes.Remove("hidden");
        }

        protected Object[] obtenerDatosRequerimiento()
        {
            Object[] datos = new Object[13];
            datos[0] = proyectoConsultado.Identificador;
            datos[1] = this.textNombreRequerimiento.Text.Trim();
            datos[2] = this.textPrioridad.Text.Trim();
            datos[3] = this.textEstadoRequerimiento.SelectedValue;
            datos[4] = this.textRol.Text.Trim();
            datos[5] = this.textContenido.Text.Trim();
            datos[6] = this.textRazon.Text.Trim();
            datos[7] = this.textEstimacion.Text.Trim();
            if (ListaIteracion.Text != "")
                datos[8] = this.ListaIteracion.Text;
            else
                datos[8] = null;
            if (ListaModulo.Text != "")
                datos[9] = this.ListaModulo.Text;
            else
            {
                datos[9] = null;
                datos[8] = null;
            }
            datos[10] = this.radioButtonList1.SelectedValue.Equals("Funcional");
            datos[11] = this.textPrimerEncargado.SelectedValue;
            datos[12] = this.textSegundoEncargado.SelectedValue;
            return datos;
        }

        protected void cambiarModo()
        {
            switch (modo)
            {
                case 0: /* No hay proyecto consultado */
                    botonAceptar.Disabled = true;
                    botonCancelar.Disabled = true;
                    // botonCancelarProyecto.Disabled = true;
                    botonInsertar.Disabled = true;
                    botonModificar.Disabled = true;
                    botonEliminar.Disabled = true;
                    botonConsultar.Disabled = true;
                    limpiarCampos();
                    limpiarCamposProyecto();
                    habilitarCampos(false);

                    break;
                case 1: /* Proyecto consultado, pero no requerimiento */
                    botonAceptar.Disabled = true;
                    botonCancelar.Disabled = true;
                    //botonCancelarProyecto.Disabled = false;
                    botonInsertar.Disabled = false;
                    botonModificar.Disabled = true;
                    botonEliminar.Disabled = true;
                    botonConsultar.Disabled = false;
                    limpiarCampos();
                    habilitarCampos(false);
                    ListaIteracion.Enabled = true;
                    ListaModulo.Enabled = true;
                    break;

                case 2: /* Insertar */
                case 3: /* Modificar */
                    botonAceptar.Disabled = false;
                    botonCancelar.Disabled = false;
                    //botonCancelarProyecto.Disabled = false;
                    botonInsertar.Disabled = true;
                    botonModificar.Disabled = true;
                    botonEliminar.Disabled = true;
                    botonConsultar.Disabled = false;
                    habilitarCampos(true);
                    break;

                case 4: /* Eliminar */
                case 5: /* Consultar */
                    botonAceptar.Disabled = true;
                    botonCancelar.Disabled = false;
                    //botonCancelarProyecto.Disabled = false;
                    botonInsertar.Disabled = true;
                    botonModificar.Disabled = false;
                    botonEliminar.Disabled = false;
                    botonConsultar.Disabled = false;
                    habilitarCampos(false);
                    break;

                default:
                    break;
            }
            aplicarPermisos();
        }

        protected void clickInsertar(object sender, EventArgs e)
        {
            modo = 2;
            inicializarGridCriterio();
            llenaGridArchivos();
            cambiarModo();
        }

        protected void clickModificar(object sender, EventArgs e)
        {
            modo = 3;
            cambiarModo();
        }

        protected void clickAceptarEliminar(object sender, EventArgs e)
        {
            String[] error = controladora.eliminarRequerimiento(requerimientoConsultado);
            mostrarMensaje(error[0], error[1], error[2]);

            // Eliminar reinicia la interfaz, pero no deselecciona el proyecto
            if (error[0].Contains("success"))
            {
                modo = 1;
                requerimientoConsultado = null;
                llenarGridProyecto();
                llenarGridRequerimiento();
                limpiarCampos();
                limpiarCriterios();
                cambiarModo();
            }
        }

        protected void limpiarCampos()
        {
            this.textNombreRequerimiento.Text = "";
            this.textRol.Text = "";
            this.textContenido.Text = "";
            this.textRazon.Text = "";
            this.textEstimacion.Text = "";
            //this.ListaIteracion.SelectedValue = "";
            //this.ListaModulo.SelectedValue = "";
            this.textPrioridad.Text = null;
            this.textPrimerEncargado.SelectedValue = null;
            this.textSegundoEncargado.SelectedValue = null;
            this.radioButtonList1.SelectedValue = null;
        }

        protected void limpiarCamposProyecto()
        {
            textNombre.Text = "";
            textEstado.Text = "";
            //textOficinaProp.Text = "";
            textDescripcionProyecto.Text = "";

        }

        protected void gridViewRequerimientos_Seleccion(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Select":
                    GridViewRow filaSeleccionada = this.gridViewRequerimientos.Rows[Convert.ToInt32(e.CommandArgument)];
                    String id = idsGrid[Convert.ToInt32(e.CommandArgument) + (this.gridViewRequerimientos.PageIndex * resultadosPorPagina)].ToString();
                    consultarRequerimiento(id);
                    Response.Redirect("InterfazRequerimientos.aspx");
                    break;
            }
        }

        protected void gridViewRequerimientos_CambioPagina(Object sender, GridViewPageEventArgs e)
        {
            this.gridViewRequerimientos.PageIndex = e.NewPageIndex;
            this.gridViewRequerimientos.DataBind();
        }

        protected void gridCriteriosAceptacion_Seleccion(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "delete":
                    GridViewRow filaSeleccionada = this.gridCriteriosAceptacion.Rows[Convert.ToInt32(e.CommandArgument)];
                    int id = Convert.ToInt32(e.CommandArgument) + (this.gridCriteriosAceptacion.PageIndex * resultadosPorPagina);
                    borrarCriterio(id);
                    //Response.Redirect("InterfazRequerimientos.aspx");
                    break;
            }
        }

        protected void gridCriteriosAceptacion_CambioPagina(Object sender, GridViewPageEventArgs e)
        {
            this.gridCriteriosAceptacion.PageIndex = e.NewPageIndex;
            this.gridCriteriosAceptacion.DataBind();
        }

        protected void dummy(Object sender, GridViewCommandEventArgs e)
        {

        }
        protected void dummy(Object sender, GridViewPageEventArgs e)
        {
            this.gridArchivos.PageIndex = e.NewPageIndex;
            this.gridArchivos.DataBind();
        }

        protected void dummy(Object sender, GridViewDeleteEventArgs e)
        {

        }




        protected void clickBotonSubir(object sender, EventArgs e)
        {
            // When the button is clicked,
            //Button3.Text = "culi";

            /*.net is windows, windows works with new threads for forms like saveFileDialogs, need to creat a new thread then, otherwise complains about threadExeption*/
            Thread myth;
            myth = new Thread(new System.Threading.ThreadStart(showOpenDialog));//same concept as pthreads, new thread by a function call with void parameters
            myth.SetApartmentState(ApartmentState.STA);
            myth.Start();
            myth.Join();
        }

        //show dialog and push file into drive at selected path
        public void showOpenDialog(/*do not include parameters or thread creation will complain*/)
        {

            OpenFileDialog dlg = new OpenFileDialog();

            dlg.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);//defaults path at desktop 

            DialogResult dialogResult = dlg.ShowDialog();//this check is needed otherwise it always executes the savefileOnDrive retrieving an invalid result
            if (dialogResult == DialogResult.OK)
            {
                //String path = dlg.FileName;

                byte[] file;
                using (var stream = new FileStream(dlg.FileName, FileMode.Open, FileAccess.Read))
                {
                    using (var reader = new BinaryReader(stream))
                    {
                        file = reader.ReadBytes((int)stream.Length);
                    }
                }

                if (tieneArchivos == false)
                {
                    listadoArchivos.Rows.RemoveAt(0);
                    listadoArchivos.Rows.Add(dlg.SafeFileName);
                    tieneArchivos = true;
                }
                else
                {
                    listadoArchivos.Rows.Add(dlg.SafeFileName);
                }
                Object[] datos = new Object[4];
                datos[0] = proyectoConsultado.Identificador;
                if (requerimientoConsultado != null)
                    datos[1] = requerimientoConsultado.Nombre;
                else
                    datos[1] = "";
                datos[2] = dlg.SafeFileName;
                datos[3] = file;

                tablaArchivosEnMemoria.Rows.Add(datos);
                gridArchivos.DataSource = listadoArchivos;
                gridArchivos.DataBind();
            }
            else if (dialogResult == DialogResult.Cancel)
            {
            }

        }




        //show dialog and pull file into drive at selected path
        public void showFDialog(/*do not include parameters or thread creation will complain*/)
        {

            SaveFileDialog dlg = new SaveFileDialog();

            GridViewRow selectedRow = gridArchivos.Rows[selectedIndex];//get gridview row

            String fFileName = tablaArchivosEnMemoria.Rows[selectedIndex][2].ToString();//get string from row column, used as global for the new thread to be able to use it, since it cannot be sent as argument


            // Split string on . This will separate all the words in a string
            string[] words = fFileName.Split('.');

            dlg.FileName = words[0]; // Default file name showing in the dialog
            dlg.DefaultExt = words[1]; // Default file extension in the dialog

            //dlg.Filter = "Text documents (.txt)|*.txt"; // Filter files by extension 
            dlg.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);//defaults path at desktop

            Byte[] blob = (Byte[])(tablaArchivosEnMemoria.Rows[selectedIndex][3]);

            DialogResult dialogResult = dlg.ShowDialog();//this check is needed otherwise it always executes the savefileOnDrive retrieving an invalid result
            if (dialogResult == DialogResult.OK)
            {
                using (var fs = new FileStream(dlg.FileName, FileMode.Create, FileAccess.Write))
                    fs.Write(blob, 0, blob.Length);
                // controladora.saveFfileOnDrive(requerimientoConsultado.IdProyecto.ToString(), requerimientoConsultado.Nombre, fFileName, dlg.FileName);// Save document to DB
            }
            else if (dialogResult == DialogResult.Cancel)
            {
            }

        }


        int selectedIndex;//global


        //this is the method called by the field button in gridArchivos, e is the command defined in the html tag for it
        protected void gridArchivos_Seleccion(object sender, GridViewCommandEventArgs e)
        {
            if (tieneArchivos)
            {
                if (e.CommandName == "Descargar")
                {

                    // Convert the row index stored in the CommandArgument
                    // property to an Integer.
                    this.selectedIndex = Convert.ToInt32(e.CommandArgument);

                    // Get the last name of the selected author from the appropriate
                    // cell in the GridView control.


                    /*.net is windows, windows works with new threads for forms like saveFileDialogs, need to creat a new thread then, otherwise complains about threadExeption*/
                    Thread myth;
                    myth = new Thread(new System.Threading.ThreadStart(showFDialog));//same concept as pthreads, new thread by a function call with void parameters
                    myth.SetApartmentState(ApartmentState.STA);
                    myth.Start();
                }

                if (e.CommandName == "Eliminar")
                {
                    if (listadoArchivos.Rows.Count == 1)
                    {
                        Object[] datos = new Object[1];
                        listadoArchivos = tablaArchivos();
                        datos[0] = "Subir archivos";
                        listadoArchivos.Rows.Add(datos);
                        tablaArchivosEnMemoria.Rows.RemoveAt(0);
                        tieneArchivos = false;
                    }
                    else
                    {
                        int index = Convert.ToInt32(e.CommandArgument);
                        listadoArchivos.Rows.RemoveAt(index);
                        tablaArchivosEnMemoria.Rows.RemoveAt(index);
                    }

                    gridArchivos.DataSource = listadoArchivos;
                    gridArchivos.DataBind();
                }
            }
        }

        protected DataTable tablaArchivos()
        {
            DataTable tabla = new DataTable();
            DataColumn columna;

            columna = new DataColumn();
            columna.DataType = System.Type.GetType("System.String");
            columna.ColumnName = "Nombre de archivo";
            tabla.Columns.Add(columna);

            return tabla;
        }

        protected void llenaGridArchivos()
        {
            listadoArchivos = tablaArchivos();
            int indiceNuevoArchivo = -1;
            int i = 0;

            try
            {
                // Cargar archivos
                Object[] datos = new Object[1];
                //DataTable nombresArchivos = controladora.consultarArchivos(requerimientoConsultado.IdProyecto, requerimientoConsultado.Nombre);
                if (requerimientoConsultado != null)
                {
                    tablaArchivosEnMemoria = controladora.consultarArchivos(proyectoConsultado.Identificador, requerimientoConsultado.Nombre);

                    //ejemplo q recorre todo el datatable fila por fila
                    if (tablaArchivosEnMemoria.Rows.Count > 0)
                    {
                        tieneArchivos = true;
                        foreach (DataRow fila in tablaArchivosEnMemoria.Rows)
                        {
                            datos[0] = fila[2].ToString();

                            listadoArchivos.Rows.Add(datos);
                            if (proyectoConsultado != null && (fila[0].Equals(proyectoConsultado.Identificador)))
                            {
                                indiceNuevoArchivo = i;
                            }
                            i++;
                        }
                    }
                    else
                    {
                        tieneArchivos = false;
                        datos[0] = "Subir archivos";
                        listadoArchivos.Rows.Add(datos);
                    }
                }
                else
                {
                    tieneArchivos = false;
                    datos[0] = "Subir archivos";
                    listadoArchivos.Rows.Add(datos);
                    tablaArchivosEnMemoria = new DataSet1.Archivo_AsociadoDataTable();
                }

                this.gridArchivos.DataSource = listadoArchivos;
                this.gridArchivos.DataBind();
            }

            catch (Exception e)
            {
                // mostrarMensaje("warning", "Alerta", "No hay archivos asociados o no hay conexion a la base de datos.");
            }

        }

        protected void agregarArchivos()
        {/*
            if( listaPathArchivos != null )
            {
                int len = listaPathArchivos.Length;
                String[][] path = new String[len][];
                int i = 0;
                foreach (DataRow row in tablaSubirArchivos.Rows)
                {
                    String[] datos = new String[2];
                    datos[0] = row[0].ToString();
                    datos[1] = listaPathArchivos[i].ToString();
                    path[i] = datos;
                    ++i;
                }
                //controladora.insertarArchivosAsociados(proyectoConsultado.Identificador, this.textNombreRequerimiento.Text, path);
            }*/
            controladora.insertarArchivosAsociados(tablaArchivosEnMemoria);
        }

        /*
        protected void eliminarArchivosSeleccionados()
        {
            Object[] datos = new Object[1];
            DataTable porBorrar = tablaArchivos();
            int i = 0;

            foreach( GridViewRow row in gridArchivos.Rows )
            {
                System.Web.UI.WebControls.CheckBox chkRow = (row.Cells[i].FindControl("borrar") as System.Web.UI.WebControls.CheckBox);
                if( chkRow.Checked )
                {
                    datos[0] = tablaArchivosBackup.Rows[i][0].ToString();
                    porBorrar.Rows.Add(datos);
                }
                ++i;
            }
            //controladora.borrarArchivos(requerimientoConsultado.IdProyecto, requerimientoConsultado.Nombre, porBorrar);
        } */

        protected void limpiarGridArchivos()
        {
            listadoArchivos = tablaArchivos();
            gridArchivos.DataSource = listadoArchivos;
            gridArchivos.DataBind();
            tablaArchivosEnMemoria = null;
        }


        protected DataTable tablaRequerimientos()
        {
            DataTable tabla = new DataTable();
            DataColumn columna;

            //corregir
            columna = new DataColumn();
            columna.DataType = System.Type.GetType("System.String");
            columna.ColumnName = "Nombre";
            tabla.Columns.Add(columna);

            columna = new DataColumn();
            columna.DataType = System.Type.GetType("System.String");
            columna.ColumnName = "Estado";
            tabla.Columns.Add(columna);

            columna = new DataColumn();
            columna.DataType = System.Type.GetType("System.Int32");
            columna.ColumnName = "Prioridad";
            tabla.Columns.Add(columna);

            return tabla;
        }

        protected void llenarGridRequerimiento()
        {
            DataTable tabla = tablaRequerimientos();
            int indiceNuevoRequerimiento = -1;
            int i = 0;

            try
            {
                // Cargar Requerimiento
                Object[] datos = new Object[3];
                DataTable requerimiento = controladora.consultarRequerimientos(proyectoConsultado.Identificador);

                if (requerimiento.Rows.Count > 0)
                {
                    idsGrid = new Object[requerimiento.Rows.Count];
                    foreach (DataRow fila in requerimiento.Rows)
                    {
                        idsGrid[i] = fila[1].ToString();
                        datos[0] = fila[1].ToString();
                        datos[1] = fila[3].ToString();
                        datos[2] = fila[2].ToString();
                        tabla.Rows.Add(datos);
                        if (proyectoConsultado != null && (fila[0].Equals(proyectoConsultado.Identificador)))
                        {
                            indiceNuevoRequerimiento = i;
                        }
                        i++;
                    }
                }
                else
                {
                    datos[0] = "-";
                    datos[1] = "-";
                    datos[2] = "-";
                    tabla.Rows.Add(datos);
                }

                this.gridViewRequerimientos.DataSource = tabla;
                this.gridViewRequerimientos.DataBind();
                if (proyectoConsultado != null)
                {
                    GridViewRow filaSeleccionada = this.gridViewRequerimientos.Rows[indiceNuevoRequerimiento];
                }
            }

            catch (Exception e)
            {
                //    mostrarMensaje("warning", "Alerta", "No hay conexión a la base de datos.");
            }

        }

        protected DataTable tablaProyectos()
        {
            DataTable tabla = new DataTable();
            DataColumn columna;

            columna = new DataColumn();
            columna.DataType = System.Type.GetType("System.String");
            columna.ColumnName = "Nombre";
            tabla.Columns.Add(columna);

            columna = new DataColumn();
            columna.DataType = System.Type.GetType("System.String");
            columna.ColumnName = "Estado";
            tabla.Columns.Add(columna);

            columna = new DataColumn();
            columna.DataType = System.Type.GetType("System.String");
            columna.ColumnName = "Oficina Propietaria";
            tabla.Columns.Add(columna);

            return tabla;
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////here/////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


        protected void inicializarGridCriterio()
        {
            criteriosEnMemoria = tablaCriterios();
            int i = 0;
            Object[] datos = new Object[2];

            if (requerimientoConsultado == null)
            {
                idsCriterio = null;
                datos[0] = "-";
                datos[1] = "-";
                criteriosEnMemoria.Rows.Add(datos);
                this.gridCriteriosAceptacion.DataSource = criteriosEnMemoria;
                this.gridCriteriosAceptacion.DataBind();
            }
            try
            {

                DataTable criterio = controladora.consultarCriterios(proyectoConsultado.Identificador, requerimientoConsultado.Nombre);

                if (criterio.Rows.Count > 0)
                {
                    idsCriterio = new Object[criterio.Rows.Count];
                    foreach (DataRow fila in criterio.Rows)
                    {
                        idsCriterio[i] = fila[2].ToString();
                        datos[0] = fila[2].ToString();
                        datos[1] = fila[3].ToString();
                        criteriosEnMemoria.Rows.Add(datos);
                        i++;
                    }
                }
                else
                {
                    idsCriterio = null;
                    datos[0] = "-";
                    datos[1] = "-";
                    criteriosEnMemoria.Rows.Add(datos);
                }
                this.gridCriteriosAceptacion.DataSource = criteriosEnMemoria;
                this.gridCriteriosAceptacion.DataBind();
            }

            catch (Exception e)
            {
                //  mostrarMensaje("warning", "Alerta", "No hay conexión a la base de datos.");
            }

        }

        protected void insertarCriterio()
        {
            Object[] datos = new Object[2];
            datos[0] = this.textEscenario.Text.Trim();
            datos[1] = this.textCriterioAceptacion.Text.Trim();
            if (idsCriterio == null)
            {
                criteriosEnMemoria.Rows.RemoveAt(0);
            }
            criteriosEnMemoria.Rows.Add(datos);
            refrescarGridCriterios(criteriosEnMemoria);
        }

        protected void borrarCriterio(int id)
        {
            criteriosEnMemoria.Rows.RemoveAt(id);
            refrescarGridCriterios(criteriosEnMemoria);
        }

        protected String[] consolidarCriterios()
        {
            return controladora.agregarCriteriosAceptacion(proyectoConsultado.Identificador, this.textNombreRequerimiento.Text, criteriosEnMemoria);
        }

        protected void limpiarCriterios()
        {
            criteriosEnMemoria = tablaCriterios();
            this.gridCriteriosAceptacion.DataSource = criteriosEnMemoria;
            this.gridCriteriosAceptacion.DataBind();
        }

        protected DataTable tablaCriterios()
        {
            DataTable tabla = new DataTable();
            DataColumn columna;

            columna = new DataColumn();
            columna.DataType = System.Type.GetType("System.String");
            columna.ColumnName = "Escenario";
            tabla.Columns.Add(columna);

            columna = new DataColumn();
            columna.DataType = System.Type.GetType("System.String");
            columna.ColumnName = "Criterio de aceptación";
            tabla.Columns.Add(columna);

            return tabla;
        }

        protected void refrescarGridCriterios(DataTable criterio)
        {
            int i = 0;

            try
            {
                Object[] datos = new Object[2];

                if (criterio.Rows.Count > 0)
                {
                    idsCriterio = new Object[criterio.Rows.Count];
                    foreach (DataRow fila in criterio.Rows)
                    {
                        idsCriterio[i] = fila[1].ToString();
                        i++;
                    }
                }
                else
                {
                    idsCriterio = null;
                    datos[0] = "-";
                    datos[1] = "-";
                    criteriosEnMemoria.Rows.Add(datos);
                }

                this.gridCriteriosAceptacion.DataSource = criteriosEnMemoria;
                this.gridCriteriosAceptacion.DataBind();

            }

            catch (Exception e)
            {
                // mostrarMensaje("warning", "Alerta", "No hay conexión a la base de datos.");
            }

        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        protected void llenarGridProyecto()
        {
            if (((SiteMaster)Page.Master).getRol() != null)
            {
                DataTable tabla = tablaProyectos();
                int indiceNuevoProyecto = -1;
                int i = 0;

                try
                {
                    // Cargar proyectos
                    Object[] datos = new Object[3];
                    DataTable proyectos;
                    if (((SiteMaster)Page.Master).getRol().IdRol == "Miembro")
                    {
                        String cedulaME = ((SiteMaster)Page.Master).getCuenta().Cedula;
                        proyectos = controladoraP.obtenerProyectosAsociados(cedulaME);
                    }
                    else
                        proyectos = controladoraP.consultarProyectos();

                    if (proyectos.Rows.Count > 0)
                    {
                        idArray = new Object[proyectos.Rows.Count];
                        foreach (DataRow fila in proyectos.Rows)
                        {
                            idArray[i] = fila[0];
                            datos[0] = fila[1].ToString();
                            datos[1] = fila[6].ToString();
                            datos[2] = fila[7].ToString();
                            tabla.Rows.Add(datos);
                            if (proyectoConsultado != null && (fila[0].Equals(proyectoConsultado.Identificador)))
                            {
                                indiceNuevoProyecto = i;
                            }
                            i++;
                        }
                    }
                    else
                    {
                        datos[0] = "-";
                        datos[1] = "-";
                        datos[2] = "-";
                        tabla.Rows.Add(datos);
                    }

                    this.gridViewProyectos.DataSource = tabla;
                    this.gridViewProyectos.DataBind();
                    if (proyectoConsultado != null)
                    {
                        GridViewRow filaSeleccionada = this.gridViewProyectos.Rows[indiceNuevoProyecto];
                    }
                }

                catch (Exception e)
                {
                    mostrarMensaje("warning", "Alerta", "No hay conexión a la base de datos.");
                }
            }
        }

        protected void llenarListMiembros()
        {
            if (proyectoConsultado != null)
            {
                vaciarListMiembros();


                textPrimerEncargado.Items.Add(new ListItem("-", null));
                textSegundoEncargado.Items.Add(new ListItem("-", null));

                DataTable tabla = controladora.obtenerCNAMiembrosAsociados(proyectoConsultado.Identificador);

                foreach (DataRow fila in tabla.Rows)
                {
                    ListItem item1 = new ListItem(fila[1].ToString(), fila[0].ToString());
                    textPrimerEncargado.Items.Add(item1);
                    ListItem item2 = new ListItem(fila[1].ToString(), fila[0].ToString());
                    textSegundoEncargado.Items.Add(item2);

                }

            }
        }

        protected void vaciarListMiembros()
        {
            textPrimerEncargado.Items.Clear();
            textSegundoEncargado.Items.Clear();
        }

        protected void limpiarListMiembros()
        {
            textPrimerEncargado.ClearSelection();
            textSegundoEncargado.ClearSelection();

        }

        protected void gridViewProyectos_Seleccion(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Select":
                    GridViewRow filaSeleccionada = this.gridViewProyectos.Rows[Convert.ToInt32(e.CommandArgument)];
                    String id = idArray[Convert.ToInt32(e.CommandArgument) + (this.gridViewProyectos.PageIndex * resultadosPorPagina)].ToString();
                    int id2 = Convert.ToInt32(id);
                    consultarProyecto(id2);
                    Response.Redirect("InterfazRequerimientos.aspx");
                    break;
            }
        }

        protected void gridViewProyectos_CambioPagina(Object sender, GridViewPageEventArgs e)
        {
            this.gridViewProyectos.PageIndex = e.NewPageIndex;
            this.gridViewProyectos.DataBind();
        }

        protected void consultarProyecto(int id)
        {
            try
            {
                proyectoConsultado = controladoraP.consultarProyecto(id);
                modo = 1;

            }
            catch
            {
                proyectoConsultado = null;
                modo = 0;
            }
            cambiarModo();
        }

        protected void setDatosConsultadosProyecto()
        {
            this.textNombre.Text = proyectoConsultado.Nombre;
            this.textEstado.Text = proyectoConsultado.Estado;
            this.textDescripcionProyecto.Text = proyectoConsultado.Objetivo_General;
            //this.textOficinaProp.Text = proyectoConsultado.Oficina_Propietaria;
        }

        protected void setDatosConsultadosRequerimiento()
        {
            this.textNombreRequerimiento.Text = requerimientoConsultado.Nombre;
            this.textPrioridad.Text = requerimientoConsultado.Prioridad.ToString();
            this.textEstadoRequerimiento.SelectedValue = requerimientoConsultado.Estado;
            this.textRol.Text = requerimientoConsultado.Rol;
            this.textContenido.Text = requerimientoConsultado.Contenido;
            this.textRazon.Text = requerimientoConsultado.Razon;
            this.textEstimacion.Text = requerimientoConsultado.Estimacion.ToString();
            cargarIteraciones();
            this.ListaIteracion.SelectedValue = requerimientoConsultado.Sprint.ToString();
            cargarModulos();
            this.ListaModulo.SelectedValue = requerimientoConsultado.Modulo;

            if (textPrimerEncargado.Items.FindByValue(requerimientoConsultado.IdEncargado1) != null)
            {
                textPrimerEncargado.ClearSelection();
                textPrimerEncargado.Items.FindByValue(requerimientoConsultado.IdEncargado1).Selected = true;
            }

            if (textSegundoEncargado.Items.FindByValue(requerimientoConsultado.IdEncargado2) != null)
            {
                textSegundoEncargado.ClearSelection();
                textSegundoEncargado.Items.FindByValue(requerimientoConsultado.IdEncargado2).Selected = true;
            }


            if (requerimientoConsultado.Funcional)
            {
                this.radioButtonList1.SelectedValue = "Funcional";
            }

            else
            {
                this.radioButtonList1.SelectedValue = "No Funcional";
            }

        }

        protected void radioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void textPrimerEncargado_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (String.Equals(textPrimerEncargado.SelectedItem.Value, '-'))
            {
                if (pEncargado != null)
                {
                    ((ListItem)pEncargado).Selected = false;
                    textSegundoEncargado.Items.Add((ListItem)pEncargado);
                    pEncargado = null;
                }
            }
            else
            {
                if (pEncargado != null)
                {
                    ((ListItem)pEncargado).Selected = false;
                    textSegundoEncargado.Items.Add((ListItem)pEncargado);
                }
                pEncargado = textPrimerEncargado.SelectedItem;
                textSegundoEncargado.Items.Remove((ListItem)pEncargado);
            }
        }

        protected void textSegundoEncargado_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (String.Equals(textSegundoEncargado.SelectedItem.Value, '-'))
            {
                if (sEncargado != null)
                {
                    ((ListItem)sEncargado).Selected = false;
                    textPrimerEncargado.Items.Add((ListItem)sEncargado);
                    sEncargado = null;
                }
            }
            else
            {
                if (sEncargado != null)
                {
                    ((ListItem)sEncargado).Selected = false;
                    textPrimerEncargado.Items.Add((ListItem)sEncargado);
                }
                sEncargado = textSegundoEncargado.SelectedItem;
                textPrimerEncargado.Items.Remove((ListItem)sEncargado);
            }

        }

        protected void cargarIteraciones()
        {
            ListaIteracion.Items.Clear();
            ListaIteracion.Items.Add(new ListItem("", null));
            DataTable iteraciones = controladora.consultarIteraciones(proyectoConsultado.Identificador);
            foreach (DataRow fila in iteraciones.Rows)
            {
                ListaIteracion.Items.Add(new ListItem(fila[1].ToString(), fila[1].ToString()));
            }

            botonAgregarIteracion.Disabled = false;
            botonEliminarIteracion.Disabled = false;


            botonAgregarModulo.Disabled = false;
            botonEliminarModulo.Disabled = false;

        }

        protected void cargarModulos()
        {
            ListaModulo.Items.Clear();
            ListaModulo.Items.Add(new ListItem("", null));
            if (ListaIteracion.SelectedValue != "")
            {
                DataTable modulos = controladora.consultarModulos(proyectoConsultado.Identificador, Convert.ToInt32(ListaIteracion.SelectedValue));
                foreach (DataRow fila in modulos.Rows)
                {
                    ListaModulo.Items.Add(new ListItem(fila[2].ToString(), fila[2].ToString()));
                }
            }

            botonAgregarIteracion.Disabled = false;
            botonEliminarIteracion.Disabled = false;


            botonAgregarModulo.Disabled = false;
            botonEliminarModulo.Disabled = false;
        }



        protected void clickAgregarModulo(object sender, EventArgs e)
        {
            String nuevoModulo = textAgregarModulo.Text;
            textAgregarModulo.Text = "";
            bool resultado = controladora.insertarModulo(proyectoConsultado.Identificador, Convert.ToInt32(ListaIteracion.SelectedValue), nuevoModulo);
            cargarModulos();

        }

        protected void clickAgregarIteracion(object sender, EventArgs e)
        {
            bool resultado = controladora.insertarIteracion(proyectoConsultado.Identificador);
            cargarIteraciones();

        }
        protected void clickEliminarIteracion(object sender, EventArgs e)
        {
            int iteracionPorEliminar = Convert.ToInt32(ListaIteracion.SelectedValue);
            bool resultado = controladora.eliminarIteracion(proyectoConsultado.Identificador, iteracionPorEliminar);
            cargarIteraciones();
        }

        protected void clickEliminarModulo(object sender, EventArgs e)
        {
            String moduloPorEliminar = ListaModulo.SelectedValue;
            bool resultado = controladora.eliminarModulo(proyectoConsultado.Identificador, Convert.ToInt32(ListaIteracion.SelectedValue), moduloPorEliminar);
            cargarModulos();
        }

        protected void ListaIteracion_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargarModulos();
        }

        protected void ListaModulo_SelectedIndexChanged(object sender, EventArgs e)
        {
            botonAgregarIteracion.Disabled = false;
            botonEliminarIteracion.Disabled = false;


            botonAgregarModulo.Disabled = false;
            botonEliminarModulo.Disabled = false;
        }

        protected void gridArchivos_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}