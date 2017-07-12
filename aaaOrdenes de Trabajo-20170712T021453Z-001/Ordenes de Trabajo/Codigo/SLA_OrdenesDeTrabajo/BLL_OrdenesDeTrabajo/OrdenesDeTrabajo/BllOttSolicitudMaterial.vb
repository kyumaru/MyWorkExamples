Imports Utilerias.OrdenesDeTrabajo
Imports OrdenesDeTrabajo.EntidadNegocio.OrdenesDeTrabajo
Imports OrdenesDeTrabajo.AccesoDatos.OrdenesDeTrabajo
Imports Utilerias.BaseDatos.OracleServer
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling

Namespace OrdenesDeTrabajo.LogicaNegocio.OrdenesDeTrabajo
	Public Class BllOttSolicitudMaterial
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
		''' Permite agregar un registro en la tabla OTT_SOLICITUD_MATERIAL, no sin antes aplicar la validación de la llave primaria o alterna según corresponda
		''' </summary>
		''' <param name="pvo_Registro">Entidad a procesar</param>
		''' <returns>Mayor a cero: El proceso ha sido exitoso; el valor puede corresponder a la llave autogenerada. Menor a cero: El proceso ha fallado</returns>
		''' <author>Generador de Código basado en objetos Oracle</author>
		''' <creationDate>03/06/2016 02:42:19 p.m.</creationDate>
		''' <changeLog></changeLog>
		Public Function InsertarRegistro(ByVal pvo_Registro As EntOttSolicitudMaterial) As Integer
			Dim vlo_Conexion As ConexionOracle
			Dim vlb_LiberarConexion As Boolean
			Dim vlo_DalOttSolicitudMaterial As DalOttSolicitudMaterial
			Dim vln_Resultado As Integer

			Try
				If vgo_Conexion Is Nothing Then
					vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
					vlb_LiberarConexion = True
				Else
					vlo_Conexion = vgo_Conexion
					vlb_LiberarConexion = False
				End If

				If ObtenerRegistroPorLlavePrimaria(pvo_Registro.IdUbicacion, pvo_Registro.IdOrdenTrabajo).Existe Then
					vln_Resultado = -1
					Throw New OrdenesDeTrabajoException("[TODO] Llave primaria repetida")
				End If

				vlo_DalOttSolicitudMaterial = New DalOttSolicitudMaterial(vlo_Conexion)
				vln_Resultado = vlo_DalOttSolicitudMaterial.InsertarRegistro(pvo_Registro)
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
		''' Permite borrar un registro en la tabla OTT_SOLICITUD_MATERIAL, no sin antes aplicar la validación de dependencia con tablas relacionadas
		''' </summary>
		''' <param name="pvo_Registro">Entidad a procesar</param>
		''' <returns>Mayor a cero: El proceso ha sido exitoso. Menor a cero: El proceso ha fallado</returns>
		''' <author>Generador de Código basado en objetos Oracle</author>
		''' <creationDate>03/06/2016 02:42:19 p.m.</creationDate>
		''' <changeLog></changeLog>
		Public Function BorrarRegistro(ByVal pvo_Registro As EntOttSolicitudMaterial) As Integer
			Dim vlo_Conexion As ConexionOracle
			Dim vlb_LiberarConexion As Boolean
			Dim vlo_DalOttSolicitudMaterial As DalOttSolicitudMaterial
			Dim vln_Resultado As Integer

			Try
				If vgo_Conexion Is Nothing Then
					vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
					vlb_LiberarConexion = True
				Else
					vlo_Conexion = vgo_Conexion
					vlb_LiberarConexion = False
				End If

				If PoseeRegistrosAsociados(pvo_Registro.IdUbicacion, pvo_Registro.IdOrdenTrabajo) Then
					vln_Resultado = -1
					Throw New OrdenesDeTrabajoException("[TODO] Hay registro asociados")
				End If

				vlo_DalOttSolicitudMaterial = New DalOttSolicitudMaterial(vlo_Conexion)
				vln_Resultado = vlo_DalOttSolicitudMaterial.BorrarRegistro(pvo_Registro)
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
		''' <param name="pvc_IdOrdenTrabajo">Identificador único alfanumérico de la orden de trabajo</param>
		''' <returns>Entidad que representa la información obtenida al ejecutar la consulta</returns>
		''' <author>Generador de Código basado en objetos Oracle</author>
		''' <creationDate>03/06/2016 02:42:19 p.m.</creationDate>
		''' <changeLog></changeLog>
		Private Function ObtenerRegistroPorLlavePrimaria(pvn_IdUbicacion As Integer, pvc_IdOrdenTrabajo As String) As EntOttSolicitudMaterial
			Dim vlo_Conexion As ConexionOracle
			Dim vlb_LiberarConexion As Boolean
			Dim vlo_DalOttSolicitudMaterial As DalOttSolicitudMaterial

			Try
				If vgo_Conexion Is Nothing Then
					vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
					vlb_LiberarConexion = True
				Else
					vlo_Conexion = vgo_Conexion
					vlb_LiberarConexion = False
				End If

				vlo_DalOttSolicitudMaterial = New DalOttSolicitudMaterial(vlo_Conexion)
				Return vlo_DalOttSolicitudMaterial.ObtenerRegistro(String.Format("{0} = {1} AND UPPER({2}) = '{3}'", Modelo.OTT_SOLICITUD_MATERIAL.ID_UBICACION, pvn_IdUbicacion, Modelo.OTT_SOLICITUD_MATERIAL.ID_ORDEN_TRABAJO, pvc_IdOrdenTrabajo.ToUpper()))
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
		''' <param name="pvc_IdOrdenTrabajo">Identificador único alfanumérico de la orden de trabajo</param>
		''' <returns>True: posee datos asociados. False: No posee datos asociados</returns>
		''' <author>Generador de Código basado en objetos Oracle</author>
		''' <creationDate>03/06/2016 02:42:19 p.m.</creationDate>
		''' <changeLog></changeLog>
		Private Function PoseeRegistrosAsociados(pvn_IdUbicacion As Integer, pvc_IdOrdenTrabajo As String) As Boolean
			Dim vlo_Conexion As ConexionOracle
			Dim vlb_LiberarConexion As Boolean
			Dim vlo_PoseeRegistrosAsociados As Boolean
			Dim vlo_DalOtlDetalleMaterial As DalOtlDetalleMaterial
			Dim vlo_DalOttDetalleMaterial As DalOttDetalleMaterial

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

				'Determinar la existencia de registros asociados en la tabla OTL_DETALLE_MATERIAL
				vlo_DalOtlDetalleMaterial = New DalOtlDetalleMaterial(vlo_Conexion)
				If vlo_DalOtlDetalleMaterial.ObtenerRegistro(String.Format("{0} = {1} AND UPPER({2}) = '{3}'", Modelo.OTL_DETALLE_MATERIAL.ID_UBICACION, pvn_IdUbicacion, Modelo.OTL_DETALLE_MATERIAL.ID_ORDEN_TRABAJO, pvc_IdOrdenTrabajo.ToUpper())).Existe Then
					Return True
				End If

				'Determinar la existencia de registros asociados en la tabla OTT_DETALLE_MATERIAL
				vlo_DalOttDetalleMaterial = New DalOttDetalleMaterial(vlo_Conexion)
				If vlo_DalOttDetalleMaterial.ObtenerRegistro(String.Format("{0} = {1} AND UPPER({2}) = '{3}'", Modelo.OTT_DETALLE_MATERIAL.ID_UBICACION, pvn_IdUbicacion, Modelo.OTT_DETALLE_MATERIAL.ID_ORDEN_TRABAJO, pvc_IdOrdenTrabajo.ToUpper())).Existe Then
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
