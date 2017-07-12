Imports Wsr_OT_Catalogos
Imports Utilerias.OrdenesDeTrabajo
Partial Class Catalogos_Frm_OT_TipoOrdenTrabajo
    Inherits System.Web.UI.Page
#Region "Propiedades"
    Private Property Operacion As eOperacion
        Get
            Return CType(ViewState("Operacion"), eOperacion)
        End Get
        Set(value As eOperacion)
            ViewState("Operacion") = value
        End Set
    End Property

    Private Property TipoOrden As EntOtcTipoOrdenTrabajo
        Get
            Return CType(ViewState("TipoOrden"), EntOtcTipoOrdenTrabajo)
        End Get
        Set(value As EntOtcTipoOrdenTrabajo)
            ViewState("TipoOrden") = value
        End Set
    End Property
#End Region

#Region "Eventos"
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
    ''' Evento click del boton aceptar, ingresa o modifica dependiendo de la opcion seleccionada
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnAceptar_Click(sender As Object, e As EventArgs) Handles btnAceptar.Click
        If Page.IsValid Then 'preguntar cuando hay validadores en el codigo
            Try
                Select Case Me.Operacion
                    Case Is = eOperacion.Agregar
                        If Agregar() Then
                            WebUtils.RegistrarScript(Me, "MostrarPopupRegistroExitoso", "mostrarPopupRegistroExitoso();")
                        Else
                            MostrarAlertaError("No a sido posible registrar la nueva ubicación.")
                        End If
                    Case Is = eOperacion.Modificar
                        If Modificar() Then
                            WebUtils.RegistrarScript(Me, "MostrarAlertaActualizacionExitosa", "mostrarAlertaActualizacionExitosa();")
                        Else
                            MostrarAlertaError("No a sido posible actualizar la información de la ubicación.")
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

#Region "Metodos"

    ''' <summary>
    ''' Carga combo de Estado y Pertenece a sede, ademas oculta Estado cuando esta en modo insertar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InicializarFormulario()
        Me.Operacion = WebUtils.LeerParametro(Of eOperacion)("pvn_Operacion")
        Select Case Me.Operacion
            Case Is = eOperacion.Agregar
                Me.lblAccion.Text = "Agregar Tipo de Orden de Trabajo"
                'Me.ddlEstado.Attributes.Add("style", "display:none;") 'ocultar etiqueta
                'Me.trEstado.Attributes.Add("style", "display:none;")
                Me.txtCodigo.ReadOnly = False

            Case Is = eOperacion.Modificar
                Me.lblAccion.Text = "Modificar Tipo de Orden de Trabajo"

                ' Me.trEstado.Attributes.Add("style", "display:block;")
                'Me.ddlEstado.Attributes.Add("style", "display:block;") 'mostrar texto
                Try
                    CargarTipoOrden(WebUtils.LeerParametro(Of String)("pvc_IdTipoOrden"))
                Catch ex As Exception
                    Throw
                End Try
        End Select
    End Sub

    ''' <summary>
    ''' Obtiene el registro por medio del parametro pvc_IdUbicacion
    ''' </summary>
    ''' <param name="pvc_IdTipoOrden"></param>
    ''' <remarks></remarks>
    Private Sub CargarTipoOrden(pvc_IdTipoOrden As String)
        Dim vlo_Ws_OT_Catalogos As Ws_OT_Catalogos

        vlo_Ws_OT_Catalogos = New Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials
        '0 columna, 1 valor busqueda
        Try
            Me.TipoOrden = vlo_Ws_OT_Catalogos.OTC_TIPO_ORDEN_TRABAJO_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("UPPER({0}) = '{1}'", Modelo.OTC_TIPO_ORDEN_TRABAJO.TIPO_ORDEN_TRABAJO, pvc_IdTipoOrden.Trim.ToUpper))   'verificar el nombre tambien?
        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_Catalogos IsNot Nothing Then
                vlo_Ws_OT_Catalogos.Dispose()
            End If
        End Try
        If Me.TipoOrden.Existe Then
            With Me.TipoOrden
                Me.txtCodigo.Text = .TipoOrdenTrabajo
                Me.txtDescripcion.Text = .Descripcion

            End With
        Else
            WebUtils.RegistrarScript(Me, "MostrarAlertaLlaveIncorrecta", "mostrarAlertaLlaveIncorrecta();")
        End If
    End Sub

    Private Sub MostrarAlertaError(pvc_Mensaje As String)
        WebUtils.RegistrarScript(Me, "alertaError", String.Format("mostrarAlertaError('{0}');", pvc_Mensaje))
    End Sub

#End Region

#Region "Funciones"

    ''' <summary>
    ''' Construye registro para insertar a la base de datos con los datos del usuario
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ConstruirRegistro() As EntOtcTipoOrdenTrabajo
        Dim vlo_EntOtcTipoOrdenTrabajo As EntOtcTipoOrdenTrabajo


        If Me.Operacion = eOperacion.Agregar Then
            vlo_EntOtcTipoOrdenTrabajo = New EntOtcTipoOrdenTrabajo
        ElseIf Me.Operacion = eOperacion.Modificar Then
            vlo_EntOtcTipoOrdenTrabajo = Me.TipoOrden
        End If

        With vlo_EntOtcTipoOrdenTrabajo

            .TipoOrdenTrabajo = Me.txtCodigo.Text.Trim
            .Descripcion = Me.txtDescripcion.Text.Trim
            '.Usuario = New UsuarioActual().UserName 


        End With
        Return vlo_EntOtcTipoOrdenTrabajo
    End Function

    ''' <summary>
    ''' Ingresa nuevo registro en la DB
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function Agregar() As Boolean
        Dim vlo_Ws_OT_Catalogos As Ws_OT_Catalogos
        Dim vlo_EntOtcTipoOrdenTrabajo As EntOtcTipoOrdenTrabajo

        vlo_Ws_OT_Catalogos = New Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        vlo_EntOtcTipoOrdenTrabajo = ConstruirRegistro()

        Try
            Return vlo_Ws_OT_Catalogos.OTC_TIPO_ORDEN_TRABAJO_InsertarRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                vlo_EntOtcTipoOrdenTrabajo) > 0
        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_Catalogos IsNot Nothing Then
                vlo_Ws_OT_Catalogos.Dispose()
            End If
        End Try
    End Function

    ''' <summary>
    ''' Modifica registro en la DB
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function Modificar() As Boolean
        Dim vlo_Ws_OT_Catalogos As Ws_OT_Catalogos
        Dim vlo_EntOtcTipoOrdenTrabajo As EntOtcTipoOrdenTrabajo

        vlo_Ws_OT_Catalogos = New Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        vlo_EntOtcTipoOrdenTrabajo = ConstruirRegistro()

        Try
            Return vlo_Ws_OT_Catalogos.OTC_TIPO_ORDEN_TRABAJO_ModificarRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                vlo_EntOtcTipoOrdenTrabajo) > 0
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
