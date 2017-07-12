Imports Utilerias.SistemaTransportes
Imports SistemaTransportes.EntidadNegocio.Catalogos
Imports SistemaTransportes.AccesoDatos.Catalogos
Imports Utilerias.BaseDatos.OracleServer
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling

Namespace SistemaTransportes.LogicaNegocio.Catalogos
	Public Class BllStmServicio
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
		''' Permite agregar un registro en la tabla STM_SERVICIO, no sin antes aplicar la validación de la llave primaria o alterna según corresponda
		''' </summary>
		''' <param name="pvo_Registro">Entidad a procesar</param>
		''' <returns>Mayor a cero: El proceso ha sido exitoso; el valor puede corresponder a la llave autogenerada. Menor a cero: El proceso ha fallado</returns>
		''' <author>Generador de Código basado en objetos Oracle</author>
		''' <creationDate>18/5/2017 2:47:15 p. m.</creationDate>
		''' <changeLog></changeLog>
		Public Function InsertarRegistro(ByVal pvo_Registro As EntStmServicio) As Integer
			Dim vlo_Conexion As ConexionOracle
			Dim vlb_LiberarConexion As Boolean
			Dim vlo_DalStmServicio As DalStmServicio
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
					Throw New SistemaTransportesException("[TODO] Llave alterna repetida")
				End If

				vlo_DalStmServicio = New DalStmServicio(vlo_Conexion)
				vln_Resultado = vlo_DalStmServicio.InsertarRegistro(pvo_Registro)
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
		''' Permite modificar un registro en la tabla STM_SERVICIO, no sin antes aplicar la validación de la llave alterna
		''' </summary>
		''' <param name="pvo_Registro">Entidad a procesar</param>
		''' <returns>Mayor a cero: El proceso ha sido exitoso. Menor a cero: El proceso ha fallado</returns>
		''' <author>Generador de Código basado en objetos Oracle</author>
		''' <creationDate>18/5/2017 2:47:15 p. m.</creationDate>
		''' <changeLog></changeLog>
		Public Function ModificarRegistro(ByVal pvo_Registro As EntStmServicio) As Integer
			Dim vlo_Conexion As ConexionOracle
			Dim vlb_LiberarConexion As Boolean
			Dim vlo_DalStmServicio As DalStmServicio
			Dim vlo_EntStmServicio As EntStmServicio
			Dim vln_Resultado As Integer

			Try
				If vgo_Conexion Is Nothing Then
					vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
					vlb_LiberarConexion = True
				Else
					vlo_Conexion = vgo_Conexion
					vlb_LiberarConexion = False
				End If

				vlo_EntStmServicio = ObtenerRegistroPorLlaveAlterna(pvo_Registro.Descripcion)
				If vlo_EntStmServicio.Existe AndAlso vlo_EntStmServicio.IdServicio <> pvo_Registro.IdServicio Then
					vln_Resultado = -1
					Throw New SistemaTransportesException("[TODO] Llave alterna repetida")
				End If

				vlo_DalStmServicio = New DalStmServicio(vlo_Conexion)
				vln_Resultado = vlo_DalStmServicio.ModificarRegistro(pvo_Registro)
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
		''' <param name="pvc_Descripcion">Almacena la descripcion del registro</param>
		''' <returns>Entidad que representa la información obtenida al ejecutar la consulta</returns>
		''' <author>Generador de Código basado en objetos Oracle</author>
		''' <creationDate>18/5/2017 2:47:15 p. m.</creationDate>
		''' <changeLog></changeLog>
		Private Function ObtenerRegistroPorLlaveAlterna(pvc_Descripcion As String) As EntStmServicio
			Dim vlo_Conexion As ConexionOracle
			Dim vlb_LiberarConexion As Boolean
			Dim vlo_DalStmServicio As DalStmServicio

			Try
				If vgo_Conexion Is Nothing Then
					vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
					vlb_LiberarConexion = True
				Else
					vlo_Conexion = vgo_Conexion
					vlb_LiberarConexion = False
				End If

				vlo_DalStmServicio = New DalStmServicio(vlo_Conexion)
				Return vlo_DalStmServicio.ObtenerRegistro(String.Format("UPPER({0}) = '{1}'", Modelo.STM_SERVICIO.DESCRIPCION, pvc_Descripcion.ToUpper()))
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
