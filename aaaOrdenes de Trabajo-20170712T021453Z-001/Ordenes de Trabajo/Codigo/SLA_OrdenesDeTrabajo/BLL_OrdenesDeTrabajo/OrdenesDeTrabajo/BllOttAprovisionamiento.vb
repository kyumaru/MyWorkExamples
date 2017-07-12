Imports Utilerias.OrdenesDeTrabajo
Imports OrdenesDeTrabajo.EntidadNegocio.OrdenesDeTrabajo
Imports OrdenesDeTrabajo.AccesoDatos.OrdenesDeTrabajo
Imports Utilerias.BaseDatos.OracleServer
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling

Namespace OrdenesDeTrabajo.LogicaNegocio.OrdenesDeTrabajo
	Public Class BllOttAprovisionamiento
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
		''' Permite agregar un registro en la tabla OTT_APROVISIONAMIENTO, no sin antes aplicar la validación de la llave primaria o alterna según corresponda
		''' </summary>
		''' <param name="pvo_Registro">Entidad a procesar</param>
		''' <returns>Mayor a cero: El proceso ha sido exitoso; el valor puede corresponder a la llave autogenerada. Menor a cero: El proceso ha fallado</returns>
		''' <author>Generador de Código basado en objetos Oracle</author>
		''' <creationDate>11/01/2017 01:45:14 p.m.</creationDate>
		''' <changeLog></changeLog>
		Public Function InsertarRegistro(ByVal pvo_Registro As EntOttAprovisionamiento) As Integer
			Dim vlo_Conexion As ConexionOracle
			Dim vlb_LiberarConexion As Boolean
			Dim vlo_DalOttAprovisionamiento As DalOttAprovisionamiento
			Dim vln_Resultado As Integer

			Try
				If vgo_Conexion Is Nothing Then
					vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
					vlb_LiberarConexion = True
				Else
					vlo_Conexion = vgo_Conexion
					vlb_LiberarConexion = False
				End If

				If ObtenerRegistroPorLlavePrimaria(pvo_Registro.IdUbicacion, pvo_Registro.NumeroGestion, pvo_Registro.Anno).Existe Then
					vln_Resultado = -1
					Throw New OrdenesDeTrabajoException("[TODO] Llave primaria repetida")
				End If

				vlo_DalOttAprovisionamiento = New DalOttAprovisionamiento(vlo_Conexion)
				vln_Resultado = vlo_DalOttAprovisionamiento.InsertarRegistro(pvo_Registro)
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
		''' Permite borrar un registro en la tabla OTT_APROVISIONAMIENTO, no sin antes aplicar la validación de dependencia con tablas relacionadas
		''' </summary>
		''' <param name="pvo_Registro">Entidad a procesar</param>
		''' <returns>Mayor a cero: El proceso ha sido exitoso. Menor a cero: El proceso ha fallado</returns>
		''' <author>Generador de Código basado en objetos Oracle</author>
		''' <creationDate>11/01/2017 01:45:14 p.m.</creationDate>
		''' <changeLog></changeLog>
		Public Function BorrarRegistro(ByVal pvo_Registro As EntOttAprovisionamiento) As Integer
			Dim vlo_Conexion As ConexionOracle
			Dim vlb_LiberarConexion As Boolean
			Dim vlo_DalOttAprovisionamiento As DalOttAprovisionamiento
			Dim vln_Resultado As Integer

			Try
				If vgo_Conexion Is Nothing Then
					vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
					vlb_LiberarConexion = True
				Else
					vlo_Conexion = vgo_Conexion
					vlb_LiberarConexion = False
				End If

				If PoseeRegistrosAsociados(pvo_Registro.IdUbicacion, pvo_Registro.NumeroGestion, pvo_Registro.Anno) Then
					vln_Resultado = -1
					Throw New OrdenesDeTrabajoException("[TODO] Hay registro asociados")
				End If

				vlo_DalOttAprovisionamiento = New DalOttAprovisionamiento(vlo_Conexion)
				vln_Resultado = vlo_DalOttAprovisionamiento.BorrarRegistro(pvo_Registro)
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
		''' <param name="pvn_NumeroGestion">Consecutivo de la gestión</param>
		''' <param name="pvn_Anno">Año</param>
		''' <returns>Entidad que representa la información obtenida al ejecutar la consulta</returns>
		''' <author>Generador de Código basado en objetos Oracle</author>
		''' <creationDate>11/01/2017 01:45:14 p.m.</creationDate>
		''' <changeLog></changeLog>
		Private Function ObtenerRegistroPorLlavePrimaria(pvn_IdUbicacion As Integer, pvn_NumeroGestion As Integer, pvn_Anno As Integer) As EntOttAprovisionamiento
			Dim vlo_Conexion As ConexionOracle
			Dim vlb_LiberarConexion As Boolean
			Dim vlo_DalOttAprovisionamiento As DalOttAprovisionamiento

			Try
				If vgo_Conexion Is Nothing Then
					vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
					vlb_LiberarConexion = True
				Else
					vlo_Conexion = vgo_Conexion
					vlb_LiberarConexion = False
				End If

				vlo_DalOttAprovisionamiento = New DalOttAprovisionamiento(vlo_Conexion)
				Return vlo_DalOttAprovisionamiento.ObtenerRegistro(String.Format("{0} = {1} AND {2} = {3} AND {4} = {5}", Modelo.OTT_APROVISIONAMIENTO.ID_UBICACION, pvn_IdUbicacion, Modelo.OTT_APROVISIONAMIENTO.NUMERO_GESTION, pvn_NumeroGestion, Modelo.OTT_APROVISIONAMIENTO.ANNO, pvn_Anno))
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
		''' <param name="pvn_NumeroGestion">Consecutivo de la gestión</param>
		''' <param name="pvn_Anno">Año</param>
		''' <returns>True: posee datos asociados. False: No posee datos asociados</returns>
		''' <author>Generador de Código basado en objetos Oracle</author>
		''' <creationDate>11/01/2017 01:45:14 p.m.</creationDate>
		''' <changeLog></changeLog>
		Private Function PoseeRegistrosAsociados(pvn_IdUbicacion As Integer, pvn_NumeroGestion As Integer, pvn_Anno As Integer) As Boolean
			Dim vlo_Conexion As ConexionOracle
			Dim vlb_LiberarConexion As Boolean
			Dim vlo_PoseeRegistrosAsociados As Boolean
			Dim vlo_DalOttDetAprovisionamiento As DalOttDetAprovisionamiento
			Dim vlo_DalOtlDetAprovisionamiento As DalOtlDetAprovisionamiento

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

				'Determinar la existencia de registros asociados en la tabla OTT_DET_APROVISIONAMIENTO
				vlo_DalOttDetAprovisionamiento = New DalOttDetAprovisionamiento(vlo_Conexion)
				If vlo_DalOttDetAprovisionamiento.ObtenerRegistro(String.Format("{0} = {1} AND {2} = {3} AND {4} = {5}", Modelo.OTT_DET_APROVISIONAMIENTO.ID_UBICACION, pvn_IdUbicacion, Modelo.OTT_DET_APROVISIONAMIENTO.NUMERO_GESTION, pvn_NumeroGestion, Modelo.OTT_DET_APROVISIONAMIENTO.ANNO, pvn_Anno)).Existe Then
					Return True
				End If

				'Determinar la existencia de registros asociados en la tabla OTL_DET_APROVISIONAMIENTO
				vlo_DalOtlDetAprovisionamiento = New DalOtlDetAprovisionamiento(vlo_Conexion)
				If vlo_DalOtlDetAprovisionamiento.ObtenerRegistro(String.Format("{0} = {1} AND {2} = {3} AND {4} = {5}", Modelo.OTL_DET_APROVISIONAMIENTO.ID_UBICACION, pvn_IdUbicacion, Modelo.OTL_DET_APROVISIONAMIENTO.NUMERO_GESTION, pvn_NumeroGestion, Modelo.OTL_DET_APROVISIONAMIENTO.ANNO, pvn_Anno)).Existe Then
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
