
Partial Class MasterPage_Mp_Base
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim vlo_ScriptManager As ScriptManager

        Try
            If ScriptManager.GetCurrent(Page) IsNot Nothing Then
                vlo_ScriptManager = ScriptManager.GetCurrent(Page)
                vlo_ScriptManager.Scripts.Clear()
                vlo_ScriptManager.Scripts.Add(New ScriptReference(AdministradorRecursos.ObtenerRutaScript("modernizr-2.8.3.js")))
                vlo_ScriptManager.Scripts.Add(New ScriptReference(AdministradorRecursos.ObtenerRutaScript("jquery-2.1.1.min.js")))
                vlo_ScriptManager.Scripts.Add(New ScriptReference(AdministradorRecursos.ObtenerRutaScript("jquery-ui.min-1.11.1.js")))
                vlo_ScriptManager.Scripts.Add(New ScriptReference(AdministradorRecursos.ObtenerRutaScript("jquery.tooltipster.min.js")))
                vlo_ScriptManager.Scripts.Add(New ScriptReference(AdministradorRecursos.ObtenerRutaScript("jquery.limiter.js")))
                vlo_ScriptManager.Scripts.Add(New ScriptReference(AdministradorRecursos.ObtenerRutaScript("WebUtils.js")))
            End If
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub
End Class

