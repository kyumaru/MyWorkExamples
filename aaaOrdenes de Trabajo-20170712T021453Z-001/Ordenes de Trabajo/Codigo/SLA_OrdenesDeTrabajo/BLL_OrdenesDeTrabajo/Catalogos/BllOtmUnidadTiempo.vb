Imports Utilerias.OrdenesDeTrabajo
Imports OrdenesDeTrabajo.EntidadNegocio.Catalogos
Imports OrdenesDeTrabajo.AccesoDatos.Catalogos
Imports OrdenesDeTrabajo.EntidadNegocio.OrdenesDeTrabajo
Imports OrdenesDeTrabajo.AccesoDatos.OrdenesDeTrabajo
Imports Utilerias.BaseDatos.OracleServer
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling

Namespace OrdenesDeTrabajo.LogicaNegocio.Catalogos
	Public Class BllOtmUnidadTiempo
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
		''' Permite agregar un registro en la tabla OTM_UNIDAD_TIEMPO, no sin antes aplicar la validación de la llave primaria o alterna según corresponda
		''' </summary>
		''' <param name="pvo_Registro">Entidad a procesar</param>
		''' <returns>Mayor a cero: El proceso ha sido exitoso; el valor puede corresponder a la llave autogenerada. Menor a cero: El proceso ha fallado</returns>
		''' <author>Generador de Código basado en objetos Oracle</author>
		''' <creationDate>15/01/2016 02:29:16 p.m.</creationDate>
		''' <changeLog></changeLog>
		Public Function InsertarRegistro(ByVal pvo_Registro As EntOtmUnidadTiempo) As Integer
			Dim vlo_Conexion As ConexionOracle
			Dim vlb_LiberarConexion As Boolean
			Dim vlo_DalOtmUnidadTiempo As DalOtmUnidadTiempo
			Dim vln_Resultado As Integer

			Try
				If vgo_Conexion Is Nothing Then
					vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
					vlb_LiberarConexion = True
				Else
					vlo_Conexion = vgo_Conexion
					vlb_LiberarConexion = False
				End If

				If ObtenerRegistroPorLlavePrimaria(pvo_Registro.IdUnidadTiempo).Existe Then
					vln_Resultado = -1
                    Throw New OrdenesDeTrabajoException("Registro ya existente.")
				End If

				If ObtenerRegistroPorLlaveAlterna(pvo_Registro.Descripcion).Existe Then
					vln_Resultado = -1
                    Throw New OrdenesDeTrabajoException("Registro ya existente.")
				End If

				vlo_DalOtmUnidadTiempo = New DalOtmUnidadTiempo(vlo_Conexion)
				vln_Resultado = vlo_DalOtmUnidadTiempo.InsertarRegistro(pvo_Registro)
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
		''' Permite modificar un registro en la tabla OTM_UNIDAD_TIEMPO, no sin antes aplicar la validación de la llave alterna
		''' </summary>
		''' <param name="pvo_Registro">Entidad a procesar</param>
		''' <returns>Mayor a cero: El proceso ha sido exitoso. Menor a cero: El proceso ha fallado</returns>
		''' <author>Generador de Código basado en objetos Oracle</author>
		''' <creationDate>15/01/2016 02:29:16 p.m.</creationDate>
		''' <changeLog></changeLog>
		Public Function ModificarRegistro(ByVal pvo_Registro As EntOtmUnidadTiempo) As Integer
			Dim vlo_Conexion As ConexionOracle
			Dim vlb_LiberarConexion As Boolean
			Dim vlo_DalOtmUnidadTiempo As DalOtmUnidadTiempo
			Dim vlo_EntOtmUnidadTiempo As EntOtmUnidadTiempo
			Dim vln_Resultado As Integer

			Try
				If vgo_Conexion Is Nothing Then
					vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
					vlb_LiberarConexion = True
				Else
					vlo_Conexion = vgo_Conexion
					vlb_LiberarConexion = False
				End If

				vlo_EntOtmUnidadTiempo = ObtenerRegistroPorLlaveAlterna(pvo_Registro.Descripcion)
				If vlo_EntOtmUnidadTiempo.Existe AndAlso vlo_EntOtmUnidadTiempo.IdUnidadTiempo <> pvo_Registro.IdUnidadTiempo Then
					vln_Resultado = -1
                    Throw New OrdenesDeTrabajoException("La descripción de la unidad de tiempo está repetida")
				End If

				vlo_DalOtmUnidadTiempo = New DalOtmUnidadTiempo(vlo_Conexion)
				vln_Resultado = vlo_DalOtmUnidadTiempo.ModificarRegistro(pvo_Registro)
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
		''' Permite borrar un registro en la tabla OTM_UNIDAD_TIEMPO, no sin antes aplicar la validación de dependencia con tablas relacionadas
		''' </summary>
		''' <param name="pvo_Registro">Entidad a procesar</param>
		''' <returns>Mayor a cero: El proceso ha sido exitoso. Menor a cero: El proceso ha fallado</returns>
		''' <author>Generador de Código basado en objetos Oracle</author>
		''' <creationDate>15/01/2016 02:29:16 p.m.</creationDate>
		''' <changeLog></changeLog>
		Public Function BorrarRegistro(ByVal pvo_Registro As EntOtmUnidadTiempo) As Integer
			Dim vlo_Conexion As ConexionOracle
			Dim vlb_LiberarConexion As Boolean
			Dim vlo_DalOtmUnidadTiempo As DalOtmUnidadTiempo
			Dim vln_Resultado As Integer

			Try
				If vgo_Conexion Is Nothing Then
					vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
					vlb_LiberarConexion = True
				Else
					vlo_Conexion = vgo_Conexion
					vlb_LiberarConexion = False
				End If

				If PoseeRegistrosAsociados(pvo_Registro.IdUnidadTiempo) Then
					vln_Resultado = -1
                    Throw New OrdenesDeTrabajoException("Hay registro asociados, no puede ser borrada.")
				End If

				vlo_DalOtmUnidadTiempo = New DalOtmUnidadTiempo(vlo_Conexion)
				vln_Resultado = vlo_DalOtmUnidadTiempo.BorrarRegistro(pvo_Registro)
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
		''' <param name="pvn_IdUnidadTiempo">Llave primaria de la tabla otm_unidad_tiempo que se asocia con la secuencia sq_id_unidad_tiempo</param>
		''' <returns>Entidad que representa la información obtenida al ejecutar la consulta</returns>
		''' <author>Generador de Código basado en objetos Oracle</author>
		''' <creationDate>15/01/2016 02:29:16 p.m.</creationDate>
		''' <changeLog></changeLog>
		Private Function ObtenerRegistroPorLlavePrimaria(pvn_IdUnidadTiempo As Integer) As EntOtmUnidadTiempo
			Dim vlo_Conexion As ConexionOracle
			Dim vlb_LiberarConexion As Boolean
			Dim vlo_DalOtmUnidadTiempo As DalOtmUnidadTiempo

			Try
				If vgo_Conexion Is Nothing Then
					vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
					vlb_LiberarConexion = True
				Else
					vlo_Conexion = vgo_Conexion
					vlb_LiberarConexion = False
				End If

				vlo_DalOtmUnidadTiempo = New DalOtmUnidadTiempo(vlo_Conexion)
				Return vlo_DalOtmUnidadTiempo.ObtenerRegistro(String.Format("{0} = {1}", Modelo.OTM_UNIDAD_TIEMPO.ID_UNIDAD_TIEMPO, pvn_IdUnidadTiempo))
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
		''' <param name="pvc_Descripcion">Descripción de la unidad de tiempo, debe ser unica, por ejemplo; semana, quincena, mes </param>
		''' <returns>Entidad que representa la información obtenida al ejecutar la consulta</returns>
		''' <author>Generador de Código basado en objetos Oracle</author>
		''' <creationDate>15/01/2016 02:29:16 p.m.</creationDate>
		''' <changeLog></changeLog>
		Private Function ObtenerRegistroPorLlaveAlterna(pvc_Descripcion As String) As EntOtmUnidadTiempo
			Dim vlo_Conexion As ConexionOracle
			Dim vlb_LiberarConexion As Boolean
			Dim vlo_DalOtmUnidadTiempo As DalOtmUnidadTiempo

			Try
				If vgo_Conexion Is Nothing Then
					vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
					vlb_LiberarConexion = True
				Else
					vlo_Conexion = vgo_Conexion
					vlb_LiberarConexion = False
				End If

				vlo_DalOtmUnidadTiempo = New DalOtmUnidadTiempo(vlo_Conexion)
				Return vlo_DalOtmUnidadTiempo.ObtenerRegistro(String.Format("UPPER({0}) = '{1}'", Modelo.OTM_UNIDAD_TIEMPO.DESCRIPCION, pvc_Descripcion.ToUpper()))
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
		''' <param name="pvn_IdUnidadTiempo">Llave primaria de la tabla otm_unidad_tiempo que se asocia con la secuencia sq_id_unidad_tiempo</param>
		''' <returns>True: posee datos asociados. False: No posee datos asociados</returns>
		''' <author>Generador de Código basado en objetos Oracle</author>
		''' <creationDate>15/01/2016 02:29:16 p.m.</creationDate>
		''' <changeLog></changeLog>
		Private Function PoseeRegistrosAsociados(pvn_IdUnidadTiempo As Integer) As Boolean
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_PoseeRegistrosAsociados As Boolean
            Dim vlo_DalOtfExcepcionPeriodo As DalOtfExcepcionPeriodo
            'Dim vlo_DalOthTiempoOperario As DalOthTiempoOperario
            Dim vlo_DalOttTiempoOperario As DalOttTiempoOperario
            Dim vlo_DalOttViabilidadTecnica As DalOttViabilidadTecnica
            'Dim vlo_DalOthViabilidadTecnica As DalOthViabilidadTecnica

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

                'Determinar la existencia de registros asociados en la tabla OTF_EXCEPCION_PERIODO
                vlo_DalOtfExcepcionPeriodo = New DalOtfExcepcionPeriodo(vlo_Conexion)
                If vlo_DalOtfExcepcionPeriodo.ObtenerRegistro(String.Format("{0} = {1}", Modelo.OTF_EXCEPCION_PERIODO.ID_UNIDAD_TIEMPO, pvn_IdUnidadTiempo)).Existe Then
                    Return True
                End If

                'Determinar la existencia de registros asociados en la tabla OTH_TIEMPO_OPERARIO
                'vlo_DalOthTiempoOperario = New DalOthTiempoOperario(vlo_Conexion)
                'If vlo_DalOthTiempoOperario.ObtenerRegistro(String.Format("{0} = {1}", Modelo.OTH_TIEMPO_OPERARIO.ID_UNIDAD_TIEMPO, pvn_IdUnidadTiempo)).Existe Then
                '    Return True
                'End If

                'Determinar la existencia de registros asociados en la tabla OTT_TIEMPO_OPERARIO
                vlo_DalOttTiempoOperario = New DalOttTiempoOperario(vlo_Conexion)
                If vlo_DalOttTiempoOperario.ObtenerRegistro(String.Format("{0} = {1}", Modelo.OTT_TIEMPO_OPERARIO.ID_UNIDAD_TIEMPO, pvn_IdUnidadTiempo)).Existe Then
                    Return True
                End If

                'Determinar la existencia de registros asociados en la tabla OTT_VIABILIDAD_TECNICA
                vlo_DalOttViabilidadTecnica = New DalOttViabilidadTecnica(vlo_Conexion)
                If vlo_DalOttViabilidadTecnica.ObtenerRegistro(String.Format("{0} = {1}", Modelo.OTT_VIABILIDAD_TECNICA.ID_UNIDAD_TIEMPO, pvn_IdUnidadTiempo)).Existe Then
                    Return True
                End If

                'Determinar la existencia de registros asociados en la tabla OTH_VIABILIDAD_TECNICA
                'vlo_DalOthViabilidadTecnica = New DalOthViabilidadTecnica(vlo_Conexion)
                'If vlo_DalOthViabilidadTecnica.ObtenerRegistro(String.Format("{0} = {1}", Modelo.OTH_VIABILIDAD_TECNICA.ID_UNIDAD_TIEMPO, pvn_IdUnidadTiempo)).Existe Then
                '    Return True
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
