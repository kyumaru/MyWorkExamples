
Partial Class Seguridad_Frm_Login
    Inherits System.Web.UI.Page
#Region "Propiedades"

#End Region

#Region "Eventos"
    Protected Sub lnkIngresarAlSistema_Click(sender As Object, e As EventArgs) Handles lnkIngresarAlSistema.Click
        If Page.IsValid Then
            ValidarAcceso(Me.txtUsuario.Text.Trim, Me.txtClave.Text.Trim)
        End If
    End Sub
#End Region

#Region "Metodos"
    Private Sub ValidarAcceso(pvc_Identificacion As String, pvc_Carne As String)
        Dim vlb_Redireccionar As Boolean

        Try
            vlb_Redireccionar = False
            If Membership.ValidateUser(Me.txtUsuario.Text.Trim, Me.txtClave.Text.Trim) Then
                Dim vlo_User As MembershipUser = Membership.GetUser(CType(Me.txtUsuario.Text, Object))
                Dim vlo_AuthTicket As FormsAuthenticationTicket = New FormsAuthenticationTicket(1, vlo_User.UserName, DateTime.Now, DateTime.Now.AddMinutes(Session.Timeout), False, "")
                Dim vlo_EncryptedTicket As String = FormsAuthentication.Encrypt(vlo_AuthTicket)
                Dim authCookie As HttpCookie = New HttpCookie(FormsAuthentication.FormsCookieName, vlo_EncryptedTicket)
                Dim vlo_UsuarioActual As New UsuarioActual(vlo_User)

                authCookie.Expires = vlo_AuthTicket.Expiration
                Response.Cookies.Add(authCookie)
                vlb_Redireccionar = True
            Else
                Me.blError.Items.Clear()
                Me.blError.Items.Add("La datos de autenticación son incorrectos.")
                Exit Sub
            End If
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try

        If vlb_Redireccionar Then
            Response.Redirect(ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_RUTA_PAGINA_LOGIN_EXITOSO))
        End If
    End Sub
#End Region

#Region "Funciones"

#End Region
End Class
