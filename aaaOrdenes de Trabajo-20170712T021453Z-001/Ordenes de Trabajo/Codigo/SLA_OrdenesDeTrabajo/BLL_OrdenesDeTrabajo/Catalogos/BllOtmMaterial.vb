Imports Utilerias.OrdenesDeTrabajo
Imports OrdenesDeTrabajo.EntidadNegocio.Catalogos
Imports OrdenesDeTrabajo.AccesoDatos.Catalogos
Imports Utilerias.BaseDatos.OracleServer
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling

Namespace OrdenesDeTrabajo.LogicaNegocio.Catalogos
	Public Class BllOtmMaterial
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
		''' Permite agregar un registro en la tabla OTM_MATERIAL, no sin antes aplicar la validación de la llave primaria o alterna según corresponda
		''' </summary>
		''' <param name="pvo_Registro">Entidad a procesar</param>
		''' <returns>Mayor a cero: El proceso ha sido exitoso; el valor puede corresponder a la llave autogenerada. Menor a cero: El proceso ha fallado</returns>
		''' <author>Generador de Código basado en objetos Oracle</author>
		''' <creationDate>29/05/2016 11:14:50 a.m.</creationDate>
		''' <changeLog></changeLog>
		Public Function InsertarRegistro(ByVal pvo_Registro As EntOtmMaterial) As Integer
			Dim vlo_Conexion As ConexionOracle
			Dim vlb_LiberarConexion As Boolean
			Dim vlo_DalOtmMaterial As DalOtmMaterial
			Dim vln_Resultado As Integer

			Try
				If vgo_Conexion Is Nothing Then
					vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
					vlb_LiberarConexion = True
				Else
					vlo_Conexion = vgo_Conexion
					vlb_LiberarConexion = False
				End If

				If ObtenerRegistroPorLlavePrimaria(pvo_Registro.IdUbicacionAdministra, pvo_Registro.IdMaterial).Existe Then
					vln_Resultado = -1
                    Throw New OrdenesDeTrabajoException("Material ya existente")
				End If

				If ObtenerRegistroPorLlaveAlterna(pvo_Registro.Descripcion, pvo_Registro.IdUbicacionAdministra).Existe Then
                    vln_Resultado = -1
                    Throw New OrdenesDeTrabajoException("La descripción de este material esta repetido(a)")
				End If

				vlo_DalOtmMaterial = New DalOtmMaterial(vlo_Conexion)
				vln_Resultado = vlo_DalOtmMaterial.InsertarRegistro(pvo_Registro)
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
		''' Permite modificar un registro en la tabla OTM_MATERIAL, no sin antes aplicar la validación de la llave alterna
		''' </summary>
		''' <param name="pvo_Registro">Entidad a procesar</param>
		''' <returns>Mayor a cero: El proceso ha sido exitoso. Menor a cero: El proceso ha fallado</returns>
		''' <author>Generador de Código basado en objetos Oracle</author>
		''' <creationDate>29/05/2016 11:14:50 a.m.</creationDate>
		''' <changeLog></changeLog>
		Public Function ModificarRegistro(ByVal pvo_Registro As EntOtmMaterial) As Integer
			Dim vlo_Conexion As ConexionOracle
			Dim vlb_LiberarConexion As Boolean
			Dim vlo_DalOtmMaterial As DalOtmMaterial
			Dim vlo_EntOtmMaterial As EntOtmMaterial
			Dim vln_Resultado As Integer

			Try
				If vgo_Conexion Is Nothing Then
					vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
					vlb_LiberarConexion = True
				Else
					vlo_Conexion = vgo_Conexion
					vlb_LiberarConexion = False
				End If

				vlo_EntOtmMaterial = ObtenerRegistroPorLlaveAlterna(pvo_Registro.Descripcion, pvo_Registro.IdUbicacionAdministra)
				If vlo_EntOtmMaterial.Existe AndAlso vlo_EntOtmMaterial.IdUbicacionAdministra <> pvo_Registro.IdUbicacionAdministra AndAlso vlo_EntOtmMaterial.IdMaterial <> pvo_Registro.IdMaterial Then
					vln_Resultado = -1
                    Throw New OrdenesDeTrabajoException("Llave alterna repetida")
                End If

                If pvo_Registro.Estado = Estado.INACTIVO Then

                    Dim vlc_Datos As String = ValidaInventariosAsociados(pvo_Registro.IdMaterial)

                    If vlc_Datos <> String.Empty Then
                        vln_Resultado = -1
                        Throw New OrdenesDeTrabajoException(String.Format("El material no puede ser inactivado, posee inventarios asociados que no están en 0 {0}.", vlc_Datos))
                    End If

                End If

                vlo_DalOtmMaterial = New DalOtmMaterial(vlo_Conexion)
                vln_Resultado = vlo_DalOtmMaterial.ModificarRegistro(pvo_Registro)
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
        ''' 
        ''' </summary>
        ''' <param name="pvn_IdMaterial"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <author>Carlos Gómez Ondoy</author>
        ''' <creationDate>06/0/2017</creationDate>
        ''' <changeLog></changeLog>
        Private Function ValidaInventariosAsociados(pvn_IdMaterial As Integer) As String
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOtfInventario As DalOtfInventario
            Dim vlo_DsDatos As Data.DataSet

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                vlo_DalOtfInventario = New DalOtfInventario(vlo_Conexion)

                vlo_DsDatos = vlo_DalOtfInventario.ListarRegistrosLista(
                    String.Format("{0} = {1} AND {2} > 0",
                                  Modelo.V_OTF_INVENTARIOLST.ID_MATERIAL, pvn_IdMaterial,
                                  Modelo.V_OTF_INVENTARIOLST.CANTIDAD_DISPONIBLE),
                    String.Empty, False, 0, 0)

                If vlo_DsDatos.Tables.Count > 0 AndAlso vlo_DsDatos.Tables(0).Rows.Count > 0 Then
                    Dim vlc_Inventarios As String = String.Empty
                    For Each vlo_Row In vlo_DsDatos.Tables(0).Rows
                        vlc_Inventarios = String.Format("{0}, {1}", vlc_Inventarios, vlo_Row(Modelo.V_OTF_INVENTARIOLST.DESCRIPCION_ALMACEN_BODEGA))
                    Next

                    Return vlc_Inventarios
                Else
                    Return String.Empty
                End If

            Catch vlo_Excepcion As Exception
                Throw
            Finally
                If vlb_LiberarConexion Then
                    vlo_Conexion.Dispose()
                End If
            End Try

        End Function

		''' <summary>
		''' Permite borrar un registro en la tabla OTM_MATERIAL, no sin antes aplicar la validación de dependencia con tablas relacionadas
		''' </summary>
		''' <param name="pvo_Registro">Entidad a procesar</param>
		''' <returns>Mayor a cero: El proceso ha sido exitoso. Menor a cero: El proceso ha fallado</returns>
		''' <author>Generador de Código basado en objetos Oracle</author>
		''' <creationDate>29/05/2016 11:14:50 a.m.</creationDate>
		''' <changeLog></changeLog>
		Public Function BorrarRegistro(ByVal pvo_Registro As EntOtmMaterial) As Integer
			Dim vlo_Conexion As ConexionOracle
			Dim vlb_LiberarConexion As Boolean
			Dim vlo_DalOtmMaterial As DalOtmMaterial
			Dim vln_Resultado As Integer

			Try
				If vgo_Conexion Is Nothing Then
					vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
					vlb_LiberarConexion = True
				Else
					vlo_Conexion = vgo_Conexion
					vlb_LiberarConexion = False
				End If

				If PoseeRegistrosAsociados(pvo_Registro.IdUbicacionAdministra, pvo_Registro.IdMaterial) Then
					vln_Resultado = -1
					Throw New OrdenesDeTrabajoException("[TODO] Hay registro asociados")
				End If

				vlo_DalOtmMaterial = New DalOtmMaterial(vlo_Conexion)
				vln_Resultado = vlo_DalOtmMaterial.BorrarRegistro(pvo_Registro)
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
		''' <param name="pvn_IdUbicacionAdministra">Id de la ubicación que administra los datos del catálogo</param>
		''' <param name="pvn_IdMaterial">Llave primaria de la tabla otm_material. consecutivo de 1 a n para cada ubicación</param>
		''' <returns>Entidad que representa la información obtenida al ejecutar la consulta</returns>
		''' <author>Generador de Código basado en objetos Oracle</author>
		''' <creationDate>29/05/2016 11:14:50 a.m.</creationDate>
		''' <changeLog></changeLog>
		Private Function ObtenerRegistroPorLlavePrimaria(pvn_IdUbicacionAdministra As Integer, pvn_IdMaterial As Integer) As EntOtmMaterial
			Dim vlo_Conexion As ConexionOracle
			Dim vlb_LiberarConexion As Boolean
			Dim vlo_DalOtmMaterial As DalOtmMaterial

			Try
				If vgo_Conexion Is Nothing Then
					vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
					vlb_LiberarConexion = True
				Else
					vlo_Conexion = vgo_Conexion
					vlb_LiberarConexion = False
				End If

				vlo_DalOtmMaterial = New DalOtmMaterial(vlo_Conexion)
				Return vlo_DalOtmMaterial.ObtenerRegistro(String.Format("{0} = {1} AND {2} = {3}", Modelo.OTM_MATERIAL.ID_UBICACION_ADMINISTRA, pvn_IdUbicacionAdministra, Modelo.OTM_MATERIAL.ID_MATERIAL, pvn_IdMaterial))
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
		''' <param name="pvc_Descripcion">Descripción del material</param>
		''' <param name="pvn_IdUbicacionAdministra">Id de la ubicación que administra los datos del catálogo</param>
		''' <returns>Entidad que representa la información obtenida al ejecutar la consulta</returns>
		''' <author>Generador de Código basado en objetos Oracle</author>
		''' <creationDate>29/05/2016 11:14:50 a.m.</creationDate>
		''' <changeLog></changeLog>
		Private Function ObtenerRegistroPorLlaveAlterna(pvc_Descripcion As String, pvn_IdUbicacionAdministra As Integer) As EntOtmMaterial
			Dim vlo_Conexion As ConexionOracle
			Dim vlb_LiberarConexion As Boolean
			Dim vlo_DalOtmMaterial As DalOtmMaterial

			Try
				If vgo_Conexion Is Nothing Then
					vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
					vlb_LiberarConexion = True
				Else
					vlo_Conexion = vgo_Conexion
					vlb_LiberarConexion = False
				End If

				vlo_DalOtmMaterial = New DalOtmMaterial(vlo_Conexion)
				Return vlo_DalOtmMaterial.ObtenerRegistro(String.Format("UPPER({0}) = '{1}' AND {2} = {3}", Modelo.OTM_MATERIAL.DESCRIPCION, pvc_Descripcion.ToUpper(), Modelo.OTM_MATERIAL.ID_UBICACION_ADMINISTRA, pvn_IdUbicacionAdministra))
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
		''' <param name="pvn_IdUbicacionAdministra">Id de la ubicación que administra los datos del catálogo</param>
		''' <param name="pvn_IdMaterial">Llave primaria de la tabla otm_material. consecutivo de 1 a n para cada ubicación</param>
		''' <returns>True: posee datos asociados. False: No posee datos asociados</returns>
		''' <author>Generador de Código basado en objetos Oracle</author>
		''' <creationDate>29/05/2016 11:14:50 a.m.</creationDate>
		''' <changeLog></changeLog>
		Private Function PoseeRegistrosAsociados(pvn_IdUbicacionAdministra As Integer, pvn_IdMaterial As Integer) As Boolean
			Dim vlo_Conexion As ConexionOracle
			Dim vlb_LiberarConexion As Boolean
			Dim vlo_PoseeRegistrosAsociados As Boolean
            'Dim vlo_DalOttDetalleMaterial As DalOttDetalleMaterial
            'Dim vlo_DalOtlDetalleMaterial As DalOtlDetalleMaterial
            'Dim vlo_DalOtfInventario As DalOtfInventario

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

                ''Determinar la existencia de registros asociados en la tabla OTT_DETALLE_MATERIAL
                'vlo_DalOttDetalleMaterial = New DalOttDetalleMaterial(vlo_Conexion)
                'If vlo_DalOttDetalleMaterial.ObtenerRegistro(String.Format("{0} = {1} AND {2} = {3}", Modelo.OTT_DETALLE_MATERIAL.ID_UBICACION_ADMINISTRA, pvn_IdUbicacionAdministra, Modelo.OTT_DETALLE_MATERIAL.ID_MATERIAL, pvn_IdMaterial)).Existe Then
                '	Return True
                'End If

                ''Determinar la existencia de registros asociados en la tabla OTL_DETALLE_MATERIAL
                'vlo_DalOtlDetalleMaterial = New DalOtlDetalleMaterial(vlo_Conexion)
                'If vlo_DalOtlDetalleMaterial.ObtenerRegistro(String.Format("{0} = {1} AND {2} = {3}", Modelo.OTL_DETALLE_MATERIAL.ID_UBICACION_ADMINISTRA, pvn_IdUbicacionAdministra, Modelo.OTL_DETALLE_MATERIAL.ID_MATERIAL, pvn_IdMaterial)).Existe Then
                '	Return True
                'End If

                ''Determinar la existencia de registros asociados en la tabla OTF_INVENTARIO
                'vlo_DalOtfInventario = New DalOtfInventario(vlo_Conexion)
                'If vlo_DalOtfInventario.ObtenerRegistro(String.Format("{0} = {1} AND {2} = {3}", Modelo.OTF_INVENTARIO.ID_UBICACION_ADMINISTRA, pvn_IdUbicacionAdministra, Modelo.OTF_INVENTARIO.ID_MATERIAL, pvn_IdMaterial)).Existe Then
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
