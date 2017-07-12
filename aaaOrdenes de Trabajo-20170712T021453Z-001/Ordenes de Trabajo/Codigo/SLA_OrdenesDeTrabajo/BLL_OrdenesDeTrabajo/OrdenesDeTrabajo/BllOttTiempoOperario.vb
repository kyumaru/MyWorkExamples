Imports Utilerias.OrdenesDeTrabajo
Imports OrdenesDeTrabajo.EntidadNegocio.OrdenesDeTrabajo
Imports OrdenesDeTrabajo.AccesoDatos.OrdenesDeTrabajo
Imports Utilerias.BaseDatos.OracleServer
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling

Namespace OrdenesDeTrabajo.LogicaNegocio.OrdenesDeTrabajo
	Public Class BllOttTiempoOperario
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
		''' Permite agregar un registro en la tabla OTT_TIEMPO_OPERARIO, no sin antes aplicar la validación de la llave primaria o alterna según corresponda
		''' </summary>
		''' <param name="pvo_Registro">Entidad a procesar</param>
		''' <returns>Mayor a cero: El proceso ha sido exitoso; el valor puede corresponder a la llave autogenerada. Menor a cero: El proceso ha fallado</returns>
		''' <author>Generador de Código basado en objetos Oracle</author>
		''' <creationDate>03/02/2016 11:15:36 a.m.</creationDate>
		''' <changeLog></changeLog>
		Public Function InsertarRegistro(ByVal pvo_Registro As EntOttTiempoOperario) As Integer
			Dim vlo_Conexion As ConexionOracle
			Dim vlb_LiberarConexion As Boolean
			Dim vlo_DalOttTiempoOperario As DalOttTiempoOperario
			Dim vln_Resultado As Integer

			Try
				If vgo_Conexion Is Nothing Then
					vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
					vlb_LiberarConexion = True
				Else
					vlo_Conexion = vgo_Conexion
					vlb_LiberarConexion = False
				End If

				If ObtenerRegistroPorLlavePrimaria(pvo_Registro.NumEmpleado, pvo_Registro.IdSectorTaller, pvo_Registro.IdUbicacion, pvo_Registro.IdOrdenTrabajo, pvo_Registro.IdEtapaOrdenTrabajo).Existe Then
					vln_Resultado = -1
                    Throw New OrdenesDeTrabajoException("Llave primaria repetida")
				End If

				vlo_DalOttTiempoOperario = New DalOttTiempoOperario(vlo_Conexion)
				vln_Resultado = vlo_DalOttTiempoOperario.InsertarRegistro(pvo_Registro)
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
		''' <param name="pvn_NumEmpleado"></param>
		''' <param name="pvn_IdSectorTaller">Llave primaria de la tabla otm_sector_taller que se asocia con la secuencia sq_id_sector_taller</param>
		''' <param name="pvn_IdUbicacion">Llave primaria de la tabla otm_ubicacion que se asocia con la secuencia sq_id_ubicacion</param>
		''' <param name="pvc_IdOrdenTrabajo">Identificador único alfanumérico de la orden de trabajo</param>
		''' <param name="pvn_IdEtapaOrdenTrabajo">Llave primaria de la tabla otm_etapa_orden_trabajo que se asocia con la secuencia sq_id_etapa_orden_trabajo</param>
		''' <returns>Entidad que representa la información obtenida al ejecutar la consulta</returns>
		''' <author>Generador de Código basado en objetos Oracle</author>
		''' <creationDate>03/02/2016 11:15:36 a.m.</creationDate>
		''' <changeLog></changeLog>
		Private Function ObtenerRegistroPorLlavePrimaria(pvn_NumEmpleado As Double, pvn_IdSectorTaller As Integer, pvn_IdUbicacion As Integer, pvc_IdOrdenTrabajo As String, pvn_IdEtapaOrdenTrabajo As Integer) As EntOttTiempoOperario
			Dim vlo_Conexion As ConexionOracle
			Dim vlb_LiberarConexion As Boolean
			Dim vlo_DalOttTiempoOperario As DalOttTiempoOperario

			Try
				If vgo_Conexion Is Nothing Then
					vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
					vlb_LiberarConexion = True
				Else
					vlo_Conexion = vgo_Conexion
					vlb_LiberarConexion = False
				End If

				vlo_DalOttTiempoOperario = New DalOttTiempoOperario(vlo_Conexion)
				Return vlo_DalOttTiempoOperario.ObtenerRegistro(String.Format("{0} = {1} AND {2} = {3} AND {4} = {5} AND UPPER({6}) = '{7}' AND {8} = {9}", Modelo.OTT_TIEMPO_OPERARIO.NUM_EMPLEADO, pvn_NumEmpleado, Modelo.OTT_TIEMPO_OPERARIO.ID_SECTOR_TALLER, pvn_IdSectorTaller, Modelo.OTT_TIEMPO_OPERARIO.ID_UBICACION, pvn_IdUbicacion, Modelo.OTT_TIEMPO_OPERARIO.ID_ORDEN_TRABAJO, pvc_IdOrdenTrabajo.ToUpper(), Modelo.OTT_TIEMPO_OPERARIO.ID_ETAPA_ORDEN_TRABAJO, pvn_IdEtapaOrdenTrabajo))
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
