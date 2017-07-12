Imports Utilerias.OrdenesDeTrabajo
Imports OrdenesDeTrabajo.EntidadNegocio.Catalogos
Imports OrdenesDeTrabajo.AccesoDatos.Catalogos
Imports Utilerias.BaseDatos.OracleServer
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling

Namespace OrdenesDeTrabajo.LogicaNegocio.Catalogos
	Public Class BllOtmSubcategoriaCategor
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
		''' Permite agregar un registro en la tabla OTM_SUBCATEGORIA_CATEGOR, no sin antes aplicar la validación de la llave primaria o alterna según corresponda
		''' </summary>
		''' <param name="pvo_Registro">Entidad a procesar</param>
		''' <returns>Mayor a cero: El proceso ha sido exitoso; el valor puede corresponder a la llave autogenerada. Menor a cero: El proceso ha fallado</returns>
		''' <author>Generador de Código basado en objetos Oracle</author>
		''' <creationDate>19/05/2016 05:14:54 p.m.</creationDate>
		''' <changeLog></changeLog>
		Public Function InsertarRegistro(ByVal pvo_Registro As EntOtmSubcategoriaCategor) As Integer
			Dim vlo_Conexion As ConexionOracle
			Dim vlb_LiberarConexion As Boolean
			Dim vlo_DalOtmSubcategoriaCategor As DalOtmSubcategoriaCategor
			Dim vln_Resultado As Integer

			Try
				If vgo_Conexion Is Nothing Then
					vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
					vlb_LiberarConexion = True
				Else
					vlo_Conexion = vgo_Conexion
					vlb_LiberarConexion = False
				End If

				If ObtenerRegistroPorLlavePrimaria(pvo_Registro.IdCategoriaMaterial, pvo_Registro.IdSubcategoriaMaterial).Existe Then
					vln_Resultado = -1
                    Throw New OrdenesDeTrabajoException("Llave primaria repetida")
				End If

				vlo_DalOtmSubcategoriaCategor = New DalOtmSubcategoriaCategor(vlo_Conexion)
				vln_Resultado = vlo_DalOtmSubcategoriaCategor.InsertarRegistro(pvo_Registro)
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
		''' Permite borrar un registro en la tabla OTM_SUBCATEGORIA_CATEGOR, no sin antes aplicar la validación de dependencia con tablas relacionadas
		''' </summary>
		''' <param name="pvo_Registro">Entidad a procesar</param>
		''' <returns>Mayor a cero: El proceso ha sido exitoso. Menor a cero: El proceso ha fallado</returns>
		''' <author>Generador de Código basado en objetos Oracle</author>
		''' <creationDate>19/05/2016 05:14:54 p.m.</creationDate>
		''' <changeLog></changeLog>
		Public Function BorrarRegistro(ByVal pvo_Registro As EntOtmSubcategoriaCategor) As Integer
			Dim vlo_Conexion As ConexionOracle
			Dim vlb_LiberarConexion As Boolean
			Dim vlo_DalOtmSubcategoriaCategor As DalOtmSubcategoriaCategor
			Dim vln_Resultado As Integer

			Try
				If vgo_Conexion Is Nothing Then
					vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
					vlb_LiberarConexion = True
				Else
					vlo_Conexion = vgo_Conexion
					vlb_LiberarConexion = False
				End If

				If PoseeRegistrosAsociados(pvo_Registro.IdCategoriaMaterial, pvo_Registro.IdSubcategoriaMaterial) Then
					vln_Resultado = -1
                    Throw New OrdenesDeTrabajoException("Hay registro asociados")
				End If

				vlo_DalOtmSubcategoriaCategor = New DalOtmSubcategoriaCategor(vlo_Conexion)
				vln_Resultado = vlo_DalOtmSubcategoriaCategor.BorrarRegistro(pvo_Registro)
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
		''' <param name="pvn_IdCategoriaMaterial">Llave primaria de la tabla otm_categoria_material que se asocia con la secuencia sq_id_categoria_material</param>
		''' <param name="pvn_IdSubcategoriaMaterial">Llave primaria de la tabla otm_subcategoria_material que se asocia con la secuencia sq_id_subcategoria_material</param>
		''' <returns>Entidad que representa la información obtenida al ejecutar la consulta</returns>
		''' <author>Generador de Código basado en objetos Oracle</author>
		''' <creationDate>19/05/2016 05:14:54 p.m.</creationDate>
		''' <changeLog></changeLog>
		Private Function ObtenerRegistroPorLlavePrimaria(pvn_IdCategoriaMaterial As Integer, pvn_IdSubcategoriaMaterial As Integer) As EntOtmSubcategoriaCategor
			Dim vlo_Conexion As ConexionOracle
			Dim vlb_LiberarConexion As Boolean
			Dim vlo_DalOtmSubcategoriaCategor As DalOtmSubcategoriaCategor

			Try
				If vgo_Conexion Is Nothing Then
					vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
					vlb_LiberarConexion = True
				Else
					vlo_Conexion = vgo_Conexion
					vlb_LiberarConexion = False
				End If

				vlo_DalOtmSubcategoriaCategor = New DalOtmSubcategoriaCategor(vlo_Conexion)
				Return vlo_DalOtmSubcategoriaCategor.ObtenerRegistro(String.Format("{0} = {1} AND {2} = {3}", Modelo.OTM_SUBCATEGORIA_CATEGOR.ID_CATEGORIA_MATERIAL, pvn_IdCategoriaMaterial, Modelo.OTM_SUBCATEGORIA_CATEGOR.ID_SUBCATEGORIA_MATERIAL, pvn_IdSubcategoriaMaterial))
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
		''' <param name="pvn_IdCategoriaMaterial">Llave primaria de la tabla otm_categoria_material que se asocia con la secuencia sq_id_categoria_material</param>
		''' <param name="pvn_IdSubcategoriaMaterial">Llave primaria de la tabla otm_subcategoria_material que se asocia con la secuencia sq_id_subcategoria_material</param>
		''' <returns>True: posee datos asociados. False: No posee datos asociados</returns>
		''' <author>Generador de Código basado en objetos Oracle</author>
		''' <creationDate>19/05/2016 05:14:54 p.m.</creationDate>
		''' <changeLog></changeLog>
		Private Function PoseeRegistrosAsociados(pvn_IdCategoriaMaterial As Integer, pvn_IdSubcategoriaMaterial As Integer) As Boolean
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
                'If vlo_DalOtmMaterial.ObtenerRegistro(String.Format("{0} = {1} AND {2} = {3}", Modelo.OTM_MATERIAL.ID_CATEGORIA_MATERIAL, pvn_IdCategoriaMaterial, Modelo.OTM_MATERIAL.ID_SUBCATEGORIA_MATERIAL, pvn_IdSubcategoriaMaterial)).Existe Then
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
