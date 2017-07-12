Imports Wsr_GN_GestorNotificaciones
Imports Utilerias.OrdenesDeTrabajo


Partial Class Notificaciones_Frm_OT_VerCorreos
    Inherits System.Web.UI.Page
#Region "Eventos"

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Dim vln_IdNotificacion As Integer
        Dim vlc_Origen As String

        Try
            vln_IdNotificacion = WebUtils.LeerParametro(Of Integer)("pvn_IdNotificacion")
            vlc_Origen = WebUtils.LeerParametro(Of String)("pvc_Origen")
            Me.Buscar(vln_IdNotificacion, vlc_Origen)


        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

#End Region

#Region "Metodos"
    Private Sub Buscar(ByVal pvn_IdNotificacion As Integer, ByVal pvc_Origen As String)
        Dim vlo_EntDatos As EntGNT_NOTIFICACION
        Dim vlo_EntDatosH As EntGNH_NOTIFICACION
        Dim vlo_wsGestorNotificaciones As wsGestorNotificaciones

        Try
            'configurar controles
            vlo_wsGestorNotificaciones = New wsGestorNotificaciones
            vlo_wsGestorNotificaciones.Timeout = -1
            vlo_wsGestorNotificaciones.Credentials = System.Net.CredentialCache.DefaultCredentials

            'obtener datos
            If pvc_Origen = "T" Then
                vlo_EntDatos = vlo_wsGestorNotificaciones.GNT_NOTIFICACION_ObtenerRegistro(ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                                                                      String.Format("{0} = {1}", Utilerias.GestorNotificaciones.Modelo.GNT_NOTIFICACION.ID_NOTIFICACION, pvn_IdNotificacion))

                If vlo_EntDatos.Existe Then
                    Me.lblContenido.Text = vlo_EntDatos.CUERPO
                End If
            Else
                vlo_EntDatosH = vlo_wsGestorNotificaciones.GNH_NOTIFICACION_ObtenerRegistro(ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                                                                      String.Format("{0} = {1}", Utilerias.GestorNotificaciones.Modelo.GNT_NOTIFICACION.ID_NOTIFICACION, pvn_IdNotificacion))
                If vlo_EntDatosH.Existe Then
                    Me.lblContenido.Text = vlo_EntDatosH.CUERPO
                End If
            End If
        Catch ex As Exception
            Throw

        Finally
            If vlo_wsGestorNotificaciones IsNot Nothing Then
                vlo_wsGestorNotificaciones.Dispose()
            End If
        End Try
    End Sub

#End Region
End Class
