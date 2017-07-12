
Partial Class MasterPage_Mp_Base
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim vlo_ScriptManager As ScriptManager

        Dim vlc_NombreLlaveSesionUsuario As String

        If Not IsPostBack Then

            vlc_NombreLlaveSesionUsuario = String.Format("{0}_NombreDeUsuario", ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_NOMBRE_DEL_SITIO_WEB))



            If Session(vlc_NombreLlaveSesionUsuario) IsNot Nothing Then
                lblNombreDeUsuario.Text = Session(vlc_NombreLlaveSesionUsuario)
            Else
                lblNombreDeUsuario.Text = String.Empty
            End If


        End If

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
    End Sub

End Class

