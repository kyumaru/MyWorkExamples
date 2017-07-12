Imports Utilerias.OrdenesDeTrabajo
Imports OrdenesDeTrabajo.EntidadNegocio.Catalogos
Imports OrdenesDeTrabajo.AccesoDatos.Catalogos
Imports Utilerias.BaseDatos.OracleServer
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling

Namespace OrdenesDeTrabajo.LogicaNegocio.Catalogos
	Public Class BllOtpParametro
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
		''' Permite agregar un registro en la tabla OTP_PARAMETRO, no sin antes aplicar la validación de la llave primaria o alterna según corresponda
		''' </summary>
		''' <param name="pvo_Registro">Entidad a procesar</param>
		''' <returns>Mayor a cero: El proceso ha sido exitoso; el valor puede corresponder a la llave autogenerada. Menor a cero: El proceso ha fallado</returns>
		''' <author>Generador de Código basado en objetos Oracle</author>
		''' <creationDate>09/09/2015 01:54:27 p.m.</creationDate>
		''' <changeLog></changeLog>
		Public Function InsertarRegistro(ByVal pvo_Registro As EntOtpParametro) As Integer
			Dim vlo_Conexion As ConexionOracle
			Dim vlb_LiberarConexion As Boolean
			Dim vlo_DalOtpParametro As DalOtpParametro
			Dim vln_Resultado As Integer

			Try
				If vgo_Conexion Is Nothing Then
					vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
					vlb_LiberarConexion = True
				Else
					vlo_Conexion = vgo_Conexion
					vlb_LiberarConexion = False
				End If

				If ObtenerRegistroPorLlavePrimaria(pvo_Registro.IdParametro).Existe Then
					vln_Resultado = -1
                    Throw New OrdenesDeTrabajoException("Llave primaria repetida")
				End If

				If ObtenerRegistroPorLlaveAlterna(pvo_Registro.Descripcion).Existe Then
					vln_Resultado = -1
                    Throw New OrdenesDeTrabajoException("Llave alterna repetida")
				End If

				vlo_DalOtpParametro = New DalOtpParametro(vlo_Conexion)
				vln_Resultado = vlo_DalOtpParametro.InsertarRegistro(pvo_Registro)
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
		''' Permite modificar un registro en la tabla OTP_PARAMETRO, no sin antes aplicar la validación de la llave alterna
		''' </summary>
		''' <param name="pvo_Registro">Entidad a procesar</param>
		''' <returns>Mayor a cero: El proceso ha sido exitoso. Menor a cero: El proceso ha fallado</returns>
		''' <author>Generador de Código basado en objetos Oracle</author>
		''' <creationDate>09/09/2015 01:54:27 p.m.</creationDate>
		''' <changeLog></changeLog>
		Public Function ModificarRegistro(ByVal pvo_Registro As EntOtpParametro) As Integer
			Dim vlo_Conexion As ConexionOracle
			Dim vlb_LiberarConexion As Boolean
			Dim vlo_DalOtpParametro As DalOtpParametro
			Dim vlo_EntOtpParametro As EntOtpParametro
			Dim vln_Resultado As Integer

			Try
				If vgo_Conexion Is Nothing Then
					vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
					vlb_LiberarConexion = True
				Else
					vlo_Conexion = vgo_Conexion
					vlb_LiberarConexion = False
				End If

				vlo_EntOtpParametro = ObtenerRegistroPorLlaveAlterna(pvo_Registro.Descripcion)
				If vlo_EntOtpParametro.Existe AndAlso vlo_EntOtpParametro.IdParametro <> pvo_Registro.IdParametro Then
					vln_Resultado = -1
                    Throw New OrdenesDeTrabajoException("No se puede modificar el registro, ya existe un parámetro con la descripción indicada.")
				End If

				vlo_DalOtpParametro = New DalOtpParametro(vlo_Conexion)
				vln_Resultado = vlo_DalOtpParametro.ModificarRegistro(pvo_Registro)
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
		''' <param name="pvn_IdParametro">Llave primaria de la tabla otp_parametro</param>
		''' <returns>Entidad que representa la información obtenida al ejecutar la consulta</returns>
		''' <author>Generador de Código basado en objetos Oracle</author>
		''' <creationDate>09/09/2015 01:54:27 p.m.</creationDate>
		''' <changeLog></changeLog>
		Private Function ObtenerRegistroPorLlavePrimaria(pvn_IdParametro As Integer) As EntOtpParametro
			Dim vlo_Conexion As ConexionOracle
			Dim vlb_LiberarConexion As Boolean
			Dim vlo_DalOtpParametro As DalOtpParametro

			Try
				If vgo_Conexion Is Nothing Then
					vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
					vlb_LiberarConexion = True
				Else
					vlo_Conexion = vgo_Conexion
					vlb_LiberarConexion = False
				End If

				vlo_DalOtpParametro = New DalOtpParametro(vlo_Conexion)
				Return vlo_DalOtpParametro.ObtenerRegistro(String.Format("{0} = {1}", Modelo.OTP_PARAMETRO.ID_PARAMETRO, pvn_IdParametro))
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
		''' <param name="pvc_Descripcion">Descripción del parámetro</param>
		''' <returns>Entidad que representa la información obtenida al ejecutar la consulta</returns>
		''' <author>Generador de Código basado en objetos Oracle</author>
		''' <creationDate>09/09/2015 01:54:27 p.m.</creationDate>
		''' <changeLog></changeLog>
		Private Function ObtenerRegistroPorLlaveAlterna(pvc_Descripcion As String) As EntOtpParametro
			Dim vlo_Conexion As ConexionOracle
			Dim vlb_LiberarConexion As Boolean
			Dim vlo_DalOtpParametro As DalOtpParametro

			Try
				If vgo_Conexion Is Nothing Then
					vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
					vlb_LiberarConexion = True
				Else
					vlo_Conexion = vgo_Conexion
					vlb_LiberarConexion = False
				End If

				vlo_DalOtpParametro = New DalOtpParametro(vlo_Conexion)
				Return vlo_DalOtpParametro.ObtenerRegistro(String.Format("UPPER({0}) = '{1}'", Modelo.OTP_PARAMETRO.DESCRIPCION, pvc_Descripcion.ToUpper()))
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
