
Partial Class Seguridad_Frm_OT_AsignarRoles
    Inherits System.Web.UI.Page

#Region "Propiedades"

    ''' <summary>
    ''' Propiedad para el usuario a trabajar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>01/09/2015</creationDate>
    ''' <changeLog></changeLog>
    Public Property UserName As String
        Get
            Return ViewState("UserName").ToString.Trim
        End Get
        Set(ByVal value As String)
            ViewState("UserName") = value
        End Set
    End Property

#End Region

#Region "Eventos"

    ''' <summary>
    ''' Evento que se ejecuta al cargar la página, inicializa los componentes necesarios
    ''' para el funcionamiento de la página
    ''' </summary>
    ''' <param name="sender">Parámetro Propio del evento</param>
    ''' <param name="e">Parámetro Propio del evento</param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>01/09/2015</creationDate>
    ''' <changeLog></changeLog> 
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                LeerParametros()
                CargarRoles()
                CargarRolesUsuario()
                Me.lblUsuario.Text = Me.UserName
            End If
            Me.lnkVolver.Focus()
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' Evento que agrega/remueve roles al usuario seleccionado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>01/09/2015</creationDate>
    ''' <changeLog></changeLog> 
    Protected Sub chkLstRoles_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkLstRoles.SelectedIndexChanged
        Try
            AsignarRoles()
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

#End Region

#Region "Métodos"

    ''' <summary>
    ''' Carga las propiedades con los parametros
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>01/09/2015</creationDate>
    ''' <changeLog></changeLog> 
    Private Sub LeerParametros()
        Me.UserName = WebUtils.LeerParametro(Of String)("pvc_Username")
    End Sub

    ''' <summary>
    ''' Metodo que carga los roles creados para el sistema
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>01/09/2015</creationDate>
    ''' <changeLog></changeLog> 
    Private Sub CargarRoles()
        Try
            Me.chkLstRoles.DataSource = Roles.GetAllRoles()
            Me.chkLstRoles.DataBind()
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' Metodo que marca los roles que tiene asigando el usuario seleccionado
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>01/09/2015</creationDate>
    ''' <changeLog></changeLog> 
    Private Sub CargarRolesUsuario()
        Dim vlo_Item As ListItem

        Try
            For Each vlo_Item In Me.chkLstRoles.Items
                If Roles.IsUserInRole(Me.UserName, vlo_Item.Text.Trim) Then
                    vlo_Item.Selected = True
                End If
            Next
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' metodo que agrega/remueve roles al usuario seleccionado
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>01/09/2015</creationDate>
    ''' <changeLog></changeLog> 
    Private Sub AsignarRoles()
        Dim vlo_Item As ListItem

        Try
            For Each vlo_Item In Me.chkLstRoles.Items
                If vlo_Item.Selected Then
                    Roles.AddUserToRole(Me.UserName, vlo_Item.Text.Trim)
                Else
                    Roles.RemoveUserFromRole(Me.UserName, vlo_Item.Text.Trim)
                End If
            Next
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

#End Region

End Class
