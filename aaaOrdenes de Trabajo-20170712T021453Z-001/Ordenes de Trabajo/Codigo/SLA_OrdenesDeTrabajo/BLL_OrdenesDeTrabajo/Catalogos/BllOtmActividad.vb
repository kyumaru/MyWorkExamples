Imports Utilerias.OrdenesDeTrabajo
Imports OrdenesDeTrabajo.EntidadNegocio.Catalogos
Imports OrdenesDeTrabajo.AccesoDatos.Catalogos
Imports Utilerias.BaseDatos.OracleServer
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports OrdenesDeTrabajo.AccesoDatos.OrdenesDeTrabajo

Namespace OrdenesDeTrabajo.LogicaNegocio.Catalogos
    Public Class BllOtmActividad
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
        ''' Permite agregar un registro en la tabla OTM_ACTIVIDAD, no sin antes aplicar la validación de la llave primaria o alterna según corresponda
        ''' </summary>
        ''' <param name="pvo_Registro">Entidad a procesar</param>
        ''' <returns>Mayor a cero: El proceso ha sido exitoso; el valor puede corresponder a la llave autogenerada. Menor a cero: El proceso ha fallado</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>31/08/2015 08:20:01 a.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Function InsertarRegistro(ByVal pvo_Registro As EntOtmActividad) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOtmActividad As DalOtmActividad
            Dim vln_Resultado As Integer

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                If ObtenerRegistroPorLlavePrimaria(pvo_Registro.IdCategoriaServicio, pvo_Registro.IdActividad).Existe Then
                    vln_Resultado = -1
                    Throw New OrdenesDeTrabajoException("No se puede agregar el registro, ya existe una actividad con la descripción indicada.")
                End If

                If ObtenerRegistroPorLlaveAlterna(pvo_Registro.Descripcion).Existe Then
                    vln_Resultado = -1
                    Throw New OrdenesDeTrabajoException("No se puede agregar el registro, ya existe una actividad con la descripción indicada.")
                End If

                vlo_DalOtmActividad = New DalOtmActividad(vlo_Conexion)
                vln_Resultado = vlo_DalOtmActividad.InsertarRegistro(pvo_Registro)
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
        ''' Permite modificar un registro en la tabla OTM_ACTIVIDAD, no sin antes aplicar la validación de la llave alterna
        ''' </summary>
        ''' <param name="pvo_Registro">Entidad a procesar</param>
        ''' <returns>Mayor a cero: El proceso ha sido exitoso. Menor a cero: El proceso ha fallado</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>31/08/2015 08:20:01 a.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Function ModificarRegistro(ByVal pvo_Registro As EntOtmActividad) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOtmActividad As DalOtmActividad
            Dim vlo_EntOtmActividad As EntOtmActividad
            Dim vln_Resultado As Integer

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                vlo_EntOtmActividad = ObtenerRegistroPorLlaveAlterna(pvo_Registro.Descripcion)
                If vlo_EntOtmActividad.Existe AndAlso vlo_EntOtmActividad.IdActividad <> pvo_Registro.IdActividad Then
                    vln_Resultado = -1
                    Throw New OrdenesDeTrabajoException("No se puede modificar el registro, ya existe una actividad con la descripción indicada.")
                End If

                vlo_DalOtmActividad = New DalOtmActividad(vlo_Conexion)
                vln_Resultado = vlo_DalOtmActividad.ModificarRegistro(pvo_Registro)
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
        ''' Permite borrar un registro en la tabla OTM_ACTIVIDAD, no sin antes aplicar la validación de dependencia con tablas relacionadas
        ''' </summary>
        ''' <param name="pvo_Registro">Entidad a procesar</param>
        ''' <returns>Mayor a cero: El proceso ha sido exitoso. Menor a cero: El proceso ha fallado</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>31/08/2015 08:20:01 a.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Function BorrarRegistro(ByVal pvo_Registro As EntOtmActividad) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOtmActividad As DalOtmActividad
            Dim vln_Resultado As Integer

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                If PoseeRegistrosAsociados(pvo_Registro.IdCategoriaServicio, pvo_Registro.IdActividad) Then
                    vln_Resultado = -1
                    Throw New OrdenesDeTrabajoException("El registro no puede ser borrado ya que está asociado a información de órdenes de trabajo. Si lo desea, puede cambiar su estado a Inactivo desde la opción Modificar.")
                End If

                vlo_DalOtmActividad = New DalOtmActividad(vlo_Conexion)
                vln_Resultado = vlo_DalOtmActividad.BorrarRegistro(pvo_Registro)
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
        ''' <param name="pvn_IdCategoriaServicio">Llave primaria de la tabla otm_categoria_servicio que se asocia con la secuencia sq_id_categoria_servicio</param>
        ''' <param name="pvn_IdActividad">Llave primaria de la tabla otm_actividad que se asocia con la secuencia sq_id_actividad</param>
        ''' <returns>Entidad que representa la información obtenida al ejecutar la consulta</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>31/08/2015 08:20:01 a.m.</creationDate>
        ''' <changeLog></changeLog>
        Private Function ObtenerRegistroPorLlavePrimaria(pvn_IdCategoriaServicio As Integer, pvn_IdActividad As Integer) As EntOtmActividad
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOtmActividad As DalOtmActividad

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                vlo_DalOtmActividad = New DalOtmActividad(vlo_Conexion)
                Return vlo_DalOtmActividad.ObtenerRegistro(String.Format("{0} = {1} AND {2} = {3}", Modelo.OTM_ACTIVIDAD.ID_CATEGORIA_SERVICIO, pvn_IdCategoriaServicio, Modelo.OTM_ACTIVIDAD.ID_ACTIVIDAD, pvn_IdActividad))
            Catch vlo_Excepcion As Exception
                Throw
            Finally
                If vlb_LiberarConexion Then
                    vlo_Conexion.Dispose()
                End If
            End Try
        End Function

        ''' <summary>
        ''' Permite obtener un registro según su llave alterna
        ''' </summary>
        ''' <param name="pvc_Descripcion">Descripción de la actividad</param>
        ''' <returns>Entidad que representa la información obtenida al ejecutar la consulta</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>31/08/2015 08:20:01 a.m.</creationDate>
        ''' <changeLog></changeLog>
        Private Function ObtenerRegistroPorLlaveAlterna(pvc_Descripcion As String) As EntOtmActividad
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOtmActividad As DalOtmActividad

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                vlo_DalOtmActividad = New DalOtmActividad(vlo_Conexion)
                Return vlo_DalOtmActividad.ObtenerRegistro(String.Format("UPPER({0}) = '{1}'", Modelo.OTM_ACTIVIDAD.DESCRIPCION, pvc_Descripcion.ToUpper()))
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
        ''' <param name="pvn_IdCategoriaServicio">Llave primaria de la tabla otm_categoria_servicio que se asocia con la secuencia sq_id_categoria_servicio</param>
        ''' <param name="pvn_IdActividad">Llave primaria de la tabla otm_actividad que se asocia con la secuencia sq_id_actividad</param>
        ''' <returns>True: posee datos asociados. False: No posee datos asociados</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>31/08/2015 08:20:01 a.m.</creationDate>
        ''' <changeLog></changeLog>
        Private Function PoseeRegistrosAsociados(pvn_IdCategoriaServicio As Integer, pvn_IdActividad As Integer) As Boolean
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
                If vlo_DalOttOrdenTrabajo.ObtenerRegistro(String.Format("{0} = {1} AND {2} = {3}", Modelo.OTT_ORDEN_TRABAJO.ID_CATEGORIA_SERVICIO, pvn_IdCategoriaServicio, Modelo.OTT_ORDEN_TRABAJO.ID_ACTIVIDAD, pvn_IdActividad)).Existe Then
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
