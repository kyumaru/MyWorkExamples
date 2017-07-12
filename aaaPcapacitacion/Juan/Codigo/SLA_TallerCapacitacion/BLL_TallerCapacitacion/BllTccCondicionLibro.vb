Imports Utilerias.TallerCapacitacion
Imports TallerCapacitacion.EntidadNegocio.Catalogos
Imports TallerCapacitacion.AccesoDatos.Catalogos
Imports Utilerias.BaseDatos.OracleServer
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling

Namespace TallerCapacitacion.LogicaNegocio.Catalogos
	Public Class BllTccCondicionLibro
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
		''' Permite agregar un registro en la tabla TCC_CONDICION_LIBRO, no sin antes aplicar la validación de la llave primaria o alterna según corresponda
		''' </summary>
		''' <param name="pvo_Registro">Entidad a procesar</param>
		''' <returns>Mayor a cero: El proceso ha sido exitoso; el valor puede corresponder a la llave autogenerada. Menor a cero: El proceso ha fallado</returns>
		''' <author>Generador de Código basado en objetos Oracle</author>
		''' <creationDate>4/5/2017 4:34:41 p. m.</creationDate>
		''' <changeLog></changeLog>
		Public Function InsertarRegistro(ByVal pvo_Registro As EntTccCondicionLibro) As Integer
			Dim vlo_Conexion As ConexionOracle
			Dim vlb_LiberarConexion As Boolean
			Dim vlo_DalTccCondicionLibro As DalTccCondicionLibro
			Dim vln_Resultado As Integer

			Try
				If vgo_Conexion Is Nothing Then
					vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
					vlb_LiberarConexion = True
				Else
					vlo_Conexion = vgo_Conexion
					vlb_LiberarConexion = False
				End If

				If ObtenerRegistroPorLlavePrimaria(pvo_Registro.CondicionLibro).Existe Then
					vln_Resultado = -1
					Throw New TallerCapacitacionException("[TODO] Llave primaria repetida")
				End If

				vlo_DalTccCondicionLibro = New DalTccCondicionLibro(vlo_Conexion)
				vln_Resultado = vlo_DalTccCondicionLibro.InsertarRegistro(pvo_Registro)
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
		''' Permite borrar un registro en la tabla TCC_CONDICION_LIBRO, no sin antes aplicar la validación de dependencia con tablas relacionadas
		''' </summary>
		''' <param name="pvo_Registro">Entidad a procesar</param>
		''' <returns>Mayor a cero: El proceso ha sido exitoso. Menor a cero: El proceso ha fallado</returns>
		''' <author>Generador de Código basado en objetos Oracle</author>
		''' <creationDate>4/5/2017 4:34:41 p. m.</creationDate>
		''' <changeLog></changeLog>
		Public Function BorrarRegistro(ByVal pvo_Registro As EntTccCondicionLibro) As Integer
			Dim vlo_Conexion As ConexionOracle
			Dim vlb_LiberarConexion As Boolean
			Dim vlo_DalTccCondicionLibro As DalTccCondicionLibro
			Dim vln_Resultado As Integer

			Try
				If vgo_Conexion Is Nothing Then
					vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
					vlb_LiberarConexion = True
				Else
					vlo_Conexion = vgo_Conexion
					vlb_LiberarConexion = False
				End If

				If PoseeRegistrosAsociados(pvo_Registro.CondicionLibro) Then
					vln_Resultado = -1
					Throw New TallerCapacitacionException("[TODO] Hay registro asociados")
				End If

				vlo_DalTccCondicionLibro = New DalTccCondicionLibro(vlo_Conexion)
				vln_Resultado = vlo_DalTccCondicionLibro.BorrarRegistro(pvo_Registro)
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
		''' <param name="pvc_CondicionLibro">Abreviatura que identifica la condición de un libro</param>
		''' <returns>Entidad que representa la información obtenida al ejecutar la consulta</returns>
		''' <author>Generador de Código basado en objetos Oracle</author>
		''' <creationDate>4/5/2017 4:34:41 p. m.</creationDate>
		''' <changeLog></changeLog>
		Private Function ObtenerRegistroPorLlavePrimaria(pvc_CondicionLibro As String) As EntTccCondicionLibro
			Dim vlo_Conexion As ConexionOracle
			Dim vlb_LiberarConexion As Boolean
			Dim vlo_DalTccCondicionLibro As DalTccCondicionLibro

			Try
				If vgo_Conexion Is Nothing Then
					vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
					vlb_LiberarConexion = True
				Else
					vlo_Conexion = vgo_Conexion
					vlb_LiberarConexion = False
				End If

				vlo_DalTccCondicionLibro = New DalTccCondicionLibro(vlo_Conexion)
				Return vlo_DalTccCondicionLibro.ObtenerRegistro(String.Format("UPPER({0}) = '{1}'", Modelo.TCC_CONDICION_LIBRO.CONDICION_LIBRO, pvc_CondicionLibro.ToUpper()))
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
		''' <param name="pvc_CondicionLibro">Abreviatura que identifica la condición de un libro</param>
		''' <returns>True: posee datos asociados. False: No posee datos asociados</returns>
		''' <author>Generador de Código basado en objetos Oracle</author>
		''' <creationDate>4/5/2017 4:34:41 p. m.</creationDate>
		''' <changeLog></changeLog>
		Private Function PoseeRegistrosAsociados(pvc_CondicionLibro As String) As Boolean
			Dim vlo_Conexion As ConexionOracle
			Dim vlb_LiberarConexion As Boolean
			Dim vlo_PoseeRegistrosAsociados As Boolean
			Dim vlo_DalTcmLibro As DalTcmLibro

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

				'Determinar la existencia de registros asociados en la tabla TCM_LIBRO
				vlo_DalTcmLibro = New DalTcmLibro(vlo_Conexion)
				If vlo_DalTcmLibro.ObtenerRegistro(String.Format("UPPER({0}) = '{1}'", Modelo.TCM_LIBRO.CONDICION_LIBRO, pvc_CondicionLibro.ToUpper())).Existe Then
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
