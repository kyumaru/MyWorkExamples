Imports Wsr_OT_Catalogos
Imports Utilerias.OrdenesDeTrabajo
Partial Class Catalogos_Frm_OT_MotivoRechazo
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

    Private Property Motivo As EntOtmMotivoRechazo
        Get
            Return CType(ViewState("Motivo"), EntOtmMotivoRechazo)
        End Get
        Set(value As EntOtmMotivoRechazo)
            ViewState("Motivo") = value
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
                            MostrarAlertaError("No a sido posible registrar el nuevo motivo de rechazo.")
                        End If
                    Case Is = eOperacion.Modificar
                        If Modificar() Then
                            WebUtils.RegistrarScript(Me, "MostrarAlertaActualizacionExitosa", "mostrarAlertaActualizacionExitosa();")
                        Else
                            MostrarAlertaError("No a sido posible actualizar la información del nuevo motivo de rechazo.")
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
        CargarEstado()
        Me.Operacion = WebUtils.LeerParametro(Of eOperacion)("pvn_Operacion")
        Select Case Me.Operacion
            Case Is = eOperacion.Agregar
                Me.lblAccion.Text = "Agregar Motivo de Rechazo"
                'Me.ddlEstado.Attributes.Add("style", "display:none;") 'ocultar etiqueta
                'Me.trEstado.Attributes.Add("style", "display:none;")
                Me.trEstado.Visible = False

            Case Is = eOperacion.Modificar
                Me.lblAccion.Text = "Modificar Motivo de Rechazo"
                Me.trEstado.Visible = True
                ' Me.trEstado.Attributes.Add("style", "display:block;")
                'Me.ddlEstado.Attributes.Add("style", "display:block;") 'mostrar texto
                Try
                    CargarMotivoRechazo(WebUtils.LeerParametro(Of String)("pvc_IdMotivo"))
                Catch ex As Exception
                    Throw
                End Try
        End Select
    End Sub

    ''' <summary>
    ''' Obtiene el registro por medio del parametro pvc_IdUbicacion
    ''' </summary>
    ''' <param name="pvc_IdMotivo"></param>
    ''' <remarks></remarks>
    Private Sub CargarMotivoRechazo(pvc_IdMotivo As String)
        Dim vlo_Ws_OT_Catalogos As Ws_OT_Catalogos

        vlo_Ws_OT_Catalogos = New Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials
        '0 columna, 1 valor busqueda
        Try
            Me.Motivo = vlo_Ws_OT_Catalogos.OTM_MOTIVO_RECHAZO_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("UPPER({0}) = '{1}'", Modelo.OTM_MOTIVO_RECHAZO.ID_MOTIVO_RECHAZO, pvc_IdMotivo.Trim.ToUpper))   'verificar el nombre tambien?
        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_Catalogos IsNot Nothing Then
                vlo_Ws_OT_Catalogos.Dispose()
            End If
        End Try
        If Me.Motivo.Existe Then
            With Me.Motivo

                Me.txtDescripcion.Text = .Descripcion
                Me.ddlEstado.SelectedValue = .Estado

            End With
        Else
            WebUtils.RegistrarScript(Me, "MostrarAlertaLlaveIncorrecta", "mostrarAlertaLlaveIncorrecta();")
        End If
    End Sub

    Private Sub MostrarAlertaError(pvc_Mensaje As String)
        WebUtils.RegistrarScript(Me, "alertaError", String.Format("mostrarAlertaError('{0}');", pvc_Mensaje))
    End Sub

    Private Sub CargarEstado()

        Me.ddlEstado.Items.Clear()
        Me.ddlEstado.Items.Add(New ListItem(Constantes.FORMATO_DDL_SELECCIONE, String.Empty))
        Me.ddlEstado.Items.Add(New ListItem("Activo", Estado.ACTIVO))
        Me.ddlEstado.Items.Add(New ListItem("Inactivo", Estado.INACTIVO))
        Me.ddlEstado.SelectedValue = Estado.ACTIVO


    End Sub
#End Region

#Region "Funciones"

    ''' <summary>
    ''' Construye registro para insertar a la base de datos con los datos del usuario
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ConstruirRegistro() As EntOtmMotivoRechazo
        Dim vlo_EntOtmMotivoRechazo As EntOtmMotivoRechazo


        If Me.Operacion = eOperacion.Agregar Then
            vlo_EntOtmMotivoRechazo = New EntOtmMotivoRechazo
        ElseIf Me.Operacion = eOperacion.Modificar Then
            vlo_EntOtmMotivoRechazo = Me.Motivo
        End If

        With vlo_EntOtmMotivoRechazo


            .Descripcion = Me.txtDescripcion.Text.Trim
            .Estado = Me.ddlEstado.SelectedValue
            .Usuario = New UsuarioActual().UserName


        End With
        Return vlo_EntOtmMotivoRechazo
    End Function

    ''' <summary>
    ''' Ingresa nuevo registro en la DB
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function Agregar() As Boolean
        Dim vlo_Ws_OT_Catalogos As Ws_OT_Catalogos
        Dim vlo_EntOtmMotivoRechazo As EntOtmMotivoRechazo

        vlo_Ws_OT_Catalogos = New Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        vlo_EntOtmMotivoRechazo = ConstruirRegistro()

        Try
            Return vlo_Ws_OT_Catalogos.OTM_MOTIVO_RECHAZO_InsertarRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                vlo_EntOtmMotivoRechazo) > 0
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
        Dim vlo_EntOtmMotivoRechazo As EntOtmMotivoRechazo

        vlo_Ws_OT_Catalogos = New Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        vlo_EntOtmMotivoRechazo = ConstruirRegistro()

        Try
            Return vlo_Ws_OT_Catalogos.OTM_MOTIVO_RECHAZO_ModificarRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                vlo_EntOtmMotivoRechazo) > 0
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
