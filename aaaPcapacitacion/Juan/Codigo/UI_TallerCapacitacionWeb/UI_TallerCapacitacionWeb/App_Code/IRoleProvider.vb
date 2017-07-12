Imports Microsoft.VisualBasic

Public Class IRoleProvider
    Inherits RoleProvider

#Region "Atributos"
    Private vgo_RolesProvider As New Wsr_SEG_RolesProvider.WsOracleRolesProvider
    Private vgc_NombreProveedor As String
    Private vlc_NombreAplicacion As String
#End Region

#Region "Propiedades"
    Public Overrides Property ApplicationName() As String
        Get
            Return vlc_NombreAplicacion
        End Get
        Set(ByVal value As String)

        End Set
    End Property
#End Region

#Region "Métodos"
    Public Overrides Sub Initialize(ByVal pvc_NombreProveedor As String, ByVal pvo_Configuracion As NameValueCollection)
        If String.IsNullOrWhiteSpace(pvc_NombreProveedor.Trim) Then
            Throw New Exception("El nombre del proveedor no puede ser nulo")
        Else
            vgc_NombreProveedor = pvc_NombreProveedor
        End If

        MyBase.Initialize(pvc_NombreProveedor, pvo_Configuracion)

        vlc_NombreAplicacion = vgo_RolesProvider.ApplicationName(vgc_NombreProveedor)
    End Sub

    Public Overrides Sub AddUsersToRoles(ByVal usernames() As String, ByVal roleNames() As String)
        vgo_RolesProvider.AddUsersToRoles(vgc_NombreProveedor, usernames, roleNames)
    End Sub

    Public Overrides Sub CreateRole(ByVal roleName As String)
        vgo_RolesProvider.CreateRole(vgc_NombreProveedor, roleName)
    End Sub
#End Region

#Region "Funciones"
    Public Overrides Function DeleteRole(ByVal roleName As String, ByVal throwOnPopulatedRole As Boolean) As Boolean
        Return vgo_RolesProvider.DeleteRole(vgc_NombreProveedor, roleName, throwOnPopulatedRole)
    End Function

    Public Overrides Function FindUsersInRole(ByVal roleName As String, ByVal usernameToMatch As String) As String()
        Return vgo_RolesProvider.FindUsersInRole(vgc_NombreProveedor, roleName, usernameToMatch)
    End Function

    Public Overrides Function GetAllRoles() As String()
        Return vgo_RolesProvider.GetAllRoles(vgc_NombreProveedor)
    End Function

    Public Overrides Function GetRolesForUser(ByVal username As String) As String()
        Return vgo_RolesProvider.GetRolesForUser(vgc_NombreProveedor, username)
    End Function

    Public Overrides Function GetUsersInRole(ByVal roleName As String) As String()
        Return vgo_RolesProvider.GetUsersInRole(vgc_NombreProveedor, roleName)
    End Function

    Public Overrides Function IsUserInRole(ByVal username As String, ByVal roleName As String) As Boolean
        Return vgo_RolesProvider.IsUserInRole(vgc_NombreProveedor, username, roleName)
    End Function

    Public Overrides Sub RemoveUsersFromRoles(ByVal usernames() As String, ByVal roleNames() As String)
        vgo_RolesProvider.RemoveUsersFromRoles(vgc_NombreProveedor, usernames, roleNames)
    End Sub

    Public Overrides Function RoleExists(ByVal roleName As String) As Boolean
        Return vgo_RolesProvider.RoleExists(vgc_NombreProveedor, roleName)
    End Function
#End Region
End Class
