Imports Utilerias.OrdenesDeTrabajo
Imports OrdenesDeTrabajo.EntidadNegocio.Catalogos
Imports OrdenesDeTrabajo.AccesoDatos.Catalogos
Imports Utilerias.BaseDatos.OracleServer
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling

Namespace OrdenesDeTrabajo.LogicaNegocio.Catalogos
    Public Class BllOtmTipoObra
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
        ''' Permite borrar un registro en la tabla OTM_TIPO_OBRA, no sin antes aplicar la validación de dependencia con tablas relacionadas
        ''' </summary>
        ''' <param name="pvo_Registro">Entidad a procesar</param>
        ''' <returns>Mayor a cero: El proceso ha sido exitoso. Menor a cero: El proceso ha fallado</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>30/03/2016 10:55:00 a.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Function BorrarRegistro(ByVal pvo_Registro As EntOtmTipoObra) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOtmTipoObra As DalOtmTipoObra
            Dim vln_Resultado As Integer

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                If PoseeRegistrosAsociados(pvo_Registro.IdTipoObra) Then
                    vln_Resultado = -1
                    Throw New OrdenesDeTrabajoException("Hay registro asociados")
                End If

                vlo_DalOtmTipoObra = New DalOtmTipoObra(vlo_Conexion)
                vln_Resultado = vlo_DalOtmTipoObra.BorrarRegistro(pvo_Registro)
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
        ''' <param name="pvn_IdTipoObra">Llave primaria de la tabla otm_rubro_desicion_inicia que se asocia con la secuencia sq_rubro_desicion_inicia</param>
        ''' <returns>True: posee datos asociados. False: No posee datos asociados</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>30/03/2016 10:55:00 a.m.</creationDate>
        ''' <changeLog></changeLog>
        Private Function PoseeRegistrosAsociados(pvn_IdTipoObra As Integer) As Boolean
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_PoseeRegistrosAsociados As Boolean
            'Dim vlo_DalOthDesicionInicial As DalOthDesicionInicial
            'Dim vlo_DalOttDesicionInicial As DalOttDesicionInicial

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                ''valor inicial de retorno
                'vlo_PoseeRegistrosAsociados = False

                ''Determinar la existencia de registros asociados en la tabla OTH_DESICION_INICIAL
                'vlo_DalOthDesicionInicial = New DalOthDesicionInicial(vlo_Conexion)
                'If vlo_DalOthDesicionInicial.ObtenerRegistro(String.Format("{0} = {1}", Modelo.OTH_DESICION_INICIAL.ID_TIPO_OBRA, pvn_IdTipoObra)).Existe Then
                '	Return True
                'End If

                ''Determinar la existencia de registros asociados en la tabla OTT_DESICION_INICIAL
                'vlo_DalOttDesicionInicial = New DalOttDesicionInicial(vlo_Conexion)
                'If vlo_DalOttDesicionInicial.ObtenerRegistro(String.Format("{0} = {1}", Modelo.OTT_DESICION_INICIAL.ID_TIPO_OBRA, pvn_IdTipoObra)).Existe Then
                '	Return True
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
