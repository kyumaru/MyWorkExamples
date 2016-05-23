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

namespace Gyoza
{
    public partial class InterfazProyecto : System.Web.UI.Page
    {
        private static EntidadProyecto proyectoConsultado;
        private static ControladoraProyecto controladora;
        private static ControladoraCuentas controladoraC;
        private static ControladoraRequerimientos controladoraRequerimientos;
        private static bool seConsulto = false;
        private static String permisos = "11111";
        private static Object[] idArray;

        private static Object[] asociadosArray;
        private static int aAPos = 0;
        private static DataTable tablaAsociados;
        private static Object[] desasociadosArray;
        private static int dAPos = 0;
        private static DataTable tablaDesasociados;

        private static int resultadosPorPagina;
        private static int miembrosPorPagina;

        /* Modo de la interfaz
         * 0 = defecto: Se permite insertar y consultar
         * 1 = insertar: Se permite modificar texto, consultar, aceptar y cancelar
         * 2 = modificar: Se permite modificar texto, consultar, aceptar y cancelar
         * 3 = eliminar: Es un modo temporal
         * 4 = consultar: Permite insertar, modificar, eliminar y consultar
         */
        private static int modo = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            controladora = new ControladoraProyecto();
            controladoraC = new ControladoraCuentas();
            controladoraRequerimientos = new ControladoraRequerimientos();

            // No sirve agregarlo en el aspx
            this.textObjetivo.Attributes.Add("maxlength", "120");

            resultadosPorPagina = gridViewProyecto.PageSize;
            miembrosPorPagina = gridMiembrosAsociados.PageSize;
            llenarGrid();

            ((SiteMaster)Page.Master).markModule("proyectos");//con esto se setea el tab en el site master puede q haga falta cambiarlo de lugar


            if (((SiteMaster)Page.Master).getRol() != null)
            {
                this.botonAyuda.Disabled = false;
                permisos = ((SiteMaster)Page.Master).getRol().PermisosProyecto;
            }
            else
            {
                this.botonAyuda.Disabled = true;
                permisos = "00000";
            }

            if (!IsPostBack)
            {
                if (!seConsulto)
                    modo = 0;
                else
                {
                    if (proyectoConsultado == null)
                        mostrarMensaje("warning", "Alerta: ", "No se pudo consultar el proyecto.");
                    else
                        setDatosConsultados();
                        llenarGridMiembros();
                        cargarIteraciones();
                        cargarModulos();

                        botonAgregarIteracion.Disabled = false;
                        botonEliminarIteracion.Disabled = false;


                        botonAgregarModulo.Disabled = false;
                        botonEliminarModulo.Disabled = false;

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
                    botonRequerimiento.Disabled = true;
                    limpiarCampos();
                    habilitarCampos(false);
                    break;
                case 1:
                    botonAceptar.Disabled = false;
                    botonCancelar.Disabled = false;
                    botonInsertar.Disabled = true;
                    botonModificar.Disabled = true;
                    botonEliminar.Disabled = true;
                    botonRequerimiento.Disabled = true;
                    habilitarCampos(true);
                    this.ListaIteracion.Enabled = false;
                    this.ListaModulo.Enabled = false;
                    this.botonAgregarModulo.Disabled = true;
                    this.botonAgregarIteracion.Disabled = true;
                    this.botonEliminarIteracion.Disabled = true;
                    this.botonEliminarModulo.Disabled = true;
                    break;
                case 2:
                    botonAceptar.Disabled = false;
                    botonCancelar.Disabled = false;
                    botonInsertar.Disabled = true;
                    botonModificar.Disabled = true;
                    botonEliminar.Disabled = true;
                    botonRequerimiento.Disabled = true;
                    habilitarCampos(true);

                    break;
                case 3:
                case 4:
                    botonAceptar.Disabled = true;
                    botonCancelar.Disabled = false;
                    botonInsertar.Disabled = true;
                    botonModificar.Disabled = false;
                    botonEliminar.Disabled = false;
                    botonRequerimiento.Disabled = false;
                    habilitarCampos(false);
                    break;

                default:
                    // Algo salio mal
                    break;
            }
            aplicarPermisos();
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
            }
            // La posicion 4 representa la asociacion de miembros de equipo

            if (permisos[4] == '0')
            {
                this.gridMiembrosAsociados.Enabled = false;
                this.gridMiembrosNoAsociados.Enabled = false;
            }
        }

        protected void habilitarCampos(bool habilitar)
        {
            this.textNombre.Enabled = habilitar;
            this.textEstado.Enabled = habilitar;
            this.textObjetivo.Enabled = habilitar;
            this.fechaAsignacion.Enabled = habilitar;
            this.fechaInicio.Enabled = habilitar;
            this.fechaFinalizacion.Enabled = habilitar;
            this.textOficina.Enabled = habilitar;
            this.textTelefono.Enabled = habilitar;
            this.textRepresentante.Enabled = habilitar;
            this.textCorreo.Enabled = habilitar;
            this.textCelular.Enabled = habilitar;
            this.gridMiembrosNoAsociados.Enabled = habilitar;
            this.gridMiembrosAsociados.Enabled = habilitar;
            this.calendario1.Enabled = habilitar;
            this.calendario2.Enabled = habilitar;
            this.calendario3.Enabled = habilitar;
            this.botonAgregarModulo.Disabled = !habilitar;
            this.botonAgregarIteracion.Disabled = !habilitar;
            this.botonEliminarIteracion.Disabled = !habilitar;
            this.botonEliminarModulo.Disabled = !habilitar;
        }

        protected void limpiarCampos()
        {
            this.textNombre.Text = "";
            this.textEstado.SelectedValue = null;
            this.textObjetivo.Text = "";
            this.fechaAsignacion.SelectedDate = this.fechaAsignacion.VisibleDate = this.fechaAsignacion.TodaysDate;
            this.fechaInicio.SelectedDate = this.fechaInicio.VisibleDate = this.fechaInicio.TodaysDate;
            this.fechaFinalizacion.SelectedDate = this.fechaFinalizacion.VisibleDate = this.fechaFinalizacion.TodaysDate;
            this.fechaAsignacion.SelectedDate = this.fechaAsignacion.SelectedDate = new DateTime(1994, 11, 30);
            this.fechaInicio.SelectedDate = this.fechaInicio.SelectedDate = new DateTime(1994, 11, 30);
            this.fechaFinalizacion.SelectedDate = this.fechaFinalizacion.SelectedDate = new DateTime(1994, 11, 30);
            this.textOficina.Text = "";
            this.textTelefono.Text = "";
            this.textRepresentante.Text = "";
            this.textCorreo.Text = "";
            this.textCelular.Text = "";
            this.Label16.Text = "";
            this.Label17.Text = "";
            this.Label18.Text = "";
            this.fechaAsignacion.Visible = false;
            this.fechaInicio.Visible = false;
            this.fechaFinalizacion.Visible = false;
        }

        protected Object[] obtenerDatosProyecto()
        {
            Object[] datos = new Object[12];
            datos[0] = 0;
            datos[1] = this.textNombre.Text.Trim();
            datos[2] = this.textObjetivo.Text.Trim();
            datos[3] = this.fechaAsignacion.SelectedDate;
            datos[4] = this.fechaInicio.SelectedDate;
            datos[5] = this.fechaFinalizacion.SelectedDate;
            datos[6] = this.textEstado.SelectedValue;
            datos[7] = this.textOficina.Text.Trim();
            datos[8] = this.textTelefono.Text.Trim();
            datos[9] = this.textRepresentante.Text.Trim();
            datos[10] = this.textCelular.Text.Trim();
            datos[11] = this.textCorreo.Text.Trim();
            return datos;
        }

        protected void setDatosConsultados()
        {
            this.textNombre.Text = proyectoConsultado.Nombre;
            this.textEstado.SelectedValue = proyectoConsultado.Estado;
            this.textObjetivo.Text = proyectoConsultado.Objetivo_General;

            this.fechaAsignacion.SelectedDate = proyectoConsultado.Fecha_Asignacion;
            this.fechaAsignacion.VisibleDate = proyectoConsultado.Fecha_Asignacion;
            Label16.Text = "" + fechaAsignacion.SelectedDate.ToShortDateString();

            this.fechaInicio.SelectedDate = proyectoConsultado.Fecha_Inicio;
            this.fechaInicio.VisibleDate = proyectoConsultado.Fecha_Inicio;
            Label17.Text = "" + fechaInicio.SelectedDate.ToShortDateString();

            this.fechaFinalizacion.SelectedDate = proyectoConsultado.Fecha_Finalizacion;
            this.fechaFinalizacion.VisibleDate = proyectoConsultado.Fecha_Finalizacion;
            Label18.Text = "" + fechaFinalizacion.SelectedDate.ToShortDateString();

            this.textOficina.Text = proyectoConsultado.Oficina_Propietaria;
            this.textTelefono.Text = proyectoConsultado.Telefono_Oficina;
            this.textRepresentante.Text = proyectoConsultado.Representante_Usuario;
            this.textCorreo.Text = proyectoConsultado.Correo_Representante;
            this.textCelular.Text = proyectoConsultado.Celular_Representante;
        }

        protected int insertar()
        {
            int id = 0;
            Object[] proveedor = obtenerDatosProyecto();

            String[] error = controladora.insertarDatos(proveedor);
            // Asociar datagrid

            id = Convert.ToInt32(error[3]);
            mostrarMensaje(error[0], error[1], error[2]);
            if (error[0].Contains("success"))
            {
                llenarGrid();
                Object[][] a = new Object[aAPos][];
                for( int i = 0; i < aAPos; ++i )
                {
                    Object[] b = new Object[2];
                    b[0] = asociadosArray[i];
                    b[1] = ((RadioButton)(gridMiembrosAsociados.Rows[i].FindControl("RadioButton1"))).Checked == true ? "Lider" : "Integrante";
                    a[i] = b;
                }   
                String[] error1 = controladora.asociarMiembroEquipo( id, a );
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

            Object[] proveedor = obtenerDatosProyecto();
            int id = proyectoConsultado.Identificador;
            proveedor[0] = id;
            String[] error = controladora.modificarDatos(proyectoConsultado, proveedor);
            mostrarMensaje(error[0], error[1], error[2]);

            if (error[0].Contains("success"))// si fue exitoso
            {
                llenarGrid();
                proyectoConsultado = controladora.consultarProyecto(proyectoConsultado.Identificador);
                modo = 4;

                Object[][] a = new Object[aAPos][];
                for (int i = 0; i < aAPos; ++i)
                {
                    Object[] b = new Object[2];
                    b[0] = asociadosArray[i];
                    b[1] = ((RadioButton)(gridMiembrosAsociados.Rows[i].FindControl("RadioButton1"))).Checked == true ? "Lider" : "Integrante";
                    a[i] = b;
                }  
                String[] error1 = controladora.asociarMiembroEquipo(id, a);

                a = new Object[dAPos][];
                for (int i = 0; i < dAPos; ++i)
                {
                    Object[] b = new Object[2];
                    b[0] = desasociadosArray[i];
                    b[1] ="";
                    a[i] = b;
                }  
                String[] error2 = controladora.desasociarMiembroEquipo(id, a);
            }
            else
            {
                res = false;
                modo = 2;
            }


            return res;
        }

        protected void clickInsertar(object sender, EventArgs e)
        {
            modo = 1;
            cambiarModo();
            llenarGridMiembros();
        }

        protected void clickModificar(object sender, EventArgs e)
        {
            modo = 2;
            cambiarModo();
        }

        protected void clickAceptarEliminar(object sender, EventArgs e)
        {
            String[] error = controladora.eliminarProyecto(proyectoConsultado);
            mostrarMensaje(error[0], error[1], error[2]);

            // Eliminar reinicia la interfaz
            if (error[0].Contains("success"))
            {
                modo = 0;
                proyectoConsultado = null;
                llenarGrid();
                limpiarCampos();
                vaciarGridMiembros();
                cambiarModo();
            }
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
                    proyectoConsultado = controladora.consultarProyecto(idInsertado);
                    modo = 4;
                    habilitarCampos(false);
                }
                else
                    operacionCorrecta = false;
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
            vaciarGridMiembros();
            proyectoConsultado = null;
        }

        protected void consultarProyecto(int id)
        {
            seConsulto = true;
            try
            {
                proyectoConsultado = controladora.consultarProyecto(id);
                modo = 4;
            }
            catch
            {
                proyectoConsultado = null;
                modo = 0;
            }
            cambiarModo();
        }

        protected void gridViewProyecto_Seleccion(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Select":
                    GridViewRow filaSeleccionada = this.gridViewProyecto.Rows[Convert.ToInt32(e.CommandArgument)];
                    int id = Convert.ToInt32(idArray[Convert.ToInt32(e.CommandArgument) + (this.gridViewProyecto.PageIndex * resultadosPorPagina)]);
                    consultarProyecto(id);
                    Response.Redirect("InterfazProyecto.aspx");
                    break;
            }
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

        protected DataTable tablaCuentas()
        {
            DataTable tabla = new DataTable();
            DataColumn columna;

            columna = new DataColumn();
            columna.DataType = System.Type.GetType("System.String");
            columna.ColumnName = "Nombre";
            tabla.Columns.Add(columna);

            columna = new DataColumn();
            columna.DataType = System.Type.GetType("System.String");
            columna.ColumnName = "Apellido";
            tabla.Columns.Add(columna);

            columna = new DataColumn();
            columna.DataType = System.Type.GetType("System.String");
            columna.ColumnName = "E-Mail";
            tabla.Columns.Add(columna);

            return tabla;
        }

        protected void llenarGrid()
        {
            if( ((SiteMaster)Page.Master).getRol() != null )
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
                        proyectos = controladora.obtenerProyectosAsociados(cedulaME);
                    }

                    else
                        proyectos = controladora.consultarProyectos();

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

                    this.gridViewProyecto.DataSource = tabla;
                    this.gridViewProyecto.DataBind();
                    if (proyectoConsultado != null)
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


        protected void llenarGridMiembros()
        {
            tablaAsociados = tablaCuentas();
            tablaDesasociados = tablaCuentas();//aqui se hacen las columnas
            dAPos = 0;
            aAPos = 0;

            try
            {
                // Cargar proyectos
                Object[] datos = new Object[3];

                // Cargar usuarios
                if (proyectoConsultado == null)
                {
                    // Solo hay desasociados
                    DataTable cuentas = controladoraC.consultarCuenta();
                    if (cuentas.Rows.Count > 0)
                    {
                        desasociadosArray = new Object[cuentas.Rows.Count];
                        foreach (DataRow fila in cuentas.Rows)
                        {
                            desasociadosArray[dAPos] = fila[0];
                            datos[0] = fila[1].ToString();
                            datos[1] = fila[2].ToString();
                            datos[2] = fila[3].ToString();
                            tablaDesasociados.Rows.Add(datos);
                            dAPos++;
                        }
                    }
                    else
                    {
                        desasociadosArray = new Object[1];
                        datos[0] = "-";
                        datos[1] = "-";
                        datos[2] = "-";
                        tablaDesasociados.Rows.Add(datos);
                    }

                    asociadosArray = new Object[1];
                    datos[0] = "-";
                    datos[1] = "-";
                    datos[2] = "-";
                    tablaAsociados.Rows.Add(datos);

                    this.gridMiembrosNoAsociados.DataSource = tablaDesasociados;
                    this.gridMiembrosNoAsociados.DataBind();
                    this.gridMiembrosAsociados.DataSource = tablaAsociados;
                    this.gridMiembrosAsociados.DataBind();
                }
                else
                {
                    // Asociados y Desasociados

                    // Desasociados
                    DataTable cuentas = controladoraC.obtenerMiembrosDesasociados(proyectoConsultado.Identificador);
                    if (cuentas.Rows.Count > 0)
                    {
                        desasociadosArray = new Object[cuentas.Rows.Count];
                        foreach (DataRow fila in cuentas.Rows)
                        {
                            desasociadosArray[dAPos] = fila[0];
                            datos[0] = fila[1].ToString();
                            datos[1] = fila[2].ToString();
                            datos[2] = fila[3].ToString();
                            tablaDesasociados.Rows.Add(datos);
                            dAPos++;
                        }
                    }
                    else
                    {
                        desasociadosArray = new Object[1];
                        datos[0] = "-";
                        datos[1] = "-";
                        datos[2] = "-";
                        tablaDesasociados.Rows.Add(datos);
                    }

                    cuentas = controladoraC.obtenerCNAMiembrosAsociados(proyectoConsultado.Identificador);
                    if (cuentas.Rows.Count > 0)
                    {
                        asociadosArray = new Object[cuentas.Rows.Count];
                        foreach (DataRow fila in cuentas.Rows)
                        {
                            asociadosArray[aAPos] = fila[0];
                            datos[0] = fila[1].ToString();
                            datos[1] = fila[2].ToString();
                            datos[2] = fila[3].ToString();
                            tablaAsociados.Rows.Add(datos);
                            aAPos++;
                        }
                    }
                    else
                    {
                        asociadosArray = new Object[1];
                        datos[0] = "-";
                        datos[1] = "-";
                        datos[2] = "-";
                        tablaAsociados.Rows.Add(datos);
                    }

                    this.gridMiembrosNoAsociados.DataSource = tablaDesasociados;
                    this.gridMiembrosNoAsociados.DataBind();
                    this.gridMiembrosAsociados.DataSource = tablaAsociados;
                    this.gridMiembrosAsociados.DataBind();

                    int i = 0;
                    if (proyectoConsultado != null && aAPos > 0)
                    {
                        foreach (GridViewRow gvr in gridMiembrosAsociados.Rows)
                        {
                            RadioButton r = (RadioButton)gvr.FindControl("RadioButton1");
                            r.Checked = (String.Equals(cuentas.Rows[i][4].ToString(),"Lider"));
                            ++i;
                        }
                    }
                }
            }

            catch (Exception e)
            {
                mostrarMensaje("warning", "Alerta", "No hay conexión a la base de datos.");
            }
        }

        protected void vaciarGridMiembros()
        {
            aAPos = 0;
            dAPos = 0;
            tablaAsociados = null;
            tablaDesasociados = null;
            asociadosArray = null;
            desasociadosArray = null;
            gridMiembrosNoAsociados.DataSource = tablaDesasociados;
            gridMiembrosNoAsociados.DataBind();
            gridMiembrosAsociados.DataSource = tablaAsociados;
            gridMiembrosAsociados.DataBind();
        }


        protected void ocultarMensaje()
        {
            Alerta.Attributes.Add("hidden", "hidden");
        }

        protected void mostrarMensaje(String tipoAlerta, String alerta, String mensaje)
        {
            Alerta.Attributes["class"] = "alert alert-" + tipoAlerta + " alert-dismissable fade in";
            labelTipoAlerta.Text = alerta + " ";
            labelAlerta.Text = mensaje;
            Alerta.Attributes.Remove("hidden");
        }

        protected void textOficina_TextChanged(object sender, EventArgs e)
        {

        }

        protected void cancelarConsultar(object sender, EventArgs e)
        {
        }

        protected void gridMiembrosNoAsociados_Seleccion(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "X":
                    if( dAPos > 0 )
                    {
                        int numeroFila = Convert.ToInt32(e.CommandArgument);
                        DataRow filaSeleccionada = tablaDesasociados.Rows[Convert.ToInt32(e.CommandArgument)];

                        Object[] nuevo = new Object[3];
                        nuevo[0] = filaSeleccionada[0];
                        nuevo[1] = filaSeleccionada[1];
                        nuevo[2] = filaSeleccionada[2];

                        tablaDesasociados.Rows.RemoveAt(numeroFila);
                        tablaAsociados.Rows.Add(nuevo);

                        if (aAPos == 0)
                            tablaAsociados.Rows.RemoveAt(0);

                        pushMiembroAsociado(popMiembroDesasociado(numeroFila));

                        if( dAPos == 0 )
                        {
                            nuevo[0] = "-";
                            nuevo[1] = "-";
                            nuevo[2] = "-";
                            tablaDesasociados.Rows.Add(nuevo);
                        }

                        gridMiembrosNoAsociados.DataSource = tablaDesasociados;
                        gridMiembrosNoAsociados.DataBind();
                        gridMiembrosAsociados.DataSource = tablaAsociados;
                        gridMiembrosAsociados.DataBind();
                    }
                    break;
            }
        }

        protected void gridMiembrosNoAsociados_CambioPagina(Object sender, GridViewPageEventArgs e)
        {
            this.gridMiembrosNoAsociados.PageIndex = e.NewPageIndex;
            this.gridMiembrosNoAsociados.DataBind();
        }

        protected void gridMiembrosAsociados_Seleccion(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "X":
                    if (aAPos > 0)
                    {
                        int numeroFila = Convert.ToInt32(e.CommandArgument);
                        DataRow filaSeleccionada = tablaAsociados.Rows[Convert.ToInt32(e.CommandArgument)];

                        Object[] nuevo = new Object[3];
                        nuevo[0] = filaSeleccionada[0];
                        nuevo[1] = filaSeleccionada[1];
                        nuevo[2] = filaSeleccionada[2];

                        tablaAsociados.Rows.RemoveAt(numeroFila);
                        tablaDesasociados.Rows.Add(nuevo);

                        if (dAPos == 0)
                            tablaDesasociados.Rows.RemoveAt(0);

                        pushMiembroDesasociado(popMiembroAsociado(numeroFila));
                        if (aAPos == 0)
                        {
                            nuevo[0] = "-";
                            nuevo[1] = "-";
                            nuevo[2] = "-";
                            tablaAsociados.Rows.Add(nuevo);
                        }

                        gridMiembrosNoAsociados.DataSource = tablaDesasociados;
                        gridMiembrosNoAsociados.DataBind();
                        gridMiembrosAsociados.DataSource = tablaAsociados;
                        gridMiembrosAsociados.DataBind();
                    }
                    break;
            }
        }

        protected void gridMiembrosAsociados_CambioPagina(Object sender, GridViewPageEventArgs e)
        {
            this.gridMiembrosAsociados.PageIndex = e.NewPageIndex;
            this.gridMiembrosAsociados.DataBind();
        }

        protected void fechaAsignacion_SelectionChanged(object sender, EventArgs e)
        {
            Label16.Text = "" + fechaAsignacion.SelectedDate.ToShortDateString();
            fechaAsignacion.Visible = false;
        }

        protected void fechaInicio_SelectionChanged(object sender, EventArgs e)
        {
            Label17.Text = "" + fechaInicio.SelectedDate.ToShortDateString();
            fechaInicio.Visible = false;
        }

        protected void fechaFinalizacion_SelectionChanged(object sender, EventArgs e)
        {
            Label18.Text = "" + fechaFinalizacion.SelectedDate.ToShortDateString();
            fechaFinalizacion.Visible = false;

        }

        protected void calendario1_Click(object sender, EventArgs e)
        {
            fechaAsignacion.Visible = !(fechaAsignacion.Visible);
        }

        protected void calendario2_Click(object sender, EventArgs e)
        {
            fechaInicio.Visible = !fechaInicio.Visible;
        }

        protected void calendario3_Click(object sender, EventArgs e)
        {
            fechaFinalizacion.Visible = !fechaFinalizacion.Visible;
        }

        protected void dummy(Object sender, GridViewDeleteEventArgs e)
        {

        }

        protected String popMiembroAsociado( int i )
        {
            if( i >= aAPos )
                return "";
            String res = asociadosArray[i].ToString();
            --aAPos;
            for( int j = i; j < aAPos; ++j )
            {
                asociadosArray[j] = asociadosArray[j + 1];
            }
            Array.Resize<Object>(ref asociadosArray, aAPos);
            return res;
        }
        protected void ListaIteracion_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargarModulos();
        }

        protected void ListaModulo_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        protected String popMiembroDesasociado(int i)
        {
            if (i >= dAPos)
                return "";
            String res = desasociadosArray[i].ToString();
            --dAPos;
            for (int j = i; j < dAPos; ++j)
            {
                desasociadosArray[j] = desasociadosArray[j + 1];
            }
            Array.Resize<Object>(ref desasociadosArray, dAPos);
            return res;
        }

        protected void pushMiembroAsociado(String s)
        {
            Array.Resize<Object>(ref asociadosArray, ++aAPos);
            asociadosArray[aAPos - 1] = s;
        }

        protected void pushMiembroDesasociado(String s)
        {
            Array.Resize<Object>(ref desasociadosArray, ++dAPos);
            desasociadosArray[dAPos - 1] = s;
        }

        protected void Seleccion_Radio(Object sender, EventArgs e)
        {
            foreach (GridViewRow gvr in gridMiembrosAsociados.Rows)
            {
                RadioButton r = (RadioButton)(gvr.FindControl("RadioButton1"));
                r.Checked = false;
            }
            if (aAPos > 0)
            {
                ((RadioButton)(sender)).Checked = true;
            }
        }

        protected void cargarIteraciones()
        {
            ListaIteracion.Items.Clear();
            ListaIteracion.Items.Add(new ListItem("", null));
            DataTable iteraciones = controladoraRequerimientos.consultarIteraciones(proyectoConsultado.Identificador);
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
                DataTable modulos = controladoraRequerimientos.consultarModulos(proyectoConsultado.Identificador, Convert.ToInt32(ListaIteracion.SelectedValue));
                foreach (DataRow fila in modulos.Rows)
                {
                    ListaModulo.Items.Add(new ListItem(fila[2].ToString(), fila[2].ToString()));
                }
            }

        }

        protected void clickAgregarModulo(object sender, EventArgs e)
        {
            if (proyectoConsultado != null)
            {
                String nuevoModulo = textAgregarModulo.Text;
                textAgregarModulo.Text = "";
                bool resultado = controladoraRequerimientos.insertarModulo(proyectoConsultado.Identificador, Convert.ToInt32(ListaIteracion.SelectedValue), nuevoModulo);
                cargarModulos();
            }
        }

        protected void clickAgregarIteracion(object sender, EventArgs e)
        {
            if (proyectoConsultado != null)
            {
                bool resultado = controladoraRequerimientos.insertarIteracion(proyectoConsultado.Identificador);
                cargarIteraciones();
            }

        }

        protected void clickEliminarIteracion(object sender, EventArgs e)
        {
            int iteracionPorEliminar = Convert.ToInt32(ListaIteracion.SelectedValue);
            bool resultado = controladoraRequerimientos.eliminarIteracion(proyectoConsultado.Identificador, iteracionPorEliminar);
            cargarIteraciones();
        }

        protected void clickEliminarModulo(object sender, EventArgs e)
        {
            String moduloPorEliminar = ListaModulo.SelectedValue;
            bool resultado = controladoraRequerimientos.eliminarModulo(proyectoConsultado.Identificador, Convert.ToInt32(ListaIteracion.SelectedValue), moduloPorEliminar);
            cargarModulos();
        }

        protected DataTable crearTablaCuentas()
        {
            DataTable tabla = new DataTable();
            DataColumn columna;

            //corregir
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

        protected void cambioARequerimiento(object sender, EventArgs e)
        {
            ((SiteMaster)Page.Master).setDatosProyecto(proyectoConsultado);
            Response.Redirect("InterfazRequerimientos.aspx");
        }

    }
}
