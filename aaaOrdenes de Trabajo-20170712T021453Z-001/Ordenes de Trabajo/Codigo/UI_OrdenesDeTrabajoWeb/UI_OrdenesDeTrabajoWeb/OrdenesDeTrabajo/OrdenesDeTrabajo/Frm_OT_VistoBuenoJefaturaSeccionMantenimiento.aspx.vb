Imports Utilerias.OrdenesDeTrabajo
Imports Wsr_OT_OrdenesDeTrabajo

Partial Class OrdenesDeTrabajo_Frm_OT_VistoBuenoJefaturaSeccionMantenimiento
    Inherits System.Web.UI.Page


#Region "Propiedades"

    ''' <summary>
    ''' Extensiones para archivo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>26/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Public Property ExtensionesArchivo As String
        Get
            Return CType(ViewState("ExtensionesArchivo"), String)
        End Get
        Set(value As String)
            ViewState("ExtensionesArchivo") = value
        End Set
    End Property

    ''' <summary>
    ''' tamaño en megas del archivo a cargar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>26/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Public Property TamanoArchivo As Integer
        Get
            Return CType(ViewState("TamanoArchivo"), Integer)
        End Get
        Set(value As Integer)
            ViewState("TamanoArchivo") = value
        End Set
    End Property

    ''' <summary>
    ''' etapa para la orden de trabajo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>26/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property EtapaOrdenTrabajo As Wsr_OT_Catalogos.EntOtmEtapaOrdenTrabajo
        Get
            Return CType(ViewState("EtapaOrdenTrabajo"), Wsr_OT_Catalogos.EntOtmEtapaOrdenTrabajo)
        End Get
        Set(value As Wsr_OT_Catalogos.EntOtmEtapaOrdenTrabajo)
            ViewState("EtapaOrdenTrabajo") = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para el usuario en session
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>26/02/2016</creationDate>
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
    ''' <creationDate>26/02/2016</creationDate>
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
    ''' <creationDate>26/02/2016</creationDate>
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
    ''' <creationDate>26/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Public Property OrdenTrabajo As EntOttOrdenTrabajo
        Get
            Return CType(ViewState("OrdenTrabajo"), EntOttOrdenTrabajo)
        End Get
        Set(value As EntOttOrdenTrabajo)
            ViewState("OrdenTrabajo") = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para la ORDEN DE TRABAJO
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>26/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Public Property OficioAdjuntoOrdenTrabajo As EntOttAdjuntoOrdenTrabajo
        Get
            Return CType(ViewState("OficioAdjuntoOrdenTrabajo"), EntOttAdjuntoOrdenTrabajo)
        End Get
        Set(value As EntOttAdjuntoOrdenTrabajo)
            ViewState("OficioAdjuntoOrdenTrabajo") = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para la ORDEN DE TRABAJO
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>26/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Public Property ViabilidadTecnica As EntOttViabilidadTecnica
        Get
            Return CType(ViewState("ViabilidadTecnica"), EntOttViabilidadTecnica)
        End Get
        Set(value As EntOttViabilidadTecnica)
            ViewState("ViabilidadTecnica") = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para el dataset de archivos adjuntos
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>26/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property DsAdjuntosInsert As Data.DataSet
        Get
            Return CType(ViewState("DsAdjuntosInsert"), Data.DataSet)
        End Get
        Set(value As Data.DataSet)
            ViewState("DsAdjuntosInsert") = value
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
    ''' <creationDate>26/02/2016</creationDate>
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
    ''' descarga la imagen adjunta
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>26/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub lnkArchivo_Command(sender As Object, e As CommandEventArgs)
        Try
            Response.Clear()
            Response.AppendHeader("content-disposition", "attachment; filename=" + Me.DsAdjuntosInsert.Tables(0).Rows(Convert.ToInt32(e.CommandArgument))(Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.NOMBRE_ARCHIVO).ToString)
            Response.BinaryWrite(CType(Me.DsAdjuntosInsert.Tables(0).Rows(Convert.ToInt32(e.CommandArgument))(Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.ARCHIVO), Byte()))
            Response.End()
        Catch ex_Descarga As System.Threading.ThreadAbortException
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta cuando se cambia el valor del check selecionado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>26/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub Condicion_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbtAprobada.CheckedChanged, rbtDevuelta.CheckedChanged
        Try

            If Me.ViabilidadTecnica.Viabilidad = 1 Then
                If Me.rbtAprobada.Checked Then
                    Me.rfvTxtJustificacion.Enabled = False
                    Me.trTiempoRespuesta.Visible = True
                    Me.rfvIfArchivo.Enabled = True
                    Me.rfvTxtTiempoRespuesta.Enabled = True
                    Me.rfvDdlUnidadTiempo.Enabled = True
                    Me.trOficio.Visible = True

                End If

                If Me.rbtDevuelta.Checked Then
                    Me.rfvTxtJustificacion.Enabled = True
                    Me.trTiempoRespuesta.Visible = False
                    Me.rfvIfArchivo.Enabled = False
                    Me.rfvTxtTiempoRespuesta.Enabled = False
                    Me.rfvDdlUnidadTiempo.Enabled = False
                    Me.trOficio.Visible = False
                End If
            Else

                If Me.rbtAprobada.Checked Then
                    Me.rfvTxtJustificacion.Enabled = False
                    Me.trTiempoRespuesta.Visible = False
                    Me.rfvIfArchivo.Enabled = True
                    Me.rfvTxtTiempoRespuesta.Enabled = False
                    Me.rfvDdlUnidadTiempo.Enabled = False
                    Me.trOficio.Visible = True

                End If

                If Me.rbtDevuelta.Checked Then
                    Me.rfvTxtJustificacion.Enabled = True
                    Me.trTiempoRespuesta.Visible = False
                    Me.rfvIfArchivo.Enabled = False
                    Me.rfvTxtTiempoRespuesta.Enabled = False
                    Me.rfvDdlUnidadTiempo.Enabled = False
                    Me.trOficio.Visible = False
                End If

            End If

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
    ''' <creationDate>26/02/2016</creationDate>
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

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnActualizar_Click(sender As Object, e As EventArgs) Handles btnActualizar.Click
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials
        Try
            Me.ViabilidadTecnica.Detalle = HttpUtility.HtmlEncode(Me.hdnNicEdit.Value)

            vlo_Ws_OT_OrdenesDeTrabajo.OTT_VIABILIDAD_TECNICA_ModificarRegistro(
              ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
              ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
             Me.ViabilidadTecnica)

        Catch ex As Exception

        End Try
    End Sub

#End Region

#Region "Métodos"

    ''' <summary>
    ''' Método encargado de descargar archivos
    ''' </summary>
    ''' <param name="pvo_Archivo">bytes del archivo a descargar</param>
    ''' <param name="pvc_NombreArchivo">nombre del archivo a descargar</param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>26/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub DescargaArchivo(pvo_Archivo As Byte(), pvc_NombreArchivo As String)
        pvc_NombreArchivo = pvc_NombreArchivo.Replace(" ", "")
        Try
            Response.Clear()
            Response.AppendHeader("content-disposition", "attachment; filename=" + pvc_NombreArchivo)
            Response.BinaryWrite(pvo_Archivo)
            Response.End()
        Catch ex_Descarga As System.Threading.ThreadAbortException
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Registra el script necesario para mostrar una alerta
    ''' </summary>
    ''' <param name="pvc_Mensaje">Mensaje a mostrar en la alerta</param>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>26/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub MostrarAlertaError(pvc_Mensaje As String)
        WebUtils.RegistrarScript(Me, "alertaError", String.Format("mostrarAlertaError('{0}');", pvc_Mensaje))
    End Sub

    ''' <summary>
    ''' Método encargado de inicializar los diferentess componentes del formulario
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>26/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub InicializarFormulario()
        LeerParametros()
        InicializarControl()
        CargarEncargadoProyecto()
        CargarListaColaborador()
        CargarViabilidadTecnica()
        Me.EtapaOrdenTrabajo = CargarEtapaOrdenTrabajo()
        CargaDsAdjuntos()
        CargarComboUnidadTiempo(String.Format("{0} = '{1}'", Modelo.V_OTM_UNIDAD_TIEMPOLST.ESTADO, Estado.ACTIVO))

        Me.ExtensionesArchivo = CargarExtensionesArchivo()
        imgExtensiones.Attributes.Add("title", String.Format("Extensiones permitidas:{0}", ExtensionesArchivo.ToLower))
        Me.TamanoArchivo = CargarTamañoMaximoArchivo()

        If Me.ViabilidadTecnica.Existe Then

            If Me.ViabilidadTecnica.Viabilidad = 1 Then
                Me.lblIndicadorViabilidad.Text = "Si"
                Me.rfvTxtJustificacion.Enabled = True
                Me.rfvTxtTiempoRespuesta.Enabled = True
                Me.rfvDdlUnidadTiempo.Enabled = True
                Me.trTiempoRespuesta.Visible = True
            Else
                Me.lblIndicadorViabilidad.Text = "No"
                Me.rfvTxtJustificacion.Enabled = False
                Me.rfvTxtTiempoRespuesta.Enabled = False
                Me.rfvDdlUnidadTiempo.Enabled = False
                Me.trTiempoRespuesta.Visible = False
            End If

            Me.lblEstimacionPresup.Text = Me.ViabilidadTecnica.EstimacionPresupuestaria.ToString("N2")
            Me.hdnNicEdit.Value = HttpUtility.HtmlDecode(Me.ViabilidadTecnica.Detalle)
            WebUtils.RegistrarScript(Me.Page, "actualizarContenidoBloqueado", "actualizarContenidoBloqueado();")

            Me.rbtDevuelta.Checked = True
            Me.rfvTxtJustificacion.Enabled = True

            Me.rfvTxtTiempoRespuesta.Enabled = False
            Me.rfvDdlUnidadTiempo.Enabled = False
            Me.trTiempoRespuesta.Visible = False
            Me.rfvIfArchivo.Enabled = False
            Me.trOficio.Visible = False

            Me.txtTiempoRespuesta.Text = IIf(Me.ViabilidadTecnica.TiempoRespuesta <> 0, Me.ViabilidadTecnica.TiempoRespuesta, String.Empty)
            Me.ddlUnidadTiempo.SelectedValue = IIf(Me.ViabilidadTecnica.IdUnidadTiempo <> 0, Me.ViabilidadTecnica.IdUnidadTiempo, String.Empty)

        Else
            WebUtils.RegistrarScript(Me.Page, "MostrarAlertaLlaveIncorrecta", "mostrarAlertaLlaveIncorrecta();")
        End If

    End Sub

    ''' <summary>
    ''' carga los datos del combo de unidades de tiempo
    ''' </summary>
    ''' <param name="pvc_Condicion"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>25/01/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarComboUnidadTiempo(pvc_Condicion As String)
        Dim vlo_Ws_OT_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos
        Dim vlo_DsDatos As Data.DataSet

        vlo_Ws_OT_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            Me.ddlUnidadTiempo.Items.Clear()
            Me.ddlUnidadTiempo.Items.Add(New ListItem(Constantes.FORMATO_DDL_SELECCIONE, String.Empty))

            vlo_DsDatos = vlo_Ws_OT_Ws_OT_Catalogos.OTM_UNIDAD_TIEMPO_ListarRegistrosLista(
                            ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                            ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                            pvc_Condicion,
                            String.Empty,
                            False,
                            0,
                            0)

            If vlo_DsDatos IsNot Nothing AndAlso vlo_DsDatos.Tables(0).Rows.Count > 0 Then
                With Me.ddlUnidadTiempo
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
            If vlo_Ws_OT_Ws_OT_Catalogos IsNot Nothing Then
                vlo_Ws_OT_Ws_OT_Catalogos.Dispose()
            End If
        End Try
    End Sub

    ''' <summary>
    ''' Carga los Archivos actuales existente de adjuntos nde viabilidad
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>26/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargaDsAdjuntos()
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            Me.DsAdjuntosInsert = vlo_Ws_OT_OrdenesDeTrabajo.OTT_ADJUNTO_ORDEN_TRABAJO_ListarRegistrosLista(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1} AND {2} = {3} AND {4} = '{5}'",
                              Modelo.V_OTT_ADJUNTO_ORDEN_TRABAJOLST.ID_ETAPA_ORDEN_TRABAJO, Me.EtapaOrdenTrabajo.IdEtapaOrdenTrabajo,
                              Modelo.V_OTT_ADJUNTO_ORDEN_TRABAJOLST.ID_UBICACION, Me.IdUbicacion,
                              Modelo.V_OTT_ADJUNTO_ORDEN_TRABAJOLST.ID_ORDEN_TRABAJO, Me.IdOrdenTrabajo),
                String.Empty,
                False,
                0,
                0)

            Me.rpAdjunto.DataSource = Me.DsAdjuntosInsert
            Me.rpAdjunto.DataMember = Me.DsAdjuntosInsert.Tables(0).TableName
            Me.rpAdjunto.DataBind()

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
    ''' <creationDate>26/02/2016</creationDate>
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
    ''' <creationDate>26/02/2016</creationDate>
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
    ''' Carga la entidad de la viabilidad tecnica, en caso de existir
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>26/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarViabilidadTecnica()
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            Me.ViabilidadTecnica = vlo_Ws_OT_OrdenesDeTrabajo.OTT_VIABILIDAD_TECNICA_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1} AND {2} = '{3}'", Modelo.V_OTT_VIABILIDAD_TECNICA.ID_UBICACION, Me.IdUbicacion, Modelo.V_OTT_VIABILIDAD_TECNICA.ID_ORDEN_TRABAJO, Me.IdOrdenTrabajo))

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_OrdenesDeTrabajo IsNot Nothing Then
                vlo_Ws_OT_OrdenesDeTrabajo.Dispose()
            End If
        End Try
    End Sub

    ''' <summary>
    ''' Se encarga de cargar el encargado actual del proyecto
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>26/02/2016</creationDate>
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

    ''' <summary>
    ''' Carga la lista de funcionario
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>26/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarListaColaborador()
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        Dim vlo_DsDatos As Data.DataSet

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            vlo_DsDatos = vlo_Ws_OT_OrdenesDeTrabajo.OTT_OPERARIO_ORDEN_TRAB_ListarRegistrosLista(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1} AND {2} = '{3}' AND {4} = '{5}' AND {6} <> 0",
                              Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.ID_UBICACION, Me.IdUbicacion,
                              Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.ID_ORDEN_TRABAJO, Me.IdOrdenTrabajo,
                              Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.CARGO, Cargo.COLABORADOR,
                              Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.ID_UNIDAD_TIEMPO_REAL),
                String.Empty,
                False,
                0,
                0)

            With Me.rpColaboradores
                .DataSource = vlo_DsDatos
                .DataMember = vlo_DsDatos.Tables(0).TableName
                .DataBind()
            End With
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
    ''' carga la etapa de la orden
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>26/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Function CargarEtapaOrdenTrabajo() As Wsr_OT_Catalogos.EntOtmEtapaOrdenTrabajo
        Dim vlo_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos

        vlo_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            Return vlo_Ws_OT_Catalogos.OTM_ETAPA_ORDEN_TRABAJO_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1}", Modelo.OTM_ETAPA_ORDEN_TRABAJO.ID_ETAPA_ORDEN_TRABAJO, EtapasOrdenTrabajo.ANALISIS_VIABILIDAD_TECNICA))
        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_Catalogos IsNot Nothing Then
                vlo_Ws_OT_Catalogos.Dispose()
            End If
        End Try
    End Function

    ''' <summary>
    ''' Funcion encargada de contruir el registro  de la viabilidad
    ''' </summary>
    ''' <returns>la convocatoria</returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>26/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Function ConstruirRegistro() As Wsr_OT_OrdenesDeTrabajo.EntOttViabilidadTecnica
        Dim vlo_EntOttViabilidadTecnica As Wsr_OT_OrdenesDeTrabajo.EntOttViabilidadTecnica

        vlo_EntOttViabilidadTecnica = Me.ViabilidadTecnica

        With vlo_EntOttViabilidadTecnica
            If Me.trTiempoRespuesta.Visible Then
                .IdUnidadTiempo = Me.ddlUnidadTiempo.SelectedValue
                .TiempoRespuesta = Me.txtTiempoRespuesta.Text
                .Usuario = Me.Usuario.UserName
            End If
        End With

        If Me.ifArchivo.Visible Then
            If Me.ifArchivo.FileName <> String.Empty Then

                'If Me.ExisteArchivo Then
                '    Me.OficioAdjuntoOrdenTrabajo.NombreArchivo = Me.ifArchivo.FileName
                '    Me.OficioAdjuntoOrdenTrabajo.Archivo = Me.ifArchivo.FileBytes
                '    Me.OficioAdjuntoOrdenTrabajo.Usuario = Me.Usuario.UserName
                'Else
                Me.OficioAdjuntoOrdenTrabajo = New EntOttAdjuntoOrdenTrabajo
                With Me.OficioAdjuntoOrdenTrabajo
                    .IdUbicacion = Me.IdUbicacion
                    .IdOrdenTrabajo = Me.IdOrdenTrabajo
                    .NombreArchivo = Me.ifArchivo.FileName
                    .Archivo = Me.ifArchivo.FileBytes
                    .Usuario = Me.Usuario.UserName
                    .IdTipoDocumento = TipoDocumento.OFICIO
                    .IdEtapaOrdentrabajo = EtapasOrdenTrabajo.EVALUACION_PRELIMINAR_INFORME
                    .Descripcion = DescripcionAdjuntos.ADJUNTOS_OFICIO
                End With
                '  End If
            End If
        End If

        Return vlo_EntOttViabilidadTecnica
    End Function

    ''' <summary>
    ''' Administra el proceso para modificar una viabilidad tecnica
    ''' </summary>
    ''' <returns></returns>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>26/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Function Modificar() As Boolean
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        Dim vlo_EntOttViabilidadTecnica As Wsr_OT_OrdenesDeTrabajo.EntOttViabilidadTecnica

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        vlo_EntOttViabilidadTecnica = ConstruirRegistro()

        Try
            Return vlo_Ws_OT_OrdenesDeTrabajo.OTT_VIABILIDAD_TECNICA_ModificarViabilidadOficio(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
              vlo_EntOttViabilidadTecnica, IIf(Me.rbtAprobada.Checked, True, False), Me.txtJustificacion.Text, Me.Usuario.NumEmpleado, Me.OficioAdjuntoOrdenTrabajo, False) > 0
        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_OrdenesDeTrabajo IsNot Nothing Then
                vlo_Ws_OT_OrdenesDeTrabajo.Dispose()
            End If
        End Try
    End Function

    ''' <summary>
    ''' Carga el tamaño maximo permitido
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>26/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Function CargarTamañoMaximoArchivo() As Integer
        Dim vlo_Ws_OT_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos
        Dim vlo_EntOtmTipoDocumento As Wsr_OT_Catalogos.EntOtmTipoDocumento

        vlo_Ws_OT_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            vlo_EntOtmTipoDocumento = vlo_Ws_OT_Ws_OT_Catalogos.OTM_TIPO_DOCUMENTO_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1}", Modelo.OTM_TIPO_DOCUMENTO.ID_TIPO_DOCUMENTO, TipoDocumento.OFICIO))

            Return vlo_EntOtmTipoDocumento.TamanioMaximo()
        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_Ws_OT_Catalogos IsNot Nothing Then
                vlo_Ws_OT_Ws_OT_Catalogos.Dispose()
            End If
        End Try
    End Function

    ''' <summary>
    ''' Carga el extensiones permitidas
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>26/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Function CargarExtensionesArchivo() As String
        Dim vlo_Ws_OT_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos
        Dim vlo_EntOtmTipoDocumento As Wsr_OT_Catalogos.EntOtmTipoDocumento

        vlo_Ws_OT_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            vlo_EntOtmTipoDocumento = vlo_Ws_OT_Ws_OT_Catalogos.OTM_TIPO_DOCUMENTO_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1}", Modelo.OTM_TIPO_DOCUMENTO.ID_TIPO_DOCUMENTO, TipoDocumento.OFICIO))

            Return vlo_EntOtmTipoDocumento.FormatosAdmitidos
        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_Ws_OT_Catalogos IsNot Nothing Then
                vlo_Ws_OT_Ws_OT_Catalogos.Dispose()
            End If
        End Try
    End Function

    ''' <summary>
    ''' CArga una entidad de tipo adjunto
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>26/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Function CargaOficioAdjuntoOrdenTrabajo() As EntOttAdjuntoOrdenTrabajo
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            Return vlo_Ws_OT_OrdenesDeTrabajo.OTT_ADJUNTO_ORDEN_TRABAJO_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1} AND {2} = '{3}' AND {4} = {5}", Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.ID_UBICACION, Me.IdUbicacion, Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.ID_ORDEN_TRABAJO, Me.IdOrdenTrabajo, Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.ID_TIPO_DOCUMENTO, TipoDocumento.OFICIO))

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
