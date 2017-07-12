Imports System.Data
Imports Wsr_OT_OrdenesDeTrabajo
Imports Utilerias.OrdenesDeTrabajo
Imports Wsr_OT_Catalogos


''' <summary>
''' Clase para manejar el comportamiento interno de la página.
''' </summary>
''' <remarks></remarks>
''' <author>César Bermudez Garcia</author>
''' <creationDate>10/02/2016</creationDate>
''' <changeLog></changeLog>
Partial Class OrdenesDeTrabajo_Frm_OT_AsignacionProfViabilidadTecnica
    Inherits System.Web.UI.Page

#Region "Propiedades"
    ''' <summary>
    ''' Profesionales ingresados por el usuario a la tabla
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>10/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Public Property DsProfesionales As DataTable
        Get
            Return CType(ViewState("DsFuncionarios"), DataTable)
        End Get
        Set(value As DataTable)
            ViewState("DsFuncionarios") = value
        End Set
    End Property

    ''' <summary>
    ''' Almacena la lista de todos los profesionales
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>10/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property DsProfesionalesTaller As DataSet
        Get
            Return CType(ViewState("DsFuncionariosTaller"), DataSet)
        End Get
        Set(value As DataSet)
            ViewState("DsFuncionariosTaller") = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para la ORDEN DE TRABAJO
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>10/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property OrdenTrabajo As EntOttOrdenTrabajo
        Get
            Return CType(ViewState("OrdenTrabajo"), EntOttOrdenTrabajo)
        End Get
        Set(value As EntOttOrdenTrabajo)
            ViewState("OrdenTrabajo") = value
        End Set
    End Property

    ''' <summary>
    ''' Almacena el profesional encargado de la orden
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>11/02/2016</creationDate>
    Public Property ProfesionalEncargado As String
        Get
            If ViewState("ProfesionalEncargado") Is Nothing Then
                Return 0
            End If
            Return CType(ViewState("ProfesionalEncargado"), String)
        End Get
        Set(value As String)
            ViewState("ProfesionalEncargado") = value
        End Set
    End Property


    ''' <summary>
    ''' Almacena la identificación del sector o taller a agregar el operario
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>10/02/2016</creationDate>
    Private Property IdSectorTaller As Integer
        Get
            If ViewState("IdSectorTaller") Is Nothing Then
                Return 0
            End If
            Return CType(ViewState("IdSectorTaller"), Integer)
        End Get
        Set(value As Integer)
            ViewState("IdSectorTaller") = value
        End Set
    End Property

    ''' <summary>
    ''' Es el identificador base de la Orden de trabajo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>10/02/2016</creationDate>
    Public Property IdOrdenTrabajo As String
        Get
            If ViewState("IdOrdenTrabajo") Is Nothing Then
                Return 0
            End If
            Return CType(ViewState("IdOrdenTrabajo"), String)
        End Get
        Set(value As String)
            ViewState("IdOrdenTrabajo") = value
        End Set
    End Property


    ''' <summary>
    ''' Guarda la ubicación de la OT
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>10/02/2016</creationDate>
    Public Property IdUbicacion As Integer
        Get
            If ViewState("IdUbicacion") Is Nothing Then
                Return 0
            End If
            Return CType(ViewState("IdUbicacion"), Integer)
        End Get
        Set(value As Integer)
            ViewState("IdUbicacion") = value
        End Set
    End Property

    ''' <summary>
    ''' Almacena el año de la orden de trabajo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>10/02/2016</creationDate>
    Public Property Anno As Integer
        Get
            If ViewState("Anno") Is Nothing Then
                Return 0
            End If
            Return CType(ViewState("Anno"), Integer)
        End Get
        Set(value As Integer)
            ViewState("Anno") = value
        End Set
    End Property

    ''' <summary>
    ''' Almacena cuando se efectuan cambios o no
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>17/02/2016</creationDate>
    Public Property BanderaCambios As Boolean
        Get
            If ViewState("BanderaCambios") Is Nothing Then
                Return 0
            End If
            Return CType(ViewState("BanderaCambios"), Boolean)
        End Get
        Set(value As Boolean)
            ViewState("BanderaCambios") = value
        End Set
    End Property


#End Region

#Region "Eventos"
    ''' <summary>
    ''' Evento para procesar la carga inicial de datos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>10/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Try
                InicializarFormulario()
            Catch ex As Exception
                Dim vlo_ControlDeErrores As New ControlDeErrores
                vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
            End Try
        End If
    End Sub

    ''' <summary>
    ''' evento que se ejecuta cuando se da click en el boton de agregar para la evaluacion
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>29/01/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnAgregarFuncionario_Click(sender As Object, e As EventArgs) Handles btnAgregarFuncionario.Click
        Try
            AgregaFuncionariosDataTable()

        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' evento que se ejecuta cuando se da click sobre el boton de eliminar
    ''' del listado de funcionarios
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>11/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub ibBorrar_Click(sender As Object, e As ImageClickEventArgs)
        Try
            BorrarFuncionario(CType(sender, ImageButton).CommandName)
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' evento que se ejecuta se carga la lista de funcionarios, por cada registro del
    ''' repeater se asigna un identificador unico
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>11/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub rpFuncionariosEjecucion_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rpProfesionales.ItemDataBound
        Dim vlo_IbBorrar As ImageButton
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            If e.Item.FindControl("ibBorrar") IsNot Nothing Then
                vlo_IbBorrar = CType(e.Item.FindControl("ibBorrar"), ImageButton)
                vlo_IbBorrar.Attributes.Add("data-uniqueid", vlo_IbBorrar.UniqueID)
            End If
        End If
    End Sub

    ''' <summary>
    ''' Cambia el label de area profesional según el profesional seleccionado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>11/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub ddlProfesional_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlProfesional.SelectedIndexChanged
        Dim vlc_Condicion As String
        Dim vlo_Profesional() As DataRow
        Dim vlo_split = Me.ddlProfesional.SelectedValue.Split("¬")

        Try
            vlc_Condicion = String.Format("{0} = {1}", Modelo.V_OTF_OPERARIO_AREALST.CEDULA, vlo_split(0))
            vlo_Profesional = Me.DsProfesionalesTaller.Tables(0).Select(vlc_Condicion)
            Me.lblAreaProfesional.Text = vlo_Profesional(0).Item(Modelo.V_OTF_OPERARIO_AREALST.AREA)
            Me.upAreaProfesional.Update()
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' Al dar click en el botón guardar se almacenaran los datos indicados por el coordinador
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>11/02/2016</creationDate>
    ''' <changeLog></changeLog>
    ''' <remarks></remarks>
    Protected Sub btnGuardar_Click(sender As Object, e As EventArgs)

        Try
            'Se cambia el estado de la orden para que quede registrada como evaluacion
            'De esta forma se asegura que la proxima vez que entre tendrá que registrar los recursos para la ejecución
            If Me.DsProfesionales.Rows.Find(New Object() {Me.ProfesionalEncargado}) IsNot Nothing Then
                If Guardar(False) Then
                    WebUtils.RegistrarScript(Me.Page, "mostrarAlertaEvaluacionExitosaGuardar", "mostrarAlertaEvaluacionExitosaGuardar();")
                Else
                    MostrarAlertaError("No ha sido posible guardar la información del registro")
                End If
            Else
                MostrarAlertaError("Debe asignar el tiempo estimado para evaluación del profesional a cargo")
            End If

        Catch ex As Exception

        End Try
    End Sub

    ''' <summary>
    ''' Al dar click en Guardar y Finalizar se guardaran los datos y se cambiara el estado para que la OT quede lista para ejecutarse. 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>11/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnGuardarYFinalizar_Click(sender As Object, e As EventArgs)
        If Me.DsProfesionales.Rows.Find(New Object() {Me.ProfesionalEncargado}) IsNot Nothing Then

            If Guardar(True) Then
                WebUtils.RegistrarScript(Me.Page, "mostrarAlertaEvaluacionExitosa", "mostrarAlertaEvaluacionExitosa();")
            Else
                MostrarAlertaError("No ha sido posible actualizar la información")
            End If
        Else
            MostrarAlertaError("Debe asignar el tiempo estimado para evaluación del profesional a cargo")
        End If

    End Sub

    ''' <summary>
    ''' Evento que se ejecuta cuando se da click sobre el boton de actualizar (nombre del proyecto)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>11/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnActualizar_Click(sender As Object, e As EventArgs) Handles btnModificar.Click
        Try
            Me.OrdenTrabajo.NombreProyecto = Me.txtNombreProyecto.Text.Trim
            If Modificar() Then
                InicializarControl()
                Me.BanderaCambios = True
                WebUtils.RegistrarScript(Me.Page, "MostrarPopupRegistroExitoso", "mostrarAlertaNombreProyectoExitoso();")
            Else
                MostrarAlertaError("No ha sido posible actualizar la información del nombre del proyecto.")
            End If
        Catch ex As Exception
            If TypeOf (ex) Is System.Web.Services.Protocols.SoapException AndAlso CType(ex, System.Web.Services.Protocols.SoapException).Actor = OrdenesDeTrabajoException.NOMBRE_CLASE Then
                Dim vlo_OrdenesDeTrabajoException As OrdenesDeTrabajoException = OrdenesDeTrabajoException.GetFromSoapException(ex)
                MostrarAlertaError(vlo_OrdenesDeTrabajoException.Message)
            Else
                Dim vlo_ControlDeErrores As New ControlDeErrores
                vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
            End If
        End Try
    End Sub


#End Region

#Region "Metodos"

    ''' <summary>
    ''' Registra el script necesario para mostrar una alerta
    ''' </summary>
    ''' <param name="pvc_Mensaje">Mensaje a mostrar en la alerta</param>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>11/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub MostrarAlertaError(pvc_Mensaje As String)
        WebUtils.RegistrarScript(Me, "alertaError", String.Format("mostrarAlertaError('{0}');", pvc_Mensaje))
    End Sub

    ''' <summary>
    ''' Inicializa el formulario y sus componentes
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>10/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub InicializarFormulario()
        LeerParametros()
        InicializarControl()
        inicializarSetDatos()
        CargarOrdenTrabajo(Me.IdUbicacion, Me.IdOrdenTrabajo)
        CargarEncargados()
        CargarProfesionalesValoracion()
        If Me.OrdenTrabajo.EstadoOrdenTrabajo = EstadoOrden.EVALUACION_PRELIMINAR_EVALUACION Then
            Me.btnGuardar.Visible = False
            Me.btnGuardarYFinalizar.Visible = False
            Me.btnAgregarFuncionario.Visible = False
            Me.ddlProfesional.Enabled = False
            Me.txtNombreProyecto.Enabled = False
            Me.txtTiempoEstimado.Enabled = False
            Me.rfvddlFuncionario.Enabled = False
            Me.rfvDdlUnidad.Enabled = False
            Me.ddlUnidad.Enabled = False
        Else
            CargarListaFuncionarios(String.Format("{0} = '{1}' AND {2} = '{3}' AND {4} <> 0", Modelo.V_OTF_OPERARIO_AREALST.CATEGORIA_LABORAL, Area.PROFESIONAL, Modelo.V_OTF_OPERARIO_AREALST.ESTADO, Estado.ACTIVO, Modelo.V_OTF_OPERARIO_AREALST.ID_AREA_PROFESIONAL),
                                    String.Format("{0} ASC", Modelo.V_OTF_OPERARIO_AREALST.NOMBRE_EMPLEADO), 1)
            CargarUnidades(String.Format("{0} = '{1}'", Modelo.V_OTM_UNIDAD_TIEMPOLST.ESTADO, Estado.ACTIVO), String.Empty, 1)
        End If
        Me.BanderaCambios = False
    End Sub

    Private Sub LeerParametros()
        Me.IdOrdenTrabajo = WebUtils.LeerParametro(Of String)("pvc_IdOrdenTrabajo")
        Me.IdUbicacion = WebUtils.LeerParametro(Of Integer)("pvn_IdUbicacion")
        Me.Anno = WebUtils.LeerParametro(Of Integer)("pvn_Anno")
        Me.ProfesionalEncargado = CargarFuncionario(WebUtils.LeerParametro(Of String)("pvn_IdEncargado")).ID_PERSONAL
        Me.IdSectorTaller = WebUtils.LeerParametro(Of Integer)("pvn_IdSectorTaller")
    End Sub

    ''' <summary>
    ''' Inicializa la tabla de datos a mostrarle al usuario
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>31/01/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub inicializarSetDatos()
        Dim vlo_columna As DataColumn
        Dim vlo_llaves(1) As DataColumn


        'Se crea un nuevo datatabla 
        Me.DsProfesionales = New DataTable

        vlo_columna = New DataColumn()
        'Se asigna un tipo de dato a la columna recientemente creada
        vlo_columna.DataType = System.Type.GetType("System.String")
        'Se le da nombre a esta columna
        vlo_columna.ColumnName = Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.CEDULA
        'Se agrega la columna configurada al set de datos
        DsProfesionales.Columns.Add(vlo_columna)
        'Se agrega al arreglo de llaves primarias la columna
        vlo_llaves(0) = vlo_columna
        'Y se configura el set de datos para que busque por formatos admitidos como llave primaria.
        Me.DsProfesionales.PrimaryKey = vlo_llaves

        vlo_columna = New DataColumn()
        'Se asigna un tipo de dato a la columna recientemente creada
        vlo_columna.DataType = System.Type.GetType("System.String")
        'Se le da nombre a esta columna
        vlo_columna.ColumnName = Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.NOMBRE_EMPLEADO
        'Se agrega la columna configurada al set de datos
        DsProfesionales.Columns.Add(vlo_columna)

        vlo_columna = New DataColumn()
        'Se asigna un tipo de dato a la columna recientemente creada
        vlo_columna.DataType = System.Type.GetType("System.String")
        'Se le da nombre a esta columna
        vlo_columna.ColumnName = Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.DESC_AREA
        'Se agrega la columna configurada al set de datos
        DsProfesionales.Columns.Add(vlo_columna)

        vlo_columna = New DataColumn()
        'Se asigna un tipo de dato a la columna recientemente creada
        vlo_columna.DataType = System.Type.GetType("System.String")
        'Se le da nombre a esta columna
        vlo_columna.ColumnName = Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.DESC_TIEMPO_REAL
        'Se agrega la columna configurada al set de datos
        DsProfesionales.Columns.Add(vlo_columna)

        vlo_columna = New DataColumn()
        'Se asigna un tipo de dato a la columna recientemente creada
        vlo_columna.DataType = System.Type.GetType("System.Int32")
        'Se le da nombre a esta columna
        vlo_columna.ColumnName = Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.TIEMPO_EST
        'Se agrega la columna configurada al set de datos
        DsProfesionales.Columns.Add(vlo_columna)

        vlo_columna = New DataColumn()
        'Se asigna un tipo de dato a la columna recientemente creada
        vlo_columna.DataType = System.Type.GetType("System.Int32")
        'Se le da nombre a esta columna
        vlo_columna.ColumnName = Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.ID_UNIDAD_TIEMPO_EST
        'Se agrega la columna configurada al set de datos
        DsProfesionales.Columns.Add(vlo_columna)

        vlo_columna = New DataColumn()
        'Se asigna un tipo de dato a la columna recientemente creada
        vlo_columna.DataType = System.Type.GetType("System.Int32")
        'Se le da nombre a esta columna
        vlo_columna.ColumnName = Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.ID_SECTOR_TALLER
        'Se agrega la columna configurada al set de datos
        DsProfesionales.Columns.Add(vlo_columna)

    End Sub

    ''' <summary>
    ''' Inicializa el control de usuario para mostrar la información general
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>10/02/2016</creationDate>
    Private Sub InicializarControl()
        'Asignación de Datos para generar el web user control de la información general

        ctrl_InfoGeneral.Anno = Me.Anno
        ctrl_InfoGeneral.IdOrdenTrabajo = IdOrdenTrabajo
        ctrl_InfoGeneral.IdUbicacion = IdUbicacion

        'Manda  llamar al control de información general de la OT
        Me.ctrl_InfoGeneral.Inicializar()
        Me.upControlOrdenTrabajo.Update()

    End Sub

    ''' <summary>
    ''' Carga la lista de unidades de tiempo
    ''' </summary>
    ''' <param name="pvc_Condicion"></param>
    ''' <param name="pvc_Orden"></param>
    ''' <param name="pvn_NumeroDePagina"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>10/02/2016</creationDate>
    Private Sub CargarUnidades(pvc_Condicion As String, pvc_Orden As String, pvn_NumeroDePagina As Integer)
        Dim vlo_Ws_OT_Catalogos As Ws_OT_Catalogos
        Dim vlo_DsDatos As DataSet

        vlo_Ws_OT_Catalogos = New Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            vlo_DsDatos = vlo_Ws_OT_Catalogos.OTM_UNIDAD_TIEMPO_ListarRegistrosLista(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                pvc_Condicion, pvc_Orden, True, pvn_NumeroDePagina,
                CType(ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_ITEMS_POR_PAGINA_EN_LISTADO), Integer))

            If vlo_DsDatos.Tables.Count > 0 AndAlso vlo_DsDatos.Tables(0).Rows.Count > 0 Then
                'ddl de unidad para valoración
                With Me.ddlUnidad
                    .Items.Add(New ListItem(Constantes.FORMATO_DDL_SELECCIONE, String.Empty))
                    .DataSource = vlo_DsDatos
                    .DataMember = vlo_DsDatos.Tables(0).TableName
                    .DataTextField = Modelo.V_OTM_UNIDAD_TIEMPOLST.DESCRIPCION
                    .DataValueField = Modelo.V_OTM_UNIDAD_TIEMPOLST.ID_UNIDAD_TIEMPO
                    .DataBind()
                End With


            End If
        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_Catalogos IsNot Nothing Then
                vlo_Ws_OT_Catalogos.Dispose()
            End If
            If vlo_DsDatos IsNot Nothing Then
                vlo_DsDatos.Dispose()
            End If
        End Try
    End Sub

    ''' <summary>
    ''' Método encargado de comunicarse con el servicio web , obtener y cargar los datos de la orden de trabajo segun 
    ''' las llaves obtenidas por parametro, muestra un mensaje en caso de ser un identificador no valido o no existente
    ''' </summary>
    ''' <param name="pvn_IdUbicacion">id de la ubicacion</param>
    ''' <param name="pvn_IdOrdenTrabajo">id de la orden</param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>10/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarOrdenTrabajo(pvn_IdUbicacion As Integer, pvn_IdOrdenTrabajo As String)
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            Me.OrdenTrabajo = vlo_Ws_OT_OrdenesDeTrabajo.OTT_ORDEN_TRABAJO_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1} AND {2} = '{3}'", Modelo.OTT_ORDEN_TRABAJO.ID_UBICACION, pvn_IdUbicacion, Modelo.OTT_ORDEN_TRABAJO.ID_ORDEN_TRABAJO, pvn_IdOrdenTrabajo))

            Me.txtNombreProyecto.Text = IIf(Me.OrdenTrabajo.NombreProyecto = "-", String.Empty, Me.OrdenTrabajo.NombreProyecto)
        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_OrdenesDeTrabajo IsNot Nothing Then
                vlo_Ws_OT_OrdenesDeTrabajo.Dispose()
            End If
        End Try
    End Sub

    ''' <summary>
    ''' Carga la lista de operarios junto con el coordinador principal y el sustituto(si existe)
    ''' </summary>
    ''' <param name="pvc_Condicion"></param>
    ''' <param name="pvc_Orden"></param>
    ''' <param name="pvn_NumeroDePagina"></param>
    ''' <remarks></remarks>
    ''' <author>Cesar Bermudez</author>
    ''' <creationDate>10/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarListaFuncionarios(pvc_Condicion As String, pvc_Orden As String, pvn_NumeroDePagina As Integer)
        Dim vlo_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos
        Dim vlo_DsDatos As DataSet

        vlo_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            Me.ddlProfesional.Items.Clear()
            Me.ddlProfesional.Items.Add(New ListItem(Constantes.FORMATO_DDL_SELECCIONE, String.Empty))

            'Se carga la lista de operarios que posee el actual sector o taller
            vlo_DsDatos = vlo_Ws_OT_Catalogos.OTF_OPERARIO_AREA_ListarRegistrosLista(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                pvc_Condicion,
                pvc_Orden,
                True,
                pvn_NumeroDePagina,
                CType(ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_ITEMS_POR_PAGINA_EN_LISTADO), Integer))

            If vlo_DsDatos.Tables.Count > 0 AndAlso vlo_DsDatos.Tables(0).Rows.Count > 0 Then

                For Each vlo_fila As DataRow In vlo_DsDatos.Tables(0).Rows
                    Me.ddlProfesional.Items.Add(New ListItem(vlo_fila(Modelo.V_OTF_OPERARIO_AREALST.NOMBRE_EMPLEADO), String.Format("{0}¬{1}", vlo_fila(Modelo.V_OTF_OPERARIO_AREALST.CEDULA), vlo_fila(Modelo.V_OTF_OPERARIO_AREALST.ID_SECTOR_TALLER))))
                Next

                Me.ddlProfesional.DataBind()
            Else
                With Me.ddlProfesional
                    .DataSource = Nothing
                    .DataBind()
                End With

            End If
            Me.DsProfesionalesTaller = vlo_DsDatos
        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_Catalogos IsNot Nothing Then
                vlo_Ws_OT_Catalogos.Dispose()
            End If
            If vlo_DsDatos IsNot Nothing Then
                vlo_DsDatos.Dispose()
            End If
        End Try
    End Sub

    ''' <summary>
    ''' Carga la lista de encargados del proyecto actual
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Cesar Bermudez</author>
    ''' <creationDate>10/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarEncargados()
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Ws_OT_OrdenesDeTrabajo
        Dim vlo_DsDatos As DataSet

        vlo_Ws_OT_OrdenesDeTrabajo = New Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            Me.ddlProfesional.Items.Clear()
            Me.ddlProfesional.Items.Add(New ListItem(Constantes.FORMATO_DDL_SELECCIONE, String.Empty))

            'Se carga la lista de encargados que posee el actual proyecto
            vlo_DsDatos = vlo_Ws_OT_OrdenesDeTrabajo.OTT_OPERARIO_ORDEN_TRAB_ListarRegistrosLista(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1} AND {2} = '{3}' AND {4} = '{5}'",
                    Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.ID_UBICACION,
                    Me.IdUbicacion,
                    Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.ID_ORDEN_TRABAJO,
                    Me.IdOrdenTrabajo,
                    Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.CARGO,
                    Cargo.ENCARGADO), String.Empty, False, 0, 0)

            Dim vlo_DataView As New Data.DataView(vlo_DsDatos.Tables(0))
            vlo_DataView.Sort = String.Format("{0} {1}", Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.FECHA_HASTA, Ordenamiento.ASCENDENTE)

            If vlo_DsDatos.Tables.Count > 0 AndAlso vlo_DsDatos.Tables(0).Rows.Count > 0 Then
                With rpEncargados
                    .DataSource = vlo_DataView
                    .DataMember = vlo_DsDatos.Tables(0).TableName
                    .DataBind()
                    Me.rpProfesionales.Visible = True
                End With
            Else
                With Me.rpEncargados
                    .DataSource = Nothing
                    .DataBind()
                End With
                Me.rpProfesionales.Visible = False
            End If
            Me.DsProfesionalesTaller = vlo_DsDatos
        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_OrdenesDeTrabajo IsNot Nothing Then
                vlo_Ws_OT_OrdenesDeTrabajo.Dispose()
            End If
            If vlo_DsDatos IsNot Nothing Then
                vlo_DsDatos.Dispose()
            End If
        End Try
    End Sub

    ''' <summary>
    ''' agrega un funcionario al dataset, estos se encuentran en memoria, y son insertados en la 
    ''' base de datos hasta el final, es decir un vez que se da click sobre el boton de aceptar
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Cesar Bermudez</author>
    ''' <creationDate>10/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub AgregaFuncionariosDataTable()
        Dim vlo_DrNuevaFila As DataRow

        Try

            Dim vln_Cedula As Integer
            Dim vln_IdSector As Integer
            Dim vlc_split = Me.ddlProfesional.SelectedValue.Split("¬")

            vln_Cedula = CType(vlc_split(0), Integer)
            vln_IdSector = CInt(vlc_split(1))

            If Me.DsProfesionales.Rows.Find(New Object() {vln_Cedula}) Is Nothing Then

                'Se recorre la lista de todos los profesionales disponibles para agregar el correspondiente
                For Each vlo_DrProfesional As DataRow In DsProfesionalesTaller.Tables(0).Rows
                    If String.Compare(vlo_DrProfesional(Modelo.V_OTF_OPERARIO_AREALST.CEDULA), vln_Cedula) = 0 Then
                        vlo_DrNuevaFila = Me.DsProfesionales.NewRow
                        'Item(0): Cedula
                        'Item(1): Nombre completo
                        'Item(2): Area profesional
                        'Item(3): Tiempo
                        vlo_DrNuevaFila.Item(Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.CEDULA) = vlo_DrProfesional(Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.CEDULA)
                        vlo_DrNuevaFila.Item(Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.NOMBRE_EMPLEADO) = vlo_DrProfesional(Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.NOMBRE_EMPLEADO)
                        vlo_DrNuevaFila.Item(Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.DESC_AREA) = vlo_DrProfesional(Modelo.V_OTF_OPERARIO_AREALST.AREA)
                        vlo_DrNuevaFila.Item(Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.DESC_TIEMPO_REAL) = String.Format("{0} {1}", Me.txtTiempoEstimado.Text, Me.ddlUnidad.SelectedItem)
                        vlo_DrNuevaFila.Item(Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.TIEMPO_EST) = CInt(Me.txtTiempoEstimado.Text)
                        vlo_DrNuevaFila.Item(Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.ID_UNIDAD_TIEMPO_EST) = CInt(Me.ddlUnidad.SelectedValue)
                        vlo_DrNuevaFila.Item(Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.ID_SECTOR_TALLER) = vln_IdSector
                        Me.DsProfesionales.Rows.Add(vlo_DrNuevaFila)
                        BanderaCambios = True
                    End If
                Next

                'Dim vlo_DataView As New Data.DataView(Me.DsProfesionales)
                'vlo_DataView.Sort = String.Format("{0} {1}", Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.FECHA_HASTA, Ordenamiento.ASCENDENTE)

                If Me.DsProfesionales IsNot Nothing AndAlso Me.DsProfesionales.Rows.Count > 0 Then
                    Me.rpProfesionales.DataSource = DsProfesionales
                    Me.rpProfesionales.DataMember = Me.DsProfesionales.TableName
                    Me.rpProfesionales.DataBind()
                    Me.rpProfesionales.Visible = True
                Else
                    With Me.rpProfesionales
                        .DataSource = Nothing
                        .DataBind()
                    End With
                    Me.rpProfesionales.Visible = False
                End If

                Me.ddlProfesional.SelectedValue = String.Empty
                Me.lblAreaProfesional.Text = String.Empty
                Me.txtTiempoEstimado.Text = String.Empty
                Me.ddlUnidad.SelectedValue = String.Empty

            Else
                MostrarAlertaError("El usuario(a) que intenta agregar ya ha sido asignado(a)")
                'vlo_DrNuevaFila = Me.DsProfesionales.Rows.Find(New Object() {vln_Cedula})
                'vlo_DrNuevaFila.Item(Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.DESC_TIEMPO_REAL) = String.Format("{0} {1}", Me.txtTiempoEstimado.Text, Me.ddlUnidad.SelectedItem)
                'vlo_DrNuevaFila.Item(Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.TIEMPO_EST) = CInt(Me.txtTiempoEstimado.Text)
                'vlo_DrNuevaFila.Item(Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.ID_UNIDAD_TIEMPO_EST) = CInt(Me.ddlUnidad.SelectedValue)
                'Me.rpProfesionales.DataSource = DsProfesionales
                'Me.rpProfesionales.DataMember = Me.DsProfesionales.TableName
                'Me.rpProfesionales.DataBind()
                'Me.rpProfesionales.Visible = True
            End If

        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' borra un registro del dataset temporal de profesionales
    ''' </summary>
    ''' <param name="pvc_CommandName"></param>
    ''' <remarks></remarks>
    ''' <author>Cesar Bermudez</author>
    ''' <creationDate>11/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub BorrarFuncionario(pvc_CommandName As String)
        Try
            'Este if en caso de que se borren funcionarios y la tabla quede vacia
            If DsProfesionales.Rows.Count <= 0 Then
                Me.rfvddlFuncionario.Enabled = True
                Me.rfvDdlUnidad.Enabled = False
                Me.rfvddlFuncionario.Enabled = False
                Me.rvfTxtTiempoEstimado.Enabled = False
            End If

            Me.DsProfesionales.Rows.Find(New Object() {pvc_CommandName}).Delete()
            Me.BanderaCambios = True


            If Me.DsProfesionales IsNot Nothing AndAlso Me.DsProfesionales.Rows.Count > 0 Then
                Me.rpProfesionales.DataSource = Me.DsProfesionales
                Me.rpProfesionales.DataMember = Me.DsProfesionales.TableName
                Me.rpProfesionales.DataBind()
                Me.rpProfesionales.Visible = True
            Else
                With Me.rpProfesionales
                    .DataSource = Nothing
                    .DataBind()
                End With
                Me.rpProfesionales.Visible = False
            End If

        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' Carga los profesionales actuales que fueron guardados por el usuario para este proyecto en específico
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Cesar Bermudez</author>
    ''' <creationDate>11/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarProfesionalesValoracion()
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        Dim vlc_CondicionEvaluacion As String
        Dim vlo_DsDatosOpeOT As DataSet
        Dim vlo_filaNueva As DataRow

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        vlc_CondicionEvaluacion = String.Format("{0} = {1} AND {2} = '{3}' AND {4} <> 0", Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.ID_UBICACION,
                              Me.IdUbicacion, Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.ID_ORDEN_TRABAJO, Me.IdOrdenTrabajo, Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.ID_UNIDAD_TIEMPO_EST)

        Try
            'Carga la lista de funcionarios actuales en el repeater
            vlo_DsDatosOpeOT = vlo_Ws_OT_OrdenesDeTrabajo.OTT_OPERARIO_ORDEN_TRAB_ListarRegistrosLista(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                vlc_CondicionEvaluacion, String.Empty, False, 1, 0)

            If vlo_DsDatosOpeOT.Tables.Count > 0 AndAlso vlo_DsDatosOpeOT.Tables(0).Rows.Count > 0 Then
                'Se actualiza la tabla en memoria con los datos obtenidos
                For Each vlo_fila As DataRow In vlo_DsDatosOpeOT.Tables(0).Rows
                    vlo_filaNueva = Me.DsProfesionales.NewRow()
                    vlo_filaNueva(Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.CEDULA) = vlo_fila(Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.CEDULA)
                    vlo_filaNueva(Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.NOMBRE_EMPLEADO) = vlo_fila(Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.NOMBRE_EMPLEADO)
                    vlo_filaNueva(Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.DESC_AREA) = vlo_fila(Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.DESC_AREA)
                    vlo_filaNueva(Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.DESC_TIEMPO_REAL) = vlo_fila(Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.DESC_TIEMPO_ESTIMADO)
                    vlo_filaNueva.Item(Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.TIEMPO_EST) = vlo_fila.Item(Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.TIEMPO_EST)
                    vlo_filaNueva.Item(Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.ID_UNIDAD_TIEMPO_EST) = vlo_fila.Item(Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.ID_UNIDAD_TIEMPO_EST)
                    vlo_filaNueva.Item(Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.ID_SECTOR_TALLER) = vlo_fila.Item(Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.ID_SECTOR_TALLER)
                    Me.DsProfesionales.Rows.Add(vlo_filaNueva)
                Next

                Me.rpProfesionales.DataSource = DsProfesionales
                Me.rpProfesionales.DataMember = DsProfesionales.TableName
                Me.rpProfesionales.DataBind()
                Me.rpProfesionales.Visible = True

            Else
                With Me.rpProfesionales
                    .DataSource = Nothing
                    .DataBind()
                End With
                Me.rpProfesionales.Visible = False
            End If


        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_OrdenesDeTrabajo IsNot Nothing Then
                vlo_Ws_OT_OrdenesDeTrabajo.Dispose()
            End If
            If vlo_DsDatosOpeOT IsNot Nothing Then
                vlo_DsDatosOpeOT.Dispose()
            End If
        End Try


    End Sub



#End Region

#Region "Funciones"
    ''' <summary>
    ''' Administra el proceso para modificar una orden de trabajo
    ''' </summary>
    ''' <returns></returns>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>11/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Function Modificar() As Boolean
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        Dim vlo_resultado As Integer

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            vlo_resultado = vlo_Ws_OT_OrdenesDeTrabajo.OTT_ORDEN_TRABAJO_ModificarRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                Me.OrdenTrabajo)
            If vlo_resultado > 0 Then
                CargarOrdenTrabajo(IdUbicacion, IdOrdenTrabajo)
            End If

            Return vlo_resultado > 0
        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_OrdenesDeTrabajo IsNot Nothing Then
                vlo_Ws_OT_OrdenesDeTrabajo.Dispose()
            End If
        End Try
    End Function

    ''' <summary>
    ''' Almacena los valores ingresados por el usuario
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez</author>
    ''' <creationDate>11/02/2016</creationDate>
    ''' <changeLog>
    '''    <author>César Bermudez</author>
    '''    <creationDate>17/02/2016</creationDate>
    '''    <change>Se cambia el numero de parámetros enviados</change>
    ''' </changeLog>
    Private Function Guardar(pvb_CambiarEstado As Boolean) As Boolean
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            'Para ver mas detalle de cada uno de los parámetros revise el SLA 
            Return vlo_Ws_OT_OrdenesDeTrabajo.OTT_OPERARIO_ORDEN_TRAB_GuardarProfesionalesValoracion(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                Me.DsProfesionales, Me.IdUbicacion, Me.IdOrdenTrabajo, New UsuarioActual().UserName, Me.OrdenTrabajo.NombreProyecto, Me.ProfesionalEncargado, pvb_CambiarEstado)

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_OrdenesDeTrabajo IsNot Nothing Then
                vlo_Ws_OT_OrdenesDeTrabajo.Dispose()
            End If
        End Try
    End Function


    ''' <summary>
    ''' Carga el empleado, segun la identificacion personal que obtenga or parametro
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>11/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Public Function CargarFuncionario(pvn_NumEmpleado As String) As WsrEU_Curriculo.EntEmpleados
        Dim vlo_WsEU_Curriculo As WsrEU_Curriculo.wsEU_Curriculo

        vlo_WsEU_Curriculo = New WsrEU_Curriculo.wsEU_Curriculo
        vlo_WsEU_Curriculo.Credentials = System.Net.CredentialCache.DefaultCredentials
        vlo_WsEU_Curriculo.Timeout = -1

        Try
            Return vlo_WsEU_Curriculo.Empleados_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("NUM_EMPLEADO = {0}", pvn_NumEmpleado))
        Catch ex As Exception
            Throw
        Finally
            If vlo_WsEU_Curriculo IsNot Nothing Then
                vlo_WsEU_Curriculo.Dispose()
            End If
        End Try
    End Function

#End Region
End Class
