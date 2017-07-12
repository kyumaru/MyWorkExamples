Imports Utilerias.OrdenesDeTrabajo
Imports Wsr_OT_OrdenesDeTrabajo


Partial Class OrdenesDeTrabajo_Frm_OT_AprobacionPresupuestoCoordinador
    Inherits System.Web.UI.Page

#Region "Propiedades"

    ''' <summary>
    ''' Propiedad para el informe de presupuesto
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>07/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Public Property InformePresupuesto As EntOttInformePresupuesto
        Get
            Return CType(ViewState("InformePresupuesto"), EntOttInformePresupuesto)
        End Get
        Set(value As EntOttInformePresupuesto)
            ViewState("InformePresupuesto") = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para el usuario en session
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>07/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property Usuario As UsuarioActual
        Get
            Return CType(ViewState("Usuario"), UsuarioActual)
        End Get
        Set(value As UsuarioActual)
            ViewState("Usuario") = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para el id ubicacion de la OT
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>07/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property IdUbicacion As Integer
        Get
            Return CType(ViewState("IdUbicacion"), Integer)
        End Get
        Set(value As Integer)
            ViewState("IdUbicacion") = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para el id ubicacion de la OT
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>07/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property IdOrdenTrabajo As String
        Get
            Return CType(ViewState("IdOrdenTrabajo"), String)
        End Get
        Set(value As String)
            ViewState("IdOrdenTrabajo") = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para la ORDEN DE TRABAJO
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>07/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Public Property OrdenTrabajo As EntOttOrdenTrabajo
        Get
            Return CType(ViewState("OrdenTrabajo"), EntOttOrdenTrabajo)
        End Get
        Set(value As EntOttOrdenTrabajo)
            ViewState("OrdenTrabajo") = value
        End Set
    End Property

#End Region

#Region "Eventos"

    ''' <summary>
    ''' Evento que se ejecuta al cargar la página, inicializa los componentes necesarios
    ''' para el funcionamiento de la página
    ''' </summary>
    ''' <param name="sender">Parámetro propio del evento</param>
    ''' <param name="e">Parámetro propio del evento</param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>07/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Try
                Me.Usuario = New UsuarioActual
                InicializarFormulario()
            Catch ex As Exception
                Dim vlo_ControlDeErrores As New ControlDeErrores
                vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
            End Try
        End If
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta cuando se cambia el valor del check selecionado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>07/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub Condicion_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbtAprobada.CheckedChanged, rbtDevuelta.CheckedChanged
        Try

            If Me.rbtAprobada.Checked Then
                Me.rfvTxtJustificacion.Enabled = False
                Me.trJustificacion.Visible = False
            End If

            If Me.rbtDevuelta.Checked Then
                Me.rfvTxtJustificacion.Enabled = True
                Me.trJustificacion.Visible = True
            End If
            Me.hdnNicEdit.Value = HttpUtility.HtmlDecode(Me.InformePresupuesto.Detalle)

            WebUtils.RegistrarScript(Me.Page, "GoDown", "GoDown();")

        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta cuando se da click sobre el boton de guardar y enviar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>07/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnGuardarEnviar_Click(sender As Object, e As EventArgs) Handles btnGuardarEnviar.Click
        If Page.IsValid Then
            Try
                If Modificar() Then
                    WebUtils.RegistrarScript(Me.Page, "MostrarAlertaActualizacionExitosa", "mostrarAlertaActualizacionExitosa();")
                Else
                    MostrarAlertaError("No ha sido posible actualizar la información del registro")
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
        End If
    End Sub

#End Region

#Region "Métodos"

    ''' <summary>
    ''' Registra el script necesario para mostrar una alerta
    ''' </summary>
    ''' <param name="pvc_Mensaje">Mensaje a mostrar en la alerta</param>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>07/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub MostrarAlertaError(pvc_Mensaje As String)
        WebUtils.RegistrarScript(Me, "alertaError", String.Format("mostrarAlertaError('{0}');", pvc_Mensaje))
    End Sub

    ''' <summary>
    ''' Método encargado de inicializar los diferentess componentes del formulario
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>07/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub InicializarFormulario()
        LeerParametros()
        InicializarControl()
        CargarEncargadoProyecto()
        CargarInformePresupuesto()
        CargarOrdenTrabajo(Me.IdUbicacion, Me.IdOrdenTrabajo)
        If Me.InformePresupuesto.Existe Then
            Me.lblPresupuesto.Text = Me.InformePresupuesto.EstimacionPresupuestaria.ToString
            Me.hdnNicEdit.Value = HttpUtility.HtmlDecode(Me.InformePresupuesto.Detalle)
            Me.rbtDevuelta.Checked = True
            Me.rfvTxtJustificacion.Enabled = True
        Else
            WebUtils.RegistrarScript(Me.Page, "MostrarAlertaLlaveIncorrecta", "mostrarAlertaLlaveIncorrecta();")
        End If
    End Sub

    ''' <summary>
    ''' Método encargado de comunicarse con el servicio web , obtener y cargar los datos de la orden de trabajo segun 
    ''' las llaves obtenidas por parametro
    ''' </summary>
    ''' <param name="pvn_IdUbicacion">id de la ubicacion</param>
    ''' <param name="pvc_IdOrdenTrabajo">id de la orden</param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>07/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarOrdenTrabajo(pvn_IdUbicacion As Integer, pvc_IdOrdenTrabajo As String)
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            Me.OrdenTrabajo = vlo_Ws_OT_OrdenesDeTrabajo.OTT_ORDEN_TRABAJO_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1} AND {2} = '{3}'", Modelo.OTT_ORDEN_TRABAJO.ID_UBICACION, pvn_IdUbicacion, Modelo.OTT_ORDEN_TRABAJO.ID_ORDEN_TRABAJO, pvc_IdOrdenTrabajo))

            If Me.OrdenTrabajo.EstadoOrdenTrabajo = EstadoOrden.PRESUPUESTO_DEVUELTO_JEFATURA Then
                CargarObservacionesDevolucionJefatura()
            End If

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_OrdenesDeTrabajo IsNot Nothing Then
                vlo_Ws_OT_OrdenesDeTrabajo.Dispose()
            End If
        End Try
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>17/08/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarObservacionesDevolucionJefatura()
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        Dim vlo_DsDatos As Data.DataSet

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            vlo_DsDatos = vlo_Ws_OT_OrdenesDeTrabajo.OTT_TRAZABILIDAD_PROCESO_ListarRegistrosLista(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1} AND {2} = '{3}' AND {4} = '{5}'", Modelo.V_OTT_TRAZABILIDAD_PROCESOLST.ID_UBICACION, Me.OrdenTrabajo.IdUbicacion, Modelo.V_OTT_TRAZABILIDAD_PROCESOLST.ID_ORDEN_TRABAJO, Me.OrdenTrabajo.IdOrdenTrabajo, Modelo.V_OTT_TRAZABILIDAD_PROCESOLST.ESTADO_ORDEN_TRABAJO, EstadoOrden.PRESUPUESTO_DEVUELTO_JEFATURA),
                String.Format("{0} {1}", Modelo.V_OTT_TRAZABILIDAD_PROCESOLST.TIME_STAMP, Ordenamiento.DESCENDENTE), False, 0, 0)

            If vlo_DsDatos.Tables.Count > 0 AndAlso vlo_DsDatos.Tables(0).Rows.Count > 0 Then
                Me.trObservacionesDevolucion.Visible = True
                Me.lblObservacionesDevolucion.Text = vlo_DsDatos.Tables(0).Rows(0)(Modelo.V_OTT_TRAZABILIDAD_PROCESOLST.OBSERVACIONES_INTERNAS).ToString
            End If

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_OrdenesDeTrabajo IsNot Nothing Then
                vlo_Ws_OT_OrdenesDeTrabajo.Dispose()
            End If
        End Try
    End Sub

    ''' <summary>
    ''' Carga la entidad de la informe presupuesto, en caso de existir
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>07/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarInformePresupuesto()
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            Me.InformePresupuesto = vlo_Ws_OT_OrdenesDeTrabajo.OTT_INFORME_PRESUPUESTO_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1} AND {2} = '{3}'", Modelo.OTT_INFORME_PRESUPUESTO.ID_UBICACION, Me.IdUbicacion, Modelo.OTT_INFORME_PRESUPUESTO.ID_ORDEN_TRABAJO, Me.IdOrdenTrabajo))

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_OrdenesDeTrabajo IsNot Nothing Then
                vlo_Ws_OT_OrdenesDeTrabajo.Dispose()
            End If
        End Try
    End Sub

    ''' <summary>
    ''' Lee los parametros 
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>07/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub LeerParametros()
        Me.IdOrdenTrabajo = WebUtils.LeerParametro(Of String)("pvc_IdOrdenTrabajo")
        Me.IdUbicacion = WebUtils.LeerParametro(Of Integer)("pvn_IdUbicacion")
    End Sub

    ''' <summary>
    ''' Inicializa el control de usuario
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>07/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub InicializarControl()
        'Asignación de Datos para generar el web user control de la información general
        ctrl_InfoGeneral.Anno = WebUtils.LeerParametro(Of Integer)("pvn_Anno")
        ctrl_InfoGeneral.IdOrdenTrabajo = Me.IdOrdenTrabajo
        ctrl_InfoGeneral.IdUbicacion = Me.IdUbicacion.ToString
        'Manda  llamar al control de información general de la OT
        Me.ctrl_InfoGeneral.Inicializar()
    End Sub

    ''' <summary>
    ''' Se encarga de cargar el encargado actual del proyecto
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>07/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarEncargadoProyecto()
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        Dim vlo_DsDatos As Data.DataSet

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            vlo_DsDatos = vlo_Ws_OT_OrdenesDeTrabajo.OTT_OPERARIO_ORDEN_TRAB_ListarRegistrosLista(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1} AND {2} = '{3}' AND {4} = '{5}'", Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.ID_UBICACION, Me.IdUbicacion, Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.ID_ORDEN_TRABAJO, Me.IdOrdenTrabajo, Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.CARGO, Cargo.ENCARGADO),
                String.Format("{0} {1}", Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.FECHA_HASTA, Ordenamiento.DESCENDENTE),
                False,
                0,
                0)

            If vlo_DsDatos.Tables.Count > 0 AndAlso vlo_DsDatos.Tables(0).Rows.Count > 0 Then
                Me.lblEncargadoProyecto.Text = vlo_DsDatos.Tables(0).Rows(0)(Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.NOMBRE_EMPLEADO)
            Else
                Me.lblEncargadoProyecto.Text = "-"
            End If
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

#End Region

#Region "Funciones"

    ''' <summary>
    ''' Administra el proceso para modificar un
    ''' </summary>
    ''' <returns></returns>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>07/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Function Modificar() As Boolean
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            Return vlo_Ws_OT_OrdenesDeTrabajo.OTT_ORDEN_TRABAJO_AprobacionPresupuestoCoordinador(
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                    Me.OrdenTrabajo,
                    Me.rbtAprobada.Checked,
                    IIf(Me.rbtAprobada.Checked = True, String.Empty, Me.txtJustificacion.Text),
                    Me.Usuario.UserName) > 0

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_OrdenesDeTrabajo IsNot Nothing Then
                vlo_Ws_OT_OrdenesDeTrabajo.Dispose()
            End If
        End Try
    End Function

#End Region

End Class
