Imports Utilerias.OrdenesDeTrabajo
Imports Wsr_OT_OrdenesDeTrabajo

Partial Class OrdenesDeTrabajo_Frm_OT_FinalizacionEntregaDis
    Inherits System.Web.UI.Page

#Region "Propiedades"

    ''' <summary>
    ''' Propiedad para el usuario en session
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>17/03/2016</creationDate>
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
    ''' Almacena la identificación del sector o taller a agregar el operario
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>17/03/2016</creationDate>
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
    ''' Almacena la identificación del sector o taller a agregar el operario
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>17/03/2016</creationDate>
    Private Property Anno As Integer
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
    ''' Es el identificador base de la Orden de trabajo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>17/03/2016</creationDate>
    Public Property IdOrdenTrabajo As String
        Get
            If ViewState("IdOrdenTrabajo") Is Nothing Then
                Return String.Empty
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
    ''' <creationDate>17/03/2016</creationDate>
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
    ''' Extensiones para archivo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>17/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Public Property ExtensionesPermitidas As String
        Get
            Return CType(ViewState("ExtensionesPermitidas"), String)
        End Get
        Set(value As String)
            ViewState("ExtensionesPermitidas") = value
        End Set
    End Property

    ''' <summary>
    ''' Tamaño maximo para el archivo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>17/03/2016</creationDate>
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
    ''' Almacena el archivo de clausula penal
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>17/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property ArchivoClausula As EntOttAdjuntoOrdenTrabajo
        Get
            Return CType(ViewState("ArchivoClausula"), EntOttAdjuntoOrdenTrabajo)
        End Get
        Set(value As EntOttAdjuntoOrdenTrabajo)
            ViewState("ArchivoClausula") = value
        End Set
    End Property

#End Region

#Region "Eventos"
    ''' <summary>
    ''' Evento que se ejecuta al cargar la pagina, inicializa la carga de datos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>17/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Try
                Me.Usuario = New UsuarioActual
                inicializar()
            Catch ex As Exception
                Dim vlo_ControlDeErrores As New ControlDeErrores
                vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
            End Try
        End If
    End Sub

    ''' <summary>
    ''' Agrega a la propiedad local los bytes y el nombre del archivo a guardar para la clausula penal
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>17/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnAgregarClausula_Click(sender As Object, e As EventArgs) Handles btnAgregarClausula.Click
        Try
            Me.ArchivoClausula.NombreArchivo = Me.fuClausula.FileName
            Me.ArchivoClausula.Archivo = Me.fuClausula.FileBytes
            fuClausula.Visible = False
            Me.lnkArchivoClausula.Text = Me.fuClausula.FileName
            btnAgregarClausula.Visible = False
            lnkArchivoClausula.Visible = True
            btnEliminarClausula.Visible = True
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' descarga el archivo adjunto
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez G</author>
    ''' <creationDate>17/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub lnkArchivoClausula_Command(sender As Object, e As CommandEventArgs)
        Try
            Response.Clear()
            Response.AppendHeader("content-disposition", "attachment; filename=" + Me.ArchivoClausula.NombreArchivo)
            Response.BinaryWrite(Me.ArchivoClausula.Archivo)
            Response.End()

        Catch ex_Descarga As System.Threading.ThreadAbortException
            Throw
        End Try
    End Sub


    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez G</author>
    ''' <creationDate>17/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        Try
            If Guardar(False) Then
                WebUtils.RegistrarScript(Me.Page, "mostrarAlertaGuardadoExitoso", "mostrarAlertaGuardadoExitoso();")
            Else
                MostrarAlertaError("No ha sido posible guardar la información del registro")
            End If
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez G</author>
    ''' <creationDate>17/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnGuardarYEnviar_Click(sender As Object, e As EventArgs) Handles btnGuardarYEnviar.Click
        Try
            If Guardar(True) Then
                WebUtils.RegistrarScript(Me.Page, "mostrarAlertaGuardadoExitoso", "mostrarAlertaGuardadoExitoso();")
            Else
                MostrarAlertaError("No ha sido posible guardar la información del registro")
            End If
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' Redirige al listado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez G</author>
    ''' <creationDate>18/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnRegresar_Click(sender As Object, e As EventArgs) Handles btnRegresar.Click
        Response.Redirect("Lst_OT_GestionProfesionalesDisenio.aspx")
    End Sub

    ''' <summary>
    ''' elimina el archivo aval foresta
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez G</author>
    ''' <creationDate>10/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnEliminarClausula_Click(sender As Object, e As ImageClickEventArgs) Handles btnEliminarClausula.Click
        Try

            Me.ArchivoClausula.Archivo = Nothing
            Me.ArchivoClausula.NombreArchivo = String.Empty
            Me.fuClausula.Visible = True
            Me.lnkArchivoClausula.Visible = False
            Me.btnEliminarClausula.Visible = False
            btnAgregarClausula.Visible = True

        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta cuando se da click sobre el link de desicion inicial
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>31/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub lnkDesInicial_Click(sender As Object, e As EventArgs) Handles lnkDesInicial.Click
        Try
            Me.Session.Add("pvn_IdUbicacion", Me.IdUbicacion)
            Me.Session.Add("pvc_IdOrdenTrabajo", Me.IdOrdenTrabajo)
            Me.Session.Add("pvn_IdSectorTaller", Me.IdSectorTaller)
            Me.Session.Add("pvn_Anno", Me.Anno)
            Me.Session.Add("pvo_Archivo", Me.ArchivoClausula)
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
        Response.Redirect("Frm_OT_DesicionInicial.aspx", False)
    End Sub


    ''' <summary>
    ''' Evento que se ejecuta cuando se da click sobre el link de expediente tecnico
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>01/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub lnkExpTecnico_Click(sender As Object, e As EventArgs) Handles lnkExpTecnico.Click
        Try
            Me.Session.Add("pvn_IdUbicacion", Me.IdUbicacion)
            Me.Session.Add("pvc_IdOrdenTrabajo", Me.IdOrdenTrabajo)
            Me.Session.Add("pvn_IdSectorTaller", Me.IdSectorTaller)
            Me.Session.Add("pvn_Anno", Me.Anno)
            Me.Session.Add("pvo_Archivo", Me.ArchivoClausula)
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
        Response.Redirect("Frm_ExpedienteTecnico.aspx", False)
    End Sub

#End Region

#Region "Metodos"

    ''' <summary>
    ''' Registra el script necesario para mostrar una alerta
    ''' </summary>
    ''' <param name="pvc_Mensaje">Mensaje a mostrar en la alerta</param>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>17/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub MostrarAlertaError(pvc_Mensaje As String)
        WebUtils.RegistrarScript(Me, "alertaError", String.Format("mostrarAlertaError('{0}');", pvc_Mensaje))
    End Sub


    ''' <summary>
    ''' Inicializa el formulario
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>17/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub inicializar()
        LeerParametros()
        inicializarControl()
        CargarAdjunto()
        CargarTiposDocumento()
    End Sub

    ''' <summary>
    ''' Lee los parametros de la sesion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>17/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub LeerParametros()
        Me.IdOrdenTrabajo = WebUtils.LeerParametro(Of String)("pvc_IdOrdenTrabajo")
        Me.IdUbicacion = WebUtils.LeerParametro(Of Integer)("pvn_IdUbicacion")
        Me.IdSectorTaller = WebUtils.LeerParametro(Of Integer)("pvn_IdSectorTaller")
        Me.Anno = WebUtils.LeerParametro(Of Integer)("pvn_Anno")
        Me.ArchivoClausula = WebUtils.LeerParametro(Of Wsr_OT_OrdenesDeTrabajo.EntOttAdjuntoOrdenTrabajo)("pvo_Archivo")
    End Sub

    ''' <summary>
    ''' inicializa control de usuario para información general de la OT
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>17/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub inicializarControl()
        'Asignación de Datos para generar el web user control de la información general
        ctrl_InfoGeneral.Anno = Me.Anno
        ctrl_InfoGeneral.IdOrdenTrabajo = Me.IdOrdenTrabajo
        ctrl_InfoGeneral.IdUbicacion = Me.IdUbicacion
        'Manda  llamar al control de información general de la OT
        Me.ctrl_InfoGeneral.Inicializar()
    End Sub

    ''' <summary>
    ''' Carga el adjunto de clausula en la propiedad local
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>17/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarAdjunto()
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Ws_OT_OrdenesDeTrabajo

        vlo_Ws_OT_OrdenesDeTrabajo = New Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            If Me.ArchivoClausula IsNot Nothing Then

                Me.lnkArchivoClausula.Text = Me.ArchivoClausula.NombreArchivo
                Me.lnkArchivoClausula.Visible = True
                Me.btnEliminarClausula.Visible = True
                Me.fuClausula.Visible = False
                Me.btnAgregarClausula.Visible = False

            Else
                Me.ArchivoClausula = vlo_Ws_OT_OrdenesDeTrabajo.OTT_ADJUNTO_ORDEN_TRABAJO_ObtenerRegistro(
                                        ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                                        ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                                        String.Format("{0} = {1} AND {2} = {3} AND {4} = '{5}'",
                                            Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.ID_TIPO_DOCUMENTO, TipoDocumento.CLAUSULA_PENAL,
                                            Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.ID_UBICACION, IdUbicacion,
                                            Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.ID_ORDEN_TRABAJO, IdOrdenTrabajo))

                If ArchivoClausula.Existe Then
                    Me.lnkArchivoClausula.Text = Me.ArchivoClausula.NombreArchivo
                    Me.lnkArchivoClausula.Visible = True
                    Me.btnEliminarClausula.Visible = True
                    Me.fuClausula.Visible = False
                    Me.btnAgregarClausula.Visible = False
                End If

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
    ''' Construye el registro de la clausula
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>17/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub construirRegistro()
        Try
            With Me.ArchivoClausula
                .Descripcion = "Clausula penal para contratación del proyecto"
                .IdTipoDocumento = TipoDocumento.CLAUSULA_PENAL
                .IdUbicacion = IdUbicacion
                .IdOrdenTrabajo = IdOrdenTrabajo
                .Usuario = Usuario.UserName
                .IdEtapaOrdentrabajo = EtapasOrdenTrabajo.PRESUPUESTO
            End With
        Catch ex As Exception

        End Try
    End Sub

    ''' <summary>
    ''' Carga las variables para validar el archivo 
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>17/03/2016</creationDate>
    Private Sub CargarTiposDocumento()
        Dim vlo_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos
        Dim vlo_EntOtmTipoDocumento As Wsr_OT_Catalogos.EntOtmTipoDocumento

        vlo_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            vlo_EntOtmTipoDocumento = vlo_Ws_OT_Catalogos.OTM_TIPO_DOCUMENTO_ObtenerRegistro(ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1}", Modelo.OTM_TIPO_DOCUMENTO.ID_TIPO_DOCUMENTO, TipoDocumento.CLAUSULA_PENAL))

            If vlo_EntOtmTipoDocumento.Existe Then
                Me.ExtensionesPermitidas = vlo_EntOtmTipoDocumento.FormatosAdmitidos
                imgExtensiones.Attributes.Add("title", String.Format("Extensiones permitidas:{0}", ExtensionesPermitidas.ToLower))
                Me.TamanoArchivo = vlo_EntOtmTipoDocumento.TamanioMaximo
            End If

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_Catalogos IsNot Nothing Then
                vlo_Ws_OT_Catalogos.Dispose()
            End If
        End Try
    End Sub

#End Region

#Region "Funciones"
    ''' <summary>
    ''' Funcion que guarda los datos de la entrega de diseño
    ''' </summary>
    ''' <param name="pvb_Enviar"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>17/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Function Guardar(pvb_Enviar As Boolean) As Boolean
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Ws_OT_OrdenesDeTrabajo
        Dim vlo_DsDatos As Data.DataSet

        vlo_Ws_OT_OrdenesDeTrabajo = New Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            construirRegistro()

            vlo_DsDatos = vlo_Ws_OT_OrdenesDeTrabajo.OTT_ADJUNTO_ORDEN_TRABAJO_ListarRegistrosLista(ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1} AND {2} = '{3}' AND {4} = 1",
                              Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.ID_UBICACION,
                              IdUbicacion,
                              Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.ID_ORDEN_TRABAJO,
                              IdOrdenTrabajo,
                              Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.EXPEDIENTE_TECNICO),
                          String.Empty, False, 0, 0)

            If vlo_DsDatos.Tables.Count > 0 AndAlso vlo_DsDatos.Tables(0).Rows.Count > 0 Then
                vlo_DsDatos = vlo_Ws_OT_OrdenesDeTrabajo.OTT_DESICION_INICIAL_ListarRegistrosLista(ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1} AND {2} = '{3}'",
                                Modelo.OTT_DESICION_INICIAL.ID_UBICACION,
                                IdUbicacion,
                                Modelo.OTT_DESICION_INICIAL.ID_ORDEN_TRABAJO,
                                IdOrdenTrabajo),
                                String.Empty, False, 0, 0)

                If vlo_DsDatos.Tables.Count > 0 AndAlso vlo_DsDatos.Tables(0).Rows.Count > 0 Then
                    Return vlo_Ws_OT_OrdenesDeTrabajo.OTT_ORDEN_TRABAJO_GuardarEntrega(
                        ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                        ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                        Me.ArchivoClausula, pvb_Enviar) > 0
                Else
                    MostrarAlertaError("No ha sido posible guardar la información ya que no se cuenta con información completada para la desición inicial")
                End If

            Else
                MostrarAlertaError("No ha sido posible guardar la información ya que no se cuenta con archivos en el expediente técnico")
            End If



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
