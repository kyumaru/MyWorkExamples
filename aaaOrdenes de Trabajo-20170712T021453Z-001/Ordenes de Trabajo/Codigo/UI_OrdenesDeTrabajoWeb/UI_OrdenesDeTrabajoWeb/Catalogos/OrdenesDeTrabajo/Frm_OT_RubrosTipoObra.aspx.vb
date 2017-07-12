Imports Utilerias.OrdenesDeTrabajo
Imports Wsr_OT_Catalogos

Partial Class Catalogos_Frm_OT_RubrosTipoObra
    Inherits System.Web.UI.Page

#Region "Propiedades"

    ''' <summary>
    ''' Propiedad para la operacion a realizar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>30/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property Operacion As eOperacion
        Get
            Return CType(ViewState("Operacion"), eOperacion)
        End Get
        Set(value As eOperacion)
            ViewState("Operacion") = value
        End Set
    End Property

    ''' <summary>
    ''' Entidad de tipo documento
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>30/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property TipoObra As EntOtmTipoObra
        Get
            Return CType(ViewState("TipoObra"), EntOtmTipoObra)
        End Get
        Set(value As EntOtmTipoObra)
            ViewState("TipoObra") = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para el usuario en session
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>30/03/2016</creationDate>
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
    ''' Evento que se ejecuta al cargar la página, inicializa los componentes necesarios
    ''' para el funcionamiento de la página
    ''' </summary>
    ''' <param name="sender">Parámetro propio del evento</param>
    ''' <param name="e">Parámetro propio del evento</param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>30/03/2016</creationDate>
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
    ''' Evento que se ejecuta cuando se da click en el boton aceptar para agregar un nuevo registro
    ''' llama a la funcion procesar y muestra un mensaje segun la operacion realizada
    ''' </summary>
    ''' <param name="sender">Parámetro propio del evento</param>
    ''' <param name="e">Parámetro propio del evento</param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>30/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnAceptar_Click(sender As Object, e As EventArgs) Handles btnAceptar.Click
        If Page.IsValid Then
            Try
                Select Case (Me.Operacion)
                    Case Is = eOperacion.Agregar
                        If Agregar() Then
                            WebUtils.RegistrarScript(Me.Page, "MostrarPopupRegistroExitoso", "mostrarPopupRegistroExitoso();")
                        Else
                            MostrarAlertaError("No ha sido posible almacenar la información del nuevo registro")
                        End If

                    Case Is = eOperacion.Modificar
                        If Modificar() Then
                            WebUtils.RegistrarScript(Me.Page, "MostrarAlertaActualizacionExitosa", "mostrarAlertaActualizacionExitosa();")
                        Else
                            MostrarAlertaError("No ha sido posible actualizar la información del Tipo de obra.")
                        End If
                End Select
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
    ''' <creationDate>30/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub MostrarAlertaError(pvc_Mensaje As String)
        WebUtils.RegistrarScript(Me, "alertaError", String.Format("mostrarAlertaError('{0}');", pvc_Mensaje))
    End Sub

    ''' <summary>
    ''' Carga el combo de estados
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>30/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarEstado()
        Me.ddlEstado.Items.Clear()
        Me.ddlEstado.Items.Add(New ListItem("Activo", Estado.ACTIVO))
        Me.ddlEstado.Items.Add(New ListItem("Inactivo", Estado.INACTIVO))
    End Sub

    ''' <summary>
    ''' Método encargado de inicializar el formulario segun la operacion a realizar
    ''' en caso de ser agregar carga el texo con "Agregar", en caso de ser modificar 
    ''' cargar el texto con "Modificar" y llama al método que obtiene los datos de la orden seleccionada segun el id 
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>30/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub InicializarFormulario()
        Me.Operacion = WebUtils.LeerParametro(Of eOperacion)("pvn_Operacion")
        CargarEstado()

        Select Case Me.Operacion
            Case Is = eOperacion.Agregar
                Me.lblAccion.Text = "Agregar Tipo de Obra"
            Case Is = eOperacion.Modificar
                Me.lblAccion.Text = "Modificar Tipo de Obra"
                Me.btnAceptar.Text = "Modificar"
                Try
                    CargarTipoObra(WebUtils.LeerParametro(Of Integer)("pvn_IdTipoObra"))
                Catch ex As Exception
                    Throw
                End Try

        End Select
    End Sub

    ''' <summary>
    ''' Obtiene el registro desde la base de datos y lo carga en memoria
    ''' </summary>
    ''' <param name="pvn_IdTipoObra"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>30/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarTipoObra(pvn_IdTipoObra As String)
        Dim vlo_Ws_OT_Catalogos As Ws_OT_Catalogos

        vlo_Ws_OT_Catalogos = New Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            Me.TipoObra = vlo_Ws_OT_Catalogos.OTM_TIPO_OBRA_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1}", Modelo.OTM_TIPO_OBRA.ID_TIPO_OBRA, pvn_IdTipoObra))

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_Catalogos IsNot Nothing Then
                vlo_Ws_OT_Catalogos.Dispose()
            End If
        End Try

        If Me.TipoObra.Existe Then
            With Me.TipoObra
                Me.txtDescripcion.Text = .Descripcion
                Me.ddlEstado.SelectedValue = .Estado
            End With
        Else
            WebUtils.RegistrarScript(Me, "MostrarAlertaLlaveIncorrrecta", "mostrarAlertaLlaveIncorrecta();")
        End If


    End Sub

#End Region

#Region "Funciones"

    ''' <summary>
    ''' Funcion encargada de contruir el registro  de tipo obra
    ''' </summary>
    ''' <returns>la convocatoria</returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>30/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Function ConstruirRegistro() As Wsr_OT_Catalogos.EntOtmTipoObra
        Dim vlo_EntOtmTipoObra As Wsr_OT_Catalogos.EntOtmTipoObra

        If Me.Operacion = eOperacion.Agregar Then
            vlo_EntOtmTipoObra = New Wsr_OT_Catalogos.EntOtmTipoObra
        Else
            vlo_EntOtmTipoObra = Me.TipoObra
        End If

        With vlo_EntOtmTipoObra
            .Descripcion = Me.txtDescripcion.Text
            .Estado = Me.ddlEstado.SelectedValue
            .Usuario = Me.Usuario.UserName
        End With

        Return vlo_EntOtmTipoObra
    End Function

    ''' <summary>
    ''' Administra el proceso para agregar un tipo obra
    ''' </summary>
    ''' <returns></returns>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>30/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Function Agregar() As Boolean
        Dim vlo_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos
        Dim vlo_EntOtmTipoObra As Wsr_OT_Catalogos.EntOtmTipoObra

        vlo_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        vlo_EntOtmTipoObra = ConstruirRegistro()

        Try
            Return vlo_Ws_OT_Catalogos.OTM_TIPO_OBRA_InsertarRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                vlo_EntOtmTipoObra) > 0
        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_Catalogos IsNot Nothing Then
                vlo_Ws_OT_Catalogos.Dispose()
            End If
        End Try
    End Function

    ''' <summary>
    ''' Administra el proceso para modificar un tipo obra
    ''' </summary>
    ''' <returns></returns>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>30/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Function Modificar() As Boolean
        Dim vlo_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos
        Dim vlo_EntOtmTipoObra As Wsr_OT_Catalogos.EntOtmTipoObra

        vlo_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        vlo_EntOtmTipoObra = ConstruirRegistro()

        Try
            Return vlo_Ws_OT_Catalogos.OTM_TIPO_OBRA_ModificarRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                vlo_EntOtmTipoObra) > 0
        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_Catalogos IsNot Nothing Then
                vlo_Ws_OT_Catalogos.Dispose()
            End If
        End Try
    End Function

#End Region

End Class
