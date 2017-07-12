
Partial Class Seguridad_Frm_Logout
    Inherits System.Web.UI.Page

#Region "Eventos"
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Me.IsPostBack Then
            FormsAuthentication.SignOut()
            If Session.Count > 0 Then
                Session.Clear()
                Session.Abandon()
            End If
        End If
    End Sub
#End Region
End Class
