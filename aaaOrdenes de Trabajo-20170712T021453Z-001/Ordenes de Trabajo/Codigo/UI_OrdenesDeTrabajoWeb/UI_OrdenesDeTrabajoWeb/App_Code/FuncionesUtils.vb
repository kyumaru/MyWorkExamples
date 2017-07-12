Imports Microsoft.VisualBasic
Imports System.Data
Public Class FuncionesUtils
#Region "UnidadesSIRH"
    ''' <summary>
    ''' Carga un DataSet con la información de las ubicaciones contables registradas en el sistema
    ''' </summary>
    ''' <param name="pvo_FechaEntrada">Fecha de entrada de los registros</param>
    ''' <param name="pvo_FechaDesde">Fecha de desde de los registros</param>
    ''' <param name="pvo_IndiceValidez">Indice de validez de los registos</param>
    ''' <param name="pvi_TipoUnidad">Indica el tipo de unidad a buscar. Cero indica que se cargarán todas</param>
    ''' <returns>DataSet con información de la tabla UBICACION</returns>
    ''' <remarks></remarks>
    Public Shared Function CargarUbicaciones(ByVal pvo_FechaEntrada As DateTime, ByVal pvo_FechaDesde As DateTime, ByVal pvi_TipoUnidad As Integer, ByVal pvo_IndiceValidez As Integer, ByRef pro_DsDatos As DataSet) As Boolean
        Dim vlo_Bll_Sirh As New WsrSIRH.WsSIRH
        Dim vlb_Resultado As Boolean

        Try
            'instanciar objetos
            vlb_Resultado = False
            vlo_Bll_Sirh.Url = ConfigurationManager.AppSettings("WsrSIRH.WsSIRH")
            vlo_Bll_Sirh.Credentials = System.Net.CredentialCache.DefaultCredentials
            vlo_Bll_Sirh.Timeout = -1

            'definir credenciales y ejecutar sentencia
            pro_DsDatos = vlo_Bll_Sirh.UBICACION_CargarRegistros(ConfigurationManager.AppSettings("UsuarioAplicacionWeb"), ConfigurationManager.AppSettings("ClaveAplicacionWeb"), pvo_FechaEntrada, pvo_FechaDesde, 0, 1, pvi_TipoUnidad, True)

            If Not pro_DsDatos.Tables("UBICACION") Is Nothing AndAlso pro_DsDatos.Tables("UBICACION").Rows.Count > 0 Then
                vlb_Resultado = True
            End If

        Catch ex As Exception
            Throw
        Finally
            vlo_Bll_Sirh.Dispose()
        End Try

        Return vlb_Resultado
    End Function
#End Region
End Class
