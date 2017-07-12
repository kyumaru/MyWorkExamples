Imports Utilerias.ORDENES_TRABAJO
Imports ORDENES_TRABAJO.EntidadNegocio.Catalogos
Imports ORDENES_TRABAJO.AccesoDatos.Catalogos
Imports Utilerias.BaseDatos.OracleServer
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports Utilerias.OrdenesDeTrabajo

Namespace ORDENES_TRABAJO.LogicaNegocio.Catalogos
    Public Class BllOtlTrazabilSolTraslado
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
        ''' Permite agregar un registro en la tabla OTL_TRAZABIL_SOL_TRASLADO, no sin antes aplicar la validación de la llave primaria o alterna según corresponda
        ''' </summary>
        ''' <param name="pvo_Registro">Entidad a procesar</param>
        ''' <returns>Mayor a cero: El proceso ha sido exitoso; el valor puede corresponder a la llave autogenerada. Menor a cero: El proceso ha fallado</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>09/08/2016 09:12:03 a.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Function InsertarRegistro(ByVal pvo_Registro As EntOtlTrazabilSolTraslado) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOtlTrazabilSolTraslado As DalOtlTrazabilSolTraslado
            Dim vln_Resultado As Integer

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                If ObtenerRegistroPorLlavePrimaria(pvo_Registro.IdTrazabilSolTraslado).Existe Then
                    vln_Resultado = -1
                    Throw New OrdenesDeTrabajoException("[TODO] Llave primaria repetida")
                End If

                vlo_DalOtlTrazabilSolTraslado = New DalOtlTrazabilSolTraslado(vlo_Conexion)
                vln_Resultado = vlo_DalOtlTrazabilSolTraslado.InsertarRegistro(pvo_Registro)
            Catch vlo_Excepcion As Exception
                Throw
            Finally
                If vlb_LiberarConexion Then
                    vlo_Conexion.Dispose()
                End If
            End Try

            Return vln_Resultado
        End Function

        ''' <summary>
        ''' Permite obtener un registro según su llave primaria
        ''' </summary>
        ''' <param name="pvn_IdTrazabilSolTraslado">Llave primaria de la tabla, asociada a la secuencia sq_id_trazabil_sol_traslado</param>
        ''' <returns>Entidad que representa la información obtenida al ejecutar la consulta</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>09/08/2016 09:12:03 a.m.</creationDate>
        ''' <changeLog></changeLog>
        Private Function ObtenerRegistroPorLlavePrimaria(pvn_IdTrazabilSolTraslado As Integer) As EntOtlTrazabilSolTraslado
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOtlTrazabilSolTraslado As DalOtlTrazabilSolTraslado

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                vlo_DalOtlTrazabilSolTraslado = New DalOtlTrazabilSolTraslado(vlo_Conexion)
                Return vlo_DalOtlTrazabilSolTraslado.ObtenerRegistro(String.Format("{0} = {1}", Utilerias.OrdenesDeTrabajo.Modelo.OTL_TRAZABIL_SOL_TRASLADO.ID_TRAZABIL_SOL_TRASLADO, pvn_IdTrazabilSolTraslado))
            Catch vlo_Excepcion As Exception
                Throw
            Finally
                If vlb_LiberarConexion Then
                    vlo_Conexion.Dispose()
                End If
            End Try
        End Function

#End Region

    End Class
End Namespace
