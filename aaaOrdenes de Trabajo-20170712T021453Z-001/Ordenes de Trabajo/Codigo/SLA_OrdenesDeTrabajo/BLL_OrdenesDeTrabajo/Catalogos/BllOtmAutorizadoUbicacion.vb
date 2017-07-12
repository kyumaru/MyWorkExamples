Imports Utilerias.OrdenesDeTrabajo
Imports OrdenesDeTrabajo.EntidadNegocio.Catalogos
Imports OrdenesDeTrabajo.AccesoDatos.Catalogos
Imports Utilerias.BaseDatos.OracleServer
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling

Namespace OrdenesDeTrabajo.LogicaNegocio.Catalogos
	Public Class BllOtmAutorizadoUbicacion
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
		''' Permite agregar un registro en la tabla OTM_AUTORIZADO_UBICACION, no sin antes aplicar la validación de la llave primaria o alterna según corresponda
		''' </summary>
		''' <param name="pvo_Registro">Entidad a procesar</param>
		''' <returns>Mayor a cero: El proceso ha sido exitoso; el valor puede corresponder a la llave autogenerada. Menor a cero: El proceso ha fallado</returns>
		''' <author>Generador de Código basado en objetos Oracle</author>
		''' <creationDate>29/09/2015 09:44:09 a.m.</creationDate>
		''' <changeLog></changeLog>
		Public Function InsertarRegistro(ByVal pvo_Registro As EntOtmAutorizadoUbicacion) As Integer
			Dim vlo_Conexion As ConexionOracle
			Dim vlb_LiberarConexion As Boolean
			Dim vlo_DalOtmAutorizadoUbicacion As DalOtmAutorizadoUbicacion
			Dim vln_Resultado As Integer

			Try
				If vgo_Conexion Is Nothing Then
					vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
					vlb_LiberarConexion = True
				Else
					vlo_Conexion = vgo_Conexion
					vlb_LiberarConexion = False
				End If

				If ObtenerRegistroPorLlavePrimaria(pvo_Registro.IdUbicacionAdministra, pvo_Registro.NumEmpleado).Existe Then
					vln_Resultado = -1
                    Throw New OrdenesDeTrabajoException("Este Funcionario ya se encuentra autorizado para la sede seleccionada.")
				End If

				If ObtenerRegistroPorLlaveAlterna(pvo_Registro.NumEmpleado).Existe Then
					vln_Resultado = -1
                    Throw New OrdenesDeTrabajoException("Este Funcionario ya se encuentra autorizado para la sede seleccionada.")
				End If

				vlo_DalOtmAutorizadoUbicacion = New DalOtmAutorizadoUbicacion(vlo_Conexion)
				vln_Resultado = vlo_DalOtmAutorizadoUbicacion.InsertarRegistro(pvo_Registro)
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
		''' Permite modificar un registro en la tabla OTM_AUTORIZADO_UBICACION, no sin antes aplicar la validación de la llave alterna
		''' </summary>
		''' <param name="pvo_Registro">Entidad a procesar</param>
		''' <returns>Mayor a cero: El proceso ha sido exitoso. Menor a cero: El proceso ha fallado</returns>
		''' <author>Generador de Código basado en objetos Oracle</author>
		''' <creationDate>29/09/2015 09:44:09 a.m.</creationDate>
		''' <changeLog></changeLog>
		Public Function ModificarRegistro(ByVal pvo_Registro As EntOtmAutorizadoUbicacion) As Integer
			Dim vlo_Conexion As ConexionOracle
			Dim vlb_LiberarConexion As Boolean
			Dim vlo_DalOtmAutorizadoUbicacion As DalOtmAutorizadoUbicacion
			Dim vlo_EntOtmAutorizadoUbicacion As EntOtmAutorizadoUbicacion
			Dim vln_Resultado As Integer

			Try
				If vgo_Conexion Is Nothing Then
					vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
					vlb_LiberarConexion = True
				Else
					vlo_Conexion = vgo_Conexion
					vlb_LiberarConexion = False
				End If

				vlo_EntOtmAutorizadoUbicacion = ObtenerRegistroPorLlaveAlterna(pvo_Registro.NumEmpleado)
				If vlo_EntOtmAutorizadoUbicacion.Existe AndAlso vlo_EntOtmAutorizadoUbicacion.IdUbicacionAdministra <> pvo_Registro.IdUbicacionAdministra AndAlso vlo_EntOtmAutorizadoUbicacion.NumEmpleado <> pvo_Registro.NumEmpleado Then
					vln_Resultado = -1
                    Throw New OrdenesDeTrabajoException("Registro ya existente.")
				End If

				vlo_DalOtmAutorizadoUbicacion = New DalOtmAutorizadoUbicacion(vlo_Conexion)
				vln_Resultado = vlo_DalOtmAutorizadoUbicacion.ModificarRegistro(pvo_Registro)
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
		''' <param name="pvn_IdUbicacionAdministra">Id de la ubicación encargada de la administración</param>
		''' <param name="pvn_NumEmpleado">Número de empleado autorizado en la sede</param>
		''' <returns>Entidad que representa la información obtenida al ejecutar la consulta</returns>
		''' <author>Generador de Código basado en objetos Oracle</author>
		''' <creationDate>29/09/2015 09:44:09 a.m.</creationDate>
		''' <changeLog></changeLog>
		Private Function ObtenerRegistroPorLlavePrimaria(pvn_IdUbicacionAdministra As Integer, pvn_NumEmpleado As Double) As EntOtmAutorizadoUbicacion
			Dim vlo_Conexion As ConexionOracle
			Dim vlb_LiberarConexion As Boolean
			Dim vlo_DalOtmAutorizadoUbicacion As DalOtmAutorizadoUbicacion

			Try
				If vgo_Conexion Is Nothing Then
					vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
					vlb_LiberarConexion = True
				Else
					vlo_Conexion = vgo_Conexion
					vlb_LiberarConexion = False
				End If

				vlo_DalOtmAutorizadoUbicacion = New DalOtmAutorizadoUbicacion(vlo_Conexion)
				Return vlo_DalOtmAutorizadoUbicacion.ObtenerRegistro(String.Format("{0} = {1} AND {2} = {3}", Modelo.OTM_AUTORIZADO_UBICACION.ID_UBICACION_ADMINISTRA, pvn_IdUbicacionAdministra, Modelo.OTM_AUTORIZADO_UBICACION.NUM_EMPLEADO, pvn_NumEmpleado))
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
		''' <param name="pvn_NumEmpleado">Número de empleado autorizado en la sede</param>
		''' <returns>Entidad que representa la información obtenida al ejecutar la consulta</returns>
		''' <author>Generador de Código basado en objetos Oracle</author>
		''' <creationDate>29/09/2015 09:44:09 a.m.</creationDate>
		''' <changeLog></changeLog>
		Private Function ObtenerRegistroPorLlaveAlterna(pvn_NumEmpleado As Double) As EntOtmAutorizadoUbicacion
			Dim vlo_Conexion As ConexionOracle
			Dim vlb_LiberarConexion As Boolean
			Dim vlo_DalOtmAutorizadoUbicacion As DalOtmAutorizadoUbicacion

			Try
				If vgo_Conexion Is Nothing Then
					vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
					vlb_LiberarConexion = True
				Else
					vlo_Conexion = vgo_Conexion
					vlb_LiberarConexion = False
				End If

				vlo_DalOtmAutorizadoUbicacion = New DalOtmAutorizadoUbicacion(vlo_Conexion)
				Return vlo_DalOtmAutorizadoUbicacion.ObtenerRegistro(String.Format("{0} = {1}", Modelo.OTM_AUTORIZADO_UBICACION.NUM_EMPLEADO, pvn_NumEmpleado))
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
