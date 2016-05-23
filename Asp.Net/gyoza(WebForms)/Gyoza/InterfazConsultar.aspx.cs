using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.IO;
using System.Web.UI.WebControls;
using System.Web.ApplicationServices;
using iTextSharp.text;
using iTextSharp.text.pdf;
using ExcelLibrary.CompoundDocumentFormat;
using ExcelLibrary.SpreadSheet;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using Gyoza.App_Code.Modulo_Consulta;
using Gyoza.Modulo_Proyecto;
using Gyoza.Modulo_Requerimientos;
using Gyoza.Modulo_Cuenta;

namespace Gyoza
{
    public partial class InterfazConsultar : System.Web.UI.Page
    {

        private static String reporteGenerado = "";
        private static ControladoraConsultas controladoraConsultas;
        private static ControladoraProyecto controladoraProyecto;
        private static ControladoraRequerimientos controladoraRequerimientos;
        private static EntidadProyecto entidadProyecto;
        private static EntidadRequerimiento entidadRequerimiento;
        private static DataTable HistoriaUsuario;
        private static String path;
        //:3
        private static Object[] idArray;
        private static int resultadosPorPagina;


        protected void Page_Load(object sender, EventArgs e)
        {
            controladoraConsultas = new ControladoraConsultas();
            controladoraProyecto = new ControladoraProyecto();
            controladoraRequerimientos = new ControladoraRequerimientos(); 
        ((SiteMaster)Page.Master).markModule("consultar");
             resultadosPorPagina = gridViewProyecto.PageSize;
             permisosProyecto_SelectedIndexChanged();
             llenarGrid();
             this.textObjetivo.Enabled = false;
             this.textNombre.Enabled = false;
             this.textEstado.Enabled = false;
             this.textDescripcionProyecto.Enabled = false;
             this.textObjetivo.Enabled = false;
             this.textObjetivo.Enabled = false;
             if (!IsPostBack)
             {
                 limpiarDatosProyecto();
                 if (((SiteMaster)Page.Master).getRol() == null)
                 {
                     this.botonConsultar.Disabled = true;
                     this.permisosProyecto.Enabled = false;
                     this.BotonGenerarInforme.Disabled = true;
                     this.BotonDescargarArchivo.Disabled = true;
                     this.ListaIteracion.Enabled = false;
                     this.ListaModulo.Enabled = false;
                     this.ListaRequerimientos.Enabled = false;
                     this.botonDescargarAbajo.Disabled = true;
                     this.botonAyuda.Disabled = true;
                 }
                 else
                 {
                     this.botonConsultar.Disabled = false;
                     this.permisosProyecto.Enabled = false;
                     this.BotonGenerarInforme.Disabled = true;
                     this.BotonDescargarArchivo.Disabled = true;
                     this.ListaIteracion.Enabled = true;
                     this.ListaModulo.Enabled = true;
                     this.ListaRequerimientos.Enabled = true;
                     this.botonDescargarAbajo.Disabled = true;
                     this.botonAyuda.Disabled = false;
                 }
                 if (entidadProyecto != null)
                 {
                     this.botonConsultar.Disabled = false;
                     this.permisosProyecto.Enabled = true;
                     this.BotonGenerarInforme.Disabled = false;
                     this.BotonDescargarArchivo.Disabled = false;
                     this.botonDescargarAbajo.Disabled = false;
                     cargarDatosProyecto();
                     cargarIteraciones();
                 }
             }

            
            /*var doc1 = new Document();

            string path = Server.MapPath("PDFs");
            PdfWriter.GetInstance(doc1, new FileStream(path + "/Doc1.pdf", FileMode.Create));

            doc1.Open();
            doc1.Add(new Paragraph("Sistema de Administración de Requerimientos - Gyoza"));
            doc1.Add(new Paragraph("Reporte generado - " + DateTime.Now.ToString() + "\n\n"));
            doc1.Close();
 
            string file = "C:\\Users\\b25201\\Source\\Repos\\gyoza\\Gyoza\\XLSXs\\Test.xlsx";
            Workbook wb = new Workbook();
            Worksheet ws = new Worksheet("Primera hoja");
            Random n = new Random();
            for(int i=0;i<10;i++){
                for(int j=0;j<10;j++){
                    ws.Cells[i, j] = new ExcelLibrary.SpreadSheet.Cell(n.Next(1, 101));
                }
            }
            wb.Worksheets.Add(ws);
            wb.Save(file);*/


        }

        protected void habilitarCampos() {
            botonConsultar.Disabled = false;
        }
        protected void gridViewProyecto_Seleccion(object sender, GridViewCommandEventArgs e)
            {
            switch (e.CommandName)
            {
                case "Select":
                    GridViewRow filaSeleccionada = this.gridViewProyecto.Rows[Convert.ToInt32(e.CommandArgument)];
                    int id = Convert.ToInt32(idArray[Convert.ToInt32(e.CommandArgument) + (this.gridViewProyecto.PageIndex * resultadosPorPagina)]);
                    consultarProyecto(id);
                    Response.Redirect("InterfazConsultar.aspx");
                    break;
            }
            }

        protected void cargarDatosProyecto() 
        {
            textNombre.Text = entidadProyecto.Nombre;
            textEstado.Text = entidadProyecto.Estado;
            textDescripcionProyecto.Text = entidadProyecto.Objetivo_General;

        }

        protected void limpiarDatosProyecto()
        {
            textNombre.Text = "";
            textEstado.Text = "";
            textDescripcionProyecto.Text = "";

        }

        protected void consultarProyecto(int id)
        {
            entidadProyecto = controladoraProyecto.consultarProyecto(id);
        }

        
        protected void gridViewProyecto_CambioPagina(Object sender, GridViewPageEventArgs e)
        {
            this.gridViewProyecto.PageIndex = e.NewPageIndex;
            this.gridViewProyecto.DataBind();
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
        protected void llenarGrid()
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
                        proyectos = controladoraProyecto.obtenerProyectosAsociados(cedulaME);
                    }

                    else
                        proyectos = controladoraProyecto.consultarProyectos();

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
                            if (entidadProyecto != null && (fila[0].Equals(entidadProyecto.Identificador)))
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

                    this.gridViewProyecto.DataSource = tabla;
                    this.gridViewProyecto.DataBind();
                    if (entidadProyecto != null)
                    {
                        GridViewRow filaSeleccionada = this.gridViewProyecto.Rows[indiceNuevoProyecto];
                    }
                }

                catch (Exception e)
                {
                    mostrarMensaje("warning", "Alerta", "No hay conexión a la base de datos.");
                }
            }
        }

        protected void mostrarMensaje(String tipoAlerta, String alerta, String mensaje)
        {
            Alerta.Attributes["class"] = "alert alert-" + tipoAlerta + " alert-dismissable fade in";
            labelTipoAlerta.Text = alerta + " ";
            labelAlerta.Text = mensaje;
            Alerta.Attributes.Remove("hidden");
        }

        public void clickGenerarInforme(object sender, EventArgs e)
        {
            reporteGenerado = "";
            if (permisosProyecto.Items.FindByText("Datos del Proyecto").Selected)
            {
                agregarDatosDelProyecto();
            }

            if (permisosProyecto.Items.FindByText("Historias de Usuario").Selected)
            {
                agregarHistoriasDeUsuario();
            }

            if (permisosProyecto.Items.FindByText("Jerarquía de iteraciones y módulos").Selected)
            {
                agregarJerarquia();
            }

            if (permisosProyecto.Items.FindByText("Requerimiento Particular").Selected)
            {
                agregarDatosRequerimiento();
            }

            textObjetivo.Text = reporteGenerado;
        }

        private void agregarDatosRequerimiento()
        {

            reporteGenerado += "------------------------------------------------------------------------------------\n";
            reporteGenerado += "Datos del requerimiento consultado\n";
            reporteGenerado += "Nombre del requerimiento: " + entidadRequerimiento.Nombre + "\n";
            reporteGenerado += "Prioridad: " + entidadRequerimiento.Prioridad + "\n";
            reporteGenerado += "Estado: " + entidadRequerimiento.Estado + "\n";
            reporteGenerado += "Rol: " + entidadRequerimiento.Rol + "\n";
            reporteGenerado += "Contenido: " + entidadRequerimiento.Contenido + "\n";
            reporteGenerado += "Razón: " + entidadRequerimiento.Razon + "\n";
            reporteGenerado += "Estimación: " + entidadRequerimiento.Estimacion + "\n";
            reporteGenerado += "Sprint: " + entidadRequerimiento.Sprint + "\n";
            reporteGenerado += "Módulo: " + entidadRequerimiento.Modulo + "\n";
            if (entidadRequerimiento.Funcional == true)
            {
                reporteGenerado += "Requerimiento funcional\n";
            }
            else
            {
                reporteGenerado += "Requerimiento no funcional\n";
            }
            reporteGenerado += "------------------------------------------------------------------------------------\n";



        }

        private void agregarJerarquia()
        {
            reporteGenerado += "------------------------------------------------------------------------------------\n";
            reporteGenerado += "Jerarquía del proyecto\n";
            
            string iteracion = "";
            try
            {
                DataTable jerarquia = controladoraProyecto.obtenerJerarquia(entidadProyecto.Identificador);
                if (jerarquia != null)
                {

                    foreach (DataRow fila in jerarquia.Rows)
                    {
                        if (iteracion != fila[1].ToString())
                        {
                            iteracion = fila[1].ToString();
                            reporteGenerado += "Iteración número " + iteracion +"\n";
                        }
                        reporteGenerado += "      Módulo: " + fila[2].ToString() +"\n";
                    }
                }
            }
            catch (Exception e)
            {
            }
            reporteGenerado += "------------------------------------------------------------------------------------\n";

        }

        private void agregarHistoriasDeUsuario()
        {
            reporteGenerado += "------------------------------------------------------------------------------------\n";
            reporteGenerado += "Requerimientos del proyecto en estilo de historias de usuario\n";
            
            try{
                if(entidadProyecto != null){
                    HistoriaUsuario = controladoraRequerimientos.consultarRequerimientos(entidadProyecto.Identificador);
                    foreach(DataRow fila in HistoriaUsuario.Rows){
                        reporteGenerado +=  "Como un " + fila[4].ToString() + " quiero " + fila[5].ToString() + " para poder " + fila[6].ToString()+"\n";
                    }
                }
            }
            catch(Exception e){
            }
            reporteGenerado += "------------------------------------------------------------------------------------\n";
            

        }
        private void agregarDatosDelProyecto()

        {
            reporteGenerado += "------------------------------------------------------------------------------------\n";
            reporteGenerado += "Datos del proyecto\n";
            reporteGenerado += "Nombre: " + entidadProyecto.Nombre+  "\n";
            reporteGenerado += "Objetivo general: " + entidadProyecto.Objetivo_General + "\n\n";
            reporteGenerado += "Fecha de asignación: " + entidadProyecto.Fecha_Asignacion + "\n";
            reporteGenerado += "Fecha de inicio: "+  entidadProyecto.Fecha_Inicio + "\n";
            reporteGenerado += "Fecha de finalización: " + entidadProyecto.Fecha_Finalizacion + "\n\n";
            reporteGenerado += "Estado: "+  entidadProyecto.Estado + "\n\n";
            reporteGenerado += "Oficina propietaria: " + entidadProyecto.Oficina_Propietaria + "\n";
            reporteGenerado += "Teléfono de oficina: "+  entidadProyecto.Telefono_Oficina+  "\n\n";
            reporteGenerado += "Representante: "+  entidadProyecto.Representante_Usuario + "\n";
            reporteGenerado += "Correo de contacto: "  +entidadProyecto.Correo_Representante+  "\n";
            reporteGenerado += "Celular de contacto: " + entidadProyecto.Celular_Representante + "\n";
            reporteGenerado += "------------------------------------------------------------------------------------\n";
        }

        protected void ListaIteracion_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargarModulos();
        }

        protected void ListaModulo_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargarRequerimientos();
        }

        protected void ListaRequerimientos_SelectedIndexChanged(object sender, EventArgs e)
        {
            entidadRequerimiento = controladoraRequerimientos.consultarRequerimiento(entidadProyecto.Identificador, ListaRequerimientos.SelectedValue);
        }

        protected void cargarIteraciones()
        {
            ListaIteracion.Items.Clear();
            ListaIteracion.Items.Add(new System.Web.UI.WebControls.ListItem("", null));
            DataTable iteraciones = controladoraRequerimientos.consultarIteraciones(entidadProyecto.Identificador);
            foreach (DataRow fila in iteraciones.Rows)
            {
                ListaIteracion.Items.Add(new System.Web.UI.WebControls.ListItem(fila[1].ToString(), fila[1].ToString()));
            }


        }

        protected void cargarModulos()
        {
            ListaModulo.Items.Clear();
            ListaModulo.Items.Add(new System.Web.UI.WebControls.ListItem("", null));
            if (ListaIteracion.SelectedValue != "")
            {
                DataTable modulos = controladoraRequerimientos.consultarModulos(entidadProyecto.Identificador, Convert.ToInt32(ListaIteracion.SelectedValue));
                foreach (DataRow fila in modulos.Rows)
                {
                    ListaModulo.Items.Add(new System.Web.UI.WebControls.ListItem(fila[2].ToString(), fila[2].ToString()));
                }
            }

        }

        protected void cargarRequerimientos()
        {
            ListaRequerimientos.Items.Clear();
            ListaRequerimientos.Items.Add(new System.Web.UI.WebControls.ListItem("", null));
            if (ListaModulo.SelectedValue != "")
            {
                DataTable requerimientos = controladoraRequerimientos.consultarRequerimientoParticular(ListaModulo.SelectedValue, Convert.ToInt32(ListaIteracion.SelectedValue), Convert.ToInt32(entidadProyecto.Identificador));
                foreach (DataRow fila in requerimientos.Rows)
                {
                    ListaRequerimientos.Items.Add(new System.Web.UI.WebControls.ListItem(fila[1].ToString(), fila[1].ToString()));
                }
            }
        }


        protected void permisosProyecto_SelectedIndexChanged()
        {
            if (permisosProyecto.Items.FindByText("Requerimiento Particular").Selected)
            {
                comboboxes.Visible = true;

            }
            else
            {
                comboboxes.Visible = false;
            }
        }

        protected void crearPdf()
        {
            var doc1 = new Document();

            //string path = Server.MapPath("PDFs");
            try
            {
                PdfWriter.GetInstance(doc1, new FileStream(path + ".pdf", FileMode.Create));

                doc1.Open();
                doc1.Add(new Paragraph("Sistema de Administración de Requerimientos - Gyoza"));
                doc1.Add(new Paragraph("Reporte generado - " + DateTime.Now.ToString() + "\n\n\n"));
                doc1.Add(new Paragraph(textObjetivo.Text));
                doc1.Close();
            }
            catch (Exception e) { }
        }
        protected void clickBotonBajar(object sender, EventArgs e)
        {
            // When the button is clicked,
            //Button3.Text = "culi";

            /*.net is windows, windows works with new threads for forms like saveFileDialogs, need to creat a new thread then, otherwise complains about threadExeption*/
            if (textObjetivo.Text != "") { 
                Thread myth;
                myth = new Thread(new System.Threading.ThreadStart(showOpenDialog));//same concept as pthreads, new thread by a function call with void parameters
                myth.SetApartmentState(ApartmentState.STA);
                myth.Start();
                myth.Join();
                crearPdf();
                mostrarMensaje("success", "El archivo se ha creado satisfactoriamente.", "Exito");
        }
        }

        //show dialog and push file into drive at selected path
        public void showOpenDialog(/*do not include parameters or thread creation will complain*/)
        {

            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "archivos pdf (*.pdf)|";
            dlg.FilterIndex = 0;
            dlg.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);//defaults path at desktop 

            DialogResult dialogResult = dlg.ShowDialog();//this check is needed otherwise it always executes the savefileOnDrive retrieving an invalid result
            if (dialogResult == DialogResult.OK)
            {
                path = dlg.FileName;

            }
            else if (dialogResult == DialogResult.Cancel)
            {
            }

        }




        //show dialog and pull file into drive at selected path

    }
          
}