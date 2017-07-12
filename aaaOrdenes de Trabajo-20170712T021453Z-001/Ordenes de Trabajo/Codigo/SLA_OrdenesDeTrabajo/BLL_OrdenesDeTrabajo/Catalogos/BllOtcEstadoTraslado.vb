Imports ORDENES_TRABAJO.EntidadNegocio.Catalogos
Imports ORDENES_TRABAJO.AccesoDatos.Catalogos
Imports Utilerias.BaseDatos.OracleServer
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports Utilerias.OrdenesDeTrabajo

Namespace ORDENES_TRABAJO.LogicaNegocio.Catalogos
    Public Class BllOtcEstadoTraslado
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
        ''' Permite agregar un registro en la tabla OTC_ESTADO_TRASLADO, no sin antes aplicar la validación de la llave primaria o alterna según corresponda
        ''' </summary>
        ''' <param name="pvo_Registro">Entidad a procesar</param>
        ''' <returns>Mayor a cero: El proceso ha sido exitoso; el valor puede corresponder a la llave autogenerada. Menor a cero: El proceso ha fallado</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>09/08/2016 08:59:53 a.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Function InsertarRegistro(ByVal pvo_Registro As EntOtcEstadoTraslado) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOtcEstadoTraslado As DalOtcEstadoTraslado
            Dim vln_Resultado As Integer

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                If ObtenerRegistroPorLlavePrimaria(pvo_Registro.EstadoTraslado).Existe Then
                    vln_Resultado = -1
                    Throw New OrdenesDeTrabajoException("[TODO] Llave primaria repetida")
                End If

                vlo_DalOtcEstadoTraslado = New DalOtcEstadoTraslado(vlo_Conexion)
                vln_Resultado = vlo_DalOtcEstadoTraslado.InsertarRegistro(pvo_Registro)
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
        ''' Permite borrar un registro en la tabla OTC_ESTADO_TRASLADO, no sin antes aplicar la validación de dependencia con tablas relacionadas
        ''' </summary>
        ''' <param name="pvo_Registro">Entidad a procesar</param>
        ''' <returns>Mayor a cero: El proceso ha sido exitoso. Menor a cero: El proceso ha fallado</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>09/08/2016 08:59:53 a.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Function BorrarRegistro(ByVal pvo_Registro As EntOtcEstadoTraslado) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOtcEstadoTraslado As DalOtcEstadoTraslado
            Dim vln_Resultado As Integer

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                If PoseeRegistrosAsociados(pvo_Registro.EstadoTraslado) Then
                    vln_Resultado = -1
                    Throw New OrdenesDeTrabajoException("[TODO] Hay registro asociados")
                End If

                vlo_DalOtcEstadoTraslado = New DalOtcEstadoTraslado(vlo_Conexion)
                vln_Resultado = vlo_DalOtcEstadoTraslado.BorrarRegistro(pvo_Registro)
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
        ''' <param name="pvc_EstadoTraslado">Llave de la tabla otc_estado_traslado</param>
        ''' <returns>Entidad que representa la información obtenida al ejecutar la consulta</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>09/08/2016 08:59:53 a.m.</creationDate>
        ''' <changeLog></changeLog>
        Private Function ObtenerRegistroPorLlavePrimaria(pvc_EstadoTraslado As String) As EntOtcEstadoTraslado
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOtcEstadoTraslado As DalOtcEstadoTraslado

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                vlo_DalOtcEstadoTraslado = New DalOtcEstadoTraslado(vlo_Conexion)
                Return vlo_DalOtcEstadoTraslado.ObtenerRegistro(String.Format("UPPER({0}) = '{1}'", Utilerias.OrdenesDeTrabajo.Modelo.OTC_ESTADO_TRASLADO.ESTADO_TRASLADO, pvc_EstadoTraslado.ToUpper()))
            Catch vlo_Excepcion As Exception
                Throw
            Finally
                If vlb_LiberarConexion Then
                    vlo_Conexion.Dispose()
                End If
            End Try
        End Function

        ''' <summary>
        ''' Verifica si un registro posee datos asociados en las tablas hijas
        ''' </summary>
        ''' <param name="pvc_EstadoTraslado">Llave de la tabla otc_estado_traslado</param>
        ''' <returns>True: posee datos asociados. False: No posee datos asociados</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>09/08/2016 08:59:53 a.m.</creationDate>
        ''' <changeLog></changeLog>
        Private Function PoseeRegistrosAsociados(pvc_EstadoTraslado As String) As Boolean
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_PoseeRegistrosAsociados As Boolean
            Dim vlo_DalOttSolicitudTraslado As DalOttSolicitudTraslado
            Dim vlo_DalOtlTrazabilSolTraslado As DalOtlTrazabilSolTraslado

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                'valor inicial de retorno
                vlo_PoseeRegistrosAsociados = False

                'Determinar la existencia de registros asociados en la tabla OTT_SOLICITUD_TRASLADO
                vlo_DalOttSolicitudTraslado = New DalOttSolicitudTraslado(vlo_Conexion)
                If vlo_DalOttSolicitudTraslado.ObtenerRegistro(String.Format("UPPER({0}) = '{1}'", Utilerias.OrdenesDeTrabajo.Modelo.OTT_SOLICITUD_TRASLADO.ESTADO_TRASLADO, pvc_EstadoTraslado.ToUpper())).Existe Then
                    Return True
                End If

                'Determinar la existencia de registros asociados en la tabla OTL_TRAZABIL_SOL_TRASLADO
                vlo_DalOtlTrazabilSolTraslado = New DalOtlTrazabilSolTraslado(vlo_Conexion)
                If vlo_DalOtlTrazabilSolTraslado.ObtenerRegistro(String.Format("UPPER({0}) = '{1}'", Utilerias.OrdenesDeTrabajo.Modelo.OTL_TRAZABIL_SOL_TRASLADO.ESTADO_TRASLADO, pvc_EstadoTraslado.ToUpper())).Existe Then
                    Return True
                End If

                Return False
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
