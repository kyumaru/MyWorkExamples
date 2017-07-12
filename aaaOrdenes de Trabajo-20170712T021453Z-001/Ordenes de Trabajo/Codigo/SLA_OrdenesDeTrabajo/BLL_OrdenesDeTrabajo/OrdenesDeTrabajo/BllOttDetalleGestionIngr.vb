Imports Utilerias.OrdenesDeTrabajo
Imports OrdenesDeTrabajo.EntidadNegocio.OrdenesDeTrabajo
Imports OrdenesDeTrabajo.AccesoDatos.OrdenesDeTrabajo
Imports Utilerias.BaseDatos.OracleServer
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling

Namespace OrdenesDeTrabajo.LogicaNegocio.OrdenesDeTrabajo
	Public Class BllOttDetalleGestionIngr
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
		''' Permite agregar un registro en la tabla OTT_DETALLE_GESTION_INGR, no sin antes aplicar la validación de la llave primaria o alterna según corresponda
		''' </summary>
		''' <param name="pvo_Registro">Entidad a procesar</param>
		''' <returns>Mayor a cero: El proceso ha sido exitoso; el valor puede corresponder a la llave autogenerada. Menor a cero: El proceso ha fallado</returns>
		''' <author>Generador de Código basado en objetos Oracle</author>
		''' <creationDate>01/02/2017 04:28:02 p.m.</creationDate>
		''' <changeLog></changeLog>
		Public Function InsertarRegistro(ByVal pvo_Registro As EntOttDetalleGestionIngr) As Integer
			Dim vlo_Conexion As ConexionOracle
			Dim vlb_LiberarConexion As Boolean
			Dim vlo_DalOttDetalleGestionIngr As DalOttDetalleGestionIngr
			Dim vln_Resultado As Integer

			Try
				If vgo_Conexion Is Nothing Then
					vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
					vlb_LiberarConexion = True
				Else
					vlo_Conexion = vgo_Conexion
					vlb_LiberarConexion = False
				End If

				If ObtenerRegistroPorLlavePrimaria(pvo_Registro.IdAdjuntoGestionIngr, pvo_Registro.IdUbicacion, pvo_Registro.IdViaCompraContrato, pvo_Registro.NumeroGestion, pvo_Registro.Anno, pvo_Registro.IdLineaGestionCompra).Existe Then
					vln_Resultado = -1
					Throw New OrdenesDeTrabajoException("[TODO] Llave primaria repetida")
				End If

				vlo_DalOttDetalleGestionIngr = New DalOttDetalleGestionIngr(vlo_Conexion)
				vln_Resultado = vlo_DalOttDetalleGestionIngr.InsertarRegistro(pvo_Registro)
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
		''' <param name="pvn_IdAdjuntoGestionIngr">Llave primaria de la tabla ott_adjunto_gestion_ingr asociada a la secuencia sq_id_adjunto_gestion_ingr</param>
		''' <param name="pvn_IdUbicacion">Llave primaria de la tabla otm_ubicacion que se asocia con la secuencia sq_id_ubicacion</param>
		''' <param name="pvn_IdViaCompraContrato">Llave primaria de la tabla otm_via_compra_contrato que se asocia con la secuencia sq_id_via_compra_contrato</param>
		''' <param name="pvn_NumeroGestion">Consecutivo de la gestión</param>
		''' <param name="pvn_Anno">Año</param>
		''' <param name="pvn_IdLineaGestionCompra">Llave primaria de la tabla ott_linea_gestion_compra asociada a la secuencia sq_id_linea_gestion_compra</param>
		''' <returns>Entidad que representa la información obtenida al ejecutar la consulta</returns>
		''' <author>Generador de Código basado en objetos Oracle</author>
		''' <creationDate>01/02/2017 04:28:02 p.m.</creationDate>
		''' <changeLog></changeLog>
		Private Function ObtenerRegistroPorLlavePrimaria(pvn_IdAdjuntoGestionIngr As Integer, pvn_IdUbicacion As Integer, pvn_IdViaCompraContrato As Integer, pvn_NumeroGestion As Integer, pvn_Anno As Integer, pvn_IdLineaGestionCompra As Integer) As EntOttDetalleGestionIngr
			Dim vlo_Conexion As ConexionOracle
			Dim vlb_LiberarConexion As Boolean
			Dim vlo_DalOttDetalleGestionIngr As DalOttDetalleGestionIngr

			Try
				If vgo_Conexion Is Nothing Then
					vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
					vlb_LiberarConexion = True
				Else
					vlo_Conexion = vgo_Conexion
					vlb_LiberarConexion = False
				End If

				vlo_DalOttDetalleGestionIngr = New DalOttDetalleGestionIngr(vlo_Conexion)
				Return vlo_DalOttDetalleGestionIngr.ObtenerRegistro(String.Format("{0} = {1} AND {2} = {3} AND {4} = {5} AND {6} = {7} AND {8} = {9} AND {10} = {11}", Modelo.OTT_DETALLE_GESTION_INGR.ID_ADJUNTO_GESTION_INGR, pvn_IdAdjuntoGestionIngr, Modelo.OTT_DETALLE_GESTION_INGR.ID_UBICACION, pvn_IdUbicacion, Modelo.OTT_DETALLE_GESTION_INGR.ID_VIA_COMPRA_CONTRATO, pvn_IdViaCompraContrato, Modelo.OTT_DETALLE_GESTION_INGR.NUMERO_GESTION, pvn_NumeroGestion, Modelo.OTT_DETALLE_GESTION_INGR.ANNO, pvn_Anno, Modelo.OTT_DETALLE_GESTION_INGR.ID_LINEA_GESTION_COMPRA, pvn_IdLineaGestionCompra))
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
