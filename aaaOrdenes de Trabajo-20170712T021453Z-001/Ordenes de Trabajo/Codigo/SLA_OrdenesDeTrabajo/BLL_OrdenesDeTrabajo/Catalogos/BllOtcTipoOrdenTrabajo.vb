Imports Utilerias.OrdenesDeTrabajo
Imports OrdenesDeTrabajo.EntidadNegocio.Catalogos
Imports OrdenesDeTrabajo.AccesoDatos.Catalogos
Imports Utilerias.BaseDatos.OracleServer
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports OrdenesDeTrabajo.AccesoDatos.OrdenesDeTrabajo

Namespace OrdenesDeTrabajo.LogicaNegocio.Catalogos
    Public Class BllOtcTipoOrdenTrabajo
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
        ''' Permite agregar un registro en la tabla OTC_TIPO_ORDEN_TRABAJO, no sin antes aplicar la validación de la llave primaria o alterna según corresponda
        ''' </summary>
        ''' <param name="pvo_Registro">Entidad a procesar</param>
        ''' <returns>Mayor a cero: El proceso ha sido exitoso; el valor puede corresponder a la llave autogenerada. Menor a cero: El proceso ha fallado</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>31/08/2015 08:20:01 a.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Function InsertarRegistro(ByVal pvo_Registro As EntOtcTipoOrdenTrabajo) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOtcTipoOrdenTrabajo As DalOtcTipoOrdenTrabajo
            Dim vln_Resultado As Integer

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                If ObtenerRegistroPorLlavePrimaria(pvo_Registro.TipoOrdenTrabajo).Existe Then
                    vln_Resultado = -1
                    Throw New OrdenesDeTrabajoException("No se puede agregar el registro, ya existe un tipo de orden de trabajo con el codigo indicado.")
                End If

                If ObtenerRegistroPorLlaveAlterna(pvo_Registro.Descripcion).Existe Then
                    vln_Resultado = -1
                    Throw New OrdenesDeTrabajoException("No se puede agregar el registro, ya existe un tipo de orden de trabajo con el nombre indicado.")
                End If

                vlo_DalOtcTipoOrdenTrabajo = New DalOtcTipoOrdenTrabajo(vlo_Conexion)
                vln_Resultado = vlo_DalOtcTipoOrdenTrabajo.InsertarRegistro(pvo_Registro)
            Catch vlo_Excepcion As Exception
                Throw
            Finally
                If vlb_LiberarConexion Then
                    vlo_Conexion.Dispose()
                End If
            End Try

            Return vln_Resultado
        End Function

        Public Function ModificarRegistro(ByVal pvo_Registro As EntOtcTipoOrdenTrabajo) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOtcTipoOrdenTrabajo As DalOtcTipoOrdenTrabajo
            Dim vlo_EntOtcTipoOrdenTrabajo As EntOtcTipoOrdenTrabajo
            Dim vln_Resultado As Integer

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                vlo_EntOtcTipoOrdenTrabajo = ObtenerRegistroPorLlaveAlterna(pvo_Registro.Descripcion)
                If vlo_EntOtcTipoOrdenTrabajo.Existe AndAlso vlo_EntOtcTipoOrdenTrabajo.TipoOrdenTrabajo <> pvo_Registro.TipoOrdenTrabajo Then
                    vln_Resultado = -1
                    Throw New OrdenesDeTrabajoException("No se puede modificar el registro, ya existe un tipo de orden de trabajo con la descripción indicada.")
                End If

                vlo_DalOtcTipoOrdenTrabajo = New DalOtcTipoOrdenTrabajo(vlo_Conexion)
                vln_Resultado = vlo_DalOtcTipoOrdenTrabajo.ModificarRegistro(pvo_Registro)
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
        ''' Permite borrar un registro en la tabla OTC_TIPO_ORDEN_TRABAJO, no sin antes aplicar la validación de dependencia con tablas relacionadas
        ''' </summary>
        ''' <param name="pvo_Registro">Entidad a procesar</param>
        ''' <returns>Mayor a cero: El proceso ha sido exitoso. Menor a cero: El proceso ha fallado</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>31/08/2015 08:20:01 a.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Function BorrarRegistro(ByVal pvo_Registro As EntOtcTipoOrdenTrabajo) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOtcTipoOrdenTrabajo As DalOtcTipoOrdenTrabajo
            Dim vln_Resultado As Integer

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                If PoseeRegistrosAsociados(pvo_Registro.TipoOrdenTrabajo) Then
                    vln_Resultado = -1
                    Throw New OrdenesDeTrabajoException("El registro no puede ser borrado ya que está asociado a órdenes de trabajo. Si lo desea, puede cambiar su estado a Inactivo desde la opción Modificar.")
                End If

                vlo_DalOtcTipoOrdenTrabajo = New DalOtcTipoOrdenTrabajo(vlo_Conexion)
                vln_Resultado = vlo_DalOtcTipoOrdenTrabajo.BorrarRegistro(pvo_Registro)
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
        ''' <param name="pvc_TipoOrdenTrabajo">Tipo de orden de trabajo: ordinaria, emergencia, preventivo</param>
        ''' <returns>Entidad que representa la información obtenida al ejecutar la consulta</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>31/08/2015 08:20:01 a.m.</creationDate>
        ''' <changeLog></changeLog>
        Private Function ObtenerRegistroPorLlavePrimaria(pvc_TipoOrdenTrabajo As String) As EntOtcTipoOrdenTrabajo
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOtcTipoOrdenTrabajo As DalOtcTipoOrdenTrabajo

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                vlo_DalOtcTipoOrdenTrabajo = New DalOtcTipoOrdenTrabajo(vlo_Conexion)
                Return vlo_DalOtcTipoOrdenTrabajo.ObtenerRegistro(String.Format("UPPER({0}) = '{1}'", Modelo.OTC_TIPO_ORDEN_TRABAJO.TIPO_ORDEN_TRABAJO, pvc_TipoOrdenTrabajo.ToUpper()))
            Catch vlo_Excepcion As Exception
                Throw
            Finally
                If vlb_LiberarConexion Then
                    vlo_Conexion.Dispose()
                End If
            End Try
        End Function

        Private Function ObtenerRegistroPorLlaveAlterna(pvc_Nombre As String) As EntOtcTipoOrdenTrabajo
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOtcTipoOrdenTrabajo As DalOtcTipoOrdenTrabajo

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                vlo_DalOtcTipoOrdenTrabajo = New DalOtcTipoOrdenTrabajo(vlo_Conexion)
                Return vlo_DalOtcTipoOrdenTrabajo.ObtenerRegistro(String.Format("UPPER({0}) = '{1}'", Modelo.OTC_TIPO_ORDEN_TRABAJO.DESCRIPCION, pvc_Nombre.ToUpper()))
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
        ''' <param name="pvc_TipoOrdenTrabajo">Tipo de orden de trabajo: ordinaria, emergencia, preventivo</param>
        ''' <returns>True: posee datos asociados. False: No posee datos asociados</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>31/08/2015 08:20:01 a.m.</creationDate>
        ''' <changeLog></changeLog>
        Private Function PoseeRegistrosAsociados(pvc_TipoOrdenTrabajo As String) As Boolean
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_PoseeRegistrosAsociados As Boolean
            Dim vlo_DalOttOrdenTrabajo As DalOttOrdenTrabajo

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

                'Determinar la existencia de registros asociados en la tabla OTF_ORDEN_TRABAJO
                vlo_DalOttOrdenTrabajo = New DalOttOrdenTrabajo(vlo_Conexion)
                If vlo_DalOttOrdenTrabajo.ObtenerRegistro(String.Format("UPPER({0}) = '{1}'", Modelo.OTT_ORDEN_TRABAJO.TIPO_ORDEN_TRABAJO, pvc_TipoOrdenTrabajo.ToUpper())).Existe Then
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
