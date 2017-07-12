Imports Utilerias.OrdenesDeTrabajo
Imports OrdenesDeTrabajo.EntidadNegocio.Catalogos
Imports OrdenesDeTrabajo.AccesoDatos.Catalogos
Imports Utilerias.BaseDatos.OracleServer
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling

Namespace OrdenesDeTrabajo.LogicaNegocio.Catalogos
	Public Class BllOtmCategoriaMaterial
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
		''' Permite agregar un registro en la tabla OTM_CATEGORIA_MATERIAL, no sin antes aplicar la validación de la llave primaria o alterna según corresponda
		''' </summary>
		''' <param name="pvo_Registro">Entidad a procesar</param>
		''' <returns>Mayor a cero: El proceso ha sido exitoso; el valor puede corresponder a la llave autogenerada. Menor a cero: El proceso ha fallado</returns>
		''' <author>Generador de Código basado en objetos Oracle</author>
		''' <creationDate>19/05/2016 05:14:54 p.m.</creationDate>
		''' <changeLog></changeLog>
		Public Function InsertarRegistro(ByVal pvo_Registro As EntOtmCategoriaMaterial) As Integer
			Dim vlo_Conexion As ConexionOracle
			Dim vlb_LiberarConexion As Boolean
			Dim vlo_DalOtmCategoriaMaterial As DalOtmCategoriaMaterial
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
                    Throw New OrdenesDeTrabajoException("Nombre ya existente para su ubicación.")
				End If

				vlo_DalOtmCategoriaMaterial = New DalOtmCategoriaMaterial(vlo_Conexion)
				vln_Resultado = vlo_DalOtmCategoriaMaterial.InsertarRegistro(pvo_Registro)
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
        ''' Permite agregar un registro en la tabla OTM_CATEGORIA_MATERIAL y en N resgitros en OTM_SUBCATEGORIA_CATEGOR
        ''' </summary>
        ''' <param name="pvo_Registro">Entidad a procesar</param>
        ''' <returns>Mayor a cero: El proceso ha sido exitoso; el valor puede corresponder a la llave autogenerada. Menor a cero: El proceso ha fallado</returns>
        ''' <author>Carlos Gómez Ondoy</author>
        ''' <creationDate>20/05/2016</creationDate>
        ''' <changeLog></changeLog>
        Public Function InsertarCategoriaAsociaciones(ByVal pvo_Registro As EntOtmCategoriaMaterial, pvo_DsAsociaciones As Data.DataSet) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOtmCategoriaMaterial As DalOtmCategoriaMaterial
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

                If ObtenerRegistroPorLlaveAlterna(pvo_Registro.IdUbicacionAdministra, pvo_Registro.Descripcion).Existe Then
                    vln_Resultado = -1
                    Throw New OrdenesDeTrabajoException("Nombre ya existente para su ubicación.")
                End If

                vlo_Conexion.TransaccionBegin()

                vlo_DalOtmCategoriaMaterial = New DalOtmCategoriaMaterial(vlo_Conexion)
                vln_Resultado = vlo_DalOtmCategoriaMaterial.InsertarRegistro(pvo_Registro)

                For Each vlo_Fila In pvo_DsAsociaciones.Tables(0).Rows
                    vlo_Fila(Modelo.V_OTM_SUBCATEGORIA_CATEGORLST.ID_CATEGORIA_MATERIAL) = vln_Resultado
                Next

                vlo_DalOtmSubcategoriaCategor = New DalOtmSubcategoriaCategor(vlo_Conexion)
                vlo_DalOtmSubcategoriaCategor.AdapterOtmSubCategoriaCategoria(pvo_DsAsociaciones)

                vlo_Conexion.TransaccionCommit()

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
		''' Permite modificar un registro en la tabla OTM_CATEGORIA_MATERIAL, no sin antes aplicar la validación de la llave alterna
		''' </summary>
		''' <param name="pvo_Registro">Entidad a procesar</param>
		''' <returns>Mayor a cero: El proceso ha sido exitoso. Menor a cero: El proceso ha fallado</returns>
		''' <author>Generador de Código basado en objetos Oracle</author>
		''' <creationDate>19/05/2016 05:14:54 p.m.</creationDate>
		''' <changeLog></changeLog>
		Public Function ModificarRegistro(ByVal pvo_Registro As EntOtmCategoriaMaterial) As Integer
			Dim vlo_Conexion As ConexionOracle
			Dim vlb_LiberarConexion As Boolean
			Dim vlo_DalOtmCategoriaMaterial As DalOtmCategoriaMaterial
			Dim vlo_EntOtmCategoriaMaterial As EntOtmCategoriaMaterial
			Dim vln_Resultado As Integer

			Try
				If vgo_Conexion Is Nothing Then
					vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
					vlb_LiberarConexion = True
				Else
					vlo_Conexion = vgo_Conexion
					vlb_LiberarConexion = False
				End If

				vlo_EntOtmCategoriaMaterial = ObtenerRegistroPorLlaveAlterna(pvo_Registro.IdUbicacionAdministra, pvo_Registro.Descripcion)
				If vlo_EntOtmCategoriaMaterial.Existe AndAlso vlo_EntOtmCategoriaMaterial.IdCategoriaMaterial <> pvo_Registro.IdCategoriaMaterial Then
					vln_Resultado = -1
                    Throw New OrdenesDeTrabajoException("Nombre ya existente para su ubicación.")
				End If

				vlo_DalOtmCategoriaMaterial = New DalOtmCategoriaMaterial(vlo_Conexion)
				vln_Resultado = vlo_DalOtmCategoriaMaterial.ModificarRegistro(pvo_Registro)
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
        ''' Permite modificar un registro en la tabla OTM_CATEGORIA_MATERIAL, no sin antes aplicar la validación de la llave alterna
        ''' </summary>
        ''' <param name="pvo_Registro">Entidad a procesar</param>
        ''' <returns>Mayor a cero: El proceso ha sido exitoso. Menor a cero: El proceso ha fallado</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>19/05/2016 05:14:54 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Function ModificarCategoriaAsociaciones(ByVal pvo_Registro As EntOtmCategoriaMaterial, pvo_DsAsociaciones As Data.DataSet) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOtmCategoriaMaterial As DalOtmCategoriaMaterial
            Dim vlo_EntOtmCategoriaMaterial As EntOtmCategoriaMaterial
            Dim vln_Resultado As Integer
            Dim vlo_DsExistentes As Data.DataSet
            Dim vlo_DalOtmSubcategoriaCategor As DalOtmSubcategoriaCategor
            Dim vlo_DrNuevaFila As Data.DataRow

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                vlo_EntOtmCategoriaMaterial = ObtenerRegistroPorLlaveAlterna(pvo_Registro.IdUbicacionAdministra, pvo_Registro.Descripcion)
                If vlo_EntOtmCategoriaMaterial.Existe AndAlso vlo_EntOtmCategoriaMaterial.IdCategoriaMaterial <> pvo_Registro.IdCategoriaMaterial Then
                    vln_Resultado = -1
                    Throw New OrdenesDeTrabajoException("Nombre ya existente para su ubicación.")
                End If

                vlo_Conexion.TransaccionBegin()

                vlo_DalOtmCategoriaMaterial = New DalOtmCategoriaMaterial(vlo_Conexion)
                vln_Resultado = vlo_DalOtmCategoriaMaterial.ModificarRegistro(pvo_Registro)

                vlo_DalOtmSubcategoriaCategor = New DalOtmSubcategoriaCategor(vlo_Conexion)
                vlo_DsExistentes = vlo_DalOtmSubcategoriaCategor.ListarRegistrosLista(String.Format("{0} = {1} AND {2} = 0", Modelo.V_OTM_SUBCATEGORIA_CATEGORLST.ID_CATEGORIA_MATERIAL, pvo_Registro.IdCategoriaMaterial, Modelo.V_OTM_SUBCATEGORIA_CATEGORLST.POSEE_REGISTROS_ASOCIADOS), String.Empty, False, 0, 0)

                For Each vlo_FilaEliminada In vlo_DsExistentes.Tables(0).Rows
                    vlo_FilaEliminada.Delete()
                Next

                vlo_DalOtmSubcategoriaCategor.AdapterOtmSubCategoriaCategoria(vlo_DsExistentes)
             
                vlo_DsExistentes = vlo_DalOtmSubcategoriaCategor.ListarRegistrosLista(String.Format("1 = 0"), String.Empty, False, 0, 0)

                For Each vlo_Fila In pvo_DsAsociaciones.Tables(0).Rows

                    If vlo_Fila(Modelo.V_OTM_SUBCATEGORIA_CATEGORLST.POSEE_REGISTROS_ASOCIADOS).ToString = "0" Then

                        vlo_DrNuevaFila = vlo_DsExistentes.Tables(0).NewRow
                        vlo_DrNuevaFila.Item(vlo_DsExistentes.Tables(0).Columns(Modelo.OTM_SUBCATEGORIA_CATEGOR.ID_CATEGORIA_MATERIAL)) = pvo_Registro.IdCategoriaMaterial
                        vlo_DrNuevaFila.Item(vlo_DsExistentes.Tables(0).Columns(Modelo.OTM_SUBCATEGORIA_CATEGOR.ID_SUBCATEGORIA_MATERIAL)) = vlo_Fila(Modelo.OTM_SUBCATEGORIA_CATEGOR.ID_SUBCATEGORIA_MATERIAL)
                        vlo_DrNuevaFila.Item(vlo_DsExistentes.Tables(0).Columns(Modelo.OTM_SUBCATEGORIA_CATEGOR.USUARIO)) = vlo_Fila(Modelo.OTM_SUBCATEGORIA_CATEGOR.USUARIO)

                        vlo_DsExistentes.Tables(0).Rows.Add(vlo_DrNuevaFila)
                    End If
                Next

                vlo_DalOtmSubcategoriaCategor.AdapterOtmSubCategoriaCategoria(vlo_DsExistentes)

                vlo_Conexion.TransaccionCommit()
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
		''' Permite borrar un registro en la tabla OTM_CATEGORIA_MATERIAL, no sin antes aplicar la validación de dependencia con tablas relacionadas
		''' </summary>
		''' <param name="pvo_Registro">Entidad a procesar</param>
		''' <returns>Mayor a cero: El proceso ha sido exitoso. Menor a cero: El proceso ha fallado</returns>
		''' <author>Generador de Código basado en objetos Oracle</author>
		''' <creationDate>19/05/2016 05:14:54 p.m.</creationDate>
		''' <changeLog></changeLog>
		Public Function BorrarRegistro(ByVal pvo_Registro As EntOtmCategoriaMaterial) As Integer
			Dim vlo_Conexion As ConexionOracle
			Dim vlb_LiberarConexion As Boolean
			Dim vlo_DalOtmCategoriaMaterial As DalOtmCategoriaMaterial
			Dim vln_Resultado As Integer

			Try
				If vgo_Conexion Is Nothing Then
					vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
					vlb_LiberarConexion = True
				Else
					vlo_Conexion = vgo_Conexion
					vlb_LiberarConexion = False
				End If

				If PoseeRegistrosAsociados(pvo_Registro.IdCategoriaMaterial) Then
					vln_Resultado = -1
                    Throw New OrdenesDeTrabajoException("Hay registro asociados")
				End If

				vlo_DalOtmCategoriaMaterial = New DalOtmCategoriaMaterial(vlo_Conexion)
				vln_Resultado = vlo_DalOtmCategoriaMaterial.BorrarRegistro(pvo_Registro)
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
        ''' Permite borrar un registro en la tabla OTM_CATEGORIA_MATERIAL, y N registros de la tabla OTM_SUBCATEGORIA_CATEGOR
        ''' </summary>
        ''' <param name="pvo_Registro">Entidad a procesar</param>
        ''' <returns>Mayor a cero: El proceso ha sido exitoso. Menor a cero: El proceso ha fallado</returns>
        ''' <author>Carlos Gómez Ondoy</author>
        ''' <creationDate>23/05/2016</creationDate>
        ''' <changeLog></changeLog>
        Public Function BorrarRegistroConAsociados(ByVal pvo_Registro As EntOtmCategoriaMaterial) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOtmCategoriaMaterial As DalOtmCategoriaMaterial
            Dim vlo_DsExistentes As Data.DataSet
            Dim vln_Resultado As Integer
            Dim vlo_DalOtmSubcategoriaCategor As DalOtmSubcategoriaCategor

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                vlo_Conexion.TransaccionBegin()

                vlo_DalOtmSubcategoriaCategor = New DalOtmSubcategoriaCategor(vlo_Conexion)
                vlo_DsExistentes = vlo_DalOtmSubcategoriaCategor.ListarRegistros(String.Format("{0} = {1}", Modelo.OTM_SUBCATEGORIA_CATEGOR.ID_CATEGORIA_MATERIAL, pvo_Registro.IdCategoriaMaterial), String.Empty, False, 0, 0)

                For Each vlo_FilaEliminada In vlo_DsExistentes.Tables(0).Rows
                    vlo_FilaEliminada.Delete()
                Next

                vlo_DalOtmSubcategoriaCategor.AdapterOtmSubCategoriaCategoria(vlo_DsExistentes)

                vlo_DalOtmCategoriaMaterial = New DalOtmCategoriaMaterial(vlo_Conexion)
                vln_Resultado = vlo_DalOtmCategoriaMaterial.BorrarRegistro(pvo_Registro)

                vlo_Conexion.TransaccionCommit()

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
		''' <param name="pvc_Descripcion">Descripción de la familia de materiales</param>
		''' <returns>Entidad que representa la información obtenida al ejecutar la consulta</returns>
		''' <author>Generador de Código basado en objetos Oracle</author>
		''' <creationDate>19/05/2016 05:14:54 p.m.</creationDate>
		''' <changeLog></changeLog>
		Private Function ObtenerRegistroPorLlaveAlterna(pvn_IdUbicacionAdministra As Integer, pvc_Descripcion As String) As EntOtmCategoriaMaterial
			Dim vlo_Conexion As ConexionOracle
			Dim vlb_LiberarConexion As Boolean
			Dim vlo_DalOtmCategoriaMaterial As DalOtmCategoriaMaterial

			Try
				If vgo_Conexion Is Nothing Then
					vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
					vlb_LiberarConexion = True
				Else
					vlo_Conexion = vgo_Conexion
					vlb_LiberarConexion = False
				End If

				vlo_DalOtmCategoriaMaterial = New DalOtmCategoriaMaterial(vlo_Conexion)
				Return vlo_DalOtmCategoriaMaterial.ObtenerRegistro(String.Format("{0} = {1} AND UPPER({2}) = '{3}'", Modelo.OTM_CATEGORIA_MATERIAL.ID_UBICACION_ADMINISTRA, pvn_IdUbicacionAdministra, Modelo.OTM_CATEGORIA_MATERIAL.DESCRIPCION, pvc_Descripcion.ToUpper()))
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
		''' <returns>True: posee datos asociados. False: No posee datos asociados</returns>
		''' <author>Generador de Código basado en objetos Oracle</author>
		''' <creationDate>19/05/2016 05:14:54 p.m.</creationDate>
		''' <changeLog></changeLog>
		Private Function PoseeRegistrosAsociados(pvn_IdCategoriaMaterial As Integer) As Boolean
			Dim vlo_Conexion As ConexionOracle
			Dim vlb_LiberarConexion As Boolean
			Dim vlo_PoseeRegistrosAsociados As Boolean
			Dim vlo_DalOtmSubcategoriaCategor As DalOtmSubcategoriaCategor

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
				vlo_DalOtmSubcategoriaCategor = New DalOtmSubcategoriaCategor(vlo_Conexion)
				If vlo_DalOtmSubcategoriaCategor.ObtenerRegistro(String.Format("{0} = {1}", Modelo.OTM_SUBCATEGORIA_CATEGOR.ID_CATEGORIA_MATERIAL, pvn_IdCategoriaMaterial)).Existe Then
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
