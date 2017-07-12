Imports Microsoft.VisualBasic

<Serializable()> _
Public Class UsuarioActual
#Region "Atributos"
    Private vgo_Usuario As MembershipUser
    Private vgb_Existe As Boolean
    Private vgc_NombreLlaveUsuariosEnSesion As String
    Private vgc_NombreCompleto As String
    Private vgn_NumeroEmpleado As Integer
#End Region

#Region "Propiedades"


    Public ReadOnly Property UserId As String
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

    Public ReadOnly Property NumEmpleado As Integer
        Get
            Return vgn_NumeroEmpleado
        End Get
    End Property

    Public ReadOnly Property NombreCompleto As String
        Get
            Return vgc_NombreCompleto
        End Get
    End Property

#End Region

#Region "Constructores"
    Sub New()
        Try

            vgc_NombreLlaveUsuariosEnSesion = String.Format("{0}_UsuariosEnSesion", ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_NOMBRE_DEL_SITIO_WEB))
            vgo_Usuario = Membership.GetUser()
            vgb_Existe = vgo_Usuario IsNot Nothing
            CargarFuncionario(vgo_Usuario.UserName)
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Sub New(pvo_Usuario As MembershipUser)

        vgc_NombreLlaveUsuariosEnSesion = String.Format("{0}_UsuariosEnSesion", ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_NOMBRE_DEL_SITIO_WEB))
        RegistrarUsuarioEnSesion(pvo_Usuario)
        CargarFuncionario(pvo_Usuario.UserName)
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

    ''' <summary>
    ''' Carga el empleado, segun la identificacion personal que obtenga por parametro
    ''' </summary>
    ''' <param name="pvn_IdPersonal"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>01/10/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarFuncionario(pvn_IdPersonal As String)
        Dim vlo_WsEU_Curriculo As WsrEU_Curriculo.wsEU_Curriculo
        Dim vlo_EntEmpleados As WsrEU_Curriculo.EntEmpleados

        vlo_WsEU_Curriculo = New WsrEU_Curriculo.wsEU_Curriculo
        vlo_WsEU_Curriculo.Credentials = System.Net.CredentialCache.DefaultCredentials
        vlo_WsEU_Curriculo.Timeout = -1

        Try
            vlo_EntEmpleados = vlo_WsEU_Curriculo.Empleados_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("ID_PERSONAL = '{0}'", pvn_IdPersonal))

            vgc_NombreCompleto = String.Format("{0} {1} {2}", vlo_EntEmpleados.NOMBRE, vlo_EntEmpleados.APELLIDO1, vlo_EntEmpleados.APELLIDO2)
            vgn_NumeroEmpleado = vlo_EntEmpleados.NUM_EMPLEADO

        Catch ex As Exception
            Throw
        Finally
            If vlo_WsEU_Curriculo IsNot Nothing Then
                vlo_WsEU_Curriculo.Dispose()
            End If
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