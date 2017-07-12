Imports Microsoft.VisualBasic

Public Class IMembershipProvider
    Inherits MembershipProvider

#Region "Atributos"
    Private vgo_MembershipProvider_LdapFuncionarios As New Wsr_SEG_MembershipProvider_LdapFuncionarios.WsLdapProvider
    Private vgc_NombreProveedor As String

    Private vlc_ApplicationName As String
    Private vlb_EnablePasswordReset As Boolean
    Private vlb_EnablePasswordRetrieval As Boolean
    Private vln_MaxInvalidPasswordAttempts As Integer
    Private vln_MinRequiredNonAlphanumericCharacters As Integer
    Private vln_MinRequiredPasswordLength As Integer
    Private vln_PasswordAttemptWindow As Integer
    Private vlo_PasswordFormat As MembershipPasswordFormat
    Private vlc_PasswordStrengthRegularExpression As String
    Private vlb_RequiresQuestionAndAnswer As Boolean
    Private vlb_RequiresUniqueEmail As Boolean
#End Region

#Region "Propiedades"
    Public Overrides Property ApplicationName() As String
        Get
            Return vlc_ApplicationName
        End Get
        Set(ByVal value As String)

        End Set
    End Property

    Public Overrides ReadOnly Property EnablePasswordReset() As Boolean
        Get
            Return vlb_EnablePasswordReset
        End Get
    End Property

    Public Overrides ReadOnly Property EnablePasswordRetrieval() As Boolean
        Get
            Return vlb_EnablePasswordRetrieval
        End Get
    End Property

    Public Overrides ReadOnly Property MaxInvalidPasswordAttempts() As Integer
        Get
            Return vln_MaxInvalidPasswordAttempts
        End Get
    End Property

    Public Overrides ReadOnly Property MinRequiredNonAlphanumericCharacters() As Integer
        Get
            Return vln_MinRequiredNonAlphanumericCharacters
        End Get
    End Property

    Public Overrides ReadOnly Property MinRequiredPasswordLength() As Integer
        Get
            Return vln_MinRequiredPasswordLength
        End Get
    End Property

    Public Overrides ReadOnly Property PasswordAttemptWindow() As Integer
        Get
            Return vln_PasswordAttemptWindow
        End Get
    End Property

    Public Overrides ReadOnly Property PasswordFormat() As System.Web.Security.MembershipPasswordFormat
        Get
            Return vlo_PasswordFormat
        End Get
    End Property

    Public Overrides ReadOnly Property PasswordStrengthRegularExpression() As String
        Get
            Return vlc_PasswordStrengthRegularExpression
        End Get
    End Property

    Public Overrides ReadOnly Property RequiresQuestionAndAnswer() As Boolean
        Get
            Return vlb_RequiresQuestionAndAnswer
        End Get
    End Property

    Public Overrides ReadOnly Property RequiresUniqueEmail() As Boolean
        Get
            Return vlb_RequiresUniqueEmail
        End Get
    End Property
#End Region

#Region "Métodos"
    Public Overrides Sub Initialize(ByVal pvc_NombreProveedor As String, ByVal pvo_Configuracion As NameValueCollection)
        If String.IsNullOrEmpty(pvc_NombreProveedor.Trim) Then
            Throw New Exception("El nombre del proveedor no puede ser nulo")
        Else
            vgc_NombreProveedor = pvc_NombreProveedor
        End If

        MyBase.Initialize(pvc_NombreProveedor, pvo_Configuracion)

        vlc_ApplicationName = "mpUcrLdapProvider"
        vlb_EnablePasswordReset = False
        vlb_EnablePasswordRetrieval = False
        vln_MaxInvalidPasswordAttempts = False
        vln_MinRequiredNonAlphanumericCharacters = False
        vln_MinRequiredPasswordLength = False
        vln_PasswordAttemptWindow = False
        vlo_PasswordFormat = False
        vlc_PasswordStrengthRegularExpression = False
        vlb_RequiresQuestionAndAnswer = False
        vlb_RequiresUniqueEmail = False
    End Sub

#Region "No soportado"
    Public Overrides Sub UpdateUser(ByVal user As System.Web.Security.MembershipUser)
        Throw New NotImplementedException("IMemberhip: UpdateUser no soportado")
    End Sub
#End Region
#End Region

#Region "Funciones"
#Region "No soportado"
    Public Overrides Function ChangePassword(ByVal username As String, ByVal oldPassword As String, ByVal newPassword As String) As Boolean
        Throw New NotImplementedException("IMemberhip: ChangePassword no soportado")
    End Function

    Public Overrides Function ChangePasswordQuestionAndAnswer(ByVal username As String, ByVal password As String, ByVal newPasswordQuestion As String, ByVal newPasswordAnswer As String) As Boolean
        Throw New NotImplementedException("IMemberhip: ChangePasswordQuestionAndAnswer no soportado")
    End Function

    Public Overrides Function CreateUser(ByVal username As String, ByVal password As String, ByVal email As String, ByVal passwordQuestion As String, ByVal passwordAnswer As String, ByVal isApproved As Boolean, ByVal providerUserKey As Object, ByRef status As System.Web.Security.MembershipCreateStatus) As System.Web.Security.MembershipUser
        Throw New NotImplementedException("IMemberhip: CreateUser no soportado")
    End Function

    Public Overrides Function DeleteUser(ByVal username As String, ByVal deleteAllRelatedData As Boolean) As Boolean
        Throw New NotImplementedException("IMemberhip: DeleteUser no soportado")
    End Function

    Public Overrides Function FindUsersByEmail(ByVal emailToMatch As String, ByVal pageIndex As Integer, ByVal pageSize As Integer, ByRef totalRecords As Integer) As System.Web.Security.MembershipUserCollection
        Throw New NotImplementedException("IMemberhip: FindUsersByEmail no soportado")
    End Function

    Public Overrides Function GetAllUsers(ByVal pageIndex As Integer, ByVal pageSize As Integer, ByRef totalRecords As Integer) As System.Web.Security.MembershipUserCollection
        Throw New NotImplementedException("IMemberhip: GetAllUsers no soportado")
    End Function

    Public Overrides Function GetNumberOfUsersOnline() As Integer
        Throw New NotImplementedException("IMemberhip: GetNumberOfUsersOnline no soportado")
    End Function

    Public Overrides Function GetPassword(ByVal username As String, ByVal answer As String) As String
        Throw New NotImplementedException("IMemberhip: GetPassword no soportado")
    End Function

    Public Overrides Function GetUserNameByEmail(ByVal email As String) As String
        Throw New NotImplementedException("IMemberhip: GetUserNameByEmail no soportado")
    End Function

    Public Overrides Function ResetPassword(ByVal username As String, ByVal answer As String) As String
        Throw New NotImplementedException("IMemberhip: ResetPassword no soportado")
    End Function

    Public Overrides Function UnlockUser(ByVal userName As String) As Boolean
        Throw New NotImplementedException("IMemberhip: UnlockUser no soportado")
    End Function
#End Region

    Public Overrides Function FindUsersByName(ByVal usernameToMatch As String, ByVal pageIndex As Integer, ByVal pageSize As Integer, ByRef totalRecords As Integer) As System.Web.Security.MembershipUserCollection
        Return ToMembershipUserCollection(vgo_MembershipProvider_LdapFuncionarios.FindUsersByName(usernameToMatch))
    End Function

    Public Overloads Overrides Function GetUser(ByVal providerUserKey As Object, ByVal userIsOnline As Boolean) As System.Web.Security.MembershipUser
        Return ToMembershipUser(vgo_MembershipProvider_LdapFuncionarios.GetUserByUserKey(providerUserKey))
    End Function

    Public Overloads Overrides Function GetUser(ByVal username As String, ByVal userIsOnline As Boolean) As System.Web.Security.MembershipUser
        Return ToMembershipUser(vgo_MembershipProvider_LdapFuncionarios.GetUserByUserName(username))
    End Function

    Public Overrides Function ValidateUser(ByVal username As String, ByVal password As String) As Boolean
        Return vgo_MembershipProvider_LdapFuncionarios.ValidateUser(username, password)
    End Function

    Private Function ToMembershipUser(ByVal user As Wsr_SEG_MembershipProvider_LdapFuncionarios.UcrLdapUser) As MembershipUser
        If user IsNot Nothing Then
            Return New MembershipUser(Membership.Provider.Name, user.Identificacion, user.ProviderKey, user.Correo, String.Empty, user.Nombre, True, False, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now)
        End If

        Return Nothing
    End Function

    Private Function ToMembershipUserCollection(ByVal userCollection As Wsr_SEG_MembershipProvider_LdapFuncionarios.UcrLdapUser()) As MembershipUserCollection
        Dim vlo_ResultList As New MembershipUserCollection
        Dim vlo_UserWrapper As Wsr_SEG_MembershipProvider_LdapFuncionarios.UcrLdapUser

        If userCollection IsNot Nothing Then
            For Each vlo_UserWrapper In userCollection
                vlo_ResultList.Add(ToMembershipUser(vlo_UserWrapper))
            Next
        End If

        Return vlo_ResultList
    End Function
#End Region

End Class
