Imports Wsr_OT_Catalogos
Imports Utilerias.OrdenesDeTrabajo
Partial Class Catalogos_Frm_OT_Parametros
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

    Private Property Parametro As EntOtpParametroUbicacion
        Get
            Return CType(ViewState("Parametro"), EntOtpParametroUbicacion)
        End Get
        Set(value As EntOtpParametroUbicacion)
            ViewState("Parametro") = value
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

            Case Is = eOperacion.Modificar
                Me.lblAccion.Text = "Modificar Parámetro del Sistema"
                ' Me.trEstado.Attributes.Add("style", "display:block;")
                'Me.ddlEstado.Attributes.Add("style", "display:block;") 'mostrar texto
                Try
                    CargarParametro(WebUtils.LeerParametro(Of String)("pvc_IdParametro"), WebUtils.LeerParametro(Of Integer)("pvn_IdUbicacion"))
                Catch ex As Exception
                    Throw
                End Try
        End Select
    End Sub

    ''' <summary>
    ''' Obtiene el registro por medio del parametro pvc_IdUbicacion y pvn_IdParametro
    ''' </summary>
    ''' <param name="pvn_IdParametro"></param>
    ''' <param name="pvn_IdUbicacion"></param>
    ''' <remarks></remarks>
    Private Sub CargarParametro(pvn_IdParametro As String, pvn_IdUbicacion As Integer)
        Dim vlo_Ws_OT_Catalogos As Ws_OT_Catalogos

        vlo_Ws_OT_Catalogos = New Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials
        '0 columna, 1 valor busqueda
        Try
            Me.Parametro = vlo_Ws_OT_Catalogos.OTP_PARAMETRO_UBICACION_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1} AND {2} = {3}", Modelo.OTP_PARAMETRO_UBICACION.ID_PARAMETRO, pvn_IdParametro, Modelo.OTP_PARAMETRO_UBICACION.ID_UBICACION_ADMINISTRA, pvn_IdUbicacion))   'verificar el nombre tambien?
        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_Catalogos IsNot Nothing Then
                vlo_Ws_OT_Catalogos.Dispose()
            End If
        End Try
        If Me.Parametro.Existe Then
            With Me.Parametro

                Me.txtDescripcion.Text = .Descripcion
                If .ValorDecimal = 0 Then
                    Me.chkValorDecimal.Checked = False
                    Me.txtValor.Text = .Valor
                Else
                    Me.chkValorDecimal.Checked = True
                    Me.txtValor.Text = .ValorDecimal
                End If

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
    Private Function ConstruirRegistro() As EntOtpParametroUbicacion
        Dim vlo_EntOtpParametro As EntOtpParametroUbicacion

        If Me.Operacion = eOperacion.Modificar Then
            vlo_EntOtpParametro = Me.Parametro
        End If

        With vlo_EntOtpParametro


            .Descripcion = Me.txtDescripcion.Text.Trim
            If chkValorDecimal.Checked = True Then
                .Valor = "-"
                .ValorDecimal = Me.txtValor.Text
            Else
                .Valor = Me.txtValor.Text
                .ValorDecimal = 0
            End If

            '.Usuario = New UsuarioActual().UserName 


        End With
        Return vlo_EntOtpParametro
    End Function

    ''' <summary>
    ''' Modifica registro en la DB
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function Modificar() As Boolean
        Dim vlo_Ws_OT_Catalogos As Ws_OT_Catalogos
        Dim vlo_EntOtpParametro As EntOtpParametroUbicacion

        vlo_Ws_OT_Catalogos = New Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        vlo_EntOtpParametro = ConstruirRegistro()

        Try
            Return vlo_Ws_OT_Catalogos.OTP_PARAMETRO_UBICACION_ModificarRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                vlo_EntOtpParametro) > 0
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
