Imports Wsr_OT_OrdenesDeTrabajo
Imports Utilerias.OrdenesDeTrabajo

Partial Class OrdenesDeTrabajo_Frm_OT_EnvioParaReciboConformeSolicitante
    Inherits System.Web.UI.Page

#Region "Propiedades"

    ''' <summary>
    ''' Propiedad para el tiempo operario
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>03/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property TiempoOperario As EntOttTiempoOperario
        Get
            Return CType(ViewState("TiempoOperario"), EntOttTiempoOperario)
        End Get
        Set(value As EntOttTiempoOperario)
            ViewState("TiempoOperario") = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para  el operario orden trabajo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>03/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property OperarioOrdenTrab As EntOttOperarioOrdenTrab
        Get
            Return CType(ViewState("OperarioOrdenTrab"), EntOttOperarioOrdenTrab)
        End Get
        Set(value As EntOttOperarioOrdenTrab)
            ViewState("OperarioOrdenTrab") = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para la OT a trabajar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>03/02/2016</creationDate>
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
    ''' Determina si se debe cambiar el estado o no
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>03/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property CambiarEstado As Boolean
        Get
            Return CType(ViewState("CambiarEstado"), Boolean)
        End Get
        Set(value As Boolean)
            ViewState("CambiarEstado") = value
        End Set
    End Property

    ''' <summary>
    ''' usuario en session
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>03/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property Usuario As UsuarioActual
        Get
            Return CType(ViewState("Usuario"), UsuarioActual)
        End Get
        Set(value As UsuarioActual)
            ViewState("Usuario") = value
        End Set
    End Property

#End Region

#Region "Eventos"

    ''' <summary>
    ''' Evento que se ejecuta al cargar la págima
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>03/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
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
    ''' Evento que se ejecuta al dar click sobre el botón de guardar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>03/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        Try
            CambiarEstado = False
            If Procesar() Then
                WebUtils.RegistrarScript(Me, "MostrarAlertaActualizacionExitosa", "mostrarAlertaActualizacionExitosa();")
            Else
                MostrarAlertaError("No a sido posible actualizar la información.")
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

    ''' <summary>
    ''' Evento que se ejecuta al dar click sobre el botón de guardar y enviar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>03/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnGuardarEnviar_Click(sender As Object, e As EventArgs) Handles btnGuardarEnviar.Click
        Try
            CambiarEstado = True
            If Procesar() Then
                WebUtils.RegistrarScript(Me, "MostrarAlertaActualizacionExitosa", "mostrarAlertaActualizacionExitosa();")
            Else
                MostrarAlertaError("No a sido posible actualizar la información.")
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

#Region "Métodos"

    ''' <summary>
    ''' Registra el script necesario para mostrar una alerta
    ''' </summary>
    ''' <param name="pvc_Mensaje">Mensaje a mostrar en la alerta</param>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>03/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub MostrarAlertaError(pvc_Mensaje As String)
        WebUtils.RegistrarScript(Me, "alertaError", String.Format("mostrarAlertaError('{0}');", pvc_Mensaje))
    End Sub

    ''' <summary>
    ''' Carga combo de unidades de tiempo, ademas inicializa difrentes componentes de la página
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>03/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub InicializarFormulario()
        Dim vln_IdUbicacion As Integer
        Dim vlc_IdOrdenTrabajo As String

        vln_IdUbicacion = WebUtils.LeerParametro(Of Integer)("pvn_IdUbicacion")
        vlc_IdOrdenTrabajo = WebUtils.LeerParametro(Of String)("pvc_IdOrdenTrabajo")

        Me.lblAccion.Text = "Envio para Recibido Conforme del Solicitante"
        CargarOrdenTrabajo(vln_IdUbicacion, vlc_IdOrdenTrabajo)
        CargarComboUnidadesTiempo()
        CargarOperarioOrdenTrabajo(vln_IdUbicacion, vlc_IdOrdenTrabajo)
        CargarTiempoOperario(vln_IdUbicacion, vlc_IdOrdenTrabajo)
    End Sub

    ''' <summary>
    ''' Cara el combo de unidades de tiempo
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>03/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarComboUnidadesTiempo()
        Dim vlo_Ws_OT_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos
        Dim vlo_DsDatos As Data.DataSet

        vlo_Ws_OT_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            Me.ddlTiempoInvertido.Items.Clear()

            vlo_DsDatos = vlo_Ws_OT_Ws_OT_Catalogos.OTM_UNIDAD_TIEMPO_ListarRegistros(
                                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                                String.Format("{0} = '{1}'", Modelo.OTM_UNIDAD_TIEMPO.ESTADO, Estado.ACTIVO),
                                String.Format("{0} ASC", Modelo.OTM_UNIDAD_TIEMPO.DESCRIPCION),
                                False,
                                0,
                                0)

            If vlo_DsDatos IsNot Nothing AndAlso vlo_DsDatos.Tables(0).Rows.Count > 0 Then
                With Me.ddlTiempoInvertido
                    .DataSource = vlo_DsDatos
                    .DataMember = vlo_DsDatos.Tables(0).TableName
                    .DataTextField = Modelo.OTM_UNIDAD_TIEMPO.DESCRIPCION
                    .DataValueField = Modelo.OTM_UNIDAD_TIEMPO.ID_UNIDAD_TIEMPO
                    .DataBind()
                End With
            End If

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_Ws_OT_Catalogos IsNot Nothing Then
                vlo_Ws_OT_Ws_OT_Catalogos.Dispose()
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
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>03/02/2016</creationDate>
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

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_OrdenesDeTrabajo IsNot Nothing Then
                vlo_Ws_OT_OrdenesDeTrabajo.Dispose()
            End If
        End Try

        If Not Me.OrdenTrabajo.Existe Then
            WebUtils.RegistrarScript(Me.Page, "MostrarAlertaLlaveIncorrecta", "mostrarAlertaLlaveIncorrecta();")
        End If
    End Sub

    ''' <summary>
    ''' registro de operario relacionado a la orden
    ''' </summary>
    ''' <param name="pvn_IdUbicacion"></param>
    ''' <param name="pvn_IdOrdenTrabajo"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>03/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarOperarioOrdenTrabajo(pvn_IdUbicacion As Integer, pvn_IdOrdenTrabajo As String)
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            Me.OperarioOrdenTrab = vlo_Ws_OT_OrdenesDeTrabajo.OTT_OPERARIO_ORDEN_TRAB_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1} AND {2} = '{3}' ORDER BY {4} {5}", Modelo.OTT_OPERARIO_ORDEN_TRAB.ID_UBICACION, pvn_IdUbicacion, Modelo.OTT_OPERARIO_ORDEN_TRAB.ID_ORDEN_TRABAJO, pvn_IdOrdenTrabajo, Modelo.OTT_OPERARIO_ORDEN_TRAB.FECHA_EJECUTA, Ordenamiento.ASCENDENTE))

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_OrdenesDeTrabajo IsNot Nothing Then
                vlo_Ws_OT_OrdenesDeTrabajo.Dispose()
            End If
        End Try

        If Me.OperarioOrdenTrab.Existe Then
            Me.txtFechaFinalizacion.Text = DateTime.Now.ToString(Constantes.FORMATO_FECHA_UI)
        End If

    End Sub

    ''' <summary>
    ''' registro de tiempo operario relacionado a la orden
    ''' </summary>
    ''' <param name="pvn_IdUbicacion"></param>
    ''' <param name="pvn_IdOrdenTrabajo"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>03/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarTiempoOperario(pvn_IdUbicacion As Integer, pvn_IdOrdenTrabajo As String)
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            Me.TiempoOperario = vlo_Ws_OT_OrdenesDeTrabajo.OTT_TIEMPO_OPERARIO_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1} AND {2} = '{3}'", Modelo.OTT_TIEMPO_OPERARIO.ID_UBICACION, pvn_IdUbicacion, Modelo.OTT_TIEMPO_OPERARIO.ID_ORDEN_TRABAJO, pvn_IdOrdenTrabajo))

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_OrdenesDeTrabajo IsNot Nothing Then
                vlo_Ws_OT_OrdenesDeTrabajo.Dispose()
            End If
        End Try

        If Me.TiempoOperario.Existe Then
            With Me.TiempoOperario
                Me.ddlTiempoInvertido.SelectedValue = .IdUnidadTiempo
                Me.txtTiempoInvertido.Text = .Tiempo
            End With
        End If

    End Sub

#End Region

#Region "Funciones"

    ''' <summary>
    ''' Se comunica con el servicio web para procesar la información
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>03/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Function Procesar() As Boolean
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            Return vlo_Ws_OT_OrdenesDeTrabajo.OTT_ORDEN_TRABAJO_EnviarParaRecibidoConformeActualizacionOperarios(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                Me.OrdenTrabajo, CType(Me.txtFechaFinalizacion.Text, DateTime), CType(Me.txtTiempoInvertido.Text.Trim, Integer), CType(Me.ddlTiempoInvertido.SelectedValue, Integer), Me.CambiarEstado, Me.Usuario.UserName, Me.Usuario.NumEmpleado) > 0
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
