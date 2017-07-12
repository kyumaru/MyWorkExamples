Imports Microsoft.VisualBasic

<Serializable()> _
Public Class UsuarioActual
#Region "Atributos"
    Private vgo_Usuario As MembershipUser
    Private vgb_Existe As Boolean
    Private vgc_NombreLlaveUsuariosEnSesion As String
#End Region

#Region "Propiedades"
    Public ReadOnly Property UserId As Integer
        Get
            Return vgo_Usuario.ProviderUserKey
        End Get
    End Property

    Public ReadOnly Property UserName As String
        Get
            Return vgo_Usuario.UserName
        End Get
    End Property

    Public ReadOnly Property CorreoElectronico As String
        Get
            Return vgo_Usuario.Email
        End Get
    End Property

    Public ReadOnly Property Existe As Boolean
        Get
            Return vgb_Existe
        End Get
    End Property
#End Region

#Region "Constructores"
    Sub New()
        Try
            vgc_NombreLlaveUsuariosEnSesion = String.Format("{0}_UsuariosEnSesion", ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_NOMBRE_DEL_SITIO_WEB))
            vgo_Usuario = Membership.GetUser()
            vgb_Existe = vgo_Usuario IsNot Nothing
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Sub New(pvo_Usuario As MembershipUser)
        vgc_NombreLlaveUsuariosEnSesion = String.Format("{0}_UsuariosEnSesion", ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_NOMBRE_DEL_SITIO_WEB))
        RegistrarUsuarioEnSesion(pvo_Usuario)
    End Sub
#End Region

#Region "Métodos"
    Private Sub RegistrarUsuarioEnSesion(pvo_Usuario As MembershipUser)
        Dim vlo_UsuariosEnSesion As System.Collections.Generic.Dictionary(Of Object, MembershipUser)

        Try
            If HttpContext.Current.Session(vgc_NombreLlaveUsuariosEnSesion) Is Nothing Then
                vlo_UsuariosEnSesion = New System.Collections.Generic.Dictionary(Of Object, MembershipUser)
            Else
                vlo_UsuariosEnSesion = CType(HttpContext.Current.Session(vgc_NombreLlaveUsuariosEnSesion), System.Collections.Generic.Dictionary(Of Object, MembershipUser))
            End If

            If Not ExisteUsuarioEnSesion(pvo_Usuario.ProviderUserKey) Then
                vlo_UsuariosEnSesion.Add(pvo_Usuario.ProviderUserKey, pvo_Usuario)
                HttpContext.Current.Session.Remove(vgc_NombreLlaveUsuariosEnSesion)
                HttpContext.Current.Session.Add(vgc_NombreLlaveUsuariosEnSesion, vlo_UsuariosEnSesion)
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
#End Region

#Region "Funciones"
    Private Function ExisteUsuarioEnSesion(pvn_UserId As Object) As Boolean
        Dim vlo_UsuariosEnSesion As System.Collections.Generic.Dictionary(Of Object, MembershipUser)

        Try
            If HttpContext.Current.Session(vgc_NombreLlaveUsuariosEnSesion) IsNot Nothing Then
                vlo_UsuariosEnSesion = CType(HttpContext.Current.Session(vgc_NombreLlaveUsuariosEnSesion), System.Collections.Generic.Dictionary(Of Object, MembershipUser))
                Return vlo_UsuariosEnSesion.ContainsKey(pvn_UserId)
            End If

        Catch ex As Exception
            Throw
        End Try

        Return False
    End Function

    Private Function ObtenerUsuarioEnSesion(pvn_UserId As Object) As MembershipUser
        Dim vlo_UsuariosEnSesion As System.Collections.Generic.Dictionary(Of Object, MembershipUser)

        Try
            If HttpContext.Current.Session(vgc_NombreLlaveUsuariosEnSesion) IsNot Nothing Then
                vlo_UsuariosEnSesion = CType(HttpContext.Current.Session(vgc_NombreLlaveUsuariosEnSesion), System.Collections.Generic.Dictionary(Of Object, MembershipUser))
                If vlo_UsuariosEnSesion.ContainsKey(pvn_UserId) Then
                    Return vlo_UsuariosEnSesion(pvn_UserId)
                End If
            End If

        Catch ex As Exception
            Throw
        End Try

        Return Nothing
    End Function
#End Region
End Class