Imports Utilerias.TallerCapacitacion
Imports TallerCapacitacion.EntidadNegocio.Catalogos
Imports TallerCapacitacion.AccesoDatos.Catalogos
Imports Utilerias.BaseDatos.OracleServer
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling

Namespace TallerCapacitacion.LogicaNegocio.Catalogos
	Public Class BllTcmAutorPorLibro
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
		''' Permite agregar un registro en la tabla TCM_AUTOR_POR_LIBRO, no sin antes aplicar la validación de la llave primaria o alterna según corresponda
		''' </summary>
		''' <param name="pvo_Registro">Entidad a procesar</param>
		''' <returns>Mayor a cero: El proceso ha sido exitoso; el valor puede corresponder a la llave autogenerada. Menor a cero: El proceso ha fallado</returns>
		''' <author>Generador de Código basado en objetos Oracle</author>
		''' <creationDate>4/5/2017 1:54:32 p. m.</creationDate>
		''' <changeLog></changeLog>
		Public Function InsertarRegistro(ByVal pvo_Registro As EntTcmAutorPorLibro) As Integer
			Dim vlo_Conexion As ConexionOracle
			Dim vlb_LiberarConexion As Boolean
			Dim vlo_DalTcmAutorPorLibro As DalTcmAutorPorLibro
			Dim vln_Resultado As Integer

			Try
				If vgo_Conexion Is Nothing Then
					vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
					vlb_LiberarConexion = True
				Else
					vlo_Conexion = vgo_Conexion
					vlb_LiberarConexion = False
				End If

				If ObtenerRegistroPorLlavePrimaria(pvo_Registro.Isbn, pvo_Registro.IdPersonal).Existe Then
					vln_Resultado = -1
					Throw New TallerCapacitacionException("[TODO] Llave primaria repetida")
				End If

				vlo_DalTcmAutorPorLibro = New DalTcmAutorPorLibro(vlo_Conexion)
				vln_Resultado = vlo_DalTcmAutorPorLibro.InsertarRegistro(pvo_Registro)
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
		''' <param name="pvc_Isbn">Número estándar internacional de libros. es un identificador único para libros previsto para uso comercial, los hay de 10 y 13 dígitos</param>
		''' <param name="pvc_IdPersonal">Número de identificación</param>
		''' <returns>Entidad que representa la información obtenida al ejecutar la consulta</returns>
		''' <author>Generador de Código basado en objetos Oracle</author>
		''' <creationDate>4/5/2017 1:54:32 p. m.</creationDate>
		''' <changeLog></changeLog>
		Private Function ObtenerRegistroPorLlavePrimaria(pvc_Isbn As String, pvc_IdPersonal As String) As EntTcmAutorPorLibro
			Dim vlo_Conexion As ConexionOracle
			Dim vlb_LiberarConexion As Boolean
			Dim vlo_DalTcmAutorPorLibro As DalTcmAutorPorLibro

			Try
				If vgo_Conexion Is Nothing Then
					vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
					vlb_LiberarConexion = True
				Else
					vlo_Conexion = vgo_Conexion
					vlb_LiberarConexion = False
				End If

				vlo_DalTcmAutorPorLibro = New DalTcmAutorPorLibro(vlo_Conexion)
				Return vlo_DalTcmAutorPorLibro.ObtenerRegistro(String.Format("UPPER({0}) = '{1}' AND UPPER({2}) = '{3}'", Modelo.TCM_AUTOR_POR_LIBRO.ISBN, pvc_Isbn.ToUpper(), Modelo.TCM_AUTOR_POR_LIBRO.ID_PERSONAL, pvc_IdPersonal.ToUpper()))
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
