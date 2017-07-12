Imports Utilerias.OrdenesDeTrabajo
Imports OrdenesDeTrabajo.EntidadNegocio.Catalogos
Imports OrdenesDeTrabajo.AccesoDatos.Catalogos
Imports Utilerias.BaseDatos.OracleServer
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling

Namespace OrdenesDeTrabajo.LogicaNegocio.Catalogos
	Public Class BllOtmUnidadMedida
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
		''' Permite agregar un registro en la tabla OTM_UNIDAD_MEDIDA, no sin antes aplicar la validación de la llave primaria o alterna según corresponda
		''' </summary>
		''' <param name="pvo_Registro">Entidad a procesar</param>
		''' <returns>Mayor a cero: El proceso ha sido exitoso; el valor puede corresponder a la llave autogenerada. Menor a cero: El proceso ha fallado</returns>
		''' <author>Generador de Código basado en objetos Oracle</author>
		''' <creationDate>23/05/2016 10:24:54 a.m.</creationDate>
		''' <changeLog></changeLog>
		Public Function InsertarRegistro(ByVal pvo_Registro As EntOtmUnidadMedida) As Integer
			Dim vlo_Conexion As ConexionOracle
			Dim vlb_LiberarConexion As Boolean
			Dim vlo_DalOtmUnidadMedida As DalOtmUnidadMedida
			Dim vln_Resultado As Integer

			Try
				If vgo_Conexion Is Nothing Then
					vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
					vlb_LiberarConexion = True
				Else
					vlo_Conexion = vgo_Conexion
					vlb_LiberarConexion = False
				End If

				If ObtenerRegistroPorLlaveAlterna(pvo_Registro.Descripcion).Existe Then
					vln_Resultado = -1
                    Throw New OrdenesDeTrabajoException("Descripción ya existe en otro registro.")
				End If

				vlo_DalOtmUnidadMedida = New DalOtmUnidadMedida(vlo_Conexion)
				vln_Resultado = vlo_DalOtmUnidadMedida.InsertarRegistro(pvo_Registro)
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
		''' Permite modificar un registro en la tabla OTM_UNIDAD_MEDIDA, no sin antes aplicar la validación de la llave alterna
		''' </summary>
		''' <param name="pvo_Registro">Entidad a procesar</param>
		''' <returns>Mayor a cero: El proceso ha sido exitoso. Menor a cero: El proceso ha fallado</returns>
		''' <author>Generador de Código basado en objetos Oracle</author>
		''' <creationDate>23/05/2016 10:24:54 a.m.</creationDate>
		''' <changeLog></changeLog>
		Public Function ModificarRegistro(ByVal pvo_Registro As EntOtmUnidadMedida) As Integer
			Dim vlo_Conexion As ConexionOracle
			Dim vlb_LiberarConexion As Boolean
			Dim vlo_DalOtmUnidadMedida As DalOtmUnidadMedida
			Dim vlo_EntOtmUnidadMedida As EntOtmUnidadMedida
			Dim vln_Resultado As Integer

			Try
				If vgo_Conexion Is Nothing Then
					vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
					vlb_LiberarConexion = True
				Else
					vlo_Conexion = vgo_Conexion
					vlb_LiberarConexion = False
				End If

				vlo_EntOtmUnidadMedida = ObtenerRegistroPorLlaveAlterna(pvo_Registro.Descripcion)
				If vlo_EntOtmUnidadMedida.Existe AndAlso vlo_EntOtmUnidadMedida.IdUnidadMedida <> pvo_Registro.IdUnidadMedida Then
					vln_Resultado = -1
                    Throw New OrdenesDeTrabajoException("Descripción ya existe en otro registro.")
				End If

				vlo_DalOtmUnidadMedida = New DalOtmUnidadMedida(vlo_Conexion)
				vln_Resultado = vlo_DalOtmUnidadMedida.ModificarRegistro(pvo_Registro)
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
		''' Permite borrar un registro en la tabla OTM_UNIDAD_MEDIDA, no sin antes aplicar la validación de dependencia con tablas relacionadas
		''' </summary>
		''' <param name="pvo_Registro">Entidad a procesar</param>
		''' <returns>Mayor a cero: El proceso ha sido exitoso. Menor a cero: El proceso ha fallado</returns>
		''' <author>Generador de Código basado en objetos Oracle</author>
		''' <creationDate>23/05/2016 10:24:54 a.m.</creationDate>
		''' <changeLog></changeLog>
		Public Function BorrarRegistro(ByVal pvo_Registro As EntOtmUnidadMedida) As Integer
			Dim vlo_Conexion As ConexionOracle
			Dim vlb_LiberarConexion As Boolean
			Dim vlo_DalOtmUnidadMedida As DalOtmUnidadMedida
			Dim vln_Resultado As Integer

			Try
				If vgo_Conexion Is Nothing Then
					vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
					vlb_LiberarConexion = True
				Else
					vlo_Conexion = vgo_Conexion
					vlb_LiberarConexion = False
				End If

				If PoseeRegistrosAsociados(pvo_Registro.IdUnidadMedida) Then
					vln_Resultado = -1
                    Throw New OrdenesDeTrabajoException("Hay registro asociados. No puede ser eliminado")
				End If

				vlo_DalOtmUnidadMedida = New DalOtmUnidadMedida(vlo_Conexion)
				vln_Resultado = vlo_DalOtmUnidadMedida.BorrarRegistro(pvo_Registro)
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
		''' Permite obtener un registro según su llave alterna
		''' </summary>
		''' <param name="pvc_Descripcion">Descripción de unidad de medida. ej: litro</param>
		''' <returns>Entidad que representa la información obtenida al ejecutar la consulta</returns>
		''' <author>Generador de Código basado en objetos Oracle</author>
		''' <creationDate>23/05/2016 10:24:54 a.m.</creationDate>
		''' <changeLog></changeLog>
		Private Function ObtenerRegistroPorLlaveAlterna(pvc_Descripcion As String) As EntOtmUnidadMedida
			Dim vlo_Conexion As ConexionOracle
			Dim vlb_LiberarConexion As Boolean
			Dim vlo_DalOtmUnidadMedida As DalOtmUnidadMedida

			Try
				If vgo_Conexion Is Nothing Then
					vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
					vlb_LiberarConexion = True
				Else
					vlo_Conexion = vgo_Conexion
					vlb_LiberarConexion = False
				End If

				vlo_DalOtmUnidadMedida = New DalOtmUnidadMedida(vlo_Conexion)
				Return vlo_DalOtmUnidadMedida.ObtenerRegistro(String.Format("UPPER({0}) = '{1}'", Modelo.OTM_UNIDAD_MEDIDA.DESCRIPCION, pvc_Descripcion.ToUpper()))
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
		''' <param name="pvn_IdUnidadMedida">Llave primaria de la tabla otm_unidad_medida que se asocia con la secuencia sq_id_unidad_medida</param>
		''' <returns>True: posee datos asociados. False: No posee datos asociados</returns>
		''' <author>Generador de Código basado en objetos Oracle</author>
		''' <creationDate>23/05/2016 10:24:54 a.m.</creationDate>
		''' <changeLog></changeLog>
		Private Function PoseeRegistrosAsociados(pvn_IdUnidadMedida As Integer) As Boolean
			Dim vlo_Conexion As ConexionOracle
			Dim vlb_LiberarConexion As Boolean
			Dim vlo_PoseeRegistrosAsociados As Boolean
            'Dim vlo_DalOtmMaterial As DalOtmMaterial

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

				'Determinar la existencia de registros asociados en la tabla OTM_MATERIAL
                'vlo_DalOtmMaterial = New DalOtmMaterial(vlo_Conexion)
                'If vlo_DalOtmMaterial.ObtenerRegistro(String.Format("{0} = {1}", Modelo.OTM_MATERIAL.ID_UNIDAD_MEDIDA, pvn_IdUnidadMedida)).Existe Then
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
