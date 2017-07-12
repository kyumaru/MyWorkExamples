Imports Utilerias.OrdenesDeTrabajo
Imports OrdenesDeTrabajo.EntidadNegocio.Catalogos
Imports OrdenesDeTrabajo.AccesoDatos.Catalogos
Imports Utilerias.BaseDatos.OracleServer
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports OrdenesDeTrabajo.AccesoDatos.OrdenesDeTrabajo

Namespace OrdenesDeTrabajo.LogicaNegocio.Catalogos
    Public Class BllOtmEtapaOrdenTrabajo
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
        ''' Permite borrar un registro en la tabla OTM_ETAPA_ORDEN_TRABAJO, no sin antes aplicar la validación de dependencia con tablas relacionadas
        ''' </summary>
        ''' <param name="pvo_Registro">Entidad a procesar</param>
        ''' <returns>Mayor a cero: El proceso ha sido exitoso. Menor a cero: El proceso ha fallado</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>29/01/2016 04:20:42 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Function BorrarRegistro(ByVal pvo_Registro As EntOtmEtapaOrdenTrabajo) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOtmEtapaOrdenTrabajo As DalOtmEtapaOrdenTrabajo
            Dim vln_Resultado As Integer

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                If PoseeRegistrosAsociados(pvo_Registro.IdEtapaOrdenTrabajo) Then
                    vln_Resultado = -1
                    Throw New OrdenesDeTrabajoException("Registro ya existente.")
                End If

                vlo_DalOtmEtapaOrdenTrabajo = New DalOtmEtapaOrdenTrabajo(vlo_Conexion)
                vln_Resultado = vlo_DalOtmEtapaOrdenTrabajo.BorrarRegistro(pvo_Registro)
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
        ''' <param name="pvn_IdEtapaOrdenTrabajo">Llave primaria de la tabla otm_etapa_orden_trabajo que se asocia con la secuencia sq_id_etapa_orden_trabajo</param>
        ''' <returns>True: posee datos asociados. False: No posee datos asociados</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>29/01/2016 04:20:42 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Private Function PoseeRegistrosAsociados(pvn_IdEtapaOrdenTrabajo As Integer) As Boolean
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_PoseeRegistrosAsociados As Boolean
            'Dim vlo_DalOthOperarioOrdenTrab As DalOthOperarioOrdenTrab
            Dim vlo_DalOttOperarioOrdenTrab As DalOttOperarioOrdenTrab

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

                'Determinar la existencia de registros asociados en la tabla OTH_OPERARIO_ORDEN_TRAB
                'vlo_DalOthOperarioOrdenTrab = New DalOthOperarioOrdenTrab(vlo_Conexion)
                'If vlo_DalOthOperarioOrdenTrab.ObtenerRegistro(String.Format("{0} = {1}", Modelo.OTH_OPERARIO_ORDEN_TRAB.ID_ETAPA_ORDEN_TRABAJO, pvn_IdEtapaOrdenTrabajo)).Existe Then
                '    Return True
                'End If

                'Determinar la existencia de registros asociados en la tabla OTT_OPERARIO_ORDEN_TRAB
                vlo_DalOttOperarioOrdenTrab = New DalOttOperarioOrdenTrab(vlo_Conexion)
                If vlo_DalOttOperarioOrdenTrab.ObtenerRegistro(String.Format("{0} = {1}", Modelo.OTT_OPERARIO_ORDEN_TRAB.ID_ETAPA_ORDEN_TRABAJO, pvn_IdEtapaOrdenTrabajo)).Existe Then
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
