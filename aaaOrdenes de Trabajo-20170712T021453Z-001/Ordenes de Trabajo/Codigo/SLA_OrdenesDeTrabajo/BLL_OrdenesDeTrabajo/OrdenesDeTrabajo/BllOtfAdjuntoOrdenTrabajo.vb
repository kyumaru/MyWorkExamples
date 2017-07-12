Imports Utilerias.OrdenesDeTrabajo
Imports OrdenesDeTrabajo.EntidadNegocio.OrdenesDeTrabajo
Imports OrdenesDeTrabajo.AccesoDatos.OrdenesDeTrabajo
Imports Utilerias.BaseDatos.OracleServer
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling

Namespace OrdenesDeTrabajo.LogicaNegocio.OrdenesDeTrabajo
	Public Class BllOtfAdjuntoOrdenTrabajo
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
		''' Permite agregar un registro en la tabla OTF_ADJUNTO_ORDEN_TRABAJO, no sin antes aplicar la validación de la llave primaria o alterna según corresponda
		''' </summary>
		''' <param name="pvo_Registro">Entidad a procesar</param>
		''' <returns>Mayor a cero: El proceso ha sido exitoso; el valor puede corresponder a la llave autogenerada. Menor a cero: El proceso ha fallado</returns>
		''' <author>Generador de Código basado en objetos Oracle</author>
		''' <creationDate>31/08/2015 08:24:17 a.m.</creationDate>
		''' <changeLog></changeLog>
		Public Function InsertarRegistro(ByVal pvo_Registro As EntOtfAdjuntoOrdenTrabajo) As Integer
			Dim vlo_Conexion As ConexionOracle
			Dim vlb_LiberarConexion As Boolean
			Dim vlo_DalOtfAdjuntoOrdenTrabajo As DalOtfAdjuntoOrdenTrabajo
			Dim vln_Resultado As Integer

			Try
				If vgo_Conexion Is Nothing Then
					vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
					vlb_LiberarConexion = True
				Else
					vlo_Conexion = vgo_Conexion
					vlb_LiberarConexion = False
				End If

				If ObtenerRegistroPorLlavePrimaria(pvo_Registro.Anno, pvo_Registro.Consecutivo, pvo_Registro.IdAdjuntoOrdenTrabajo).Existe Then
					vln_Resultado = -1
                    Throw New OrdenesDeTrabajoException("Llave primaria repetida")
				End If

				vlo_DalOtfAdjuntoOrdenTrabajo = New DalOtfAdjuntoOrdenTrabajo(vlo_Conexion)
				vln_Resultado = vlo_DalOtfAdjuntoOrdenTrabajo.InsertarRegistro(pvo_Registro)
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
		''' <param name="pvn_Anno">Año de solicitud de la ot</param>
		''' <param name="pvn_Consecutivo">Consecutivo de orden de trabajo, se reinicia cada año.</param>
		''' <param name="pvn_IdAdjuntoOrdenTrabajo">Llave primaria de la tabla otf_adjunto_orden_trabajo que se asocia con la secuencia sq_id_adjunto_orden_trabajo</param>
		''' <returns>Entidad que representa la información obtenida al ejecutar la consulta</returns>
		''' <author>Generador de Código basado en objetos Oracle</author>
		''' <creationDate>31/08/2015 08:24:17 a.m.</creationDate>
		''' <changeLog></changeLog>
		Private Function ObtenerRegistroPorLlavePrimaria(pvn_Anno As Integer, pvn_Consecutivo As Integer, pvn_IdAdjuntoOrdenTrabajo As Integer) As EntOtfAdjuntoOrdenTrabajo
			Dim vlo_Conexion As ConexionOracle
			Dim vlb_LiberarConexion As Boolean
			Dim vlo_DalOtfAdjuntoOrdenTrabajo As DalOtfAdjuntoOrdenTrabajo

			Try
				If vgo_Conexion Is Nothing Then
					vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
					vlb_LiberarConexion = True
				Else
					vlo_Conexion = vgo_Conexion
					vlb_LiberarConexion = False
				End If

				vlo_DalOtfAdjuntoOrdenTrabajo = New DalOtfAdjuntoOrdenTrabajo(vlo_Conexion)
				Return vlo_DalOtfAdjuntoOrdenTrabajo.ObtenerRegistro(String.Format("{0} = {1} AND {2} = {3} AND {4} = {5}", Modelo.OTF_ADJUNTO_ORDEN_TRABAJO.ANNO, pvn_Anno, Modelo.OTF_ADJUNTO_ORDEN_TRABAJO.CONSECUTIVO, pvn_Consecutivo, Modelo.OTF_ADJUNTO_ORDEN_TRABAJO.ID_ADJUNTO_ORDEN_TRABAJO, pvn_IdAdjuntoOrdenTrabajo))
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
