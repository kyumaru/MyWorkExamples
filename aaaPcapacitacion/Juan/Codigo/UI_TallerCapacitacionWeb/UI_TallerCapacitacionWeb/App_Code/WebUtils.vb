Imports Microsoft.VisualBasic

Public Class WebUtils
#Region "Métodos"
    Public Shared Sub RegistrarScript(pvo_Pagina As System.Web.UI.Control, pvc_Llave As String, pvc_Script As String)
        ScriptManager.RegisterStartupScript(pvo_Pagina, GetType(String), pvc_Llave, pvc_Script, True)
    End Sub
#End Region

#Region "Funciones"
    Public Shared Function LeerParametro(Of T)(pvc_Nombre As String) As T
        Dim vlo_Valor As T

        If HttpContext.Current.Request.QueryString(pvc_Nombre) IsNot Nothing Then
            vlo_Valor = CType(CType(HttpContext.Current.Request.QueryString(pvc_Nombre), Object), T)
        ElseIf HttpContext.Current.Session(pvc_Nombre) IsNot Nothing Then
            vlo_Valor = CType(HttpContext.Current.Session(pvc_Nombre), T)
            HttpContext.Current.Session.Remove(pvc_Nombre)
        Else
            vlo_Valor = Nothing
        End If

        Return vlo_Valor
    End Function
#End Region
End Class