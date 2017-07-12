Imports Wsr_SDP_ReportServer
Imports Utilerias.GeneradorDeReportes
Imports Wsr_SDP_ReportServer.asmx ' mal nombrado, debe ser Wsr_SDP_ReportServer

Partial Class Reportes_Rpt_Tc_DesplegarPdf
    Inherits System.Web.UI.Page

#Region "Propiedades"

#End Region

#Region "Eventos"
    Protected Sub btnMostrar_Click(sender As Object, e As EventArgs) Handles btnMostrar.click
        If String.IsNullOrWhiteSpace(Me.txtIsbn.Text) Then
            WebUtils.RegistrarScript(Me, "alerta", "alert('Indique ISBN');")
        Else
            MostrarReporte(Me.txtIsbn.Text)
        End If
    End Sub
#End Region

#Region "Métodos"
    Private Sub MostrarReporte(pvc_Isbn As String)
        ' rpts tienen una lista de parametros
        Dim vlo_ListaParametros As List(Of EntParametroReporte)
        Dim vlo_Parametro As EntParametroReporte

        Dim vlo_EntReporte As EntReporte
        Dim vlo_Ws_SDP_ReportServer As Ws_SDP_ReportServer

        vlo_ListaParametros = New List(Of EntParametroReporte)

        'usuario
        vlo_Parametro = New EntParametroReporte
        vlo_Parametro.Nombre = "pvc_Usuario"
        vlo_Parametro.Valor = ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB)
        vlo_ListaParametros.Add(vlo_Parametro)

        'clave
        vlo_Parametro = New EntParametroReporte
        vlo_Parametro.Nombre = "pvc_Clave"
        vlo_Parametro.Valor = ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB)
        vlo_ListaParametros.Add(vlo_Parametro)

        'isbn
        vlo_Parametro = New EntParametroReporte
        vlo_Parametro.Nombre = "pvc_Isbn"
        vlo_Parametro.Valor = pvc_Isbn
        vlo_ListaParametros.Add(vlo_Parametro)

        'usuario ejecuta

        vlo_Parametro = New EntParametroReporte
        vlo_Parametro.Nombre = "pvc_UsuarioEjecuta"
        vlo_Parametro.Valor = "RGS"
        vlo_ListaParametros.Add(vlo_Parametro)

        'instanciamos ws
        vlo_Ws_SDP_ReportServer = New Ws_SDP_ReportServer
        vlo_Ws_SDP_ReportServer.Timeout = -1
        vlo_Ws_SDP_ReportServer.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            ' todo sevicio web ocupa usuario, clave, q se pueden obtener del web config, 'formato del reporte, ubicacion del reporte
            vlo_EntReporte = vlo_Ws_SDP_ReportServer.GenerarReporte(
            ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
            ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
            FORMATO_REPORTE.PDF,
            Utilerias.TallerCapacitacion.Reportes.RUTA_BASE,
            Utilerias.TallerCapacitacion.Reportes.RPT_TC_AUTORES_DEL_LIBRO,
            vlo_ListaParametros.ToArray)

            If vlo_EntReporte.Reporte.Length > 0 Then
                Response.Buffer = True
                Response.ContentType = vlo_EntReporte.MimeType
                Response.BinaryWrite(vlo_EntReporte.Reporte) 'todo
            End If

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_SDP_ReportServer IsNot Nothing Then
                vlo_Ws_SDP_ReportServer.Dispose()
            End If
        End Try

    End Sub
#End Region

#Region "Funciones"

#End Region

End Class
