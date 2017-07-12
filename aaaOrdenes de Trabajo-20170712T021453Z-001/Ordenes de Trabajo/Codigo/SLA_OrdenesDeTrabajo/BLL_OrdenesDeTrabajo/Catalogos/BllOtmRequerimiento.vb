Imports Utilerias.OrdenesDeTrabajo
Imports OrdenesDeTrabajo.EntidadNegocio.Catalogos
Imports OrdenesDeTrabajo.AccesoDatos.Catalogos
Imports Utilerias.BaseDatos.OracleServer
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports OrdenesDeTrabajo.AccesoDatos.OrdenesDeTrabajo

Namespace OrdenesDeTrabajo.LogicaNegocio.Catalogos
    Public Class BllOtmRequerimiento
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
        ''' Permite borrar un registro en la tabla OTM_REQUERIMIENTO, no sin antes aplicar la validación de dependencia con tablas relacionadas
        ''' </summary>
        ''' <param name="pvo_Registro">Entidad a procesar</param>
        ''' <returns>Mayor a cero: El proceso ha sido exitoso. Menor a cero: El proceso ha fallado</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>19/11/2015 10:05:27 a.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Function BorrarRegistro(ByVal pvo_Registro As EntOtmRequerimiento) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOtmRequerimiento As DalOtmRequerimiento
            Dim vln_Resultado As Integer

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                If PoseeRegistrosAsociados(pvo_Registro.IdRequerimiento) Then
                    vln_Resultado = -1
                    Throw New OrdenesDeTrabajoException("Registro ya existente.")
                End If

                vlo_DalOtmRequerimiento = New DalOtmRequerimiento(vlo_Conexion)
                vln_Resultado = vlo_DalOtmRequerimiento.BorrarRegistro(pvo_Registro)
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
        ''' Verifica si un registro posee datos asociados en las tablas hijas
        ''' </summary>
        ''' <param name="pvn_IdRequerimiento">Llave primaria de la tabla otm_requerimiento que se asocia con la secuencia sq_id_requerimiento</param>
        ''' <returns>True: posee datos asociados. False: No posee datos asociados</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>19/11/2015 10:05:27 a.m.</creationDate>
        ''' <changeLog></changeLog>
        Private Function PoseeRegistrosAsociados(pvn_IdRequerimiento As Integer) As Boolean

            'TODO para verificar si posee registros asociados

            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_PoseeRegistrosAsociados As Boolean
            Dim vlo_DalOtmRequerimiento As DalOtmRequerimiento
            Dim vlo_DalOttFichaTecnicaDetalle As DalOttFichaTecnicaDetalle
            'Dim vlo_DalOthFichaTecnicaDetalle As DalOthFichaTecnicaDetalle

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

                'Determinar la existencia de registros asociados en la tabla OTM_REQUERIMIENTO
                vlo_DalOtmRequerimiento = New DalOtmRequerimiento(vlo_Conexion)
                If vlo_DalOtmRequerimiento.ObtenerRegistro(String.Format("{0} = {1}", Modelo.OTM_REQUERIMIENTO.ID_REQUERIMIENTO, pvn_IdRequerimiento)).Existe Then
                    Return True
                End If

                'Determinar la existencia de registros asociados en la tabla OTT_FICHA_TECNICA_DETALLE
                vlo_DalOttFichaTecnicaDetalle = New DalOttFichaTecnicaDetalle(vlo_Conexion)
                If vlo_DalOttFichaTecnicaDetalle.ObtenerRegistro(String.Format("{0} = {1}", Modelo.OTT_FICHA_TECNICA_DETALLE.ID_REQUERIMIENTO, pvn_IdRequerimiento)).Existe Then
                    Return True
                End If

                'Determinar la existencia de registros asociados en la tabla OTH_FICHA_TECNICA_DETALLE
                'vlo_DalOthFichaTecnicaDetalle = New DalOthFichaTecnicaDetalle(vlo_Conexion)
                'If vlo_DalOthFichaTecnicaDetalle.ObtenerRegistro(String.Format("{0} = {1}", Modelo.OTH_FICHA_TECNICA_DETALLE.ID_REQUERIMIENTO, pvn_IdRequerimiento)).Existe Then
                '    Return True
                'End If

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
