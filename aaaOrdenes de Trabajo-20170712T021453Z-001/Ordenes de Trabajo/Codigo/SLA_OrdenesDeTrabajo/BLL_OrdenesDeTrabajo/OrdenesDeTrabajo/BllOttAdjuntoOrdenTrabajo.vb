Imports Utilerias.OrdenesDeTrabajo
Imports OrdenesDeTrabajo.EntidadNegocio.OrdenesDeTrabajo
Imports OrdenesDeTrabajo.AccesoDatos.OrdenesDeTrabajo
Imports Utilerias.BaseDatos.OracleServer
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports System.Configuration

Namespace OrdenesDeTrabajo.LogicaNegocio.OrdenesDeTrabajo
    Public Class BllOttAdjuntoOrdenTrabajo

#Region "Atributos"
        Private vgc_CadenaConexion As String
        Private vgo_Conexion As ConexionOracle
#End Region

#Region "Constructores"
        Public Sub New(pvc_CadenaConexion As String)
            vgc_CadenaConexion = pvc_CadenaConexion
        End Sub

        Public Sub New(pvo_Conexion As ConexionOracle)
            vgo_Conexion = pvo_Conexion
        End Sub
#End Region

#Region "Funciones"

        ''' <summary>
        ''' Permite modificar registros en la tabla OTT_ADJUNTO_ORDEN_TRABAJO
        ''' </summary>
        ''' <param name="pvo_DsAdjuntoOrdenTrabajo">dataset de adjuntos</param>
        ''' <returns>Mayor a cero: El proceso ha sido exitoso; el valor puede corresponder a la llave autogenerada. Menor a cero: El proceso ha fallado</returns>
        ''' <author>Carlos Gómez Ondoy</author>
        ''' <creationDate>04/04/2016</creationDate>
        ''' <changeLog></changeLog>
        Public Function ActualizarExpedienteTecnico(pvo_DsAdjuntoOrdenTrabajo As Data.DataSet) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vln_Resultado As Integer
            Dim vlo_DalOttAdjuntoOrdenTrabajo As DalOttAdjuntoOrdenTrabajo
            vln_Resultado = -1

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                vlo_Conexion.TransaccionBegin()

                vlo_DalOttAdjuntoOrdenTrabajo = New DalOttAdjuntoOrdenTrabajo(vlo_Conexion)
                vlo_DalOttAdjuntoOrdenTrabajo.AdapterOtTAdjuntoExpedienteTecnico(pvo_DsAdjuntoOrdenTrabajo)

                vlo_Conexion.TransaccionCommit()

                vln_Resultado = 1
                Return vln_Resultado

            Catch vlo_Excepcion As Exception
                Throw
            Finally
                If vlb_LiberarConexion Then
                    vlo_Conexion.Dispose()
                End If
            End Try

            Return vln_Resultado
        End Function

#End Region

    End Class
End Namespace