Imports Utilerias.OrdenesDeTrabajo
'Imports Utilerias.ServiciosEU.Modelo
Imports Wsr_OT_OrdenesDeTrabajo
Imports Wsr_OT_Catalogos
Imports System.Data
Imports Utilerias.OrdenesDeTrabajo.Modelo
Imports Wsr_SDP_ReportServer
Imports Utilerias.GeneradorDeReportes


Partial Class Controles_Frm_EM_ManejoReportes
    Inherits System.Web.UI.Page
#Region "Propiedades"
    Public Property RutaBase As String
        Get
            Return CType(ViewState("RutaBase"), String)
        End Get
        Set(ByVal value As String)
            ViewState("RutaBase") = value
        End Set
    End Property

    Public Property FormatoReporte As String
        Get
            Return CType(ViewState("FormatoReporte"), String)
        End Get
        Set(ByVal value As String)
            ViewState("FormatoReporte") = value
        End Set
    End Property

    Public Property NombreReporte As String
        Get
            Return CType(ViewState("NombreReporte"), String)
        End Get
        Set(ByVal value As String)
            ViewState("NombreReporte") = value
        End Set
    End Property

    Public Property Condicion As String
        Get
            Return CType(ViewState("Condicion"), String)
        End Get
        Set(ByVal value As String)
            ViewState("Condicion") = value
        End Set
    End Property

    Public Property Concatenar As Integer
        Get
            Return CType(ViewState("Concatenar"), Integer)
        End Get
        Set(ByVal value As Integer)
            ViewState("Concatenar") = value
        End Set
    End Property

    Public Property ListaEntParametroReporte() As List(Of EntParametroReporte)
        Get
            Return CType(ViewState("ListaEntParametroReporte"), List(Of EntParametroReporte))
        End Get
        Set(ByVal value As List(Of EntParametroReporte))
            ViewState("ListaEntParametroReporte") = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para el Ante Proyecto
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>14/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Public Property Anteproyecto As EntOttAnteproyecto
        Get
            Return CType(ViewState("Anteproyecto"), EntOttAnteproyecto)
        End Get
        Set(value As EntOttAnteproyecto)
            ViewState("Anteproyecto") = value
        End Set
    End Property

    Public Property EmpleadoEjecuta As String
        Get
            Return CType(ViewState("EmpleadoEjecuta"), String)
        End Get
        Set(ByVal value As String)
            ViewState("EmpleadoEjecuta") = value
        End Set
    End Property


#End Region


#Region "Eventos"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim vlb_ExcepcionDescarga As Boolean

        Try
            Me.LeerParametros()

            If Me.Concatenar = 1 Then
                MostrarReporteConcatenar()
            Else
                MostrarReporte()
            End If
        Catch ex_Descarga As System.Threading.ThreadAbortException
            vlb_ExcepcionDescarga = True
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)

        End Try
    End Sub
#End Region


#Region "Metodos"
    Private Sub LeerParametros()

        Me.RutaBase = WebUtils.LeerParametro(Of String)("pvc_RutaBase")
        Me.NombreReporte = WebUtils.LeerParametro(Of String)("pvc_NombreReporte")
        Me.FormatoReporte = WebUtils.LeerParametro(Of String)("pvc_FormatoReporte")
        Me.Concatenar = WebUtils.LeerParametro(Of Integer)("pvn_Concatenar")
        Me.Condicion = WebUtils.LeerParametro(Of String)("pvc_Condicion")
        Me.EmpleadoEjecuta = WebUtils.LeerParametro(Of String)("pvc_EmpleadoEjecuta")
        ListaEntParametroReporte = CType(Session.Item("pvo_ListaEntParametroReporte"), List(Of EntParametroReporte))


    End Sub


    Private Sub MostrarReporte()
        Dim vlb_ExcepcionDescarga As Boolean
        Dim vlo_EntReporte As EntReporte
        Dim vlo_Ws_SDP_ReportServer As Ws_SDP_ReportServer

        'Dim ListaEntParametroReporte As List(Of EntParametroReporte) = Session.Item("pvo_ListaEntParametroReporte")

        Try
            vlo_Ws_SDP_ReportServer = New Ws_SDP_ReportServer
            vlo_Ws_SDP_ReportServer.Timeout = -1
            vlo_Ws_SDP_ReportServer.Credentials = System.Net.CredentialCache.DefaultCredentials


            vlo_EntReporte = vlo_Ws_SDP_ReportServer.GenerarReporte(ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                                                                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                                                                    Me.FormatoReporte,
                                                                    Me.RutaBase,
                                                                    Me.NombreReporte,
                                                                    ListaEntParametroReporte.ToArray)

            If vlo_EntReporte.Reporte.Length > 0 Then
                Response.Buffer = True
                Response.ContentType = vlo_EntReporte.MimeType
                Response.BinaryWrite(vlo_EntReporte.Reporte)
            End If


        Catch ex_Descarga As System.Threading.ThreadAbortException
            vlb_ExcepcionDescarga = True
        Catch ex As Exception

            Throw
        Finally
            Me.Session.Remove("pvo_ListaEntParametroReporte")

            If vlo_Ws_SDP_ReportServer IsNot Nothing Then
                vlo_Ws_SDP_ReportServer.Dispose()
            End If
        End Try

    End Sub


    Private Sub MostrarReporteConcatenar()
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        Dim vlo_Reporte As Byte()
        Dim vlb_ExcepcionDescarga As Boolean

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            Me.Anteproyecto = CType(Session.Item("pvo_EntOttAnteproyecto"), EntOttAnteproyecto)

            vlo_Reporte = vlo_Ws_OT_OrdenesDeTrabajo.OTT_ANTEPROYECTO_MezclarPdf(
                                            ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                                            ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                                            Me.Anteproyecto,
                                            Me.Condicion,
                                            Me.EmpleadoEjecuta,
                                            FORMATO_REPORTE.PDF,
                                            Utilerias.OrdenesDeTrabajo.Reportes.RUTA_BASE,
                                            Utilerias.OrdenesDeTrabajo.Reportes.RPT_OT_ANTE_PROYECTO)

            If vlo_Reporte.Length > 0 Then
                Response.Clear()
                Response.Buffer = True
                Response.ContentType = "Application/pdf"
                Response.BinaryWrite(vlo_Reporte)
                Response.End()
            End If

        Catch ex_Descarga As System.Threading.ThreadAbortException
            vlb_ExcepcionDescarga = True
        Catch ex As Exception
            Throw
        End Try
    End Sub
#End Region
End Class

