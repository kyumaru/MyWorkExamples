Imports Wsr_OT_Catalogos
Imports Utilerias.OrdenesDeTrabajo
Imports Wsr_OT_OrdenesDeTrabajo

''' <summary>
''' Clase que administra la inclusión y modificación de areas profesionales 
''' </summary>
''' <remarks></remarks>
''' <author>César Bermúdez García</author>
''' <creationDate>20/01/2016</creationDate>
Partial Class Catalogos_Frm_OT_AreaProfesional
    Inherits System.Web.UI.Page
#Region "Propiedades"
    ''' <summary>
    ''' Almacena la operación que el usuario desea efectuar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property operacion As eOperacion
        Get
            Return CType(ViewState("Operacion"), eOperacion)
        End Get
        Set(value As eOperacion)
            ViewState("Operacion") = value
        End Set
    End Property

    ''' <summary>
    ''' Almacena el valor actual del objeto a insertar/modificar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>20/01/2016</creationDate>
    Private Property AreaProfesional As EntOtmAreaProfesional
        Get
            Return CType(ViewState("AreaProfesional"), EntOtmAreaProfesional)
        End Get
        Set(value As EntOtmAreaProfesional)
            ViewState("AreaProfesional") = value
        End Set
    End Property

    ''' <summary>
    ''' propiedad para la ubicacion autorizada del usuario que desea registrar ordenes
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>23/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property AutorizadoUbicacion As Wsr_OT_Catalogos.EntOtmAutorizadoUbicacion
        Get
            Return CType(ViewState("AutorizadoUbicacion"), Wsr_OT_Catalogos.EntOtmAutorizadoUbicacion)
        End Get
        Set(value As Wsr_OT_Catalogos.EntOtmAutorizadoUbicacion)
            ViewState("AutorizadoUbicacion") = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para el usuario en sesion 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>23/02/2016</creationDate>
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
    ''' Inicializa los componentes del formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>20/01/2016</creationDate>
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
    ''' Este método se ejecutará al presionar el botón aceptar, dependiendo de la acción modificará o agregará y al final presentará un mensaje
    ''' Si la operación fue exitosa
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>20/01/2016</creationDate>
    Protected Sub btnAceptar_Click(sender As Object, e As EventArgs) Handles btnAceptar.Click
        If Page.IsValid Then
            Try
                Select Case Me.operacion
                    Case Is = eOperacion.Agregar

                        If Agregar() Then
                            WebUtils.RegistrarScript(Me, "RegistroExitoso", "mostrarPopupRegistroExitoso();")
                        Else
                            MostrarAlertaError("La Descripción o sufijo ingresados ya existen.")
                        End If
                    Case Is = eOperacion.Modificar
                        If Modificar() Then
                            WebUtils.RegistrarScript(Me, "ActualizacionExitosa", "mostrarAlertaActualizacionExitosa();")
                        Else
                            MostrarAlertaError("No ha sido posible actualizar la información del Area Profesional.")
                        End If

                End Select
            Catch ex As Exception
                If TypeOf (ex) Is System.Web.Services.Protocols.SoapException AndAlso
                CType(ex, System.Web.Services.Protocols.SoapException).Actor = OrdenesDeTrabajoException.NOMBRE_CLASE Then
                    Dim vlo_TallerCapacitacionException As OrdenesDeTrabajoException = OrdenesDeTrabajoException.GetFromSoapException(ex)
                    MostrarAlertaError(vlo_TallerCapacitacionException.Message)
                    WebUtils.RegistrarScript(Me, "OcultarAreaDeFiltros", "ocultarAreaFiltrosDeBusqueda();")
                Else
                    Dim vlo_ControlDeErrores As New ControlDeErrores
                    vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
                End If
            End Try
        End If
    End Sub
#End Region

#Region "Metodos"

    Private Sub CargarEstado()
        Me.ddlEstado.Items.Clear()
        Me.ddlEstado.Items.Add(New ListItem("Activo", Estado.ACTIVO))
        Me.ddlEstado.Items.Add(New ListItem("Inactivo", Estado.INACTIVO))

    End Sub

    ''' <summary>
    ''' Obtiene el registro desde la base de datos y lo carga en memoria
    ''' </summary>
    ''' <param name="pvc_IdArea"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>20/01/2016</creationDate>
    Private Sub CargarAreaProfesional(pvc_IdArea As String)
        Dim vlo_Ws_OT_Catalogos As Ws_OT_Catalogos

        vlo_Ws_OT_Catalogos = New Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            '{0} Nombre de la columna 
            '{1} numero de ID_AREA_PROFESIONAL

            Me.AreaProfesional = vlo_Ws_OT_Catalogos.OTM_AREA_PROFESIONAL_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("UPPER({0})={1}", Modelo.OTM_AREA_PROFESIONAL.ID_AREA_PROFESIONAL, pvc_IdArea.Trim.ToUpper))

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_Catalogos IsNot Nothing Then
                vlo_Ws_OT_Catalogos.Dispose()
            End If
        End Try

        If Me.AreaProfesional.Existe Then
            With Me.AreaProfesional
                Me.txtDescripcion.Text = .Descripcion
                Me.ddlEstado.SelectedValue = .Estado
                Me.txtSufijo.Text = .Sufijo
            End With
        Else
            WebUtils.RegistrarScript(Me, "MostrarAlertaLlaveIncorrrecta", "mostrarAlertaLlaveIncorrrecta();")
        End If


    End Sub

    ''' <summary>
    ''' Inicializa el formulario dependiendo de la operación recibida por parámetro
    ''' Tambien carga el drop down list de estados
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>20/01/2016</creationDate>
    Private Sub InicializarFormulario()
        Try
            CargarEstado()

            Me.Usuario = New UsuarioActual
            Me.AutorizadoUbicacion = CargarAutorizadoUbicacion(Me.Usuario.NumEmpleado)
            Me.operacion = WebUtils.LeerParametro(Of eOperacion)("pvn_Operacion")

            Select Case Me.operacion
                Case Is = eOperacion.Agregar
                    Me.lblAccion.Text = "Agregar Area Profesional"
                    Me.ddlEstado.Enabled = False
                Case Is = eOperacion.Modificar
                    Me.lblAccion.Text = "Modificar Area Profesional"
                    Me.ddlEstado.Enabled = True

                    Try
                        CargarAreaProfesional(WebUtils.LeerParametro(Of String)("pvc_IdArea"))
                    Catch ex As Exception
                        Throw
                    End Try
            End Select
        Catch ex As Exception

        End Try
    End Sub

    Private Sub MostrarAlertaError(pvc_Mensaje As String)
        WebUtils.RegistrarScript(Me, "alertaError", String.Format("mostrarAlertaError('{0}');", pvc_Mensaje))
    End Sub

#End Region

#Region "Funciones"

    ''' <summary>
    '''  Construye el registro para ser enviado a la base de datos
    ''' </summary>
    ''' <returns>Entidad de area profesional</returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>20/01/2016</creationDate>
    Private Function ConstruirRegistro() As EntOtmAreaProfesional
        Dim vlo_EntOtmAreaProfesional As EntOtmAreaProfesional

        If Me.operacion = eOperacion.Agregar Then
            vlo_EntOtmAreaProfesional = New EntOtmAreaProfesional
        ElseIf Me.operacion = eOperacion.Modificar Then
            vlo_EntOtmAreaProfesional = Me.AreaProfesional
        End If
        With vlo_EntOtmAreaProfesional
            .Descripcion = Me.txtDescripcion.Text.ToUpper.Trim
            .Estado = IIf(Me.operacion = eOperacion.Modificar, Me.ddlEstado.SelectedValue, Estado.ACTIVO)
            .Sufijo = txtSufijo.Text.Trim.ToUpper
            .Usuario = Me.Usuario.UserName
            .IdUbicacion = Me.AutorizadoUbicacion.IdUbicacionAdministra
        End With

        Return vlo_EntOtmAreaProfesional

    End Function

    ''' <summary>
    '''  Agrega un area profesional nueva a la tabla de areas profesionales
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>20/01/2016</creationDate>
    Private Function Agregar() As Boolean
        Dim vlo_Ws_OT_Catalogos As Ws_OT_Catalogos
        Dim vlo_EntOtmAreaProfesional As EntOtmAreaProfesional
        Dim vlo_EntidadOtmAreaProfesional As EntOtmAreaProfesional

        vlo_Ws_OT_Catalogos = New Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        vlo_EntOtmAreaProfesional = ConstruirRegistro()

        'Si la descripción o el sufijo está repetido no permite agregarlo
        '{0}: OTM_AREA_PROFESIONAL.DESCRIPCION,
        '{1}: Descripcion ingresada por el usuario
        '{2}: Modelo.OTM_AREA_PROFESIONAL.SUFIJO
        '{3}: Sufijo ingresado por el usuario

        vlo_EntidadOtmAreaProfesional = vlo_Ws_OT_Catalogos.OTM_AREA_PROFESIONAL_ObtenerRegistro(
               ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
               ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
               String.Format("UPPER({0}) = '{1}' OR UPPER({2}) = '{3}'", Modelo.OTM_AREA_PROFESIONAL.DESCRIPCION, vlo_EntOtmAreaProfesional.Descripcion.ToUpper(),
                                                            Modelo.OTM_AREA_PROFESIONAL.SUFIJO, vlo_EntOtmAreaProfesional.Sufijo.ToUpper))

        If Not vlo_EntidadOtmAreaProfesional.Existe Then
            Try
                Return vlo_Ws_OT_Catalogos.OTM_AREA_PROFESIONAL_InsertarRegistro(
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                    vlo_EntOtmAreaProfesional) > 0
            Catch ex As Exception
                Throw
            Finally
                If vlo_Ws_OT_Catalogos IsNot Nothing Then
                    vlo_Ws_OT_Catalogos.Dispose()
                End If
            End Try
        End If

        Return False
    End Function


    ''' <summary>
    ''' Modifica un registro en la tabla de espacios
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>20/01/2016</creationDate>
    Private Function Modificar() As Boolean
        Dim vlo_Ws_OT_Catalogos As Ws_OT_Catalogos
        Dim vlo_EntOtmAreaProfesional As EntOtmAreaProfesional
        Dim vlo_EntidadOtmAreaProfesional As EntOtmAreaProfesional

        vlo_Ws_OT_Catalogos = New Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        vlo_EntOtmAreaProfesional = ConstruirRegistro()

        Try
            'Si la descripción o el sufijo está repetido no permite agregarlo
            '{0}: OTM_AREA_PROFESIONAL.DESCRIPCION,
            '{1}: Descripcion ingresada por el usuario
            '{2}: Modelo.OTM_AREA_PROFESIONAL.SUFIJO
            '{3}: Sufijo ingresado por el usuario

            vlo_EntidadOtmAreaProfesional = vlo_Ws_OT_Catalogos.OTM_AREA_PROFESIONAL_ObtenerRegistro(
                   ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                   ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                   String.Format("UPPER({0}) = '{1}' OR UPPER({2}) = '{3}'", Modelo.OTM_AREA_PROFESIONAL.DESCRIPCION, vlo_EntOtmAreaProfesional.Descripcion.ToUpper(),
                                                            Modelo.OTM_AREA_PROFESIONAL.SUFIJO, vlo_EntOtmAreaProfesional.Sufijo.ToUpper))

            If vlo_EntidadOtmAreaProfesional.Existe Then
                If vlo_EntidadOtmAreaProfesional.IdAreaProfesional = vlo_EntOtmAreaProfesional.IdAreaProfesional Then
                    Return vlo_Ws_OT_Catalogos.OTM_AREA_PROFESIONAL_ModificarRegistro(
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                    vlo_EntOtmAreaProfesional) > 0
                End If
            End If

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_Catalogos IsNot Nothing Then
                vlo_Ws_OT_Catalogos.Dispose()
            End If
        End Try
        Return False
    End Function

    ''' <summary>
    ''' Carga una entidad de tipo autorizado ubicacion
    ''' </summary>
    ''' <param name="pvn_NumEmpleado"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>23/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Function CargarAutorizadoUbicacion(pvn_NumEmpleado As Integer) As Wsr_OT_Catalogos.EntOtmAutorizadoUbicacion
        Dim vlo_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos

        vlo_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            Return vlo_Ws_OT_Catalogos.OTM_AUTORIZADO_UBICACION_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1}", Modelo.OTM_AUTORIZADO_UBICACION.NUM_EMPLEADO, pvn_NumEmpleado))
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
