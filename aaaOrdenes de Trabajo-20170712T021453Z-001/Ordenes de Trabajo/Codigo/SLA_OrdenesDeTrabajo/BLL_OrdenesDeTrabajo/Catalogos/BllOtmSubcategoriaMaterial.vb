Imports Utilerias.OrdenesDeTrabajo
Imports OrdenesDeTrabajo.EntidadNegocio.Catalogos
Imports OrdenesDeTrabajo.AccesoDatos.Catalogos
Imports Utilerias.BaseDatos.OracleServer
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling

Namespace OrdenesDeTrabajo.LogicaNegocio.Catalogos
	Public Class BllOtmSubcategoriaMaterial
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
		''' Permite agregar un registro en la tabla OTM_SUBCATEGORIA_MATERIAL, no sin antes aplicar la validación de la llave primaria o alterna según corresponda
		''' </summary>
		''' <param name="pvo_Registro">Entidad a procesar</param>
		''' <returns>Mayor a cero: El proceso ha sido exitoso; el valor puede corresponder a la llave autogenerada. Menor a cero: El proceso ha fallado</returns>
		''' <author>Generador de Código basado en objetos Oracle</author>
		''' <creationDate>19/05/2016 09:54:59 a.m.</creationDate>
		''' <changeLog></changeLog>
		Public Function InsertarRegistro(ByVal pvo_Registro As EntOtmSubcategoriaMaterial) As Integer
			Dim vlo_Conexion As ConexionOracle
			Dim vlb_LiberarConexion As Boolean
			Dim vlo_DalOtmSubcategoriaMaterial As DalOtmSubcategoriaMaterial
			Dim vln_Resultado As Integer

			Try
				If vgo_Conexion Is Nothing Then
					vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
					vlb_LiberarConexion = True
				Else
					vlo_Conexion = vgo_Conexion
					vlb_LiberarConexion = False
				End If

				If ObtenerRegistroPorLlaveAlterna(pvo_Registro.IdUbicacionAdministra, pvo_Registro.Descripcion).Existe Then
					vln_Resultado = -1
                    Throw New OrdenesDeTrabajoException("Descripción ya existe en otro registro.")
				End If

				vlo_DalOtmSubcategoriaMaterial = New DalOtmSubcategoriaMaterial(vlo_Conexion)
				vln_Resultado = vlo_DalOtmSubcategoriaMaterial.InsertarRegistro(pvo_Registro)
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
		''' Permite modificar un registro en la tabla OTM_SUBCATEGORIA_MATERIAL, no sin antes aplicar la validación de la llave alterna
		''' </summary>
		''' <param name="pvo_Registro">Entidad a procesar</param>
		''' <returns>Mayor a cero: El proceso ha sido exitoso. Menor a cero: El proceso ha fallado</returns>
		''' <author>Generador de Código basado en objetos Oracle</author>
		''' <creationDate>19/05/2016 09:54:59 a.m.</creationDate>
		''' <changeLog></changeLog>
		Public Function ModificarRegistro(ByVal pvo_Registro As EntOtmSubcategoriaMaterial) As Integer
			Dim vlo_Conexion As ConexionOracle
			Dim vlb_LiberarConexion As Boolean
			Dim vlo_DalOtmSubcategoriaMaterial As DalOtmSubcategoriaMaterial
			Dim vlo_EntOtmSubcategoriaMaterial As EntOtmSubcategoriaMaterial
			Dim vln_Resultado As Integer

			Try
				If vgo_Conexion Is Nothing Then
					vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
					vlb_LiberarConexion = True
				Else
					vlo_Conexion = vgo_Conexion
					vlb_LiberarConexion = False
				End If

				vlo_EntOtmSubcategoriaMaterial = ObtenerRegistroPorLlaveAlterna(pvo_Registro.IdUbicacionAdministra, pvo_Registro.Descripcion)
				If vlo_EntOtmSubcategoriaMaterial.Existe AndAlso vlo_EntOtmSubcategoriaMaterial.IdSubcategoriaMaterial <> pvo_Registro.IdSubcategoriaMaterial Then
					vln_Resultado = -1
                    Throw New OrdenesDeTrabajoException("Descripción ya existe en otro registro.")
				End If

				vlo_DalOtmSubcategoriaMaterial = New DalOtmSubcategoriaMaterial(vlo_Conexion)
				vln_Resultado = vlo_DalOtmSubcategoriaMaterial.ModificarRegistro(pvo_Registro)
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
		''' Permite borrar un registro en la tabla OTM_SUBCATEGORIA_MATERIAL, no sin antes aplicar la validación de dependencia con tablas relacionadas
		''' </summary>
		''' <param name="pvo_Registro">Entidad a procesar</param>
		''' <returns>Mayor a cero: El proceso ha sido exitoso. Menor a cero: El proceso ha fallado</returns>
		''' <author>Generador de Código basado en objetos Oracle</author>
		''' <creationDate>19/05/2016 09:54:59 a.m.</creationDate>
		''' <changeLog></changeLog>
		Public Function BorrarRegistro(ByVal pvo_Registro As EntOtmSubcategoriaMaterial) As Integer
			Dim vlo_Conexion As ConexionOracle
			Dim vlb_LiberarConexion As Boolean
			Dim vlo_DalOtmSubcategoriaMaterial As DalOtmSubcategoriaMaterial
			Dim vln_Resultado As Integer

			Try
				If vgo_Conexion Is Nothing Then
					vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
					vlb_LiberarConexion = True
				Else
					vlo_Conexion = vgo_Conexion
					vlb_LiberarConexion = False
				End If

				If PoseeRegistrosAsociados(pvo_Registro.IdSubcategoriaMaterial) Then
					vln_Resultado = -1
                    Throw New OrdenesDeTrabajoException("Hay registro asociados")
				End If

				vlo_DalOtmSubcategoriaMaterial = New DalOtmSubcategoriaMaterial(vlo_Conexion)
				vln_Resultado = vlo_DalOtmSubcategoriaMaterial.BorrarRegistro(pvo_Registro)
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
		''' <param name="pvn_IdUbicacionAdministra">Id de la ubicación que administra los datos del catálogo</param>
		''' <param name="pvc_Descripcion">Descripción de la etapa a la cual pertenece el registro asociado.</param>
		''' <returns>Entidad que representa la información obtenida al ejecutar la consulta</returns>
		''' <author>Generador de Código basado en objetos Oracle</author>
		''' <creationDate>19/05/2016 09:54:59 a.m.</creationDate>
		''' <changeLog></changeLog>
		Private Function ObtenerRegistroPorLlaveAlterna(pvn_IdUbicacionAdministra As Integer, pvc_Descripcion As String) As EntOtmSubcategoriaMaterial
			Dim vlo_Conexion As ConexionOracle
			Dim vlb_LiberarConexion As Boolean
			Dim vlo_DalOtmSubcategoriaMaterial As DalOtmSubcategoriaMaterial

			Try
				If vgo_Conexion Is Nothing Then
					vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
					vlb_LiberarConexion = True
				Else
					vlo_Conexion = vgo_Conexion
					vlb_LiberarConexion = False
				End If

				vlo_DalOtmSubcategoriaMaterial = New DalOtmSubcategoriaMaterial(vlo_Conexion)
				Return vlo_DalOtmSubcategoriaMaterial.ObtenerRegistro(String.Format("{0} = {1} AND UPPER({2}) = '{3}'", Modelo.OTM_SUBCATEGORIA_MATERIAL.ID_UBICACION_ADMINISTRA, pvn_IdUbicacionAdministra, Modelo.OTM_SUBCATEGORIA_MATERIAL.DESCRIPCION, pvc_Descripcion.ToUpper()))
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
		''' <param name="pvn_IdSubcategoriaMaterial">Llave primaria de la tabla otm_subcategoria_material que se asocia con la secuencia sq_id_subcategoria_material</param>
		''' <returns>True: posee datos asociados. False: No posee datos asociados</returns>
		''' <author>Generador de Código basado en objetos Oracle</author>
		''' <creationDate>19/05/2016 09:54:59 a.m.</creationDate>
		''' <changeLog></changeLog>
		Private Function PoseeRegistrosAsociados(pvn_IdSubcategoriaMaterial As Integer) As Boolean
			Dim vlo_Conexion As ConexionOracle
			Dim vlb_LiberarConexion As Boolean
			Dim vlo_PoseeRegistrosAsociados As Boolean
            'Dim vlo_DalOtmSubcategoriaCategor As DalOtmSubcategoriaCategor

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

				'Determinar la existencia de registros asociados en la tabla OTM_SUBCATEGORIA_CATEGOR
                'vlo_DalOtmSubcategoriaCategor = New DalOtmSubcategoriaCategor(vlo_Conexion)
                'If vlo_DalOtmSubcategoriaCategor.ObtenerRegistro(String.Format("{0} = {1}", Modelo.OTM_SUBCATEGORIA_CATEGOR.ID_SUBCATEGORIA_MATERIAL, pvn_IdSubcategoriaMaterial)).Existe Then
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
