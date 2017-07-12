Imports Utilerias.OrdenesDeTrabajo
Imports OrdenesDeTrabajo.EntidadNegocio.OrdenesDeTrabajo
Imports OrdenesDeTrabajo.AccesoDatos.OrdenesDeTrabajo
Imports Utilerias.BaseDatos.OracleServer
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling

Namespace OrdenesDeTrabajo.LogicaNegocio.OrdenesDeTrabajo
	Public Class BllOttOfertaProveedor
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
		''' Permite agregar un registro en la tabla OTT_OFERTA_PROVEEDOR, no sin antes aplicar la validación de la llave primaria o alterna según corresponda
		''' </summary>
		''' <param name="pvo_Registro">Entidad a procesar</param>
		''' <returns>Mayor a cero: El proceso ha sido exitoso; el valor puede corresponder a la llave autogenerada. Menor a cero: El proceso ha fallado</returns>
		''' <author>Generador de Código basado en objetos Oracle</author>
		''' <creationDate>25/08/2016 12:17:45 p.m.</creationDate>
		''' <changeLog></changeLog>
		Public Function InsertarRegistro(ByVal pvo_Registro As EntOttOfertaProveedor) As Integer
			Dim vlo_Conexion As ConexionOracle
			Dim vlb_LiberarConexion As Boolean
			Dim vlo_DalOttOfertaProveedor As DalOttOfertaProveedor
			Dim vln_Resultado As Integer

			Try
				If vgo_Conexion Is Nothing Then
					vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
					vlb_LiberarConexion = True
				Else
					vlo_Conexion = vgo_Conexion
					vlb_LiberarConexion = False
				End If

				If ObtenerRegistroPorLlavePrimaria(pvo_Registro.IdUbicacion, pvo_Registro.IdViaCompraContrato, pvo_Registro.Anno, pvo_Registro.NumeroGestion, pvo_Registro.IdGrupoGestionCompra, pvo_Registro.Identificacion).Existe Then
					vln_Resultado = -1
					Throw New OrdenesDeTrabajoException("[TODO] Llave primaria repetida")
				End If

				vlo_DalOttOfertaProveedor = New DalOttOfertaProveedor(vlo_Conexion)
				vln_Resultado = vlo_DalOttOfertaProveedor.InsertarRegistro(pvo_Registro)
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
		''' <param name="pvn_IdUbicacion">Llave primaria de la tabla otm_ubicacion que se asocia con la secuencia sq_id_ubicacion</param>
		''' <param name="pvn_IdViaCompraContrato">Llave primaria de la tabla otm_via_compra_contrato que se asocia con la secuencia sq_id_via_compra_contrato</param>
		''' <param name="pvn_Anno">Año</param>
		''' <param name="pvn_NumeroGestion">Consecutivo de la gestión</param>
		''' <param name="pvn_IdGrupoGestionCompra">Llave de la tabla ott_grupo_gestion_compra asociada a la secuencia sq_id_grupo_gestion_compra</param>
		''' <param name="pvc_Identificacion">Identificación del proveedor (física o jurídica)</param>
		''' <returns>Entidad que representa la información obtenida al ejecutar la consulta</returns>
		''' <author>Generador de Código basado en objetos Oracle</author>
		''' <creationDate>25/08/2016 12:17:45 p.m.</creationDate>
		''' <changeLog></changeLog>
		Private Function ObtenerRegistroPorLlavePrimaria(pvn_IdUbicacion As Integer, pvn_IdViaCompraContrato As Integer, pvn_Anno As Integer, pvn_NumeroGestion As Integer, pvn_IdGrupoGestionCompra As Integer, pvc_Identificacion As String) As EntOttOfertaProveedor
			Dim vlo_Conexion As ConexionOracle
			Dim vlb_LiberarConexion As Boolean
			Dim vlo_DalOttOfertaProveedor As DalOttOfertaProveedor

			Try
				If vgo_Conexion Is Nothing Then
					vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
					vlb_LiberarConexion = True
				Else
					vlo_Conexion = vgo_Conexion
					vlb_LiberarConexion = False
				End If

				vlo_DalOttOfertaProveedor = New DalOttOfertaProveedor(vlo_Conexion)
				Return vlo_DalOttOfertaProveedor.ObtenerRegistro(String.Format("{0} = {1} AND {2} = {3} AND {4} = {5} AND {6} = {7} AND {8} = {9} AND UPPER({10}) = '{11}'", Modelo.OTT_OFERTA_PROVEEDOR.ID_UBICACION, pvn_IdUbicacion, Modelo.OTT_OFERTA_PROVEEDOR.ID_VIA_COMPRA_CONTRATO, pvn_IdViaCompraContrato, Modelo.OTT_OFERTA_PROVEEDOR.ANNO, pvn_Anno, Modelo.OTT_OFERTA_PROVEEDOR.NUMERO_GESTION, pvn_NumeroGestion, Modelo.OTT_OFERTA_PROVEEDOR.ID_GRUPO_GESTION_COMPRA, pvn_IdGrupoGestionCompra, Modelo.OTT_OFERTA_PROVEEDOR.IDENTIFICACION, pvc_Identificacion.ToUpper()))
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
