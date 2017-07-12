Imports Utilerias.OrdenesDeTrabajo
Imports OrdenesDeTrabajo.EntidadNegocio.OrdenesDeTrabajo
Imports OrdenesDeTrabajo.AccesoDatos.OrdenesDeTrabajo
Imports Utilerias.BaseDatos.OracleServer
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling

Namespace OrdenesDeTrabajo.LogicaNegocio.OrdenesDeTrabajo
	Public Class BllOttDocumentoAnteproyect
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
		''' Permite agregar un registro en la tabla OTT_DOCUMENTO_ANTEPROYECT, no sin antes aplicar la validación de la llave primaria o alterna según corresponda
		''' </summary>
		''' <param name="pvo_Registro">Entidad a procesar</param>
		''' <returns>Mayor a cero: El proceso ha sido exitoso; el valor puede corresponder a la llave autogenerada. Menor a cero: El proceso ha fallado</returns>
		''' <author>Generador de Código basado en objetos Oracle</author>
		''' <creationDate>29/02/2016 11:26:35 a.m.</creationDate>
		''' <changeLog></changeLog>
		Public Function InsertarRegistro(ByVal pvo_Registro As EntOttDocumentoAnteproyect) As Integer
			Dim vlo_Conexion As ConexionOracle
			Dim vlb_LiberarConexion As Boolean
			Dim vlo_DalOttDocumentoAnteproyect As DalOttDocumentoAnteproyect
			Dim vln_Resultado As Integer

			Try
				If vgo_Conexion Is Nothing Then
					vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
					vlb_LiberarConexion = True
				Else
					vlo_Conexion = vgo_Conexion
					vlb_LiberarConexion = False
				End If

				If ObtenerRegistroPorLlavePrimaria(pvo_Registro.IdTipoDocumento, pvo_Registro.IdEtapaOrdenTrabajo, pvo_Registro.IdAdjuntoOrdenTrabajo, pvo_Registro.IdUbicacion, pvo_Registro.IdOrdenTrabajo, pvo_Registro.Version).Existe Then
					vln_Resultado = -1
                    Throw New OrdenesDeTrabajoException("Llave primaria repetida")
				End If

				vlo_DalOttDocumentoAnteproyect = New DalOttDocumentoAnteproyect(vlo_Conexion)
				vln_Resultado = vlo_DalOttDocumentoAnteproyect.InsertarRegistro(pvo_Registro)
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
		''' <param name="pvn_IdTipoDocumento">Llave primaria de la tabla otm_tipo_documento que se asocia con la secuencia sq_id_tipo_documento</param>
		''' <param name="pvn_IdEtapaOrdenTrabajo">Llave primaria de la tabla otm_etapa_orden_trabajo que se asocia con la secuencia sq_id_etapa_orden_trabajo</param>
		''' <param name="pvn_IdAdjuntoOrdenTrabajo">Llave primaria de la tabla otf_adjunto_orden_trabajo que se asocia con la secuencia sq_id_adjunto_orden_trabajo</param>
		''' <param name="pvn_IdUbicacion">Llave primaria de la tabla otm_ubicacion que se asocia con la secuencia sq_id_ubicacion</param>
		''' <param name="pvc_IdOrdenTrabajo">Identificador único alfanumérico de la orden de trabajo</param>
		''' <param name="pvn_Version">Numero de version del anteproyecto asociado a una orden de trabajo</param>
		''' <returns>Entidad que representa la información obtenida al ejecutar la consulta</returns>
		''' <author>Generador de Código basado en objetos Oracle</author>
		''' <creationDate>29/02/2016 11:26:35 a.m.</creationDate>
		''' <changeLog></changeLog>
		Private Function ObtenerRegistroPorLlavePrimaria(pvn_IdTipoDocumento As Integer, pvn_IdEtapaOrdenTrabajo As Integer, pvn_IdAdjuntoOrdenTrabajo As Integer, pvn_IdUbicacion As Integer, pvc_IdOrdenTrabajo As String, pvn_Version As Integer) As EntOttDocumentoAnteproyect
			Dim vlo_Conexion As ConexionOracle
			Dim vlb_LiberarConexion As Boolean
			Dim vlo_DalOttDocumentoAnteproyect As DalOttDocumentoAnteproyect

			Try
				If vgo_Conexion Is Nothing Then
					vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
					vlb_LiberarConexion = True
				Else
					vlo_Conexion = vgo_Conexion
					vlb_LiberarConexion = False
				End If

				vlo_DalOttDocumentoAnteproyect = New DalOttDocumentoAnteproyect(vlo_Conexion)
				Return vlo_DalOttDocumentoAnteproyect.ObtenerRegistro(String.Format("{0} = {1} AND {2} = {3} AND {4} = {5} AND {6} = {7} AND UPPER({8}) = '{9}' AND {10} = {11}", Modelo.OTT_DOCUMENTO_ANTEPROYECT.ID_TIPO_DOCUMENTO, pvn_IdTipoDocumento, Modelo.OTT_DOCUMENTO_ANTEPROYECT.ID_ETAPA_ORDEN_TRABAJO, pvn_IdEtapaOrdenTrabajo, Modelo.OTT_DOCUMENTO_ANTEPROYECT.ID_ADJUNTO_ORDEN_TRABAJO, pvn_IdAdjuntoOrdenTrabajo, Modelo.OTT_DOCUMENTO_ANTEPROYECT.ID_UBICACION, pvn_IdUbicacion, Modelo.OTT_DOCUMENTO_ANTEPROYECT.ID_ORDEN_TRABAJO, pvc_IdOrdenTrabajo.ToUpper(), Modelo.OTT_DOCUMENTO_ANTEPROYECT.VERSION, pvn_Version))
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
