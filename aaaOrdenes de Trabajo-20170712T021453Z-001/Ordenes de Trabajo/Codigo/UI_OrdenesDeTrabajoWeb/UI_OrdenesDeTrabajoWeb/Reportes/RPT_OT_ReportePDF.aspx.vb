Imports Wsr_SDP_ReportServer
Imports Utilerias.GeneradorDeReportes
Imports Utilerias.OrdenesDeTrabajo

Partial Class Reportes_RPT_OT_ReportePDF
    Inherits System.Web.UI.Page
#Region "Propiedades"

    Public Property Reporte() As String
        Get
            Return CType(ViewState("Reporte"), String)
        End Get
        Set(ByVal value As String)
            ViewState("Reporte") = value
        End Set
    End Property

    Public Property Condicion() As String
        Get
            Return CType(ViewState("Condicion"), String)
        End Get
        Set(ByVal value As String)
            ViewState("Condicion") = value
        End Set
    End Property

    Public Property CondicionUsuario() As String
        Get
            Return CType(ViewState("CondicionUsuario"), String)
        End Get
        Set(ByVal value As String)
            ViewState("CondicionUsuario") = value
        End Set
    End Property

    Public Property Orden() As String
        Get
            Return CType(ViewState("Orden"), String)
        End Get
        Set(ByVal value As String)
            ViewState("Orden") = value
        End Set
    End Property

    ''' <summary>
    ''' parametro
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>16/12/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Property RequiereFichaTecnica As Boolean
        Get
            Return CType(ViewState("vlo_RequiereFichaTecnica"), Boolean)
        End Get
        Set(value As Boolean)
            ViewState("vlo_RequiereFichaTecnica") = value
        End Set
    End Property



    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property LsEntParametroReporte As List(Of EntParametroReporte)
        Get
            Return CType(ViewState("LsEntParametroReporte"), List(Of EntParametroReporte))
        End Get
        Set(value As List(Of EntParametroReporte))
            ViewState("LsEntParametroReporte") = value
        End Set
    End Property

#End Region

#Region "Eventos"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                InicializarList()
                LeerParametros()
                MostrarReportePDF()
            End If
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub
#End Region

#Region "Metodos"

    ''' <summary>
    ''' Inicializa la lista
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>11/12/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Sub InicializarList()
        Try
            Me.LsEntParametroReporte = New List(Of EntParametroReporte)
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' Lee los parámetros enviados desde la pantalla de filtros del reporte
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>´César Bermúdez García</author>
    ''' <creationDate>11/12/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Sub LeerParametros()
        Try
            Me.Reporte = WebUtils.LeerParametro(Of String)("pvc_NombreReporte")
            Me.Condicion = WebUtils.LeerParametro(Of String)("pvc_Condicion")
            Me.Orden = WebUtils.LeerParametro(Of String)("pvc_Orden")
            Me.CondicionUsuario = WebUtils.LeerParametro(Of String)("pvc_CondicionUsuario")
            Me.RequiereFichaTecnica = WebUtils.LeerParametro(Of Boolean)("pvb_ReqFicha")
        Catch ex As Exception
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Carga la lista de parámetros que aceptará el reporte y manda a generar el reporte
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>15/12/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Sub MostrarReportePDF()
        Dim vlc_NombreReporte As String
        'Dim vlo_EntParametroReporte As EntParametroReporte
        vlc_NombreReporte = String.Empty
        Dim vlo_Wsr_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo

        vlo_Wsr_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Wsr_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials
        vlo_Wsr_OT_OrdenesDeTrabajo.Timeout = -1


        Try
            CargaListaParametrosBase()

            If String.Compare(Me.Reporte, Reportes.RPT_OT_REPORTE_GENERAL_SECTORES_TALLERES) = 0 Then
                CargarParametrosReporteGeneral()
            ElseIf String.Compare(Me.Reporte, Reportes.RPT_OT_REPORTE_ALERTAS_TIEMPO_ATENCION) = 0 Then
                CargarParametrosReporteAlertas()
            ElseIf String.Compare(Me.Reporte, Reportes.RPT_OT_REPORTE_ORDENES_TRABAJO) = 0 Then
                CargarParametrosReporteOrdenesTrabajo()
            ElseIf String.Compare(Me.Reporte, Reportes.RPT_OT_SECTORES_ORDEN_TRABAJO) = 0 Then
                CargarParametrosReporteOrdenesTrabajo()
            End If

            GenerarReporte()
        Catch ex As Exception
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Carga los parámetros que necesita el reporte para ser generado correctamente
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>14/12/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargaListaParametrosBase()
        Dim vlo_EntParametroReporte As EntParametroReporte
        Dim vlo_UsuarioActual As UsuarioActual
        Try
            'configuracion de los parametros para el reporte
            vlo_UsuarioActual = New UsuarioActual

            vlo_EntParametroReporte = New EntParametroReporte
            vlo_EntParametroReporte.Nombre = "pvc_Usuario"
            vlo_EntParametroReporte.Valor = ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB)
            vlo_EntParametroReporte.UsuarioResponsable = vlo_UsuarioActual.NumEmpleado
            LsEntParametroReporte.Add(vlo_EntParametroReporte)

            vlo_EntParametroReporte = New EntParametroReporte
            vlo_EntParametroReporte.Nombre = "pvc_Clave"
            vlo_EntParametroReporte.Valor = ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB)
            vlo_EntParametroReporte.UsuarioResponsable = vlo_UsuarioActual.NumEmpleado
            LsEntParametroReporte.Add(vlo_EntParametroReporte)

            vlo_EntParametroReporte = New EntParametroReporte
            vlo_EntParametroReporte.Nombre = "pvc_Condicion"
            vlo_EntParametroReporte.Valor = Condicion
            vlo_EntParametroReporte.UsuarioResponsable = vlo_UsuarioActual.NumEmpleado
            LsEntParametroReporte.Add(vlo_EntParametroReporte)

            vlo_EntParametroReporte = New EntParametroReporte
            vlo_EntParametroReporte.Nombre = "pvc_Orden"
            vlo_EntParametroReporte.Valor = IIf(Orden Is Nothing, " ", Orden)
            vlo_EntParametroReporte.UsuarioResponsable = vlo_UsuarioActual.NumEmpleado
            LsEntParametroReporte.Add(vlo_EntParametroReporte)

        Catch ex As Exception
            Throw
        End Try

    End Sub

    Private Sub CargarParametrosReporteOrdenesTrabajo()
        Dim vlo_EntParametroReporte As EntParametroReporte
        Dim vlo_UsuarioActual As UsuarioActual
        Try
            vlo_UsuarioActual = New UsuarioActual
            vlo_EntParametroReporte = New EntParametroReporte
            vlo_EntParametroReporte.Nombre = "pvc_UsuarioEjecuta"
            vlo_EntParametroReporte.Valor = vlo_UsuarioActual.UserName
            vlo_EntParametroReporte.UsuarioResponsable = vlo_UsuarioActual.NumEmpleado
            LsEntParametroReporte.Add(vlo_EntParametroReporte)

        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Sub CargarParametrosReporteGeneral()
        Dim vlo_EntParametroReporte As EntParametroReporte
        Dim vlo_UsuarioActual As UsuarioActual
        Try
            vlo_UsuarioActual = New UsuarioActual
            vlo_EntParametroReporte = New EntParametroReporte
            vlo_EntParametroReporte.Nombre = "pvc_UsuarioEjecuta"
            vlo_EntParametroReporte.Valor = vlo_UsuarioActual.UserId
            vlo_EntParametroReporte.UsuarioResponsable = vlo_UsuarioActual.NumEmpleado
            LsEntParametroReporte.Add(vlo_EntParametroReporte)

            vlo_EntParametroReporte = New EntParametroReporte
            vlo_EntParametroReporte.Nombre = "pvc_Filtros"
            vlo_EntParametroReporte.Valor = CondicionUsuario
            vlo_EntParametroReporte.UsuarioResponsable = vlo_UsuarioActual.NumEmpleado
            LsEntParametroReporte.Add(vlo_EntParametroReporte)

        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Sub CargarParametrosReporteAlertas()
        Dim vlo_EntParametroReporte As EntParametroReporte
        Dim vlo_UsuarioActual As UsuarioActual
        Try
            vlo_UsuarioActual = New UsuarioActual
            vlo_EntParametroReporte = New EntParametroReporte
            vlo_EntParametroReporte.Nombre = "pvc_UsuarioEjecuta"
            vlo_EntParametroReporte.Valor = vlo_UsuarioActual.UserId
            vlo_EntParametroReporte.UsuarioResponsable = vlo_UsuarioActual.NumEmpleado
            LsEntParametroReporte.Add(vlo_EntParametroReporte)

            vlo_EntParametroReporte = New EntParametroReporte
            vlo_EntParametroReporte.Nombre = "pvc_Filtros"
            vlo_EntParametroReporte.Valor = CondicionUsuario
            vlo_EntParametroReporte.UsuarioResponsable = vlo_UsuarioActual.NumEmpleado
            LsEntParametroReporte.Add(vlo_EntParametroReporte)

            vlo_EntParametroReporte = New EntParametroReporte
            vlo_EntParametroReporte.Nombre = "pvc_ReqFichaTecnica"
            vlo_EntParametroReporte.Valor = IIf(Me.RequiereFichaTecnica, "1", "0")
            vlo_EntParametroReporte.UsuarioResponsable = vlo_UsuarioActual.NumEmpleado
            LsEntParametroReporte.Add(vlo_EntParametroReporte)
        Catch ex As Exception
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Genera el reporte según el parametro de sesión
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>11/12/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Sub GenerarReporte()
        Dim vlo_EntReporte As EntReporte
        Dim vlo_Ws_SDP_ReportServer As Ws_SDP_ReportServer

        vlo_Ws_SDP_ReportServer = New Ws_SDP_ReportServer
        vlo_Ws_SDP_ReportServer.Credentials = System.Net.CredentialCache.DefaultCredentials
        vlo_Ws_SDP_ReportServer.Timeout = -1

        Try
            vlo_EntReporte = vlo_Ws_SDP_ReportServer.GenerarReporte(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                FORMATO_REPORTE.PDF, Utilerias.OrdenesDeTrabajo.Reportes.RUTA_BASE, Me.Reporte, LsEntParametroReporte.ToArray)

            If vlo_EntReporte.Reporte.Length > 0 Then
                Response.Buffer = True
                Response.ContentType = vlo_EntReporte.MimeType
                Response.BinaryWrite(vlo_EntReporte.Reporte)
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
End Class
