
Partial Class MasterPage_Mp_Formulario
    Inherits System.Web.UI.MasterPage

#Region "Eventos"
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Try
                Dim vlo_ConstructorMenu As New Utilerias.Seguridad.ControlAcceso.ConstructorMenu(Server.MapPath("~/Web.sitemap"), CType(ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_APLICAR_SEGURIDAD_MENU), Boolean), False, Me.tvMenu)
                Me.tvMenu.CollapseAll()

            Catch ex As Exception
                Dim vlo_ControlDeErrores As New ControlDeErrores
                vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
            End Try
        End If
    End Sub
#End Region
End Class

