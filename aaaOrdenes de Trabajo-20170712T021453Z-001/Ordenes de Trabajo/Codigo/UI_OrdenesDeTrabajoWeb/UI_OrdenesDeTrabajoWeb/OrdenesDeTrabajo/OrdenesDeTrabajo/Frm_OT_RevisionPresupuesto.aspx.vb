Imports Wsr_OT_OrdenesDeTrabajo
Imports Utilerias.OrdenesDeTrabajo

''' <summary>
''' 
''' </summary>
''' <remarks></remarks>
''' <author>César Bermudez Garcia</author>
''' <creationDate>07/4/2016</creationDate>
''' <changeLog></changeLog>
Partial Class OrdenesDeTrabajo_Frm_OT_RevisionPresupuesto
    Inherits System.Web.UI.Page


#Region "Propiedades"

    ''' <summary>
    ''' Extensiones para archivo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>07/4/2016</creationDate>
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
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>07/4/2016</creationDate>
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
    ''' Propiedad para el usuario en session
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>07/4/2016</creationDate>
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
    ''' Almacena la cantidad de dias restantes para que la orden se de por liquidada
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>07/4/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property DiasRestantes As Integer
        Get
            If ViewState("DiasRestantes") Is Nothing Then
                Return 0
            End If
            Return CType(ViewState("DiasRestantes"), Integer)
        End Get
        Set(value As Integer)
            ViewState("DiasRestantes") = value
        End Set
    End Property

    ''' <summary>
    ''' Es el identificador base de la Orden de trabajo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>07/4/2016</creationDate>
    ''' <changeLog></changeLog>
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
    ''' <creationDate>07/4/2016</creationDate>
    ''' <changeLog></changeLog>
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
    ''' Guarda la ubicación de la OT
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>07/4/2016</creationDate>
    ''' <changeLog></changeLog>
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
    ''' Almacena el archivo adjunto de oficio para la respuesta
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>07/4/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property Archivo As EntOttAdjuntoOrdenTrabajo
        Get
            Return CType(ViewState("Archivo"), EntOttAdjuntoOrdenTrabajo)
        End Get
        Set(value As EntOttAdjuntoOrdenTrabajo)
            ViewState("Archivo") = value
        End Set
    End Property

#End Region

#Region "Eventos"
    ''' <summary>
    ''' Evento que inicializa el estado de la pantalla.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>07/4/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Try
                Me.Usuario = New UsuarioActual()
                InicializarFormulario()
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
    ''' <creationDate>07/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnAgregarOficio_Click(sender As Object, e As EventArgs) Handles btnAgregarOficio.Click
        Try
            Me.Archivo.NombreArchivo = Me.fuOficio.FileName
            Me.Archivo.Archivo = Me.fuOficio.FileBytes
            fuOficio.Visible = False
            Me.lnkArchivoOficio.Text = Me.fuOficio.FileName
            btnAgregarOficio.Visible = False
            lnkArchivoOficio.Visible = True
            btnEliminarOficio.Visible = True
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
    ''' <creationDate>07/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub lnkArchivoOficio_Command(sender As Object, e As CommandEventArgs)
        Try
            Response.Clear()
            Response.AppendHeader("content-disposition", "attachment; filename=" + Me.Archivo.NombreArchivo)
            Response.BinaryWrite(Me.Archivo.Archivo)
            Response.End()

        Catch ex_Descarga As System.Threading.ThreadAbortException
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' elimina el archivo 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez G</author>
    ''' <creationDate>07/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnEliminarOficio_Click(sender As Object, e As ImageClickEventArgs) Handles btnEliminarOficio.Click
        Try

            Me.Archivo.Archivo = Nothing
            Me.Archivo.NombreArchivo = String.Empty
            Me.fuOficio.Visible = True
            Me.lnkArchivoOficio.Visible = False
            Me.btnEliminarOficio.Visible = False
            btnAgregarOficio.Visible = True

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
    ''' <creationDate>07/06/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnRegresar_Click(sender As Object, e As EventArgs) Handles btnRegresar.Click
        Response.Redirect("Lst_OT_OrdenTrabajo.aspx")
    End Sub

    ''' <summary>
    ''' Guarda y envia la respuesta del solicitante, con esta acción se envian correos al coordinador
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez G</author>
    ''' <creationDate>07/06/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnGuardarYEnviar_Click(sender As Object, e As EventArgs) Handles btnGuardarYEnviar.Click
        Try
            If IsValid AndAlso Guardar() Then
                WebUtils.RegistrarScript(Me.Page, "mostrarAlertaGuardadoExitoso", "mostrarAlertaGuardadoExitoso();")
            Else
                MostrarAlertaError("No ha sido posible guardar la información del registro")
            End If
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
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
    ''' Inicializa el formulario y sus componentes
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>07/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub InicializarFormulario()
        Me.Archivo = New EntOttAdjuntoOrdenTrabajo
        CargarParametrosArchivo()
        LeerParametros()
        InicializarControlUsuario()
        Me.lblDiasRestantes.Text = "Dias restantes para responder: " & DiasRestantes & "<br />" & "<b>*Una vez vencido el plazo la orden de trabajo se dará por liquidada</b>" & "<br />"
    End Sub

    ''' <summary>
    ''' Lee los parámetros de la sesion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>07/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub LeerParametros()
        Me.IdOrdenTrabajo = WebUtils.LeerParametro(Of String)("pvc_IdOrdenTrabajo")
        Me.IdUbicacion = WebUtils.LeerParametro(Of Integer)("pvn_IdUbicacion")
        Me.Anno = WebUtils.LeerParametro(Of Integer)("pvn_Anno")
        Me.DiasRestantes = WebUtils.LeerParametro(Of Integer)("pvn_DiasRestantes")
    End Sub

    ''' <summary>
    ''' Inicializa el control de usuario para mostrar la información general
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>11/02/2016</creationDate>
    Private Sub InicializarControlUsuario()
        'Asignación de Datos para generar el web user control de la información general


        ctrl_InfoGeneral.Anno = Me.Anno
        ctrl_InfoGeneral.IdOrdenTrabajo = Me.IdOrdenTrabajo
        ctrl_InfoGeneral.IdUbicacion = Me.IdUbicacion

        'Manda  llamar al control de información general de la OT
        Me.ctrl_InfoGeneral.Inicializar()

    End Sub

    ''' <summary>
    ''' Carga el tamaño maximo permito
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermudez G</author>
    ''' <creationDate>07/04/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarParametrosArchivo()
        Dim vlo_Ws_OT_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos
        Dim vlo_EntOtmTipoDocumento As Wsr_OT_Catalogos.EntOtmTipoDocumento
        Dim vln_TamanoBytesMega As Integer

        vlo_Ws_OT_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            vlo_EntOtmTipoDocumento = vlo_Ws_OT_Ws_OT_Catalogos.OTM_TIPO_DOCUMENTO_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1}", Modelo.OTM_TIPO_DOCUMENTO.ID_TIPO_DOCUMENTO, TipoDocumento.OFICIO))

            vln_TamanoBytesMega = 1048576
            Me.TamanoArchivo = vlo_EntOtmTipoDocumento.TamanioMaximo * vln_TamanoBytesMega
            Me.ExtensionesPermitidas = vlo_EntOtmTipoDocumento.FormatosAdmitidos
            imgExtensiones.Attributes.Add("title", String.Format("Extensiones permitidas:{0}", ExtensionesPermitidas.ToLower))

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_Ws_OT_Catalogos IsNot Nothing Then
                vlo_Ws_OT_Ws_OT_Catalogos.Dispose()
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
            With Me.Archivo
                .Descripcion = "Oficio de respuesta a la jefatura de la sección de mantenimiento y construcción por parte del solicitante con respecto al informe de valoración presupuestaria"
                .IdTipoDocumento = TipoDocumento.OFICIO
                .IdUbicacion = IdUbicacion
                .IdOrdenTrabajo = IdOrdenTrabajo
                .Usuario = Usuario.UserName
                .IdEtapaOrdentrabajo = EtapasOrdenTrabajo.PRESUPUESTO
            End With
        Catch ex As Exception

        End Try
    End Sub

#End Region

#Region "Funciones"
    ''' <summary>
    ''' Funcion que guarda los datos de la entrega de diseño
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>17/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Function Guardar() As Boolean
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Ws_OT_OrdenesDeTrabajo

        vlo_Ws_OT_OrdenesDeTrabajo = New Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            construirRegistro()
            Dim vlo_enviar As Boolean = IIf(Me.rbtnPresupuesto.SelectedValue = "1", True, False)

            If Me.Archivo.Archivo IsNot Nothing Then

                Return vlo_Ws_OT_OrdenesDeTrabajo.OTT_INFORME_PRESUPUESTO_GuardarAprobacionPresupuesto(
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                    Me.Archivo, vlo_enviar, Roles.GetUsersInRole(Utilerias.OrdenesDeTrabajo.RolesSistema.OT_JEFE_SECCION)) > 0

            Else
                MostrarAlertaError("Debe agregar un archivo de respuesta a la jefatura de mantenimiento y construcción con respecto al informe de valoración presupuestaria.")
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
