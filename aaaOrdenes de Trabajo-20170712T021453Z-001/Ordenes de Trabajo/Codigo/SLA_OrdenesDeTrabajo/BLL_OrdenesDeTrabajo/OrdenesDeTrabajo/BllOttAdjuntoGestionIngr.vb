Imports Utilerias.OrdenesDeTrabajo
Imports OrdenesDeTrabajo.EntidadNegocio.OrdenesDeTrabajo
Imports OrdenesDeTrabajo.AccesoDatos.OrdenesDeTrabajo
Imports Utilerias.BaseDatos.OracleServer
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling

Namespace OrdenesDeTrabajo.LogicaNegocio.OrdenesDeTrabajo
	Public Class BllOttAdjuntoGestionIngr
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
		''' Permite borrar un registro en la tabla OTT_ADJUNTO_GESTION_INGR, no sin antes aplicar la validación de dependencia con tablas relacionadas
		''' </summary>
		''' <param name="pvo_Registro">Entidad a procesar</param>
		''' <returns>Mayor a cero: El proceso ha sido exitoso. Menor a cero: El proceso ha fallado</returns>
		''' <author>Generador de Código basado en objetos Oracle</author>
		''' <creationDate>01/02/2017 04:28:02 p.m.</creationDate>
		''' <changeLog></changeLog>
		Public Function BorrarRegistro(ByVal pvo_Registro As EntOttAdjuntoGestionIngr) As Integer
			Dim vlo_Conexion As ConexionOracle
			Dim vlb_LiberarConexion As Boolean
			Dim vlo_DalOttAdjuntoGestionIngr As DalOttAdjuntoGestionIngr
			Dim vln_Resultado As Integer

			Try
				If vgo_Conexion Is Nothing Then
					vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
					vlb_LiberarConexion = True
				Else
					vlo_Conexion = vgo_Conexion
					vlb_LiberarConexion = False
				End If

				If PoseeRegistrosAsociados(pvo_Registro.IdAdjuntoGestionIngr) Then
					vln_Resultado = -1
					Throw New OrdenesDeTrabajoException("[TODO] Hay registro asociados")
				End If

				vlo_DalOttAdjuntoGestionIngr = New DalOttAdjuntoGestionIngr(vlo_Conexion)
				vln_Resultado = vlo_DalOttAdjuntoGestionIngr.BorrarRegistro(pvo_Registro)
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
		''' Verifica si un registro posee datos asociados en las tablas hijas
		''' </summary>
		''' <param name="pvn_IdAdjuntoGestionIngr">Llave primaria de la tabla ott_adjunto_gestion_ingr asociada a la secuencia sq_id_adjunto_gestion_ingr</param>
		''' <returns>True: posee datos asociados. False: No posee datos asociados</returns>
		''' <author>Generador de Código basado en objetos Oracle</author>
		''' <creationDate>01/02/2017 04:28:02 p.m.</creationDate>
		''' <changeLog></changeLog>
		Private Function PoseeRegistrosAsociados(pvn_IdAdjuntoGestionIngr As Integer) As Boolean
			Dim vlo_Conexion As ConexionOracle
			Dim vlb_LiberarConexion As Boolean
			Dim vlo_PoseeRegistrosAsociados As Boolean
			Dim vlo_DalOttDetalleGestionIngr As DalOttDetalleGestionIngr

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

				'Determinar la existencia de registros asociados en la tabla OTT_DETALLE_GESTION_INGR
				vlo_DalOttDetalleGestionIngr = New DalOttDetalleGestionIngr(vlo_Conexion)
				If vlo_DalOttDetalleGestionIngr.ObtenerRegistro(String.Format("{0} = {1}", Modelo.OTT_DETALLE_GESTION_INGR.ID_ADJUNTO_GESTION_INGR, pvn_IdAdjuntoGestionIngr)).Existe Then
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
