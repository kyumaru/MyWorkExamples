Imports Utilerias.OrdenesDeTrabajo
Imports OrdenesDeTrabajo.EntidadNegocio.Catalogos
Imports OrdenesDeTrabajo.AccesoDatos.Catalogos
Imports OrdenesDeTrabajo.AccesoDatos.OrdenesDeTrabajo
Imports Utilerias.BaseDatos.OracleServer
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling

Namespace OrdenesDeTrabajo.LogicaNegocio.Catalogos
	Public Class BllOtmUbicacion
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
		''' Permite agregar un registro en la tabla OTM_UBICACION, no sin antes aplicar la validación de la llave primaria o alterna según corresponda
		''' </summary>
		''' <param name="pvo_Registro">Entidad a procesar</param>
		''' <returns>Mayor a cero: El proceso ha sido exitoso; el valor puede corresponder a la llave autogenerada. Menor a cero: El proceso ha fallado</returns>
		''' <author>Generador de Código basado en objetos Oracle</author>
		''' <creationDate>31/08/2015 08:20:01 a.m.</creationDate>
		''' <changeLog></changeLog>
		Public Function InsertarRegistro(ByVal pvo_Registro As EntOtmUbicacion) As Integer
			Dim vlo_Conexion As ConexionOracle
			Dim vlb_LiberarConexion As Boolean
			Dim vlo_DalOtmUbicacion As DalOtmUbicacion
			Dim vln_Resultado As Integer

			Try
				If vgo_Conexion Is Nothing Then
					vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
					vlb_LiberarConexion = True
				Else
					vlo_Conexion = vgo_Conexion
					vlb_LiberarConexion = False
				End If

				If ObtenerRegistroPorLlavePrimaria(pvo_Registro.IdUbicacion).Existe Then
					vln_Resultado = -1
                    Throw New OrdenesDeTrabajoException("No se puede agregar el registro, ya existe una ubicación con la descripción indicada.")
				End If

				If ObtenerRegistroPorLlaveAlterna(pvo_Registro.Descripcion).Existe Then
					vln_Resultado = -1
                    Throw New OrdenesDeTrabajoException("No se puede agregar el registro, ya existe una ubicación con la descripción indicada.")
				End If

				vlo_DalOtmUbicacion = New DalOtmUbicacion(vlo_Conexion)
				vln_Resultado = vlo_DalOtmUbicacion.InsertarRegistro(pvo_Registro)
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
		''' Permite modificar un registro en la tabla OTM_UBICACION, no sin antes aplicar la validación de la llave alterna
		''' </summary>
		''' <param name="pvo_Registro">Entidad a procesar</param>
		''' <returns>Mayor a cero: El proceso ha sido exitoso. Menor a cero: El proceso ha fallado</returns>
		''' <author>Generador de Código basado en objetos Oracle</author>
		''' <creationDate>31/08/2015 08:20:01 a.m.</creationDate>
		''' <changeLog></changeLog>
		Public Function ModificarRegistro(ByVal pvo_Registro As EntOtmUbicacion) As Integer
			Dim vlo_Conexion As ConexionOracle
			Dim vlb_LiberarConexion As Boolean
			Dim vlo_DalOtmUbicacion As DalOtmUbicacion
			Dim vlo_EntOtmUbicacion As EntOtmUbicacion
			Dim vln_Resultado As Integer

			Try
				If vgo_Conexion Is Nothing Then
					vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
					vlb_LiberarConexion = True
				Else
					vlo_Conexion = vgo_Conexion
					vlb_LiberarConexion = False
				End If

				vlo_EntOtmUbicacion = ObtenerRegistroPorLlaveAlterna(pvo_Registro.Descripcion)
				If vlo_EntOtmUbicacion.Existe AndAlso vlo_EntOtmUbicacion.IdUbicacion <> pvo_Registro.IdUbicacion Then
					vln_Resultado = -1
                    Throw New OrdenesDeTrabajoException("No se puede modificar el registro, ya existe una ubicación con la descripción indicada.")
				End If

				vlo_DalOtmUbicacion = New DalOtmUbicacion(vlo_Conexion)
				vln_Resultado = vlo_DalOtmUbicacion.ModificarRegistro(pvo_Registro)
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
		''' Permite borrar un registro en la tabla OTM_UBICACION, no sin antes aplicar la validación de dependencia con tablas relacionadas
		''' </summary>
		''' <param name="pvo_Registro">Entidad a procesar</param>
		''' <returns>Mayor a cero: El proceso ha sido exitoso. Menor a cero: El proceso ha fallado</returns>
		''' <author>Generador de Código basado en objetos Oracle</author>
		''' <creationDate>31/08/2015 08:20:01 a.m.</creationDate>
		''' <changeLog></changeLog>
		Public Function BorrarRegistro(ByVal pvo_Registro As EntOtmUbicacion) As Integer
			Dim vlo_Conexion As ConexionOracle
			Dim vlb_LiberarConexion As Boolean
			Dim vlo_DalOtmUbicacion As DalOtmUbicacion
			Dim vln_Resultado As Integer

			Try
				If vgo_Conexion Is Nothing Then
					vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
					vlb_LiberarConexion = True
				Else
					vlo_Conexion = vgo_Conexion
					vlb_LiberarConexion = False
				End If

				If PoseeRegistrosAsociados(pvo_Registro.IdUbicacion) Then
					vln_Resultado = -1
                    Throw New OrdenesDeTrabajoException("El registro no puede ser borrado ya que está asociado a información de eficicios o sitios. Si lo desea, puede cambiar su estado a Inactivo desde la opción Modificar")
				End If

				vlo_DalOtmUbicacion = New DalOtmUbicacion(vlo_Conexion)
				vln_Resultado = vlo_DalOtmUbicacion.BorrarRegistro(pvo_Registro)
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
		''' <param name="pvn_IdUbicacion">Llave primaria de la tabla otm_ubicacion que se asocia con la secuencia sq_id_ubicacion</param>
		''' <returns>Entidad que representa la información obtenida al ejecutar la consulta</returns>
		''' <author>Generador de Código basado en objetos Oracle</author>
		''' <creationDate>31/08/2015 08:20:01 a.m.</creationDate>
		''' <changeLog></changeLog>
		Private Function ObtenerRegistroPorLlavePrimaria(pvn_IdUbicacion As Integer) As EntOtmUbicacion
			Dim vlo_Conexion As ConexionOracle
			Dim vlb_LiberarConexion As Boolean
			Dim vlo_DalOtmUbicacion As DalOtmUbicacion

			Try
				If vgo_Conexion Is Nothing Then
					vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
					vlb_LiberarConexion = True
				Else
					vlo_Conexion = vgo_Conexion
					vlb_LiberarConexion = False
				End If

				vlo_DalOtmUbicacion = New DalOtmUbicacion(vlo_Conexion)
				Return vlo_DalOtmUbicacion.ObtenerRegistro(String.Format("{0} = {1}", Modelo.OTM_UBICACION.ID_UBICACION, pvn_IdUbicacion))
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
		''' <param name="pvc_Descripcion">Descripción de la ubicación</param>
		''' <returns>Entidad que representa la información obtenida al ejecutar la consulta</returns>
		''' <author>Generador de Código basado en objetos Oracle</author>
		''' <creationDate>31/08/2015 08:20:01 a.m.</creationDate>
		''' <changeLog></changeLog>
		Private Function ObtenerRegistroPorLlaveAlterna(pvc_Descripcion As String) As EntOtmUbicacion
			Dim vlo_Conexion As ConexionOracle
			Dim vlb_LiberarConexion As Boolean
			Dim vlo_DalOtmUbicacion As DalOtmUbicacion

			Try
				If vgo_Conexion Is Nothing Then
					vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
					vlb_LiberarConexion = True
				Else
					vlo_Conexion = vgo_Conexion
					vlb_LiberarConexion = False
				End If

				vlo_DalOtmUbicacion = New DalOtmUbicacion(vlo_Conexion)
				Return vlo_DalOtmUbicacion.ObtenerRegistro(String.Format("UPPER({0}) = '{1}'", Modelo.OTM_UBICACION.DESCRIPCION, pvc_Descripcion.ToUpper()))
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
		''' <param name="pvn_IdUbicacion">Llave primaria de la tabla otm_ubicacion que se asocia con la secuencia sq_id_ubicacion</param>
		''' <returns>True: posee datos asociados. False: No posee datos asociados</returns>
		''' <author>Generador de Código basado en objetos Oracle</author>
		''' <creationDate>31/08/2015 08:20:01 a.m.</creationDate>
		''' <changeLog></changeLog>
		Private Function PoseeRegistrosAsociados(pvn_IdUbicacion As Integer) As Boolean
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_PoseeRegistrosAsociados As Boolean
            Dim vlo_DalOtmAutorizadoUbicacion As DalOtmAutorizadoUbicacion
            Dim vlo_DalOtmUnidadUbicacion As DalOtmUnidadUbicacion
            Dim vlo_DalOtmLugarTrabajo As DalOtmLugarTrabajo
            Dim vlo_DalOtmCategoriaServicio As DalOtmCategoriaServicio
            Dim vlo_DalOtmSectorTaller As DalOtmSectorTaller
            Dim vlo_DalOtfUbicacionFavorita As DalOtfUbicacionFavorita
            Dim vlo_DalOttOrdenTrabajo As DalOttOrdenTrabajo
            Dim vlo_DalOtfPlaneacionPreventivo As DalOtfPlaneacionPreventivo

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

                'Determinar la existencia de registros asociados en la tabla OTM_AUTORIZADO_UBICACION
                vlo_DalOtmAutorizadoUbicacion = New DalOtmAutorizadoUbicacion(vlo_Conexion)
                If vlo_DalOtmAutorizadoUbicacion.ObtenerRegistro(String.Format("{0} = {1}", Modelo.OTM_AUTORIZADO_UBICACION.ID_UBICACION_ADMINISTRA, pvn_IdUbicacion)).Existe Then
                    Return True
                End If

                'Determinar la existencia de registros asociados en la tabla OTM_UNIDAD_UBICACION
                vlo_DalOtmUnidadUbicacion = New DalOtmUnidadUbicacion(vlo_Conexion)
                If vlo_DalOtmUnidadUbicacion.ObtenerRegistro(String.Format("{0} = {1}", Modelo.OTM_UNIDAD_UBICACION.ID_UBICACION, pvn_IdUbicacion)).Existe Then
                    Return True
                End If

                'Determinar la existencia de registros asociados en la tabla OTM_LUGAR_TRABAJO
                vlo_DalOtmLugarTrabajo = New DalOtmLugarTrabajo(vlo_Conexion)
                If vlo_DalOtmLugarTrabajo.ObtenerRegistro(String.Format("{0} = {1}", Modelo.OTM_LUGAR_TRABAJO.ID_UBICACION_PERTENECE, pvn_IdUbicacion)).Existe Then
                    Return True
                End If

                'Determinar la existencia de registros asociados en la tabla OTM_LUGAR_TRABAJO
                vlo_DalOtmLugarTrabajo = New DalOtmLugarTrabajo(vlo_Conexion)
                If vlo_DalOtmLugarTrabajo.ObtenerRegistro(String.Format("{0} = {1}", Modelo.OTM_LUGAR_TRABAJO.ID_UBICACION_ADMINISTRA, pvn_IdUbicacion)).Existe Then
                    Return True
                End If

                'Determinar la existencia de registros asociados en la tabla OTM_CATEGORIA_SERVICIO
                vlo_DalOtmCategoriaServicio = New DalOtmCategoriaServicio(vlo_Conexion)
                If vlo_DalOtmCategoriaServicio.ObtenerRegistro(String.Format("{0} = {1}", Modelo.OTM_CATEGORIA_SERVICIO.ID_UBICACION_ADMINISTRA, pvn_IdUbicacion)).Existe Then
                    Return True
                End If

                'Determinar la existencia de registros asociados en la tabla OTM_SECTOR_TALLER
                vlo_DalOtmSectorTaller = New DalOtmSectorTaller(vlo_Conexion)
                If vlo_DalOtmSectorTaller.ObtenerRegistro(String.Format("{0} = {1}", Modelo.OTM_SECTOR_TALLER.ID_UBICACION_ADMINISTRA, pvn_IdUbicacion)).Existe Then
                    Return True
                End If

                'Determinar la existencia de registros asociados en la tabla OTF_UBICACION_FAVORITA
                vlo_DalOtfUbicacionFavorita = New DalOtfUbicacionFavorita(vlo_Conexion)
                If vlo_DalOtfUbicacionFavorita.ObtenerRegistro(String.Format("{0} = {1}", Modelo.OTF_UBICACION_FAVORITA.ID_UBICACION, pvn_IdUbicacion)).Existe Then
                    Return True
                End If

                'Determinar la existencia de registros asociados en la tabla OTF_ORDEN_TRABAJO
                vlo_DalOttOrdenTrabajo = New DalOttOrdenTrabajo(vlo_Conexion)
                If vlo_DalOttOrdenTrabajo.ObtenerRegistro(String.Format("{0} = {1}", Modelo.OTT_ORDEN_TRABAJO.ID_UBICACION, pvn_IdUbicacion)).Existe Then
                    Return True
                End If

                'Determinar la existencia de registros asociados en la tabla OTF_PLANEACION_PREVENTIVO
                vlo_DalOtfPlaneacionPreventivo = New DalOtfPlaneacionPreventivo(vlo_Conexion)
                If vlo_DalOtfPlaneacionPreventivo.ObtenerRegistro(String.Format("{0} = {1}", Modelo.OTF_PLANEACION_PREVENTIVO.ID_UBICACION_ADMINISTRA, pvn_IdUbicacion)).Existe Then
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
