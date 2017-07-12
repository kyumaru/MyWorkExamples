Partial Class Genericos_Frm_ControlDeErrores
    Inherits System.Web.UI.Page

#Region "Propiedades"
    Public Property DetalleDelError() As String
        Get
            Return CType(ViewState("DetalleDelError"), String)
        End Get
        Set(ByVal value As String)
            ViewState("DetalleDelError") = value
        End Set
    End Property
#End Region

#Region "Eventos"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            InicializarFormulario()
        End If
    End Sub

    Protected Sub btnNotificarError_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNotificarError.Click
        Try
            Notificar()
        Catch ex As Exception
            MostrarMensaje("Se ha presentado un error durante el envío de la notificación. Por favor contacte al administrador del sistema", False)
        End Try
    End Sub
#End Region

#Region "Métodos"
    Private Sub InicializarFormulario()
        Dim vlo_ControlDeErrores As New ControlDeErrores

        Me.DetalleDelError = vlo_ControlDeErrores.ObtenerDetalleDelError

        If CType(ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_MOSTRAR_DETALLE_ERROR), Boolean) Then
            Me.lblError.Text = Me.DetalleDelError
            Me.lblError.Visible = True
        Else
            Me.lblError.Visible = False
            Me.lblError.Text = String.Empty
        End If
    End Sub

    Private Sub Notificar()
        Dim vlo_WsGestorNotificaciones As Wsr_GN_GestorNotificaciones.wsGestorNotificaciones
        Dim vlo_Sistema As Wsr_GN_GestorNotificaciones.EntGNM_SISTEMA
        Dim vlo_Notificacion As Wsr_GN_GestorNotificaciones.EntGNT_NOTIFICACION
        Dim vlo_ListaDestinatario As List(Of Wsr_GN_GestorNotificaciones.EntGNT_DESTINATARIO)
        Dim vlo_EntGNT_DESTINATARIO As Wsr_GN_GestorNotificaciones.EntGNT_DESTINATARIO
        Dim vlc_CorreoAdministrador As String
        Dim vlo_ListaCorreoAdministrador As String()

        Try
            vlo_WsGestorNotificaciones = New Wsr_GN_GestorNotificaciones.wsGestorNotificaciones
            vlo_WsGestorNotificaciones.Timeout = -1
            vlo_WsGestorNotificaciones.Credentials = System.Net.CredentialCache.DefaultCredentials

            vlo_Sistema = vlo_WsGestorNotificaciones.GNM_SISTEMA_ObtenerPorNombre(ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB), ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB), ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_NOMBRE_APLICACION_EN_GESTOR_DE_NOTIFICACIONES))

            If vlo_Sistema IsNot Nothing AndAlso vlo_Sistema.Existe Then
                'configurar la notificación
                vlo_Notificacion = New Wsr_GN_GestorNotificaciones.EntGNT_NOTIFICACION()
                vlo_Notificacion.ASUNTO = String.Format("{0}: Notificación de Error {1}", ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_NOMBRE_DE_LA_APLICACION), DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"))
                vlo_Notificacion.CUERPO = Me.DetalleDelError.Replace("<br />", vbCrLf).Replace("<hr />", "/*---------------------------------------------------------------------------*/")
                vlo_Notificacion.ES_HTML = 0
                vlo_Notificacion.USUARIO_CREA = System.Environment.UserName.Trim()

                'Generar destinatario
                vlo_ListaDestinatario = New List(Of Wsr_GN_GestorNotificaciones.EntGNT_DESTINATARIO)
                vlo_ListaCorreoAdministrador = ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CORREO_DEL_ADMINISTRADOR).Split(";")
                For Each vlc_CorreoAdministrador In vlo_ListaCorreoAdministrador
                    vlo_EntGNT_DESTINATARIO = New Wsr_GN_GestorNotificaciones.EntGNT_DESTINATARIO()
                    vlo_EntGNT_DESTINATARIO.DESTINATARIO = vlc_CorreoAdministrador
                    vlo_ListaDestinatario.Add(vlo_EntGNT_DESTINATARIO)
                Next

                vlo_WsGestorNotificaciones.GNT_NOTIFICACION_Registrar(ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB), ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB), vlo_Sistema, vlo_Notificacion, (New List(Of Wsr_GN_GestorNotificaciones.EntGNT_ARCHIVO_ADJUNTO)).ToArray(), vlo_ListaDestinatario.ToArray())

                MostrarMensaje("Se ha notificado al administrador del sistema sobre el error ocurrido.", True)
                Me.btnNotificarError.Enabled = False
            Else
                MostrarMensaje("El sistema no posee configuración asociada en el Gestor de Notificaciones", False)
            End If
        Catch ex As Exception
            Throw
        Finally
            If vlo_WsGestorNotificaciones IsNot Nothing Then
                vlo_WsGestorNotificaciones.Dispose()
            End If
        End Try
    End Sub

    Private Sub MostrarMensaje(ByVal pvc_Mensaje As String, pvb_Redireccionar As Boolean)
        WebUtils.RegistrarScript(Me.Page, "MensajeAlerta", String.Format("javascript:mostrarMensaje('{0}', {1});", pvc_Mensaje, IIf(pvb_Redireccionar, "true", "false")))
    End Sub
#End Region

#Region "Funciones"

#End Region
End Class
